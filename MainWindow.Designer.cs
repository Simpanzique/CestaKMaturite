namespace Petr_RP_Silksong
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
            this.components = new System.ComponentModel.Container();
            this.GameScene = new System.Windows.Forms.GroupBox();
            this.lbPozastaveno = new System.Windows.Forms.Label();
            this.lbStats = new System.Windows.Forms.Label();
            this.Player = new System.Windows.Forms.PictureBox();
            this.lbPress = new System.Windows.Forms.Label();
            this.lbGameOver = new System.Windows.Forms.Label();
            this.Menu = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btExit = new System.Windows.Forms.Button();
            this.btOptions = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btPlay = new System.Windows.Forms.Button();
            this.UpdateMethod = new System.Windows.Forms.Timer(this.components);
            this.AbilityQ = new System.Windows.Forms.Timer(this.components);
            this.Pause = new System.Windows.Forms.Panel();
            this.panelPauza = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.btMenu = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbNazev = new System.Windows.Forms.Label();
            this.Dash = new System.Windows.Forms.Timer(this.components);
            this.abilityLMB = new System.Windows.Forms.Timer(this.components);
            this.DMGcooldown = new System.Windows.Forms.Timer(this.components);
            this.Absence1 = new System.Windows.Forms.Timer(this.components);
            this.Absence2 = new System.Windows.Forms.Timer(this.components);
            this.Oberhofnerova = new System.Windows.Forms.Timer(this.components);
            this.Hacek = new System.Windows.Forms.Timer(this.components);
            this.NuggetDisappear = new System.Windows.Forms.Timer(this.components);
            this.Lemka = new System.Windows.Forms.Timer(this.components);
            this.GameScene.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Player)).BeginInit();
            this.Menu.SuspendLayout();
            this.Pause.SuspendLayout();
            this.panelPauza.SuspendLayout();
            this.SuspendLayout();
            // 
            // GameScene
            // 
            this.GameScene.Controls.Add(this.lbPozastaveno);
            this.GameScene.Controls.Add(this.lbStats);
            this.GameScene.Controls.Add(this.Player);
            this.GameScene.Enabled = false;
            this.GameScene.Location = new System.Drawing.Point(0, 0);
            this.GameScene.Name = "GameScene";
            this.GameScene.Padding = new System.Windows.Forms.Padding(0);
            this.GameScene.Size = new System.Drawing.Size(1520, 825);
            this.GameScene.TabIndex = 3;
            this.GameScene.TabStop = false;
            this.GameScene.Visible = false;
            // 
            // lbPozastaveno
            // 
            this.lbPozastaveno.AutoSize = true;
            this.lbPozastaveno.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lbPozastaveno.Font = new System.Drawing.Font("Segoe UI", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbPozastaveno.Location = new System.Drawing.Point(441, -203);
            this.lbPozastaveno.Name = "lbPozastaveno";
            this.lbPozastaveno.Size = new System.Drawing.Size(578, 46);
            this.lbPozastaveno.TabIndex = 7;
            this.lbPozastaveno.Text = "Pozastaveno - Pro spuštění zmáčkni F";
            // 
            // lbStats
            // 
            this.lbStats.AutoSize = true;
            this.lbStats.BackColor = System.Drawing.SystemColors.InfoText;
            this.lbStats.Font = new System.Drawing.Font("Cascadia Code", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbStats.ForeColor = System.Drawing.Color.Lime;
            this.lbStats.Location = new System.Drawing.Point(9, 6);
            this.lbStats.Name = "lbStats";
            this.lbStats.Padding = new System.Windows.Forms.Padding(2, 2, 5, 5);
            this.lbStats.Size = new System.Drawing.Size(79, 24);
            this.lbStats.TabIndex = 6;
            this.lbStats.Text = "F3 Stats";
            // 
            // Player
            // 
            this.Player.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Player.BackColor = System.Drawing.Color.Chocolate;
            this.Player.Location = new System.Drawing.Point(508, 191);
            this.Player.Name = "Player";
            this.Player.Size = new System.Drawing.Size(75, 115);
            this.Player.TabIndex = 1;
            this.Player.TabStop = false;
            this.Player.Tag = "Player";
            // 
            // lbPress
            // 
            this.lbPress.AutoSize = true;
            this.lbPress.BackColor = System.Drawing.Color.Transparent;
            this.lbPress.Font = new System.Drawing.Font("Segoe UI", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbPress.ForeColor = System.Drawing.Color.Red;
            this.lbPress.Location = new System.Drawing.Point(586, -200);
            this.lbPress.Name = "lbPress";
            this.lbPress.Size = new System.Drawing.Size(397, 108);
            this.lbPress.TabIndex = 16;
            this.lbPress.Text = "Zkus to za rok\r\n       nebo zmáčkni R";
            // 
            // lbGameOver
            // 
            this.lbGameOver.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbGameOver.AutoSize = true;
            this.lbGameOver.BackColor = System.Drawing.Color.Transparent;
            this.lbGameOver.Font = new System.Drawing.Font("Segoe UI", 50F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbGameOver.Location = new System.Drawing.Point(450, -200);
            this.lbGameOver.Name = "lbGameOver";
            this.lbGameOver.Size = new System.Drawing.Size(619, 89);
            this.lbGameOver.TabIndex = 15;
            this.lbGameOver.Text = "Neodmaturoval si!";
            // 
            // Menu
            // 
            this.Menu.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Menu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Menu.Controls.Add(this.label2);
            this.Menu.Controls.Add(this.btExit);
            this.Menu.Controls.Add(this.btOptions);
            this.Menu.Controls.Add(this.label1);
            this.Menu.Controls.Add(this.btPlay);
            this.Menu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Menu.Location = new System.Drawing.Point(0, 0);
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(1520, 825);
            this.Menu.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(1281, 788);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(172, 28);
            this.label2.TabIndex = 4;
            this.label2.Text = "© RP - Petr 2023";
            // 
            // btExit
            // 
            this.btExit.BackColor = System.Drawing.Color.RosyBrown;
            this.btExit.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btExit.Location = new System.Drawing.Point(977, 499);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(248, 65);
            this.btExit.TabIndex = 3;
            this.btExit.TabStop = false;
            this.btExit.Text = "Exit";
            this.btExit.UseVisualStyleBackColor = false;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // btOptions
            // 
            this.btOptions.BackColor = System.Drawing.Color.RosyBrown;
            this.btOptions.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btOptions.Location = new System.Drawing.Point(977, 403);
            this.btOptions.Name = "btOptions";
            this.btOptions.Size = new System.Drawing.Size(248, 65);
            this.btOptions.TabIndex = 2;
            this.btOptions.TabStop = false;
            this.btOptions.Text = "Možnosti";
            this.btOptions.UseVisualStyleBackColor = false;
            this.btOptions.Click += new System.EventHandler(this.btOptions_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(869, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(335, 54);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cesta k Maturitě";
            // 
            // btPlay
            // 
            this.btPlay.BackColor = System.Drawing.Color.RosyBrown;
            this.btPlay.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btPlay.Location = new System.Drawing.Point(977, 307);
            this.btPlay.Name = "btPlay";
            this.btPlay.Size = new System.Drawing.Size(248, 65);
            this.btPlay.TabIndex = 0;
            this.btPlay.TabStop = false;
            this.btPlay.Text = "Hrát";
            this.btPlay.UseVisualStyleBackColor = false;
            this.btPlay.Click += new System.EventHandler(this.btPlay_Click);
            // 
            // UpdateMethod
            // 
            this.UpdateMethod.Interval = 1;
            this.UpdateMethod.Tick += new System.EventHandler(this.UpdateMethod_Tick);
            // 
            // AbilityQ
            // 
            this.AbilityQ.Interval = 1000;
            this.AbilityQ.Tick += new System.EventHandler(this.AbilityQ_Tick);
            // 
            // Pause
            // 
            this.Pause.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Pause.Controls.Add(this.panelPauza);
            this.Pause.Controls.Add(this.label18);
            this.Pause.Controls.Add(this.label19);
            this.Pause.Controls.Add(this.label20);
            this.Pause.Controls.Add(this.btMenu);
            this.Pause.Controls.Add(this.label12);
            this.Pause.Controls.Add(this.label13);
            this.Pause.Controls.Add(this.label14);
            this.Pause.Controls.Add(this.label9);
            this.Pause.Controls.Add(this.label10);
            this.Pause.Controls.Add(this.label11);
            this.Pause.Controls.Add(this.label6);
            this.Pause.Controls.Add(this.label7);
            this.Pause.Controls.Add(this.label8);
            this.Pause.Controls.Add(this.label5);
            this.Pause.Controls.Add(this.label4);
            this.Pause.Controls.Add(this.label3);
            this.Pause.Controls.Add(this.lbNazev);
            this.Pause.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pause.Enabled = false;
            this.Pause.Location = new System.Drawing.Point(0, 0);
            this.Pause.Name = "Pause";
            this.Pause.Size = new System.Drawing.Size(1520, 825);
            this.Pause.TabIndex = 4;
            this.Pause.Visible = false;
            // 
            // panelPauza
            // 
            this.panelPauza.BackColor = System.Drawing.Color.Transparent;
            this.panelPauza.Controls.Add(this.label17);
            this.panelPauza.Controls.Add(this.label16);
            this.panelPauza.Controls.Add(this.label15);
            this.panelPauza.Location = new System.Drawing.Point(12, 12);
            this.panelPauza.Name = "panelPauza";
            this.panelPauza.Size = new System.Drawing.Size(419, 100);
            this.panelPauza.TabIndex = 21;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(3, 26);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(249, 37);
            this.label17.TabIndex = 13;
            this.label17.Text = "Pauza/Pokračovat";
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(258, 26);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(28, 37);
            this.label16.TabIndex = 14;
            this.label16.Text = "-";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(292, 26);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(103, 37);
            this.label15.TabIndex = 15;
            this.label15.Text = "Escape";
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(869, 510);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(31, 37);
            this.label18.TabIndex = 20;
            this.label18.Text = "E";
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(714, 510);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(28, 37);
            this.label19.TabIndex = 19;
            this.label19.Text = "-";
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(536, 510);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(80, 37);
            this.label20.TabIndex = 18;
            this.label20.Text = "Dash";
            // 
            // btMenu
            // 
            this.btMenu.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btMenu.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btMenu.Location = new System.Drawing.Point(618, 623);
            this.btMenu.Name = "btMenu";
            this.btMenu.Size = new System.Drawing.Size(242, 59);
            this.btMenu.TabIndex = 17;
            this.btMenu.TabStop = false;
            this.btMenu.Text = "Zpátky do menu";
            this.btMenu.UseVisualStyleBackColor = false;
            this.btMenu.Click += new System.EventHandler(this.btMenu_Click);
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(869, 440);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(37, 37);
            this.label12.TabIndex = 12;
            this.label12.Text = "Q";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(714, 440);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(28, 37);
            this.label13.TabIndex = 11;
            this.label13.Text = "-";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(536, 440);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(99, 37);
            this.label14.TabIndex = 10;
            this.label14.Text = "Výpad";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(869, 377);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 37);
            this.label9.TabIndex = 9;
            this.label9.Text = "LTM";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(714, 377);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(28, 37);
            this.label10.TabIndex = 8;
            this.label10.Text = "-";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(536, 379);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 37);
            this.label11.TabIndex = 7;
            this.label11.Text = "Útok";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(869, 318);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(136, 37);
            this.label6.TabIndex = 6;
            this.label6.Text = "Mezerník";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(714, 318);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 37);
            this.label7.TabIndex = 5;
            this.label7.Text = "-";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(536, 318);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 37);
            this.label8.TabIndex = 4;
            this.label8.Text = "Skok";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(869, 257);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 37);
            this.label5.TabIndex = 3;
            this.label5.Text = "A,D";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(714, 257);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 37);
            this.label4.TabIndex = 2;
            this.label4.Text = "-";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(536, 257);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 37);
            this.label3.TabIndex = 1;
            this.label3.Text = "Pohyb";
            // 
            // lbNazev
            // 
            this.lbNazev.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbNazev.AutoSize = true;
            this.lbNazev.BackColor = System.Drawing.Color.Transparent;
            this.lbNazev.Font = new System.Drawing.Font("Segoe UI", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbNazev.ForeColor = System.Drawing.Color.White;
            this.lbNazev.Location = new System.Drawing.Point(651, 73);
            this.lbNazev.Name = "lbNazev";
            this.lbNazev.Size = new System.Drawing.Size(179, 72);
            this.lbNazev.TabIndex = 0;
            this.lbNazev.Text = "Pauza";
            // 
            // Dash
            // 
            this.Dash.Interval = 500;
            this.Dash.Tick += new System.EventHandler(this.Dash_Tick);
            // 
            // abilityLMB
            // 
            this.abilityLMB.Interval = 20;
            this.abilityLMB.Tick += new System.EventHandler(this.abilityLMB_Tick);
            // 
            // DMGcooldown
            // 
            this.DMGcooldown.Interval = 2000;
            this.DMGcooldown.Tick += new System.EventHandler(this.DMGcooldown_Tick);
            // 
            // Absence1
            // 
            this.Absence1.Tick += new System.EventHandler(this.Absence1_Tick);
            // 
            // Absence2
            // 
            this.Absence2.Tick += new System.EventHandler(this.Absence2_Tick);
            // 
            // Oberhofnerova
            // 
            this.Oberhofnerova.Tick += new System.EventHandler(this.Oberhofnerova_Tick);
            // 
            // Hacek
            // 
            this.Hacek.Tick += new System.EventHandler(this.Hacek_Tick);
            // 
            // NuggetDisappear
            // 
            this.NuggetDisappear.Tick += new System.EventHandler(this.NuggetDisappear_Tick);
            // 
            // Lemka
            // 
            this.Lemka.Tick += new System.EventHandler(this.Lemka_Tick);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1520, 825);
            this.Controls.Add(this.lbPress);
            this.Controls.Add(this.lbGameOver);
            this.Controls.Add(this.GameScene);
            this.Controls.Add(this.Menu);
            this.Controls.Add(this.Pause);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cesta k Maturitě";
            this.Load += new System.EventHandler(this.GameScene_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyUp);
            this.GameScene.ResumeLayout(false);
            this.GameScene.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Player)).EndInit();
            this.Menu.ResumeLayout(false);
            this.Menu.PerformLayout();
            this.Pause.ResumeLayout(false);
            this.Pause.PerformLayout();
            this.panelPauza.ResumeLayout(false);
            this.panelPauza.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox GameScene;
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
    }
}