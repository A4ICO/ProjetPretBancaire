using GestionPretBancaire.Managers;
using GestionPretBancaire.Models;
using GestionPretBancaire.ViewModels;
using System.Windows.Controls;
using System.Windows.Media;
//using DrawingColor = System.Drawing.Color;
//using DrawingBrushes = System.Drawing.Brushes;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

//using static LinqToDB.SqlQuery.SqlPredicate;

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
                MessageBox.Show($"Modification de {selected.Prenom} {selected.Nom} (à implémenter)");
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
                    //Duree_Mois = 12,
                    Type_Pret = p.TypePret,
                    Status_Pret = p.Status
                });
            }

            foreach(var pret in listePrets)
            {
                Debug.WriteLine($"Pret: {pret.Ref_Pret}, Client: {pret.Nom_Client} {pret.Prenom_Client}");
            }

            Releve_Pret.ItemsSource = null;              
            Releve_Pret.ItemsSource = listePrets;

int row = 0;
            int col = 0;
            int maxCols = 3; // Number of cards per row

            // Define grid rows/columns dynamically
            Acceuil_grid.RowDefinitions.Clear();
            Acceuil_grid.ColumnDefinitions.Clear();

for (int i = 0; i < maxCols; i++)
                    Acceuil_grid.ColumnDefinitions.Add(new ColumnDefinition());

            // Create enough rows for all cards
            int totalRows = (int)Math.Ceiling((double)listePrets.Count / maxCols);
            for (int i = 0; i < totalRows; i++)
                Acceuil_grid.RowDefinitions.Add(new RowDefinition());

            foreach (var pret in listePrets)
            {
                // Create card border
                Border cardBorder = new Border
                {
                    Background = new SolidColorBrush(Color.FromRgb(128, 0, 128)), // Purple
                    CornerRadius = new CornerRadius(8),
                    Margin = new Thickness(8),
                    Padding = new Thickness(10),
                    BorderBrush = Brushes.White,
                    BorderThickness = new Thickness(1)
                };

                // StackPanel for card content
                StackPanel contentPanel = new StackPanel();

                // Title: Pret Reference
                contentPanel.Children.Add(new TextBlock
                {
                    Text = $"Réf: {pret.Ref_Pret}",
                    FontWeight = FontWeights.Bold,
                    Foreground = new SolidColorBrush(Color.FromRgb(128, 0, 128)),
                    FontSize = 16
                });

                // Client name
                contentPanel.Children.Add(new TextBlock
                {
                    Text = $"Client: {pret.Nom_Client} {pret.Prenom_Client}",
                    Foreground = Brushes.White
                });

                // Montant
                contentPanel.Children.Add(new TextBlock
                {
                    Text = $"Montant: {pret.Montant_Pret:C}",
                    Foreground = Brushes.White
                });

                // Taux
                contentPanel.Children.Add(new TextBlock
                {
                    Text = $"Taux: {pret.Taux_Interet}%",
                    Foreground = Brushes.White
                });

                // Type
                contentPanel.Children.Add(new TextBlock
                {
                    Text = $"Type: {pret.Type_Pret}",
                    Foreground = Brushes.White
                });

                // Status
                contentPanel.Children.Add(new TextBlock
                {
                    Text = $"Statut: {pret.Status_Pret}",
                    Foreground = Brushes.White
                });

                // Add content to border
                cardBorder.Child = contentPanel;

                // Place card in grid
                Grid.SetRow(cardBorder, row);
                Grid.SetColumn(cardBorder, col);
                Acceuil_grid.Children.Add(cardBorder);

                // Move to next cell
                col++;
                if (col >= maxCols)
                {
                    col = 0;
                    row++;
                }
            }




        }

    }
}
