namespace Petr_RP_CestaKMaturite
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            GameScene = new Panel();
            lbRozvrh2 = new Label();
            lbRozvrh1 = new Label();
            StarkHealth = new PictureBox();
            StarkHealthBackground = new PictureBox();
            health5 = new PictureBox();
            health4 = new PictureBox();
            health3 = new PictureBox();
            health2 = new PictureBox();
            lbLevel = new Label();
            health1 = new PictureBox();
            lbPozastaveno = new Label();
            lbStats = new Label();
            Player = new PictureBox();
            lbPress = new Label();
            lbGameOver = new Label();
            Menu = new Panel();
            pbHardestDifficulty = new PictureBox();
            pbCompletedGame = new PictureBox();
            label30 = new Label();
            difficultySelect = new Panel();
            lbZaskolak = new Label();
            label29 = new Label();
            label28 = new Label();
            btInsane = new Button();
            label27 = new Label();
            label26 = new Label();
            btHard = new Button();
            label25 = new Label();
            label24 = new Label();
            label23 = new Label();
            btNormal = new Button();
            label21 = new Label();
            btEasy = new Button();
            label22 = new Label();
            btContinue = new Button();
            label2 = new Label();
            btExit = new Button();
            btOptions = new Button();
            label1 = new Label();
            btPlay = new Button();
            UpdateMethod = new System.Windows.Forms.Timer(components);
            AbilityQ = new System.Windows.Forms.Timer(components);
            Pause = new Panel();
            label31 = new Label();
            Sound = new PictureBox();
            panelPauza = new Panel();
            label17 = new Label();
            label16 = new Label();
            label15 = new Label();
            label18 = new Label();
            label19 = new Label();
            label20 = new Label();
            btMenu = new Button();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            lbNazev = new Label();
            Dash = new System.Windows.Forms.Timer(components);
            abilityLMB = new System.Windows.Forms.Timer(components);
            DMGcooldown = new System.Windows.Forms.Timer(components);
            Absence1 = new System.Windows.Forms.Timer(components);
            Absence2 = new System.Windows.Forms.Timer(components);
            Oberhofnerova = new System.Windows.Forms.Timer(components);
            Hacek = new System.Windows.Forms.Timer(components);
            NuggetDisappear = new System.Windows.Forms.Timer(components);
            Lemka = new System.Windows.Forms.Timer(components);
            Stark = new System.Windows.Forms.Timer(components);
            JumpCooldown = new System.Windows.Forms.Timer(components);
            btResetProgress = new Button();
            GameScene.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)StarkHealth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)StarkHealthBackground).BeginInit();
            ((System.ComponentModel.ISupportInitialize)health5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)health4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)health3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)health2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)health1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Player).BeginInit();
            Menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbHardestDifficulty).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbCompletedGame).BeginInit();
            difficultySelect.SuspendLayout();
            Pause.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Sound).BeginInit();
            panelPauza.SuspendLayout();
            SuspendLayout();
            // 
            // GameScene
            // 
            GameScene.BackColor = SystemColors.ActiveCaption;
            GameScene.Controls.Add(lbRozvrh2);
            GameScene.Controls.Add(lbRozvrh1);
            GameScene.Controls.Add(StarkHealth);
            GameScene.Controls.Add(StarkHealthBackground);
            GameScene.Controls.Add(health5);
            GameScene.Controls.Add(health4);
            GameScene.Controls.Add(health3);
            GameScene.Controls.Add(health2);
            GameScene.Controls.Add(lbLevel);
            GameScene.Controls.Add(health1);
            GameScene.Controls.Add(lbPozastaveno);
            GameScene.Controls.Add(lbStats);
            GameScene.Controls.Add(Player);
            GameScene.Enabled = false;
            GameScene.Location = new Point(0, 0);
            GameScene.Name = "GameScene";
            GameScene.Size = new Size(1520, 825);
            GameScene.TabIndex = 3;
            GameScene.Visible = false;
            // 
            // lbRozvrh2
            // 
            lbRozvrh2.AutoSize = true;
            lbRozvrh2.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            lbRozvrh2.ForeColor = Color.Red;
            lbRozvrh2.Location = new Point(1210, 139);
            lbRozvrh2.Name = "lbRozvrh2";
            lbRozvrh2.Size = new Size(159, 56);
            lbRozvrh2.TabIndex = 21;
            lbRozvrh2.Text = "Nečekaná změna\r\n        v rozvrhu!";
            lbRozvrh2.Visible = false;
            // 
            // lbRozvrh1
            // 
            lbRozvrh1.AutoSize = true;
            lbRozvrh1.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            lbRozvrh1.ForeColor = Color.Red;
            lbRozvrh1.Location = new Point(139, 139);
            lbRozvrh1.Name = "lbRozvrh1";
            lbRozvrh1.Size = new Size(159, 56);
            lbRozvrh1.TabIndex = 18;
            lbRozvrh1.Text = "Nečekaná změna\r\n        v rozvrhu!";
            lbRozvrh1.Visible = false;
            // 
            // StarkHealth
            // 
            StarkHealth.BackColor = Color.Red;
            StarkHealth.Location = new Point(651, 25);
            StarkHealth.Name = "StarkHealth";
            StarkHealth.Size = new Size(600, 50);
            StarkHealth.TabIndex = 15;
            StarkHealth.TabStop = false;
            StarkHealth.Visible = false;
            // 
            // StarkHealthBackground
            // 
            StarkHealthBackground.BackColor = Color.Black;
            StarkHealthBackground.Location = new Point(651, 25);
            StarkHealthBackground.Name = "StarkHealthBackground";
            StarkHealthBackground.Size = new Size(600, 50);
            StarkHealthBackground.TabIndex = 14;
            StarkHealthBackground.TabStop = false;
            StarkHealthBackground.Visible = false;
            // 
            // health5
            // 
            health5.BackgroundImageLayout = ImageLayout.Stretch;
            health5.Location = new Point(366, 12);
            health5.Name = "health5";
            health5.Size = new Size(65, 70);
            health5.TabIndex = 13;
            health5.TabStop = false;
            // 
            // health4
            // 
            health4.BackgroundImageLayout = ImageLayout.Stretch;
            health4.Location = new Point(295, 12);
            health4.Name = "health4";
            health4.Size = new Size(65, 70);
            health4.TabIndex = 12;
            health4.TabStop = false;
            // 
            // health3
            // 
            health3.BackgroundImageLayout = ImageLayout.Stretch;
            health3.Location = new Point(224, 12);
            health3.Name = "health3";
            health3.Size = new Size(65, 70);
            health3.TabIndex = 11;
            health3.TabStop = false;
            // 
            // health2
            // 
            health2.BackgroundImageLayout = ImageLayout.Stretch;
            health2.Location = new Point(153, 12);
            health2.Name = "health2";
            health2.Size = new Size(65, 70);
            health2.TabIndex = 10;
            health2.TabStop = false;
            // 
            // lbLevel
            // 
            lbLevel.AutoSize = true;
            lbLevel.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbLevel.Location = new Point(15, 28);
            lbLevel.Name = "lbLevel";
            lbLevel.Size = new Size(46, 30);
            lbLevel.TabIndex = 9;
            lbLevel.Text = "1/5";
            // 
            // health1
            // 
            health1.BackgroundImageLayout = ImageLayout.Stretch;
            health1.Location = new Point(83, 12);
            health1.Name = "health1";
            health1.Size = new Size(65, 70);
            health1.TabIndex = 8;
            health1.TabStop = false;
            // 
            // lbPozastaveno
            // 
            lbPozastaveno.AutoSize = true;
            lbPozastaveno.BackColor = SystemColors.ActiveCaption;
            lbPozastaveno.Font = new Font("Segoe UI", 25F, FontStyle.Regular, GraphicsUnit.Point);
            lbPozastaveno.Location = new Point(441, -203);
            lbPozastaveno.Name = "lbPozastaveno";
            lbPozastaveno.Size = new Size(578, 46);
            lbPozastaveno.TabIndex = 7;
            lbPozastaveno.Text = "Pozastaveno - Pro spuštění zmáčkni F";
            // 
            // lbStats
            // 
            lbStats.AutoSize = true;
            lbStats.BackColor = SystemColors.InfoText;
            lbStats.Font = new Font("Cascadia Code", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            lbStats.ForeColor = Color.Lime;
            lbStats.Location = new Point(12, 91);
            lbStats.Name = "lbStats";
            lbStats.Padding = new Padding(2, 2, 5, 5);
            lbStats.Size = new Size(79, 24);
            lbStats.TabIndex = 6;
            lbStats.Text = "F3 Stats";
            lbStats.Visible = false;
            // 
            // Player
            // 
            Player.Anchor = AnchorStyles.None;
            Player.BackColor = Color.Chocolate;
            Player.Location = new Point(731, 377);
            Player.Name = "Player";
            Player.Size = new Size(75, 115);
            Player.TabIndex = 1;
            Player.TabStop = false;
            Player.Tag = "Player";
            // 
            // lbPress
            // 
            lbPress.AutoSize = true;
            lbPress.BackColor = Color.Transparent;
            lbPress.Font = new Font("Segoe UI", 30F, FontStyle.Bold, GraphicsUnit.Point);
            lbPress.ForeColor = Color.Red;
            lbPress.Location = new Point(586, -200);
            lbPress.Name = "lbPress";
            lbPress.Size = new Size(397, 108);
            lbPress.TabIndex = 16;
            lbPress.Text = "Zkus to za rok\r\n       nebo zmáčkni R";
            // 
            // lbGameOver
            // 
            lbGameOver.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lbGameOver.AutoSize = true;
            lbGameOver.BackColor = Color.Transparent;
            lbGameOver.Font = new Font("Segoe UI", 50F, FontStyle.Bold, GraphicsUnit.Point);
            lbGameOver.Location = new Point(450, -200);
            lbGameOver.Name = "lbGameOver";
            lbGameOver.Size = new Size(619, 89);
            lbGameOver.TabIndex = 15;
            lbGameOver.Text = "Neodmaturoval si!";
            // 
            // Menu
            // 
            Menu.BackColor = SystemColors.ActiveCaption;
            Menu.BackgroundImageLayout = ImageLayout.Stretch;
            Menu.Controls.Add(pbHardestDifficulty);
            Menu.Controls.Add(pbCompletedGame);
            Menu.Controls.Add(label30);
            Menu.Controls.Add(difficultySelect);
            Menu.Controls.Add(btContinue);
            Menu.Controls.Add(label2);
            Menu.Controls.Add(btExit);
            Menu.Controls.Add(btOptions);
            Menu.Controls.Add(label1);
            Menu.Controls.Add(btPlay);
            Menu.Location = new Point(0, 0);
            Menu.Name = "Menu";
            Menu.Size = new Size(1520, 825);
            Menu.TabIndex = 14;
            // 
            // pbHardestDifficulty
            // 
            pbHardestDifficulty.BackColor = Color.Maroon;
            pbHardestDifficulty.Location = new Point(83, 15);
            pbHardestDifficulty.Name = "pbHardestDifficulty";
            pbHardestDifficulty.Size = new Size(50, 50);
            pbHardestDifficulty.TabIndex = 9;
            pbHardestDifficulty.TabStop = false;
            pbHardestDifficulty.Visible = false;
            // 
            // pbCompletedGame
            // 
            pbCompletedGame.BackColor = Color.GreenYellow;
            pbCompletedGame.Location = new Point(15, 15);
            pbCompletedGame.Name = "pbCompletedGame";
            pbCompletedGame.Size = new Size(50, 50);
            pbCompletedGame.TabIndex = 8;
            pbCompletedGame.TabStop = false;
            pbCompletedGame.Visible = false;
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.BackColor = Color.Transparent;
            label30.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point);
            label30.ForeColor = Color.White;
            label30.Location = new Point(44, 788);
            label30.Name = "label30";
            label30.Size = new Size(323, 28);
            label30.TabIndex = 7;
            label30.Text = "Arty - Jakub Svoboda (@j4c.k0b)";
            // 
            // difficultySelect
            // 
            difficultySelect.Controls.Add(lbZaskolak);
            difficultySelect.Controls.Add(label29);
            difficultySelect.Controls.Add(label28);
            difficultySelect.Controls.Add(btInsane);
            difficultySelect.Controls.Add(label27);
            difficultySelect.Controls.Add(label26);
            difficultySelect.Controls.Add(btHard);
            difficultySelect.Controls.Add(label25);
            difficultySelect.Controls.Add(label24);
            difficultySelect.Controls.Add(label23);
            difficultySelect.Controls.Add(btNormal);
            difficultySelect.Controls.Add(label21);
            difficultySelect.Controls.Add(btEasy);
            difficultySelect.Controls.Add(label22);
            difficultySelect.Location = new Point(68, 165);
            difficultySelect.Name = "difficultySelect";
            difficultySelect.Size = new Size(738, 567);
            difficultySelect.TabIndex = 6;
            difficultySelect.Visible = false;
            // 
            // lbZaskolak
            // 
            lbZaskolak.AutoSize = true;
            lbZaskolak.BackColor = SystemColors.ActiveCaption;
            lbZaskolak.ForeColor = Color.Blue;
            lbZaskolak.Location = new Point(29, 535);
            lbZaskolak.Name = "lbZaskolak";
            lbZaskolak.Size = new Size(270, 15);
            lbZaskolak.TabIndex = 15;
            lbZaskolak.Text = "Pro zpřístupnění obtížnosti Záškolák, dokonči hru.";
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.ForeColor = Color.Red;
            label29.Location = new Point(653, 520);
            label29.Name = "label29";
            label29.Size = new Size(41, 15);
            label29.TabIndex = 14;
            label29.Text = "1 život";
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Font = new Font("Segoe UI", 15.25F, FontStyle.Bold, GraphicsUnit.Point);
            label28.Location = new Point(195, 458);
            label28.Name = "label28";
            label28.Size = new Size(490, 90);
            label28.TabIndex = 13;
            label28.Text = "Začínáš s podmíněným vyloučením, tudíž špatně\r\nse na někoho podíváš a takzvaně letíš.\r\n\r\n";
            // 
            // btInsane
            // 
            btInsane.BackColor = Color.Crimson;
            btInsane.Enabled = false;
            btInsane.Location = new Point(29, 458);
            btInsane.Name = "btInsane";
            btInsane.Size = new Size(138, 40);
            btInsane.TabIndex = 12;
            btInsane.Text = "Záškolák";
            btInsane.UseVisualStyleBackColor = false;
            btInsane.Click += btDifficulty;
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.ForeColor = Color.Red;
            label27.Location = new Point(646, 414);
            label27.Name = "label27";
            label27.Size = new Size(48, 15);
            label27.TabIndex = 11;
            label27.Text = "5 životů";
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Font = new Font("Segoe UI", 15.25F, FontStyle.Bold, GraphicsUnit.Point);
            label26.Location = new Point(195, 335);
            label26.Name = "label26";
            label26.Size = new Size(529, 60);
            label26.TabIndex = 10;
            label26.Text = "Na studium kašleš a prostě nějak doufáš, že prolezeš\r\ndo dalšího ročníku a dokončíš studium.\r\n";
            // 
            // btHard
            // 
            btHard.BackColor = Color.DarkKhaki;
            btHard.Location = new Point(29, 335);
            btHard.Name = "btHard";
            btHard.Size = new Size(138, 40);
            btHard.TabIndex = 9;
            btHard.Text = "Flákač";
            btHard.UseVisualStyleBackColor = false;
            btHard.Click += btDifficulty;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.ForeColor = Color.Red;
            label25.Location = new Point(523, 293);
            label25.Name = "label25";
            label25.Size = new Size(171, 15);
            label25.TabIndex = 8;
            label25.Text = "5 životů,nugetky jsou povolené";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Font = new Font("Segoe UI", 15.25F, FontStyle.Bold, GraphicsUnit.Point);
            label24.Location = new Point(195, 212);
            label24.Name = "label24";
            label24.Size = new Size(449, 60);
            label24.TabIndex = 7;
            label24.Text = "Občas něco pokazíš, ale jinak většinu chápeš\r\na nemáš větší problémy se studiem.";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.ForeColor = Color.Red;
            label23.Location = new Point(357, 175);
            label23.Name = "label23";
            label23.Size = new Size(337, 15);
            label23.TabIndex = 6;
            label23.Text = "5 životů, obnova 1HP po každém levelu,nugetky jsou povolené";
            // 
            // btNormal
            // 
            btNormal.BackColor = Color.Yellow;
            btNormal.Location = new Point(29, 212);
            btNormal.Name = "btNormal";
            btNormal.Size = new Size(138, 40);
            btNormal.TabIndex = 5;
            btNormal.Text = "Průměrný student";
            btNormal.UseVisualStyleBackColor = false;
            btNormal.Click += btDifficulty;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Font = new Font("Segoe UI", 15.25F, FontStyle.Bold, GraphicsUnit.Point);
            label21.Location = new Point(202, 89);
            label21.Name = "label21";
            label21.Size = new Size(445, 60);
            label21.TabIndex = 4;
            label21.Text = "Nemáš domácí úkol? Nevadí. Přijdeš pozdě?\r\nV pohodě. Něco si pokazil? Stává se.";
            // 
            // btEasy
            // 
            btEasy.BackColor = Color.GreenYellow;
            btEasy.Location = new Point(29, 89);
            btEasy.Name = "btEasy";
            btEasy.Size = new Size(138, 40);
            btEasy.TabIndex = 3;
            btEasy.Text = "Učitelů miláček";
            btEasy.UseVisualStyleBackColor = false;
            btEasy.Click += btDifficulty;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.BackColor = Color.Transparent;
            label22.Font = new Font("Segoe UI", 30F, FontStyle.Bold, GraphicsUnit.Point);
            label22.ForeColor = Color.White;
            label22.Location = new Point(17, 11);
            label22.Name = "label22";
            label22.Size = new Size(363, 54);
            label22.TabIndex = 2;
            label22.Text = "Vyber si obtížnost";
            // 
            // btContinue
            // 
            btContinue.BackColor = Color.IndianRed;
            btContinue.Enabled = false;
            btContinue.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            btContinue.Location = new Point(980, 246);
            btContinue.Name = "btContinue";
            btContinue.Size = new Size(248, 65);
            btContinue.TabIndex = 5;
            btContinue.TabStop = false;
            btContinue.Text = "Pokračovat";
            btContinue.UseVisualStyleBackColor = false;
            btContinue.Click += btContinue_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.White;
            label2.Location = new Point(1281, 788);
            label2.Name = "label2";
            label2.Size = new Size(172, 28);
            label2.TabIndex = 4;
            label2.Text = "© RP - Petr 2023";
            // 
            // btExit
            // 
            btExit.BackColor = Color.RosyBrown;
            btExit.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            btExit.Location = new Point(980, 550);
            btExit.Name = "btExit";
            btExit.Size = new Size(248, 65);
            btExit.TabIndex = 3;
            btExit.TabStop = false;
            btExit.Text = "Opustit hru";
            btExit.UseVisualStyleBackColor = false;
            btExit.Click += btExit_Click;
            // 
            // btOptions
            // 
            btOptions.BackColor = Color.RosyBrown;
            btOptions.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            btOptions.Location = new Point(980, 454);
            btOptions.Name = "btOptions";
            btOptions.Size = new Size(248, 65);
            btOptions.TabIndex = 2;
            btOptions.TabStop = false;
            btOptions.Text = "Ovládání";
            btOptions.UseVisualStyleBackColor = false;
            btOptions.Click += btOptions_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 30F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.White;
            label1.Location = new Point(869, 91);
            label1.Name = "label1";
            label1.Size = new Size(335, 54);
            label1.TabIndex = 1;
            label1.Text = "Cesta k Maturitě";
            // 
            // btPlay
            // 
            btPlay.BackColor = Color.RosyBrown;
            btPlay.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            btPlay.Location = new Point(980, 358);
            btPlay.Name = "btPlay";
            btPlay.Size = new Size(248, 65);
            btPlay.TabIndex = 0;
            btPlay.TabStop = false;
            btPlay.Text = "Nová hra";
            btPlay.UseVisualStyleBackColor = false;
            btPlay.Click += btPlay_Click;
            // 
            // UpdateMethod
            // 
            UpdateMethod.Interval = 1;
            UpdateMethod.Tick += UpdateMethod_Tick;
            // 
            // AbilityQ
            // 
            AbilityQ.Interval = 1000;
            AbilityQ.Tick += AbilityQ_Tick;
            // 
            // Pause
            // 
            Pause.BackColor = SystemColors.ActiveCaption;
            Pause.Controls.Add(btResetProgress);
            Pause.Controls.Add(label31);
            Pause.Controls.Add(Sound);
            Pause.Controls.Add(panelPauza);
            Pause.Controls.Add(label18);
            Pause.Controls.Add(label19);
            Pause.Controls.Add(label20);
            Pause.Controls.Add(btMenu);
            Pause.Controls.Add(label12);
            Pause.Controls.Add(label13);
            Pause.Controls.Add(label14);
            Pause.Controls.Add(label9);
            Pause.Controls.Add(label10);
            Pause.Controls.Add(label11);
            Pause.Controls.Add(label6);
            Pause.Controls.Add(label7);
            Pause.Controls.Add(label8);
            Pause.Controls.Add(label5);
            Pause.Controls.Add(label4);
            Pause.Controls.Add(label3);
            Pause.Controls.Add(lbNazev);
            Pause.Dock = DockStyle.Fill;
            Pause.Enabled = false;
            Pause.Location = new Point(0, 0);
            Pause.Name = "Pause";
            Pause.Size = new Size(1520, 825);
            Pause.TabIndex = 4;
            Pause.Visible = false;
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Location = new Point(1370, 97);
            label31.Name = "label31";
            label31.Size = new Size(138, 45);
            label31.TabIndex = 22;
            label31.Text = "Doporučuji nezapínat,\r\notravný a zpomaluje hru.\r\n                                      <3";
            // 
            // Sound
            // 
            Sound.BackColor = Color.Transparent;
            Sound.BackgroundImage = Properties.Resources.sound_muted;
            Sound.BackgroundImageLayout = ImageLayout.Stretch;
            Sound.Location = new Point(1435, 15);
            Sound.Name = "Sound";
            Sound.Size = new Size(70, 70);
            Sound.TabIndex = 8;
            Sound.TabStop = false;
            Sound.Click += Sound_Click;
            // 
            // panelPauza
            // 
            panelPauza.BackColor = Color.Transparent;
            panelPauza.Controls.Add(label17);
            panelPauza.Controls.Add(label16);
            panelPauza.Controls.Add(label15);
            panelPauza.Location = new Point(12, 12);
            panelPauza.Name = "panelPauza";
            panelPauza.Size = new Size(419, 100);
            panelPauza.TabIndex = 21;
            // 
            // label17
            // 
            label17.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label17.AutoSize = true;
            label17.BackColor = Color.Transparent;
            label17.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label17.ForeColor = Color.White;
            label17.Location = new Point(3, 26);
            label17.Name = "label17";
            label17.Size = new Size(249, 37);
            label17.TabIndex = 13;
            label17.Text = "Pauza/Pokračovat";
            // 
            // label16
            // 
            label16.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label16.AutoSize = true;
            label16.BackColor = Color.Transparent;
            label16.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label16.ForeColor = Color.White;
            label16.Location = new Point(258, 26);
            label16.Name = "label16";
            label16.Size = new Size(28, 37);
            label16.TabIndex = 14;
            label16.Text = "-";
            // 
            // label15
            // 
            label15.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label15.AutoSize = true;
            label15.BackColor = Color.Transparent;
            label15.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label15.ForeColor = Color.White;
            label15.Location = new Point(292, 26);
            label15.Name = "label15";
            label15.Size = new Size(103, 37);
            label15.TabIndex = 15;
            label15.Text = "Escape";
            // 
            // label18
            // 
            label18.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label18.AutoSize = true;
            label18.BackColor = Color.Transparent;
            label18.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label18.ForeColor = Color.White;
            label18.Location = new Point(869, 510);
            label18.Name = "label18";
            label18.Size = new Size(31, 37);
            label18.TabIndex = 20;
            label18.Text = "E";
            // 
            // label19
            // 
            label19.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label19.AutoSize = true;
            label19.BackColor = Color.Transparent;
            label19.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label19.ForeColor = Color.White;
            label19.Location = new Point(714, 510);
            label19.Name = "label19";
            label19.Size = new Size(28, 37);
            label19.TabIndex = 19;
            label19.Text = "-";
            // 
            // label20
            // 
            label20.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label20.AutoSize = true;
            label20.BackColor = Color.Transparent;
            label20.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label20.ForeColor = Color.White;
            label20.Location = new Point(536, 510);
            label20.Name = "label20";
            label20.Size = new Size(80, 37);
            label20.TabIndex = 18;
            label20.Text = "Dash";
            // 
            // btMenu
            // 
            btMenu.BackColor = Color.WhiteSmoke;
            btMenu.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            btMenu.Location = new Point(618, 623);
            btMenu.Name = "btMenu";
            btMenu.Size = new Size(242, 59);
            btMenu.TabIndex = 17;
            btMenu.TabStop = false;
            btMenu.Text = "Zpátky do menu";
            btMenu.UseVisualStyleBackColor = false;
            btMenu.Click += btMenu_Click;
            // 
            // label12
            // 
            label12.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label12.AutoSize = true;
            label12.BackColor = Color.Transparent;
            label12.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label12.ForeColor = Color.White;
            label12.Location = new Point(869, 440);
            label12.Name = "label12";
            label12.Size = new Size(37, 37);
            label12.TabIndex = 12;
            label12.Text = "Q";
            // 
            // label13
            // 
            label13.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label13.AutoSize = true;
            label13.BackColor = Color.Transparent;
            label13.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label13.ForeColor = Color.White;
            label13.Location = new Point(714, 440);
            label13.Name = "label13";
            label13.Size = new Size(28, 37);
            label13.TabIndex = 11;
            label13.Text = "-";
            // 
            // label14
            // 
            label14.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label14.AutoSize = true;
            label14.BackColor = Color.Transparent;
            label14.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label14.ForeColor = Color.White;
            label14.Location = new Point(536, 440);
            label14.Name = "label14";
            label14.Size = new Size(99, 37);
            label14.TabIndex = 10;
            label14.Text = "Výpad";
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label9.AutoSize = true;
            label9.BackColor = Color.Transparent;
            label9.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label9.ForeColor = Color.White;
            label9.Location = new Point(869, 377);
            label9.Name = "label9";
            label9.Size = new Size(71, 37);
            label9.TabIndex = 9;
            label9.Text = "LTM";
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label10.AutoSize = true;
            label10.BackColor = Color.Transparent;
            label10.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label10.ForeColor = Color.White;
            label10.Location = new Point(714, 377);
            label10.Name = "label10";
            label10.Size = new Size(28, 37);
            label10.TabIndex = 8;
            label10.Text = "-";
            // 
            // label11
            // 
            label11.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label11.AutoSize = true;
            label11.BackColor = Color.Transparent;
            label11.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label11.ForeColor = Color.White;
            label11.Location = new Point(536, 379);
            label11.Name = "label11";
            label11.Size = new Size(80, 37);
            label11.TabIndex = 7;
            label11.Text = "Útok";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = Color.White;
            label6.Location = new Point(869, 318);
            label6.Name = "label6";
            label6.Size = new Size(136, 37);
            label6.TabIndex = 6;
            label6.Text = "Mezerník";
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label7.ForeColor = Color.White;
            label7.Location = new Point(714, 318);
            label7.Name = "label7";
            label7.Size = new Size(28, 37);
            label7.TabIndex = 5;
            label7.Text = "-";
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label8.AutoSize = true;
            label8.BackColor = Color.Transparent;
            label8.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label8.ForeColor = Color.White;
            label8.Location = new Point(536, 318);
            label8.Name = "label8";
            label8.Size = new Size(79, 37);
            label8.TabIndex = 4;
            label8.Text = "Skok";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = Color.White;
            label5.Location = new Point(869, 257);
            label5.Name = "label5";
            label5.Size = new Size(64, 37);
            label5.TabIndex = 3;
            label5.Text = "A,D";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = Color.White;
            label4.Location = new Point(714, 257);
            label4.Name = "label4";
            label4.Size = new Size(28, 37);
            label4.TabIndex = 2;
            label4.Text = "-";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.White;
            label3.Location = new Point(536, 257);
            label3.Name = "label3";
            label3.Size = new Size(98, 37);
            label3.TabIndex = 1;
            label3.Text = "Pohyb";
            // 
            // lbNazev
            // 
            lbNazev.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lbNazev.AutoSize = true;
            lbNazev.BackColor = Color.Transparent;
            lbNazev.Font = new Font("Segoe UI", 40F, FontStyle.Bold, GraphicsUnit.Point);
            lbNazev.ForeColor = Color.White;
            lbNazev.Location = new Point(600, 73);
            lbNazev.Name = "lbNazev";
            lbNazev.Size = new Size(179, 72);
            lbNazev.TabIndex = 0;
            lbNazev.Text = "Pauza";
            // 
            // Dash
            // 
            Dash.Interval = 500;
            Dash.Tick += Dash_Tick;
            // 
            // abilityLMB
            // 
            abilityLMB.Interval = 20;
            abilityLMB.Tick += abilityLMB_Tick;
            // 
            // DMGcooldown
            // 
            DMGcooldown.Interval = 2000;
            DMGcooldown.Tick += DMGcooldown_Tick;
            // 
            // Absence1
            // 
            Absence1.Tick += Absence1_Tick;
            // 
            // Absence2
            // 
            Absence2.Tick += Absence2_Tick;
            // 
            // Oberhofnerova
            // 
            Oberhofnerova.Tick += Oberhofnerova_Tick;
            // 
            // Hacek
            // 
            Hacek.Tick += Hacek_Tick;
            // 
            // NuggetDisappear
            // 
            NuggetDisappear.Tick += NuggetDisappear_Tick;
            // 
            // Lemka
            // 
            Lemka.Tick += Lemka_Tick;
            // 
            // Stark
            // 
            Stark.Tick += Stark_Tick;
            // 
            // JumpCooldown
            // 
            JumpCooldown.Interval = 500;
            JumpCooldown.Tick += JumpCooldown_Tick;
            // 
            // btResetProgress
            // 
            btResetProgress.BackColor = Color.RosyBrown;
            btResetProgress.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            btResetProgress.Location = new Point(12, 779);
            btResetProgress.Name = "btResetProgress";
            btResetProgress.Size = new Size(108, 37);
            btResetProgress.TabIndex = 23;
            btResetProgress.TabStop = false;
            btResetProgress.Text = "Reset úspěchů";
            btResetProgress.UseVisualStyleBackColor = false;
            btResetProgress.Click += btResetProgress_Click;
            // 
            // MainWindow
            // 
            AutoScaleMode = AutoScaleMode.Inherit;
            ClientSize = new Size(1520, 825);
            Controls.Add(lbPress);
            Controls.Add(lbGameOver);
            Controls.Add(Pause);
            Controls.Add(Menu);
            Controls.Add(GameScene);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainWindow";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cesta k Maturitě";
            Load += MainWindow_Load;
            KeyDown += MainWindow_KeyDown;
            KeyUp += MainWindow_KeyUp;
            GameScene.ResumeLayout(false);
            GameScene.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)StarkHealth).EndInit();
            ((System.ComponentModel.ISupportInitialize)StarkHealthBackground).EndInit();
            ((System.ComponentModel.ISupportInitialize)health5).EndInit();
            ((System.ComponentModel.ISupportInitialize)health4).EndInit();
            ((System.ComponentModel.ISupportInitialize)health3).EndInit();
            ((System.ComponentModel.ISupportInitialize)health2).EndInit();
            ((System.ComponentModel.ISupportInitialize)health1).EndInit();
            ((System.ComponentModel.ISupportInitialize)Player).EndInit();
            Menu.ResumeLayout(false);
            Menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbHardestDifficulty).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbCompletedGame).EndInit();
            difficultySelect.ResumeLayout(false);
            difficultySelect.PerformLayout();
            Pause.ResumeLayout(false);
            Pause.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Sound).EndInit();
            panelPauza.ResumeLayout(false);
            panelPauza.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel GameScene;
        private Label lbStats;
        private PictureBox Player;
        private System.Windows.Forms.Timer UpdateMethod;
        private System.Windows.Forms.Timer AbilityQ;
        private Panel Menu;
        private Button btExit;
        private Button btOptions;
        private Label label1;
        private Button btPlay;
        private Panel Pause;
        private Label label15;
        private Label label16;
        private Label label17;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label lbNazev;
        private System.Windows.Forms.Timer Dash;
        private Label label18;
        private Label label19;
        private Label label20;
        private Button btMenu;
        private Panel panelPauza;
        private Label label2;
        private System.Windows.Forms.Timer abilityLMB;
        private System.Windows.Forms.Timer DMGcooldown;
        private Label lbGameOver;
        private Label lbPress;
        private Label lbPozastaveno;
        private System.Windows.Forms.Timer Absence1;
        private System.Windows.Forms.Timer Absence2;
        private System.Windows.Forms.Timer Oberhofnerova;
        private System.Windows.Forms.Timer Hacek;
        private System.Windows.Forms.Timer NuggetDisappear;
        private System.Windows.Forms.Timer Lemka;
        private System.Windows.Forms.Timer Stark;
        private Button btContinue;
        private Panel difficultySelect;
        private Label lbZaskolak;
        private Label label29;
        private Label label28;
        private Button btInsane;
        private Label label27;
        private Label label26;
        private Button btHard;
        private Label label25;
        private Label label24;
        private Label label23;
        private Button btNormal;
        private Label label21;
        private Button btEasy;
        private Label label22;
        private Label label30;
        private System.Windows.Forms.Timer JumpCooldown;
        private PictureBox Sound;
        private Label lbLevel;
        private PictureBox health1;
        private PictureBox health5;
        private PictureBox health4;
        private PictureBox health3;
        private PictureBox health2;
        private PictureBox StarkHealth;
        private PictureBox StarkHealthBackground;
        private Label lbRozvrh1;
        private Label lbRozvrh2;
        private PictureBox pbHardestDifficulty;
        private PictureBox pbCompletedGame;
        private Label label31;
        private Button btResetProgress;
    }
}