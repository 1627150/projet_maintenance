using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS2013
{
    class ClConcentration : ClObjet
    {
        /// <summary>
        /// Condition javascript dans le cas d'une supression de Concentration
        /// </summary>
        public const string JS_CONFIRMATION_SUPRESSION =
            "if (confirm('Voulez-vous supprimer cet item ?') == false) return false;";

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="p_idConcentration">L'id de la concentration (GUID)</param>
        /// <param name="p_nomConcentration">Le nom de la concentration</param>
        /// <param name="p_concentration">Le type de la concentration</param>
        /// /// <param name="p_objectifCredits">L'objectif de Crédits</param>
        public ClConcentration(string p_idConcentration, string p_nomConcentration,
            TypeConcentration p_concentration, int p_objectifCredits, TypeUser p_typeUser)
        {
            IdConcentration = p_idConcentration;
            NomConcentration = p_nomConcentration;
            Concentration = p_concentration;
            ObjectifCredits = p_objectifCredits;
            TypeUser = p_typeUser;
        }

        /// <summary>
        /// L'id de la concentration (GUID)
        /// </summary>
        public string IdConcentration
        { get; private set; }

        /// <summary>
        /// Le nom de la concentration
        /// </summary>
        public string NomConcentration
        { get; private set; }

        /// <summary>
        /// Le type de la concentration
        /// </summary>
        public TypeConcentration Concentration
        { get; private set; }

        public TypeUser TypeUser
        { get; private set; }

        /// <summary>
        /// L'objectif de Crédits
        /// </summary>
        public int ObjectifCredits
        { get; private set; }

        /// <summary>
        /// Obtient le type de concentration dans son type orignal (TypeConcentration) selon une string reçu en paramètre.
        /// </summary>
        /// <param name="p_typeConcentration">Le type de la concentration en string</param>
        /// <returns>Le type de concentration dans son type original (TypeConcentration)</returns>
        public static TypeConcentration GetTypeConcentration(string p_typeConcentration)
        {
            string typeConcentration = p_typeConcentration.ToLower();
            if (typeConcentration.Equals(TypeConcentration.Département.ToString().ToLower())
                || typeConcentration.Equals("departement"))
                return TypeConcentration.Département;
            else if (typeConcentration.Equals(TypeConcentration.Service.ToString().ToLower()))
                return TypeConcentration.Service;
            else if (typeConcentration.Equals(TypeConcentration.ProgrammeAccueilIntegration.ToString().ToLower()))
                return TypeConcentration.ProgrammeAccueilIntegration;
            else if (typeConcentration.Equals(TypeConcentration.ProgrammePreUniversitaire.ToString().ToLower()))
                return TypeConcentration.ProgrammePreUniversitaire;
            else if (typeConcentration.Equals(TypeConcentration.ProgrammeTechnique.ToString().ToLower()))
                return TypeConcentration.ProgrammeTechnique;
            else if (typeConcentration.Equals(TypeConcentration.Partenaire.ToString().ToLower()))
                return TypeConcentration.Partenaire;
            else if (typeConcentration.Equals(TypeConcentration.Ecole.ToString().ToLower()))
                return TypeConcentration.Ecole;
            else if (typeConcentration.Equals(TypeConcentration.Communaute.ToString().ToLower()))
                return TypeConcentration.Communaute;

            return TypeConcentration.NULL;
        }

        public static string GetConcentrationTexte(TypeConcentration p_typeConcentration)
        {
            switch (p_typeConcentration)
            {
                case TypeConcentration.Département:
                    return p_typeConcentration.ToString();
                case TypeConcentration.Service:
                    return p_typeConcentration.ToString();
                case TypeConcentration.ProgrammePreUniversitaire:
                case TypeConcentration.ProgrammeTechnique:
                case TypeConcentration.ProgrammeAccueilIntegration:
                    return "Programme";
                case TypeConcentration.Partenaire:
                    return p_typeConcentration.ToString();
                case TypeConcentration.Communaute:
                    return "Communauté";
                case TypeConcentration.Ecole:
                    return "Comission scolaire";
            }

            return String.Empty;
        }

        /// <summary>
        /// Vérifie si les propriétés de la concentration sont valides. Dans le cas contraire, remplis le tableau d'erreurs.
        /// Pour l'instant, aucune validation à faire.
        /// </summary>
        public override void Valider()
        {
            throw new NotImplementedException();
        }
    }
}
