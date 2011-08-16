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
        public ArrayList clients = new ArrayList();
        public Hashtable tblList = new Hashtable();
        public Hashtable lblList = new Hashtable();
        int curFlag = 1;
        int port = 9999;
        public MainForm()
        {
            InitializeComponent();
            sockStatLbl.Text = "Socket Open on port " + port;
            this.StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            tblList.Add(1, singProgTbl);
            tblList.Add(2, multProgTbl);
            tblList.Add(3, singFixtTbl);
            tblList.Add(4, fixtPallTbl);
            tblList.Add(5, multPartTbl);
            tblList.Add(6, twoPartTbl);
            tblList.Add(7, twoPathTbl);
            lblList.Add(1, "Single Program");
            lblList.Add(2, "Multiple Programs");
            lblList.Add(3, "Multiple Parts, Single Fixture");
            lblList.Add(4, "Pallet with Fixtures");
            lblList.Add(5, "Multiple Similar Parts, Single Workpiece");
            lblList.Add(6, "Two Different Parts, Single Workpiece");
            lblList.Add(7, "Two Parts, Two Paths");
        }
        private void Init_Login()
        {
            settingBtn.Enabled = true;
            //logBtn.Enabled = true;
            hideAllTbl();
            stackPanel1.Visible = false;
            t1strt.Visible = false;
            t1stop.Visible = false;
            cntPnlLblPnl.Visible = false;
            disableAllMenu();
            logInPnl.Visible = true;
            logoutBtn.Visible = false;
            empIdLbl.Visible = false;
            empIdIndLbl.Text = "No Employee Logged In";
            label45.Visible = false;
        }
        private void disableAllMenu()
        {
            singProgBtn.Enabled = false;
            multProgBtn.Enabled = false;
            singFixtBtn.Enabled = false;
            FixtPallBtn.Enabled = false;
            multPartBtn.Enabled = false;
            twoPartBtn.Enabled = false;
            twoPathBtn.Enabled = false;
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
                settingBtn.Enabled = false;
                //logBtn.Enabled = false;
                loadDefaults();
                label45.Visible = true;
                string cmd = getCurrTime() + "|EMP|" + curEmpIdTxt.Text + "\n";                
            }
            else
            {
                MessageBox.Show("Enter your employee ID", "Error");
            }
        }
        //loading default status settings
        private void loadDefaults()
        {
            /*
            singProgBtn.Enabled = Properties.Settings.Default.singprog;
            multProgBtn.Enabled = Properties.Settings.Default.multprog;
            singFixtBtn.Enabled = Properties.Settings.Default.singfixt;
            FixtPallBtn.Enabled = Properties.Settings.Default.pallfixt;
            multPartBtn.Enabled = Properties.Settings.Default.multpart;
            twoPartBtn.Enabled = Properties.Settings.Default.twopart;
            twoPathBtn.Enabled = Properties.Settings.Default.twopath;
             **/
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
        //Hides all tables and clears the label during initlization.
        private void hideAllTbl()
        {
            foreach (DictionaryEntry entry in tblList)
            {
                TableLayoutPanel tlp = (TableLayoutPanel)entry.Value;
                tlp.Visible = false;
            }
            cntPnlLbl.Text = "";
        }
        //Shows the respective table and the label for that table.
        private void showTabl(int tblId)
        {
            TableLayoutPanel tlp = (TableLayoutPanel)tblList[tblId];
            tlp.Show();
            cntPnlLbl.Text = (String)lblList[tblId];
            curFlag = tblId;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Init_Login();
           // Manifest_Entry();
        }


        private void logoutBtn_Click(object sender, EventArgs e)
        {
            Init_Login();
        }
        private void loginBtn_Click(object sender, EventArgs e)
        {
            doLogin();
        }
    }
}
