using Dapper;
using GestionPretBancaire.Helpers;
using GestionPretBancaire.Models;
using GestionPretBancaire.Repositories.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace GestionPretBancaire.Repositories
{
    public class PretRepository : IPretRepository
    {
        // ====================== ADD PRET ======================
        public async Task AddAsync(Pret pret)
        {
            string query = @"INSERT INTO Pret (Reference, NumCompte, TypePret, Name, Montant, 
                                             TauxInteret, Status, DateCreation) 
                           VALUES (@Reference, @NumCompte, @TypePret, @Name, @Montant, 
                                   @TauxInteret, @Status, @DateCreation);";

            try
            {
                using (var conn = new DatabaseHelper().getConnection())
                {
                    await conn.ExecuteAsync(query, pret);
                    MessageBox.Show("Prêt ajouté avec succès !", "Succès",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                MessageBox.Show("Un prêt avec cette référence existe déjà.", "Doublon",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erreur de base de données :\n{ex.Message}", "Erreur",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur inattendue :\n{ex.Message}", "Erreur",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ====================== GET ALL ======================
        public async Task<List<Pret>> GetAllAsync()
        {
            string query = "SELECT * FROM Pret ORDER BY DateCreation DESC;";

            try
            {
                using (var conn = new DatabaseHelper().getConnection())
                {
                    var result = await conn.QueryAsync<Pret>(query);
                    return result.AsList();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erreur de base de données :\n{ex.Message}", "Erreur",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Pret>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur inattendue :\n{ex.Message}", "Erreur",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Pret>();
            }
        }

        // ====================== GET BY CLIENT ======================
        public async Task<List<Pret>> GetByNumCompteAsync(string numCompte)
        {
            string query = "SELECT * FROM Pret WHERE NumCompte = @NumCompte ORDER BY DateCreation DESC;";

            try
            {
                using (var conn = new DatabaseHelper().getConnection())
                {
                    var result = await conn.QueryAsync<Pret>(query, new { NumCompte = numCompte });
                    return result.AsList();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erreur de base de données :\n{ex.Message}", "Erreur",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Pret>();
            }
        }

        // ====================== GET BY REFERENCE ======================
        public async Task<Pret?> GetByReferenceAsync(int reference)
        {
            string query = "SELECT * FROM Pret WHERE Reference = @Reference;";

            try
            {
                using (var conn = new DatabaseHelper().getConnection())
                {
                    return await conn.QueryFirstOrDefaultAsync<Pret>(query, new { Reference = reference });
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erreur de base de données :\n{ex.Message}", "Erreur",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        // ====================== UPDATE ======================
        public async Task UpdateAsync(Pret pret)
        {
            string query = @"UPDATE Pret 
                           SET NumCompte = @NumCompte,
                               TypePret = @TypePret,
                               Name = @Name,
                               Montant = @Montant,
                               TauxInteret = @TauxInteret,
                               Status = @Status
                           WHERE Reference = @Reference;";

            try
            {
                using (var conn = new DatabaseHelper().getConnection())
                {
                    int rowsAffected = await conn.ExecuteAsync(query, pret);

                    if (rowsAffected > 0)
                        MessageBox.Show("Prêt modifié avec succès !", "Succès",
                                      MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        MessageBox.Show("Aucun prêt trouvé avec cette référence.", "Attention");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erreur de base de données :\n{ex.Message}", "Erreur",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur inattendue :\n{ex.Message}", "Erreur",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ====================== DELETE ======================
        public async Task<bool> DeleteAsync(string reference)
        {
            string query = "DELETE FROM Pret WHERE Reference = @Reference;";

            try
            {
                using (var conn = new DatabaseHelper().getConnection())
                {
                    int rowsAffected = await conn.ExecuteAsync(query, new { Reference = reference });
                    return rowsAffected > 0;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erreur de base de données :\n{ex.Message}", "Erreur",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        // ====================== EXISTS ======================
        public async Task<bool> ExistsAsync(int reference)
        {
            string query = "SELECT COUNT(1) FROM Pret WHERE Reference = @Reference;";

            try
            {
                using (var conn = new DatabaseHelper().getConnection())
                {
                    int count = await conn.ExecuteScalarAsync<int>(query, new { Reference = reference });
                    return count > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}