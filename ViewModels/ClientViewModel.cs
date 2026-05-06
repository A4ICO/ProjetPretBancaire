using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using GestionPretBancaire.Models;
using GestionPretBancaire.Repositories;

namespace GestionPretBancaire.Managers
{
    public class ClientManager
    {
        private readonly ClientRepository _repository;

        public ClientManager()
        {
            _repository = new ClientRepository();
        }

        // ====================== GET ALL ======================
        public async Task<List<Client>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        // ====================== GET BY ID ======================
        public async Task<Client> GetByIdAsync(string numCompte)
        {
            return await _repository.GetByIdAsync(numCompte);
        }

        // ====================== GET BY CIN ======================
        public async Task<Client?> GetByCINAsync(string numCIN)
        {
            return await _repository.GetByCINAsync(numCIN);
        }

        // ====================== ADD ======================
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

        // ====================== UPDATE ======================
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

        // ====================== DELETE ======================
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