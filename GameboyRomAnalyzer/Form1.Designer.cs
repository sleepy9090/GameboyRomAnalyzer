namespace GameboyRomAnalyzer
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
            if(disposing && (components != null))
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.writeROMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fixChecksumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxCorrectHeaderChecksum = new System.Windows.Forms.TextBox();
            this.comboBoxNewLicensee = new System.Windows.Forms.ComboBox();
            this.comboBoxOldLicensee = new System.Windows.Forms.ComboBox();
            this.textBoxGlobalChecksum = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxDestinationCode = new System.Windows.Forms.ComboBox();
            this.textBoxNintendoLogo = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.comboBoxRAMSize = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.comboBoxROMSize = new System.Windows.Forms.ComboBox();
            this.comboBoxCartridgeType = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.checkBoxColorGameBoy = new System.Windows.Forms.CheckBox();
            this.checkBoxGameBoy = new System.Windows.Forms.CheckBox();
            this.checkBoxIsSuperGameBoy = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxHeaderChecksum = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxVersionNumber = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxManufacturerCode = new System.Windows.Forms.TextBox();
            this.textBoxGameTitle = new System.Windows.Forms.TextBox();
            this.textBoxROMEntryPoint = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.restoreNintendoLogoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableColorGameBoyOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(634, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(100, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.writeROMToolStripMenuItem,
            this.fixChecksumToolStripMenuItem,
            this.restoreNintendoLogoToolStripMenuItem,
            this.enableColorGameBoyOptionsToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // writeROMToolStripMenuItem
            // 
            this.writeROMToolStripMenuItem.Name = "writeROMToolStripMenuItem";
            this.writeROMToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.writeROMToolStripMenuItem.Text = "&Write ROM";
            this.writeROMToolStripMenuItem.Click += new System.EventHandler(this.writeROMToolStripMenuItem_Click);
            // 
            // fixChecksumToolStripMenuItem
            // 
            this.fixChecksumToolStripMenuItem.Name = "fixChecksumToolStripMenuItem";
            this.fixChecksumToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.fixChecksumToolStripMenuItem.Text = "&Fix Header Checksum";
            this.fixChecksumToolStripMenuItem.Click += new System.EventHandler(this.fixChecksumToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textBoxCorrectHeaderChecksum);
            this.groupBox1.Controls.Add(this.comboBoxNewLicensee);
            this.groupBox1.Controls.Add(this.comboBoxOldLicensee);
            this.groupBox1.Controls.Add(this.textBoxGlobalChecksum);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBoxDestinationCode);
            this.groupBox1.Controls.Add(this.textBoxNintendoLogo);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.comboBoxRAMSize);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.comboBoxROMSize);
            this.groupBox1.Controls.Add(this.comboBoxCartridgeType);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.checkBoxColorGameBoy);
            this.groupBox1.Controls.Add(this.checkBoxGameBoy);
            this.groupBox1.Controls.Add(this.checkBoxIsSuperGameBoy);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.textBoxHeaderChecksum);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.textBoxVersionNumber);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textBoxManufacturerCode);
            this.groupBox1.Controls.Add(this.textBoxGameTitle);
            this.groupBox1.Controls.Add(this.textBoxROMEntryPoint);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 73);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(610, 353);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(224, 198);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(135, 13);
            this.label7.TabIndex = 44;
            this.label7.Text = "Correct Header Checksum:";
            // 
            // textBoxCorrectHeaderChecksum
            // 
            this.textBoxCorrectHeaderChecksum.Location = new System.Drawing.Point(365, 195);
            this.textBoxCorrectHeaderChecksum.Name = "textBoxCorrectHeaderChecksum";
            this.textBoxCorrectHeaderChecksum.Size = new System.Drawing.Size(100, 20);
            this.textBoxCorrectHeaderChecksum.TabIndex = 43;
            // 
            // comboBoxNewLicensee
            // 
            this.comboBoxNewLicensee.FormattingEnabled = true;
            this.comboBoxNewLicensee.Location = new System.Drawing.Point(118, 143);
            this.comboBoxNewLicensee.Name = "comboBoxNewLicensee";
            this.comboBoxNewLicensee.Size = new System.Drawing.Size(274, 21);
            this.comboBoxNewLicensee.TabIndex = 42;
            // 
            // comboBoxOldLicensee
            // 
            this.comboBoxOldLicensee.FormattingEnabled = true;
            this.comboBoxOldLicensee.Location = new System.Drawing.Point(118, 116);
            this.comboBoxOldLicensee.Name = "comboBoxOldLicensee";
            this.comboBoxOldLicensee.Size = new System.Drawing.Size(274, 21);
            this.comboBoxOldLicensee.TabIndex = 41;
            // 
            // textBoxGlobalChecksum
            // 
            this.textBoxGlobalChecksum.Location = new System.Drawing.Point(365, 169);
            this.textBoxGlobalChecksum.Name = "textBoxGlobalChecksum";
            this.textBoxGlobalChecksum.Size = new System.Drawing.Size(100, 20);
            this.textBoxGlobalChecksum.TabIndex = 40;
            this.textBoxGlobalChecksum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.validateHexInput);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(266, 172);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 13);
            this.label6.TabIndex = 39;
            this.label6.Text = "Global Checksum:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 35;
            this.label3.Text = "New Licensee Code:";
            // 
            // comboBoxDestinationCode
            // 
            this.comboBoxDestinationCode.FormattingEnabled = true;
            this.comboBoxDestinationCode.Location = new System.Drawing.Point(118, 90);
            this.comboBoxDestinationCode.Name = "comboBoxDestinationCode";
            this.comboBoxDestinationCode.Size = new System.Drawing.Size(206, 21);
            this.comboBoxDestinationCode.TabIndex = 34;
            // 
            // textBoxNintendoLogo
            // 
            this.textBoxNintendoLogo.Location = new System.Drawing.Point(118, 302);
            this.textBoxNintendoLogo.Multiline = true;
            this.textBoxNintendoLogo.Name = "textBoxNintendoLogo";
            this.textBoxNintendoLogo.Size = new System.Drawing.Size(486, 40);
            this.textBoxNintendoLogo.TabIndex = 33;
            this.textBoxNintendoLogo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.validateHexInput);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 278);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(57, 13);
            this.label14.TabIndex = 32;
            this.label14.Text = "RAM Size:";
            // 
            // comboBoxRAMSize
            // 
            this.comboBoxRAMSize.FormattingEnabled = true;
            this.comboBoxRAMSize.Location = new System.Drawing.Point(118, 275);
            this.comboBoxRAMSize.Name = "comboBoxRAMSize";
            this.comboBoxRAMSize.Size = new System.Drawing.Size(486, 21);
            this.comboBoxRAMSize.TabIndex = 31;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 251);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(58, 13);
            this.label13.TabIndex = 30;
            this.label13.Text = "ROM Size:";
            // 
            // comboBoxROMSize
            // 
            this.comboBoxROMSize.FormattingEnabled = true;
            this.comboBoxROMSize.Location = new System.Drawing.Point(118, 248);
            this.comboBoxROMSize.Name = "comboBoxROMSize";
            this.comboBoxROMSize.Size = new System.Drawing.Size(486, 21);
            this.comboBoxROMSize.TabIndex = 29;
            // 
            // comboBoxCartridgeType
            // 
            this.comboBoxCartridgeType.FormattingEnabled = true;
            this.comboBoxCartridgeType.Location = new System.Drawing.Point(118, 221);
            this.comboBoxCartridgeType.Name = "comboBoxCartridgeType";
            this.comboBoxCartridgeType.Size = new System.Drawing.Size(486, 21);
            this.comboBoxCartridgeType.TabIndex = 28;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 224);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 13);
            this.label12.TabIndex = 26;
            this.label12.Text = "Catridge Type:";
            // 
            // checkBoxColorGameBoy
            // 
            this.checkBoxColorGameBoy.AutoSize = true;
            this.checkBoxColorGameBoy.Location = new System.Drawing.Point(442, 41);
            this.checkBoxColorGameBoy.Name = "checkBoxColorGameBoy";
            this.checkBoxColorGameBoy.Size = new System.Drawing.Size(157, 17);
            this.checkBoxColorGameBoy.TabIndex = 25;
            this.checkBoxColorGameBoy.Text = "Color Game Boy Compatible";
            this.checkBoxColorGameBoy.UseVisualStyleBackColor = true;
            // 
            // checkBoxGameBoy
            // 
            this.checkBoxGameBoy.AutoSize = true;
            this.checkBoxGameBoy.Location = new System.Drawing.Point(442, 15);
            this.checkBoxGameBoy.Name = "checkBoxGameBoy";
            this.checkBoxGameBoy.Size = new System.Drawing.Size(130, 17);
            this.checkBoxGameBoy.TabIndex = 24;
            this.checkBoxGameBoy.Text = "Game Boy Compatible";
            this.checkBoxGameBoy.UseVisualStyleBackColor = true;
            // 
            // checkBoxIsSuperGameBoy
            // 
            this.checkBoxIsSuperGameBoy.AutoSize = true;
            this.checkBoxIsSuperGameBoy.Location = new System.Drawing.Point(442, 67);
            this.checkBoxIsSuperGameBoy.Name = "checkBoxIsSuperGameBoy";
            this.checkBoxIsSuperGameBoy.Size = new System.Drawing.Size(161, 17);
            this.checkBoxIsSuperGameBoy.TabIndex = 23;
            this.checkBoxIsSuperGameBoy.Text = "Super Game Boy Compatible";
            this.checkBoxIsSuperGameBoy.UseVisualStyleBackColor = true;
            this.checkBoxIsSuperGameBoy.CheckedChanged += new System.EventHandler(this.checkBoxIsSuperGameBoy_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 198);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Header Checksum:";
            // 
            // textBoxHeaderChecksum
            // 
            this.textBoxHeaderChecksum.Location = new System.Drawing.Point(118, 195);
            this.textBoxHeaderChecksum.Name = "textBoxHeaderChecksum";
            this.textBoxHeaderChecksum.Size = new System.Drawing.Size(100, 20);
            this.textBoxHeaderChecksum.TabIndex = 20;
            this.textBoxHeaderChecksum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.validateHexInput);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 172);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Version Number:";
            // 
            // textBoxVersionNumber
            // 
            this.textBoxVersionNumber.Location = new System.Drawing.Point(118, 169);
            this.textBoxVersionNumber.Name = "textBoxVersionNumber";
            this.textBoxVersionNumber.Size = new System.Drawing.Size(100, 20);
            this.textBoxVersionNumber.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 120);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Old Licensee Code:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 94);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Destination Code:";
            // 
            // textBoxManufacturerCode
            // 
            this.textBoxManufacturerCode.Location = new System.Drawing.Point(118, 65);
            this.textBoxManufacturerCode.Name = "textBoxManufacturerCode";
            this.textBoxManufacturerCode.Size = new System.Drawing.Size(100, 20);
            this.textBoxManufacturerCode.TabIndex = 8;
            // 
            // textBoxGameTitle
            // 
            this.textBoxGameTitle.Location = new System.Drawing.Point(118, 39);
            this.textBoxGameTitle.Name = "textBoxGameTitle";
            this.textBoxGameTitle.Size = new System.Drawing.Size(206, 20);
            this.textBoxGameTitle.TabIndex = 7;
            // 
            // textBoxROMEntryPoint
            // 
            this.textBoxROMEntryPoint.Location = new System.Drawing.Point(118, 13);
            this.textBoxROMEntryPoint.Name = "textBoxROMEntryPoint";
            this.textBoxROMEntryPoint.Size = new System.Drawing.Size(100, 20);
            this.textBoxROMEntryPoint.TabIndex = 5;
            this.textBoxROMEntryPoint.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.validateHexInput);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Manufacturer Code:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Game Title:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 305);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nintendo Logo:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ROM Entry Point:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 16);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(54, 13);
            this.label15.TabIndex = 35;
            this.label15.Text = "FileName:";
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.Location = new System.Drawing.Point(66, 13);
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.Size = new System.Drawing.Size(538, 20);
            this.textBoxFileName.TabIndex = 34;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxFileName);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Location = new System.Drawing.Point(12, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(610, 40);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // restoreNintendoLogoToolStripMenuItem
            // 
            this.restoreNintendoLogoToolStripMenuItem.Name = "restoreNintendoLogoToolStripMenuItem";
            this.restoreNintendoLogoToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.restoreNintendoLogoToolStripMenuItem.Text = "&Restore Nintendo Logo";
            this.restoreNintendoLogoToolStripMenuItem.Click += new System.EventHandler(this.restoreNintendoLogoToolStripMenuItem_Click);
            // 
            // enableColorGameBoyOptionsToolStripMenuItem
            // 
            this.enableColorGameBoyOptionsToolStripMenuItem.Name = "enableColorGameBoyOptionsToolStripMenuItem";
            this.enableColorGameBoyOptionsToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.enableColorGameBoyOptionsToolStripMenuItem.Text = "&Enable Color Game Boy Options";
            this.enableColorGameBoyOptionsToolStripMenuItem.Click += new System.EventHandler(this.enableColorGameBoyOptionsToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 451);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Game Boy ROM Analyzer / Header Editor / Checksum Fixer";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem writeROMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fixChecksumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxIsSuperGameBoy;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxHeaderChecksum;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxVersionNumber;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxManufacturerCode;
        private System.Windows.Forms.TextBox textBoxGameTitle;
        private System.Windows.Forms.TextBox textBoxROMEntryPoint;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox comboBoxRAMSize;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboBoxROMSize;
        private System.Windows.Forms.ComboBox comboBoxCartridgeType;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox checkBoxColorGameBoy;
        private System.Windows.Forms.CheckBox checkBoxGameBoy;
        private System.Windows.Forms.TextBox textBoxNintendoLogo;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBoxDestinationCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxGlobalChecksum;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxNewLicensee;
        private System.Windows.Forms.ComboBox comboBoxOldLicensee;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxCorrectHeaderChecksum;
        private System.Windows.Forms.ToolStripMenuItem restoreNintendoLogoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableColorGameBoyOptionsToolStripMenuItem;
    }
}

