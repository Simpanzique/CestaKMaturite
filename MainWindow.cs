namespace Petr_RP_Silksong;
public partial class MainWindow : Form
{
    public MainWindow()
    {
        InitializeComponent();
    }

    //Globální promìnné
    bool A, D, Space, Q, E, LMB; //hráèovo inputy
    bool moveLeft, moveRight, onTop, isJumping, onGround, lastInputLeft, facingRight, dashLeft, dashRight, banInput, canDash = true; //pohyb
    int jumpSpeed, dashX, dashIndex; //pohyb
    bool attackQphase1, attackQphase2, attackQcooldown, QOnLeft, attackLMBcooldown = false, alreadyHit, hitQ, underTerrain; //utok
    int abilityQIndex, rulerLength = 100, abilityLMBIndex; //utok
    int levelCount = 1, playerHealth = 5, dmgIndex, enMiddle, absence1Index, absence2Index, lemkaIndex;
    bool canGetHit = true, disableallInputs = false, unHitable = false, knockback, paused, cheatHealth, nuggetSpawn = false, lemkaCooldown, lemkaRight; //managment
    int OberhofnerovaHP = 10, LemkaHP = 10, HacekHP = 10, SysalovaHP = 10, StarkHP = 50, OberhofnerovaMovementSpeed = 10, LemkaMovementSpeed = 6;
    string info, info1;


    Rectangle HitboxLeft;
    Rectangle HitboxRight;
    Rectangle HitboxUp;
    Rectangle HitboxDown;

    Rectangle HitboxDashLeft;
    Rectangle HitboxDashRight;

    PictureBox HitboxAttackLeft;
    PictureBox HitboxAttackRight;
    PictureBox HitboxAttackTop;

    PictureBox HitboxLemkaRight;
    PictureBox HitboxLemkaLeft;
    Enemy lemka;

    PictureBox closestEnemy;
    PictureBox nuggetPB;

    Terrain[] terrainArray;
    Enemy[] enemyArray;
    Absence[] absenceArray;
    List<Nugget> nuggetList = new();

