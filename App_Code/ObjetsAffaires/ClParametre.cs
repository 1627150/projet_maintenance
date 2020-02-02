using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS2013
{
    class ClParametre
    {
        public enum TypeParametre
        {
            MaxCreditsSemaine,
            MaxCreditsSeance,
            MaxTempsEntrainement,
            MaxTempsNotification,
            MaxSeancesJour,
            DateDebutBougeotte,
            DateFinBougeotte,
            FournisseurServiceCell,
            NotifiedAdminEmail,
            UtilisationInviteAmi
        }

        public ClParametre()
        { }

        public ClParametre(string p_nom, int p_valeur)
        {
            Nom = p_nom;
            Valeur = p_valeur;
        }

        public ClParametre(string p_nom, DateTime date)
        {
            Nom = p_nom;
            Date = date;
        }

        public ClParametre(string p_nom, string p_value)
        {
            Nom = p_nom;
            Value = p_value;
        }

        public string Nom
        {
            get;
            set;
        }

        public int Valeur
        {
            get;
            set;
        }

        public DateTime Date
        {
            get;
            set;
        }

        public string Value
        {
            get;
            set;
        }
    }
}
