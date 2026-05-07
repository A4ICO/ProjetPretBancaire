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
        public string? DateFin { get; set; }
        public List<Echeance>? Echeances { get; set; }

        public double GetMontant() {  return Montant; }
        public void SetMontant(double montant)
        {
            if (montant <= 0)
                throw new ArgumentException("Le montant doit être supérieur à 0.");
            Montant = montant;
        }

    }
}