    private void UpdateMethod_Tick(object sender, EventArgs e)
    {
        //reset promìnných
        moveLeft = true;
        moveRight = true;
        onTop = false;
        underTerrain = false;

        //kurzor
        Point cursor = this.PointToClient(Cursor.Position);

        if ((Control.MouseButtons & MouseButtons.Left) != 0)
            LMB = true;
        else
            LMB = false;

        //šance 1/10000 kazdou milisekundu (1/100s) ze se nekde nahodne spawne nuggetka
        if (Random.Shared.Next(0, 10001) == 10000 && !nuggetSpawn)
        {
            nuggetSpawn = true;
            bool intersects = true;
            int positionX = 0, positionY = 0;
            while (intersects)
            {
                intersects = false;
                positionX = Random.Shared.Next(50, 1404);
                positionY = Random.Shared.Next(50, 706);
                Rectangle spawnNugget = new(positionX, positionY, 65, 70);
                foreach (PictureBox anything in GameScene.Controls.OfType<PictureBox>())
                {
                    if (anything.Bounds.IntersectsWith(spawnNugget))
                        intersects = true;
                }
            }
            Nugget nugget = new(positionX, positionY, 1, Color.Blue, GameScene);
            nuggetList.Add(nugget);
            nuggetPB = nugget.pb as PictureBox;
            NuggetDisappear.Interval = Random.Shared.Next(3000, 5000);
            NuggetDisappear.Start();
        }


        //Hitboxy
        HitboxLeft = new Rectangle(Player.Left - 3, Player.Top, 3, Player.Height - 2);
        HitboxRight = new Rectangle(Player.Left + Player.Width, Player.Top, 3, Player.Height - 2);
        HitboxUp = new Rectangle(Player.Left, Player.Top - 4, Player.Width, 2);
        HitboxDown = new Rectangle(Player.Left + 2, Player.Bottom - 2, Player.Width - 4, 2);

        HitboxDashLeft = new Rectangle(Player.Left - 15, Player.Top, Player.Width, Player.Height - 2);
        HitboxDashRight = new Rectangle(Player.Left + 15, Player.Top, Player.Width, Player.Height - 2);

        //platformy
        foreach (PictureBox terrain in GameScene.Controls.OfType<PictureBox>().Where(x => x.Tag == "Terrain"))
        {
            if (terrain.Bounds.IntersectsWith(HitboxLeft))
                moveLeft = false;
            if (terrain.Bounds.IntersectsWith(HitboxRight))
                moveRight = false;
            if (terrain.Bounds.IntersectsWith(HitboxDown) && Player.Bottom - 20 <= terrain.Top && jumpSpeed < 0)
            {
                Player.Top = terrain.Top - Player.Height + 1;
                onTop = true;
                jumpSpeed = 0;
                isJumping = false;
            }
            if (terrain.Bounds.IntersectsWith(HitboxUp))
            {
                jumpSpeed = -2;
                isJumping = false;
                underTerrain = true;
            }
        }

        //Doleva a Doprava
        if (!(A && D))
        {
            if (D && moveRight == true && !(Player.Right >= GameScene.Width) && !banInput)
            {
                Player.Left += 8; //movementSpeed
                lastInputLeft = false;
            }
            if (A && moveLeft == true && !(Player.Left <= 0) && !banInput)
            {
                Player.Left -= 8; //movementSpeed
                lastInputLeft = true;
            }
        }

        //leva a prava strana sceny
        if (Player.Right >= GameScene.Width)
            Player.Left = GameScene.Width - Player.Width;
        if (Player.Left <= 0)
            Player.Left = 0;

        //Smìr koukání
        if (lastInputLeft)
            facingRight = false;
        else
            facingRight = true;

        //Skakani
        if (GameScene.Height - Player.Bottom <= 0 || onTop == true)
            onGround = true;
        else
            onGround = false;

        if (Space && onGround)
        {
            isJumping = true;
            jumpSpeed = 24;
        }

        //vrsek sceny
        if (Player.Top <= 0)
        {
            jumpSpeed = -2;
            isJumping = false;
            underTerrain = true;
        }

        //gravitace
        if (isJumping || (!isJumping && (GameScene.Height - Player.Bottom >= 0)))
        {
            Player.Top -= jumpSpeed;

            if (jumpSpeed > -20)
                jumpSpeed -= 1;
        }

        //spodek sceny
        if (Player.Bottom >= GameScene.Height)
        {
            isJumping = false;
            jumpSpeed = 0;
        }

        //nezaboreni do zeme
        if (GameScene.Height - Player.Bottom < 0)
            Player.Top = GameScene.Height - Player.Height;

        //Dash
        if (E && facingRight && !dashRight && !dashLeft && canDash)
        {
            dashRight = true;
            dashX = Player.Left;
            banInput = true;
            dashIndex = 0;
            Dash.Interval = 300;
            Dash.Start();
        }

        if (E && !facingRight && !dashRight && !dashLeft && canDash)
        {
            dashLeft = true;
            dashX = Player.Left;
            banInput = true;
            dashIndex = 0;
            Dash.Interval = 300;
            Dash.Start();
        }
        foreach (PictureBox terrain in GameScene.Controls.OfType<PictureBox>().Where(x => x.Tag == "Terrain"))
        {
            if (terrain.Bounds.IntersectsWith(HitboxDashLeft))
                dashLeft = false;
            if (terrain.Bounds.IntersectsWith(HitboxDashRight))
                dashRight = false;
        }

        if (dashLeft && dashX - Player.Left < 300)
        {
            Player.Left -= 15;
            jumpSpeed = 0;
        }
        if (dashRight && Player.Left - dashX < 300)
        {
            Player.Left += 15;
            jumpSpeed = 0;
        }


        //Enemy
        //Poèet a Pole Enemákù
        int iEnemy = 0;
        foreach (PictureBox enemy in GameScene.Controls.OfType<PictureBox>().Where(x => x.Tag == "Enemy"))
            iEnemy++;

        if (iEnemy == 0)
        {
            //žádný enemy = ukonèit level
            switch (levelCount)
            {
                case 1:
                    Level1();
                    //Pauza();
                    break;
                case 2:
                    Level2();
                    //Pauza();
                    break;
                case 3:
                    Level3();
                    //Pauza();
                    break;
                case 4:
                    Level4();
                    //Pauza();
                    break;
                default:
                    levelCount = 0;
                    break;
            }
            levelCount++;

        }
        else
        {
            //když existují enemy (mùže být kombat)

            //porovnání vzdáleností a hledání nejbližšího Enemy na kurzor (enemyObjectArray[closestIndex])
            //pomocí tagù
            int[] enemyDistanceArray = new int[iEnemy];
            PictureBox[] enemyObjectArray = new PictureBox[iEnemy];
            iEnemy = 0;
            int? sumDistance;
            foreach (PictureBox enemy in GameScene.Controls.OfType<PictureBox>().Where(x => x.Tag == "Enemy"))
            {
                sumDistance = Math.Abs(cursor.X - enemy.Left) + Math.Abs(cursor.Y - enemy.Top);
                enemyDistanceArray[iEnemy] = Convert.ToInt32(sumDistance);
                enemyObjectArray[iEnemy] = enemy;

                iEnemy++;
            }
            int enemyObjectClosest = enemyDistanceArray[0];
            int closestIndex = 0;
            iEnemy = 0;
            foreach (int item in enemyDistanceArray)
            {
                if (item < enemyObjectClosest)
                {
                    enemyObjectClosest = item;
                    closestIndex = iEnemy;
                }

                iEnemy++;
            }


            //Ability Q na target myši
            if (Q && !onGround && !attackQcooldown && Player.Top < enemyObjectArray[closestIndex].Top)
            {
                if (enemyObjectArray[closestIndex].Top - Player.Top < 300 &&
                    enemyObjectArray[closestIndex].Left - Player.Left < 250 &&
                    Player.Right - enemyObjectArray[closestIndex].Right < 250)
                {
                    closestEnemy = enemyObjectArray[closestIndex] as PictureBox;

                    if (Player.Left < closestEnemy.Left)
                    {
                        QOnLeft = true;
                        lastInputLeft = false;
                        facingRight = true;
                    }

                    else
                    {
                        QOnLeft = false;
                        lastInputLeft = true;
                        facingRight = false;
                    }

                    hitQ = false;
                    unHitable = true;
                    attackQcooldown = true;
                    abilityQIndex = 0;
                    AbilityQ.Interval = 5;
                    AbilityQ.Start();
                }

            }

            if (attackQphase1)
            {
                if (QOnLeft && Player.Right - (closestEnemy.Left + closestEnemy.Width / 2) < 10)
                    Player.Left += 15;

                if (!QOnLeft && Player.Left - (closestEnemy.Right - closestEnemy.Width / 2) > 0)
                    Player.Left -= 15;

                if (Player.Top + Player.Height < closestEnemy.Top - 25)
                    Player.Top += 15;

                isJumping = false;
                jumpSpeed = 0;
                banInput = true;

                if (Player.Right - closestEnemy.Left > 10 && Player.Top + Player.Height > closestEnemy.Top - 25)
                    AbilityQ.Interval = 1;

                //-HP
                foreach (Enemy enemy in enemyArray)
                {
                    if (enemy.pb == closestEnemy)
                    {
                        if (!hitQ)
                        {
                            enemy.health -= 4;
                            hitQ = true;
                            enemy.CheckHealth(enemy, GameScene);
                        }
                    }
                }
            }
            if (attackQphase2)
            {
                if (QOnLeft)
                    Player.Left += 15;
                else
                    Player.Left -= 15;

                if (!underTerrain && Player.Top > 0)
                    Player.Top -= 15;

                isJumping = false;
                jumpSpeed = 0;
                banInput = true;
            }

            //Útok LMB
            if (LMB && cursor.Y < Player.Top && !attackLMBcooldown)
            {
                //utok nahoru
                HitboxAttackTop = new PictureBox();
                HitboxAttackTop.Left = Player.Left;
                HitboxAttackTop.Top = Player.Top - rulerLength;
                HitboxAttackTop.Width = Player.Width;
                HitboxAttackTop.Height = rulerLength;
                HitboxAttackTop.BackColor = Color.Blue;
                GameScene.Controls.Add(HitboxAttackTop);
                attackLMBcooldown = true;
                alreadyHit = false;
                abilityLMB.Interval = 100;
                abilityLMBIndex = 0;
                abilityLMB.Start();
            }
            else
            {
                if (LMB && facingRight && !attackLMBcooldown)
                {
                    //utok doleva
                    HitboxAttackRight = new PictureBox
                    {
                        Left = Player.Right,
                        Top = Player.Top,
                        Width = rulerLength,
                        Height = Player.Height,
                        BackColor = Color.Blue
                    };
                    GameScene.Controls.Add(HitboxAttackRight);
                    attackLMBcooldown = true;
                    alreadyHit = false;
                    abilityLMB.Interval = 100;
                    abilityLMBIndex = 0;
                    abilityLMB.Start();
                }
                if (LMB && !facingRight && !attackLMBcooldown)
                {
                    //utok doprava
                    HitboxAttackLeft = new PictureBox
                    {
                        Left = Player.Left - rulerLength,
                        Top = Player.Top,
                        Width = rulerLength,
                        Height = Player.Height,
                        BackColor = Color.Blue
                    };
                    GameScene.Controls.Add(HitboxAttackLeft);
                    attackLMBcooldown = true;
                    alreadyHit = false;
                    abilityLMB.Interval = 100;
                    abilityLMBIndex = 0;
                    abilityLMB.Start();
                }
            }



            foreach (Enemy enemy in enemyArray)
            {
                //gravitace
                foreach (PictureBox terrain in GameScene.Controls.OfType<PictureBox>().Where(x => x.Tag == "Terrain"))
                {
                    if (enemy.gravitation)
                    {
                        if (enemy.pb.Top + enemy.pb.Height >= GameScene.Height)
                        {
                            enemy.pb.Top = GameScene.Height - enemy.pb.Height;
                            enemy.stopGravitation = true;
                        }
                        if (enemy.pb.Bounds.IntersectsWith(terrain.Bounds))
                        {
                            enemy.pb.Top = terrain.Top - enemy.pb.Height;
                            enemy.stopGravitation = true;
                        }
                        if (!enemy.stopGravitation)
                            enemy.pb.Top += 1;
                    }
                }

                if (enemy.type == "Lemka")
                {
                    //Lemka movement
                    if (enemy.pb.Top+enemy.pb.Height > Player.Top+Player.Height-5)
                    {
                        enemy.moveSwitch = false;
                        if (Math.Abs(enemy.pb.Left - Player.Left)<20)
                        {
                            enemy.moveLeft = false;
                            enemy.moveRight = false;
                        }else if (enemy.pb.Left < Player.Left)
                        {
                            enemy.moveRight = true;
                            enemy.moveLeft = false;
                        }
                        else
                        {
                            enemy.moveLeft = true;
                            enemy.moveRight = false;
                        }
                    }
                    else
                        enemy.moveSwitch = true;

                    //Lemka utok
                    if (((enemy.pb.Left - Player.Left - Player.Width) < 100 && enemy.pb.Left > (Player.Left + Player.Width)) ||
                        (Player.Left - (enemy.pb.Left + enemy.pb.Width) < 100 && (enemy.pb.Left + enemy.pb.Width) < Player.Left))
                    {
                        if ((Player.Top + Player.Height) > enemy.pb.Top && Player.Top < (enemy.pb.Top + enemy.pb.Height) && !lemkaCooldown)
                        {
                            if ((enemy.pb.Left - Player.Left - Player.Width) < 100 && enemy.pb.Left > (Player.Left + Player.Width))
                                lemkaRight = false;
                            if (Player.Left - (enemy.pb.Left + enemy.pb.Width) < 100 && (enemy.pb.Left + enemy.pb.Width) < Player.Left)
                                lemkaRight = true;
                            lemka = enemy as Enemy;
                            lemkaCooldown = true;
                            lemkaIndex = 0;
                            Lemka.Interval = 300;
                            Lemka.Start();
                        }
                    }
                    if (HitboxLemkaLeft != null && HitboxLemkaLeft.Bounds.IntersectsWith(Player.Bounds) && canGetHit)
                        Hit(HitboxLemkaLeft);
                    if (HitboxLemkaRight != null && HitboxLemkaRight.Bounds.IntersectsWith(Player.Bounds) && canGetHit)
                        Hit(HitboxLemkaRight);
                }

                //enemy doleva a doprava
                if (enemy.moving)
                {
                    if (enemy.moveLeft)
                    {
                        if (enemy.pb.Left > enemy.xLeft)
                        {
                            enemy.pb.Left -= enemy.movementSpeed;
                        }
                        else if (enemy.moveSwitch)
                        {
                            enemy.moveLeft = false;
                            enemy.moveRight = true;
                        }
                    }
                    if (enemy.moveRight)
                    {
                        if (enemy.pb.Left < enemy.xRight)
                        {
                            enemy.pb.Left += enemy.movementSpeed;
                        }
                        else if (enemy.moveSwitch)
                        {
                            enemy.moveLeft = true;
                            enemy.moveRight = false;
                        }
                    }
                }
                

                //bouchání LMB
                if (HitboxAttackLeft != null)
                {
                    if (enemy.pb.Bounds.IntersectsWith(HitboxAttackLeft.Bounds))
                    {
                        if (!alreadyHit && !unHitable)
                        {
                            enemy.health -= 2;
                            alreadyHit = true;
                            enemy.CheckHealth(enemy, GameScene);
                        }
                    }
                }
                if (HitboxAttackRight != null)
                {
                    if (enemy.pb.Bounds.IntersectsWith(HitboxAttackRight.Bounds))
                    {
                        if (!alreadyHit && !unHitable)
                        {
                            enemy.health -= 2;
                            alreadyHit = true;
                            enemy.CheckHealth(enemy, GameScene);
                        }
                    }
                }
                if (HitboxAttackTop != null)
                {
                    if (enemy.pb.Bounds.IntersectsWith(HitboxAttackTop.Bounds))
                    {
                        if (!alreadyHit && !unHitable)
                        {
                            enemy.health -= 2;
                            alreadyHit = true;
                            enemy.CheckHealth(enemy, GameScene);
                        }
                    }
                }

                
                //naražení do enemy -HP
                if (enemy.pb.Bounds.IntersectsWith(Player.Bounds) && canGetHit)
                {
                    Hit(enemy.pb);
                    info = "hit s " + enemy.pb.Name;
                }

                
                //let projektilù
                if (enemy.projectile != null)
                {
                    if (enemy.projectile.Left < 0 || enemy.projectile.Right < 0 || enemy.projectile.Top < 0 || enemy.projectile.Bottom < 0)
                    {
                        DestroyAll(enemy.projectile, GameScene);
                        enemy.projectileStop = true;
                    }
                    foreach (PictureBox terrain in GameScene.Controls.OfType<PictureBox>().Where(x => x.Tag == "Terrain"))
                    {
                        if (enemy.projectile.Bounds.IntersectsWith(terrain.Bounds))
                        {
                            DestroyAll(enemy.projectile, GameScene);
                            enemy.projectileStop = true;
                        }
                    }
                    if (enemy.projectile.Bounds.IntersectsWith(Player.Bounds) && canGetHit && (enemy.projectile.Name == "projectileO" + (Enemy.ProjectileCountO - 1) || enemy.projectile.Name == "projectileH" + (Enemy.ProjectileCountH - 1)))
                    {
                        Hit(enemy.projectile);
                        DestroyAll(enemy.projectile, GameScene);
                        enemy.projectileStop = true;
                    }
                    if (enemy.type == "Oberhofnerova")
                    {
                        if (enemy.projectile.Bottom < enemy.player.Y && Math.Abs(enemy.projectile.Left+15 - enemy.player.X) > 30 &&!enemy.projectileStop)
                        {
                            if (enemy.projectile.Left < enemy.player.X)
                                enemy.projectile.Left += enemy.projectileSpeedX;
                            else
                                enemy.projectile.Left -= enemy.projectileSpeedX;

                            if (enemy.projectile.Top < enemy.player.Y)
                                enemy.projectile.Top += enemy.projectileSpeedY;
                        }
                        else
                        {
                            DestroyAll(enemy.projectile, GameScene);
                            enemy.projectileStop = true;
                        }
                    }
                    if (enemy.type == "Hacek")
                    {
                        if ((HitboxAttackLeft != null && HitboxAttackLeft.Bounds.IntersectsWith(enemy.projectile.Bounds))
                            || (HitboxAttackRight != null && HitboxAttackRight.Bounds.IntersectsWith(enemy.projectile.Bounds))
                            || (HitboxAttackTop != null && HitboxAttackTop.Bounds.IntersectsWith(enemy.projectile.Bounds)))
                        {
                            DestroyAll(enemy.projectile, GameScene);
                            enemy.projectileStop = true;
                        }
                        if (!enemy.projectileStop)
                        {
                            if (Math.Abs(Player.Left - enemy.projectile.Left) > 400)
                            {
                                enemy.projectileSpeedX = Math.Abs(Player.Left + Player.Width / 2 - enemy.projectile.Left + 15) / 80;
                                enemy.projectileSpeedY = Math.Abs(Player.Top + Player.Height / 2 - enemy.projectile.Top + 15) / 50;
                                info = "yes";
                            }
                            else
                            {
                                enemy.projectileSpeedX = (Math.Abs(Player.Left + Player.Width / 2 - enemy.projectile.Left + 15) / 80)+4;
                                enemy.projectileSpeedY = (Math.Abs(Player.Top + Player.Height / 2 - enemy.projectile.Top + 15) / 50 )+4;
                                info = "no";
                            }

                            if (enemy.projectile.Left + 15 > Player.Left + Player.Width/2)
                                enemy.projectile.Left -= enemy.projectileSpeedX;
                            if (enemy.projectile.Left + 15 < Player.Left + Player.Width/2)
                                enemy.projectile.Left += enemy.projectileSpeedX;
                            if (enemy.projectile.Top + 15 > Player.Top + Player.Height/2)
                                enemy.projectile.Top -= enemy.projectileSpeedY;
                            if (enemy.projectile.Top + 15 < Player.Top + Player.Height/2)
                                enemy.projectile.Top += enemy.projectileSpeedY;
                        }
                        else
                        {
                            DestroyAll(enemy.projectile, GameScene);
                            enemy.projectileStop = true;
                        }
                        if (enemy.projectileStop)
                            Hacek.Start();
                    }
                }
            }
        }

        if (absenceArray != null)
        {
            foreach (Absence absence in absenceArray)
            {
                if (absence.spawn && absence.move)
                {
                    if (absence.spawned == false)
                    {
                        absence.spawned = true;
                        if (absence.type == "X")
                            absence.pb.Left = absence.from;
                        else
                            absence.pb.Top = absence.from;

                        absence.pb.SetBounds(absence.boundsX, absence.boundsY, 30, 30);
                        GameScene.Controls.Add(absence.pb);
                    }

                    if (absence.type == "X")
                    {
                        if (absence.plusCoordinates)
                        {
                            if (absence.pb.Left < absence.to)
                                absence.pb.Left += absence.movementSpeed;
                            else
                            {
                                absence.pb.Bounds = Rectangle.Empty;
                                GameScene.Controls.Remove(absence.pb);
                            }
                        }
                        else
                        {
                            if (absence.pb.Left > absence.to)
                                absence.pb.Left -= absence.movementSpeed;
                            else
                            {
                                absence.pb.Bounds = Rectangle.Empty;
                                GameScene.Controls.Remove(absence.pb);
                            }
                        }
                    }
                    else if (absence.type == "Y")
                    {
                        if (absence.plusCoordinates)
                        {
                            if (absence.pb.Top < absence.to)
                                absence.pb.Top += absence.movementSpeed;
                            else
                            {
                                absence.pb.Bounds = Rectangle.Empty;
                                GameScene.Controls.Remove(absence.pb);
                            }
                        }
                        else
                        {
                            if (absence.pb.Top > absence.to)
                                absence.pb.Top -= absence.movementSpeed;
                            else
                            {
                                absence.pb.Bounds = Rectangle.Empty;
                                GameScene.Controls.Remove(absence.pb);
                            }
                        }
                    }
                }
                if (absence.pb.Bounds.IntersectsWith(Player.Bounds) && canGetHit)
                {
                    info = "hit s absenci";
                    Hit(absence.pb);
                }

            }
        }
        if (nuggetList != null)
        {

            foreach (Nugget nugget in nuggetList)
            {
                if (nugget.pb.Bounds.IntersectsWith(Player.Bounds) && playerHealth < 5)
                {
                    if (playerHealth + nugget.healthAdd > 5)
                        playerHealth = 5;
                    else
                        playerHealth += nugget.healthAdd;

                    DestroyAll(nugget.pb, GameScene);
                }
            }
        }


        if (knockback)
        {
            if (Player.Left + Player.Width < enMiddle && moveLeft)
            {
                Player.Left -= 10;
                Dash.Interval = 1;
            }
            else if (moveRight)
            {
                Player.Left += 10;
                Dash.Interval = 1;
            }
        }

        if (playerHealth <= 0)
        {
            UpdateMethod.Interval = 35;
            GameScene.Enabled = false;
            lbGameOver.Left = 450;
            lbGameOver.Top = 195;
            lbPress.Left = 586;
            lbPress.Top = 401;
            disableallInputs = true;
        }

        lbStats.Text =
                "OnGround: " + onGround +
                "\nisJumping: " + isJumping +
                "\nonTop: " + onTop +
                "\nmoveLeft: " + moveLeft +
                "\nmoveRight: " + moveRight +
                "\nCursorX: " + cursor.X +
                "\nCursorY: " + cursor.Y +
                "\nFacing: " + (facingRight ? "Right" : "Left") +
                "\nPlayerHealth: " + playerHealth +
                "\nCheatHealth: " + cheatHealth +
                "\nInfo: " + info +
                "\nInfo1: " + info1;
    }
    private void abilityLMB_Tick(object sender, EventArgs e)
    {
        if (abilityLMBIndex == 0)
        {
            if (HitboxAttackLeft != null)
                DestroyAll(HitboxAttackLeft, GameScene);
            if (HitboxAttackRight != null)
                DestroyAll(HitboxAttackRight, GameScene);
            if (HitboxAttackTop != null)
                DestroyAll(HitboxAttackTop, GameScene);


            LMB = false;
            abilityLMB.Interval = 600;
        }
        if (abilityLMBIndex == 1)
        {
            attackLMBcooldown = false;
            abilityLMB.Stop();
        }
        abilityLMBIndex++;
    }
    private void Dash_Tick(object sender, EventArgs e)
    {
        if (dashIndex == 0)
        {
            canDash = false;
            dashLeft = false;
            dashRight = false;
            banInput = false;
            jumpSpeed = -1;
            Dash.Interval = 1500;
        }
        if (dashIndex == 1)
        {
            canDash = true;
            Dash.Stop();
        }
        dashIndex++;

    }
    private void AbilityQ_Tick(object sender, EventArgs e)
    {
        if (abilityQIndex == 0)
        {
            attackQphase1 = true;
            AbilityQ.Interval = 350;
        }
        if (abilityQIndex == 1)
        {
            attackQphase1 = false;
            attackQphase2 = true;
            AbilityQ.Interval = 200;
        }
        if (abilityQIndex == 2)
        {
            AbilityQ.Interval = 2000;
            attackQphase2 = false;
            banInput = false;
            unHitable = false;
        }
        if (abilityQIndex == 3)
        {
            attackQcooldown = false;
            AbilityQ.Stop();
        }
        abilityQIndex++;
    }

