using Dapper;
using GestionPretBancaire.Helpers;
using GestionPretBancaire.Models;
using GestionPretBancaire.Repositories.Interfaces;
using LinqToDB.SqlQuery;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Prng;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace GestionPretBancaire.Repositories
{
    public class ClientRepository : IClientRepository
    {
        //private const string V = "SELECT * FROM client ORDER BY nom;";


         // for getting all client
        public async Task<List<Client>> GetAllAsync()
        {
            try
            {
                using (var conn = new DatabaseHelper().getConnection())
                {
                    string query = "SELECT * FROM client ORDER BY nom;";
                    var result = await conn.QueryAsync<Client>(query);
                    return result.AsList();
                }
            }
            catch (SqlException ex)
            {             
                Console.WriteLine("Database error: " + ex.Message);
                return new List<Client>();
            }

            catch (Exception ex)
            {
                // Any other unexpected error
                Console.WriteLine("Unexpected error: " + ex.Message);
                return new List<Client>();
            }
        }

        // get one client by id

        public async Task<Client> GetByIdAsync(string _numCompte)
        {
            try
            {
                using (var conn = new DatabaseHelper().getConnection())
                {
                    string query = "SELECT * FROM CLIENT WHERE NumCompte = @numCompte;";
                    var result = await conn.QueryFirstOrDefaultAsync<Client>(query , new { numCompte = _numCompte});
                    return result;
                }
            }
            catch (SqlException ex) { 
                MessageBox.Show(ex.Message);
                return null;
            }

        }

        public Task<Client?> GetByCINAsync(string _numCIN)
        {
            try
            {
                using (var con = new DatabaseHelper().getConnection())
                {
                    string query = "SELECT * FROM CLIENT WHERE NumCIN = @numCIN;";
                    var result = con.QueryFirstOrDefault<Client>(query, new { numCIN = _numCIN });
                    return Task.FromResult(result);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return Task.FromResult<Client?>(null);

            }
        }

        public async Task AddAsync(Client client)
        {
            try
            {
                using (var conn = new DatabaseHelper().getConnection())
                {
                    string query = "INSERT INTO CLIENT (NumCompte , Nom, Prenom, NumTel, Email, Adresse, NumCIN) " +
                                   "VALUES (@NumCompte, @Nom, @Prenom, @NumTel, @Email, @Adresse, @NumCIN);";
                    await conn.ExecuteAsync(query, client);
                    MessageBox.Show("Client added successfully!");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public async Task UpdateAsync(Client client)
        {
            try
            {
                using (var conn = new DatabaseHelper().getConnection())
                {
                    string query = "UPDATE CLIENT SET Nom = @Nom, Prenom = @Prenom, NumTel = @NumTel, " +
                                   "Email = @Email, Adresse = @Adresse, NumCIN = @NumCIN WHERE NumCompte = @NumCompte;";
                    await conn.ExecuteAsync(query, client);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public async Task<bool> DeleteAsync(string _numCompte)
        {
            try
            {
                using (var conn = new DatabaseHelper().getConnection())
                {
                    string query = "DELETE FROM CLIENT WHERE NumCompte = @numCompte;";
                    int rowsAffected = await conn.ExecuteAsync(query, new { numCompte = _numCompte });
                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

    }

}