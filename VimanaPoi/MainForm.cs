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
    public partial class MainForm : Form
    {                                
        public MainForm()
        {
            InitializeComponent();                        
            this.StartPosition = FormStartPosition.CenterScreen;
            Init_Login();        
        }
        private void Init_Login()
        {
            nrmlPicBox.Visible = true;
            opPicBox.Visible = false;
            setPixBox.Visible = false;
            settingBtn.Enabled = true;
            stackPanel1.Visible = false;                       
            disableAllMenu();
            logInPnl.Visible = true;
            logoutBtn.Visible = false;
            empIdLbl.Visible = false;
            empIdIndLbl.Text = "No Employee Logged In";
            greetLbl.Visible = false;
        }

        private void MainfestionComboBoxes()
        {
            ComboBox[] parts = new ComboBox[] {
                t1part1,t2part1,t2part2,t2part3,t2part4,t3part1,t4part1,t4part2,t5part1,t6part1,t6part2,t7part1,t7part2
            };
            ComboBox[] opr = new ComboBox[] {
                t1opr1,t2opr1,t2opr2,t2opr3,t2opr4,t3opr1,t4opr1,t4opr2,t5opr1,t6opr1,t6opr2,t7opr1,t7opr2
            };
            ControlManifest cm = new ControlManifest(parts, opr);
        }

        private void disableAllMenu()
        {
            singProgBtn0.Enabled = false;
            multProgBtn1.Enabled = false;
            singFixtBtn2.Enabled = false;
            FixtPallBtn3.Enabled = false;
            multPartBtn4.Enabled = false;
            twoPartBtn5.Enabled = false;
            twoPathBtn6.Enabled = false;
        }
        private void doLogin()
        {
            if (curEmpIdTxt.Text != "")
            {               
                logInPnl.Visible = false;
                logoutBtn.Visible = true;
                empIdIndLbl.Text = "Current Employee :";
                empIdLbl.Visible = true;
                empIdLbl.Text = curEmpIdTxt.Text;
                nrmlPicBox.Visible = false;
                if (isSetup.Checked) { empIdLbl.Text += " (Setup)"; setPixBox.Visible = true; } else { opPicBox.Visible = true; }
                settingBtn.Enabled = false;
                //logBtn.Enabled = false;
                loadDefaults();
                greetLbl.Visible = true;
                string cmd = getCurrTime() + "|EMP|" + curEmpIdTxt.Text + "\n";
                MainfestionComboBoxes();
            }
            else
            {
                MessageBox.Show("Enter your employee ID", "Error");
            }
        }
        //loading default status settings
        private void loadDefaults()
        {            
            singProgBtn0.Enabled = Properties.Settings.Default.singprog;
            multProgBtn1.Enabled = Properties.Settings.Default.multprog;
            singFixtBtn2.Enabled = Properties.Settings.Default.singfixt;
            FixtPallBtn3.Enabled = Properties.Settings.Default.pallfixt;
            multPartBtn4.Enabled = Properties.Settings.Default.multpart;
            twoPartBtn5.Enabled = Properties.Settings.Default.twopart;
            twoPathBtn6.Enabled = Properties.Settings.Default.twopath;             
        }
        //switch start and stop
        private void switchBtn(int flg, Button[] btn)
        {
            Button tst = btn[0];
            Button tsp = btn[1];
            if (flg == 1)
            {
                tst.Enabled = false;
                tsp.Enabled = true;
            }
            if (flg == 2)
            {
                tst.Enabled = true;
                tsp.Enabled = false;
            }
        }
        //current time
        public string getCurrTime()
        {
            return DateTime.UtcNow.ToString("o");
        }
        //capturing key event        
        void curEmpIdTxt_GotFocus(Object sender, KeyEventArgs e)
        {
            //MessageBox.Show(e.KeyCode.ToString());
            if (e.KeyCode.ToString() == "Return")
            {
                doLogin();
            }
        }
        
        private void logoutBtn_Click(object sender, EventArgs e)
        {
            Init_Login();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            doLogin();
        }

        private void settingBtn_Click(object sender, EventArgs e)
        {
            SettingsDialog objCustomDialogBox = new SettingsDialog();
            objCustomDialogBox.ShowDialog();
            objCustomDialogBox = null;
        }

        private void menuButton_Click(object sender, EventArgs e)
        {
            greetLbl.Visible = false;
            stackPanel1.Visible = true;
            Button n = (Button)sender;
            string name = n.Name;
            int index = Int32.Parse(name.Substring(name.Length - 1, 1));
            stackPanel1.SelectedIndex = index;
        }     

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
                   
    }
}
