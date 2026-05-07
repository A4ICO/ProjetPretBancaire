using GestionPretBancaire.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GestionPretBancaire.Services
{
    class S_LoadPrets
    {
        private readonly ObservableCollection<Client> listeClients = new();
        private readonly ObservableCollection<PretViewModel> listePrets = new() ;


        public async Task Charger_Pret(Grid grid)
        {
            var _pretManager = new PretViewModel();
            var prets = await _pretManager.GetAllWithOwner();

            listePrets.Clear();

            foreach (var p in prets)
            {
                var client = listeClients
                    .FirstOrDefault(c => c.NumCompte == p.NumCompte);

                listePrets.Add(new PretViewModel
                {
                    ReferencePret = p.ReferencePret,
                    NomClient = p.NomClient ?? "N/A",
                    PrenomClient = p.PrenomClient ?? "N/A",
                    Montant = p.Montant ,
                    TauxInteret = p.TauxInteret,
                    DateCreation = p.DateCreation ?? "N/A",
                    DateFin = p.DateFin ?? "N/A",
                    TypePret = p.TypePret ?? "N/A",
                    StatusPret = p.StatusPret ?? "N/A"
                });
            }


          
            int row = 0;
            int col = 0;
            int maxCols = 3; // Number of cards per row

            // Define grid rows/columns dynamically
            grid.RowDefinitions.Clear();
            grid.ColumnDefinitions.Clear();

            for (int i = 0; i < maxCols; i++)
                grid.ColumnDefinitions.Add(new ColumnDefinition());

            // Create enough rows for all cards
            int totalRows = (int)Math.Ceiling((double)listePrets.Count / maxCols);
            for (int i = 0; i < totalRows; i++)
                grid.RowDefinitions.Add(new RowDefinition());

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
                    Text = $"Réf: {pret.ReferencePret}",
                    FontWeight = FontWeights.Bold,
                    Foreground = new SolidColorBrush(Color.FromRgb(128, 0, 128)),
                    FontSize = 16
                });

                // Client name
                contentPanel.Children.Add(new TextBlock
                {
                    Text = $"Client: {pret.NomClient} {pret.PrenomClient}",
                    Foreground = Brushes.White
                });

                // Montant
                contentPanel.Children.Add(new TextBlock
                {
                    Text = $"Montant: {pret.Montant:C}",
                    Foreground = Brushes.White
                });

                // Taux
                contentPanel.Children.Add(new TextBlock
                {
                    Text = $"Taux: {pret.TauxInteret}%",
                    Foreground = Brushes.White
                });

                // Type
                contentPanel.Children.Add(new TextBlock
                {
                    Text = $"Type: {pret.TypePret}",
                    Foreground = Brushes.White
                });

                // Status
                contentPanel.Children.Add(new TextBlock
                {
                    Text = $"Statut: {pret.StatusPret}",
                    Foreground = Brushes.White
                });

                contentPanel.Children.Add(new TextBlock
                {
                    Text = $"Date Creation: {pret.DateCreation}",
                    Foreground = Brushes.White
                });

                // Add content to border
                cardBorder.Child = contentPanel;

                // Place card in grid
                Grid.SetRow(cardBorder, row);
                Grid.SetColumn(cardBorder, col);
                grid.Children.Add(cardBorder);

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
