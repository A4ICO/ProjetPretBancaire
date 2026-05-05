namespace GestionPretBancaire.Models
{
    public class Client
    {
        public string NumCompte { get; set; } = string.Empty;
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public string NumTel { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Adresse { get; set; } = string.Empty;
        public string NumCIN { get; set; } = string.Empty;
        //public DateTime DateCreation { get; set; } = DateTime.Now;

        // Relation avec les prêts
        //public virtual ICollection<Pret> Pret { get; set; } = new List<Pret>();

        public string NomComplet => $"{Prenom} {Nom}";
    }
}