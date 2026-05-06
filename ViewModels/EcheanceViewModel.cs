using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using GestionPretBancaire.Models;
using GestionPretBancaire.Repositories;

namespace GestionPretBancaire.Managers
{
    public class EcheanceManager
    {
        private readonly EcheanceRepository _repository;

        public EcheanceManager()
        {
            _repository = new EcheanceRepository();
        }

        // ====================== GET ALL ======================
        public async Task<List<Echeance>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        // ====================== GET BY PRET ======================
        public async Task<List<Echeance>> GetByReferencePretAsync(int referencePret)
        {
            return await _repository.GetByReferencePretAsync(referencePret);
        }

        // ====================== ADD ======================
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

        // ====================== UPDATE ======================
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

        // ====================== DELETE ======================
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

        // ====================== GET ONE ======================
        public async Task<Echeance?> GetByIdAsync(int codeEcheance)
        {
            return await _repository.GetAsync(codeEcheance);
        }
    }
}