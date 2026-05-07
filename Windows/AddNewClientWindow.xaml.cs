using GestionPretBancaire.Models;
using GestionPretBancaire.Repositories;
using GestionPretBancaire.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GestionPretBancaire
{
   
    public partial class Window1 : Window
    {

        public Window1()
        {
            InitializeComponent();
            var _repository = new ClientRepository();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var result = MessageBox.Show(
                "Voulez-vous vraiment annuler ?",
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(txt_Compte.Text) ||
                    string.IsNullOrWhiteSpace(txt_Nom.Text) ||
                    string.IsNullOrWhiteSpace(txt_Prenom.Text) ||
                    string.IsNullOrWhiteSpace(txt_Email.Text))
                {
                    MessageBox.Show("Les champs Numéro de Compte, Nom, Prénom et Email sont obligatoires.",
                                    "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var nouveauClient = new Client
                {
                    NumCompte = txt_Compte.Text.Trim(),
                    NomClient = txt_Nom.Text.Trim(),
                    PrenomClient = txt_Prenom.Text.Trim(),
                    NumTel = txt_Tel.Text?.Trim(),
                    Email = txt_Email.Text.Trim(),
                    Adresse = txt_Adresse.Text?.Trim(),
                    NumCIN = txt_CIN.Text?.Trim()
                };

                var clientRepository = new ClientRepository();
await clientRepository.AddAsync(nouveauClient);

                // === Success ===
                MessageBox.Show("Client enregistré avec succès !",
                                "Succès",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);

                DialogResult = true; 
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'enregistrement :\n\n{ex.Message}",
                                "Erreur",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }
    }
}
