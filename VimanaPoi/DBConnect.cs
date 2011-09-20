using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Collections;

namespace VimanaPoi
{
    class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        private string dbport;
        //Constructor
        public DBConnect()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = Properties.Settings.Default.server;
            database = Properties.Settings.Default.dbname;
            uid = Properties.Settings.Default.username;
            password = Properties.Settings.Default.password;
            dbport = Properties.Settings.Default.dbport;
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";PORT=" + dbport + ";";
            
            connection = new MySqlConnection(connectionString);
        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {                
                switch (ex.Number)
                {
                    case 0:
                        Console.Write("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.Write("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }

        public ArrayList GetMachineNames()
        {
            string query = "SELECT DISTINCT PREF_MACH_SER_NO FROM parts ORDER BY PREF_MACH_SER_NO ASC";

            //Create a list to store the result
            ArrayList machines = new ArrayList();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    machines.Add(dataReader["PREF_MACH_SER_NO"]);                    
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return machines;
            }
            else
            {
                return machines;
            }
        }

        public ArrayList GetPartNames(string deviceName,string cat)
        {
            string query = "SELECT DISTINCT PART_NO FROM parts WHERE PREF_MACH_SER_NO = '" + deviceName + "' and OPER_TYPE = '" + cat + "' ORDER BY PRIORITY ASC LIMIT 0,5";            
            ArrayList part_numbers = new ArrayList();            
            if (this.OpenConnection() == true)
            {                
                MySqlCommand cmd = new MySqlCommand(query, connection);                
                MySqlDataReader dataReader = cmd.ExecuteReader();                
                while (dataReader.Read())
                {
                    part_numbers.Add(dataReader["PART_NO"]);
                }
                dataReader.Close();
                this.CloseConnection();
            }
            return part_numbers;
        }
        public ArrayList GetOperationNames(string deviceName)
        {
            string query = "SELECT DISTINCT PART_OPER_NO FROM parts WHERE PREF_MACH_SER_NO = '" + deviceName + "' ORDER BY PART_OPER_NO";
            ArrayList oper_numbers = new ArrayList();
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    oper_numbers.Add(dataReader["PART_OPER_NO"]);
                }
                dataReader.Close();
                this.CloseConnection();
            }
            return oper_numbers;
        }
        public ArrayList GetOperationNames(string deviceName,string partNme,string cat)
        {
            string query = "SELECT DISTINCT PART_OPER_NO FROM parts WHERE PREF_MACH_SER_NO = '" + deviceName + "' and PART_NO = '" + partNme + "' and OPER_TYPE = '" + cat + "' ORDER BY PART_OPER_NO";
            ArrayList oper_numbers = new ArrayList();
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    oper_numbers.Add(dataReader["PART_OPER_NO"]);
                }
                dataReader.Close();
                this.CloseConnection();
            }
            return oper_numbers;
        }

        public string GetTarget(string macname, string partname, string oprname, string cat)
        {
            string trgt = string.Empty;
            string query = "SELECT QTY_ON_ORDER FROM parts WHERE PREF_MACH_SER_NO = '" + macname + "' and PART_NO = '" + partname + "' and PART_OPER_NO = '" + oprname + "' and OPER_TYPE = '" + cat + "' ORDER BY PART_OPER_NO";            
            if (this.OpenConnection() == true)
            {                
                MySqlCommand cmd = new MySqlCommand(query, connection);
                trgt = cmd.ExecuteScalar() + "";
                this.CloseConnection();
            }
            return trgt;
        }

        public bool TestConnection()
        {
            if (OpenConnection())
            {
                CloseConnection();
                return true;                
            }
            else
            {
                return false;
            }
        }
    }
}
