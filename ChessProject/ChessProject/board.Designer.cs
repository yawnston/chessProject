namespace ChessProject
{
    partial class board
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(board));
            this.savequitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // savequitButton
            // 
            this.savequitButton.Location = new System.Drawing.Point(520, 31);
            this.savequitButton.Name = "savequitButton";
            this.savequitButton.Size = new System.Drawing.Size(138, 45);
            this.savequitButton.TabIndex = 0;
            this.savequitButton.Text = "Quit Game";
            this.savequitButton.UseVisualStyleBackColor = true;
            this.savequitButton.Click += new System.EventHandler(this.savequitButton_Click);
            // 
            // board
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 496);
            this.Controls.Add(this.savequitButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "board";
            this.Text = "board";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button savequitButton;
    }
}