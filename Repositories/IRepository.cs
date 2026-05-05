//using GestionPretBancaire.Models;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace GestionPretBancaire.Repositories
//{

//    public class IRepository
//    {

//        // Interface pour les clients
//        public interface IClientRepository
//        {
//            Task<List<Client>> GetAllAsync();
//            Task<Client?> GetByIdAsync(string numCompte);
//            Task<Client?> GetByCINAsync(string numCIN);
//            Task AddAsync(Client client);
//            Task UpdateAsync(Client client);
//            Task<bool> DeleteAsync(string numCompte);
//            Task<bool> ExistsAsync(string numCompte);
//        }


//        // Interface pour les prets
//        public interface IPretRepository
//        {
//            Task AddAsync(Pret pret);
//            Task<List<Pret>> GetAllAsync();
//            Task<Pret?> GetByNumCompteAsync(string numCompte);
//            Task<bool> DeleteAsync(int ReferencePret);
//            Task UpdateAsync(Pret pret);

//        }


//        // Interfacepour les echeances
//        public interface IEcheanceRepository
//        {

//            Task AddAsync(Echeance echeance);
//            Task<Echeance?> GetAsync(int CodeEcheance);
//            Task<bool> DeleteAsync(int CodeEcheance);
//            Task<Echeance> GetByReferencePretAsync(int referncePret);


//        }
//    }

//}