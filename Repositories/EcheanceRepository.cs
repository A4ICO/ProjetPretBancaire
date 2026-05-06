using Dapper;
using GestionPretBancaire.Helpers;
using GestionPretBancaire.Models;
using System.Windows;


namespace GestionPretBancaire.Repositories
{
    class EcheanceRepository
    {

        /// 
        /// 
        /// Add  a new Echeance to the database.
        ///         
        /// 
        public async Task AddAsync(Echeance echeance)
        {
            string query = "INSERT INTO Echeance (CodeEcheance, ReferencePret, Date, Balance, Interet, SoldeRestant, Operation, DatePaiment) " +
                            "VALUES (@CodeEcheance, @ReferencePret, @Date, @Balance, @Interet, @SoldeRestant, @Operation, @DatePaiment)";
            try
            {
                using (var connection = new DatabaseHelper().getConnection())
                {
                    connection.Open();
                    await connection.ExecuteAsync(query, echeance);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        /// 
        /// 
        /// Get all Echeances from the database and return them as a list
        /// 
        /// 
        public async Task<List<Echeance>> GetAllAsync()
        {
            string query = " SELECT * FROM Echeance";

            try
            {
                using (var connection = new DatabaseHelper().getConnection())
                {
                    connection.Open();
                    var result = await connection.QueryAsync<Echeance>(query);
                    return result.AsList();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
                return new List<Echeance>();
            }
        }

        /// 
        /// 
        /// Get  a specific Echeance by its CodeEcheance
        /// 
        /// 

        public async Task<Echeance?> GetAsync(int codeEcheance)
        {
            string query = "SELECT * FROM Echeance WHERE CodeEcheance  = @codeEcheance;";

            try
            {
                using (var connection = new DatabaseHelper().getConnection())
                {
                    connection.Open();
                    var result = await connection.QueryFirstOrDefaultAsync<Echeance>(query, new { codeEcheance });
                    return result;
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.Message);
                return null;
            }
        }

        /// 
        /// 
        /// Get all Echeances associated with a specific ReferencePret 
        /// 
        /// 
        public async Task<List<Echeance>> GetByReferencePretAsync(int _referencePret)
        {
            string query = "SELECT * FROM Echeance WHERE ReferencePret = @referencePret;";
            try
            {
                using (var connection = new DatabaseHelper().getConnection())
                {
                    var result = await connection.QueryAsync<Echeance>(query, new { referencePret = _referencePret });
                    return result.AsList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return new List<Echeance>();
            }
        }

        /// 
        /// 
        /// Delete an Echeance from the database by using it's CodeEcheance 
        /// 
        /// 
        public async Task<bool> DeleteAsync(int _codeEcheance)
        {
            string query = "DELETE FROM Echeance WHERE CodeEcheance = @codeEcheance;";
            try
            {
                using (var connection = new DatabaseHelper().getConnection())
                {
                    connection.Open();
                    int rowsAffected = await connection.ExecuteAsync(query, new { codeEcheance = _codeEcheance });
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }

        }



        /// 
        /// 
        ///  Update an existing Echeance in the database by using it's CodeEcheance as a reference
        /// 
        /// 
        public async Task UpdateAsync(Echeance echeance)
        {
            string query = "UPDATE Echeance SET ReferencePret = @ReferencePret, Date = @Date, Balance = @Balance, " +
                           "Interet = @Interet, SoldeRestant = @SoldeRestant, Operation = @Operation, DatePaiment = @DatePaiment " +
                           "WHERE CodeEcheance = @CodeEcheance;";
            try
            {
                using (var connection = new DatabaseHelper().getConnection())
                {
                    connection.Open();
                    await connection.ExecuteAsync(query, echeance);
                    MessageBox.Show("Echeance updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating echeance: " + ex.Message);
            }
        }
    }
}
