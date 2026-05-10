using GestionPretBancaire.Managers;
using GestionPretBancaire.ViewModels;
using System.Windows;
using System.Windows.Controls;
using GestionPretBancaire.Models;
using GestionPretBancaire.Repositories;

namespace GestionPretBancaire
{
    /// <summary>
    /// Logique d'interaction pour AddNewPrettWindow.xaml
    /// </summary>
    public partial class AddNewPrettWindow : Window
    {

        private readonly ClientManager _clientManager = new();
        private readonly PretManager _pretManager = new();
        private readonly PretRepository _pretRepository = new();
        private List<Client> _allClients = new();
        private Client? _selectedClient;
        public AddNewPrettWindow()
        {
            InitializeComponent();
            LoadClients();
        }



        // Load all clients once
        private async void LoadClients()
        {
            try
            {
                _allClients = await _clientManager.GetAllAsync() ?? new List<Client>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur chargement clients :\n{ex.Message}",
                                "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Live search as user types  
        private void TxtClientSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string query = TxtClientSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(query))
            {
                SearchResultsPanel.Visibility = Visibility.Collapsed;
                LstClientResults.ItemsSource = null;
                return;
            }

            var filtered = _allClients
                .Where(c =>
                    (c.NomClient ?? "").ToLower().Contains(query) ||
                    (c.PrenomClient ?? "").ToLower().Contains(query) ||
                    (c.NumCompte ?? "").ToLower().Contains(query))
                .Take(8)
                .ToList();

            LstClientResults.ItemsSource = filtered;
            SearchResultsPanel.Visibility = filtered.Count > 0
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        // Client selected from list  
        private void LstClientResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LstClientResults.SelectedItem is not Client client) return;

            _selectedClient = client;
            //MessageBox.Show($"Client sélectionné : {client.PrenomClient} {client.NomClient}",
            //                "Client Sélectionné", MessageBoxButton.OK, MessageBoxImage.Information);

            // Build initials
            string initials =
                $"{client.PrenomClient?.FirstOrDefault()}{client.NomClient?.FirstOrDefault()}"
                .ToUpper();

            TxtSelectedInitials.Text = initials;
            TxtSelectedClientName.Text = $"{client.PrenomClient} {client.NomClient}";
            TxtSelectedClientAccount.Text = $"N° {client.NumCompte}";

            // Show badge, hide search
            SelectedClientBadge.Visibility = Visibility.Visible;
            SearchResultsPanel.Visibility = Visibility.Collapsed;
            TxtClientSearch.Text = string.Empty;
            TxtClientSearch.Visibility = Visibility.Collapsed;
        }

        // Clear selected client
        private void BtnClearClient_Click(object sender, RoutedEventArgs e)
        {
            _selectedClient = null;
            SelectedClientBadge.Visibility = Visibility.Collapsed;
            TxtClientSearch.Visibility = Visibility.Visible;
            TxtClientSearch.Text = string.Empty;
            TxtClientSearch.Focus();
        }

        // Save
        private async void BtnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            // Validation
            if (_selectedClient == null)
            {
                MessageBox.Show("Veuillez sélectionner un client.",
                                "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!double.TryParse(TxtMontant.Text, out double montant) || montant <= 0)
            {
                MessageBox.Show("Veuillez entrer un montant valide.",
                                "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!decimal.TryParse(TxtTaux.Text, out decimal taux) || taux <= 0)
            {
                MessageBox.Show("Veuillez entrer un taux d'intérêt valide.",
                                "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (CmbTypePret.SelectedItem is not ComboBoxItem typeItem)
            {
                MessageBox.Show("Veuillez sélectionner un type de prêt.",
                                "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (CmbStatut.SelectedItem is not ComboBoxItem statutItem)
            {
                MessageBox.Show("Veuillez sélectionner un statut.",
                                "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
               

                var pret = new Pret(montant)
                {
                    NumCompte = _selectedClient!.NumCompte,
                    TauxInteret = taux,
                    TypePret = typeItem.Content.ToString()!,
                    Status = statutItem.Content.ToString()!,
                    DateCreation = DpDateCreation.SelectedDate?.ToString("yyyy-MM-dd") ?? "",
                    DateFin = DpDateFin.SelectedDate?.ToString("yyyy-MM-dd") ?? ""
                };

                
                await _pretRepository.AddAsync(pret);
                DialogResult = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur :\n{ex.Message}",
                                "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        // Cancel
        private void BtnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
