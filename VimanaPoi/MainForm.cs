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
            MultiProgTblInit();
        }
        MultiProgTbl td = new MultiProgTbl();
        public void MultiProgTblInit()
        {            
            //textboex of table 2 separately
            td.t2progBox = new TextBox[4] { t2prog1, t2prog2, t2prog3, t2prog4 };
            td.t2partBox = new ComboBox[4] { t2part1, t2part2, t2part3, t2part4 };
            td.t2operBox = new ComboBox[4] { t2opr1, t2opr2, t2opr3, t2opr4 };
            td.t2gpbox = new TextBox[4] { t2gp1, t2gp2, t2gp3, t2gp4 };
            td.t2bpbox = new TextBox[4] { t2bp1, t2bp2, t2bp3, t2bp4 };
            //adding table 2 textbox to array
            var t2sndlist = new List<Control>();
            t2sndlist.AddRange(td.t2progBox);
            t2sndlist.AddRange(td.t2partBox);
            t2sndlist.AddRange(td.t2operBox);
            td.t2snd = t2sndlist.ToArray();
            var t2stplist = new List<TextBox>();
            t2stplist.AddRange(td.t2gpbox);
            t2stplist.AddRange(td.t2bpbox);
            td.t2stp = t2stplist.ToArray();
            td.t2btn = new Button[] { t2strt, t2stop };
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
            empIdIndLbl.Text = "Not Logged In";
            macNameLbl.Text = "N/A";
            greetLbl.Visible = false;
        }

        private void MainfestionComboBoxes()
        {
            ComboBox[] parts = new ComboBox[] {
                t1part1,t2part1,t2part2,t2part3,t2part4,t3part1,t4part1,t4part2,t4part3,t4part4,t5part1,t6part1,t6part2,t7part1,t7part2
            };
            ComboBox[] opr = new ComboBox[] {
                t1opr1,t2opr1,t2opr2,t2opr3,t2opr4,t3opr1,t4opr1,t4opr2,t4opr3,t4opr4,t5opr1,t6opr1,t6opr2,t7opr1,t7opr2
            };            
            ControlManifest cm = new ControlManifest(parts, opr);            
        }
        
        private void ManifestControls()
        {
            string cmdFormat, cmdStop;
            string strtc, stopc;
            Control[] strt, stop;

            com.tbl1strt = new Control[] { t1part1, t1opr1 };
            com.tbl1stop = new Control[] { t1gpTxt, t1bpTxt };
            cmdFormat = "{0}|part-type|{1}\n{0}|operation-type|{2}\n";
            cmdStop = "{0}|part-count-good|{1}\n{0}|part-count-bad|{2}\n";
            strtc = "Part Type - {0}\nOperation Type - {1}";
            stopc = "Good Parts - {0}\nBad Parts - {1}";
            _manifest.Add("1", new ControlContainer { strt = com.tbl1strt, stop = com.tbl1stop, cmdStrt = cmdFormat, cmdStop = cmdStop, strtBtn = tstrt1, stopBtn = tstop1, strtcnf = strtc, stopcnf = stopc });
            
            com.tbl3strt = new Control[] { t3part1, t3opr1, t3fixPosnTxt };
            com.tbl3stop = new Control[] { t3gpTxt, t3bpTxt };
            cmdFormat = "{0}|part-type|{1}\n{0}|operation-type|{2}\n{0}|fixture-positions|{3}\n";
            cmdStop = "{0}|part-count-good|{1}\n{0}|part-count-bad|{2}\n";
            strtc = "Part Type - {0}\nOperation Type - {1}\nFixture Position - {2}";
            stopc = "Good Parts - {0}\nBad Parts - {1}";
            _manifest.Add("3", new ControlContainer { strt = com.tbl3strt, stop = com.tbl3stop, cmdStrt = cmdFormat, cmdStop = cmdStop, strtBtn = tstrt3, stopBtn = tstop3, strtcnf = strtc, stopcnf = stopc });            

            com.tbl5strt = new Control[] { t5part1, t5opr1, t5noPrtTxt };
            com.tbl5stop = new Control[] { t5gpTxt, t5bpTxt };
            cmdFormat = "{0}|part-type|{1}\n{0}|operation-type|{2}\n{0}|parts-per-workpiece|{3}\n";
            cmdStop = "{0}|part-count-good|{1}\n{0}|part-count-bad|{2}\n";
            strtc = "Part Type - {0}\nOperation Type - {1}\nParts per Workpiece - {2}";
            stopc = "Good Parts - {0}\nBad Parts - {1}";
            _manifest.Add("5", new ControlContainer { strt = com.tbl5strt, stop = com.tbl5stop, cmdStrt = cmdFormat, cmdStop = cmdStop, strtBtn = tstrt5, stopBtn = tstop5, strtcnf = strtc, stopcnf = stopc });

            com.tbl6strt = new Control[] { t6part1, t6opr1, t6part2, t6opr2 };
            com.tbl6stop = new Control[] { t6gp1Txt, t6bp1Txt, t6gp2Txt, t6bp2Txt };
            cmdFormat = "{0}|multi-part-config|nparts=2;part-type1={1};operation-type1={2};part-type2={3};operation-type2={4}\n";
            cmdStop = "{0}|part-count-multiple|good1={1};bad1={2};good2={3};bad2={4};\n";
            strtc = "Part 1\n\nPart Type - {0}\nOperation Type - {1}\n\nPart 2\n\nPart Type - {2}\nOperation Type - {3}";
            stopc = "Part 1\n\nGood Parts - {0}\nBad Parts - {1}\n\nPart 2\n\nGood Parts - {2}\nBad Parts - {3}";
            _manifest.Add("6", new ControlContainer { strt = com.tbl6strt, stop = com.tbl6stop, cmdStrt = cmdFormat, cmdStop = cmdStop, strtBtn = tstrt6, stopBtn = tstop6, strtcnf = strtc, stopcnf = stopc });

            com.tbl7strt = new Control[] { t7part1, t7opr1, t7part2, t7opr2 };
            com.tbl7stop = new Control[] { t7gp1Txt, t7bp1Txt, t7gp2Txt, t7bp2Txt };
            cmdFormat = "{0}|path-config|path=1;part-type1={1};operation-type1={2};path=2;part-type={3};operation-type={4}\n";
            cmdStop = "{0}|part-count-multiple|good1={1};bad1={2};good2={3};bad2={4};\n";
            strtc = "Path 1\n\nPart Type - {0}\nOperation Type - {1}\n\nPath 2\n\nPart Type - {2}\nOperation Type - {3}";
            stopc = "Path 1\n\nGood Parts - {0}\nBad Parts - {1}\n\nPath 2\n\nGood Parts - {2}\nBad Parts - {3}";
            _manifest.Add("7", new ControlContainer { strt = com.tbl7strt, stop = com.tbl7stop, cmdStrt = cmdFormat, cmdStop = cmdStop, strtBtn = tstrt7, stopBtn = tstop7, strtcnf = strtc, stopcnf = stopc });

            strt = new Control[] { t4part1 , t4opr1, t4fixPosn1Txt };
            stop = new Control[] { t4gp1Txt, t4bp1Txt };
            cmdFormat = "{0}|pallet-1-fixture-positions|{1}\n{0}|pallet-1-part-type|{2}\n{0}|pallet-1-operation-type|{3}\n";
            cmdStop = "{0}|pallet-1-part-count-good|{1}\n{0}|pallet-1-part-count-bad|{2}\n";
            strtc = "Pallet 1\n\nFixture Position - {2}\nPart Name - {0}\nOperation Type - {1}";
            stopc = "Pallet 1\n\nGood Parts - {0}\nBad Parts - {1}";
            _manifest.Add("8", new ControlContainer { strt = strt, stop = stop, cmdStrt = cmdFormat, cmdStop = cmdStop, strtBtn = tstrt8, stopBtn = tstop8, strtcnf = strtc, stopcnf = stopc });

            strt = new Control[] { t4part2, t4opr2, t4fixPosn2Txt };
            stop = new Control[] { t4gp2Txt, t4bp2Txt };
            cmdFormat = "{0}|pallet-2-fixture-positions|{1}\n{0}|pallet-2-part-type|{2}\n{0}|pallet-2-operation-type|{3}\n";
            cmdStop = "{0}|pallet-2-part-count-good|{1}\n{0}|pallet-2-part-count-bad|{2}\n";
            strtc = "Pallet 2\n\nFixture Position - {2}\nPart Name - {0}\nOperation Type - {1}";
            stopc = "Pallet 2\n\nGood Parts - {0}\nBad Parts - {1}";
            _manifest.Add("9", new ControlContainer { strt = strt, stop = stop, cmdStrt = cmdFormat, cmdStop = cmdStop, strtBtn = tstrt9, stopBtn = tstop9, strtcnf = strtc, stopcnf = stopc });

            strt = new Control[] { t4part3, t4opr3, t4fixPosn3Txt };
            stop = new Control[] { t4gp3Txt, t4bp3Txt };
            cmdFormat = "{0}|pallet-3-fixture-positions|{1}\n{0}|pallet-3-part-type|{2}\n{0}|pallet-3-operation-type|{3}\n";
            cmdStop = "{0}|pallet-3-part-count-good|{1}\n{0}|pallet-3-part-count-bad|{2}\n";
            strtc = "Pallet 3\n\nFixture Position - {2}\nPart Name - {0}\nOperation Type - {1}";
            stopc = "Pallet 3\n\nGood Parts - {0}\nBad Parts - {1}";
            _manifest.Add("10", new ControlContainer { strt = strt, stop = stop, cmdStrt = cmdFormat, cmdStop = cmdStop, strtBtn = tstrt10, stopBtn = tstop10, strtcnf = strtc, stopcnf = stopc });

            strt = new Control[] { t4part4, t4opr4, t4fixPosn4Txt };
            stop = new Control[] { t4gp4Txt, t4bp4Txt };
            cmdFormat = "{0}|pallet-4-fixture-positions|{1}\n{0}|pallet-4-part-type|{2}\n{0}|pallet-4-operation-type|{3}\n";
            cmdStop = "{0}|pallet-4-part-count-good|{1}\n{0}|pallet-4-part-count-bad|{2}\n";
            strtc = "Pallet 4\n\nFixture Position - {2}\nPart Name - {0}\nOperation Type - {1}";
            stopc = "Pallet 4\n\nGood Parts - {0}\nBad Parts - {1}";
            _manifest.Add("11", new ControlContainer { strt = strt, stop = stop, cmdStrt = cmdFormat, cmdStop = cmdStop, strtBtn = tstrt11, stopBtn = tstop11, strtcnf = strtc, stopcnf = stopc });


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
                cmd += getCurrTime() + "|EMP-TYPE|setup\n";
                tcp.sndData(cmd);                
                MainfestionComboBoxes();
                macNameLbl.Text = Properties.Settings.Default.machinename;
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
            isOnMultiPall = false;
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
        int palletCnt = 0;
        private void AllButtons_click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            string typ = b.Name.Substring(1, 4);
            string ind = string.Empty;
            if(b.Name.Length == 6)
            ind = b.Name.Substring(5, 1);      
            else if(b.Name.Length == 7)
            ind = b.Name.Substring(5, 2);   
            Control[] ctrl;
            string cmdToSend;
            string cmdToShow;
            if (typ == "strt") 
            {
                if (isOnMultiPall) { palletCnt++; }
                ctrl = _manifest[ind].strt; 
                cmdToSend = _manifest[ind].cmdStrt;
                cmdToShow = _manifest[ind].strtcnf; 
            } 
            else 
            {
                if (isOnMultiPall) { palletCnt--; }
                ctrl = _manifest[ind].stop; 
                cmdToSend = _manifest[ind].cmdStop;
                cmdToShow = _manifest[ind].stopcnf;
            }
            
            object[] astatus = new object[1];
            astatus = com.CheckOk(ctrl);
            string status = (string)astatus[0];
            if (status  == "true")
            {
                if (MessageBox.Show(String.Format(cmdToShow, com.GetShowData(ctrl)), "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    tcp.sndData(String.Format(cmdToSend, com.GetData(ctrl)));
                    if (typ == "strt") { com.ReadUnRead(_manifest[ind].strt, false); com.ReadUnRead(_manifest[ind].stop, true); disableAllMenu(); _manifest[ind].stopBtn.Enabled = true; }
                    if (typ == "stop") { com.ReadUnRead(_manifest[ind].stop, false); com.ReadUnRead(_manifest[ind].strt, true); if (palletCnt == 0) { loadDefaults(); } _manifest[ind].strtBtn.Enabled = true; }
                    b.Enabled = false;
                }
            }
            else
            {
                this.ActiveControl = (Control)astatus[1];                
                Error(status);
            }
        }
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
        public void getFrmMultProgTbl(int flg)
        {
            string cmd = string.Empty;
            string err = string.Empty;
            int j = 1;
            int errCnt = 0;
            Hashtable h = new Hashtable();
            h.Add("prg", "Program");
            h.Add("prt", "Part Name");
            h.Add("opr", "Operation");
            h.Add("gp", "Good Parts");
            h.Add("bp", "Bad Parts");
            var t = new List<Control>();
            var k = new List<Control>();
            Control[] m;
            Control[] n;
            if (flg == 1)
            {
                string showCmd = string.Empty;
                Control tb = new Control();
                tb = null;
                for (int i = 0; i < 4; i++)
                {
                    if (td.t2progBox[i].Text == "" && td.t2partBox[i].Text == "" && td.t2operBox[i].Text == "")
                    {
                        t.Add(td.t2progBox[i]);
                        t.Add(td.t2partBox[i]);
                        t.Add(td.t2operBox[i]);
                        errCnt++;
                        if (errCnt == 4)
                        {
                            err += "Enter atleast one row of data";
                        }
                    }
                    else if (td.t2progBox[i].Text != "" && td.t2partBox[i].Text != "" && td.t2operBox[i].Text != "")
                    {
                        cmd += "prgm" + j + "=" + td.t2progBox[i].Text + ";ptype" + j + "=" + td.t2partBox[i].Text + ";otype" + j + "=" + td.t2operBox[i].Text + ";";
                        t.Add(td.t2progBox[i]);
                        t.Add(td.t2partBox[i]);
                        t.Add(td.t2operBox[i]);
                        k.Add(td.t2gpbox[i]);
                        k.Add(td.t2bpbox[i]);
                        showCmd += "Program " + j + " - " + td.t2progBox[i].Text + "\nPart " + j + " - " + td.t2partBox[i].Text + "\nOperation " + j + " - " + td.t2operBox[i].Text + "\n\n";
                    }
                    else if (td.t2progBox[i].Text != "" || td.t2partBox[i].Text != "" || td.t2operBox[i].Text != "")
                    {
                        if (td.t2progBox[i].Text == "") { if (tb == null) { tb = td.t2progBox[i]; } err += "Please enter " + h[td.t2progBox[i].Name.ToString().Substring(2, 3)] + " " + (i + 1) + Environment.NewLine; }
                        if (td.t2partBox[i].Text == "") { if (tb == null) { tb = td.t2partBox[i]; } err += "Please enter " + h[td.t2partBox[i].Name.ToString().Substring(2, 3)] + " " + (i + 1) + Environment.NewLine; }
                        if (td.t2operBox[i].Text == "") { if (tb == null) { tb = td.t2operBox[i]; } err += "Please enter " + h[td.t2operBox[i].Name.ToString().Substring(2, 3)] + " " + (i + 1) + Environment.NewLine; }

                    }
                    j++;
                }
                if (err == string.Empty && cmd != string.Empty)
                {
                    if (MessageBox.Show(showCmd, "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        m = t.ToArray();
                        n = k.ToArray();
                        //td.ClearBoxes(m);
                        td.MakeReadOnly(m);
                        td.MakeNotReadOnly(n);
                        string fullCmd = getCurrTime() + "|" + "program-config" + "|" + cmd + "\n";
                        switchBtn(1, td.t2btn);
                        disableAllMenu();
                        tcp.sndData(fullCmd);                        
                    }
                }
                else
                {
                    this.ActiveControl = tb;
                    MessageBox.Show(err, "Error");
                }

            }
            else if (flg == 2)
            {
                TextBox tb = new TextBox();
                tb = null;
                string showCmd = string.Empty;
                for (int i = 0; i < 4; i++)
                {
                    if (td.t2gpbox[i].Enabled == true)
                    {
                        if (td.t2gpbox[i].Text == "") { if (tb == null) { tb = td.t2gpbox[i]; } err += "Please enter " + h[td.t2gpbox[i].Name.ToString().Substring(2, 2)] + " " + (i + 1) + Environment.NewLine; }
                        if (td.t2bpbox[i].Text == "") { if (tb == null) { tb = td.t2bpbox[i]; } err += "Please enter " + h[td.t2bpbox[i].Name.ToString().Substring(2, 2)] + " " + (i + 1) + Environment.NewLine; }
                        if (td.t2gpbox[i].Text != "" && td.t2bpbox[i].Text != "")
                        {
                            showCmd += "Good Parts " + j + " - " + td.t2gpbox[i].Text + "\nBad Parts " + j + " - " + td.t2bpbox[i].Text + "\n\n";
                            cmd += "good" + j + "=" + td.t2gpbox[i].Text + ";bad" + j + "=" + td.t2bpbox[i].Text + ";";
                        }
                    }
                    j++;
                }
                if (err == string.Empty && cmd != string.Empty)
                {
                    if (td.CheckNumeric(td.t2gpbox) && td.CheckNumeric(td.t2bpbox))
                    {
                        if (MessageBox.Show(showCmd, "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            string fullCmd = getCurrTime() + "|" + "part-count-multiple" + "|" + cmd + "\n";
                            switchBtn(2, td.t2btn);
                            tcp.sndData(fullCmd);  
                            HandleAfterSend(td.t2stp, td.t2snd, 2, td.t2btn);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Enter only numbers for Part Count", "Error");
                    }
                }
                else
                {
                    this.ActiveControl = tb;
                    MessageBox.Show(err, "Error");
                }
            }
        }

        public void HandleAfterSend(Control[] frm, Control[] to, int flag, Button[] btn)
        {
            td.MakeReadOnly(frm);
            td.MakeNotReadOnly(to);
            disableAllMenu();
            if (flag == 2)
            {
                //td.ClearBoxes(frm);
                this.ActiveControl = to[0];

            }
            switchBtn(flag, btn);
            if (flag == 2)
            {
                if ((tstop8.Enabled == false) && (tstop9.Enabled == false))
                {
                    loadDefaults();
                }
            }
        }
        
        private void MultiProgClick(object sender, EventArgs e)
        {
            isOnMultiPall = false;
            greetLbl.Visible = false;
            stackPanel1.SelectedIndex = 1;
            stackPanel1.Visible = true;                       
            HandleAfterSend(td.t2stp, td.t2snd, 2, td.t2btn);
        }

        private void t2strt_Click(object sender, EventArgs e)
        {
            getFrmMultProgTbl(1);
        }

        private void t2stop_Click(object sender, EventArgs e)
        {
            getFrmMultProgTbl(2);
        }
        bool isOnMultiPall;
        private void MultPanlClick(object sender, EventArgs e)
        {
            isOnMultiPall = true;
            greetLbl.Visible = false;
            stackPanel1.SelectedIndex = 3;
            stackPanel1.Visible = true;
            Button[] b = new Button[] { tstop8, tstop9, tstop10, tstop11 };
            TextBox[] c = new TextBox[] { t4gp1Txt, t4gp2Txt, t4gp3Txt, t4gp4Txt, t4bp1Txt, t4bp2Txt, t4bp3Txt, t4bp4Txt };
            td.MakeReadOnly(c);
            td.MakeReadOnly(b);
        }

        private void PopulateOperation(object sender, EventArgs e)
        {
            MessageBox.Show(this.Text);
        }
       
                  
    }
}
