namespace TournamentManager
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtGamerTag = new System.Windows.Forms.TextBox();
            this.dfdfdadfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.btnDeleteChecked = new System.Windows.Forms.Button();
            this.btnTournamentScreen = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(34, 209);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(290, 52);
            this.button1.TabIndex = 0;
            this.button1.Text = "Add Gamer";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnAddGamer_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(153, 71);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(171, 27);
            this.txtUsername.TabIndex = 1;
            // 
            // txtGamerTag
            // 
            this.txtGamerTag.Location = new System.Drawing.Point(153, 147);
            this.txtGamerTag.Name = "txtGamerTag";
            this.txtGamerTag.Size = new System.Drawing.Size(171, 27);
            this.txtGamerTag.TabIndex = 2;
            // 
            // dfdfdadfToolStripMenuItem
            // 
            this.dfdfdadfToolStripMenuItem.Name = "dfdfdadfToolStripMenuItem";
            this.dfdfdadfToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(79, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(634, 46);
            this.label1.TabIndex = 3;
            this.label1.Text = "Video Game Tournament Manager";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Enter Username";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Enter Gamer Tag";
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Location = new System.Drawing.Point(395, 71);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(178, 29);
            this.btnDeleteAll.TabIndex = 6;
            this.btnDeleteAll.Text = "Delete All";
            this.btnDeleteAll.UseVisualStyleBackColor = true;
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // btnDeleteChecked
            // 
            this.btnDeleteChecked.Location = new System.Drawing.Point(579, 71);
            this.btnDeleteChecked.Name = "btnDeleteChecked";
            this.btnDeleteChecked.Size = new System.Drawing.Size(178, 29);
            this.btnDeleteChecked.TabIndex = 7;
            this.btnDeleteChecked.Text = "Delete Checked";
            this.btnDeleteChecked.UseVisualStyleBackColor = true;
            this.btnDeleteChecked.Click += new System.EventHandler(this.btnDeleteChecked_Click);
            // 
            // btnTournamentScreen
            // 
            this.btnTournamentScreen.Location = new System.Drawing.Point(34, 267);
            this.btnTournamentScreen.Name = "btnTournamentScreen";
            this.btnTournamentScreen.Size = new System.Drawing.Size(290, 52);
            this.btnTournamentScreen.TabIndex = 8;
            this.btnTournamentScreen.Text = "Start Tournament";
            this.btnTournamentScreen.UseVisualStyleBackColor = true;
            this.btnTournamentScreen.Click += new System.EventHandler(this.btnTournamentScreen_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(533, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "All Gamers";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 340);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(299, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = " Enter a username and gamer tag. Click Add";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 360);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(277, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "gamer to add to the database. Once you";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 380);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(299, 20);
            this.label7.TabIndex = 12;
            this.label7.Text = "have enough players click Start Tournament.";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(34, 400);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(294, 20);
            this.label8.TabIndex = 13;
            this.label8.Text = "8 players are needed to start a tournament.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 551);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnTournamentScreen);
            this.Controls.Add(this.btnDeleteChecked);
            this.Controls.Add(this.btnDeleteAll);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtGamerTag);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button button1;
        private TextBox txtUsername;
        private TextBox txtGamerTag;
        private ToolStripMenuItem dfdfdadfToolStripMenuItem;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnDeleteAll;
        private Button btnDeleteChecked;
        private Button btnTournamentScreen;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
    }
}