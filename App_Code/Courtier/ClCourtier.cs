using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS2013
{
    class ClCourtier
    {
        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        public ClCourtier()
        { }

        /// <summary>
        /// String permettant de se connecter à la base de données.
        /// </summary>
        public static string StringDeConnexion
        {
            get { return Utilitaire.GetConnectionString("DonneesCreditsSante"); }
        }
    }
}
