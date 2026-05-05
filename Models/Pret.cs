namespace GestionPretBancaire.Models
{
    public class Pret
    {
        public String? Reference { get; set; }
        public String? NumCompte { get; set; }  
        public String? TypePret { get; set; }
        private double Montant { get; set; }
        public decimal TauxInteret { get; set; }
        public string? Status { get; set; }
        public string? DateCreation { get; set; }

    }
}