    private void DMGcooldown_Tick(object sender, EventArgs e)
    {
        if (dmgIndex == 0)
        {
            knockback = false;
            DMGcooldown.Interval = 1900;
        }
        else
        {
            canGetHit = true;
            DMGcooldown.Stop();
        }
        dmgIndex++;
    }

    private void Absence1_Tick(object sender, EventArgs e)
    {
        if (absence1Index == 1)
        {
            Absence(absenceArray[0]);

            absenceArray[0].spawned = false;
            absenceArray[0].spawn = true;
            Absence1.Interval = absenceArray[0].cooldown;
        }
        if (absence1Index == 2)
        {
            absenceArray[0].spawn = false;
            Absence1.Interval = 1;
            absence1Index = 0;
        }
        absence1Index++;
    }

    private void Absence2_Tick(object sender, EventArgs e)
    {
        if (absence2Index == 1)
        {
            Absence(absenceArray[1]);

            absenceArray[1].spawned = false;
            absenceArray[1].spawn = true;
            Absence2.Interval = absenceArray[1].cooldown;
        }
        if (absence2Index == 2)
        {
            absenceArray[1].spawn = false;
            Absence2.Interval = 1;
            absence2Index = 0;
        }
        absence2Index++;
    }
    private void Lemka_Tick(object sender, EventArgs e)
    {
        if (lemkaIndex == 0)
        {
            if (lemkaRight)
            {
                HitboxLemkaRight = new PictureBox
                {
                    Left = lemka.pb.Left + lemka.pb.Width,
                    Top = lemka.pb.Top + 50,
                    Width = 100,
                    Height = 80,
                    BackColor = Color.Purple,
                };
                GameScene.Controls.Add(HitboxLemkaRight);
            }
            else
            {
                HitboxLemkaLeft = new PictureBox
                {
                    Left = lemka.pb.Left - 100,
                    Top = lemka.pb.Top + 50,
                    Width = 100,
                    Height = 80,
                    BackColor = Color.Purple,
                };
                GameScene.Controls.Add(HitboxLemkaLeft);
            }
            
            Lemka.Interval = 100;
        }
        if (lemkaIndex == 1)
        {
            if (HitboxLemkaLeft != null)
                DestroyAll(HitboxLemkaLeft, GameScene);
            if (HitboxLemkaRight != null)
                DestroyAll(HitboxLemkaRight, GameScene);
            Lemka.Interval = 3000;
        }
        if (lemkaIndex == 2)
        {
            lemkaCooldown = false;
            Lemka.Stop();
        }
        lemkaIndex++;
    }
    private void Oberhofnerova_Tick(object sender, EventArgs e)
    {
        foreach (Enemy enemy in enemyArray)
        {
            if (enemy.type == "Oberhofnerova")
                enemy.ShootProjectile(enemy, Player, GameScene);
        }
    }

