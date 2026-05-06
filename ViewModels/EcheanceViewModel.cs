using System.Windows;
using GestionPretBancaire.Models;
using GestionPretBancaire.Repositories;


/// 
/// 
///  It's a layer for the business logic of the application, 
///  it interacts with the ClientRepository to perform operations related to clients,


namespace GestionPretBancaire.Managers
{
    public class EcheanceManager
    {
        private readonly EcheanceRepository _repository;

        public EcheanceManager()
        {
            _repository = new EcheanceRepository();
        }


        /// 
        /// 
        /// Get all Echeances from the database
        /// 
        /// 
        public async Task<List<Echeance>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }


        /// 
        /// 
        /// Get all Echeances by reference pret from the database
        /// 
        /// 
        public async Task<List<Echeance>> GetByReferencePretAsync(int referencePret)
        {
            return await _repository.GetByReferencePretAsync(referencePret);
        }



        /// 
        /// 
        /// Request add a new Echeance to the database
        /// 
        ///
        public async Task<bool> AddAsync(Echeance echeance)
        {
            try
            {
                await _repository.AddAsync(echeance);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'ajout de l'échéance :\n" + ex.Message,
                              "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }


        /// 
        /// 
        /// Request update an existing Echeance in the database  
        /// 
        ///
        public async Task<bool> UpdateAsync(Echeance echeance)
        {
            try
            {
                await _repository.UpdateAsync(echeance);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la modification :\n" + ex.Message);
                return false;
            }
        }

        /// 
        /// 
        /// Request delete an Echeance from the database 
        /// 
        ///

        public async Task<bool> DeleteAsync(int codeEcheance)
        {
            try
            {
                return await _repository.DeleteAsync(codeEcheance);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la suppression :\n" + ex.Message);
                return false;
            }
        }

        /// 
        /// 
        ///  Request to get an Echeance by its codeEcheance from the database
        /// 
        ///
        public async Task<Echeance?> GetByIdAsync(int codeEcheance)
        {
            return await _repository.GetAsync(codeEcheance);
        }
    }
}