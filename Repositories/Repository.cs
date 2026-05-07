using GestionPretBancaire.Helpers;
using GestionPretBancaire.Models;
using Dapper;
using LinqToDB.SqlQuery;
using System.Windows;

namespace GestionPretBancaire.Repositories
{
    internal class Repository : IRepository
    {
        public async Task<List<Pret>> GetAllClientPretAsync(Client client)
        {
            string query = $"SELECT * FROM Pret WHERE NumCompte = '{client.NumCompte}'";

            try
            {

                using (var conn = new DatabaseHelper().getConnection())
                {
                    var result = await conn.QueryAsync<Pret>(query);
                    return result.AsList();
                }
            }

            catch(SqlException ex) { throw new Exception(ex.Message);}
            catch(Exception ex){
                MessageBox.Show(ex.Message);
                return new List<Pret>();}
        }

        //public async Task GetClientInfoAsync(Client client)
        //{
        //    string query = $"SELECT * FROM Client WHERE NumCompte = '{client.NumCompte}'";
        //}

    }
}