    private void Hacek_Tick(object sender, EventArgs e)
    {
        foreach (Enemy enemy in enemyArray)
        {
            if (enemy.type == "Hacek")
                enemy.ShootProjectile(enemy, Player, GameScene);
        }
        Hacek.Stop();
    }

    private void NuggetDisappear_Tick(object sender, EventArgs e)
    {
        DestroyAll(nuggetPB, GameScene);
        NuggetDisappear.Stop();
    }

    private void MainWindow_KeyDown(object sender, KeyEventArgs e)
    {
        //Input - zmacknuti
        if (!disableallInputs)
        {
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
                A = true;
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
                D = true;
            if (e.KeyCode == Keys.Space && Space == false)
                Space = true;
            if (e.KeyCode == Keys.Q)
                Q = true;
            if (e.KeyCode == Keys.E)
                E = true;
            if (e.KeyCode == Keys.F3)
            {
                if (lbStats.Visible)
                    lbStats.Visible = false;
                else
                    lbStats.Visible = true;
            }
            if (e.KeyCode == Keys.Escape)
            {
                if (!Pause.Visible)
                {
                    Pause.Visible = true;
                    Pause.Enabled = true;
                    GameScene.Enabled = false;
                    GameScene.Visible = false;
                    UpdateMethod.Stop();
                    panelPauza.Visible = true;
                    lbNazev.Text = "Pauza";
                    this.Focus();
                }
                else
                {
                    Pause.Visible = false;
                    Pause.Enabled = false;
                    GameScene.Enabled = true;
                    GameScene.Visible = true;
                    UpdateMethod.Start();
                    this.Focus();
                }
            }
            if (e.KeyCode == Keys.F && paused)
            {
                lbPozastaveno.Top = -200;
                UpdateMethod.Start();
                paused = false;
            }
            if (e.KeyCode == Keys.K)
            {
                foreach (Enemy enemy in enemyArray)
                {
                    enemy.health = 0;
                    enemy.CheckHealth(enemy, GameScene);
                }

            }
            if (e.KeyCode == Keys.L)
            {
                if (cheatHealth)
                    cheatHealth = false;
                else
                    cheatHealth = true;
            }
        }
        else
        {
            if (e.KeyCode == Keys.R)
            {
                //Reset
                Reset();

                //promenny s defaultni hodnotou
                lbGameOver.Top = -200;
                lbPress.Top = -200;
                canDash = true;
                attackLMBcooldown = false;
                levelCount = 1;
                playerHealth = 5;
                canGetHit = true;
                disableallInputs = false;
                unHitable = false;
                UpdateMethod.Interval = 1;
                GameScene.Enabled = true;
            }
        }
    }

