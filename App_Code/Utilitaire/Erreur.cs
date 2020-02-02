using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS2013
{
    class Erreur
    {
        public static readonly string CONNECTION = "Cette combinaison utilisateur et mot de passe est incorrecte.";
        public static readonly string MIN_CHAR_MDP = "Le mot de passe doit avoir au minimum " + ClCompte.NB_CHARACTÈRE_MOT_DE_PASSE + " caractères.";

        public static readonly string UTILISATEUR_EXISTE = "Le nom d'utilisateur existe déjà.";
        public static readonly string CONFIRMATION_MDP = "Le mot de passe et sa confirmation doivent être identiques.";
        public static readonly string CONFIRMATION_MDP_MANQUANTE = "La confirmation du mot de passe est obligatoire.";

        public static readonly string MAX_CREDIT_ATTEINT = "Le maximum de <i>Crédits $anté<sup>®</sup></i> par semaine est de : ";
        public static readonly string MAX_SEANCE_ATTEINT = "Le maximum de séances permises par jour est de : ";
        public static readonly string DUREE_INVALIDE = "Veuillez entrer une durée d'activité valide.<br>Maximum : ";
        public static readonly string CREDITS_INVALIDE = "Veuillez entrer un nombre de crédits valide.<br>Maximum : ";
        public static readonly string CHAMPS_INVALIDE = "Veuillez entrer une durée en minutes OU le nombre de <i>Crédits $anté<sup>®</sup></i>.";

        public static readonly string AJOUT = "Une erreur est survenue lors de l'ajout.";
        public static readonly string MISE_A_JOUR = "Une erreur est survenue pendant la mise à jour.";
        public static readonly string SUPPRESSION = "Une erreur est survenue pendant la supression.";

        public static readonly string SUPPRESSION_COMPTE_COURANT = "Vous ne pouvez pas supprimer votre propre compte.";

        public static readonly string CONCENTRATION_UTILISÉE = "Impossible de supprimer cette concentration " +
            "puisque des utilisateurs l'utilisent dans leur profil.";
        public static readonly string CONCENTRATION_EXISTE = "Cette concentration existe déjà.";
        public static readonly string ACTIVITE_EXISTE = "Une activité portant ce nom existe déjà.";
    }
}
