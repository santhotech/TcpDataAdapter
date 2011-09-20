using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace VimanaPoi
{
    class Logger
    {
        private string fileName;
        public Logger()
        {
            fileName = "adapterlog.txt";
        }

        public void WriteLog(string statement)
        {
            try
            {
                statement = statement + Environment.NewLine;
                File.AppendAllText(fileName, statement);
            }
            catch { Console.WriteLine("Unable to write log"); }
        }
    }
}
