using Dapper;
using GestionPretBancaire.Helpers;
using System.Windows;
using System.Windows.Automation;

namespace GestionPretBancaire.Models
{
    public class PretViewModel
    {
        public string? ReferencePret { get; set; }
        public string? NumCompte { get; set; }
        public string? NomClient { get; set; }
        public string? PrenomClient { get; set; }
        public double Montant { get; set; }
        public decimal TauxInteret { get; set; }
        public string? TypePret { get; set; }
        public string? StatusPret { get; set; }
        public string? DateCreation { get; set; }
        public string? DateFin { get; set; }


        public async Task<List<PretViewModel?>> GetAllWithOwner()
        {
            string query ="SELECT p.ReferencePret, p.NumCompte, c.NomClient, c.PrenomClient, " +
                "p.Montant, p.TauxTnteret, p.TypePret, p.SatusPret, " +
                "p.DateCreation, p.DateFin " +
                "FROM Pret p LEFT JOIN Client c ON p.NumCompte = c.NumCompte";

            try
            {
                using (var conn = new DatabaseHelper().getConnection())
                {
                    var result = await conn.QueryAsync<PretViewModel>(query);

                    //foreach (var item in result)
                    //{
                    //    MessageBox.Show($"{item.Montant} {item.StatusPret}");
                    //}
                    return result.AsList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"SQL Error: {ex.Message}\n{ex.InnerException?.Message}");
                return new List<PretViewModel?>();
            }
        }
    }
    }


