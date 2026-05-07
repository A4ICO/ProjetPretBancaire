namespace GestionPretBancaire.Models
{
    public class Echeance
    {
       public String? CodeEcheance { get ; set; }
        public int ReferencePret { get ; set; }
        public String? DateOperation { get ; set; }
        public Double? Capital { get ; set; }
        public Double? Interet {  get ; set; }
        public Double?  SoldeRestant { get ; set; }
        public String?  Operation { get ; set ; }
        public String? DatePaiment {  get ; set; }
        public String? StatutPaiement { get; set; }
    }
}
