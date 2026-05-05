using Dapper;
using GestionPretBancaire.Helpers;
using GestionPretBancaire.Models;
using GestionPretBancaire.Repositories.Interfaces;
using LinqToDB.SqlQuery;
using Mysqlx;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace GestionPretBancaire.Repositories
{
    class PretRepositorycs : IPretRepository
    {
        public async Task AddAsync(Pret pret)
        {
            string query = "INSERT INTO Pret (Reference, NumCompte, TypePret, Name, Montant, TauxInteret, Status, DateCreation) VALUES (@Reference, @NumCompte, @TypePret, @Name, @Montant, @TauxInteret, @Status, @DateCreation);";
            try
            {

                using (var conn = new DatabaseHelper().getConnection())
                {
                    await conn.ExecuteAsync(query, pret);
                }
            }

            catch (SqlException ex)
            {
                Console.WriteLine("Error adding pret: " + ex.Message);
            }
        }



        public async Task<List<Pret>> GetAllAsync()
        {
            string query = "SELECT * FROM client;";

            try
            {
                using (var connection = new DatabaseHelper().getConnection())
                {
                    connection.Open();
                    var result = await connection.QueryAsync<Pret>(query);
                    return result.AsList();
                }
            }

            catch (SqlException ex)
            {
                MessageBox.Show("Error :" + ex.Message);
                return new List<Pret>();


            }
        }



        public async Task<List<Pret>> GetByNumCompteAsync(string numCompte)
        {
            string query = "SELECT * FROM Pret WHERE NumCompte = @NumCompte;";

            try
            {

                using (var conn = new DatabaseHelper().getConnection())
                {
                    conn.Open();
                    var result = await conn.QueryAsync<Pret>(query, new { NumCompte = numCompte });
                    return result.AsList();
                }
            }

            catch (SqlException ex)
            {
                Console.WriteLine("Error fetching pret by NumCompte: " + ex.Message);
                return new List<Pret>();
            }

        }


        public Task<bool> DeleteAsync(int referencePret)
        {
            string query = "DELETE FROM Pret WHERE Reference = @Reference;";
            try
            {
                using (var conn = new DatabaseHelper().getConnection())
                {
                    conn.Open();
                    int rowsAffected = conn.Execute(query, new { Reference = referencePret });
                    return Task.FromResult(rowsAffected > 0);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error deleting pret: " + ex.Message);

                //, MessageBoxButton.OK, MessageBoxImage.Warning);

                return Task.FromResult(false);
            }

        }

        public async Task UpdateAsync(Pret pret)
        {
            string query = "UPDATE FROM Pret SET Reference = @Reference , NumCompte = @NumCompte , TypePret = @TypePret , Status = @Status ;";

            try
            {
                using (var connection = new DatabaseHelper().getConnection())
                {
                    connection.Open();
                    await connection.QueryAsync(query, pret);
                    MessageBox.Show("Pret updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }

            catch (SqlException ex)
            {
                MessageBox.Show("Error updating pret: " + ex.Message);
            }
        }

        public Task<Pret?> GetByReferencePretAsync(int referencePret)
        {
            string query = "SELECT * FROM Pret WHERE Reference = @Reference;";
            try
            {
                using (var conn = new DatabaseHelper().getConnection())
                {
                    conn.Open();
                    var result = conn.QueryFirstOrDefault<Pret>(query, new { Reference = referencePret });
                    return Task.FromResult(result);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error fetching pret by reference: " + ex.Message);
                return Task.FromResult<Pret?>(null);
            }
        }

        public Task<bool> ExistsAsync(int referencePret)
        {
            string query = "SELECT COUNT(1) FROM Pret WHERE Reference = @Reference;";
            try
            {
                using (var conn = new DatabaseHelper().getConnection())
                {
                    conn.Open();
                    int count = conn.ExecuteScalar<int>(query, new { Reference = referencePret });
                    return Task.FromResult(count > 0);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error checking pret existence: " + ex.Message);
                return Task.FromResult(false);
            }
        }



}

