using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS2013
{
    class ClSeanceEntrainement : ClObjet
    {
        /// <summary>
        /// La condition javascript pour valider si l'utilisateur veut vraiment supprimer le compte.
        /// </summary>
        public const string JS_CONFIRMATION_SUPRESSION =
            "if (confirm('Voulez-vous vraiment supprimer cet entrainement ?') == false) return false;";

        /// <summary>
        /// Constructeur vide
        /// </summary>
        public ClSeanceEntrainement()
        { }

        /// <summary>
        /// Constructeur par défaut lors de la création d'une nouvelle séance d'entrainement (Id est généré automatiquement)
        /// </summary>
        /// <param name="p_intensite">L'intensité de la séance d'entrainement (1-3).</param>
        /// <param name="p_tempsActivite">Le temps que la séance d'entrainement à durée.</param>
        /// <param name="p_nbCredits">Le nombre de Crédit $anté généré par cette activité.</param>
        /// <param name="p_idUtilisateur">L'id de l'utilisateur ayant fait cette activité.</param>
        /// <param name="p_idActivite">L'id de l'activité faite pour la séance d'entrainement.</param>
        /// <param name="p_estValide">Si la séance n'a pas été accepté par l'administrateur, sera à faux, sinon, sera à vrai.</param>
        public ClSeanceEntrainement(int? p_intensite,
            int? p_tempsActivite, double p_nbCredits,
            string p_idUtilisateur, string p_idActivite,
            bool p_estValide)
        {
            IdSeance = Guid.NewGuid().ToString();
            Intensite = p_intensite;
            TempsActivite = p_tempsActivite;
            NbCredits = p_nbCredits;
            IdUtilisateur = p_idUtilisateur;
            IdActivite = p_idActivite;
            DateActivite = DateTime.Today;
            EstValide = p_estValide;

            Valider();
        }

        /// <summary>
        /// Constructeur par défaut lorsque la séance est obtenu dans la base de données et qu'elle existe déjà
        /// </summary>
        /// <param name="p_idSeance">L'id de la séance d'entrainement</param>
        /// <param name="p_intensite">L'intensité de la séance d'entrainement (1-3).</param>
        /// <param name="p_tempsActivite">Le temps que la séance d'entrainement à durée.</param>
        /// <param name="p_nbCredits">Le nombre de Crédit $anté généré par cette activité.</param>
        /// <param name="p_idUtilisateur">L'id de l'utilisateur ayant fait cette activité.</param>
        /// <param name="p_idActivite">L'id de l'activité faite pour la séance d'entrainement.</param>
        /// <param name="p_date">La date ou la séance a été entré dans le système.</param>
        /// <param name="p_estValide">Si la séance n'a pas été accepté par l'administrateur, sera à faux, sinon, sera à vrai.</param>
        public ClSeanceEntrainement(string p_idSeance, int? p_intensite,
            int? p_tempsActivite, double p_nbCredits,
            string p_idUtilisateur, string p_idActivite, DateTime p_date,
            bool p_estValide)
        {
            IdSeance = p_idSeance;
            Intensite = p_intensite;
            TempsActivite = p_tempsActivite;
            NbCredits = p_nbCredits;
            IdUtilisateur = p_idUtilisateur;
            IdActivite = p_idActivite;
            DateActivite = p_date;
            EstValide = p_estValide;

            Valider();
        }

        /// <summary>
        /// Constructeur par défaut lorsque la séance est obtenu dans la base de données et qu'elle existe déjà
        /// </summary>
        /// <param name="p_idSeance">L'id de la séance d'entrainement</param>
        /// <param name="p_intensite">L'intensité de la séance d'entrainement (1-3).</param>
        /// <param name="p_tempsActivite">Le temps que la séance d'entrainement à durée.</param>
        /// <param name="p_nbCredits">Le nombre de Crédit $anté généré par cette activité.</param>
        /// <param name="p_idUtilisateur">L'id de l'utilisateur ayant fait cette activité.</param>
        /// <param name="p_idActivite">L'id de l'activité faite pour la séance d'entrainement.</param>
        /// <param name="p_date">La date ou la séance a été entré dans le système.</param>
        /// <param name="p_estValide">Si la séance n'a pas été accepté par l'administrateur, sera à faux, sinon, sera à vrai.</param>
        public ClSeanceEntrainement(string p_idSeance, int? p_intensite,
            int? p_tempsActivite, double p_nbCredits,
            string p_idUtilisateur, DateTime p_date,
            bool p_estValide)
        {
            IdSeance = p_idSeance;
            Intensite = p_intensite;
            TempsActivite = p_tempsActivite;
            NbCredits = p_nbCredits;
            IdUtilisateur = p_idUtilisateur;
            DateActivite = p_date;
            EstValide = p_estValide;

            Valider();
        }

        /// <summary>
        /// L'id de la séance d'entrainement (GUID).
        /// </summary>
        public string IdSeance
        { get; private set; }

        /// <summary>
        /// L'intensité de la séance d'entrainement (1-3).
        /// </summary>
        public int? Intensite
        { get; private set; }

        /// <summary>
        /// Le temps que la séance d'entrainement à durée.
        /// </summary>
        public int? TempsActivite
        { get; private set; }

        /// <summary>
        /// Le nombre de Crédit $anté généré par cette activité.
        /// </summary>
        public double NbCredits
        { get; private set; }

        /// <summary>
        /// L'id de l'utilisateur ayant fait cette activité.
        /// </summary>
        public string IdUtilisateur
        { get; private set; }

        /// <summary>
        /// L'id de l'activité faite pour la séance d'entrainement.
        /// </summary>
        public string IdActivite
        { get; private set; }

        /// <summary>
        /// La date ou la séance a été entré dans le système.
        /// </summary>
        public DateTime DateActivite
        { get; set; }

        /// <summary>
        /// Si la séance n'a pas été accepté par l'administrateur, sera à faux, sinon, sera à vrai.
        /// </summary>
        public bool EstValide
        { get; private set; }

        /// <summary>
        /// Vérifie si les propriétés de la séance d'entrainement sont valident sont valides. Dans le cas contraire, remplis le tableau d'erreurs.
        /// </summary>
        public override void Valider()
        {
        }
    }
}
