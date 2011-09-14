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
        public Control[] tbl1;
        public ControlManifest();
        public ControlManifest(ComboBox[] parts,ComboBox[] opr)
        {
            this.parts = parts;
            this.opr = opr;            
            PopulateComboBoxes();            
        }

        public string[] GetData(Control[] ctrl)
        {
            string[] str = new string[] { };
            int i = 0;
            foreach (Control ct in ctrl)
            {
                str[i] = ct.Text; 
                i++;
            }
            return str;
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
    }
}
