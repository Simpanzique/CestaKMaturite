using NAudio.Wave;
using Petr_RP_CestaKMaturite.Properties;
using System.ComponentModel;
using System.Diagnostics;

namespace Petr_RP_CestaKMaturite;
public partial class MainWindow : Form
{
    public MainWindow()
    {
        InitializeComponent();

        // Sysalova
        ComponentResourceManager rm = new(typeof(Resources));
        Stream stream = (Stream)rm.GetObject("Sysalova_Basnicka");
        reader = new WaveFileReader(stream);
        basnicka = new();
        basnicka.Init(reader);
        basnicka.Play();
        basnicka.Pause();
        stopwatch = new();
    }

    #region Zvuky
    // nAudio
    readonly SoundManager soundBaseball = new("baseball");
    readonly SoundManager soundSpring = new("spring");
    readonly SoundManager soundDash = new("dash");
    readonly SoundManager soundDeath = new("death");
    readonly SoundManager soundhitSomeone = new("hitSomeone");
    readonly SoundManager soundHit = new("hit");
    readonly SoundManager soundChalk = new("chalk");
    readonly SoundManager soundJump = new("jump");
    readonly SoundManager soundLemka = new("lemka");
    readonly SoundManager soundNextLevel = new("nextLevel");
    readonly SoundManager soundNugget = new("nugget");
    readonly SoundManager soundProjectile = new("projectile");
    readonly SoundManager soundProjectileDestroy = new("projectileDestroy");
    readonly SoundManager soundQ = new("q");
    readonly SoundManager soundRuler = new("ruler");
    readonly SoundManager soundSelect = new("select");
    readonly SoundManager soundWin = new("win");
    readonly SoundManager soundDestroy = new("destroy");

    //Sysalova
    Stopwatch stopwatch;
    WaveOut basnicka;
    WaveFileReader reader;
    #endregion

    //Globální promìnné
    bool A, D, Space, Q, E, LMB; //hráèovo inputy
    bool moveLeft, moveRight, onTop, isJumping, onGround, lastInputLeft, facingRight, dashLeft, dashRight, banInput, canDash = true, landed, touchedGround, jumpCooldown, fixQ; //pohyb
    int jumpSpeed, dashX, dashIndex, hupIndex; //pohyb
    bool attackQphase1, attackQphase2, attackQcooldown, QOnLeft, attackLMBcooldown = false, alreadyHit, hitQ, underTerrain, soundFixQ; //utok
    int abilityQIndex, rulerLength = 100, abilityLMBIndex; //utok
    int levelCount = 1, playerHealth, dmgIndex, enMiddle, absence1Index, absence2Index, currentLevel;
    bool canGetHit = true, disableallInputs = false, unHitable = false, knockback, cheatHealth, nuggetSpawn = false, soundDeathOnce, won; string difficulty; //managment
    bool lemkaCooldown, lemkaRight; int lemkaIndex; //enemy
    int OberhofnerovaHP = 6, LemkaHP = 12, HacekHP = 10, SysalovaHP = 12, StarkHP = 60, OberhofnerovaMovementSpeed = 10, LemkaMovementSpeed = 6, StarkMovementSpeed = 3; //enemy
    int bossPhase = 0, baseballSlam = 0, starkIndex; bool starkQ = false, baseballGetDMG = false, baseballCooldown = false, changedPhase = false, starkIdle, playerSideLeft, bookLeftDestroyed, bookRightDestroyed; //bossfight
    bool tOberhofnerova, tHacek, tJumpCooldown, tDMGCooldown, tNuggetDisappear, basnickaPlaying; //fixy timerù
    string info, info1; //bullshit
    bool continueGame, completedGame, hardestDifficulty;
    int savedHealth, savedLevel;

    bool tutorial, writeInstructions, typing, tutBanJump, tutBanDash, tutBanQ, tutBanLMB, tutBanMovement;
    int tutorialPhase = 0;
    string[] poleInstrukci = new string[] { "Vítej na škole SPŠ Ostrov!", "Pohybuj se pomocí kláves A a D", "Skvìle! Zkus si vyskoèit pomocí mezerníku.", "Dobrá práce, ale bacha, mnoha vìcí ti bude bránit v jednoduché maturitì.",
    "A to už tak jednoduché nebude...", "Zkus si bouchnout pravítkem pomocí levého tlaèítka myši.","Když míøíš kurzorem nad hráèe, boucháš nahoru.", "Pro útok doleva èi doprava, pøesuò kurzor do patøièné strany.",
    "Nyní ti pøedstavím uèitele. Zaèneme s panem uèitelem Lemkou.", "Jakmile jsi nad jeho úrovní, zaène tì sledovat.", "TIP: Dávej bacha na jeho kulièkovou myš!", "Máš štìstí, že zatím nemùžeš ztrácet životy.", "Kdybys chtìl nìjaký rychlý útìk, zkus zmáèkout E.",
    "Pokraèujme nyní s paní uèitelkou Sysalovou.", "Bohužel nìmèina není tvùj oblíbený jazyk.", "A proto se jí pokus zastavit co nejdøíve :)", "Když jsi ve vzduchu nad uèitelem, mùžeš na nìj namíøit myší a zmáèknout Q.", "Tento výpad dává dvojnásobné poškození!",
    "Mùžeš si to vyzkoušet na panu Háèkovi.", "Jenom dávej bacha na navádìný twitter!", "TIP: Pravítkem se twitteru zbavíš", "A jako poslední je tu paní uèitelka Oberhofnerova", "Jako flákaè u ní rozhodnì neprojdeš!", "TIP: Zkus se nenechat trefit <3" , "To bude ode mì všechno, hodnì štìstí!"};

    string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    string fileName = "CestaKMaturite_SaveFile.txt";


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
    PictureBox landedBlock;

    Terrain[] terrainArray;
    Enemy[] enemyArray;
    Absence[] absenceArray;
    List<Nugget> nuggetList = new();

    Enemy stark;
    PictureBox baseballka;
    Enemy bossEnemy1;
    Enemy bossEnemy2;
    Terrain spring;
    Terrain bookLeft;
    Terrain bookRight;
    PictureBox hacek;
    PictureBox hacekPlatforma;
    Enemy hacekOnBook;

