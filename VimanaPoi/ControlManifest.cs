using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace VimanaPoi
{
    class ControlManifest
    {
        ComboBox[] parts;
        ComboBox[] opr;
        
        public Control[] tbl1strt;
        public Control[] tbl1stop;

        public Control[] tbl2strt;
        public Control[] tbl2stop;

        public Control[] tbl3strt;
        public Control[] tbl3stop;

        public Control[] tbl4strt;
        public Control[] tbl4stop;

        public Control[] tbl5strt;
        public Control[] tbl5stop;

        public Control[] tbl6strt;
        public Control[] tbl6stop;

        public Control[] tbl7strt;
        public Control[] tbl7stop;

        public ControlManifest(){}
        public ControlManifest(ComboBox[] parts,ComboBox[] opr)
        {
            this.parts = parts;
            this.opr = opr;            
            PopulateComboBoxes();            
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
            foreach (Control ct in ctrl)
            {
                if (ct.Text == string.Empty)
                {
                    return false;
                }
            }
            return true;
        }

        public void ReadUnRead(Control[] ctrl,bool bl)
        {
            foreach (Control ct in ctrl)
            {
                ct.Enabled = bl;
            }
        }

    }
}