    private void MainWindow_KeyUp(object sender, KeyEventArgs e)
    {
        //Input - uvolneni
        if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            A = false;
        if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            D = false;
        if (e.KeyCode == Keys.Space)
            Space = false;
        if (e.KeyCode == Keys.Q)
            Q = false;
        if (e.KeyCode == Keys.E)
            E = false;

    }
    private void GameScene_Load(object sender, EventArgs e)
    {

    }

    private void btPlay_Click(object sender, EventArgs e)
    {
        Menu.Visible = false;
        Menu.Enabled = false;
        GameScene.Enabled = true;
        GameScene.Visible = true;
        UpdateMethod.Start();
        this.Focus();
    }

    private void btOptions_Click(object sender, EventArgs e)
    {
        Menu.Visible = false;
        Menu.Enabled = false;
        Pause.Enabled = true;
        Pause.Visible = true;
        panelPauza.Visible = false;
        lbNazev.Text = "Možnosti";
        this.Focus();
    }
    private void btMenu_Click(object sender, EventArgs e)
    {
        Pause.Enabled = false;
        Pause.Visible = false;
        Menu.Visible = true;
        Menu.Enabled = true;
        this.Focus();
    }
    private void btExit_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    void Absence(Absence absence)
    {
        if (absence.changeDirection)
        {
            if (absence.indexDirection != 0)
            {
                if (absence.type == "X")
                {
                    if (absence.positionX == absence.from)
                        absence.positionX = absence.to;
                    else
                        absence.positionX = absence.from;
                }
                if (absence.type == "Y")
                {
                    if (absence.positionY == absence.from)
                        absence.positionY = absence.to;
                    else
                        absence.positionY = absence.from;
                }

                if (absence.plusCoordinates)
                    absence.plusCoordinates = false;
                else
                    absence.plusCoordinates = true;

                absence.help = absence.from;
                absence.from = absence.to;
                absence.to = absence.help;

                absence.boundsX = absence.positionX;
                absence.boundsY = absence.positionY;
            }

            absence.indexDirection++;
        }
    }
    void Hit(PictureBox pb)
    {
        enMiddle = pb.Left + pb.Width;
        canGetHit = false;
        knockback = true;
        dmgIndex = 0;
        DMGcooldown.Interval = 100;
        DMGcooldown.Start();
        if (!cheatHealth)
        {
            playerHealth--;
            info = "hit s " + pb.Name;
        }

    }
    void DestroyAll(PictureBox pb, GroupBox panel)
    {
        pb.Bounds = Rectangle.Empty;
        panel.Controls.Remove(pb);
        pb.Dispose();
    }
    void Reset()
    {
        nuggetSpawn = false;

        Absence1.Stop();
        Absence2.Stop();
        Oberhofnerova.Stop();
        Hacek.Stop();

        if (enemyArray != null)
        {
            foreach (Enemy enemy in enemyArray)
            {
                if (enemy.projectile != null)
                    DestroyAll(enemy.projectile, GameScene);
                enemy.moving = false;
                enemy.projectileStop = true;

                DestroyAll(enemy.pb, GameScene);
            }
        }
        if (terrainArray != null)
        {
            foreach (Terrain terrain in terrainArray)
                DestroyAll(terrain.pb, GameScene);
        }
        if (absenceArray != null)
        {
            foreach (Absence absence in absenceArray)
            {
                absence.move = false;
                DestroyAll(absence.pb, GameScene);
            }
        }
        if (nuggetList != null)
        {
            foreach (Nugget nugget in nuggetList)
                DestroyAll(nugget.pb, GameScene);
        }

        //reset hráèe
        Player.Left = 75;
        Player.Top = 526;
    }
    void LevelPrepare()
    {
        //fix na Q
        if (attackQphase1 || attackQphase2)
        {
            attackQphase1 = false;
            attackQphase2 = false;
            AbilityQ.Stop();
            banInput = false;
            unHitable = false;
            attackQcooldown = false;
        }
        //doplni jedno HP
        if (playerHealth < 5)
            playerHealth++;

        Reset();
    }

