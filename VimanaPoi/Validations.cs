using System;
using System.Collections.Generic;
using System.Text;

namespace VimanaPoi
{
    class Validations
    {
        public string ValidateLogin(string str)
        {
            string error = string.Empty;
            if (isEmpty(str))
            {
                error += "Enter an Employee Name" + Environment.NewLine;
            }
            if (!TestDbConn())
            {
                error += "Cannot connect to DB. Check DB settings" + Environment.NewLine;
            }
            if (CheckMacNameEmpty())
            {
                error += "No Machine Name found. Select a Machine Name." + Environment.NewLine;
            }
            return error;
        }

        private bool isEmpty(string str)
        {
            if (str == string.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool TestDbConn()
        {
            DBConnect dbc = new DBConnect();
            if (dbc.TestConnection())
            {
                return true;
            }
            else
            {
                return false;          
            }
        }

        private bool CheckMacNameEmpty()
        {
            string macName = Properties.Settings.Default.machinename;
            if (isEmpty(macName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
