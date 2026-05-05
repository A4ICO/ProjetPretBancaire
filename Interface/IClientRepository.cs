using GestionPretBancaire.Helpers;
using GestionPretBancaire.Models;
using LinqToDB.SqlQuery;

namespace GestionPretBancaire.Repositories.Interfaces
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAllAsync();
        Task<Client> GetByIdAsync(string numCompte);
        Task<Client?> GetByCINAsync(string numCIN);
        Task AddAsync(Client client);
        Task UpdateAsync(Client client);
        Task<bool> DeleteAsync(string numCompte);
        //Task<bool> ExistsAsync(string numCompte);
    }
}






//public async Task<Client> GetByIdAsync(string numCompte)
//{
//    try
//    {
//        using (var conn = new DatabaseHelper().getConnection())
//        {
//            string query = "SELECT * FROM client WHERE num_compte = @NumCompte";

//            var result = await conn.QueryFirstOrDefaultAsync<Client>(query, new { NumCompte = numCompte });

//            return result;
//        }
//    }
//    catch (SqlException ex)
//    {
//        Console.WriteLine("Database error: " + ex.Message);
//        return null;
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine("Unexpected error: " + ex.Message);
//        return null;
//    }
//}