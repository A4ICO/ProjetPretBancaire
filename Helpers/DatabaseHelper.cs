using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows;
using MySql.Data.MySqlClient;

namespace GestionPretBancaire.Helpers
{

    /// 
    ///To facilitate database connection and operations,
    /// this helper class provides a method to get a MySQL connection using the specified connection string. 
    /// It handles exceptions and displays error messages if the connection fails.
    /// 
    internal class DatabaseHelper
    {

        private string _connectionString = "Server=127.0.0.1;Port=3306;Database=banquecentral;Uid=root;Pwd=;";
        public MySqlConnection getConnection()
        {

            try
            {
                MySqlConnection conn = new MySqlConnection(_connectionString);
                //conn.Open();
                return conn;
            }

            catch(Exception ex) 
            {
                MessageBox.Show("Error : " + ex.Message);
                throw;
            }

        }
    }
    
}
