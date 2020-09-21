namespace UA_Fiscal_Leocas
{
    partial class ConfirmationWindow
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnConfirmConfirm = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txbConfirmSumm = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCancel.Location = new System.Drawing.Point(12, 73);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Скасувати";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirmConfirm
            // 
            this.btnConfirmConfirm.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnConfirmConfirm.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnConfirmConfirm.Location = new System.Drawing.Point(120, 73);
            this.btnConfirmConfirm.Name = "btnConfirmConfirm";
            this.btnConfirmConfirm.Size = new System.Drawing.Size(75, 30);
            this.btnConfirmConfirm.TabIndex = 1;
            this.btnConfirmConfirm.Text = "Готово";
            this.btnConfirmConfirm.UseVisualStyleBackColor = false;
            this.btnConfirmConfirm.Click += new System.EventHandler(this.btnConfirmConfirm_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Введіть сумму повторно";
            // 
            // txbConfirmSumm
            // 
            this.txbConfirmSumm.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txbConfirmSumm.Location = new System.Drawing.Point(56, 38);
            this.txbConfirmSumm.Name = "txbConfirmSumm";
            this.txbConfirmSumm.Size = new System.Drawing.Size(100, 29);
            this.txbConfirmSumm.TabIndex = 3;
            this.txbConfirmSumm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txbConfirmSumm.TextChanged += new System.EventHandler(this.txbConfirmSumm_TextChanged);
            this.txbConfirmSumm.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbConfirmSumm_KeyPress);
            // 
            // ConfirmationWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(207, 114);
            this.Controls.Add(this.txbConfirmSumm);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConfirmConfirm);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Location = new System.Drawing.Point(700, 100);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfirmationWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Підтвердження суми";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnConfirmConfirm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbConfirmSumm;
    }
}