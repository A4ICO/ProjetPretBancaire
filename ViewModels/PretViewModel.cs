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


        /// 
        /// 
        ///  Request to retrieve all loans from the database
        /// 
        ///
        public async Task<List<Pret>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }



        /// 
        /// 
        ///  Request to retrieve a loan by its numero de compte from the database
        /// 
        ///
        public async Task<List<Pret>> GetByNumCompte(string numCompte)
        {
           
            return await _repository.GetByNumCompteAsync(numCompte);
        }



        /// 
        /// 
        ///  Add a new loan to the database
        /// 
        ///
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



        /// 
        /// 
        /// Update an existing pret in the database
        /// 
        ///
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



        /// 
        /// 
        ///  Delete a pret from the database by numero de compte
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
