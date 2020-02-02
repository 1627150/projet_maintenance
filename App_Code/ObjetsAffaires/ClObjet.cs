using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS2013
{
    /// <summary>
    /// Représente l'instance de n'importes quels objet de transport dans le projet. 
    /// Comme cette classe est abstraite, elle contient seulement ce qui est utilisé par chaques objet, leur spécification comme 
    /// les propritées sont définitent par les enfants.
    /// Auteurs : RPDG & FL
    /// </summary>
    public abstract class ClObjet
    {
        /// <summary>
        /// Contient les différentes erreurs que l'objet peux contenir dans ses données. La "Key" est le nom du label dans lequel faire afficher le message
        /// d'erreur, la "Value" contient le message à faire afficher. Au départ, ce tableau est rempli dans la fonction validé.
        /// </summary>
        public Dictionary<String, String> tableauErreurs = new Dictionary<string, string>();

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public ClObjet()
        { }

        /// <summary>
        /// Regarde chaque propriétés de l'objet et vérifie si elle sont valide et prête à être entré dans la base de données.
        /// </summary>
        abstract public void Valider();
    }
}
