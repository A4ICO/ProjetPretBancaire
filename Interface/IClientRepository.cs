using GestionPretBancaire.Models;

namespace GestionPretBancaire.Repositories.Interfaces
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAllAsync();
        Task<Client?> GetByIdAsync(string numCompte);
        Task<Client?> GetByCINAsync(string numCIN);
        Task AddAsync(Client client);
        Task UpdateAsync(Client client);
        Task<bool> DeleteAsync(string numCompte);
        //Task<bool> ExistsAsync(string numCompte);
    }
}
