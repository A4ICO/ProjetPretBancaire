using GestionPretBancaire.Managers;
using GestionPretBancaire.Models;
using GestionPretBancaire.Services;
using GestionPretBancaire.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GestionPretBancaire.Windows;


namespace GestionPretBancaire
{
    public partial class MainWindow : Window
    {
        private readonly ClientManager _clientManager;
        private readonly PretManager _pretManager;
        private readonly ObservableCollection<PretViewModel> listePrets = new();

        public MainWindow()
        {
            InitializeComponent();

            _clientManager = new ClientManager(); // ✅ before use
            _pretManager = new PretManager();     // ✅ before use

            LoadClients();
            Charger_Pret(PretList);
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

        public async Task Charger_Pret(Grid grid)
        {

            grid.Children.Clear();
            grid.RowDefinitions.Clear();
            grid.ColumnDefinitions.Clear();
            var _pretManager = new PretViewModel();
            var prets = await _pretManager.GetAllWithOwner();

            //MessageBox.Show($"Loaded: {prets.Count} prets");
            //listePrets.Clear();

            foreach (var p in prets)
            {

                listePrets.Add(new PretViewModel
                {
                    ReferencePret = p.ReferencePret,
                    NomClient = p.NomClient ?? "N/A",
                    PrenomClient = p.PrenomClient ?? "N/A",
                    Montant = p.Montant,
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
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            // Create enough rows for all cards
            int totalRows = (int)Math.Ceiling((double)listePrets.Count / maxCols);
            for (int i = 0; i < totalRows; i++)
                grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

            foreach (var pret in listePrets)
            {
                // ── Card container 
                Border cardBorder = new Border
                {
                    Background = new SolidColorBrush(Color.FromRgb(30, 32, 48)),   // #1E2030
                    CornerRadius = new CornerRadius(10),
                    Margin = new Thickness(0, 0, 12, 12),
                    Padding = new Thickness(16),
                    BorderBrush = new SolidColorBrush(Color.FromRgb(31, 34, 53)),   // #1F2235
                    BorderThickness = new Thickness(1)
                };

                StackPanel contentPanel = new StackPanel();

                // ── Reference 
                contentPanel.Children.Add(new TextBlock
                {
                    Text = pret.ReferencePret ?? "—",
                    FontSize = 11,
                    FontFamily = new FontFamily("Consolas"),
                    Foreground = new SolidColorBrush(Color.FromRgb(107, 114, 128)),  // muted gray
                    Margin = new Thickness(0, 0, 0, 10)
                });

                // ── Avatar + Client name row 
                StackPanel nameRow = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 0, 0, 14)
                };

                string initials =
                    $"{pret.PrenomClient?.FirstOrDefault()}{pret.NomClient?.FirstOrDefault()}";

                Border avatar = new Border
                {
                    Width = 30,
                    Height = 30,
                    CornerRadius = new CornerRadius(15),
                    Background = new SolidColorBrush(Color.FromRgb(99, 102, 241)), // indigo
                    Margin = new Thickness(0, 0, 10, 0),
                    VerticalAlignment = VerticalAlignment.Center
                };
                avatar.Child = new TextBlock
                {
                    Text = initials.ToUpper(),
                    FontSize = 11,
                    FontWeight = FontWeights.SemiBold,
                    Foreground = Brushes.White,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                nameRow.Children.Add(avatar);
                nameRow.Children.Add(new TextBlock
                {
                    Text = $"{pret.PrenomClient} {pret.NomClient}",
                    FontSize = 14,
                    FontWeight = FontWeights.SemiBold,
                    Foreground = Brushes.White,
                    VerticalAlignment = VerticalAlignment.Center
                });
                contentPanel.Children.Add(nameRow);

                // ── Helper: key / value row ─
                void AddRow(string key, string value, Brush valueBrush = null)
                {
                    Grid row = new Grid { Margin = new Thickness(0, 0, 0, 6) };
                    row.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                    row.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                    TextBlock keyTb = new TextBlock
                    {
                        Text = key,
                        FontSize = 12,
                        Foreground = new SolidColorBrush(Color.FromRgb(107, 114, 128))
                    };
                    TextBlock valTb = new TextBlock
                    {
                        Text = value,
                        FontSize = 12,
                        FontWeight = FontWeights.SemiBold,
                        Foreground = valueBrush
                                            ?? new SolidColorBrush(Color.FromRgb(156, 163, 175)),
                        TextAlignment = TextAlignment.Right,
                        HorizontalAlignment = HorizontalAlignment.Right
                    };

                    Grid.SetColumn(keyTb, 0);
                    Grid.SetColumn(valTb, 1);
                    row.Children.Add(keyTb);
                    row.Children.Add(valTb);
                    contentPanel.Children.Add(row);
                }

                AddRow("Montant", $"{pret.Montant:C}",
                    new SolidColorBrush(Color.FromRgb(165, 180, 252)));  // soft indigo
                AddRow("Taux", $"{pret.TauxInteret} %");
                AddRow("Type", pret.TypePret ?? "—");

                // Divider 
                contentPanel.Children.Add(new Border
                {
                    Height = 1,
                    Background = new SolidColorBrush(Color.FromRgb(31, 34, 53)),
                    Margin = new Thickness(0, 8, 0, 10)
                });

                // ── Footer: date + status badge 
                Grid footer = new Grid();
                footer.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                footer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                footer.Children.Add(new TextBlock
                {
                    Text = pret.DateCreation ?? "—",
                    FontSize = 11,
                    Foreground = new SolidColorBrush(Color.FromRgb(107, 114, 128)),
                    VerticalAlignment = VerticalAlignment.Center
                });

                // Badge color based on status
                (Color bgColor, Color fgColor) = (pret.StatusPret ?? "").ToLower() switch
                {
                    "actif" => (Color.FromArgb(40, 74, 222, 128),
                                     Color.FromRgb(74, 222, 128)),   // green
                    "en attente" => (Color.FromArgb(40, 251, 191, 36),
                                     Color.FromRgb(251, 191, 36)),   // amber
                    _ => (Color.FromArgb(40, 248, 113, 113),
                                     Color.FromRgb(248, 113, 113))    // red
                };

                Border badge = new Border
                {
                    Background = new SolidColorBrush(bgColor),
                    CornerRadius = new CornerRadius(12),
                    Padding = new Thickness(10, 3, 10, 3),
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Center
                };
                badge.Child = new TextBlock
                {
                    Text = pret.StatusPret ?? "—",
                    FontSize = 11,
                    FontWeight = FontWeights.SemiBold,
                    Foreground = new SolidColorBrush(fgColor)
                };

                Grid.SetColumn(badge, 1);
                footer.Children.Add(badge);
                contentPanel.Children.Add(footer);

                // ── Assemble and place in grid ─
                cardBorder.Child = contentPanel;
                Grid.SetRow(cardBorder, row);
                Grid.SetColumn(cardBorder, col);
                grid.Children.Add(cardBorder);

                col++;
                if (col >= maxCols)
                {
                    col = 0;
                    row++;
                }
            }

        }
    

        private void BtnNouveauPret_Click(object sender, RoutedEventArgs e)
        {
            var w = new AddPretWindow();

            if (w.ShowDialog() == true)
                Charger_Pret(PretList);

        }

    }
}
