namespace SpreadsheetGUI
{
    partial class ConcatForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.CalculationCell = new System.Windows.Forms.TextBox();
            this.Perform = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(401, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(442, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please Select Cells you could like to concat: ";
            // 
            // CalculationCell
            // 
            this.CalculationCell.Location = new System.Drawing.Point(238, 159);
            this.CalculationCell.Name = "CalculationCell";
            this.CalculationCell.Size = new System.Drawing.Size(809, 31);
            this.CalculationCell.TabIndex = 1;
            // 
            // Perform
            // 
            this.Perform.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Perform.Location = new System.Drawing.Point(501, 389);
            this.Perform.Name = "Perform";
            this.Perform.Size = new System.Drawing.Size(230, 110);
            this.Perform.TabIndex = 4;
            this.Perform.Text = "Enter";
            this.Perform.UseVisualStyleBackColor = false;
            this.Perform.Click += new System.EventHandler(this.Perform_Click);
            // 
            // ConcatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 655);
            this.Controls.Add(this.Perform);
            this.Controls.Add(this.CalculationCell);
            this.Controls.Add(this.label1);
            this.Name = "ConcatForm";
            this.Text = "Concatnation Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CalculationCell;
        private System.Windows.Forms.Button Perform;
    }
}