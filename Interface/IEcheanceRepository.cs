using GestionPretBancaire.Models;

namespace GestionPretBancaire.Repositories.Interfaces
{
    public interface IEcheanceRepository
    {
        Task AddAsync(Echeance echeance);
        Task<List<Echeance>> GetAllAsync();
        Task<Echeance?> GetAsync(int codeEcheance);
        Task<List<Echeance>> GetByReferencePretAsync(int referencePret);
        Task<bool> DeleteAsync(int codeEcheance);
        Task UpdateAsync(Echeance echeance);
    }
}