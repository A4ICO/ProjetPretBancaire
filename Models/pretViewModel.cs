using Dapper;
using GestionPretBancaire.Helpers;
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
            string query = @"
                SELECT p.ReferencePret, c.NomClient, c.PrenomClient,
                c.NumCompte, p.Montant, p.TauxInteret, 
                p.DateCreation,p.DateFin, p.TypePret, p.statusPret
                FROM Pret p
                INNER JOIN Client c ON p.NumCompte = c.NumCompte;";

            try
            {

                using (var conn = new DatabaseHelper().getConnection())
                {
                    var result = await conn.QueryAsync<PretViewModel>(query);
                   return result.ToList();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching data: {ex.Message}");
                return null;
            }
           }
        }
    }


