using NAudio.Wave;
using Petr_RP_CestaKMaturite.Properties;
using System.ComponentModel;
using System.Diagnostics;

namespace Petr_RP_CestaKMaturite;

internal class Enemy : IDisposable {

    public bool disposed;

    public bool dead;
    public int health;
    public PictureBox pb;
    public bool moving;
    public bool moveLeft;
    public bool moveRight = true;
    public bool moveSwitch = true;
    public int xLeft;
    public int xRight;
    public int movementSpeed;
    public enum enemyType{
        Stark,
        Oberhofnerova,
        Hacek,
        Lemka,
        Sysalova,
        Secret,
    };

    public enemyType type;

    public PictureBox projectile;
    public bool projectileStop;
    public int projectileCooldown;
    public int projectileSpeedX;
    public int projectileSpeedY;
    public bool projectileGoDown;
    public bool projectileGoRight;
    public bool projectileParry;
    public bool projectileLeft;
    public bool projectileRight;
    public bool projectileUp;
    public bool projectileBirdFacingRight;
    public Point player;
    public bool hitImage = false;
    public bool facingRight = true;
    public bool differentAnimation = false;

    private System.Windows.Forms.Timer hitTimer;
    private System.Windows.Forms.Timer shootAnim;
    private int shootIndex; // Index pro timer
    public int shootAnimIndex = 1; // 0 - Idle , 1 - Charge, 2- Throw


    //Sysalova
    public static Stopwatch stopwatch;
    public static WaveOut basnicka;
    public static bool basnickaHit;
    public static WaveFileReader reader;

    static Enemy () {

        // Příprava pro Sysalovou
        ComponentResourceManager rm = new(typeof(Resources));
        Stream stream = (Stream)rm.GetObject("Sysalova_Basnicka");
        reader = new WaveFileReader(stream);
        basnicka = new();
        basnicka.Init(reader);
        basnicka.Play();
        basnicka.Pause();
        stopwatch = new();
    }

    public Enemy(int positionX, int positionY, int width, int height, Image image,
        int _health, bool _moving, int _xLeft, int _xRight, int _movementSpeed,
        enemyType _type, int _projectileCooldown, Panel scene) {
        //X,Y,Width,Height,Color,HP,Gravitation,Moving
        //color premenit na img
        projectileCooldown = _projectileCooldown;
        type = _type;
        health = _health;
        moving = _moving;
        xLeft = _xLeft;
        xRight = _xRight;
        movementSpeed = _movementSpeed;
        pb = new PictureBox {
            Left = positionX,
            Top = positionY,
            Width = width,
            Height = height,
            Tag = "Enemy",
            SizeMode = PictureBoxSizeMode.StretchImage
        };

        if (image == null) {
            pb.BackColor = Color.Red;
        } else {
            pb.Image = image;
        }

        scene.Controls.Add(pb);

        if (type != enemyType.Stark)
            pb.BringToFront();
    }

