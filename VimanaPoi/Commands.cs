using System;
using System.Collections.Generic;
using System.Text;

namespace VimanaPoi
{
    class Commands
    {
        ControlManifest cmf;
        public Commands(ControlManifest cmf)
        {
            this.cmf = cmf;
        }
        ControlManifest com = new ControlManifest(); 
        

        public string SingProgTbl()
        {
            string cmdFormat = "{0}|part-type|{1}\n{0}|operation-type|{2}\n";
            string[] data = com.GetData(cmf.tbl1strt);
            string cmd = String.Format(cmdFormat, data);
            return cmd;
        }

        public string ThirdTable()
        {
            string cmdFormat = "{0}|part-type|{1}\n{0}|operation-type|{1}\n{0}|fixture-position|{2}\n";
            string[] data = com.GetData(cmf.tbl3strt);
            string cmd = String.Format(cmdFormat, data);
            return cmd;
        }

    }
}
