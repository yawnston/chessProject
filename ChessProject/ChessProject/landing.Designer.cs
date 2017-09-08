namespace ChessProject
{
    partial class landing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(landing));
            this.cPlayButton = new System.Windows.Forms.Button();
            this.hPlayButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.colorBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cPlayButton
            // 
            this.cPlayButton.Location = new System.Drawing.Point(31, 12);
            this.cPlayButton.Name = "cPlayButton";
            this.cPlayButton.Size = new System.Drawing.Size(142, 40);
            this.cPlayButton.TabIndex = 0;
            this.cPlayButton.Text = "Play versus A.I.";
            this.cPlayButton.UseVisualStyleBackColor = true;
            this.cPlayButton.Click += new System.EventHandler(this.startButtonHandler);
            // 
            // hPlayButton
            // 
            this.hPlayButton.Location = new System.Drawing.Point(31, 100);
            this.hPlayButton.Name = "hPlayButton";
            this.hPlayButton.Size = new System.Drawing.Size(142, 40);
            this.hPlayButton.TabIndex = 1;
            this.hPlayButton.Text = "2 player";
            this.hPlayButton.UseVisualStyleBackColor = true;
            this.hPlayButton.Click += new System.EventHandler(this.startButtonHandler);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(31, 227);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(142, 40);
            this.exitButton.TabIndex = 2;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // colorBox
            // 
            this.colorBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.colorBox.FormattingEnabled = true;
            this.colorBox.Items.AddRange(new object[] {
            "Random",
            "White",
            "Black"});
            this.colorBox.Location = new System.Drawing.Point(31, 58);
            this.colorBox.Name = "colorBox";
            this.colorBox.Size = new System.Drawing.Size(141, 21);
            this.colorBox.TabIndex = 3;
            // 
            // landing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(207, 279);
            this.Controls.Add(this.colorBox);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.hPlayButton);
            this.Controls.Add(this.cPlayButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "landing";
            this.Text = "landing form";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cPlayButton;
        private System.Windows.Forms.Button hPlayButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.ComboBox colorBox;
    }
}