    void Pauza()
    {
        UpdateMethod.Stop();
        lbPozastaveno.Top = 318;
        paused = true;
    }

    void Level1()
    {
        LevelPrepare();
        Terrain terrain1 = new(28, 658, 164, 39, Color.Green, GameScene);
        Terrain terrain2 = new(291, 753, 100, 72, Color.Green, GameScene);
        Terrain terrain3 = new(341, 444, 110, 33, Color.Green, GameScene);
        Terrain terrain4 = new(577, 260, 593, 34, Color.Green, GameScene);
        Terrain terrain5 = new(1402, 460, 97, 34, Color.Green, GameScene);
        Terrain terrain6 = new(1263, 750, 100, 75, Color.Green, GameScene);

        terrainArray = new Terrain[] { terrain1, terrain2, terrain3, terrain4, terrain5, terrain6 };


        Enemy enemy1 = new(1296, 12, 100, 100, Color.Red, OberhofnerovaHP, false, true,
            15, 1408, OberhofnerovaMovementSpeed, "Oberhofnerova", 2000, GameScene);
        Enemy enemy2 = new(1000, 645, 110, 180, Color.Red, LemkaHP, false, true, 400, 1150, LemkaMovementSpeed, "Lemka", 0, GameScene);

        enemyArray = new Enemy[] { enemy1, enemy2 };

        Oberhofnerova.Interval = enemy1.projectileCooldown;
        Oberhofnerova.Start();
    }
    void Level2()
    {
        LevelPrepare();
        Terrain terrain1 = new(28, 658, 164, 39, Color.Green, GameScene);
        Terrain terrain2 = new(28, 360, 97, 34, Color.Green, GameScene);
        Terrain terrain3 = new(457, 510, 593, 34, Color.Green, GameScene);
        Terrain terrain4 = new(370, 189, 110, 33, Color.Green, GameScene);
        Terrain terrain5 = new(1088, 248, 100, 75, Color.Green, GameScene);
        Terrain terrain6 = new(705, 750, 100, 75, Color.Green, GameScene);
        Terrain terrain7 = new(1342, 659, 100, 167, Color.Green, GameScene);

        terrainArray = new Terrain[] { terrain1, terrain2, terrain3, terrain4, terrain5, terrain6, terrain7 };

        Enemy enemy1 = new(1296, 12, 100, 100, Color.Red, OberhofnerovaHP, false, true,
            15, 1408, OberhofnerovaMovementSpeed, "Oberhofnerova", 3000, GameScene);
        Enemy enemy2 = new(1342, 499, 100, 160, Color.Red, SysalovaHP, false, false, 0, 0, 0, "Sysalova", 0, GameScene);
        Enemy enemy3 = new(830, 330, 110, 180, Color.Red, LemkaHP, false, true, 460, 940, LemkaMovementSpeed, "Lemka", 0, GameScene);

        enemyArray = new Enemy[] { enemy1, enemy2, enemy3 };

        Absence absence1 = new(740, 720, Color.Purple, 2000, "Y", false, false, 720, 550, 8);

        absenceArray = new Absence[] { absence1 };

        Oberhofnerova.Interval = enemy1.projectileCooldown;
        Oberhofnerova.Start();

        absence1Index = 0;
        Absence1.Interval = 1;
        Absence1.Start();
    }

