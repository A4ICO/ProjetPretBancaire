using GestionPretBancaire.Models;
using GestionPretBancaire.Repositories;
using System.Windows;

namespace GestionPretBancaire.Controllers
{
    class EcheanceController
    {
        private readonly EcheanceRepository _repository;

        public EcheanceController()
        {
            _repository = new EcheanceRepository();
        }

        // ─────────────────────────────────────────
        // GET ALL
        // ─────────────────────────────────────────
        public async Task<List<Echeance>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting all echeances: " + ex.Message);
                return new List<Echeance>();
            }
        }

        // ─────────────────────────────────────────
        // GET BY CODE
        // ─────────────────────────────────────────
        public async Task<Echeance?> GetAsync(int codeEcheance)
        {
            try
            {
                var echeance = await _repository.GetAsync(codeEcheance);

                if (echeance == null)
                    MessageBox.Show($"No echeance found with code: {codeEcheance}");

                return echeance;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting echeance: " + ex.Message);
                return null;
            }
        }

        // ─────────────────────────────────────────
        // GET BY REFERENCE PRET
        // ─────────────────────────────────────────
        public async Task<List<Echeance>> GetByReferencePretAsync(int referencePret)
        {
            try
            {
                var list = await _repository.GetByReferencePretAsync(referencePret);

                if (list.Count == 0)
                    MessageBox.Show($"No echeances found for pret: {referencePret}");

                return list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting echeances by pret: " + ex.Message);
                return new List<Echeance>();
            }
        }

        // ─────────────────────────────────────────
        // ADD
        // ─────────────────────────────────────────
        public async Task<bool> AddAsync(Echeance echeance)
        {
            try
            {
                // Validation
                if (echeance == null)
                {
                    MessageBox.Show("Echeance cannot be null.");
                    return false;
                }

                if (echeance.ReferencePret <= 0)
                {
                    MessageBox.Show("ReferencePret is invalid.");
                    return false;
                }

                if (echeance.Capital < 0)
                {
                    MessageBox.Show("Balance cannot be negative.");
                    return false;
                }

                await _repository.AddAsync(echeance);
                MessageBox.Show("✅ Echeance added successfully!");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding echeance: " + ex.Message);
                return false;
            }
        }

        // ─────────────────────────────────────────
        // UPDATE
        // ─────────────────────────────────────────
        public async Task<bool> UpdateAsync(Echeance echeance)
        {
            try
            {
                // Validation
                if (echeance == null)
                {
                    MessageBox.Show("Echeance cannot be null.");
                    return false;
                }

                int reference = int.Parse(echeance.CodeEcheance);
                if (reference <= 0)
                {
                    MessageBox.Show("Invalid CodeEcheance.");
                    return false;
                }

                // Check exists before updating
                var existing = await _repository.GetAsync(reference);
                if (existing == null)
                {
                    MessageBox.Show($"Echeance {echeance.CodeEcheance} not found.");
                    return false;
                }

                await _repository.UpdateAsync(echeance);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating echeance: " + ex.Message);
                return false;
            }
        }

        // ─────────────────────────────────────────
        // DELETE
        // ─────────────────────────────────────────
        public async Task<bool> DeleteAsync(int codeEcheance)
        {
            try
            {
                // Check exists before deleting
                var existing = await _repository.GetAsync(codeEcheance);
                if (existing == null)
                {
                    MessageBox.Show($"Echeance {codeEcheance} not found.");
                    return false;
                }

                // Confirm before delete
                var confirm = MessageBox.Show(
                    $"Are you sure you want to delete echeance {codeEcheance}?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning
                );

                if (confirm == MessageBoxResult.No)
                    return false;

                bool deleted = await _repository.DeleteAsync(codeEcheance);

                if (deleted)
                    MessageBox.Show("✅ Echeance deleted successfully!");
                else
                    MessageBox.Show("❌ Delete failed.");

                return deleted;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting echeance: " + ex.Message);
                return false;
            }
        }
    }
}