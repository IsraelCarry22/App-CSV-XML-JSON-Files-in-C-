namespace App_CSV_XML_JSON_Files_in_C_
{
    partial class FormNombreColumna
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
            this.NombreColumnaTXT = new System.Windows.Forms.TextBox();
            this.AceptarBTM = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // NombreColumnaTXT
            // 
            this.NombreColumnaTXT.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NombreColumnaTXT.Location = new System.Drawing.Point(12, 49);
            this.NombreColumnaTXT.Name = "NombreColumnaTXT";
            this.NombreColumnaTXT.Size = new System.Drawing.Size(175, 24);
            this.NombreColumnaTXT.TabIndex = 0;
            this.NombreColumnaTXT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NombreColumnaTXT_KeyPress);
            // 
            // AceptarBTM
            // 
            this.AceptarBTM.Location = new System.Drawing.Point(112, 82);
            this.AceptarBTM.Name = "AceptarBTM";
            this.AceptarBTM.Size = new System.Drawing.Size(75, 23);
            this.AceptarBTM.TabIndex = 1;
            this.AceptarBTM.Text = "Aceptar";
            this.AceptarBTM.UseVisualStyleBackColor = true;
            this.AceptarBTM.Click += new System.EventHandler(this.AceptarBTM_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nombra la columna";
            // 
            // FormNombreColumna
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(197, 111);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AceptarBTM);
            this.Controls.Add(this.NombreColumnaTXT);
            this.Name = "FormNombreColumna";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox NombreColumnaTXT;
        private System.Windows.Forms.Button AceptarBTM;
        private System.Windows.Forms.Label label1;
    }
}