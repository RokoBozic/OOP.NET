namespace WorldCupWinForms
{
    partial class FormSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Timer fadeOutTimer;

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
            lblGender = new Label();
            cmbGender = new ComboBox();
            btnConfirm = new Button();
            cmbLanguage = new ComboBox();
            lblLanguage = new Label();
            btnCancel = new Button();
            fadeOutTimer = new System.Windows.Forms.Timer();
            SuspendLayout();
            // 
            // lblGender
            // 
            lblGender.AutoSize = true;
            lblGender.Location = new Point(332, 57);
            lblGender.Name = "lblGender";
            lblGender.Size = new Size(120, 25);
            lblGender.TabIndex = 0;
            lblGender.Text = "Select Gender";
            // 
            // cmbGender
            // 
            cmbGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGender.FormattingEnabled = true;
            cmbGender.Location = new Point(107, 49);
            cmbGender.Name = "cmbGender";
            cmbGender.Size = new Size(182, 33);
            cmbGender.TabIndex = 1;
            // 
            // btnConfirm
            // 
            btnConfirm.Location = new Point(212, 216);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(112, 34);
            btnConfirm.TabIndex = 2;
            btnConfirm.Text = "Confirm";
            btnConfirm.UseVisualStyleBackColor = true;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // cmbLanguage
            // 
            cmbLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLanguage.FormattingEnabled = true;
            cmbLanguage.Location = new Point(107, 124);
            cmbLanguage.Name = "cmbLanguage";
            cmbLanguage.Size = new Size(182, 33);
            cmbLanguage.TabIndex = 3;
            // 
            // lblLanguage
            // 
            lblLanguage.AutoSize = true;
            lblLanguage.Location = new Point(332, 132);
            lblLanguage.Name = "lblLanguage";
            lblLanguage.Size = new Size(140, 25);
            lblLanguage.TabIndex = 4;
            lblLanguage.Text = "Select Language";
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(360, 216);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(112, 34);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // FormSettings
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnCancel);
            Controls.Add(lblLanguage);
            Controls.Add(cmbLanguage);
            Controls.Add(btnConfirm);
            Controls.Add(cmbGender);
            Controls.Add(lblGender);
            Name = "FormSettings";
            Text = "FormSettings";
            Load += FormSettings_Load;
            fadeOutTimer = new System.Windows.Forms.Timer();
            fadeOutTimer.Interval = 30; // 30 ms for smooth fade
            fadeOutTimer.Tick += fadeOutTimer_Tick;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblGender;
        private ComboBox cmbGender;
        private Button btnConfirm;
        private ComboBox cmbLanguage;
        private Label lblLanguage;
        private Button btnCancel;
    }
}