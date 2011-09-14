using System;
using System.Collections.Generic;
using System.Text;

namespace VimanaPoi
{
    class Commands
    {
        ControlManifest com = new ControlManifest(); 
        public string getCurrTime()
        {
            return DateTime.UtcNow.ToString("o");
        }

        public void SingProgTbl()
        {
            string cmdFormat = "{0}|part-type|{1}\n{0}|operation-type|{2}\n";
            string cmd = String.Format(cmdFormat, getCurrTime(), com.GetData(com.tbl1));
        }

    }
}
