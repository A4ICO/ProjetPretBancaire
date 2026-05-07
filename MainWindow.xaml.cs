using GestionPretBancaire.Managers;
using GestionPretBancaire.Models;
using GestionPretBancaire.ViewModels;
using System.Windows;
using GestionPretBancaire.Services;
namespace GestionPretBancaire
{
    public partial class MainWindow : Window
    {
        private readonly ClientManager _clientManager;
        private readonly PretManager _pretManager;
        private readonly S_LoadPrets _s_loadPrets = new();


        public MainWindow()
        {
            InitializeComponent();
            _clientManager = new ClientManager();
            _pretManager = new PretManager();

            // Load data when window opens
            LoadClients();
            LoadPrets();
           _s_loadPrets.Charger_Pret(PretList);
        }
        private async void LoadClients()
        {


            try
            {
                var clients = await _clientManager.GetAllAsync();
                Tab_Client.ItemsSource = clients;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur chargement clients :\n" + ex.Message,
                              "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)   
        {
            var addWindow = new Window1();
            addWindow.ShowDialog();

            LoadClients();
        }

        private async void BtnModifier_Click(object sender, RoutedEventArgs e)
        {
            if (Tab_Client.SelectedItem is Client selected)
            {
                MessageBox.Show($"Modification de {selected.PrenomClient} {selected.NomClient} (à implémenter)");
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un client.");
            }
        }

        private async void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (Tab_Client.SelectedItem is Client selected)
            {
                var result = MessageBox.Show(
                    $"Supprimer {selected.PrenomClient} {selected.NomClient} ?",
                    "Confirmation",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    bool success = await _clientManager.DeleteAsync(selected.NumCompte);
                    if (success)
                    {
                        MessageBox.Show("Client supprimé avec succès !");
                        LoadClients();
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un client à supprimer.");
            }
        }

        private async void LoadPrets()
        {
            try
            {
                var prets = await _pretManager.GetAllAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur chargement des prêts :\n" + ex.Message);
            }
        }

        private void BtnNouveauPret_Click(object sender, RoutedEventArgs e)
        {
            // Open new Pret window later
            MessageBox.Show("Fenêtre de création de prêt (à créer)");
        }

    }
}
