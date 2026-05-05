namespace GestionPretBancaire.Models
{
    public class Echeance
    {
       public String? CodeEcheance { get ; set; }
        public int ReferencePret { get ; set; }
        public String? Date { get ; set; }
        public Double? Balance { get ; set; }
        public Double? Interet {  get ; set; }
        public Double?  SoldeRestant { get ; set; }
        public String?  Operation { get ; set ; }
        public String? DatePaiment {  get ; set; }
    }
}