    public void ShootProjectile(PictureBox Player, Panel panel) {
        if (projectile != null) {
            projectile.Bounds = Rectangle.Empty;
            panel.Controls.Remove(projectile);
            projectile.Dispose();
        }

        projectile = new() {
            Width = 30,
            Height = 30
        };

        if (type == enemyType.Oberhofnerova || type == enemyType.Stark) {
            if (type == enemyType.Oberhofnerova) {
                
                //random co hodi projektil
                switch (Random.Shared.Next(0, 8)) {
                    case 0: projectile.Image = Resources.OProjektil_5; break;
                    case 1: projectile.Image = Resources.OProjektil_binder; break;
                    case 2: projectile.Image = Resources.OProjektil_centropen; break;
                    case 3: projectile.Image = Resources.OProjektil_cos; break;
                    case 4: projectile.Image = Resources.OProjektil_guma; break;
                    case 5: projectile.Image = Resources.OProjektil_minus; break;
                    case 6: projectile.Image = Resources.OProjektil_plus; break;
                    case 7: projectile.Image = Resources.OProjektil_sin; break;
                }

                projectile.Left = pb.Left + 45;
                projectile.Top = pb.Top + 45;
                projectile.SetBounds(pb.Left + 45, pb.Top + 45, 30, 30);
            } else //Stark
              {
                projectile.Image = Resources.Stark_Chalk;
                projectile.SizeMode = PictureBoxSizeMode.StretchImage;
                projectile.Left = pb.Left + 40;
                projectile.Top = pb.Top + 60;
                projectile.Width = 60;
                projectile.Height = 80;
                projectile.SetBounds(pb.Left + 40, pb.Top + 60, 60, 80);
            }

            player = new Point(Player.Left + Player.Width / 2, Player.Top + Player.Height / 2);

            int projectileSpeed = 9;

            int projectileX = projectile.Left + projectile.Width / 2;
            int projectileY = projectile.Top + projectile.Height / 2;

            double uX = Math.Abs(player.X - projectileX);
            double uY = Math.Abs(player.Y - projectileY);

            double u = Math.Sqrt(Math.Pow(uX, 2) + Math.Pow(uY, 2));

            try
            {
                projectileSpeedX = Convert.ToInt32(uX / u * projectileSpeed);
                projectileSpeedY = Convert.ToInt32(uY / u * projectileSpeed);
            }
            catch
            {
                projectileSpeedX = 1;
                projectileSpeedY = 1;
            }

            projectileStop = false;

            projectileGoDown = projectileY < player.Y;
            projectileGoRight = projectileX < player.X;
        }
        if (type == enemyType.Hacek) {
            projectile.Left = pb.Left + 40;
            projectile.Top = pb.Top + 75;
            projectile.SetBounds(pb.Left + 40, pb.Top + 75, 30, 30);
            projectileStop = false;
        }

        //animace
        shootAnimIndex = 2;

        shootIndex = 0;
        shootAnim = new();
        shootAnim.Interval = 500;
        shootAnim.Tick += ShootAnim_Tick;
        shootAnim.Start();

        panel.Controls.Add(projectile);
        projectile.BringToFront();
    }

    private void ShootAnim_Tick(object? sender, EventArgs e) {
        if (shootIndex == 0) {
            shootAnimIndex = 0;
            shootAnim.Interval = projectileCooldown / 2;
        }else if (shootIndex == 1) {
            shootAnimIndex = 1;
            shootAnim.Stop();
            shootAnim.Dispose();
        }
        shootIndex++;
    }

    public void CheckHealth(Panel panel) {
        if (health <= 0) {
            Dispose();
        } else {
            // Obrazek hitu
            hitImage = true;

            if (hitTimer != null) {
                hitTimer.Stop();
                hitTimer.Dispose();
            }

            // Timer pro obnoveni normalnich obrazku
            hitTimer = new();
            hitTimer.Interval = 1000;
            hitTimer.Tick += HitTimer_Tick;
            hitTimer.Start();
        }
    }

    private void HitTimer_Tick(object? sender, EventArgs e) {
        hitImage = false;
        hitTimer.Stop();
        hitTimer.Tick -= HitTimer_Tick;
        hitTimer.Dispose();
    }

    public void Dispose() {

        if (disposed) return;

        if (type == enemyType.Sysalova) {
            basnicka.Pause();
            stopwatch.Reset();
            basnickaHit = false;
        }

        if (hitTimer != null) {
            hitTimer.Tick -= HitTimer_Tick;
            hitTimer.Dispose();
        }
        if (shootAnim != null) {
            shootAnim.Tick -= ShootAnim_Tick;
            shootAnim.Dispose();
        }
        if (projectile != null) {
            projectile.Parent?.Controls.Remove(projectile);
            projectile.Dispose();
            projectile.Bounds = Rectangle.Empty;
        }
        
        pb.Parent?.Controls.Remove(pb);
        pb.Dispose();
        pb.Bounds = Rectangle.Empty;

        dead = true;
        moving = false;
        projectileStop = true;
        disposed = true;
    }
}
