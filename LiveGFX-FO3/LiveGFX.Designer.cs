namespace LiveGFX_FO3
{
    partial class LiveGFX
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LiveGFX));
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.bar_title = new System.Windows.Forms.Panel();
            this.bunifuCustomLabel1 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_minimize = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_maximize = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_close = new Bunifu.Framework.UI.BunifuImageButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_options = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btn_table = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btn_event = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btn_team = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btn_live2 = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btn_live1 = new Bunifu.Framework.UI.BunifuFlatButton();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.eventInfo1 = new LiveGFX_FO3.EventInfo();
            this.inGame1 = new LiveGFX_FO3.InGame();
            this.liveEvent1 = new LiveGFX_FO3.LiveEvent();
            this.table1 = new LiveGFX_FO3.Table();
            this.teamDatabase1 = new LiveGFX_FO3.TeamDatabase();
            this.bar_title.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_minimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_maximize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_close)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // bar_title
            // 
            this.bar_title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(29)))), ((int)(((byte)(34)))));
            this.bar_title.Controls.Add(this.bunifuCustomLabel1);
            this.bar_title.Controls.Add(this.pictureBox1);
            this.bar_title.Controls.Add(this.btn_minimize);
            this.bar_title.Controls.Add(this.btn_maximize);
            this.bar_title.Controls.Add(this.btn_close);
            this.bar_title.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar_title.Location = new System.Drawing.Point(0, 0);
            this.bar_title.Name = "bar_title";
            this.bar_title.Size = new System.Drawing.Size(1600, 28);
            this.bar_title.TabIndex = 1;
            this.bar_title.DoubleClick += new System.EventHandler(this.bar_title_DoubleClick);
            // 
            // bunifuCustomLabel1
            // 
            this.bunifuCustomLabel1.AutoSize = true;
            this.bunifuCustomLabel1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel1.ForeColor = System.Drawing.Color.White;
            this.bunifuCustomLabel1.Location = new System.Drawing.Point(40, 6);
            this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
            this.bunifuCustomLabel1.Size = new System.Drawing.Size(341, 16);
            this.bunifuCustomLabel1.TabIndex = 5;
            this.bunifuCustomLabel1.Text = "Studio INVATE - LiveGFX : League of Legends Version 0.1 ALPHA";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(8, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(26, 25);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // btn_minimize
            // 
            this.btn_minimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_minimize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(29)))), ((int)(((byte)(34)))));
            this.btn_minimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_minimize.Image = ((System.Drawing.Image)(resources.GetObject("btn_minimize.Image")));
            this.btn_minimize.ImageActive = null;
            this.btn_minimize.Location = new System.Drawing.Point(1496, 2);
            this.btn_minimize.Name = "btn_minimize";
            this.btn_minimize.Size = new System.Drawing.Size(25, 25);
            this.btn_minimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_minimize.TabIndex = 3;
            this.btn_minimize.TabStop = false;
            this.btn_minimize.Zoom = 10;
            this.btn_minimize.Click += new System.EventHandler(this.btn_minimize_Click);
            // 
            // btn_maximize
            // 
            this.btn_maximize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_maximize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(29)))), ((int)(((byte)(34)))));
            this.btn_maximize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_maximize.Image = ((System.Drawing.Image)(resources.GetObject("btn_maximize.Image")));
            this.btn_maximize.ImageActive = null;
            this.btn_maximize.Location = new System.Drawing.Point(1534, 2);
            this.btn_maximize.Name = "btn_maximize";
            this.btn_maximize.Size = new System.Drawing.Size(25, 25);
            this.btn_maximize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_maximize.TabIndex = 2;
            this.btn_maximize.TabStop = false;
            this.btn_maximize.Zoom = 10;
            this.btn_maximize.Click += new System.EventHandler(this.btn_maximize_Click);
            // 
            // btn_close
            // 
            this.btn_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(29)))), ((int)(((byte)(34)))));
            this.btn_close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_close.Image = ((System.Drawing.Image)(resources.GetObject("btn_close.Image")));
            this.btn_close.ImageActive = null;
            this.btn_close.Location = new System.Drawing.Point(1568, 2);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(25, 25);
            this.btn_close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_close.TabIndex = 1;
            this.btn_close.TabStop = false;
            this.btn_close.Zoom = 10;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.panel2.Controls.Add(this.btn_options);
            this.panel2.Controls.Add(this.btn_table);
            this.panel2.Controls.Add(this.btn_event);
            this.panel2.Controls.Add(this.btn_team);
            this.panel2.Controls.Add(this.btn_live2);
            this.panel2.Controls.Add(this.btn_live1);
            this.panel2.Controls.Add(this.pictureBox3);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 28);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1600, 66);
            this.panel2.TabIndex = 2;
            // 
            // btn_options
            // 
            this.btn_options.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(46)))), ((int)(((byte)(48)))));
            this.btn_options.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btn_options.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_options.BorderRadius = 0;
            this.btn_options.ButtonText = "   OPTIONS";
            this.btn_options.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_options.DisabledColor = System.Drawing.Color.Gray;
            this.btn_options.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_options.Iconcolor = System.Drawing.Color.Transparent;
            this.btn_options.Iconimage = ((System.Drawing.Image)(resources.GetObject("btn_options.Iconimage")));
            this.btn_options.Iconimage_right = null;
            this.btn_options.Iconimage_right_Selected = null;
            this.btn_options.Iconimage_Selected = ((System.Drawing.Image)(resources.GetObject("btn_options.Iconimage_Selected")));
            this.btn_options.IconRightVisible = true;
            this.btn_options.IconRightZoom = 0D;
            this.btn_options.IconVisible = true;
            this.btn_options.IconZoom = 40D;
            this.btn_options.IsTab = true;
            this.btn_options.Location = new System.Drawing.Point(1450, 0);
            this.btn_options.Name = "btn_options";
            this.btn_options.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btn_options.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(72)))), ((int)(((byte)(75)))));
            this.btn_options.OnHoverTextColor = System.Drawing.Color.White;
            this.btn_options.selected = false;
            this.btn_options.Size = new System.Drawing.Size(150, 66);
            this.btn_options.TabIndex = 7;
            this.btn_options.Text = "   OPTIONS";
            this.btn_options.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_options.Textcolor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_options.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btn_table
            // 
            this.btn_table.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(46)))), ((int)(((byte)(48)))));
            this.btn_table.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btn_table.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_table.BorderRadius = 0;
            this.btn_table.ButtonText = "   TABLE";
            this.btn_table.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_table.DisabledColor = System.Drawing.Color.Gray;
            this.btn_table.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_table.Iconcolor = System.Drawing.Color.Transparent;
            this.btn_table.Iconimage = ((System.Drawing.Image)(resources.GetObject("btn_table.Iconimage")));
            this.btn_table.Iconimage_right = null;
            this.btn_table.Iconimage_right_Selected = null;
            this.btn_table.Iconimage_Selected = ((System.Drawing.Image)(resources.GetObject("btn_table.Iconimage_Selected")));
            this.btn_table.IconRightVisible = true;
            this.btn_table.IconRightZoom = 0D;
            this.btn_table.IconVisible = true;
            this.btn_table.IconZoom = 40D;
            this.btn_table.IsTab = true;
            this.btn_table.Location = new System.Drawing.Point(845, 0);
            this.btn_table.Name = "btn_table";
            this.btn_table.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btn_table.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(72)))), ((int)(((byte)(75)))));
            this.btn_table.OnHoverTextColor = System.Drawing.Color.White;
            this.btn_table.selected = false;
            this.btn_table.Size = new System.Drawing.Size(150, 66);
            this.btn_table.TabIndex = 6;
            this.btn_table.Text = "   TABLE";
            this.btn_table.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_table.Textcolor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_table.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_table.Click += new System.EventHandler(this.btn_table_Click);
            // 
            // btn_event
            // 
            this.btn_event.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(46)))), ((int)(((byte)(48)))));
            this.btn_event.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btn_event.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_event.BorderRadius = 0;
            this.btn_event.ButtonText = "   EVENT INFO";
            this.btn_event.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_event.DisabledColor = System.Drawing.Color.Gray;
            this.btn_event.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_event.Iconcolor = System.Drawing.Color.Transparent;
            this.btn_event.Iconimage = ((System.Drawing.Image)(resources.GetObject("btn_event.Iconimage")));
            this.btn_event.Iconimage_right = null;
            this.btn_event.Iconimage_right_Selected = null;
            this.btn_event.Iconimage_Selected = ((System.Drawing.Image)(resources.GetObject("btn_event.Iconimage_Selected")));
            this.btn_event.IconRightVisible = true;
            this.btn_event.IconRightZoom = 0D;
            this.btn_event.IconVisible = true;
            this.btn_event.IconZoom = 40D;
            this.btn_event.IsTab = true;
            this.btn_event.Location = new System.Drawing.Point(679, 0);
            this.btn_event.Name = "btn_event";
            this.btn_event.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btn_event.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(72)))), ((int)(((byte)(75)))));
            this.btn_event.OnHoverTextColor = System.Drawing.Color.White;
            this.btn_event.selected = false;
            this.btn_event.Size = new System.Drawing.Size(166, 66);
            this.btn_event.TabIndex = 5;
            this.btn_event.Text = "   EVENT INFO";
            this.btn_event.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_event.Textcolor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_event.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_event.Click += new System.EventHandler(this.btn_event_Click);
            // 
            // btn_team
            // 
            this.btn_team.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(46)))), ((int)(((byte)(48)))));
            this.btn_team.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btn_team.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_team.BorderRadius = 0;
            this.btn_team.ButtonText = "   TEAM DATABASE";
            this.btn_team.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_team.DisabledColor = System.Drawing.Color.Gray;
            this.btn_team.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_team.Iconcolor = System.Drawing.Color.Transparent;
            this.btn_team.Iconimage = ((System.Drawing.Image)(resources.GetObject("btn_team.Iconimage")));
            this.btn_team.Iconimage_right = null;
            this.btn_team.Iconimage_right_Selected = null;
            this.btn_team.Iconimage_Selected = ((System.Drawing.Image)(resources.GetObject("btn_team.Iconimage_Selected")));
            this.btn_team.IconRightVisible = true;
            this.btn_team.IconRightZoom = 0D;
            this.btn_team.IconVisible = true;
            this.btn_team.IconZoom = 40D;
            this.btn_team.IsTab = true;
            this.btn_team.Location = new System.Drawing.Point(479, 0);
            this.btn_team.Name = "btn_team";
            this.btn_team.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btn_team.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(72)))), ((int)(((byte)(75)))));
            this.btn_team.OnHoverTextColor = System.Drawing.Color.White;
            this.btn_team.selected = false;
            this.btn_team.Size = new System.Drawing.Size(200, 66);
            this.btn_team.TabIndex = 4;
            this.btn_team.Text = "   TEAM DATABASE";
            this.btn_team.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_team.Textcolor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_team.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_team.Click += new System.EventHandler(this.btn_team_Click);
            // 
            // btn_live2
            // 
            this.btn_live2.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(46)))), ((int)(((byte)(48)))));
            this.btn_live2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btn_live2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_live2.BorderRadius = 0;
            this.btn_live2.ButtonText = "    IN-GAME";
            this.btn_live2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_live2.DisabledColor = System.Drawing.Color.Gray;
            this.btn_live2.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_live2.Iconcolor = System.Drawing.Color.Transparent;
            this.btn_live2.Iconimage = ((System.Drawing.Image)(resources.GetObject("btn_live2.Iconimage")));
            this.btn_live2.Iconimage_right = null;
            this.btn_live2.Iconimage_right_Selected = null;
            this.btn_live2.Iconimage_Selected = ((System.Drawing.Image)(resources.GetObject("btn_live2.Iconimage_Selected")));
            this.btn_live2.IconRightVisible = true;
            this.btn_live2.IconRightZoom = 0D;
            this.btn_live2.IconVisible = true;
            this.btn_live2.IconZoom = 40D;
            this.btn_live2.IsTab = true;
            this.btn_live2.Location = new System.Drawing.Point(331, 0);
            this.btn_live2.Name = "btn_live2";
            this.btn_live2.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btn_live2.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(72)))), ((int)(((byte)(75)))));
            this.btn_live2.OnHoverTextColor = System.Drawing.Color.White;
            this.btn_live2.selected = false;
            this.btn_live2.Size = new System.Drawing.Size(148, 66);
            this.btn_live2.TabIndex = 3;
            this.btn_live2.Text = "    IN-GAME";
            this.btn_live2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_live2.Textcolor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_live2.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_live2.Click += new System.EventHandler(this.btn_live2_Click);
            // 
            // btn_live1
            // 
            this.btn_live1.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.btn_live1.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(46)))), ((int)(((byte)(48)))));
            this.btn_live1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btn_live1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_live1.BorderRadius = 0;
            this.btn_live1.ButtonText = "   LIVE EVENT";
            this.btn_live1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_live1.DisabledColor = System.Drawing.Color.Gray;
            this.btn_live1.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_live1.Iconcolor = System.Drawing.Color.Transparent;
            this.btn_live1.Iconimage = ((System.Drawing.Image)(resources.GetObject("btn_live1.Iconimage")));
            this.btn_live1.Iconimage_right = null;
            this.btn_live1.Iconimage_right_Selected = null;
            this.btn_live1.Iconimage_Selected = ((System.Drawing.Image)(resources.GetObject("btn_live1.Iconimage_Selected")));
            this.btn_live1.IconRightVisible = true;
            this.btn_live1.IconRightZoom = 0D;
            this.btn_live1.IconVisible = true;
            this.btn_live1.IconZoom = 40D;
            this.btn_live1.IsTab = true;
            this.btn_live1.Location = new System.Drawing.Point(154, 0);
            this.btn_live1.Name = "btn_live1";
            this.btn_live1.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btn_live1.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(72)))), ((int)(((byte)(75)))));
            this.btn_live1.OnHoverTextColor = System.Drawing.Color.White;
            this.btn_live1.selected = false;
            this.btn_live1.Size = new System.Drawing.Size(177, 66);
            this.btn_live1.TabIndex = 0;
            this.btn_live1.Text = "   LIVE EVENT";
            this.btn_live1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_live1.Textcolor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_live1.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_live1.Click += new System.EventHandler(this.btn_live1_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(143, 0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(11, 66);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(143, 66);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(10, 1);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(125, 60);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this.bar_title;
            this.bunifuDragControl1.Vertical = true;
            // 
            // eventInfo1
            // 
            this.eventInfo1.AutoScroll = true;
            this.eventInfo1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(61)))), ((int)(((byte)(64)))));
            this.eventInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eventInfo1.Location = new System.Drawing.Point(0, 0);
            this.eventInfo1.Name = "eventInfo1";
            this.eventInfo1.Size = new System.Drawing.Size(1600, 900);
            this.eventInfo1.TabIndex = 5;
            // 
            // inGame1
            // 
            this.inGame1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(61)))), ((int)(((byte)(64)))));
            this.inGame1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inGame1.Location = new System.Drawing.Point(0, 0);
            this.inGame1.Name = "inGame1";
            this.inGame1.Size = new System.Drawing.Size(1600, 900);
            this.inGame1.TabIndex = 4;
            // 
            // liveEvent1
            // 
            this.liveEvent1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(61)))), ((int)(((byte)(64)))));
            this.liveEvent1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.liveEvent1.Location = new System.Drawing.Point(0, 94);
            this.liveEvent1.Name = "liveEvent1";
            this.liveEvent1.Size = new System.Drawing.Size(1600, 806);
            this.liveEvent1.TabIndex = 3;
            // 
            // table1
            // 
            this.table1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(61)))), ((int)(((byte)(64)))));
            this.table1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.table1.Location = new System.Drawing.Point(0, 0);
            this.table1.Name = "table1";
            this.table1.Size = new System.Drawing.Size(1600, 900);
            this.table1.TabIndex = 6;
            // 
            // teamDatabase1
            // 
            this.teamDatabase1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(61)))), ((int)(((byte)(64)))));
            this.teamDatabase1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.teamDatabase1.Location = new System.Drawing.Point(0, 0);
            this.teamDatabase1.Name = "teamDatabase1";
            this.teamDatabase1.Size = new System.Drawing.Size(1600, 900);
            this.teamDatabase1.TabIndex = 7;
            // 
            // LiveGFX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(61)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1600, 900);
            this.Controls.Add(this.liveEvent1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.bar_title);
            this.Controls.Add(this.teamDatabase1);
            this.Controls.Add(this.table1);
            this.Controls.Add(this.eventInfo1);
            this.Controls.Add(this.inGame1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LiveGFX";
            this.Text = "Form1";
            this.bar_title.ResumeLayout(false);
            this.bar_title.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_minimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_maximize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_close)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.Panel panel2;
        private Bunifu.Framework.UI.BunifuFlatButton btn_options;
        private Bunifu.Framework.UI.BunifuFlatButton btn_table;
        private Bunifu.Framework.UI.BunifuFlatButton btn_event;
        private Bunifu.Framework.UI.BunifuFlatButton btn_team;
        private Bunifu.Framework.UI.BunifuFlatButton btn_live2;
        private Bunifu.Framework.UI.BunifuFlatButton btn_live1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel bar_title;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Bunifu.Framework.UI.BunifuImageButton btn_minimize;
        private Bunifu.Framework.UI.BunifuImageButton btn_maximize;
        private Bunifu.Framework.UI.BunifuImageButton btn_close;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private InGame inGame1;
        private EventInfo eventInfo1;
        private LiveEvent liveEvent1;
        private TeamDatabase teamDatabase1;
        private Table table1;
    }
}

