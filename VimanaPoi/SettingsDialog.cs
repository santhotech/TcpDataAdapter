using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace VimanaPoi
{
    public partial class SettingsDialog : Form
    {        
        public SettingsDialog()
        {
            InitializeComponent();
            this.pwdTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pwdTxtChange);
            textBox1.Text = Properties.Settings.Default.port.ToString();
            checkBox1.Checked = Properties.Settings.Default.singprog;
            checkBox2.Checked = Properties.Settings.Default.multprog;
            checkBox3.Checked = Properties.Settings.Default.singfixt;
            checkBox4.Checked = Properties.Settings.Default.pallfixt;
            checkBox5.Checked = Properties.Settings.Default.multpart;
            checkBox6.Checked = Properties.Settings.Default.twopart;
            checkBox7.Checked = Properties.Settings.Default.twopath;
            dbuname.Text = Properties.Settings.Default.username;
            dbpwd.Text = Properties.Settings.Default.password;
            dbname.Text = Properties.Settings.Default.dbname;
            dbserver.Text = Properties.Settings.Default.server;
            dbport.Text = Properties.Settings.Default.dbport;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowInTaskbar = false;            
            loadMachineNames();
        }

        public void loadMachineNames()
        {
            DBConnect dbc = new DBConnect();   
            ArrayList al = dbc.GetMachineNames();
            machineNames.Items.Clear();
            machineNames.Items.AddRange(al.ToArray());
            machineNames.Text = Properties.Settings.Default.machinename;
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
            Properties.Settings.Default.machinename = machineNames.Text;
            Properties.Settings.Default.Save();
            MessageBox.Show("Settings Saved!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.username = dbuname.Text;
            Properties.Settings.Default.password = dbpwd.Text;
            Properties.Settings.Default.dbname = dbname.Text;
            Properties.Settings.Default.server = dbserver.Text;
            Properties.Settings.Default.dbport = dbport.Text;
            Properties.Settings.Default.Save();
            DBConnect dbc1 = new DBConnect();
            if (dbc1.TestConnection())
            {
                MessageBox.Show("DB Connection Successful! Settings Saved");                
                loadMachineNames();
            }
            else
            {
                MessageBox.Show("DB Connection Failed!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pwdBtn_Click(object sender, EventArgs e)
        {
            CheckLogin();
        }

        private void CheckLogin()
        {
            if (pwdTxt.Text == "TFT001")
            {
                passPnl.Visible = false;
                mainPnl.Visible = true;
            }
            else
            {
                MessageBox.Show("Wrong Password!");
            }
        }

        void pwdTxtChange(Object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                CheckLogin();
            }
        }

    }
}
