using GestionPretBancaire.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using GestionPretBancaire.Models;

namespace GestionPretBancaire.ViewModels
{


    public class PretManager
    {
        private readonly PretRepository _repository;

        public PretManager()
        {
            _repository = new PretRepository();
        }

        // ====================== GET ALL ======================
        public async Task<List<Pret>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        // ====================== GET BY ID ======================
        public async Task<List<Pret>> GetByNumCompte(string numCompte)
        {
           
            return await _repository.GetByNumCompteAsync(numCompte);
        }

        // ====================== GET BY CIN ======================
        //public async Task<Pret?> GetByCINAsync(string numCIN)
        //{
        //    return await _repository.GetByCINAsync(numCIN);
        //}

        // ====================== ADD ======================
        public async Task<bool> AddAsync(Pret pret)
        {
            try
            {
                await _repository.AddAsync(pret);
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
        public async Task<bool> UpdateAsync(Pret pret)
        {
            try
            {
                await _repository.UpdateAsync(pret);
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
