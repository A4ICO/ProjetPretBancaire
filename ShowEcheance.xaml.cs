using GestionPretBancaire.Models;
using GestionPretBancaire.Repositories;
using System.Collections.Generic;
using System.Windows;

namespace GestionPretBancaire
{
    public partial class ShowEcheance : Window
    {
        private readonly EcheanceRepository _repository = new();

        private List<Echeance> _echeances = new() ;

        public ShowEcheance()
        {
            InitializeComponent();
        }

        
        public async Task ListAllEcheance( int reference)
        {
            try
            {

                _echeances = await _repository.GetByReferencePretAsync(reference);

                //foreach (var echeance in _echeances)
                //{
                //    MessageBox.Show($"CodeEcheance: {echeance.CodeEcheance}, ReferencePret: {echeance.ReferencePret}, Date: {echeance.DateOperation}, Balance: {echeance.Capital}, Interet: {echeance.Interet}, SoldeRestant: {echeance.SoldeRestant}, Operation: {echeance.Operation}, DatePaiment: {echeance.DatePaiment}");
                //}

                Tab_Echeance.ItemsSource = _echeances;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur :\n{ex.Message}\n{ex.InnerException?.Message}",
                               "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

   
}