using Petr_RP_CestaKMaturite.Properties;

namespace Petr_RP_CestaKMaturite;

internal class Enemy
{
    public static int Count = 1;
    public static int ProjectileCountO = 1;
    public static int ProjectileCountH = 1;
    public static int ProjectileCountS = 1;

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
    public string type;
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

    public Enemy(int positionX, int positionY, int width, int height, Color color,
        int _health, bool _moving, int _xLeft, int _xRight, int _movementSpeed,
        string _type, int _projectileCooldown, Panel scene)
    {
        //X,Y,Width,Height,Color,HP,Gravitation,Moving
        //color premenit na img
        projectileCooldown = _projectileCooldown;
        type = _type;
        health = _health;
        moving = _moving;
        xLeft = _xLeft;
        xRight = _xRight;
        movementSpeed = _movementSpeed;
        pb = new PictureBox
        {
            Left = positionX,
            Top = positionY,
            Width = width,
            Height = height,
            BackColor = color,
            Tag = "Enemy",
            Name = "enemy" + Count
        };
        scene.Controls.Add(pb);
        if (type == "Oberhofnerova")
            pb.BringToFront();
        Count++;
    }

    public void ShootProjectile(PictureBox Player, Panel panel)
    {
        if (projectile != null)
        {
            projectile.Bounds = Rectangle.Empty;
            panel.Controls.Remove(projectile);
            projectile.Dispose();
        }

        projectile = new()
        {
            Width = 30,
            Height = 30
        };

        if (type == "Oberhofnerova" || type == "Stark")
        {
            if(type == "Oberhofnerova")
            {
                //random co hodi
                switch (Random.Shared.Next(0,8))
                {
                    case 0: projectile.Image = Resources.OProjektil_5; break;
                    case 1: projectile.Image = Resources.OProjektil_binder; break;
                    case 2: projectile.Image = Resources.OProjektil_centropen; break;
                    case 3: projectile.Image = Resources.OProjektil_cos; break;
                    case 4: projectile.Image = Resources.OProjektil_guma; break;
                    case 5: projectile.Image = Resources.OProjektil_minus; break;
                    case 6: projectile.Image = Resources.OProjektil_plus; break;
                    case 7: projectile.Image = Resources.OProjektil_sin; break;
                }

                projectile.Name = "projectileO" + ProjectileCountO;
                projectile.Left = pb.Left + 45;
                projectile.Top = pb.Top + 45;
                projectile.SetBounds(pb.Left + 45, pb.Top + 45, 30, 30);
                ProjectileCountO++;
            }
            else //Stark
            {
                projectile.BackColor = Color.Yellow;
                projectile.Name = "projectileS" + ProjectileCountS;
                projectile.Left = pb.Left + 40;
                projectile.Top = pb.Top + 60;
                projectile.Width = 60;
                projectile.Height = 80;
                projectile.SetBounds(pb.Left + 40, pb.Top + 60, 60, 80);
                ProjectileCountS++;
            }
               
            player = new Point(Player.Left + Player.Width / 2, Player.Top + Player.Height / 2);
            if (player.Y - projectile.Bottom > 300)
            {
                projectileSpeedX = Math.Abs(player.X - projectile.Left) / 100;
                projectileSpeedY = (player.Y - projectile.Bottom) / 100;
            }
            else
            {
                projectileSpeedX = Math.Abs(player.X - projectile.Left) / 50;
                projectileSpeedY = (player.Y - projectile.Bottom) / 50;
            }
            projectileStop = false;
            if (projectile.Bottom > player.Y)
                projectileGoDown = false;
            else
                projectileGoDown = true;

            if (projectile.Left > player.X)
                projectileGoRight = false;
            else
                projectileGoRight = true;
        }
        if (type == "Hacek")
        {
            projectile.Name = "projectileH" + ProjectileCountH;
            projectile.Left = pb.Left + 40;
            projectile.Top = pb.Top + 75;
            projectile.SetBounds(pb.Left + 40, pb.Top + 75, 30, 30);
            projectileStop = false;

            ProjectileCountH++;
        }

        panel.Controls.Add(projectile);
        projectile.BringToFront();
    }

    public void CheckHealth(Panel panel)
    {
        if (health <= 0)
        {
            if (projectile != null)
            {
                projectile.Bounds = Rectangle.Empty;
                panel.Controls.Remove(projectile);
                projectile.Dispose();
            }

            moving = false;
            projectileStop = true;
            dead = true;

            pb.Bounds = Rectangle.Empty;
            panel.Controls.Remove(pb);
            pb.Dispose();
        }
    }
}
