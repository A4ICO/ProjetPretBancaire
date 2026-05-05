using GestionPretBancaire.Models;

namespace GestionPretBancaire.Repositories.Interfaces
{
    public interface IPretRepository
    {
        Task AddAsync(Pret pret);
        Task<List<Pret>> GetAllAsync();
        Task<List<Pret>> GetByNumCompteAsync(string numCompte);
        Task<Pret?> GetByReferencePretAsync(int referencePret);
        Task<bool> DeleteAsync(int referencePret);
        Task UpdateAsync(Pret pret);
        Task<bool> ExistsAsync(int referencePret);
    }
}