using NAudio.Wave;
using Petr_RP_CestaKMaturite.Properties;
using SharpDX.DirectInput;
using XInputDotNetPure;
using ButtonState = XInputDotNetPure.ButtonState;
using Timer = System.Windows.Forms.Timer;

namespace Petr_RP_CestaKMaturite;
public partial class MainWindow : Form {
    public MainWindow() {
        InitializeComponent();

        Maximize();

        // Loading Screen
        loadingScreen = new PictureBox() {
            Left = 0,
            Top = 0,
            Width = Screen.PrimaryScreen.Bounds.Width,
            Height = Screen.PrimaryScreen.Bounds.Height,
            Image = Resources.Main,
            SizeMode = PictureBoxSizeMode.Zoom
        };
        this.Controls.Add(loadingScreen);
        loadingScreen.BringToFront();

        loadingScreenTimer = new Timer();
        loadingScreenTimer.Interval = 3500;
        loadingScreenTimer.Tick += LoadingScreenTimer_Tick;
        loadingScreenTimer.Start();
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
    readonly SoundManager soundLand = new("land");
    readonly SoundManager soundSysalova = new("Sysalova");
    #endregion

    //Glob�ln� prom�nn�
    bool A, D, Space, Q, E, LMB, cX, cA, cLeft, cRight, cUp, cTrigger, cOptions, cB; //hr��ovo inputy
    bool xboxA = false, xboxX = false, xboxB = false, xboxLeft = false, xboxRight = false, xboxUp = false, xboxTrigger = false, xboxOptions = false; //Xbox
    bool psA = false, psX = false, psB = false, psLeft = false, psRight = false, psUp = false, psTrigger = false, psOptions = false, psController; //PlayStation
    bool moveLeft, moveRight, onTop, onGround, lastInputLeft, facingRight, dashLeft, dashRight, banInput, canDash = true, landed, touchedGround, jumpCooldown, fixQ, landSound, specialImage, winAnimation; //pohyb
    int jumpSpeed, dashX, dashIndex, hupIndex, playerCenterX, playerCenterY, idleTime; //pohyb
    bool attackQphase1, attackQphase2, attackQcooldown, QOnLeft, attackLMBcooldown = false, alreadyHit, hitQ, underTerrain, soundFixQ; //utok
    int abilityQIndex, rulerLength = 100, abilityLMBIndex; //utok
    int levelCount = 1, playerHealth, dmgIndex, enMiddle, currentLevel, animationTick = 0;
    bool canGetHit = true, disableAllInputs = false, unHitable = false, knockback, cheatHealth, nuggetSpawn = false, soundDeathOnce, won, maximized, reseting, soundShouldBeBanned; string difficulty; //managment
    bool lemkaCooldown, lemkaRight; int lemkaIndex; //enemy
    int OberhofnerovaHP = 6, LemkaHP = 12, HacekHP = 10, SysalovaHP = 12, StarkHP = 60, OberhofnerovaMovementSpeed = 10, LemkaMovementSpeed = 6, StarkMovementSpeed = 3; //enemy
    int bossPhase = 0, baseballSlam = 0, starkIndex; bool starkQ = false, baseballGetDMG = false, baseballCooldown = false, changedPhase = false, starkIdle, playerSideLeft, bookLeftDestroyed, bookRightDestroyed, starkSmackFix; //bossfight
    bool tOberhofnerova, tHacek, tJumpCooldown, tDMGCooldown, tNuggetDisappear, tStark, basnickaPlaying; //fixy timer�
    bool continueGame, completedGame, hardestDifficulty, versionForRelease = true, enLanguage;
    int savedHealth, savedLevel;

    bool tutorial, writeInstructions, typing, tutBanJump, tutBanDash, tutBanQ, tutBanLMB, tutBanMovement;
    int tutorialPhase = 0;
    string[] poleInstrukci = new string[] { "V�tej na �kole SP� Ostrov!", "Pohybuj se pomoc� kl�ves A a D", "Skv�le! Zkus si vysko�it pomoc� mezern�ku.", "Dobr� pr�ce, ale bacha, mnoha v�c� ti bude br�nit v jednoduch� maturit�.",
    "A to u� tak jednoduch� nebude...", "Zkus si bouchnout prav�tkem pomoc� lev�ho tla��tka my�i.","Kdy� m��� kurzorem nad hr��e, bouch� nahoru.", "Pro �tok doleva �i doprava, p�esu� kurzor do pat�i�n� strany.",
    "Nyn� ti p�edstav�m u�itele. Za�neme s panem u�itelem Lemkou.", "Jakmile jsi nad jeho �rovn�, za�ne t� sledovat.", "TIP: D�vej bacha na jeho kuli�kovou my�!", "M� �t�st�, �e zat�m nem��e� ztr�cet �ivoty.", "Kdybys cht�l n�jak� rychl� �t�k, zkus zm��kout E.",
    "Pokra�ujme nyn� s pan� u�itelkou Sysalovou.", "Bohu�el n�m�ina nen� tv�j obl�ben� jazyk.", "A proto se j� pokus zastavit co nejd��ve :)", "Kdy� jsi ve vzduchu nad u�itelem, m��e� na n�j nam��it my�� a zm��knout Q.", "Tento v�pad d�v� dvojn�sobn� po�kozen�!",
    "M��e� si to vyzkou�et na panu H��kovi.", "Jenom d�vej bacha na nav�d�n� twitter!", "TIP: Prav�tkem se twitteru zbav�", "A jako posledn� je tu pan� u�itelka Oberhofnerova", "Jako fl�ka� u n� rozhodn� neprojde�!", "TIP: Zkus se nenechat trefit <3" , "To bude ode m� v�echno, hodn� �t�st�!"};

    string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    string fileName = "CestaKMaturite_SaveFile.txt";

    private GamePadState previousXboxState; // Xbox

    // PlayStation
    Joystick joystick;
    bool[]? previousButtons;

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

    PictureBox loadingScreen;
    Timer loadingScreenTimer;

    Timer winScreenTimer;
    PictureBox winScreen;
    int winIndex;

    private void UpdateMethod_Tick(object sender, EventArgs e) {
        //reset prom�nn�ch
        moveLeft = true;
        moveRight = true;
        onTop = false;
        underTerrain = false;

        //st�ed hr��e
        playerCenterX = Player.Left + Player.Width / 2;
        playerCenterY = Player.Top + Player.Height / 2;

        //tick prom�nn� pro animace
        animationTick++;
        if (animationTick == 1001)
            animationTick = 0;

        //kurzor
        Point cursor;
        if (maximized) {
            Point oldCursor = this.PointToClient(Cursor.Position);
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            cursor = new Point(oldCursor.X - ((screenWidth - 1536) / 2), oldCursor.Y - ((screenHeight - 864) / 2));
        } else {
            cursor = this.PointToClient(Cursor.Position);
        }


        if (((Control.MouseButtons & MouseButtons.Left) != 0) && !disableAllInputs)
            LMB = true;
        else
            LMB = false;

        #region Nuggetka

        //�ance 1/10000 kazdou milisekundu (1/100s) ze se nekde nahodne spawne nuggetka
        if (Random.Shared.Next(0, 10001) == 10000 && !nuggetSpawn && (difficulty == "Easy" || difficulty == "Normal")) {
            nuggetSpawn = true;
            bool intersects = true;
            int positionX = 0, positionY = 0;
            while (intersects) {
                intersects = false;
                positionX = Random.Shared.Next(50, 1404);
                positionY = Random.Shared.Next(50, 706);
                Rectangle spawnNugget = new(positionX, positionY, 65, 70);
                foreach (PictureBox anything in GameScene.Controls.OfType<PictureBox>()) {
                    if (anything.Bounds.IntersectsWith(spawnNugget))
                        intersects = true;
                }
            }
            Nugget nugget = new(positionX, positionY, 1, Resources.nugeta_normal, GameScene);
            nuggetList.Add(nugget);
            nuggetPB = nugget.pb as PictureBox;
            NuggetDisappear.Interval = Random.Shared.Next(3000, 5000);
            NuggetDisappear.Start();
        }

        if (nuggetList != null) {
            foreach (Nugget nugget in nuggetList) {
                if (nugget.pb.Bounds.IntersectsWith(Player.Bounds) && playerHealth < 5) {
                    if (playerHealth + nugget.healthAdd > 5)
                        playerHealth = 5;
                    else
                        playerHealth += nugget.healthAdd;

                    HealthUI();
                    nuggetList.Remove(nugget);
                    nugget.Dispose();
                    soundNugget.PlaySound();
                    return;
                }
            }
        }

        #endregion

        //Hitboxy
        HitboxLeft = new Rectangle(Player.Left - 3, Player.Top, 3, Player.Height - 10);
        HitboxRight = new Rectangle(Player.Right, Player.Top, 3, Player.Height - 10);
        HitboxUp = new Rectangle(Player.Left + 10, Player.Top - 4, Player.Width - 10, 2);
        HitboxDown = new Rectangle(Player.Left + 2, Player.Bottom - 2, Player.Width - 4, 2);

        //Hitboxy, kter� jsou d�l kv�li dashi
        HitboxDashLeft = new Rectangle(Player.Left - 15, Player.Top, Player.Width, Player.Height - 2);
        HitboxDashRight = new Rectangle(Player.Left + 15, Player.Top, Player.Width, Player.Height - 2);

        //Ter�n
        foreach (PictureBox terrain in GameScene.Controls.OfType<PictureBox>().Where(x => x.Tag != null)) {
            if (terrain.Tag.ToString().Contains("Terrain")) {
                if (terrain.Bounds.IntersectsWith(HitboxUp)) {
                    Player.Top = terrain.Bottom + 3;
                    jumpSpeed = -2;
                    underTerrain = true;
                }
                if (terrain.Bounds.IntersectsWith(HitboxLeft)) {
                    moveLeft = false;
                    if (GameScene.Height - Player.Bottom <= 0 || onTop == true)
                        Player.Left = terrain.Right;
                }
                if (terrain.Bounds.IntersectsWith(HitboxRight)) {
                    moveRight = false;
                    if (GameScene.Height - Player.Bottom <= 0 || onTop == true)
                        Player.Left = terrain.Left - Player.Width;
                }

                if (terrain.Bounds.IntersectsWith(HitboxDown) && Player.Bottom - 20 <= terrain.Top && jumpSpeed < 0) {
                    Player.Top = terrain.Top - Player.Height + 1;
                    onTop = true;
                    jumpSpeed = 0;

                    //setup na pohoupnut� kn�ek
                    if (terrain.Tag.ToString().Contains("Book")) {
                        if (!touchedGround)
                            landed = true;
                        touchedGround = true;

                        landedBlock = terrain as PictureBox;
                    }
                }
            }
        }

        //Pohoupnut� kn�ek
        if (landedBlock != null && landed) {
            switch (hupIndex) {
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
        if (!(A && D) && !banInput) {
            int movementSpeed = 8;
            if ((D || cRight) && moveRight && !(Player.Right >= GameScene.Width) && !tutBanMovement) {
                Player.Left += movementSpeed;
                lastInputLeft = false;

                if (tutorialPhase == 2 && !typing)
                    writeInstructions = true;

                idleTime = 0;
            }
            if ((A || cLeft) && moveLeft && !(Player.Left <= 0) && !tutBanMovement) {
                Player.Left -= movementSpeed;
                lastInputLeft = true;

                if (tutorialPhase == 2 && !typing)
                    writeInstructions = true;

                idleTime = 0;
            }
        }

        //Lev� a prav� strana sc�ny
        if (Player.Right >= GameScene.Width)
            Player.Left = GameScene.Width - Player.Width;
        if (Player.Left <= 0)
            Player.Left = 0;

        //Sm�r kouk�n�
        if (lastInputLeft)
            facingRight = false;
        else
            facingRight = true;

        //Skakani
        if (GameScene.Height - Player.Bottom <= 0 || onTop) {
            onGround = true;
            if (!landSound) {
                soundLand.PlaySound();
                landSound = true;
            }
        } else {
            onGround = false;
            landSound = false;
        }

        if ((Space || cA) && onGround && !jumpCooldown && !tutBanJump) {
            jumpSpeed = 24;
            jumpCooldown = true;
            JumpCooldown.Start();
            soundJump.PlaySound();

            if (!winAnimation) {
                if (facingRight)
                    Player.Image = Resources.Player_Jump_Right1;
                else
                    Player.Image = Resources.Player_Jump_Left1;
            }

            if (tutorialPhase == 3 && !typing)
                writeInstructions = true;
        }

        //Vrsek sceny
        if (Player.Top <= 0 && !attackQphase2 && jumpSpeed > -2)
            jumpSpeed = -2;

        //Gravitace
        Player.Top -= jumpSpeed;

        if (jumpSpeed > -20)
            jumpSpeed -= 1;

        if (jumpSpeed < -3 && !specialImage && !winAnimation) {
            if (facingRight)
                Player.Image = Resources.Player_Jump_Right2;
            else
                Player.Image = Resources.Player_Jump_Left2;
        }

        //Spodek sceny
        if (Player.Bottom >= GameScene.Height)
            jumpSpeed = 0;

        //Nezaboreni do zeme
        if (GameScene.Height - Player.Bottom < 0)
            Player.Top = GameScene.Height - Player.Height;

        //Pru�ina
        foreach (PictureBox terrain in GameScene.Controls.OfType<PictureBox>().Where(x => x.Tag != null)) {
            if (terrain.Tag.ToString().Contains("Spring") && Player.Bounds.IntersectsWith(terrain.Bounds)) {
                jumpSpeed = 31;
                soundSpring.PlaySound();
            }
        }

        //Dash
        if ((E || cTrigger) && !dashRight && !dashLeft && canDash && !tutBanDash) {
            if (facingRight)
                dashRight = true;
            else
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

        foreach (PictureBox terrain in GameScene.Controls.OfType<PictureBox>().Where(x => x.Tag != null)) {
            if (terrain.Tag.ToString().Contains("Terrain")) {
                if (terrain.Bounds.IntersectsWith(HitboxDashLeft))
                    dashLeft = false;
                if (terrain.Bounds.IntersectsWith(HitboxDashRight))
                    dashRight = false;
            }
        }

        int dashDistance = 300;
        int dashSpeed = 15;
        if (dashLeft && dashX - Player.Left < dashDistance) {
            Player.Left -= dashSpeed;
            jumpSpeed = 0;

            if (!winAnimation)
                Player.Image = Resources.Player_Dash_Left;
        }
        if (dashRight && Player.Left - dashX < dashDistance) {
            Player.Left += dashSpeed;
            jumpSpeed = 0;

            if (!winAnimation)
                Player.Image = Resources.Player_Dash_Right;
        }

        //ENEMY
        //Po�et a Pole Enem�k�
        int iEnemy = 0;
        foreach (PictureBox enemy in GameScene.Controls.OfType<PictureBox>().Where(x => x.Tag == "Enemy"))
            iEnemy++;

        if (iEnemy == 0) {
            //��dn� enemy = ukon�it level
            switch (levelCount) {
                case 1: Level1(); break;
                case 2: Level2(); break;
                case 3: Level3(); break;
                case 4: Level4(); break;
                case 5: Level5(); break;
                case 6:
                    //VICTORY
                    if (!won) {
                        won = true;
                        soundWin.PlaySound();
                        continueGame = false;
                        if (!cheatHealth) {
                            completedGame = true;
                            if (difficulty == "Insane")
                                hardestDifficulty = true;
                        } else {
                            MessageBox.Show("�koda �e m� cheaty, ale jinak douf�m �e se ti aspo� hra l�bila :)");
                        }
                        btContinue.Enabled = false;
                        SaveFileWrite();
                        UpdateProgress();

                        winAnimation = true;
                        disableAllInputs = true;

                        winIndex = 0;
                        winScreenTimer = new Timer();
                        winScreenTimer.Interval = 3000;
                        winScreenTimer.Tick += WinScreenTimer_Tick;
                        winScreenTimer.Start();
                    }
                    break;
            }
            levelCount++;
        } else {
            //kdy� existuj� enemy (m��e b�t kombat)

            #region AttackQ
            //porovn�n� vzd�lenost� a hled�n� nejbli���ho Enemy na kurzor (enemyObjectArray[closestIndex])
            //pomoc� tag�

            //PC x Controller
            int X, Y;
            if (Q) {
                X = cursor.X;
                Y = cursor.Y;
            } else {
                X = playerCenterX;
                Y = playerCenterY;
            }

            int[] enemyDistanceArray = new int[iEnemy];
            PictureBox[] enemyObjectArray = new PictureBox[iEnemy];
            iEnemy = 0;
            int? sumDistance;
            foreach (PictureBox enemy in GameScene.Controls.OfType<PictureBox>().Where(x => x.Tag == "Enemy")) {
                sumDistance = Math.Abs(X - enemy.Left) + Math.Abs(Y - enemy.Top);
                enemyDistanceArray[iEnemy] = Convert.ToInt32(sumDistance);
                enemyObjectArray[iEnemy] = enemy;

                iEnemy++;
            }
            int enemyObjectClosest = enemyDistanceArray[0];
            int closestIndex = 0;
            iEnemy = 0;
            foreach (int item in enemyDistanceArray) {
                if (item < enemyObjectClosest) {
                    enemyObjectClosest = item;
                    closestIndex = iEnemy;
                }

                iEnemy++;
            }

            //Ability Q na target my�i
            if ((Q || cB) && !onGround && !attackQcooldown && enemyObjectArray[closestIndex].Top - Player.Bottom > 40 && !tutBanQ) {
                foreach (Enemy enemy in enemyArray) {
                    if (enemy == stark && starkQ || enemy != stark) {
                        if (enemyObjectArray[closestIndex].Top - Player.Top < 300 &&
                            enemyObjectArray[closestIndex].Left - Player.Left < 250 &&
                            Player.Right - enemyObjectArray[closestIndex].Right < 250) {
                            closestEnemy = enemyObjectArray[closestIndex] as PictureBox;

                            if (Player.Left < closestEnemy.Left) {
                                QOnLeft = true;
                                lastInputLeft = false;
                                facingRight = true;
                            } else {
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

            if (attackQphase1) {
                if (soundFixQ) {
                    soundQ.PlaySound();
                    soundFixQ = false;
                }

                if (QOnLeft && Player.Right - (closestEnemy.Right / 2) < 10)
                    Player.Left += 15;

                if (!QOnLeft && Player.Left - (closestEnemy.Right - closestEnemy.Width / 2) > 0)
                    Player.Left -= 15;

                if (Player.Bottom < closestEnemy.Top - 25)
                    Player.Top += 15;

                jumpSpeed = 0;
                banInput = true;

                //animace
                if (!winAnimation) {
                    if (!QOnLeft)
                        Player.Image = Resources.Player_Q_Left1;
                    else
                        Player.Image = Resources.Player_Q_Right1;
                }

                specialImage = true;

                //p�ed�asn� ukon�en�
                if ((Math.Abs(playerCenterX) - (closestEnemy.Left + closestEnemy.Width / 2)) < 40 && closestEnemy.Top - Player.Bottom < 40)
                    AbilityQ.Interval = 1;
            }
            if (attackQphase2 && !fixQ) {
                if (QOnLeft)
                    Player.Left += 15;
                else
                    Player.Left -= 15;

                if (!underTerrain)
                    Player.Top -= 15;

                jumpSpeed = 0;
                banInput = true;

                //-HP
                foreach (Enemy enemy in enemyArray) {
                    if (enemy.pb == closestEnemy && !hitQ) {
                        hitQ = true;
                        enemy.TakeDamage(4, GameScene);
                        if (enemy.dead)
                            fixQ = true;

                        soundhitSomeone.PlaySound();
                    }
                }

                //animace
                if (!winAnimation) {
                    if (!QOnLeft)
                        Player.Image = Resources.Player_Q_Left2;
                    else
                        Player.Image = Resources.Player_Q_Right2;
                }
            }

            #endregion

            #region Attack
            //�tok LMB
            bool attack = false;
            if (((LMB && cursor.Y < Player.Top) || cUp && cX) && !attackLMBcooldown && !tutBanLMB) {
                //utok nahoru
                HitboxAttackTop = new PictureBox {
                    Left = Player.Left + 15,
                    Top = Player.Top - rulerLength,
                    Width = 45,
                    Height = rulerLength,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Image = Resources.Player_Ruler_Up
                };
                GameScene.Controls.Add(HitboxAttackTop);
                HitboxAttackTop.BringToFront();

                if (!winAnimation)
                    Player.Image = Resources.Player_Attack_Up;

                attack = true;
            } else {
                if (((LMB && cursor.X > playerCenterX) || cX && facingRight) && !attackLMBcooldown && !tutBanLMB) {
                    //utok doprava
                    HitboxAttackRight = new PictureBox {
                        Left = Player.Right,
                        Top = Player.Top + 35,
                        Width = rulerLength,
                        Height = 45,
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Image = Resources.Player_Ruler_Right
                    };
                    GameScene.Controls.Add(HitboxAttackRight);
                    HitboxAttackRight.BringToFront();

                    if (!winAnimation)
                        Player.Image = Resources.Player_Attack_Right;

                    attack = true;
                }
                if (((LMB && cursor.X < playerCenterX) || cX && !facingRight) && !attackLMBcooldown && !tutBanLMB) {
                    //utok doleva
                    HitboxAttackLeft = new PictureBox {
                        Left = Player.Left - rulerLength,
                        Top = Player.Top + 35,
                        Width = rulerLength,
                        Height = 45,
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Image = Resources.Player_Ruler_Left
                    };
                    GameScene.Controls.Add(HitboxAttackLeft);
                    HitboxAttackLeft.BringToFront();

                    if (!winAnimation)
                        Player.Image = Resources.Player_Attack_Left;

                    attack = true;
                }
            }

            if (attack) {
                attackLMBcooldown = true;
                alreadyHit = false;
                abilityLMB.Interval = 100;
                abilityLMBIndex = 0;
                abilityLMB.Start();
                soundRuler.PlaySound();

                if (tutorialPhase == 6 && !typing)
                    writeInstructions = true;

                specialImage = true;
            }

            #endregion

            foreach (Enemy enemy in enemyArray) {

                #region EnemyAnimace 

                //Sysalova
                if (enemy.type == Enemy.enemyType.Sysalova) {

                    //Sysalova animace
                    if (Player.Left < enemy.pb.Left)
                        enemy.facingRight = false;
                    else
                        enemy.facingRight = true;

                    if (!enemy.hitImage) {
                        if (enemy.facingRight) {
                            if (animationTick % 10 == 0)
                                enemy.pb.Image = Resources.Sysalova_living_right;
                            if (animationTick % 20 == 0)
                                enemy.pb.Image = Resources.Sysalova_singing_right;
                        } else {
                            if (animationTick % 10 == 0)
                                enemy.pb.Image = Resources.Sysalova_living_left;
                            if (animationTick % 20 == 0)
                                enemy.pb.Image = Resources.Sysalova_singing_left;
                        }
                    }
                }

                // Lemka
                if (enemy.type == Enemy.enemyType.Lemka && !enemy.differentAnimation) {

                    //Lemka animace
                    if (enemy.moveSwitch) {
                        // normal
                        if (enemy.facingRight) {
                            if (animationTick % 20 == 0)
                                enemy.pb.Image = Resources.Lemka_Walk_Right_Normal1;
                            if (animationTick % 40 == 0)
                                enemy.pb.Image = Resources.Lemka_Walk_Right_Normal2;
                            if (animationTick % 60 == 0)
                                enemy.pb.Image = Resources.Lemka_Walk_Right_Normal3;

                        } else {
                            if (animationTick % 20 == 0)
                                enemy.pb.Image = Resources.Lemka_Walk_Left_Normal1;
                            if (animationTick % 40 == 0)
                                enemy.pb.Image = Resources.Lemka_Walk_Left_Normal2;
                            if (animationTick % 60 == 0)
                                enemy.pb.Image = Resources.Lemka_Walk_Left_Normal3;
                        }

                    } else {
                        // angry
                        if (enemy.facingRight) {
                            if (animationTick % 20 == 0)
                                enemy.pb.Image = Resources.Lemka_Walk_Right_Angry1;
                            if (animationTick % 40 == 0)
                                enemy.pb.Image = Resources.Lemka_Walk_Right_Angry2;
                            if (animationTick % 60 == 0)
                                enemy.pb.Image = Resources.Lemka_Walk_Right_Angry3;
                        } else {
                            if (animationTick % 20 == 0)
                                enemy.pb.Image = Resources.Lemka_Walk_Left_Angry1;
                            if (animationTick % 40 == 0)
                                enemy.pb.Image = Resources.Lemka_Walk_Left_Angry2;
                            if (animationTick % 60 == 0)
                                enemy.pb.Image = Resources.Lemka_Walk_Left_Angry3;
                        }
                    }
                }

                // Oberhofnerova animace
                if (enemy.type == Enemy.enemyType.Oberhofnerova) {
                    if (enemy.shootAnimIndex == 0)
                        enemy.pb.Image = Resources.Oberhofnerova_Idle;
                    else if (enemy.shootAnimIndex == 1)
                        enemy.pb.Image = Resources.Oberhofnerova_Charge;
                    else if (enemy.shootAnimIndex == 2)
                        enemy.pb.Image = Resources.Oberhofnerova_Shoot;
                }

                // Hacek animace
                if (enemy.type == Enemy.enemyType.Hacek) {

                    if (Player.Left < enemy.pb.Left)
                        enemy.facingRight = false;
                    else
                        enemy.facingRight = true;

                    if (enemy.facingRight) {
                        if (enemy.shootAnimIndex == 0)
                            enemy.pb.Image = Resources.Hacek_Idle_Right;
                        else if (enemy.shootAnimIndex == 1)
                            enemy.pb.Image = Resources.Hacek_Charge_Right;
                        else if (enemy.shootAnimIndex == 2)
                            enemy.pb.Image = Resources.Hacek_Shoot_Right;
                    } else {
                        if (enemy.shootAnimIndex == 0)
                            enemy.pb.Image = Resources.Hacek_Idle_Left;
                        else if (enemy.shootAnimIndex == 1)
                            enemy.pb.Image = Resources.Hacek_Charge_Left;
                        else if (enemy.shootAnimIndex == 2)
                            enemy.pb.Image = Resources.Hacek_Shoot_Left;
                    }
                }

                // Stark phase 2 animace
                if (enemy.type == Enemy.enemyType.Stark && bossPhase == 2) {
                    if (enemy.shootAnimIndex == 0)
                        enemy.pb.Image = Resources.Stark_Walk;
                    else if (enemy.shootAnimIndex == 1)
                        enemy.pb.Image = Resources.Stark_Chalk_Charge;
                    else if (enemy.shootAnimIndex == 2)
                        enemy.pb.Image = Resources.Stark_Chalk_Shoot;
                }

                #endregion


                #region Lemka
                if (enemy.type == Enemy.enemyType.Lemka && !enemy.disposed) {
                    //Lemka movement
                    if (enemy.pb.Bottom > Player.Bottom - 5) {
                        enemy.moveSwitch = false;
                        if (Math.Abs(enemy.pb.Left - Player.Left) < 20) {
                            enemy.moveLeft = false;
                            enemy.moveRight = false;
                        } else if (enemy.pb.Left < Player.Left) {
                            enemy.moveRight = true;
                            enemy.moveLeft = false;
                        } else {
                            enemy.moveLeft = true;
                            enemy.moveRight = false;
                        }
                    } else
                        enemy.moveSwitch = true;

                    //Lemka utok
                    if (((enemy.pb.Left - Player.Left - Player.Width) < 100 && enemy.pb.Left > Player.Right) ||
                        (Player.Left - (enemy.pb.Right) < 100 && (enemy.pb.Right) < Player.Left)) {
                        if (Player.Bottom > enemy.pb.Top && Player.Top < enemy.pb.Bottom && !lemkaCooldown) {
                            if ((enemy.pb.Left - Player.Left - Player.Width) < 100 && enemy.pb.Left > Player.Right)
                                lemkaRight = false;
                            if (Player.Left - enemy.pb.Right < 100 && enemy.pb.Right < Player.Left)
                                lemkaRight = true;
                            enemy.moving = false;
                            lemka = enemy as Enemy;
                            lemkaCooldown = true;
                            lemkaIndex = 0;
                            Lemka.Interval = 300;

                            if (enemy.facingRight)
                                enemy.pb.Image = Resources.Lemka_Charge_Right;
                            else
                                enemy.pb.Image = Resources.Lemka_Charge_Left;

                            enemy.differentAnimation = true;

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

                if (stark != null && bossPhase != 0) {
                    //stark movement phase 1
                    if (enemy == stark && bossPhase == 1 && !starkIdle) {
                        stark.moveSwitch = false;
                        if (Math.Abs(stark.pb.Left + 135 - Player.Left) < 20) {
                            stark.moveLeft = false;
                            stark.moveRight = false;
                        } else if (stark.pb.Left + 135 > Player.Left) {
                            stark.moveLeft = true;
                            stark.moveRight = false;
                        } else if (stark.pb.Left + 135 < Player.Left) {
                            stark.moveRight = true;
                            stark.moveLeft = false;
                        }
                    }
                    //stark movement phase 2 je default moving (zap�n� se po zni�en� baseballky)
                    if (enemy == stark && bossPhase == 2 && !starkIdle)
                        stark.moveSwitch = true;

                    //stark movement phase 3 (v timeru switchuje doleva/doprava)

                    //stark movement idle
                    if (enemy == stark && starkIdle) {
                        stark.moveSwitch = false;
                        stark.moving = true;

                        if (bossPhase == 1)
                            stark.pb.Image = Resources.Stark_Baseball_Exhausted;
                        else if (bossPhase == 2)
                            stark.pb.Image = Resources.Stark_Exhausted;
                    }

                    //stark baseballka
                    if (enemy == stark && bossPhase == 1 && !baseballCooldown && !starkIdle) {
                        if (Math.Abs(stark.pb.Left + 135 - Player.Left) < 40 && Player.Bottom > stark.pb.Top + 465) {
                            stark.pb.Image = Resources.Stark_Baseball_Charge;
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
                            (HitboxAttackTop != null && baseballka.Bounds.IntersectsWith(HitboxAttackTop.Bounds)))) {
                        alreadyHit = true;
                        stark.TakeDamage(2, GameScene);
                        soundhitSomeone.PlaySound();
                    }

                    //zjisteni Starkovo HP a zm�na bossPhase
                    if (!changedPhase && (stark.health == 54 || stark.health == 48)) {
                        Stark.Interval = 1;
                        starkIndex = 1;
                        Stark.Start();
                        changedPhase = true;
                        if (stark.health == 54) {
                            starkIdle = true;
                            if (Player.Left > GameScene.Width / 2) {
                                stark.moveRight = true;
                                stark.moveLeft = false;
                            } else {
                                stark.moveLeft = true;
                                stark.moveRight = false;
                            }
                        }
                        ClearEnemiesInBossFight();
                        SpawnEnemyBoss();
                        return;
                    } else if (!changedPhase && (stark.health == 40)) {

                        //finalni zniceni baseballky
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
                        ClearEnemiesInBossFight();

                    } else if (!changedPhase && (stark.health == 34 || stark.health == 28)) {
                        if (!changedPhase && stark.health == 34) {
                            starkIdle = true;
                            if (Player.Left > GameScene.Width / 2) {
                                stark.moveRight = true;
                                stark.moveLeft = false;
                            } else {
                                stark.moveLeft = true;
                                stark.moveRight = false;
                            }
                        }
                        changedPhase = true;
                        ClearEnemiesInBossFight();
                        SpawnEnemyBoss();
                        return;
                    } else if (!changedPhase && (stark.health == 20)) {
                        ClearEnemiesInBossFight();
                        Stark.Stop();
                        bossPhase = 3;
                        changedPhase = true;
                        starkQ = true;
                        starkIdle = true;
                        if (Player.Left > stark.pb.Right) {
                            stark.moveRight = false;
                            stark.moveLeft = true;
                        } else {
                            stark.moveLeft = false;
                            stark.moveRight = true;
                        }
                        if (stark.moveRight) {
                            Player.Left = 170;
                            Player.Top = 680;
                            hacek = new PictureBox {
                                Left = 15,
                                Top = 105,
                                Width = 110,
                                Height = 180,
                                Image = Resources.Hacek_Idle_Right,
                                SizeMode = PictureBoxSizeMode.StretchImage
                            };
                            hacekPlatforma = new PictureBox {
                                Left = 15,
                                Top = 284,
                                Width = 110,
                                Height = 27,
                                Image = Resources.Knizka,
                            };
                            lbRozvrh1.Visible = true;
                        } else {
                            Player.Left = 1250;
                            Player.Top = 680;
                            hacek = new PictureBox {
                                Left = 1395,
                                Top = 105,
                                Width = 110,
                                Height = 180,
                                Image = Resources.Hacek_Idle_Left,
                                SizeMode = PictureBoxSizeMode.StretchImage
                            };
                            hacekPlatforma = new PictureBox {
                                Left = 1395,
                                Top = 284,
                                Width = 110,
                                Height = 27,
                                Image = Resources.Knizka,
                            };
                            lbRozvrh2.Visible = true;
                        }
                        GameScene.Controls.Add(hacek);
                        GameScene.Controls.Add(hacekPlatforma);

                        stark.pb.Image = Resources.Stark_Exhausted;

                        stark.movementSpeed = 10;
                        starkIndex = 0;
                        Stark.Interval = 500;
                        Stark.Start();
                    } else if (!(stark.health == 54) && !(stark.health == 48) && !(stark.health == 40) && !(stark.health == 34) &&
                          !(stark.health == 28) && !(stark.health == 20) && stark.health > 20) {
                        changedPhase = false;
                    }

                    int bossEnemyCount = 0;
                    foreach (PictureBox enemyBoss in GameScene.Controls.OfType<PictureBox>().Where(x => x.Tag == "Enemy"))
                        bossEnemyCount++;

                    if (stark.health != 54 && stark.health != 34) {
                        starkSmackFix = true;
                    }

                    if (((stark.health == 54 && bossEnemyCount == 1) || (stark.health == 34 && bossEnemyCount == 1)) && starkSmackFix) {
                        starkIdle = false;
                        stark.pb.Image = Resources.Stark_Baseball_Idle;
                        starkSmackFix = false;
                    }

                    StarkHealth.Width = stark.health * 10;
                }

                #endregion

                //enemy doleva a doprava
                if (enemy.moving) {
                    if (enemy.moveLeft) {
                        if (enemy.pb.Left > enemy.xLeft)
                            enemy.pb.Left -= enemy.movementSpeed;
                        else if (enemy.moveSwitch) {
                            enemy.moveLeft = false;
                            enemy.moveRight = true;
                        }
                        enemy.facingRight = false;
                    }
                    if (enemy.moveRight) {
                        if (enemy.pb.Left < enemy.xRight)
                            enemy.pb.Left += enemy.movementSpeed;
                        else if (enemy.moveSwitch) {
                            enemy.moveLeft = true;
                            enemy.moveRight = false;
                        }
                        enemy.facingRight = true;
                    }
                }


                //bouch�n� LMB
                if (enemy != stark) {
                    if ((HitboxAttackLeft != null && enemy.pb.Bounds.IntersectsWith(HitboxAttackLeft.Bounds)) ||
                        (HitboxAttackRight != null && enemy.pb.Bounds.IntersectsWith(HitboxAttackRight.Bounds)) ||
                        (HitboxAttackTop != null && enemy.pb.Bounds.IntersectsWith(HitboxAttackTop.Bounds))) {
                        if (!alreadyHit) {
                            alreadyHit = true;
                            enemy.TakeDamage(2, GameScene);
                            soundhitSomeone.PlaySound();
                        }
                    }
                }

                //nara�en� do enemy -HP
                if (enemy.pb.Bounds.IntersectsWith(Player.Bounds) && canGetHit && (enemy != stark || (enemy == stark && bossPhase == 3)))
                    Hit(enemy.pb);

                #region Projektily
                //let projektil�
                if (enemy.projectile != null) {
                    //mimo obrazovku zni�en�
                    if (enemy.projectile.Left < 0 || enemy.projectile.Right > GameScene.Width || enemy.projectile.Top < 0 || enemy.projectile.Bottom > GameScene.Height) {
                        DestroyAll(enemy.projectile, GameScene);
                        enemy.projectileStop = true;
                        soundProjectileDestroy.PlaySound();
                    }
                    foreach (PictureBox terrain in GameScene.Controls.OfType<PictureBox>().Where(x => x.Tag != null)) {
                        if (terrain.Tag.ToString().Contains("Terrain")) {

                            //Stark ni�� kn�ky
                            if (enemy.type == Enemy.enemyType.Stark && enemy.projectile.Bounds.IntersectsWith(terrain.Bounds) && terrain.Tag.ToString().Contains("Book")) {
                                if (terrain == bookLeft.pb)
                                    bookLeftDestroyed = true;
                                if (terrain == bookRight.pb)
                                    bookRightDestroyed = true;
                                if (hacekOnBook != null && hacekOnBook.pb.Left == terrain.Left) {
                                    hacekOnBook.TakeDamage(20, GameScene);
                                }
                                terrain.Dispose();
                                DestroyAll(enemy.projectile, GameScene);
                                enemy.projectileStop = true;
                                soundDestroy.PlaySound();
                            }
                            //kdy� projektil naraz� do ter�nu, zni�� se
                            if (enemy.projectile.Bounds.IntersectsWith(terrain.Bounds)) {
                                DestroyAll(enemy.projectile, GameScene);
                                enemy.projectileStop = true;
                                soundProjectileDestroy.PlaySound();
                            }
                        }
                    }
                    if (enemy.projectile.Bounds.IntersectsWith(Player.Bounds) && canGetHit) {
                        //hr�� dost�v� dmg od projektilu
                        Hit(enemy.projectile);
                        DestroyAll(enemy.projectile, GameScene);
                        enemy.projectileStop = true;
                    }
                    if (enemy.type == Enemy.enemyType.Oberhofnerova || enemy.type == Enemy.enemyType.Stark) {
                        //Oberhofnerovy a Starkovo pohyb projektil�
                        if (!enemy.projectileStop && !enemy.projectileParry) {
                            enemy.projectile.Left += enemy.projectileSpeedX;
                            enemy.projectile.Top += enemy.projectileSpeedY;
                        }
                    }
                    if (enemy.type == Enemy.enemyType.Hacek) {
                        //H��kovo nav�d�n� st�ely
                        if ((HitboxAttackLeft != null && HitboxAttackLeft.Bounds.IntersectsWith(enemy.projectile.Bounds))
                            || (HitboxAttackRight != null && HitboxAttackRight.Bounds.IntersectsWith(enemy.projectile.Bounds))
                            || (HitboxAttackTop != null && HitboxAttackTop.Bounds.IntersectsWith(enemy.projectile.Bounds))) {
                            DestroyAll(enemy.projectile, GameScene);
                            enemy.projectileStop = true;
                        }
                        if (!enemy.projectileStop) {
                            int projectileSpeed = 9;

                            int projectileX = enemy.projectile.Left + enemy.projectile.Width / 2;
                            int projectileY = enemy.projectile.Top + enemy.projectile.Height / 2;

                            double uX = playerCenterX - projectileX;
                            double uY = playerCenterY - projectileY;

                            double u = Math.Sqrt(Math.Pow(uX, 2) + Math.Pow(uY, 2));

                            int projectileSpeedX;
                            int projectileSpeedY;

                            try {
                                projectileSpeedX = Convert.ToInt32(uX / u * projectileSpeed);
                                projectileSpeedY = Convert.ToInt32(uY / u * projectileSpeed);
                            } catch {
                                projectileSpeedX = 1;
                                projectileSpeedY = 1;
                            }

                            enemy.projectile.Left += projectileSpeedX;
                            enemy.projectile.Top += projectileSpeedY;

                        } else {
                            DestroyAll(enemy.projectile, GameScene);
                            enemy.projectileStop = true;
                        }
                        if (enemy.projectileStop)
                            Hacek.Start();

                        //Pt��ek
                        if (enemy.projectileBirdFacingRight) {
                            if (animationTick % 10 == 0)
                                enemy.projectile.Image = Resources.twitter_right;
                            if (animationTick % 20 == 0)
                                enemy.projectile.Image = Resources.twitter_right_move;
                        } else {
                            if (animationTick % 10 == 0)
                                enemy.projectile.Image = Resources.twitter_left;
                            if (animationTick % 20 == 0)
                                enemy.projectile.Image = Resources.twitter_left_move;
                        }

                    }
                    if (enemy.type == Enemy.enemyType.Stark) {
                        //odr�en� k��dy do Starka
                        if ((HitboxAttackLeft != null && HitboxAttackLeft.Bounds.IntersectsWith(enemy.projectile.Bounds))
                            || (HitboxAttackRight != null && HitboxAttackRight.Bounds.IntersectsWith(enemy.projectile.Bounds))
                            || (HitboxAttackTop != null && HitboxAttackTop.Bounds.IntersectsWith(enemy.projectile.Bounds))) {
                            enemy.projectileParry = true;
                            if (HitboxAttackLeft != null && HitboxAttackLeft.Bounds.IntersectsWith(enemy.projectile.Bounds))
                                enemy.projectileLeft = true;
                            if (HitboxAttackRight != null && HitboxAttackRight.Bounds.IntersectsWith(enemy.projectile.Bounds))
                                enemy.projectileRight = true;
                            if (HitboxAttackTop != null && HitboxAttackTop.Bounds.IntersectsWith(enemy.projectile.Bounds))
                                enemy.projectileUp = true;
                        }

                        //let k��dy
                        if (enemy.projectileLeft && !enemy.projectileStop)
                            enemy.projectile.Left -= 6;
                        if (enemy.projectileRight && !enemy.projectileStop)
                            enemy.projectile.Left += 6;
                        if (enemy.projectileUp && !enemy.projectileStop)
                            enemy.projectile.Top -= 6;

                        //dmg Starkovi, zni�� se k��da
                        if (enemy.projectile.Bounds.IntersectsWith(enemy.pb.Bounds) && enemy.projectileParry && !alreadyHit) {
                            enemy.projectileStop = true;
                            alreadyHit = true;
                            enemy.TakeDamage(2, GameScene);
                            soundhitSomeone.PlaySound();
                            DestroyAll(enemy.projectile, GameScene);
                        }
                    }
                }
                #endregion
            }
        }

        #region Absence
        if (absenceArray != null) {
            foreach (Absence absence in absenceArray) {
                if (absence.spawn && absence.move) {
                    if (absence.spawned == false) {
                        absence.spawned = true;
                        if (absence.type == "X")
                            absence.pb.Left = absence.from;
                        else
                            absence.pb.Top = absence.from;

                        absence.pb.SetBounds(absence.boundsX, absence.boundsY, 30, 30);
                        GameScene.Controls.Add(absence.pb);
                    }

                    if (absence.type == "X") {
                        if (absence.plusCoordinates) {
                            if (absence.pb.Left < absence.to)
                                absence.pb.Left += absence.movementSpeed;
                            else {
                                absence.pb.Bounds = Rectangle.Empty;
                                GameScene.Controls.Remove(absence.pb);
                            }
                        } else {
                            if (absence.pb.Left > absence.to)
                                absence.pb.Left -= absence.movementSpeed;
                            else {
                                absence.pb.Bounds = Rectangle.Empty;
                                GameScene.Controls.Remove(absence.pb);
                            }
                        }
                    } else if (absence.type == "Y") {
                        if (absence.plusCoordinates) {
                            if (absence.pb.Top < absence.to)
                                absence.pb.Top += absence.movementSpeed;
                            else {
                                absence.pb.Bounds = Rectangle.Empty;
                                GameScene.Controls.Remove(absence.pb);
                            }
                        } else {
                            if (absence.pb.Top > absence.to)
                                absence.pb.Top -= absence.movementSpeed;
                            else {
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
        if (Enemy.stopwatch.ElapsedMilliseconds > 15000 && !Enemy.basnickaHit) {
            Enemy.basnickaHit = true;
            Enemy.basnicka.Pause();
            soundHit.PlaySound();
            soundSysalova.PlaySound();
            if (!cheatHealth)
                playerHealth--;
            if (playerHealth <= 0)
                continueGame = false;
            SaveFileWrite();
            HealthUI();
        } else if (Enemy.stopwatch.ElapsedMilliseconds > 18000) {
            Enemy.stopwatch.Restart();
            Enemy.basnicka.Resume();
            Enemy.basnickaHit = false;
        }
        if (Enemy.reader.Position >= Enemy.reader.Length) {
            Enemy.reader.Position = 0;
            Enemy.basnicka.Play();
            Enemy.basnicka.Pause();
            Enemy.basnicka.Resume();
        }

        // Fix
        if (Enemy.stopwatch.IsRunning && !Enemy.basnickaHit && (Enemy.basnicka.PlaybackState == PlaybackState.Stopped || Enemy.basnicka.PlaybackState == PlaybackState.Paused))
            Enemy.basnicka.Play();

        // Odhozeni od nepratel
        if (knockback) {
            if (Player.Right < enMiddle && moveLeft) {
                Player.Left -= 10;
                Dash.Interval = 1;
            } else if (moveRight) {
                Player.Left += 10;
                Dash.Interval = 1;
            }
        }

        // Smrt
        if (playerHealth <= 0) {
            if (!soundDeathOnce) {
                soundDeath.PlaySound();
                soundDeathOnce = true;
            }

            UpdateMethod.Interval = 35;
            GameScene.Enabled = false;
            lbGameOver.Left = this.Width / 2 - lbGameOver.Width / 2;
            lbGameOver.Top = this.Height / 2 - lbGameOver.Height / 2 - 100;
            disableAllInputs = true;
        }

        Player.BringToFront();

        if (!specialImage && !winAnimation) {
            if ((A || D || cLeft || cRight) && jumpSpeed < 2 && jumpSpeed > -2) {
                if (facingRight) {
                    if (animationTick % 10 == 0)
                        Player.Image = Resources.Player_Walk_Right1;
                    if (animationTick % 20 == 0)
                        Player.Image = Resources.Player_Walk_Right2;
                    if (animationTick % 30 == 0)
                        Player.Image = Resources.Player_Walk_Right3;
                } else {
                    if (animationTick % 10 == 0)
                        Player.Image = Resources.Player_Walk_Left1;
                    if (animationTick % 20 == 0)
                        Player.Image = Resources.Player_Walk_Left2;
                    if (animationTick % 30 == 0)
                        Player.Image = Resources.Player_Walk_Left3;
                }
            } else {
                idleTime++;
                if (idleTime > 10 && jumpSpeed < 2 && jumpSpeed > -2) {
                    if (animationTick % 20 == 0)
                        Player.Image = Resources.Player_Idle1;
                    if (animationTick % 40 == 0)
                        Player.Image = Resources.Player_Idle2;
                }
            }
        } else if (winAnimation) {
            if (animationTick % 10 == 0)
                Player.Image = Resources.Player_Twerk1;
            if (animationTick % 20 == 0)
                Player.Image = Resources.Player_Twerk2;
        }
    }

    #region Tutorial
    private async void InstrukceTimer_Tick(object sender, EventArgs e) {
        if (writeInstructions) {
            if (tutorialPhase < poleInstrukci.Length) {
                writeInstructions = false;
                typing = true;
                lbTutorial.Text = string.Empty;
                foreach (char a in poleInstrukci[tutorialPhase]) {
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

                switch (tutorialPhase) {
                    case 2 or 3 or 6 or 11 or 13 or 16 or 21 or 24:
                        break;
                    default:
                        writeInstructions = true;
                        break;
                }

                if (tutorialPhase == 9) {
                    tutorialLemka.moving = true;
                    tutorialLemka.pb.Left = 1100;
                    tutorialLemka.pb.Top = 137;
                } else if (tutorialPhase == 14) {
                    tutorialSysalova.pb.Left = 1200;
                    tutorialSysalova.pb.Top = 157;
                    Enemy.basnicka.Resume();
                    Enemy.stopwatch.Restart();
                } else if (tutorialPhase == 19) {
                    tutorialHacek.pb.Left = 1300;
                    tutorialHacek.pb.Top = GameScene.Height - tutorialHacek.pb.Height;
                    Hacek.Start();
                    tutBanQ = false;
                } else if (tutorialPhase == 22) {
                    tutorialOberhofnerova.pb.Left = 800;
                    tutorialOberhofnerova.pb.Top = 112;
                    tutorialOberhofnerova.moving = true;
                    Oberhofnerova.Start();
                }
            } else {
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

    #region PlayerTimers
    private void abilityLMB_Tick(object sender, EventArgs e) {
        if (abilityLMBIndex == 0) {
            if (HitboxAttackLeft != null)
                DestroyAll(HitboxAttackLeft, GameScene);
            if (HitboxAttackRight != null)
                DestroyAll(HitboxAttackRight, GameScene);
            if (HitboxAttackTop != null)
                DestroyAll(HitboxAttackTop, GameScene);

            LMB = false;
            specialImage = false;
            abilityLMB.Interval = 600;
        }
        if (abilityLMBIndex == 1) {
            attackLMBcooldown = false;
            abilityLMB.Stop();
        }
        abilityLMBIndex++;
    }
    private void Dash_Tick(object sender, EventArgs e) {
        if (dashIndex == 0) {
            canDash = false;
            dashLeft = false;
            dashRight = false;
            banInput = false;
            specialImage = false;
            jumpSpeed = -1;
            Dash.Interval = 1500;
        }
        if (dashIndex == 1) {
            canDash = true;
            Dash.Stop();
        }
        dashIndex++;
    }
    private void AbilityQ_Tick(object sender, EventArgs e) {
        if (abilityQIndex == 0) {
            attackQphase1 = true;
            AbilityQ.Interval = 350;
        }
        if (abilityQIndex == 1) {
            attackQphase1 = false;
            attackQphase2 = true;
            AbilityQ.Interval = 200;
        }
        if (abilityQIndex == 2) {
            AbilityQ.Interval = 2000;
            attackQphase2 = false;
            banInput = false;
            unHitable = false;
            specialImage = false;
        }
        if (abilityQIndex == 3) {
            attackQcooldown = false;
            AbilityQ.Stop();
        }
        abilityQIndex++;
    }

    private void DMGcooldown_Tick(object sender, EventArgs e) {
        if (dmgIndex == 0) {
            knockback = false;
            DMGcooldown.Interval = 2000;
        } else {
            canGetHit = true;
            DMGcooldown.Stop();
        }
        dmgIndex++;
    }

    #endregion

    #region EnemyTimers

    private void Lemka_Tick(object sender, EventArgs e) {
        if (lemkaIndex == 0) {
            if (lemka.disposed) {
                lemkaCooldown = false;
                return;
            }

            if (lemkaRight) {
                HitboxLemkaRight = new PictureBox {
                    Left = lemka.pb.Right,
                    Top = lemka.pb.Top + 50,
                    Width = 100,
                    Height = 80,
                    Image = Resources.Lemka_Mouse_right,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                };
                GameScene.Controls.Add(HitboxLemkaRight);
            } else {
                HitboxLemkaLeft = new PictureBox {
                    Left = lemka.pb.Left - 100,
                    Top = lemka.pb.Top + 50,
                    Width = 100,
                    Height = 80,
                    Image = Resources.Lemka_Mouse_left,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                };
                GameScene.Controls.Add(HitboxLemkaLeft);
            }
            soundLemka.PlaySound();

            if (lemka.facingRight)
                lemka.pb.Image = Resources.Lemka_Attack_Right;
            else
                lemka.pb.Image = Resources.Lemka_Attack_Left;

            Lemka.Interval = 100;
        }
        if (lemkaIndex == 1) {
            if (HitboxLemkaLeft != null)
                DestroyAll(HitboxLemkaLeft, GameScene);
            if (HitboxLemkaRight != null)
                DestroyAll(HitboxLemkaRight, GameScene);
            Lemka.Interval = 3000;
            lemka.moving = true;
            lemka.differentAnimation = false;
        }
        if (lemkaIndex == 2) {
            lemkaCooldown = false;
            Lemka.Stop();
        }
        lemkaIndex++;
    }
    private void Oberhofnerova_Tick(object sender, EventArgs e) {
        foreach (Enemy enemy in enemyArray) {
            if (enemy.type == Enemy.enemyType.Oberhofnerova && !enemy.disposed) {
                enemy.ShootProjectile(Player, GameScene);
                soundProjectile.PlaySound();
            }
        }
    }

    private void Hacek_Tick(object sender, EventArgs e) {
        foreach (Enemy enemy in enemyArray) {
            if (enemy.type == Enemy.enemyType.Hacek && !enemy.disposed) {
                enemy.ShootProjectile(Player, GameScene);
                soundProjectile.PlaySound();
            }
        }
        Hacek.Stop();
    }
    private void Stark_Tick(object sender, EventArgs e) {
        if (bossPhase == 1) {
            //bouchani baseballkou
            if (starkIndex == 0) {
                //bouchne
                baseballka = new PictureBox {
                    Left = stark.pb.Left + 55,
                    Top = stark.pb.Top + 465,
                    Width = 225,
                    Height = 185,
                    Image = Resources.Stark_Baseballka,
                };
                GameScene.Controls.Add(baseballka);
                baseballka.BringToFront();
                baseballGetDMG = false;
                baseballCooldown = true;
                baseballSlam++;

                stark.pb.Image = Resources.Stark_Baseball_Smack;
                soundBaseball.PlaySound();

                if (baseballSlam % 3 == 0) {
                    baseballGetDMG = true;
                    baseballCooldown = true;
                    Stark.Stop();
                } else {
                    baseballGetDMG = false;
                    Stark.Interval = 500;
                }
            } else if (starkIndex == 1) {
                if (baseballka != null)
                    DestroyAll(baseballka, GameScene);
                stark.moving = true;
                Stark.Interval = 3000;
                stark.pb.Image = Resources.Stark_Baseball_Idle;
            } else if (starkIndex == 2)
                baseballCooldown = false;

            starkIndex++;
        } else if (bossPhase == 2 && !starkIdle) {
            //spawn enemy a strileni kridy
            stark.ShootProjectile(Player, GameScene);
            stark.projectileParry = false;
            stark.projectileLeft = false;
            stark.projectileRight = false;
            stark.projectileUp = false;
            soundChalk.PlaySound();
        } else if (bossPhase == 3) {
            if (starkIndex == 0) {
                //special, hacek objevi a nici knizky, prida pruzinu
                int cooldown = 0;
                foreach (Terrain terrain in terrainArray) {
                    if (terrain.pb.Tag.ToString().Contains("Book") && terrain.pb.Bounds != Rectangle.Empty) {
                        terrain.Dispose();
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
            if (starkIndex == 1) {
                if (!stark.moveLeft)
                    stark.pb.Image = Resources.Stark_Dash_Charge_Left;
                else
                    stark.pb.Image = Resources.Stark_Dash_Charge_Right;

                Stark.Interval = 1000;
            } else if (starkIndex == 2) {
                stark.moveLeft = !stark.moveLeft;
                stark.moveRight = !stark.moveRight;

                if (stark.moveLeft)
                    stark.pb.Image = Resources.Stark_Dash_Left;
                else
                    stark.pb.Image = Resources.Stark_Dash_Right;

                Stark.Interval = 2000;
            } else if (starkIndex == 3) {
                stark.pb.Image = Resources.Stark_Exhausted;
                Stark.Interval = 3000;
                starkIndex = 0;
            }
            starkIndex++;
        }
    }

    #endregion

    #region Voidy

    private void CheckPS() {
        var input = new DirectInput();
        var joystickGuid = Guid.Empty;

        foreach (var deviceInstance in input.GetDevices(DeviceClass.GameControl, DeviceEnumerationFlags.AllDevices)) {
            if (!(deviceInstance.Type == DeviceType.Gamepad)) // nep�id� xbox
                joystickGuid = deviceInstance.InstanceGuid;
        }

        if (joystickGuid == Guid.Empty)
            psController = false;
        else {
            joystick = new Joystick(input, joystickGuid);
            joystick.Properties.BufferSize = 128;
            joystick.Acquire();
            psController = true;
        }
    }

    private void ClearEnemiesInBossFight() {
        foreach (Enemy bossEnemy in enemyArray) {
            if (bossEnemy != stark) {
                bossEnemy.Dispose();
            }
        }
    }
    private void SpawnEnemyBoss() {
        if (reseting) return;

        if (Player.Left > GameScene.Width / 2)
            playerSideLeft = false;
        else
            playerSideLeft = true;

        int random1 = 0, random2 = 0, LemkaMove = 0;
        if (playerSideLeft && bookRightDestroyed) {
            random1 = Random.Shared.Next(1, 4);
            random2 = Random.Shared.Next(1, 4);
            while (random1 == random2)
                random1 = Random.Shared.Next(1, 4);

            LemkaMove = 925;
        } else if (playerSideLeft && !bookRightDestroyed) {
            random1 = Random.Shared.Next(1, 5);
            random2 = Random.Shared.Next(1, 5);
            while (random1 == random2)
                random1 = Random.Shared.Next(1, 5);

            LemkaMove = 1393;
        }
        if (!playerSideLeft && bookLeftDestroyed) {
            random1 = Random.Shared.Next(1, 4);
            random2 = Random.Shared.Next(1, 4);
            while (random1 == random2)
                random1 = Random.Shared.Next(1, 4);

            LemkaMove = 485;
        } else if (!playerSideLeft && !bookLeftDestroyed) {
            random1 = Random.Shared.Next(1, 5);
            random2 = Random.Shared.Next(1, 5);
            while (random1 == random2)
                random1 = Random.Shared.Next(1, 5);

            LemkaMove = 18;
        }

        if (playerSideLeft) {
            switch (random1) {
                case 1: bossEnemy1 = new(1296, 112, 100, 100, Resources.Oberhofnerova_Charge, OberhofnerovaHP, true, 15, 1408, OberhofnerovaMovementSpeed, Enemy.enemyType.Oberhofnerova, 2000, GameScene); Oberhofnerova.Start(); break;
                case 2: bossEnemy1 = new(714, 500, 100, 160, Resources.Sysalova_living_left, SysalovaHP, false, 0, 0, 0, Enemy.enemyType.Sysalova, 0, GameScene); Enemy.basnicka.Resume(); Enemy.stopwatch.Start(); break;
                case 3: bossEnemy1 = new(1420, 616, 110, 180, Resources.Lemka_Walk_Right_Normal1, LemkaHP, true, LemkaMove, 1420, LemkaMovementSpeed, Enemy.enemyType.Lemka, 0, GameScene); break;
                case 4: bossEnemy1 = new(1282, 418, 110, 180, Resources.Hacek_Idle_Left, HacekHP, false, 0, 0, 0, Enemy.enemyType.Hacek, 3000, GameScene); Hacek.Start(); hacekOnBook = bossEnemy1; break;
            }
            switch (random2) {
                case 1: bossEnemy2 = new(1296, 112, 100, 100, Resources.Oberhofnerova_Charge, OberhofnerovaHP, true, 15, 1408, OberhofnerovaMovementSpeed, Enemy.enemyType.Oberhofnerova, 2000, GameScene); Oberhofnerova.Start(); break;
                case 2: bossEnemy2 = new(714, 500, 100, 160, Resources.Sysalova_living_left, SysalovaHP, false, 0, 0, 0, Enemy.enemyType.Sysalova, 0, GameScene); Enemy.basnicka.Resume(); Enemy.stopwatch.Start(); break;
                case 3: bossEnemy2 = new(1420, 616, 110, 180, Resources.Lemka_Walk_Right_Normal1, LemkaHP, true, LemkaMove, 1420, LemkaMovementSpeed, Enemy.enemyType.Lemka, 0, GameScene); break;
                case 4: bossEnemy2 = new(1282, 418, 110, 180, Resources.Hacek_Idle_Left, HacekHP, false, 0, 0, 0, Enemy.enemyType.Hacek, 3000, GameScene); Hacek.Start(); hacekOnBook = bossEnemy2; break;
            }
        } else {
            switch (random1) {
                case 1: bossEnemy1 = new(100, 112, 100, 100, Resources.Oberhofnerova_Charge, OberhofnerovaHP, true, 15, 1408, OberhofnerovaMovementSpeed, Enemy.enemyType.Oberhofnerova, 2000, GameScene); Oberhofnerova.Start(); break;
                case 2: bossEnemy1 = new(714, 500, 100, 160, Resources.Sysalova_living_left, SysalovaHP, false, 0, 0, 0, Enemy.enemyType.Sysalova, 0, GameScene); Enemy.basnicka.Resume(); Enemy.stopwatch.Start(); break;
                case 3: bossEnemy1 = new(0, 616, 110, 180, Resources.Lemka_Walk_Right_Normal1, LemkaHP, true, 0, LemkaMove, LemkaMovementSpeed, Enemy.enemyType.Lemka, 0, GameScene); break;
                case 4: bossEnemy1 = new(122, 419, 110, 180, Resources.Hacek_Idle_Left, HacekHP, false, 0, 0, 0, Enemy.enemyType.Hacek, 3000, GameScene); Hacek.Start(); hacekOnBook = bossEnemy1; break;
            }
            switch (random2) {
                case 1: bossEnemy2 = new(100, 112, 100, 100, Resources.Oberhofnerova_Charge, OberhofnerovaHP, true, 15, 1408, OberhofnerovaMovementSpeed, Enemy.enemyType.Oberhofnerova, 2000, GameScene); Oberhofnerova.Start(); break;
                case 2: bossEnemy2 = new(714, 500, 100, 160, Resources.Sysalova_living_left, SysalovaHP, false, 0, 0, 0, Enemy.enemyType.Sysalova, 0, GameScene); Enemy.basnicka.Resume(); Enemy.stopwatch.Start(); break;
                case 3: bossEnemy2 = new(0, 616, 110, 180, Resources.Lemka_Walk_Right_Normal1, LemkaHP, true, 0, LemkaMove, LemkaMovementSpeed, Enemy.enemyType.Lemka, 0, GameScene); break;
                case 4: bossEnemy2 = new(122, 419, 110, 180, Resources.Hacek_Idle_Left, HacekHP, false, 0, 0, 0, Enemy.enemyType.Hacek, 3000, GameScene); Hacek.Start(); hacekOnBook = bossEnemy2; break;
            }
        }
        enemyArray = new Enemy[] { stark, bossEnemy1, bossEnemy2 };
    }
    private void NuggetDisappear_Tick(object sender, EventArgs e) {
        DestroyAll(nuggetPB, GameScene);
        NuggetDisappear.Stop();
    }
    void Hit(PictureBox pb) {
        soundHit.PlaySound();
        enMiddle = pb.Right;
        canGetHit = false;
        knockback = true;
        dmgIndex = 0;
        DMGcooldown.Interval = 100;
        DMGcooldown.Start();
        if (!cheatHealth && !unHitable && !tutorial)
            playerHealth--;
        if (playerHealth <= 0)
            continueGame = false;
        SaveFileWrite();
        HealthUI();
    }
    static private void DestroyAll(PictureBox pb, Panel panel) {
        pb.Bounds = Rectangle.Empty;
        panel.Controls.Remove(pb);
        pb.Dispose();
    }
    void Reset() {
        Enemy.stopwatch.Reset();
        Enemy.basnicka.Pause();
        Enemy.basnickaHit = false;

        nuggetSpawn = false;
        specialImage = false;

        //fix na Q
        if (attackQphase1 || attackQphase2) {
            attackQphase1 = false;
            attackQphase2 = false;
            AbilityQ.Stop();
            banInput = false;
            unHitable = false;
            attackQcooldown = false;
        }

        //inputy
        A = false; D = false; Space = false; Q = false; LMB = false;
        xboxA = false; xboxX = false; xboxB = false; xboxOptions = false; xboxRight = false; xboxLeft = false; xboxUp = false; xboxTrigger = false;
        psA = false; psB = false; psX = false; psOptions = false; psRight = false; psLeft = false; psUp = false; psTrigger = false;

        Oberhofnerova.Stop();
        Hacek.Stop();

        if (absenceArray != null) {
            foreach (Absence absence in absenceArray)
                absence.Dispose();
        }

        if (enemyArray != null) {
            foreach (Enemy enemy in enemyArray)
                enemy.Dispose();

        }
        if (terrainArray != null) {
            foreach (Terrain terrain in terrainArray)
                terrain.Dispose();
        }
        if (absenceArray != null) {
            foreach (Absence absence in absenceArray)
                absence.Dispose();

        }
        if (nuggetList != null) {
            foreach (Nugget nugget in nuggetList)
                nugget.Dispose();
        }
        if (baseballka != null)
            DestroyAll(baseballka, GameScene);

        //reset hr��e
        Player.Left = 75;
        Player.Top = 526;

        StarkHealthBackground.Visible = false;
        StarkHealth.Visible = false;
    }
    void FullReset() {
        //pozice,timery,bloky,enemy...
        Reset();

        lbGameOver.Top = -300;

        //default promenny
        canDash = true;
        attackLMBcooldown = false;
        levelCount = 1;
        if (difficulty != "Insane")
            playerHealth = 5;
        else
            playerHealth = 1;
        canGetHit = true;
        disableAllInputs = false;
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

    void HealthUI() {
        if (difficulty != "Insane") {
            if (playerHealth > 0)
                health1.Image = Resources.nugeta1;
            else
                health1.Image = Resources.nugeta_drobky;
            if (playerHealth > 1)
                health2.Image = Resources.nugeta2;
            else
                health2.Image = Resources.nugeta_drobky;
            if (playerHealth > 2)
                health3.Image = Resources.nugeta3;
            else
                health3.Image = Resources.nugeta_drobky;
            if (playerHealth > 3)
                health4.Image = Resources.nugeta4;
            else
                health4.Image = Resources.nugeta_drobky;
            if (playerHealth > 4)
                health5.Image = Resources.nugeta5;
            else
                health5.Image = Resources.nugeta_drobky;
        } else {
            if (playerHealth == 1)
                health1.Image = Resources.nugeta1_insane;
            else
                health1.Image = Resources.nugeta_drobky;
            health2.Image = null;
            health3.Image = null;
            health4.Image = null;
            health5.Image = null;
        }
    }

    void TimerHandler(string action) {
        //bool tOberhofnerova, tHacek, tJumpCooldown, tDMGCooldown, tNuggetDisappear;
        if (action == "Pause") {
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

            if (Stark.Enabled && bossPhase == 2) {
                tStark = true;
                Stark.Stop();
            }

            if (Enemy.basnicka.PlaybackState == PlaybackState.Playing) {
                basnickaPlaying = true;
                Enemy.stopwatch.Stop();
                Enemy.basnicka.Pause();
            }
        }
        if (action == "Play") {
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
            if (tStark)
                Stark.Start();

            tOberhofnerova = false;
            tHacek = false;
            tJumpCooldown = false;
            tDMGCooldown = false;
            tNuggetDisappear = false;
            tStark = false;

            if (basnickaPlaying) {
                Enemy.basnicka.Resume();
                Enemy.stopwatch.Start();
            }

            basnickaPlaying = false;
        }
    }

    void SaveFileRead() {
        string filePath = Path.Combine(folderPath, fileName);

        if (!File.Exists(filePath)) {
            continueGame = false;
            savedHealth = 0;
            savedLevel = 0;
            completedGame = false;
            hardestDifficulty = false;
            using (StreamWriter writer = File.CreateText(filePath)) {
                writer.Write("false\n0\n0\nEasy\nfalse\nfalse");
            }
        } else {
            using (StreamReader reader = new StreamReader(filePath)) {
                continueGame = Convert.ToBoolean(reader.ReadLine());
                savedHealth = Convert.ToInt32(reader.ReadLine());
                savedLevel = Convert.ToInt32(reader.ReadLine());
                difficulty = reader.ReadLine();
                completedGame = Convert.ToBoolean(reader.ReadLine());
                hardestDifficulty = Convert.ToBoolean(reader.ReadLine());
            }
        }
    }

    void SaveFileWrite() {
        // velmi jednoduch� saveFile, ka�d� line jeden udaj
        // pokra�ovat ve h�e, hp, level, dohral hru, dohral na nejtezsi obtiznost.. 

        savedHealth = playerHealth;
        savedLevel = currentLevel;
        string saveFile = continueGame + "\n" + savedHealth + "\n" + savedLevel + "\n" + difficulty + "\n" + completedGame + "\n" + hardestDifficulty;
        string filePath = Path.Combine(folderPath, fileName);

        File.WriteAllText(filePath, saveFile);
    }

    void UpdateProgress() {
        SaveFileRead();
        if (continueGame) {
            levelCount = savedLevel;
            playerHealth = savedHealth;
            btContinue.Enabled = true;
        }
        if (completedGame) {
            pbCompletedGame.Visible = true;
            btInsane.Enabled = true;
            lbZaskolak.Visible = false;
        } else
            pbCompletedGame.Visible = false;

        if (hardestDifficulty)
            pbHardestDifficulty.Visible = true;
        else
            pbHardestDifficulty.Visible = false;
    }

    void Pauza() {
        if (!Pause.Visible) {
            Pause.Visible = true;
            Pause.Enabled = true;
            GameScene.Enabled = false;
            GameScene.Visible = false;
            panelPauza.Visible = true;
            if (enLanguage)
                lbNazev.Text = "Paused";
            else
                lbNazev.Text = "Pauza";
            lbNazev.Left = 650;
            UpdateMethod.Stop();
            TimerHandler("Pause");
            Focus();
        } else if (lbNazev.Text == "Pauza" || lbNazev.Text == "Paused") {
            Pause.Visible = false;
            Pause.Enabled = false;
            GameScene.Enabled = true;
            GameScene.Visible = true;
            UpdateMethod.Start();
            TimerHandler("Play");
            Focus();
        }
    }

    void Maximize() {
        this.FormBorderStyle = FormBorderStyle.None;
        this.BackColor = Color.Black;
        this.WindowState = FormWindowState.Maximized;

        int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        int screenHeight = Screen.PrimaryScreen.Bounds.Height;

        int posX = (screenWidth - 1536) / 2;
        int posY = (screenHeight - 864) / 2;

        Menu.Location = new Point(posX, posY);
        Pause.Location = new Point(posX, posY);
        GameScene.Location = new Point(posX, posY);

        maximized = true;
    }

    void Minimize() {
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.Size = new Size(1536, 864);
        this.WindowState = FormWindowState.Normal;

        int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        int screenHeight = Screen.PrimaryScreen.Bounds.Height;

        int posX = (screenWidth - 1558) / 2;
        int posY = (screenHeight - 920) / 2;

        this.Location = new Point(posX, posY);

        Menu.Location = new Point(0, 0);
        Pause.Location = new Point(0, 0);
        GameScene.Location = new Point(0, 0);

        maximized = false;
    }

    #endregion

    #region LevelDesign
    void LevelPrepare() {
        //doplni jedno HP
        if (playerHealth < 5 && difficulty == "Easy") {
            playerHealth++;
            HealthUI();
        }

        Reset();
        soundNextLevel.PlaySound();
        continueGame = true;
    }
    void TutorialLevel() {
        FullReset();
        continueGame = false;
        Terrain terrain1 = new(486, 563, 130, 34, "Terrain Book", Resources.Knizka, GameScene);
        Terrain terrain2 = new(730, 427, 130, 34, "Terrain Book", Resources.Knizka, GameScene);
        Terrain terrain3 = new(997, 317, 511, 34, "Terrain", Resources.Tuzka, GameScene);

        terrainArray = new Terrain[] { terrain1, terrain2, terrain3 };

        tutorialOberhofnerova = new(-100, -100, 100, 100, Resources.Oberhofnerova_Charge, OberhofnerovaHP, true,
            15, 1408, OberhofnerovaMovementSpeed, Enemy.enemyType.Oberhofnerova, 2000, GameScene);
        tutorialLemka = new(-200, -200, 110, 180, Resources.Lemka_Walk_Right_Normal1, LemkaHP, false, 1000, 1400, LemkaMovementSpeed, Enemy.enemyType.Lemka, 0, GameScene);
        tutorialSysalova = new(-200, -200, 100, 160, Resources.Sysalova_living_left, SysalovaHP, false, 0, 0, 0, Enemy.enemyType.Sysalova, 0, GameScene);
        tutorialHacek = new(-200, -200, 110, 180, Resources.Hacek_Idle_Left, HacekHP, false, 0, 0, 0, Enemy.enemyType.Hacek, 3000, GameScene);
        tutorialhidden = new(-100, -100, 20, 20, Resources.Player_Twerk1, 20, false, 0, 0, 0, Enemy.enemyType.Secret, 0, GameScene);

        enemyArray = new Enemy[] { tutorialHacek, tutorialhidden, tutorialLemka, tutorialOberhofnerova, tutorialSysalova };

        Oberhofnerova.Interval = tutorialOberhofnerova.projectileCooldown;
        Hacek.Interval = tutorialHacek.projectileCooldown;

        lbLevel.Text = "Tutorial";
        lbLevel.ForeColor = Color.LightGreen;
    }
    void Level1() {
        LevelPrepare();
        Terrain terrain1 = new(28, 658, 164, 39, "Terrain Book", Resources.Knizka, GameScene);
        Terrain terrain2 = new(291, 753, 100, 72, "Terrain", Resources.PC, GameScene);
        Terrain terrain3 = new(341, 444, 110, 33, "Terrain Book", Resources.Knizka, GameScene);
        Terrain terrain4 = new(577, 260, 593, 34, "Terrain", Resources.Tuzka, GameScene);
        Terrain terrain5 = new(1402, 460, 97, 34, "Terrain Book", Resources.Knizka, GameScene);
        Terrain terrain6 = new(1263, 750, 100, 75, "Terrain", Resources.PC, GameScene);

        terrainArray = new Terrain[] { terrain1, terrain2, terrain3, terrain4, terrain5, terrain6 };


        Enemy enemy1 = new(1296, 12, 100, 100, Resources.Oberhofnerova_Charge, OberhofnerovaHP, true,
            15, 1408, OberhofnerovaMovementSpeed, Enemy.enemyType.Oberhofnerova, 2000, GameScene);
        Enemy enemy2 = new(1000, 645, 110, 180, Resources.Lemka_Walk_Right_Normal1, LemkaHP, true, 400, 1150, LemkaMovementSpeed, Enemy.enemyType.Lemka, 0, GameScene);

        enemyArray = new Enemy[] { enemy1, enemy2 };

        enemy1.pb.Image = Resources.Oberhofnerova_Idle;
        enemy1.pb.BackColor = Color.Transparent;
        Oberhofnerova.Interval = enemy1.projectileCooldown;
        Oberhofnerova.Start();

        lbLevel.Text = "1 / 5";
        currentLevel = 1;
        SaveFileWrite();
    }
    void Level2() {
        LevelPrepare();
        Terrain terrain1 = new(28, 658, 164, 39, "Terrain Book", Resources.Knizka, GameScene);
        Terrain terrain2 = new(28, 360, 97, 34, "Terrain Book", Resources.Knizka, GameScene);
        Terrain terrain3 = new(457, 510, 593, 34, "Terrain", Resources.Tuzka, GameScene);
        Terrain terrain4 = new(370, 189, 110, 33, "Terrain Book", Resources.Knizka, GameScene);
        Terrain terrain5 = new(1088, 248, 100, 75, "Terrain", Resources.PC, GameScene);
        Terrain terrain6 = new(705, 750, 100, 75, "Terrain", Resources.PC, GameScene);
        Terrain terrain7 = new(1342, 659, 100, 167, "Terrain", Resources.Skrinka, GameScene);

        terrainArray = new Terrain[] { terrain1, terrain2, terrain3, terrain4, terrain5, terrain6, terrain7 };

        Enemy enemy1 = new(1296, 12, 100, 100, Resources.Oberhofnerova_Charge, OberhofnerovaHP, true,
            15, 1408, OberhofnerovaMovementSpeed, Enemy.enemyType.Oberhofnerova, 3000, GameScene);
        Enemy enemy2 = new(1342, 499, 100, 160, Resources.Sysalova_living_left, SysalovaHP, false, 0, 0, 0, Enemy.enemyType.Sysalova, 0, GameScene);
        Enemy enemy3 = new(830, 330, 110, 180, Resources.Lemka_Walk_Right_Normal1, LemkaHP, true, 460, 940, LemkaMovementSpeed, Enemy.enemyType.Lemka, 0, GameScene);

        enemyArray = new Enemy[] { enemy1, enemy2, enemy3 };

        Absence absence1 = new(740, 720, 2000, "Y", false, false, 720, 550, 8);

        absenceArray = new Absence[] { absence1 };

        Oberhofnerova.Interval = enemy1.projectileCooldown;
        Oberhofnerova.Start();

        Enemy.basnicka.Resume();
        Enemy.stopwatch.Start();

        lbLevel.Text = "2 / 5";
        currentLevel = 2;
        SaveFileWrite();
    }

    void Level3() {
        LevelPrepare();
        Terrain terrain1 = new(28, 658, 164, 39, "Terrain Book", Resources.Knizka, GameScene);
        Terrain terrain2 = new(270, 377, 100, 69, "Terrain", Resources.PC, GameScene);
        Terrain terrain3 = new(455, 161, 593, 34, "Terrain", Resources.Tuzka, GameScene);
        Terrain terrain4 = new(681, 531, 110, 33, "Terrain Book", Resources.Knizka, GameScene);
        Terrain terrain5 = new(1029, 719, 91, 40, "Terrain Book", Resources.Knizka, GameScene);
        Terrain terrain6 = new(1353, 640, 110, 75, "Terrain", Resources.PC, GameScene);

        terrainArray = new Terrain[] { terrain1, terrain2, terrain3, terrain4, terrain5, terrain6 };

        Enemy enemy1 = new(686, 372, 100, 160, Resources.Sysalova_living_left, SysalovaHP, false, 0, 0, 0, Enemy.enemyType.Sysalova, 0, GameScene);
        Enemy enemy2 = new(1353, 460, 110, 180, Resources.Hacek_Idle_Left, HacekHP, false, 0, 0, 0, Enemy.enemyType.Hacek, 3000, GameScene);

        enemyArray = new Enemy[] { enemy1, enemy2 };

        Absence absence1 = new(576, 121, 1000, "Y", false, true, 121, 12, 8);
        Absence absence2 = new(878, 12, 1000, "Y", true, true, 12, 121, 8);

        absenceArray = new Absence[] { absence1, absence2 };

        if (difficulty == "Easy" || difficulty == "Normal") {
            Nugget nugget1 = new(714, 73, 2, Resources.nugeta_double, GameScene);
            nuggetList.Add(nugget1);
        }

        Hacek.Interval = enemy2.projectileCooldown;
        Hacek.Start();

        Enemy.basnicka.Resume();
        Enemy.stopwatch.Start();

        lbLevel.Text = "3 / 5";
        currentLevel = 3;
        SaveFileWrite();
    }
    void Level4() {
        LevelPrepare();
        Terrain terrain1 = new(28, 658, 164, 39, "Terrain Book", Resources.Knizka, GameScene);
        Terrain terrain2 = new(126, 280, 110, 35, "Terrain Book", Resources.Knizka, GameScene);
        Terrain terrain3 = new(449, 524, 100, 49, "Terrain Book", Resources.Knizka, GameScene);
        Terrain terrain4 = new(449, 753, 91, 72, "Terrain", Resources.PC, GameScene);
        Terrain terrain5 = new(840, 480, 593, 40, "Terrain", Resources.Tuzka_reversed, GameScene);
        Terrain terrain6 = new(840, 155, 593, 40, "Terrain", Resources.Tuzka_reversed, GameScene);
        Terrain terrain7 = new(1323, 418, 110, 63, "Terrain", Resources.PC, GameScene);

        terrainArray = new Terrain[] { terrain1, terrain2, terrain3, terrain4, terrain5, terrain6, terrain7 };

        Enemy enemy1 = new(132, 120, 100, 160, Resources.Sysalova_living_left, SysalovaHP, false, 0, 0, 0, Enemy.enemyType.Sysalova, 0, GameScene);
        Enemy enemy2 = new(1323, 239, 110, 180, Resources.Hacek_Idle_Left, HacekHP, false, 0, 0, 0, Enemy.enemyType.Hacek, 3000, GameScene);
        Enemy enemy3 = new(1296, 12, 100, 100, Resources.Oberhofnerova_Charge, OberhofnerovaHP, true,
            15, 1408, OberhofnerovaMovementSpeed, Enemy.enemyType.Oberhofnerova, 2000, GameScene);
        Enemy enemy4 = new(1066, 645, 110, 180, Resources.Lemka_Walk_Right_Normal1, LemkaHP, true, 548, 1410, LemkaMovementSpeed, Enemy.enemyType.Lemka, 0, GameScene);

        enemyArray = new Enemy[] { enemy1, enemy2, enemy3, enemy4 };

        Absence absence1 = new(934, 198, 1000, "Y", true, true, 198, 449, 8);
        Absence absence2 = new(1111, 449, 1000, "Y", false, true, 449, 198, 8);

        absenceArray = new Absence[] { absence1, absence2 };

        if (difficulty == "Easy" || difficulty == "Normal") {
            Nugget nugget1 = new(1340, 639, 1, Resources.nugeta_normal, GameScene);
            nuggetList.Add(nugget1);
        }

        Hacek.Interval = enemy2.projectileCooldown;
        Hacek.Start();

        Oberhofnerova.Interval = enemy3.projectileCooldown;
        Oberhofnerova.Start();

        Enemy.basnicka.Resume();
        Enemy.stopwatch.Start();

        lbLevel.Text = "4 / 5";
        currentLevel = 4;
        SaveFileWrite();
    }
    void Level5() {
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

        stark = new(1185, 175, 335, 650, Resources.Stark_Baseball_Idle, StarkHP, true, 0, 1185, StarkMovementSpeed, Enemy.enemyType.Stark, 3000, GameScene);

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

    private void Controller_Tick(object sender, EventArgs e) {
        // XBOX
        GamePadState xboxState = GamePad.GetState(PlayerIndex.One);
        if (xboxState.IsConnected) {
            if (!disableAllInputs) {
                xboxA = xboxState.Buttons.A == ButtonState.Pressed;
                xboxX = xboxState.Buttons.X == ButtonState.Pressed && previousXboxState.Buttons.X == ButtonState.Released;
                xboxB = xboxState.Buttons.B == ButtonState.Pressed && previousXboxState.Buttons.B == ButtonState.Released;
                xboxTrigger = xboxState.Triggers.Right > 0;
                xboxLeft = xboxState.ThumbSticks.Left.X < -0.3;
                xboxRight = xboxState.ThumbSticks.Left.X > 0.3;
                xboxUp = xboxState.ThumbSticks.Left.Y > 0.5;
            }

            xboxOptions = xboxState.Buttons.Start == ButtonState.Pressed && previousXboxState.Buttons.Start == ButtonState.Released;

            previousXboxState = xboxState;
        }

        // PlayStation
        try {
            if (psController) {
                joystick.Poll();

                var state = joystick.GetCurrentState();
                var buttons = state.Buttons;

                if (!disableAllInputs) {
                    psA = buttons[1];
                    psX = buttons[0] && !previousButtons[0];
                    psB = buttons[2] && !previousButtons[2];
                    psTrigger = buttons[7];
                    psLeft = (state.X / 32768.0f) - 1 < -0.5f;
                    psRight = (state.X / 32768.0f) - 1 > 0.5f;
                    psUp = (state.Y / 32768.0f) - 1 < -0.3f;
                }
                psOptions = buttons[9] && !previousButtons[9];

                previousButtons = buttons;
            }
        } catch {
            psController = false;
            return;
        }

        // p�em�nit PS a XBOX vstupy na jeden
        cA = xboxA || psA;
        cX = xboxX || psX;
        cB = xboxB || psB;
        cLeft = xboxLeft || psLeft;
        cRight = xboxRight || psRight;
        cUp = xboxUp || psUp;
        cTrigger = xboxTrigger || psTrigger;
        cOptions = xboxOptions || psOptions;

        // Reset hry pomoc� OPTIONS
        if (cOptions && disableAllInputs)
            FullReset();
        else if (cOptions)
            Pauza();
    }

    private void MainWindow_KeyDown(object sender, KeyEventArgs e) {
        //Input - zmacknuti
        if (!disableAllInputs) {
            switch (e.KeyCode) {
                case Keys.A or Keys.Left:
                    A = true;
                    break;
                case Keys.D or Keys.Right:
                    D = true;
                    break;
                case Keys.Space:
                    if (!Space)
                        Space = true;
                    break;
                case Keys.Q:
                    Q = true;
                    break;
                case Keys.E:
                    E = true;
                    break;
                case Keys.Escape:
                    if (Menu.Enabled) {
                        if (maximized)
                            Minimize();
                        else
                            Maximize();
                    } else {
                        Pauza();
                    }
                    break;
                case Keys.K:
                    if (!tutorial && !versionForRelease) {
                        foreach (Enemy enemy in enemyArray) {
                            if (enemy.type != Enemy.enemyType.Stark)
                                enemy.Dispose();
                        }
                    }
                    break;
                case Keys.L:
                    if (!versionForRelease)
                        cheatHealth = !cheatHealth;
                    break;
                case Keys.P:
                    if (!versionForRelease) {
                        if (stark != null)
                            stark.health = 40;
                        changedPhase = false;
                    }
                    break;
                case Keys.O:
                    if (!versionForRelease) {
                        if (stark != null)
                            stark.health = 20;
                        changedPhase = false;
                    }
                    break;
                case Keys.I:
                    if (!versionForRelease) {
                        if (stark != null) {
                            stark.TakeDamage(2, GameScene);
                        }
                    }
                    break;
                case Keys.F3:
                    lbStats.Visible = !lbStats.Visible;
                    break;
                case Keys.F:
                    winAnimation = true;
                    break;
            }
            if (e.KeyCode == Keys.T && e.Modifiers == Keys.Control) {
                MessageBox.Show("Nekone�no �ivot� aktivov�no!\nAchievementy deaktivov�ny.\nPro ukon�en� restartujte aplikaci.", "Cheat Mode", MessageBoxButtons.OK);
                cheatHealth = true;
            }
        } else if (e.KeyCode == Keys.R) {
            reseting = true;
            FullReset();
            ResetTimer.Start();
        }
    }

    private void MainWindow_KeyUp(object sender, KeyEventArgs e) {
        //Input - uvolneni klavesy
        switch (e.KeyCode) {
            case Keys.A or Keys.Left:
                A = false;
                break;
            case Keys.D or Keys.Right:
                D = false;
                break;
            case Keys.Space:
                Space = false;
                break;
            case Keys.Q:
                Q = false;
                break;
            case Keys.E:
                E = false;
                break;
            case Keys.F:
                winAnimation = false;
                break;
        }
    }

    private void btPlay_Click(object sender, EventArgs e) {
        difficultySelect.Visible = !difficultySelect.Visible;
        soundSelect.PlaySound();
        Focus();
    }

    private void btOptions_Click(object sender, EventArgs e) {
        Menu.Visible = false;
        Menu.Enabled = false;
        Pause.Enabled = true;
        Pause.Visible = true;
        panelPauza.Visible = false;
        if (enLanguage)
            lbNazev.Text = "Controls";
        else
            lbNazev.Text = "Ovl�d�n�";
        lbNazev.Left = 600;
        Focus();
        soundSelect.PlaySound();
    }
    private void btMenu_Click(object sender, EventArgs e) {
        Pause.Enabled = false;
        Pause.Visible = false;
        Menu.Visible = true;
        Menu.Enabled = true;
        Focus();
        soundSelect.PlaySound();

        if (tutorial) {
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
    private void btDifficulty(object sender, EventArgs e) {
        soundSelect.PlaySound();
        Button diff = sender as Button;
        if (diff.Name == "btEasy")
            difficulty = "Easy";
        if (diff.Name == "btNormal")
            difficulty = "Normal";
        if (diff.Name == "btHard")
            difficulty = "Hard";
        if (diff.Name == "btInsane") {
            difficulty = "Insane";
            playerHealth = 1;
        } else
            playerHealth = 5;

        // reset TimerHandler
        tOberhofnerova = false;
        tHacek = false;
        tJumpCooldown = false;
        tDMGCooldown = false;
        tNuggetDisappear = false;
        tStark = false;
        basnickaPlaying = false;

        Menu.Visible = false;
        Menu.Enabled = false;
        GameScene.Enabled = true;
        GameScene.Visible = true;
        FullReset();
        btContinue.Enabled = true;
        continueGame = true;
        won = false;
        SaveFileWrite();
        difficultySelect.Visible = false;
        Focus();
        UpdateMethod.Start();
    }
    private void btContinue_Click(object sender, EventArgs e) {
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
    private void JumpCooldown_Tick(object sender, EventArgs e) {
        jumpCooldown = false;
    }

    private void Sound_Click(object sender, EventArgs e) {
        SoundManager.bannedSound = !SoundManager.bannedSound;
        if (SoundManager.bannedSound) {
            Sound.Image = Resources.sound_muted;
            soundShouldBeBanned = true;
        } else {
            Sound.Image = Resources.sound;
            soundSelect.PlaySound();
            soundShouldBeBanned = false;
        }
    }
    private void btExit_Click(object sender, EventArgs e) {
        Application.Exit();
    }

    private void MainWindow_Load(object sender, EventArgs e) {
        UpdateProgress();
        CheckPS();
    }

    private void btResetProgress_Click(object sender, EventArgs e) {
        string filePath = Path.Combine(folderPath, fileName);
        using (StreamWriter writer = File.CreateText(filePath)) {
            writer.Write("false\n0\n0\nfalse\nfalse");
        }
        UpdateProgress();
    }
    private void btTutorial_Click(object sender, EventArgs e) {
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
        writeInstructions = true;
        tutorialPhase = 0;
        tutorial = true;
        btContinue.Enabled = false;
        InstrukceTimer.Start();
        soundSelect.PlaySound();
    }

    private void btFindController_Click(object sender, EventArgs e) {
        CheckPS();
    }

    private void ResetTimer_Tick(object sender, EventArgs e) {
        reseting = false;
        ResetTimer.Stop();
    }
    private void LoadingScreenTimer_Tick(object? sender, EventArgs e) {
        loadingScreen.Parent?.Controls.Remove(loadingScreen);
        loadingScreen.Dispose();
        loadingScreenTimer.Stop();
        loadingScreenTimer.Tick -= LoadingScreenTimer_Tick;
        loadingScreenTimer.Dispose();
    }
    private void WinScreenTimer_Tick(object? sender, EventArgs e) {
        if (winIndex == 0) {
            winScreen = new PictureBox() {
                Left = 0,
                Top = 0,
                Width = this.Width,
                Height = this.Height,
                Image = Resources.Victory,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            this.Controls.Add(winScreen);
            winScreen.BringToFront();

            SoundManager.bannedSound = true;

            winScreenTimer.Interval = 10000;
            winIndex++;
        } else {
            winScreen.Parent?.Controls.Remove(winScreen);
            winScreen.Dispose();

            if (!soundShouldBeBanned)
                SoundManager.bannedSound = false;

            disableAllInputs = false;
            winAnimation = false;
            FullReset();
            UpdateMethod.Stop();
            GameScene.Visible = false;
            GameScene.Enabled = false;
            Menu.Visible = true;
            Menu.Enabled = true;
            Focus();

            winScreenTimer.Stop();
            winScreenTimer.Tick -= WinScreenTimer_Tick;
            winScreenTimer.Dispose();
        }
    }

    private void pbLanguage_Click(object sender, EventArgs e) {
        enLanguage = !enLanguage;

        if (enLanguage) {
            pbLanguage.Image = Resources.Czech;

            poleInstrukci = new string[] { "Welcome to this school!" , "Move using A and D", "Great! Now try to jump using Space.", "Be carefull, a lot of things wants to stop you from succeding at exams!", 
                "And there is a lot of those.", "Try to attck using LMB", "When you point over Player, you attack up.", "For other directions, move mouse down and into side.", 
                "Lets introduce you to teachers now.", "This is Mr. Lemka", "He will follow you, once you are over his height.", "TIP: Watch out for his mouse!", "You are lucky you can't lose health for now.", "If you would like to get out of nasty situation, use E",
                "This is Mrs. Sysalov�", "Unfortunatelly, Deutch is not your favorite language.", "So try to deal with her as fast as possible!", "You can try to dive into enemy if you are over them using Q.", "This deals double the damage!", 
                "You can try that on Mr. Hacek.", "Just be carefull about his guided twitter.", "TIP: You can destroy it with your ruler.", "Last is Mrs. Oberhofnerova.", "She will definitely be your biggest problem.", "TIP: Try not to catch her stuff <3", "That's it, Good luck!"};

            label22.Text = "Choose difficulty";
            btEasy.Text = "Teacher's pet";
            btNormal.Text = "Average student";
            btHard.Text = "Slacker";
            btInsane.Text = "Truant";
            lbZaskolak.Text = "Complete game for unlocking this difficulty.";
            label30.Text = " Arts - Jakub Svoboda (@j4c.k0b)";
            label21.Text = "Don't you have homework? Does not matter. will you be late\r\nCool. Did you mess something up? It happens.";
            label24.Text = "Sometimes you mess up, but otherwise you understand most of it\r\nand you don't have major problems with your studies.";
            label26.Text = "You don't study at all and just somehow hope that you'll pass\r\nto the next year and you will finish your studies.";
            label28.Text = "You start with conditional exclusion, so wrong\r\nlook and you are out.";
            label23.Text = "5 health, 1HP recovery after each level, nuggets are allowed";
            label25.Text = "5 health, nuggets are allowed";
            label27.Text = "5 health";
            label29.Text = "1 health";
            label1.Text = "Graduation Journey";
            btContinue.Text = "Continue";
            btPlay.Text = "New Game";
            btOptions.Text = "Controls";
            btExit.Text = "Quit Game";
            lbRozvrh1.Text = "Unexpected change in schedule!";
            lbRozvrh2.Text = "Unexpected change in schedule!";
            btResetProgress.Text = "Reset Achievements";
            btFindController.Text = "Search controller";
            btMenu.Text = "Back to menu";
            label17.Text = "Pause/Continue";
            lbNazev.Text = "Paused";
            label3.Text = "Movement";
            label8.Text = "Jump";
            label11.Text = "Attack";
            label6.Text = "Space (A)";
            label9.Text = "LMB (X)";
            label14.Text = "Dive";
        } else {
            pbLanguage.Image = Resources.English;

            poleInstrukci = new string[] { "V�tej na �kole SP� Ostrov!", "Pohybuj se pomoc� kl�ves A a D", "Skv�le! Zkus si vysko�it pomoc� mezern�ku.", "Dobr� pr�ce, ale bacha, mnoha v�c� ti bude br�nit v jednoduch� maturit�.",
    "A to u� tak jednoduch� nebude...", "Zkus si bouchnout prav�tkem pomoc� lev�ho tla��tka my�i.","Kdy� m��� kurzorem nad hr��e, bouch� nahoru.", "Pro �tok doleva �i doprava, p�esu� kurzor do pat�i�n� strany.",
    "Nyn� ti p�edstav�m u�itele. Za�neme s panem u�itelem Lemkou.", "Jakmile jsi nad jeho �rovn�, za�ne t� sledovat.", "TIP: D�vej bacha na jeho kuli�kovou my�!", "M� �t�st�, �e zat�m nem��e� ztr�cet �ivoty.", "Kdybys cht�l n�jak� rychl� �t�k, zkus zm��kout E.",
    "Pokra�ujme nyn� s pan� u�itelkou Sysalovou.", "Bohu�el n�m�ina nen� tv�j obl�ben� jazyk.", "A proto se j� pokus zastavit co nejd��ve :)", "Kdy� jsi ve vzduchu nad u�itelem, m��e� na n�j nam��it my�� a zm��knout Q.", "Tento v�pad d�v� dvojn�sobn� po�kozen�!",
    "M��e� si to vyzkou�et na panu H��kovi.", "Jenom d�vej bacha na nav�d�n� twitter!", "TIP: Prav�tkem se twitteru zbav�", "A jako posledn� je tu pan� u�itelka Oberhofnerova", "Jako fl�ka� u n� rozhodn� neprojde�!", "TIP: Zkus se nenechat trefit <3" , "To bude ode m� v�echno, hodn� �t�st�!"};

            label22.Text = "Vyber si obt�nost";
            btEasy.Text = "U�itel� mil��ek";
            btNormal.Text = "Pr�m�rn� student";
            btHard.Text = "Fl�ka�";
            btInsane.Text = "Z�kol�k";
            lbZaskolak.Text = "Pro zp��stupn�n� obt�nosti Z�kol�k, dokon�i hru.";
            label30.Text = "Arty - Jakub Svoboda (@j4c.k0b)";
            label21.Text = "Nem� dom�c� �kol? Nevad�. P�ijde� pozd�?\r\nV pohod�. N�co si pokazil? St�v� se.";
            label24.Text = "Ob�as n�co pokaz�, ale jinak v�t�inu ch�pe�\r\na nem� v�t�� probl�my se studiem.";
            label26.Text = "Na studium ka�le� a prost� n�jak douf�, �e proleze�\r\ndo dal��ho ro�n�ku a dokon�� studium.";
            label28.Text = "Za��n� s podm�n�n�m vylou�en�m, tud� �patn�\r\nse na n�koho pod�v� a takzvan� let�.";
            label23.Text = "5 �ivot�, obnova 1HP po ka�d�m levelu,nugetky jsou povolen�";
            label25.Text = "5 �ivot�, nugetky jsou povolen�";
            label27.Text = "5 �ivot�";
            label29.Text = "1 �ivot";
            label1.Text = "Cesta k Maturit�";
            btContinue.Text = "Pokra�ovat";
            btPlay.Text = "Nov� hra";
            btOptions.Text = "Ovl�d�n�";
            btExit.Text = "Opustit hru";
            lbRozvrh1.Text = "Ne�ekan� zm�na v rozvrhu!";
            lbRozvrh2.Text = "Ne�ekan� zm�na v rozvrhu!";
            btResetProgress.Text = "Reset �sp�ch�";
            btFindController.Text = "Vyhledat ovlada�e";
            btMenu.Text = "Zp�tky do menu";
            label17.Text = "Pauza/Pokra�ovat";
            lbNazev.Text = "Pauza";
            label3.Text = "Pohyb";
            label8.Text = "Skok";
            label11.Text = "�tok";
            label6.Text = "Mezern�k (A)";
            label9.Text = "LTM (X)";
            label14.Text = "V�pad";
        }
    }
}