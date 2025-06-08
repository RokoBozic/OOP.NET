using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorldCupWinForms
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();

            // Enable the form to capture key presses for Enter/Esc shortcuts
            this.KeyPreview = true;
            this.KeyDown += FormSettings_KeyDown;

            // Fill combo boxes for gender and language selection
            cmbGender.Items.AddRange(new[] { "Men", "Women" });
            cmbLanguage.Items.AddRange(new[] { "English", "Croatian" });
            cmbGender.SelectedIndex = 0;
            cmbLanguage.SelectedIndex = 0;

            // Set initial opacity for fade-out effect (animation)
            this.Opacity = 1.0;
        }

        private void FormSettings_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnConfirm.PerformClick();   // Simulate clicking Confirm
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                btnCancel.PerformClick();    // Simulate clicking Cancel
                e.Handled = true;
            }
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            // Save selected gender and language to config file
            string gender = cmbGender.SelectedItem.ToString().ToLower();
            string language = cmbLanguage.SelectedItem.ToString().ToLower();
            Directory.CreateDirectory("Resources");
            var configLines = new[]
            {
                "source=api",
                $"gender={gender}",
                $"language={language}"
            };
            File.WriteAllLines("Resources/config.txt", configLines);

            // Start fade-out animation instead of closing immediately
            // This makes the transition to MainForm smooth and visually appealing
            fadeOutTimer.Start();
        }

        // Timer tick event for fade-out animation
        // Gradually decreases the form's opacity until it is fully transparent, then closes the form
        private void fadeOutTimer_Tick(object sender, EventArgs e)
        {
            this.Opacity -= 0.05; // Reduce opacity by 0.05 each tick
            if (this.Opacity <= 0)
            {
                fadeOutTimer.Stop();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Cancel and close the settings form
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
