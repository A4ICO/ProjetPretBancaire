using GestionPretBancaire.Models;
using GestionPretBancaire.Repositories;
using System.Collections.Generic;
using System.Windows;

namespace GestionPretBancaire
{
    public partial class ShowEcheance : Window
    {
        private readonly EcheanceRepository _repository = new();
        private List<Echeance> _echeances = new();
        private int _referencePret = 3;

        public ShowEcheance()
        {
            InitializeComponent();
        }

        public ShowEcheance(int referencePret) 
        {
            //_referencePret = referencePret;
            _ = ListAllEcheance(); // load on open
        }

        public async Task ListAllEcheance()
        {
            try
            {
                _echeances = await _repository.GetByReferencePretAsync(_referencePret);


                if (_echeances.Count == 0)
                    MessageBox.Show("Aucune échéance trouvée pour ce prêt.",
                                    "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                MessageBox.Show($"Nombre d'échéances trouvées : {_echeances.Count}", "Résultat",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                Tab_Echeance.ItemsSource = _echeances;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur :\n{ex.Message}", "Erreur",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }


            

        }
    }
}