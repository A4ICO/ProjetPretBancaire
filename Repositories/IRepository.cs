using GestionPretBancaire.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionPretBancaire.Repositories
{

    public interface IRepository
    {

        Task<List<Pret>> GetAllClientPretAsync(Client client);
        //Task<List<String>> GetAllClientInfoAsync(Client client);

    }

}