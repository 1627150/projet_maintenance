using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS2013
{
    class ClCapsuleSante
    {
        public ClCapsuleSante()
        { }

        public ClCapsuleSante(string p_titre, DateTime p_datePublication, string p_contenu)
        {
            Titre = p_titre;
            DatePublication = p_datePublication;
            Contenu = p_contenu;
        }

        public ClCapsuleSante(string p_id, string p_titre, DateTime p_datePublication, string p_contenu)
        {
            ID = p_id;
            Titre = p_titre;
            DatePublication = p_datePublication;
            Contenu = p_contenu;
        }

        public string Titre
        {
            get;
            set;
        }

        public DateTime DatePublication
        {
            get;
            set;
        }

        public string Contenu
        {
            get;
            set;
        }

        public string ID
        {
            get;
            set;
        }

        /// <summary>
        /// Condition javascript dans le cas d'une supression de Concentration
        /// </summary>
        public const string JS_CONFIRMATION_SUPRESSION =
            "if (confirm('Voulez-vous vraiment supprimer cette Capsule Santé ?') == false) return false;";
    }
}
