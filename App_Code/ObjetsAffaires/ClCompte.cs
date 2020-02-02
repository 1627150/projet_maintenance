using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS2013
{
    /// <summary>
    /// Représente l'instance d'un compte dans le programme. Il est représenté par son nom d'utilisateur (numéro de DA et d'employé seulement), un mot de passe, 
    /// la concentration dans lequel le compte est (informatique, employé de la bibliothèque, design intérieur, etc.) et du rôle qu'il 
    /// a dans le programme.
    /// Auteurs : RPDG & FL
    /// </summary>
    class ClCompte : ClObjet
    {
        /// <summary>
        /// Représente les différents rôles qu'un compte peut avoir
        /// </summary>
        public enum TypeCompte
        {
            Administrateur,
            Utilisateur,
            Entraineur,
            Invalide
        }

        /// <summary>
        /// Le nombre de charactère pour qu'un mot de passe soit valide.
        /// </summary>
        public const int NB_CHARACTÈRE_MOT_DE_PASSE = 6;

        /// <summary>
        /// La condition javascript pour valider si l'utilisateur veut vraiment supprimer le compte.
        /// </summary>
        public const string JS_CONFIRMATION_SUPRESSION =
            "if (confirm('Voulez-vous vraiment supprimer ce compte ?') == false) return false;";

        /// <summary>
        /// Constructeur vide.
        /// </summary>
        public ClCompte()
        { }

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        /// <param name="p_nomUtilisateur">Le nom d'utilisateur.</param>
        /// <param name="p_motDePasse">Le mot de passe.</param>
        /// <param name="p_concentration">La concentration du compte.</param>
        /// <param name="p_typeCompte">Le type du compte.</param>
        public ClCompte(string p_nomUtilisateur, string p_motDePasse,
            string p_concentration, TypeCompte p_typeCompte, TypeUser p_typeUser,
            string email, string nom, string p_email_telephone)
        {
            IdUtilisateur = p_nomUtilisateur;
            MotDePasse = p_motDePasse;
            Concentration = p_concentration;
            Role = p_typeCompte;
            Type = p_typeUser;
            Email = email;
            Nom = nom;
            EmailTelephone = p_email_telephone;
        }

        /// <summary>
        /// Le nom d'utilisateur.
        /// </summary>
        public string IdUtilisateur
        { get; set; }

        /// <summary>
        /// Le mot de passe du compte.
        /// </summary>
        public string MotDePasse
        { get; set; }

        /// <summary>
        /// La concentration du compte. (informatique, employé de la bibliothèque, design intérieur, etc.) 
        /// </summary>
        public string Concentration
        { get; set; }

        /// <summary>
        /// Le rôle du compte dans le site.
        /// </summary>
        public TypeCompte Role
        { get; set; }

        /// <summary>
        /// Le rôle du compte dans le site.
        /// </summary>
        public TypeUser Type
        { get; set; }

        public string Email
        { get; set; }

        public string Nom
        { get; set; }

        public string EmailTelephone
        { get; set; }

        public DateTime LastLogin
        { get; set; }

        /// <summary>
        /// Vérifie si les propriétés du compte sont valides. Dans le cas contraire, remplis le tableau d'erreurs.
        /// </summary>
        override public void Valider()
        {
            tableauErreurs.Clear();
            //Validation du nom d'utilisateur
            if (IdUtilisateur.Length != 0)
            {
                Int32 test;
                if (!Int32.TryParse(IdUtilisateur, out test))
                {
                    tableauErreurs.Add("labelNomUtilisateur", "Le nom d'utilisateur doit être un numéro");
                }
                else if (!(IdUtilisateur.Length == 7 || IdUtilisateur.Length == 5))
                {
                    tableauErreurs.Add("labelNomUtilisateur", "Le nom d'utilisateur est invalide");
                }
            }
            else
            {
                tableauErreurs.Add("labelNomUtilisateur", "Le numéro d'admission ou (d'employé) est obligatoire");
            }

            //Validation du mot de passe
            if (MotDePasse.Length != 0)
            {
                if (MotDePasse.Length < NB_CHARACTÈRE_MOT_DE_PASSE)
                {
                    tableauErreurs.Add("labelMotDePasse", "Le mot de passe doit être minimum de " + NB_CHARACTÈRE_MOT_DE_PASSE);
                }
            }
            else
            {
                tableauErreurs.Add("labelMotDePasse", "Le mot de passe est obligatoire");
            }
        }

        /// <summary>
        /// Obtient le type du compte dans son type original (TypeCompte) en lui donnant une string.
        /// </summary>
        /// <param name="p_typeCompte">Le type du compte à obtenir</param>
        /// <returns>Le type de compte dans son type original (TypeCompte)</returns>
        public static TypeCompte GetTypeCompte(string p_typeCompte)
        {
            string typeCompte = p_typeCompte.ToLower();
            if (typeCompte.Equals(TypeCompte.Administrateur.ToString().ToLower()))
                return TypeCompte.Administrateur;
            else if (typeCompte.Equals(TypeCompte.Utilisateur.ToString().ToLower()))
                return TypeCompte.Utilisateur;
            else if (typeCompte.Equals(TypeCompte.Entraineur.ToString().ToLower()))
                return TypeCompte.Entraineur;
            else
                return TypeCompte.Invalide;
        }
    }
}
