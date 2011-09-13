using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace VimanaPoi
{
    class ControlManifest
    {
        ComboBox[] parts;
        ComboBox[] ops;
        public ControlManifest(ComboBox[] parts,ComboBox[] ops)
        {
            this.parts = parts;
            this.ops = ops;
        }
    }
}
