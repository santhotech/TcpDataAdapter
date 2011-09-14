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
        ControlManifest com;
        Commands cmd;
        TcpActions tcp;
        Dictionary<string, ControlContainer> _manifest;
        public MainForm()
        {
            InitializeComponent();
            _manifest = new Dictionary<string, ControlContainer>();        
            this.StartPosition = FormStartPosition.CenterScreen;
            Init_Login();
            com = new ControlManifest();
            ManifestControls();
            cmd = new Commands(com);
            tcp = new TcpActions();
            tcp.ClientCountChanged += new TcpActions.ClientCountChangedEventHandler(tcp_ClientCountChanged);
            string port = Properties.Settings.Default.port.ToString();
            sockStatLbl.Text = "Listening on port " + port;
        }

        public void Form1_Closing(object sender, CancelEventArgs cArgs)
        {
            Environment.Exit(0);
            if (sender == this)
            {
                //MessageBox.Show("Form Closing Event....");
                if (sender != this)
                {
                    cArgs.Cancel = true;
                }
            }
        }

        private void tcp_ClientCountChanged(int count)
        {            
            cliNumStatLbl.BeginInvoke((MethodInvoker)(() => cliNumStatLbl.Text  = count.ToString() + " " + GetClientMsg(count) + " Connected"));
        }

        private string GetClientMsg(int count)
        {
            if (count > 1)
            {
                return "Clients";
            }
            else
                return "Client";
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

        private void ManifestControls()
        {
            string cmdFormat, cmdStop;

            com.tbl1strt = new Control[] { t1part1, t1opr1 };
            com.tbl1stop = new Control[] { t1gpTxt, t1bpTxt };
            cmdFormat = "{0}|part-type|{1}\n{0}|operation-type|{2}\n";
            cmdStop = "{0}|part-count-good|{1}\n{0}|part-count-bad|{2}\n";
            _manifest.Add("1", new ControlContainer { strt = com.tbl1strt, stop = com.tbl1stop, cmdStrt = cmdFormat, cmdStop = cmdStop, strtBtn = t1strt, stopBtn = t1stop });
            
            com.tbl3strt = new Control[] { t3part1, t3opr1, t3fixPosnTxt };
            com.tbl3stop = new Control[] { t3gpTxt, t3bpTxt };
            cmdFormat = "{0}|part-type|{1}\n{0}|operation-type|{2}\n{0}|fixture-positions|{3}\n";
            cmdStop = "{0}|part-count-good|{1}\n{0}|part-count-bad|{2}\n";
            _manifest.Add("3", new ControlContainer { strt = com.tbl3strt, stop = com.tbl3stop, cmdStrt = cmdFormat, cmdStop = cmdStop, strtBtn = t3strt, stopBtn = t3stop });            

            com.tbl5strt = new Control[] { t5part1, t5opr1, t5noPrtTxt };
            com.tbl5stop = new Control[] { t5gpTxt, t5bpTxt };
            cmdFormat = "{0}|part-type|{1}\n{0}|operation-type|{2}\n{0}|parts-per-workpiece|{3}\n";
            cmdStop = "{0}|part-count-good|{1}\n{0}|part-count-bad|{2}\n";
            _manifest.Add("5", new ControlContainer { strt = com.tbl5strt, stop = com.tbl5stop, cmdStrt = cmdFormat, cmdStop = cmdStop, strtBtn = t5strt, stopBtn = t5stop });

            com.tbl6strt = new Control[] { t6part1, t6opr1, t6part2, t6opr2 };
            com.tbl6stop = new Control[] { t6gp1Txt, t6bp1Txt, t6gp2Txt, t6bp2Txt };
            cmdFormat = "{0}|multi-part-config|nparts=2;part-type1={1};operation-type1={2};part-type2={3};operation-type2={4}\n";
            cmdStop = "{0}|part-count-multiple|good1={1};bad1={2};good2={3};bad2={4};\n";
            _manifest.Add("6", new ControlContainer { strt = com.tbl6strt, stop = com.tbl6stop, cmdStrt = cmdFormat, cmdStop = cmdStop, strtBtn = t6strt, stopBtn = t6stop });

            com.tbl7strt = new Control[] { t7part1, t7opr1, t7part2, t7opr2 };
            com.tbl7stop = new Control[] { t7gp1Txt, t7bp1Txt, t7gp2Txt, t7bp2Txt };
            cmdFormat = "{0}|path-config|path=1;part-type1={1};operation-type1={2};path=2;part-type={3};operation-type={4}\n";
            cmdStop = "{0}|part-count-multiple|good1={1};bad1={2};good2={3};bad2={4};\n";
            _manifest.Add("7", new ControlContainer { strt = com.tbl7strt, stop = com.tbl7stop, cmdStrt = cmdFormat, cmdStop = cmdStop, strtBtn = t7strt, stopBtn = t7stop });

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
            Validations val = new Validations();
            string error;
            if((error = val.ValidateLogin(curEmpIdTxt.Text)) == String.Empty)
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
                Error(error);
            }            
        }           

        private void Error(string msg)
        {
            MessageBox.Show(msg,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
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
        
        public string getCurrTime()
        {
            return DateTime.UtcNow.ToString("o");
        }
            
        void curEmpIdTxt_GotFocus(Object sender, KeyEventArgs e)
        {            
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
            com.ReadUnRead(_manifest[(index + 1).ToString()].stop, false);
        }     

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void AllButtons_click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            string typ = b.Name.Substring(2, 4);
            string ind = b.Name.Substring(1, 1);        
            Control[] ctrl;
            string cmdToSend;
            if (typ == "strt") { ctrl = _manifest[ind].strt; cmdToSend = _manifest[ind].cmdStrt; } else { ctrl = _manifest[ind].stop; cmdToSend = _manifest[ind].cmdStop; }
            if (com.ValidateControls(ctrl))
            {
                b.Enabled = false;
                tcp.sndData(String.Format(cmdToSend, com.GetData(ctrl)));
                if (typ == "strt") { com.ReadUnRead(_manifest[ind].strt, false); com.ReadUnRead(_manifest[ind].stop, true); disableAllMenu(); _manifest[ind].stopBtn.Enabled = true; }
                if (typ == "stop") { com.ReadUnRead(_manifest[ind].stop, false); com.ReadUnRead(_manifest[ind].strt, true); loadDefaults(); _manifest[ind].strtBtn.Enabled = true; }
            }
            else
            {
                Error("Enter All the fields");
            }
        }                           
    }
}
