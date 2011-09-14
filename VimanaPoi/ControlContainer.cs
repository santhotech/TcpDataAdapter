using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace VimanaPoi
{
    public class ControlContainer
    {
        public Control[] strt { get; set; }
        public Control[] stop { get; set; }
        public String cmdStrt { get; set; }
        public String cmdStop { get; set; }
        public Button strtBtn { get; set; }
        public Button stopBtn { get; set; }
    }
}
