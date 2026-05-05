using GestionPretBancaire.Helpers;
using GestionPretBancaire.Models;
using GestionPretBancaire.Repositories;
using MySql.Data.MySqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GestionPretBancaire
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Loaded += async (s, e) => await InitAsync();
        }

        //private async Task InitAsync()
        //{

            
        //}

        private async void addNewClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var c = new Client();
                c.NumCompte = "12345678";
                c.Nom = "D0";
                c.Prenom = "John";
                c.NumTel = "1234567790";
                c.Email = "Ravabba@gmail.com";
                c.Adresse = "123 Main St, Anytown, USA";
                c.NumCIN = "AB12456";

                var clientRepo = new ClientRepository();
                await clientRepo.AddAsync(c);

                MessageBox.Show("✅ Client added successfully!");
            }
            catch (Exception ex)
            {
                // This will show you EXACTLY what is wrong
                MessageBox.Show("ERROR: " + ex.Message + "\n\n" + ex.StackTrace);
            }
        }

        private async void existsClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string numCompte = "12345678"; // ← changer pour tester

                bool exists = await _clientRepo.ExistsAsync(numCompte);

                MessageBox.Show(exists
                    ? $"✅ Client {numCompte} EXISTS dans la base"
                    : $"❌ Client {numCompte} n'existe PAS dans la base"
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message + "\n\n" + ex.StackTrace);
            }
        }
    }

    }




