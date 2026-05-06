using System.Windows;
using GestionPretBancaire.Models;
using GestionPretBancaire.Repositories;



/// 
/// 
///  It's a layer for the business logic of the application, 
///  it interacts with the ClientRepository to perform operations related to clients,
///  such as retrieving all clients, getting a client 
///  by its numero de compte or CIN, adding a new client, updating an existing client, and deleting a client.
///  It also handles exceptions and displays error messages when necessary
/// 
/// 



namespace GestionPretBancaire.Managers
{
    public class ClientManager
    {
        private readonly ClientRepository _repository;

        public ClientManager()
        {
            _repository = new ClientRepository();
        }

        /// 
        /// 
        /// Get all clients from the database
        /// 
        /// 
        public async Task<List<Client>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        /// 
        /// 
        /// Get a client by its numero de compte
        /// 
        /// 
        public async Task<Client> GetByIdAsync(string numCompte)
        {
            return await _repository.GetByIdAsync(numCompte);
        }

        /// 
        /// 
        ///  Get a client by its numero de CIN
        /// 
        /// 
        public async Task<Client?> GetByCINAsync(string numCIN)
        {
            return await _repository.GetByCINAsync(numCIN);
        }

        /// 
        /// 
        /// Add a new client to the database
        /// 
        /// 
        public async Task<bool> AddAsync(Client client)
        {
            try
            {
                await _repository.AddAsync(client);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'ajout du client :\n" + ex.Message,
                              "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        /// 
        /// 
        /// Update an existing client in the database by using its numero de compte as a reference
        /// 
        /// 
        public async Task<bool> UpdateAsync(Client client)
        {
            try
            {
                await _repository.UpdateAsync(client);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la modification :\n" + ex.Message,
                              "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        /// 
        /// 
        /// Delete a client from the database by using its numero de compte as a reference
        /// 
        /// 
        public async Task<bool> DeleteAsync(string numCompte)
        {
            try
            {
                return await _repository.DeleteAsync(numCompte);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la suppression :\n" + ex.Message,
                              "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}