using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;

namespace VimanaPoi
{
    class ControlManifest
    {
        ComboBox[] parts;
        ComboBox[] opr;
        
        public Control[] tbl1strt;
        public Control[] tbl1stop;

        public Control[] tbl3strt;
        public Control[] tbl3stop;

        public Control[] tbl5strt;
        public Control[] tbl5stop;

        public Control[] tbl6strt;
        public Control[] tbl6stop;

        public Control[] tbl7strt;
        public Control[] tbl7stop;

        Hashtable h = new Hashtable();
        ArrayList al = new ArrayList();
            

        public ControlManifest(){}
        public ControlManifest(ComboBox[] parts,ComboBox[] opr)
        {
            this.parts = parts;
            this.opr = opr;            
            PopulateComboBoxes();
            h.Add("part", "Part Name");
            h.Add("opr", "Operation Name");
            h.Add("gp", "Good Parts");
            h.Add("bp", "Bad Parts");
            h.Add("fixPosn", "Parts per Fixture");
            h.Add("noPrt", "Number of Parts");
            al.Add("gp");
            al.Add("bp");
            al.Add("noPrt");
            al.Add("fixPosn");
        }

        public bool CheckNumeric(TextBox[] tb)
        {
            foreach (TextBox t in tb)
            {
                if (!IsAllDigits(t.Text) && t.Enabled == true)
                {
                    return false;
                }
            }
            return true;
        }

        bool IsAllDigits(string s)
        {
            foreach (char c in s)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }

        public string[] GetData(Control[] ctrl)
        {
            string[] str = new string[ctrl.Length+1]; 
            int i = 1;
            str[0] = getCurrTime();
            foreach (Control ct in ctrl)
            {
                str[i] = ct.Text; 
                i++;
            }
            return str;
        }

        public string[] GetShowData(Control[] ctrl)
        {
            string[] str = new string[ctrl.Length + 1];
            int i = 0;            
            foreach (Control ct in ctrl)
            {
                str[i] = ct.Text;
                i++;
            }
            return str;
        }

        public string getCurrTime()
        {
            return DateTime.UtcNow.ToString("o");
        }

        private void PopulateComboBoxes()
        {
            string macName = Properties.Settings.Default.machinename;
            DBConnect dbc = new DBConnect();
            ArrayList partNames = dbc.GetPartNames(macName);
            ArrayList OperNames = dbc.GetOperationNames(macName);
            foreach (ComboBox cb in parts)
            {
                cb.Items.Clear();
                cb.Items.AddRange(partNames.ToArray());
            }
            foreach (ComboBox cb in opr)
            {
                cb.Items.Clear();
                cb.Items.AddRange(OperNames.ToArray());
            }
        }

        public bool ValidateControls(Control[] ctrl)
        {
            string error = string.Empty;
            foreach (Control ct in ctrl)
            {
                string nm = ct.Name;
                if (ct.Text == string.Empty)
                {
                    return false;
                }
            }
            return true;
        }

        public void ReadUnRead(Control[] ctrl,bool bl,bool clear)
        {
            foreach (Control ct in ctrl)
            {
                ct.Enabled = bl;
                if (bl == true)
                {
                    ct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
                }
                else
                {
                    ct.BackColor = Color.White;
                }
                if (clear == true)
                {
                    ct.Text = string.Empty;
                }                
            }
        }

        public string ValidationMessage(Control[] ctrl)
        {
            string error = string.Empty;

            return error;
        }

        public object[] CheckOk(Control[] curObj)
        {
            Hashtable h = new Hashtable();
            int count = 0;
            h.Add("part", "Part Name");
            h.Add("opr", "Operation Name");
            h.Add("gp", "Good Parts");
            h.Add("bp", "Bad Parts");
            h.Add("fixPosn", "Parts per Fixture");
            h.Add("noPrt", "Number of Parts");
            h.Add("trgt", "Target");
            string err = string.Empty;
            object[] parameters = new object[2];
            parameters[1] = null;
            for (int i = 0; i < curObj.Length; i++)
            {
                if (curObj[i].Text == string.Empty)
                {
                    if (parameters[1] == null)
                    {
                        parameters[1] = curObj[i];
                    }
                    string curObjName = (string)curObj[i].Name;
                    foreach (DictionaryEntry entry in h)
                    {
                        if (curObjName.IndexOf(entry.Key.ToString()) != -1)
                        {
                            for (int j = 1; j < 5; j++)
                            {
                                if (curObjName.Substring(2).IndexOf(j.ToString()) != -1)
                                {
                                    err += "Please enter " + entry.Value + j + Environment.NewLine;
                                    count++;
                                }
                            }
                            if (count == 0)
                            {
                                err += "Please enter " + entry.Value + Environment.NewLine;
                            }
                        }
                    }
                }
                if (curObj[i].Name.Contains("fixPosn") || curObj[i].Name.Contains("noPrt") || curObj[i].Name.Contains("gp") || curObj[i].Name.Contains("bp"))
                {
                    if (!IsAllDigits(curObj[i].Text))
                    {
                        err += "Please enter only numbers for parts" + Environment.NewLine;
                    }
                }
            }


            if (err == string.Empty)
            {
                parameters[0] = "true";
            }
            else
            {
                parameters[0] = err;
            }
            return parameters;
        }
    }
}
