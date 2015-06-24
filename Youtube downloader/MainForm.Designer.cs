namespace Youtube_downloader
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lblURL = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.lblSave = new System.Windows.Forms.Label();
            this.txtdir = new System.Windows.Forms.TextBox();
            this.btnbrowse = new System.Windows.Forms.Button();
            this.groupBoxQlty = new System.Windows.Forms.GroupBox();
            this.rdb4k = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.rdbmaxqlty = new System.Windows.Forms.RadioButton();
            this.rdbsd360 = new System.Windows.Forms.RadioButton();
            this.rdbsd480 = new System.Windows.Forms.RadioButton();
            this.rdbhd720 = new System.Windows.Forms.RadioButton();
            this.rdbhd1080 = new System.Windows.Forms.RadioButton();
            this.groupBoxFtype = new System.Windows.Forms.GroupBox();
            this.rdbwebm = new System.Windows.Forms.RadioButton();
            this.rdbmp4 = new System.Windows.Forms.RadioButton();
            this.txtfilename = new System.Windows.Forms.TextBox();
            this.btndwnld = new System.Windows.Forms.Button();
            this.lblFileSpChar = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radCustomFileName = new System.Windows.Forms.RadioButton();
            this.radYTtitle = new System.Windows.Forms.RadioButton();
            this.chkDefLoc = new System.Windows.Forms.CheckBox();
            this.chkPlaylst = new System.Windows.Forms.CheckBox();
            this.lblPLstart = new System.Windows.Forms.Label();
            this.txtPLstart = new System.Windows.Forms.TextBox();
            this.lblPLend = new System.Windows.Forms.Label();
            this.txtPLend = new System.Windows.Forms.TextBox();
            this.linkgit = new System.Windows.Forms.LinkLabel();
            this.groupBoxQlty.SuspendLayout();
            this.groupBoxFtype.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblURL
            // 
            this.lblURL.AutoSize = true;
            this.lblURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblURL.Location = new System.Drawing.Point(19, 33);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(122, 18);
            this.lblURL.TabIndex = 0;
            this.lblURL.Text = "URL of the video:";
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(143, 34);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(564, 20);
            this.txtURL.TabIndex = 1;
            // 
            // lblSave
            // 
            this.lblSave.AutoSize = true;
            this.lblSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSave.Location = new System.Drawing.Point(79, 93);
            this.lblSave.Name = "lblSave";
            this.lblSave.Size = new System.Drawing.Size(62, 18);
            this.lblSave.TabIndex = 2;
            this.lblSave.Text = "Save to:";
            // 
            // txtdir
            // 
            this.txtdir.Location = new System.Drawing.Point(143, 91);
            this.txtdir.Name = "txtdir";
            this.txtdir.Size = new System.Drawing.Size(493, 20);
            this.txtdir.TabIndex = 2;
            this.txtdir.TextChanged += new System.EventHandler(this.txtdir_TextChanged);
            // 
            // btnbrowse
            // 
            this.btnbrowse.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnbrowse.Location = new System.Drawing.Point(639, 86);
            this.btnbrowse.Name = "btnbrowse";
            this.btnbrowse.Size = new System.Drawing.Size(70, 30);
            this.btnbrowse.TabIndex = 3;
            this.btnbrowse.Text = "...";
            this.btnbrowse.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnbrowse.UseVisualStyleBackColor = true;
            this.btnbrowse.Click += new System.EventHandler(this.btnbrowse_Click);
            // 
            // groupBoxQlty
            // 
            this.groupBoxQlty.Controls.Add(this.rdb4k);
            this.groupBoxQlty.Controls.Add(this.label1);
            this.groupBoxQlty.Controls.Add(this.rdbmaxqlty);
            this.groupBoxQlty.Controls.Add(this.rdbsd360);
            this.groupBoxQlty.Controls.Add(this.rdbsd480);
            this.groupBoxQlty.Controls.Add(this.rdbhd720);
            this.groupBoxQlty.Controls.Add(this.rdbhd1080);
            this.groupBoxQlty.Location = new System.Drawing.Point(114, 281);
            this.groupBoxQlty.Name = "groupBoxQlty";
            this.groupBoxQlty.Size = new System.Drawing.Size(225, 137);
            this.groupBoxQlty.TabIndex = 8;
            this.groupBoxQlty.TabStop = false;
            this.groupBoxQlty.Text = "Quality (depends on availability)";
            // 
            // rdb4k
            // 
            this.rdb4k.AutoSize = true;
            this.rdb4k.Location = new System.Drawing.Point(74, 39);
            this.rdb4k.Name = "rdb4k";
            this.rdb4k.Size = new System.Drawing.Size(76, 17);
            this.rdb4k.TabIndex = 10;
            this.rdb4k.TabStop = true;
            this.rdb4k.Text = "4k (DASH)";
            this.rdb4k.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "DASH requires ffmpeg to be installed.";
            // 
            // rdbmaxqlty
            // 
            this.rdbmaxqlty.AutoSize = true;
            this.rdbmaxqlty.Checked = true;
            this.rdbmaxqlty.Location = new System.Drawing.Point(9, 18);
            this.rdbmaxqlty.Name = "rdbmaxqlty";
            this.rdbmaxqlty.Size = new System.Drawing.Size(186, 17);
            this.rdbmaxqlty.TabIndex = 9;
            this.rdbmaxqlty.TabStop = true;
            this.rdbmaxqlty.Text = "Max. available quality (non DASH)";
            this.rdbmaxqlty.UseVisualStyleBackColor = true;
            // 
            // rdbsd360
            // 
            this.rdbsd360.AutoSize = true;
            this.rdbsd360.Location = new System.Drawing.Point(148, 89);
            this.rdbsd360.Name = "rdbsd360";
            this.rdbsd360.Size = new System.Drawing.Size(67, 17);
            this.rdbsd360.TabIndex = 14;
            this.rdbsd360.TabStop = true;
            this.rdbsd360.Text = "SD 360p";
            this.rdbsd360.UseVisualStyleBackColor = true;
            // 
            // rdbsd480
            // 
            this.rdbsd480.AutoSize = true;
            this.rdbsd480.Location = new System.Drawing.Point(9, 89);
            this.rdbsd480.Name = "rdbsd480";
            this.rdbsd480.Size = new System.Drawing.Size(106, 17);
            this.rdbsd480.TabIndex = 13;
            this.rdbsd480.TabStop = true;
            this.rdbsd480.Text = "SD 480p (DASH)";
            this.rdbsd480.UseVisualStyleBackColor = true;
            // 
            // rdbhd720
            // 
            this.rdbhd720.AutoSize = true;
            this.rdbhd720.Location = new System.Drawing.Point(148, 62);
            this.rdbhd720.Name = "rdbhd720";
            this.rdbhd720.Size = new System.Drawing.Size(68, 17);
            this.rdbhd720.TabIndex = 12;
            this.rdbhd720.TabStop = true;
            this.rdbhd720.Text = "HD 720p";
            this.rdbhd720.UseVisualStyleBackColor = true;
            // 
            // rdbhd1080
            // 
            this.rdbhd1080.AutoSize = true;
            this.rdbhd1080.Location = new System.Drawing.Point(9, 62);
            this.rdbhd1080.Name = "rdbhd1080";
            this.rdbhd1080.Size = new System.Drawing.Size(113, 17);
            this.rdbhd1080.TabIndex = 11;
            this.rdbhd1080.TabStop = true;
            this.rdbhd1080.Text = "HD 1080p (DASH)";
            this.rdbhd1080.UseVisualStyleBackColor = true;
            // 
            // groupBoxFtype
            // 
            this.groupBoxFtype.Controls.Add(this.rdbwebm);
            this.groupBoxFtype.Controls.Add(this.rdbmp4);
            this.groupBoxFtype.Location = new System.Drawing.Point(382, 281);
            this.groupBoxFtype.Name = "groupBoxFtype";
            this.groupBoxFtype.Size = new System.Drawing.Size(213, 137);
            this.groupBoxFtype.TabIndex = 15;
            this.groupBoxFtype.TabStop = false;
            this.groupBoxFtype.Text = "File type";
            // 
            // rdbwebm
            // 
            this.rdbwebm.AutoSize = true;
            this.rdbwebm.Location = new System.Drawing.Point(142, 48);
            this.rdbwebm.Name = "rdbwebm";
            this.rdbwebm.Size = new System.Drawing.Size(53, 17);
            this.rdbwebm.TabIndex = 16;
            this.rdbwebm.Text = "webm";
            this.rdbwebm.UseVisualStyleBackColor = true;
            // 
            // rdbmp4
            // 
            this.rdbmp4.AutoSize = true;
            this.rdbmp4.Checked = true;
            this.rdbmp4.Location = new System.Drawing.Point(18, 48);
            this.rdbmp4.Name = "rdbmp4";
            this.rdbmp4.Size = new System.Drawing.Size(47, 17);
            this.rdbmp4.TabIndex = 15;
            this.rdbmp4.TabStop = true;
            this.rdbmp4.Text = "MP4";
            this.rdbmp4.UseVisualStyleBackColor = true;
            // 
            // txtfilename
            // 
            this.txtfilename.Location = new System.Drawing.Point(129, 51);
            this.txtfilename.Name = "txtfilename";
            this.txtfilename.Size = new System.Drawing.Size(510, 20);
            this.txtfilename.TabIndex = 7;
            this.txtfilename.TextChanged += new System.EventHandler(this.txtfilename_TextChanged);
            // 
            // btndwnld
            // 
            this.btndwnld.Location = new System.Drawing.Point(317, 435);
            this.btndwnld.Name = "btndwnld";
            this.btndwnld.Size = new System.Drawing.Size(95, 29);
            this.btndwnld.TabIndex = 17;
            this.btndwnld.Text = "Download";
            this.btndwnld.UseVisualStyleBackColor = true;
            this.btndwnld.Click += new System.EventHandler(this.btndwnld_Click);
            // 
            // lblFileSpChar
            // 
            this.lblFileSpChar.AutoSize = true;
            this.lblFileSpChar.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileSpChar.Location = new System.Drawing.Point(97, 74);
            this.lblFileSpChar.Name = "lblFileSpChar";
            this.lblFileSpChar.Size = new System.Drawing.Size(490, 19);
            this.lblFileSpChar.TabIndex = 10;
            this.lblFileSpChar.Text = "File name should not contain special characters such as  \" : ; / \\ % $ etc..";
            this.lblFileSpChar.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radCustomFileName);
            this.groupBox1.Controls.Add(this.radYTtitle);
            this.groupBox1.Controls.Add(this.lblFileSpChar);
            this.groupBox1.Controls.Add(this.txtfilename);
            this.groupBox1.Location = new System.Drawing.Point(22, 147);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(687, 103);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File Name";
            // 
            // radCustomFileName
            // 
            this.radCustomFileName.AutoSize = true;
            this.radCustomFileName.Location = new System.Drawing.Point(60, 51);
            this.radCustomFileName.Name = "radCustomFileName";
            this.radCustomFileName.Size = new System.Drawing.Size(63, 17);
            this.radCustomFileName.TabIndex = 6;
            this.radCustomFileName.TabStop = true;
            this.radCustomFileName.Text = "Custom:";
            this.radCustomFileName.UseVisualStyleBackColor = true;
            this.radCustomFileName.CheckedChanged += new System.EventHandler(this.radCustomFileName_CheckedChanged);
            // 
            // radYTtitle
            // 
            this.radYTtitle.AutoSize = true;
            this.radYTtitle.Location = new System.Drawing.Point(60, 19);
            this.radYTtitle.Name = "radYTtitle";
            this.radYTtitle.Size = new System.Drawing.Size(132, 17);
            this.radYTtitle.TabIndex = 5;
            this.radYTtitle.TabStop = true;
            this.radYTtitle.Text = "Same as YouTube title";
            this.radYTtitle.UseVisualStyleBackColor = true;
            this.radYTtitle.CheckedChanged += new System.EventHandler(this.radYTtitle_CheckedChanged);
            // 
            // chkDefLoc
            // 
            this.chkDefLoc.AutoSize = true;
            this.chkDefLoc.Location = new System.Drawing.Point(149, 124);
            this.chkDefLoc.Name = "chkDefLoc";
            this.chkDefLoc.Size = new System.Drawing.Size(131, 17);
            this.chkDefLoc.TabIndex = 4;
            this.chkDefLoc.Text = "Set as default location";
            this.chkDefLoc.UseVisualStyleBackColor = true;
            this.chkDefLoc.CheckedChanged += new System.EventHandler(this.chkDefLoc_CheckedChanged);
            // 
            // chkPlaylst
            // 
            this.chkPlaylst.AutoSize = true;
            this.chkPlaylst.Location = new System.Drawing.Point(143, 60);
            this.chkPlaylst.Name = "chkPlaylst";
            this.chkPlaylst.Size = new System.Drawing.Size(116, 17);
            this.chkPlaylst.TabIndex = 18;
            this.chkPlaylst.Text = "URL is for a playlist";
            this.chkPlaylst.UseVisualStyleBackColor = true;
            this.chkPlaylst.CheckedChanged += new System.EventHandler(this.chkPlaylst_CheckedChanged);
            // 
            // lblPLstart
            // 
            this.lblPLstart.AutoSize = true;
            this.lblPLstart.Enabled = false;
            this.lblPLstart.Location = new System.Drawing.Point(302, 61);
            this.lblPLstart.Name = "lblPLstart";
            this.lblPLstart.Size = new System.Drawing.Size(153, 13);
            this.lblPLstart.TabIndex = 19;
            this.lblPLstart.Text = "Start from number (default is 1):";
            // 
            // txtPLstart
            // 
            this.txtPLstart.Enabled = false;
            this.txtPLstart.Location = new System.Drawing.Point(461, 61);
            this.txtPLstart.Name = "txtPLstart";
            this.txtPLstart.Size = new System.Drawing.Size(39, 20);
            this.txtPLstart.TabIndex = 20;
            // 
            // lblPLend
            // 
            this.lblPLend.AutoSize = true;
            this.lblPLend.Enabled = false;
            this.lblPLend.Location = new System.Drawing.Point(512, 63);
            this.lblPLend.Name = "lblPLend";
            this.lblPLend.Size = new System.Drawing.Size(149, 13);
            this.lblPLend.TabIndex = 21;
            this.lblPLend.Text = "End at number (default is last):";
            // 
            // txtPLend
            // 
            this.txtPLend.Enabled = false;
            this.txtPLend.Location = new System.Drawing.Point(667, 60);
            this.txtPLend.Name = "txtPLend";
            this.txtPLend.Size = new System.Drawing.Size(40, 20);
            this.txtPLend.TabIndex = 22;
            // 
            // linkgit
            // 
            this.linkgit.AutoSize = true;
            this.linkgit.Location = new System.Drawing.Point(639, 458);
            this.linkgit.Name = "linkgit";
            this.linkgit.Size = new System.Drawing.Size(91, 13);
            this.linkgit.TabIndex = 23;
            this.linkgit.TabStop = true;
            this.linkgit.Text = "github.com/rnand";
            this.linkgit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkgit_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 472);
            this.Controls.Add(this.linkgit);
            this.Controls.Add(this.txtPLend);
            this.Controls.Add(this.lblPLend);
            this.Controls.Add(this.txtPLstart);
            this.Controls.Add(this.lblPLstart);
            this.Controls.Add(this.chkPlaylst);
            this.Controls.Add(this.chkDefLoc);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btndwnld);
            this.Controls.Add(this.groupBoxFtype);
            this.Controls.Add(this.groupBoxQlty);
            this.Controls.Add(this.btnbrowse);
            this.Controls.Add(this.txtdir);
            this.Controls.Add(this.lblSave);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.lblURL);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "YouTube Downloader 1.3.5";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxQlty.ResumeLayout(false);
            this.groupBoxQlty.PerformLayout();
            this.groupBoxFtype.ResumeLayout(false);
            this.groupBoxFtype.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblURL;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Label lblSave;
        private System.Windows.Forms.TextBox txtdir;
        private System.Windows.Forms.Button btnbrowse;
        private System.Windows.Forms.GroupBox groupBoxQlty;
        private System.Windows.Forms.RadioButton rdbsd360;
        private System.Windows.Forms.RadioButton rdbsd480;
        private System.Windows.Forms.RadioButton rdbhd720;
        private System.Windows.Forms.RadioButton rdbhd1080;
        private System.Windows.Forms.GroupBox groupBoxFtype;
        private System.Windows.Forms.RadioButton rdbwebm;
        private System.Windows.Forms.RadioButton rdbmp4;
        private System.Windows.Forms.TextBox txtfilename;
        private System.Windows.Forms.Button btndwnld;
        private System.Windows.Forms.RadioButton rdbmaxqlty;
        private System.Windows.Forms.Label lblFileSpChar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radCustomFileName;
        private System.Windows.Forms.RadioButton radYTtitle;
        private System.Windows.Forms.CheckBox chkDefLoc;
        private System.Windows.Forms.RadioButton rdb4k;
        private System.Windows.Forms.CheckBox chkPlaylst;
        private System.Windows.Forms.Label lblPLstart;
        private System.Windows.Forms.TextBox txtPLstart;
        private System.Windows.Forms.Label lblPLend;
        private System.Windows.Forms.TextBox txtPLend;
        private System.Windows.Forms.LinkLabel linkgit;
    }
}

