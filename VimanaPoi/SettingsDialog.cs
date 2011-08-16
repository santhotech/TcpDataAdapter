using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace VimanaPoi
{
    public partial class SettingsDialog : Form
    {
        public SettingsDialog()
        {
            InitializeComponent();
            textBox1.Text = Properties.Settings.Default.port.ToString();
            checkBox1.Checked = Properties.Settings.Default.singprog;
            checkBox2.Checked = Properties.Settings.Default.multprog;
            checkBox3.Checked = Properties.Settings.Default.singfixt;
            checkBox4.Checked = Properties.Settings.Default.pallfixt;
            checkBox5.Checked = Properties.Settings.Default.multpart;
            checkBox6.Checked = Properties.Settings.Default.twopart;
            checkBox7.Checked = Properties.Settings.Default.twopath;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowInTaskbar = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void settingsDialog_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Properties.Settings.Default.port = Int32.Parse(textBox1.Text);
            Properties.Settings.Default.singprog = checkBox1.Checked;
            Properties.Settings.Default.multprog = checkBox2.Checked;
            Properties.Settings.Default.singfixt = checkBox3.Checked;
            Properties.Settings.Default.pallfixt = checkBox4.Checked;
            Properties.Settings.Default.multpart = checkBox5.Checked;
            Properties.Settings.Default.twopart = checkBox6.Checked;
            Properties.Settings.Default.twopath = checkBox7.Checked;
            Properties.Settings.Default.Save();
            this.Close();
        }
    }
}