    void Level3()
    {
        LevelPrepare();
        Terrain terrain1 = new(28, 658, 164, 39, Color.Green, GameScene);
        Terrain terrain2 = new(270, 377, 100, 69, Color.Green, GameScene);
        Terrain terrain3 = new(455, 161, 593, 34, Color.Green, GameScene);
        Terrain terrain4 = new(681, 531, 110, 33, Color.Green, GameScene);
        Terrain terrain5 = new(1029, 719, 91, 72, Color.Green, GameScene);
        Terrain terrain6 = new(1353, 640, 110, 75, Color.Green, GameScene);

        terrainArray = new Terrain[] { terrain1, terrain2, terrain3, terrain4, terrain5, terrain6 };

        Enemy enemy1 = new(686, 372, 100, 160, Color.Red, SysalovaHP, false, false, 0, 0, 0, "Sysalova", 0, GameScene);
        Enemy enemy2 = new(1353, 460, 110, 180, Color.Red, HacekHP, false, false, 0, 0, 0, "Hacek", 3000, GameScene);

        enemyArray = new Enemy[] { enemy1, enemy2 };

        Absence absence1 = new(576, 121, Color.Purple, 1000, "Y", false, true, 121, 12, 8);
        Absence absence2 = new(878, 12, Color.Purple, 1000, "Y", true, true, 12, 121, 8);

        absenceArray = new Absence[] { absence1, absence2 };

        Nugget nugget1 = new(714, 73, 2, Color.Blue, GameScene);

        nuggetList.Add(nugget1);

        absence1Index = 0;
        Absence1.Interval = 1;
        Absence1.Start();

        absence2Index = 0;
        Absence2.Interval = 1;
        Absence2.Start();

        Hacek.Interval = enemy2.projectileCooldown;
        Hacek.Start();
    }
    void Level4()
    {
        LevelPrepare();
        Terrain terrain1 = new(28, 658, 164, 39, Color.Green, GameScene);
        Terrain terrain2 = new(126, 280, 110, 35, Color.Green, GameScene);
        Terrain terrain3 = new(449, 524, 100, 49, Color.Green, GameScene);
        Terrain terrain4 = new(449, 753, 91, 72, Color.Green, GameScene);
        Terrain terrain5 = new(840, 480, 593, 40, Color.Green, GameScene);
        Terrain terrain6 = new(840, 155, 593, 40, Color.Green, GameScene);
        Terrain terrain7 = new(1323, 418, 110, 63, Color.Green, GameScene);

        terrainArray = new Terrain[] { terrain1, terrain2, terrain3, terrain4, terrain5, terrain6, terrain7 };

        Enemy enemy1 = new(132,120, 100, 160, Color.Red, SysalovaHP, false, false, 0, 0, 0, "Sysalova", 0, GameScene);
        Enemy enemy2 = new(1323, 239, 110, 180, Color.Red, HacekHP, false, false, 0, 0, 0, "Hacek", 3000, GameScene);
        Enemy enemy3 = new(1296, 12, 100, 100, Color.Red, OberhofnerovaHP, false, true,
            15, 1408, OberhofnerovaMovementSpeed, "Oberhofnerova", 2000, GameScene);
        Enemy enemy4 = new(1066, 645, 110, 180, Color.Red, LemkaHP, false, true, 548, 1410, LemkaMovementSpeed, "Lemka", 0, GameScene);

        enemyArray = new Enemy[] { enemy1, enemy2, enemy3, enemy4 };

        Absence absence1 = new(934, 198, Color.Purple, 1000, "Y", true, true, 198, 449, 8);
        Absence absence2 = new(1111, 449, Color.Purple, 1000, "Y", false, true, 449, 198, 8);

        absenceArray = new Absence[] { absence1, absence2 };

        absence1Index = 0;
        Absence1.Interval = 1;
        Absence1.Start();

        absence2Index = 0;
        Absence2.Interval = 1;
        Absence2.Start();

        Hacek.Interval = enemy2.projectileCooldown;
        Hacek.Start();

        Oberhofnerova.Interval = enemy3.projectileCooldown;
        Oberhofnerova.Start();
    }

}