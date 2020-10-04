namespace AspirobotT01
{
    partial class Presentation
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
            this.lblElectricity = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDirty = new System.Windows.Forms.Label();
            this.lblJewel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblPenitence = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblElectricity
            // 
            this.lblElectricity.AutoSize = true;
            this.lblElectricity.Location = new System.Drawing.Point(629, 28);
            this.lblElectricity.Name = "lblElectricity";
            this.lblElectricity.Size = new System.Drawing.Size(13, 13);
            this.lblElectricity.TabIndex = 0;
            this.lblElectricity.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(567, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Dirty:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(567, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Electricity: ";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(28, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(511, 486);
            this.panel1.TabIndex = 3;
            this.panel1.Visible = false;
            // 
            // lblDirty
            // 
            this.lblDirty.AutoSize = true;
            this.lblDirty.Location = new System.Drawing.Point(604, 55);
            this.lblDirty.Name = "lblDirty";
            this.lblDirty.Size = new System.Drawing.Size(13, 13);
            this.lblDirty.TabIndex = 4;
            this.lblDirty.Text = "0";
            // 
            // lblJewel
            // 
            this.lblJewel.AutoSize = true;
            this.lblJewel.Location = new System.Drawing.Point(610, 82);
            this.lblJewel.Name = "lblJewel";
            this.lblJewel.Size = new System.Drawing.Size(13, 13);
            this.lblJewel.TabIndex = 6;
            this.lblJewel.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(567, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Jewel:";
            // 
            // lblPenitence
            // 
            this.lblPenitence.AutoSize = true;
            this.lblPenitence.Location = new System.Drawing.Point(631, 110);
            this.lblPenitence.Name = "lblPenitence";
            this.lblPenitence.Size = new System.Drawing.Size(13, 13);
            this.lblPenitence.TabIndex = 8;
            this.lblPenitence.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(567, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Penitence:";
            // 
            // Presentation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 563);
            this.Controls.Add(this.lblPenitence);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblJewel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblDirty);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblElectricity);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Presentation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aspirobot T-0.1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Presentation_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblElectricity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblDirty;
        private System.Windows.Forms.Label lblJewel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblPenitence;
        private System.Windows.Forms.Label label5;
    }
}

