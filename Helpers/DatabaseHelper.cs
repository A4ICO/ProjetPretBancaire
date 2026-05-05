using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Windows;
using MySql.Data.MySqlClient;
using Mysqlx.Connection;

namespace GestionPretBancaire.Helpers
{
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
