using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using GestionPretBancaire.Managers;
using GestionPretBancaire.Models;
using GestionPretBancaire.ViewModels;

namespace GestionPretBancaire
{
    public partial class MainWindow : Window
    {
        private readonly ClientManager _clientManager;
        private readonly PretManager _pretManager;
        private readonly ObservableCollection<Client> listeClients = new();
        private readonly ObservableCollection<PretViewModel> listePrets = new();

        public MainWindow()
        {
            InitializeComponent();
            Debug.Write("Yes");
            _clientManager = new ClientManager();
            _pretManager = new PretManager();

            // Load data when window opens
            LoadClients();
            LoadPrets();
            Charger_Prets();
        }

        // ====================== CLIENTS ======================
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

        private void Button_Click(object sender, RoutedEventArgs e)   // Ajouter Client
        {
            var addWindow = new Window1();   // Your existing Add Client window
            addWindow.ShowDialog();

            LoadClients();        // Refresh after adding
        }

        private async void BtnModifier_Click(object sender, RoutedEventArgs e)
        {
            if (Tab_Client.SelectedItem is Client selected)
            {
                MessageBox.Show($"Modification de {selected.Prenom} {selected.Nom} (à implémenter)");
                // Later: Open edit window then call LoadClients()
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
                    $"Supprimer {selected.Prenom} {selected.Nom} ?",
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

         //====================== PRETS ======================
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
            // After creation: LoadPrets();
        }







        private async Task Charger_Prets()
        {
            var prets = await _pretManager.GetAllAsync();

            listePrets.Clear();

            foreach (var p in prets)
            {
                var client = listeClients
                    .FirstOrDefault(c => c.NumCompte == p.NumCompte);

                listePrets.Add(new PretViewModel
                {
                    Ref_Pret = p.Reference,
                    Nom_Client = client?.Nom ?? "N/A",
                    Prenom_Client = client?.Prenom ?? "N/A",
                    Montant_Pret = p.GetMontant(),
                    Taux_Interet = p.TauxInteret,
                    Duree_Mois = 12,
                    Type_Pret = p.TypePret,
                    Status_Pret = p.Status
                });
            }

            foreach(var pret in listePrets)
            {
                Debug.WriteLine($"Pret: {pret.Ref_Pret}, Client: {pret.Nom_Client} {pret.Prenom_Client}");
            }

            Releve_Pret.ItemsSource = null;   // force refresh
            Releve_Pret.ItemsSource = listePrets;
        }
        //private async Task Charger_Prets()
        //{
        //    var prets = await _pretManager.GetAllAsync();

        //    listePrets.Clear();
        //    foreach (var p in prets)
        //    {

        //        var client = listeClients.FirstOrDefault(c => c.NumCompte == p.Reference);

        //        listePrets.Add(new PretViewModel
        //        {
        //            Ref_Pret = p.Reference,
        //            Nom_Client = client?.Nom , 
        //            Prenom_Client = client?.Prenom ,
        //            Montant_Pret = p.GetMontant(),
        //            Taux_Interet = p.TauxInteret,
        //            Duree_Mois = 12,
        //            Type_Pret = p.TypePret,
        //            Status_Pret = p.Status
        //        });
        //    }

        //    Releve_Pret.ItemsSource = listePrets;
        //}


    }
}