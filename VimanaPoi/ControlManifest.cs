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
        public ControlManifest(ComboBox[] parts,ComboBox[] opr)
        {
            this.parts = parts;
            this.opr = opr;
            PopulateComboBoxes();
        }
        private void PopulateComboBoxes()
        {
            string macName = Properties.Settings.Default.machinename;
            DBConnect dbc = new DBConnect();
            ArrayList parts = dbc.GetPartNames(macName);
            foreach (ComboBox cb in parts)
            {
                cb.Items.AddRange(parts.ToArray());
            }
        }
    }
}
