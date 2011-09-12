using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;


namespace VimanaPoi
{
    class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

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
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + 
		    database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
        }


    }
}
