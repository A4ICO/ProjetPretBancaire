using System;
using System.Collections.Generic;
using System.Text;

namespace GestionPretBancaire.Models
{
    public class PretViewModel
    {
        public string? Ref_Pret { get; set; }
        public string? Nom_Client { get; set; }
        public string? Prenom_Client { get; set; }
        public int Id_Client { get; set; }
        public double Montant_Pret { get; set; }
        public decimal Taux_Interet { get; set; }
        public int Duree_Mois { get; set; }
        public string? Type_Pret { get; set; }
        public string? Status_Pret { get; set; }
    }
}


