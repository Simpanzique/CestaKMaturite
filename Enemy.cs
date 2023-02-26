namespace Petr_RP_Silksong;

internal class Enemy
{
    public static int Count = 1;
    public static int ProjectileCountO = 1;
    public static int ProjectileCountH = 1;

    public int health;
    public PictureBox pb;
    public bool gravitation;
    public bool moving;
    public bool stopGravitation = false;
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
    public Point player;

    public Enemy(int positionX, int positionY, int width, int height, Color color,
        int _health, bool _gravitation, bool _moving, int _xLeft, int _xRight, int _movementSpeed,
        string _type, int _projectileCooldown, Panel scene)
    {
        //X,Y,Width,Height,Color,HP,Gravitation,Moving
        //color premenit na img
        projectileCooldown = _projectileCooldown;
        type = _type;
        health = _health;
        gravitation = _gravitation;
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

        Count++;
    }

    public void ShootProjectile(Enemy enemy, PictureBox player, Panel panel)
    {
        if (enemy.projectile != null)
        {
            enemy.projectile.Bounds = Rectangle.Empty;
            panel.Controls.Remove(enemy.projectile);
            enemy.projectile.Dispose();
        }

        if (enemy.health > 0)
        {
            enemy.projectile = new PictureBox();
            enemy.projectile.Width = 30;
            enemy.projectile.Height = 30;

            if (enemy.type == "Oberhofnerova")
            {
                //random co hodi
                enemy.projectile.BackColor = Color.Yellow;

                enemy.projectile.Name = "projectileO" + ProjectileCountO;
                enemy.projectile.Left = enemy.pb.Left + 45;
                enemy.projectile.Top = enemy.pb.Top + 45;
                enemy.projectile.SetBounds(enemy.pb.Left + 45, enemy.pb.Top + 45, 30, 30);
                enemy.player = new Point(player.Left + player.Width / 2, player.Top + player.Height / 2);
                if (enemy.player.Y - projectile.Bottom > 300)
                {
                    enemy.projectileSpeedX = Math.Abs(enemy.player.X - enemy.projectile.Left) / 100;
                    enemy.projectileSpeedY = (enemy.player.Y - enemy.projectile.Bottom) / 100;
                }
                else
                {
                    enemy.projectileSpeedX = Math.Abs(enemy.player.X - enemy.projectile.Left) / 50;
                    enemy.projectileSpeedY = (enemy.player.Y - enemy.projectile.Bottom) / 50;
                }
                enemy.projectileStop = false;

                ProjectileCountO++;
            }
            if (enemy.type == "Hacek")
            {
                enemy.projectile.BackColor = Color.Yellow;

                enemy.projectile.Name = "projectileH" + ProjectileCountH;
                enemy.projectile.Left = enemy.pb.Left + 40;
                enemy.projectile.Top = enemy.pb.Top + 75;
                enemy.projectile.SetBounds(enemy.pb.Left + 40, enemy.pb.Top + 75, 30, 30);
                enemy.projectileStop = false;

                ProjectileCountH++;
            }

            panel.Controls.Add(enemy.projectile);
        }
    }

    public void CheckHealth(Enemy enemy, Panel panel)
    {
        if (enemy.health <= 0)
        {
            if (enemy.projectile != null)
            {
                enemy.projectile.Bounds = Rectangle.Empty;
                panel.Controls.Remove(enemy.projectile);
                enemy.projectile.Dispose();
            }

            enemy.moving = false;
            enemy.projectileStop = true;

            enemy.pb.Bounds = Rectangle.Empty;
            panel.Controls.Remove(enemy.pb);
            enemy.pb.Dispose();
        }
    }
}
