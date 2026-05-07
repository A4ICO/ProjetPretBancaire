using Dapper;
using GestionPretBancaire.Helpers;
using GestionPretBancaire.Models;
using GestionPretBancaire.Repositories.Interfaces;
using MySql.Data.MySqlClient;
using System.Windows;

namespace GestionPretBancaire.Repositories
{
    public class ClientRepository : IClientRepository
    {


        /// 
        /// 
        /// Get all clients
        /// 
        /// 
        public async Task<List<Client>> GetAllAsync()
        {
            try
            {
                using (var conn = new DatabaseHelper().getConnection())
                {
                    string query = "SELECT * FROM Client ORDER BY NomClient, PrenomClient;";
                    var result = await conn.QueryAsync<Client>(query);
                    return result.AsList();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erreur de base de données :\n{ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Client>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur inattendue :\n{ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Client>();
            }
        }



        /// 
        /// 
        /// Get one client by numero de compte
        /// 
        /// 
        public async Task<Client?> GetByIdAsync(string numCompte)
        {
            try
            {
                using (var conn = new DatabaseHelper().getConnection())
                {
                    string query = "SELECT * FROM Client WHERE NumCompte = @NumCompte;";
                    return await conn.QueryFirstOrDefaultAsync<Client>(query, new { NumCompte = numCompte });
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erreur de base de données :\n{ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }


        /// 
        /// 
        /// Get a client by numero de CIN
        /// 
        /// 
        public async Task<Client?> GetByCINAsync(string numCIN)
        {
            try
            {
                using (var conn = new DatabaseHelper().getConnection())
                {
                    string query = "SELECT * FROM Client WHERE NumCIN = @NumCIN;";
                    return await conn.QueryFirstOrDefaultAsync<Client>(query, new { NumCIN = numCIN });
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erreur de base de données :\n{ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }


        /// 
        /// 
        /// Insert a new client into the database
        /// 
        /// 
        public async Task AddAsync(Client client)
        {
            try
            {
                using (var conn = new DatabaseHelper().getConnection())
                {
                    string query = @"INSERT INTO Client (NumCompte, NomClient, PrenomClient, NumTel, Email, Adresse, NumCIN) 
                                   VALUES (@NumCompte, @NomClient, @PrenomClient, @NumTel, @Email, @Adresse, @NumCIN);";

                    await conn.ExecuteAsync(query, client);
                    MessageBox.Show("Client ajouté avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (MySqlException ex) when (ex.Number == 1062) // Duplicate entry
            {
                MessageBox.Show("Un client avec ce Numéro de Compte ou Email existe déjà.", "Doublon", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erreur de base de données :\n{ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur inattendue :\n{ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        /// 
        /// 
        /// update an existing client in the database
        /// 
        /// 
        public async Task UpdateAsync(Client client)
        {
            try
            {
                using (var conn = new DatabaseHelper().getConnection())
                {
                    string query = @"UPDATE Client 
                                   SET NomClient = @Nom, 
                                       PrenomClient = @Prenom, 
                                       NumTel = @NumTel, 
                                       Email = @Email, 
                                       Adresse = @Adresse, 
                                       NumCIN = @NumCIN 
                                   WHERE NumCompte = @NumCompte;";

                    int rowsAffected = await conn.ExecuteAsync(query, client);

                    if (rowsAffected > 0)
                        MessageBox.Show("Client modifié avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        MessageBox.Show("Aucun client trouvé avec ce NumCompte.", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erreur de base de données :\n{ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur inattendue :\n{ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        /// 
        /// 
        /// Delete a client from the database by numero de compte
        /// 
        /// 
        public async Task<bool> DeleteAsync(string numCompte)
        {
            try
            {
                using (var conn = new DatabaseHelper().getConnection())
                {
                    string query = "DELETE FROM Client WHERE NumCompte = @NumCompte;";
                    int rowsAffected = await conn.ExecuteAsync(query, new { NumCompte = numCompte });
                    return rowsAffected > 0;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erreur de base de données :\n{ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur inattendue :\n{ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}