    Enemy tutorialHacek;
    Enemy tutorialOberhofnerova;
    Enemy tutorialLemka;
    Enemy tutorialSysalova;
    Enemy tutorialhidden;
    private void UpdateMethod_Tick(object sender, EventArgs e)
    {
        //reset promìnných
        moveLeft = true;
        moveRight = true;
        onTop = false;
        underTerrain = false;

        //kurzor
        Point cursor = this.PointToClient(Cursor.Position);

        if (((Control.MouseButtons & MouseButtons.Left) != 0) && !disableallInputs)
            LMB = true;
        else
            LMB = false;

        #region Nuggetka

        //šance 1/10000 kazdou milisekundu (1/100s) ze se nekde nahodne spawne nuggetka
        if (Random.Shared.Next(0, 10001) == 10000 && !nuggetSpawn && (difficulty == "Easy" || difficulty == "Normal"))
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
            Nugget nugget = new(positionX, positionY, 1, GameScene);
            nuggetList.Add(nugget);
            nuggetPB = nugget.pb as PictureBox;
            NuggetDisappear.Interval = Random.Shared.Next(3000, 5000);
            NuggetDisappear.Start();
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

                    HealthUI();
                    DestroyAll(nugget.pb, GameScene);
                    soundNugget.PlaySound();
                }
            }
        }

        #endregion

        //Hitboxy
        HitboxLeft = new Rectangle(Player.Left - 3, Player.Top + 20, 3, Player.Height - 40);
        HitboxRight = new Rectangle(Player.Right, Player.Top + 20, 3, Player.Height - 40);
        HitboxUp = new Rectangle(Player.Left + 10, Player.Top - 4, Player.Width - 10, 2);
        HitboxDown = new Rectangle(Player.Left + 2, Player.Bottom - 2, Player.Width - 4, 2);

        HitboxDashLeft = new Rectangle(Player.Left - 15, Player.Top, Player.Width, Player.Height - 2);
        HitboxDashRight = new Rectangle(Player.Left + 15, Player.Top, Player.Width, Player.Height - 2);

        //platformy
        foreach (PictureBox terrain in GameScene.Controls.OfType<PictureBox>().Where(x => x.Tag != null))
        {
            if (terrain.Tag.ToString().Contains("Terrain"))
            {
                if (terrain.Bounds.IntersectsWith(HitboxLeft))
                {
                    moveLeft = false;
                    if (onGround)
                        Player.Left = terrain.Right;
                }

                if (terrain.Bounds.IntersectsWith(HitboxRight))
                {
                    moveRight = false;
                    if (onGround)
                        Player.Left = terrain.Left - Player.Width;
                }

                if (terrain.Bounds.IntersectsWith(HitboxDown) && Player.Bottom - 20 <= terrain.Top && jumpSpeed < 0)
                {
                    Player.Top = terrain.Top - Player.Height + 1;
                    onTop = true;
                    jumpSpeed = 0;
                    isJumping = false;

                    //setup na pohoupnutí knížek
                    if (terrain.Tag.ToString().Contains("Book"))
                    {
                        if (!touchedGround)
                            landed = true;
                        touchedGround = true;

                        landedBlock = terrain as PictureBox;
                    }
                }
                if (terrain.Bounds.IntersectsWith(HitboxUp))
                {
                    Player.Top = terrain.Bottom + 3;
                    jumpSpeed = -2;
                    isJumping = false;
                    underTerrain = true;
                }
            }
        }

        //pohoupnutí knížek
        if (landedBlock != null && landed)
        {
            switch (hupIndex)
            {
                case 0: landedBlock.Top += 1; Player.Top += 1; break;
                case 2: landedBlock.Top += 2; Player.Top += 2; break;
                case 4: landedBlock.Top += 3; Player.Top += 3; break;
                case 6: landedBlock.Top += 2; Player.Top += 2; break;
                case 8: landedBlock.Top += 1; Player.Top += 1; break;
                case 12: landedBlock.Top -= 1; Player.Top -= 1; break;
                case 14: landedBlock.Top -= 2; Player.Top -= 2; break;
                case 16: landedBlock.Top -= 3; Player.Top -= 3; break;
                case 18: landedBlock.Top -= 2; Player.Top -= 2; break;
                case 20: landedBlock.Top -= 1; Player.Top -= 1; landed = false; hupIndex = 0; break;
            }
            hupIndex++;
        }
        if (!onGround)
            touchedGround = false;

        //Doleva a Doprava
        if (!(A && D))
        {
            if (D && moveRight == true && !(Player.Right >= GameScene.Width) && !banInput && !tutBanMovement)
            {
                Player.Left += 8; //movementSpeed
                lastInputLeft = false;

                if (tutorialPhase == 2 && !typing)
                    writeInstructions = true;
            }
            if (A && moveLeft == true && !(Player.Left <= 0) && !banInput && !tutBanMovement)
            {
                Player.Left -= 8; //movementSpeed
                lastInputLeft = true;

                if (tutorialPhase == 2 && !typing)
                    writeInstructions = true;
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

        if (Space && onGround && !jumpCooldown && !tutBanJump)
        {
            isJumping = true;
            jumpSpeed = 24;
            jumpCooldown = true;
            JumpCooldown.Start();
            soundJump.PlaySound();

            if (tutorialPhase == 3 && !typing)
                writeInstructions = true;
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

        //pružina
        foreach (PictureBox terrain in GameScene.Controls.OfType<PictureBox>().Where(x => x.Tag != null))
        {
            if (terrain.Tag.ToString().Contains("Spring") && Player.Bounds.IntersectsWith(terrain.Bounds))
            {
                isJumping = true;
                jumpSpeed = 31;
                soundSpring.PlaySound();
            }
        }

        //Dash
        if (E && facingRight && !dashRight && !dashLeft && canDash && !tutBanDash)
        {
            dashRight = true;
            dashX = Player.Left;
            banInput = true;
            dashIndex = 0;
            Dash.Interval = 300;
            Dash.Start();
            soundDash.PlaySound();

            if (tutorialPhase == 13 && !typing)
                writeInstructions = true;
        }

        if (E && !facingRight && !dashRight && !dashLeft && canDash && !tutBanDash)
        {
            dashLeft = true;
            dashX = Player.Left;
            banInput = true;
            dashIndex = 0;
            Dash.Interval = 300;
            Dash.Start();
            soundDash.PlaySound();

            if (tutorialPhase == 13 && !typing)
                writeInstructions = true;
        }
        foreach (PictureBox terrain in GameScene.Controls.OfType<PictureBox>().Where(x => x.Tag != null))
        {
            if (terrain.Tag.ToString().Contains("Terrain"))
            {
                if (terrain.Bounds.IntersectsWith(HitboxDashLeft))
                    dashLeft = false;
                if (terrain.Bounds.IntersectsWith(HitboxDashRight))
                    dashRight = false;
            }
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

        //ENEMY
        //Poèet a Pole Enemákù
        int iEnemy = 0;
        foreach (PictureBox enemy in GameScene.Controls.OfType<PictureBox>().Where(x => x.Tag == "Enemy"))
            iEnemy++;

        if (iEnemy == 0)
        {
            //žádný enemy = ukonèit level
            switch (levelCount)
            {
                case 1: Level1(); break;
                case 2: Level2(); break;
                case 3: Level3(); break;
                case 4: Level4(); break;
                case 5: Level5(); break;
                case 6:
                    //VICTORY
                    if (!won)
                    {
                        won = true;
                        soundWin.PlaySound();
                        continueGame = false;
                        completedGame = true;
                        if (difficulty == "Insane")
                            hardestDifficulty = true;
                        btContinue.Enabled = false;
                        SaveFileWrite();
                        MessageBox.Show("yey");
                        UpdateProgress();
                        FullReset();
                        UpdateMethod.Stop();
                        GameScene.Visible = false;
                        GameScene.Enabled = false;
                        Menu.Visible = true;
                        Menu.Enabled = true;
                        Focus();
                    }
                    break;
            }
            levelCount++;
        }
        else
        {
            //když existují enemy (mùže být kombat)

            #region AttackQ
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
            if (Q && !onGround && !attackQcooldown && enemyObjectArray[closestIndex].Top - Player.Bottom > 40 && !tutBanQ)
            {
                foreach (Enemy enemy in enemyArray)
                {
                    if (enemy == stark && starkQ || enemy != stark)
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

                            fixQ = false;
                            hitQ = false;
                            unHitable = true;
                            attackQcooldown = true;
                            soundFixQ = true;
                            abilityQIndex = 0;
                            AbilityQ.Interval = 5;
                            AbilityQ.Start();
                        }
                    }
                }
            }

            if (attackQphase1)
            {
                if (soundFixQ)
                {
                    soundQ.PlaySound();
                    soundFixQ = false;
                }

                if (QOnLeft && Player.Right - (closestEnemy.Right / 2) < 10)
                    Player.Left += 15;

                if (!QOnLeft && Player.Left - (closestEnemy.Right - closestEnemy.Width / 2) > 0)
                    Player.Left -= 15;

                if (Player.Bottom < closestEnemy.Top - 25)
                    Player.Top += 15;

                isJumping = false;
                jumpSpeed = 0;
                banInput = true;

                //pøedèasný ukonèení
                if ((Math.Abs(Player.Left + Player.Width / 2) - (closestEnemy.Left + closestEnemy.Width / 2)) < 40 && closestEnemy.Top - Player.Bottom < 40)
                    AbilityQ.Interval = 1;

            }
            if (attackQphase2 && !fixQ)
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

                //-HP
                foreach (Enemy enemy in enemyArray)
                {
                    if (enemy.pb == closestEnemy && !hitQ)
                    {
                        enemy.health -= 4;
                        hitQ = true;
                        enemy.CheckHealth(enemy, GameScene);
                        if (enemy.dead)
                            fixQ = true;

                        soundhitSomeone.PlaySound();
                    }
                }
            }

            #endregion

            #region Attack
            //Útok LMB
            if (LMB && cursor.Y < Player.Top && !attackLMBcooldown && !tutBanLMB)
            {
                //utok nahoru
                HitboxAttackTop = new PictureBox
                {
                    Left = Player.Left,
                    Top = Player.Top - rulerLength,
                    Width = Player.Width,
                    Height = rulerLength,
                    BackColor = Color.Blue
                };
                GameScene.Controls.Add(HitboxAttackTop);
                HitboxAttackTop.BringToFront();
                attackLMBcooldown = true;
                alreadyHit = false;
                abilityLMB.Interval = 100;
                abilityLMBIndex = 0;
                abilityLMB.Start();
                soundRuler.PlaySound();

                if (tutorialPhase == 6 && !typing)
                    writeInstructions = true;

            }
            else
            {
                if (LMB && !attackLMBcooldown && cursor.X > Player.Left + (Player.Width / 2) && !tutBanLMB)
                {
                    //utok doprava
                    HitboxAttackRight = new PictureBox
                    {
                        Left = Player.Right,
                        Top = Player.Top + 20,
                        Width = rulerLength,
                        Height = 75,
                        BackColor = Color.Blue
                    };
                    GameScene.Controls.Add(HitboxAttackRight);
                    HitboxAttackRight.BringToFront();
                    attackLMBcooldown = true;
                    alreadyHit = false;
                    abilityLMB.Interval = 100;
                    abilityLMBIndex = 0;
                    abilityLMB.Start();
                    soundRuler.PlaySound();

                    if (tutorialPhase == 6 && !typing)
                        writeInstructions = true;
                }
                if (LMB && !attackLMBcooldown && cursor.X < Player.Left + (Player.Width / 2) && !tutBanLMB)
                {
                    //utok doleva
                    HitboxAttackLeft = new PictureBox
                    {
                        Left = Player.Left - rulerLength,
                        Top = Player.Top + 20,
                        Width = rulerLength,
                        Height = 75,
                        BackColor = Color.Blue
                    };
                    GameScene.Controls.Add(HitboxAttackLeft);
                    HitboxAttackLeft.BringToFront();
                    attackLMBcooldown = true;
                    alreadyHit = false;
                    abilityLMB.Interval = 100;
                    abilityLMBIndex = 0;
                    abilityLMB.Start();
                    soundRuler.PlaySound();

                    if (tutorialPhase == 6 && !typing)
                        writeInstructions = true;
                }
            }

            #endregion

            foreach (Enemy enemy in enemyArray)
            {
                #region Lemka
                if (enemy.type == "Lemka" && enemy.health > 0)
                {
                    //Lemka movement
                    if (enemy.pb.Top + enemy.pb.Height > Player.Top + Player.Height - 5)
                    {
                        enemy.moveSwitch = false;
                        if (Math.Abs(enemy.pb.Left - Player.Left) < 20)
                        {
                            enemy.moveLeft = false;
                            enemy.moveRight = false;
                        }
                        else if (enemy.pb.Left < Player.Left)
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
                    if (((enemy.pb.Left - Player.Left - Player.Width) < 100 && enemy.pb.Left > Player.Right) ||
                        (Player.Left - (enemy.pb.Right) < 100 && (enemy.pb.Right) < Player.Left))
                    {
                        if ((Player.Top + Player.Height) > enemy.pb.Top && Player.Top < (enemy.pb.Top + enemy.pb.Height) && !lemkaCooldown)
                        {
                            if ((enemy.pb.Left - Player.Left - Player.Width) < 100 && enemy.pb.Left > Player.Right)
                                lemkaRight = false;
                            if (Player.Left - enemy.pb.Right < 100 && enemy.pb.Right < Player.Left)
                                lemkaRight = true;
                            enemy.moving = false;
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
                #endregion

                #region BossFight
                //BOSS FIGHT

                if (stark != null)
                {
                    //stark movement phase 1
                    if (enemy == stark && bossPhase == 1 && !starkIdle)
                    {
                        stark.moveSwitch = false;
                        if (Math.Abs(stark.pb.Left + 135 - Player.Left) < 20)
                        {
                            stark.moveLeft = false;
                            stark.moveRight = false;
                        }
                        else if (stark.pb.Left + 135 > Player.Left)
                        {
                            stark.moveLeft = true;
                            stark.moveRight = false;
                        }
                        else if (stark.pb.Left + 135 < Player.Left)
                        {
                            stark.moveRight = true;
                            stark.moveLeft = false;
                        }
                    }
                    //stark movement phase 2 je default moving (zapíná se po znièení baseballky)
                    if (enemy == stark && bossPhase == 2 && !starkIdle)
                        stark.moveSwitch = true;

                    //stark movement phase 3 (v timeru switchuje doleva/doprava)

                    //stark movement idle
                    if (enemy == stark && starkIdle)
                    {
                        stark.moveSwitch = false;
                        stark.moving = true;
                    }

                    //stark baseballka
                    if (enemy == stark && bossPhase == 1 && !baseballCooldown && !starkIdle)
                    {
                        if (Math.Abs(stark.pb.Left + 135 - Player.Left) < 40 && Player.Top + Player.Height > stark.pb.Top + 465)
                        {
                            stark.moving = false;
                            starkIndex = 0;
                            Stark.Interval = 1000;
                            Stark.Start();
                        }
                    }

                    //dmg hracovi
                    if (baseballka != null && baseballka.Bounds.IntersectsWith(Player.Bounds) && canGetHit)
                        Hit(baseballka);

                    //hrac dava dmg Starkovi
                    if (baseballGetDMG && !alreadyHit && baseballka != null && ((HitboxAttackLeft != null && baseballka.Bounds.IntersectsWith(HitboxAttackLeft.Bounds)) ||
                            (HitboxAttackRight != null && baseballka.Bounds.IntersectsWith(HitboxAttackRight.Bounds)) ||
                            (HitboxAttackTop != null && baseballka.Bounds.IntersectsWith(HitboxAttackTop.Bounds))))
                    {
                        stark.health -= 2;
                        alreadyHit = true;
                        stark.CheckHealth(stark, GameScene);
                        soundhitSomeone.PlaySound();
                    }

                    //zjisteni Starkovo HP a zmìna bossPhase
                    if (!changedPhase && (stark.health == 54 || stark.health == 48))
                    {
                        Stark.Interval = 1;
                        starkIndex = 1;
                        Stark.Start();
                        changedPhase = true;
                        if (stark.health == 54)
                        {
                            starkIdle = true;
                            if (Player.Left > GameScene.Width / 2)
                            {
                                stark.moveRight = true;
                                stark.moveLeft = false;
                            }
                            else
                            {
                                stark.moveLeft = true;
                                stark.moveRight = false;
                            }
                        }
                        foreach (Enemy bossEnemy in enemyArray)
                        {
                            if (bossEnemy != stark)
                            {
                                bossEnemy.health = 0;
                                bossEnemy.CheckHealth(bossEnemy, GameScene);
                            }
                        }
                        SpawnEnemyBoss();
                    }
                    else if (!changedPhase && (stark.health == 40)) //finalni zniceni baseballky
                    {
                        if (baseballka != null)
                            DestroyAll(baseballka, GameScene);
                        stark.moving = true;
                        stark.moveSwitch = true;
                        stark.moveRight = true;
                        stark.moveLeft = false;
                        bossPhase = 2;
                        Stark.Interval = 3000;
                        Stark.Start();
                        changedPhase = true;
                        starkIdle = false;

                        foreach (Enemy bossEnemy in enemyArray)
                        {
                            if (bossEnemy != stark)
                            {
                                bossEnemy.health = 0;
                                bossEnemy.CheckHealth(bossEnemy, GameScene);
                            }
                        }
                    }
                    else if (!changedPhase && (stark.health == 34 || stark.health == 28))
                    {
                        if (!changedPhase && stark.health == 34)
                        {
                            starkIdle = true;
                            if (Player.Left > GameScene.Width / 2)
                            {
                                stark.moveRight = true;
                                stark.moveLeft = false;
                            }
                            else
                            {
                                stark.moveLeft = true;
                                stark.moveRight = false;
                            }
                        }
                        changedPhase = true;
                        foreach (Enemy bossEnemy in enemyArray)
                        {
                            if (bossEnemy != stark)
                            {
                                bossEnemy.health = 0;
                                bossEnemy.CheckHealth(bossEnemy, GameScene);
                            }
                        }
                        SpawnEnemyBoss();
                    }
                    else if (!changedPhase && (stark.health == 20))
                    {
                        foreach (Enemy bossEnemy in enemyArray)
                        {
                            if (bossEnemy != stark)
                            {
                                bossEnemy.health = 0;
                                bossEnemy.CheckHealth(bossEnemy, GameScene);
                            }
                        }
                        Stark.Stop();
                        bossPhase = 3;
                        changedPhase = true;
                        starkQ = true;
                        starkIdle = true;
                        if (Player.Left > stark.pb.Right)
                        {
                            stark.moveRight = false;
                            stark.moveLeft = true;
                        }
                        else
                        {
                            stark.moveLeft = false;
                            stark.moveRight = true;
                        }
                        if (stark.moveRight)
                        {
                            Player.Left = 170;
                            Player.Top = 680;
                            hacek = new PictureBox
                            {
                                Left = 15,
                                Top = 105,
                                Width = 110,
                                Height = 180,
                                BackColor = Color.Red,
                                BackgroundImageLayout = ImageLayout.Stretch,
                            };
                            hacekPlatforma = new PictureBox
                            {
                                Left = 15,
                                Top = 284,
                                Width = 110,
                                Height = 27,
                                BackgroundImage = Resources.Knizka,
                                BackgroundImageLayout = ImageLayout.Stretch,
                            };
                            lbRozvrh1.Visible = true;
                        }
                        else
                        {
                            Player.Left = 1250;
                            Player.Top = 680;
                            hacek = new PictureBox
                            {
                                Left = 1395,
                                Top = 105,
                                Width = 110,
                                Height = 180,
                                BackColor = Color.Red,
                                BackgroundImageLayout = ImageLayout.Stretch,
                            };
                            hacekPlatforma = new PictureBox
                            {
                                Left = 1395,
                                Top = 284,
                                Width = 110,
                                Height = 27,
                                BackgroundImage = Resources.Knizka,
                                BackgroundImageLayout = ImageLayout.Stretch,
                            };
                            lbRozvrh2.Visible = true;
                        }
                        GameScene.Controls.Add(hacek);
                        GameScene.Controls.Add(hacekPlatforma);

                        stark.movementSpeed = 10;
                        starkIndex = 0;
                        Stark.Interval = 500;
                        Stark.Start();
                    }
                    else if (!(stark.health == 54) && !(stark.health == 48) && !(stark.health == 40) && !(stark.health == 34) &&
                        !(stark.health == 28) && !(stark.health == 20) && stark.health > 20)
                    {
                        changedPhase = false;
                    }

                    int bossEnemyCount = 0;
                    foreach (PictureBox enemyBoss in GameScene.Controls.OfType<PictureBox>().Where(x => x.Tag == "Enemy"))
                        bossEnemyCount++;

                    if ((stark.health == 54 && bossEnemyCount == 1) || (stark.health == 34 && bossEnemyCount == 1))
                        starkIdle = false;

                    StarkHealth.Width = stark.health * 10;
                }

                #endregion

                //enemy doleva a doprava
                if (enemy.moving)
                {
                    if (enemy.moveLeft)
                    {
                        if (enemy.pb.Left > enemy.xLeft)
                            enemy.pb.Left -= enemy.movementSpeed;
                        else if (enemy.moveSwitch)
                        {
                            enemy.moveLeft = false;
                            enemy.moveRight = true;
                        }
                    }
                    if (enemy.moveRight)
                    {
                        if (enemy.pb.Left < enemy.xRight)
                            enemy.pb.Left += enemy.movementSpeed;
                        else if (enemy.moveSwitch)
                        {
                            enemy.moveLeft = true;
                            enemy.moveRight = false;
                        }
                    }
                }


                //bouchání LMB
                if (enemy != stark)
                {
                    if ((HitboxAttackLeft != null && enemy.pb.Bounds.IntersectsWith(HitboxAttackLeft.Bounds)) ||
                        (HitboxAttackRight != null && enemy.pb.Bounds.IntersectsWith(HitboxAttackRight.Bounds)) ||
                        (HitboxAttackTop != null && enemy.pb.Bounds.IntersectsWith(HitboxAttackTop.Bounds)))
                    {
                        if (!alreadyHit)
                        {
                            enemy.health -= 2;
                            alreadyHit = true;
                            enemy.CheckHealth(enemy, GameScene);
                            soundhitSomeone.PlaySound();
                        }
                    }
                }

                //naražení do enemy -HP
                if (enemy.pb.Bounds.IntersectsWith(Player.Bounds) && canGetHit && (enemy != stark || (enemy == stark && bossPhase == 3)))
                    Hit(enemy.pb);

                //Sysalova smrt
                if (enemy.type == "Sysalova" && enemy.dead)
                {
                    stopwatch.Reset();
                    basnicka.Pause();
                }

                #region Projektily
                //let projektilù
                if (enemy.projectile != null)
                {
                    //mimo obrazovku znièení
                    if (enemy.projectile.Left < 0 || enemy.projectile.Right < 0 || enemy.projectile.Top < 0 || enemy.projectile.Bottom < 0)
                    {
                        DestroyAll(enemy.projectile, GameScene);
                        enemy.projectileStop = true;
                        soundProjectileDestroy.PlaySound();
                    }
                    foreach (PictureBox terrain in GameScene.Controls.OfType<PictureBox>().Where(x => x.Tag != null))
                    {
                        if (terrain.Tag.ToString().Contains("Terrain"))
                        {
                            //Stark nièí knížky
                            if (enemy.type == "Stark" && enemy.projectile.Bounds.IntersectsWith(terrain.Bounds) && terrain.Tag.ToString().Contains("Book"))
                            {
                                if (terrain == bookLeft.pb)
                                    bookLeftDestroyed = true;
                                if (terrain == bookRight.pb)
                                    bookRightDestroyed = true;
                                if (hacekOnBook != null && hacekOnBook.pb.Left == terrain.Left)
                                {
                                    hacekOnBook.health = 0;
                                    hacekOnBook.CheckHealth(hacekOnBook, GameScene);
                                }
                                DestroyAll(terrain, GameScene);
                                DestroyAll(enemy.projectile, GameScene);
                                enemy.projectileStop = true;
                                soundDestroy.PlaySound();
                            }
                            //když projektil narazí do terénu, znièí se
                            if (enemy.projectile.Bounds.IntersectsWith(terrain.Bounds))
                            {
                                DestroyAll(enemy.projectile, GameScene);
                                enemy.projectileStop = true;
                                soundProjectileDestroy.PlaySound();
                            }
                        }
                    }
                    if (enemy.projectile.Bounds.IntersectsWith(Player.Bounds) && canGetHit)
                    {
                        //hráè dostává dmg od projektilu
                        if (enemy.projectile.Name == "projectileO" + (Enemy.ProjectileCountO - 1) || enemy.projectile.Name == "projectileH" + (Enemy.ProjectileCountH - 1)
                            || enemy.projectile.Name == "projectileS" + (Enemy.ProjectileCountS - 1))
                        {
                            Hit(enemy.projectile);
                            DestroyAll(enemy.projectile, GameScene);
                            enemy.projectileStop = true;
                        }
                    }
                    if (enemy.type == "Oberhofnerova" || enemy.type == "Stark")
                    {
                        //Oberhofnerovy a Starkovo pohyb projektilù
                        if (!enemy.projectileStop && !enemy.projectileParry)
                        {
                            if (enemy.projectileGoRight)
                                enemy.projectile.Left += enemy.projectileSpeedX;
                            else
                                enemy.projectile.Left -= enemy.projectileSpeedX;

                            if (enemy.projectileGoDown)
                                enemy.projectile.Top += enemy.projectileSpeedY;
                            else
                                enemy.projectile.Top -= enemy.projectileSpeedY;
                        }
                    }
                    if (enemy.type == "Hacek")
                    {
                        //Háèkovo navádìný støely
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
                            }
                            else
                            {
                                enemy.projectileSpeedX = (Math.Abs(Player.Left + Player.Width / 2 - enemy.projectile.Left + 15) / 80) + 4;
                                enemy.projectileSpeedY = (Math.Abs(Player.Top + Player.Height / 2 - enemy.projectile.Top + 15) / 50) + 4;
                            }

                            if (enemy.projectile.Left + 15 > Player.Left + Player.Width / 2)
                                enemy.projectile.Left -= enemy.projectileSpeedX;
                            if (enemy.projectile.Left + 15 < Player.Left + Player.Width / 2)
                                enemy.projectile.Left += enemy.projectileSpeedX;
                            if (enemy.projectile.Top + 15 > Player.Top + Player.Height / 2)
                                enemy.projectile.Top -= enemy.projectileSpeedY;
                            if (enemy.projectile.Top + 15 < Player.Top + Player.Height / 2)
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
                    if (enemy.type == "Stark")
                    {
                        //odrážení køídy do Starka
                        if ((HitboxAttackLeft != null && HitboxAttackLeft.Bounds.IntersectsWith(enemy.projectile.Bounds))
                            || (HitboxAttackRight != null && HitboxAttackRight.Bounds.IntersectsWith(enemy.projectile.Bounds))
                            || (HitboxAttackTop != null && HitboxAttackTop.Bounds.IntersectsWith(enemy.projectile.Bounds)))
                        {
                            enemy.projectileParry = true;
                            if (HitboxAttackLeft != null && HitboxAttackLeft.Bounds.IntersectsWith(enemy.projectile.Bounds))
                                enemy.projectileLeft = true;
                            if (HitboxAttackRight != null && HitboxAttackRight.Bounds.IntersectsWith(enemy.projectile.Bounds))
                                enemy.projectileRight = true;
                            if (HitboxAttackTop != null && HitboxAttackTop.Bounds.IntersectsWith(enemy.projectile.Bounds))
                                enemy.projectileUp = true;
                        }

                        //let køídy
                        if (enemy.projectileLeft && !enemy.projectileStop)
                            enemy.projectile.Left -= 6;
                        if (enemy.projectileRight && !enemy.projectileStop)
                            enemy.projectile.Left += 6;
                        if (enemy.projectileUp && !enemy.projectileStop)
                            enemy.projectile.Top -= 6;

                        //dmg Starkovi, znièí se køída
                        if (enemy.projectile.Bounds.IntersectsWith(enemy.pb.Bounds) && enemy.projectileParry && !alreadyHit)
                        {
                            enemy.projectileStop = true;
                            alreadyHit = true;
                            enemy.CheckHealth(enemy, GameScene);
                            soundhitSomeone.PlaySound();
                            enemy.health -= 2;
                            DestroyAll(enemy.projectile, GameScene);
                        }
                    }
                }
                #endregion
            }
        }

        #region Absence
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
                    Hit(absence.pb);
            }
        }
        #endregion

        // Sysalova
        if (stopwatch.ElapsedMilliseconds > 15000)
        {
            stopwatch.Restart();
            soundHit.PlaySound();
            if (!cheatHealth || unHitable)
                playerHealth--;
            if (playerHealth <= 0)
                continueGame = false;
            SaveFileWrite();
            HealthUI();
        }
        if (reader.Position >= reader.Length)
        {
            reader.Position = 0;
            basnicka.Play();
        }

        // Odhozeni od nepratel
        if (knockback)
        {
            if (Player.Right < enMiddle && moveLeft)
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

        // Smrt
        if (playerHealth <= 0)
        {
            if (!soundDeathOnce)
            {
                soundDeath.PlaySound();
                soundDeathOnce = true;
            }

            UpdateMethod.Interval = 35;
            GameScene.Enabled = false;
            lbGameOver.Left = 450;
            lbGameOver.Top = 195;
            lbPress.Left = 586;
            lbPress.Top = 401;
            disableallInputs = true;
        }

        //Animace
        //if (facingRight)
        //    Player.Image = Resources.Gurl_right;
        //else
        //    Player.Image = Resources.Gurl_left;


        //MESS
        info1 = baseballCooldown.ToString();
        if (stopwatch != null)
            info1 = stopwatch.ElapsedMilliseconds.ToString();
        if (stark != null)
            info = Convert.ToString(stark.health);
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
                "\nBossPhase: " + bossPhase.ToString() +
                "\nDifficulty: " + difficulty +
                "\nInfo: " + info +
                "\nInfo1: " + info1 +
                "\nLanded: " + landed +
                "\nStarkIdle: " + starkIdle +
                "\niEnemy: " + iEnemy.ToString();
    }

    #region Tutorial
    private async void InstrukceTimer_Tick(object sender, EventArgs e)
    {
        if (writeInstructions)
        {
            if (tutorialPhase < poleInstrukci.Length)
            {
                writeInstructions = false;
                typing = true;
                lbTutorial.Text = string.Empty;
                foreach (char a in poleInstrukci[tutorialPhase])
                {
                    lbTutorial.Text += a;
                    Application.DoEvents();
                    await Task.Delay(20);
                }

                if (tutorialPhase == 1)
                    tutBanMovement = false;
                else if (tutorialPhase == 2)
                    tutBanJump = false;
                else if (tutorialPhase == 5)
                    tutBanLMB = false;
                else if (tutorialPhase == 12)
                    tutBanDash = false;

                await Task.Delay(2000);
                tutorialPhase++;
                typing = false;

                switch (tutorialPhase)
                {
                    case 2 or 3 or 6 or 11 or 13 or 16 or 21 or 24:
                        break;
                    default:
                        writeInstructions = true;
                        break;
                }

                if (tutorialPhase == 9)
                {
                    tutorialLemka.moving = true;
                    tutorialLemka.pb.Left = 1100;
                    tutorialLemka.pb.Top = 137;
                }
                else if (tutorialPhase == 14)
                {
                    tutorialSysalova.pb.Left = 1200;
                    tutorialSysalova.pb.Top = 157;
                    basnicka.Resume();
                    stopwatch.Restart();
                }
                else if (tutorialPhase == 19)
                {
                    tutorialHacek.pb.Left = 1300;
                    tutorialHacek.pb.Top = GameScene.Height - tutorialHacek.pb.Height;
                    Hacek.Start();
                    tutBanQ = false;
                }
                else if (tutorialPhase == 22)
                {
                    tutorialOberhofnerova.pb.Left = 800;
                    tutorialOberhofnerova.pb.Top = 112;
                    tutorialOberhofnerova.moving = true;
                    Oberhofnerova.Start();
                }
            }
            else
            {
                //konec tutorialu
                FullReset();
                GameScene.Enabled = false;
                GameScene.Visible = false;
                Menu.Visible = true;
                Menu.Enabled = true;
                Focus();
                soundWin.PlaySound();
                cheatHealth = false;
                tutorial = false;
                tutorialPhase = 0;
                lbTutorial.Text = string.Empty;
                UpdateMethod.Stop();
                InstrukceTimer.Stop();
            }
        }
        if (tutorialLemka.dead && !typing && tutorialPhase == 11)
            writeInstructions = true;
        if (tutorialSysalova.dead & !typing && tutorialPhase == 16)
            writeInstructions = true;
        if (tutorialHacek.dead && !typing && tutorialPhase == 21)
            writeInstructions = true;
        if (tutorialOberhofnerova.dead && !typing && tutorialPhase == 24)
            writeInstructions = true;
    }

    #endregion

    #region playerTimers
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

    #endregion

    #region Absence
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

    static private void Absence(Absence absence)
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

                absence.plusCoordinates = !absence.plusCoordinates;

                absence.help = absence.from;
                absence.from = absence.to;
                absence.to = absence.help;

                absence.boundsX = absence.positionX;
                absence.boundsY = absence.positionY;
            }

            absence.indexDirection++;
        }
    }

    #endregion

    #region EnemyTimers

    private void Lemka_Tick(object sender, EventArgs e)
    {
        if (lemkaIndex == 0)
        {
            if (lemkaRight)
            {
                HitboxLemkaRight = new PictureBox
                {
                    Left = lemka.pb.Right,
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
            soundLemka.PlaySound();
            Lemka.Interval = 100;
        }
        if (lemkaIndex == 1)
        {
            if (HitboxLemkaLeft != null)
                DestroyAll(HitboxLemkaLeft, GameScene);
            if (HitboxLemkaRight != null)
                DestroyAll(HitboxLemkaRight, GameScene);
            Lemka.Interval = 3000;
            lemka.moving = true;
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
            if (enemy.type == "Oberhofnerova" && enemy.health > 0)
            {
                enemy.ShootProjectile(enemy, Player, GameScene);
                soundProjectile.PlaySound();
            }
        }
    }

    private void Hacek_Tick(object sender, EventArgs e)
    {
        foreach (Enemy enemy in enemyArray)
        {
            if (enemy.type == "Hacek" && enemy.health > 0)
            {
                enemy.ShootProjectile(enemy, Player, GameScene);
                soundProjectile.PlaySound();
            }
        }
        Hacek.Stop();
    }
    private void Stark_Tick(object sender, EventArgs e)
    {
        if (bossPhase == 1)
        {
            //bouchani baseballkou
            if (starkIndex == 0)
            {
                //bouchne
                baseballka = new PictureBox
                {
                    Left = stark.pb.Left + 55,
                    Top = stark.pb.Top + 465,
                    Width = 225,
                    Height = 185,
                    BackColor = Color.Purple,
                };
                GameScene.Controls.Add(baseballka);
                baseballka.BringToFront();
                baseballGetDMG = false;
                baseballCooldown = true;
                baseballSlam++;

                soundBaseball.PlaySound();

                if (baseballSlam % 3 == 0)
                {
                    baseballGetDMG = true;
                    baseballCooldown = true;
                    Stark.Stop();
                }
                else
                {
                    baseballGetDMG = false;
                    Stark.Interval = 500;
                }
            }
            else if (starkIndex == 1)
            {
                DestroyAll(baseballka, GameScene);
                stark.moving = true;
                Stark.Interval = 3000;
            }
            else if (starkIndex == 2)
                baseballCooldown = false;

            starkIndex++;
        }
        else if (bossPhase == 2 && !starkIdle)
        {
            //spawn enemy a strileni kridy
            stark.ShootProjectile(stark, Player, GameScene);
            stark.projectileParry = false;
            stark.projectileLeft = false;
            stark.projectileRight = false;
            stark.projectileUp = false;
            soundChalk.PlaySound();
        }
        else if (bossPhase == 3)
        {
            if (starkIndex == 0)
            {
                //special, hacek objevi a nici knizky, prida pruzinu
                int cooldown = 0;
                foreach (Terrain book in terrainArray)
                {
                    if (book.pb.Tag.ToString().Contains("Book") && book.pb.Bounds != Rectangle.Empty)
                    {
                        DestroyAll(book.pb, GameScene);
                        soundDestroy.PlaySound();
                        GameScene.Refresh();
                        Thread.Sleep(1000);
                        cooldown += 1000;
                    }
                }
                GameScene.Controls.Add(spring.pb);
                GameScene.Refresh();
                soundSpring.PlaySound();
                Thread.Sleep(4000 - cooldown);

                DestroyAll(hacek, GameScene);
                DestroyAll(hacekPlatforma, GameScene);
                lbRozvrh1.Visible = false;
                lbRozvrh2.Visible = false;
                Stark.Interval = 4000;
            }
            if (starkIndex == 1)
            {
                stark.pb.BackColor = Color.Green;
                Stark.Interval = 1000;
            }
            else if (starkIndex == 2)
            {
                stark.moveLeft = !stark.moveLeft;
                stark.moveRight = !stark.moveRight;
                stark.pb.BackColor = Color.Yellow;
                Stark.Interval = 2000;
            }
            else if (starkIndex == 3)
            {
                stark.pb.BackColor = Color.Red;
                Stark.Interval = 3000;
                starkIndex = 0;
            }
            starkIndex++;
        }
    }

    #endregion

    #region Voidy

    private void SpawnEnemyBoss()
    {
        if (Player.Left > GameScene.Width / 2)
            playerSideLeft = false;
        else
            playerSideLeft = true;

        int random1 = 0, random2 = 0, LemkaMove = 0;
        if (playerSideLeft && bookLeftDestroyed)
        {
            random1 = Random.Shared.Next(1, 4);
            random2 = Random.Shared.Next(1, 4);
            while (random1 == random2)
                random1 = Random.Shared.Next(1, 4);

            LemkaMove = 925;
        }
        else if (playerSideLeft && !bookLeftDestroyed)
        {
            random1 = Random.Shared.Next(1, 5);
            random2 = Random.Shared.Next(1, 5);
            while (random1 == random2)
                random1 = Random.Shared.Next(1, 5);

            LemkaMove = 1393;
        }
        if (!playerSideLeft && bookRightDestroyed)
        {
            random1 = Random.Shared.Next(1, 4);
            random2 = Random.Shared.Next(1, 4);
            while (random1 == random2)
                random1 = Random.Shared.Next(1, 4);

            LemkaMove = 595;
        }
        else if (!playerSideLeft && !bookRightDestroyed)
        {
            random1 = Random.Shared.Next(1, 5);
            random2 = Random.Shared.Next(1, 5);
            while (random1 == random2)
                random1 = Random.Shared.Next(1, 5);

            LemkaMove = 18;
        }

        if (playerSideLeft)
        {
            switch (random1)
            {
                case 1: bossEnemy1 = new(1296, 112, 100, 100, Color.Red, OberhofnerovaHP, true, 15, 1408, OberhofnerovaMovementSpeed, "Oberhofnerova", 2000, GameScene); Oberhofnerova.Start(); break;
                case 2: bossEnemy1 = new(714, 500, 100, 160, Color.Red, SysalovaHP, false, 0, 0, 0, "Sysalova", 0, GameScene); basnicka.Resume(); stopwatch.Restart(); break;
                case 3: bossEnemy1 = new(1420, 616, 110, 180, Color.Red, LemkaHP, true, LemkaMove, 1420, LemkaMovementSpeed, "Lemka", 0, GameScene); break;
                case 4: bossEnemy1 = new(1282, 418, 110, 180, Color.Red, HacekHP, false, 0, 0, 0, "Hacek", 3000, GameScene); Hacek.Start(); hacekOnBook = bossEnemy1; break;
            }
            switch (random2)
            {
                case 1: bossEnemy2 = new(1296, 112, 100, 100, Color.Red, OberhofnerovaHP, true, 15, 1408, OberhofnerovaMovementSpeed, "Oberhofnerova", 2000, GameScene); Oberhofnerova.Start(); break;
                case 2: bossEnemy2 = new(714, 500, 100, 160, Color.Red, SysalovaHP, false, 0, 0, 0, "Sysalova", 0, GameScene); basnicka.Resume(); stopwatch.Restart(); break;
                case 3: bossEnemy2 = new(1420, 616, 110, 180, Color.Red, LemkaHP, true, LemkaMove, 1420, LemkaMovementSpeed, "Lemka", 0, GameScene); break;
                case 4: bossEnemy2 = new(1282, 418, 110, 180, Color.Red, HacekHP, false, 0, 0, 0, "Hacek", 3000, GameScene); Hacek.Start(); hacekOnBook = bossEnemy2; break;
            }
        }
        else
        {
            switch (random1)
            {
                case 1: bossEnemy1 = new(100, 112, 100, 100, Color.Red, OberhofnerovaHP, true, 15, 1408, OberhofnerovaMovementSpeed, "Oberhofnerova", 2000, GameScene); Oberhofnerova.Start(); break;
                case 2: bossEnemy1 = new(714, 500, 100, 160, Color.Red, SysalovaHP, false, 0, 0, 0, "Sysalova", 0, GameScene); basnicka.Resume(); stopwatch.Restart(); break;
                case 3: bossEnemy1 = new(0, 616, 110, 180, Color.Red, LemkaHP, true, 0, LemkaMove, LemkaMovementSpeed, "Lemka", 0, GameScene); break;
                case 4: bossEnemy1 = new(122, 419, 110, 180, Color.Red, HacekHP, false, 0, 0, 0, "Hacek", 3000, GameScene); Hacek.Start(); hacekOnBook = bossEnemy1; break;
            }
            switch (random2)
            {
                case 1: bossEnemy2 = new(100, 112, 100, 100, Color.Red, OberhofnerovaHP, true, 15, 1408, OberhofnerovaMovementSpeed, "Oberhofnerova", 2000, GameScene); Oberhofnerova.Start(); break;
                case 2: bossEnemy2 = new(714, 500, 100, 160, Color.Red, SysalovaHP, false, 0, 0, 0, "Sysalova", 0, GameScene); basnicka.Resume(); stopwatch.Restart(); break;
                case 3: bossEnemy2 = new(0, 616, 110, 180, Color.Red, LemkaHP, true, 0, LemkaMove, LemkaMovementSpeed, "Lemka", 0, GameScene); break;
                case 4: bossEnemy2 = new(122, 419, 110, 180, Color.Red, HacekHP, false, 0, 0, 0, "Hacek", 3000, GameScene); Hacek.Start(); hacekOnBook = bossEnemy2; break;
            }
        }
        enemyArray = new Enemy[] { stark, bossEnemy1, bossEnemy2 };
    }
    private void NuggetDisappear_Tick(object sender, EventArgs e)
    {
        DestroyAll(nuggetPB, GameScene);
        NuggetDisappear.Stop();
    }
    void Hit(PictureBox pb)
    {
        soundHit.PlaySound();
        enMiddle = pb.Right;
        canGetHit = false;
        knockback = true;
        dmgIndex = 0;
        DMGcooldown.Interval = 100;
        DMGcooldown.Start();
        if (!cheatHealth || unHitable)
            playerHealth--;
        if (playerHealth <= 0)
            continueGame = false;
        SaveFileWrite();
        HealthUI();
    }
    static private void DestroyAll(PictureBox pb, Panel panel)
    {
        pb.Bounds = Rectangle.Empty;
        panel.Controls.Remove(pb);
        pb.Dispose();
    }
    void Reset()
    {
        stopwatch.Reset();
        basnicka.Pause();

        nuggetSpawn = false;

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

        //inputy
        A = false;
        D = false;
        Space = false;
        Q = false;
        LMB = false;

        Absence1.Stop();
        Absence2.Stop();
        Oberhofnerova.Stop();
        Hacek.Stop();

        if (enemyArray != null)
        {
            foreach (Enemy enemy in enemyArray)
            {
                enemy.health = 0;
                enemy.CheckHealth(enemy, GameScene);
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
        if (baseballka != null)
            DestroyAll(baseballka, GameScene);

        //reset hráèe
        Player.Left = 75;
        Player.Top = 526;

        StarkHealthBackground.Visible = false;
        StarkHealth.Visible = false;
    }
    void FullReset()
    {
        //pozice,timery,bloky,enemy...
        Reset();

        lbGameOver.Top = -200;
        lbPress.Top = -200;

        //default promenny
        canDash = true;
        attackLMBcooldown = false;
        levelCount = 1;
        if (difficulty != "Insane")
            playerHealth = 5;
        else
            playerHealth = 1;
        canGetHit = true;
        disableallInputs = false;
        unHitable = false;
        nuggetSpawn = false;
        bossPhase = 0;
        baseballSlam = 0;
        starkQ = false;
        baseballGetDMG = false;
        baseballCooldown = false;
        changedPhase = false;
        UpdateMethod.Interval = 1;
        GameScene.Enabled = true;
        lbLevel.ForeColor = Color.Black;

        HealthUI();
    }

    void HealthUI()
    {
        if (difficulty != "Insane")
        {
            switch (playerHealth)
            {
                case 1:
                    health1.BackgroundImage = Resources.heart_full;
                    health2.BackgroundImage = Resources.heart_empty;
                    health3.BackgroundImage = Resources.heart_empty;
                    health4.BackgroundImage = Resources.heart_empty;
                    health5.BackgroundImage = Resources.heart_empty;
                    break;
                case 2:
                    health1.BackgroundImage = Resources.heart_full;
                    health2.BackgroundImage = Resources.heart_full;
                    health3.BackgroundImage = Resources.heart_empty;
                    health4.BackgroundImage = Resources.heart_empty;
                    health5.BackgroundImage = Resources.heart_empty;
                    break;
                case 3:
                    health1.BackgroundImage = Resources.heart_full;
                    health2.BackgroundImage = Resources.heart_full;
                    health3.BackgroundImage = Resources.heart_full;
                    health4.BackgroundImage = Resources.heart_empty;
                    health5.BackgroundImage = Resources.heart_empty;
                    break;
                case 4:
                    health1.BackgroundImage = Resources.heart_full;
                    health2.BackgroundImage = Resources.heart_full;
                    health3.BackgroundImage = Resources.heart_full;
                    health4.BackgroundImage = Resources.heart_full;
                    health5.BackgroundImage = Resources.heart_empty;
                    break;
                case 5:
                    health1.BackgroundImage = Resources.heart_full;
                    health2.BackgroundImage = Resources.heart_full;
                    health3.BackgroundImage = Resources.heart_full;
                    health4.BackgroundImage = Resources.heart_full;
                    health5.BackgroundImage = Resources.heart_full;
                    break;
            }
        }
        else
        {
            health1.BackgroundImage = Resources.heart_full;
            health2.BackgroundImage = Resources.heart_empty;
            health3.BackgroundImage = Resources.heart_empty;
            health4.BackgroundImage = Resources.heart_empty;
            health5.BackgroundImage = Resources.heart_empty;
        }

    }

    void TimerHandler(string action)
    {
        //bool tOberhofnerova, tHacek, tJumpCooldown, tDMGCooldown, tNuggetDisappear;
        if (action == "Pause")
        {
            if (Oberhofnerova.Enabled)
                tOberhofnerova = true;
            if (Hacek.Enabled)
                tHacek = true;
            if (JumpCooldown.Enabled)
                tJumpCooldown = true;
            if (DMGcooldown.Enabled)
                tDMGCooldown = true;
            if (NuggetDisappear.Enabled)
                tNuggetDisappear = true;
            Oberhofnerova.Stop();
            Hacek.Stop();
            JumpCooldown.Stop();
            DMGcooldown.Stop();
            NuggetDisappear.Stop();

            if (basnicka.PlaybackState == PlaybackState.Playing)
            {
                basnickaPlaying = true;
                stopwatch.Stop();
                basnicka.Pause();
            }
        }
        if (action == "Play")
        {
            if (tOberhofnerova)
                Oberhofnerova.Start();
            if (tHacek)
                Hacek.Start();
            if (tJumpCooldown)
                JumpCooldown.Start();
            if (tDMGCooldown)
                DMGcooldown.Start();
            if (tNuggetDisappear)
                NuggetDisappear.Start();
            tOberhofnerova = false;
            tHacek = false;
            tJumpCooldown = false;
            tDMGCooldown = false;
            tNuggetDisappear = false;

            if (basnickaPlaying)
            {
                basnicka.Resume();
                stopwatch.Start();
            }
            basnickaPlaying = false;
        }
    }

    void SaveFileRead()
    {
        string filePath = Path.Combine(folderPath, fileName);

        if (!File.Exists(filePath))
        {
            continueGame = false;
            savedHealth = 0;
            savedLevel = 0;
            completedGame = false;
            hardestDifficulty = false;
            using (StreamWriter writer = File.CreateText(filePath))
            {
                writer.Write("false\n0\n0\nEasy\nfalse\nfalse");
            }
        }
        else
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                continueGame = Convert.ToBoolean(reader.ReadLine());
                savedHealth = Convert.ToInt32(reader.ReadLine());
                savedLevel = Convert.ToInt32(reader.ReadLine());
                difficulty = reader.ReadLine();
                completedGame = Convert.ToBoolean(reader.ReadLine());
                hardestDifficulty = Convert.ToBoolean(reader.ReadLine());
            }
        }
    }

    void SaveFileWrite()
    {
        // velmi jednoduchý saveFile, každá line jeden udaj
        // pokraèovat ve høe, hp, level, dohral hru, dohral na nejtezsi obtiznost.. 

        savedHealth = playerHealth;
        savedLevel = currentLevel;
        string saveFile = continueGame + "\n" + savedHealth + "\n" + savedLevel + "\n" + difficulty + "\n" + completedGame + "\n" + hardestDifficulty;
        string filePath = Path.Combine(folderPath, fileName);

        File.WriteAllTextAsync(filePath, saveFile);
    }

    void UpdateProgress()
    {
        SaveFileRead();
        if (continueGame)
        {
            levelCount = savedLevel;
            playerHealth = savedHealth;
            btContinue.Enabled = true;
        }
        if (completedGame)
        {
            pbCompletedGame.Visible = true;
            btInsane.Enabled = true;
            lbZaskolak.Visible = false;
        }
        else
            pbCompletedGame.Visible = false;

        if (hardestDifficulty)
            pbHardestDifficulty.Visible = true;
        else
            pbHardestDifficulty.Visible = false;
    }

    #endregion

    #region LevelDesign
    void LevelPrepare()
    {
        //doplni jedno HP
        if (playerHealth < 5 && difficulty == "Easy")
        {
            playerHealth++;
            HealthUI();
        }

        Reset();
        soundNextLevel.PlaySound();
        continueGame = true;
    }
    void TutorialLevel()
    {
        FullReset();
        continueGame = false;
        Terrain terrain1 = new(486, 563, 130, 34, "Terrain", Resources.Knizka, GameScene);
        Terrain terrain2 = new(730, 427, 130, 34, "Terrain", Resources.Knizka, GameScene);
        Terrain terrain3 = new(997, 317, 511, 34, "Terrain", Resources.Tuzka, GameScene);

        terrainArray = new Terrain[] { terrain1, terrain2, terrain3 };

        tutorialOberhofnerova = new(-100, -100, 100, 100, Color.Red, OberhofnerovaHP, true,
            15, 1408, OberhofnerovaMovementSpeed, "Oberhofnerova", 2000, GameScene);
        tutorialLemka = new(-200, -200, 110, 180, Color.Red, LemkaHP, false, 1000, 1400, LemkaMovementSpeed, "Lemka", 0, GameScene);
        tutorialSysalova = new(-200, -200, 100, 160, Color.Red, SysalovaHP, false, 0, 0, 0, "Sysalova", 0, GameScene);
        tutorialHacek = new(-200, -200, 110, 180, Color.Red, HacekHP, false, 0, 0, 0, "Hacek", 3000, GameScene);
        tutorialhidden = new(-100, -100, 20, 20, Color.Red, 20, false, 0, 0, 0, "secret", 0, GameScene);

        enemyArray = new Enemy[] { tutorialHacek, tutorialhidden, tutorialLemka, tutorialOberhofnerova, tutorialSysalova };

        Oberhofnerova.Interval = tutorialOberhofnerova.projectileCooldown;
        Hacek.Interval = tutorialHacek.projectileCooldown;

        lbLevel.Text = "Tutorial";
        lbLevel.ForeColor = Color.LightGreen;
    }
    void Level1()
    {
        LevelPrepare();
        Terrain terrain1 = new(28, 658, 164, 39, "Terrain Book", Resources.Knizka, GameScene);
        Terrain terrain2 = new(291, 753, 100, 72, "Terrain", Resources.PC, GameScene);
        Terrain terrain3 = new(341, 444, 110, 33, "Terrain Book", Resources.Knizka, GameScene);
        Terrain terrain4 = new(577, 260, 593, 34, "Terrain", Resources.Tuzka, GameScene);
        Terrain terrain5 = new(1402, 460, 97, 34, "Terrain Book", Resources.Knizka, GameScene);
        Terrain terrain6 = new(1263, 750, 100, 75, "Terrain", Resources.PC, GameScene);

        terrainArray = new Terrain[] { terrain1, terrain2, terrain3, terrain4, terrain5, terrain6 };


        Enemy enemy1 = new(1296, 12, 100, 100, Color.Red, OberhofnerovaHP, true,
            15, 1408, OberhofnerovaMovementSpeed, "Oberhofnerova", 2000, GameScene);
        Enemy enemy2 = new(1000, 645, 110, 180, Color.Red, LemkaHP, true, 400, 1150, LemkaMovementSpeed, "Lemka", 0, GameScene);

        enemyArray = new Enemy[] { enemy1, enemy2 };

        enemy1.pb.BackgroundImage = Resources.Milanka;
        enemy1.pb.BackColor = Color.Transparent;
        Oberhofnerova.Interval = enemy1.projectileCooldown;
        Oberhofnerova.Start();

        lbLevel.Text = "1 / 5";
        currentLevel = 1;
        SaveFileWrite();
    }
    void Level2()
    {
        LevelPrepare();
        Terrain terrain1 = new(28, 658, 164, 39, "Terrain Book", Resources.Knizka, GameScene);
        Terrain terrain2 = new(28, 360, 97, 34, "Terrain Book", Resources.Knizka, GameScene);
        Terrain terrain3 = new(457, 510, 593, 34, "Terrain", Resources.Tuzka, GameScene);
        Terrain terrain4 = new(370, 189, 110, 33, "Terrain Book", Resources.Knizka, GameScene);
        Terrain terrain5 = new(1088, 248, 100, 75, "Terrain", Resources.PC, GameScene);
        Terrain terrain6 = new(705, 750, 100, 75, "Terrain", Resources.PC, GameScene);
        Terrain terrain7 = new(1342, 659, 100, 167, "Terrain", Resources.Skrinka, GameScene);

        terrainArray = new Terrain[] { terrain1, terrain2, terrain3, terrain4, terrain5, terrain6, terrain7 };

        Enemy enemy1 = new(1296, 12, 100, 100, Color.Red, OberhofnerovaHP, true,
            15, 1408, OberhofnerovaMovementSpeed, "Oberhofnerova", 3000, GameScene);
        Enemy enemy2 = new(1342, 499, 100, 160, Color.Red, SysalovaHP, false, 0, 0, 0, "Sysalova", 0, GameScene);
        Enemy enemy3 = new(830, 330, 110, 180, Color.Red, LemkaHP, true, 460, 940, LemkaMovementSpeed, "Lemka", 0, GameScene);

        enemyArray = new Enemy[] { enemy1, enemy2, enemy3 };

        Absence absence1 = new(740, 720, Color.Purple, 2000, "Y", false, false, 720, 550, 8);

        absenceArray = new Absence[] { absence1 };

        Oberhofnerova.Interval = enemy1.projectileCooldown;
        Oberhofnerova.Start();

        absence1Index = 0;
        Absence1.Interval = 1;
        Absence1.Start();

        basnicka.Resume();
        stopwatch.Restart();

        lbLevel.Text = "2 / 5";
        currentLevel = 2;
        SaveFileWrite();
    }

    void Level3()
    {
        LevelPrepare();
        Terrain terrain1 = new(28, 658, 164, 39, "Terrain Book", Resources.Knizka, GameScene);
        Terrain terrain2 = new(270, 377, 100, 69, "Terrain", Resources.PC, GameScene);
        Terrain terrain3 = new(455, 161, 593, 34, "Terrain", Resources.Tuzka, GameScene);
        Terrain terrain4 = new(681, 531, 110, 33, "Terrain Book", Resources.Knizka, GameScene);
        Terrain terrain5 = new(1029, 719, 91, 40, "Terrain Book", Resources.Knizka, GameScene);
        Terrain terrain6 = new(1353, 640, 110, 75, "Terrain", Resources.PC, GameScene);

        terrainArray = new Terrain[] { terrain1, terrain2, terrain3, terrain4, terrain5, terrain6 };

        Enemy enemy1 = new(686, 372, 100, 160, Color.Red, SysalovaHP, false, 0, 0, 0, "Sysalova", 0, GameScene);
        Enemy enemy2 = new(1353, 460, 110, 180, Color.Red, HacekHP, false, 0, 0, 0, "Hacek", 3000, GameScene);

        enemyArray = new Enemy[] { enemy1, enemy2 };

        Absence absence1 = new(576, 121, Color.Purple, 1000, "Y", false, true, 121, 12, 8);
        Absence absence2 = new(878, 12, Color.Purple, 1000, "Y", true, true, 12, 121, 8);

        absenceArray = new Absence[] { absence1, absence2 };

        if (difficulty == "Easy" || difficulty == "Normal")
        {
            Nugget nugget1 = new(714, 73, 2, GameScene);
            nuggetList.Add(nugget1);
        }

        absence1Index = 0;
        Absence1.Interval = 1;
        Absence1.Start();

        absence2Index = 0;
        Absence2.Interval = 1;
        Absence2.Start();

        Hacek.Interval = enemy2.projectileCooldown;
        Hacek.Start();

        basnicka.Resume();
        stopwatch.Restart();

        lbLevel.Text = "3 / 5";
        currentLevel = 3;
        SaveFileWrite();
    }
    void Level4()
    {
        LevelPrepare();
        Terrain terrain1 = new(28, 658, 164, 39, "Terrain Book", Resources.Knizka, GameScene);
        Terrain terrain2 = new(126, 280, 110, 35, "Terrain Book", Resources.Knizka, GameScene);
        Terrain terrain3 = new(449, 524, 100, 49, "Terrain Book", Resources.Knizka, GameScene);
        Terrain terrain4 = new(449, 753, 91, 72, "Terrain", Resources.PC, GameScene);
        Terrain terrain5 = new(840, 480, 593, 40, "Terrain", Resources.Tuzka_reversed, GameScene);
        Terrain terrain6 = new(840, 155, 593, 40, "Terrain", Resources.Tuzka_reversed, GameScene);
        Terrain terrain7 = new(1323, 418, 110, 63, "Terrain", Resources.PC, GameScene);

        terrainArray = new Terrain[] { terrain1, terrain2, terrain3, terrain4, terrain5, terrain6, terrain7 };

        Enemy enemy1 = new(132, 120, 100, 160, Color.Red, SysalovaHP, false, 0, 0, 0, "Sysalova", 0, GameScene);
        Enemy enemy2 = new(1323, 239, 110, 180, Color.Red, HacekHP, false, 0, 0, 0, "Hacek", 3000, GameScene);
        Enemy enemy3 = new(1296, 12, 100, 100, Color.Red, OberhofnerovaHP, true,
            15, 1408, OberhofnerovaMovementSpeed, "Oberhofnerova", 2000, GameScene);
        Enemy enemy4 = new(1066, 645, 110, 180, Color.Red, LemkaHP, true, 548, 1410, LemkaMovementSpeed, "Lemka", 0, GameScene);

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

        basnicka.Resume();
        stopwatch.Restart();

        lbLevel.Text = "4 / 5";
        currentLevel = 4;
        SaveFileWrite();
    }
    void Level5()
    {
        LevelPrepare();
        Player.Left = 138;
        Player.Top = 477;

        spring = new(734, 609, 60, 50, "Terrain Spring", Resources.Pruzina, GameScene);
        bookLeft = new(122, 598, 112, 35, "Terrain Book", Resources.Knizka, GameScene);
        Terrain terrain3 = new(402, 381, 112, 35, "Terrain Book", Resources.Knizka, GameScene);
        Terrain terrain4 = new(977, 381, 112, 35, "Terrain Book", Resources.Knizka, GameScene);
        bookRight = new(1282, 598, 112, 35, "Terrain Book", Resources.Knizka, GameScene);
        Terrain terrain6 = new(0, 795, 600, 30, "Terrain", Resources.Tuzka, GameScene);
        Terrain terrain7 = new(920, 795, 600, 30, "Terrain", Resources.Tuzka_reversed, GameScene);
        Terrain terrain8 = new(714, 658, 100, 167, "Terrain", Resources.Skrinka, GameScene);

        terrainArray = new Terrain[] { spring, bookLeft, terrain3, terrain4, bookRight, terrain6, terrain7, terrain8 };

        stark = new(1185, 175, 335, 650, Color.Red, StarkHP, true, 0, 1185, StarkMovementSpeed, "Stark", 5000, GameScene);

        enemyArray = new Enemy[] { stark };

        bossPhase = 1;
        starkIdle = false;
        StarkHealth.Visible = true;
        StarkHealthBackground.Visible = true;

        lbLevel.Text = "5 / 5";
        lbLevel.ForeColor = Color.Red;
        currentLevel = 5;
        SaveFileWrite();
    }

    #endregion

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
                    panelPauza.Visible = true;
                    lbNazev.Text = "Pauza";
                    lbNazev.Left = 650;
                    UpdateMethod.Stop();
                    TimerHandler("Pause");
                    Focus();
                }
                else if (lbNazev.Text == "Pauza")
                {
                    Pause.Visible = false;
                    Pause.Enabled = false;
                    GameScene.Enabled = true;
                    GameScene.Visible = true;
                    UpdateMethod.Start();
                    TimerHandler("Play");
                    Focus();
                }
            }
            if (e.KeyCode == Keys.K && !tutorial)
            {
                foreach (Enemy enemy in enemyArray)
                {
                    if (enemy != stark)
                    {
                        enemy.health = 0;
                        enemy.CheckHealth(enemy, GameScene);
                    }
                }
            }
            if (e.KeyCode == Keys.L)
            {
                if (cheatHealth)
                    cheatHealth = false;
                else
                    cheatHealth = true;
            }
            if (e.KeyCode == Keys.P)
            {
                if (stark != null)
                    stark.health = 40;
                changedPhase = false;
            }
            if (e.KeyCode == Keys.O)
            {
                if (stark != null)
                    stark.health = 20;
                changedPhase = false;
            }
            if (e.KeyCode == Keys.I)
            {
                if (stark != null)
                {
                    stark.health -= 2;
                    stark.CheckHealth(stark, GameScene);
                }
            }
        }
        else if (e.KeyCode == Keys.R)
            FullReset();
    }

    private void MainWindow_KeyUp(object sender, KeyEventArgs e)
    {
        //Input - uvolneni klavesy
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

    private void btPlay_Click(object sender, EventArgs e)
    {
        difficultySelect.Visible = !difficultySelect.Visible;
        soundSelect.PlaySound();
    }

    private void btOptions_Click(object sender, EventArgs e)
    {
        Menu.Visible = false;
        Menu.Enabled = false;
        Pause.Enabled = true;
        Pause.Visible = true;
        panelPauza.Visible = false;
        lbNazev.Text = "Ovládání";
        lbNazev.Left = 600;
        Focus();
        soundSelect.PlaySound();
    }
    private void btMenu_Click(object sender, EventArgs e)
    {
        Pause.Enabled = false;
        Pause.Visible = false;
        Menu.Visible = true;
        Menu.Enabled = true;
        Focus();
        soundSelect.PlaySound();

        if (tutorial)
        {
            cheatHealth = false;
            tutorial = false;
            UpdateMethod.Stop();
            InstrukceTimer.Stop();
            tutBanJump = false;
            tutBanLMB = false;
            tutBanDash = false;
            tutBanQ = false;
            tutBanMovement = false;
            tutorialPhase = 0;
            lbTutorial.Text = string.Empty;
        }

    }
    private void btExit_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void btDifficulty(object sender, EventArgs e)
    {
        soundSelect.PlaySound();
        Button diff = sender as Button;
        if (diff.Name == "btEasy")
            difficulty = "Easy";
        if (diff.Name == "btNormal")
            difficulty = "Normal";
        if (diff.Name == "btHard")
            difficulty = "Hard";
        if (diff.Name == "btInsane")
        {
            difficulty = "Insane";
            playerHealth = 1;
        }
        else
            playerHealth = 5;

        FullReset();
        btContinue.Enabled = true;
        continueGame = true;
        cheatHealth = false;
        won = false;
        SaveFileWrite();
        difficultySelect.Visible = false;
        Menu.Visible = false;
        Menu.Enabled = false;
        GameScene.Enabled = true;
        GameScene.Visible = true;
        Focus();
        UpdateMethod.Start();
    }
    private void btContinue_Click(object sender, EventArgs e)
    {
        Menu.Visible = false;
        Menu.Enabled = false;
        GameScene.Enabled = true;
        GameScene.Visible = true;
        Focus();
        UpdateMethod.Start();
        TimerHandler("Play");
        soundSelect.PlaySound();
        HealthUI();
    }
    private void JumpCooldown_Tick(object sender, EventArgs e)
    {
        jumpCooldown = false;
    }

    private void Sound_Click(object sender, EventArgs e)
    {
        SoundManager.bannedSound = !SoundManager.bannedSound;
        if (SoundManager.bannedSound)
            Sound.BackgroundImage = Resources.sound_muted;
        else
            Sound.BackgroundImage = Resources.sound;
    }

    private void MainWindow_Load(object sender, EventArgs e)
    {
        UpdateProgress();
        basnicka.Play();
        basnicka.Pause();
        stopwatch.Reset();
    }

    private void btResetProgress_Click(object sender, EventArgs e)
    {
        string filePath = Path.Combine(folderPath, fileName);
        using (StreamWriter writer = File.CreateText(filePath))
        {
            writer.Write("false\n0\n0\nfalse\nfalse");
        }
        UpdateProgress();
    }
    private void btTutorial_Click(object sender, EventArgs e)
    {
        Menu.Visible = false;
        Menu.Enabled = false;
        GameScene.Visible = true;
        GameScene.Enabled = true;
        TutorialLevel();
        UpdateMethod.Start();
        Focus();
        tutBanDash = true;
        tutBanJump = true;
        tutBanLMB = true;
        tutBanQ = true;
        tutBanMovement = true;
        cheatHealth = true;
        writeInstructions = true;
        tutorialPhase = 0;
        tutorial = true;
        btContinue.Enabled = false;
        InstrukceTimer.Start();
        soundSelect.PlaySound();
    }
}