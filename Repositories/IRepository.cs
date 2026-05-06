using GestionPretBancaire.Models;

namespace GestionPretBancaire.Repositories
{

    public interface IRepository
    {

        Task<List<Pret>> GetAllClientPretAsync(Client client);
        //Task<List<String>> GetAllClientInfoAsync(Client client);

    }

}