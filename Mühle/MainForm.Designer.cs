namespace Mühle
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtp1 = new System.Windows.Forms.TextBox();
            this.txtturn = new System.Windows.Forms.TextBox();
            this.txtp2 = new System.Windows.Forms.TextBox();
            this.btnsavegame = new System.Windows.Forms.Button();
            this.btnloadgame = new System.Windows.Forms.Button();
            this.btnresetgame = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtp1
            // 
            this.txtp1.Enabled = false;
            this.txtp1.Location = new System.Drawing.Point(229, 12);
            this.txtp1.Name = "txtp1";
            this.txtp1.Size = new System.Drawing.Size(20, 20);
            this.txtp1.TabIndex = 0;
            // 
            // txtturn
            // 
            this.txtturn.Enabled = false;
            this.txtturn.Location = new System.Drawing.Point(255, 12);
            this.txtturn.Name = "txtturn";
            this.txtturn.Size = new System.Drawing.Size(100, 20);
            this.txtturn.TabIndex = 1;
            // 
            // txtp2
            // 
            this.txtp2.Enabled = false;
            this.txtp2.Location = new System.Drawing.Point(361, 12);
            this.txtp2.Name = "txtp2";
            this.txtp2.Size = new System.Drawing.Size(20, 20);
            this.txtp2.TabIndex = 2;
            // 
            // btnsavegame
            // 
            this.btnsavegame.Location = new System.Drawing.Point(12, 12);
            this.btnsavegame.Name = "btnsavegame";
            this.btnsavegame.Size = new System.Drawing.Size(42, 23);
            this.btnsavegame.TabIndex = 3;
            this.btnsavegame.Text = "Save";
            this.btnsavegame.UseVisualStyleBackColor = true;
            this.btnsavegame.Click += new System.EventHandler(this.btnsavegame_Click);
            // 
            // btnloadgame
            // 
            this.btnloadgame.Location = new System.Drawing.Point(60, 12);
            this.btnloadgame.Name = "btnloadgame";
            this.btnloadgame.Size = new System.Drawing.Size(40, 23);
            this.btnloadgame.TabIndex = 4;
            this.btnloadgame.Text = "Load";
            this.btnloadgame.UseVisualStyleBackColor = true;
            this.btnloadgame.Click += new System.EventHandler(this.btnloadgame_Click);
            // 
            // btnresetgame
            // 
            this.btnresetgame.Location = new System.Drawing.Point(106, 12);
            this.btnresetgame.Name = "btnresetgame";
            this.btnresetgame.Size = new System.Drawing.Size(43, 23);
            this.btnresetgame.TabIndex = 5;
            this.btnresetgame.Text = "Reset";
            this.btnresetgame.UseVisualStyleBackColor = true;
            this.btnresetgame.Click += new System.EventHandler(this.btnresetgame_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1330, 824);
            this.Controls.Add(this.btnresetgame);
            this.Controls.Add(this.btnloadgame);
            this.Controls.Add(this.btnsavegame);
            this.Controls.Add(this.txtp2);
            this.Controls.Add(this.txtturn);
            this.Controls.Add(this.txtp1);
            this.Name = "Form1";
            this.Text = "Mühle";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtp1;
        private System.Windows.Forms.TextBox txtturn;
        private System.Windows.Forms.TextBox txtp2;
        private System.Windows.Forms.Button btnsavegame;
        private System.Windows.Forms.Button btnloadgame;
        private System.Windows.Forms.Button btnresetgame;
    }
}

