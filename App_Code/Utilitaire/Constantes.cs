using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS2013
{
    /// <summary>
    /// Contient les différents messages contenus dans chaque pages du site.
    /// Auteurs : RPDG & FL
    /// </summary>
    class Constantes
    {
        #region Connexion
        public static readonly string CONNEXION_MESSAGE_BIENVENUE =
            "Bienvenue dans le système d'ajout de <i>Crédit $anté<sup>®</sup></i>!";
        public static readonly string CONNEXION = "Connexion";
        public static readonly string CONNEXION_MESSAGE_NOUVEAU_COMPTE =
            "Première utilisation, cliquez ";
        public static readonly string CONNEXION_LIEN_NOUVEAU_COMPTE = "ici";
        #endregion

        #region Master
        public static readonly string MASTER_TITRE = "<i>Crédit $anté<sup>®</sup></i>";
        public static readonly string DECONNEXION = "Déconnexion";
        public static readonly string COMPTE = "Compte";
        #endregion

        #region Déconnexion
        public static readonly string DECONNEXTION_DESCRIPTION =
            "Vous avez quitté de façon sécuritaire.<br>Merci d'avoir utilisé le programme <i>Crédits $anté<sup>®</sup></i>!";
        #endregion

        #region Ajout
        public static readonly string AJOUT_TITRE = "Ajout de <i>Crédit $anté<sup>®</sup></i>";
        public static readonly string AJOUT_MESSAGE_TOTAL_CREDITS = "Votre total de <i>Crédit $anté<sup>®</sup></i> :";
        public static readonly string AJOUT_MESSAGE_TOTAL_BOUGEOTTE = "<i>Crédits $anté<sup>®</sup></i> pendant la Bougeotte :";
        public static readonly string AJOUT_MESSAGE_OBJECTIF_CREDITS = "Objectif de votre {0} :";
        public static readonly string AJOUT_FIELDSET = "Séance d'entraînement";
        public static readonly string ACTIVITE = "Activité";
        public static readonly string AJOUT_FIELDSET_INTENSITE = "Intensité";
        public static readonly string AJOUT_TEMPS = "Temps (minutes)";
        public static readonly string INTENSITE_FAIBLE_DESCRIPTION =
            "Activité physique qui n'implique pas de changement important dans la respiration.";
        public static readonly string INTENSITE_MODERE_DESCRIPTION =
            "Essoufflement léger à modéré, vous êtes capable de parler, mais pas de chanter.";
        public static readonly string INTENSITE_ELEVE_DESCRIPTION =
            "Essoufflement important, vous pouvez dire quelques mots seulement avant de reprendre votre souffle.";
        public static readonly string CONSULTEZ_CHARTE_CALCUL =
            "Consultez l'image à droite pour connaître l'intensité de votre activité si vous calculez votre fréquence cardiaque.";
        #endregion

        #region Statistiques
        public static readonly string STATISTIQUES_TITRE = "Statistiques";
        public static readonly string TOTAL_BOUGEOTTE_CEGEP = "Total La Bougeotte Cégep (communauté étudiante, personnel et partenaires) : ";
        public static readonly string GRAND_TOTAL_BOUGEOTTE = "Grand total La Bougeotte (Cégep et externe) : ";
        public static readonly string TOTAL_ANNUEL_CEGEP = "Total annuel Cégep de <i>Crédit $anté<sup>®</sup></i> (communauté étudiante, personnel et partenaires) : ";
        public static readonly string GRAND_TOTAL_ANNUEL = "Grand total annuel de <i>Crédit $anté<sup>®</sup></i> (Cégep et externe) : ";
        public static readonly string AFFICHAGE_RADIO_STATS = "Visible|Visible|Visible|Visible|Visible|Visible";
        #endregion

        #region Statistiques Personnelles
        public static readonly string STATISTIQUES_PERSONNELLES_TITRE = "Statistiques personnelles";
        public static readonly string PRESENTATION_NB_CREDITS = "Votre total de <i>Crédit $anté<sup>®</sup></i> : ";
        #endregion

        #region Photos
        public static readonly string PHOTO_TITRE = "Photos";
        public static readonly string PHOTO_DESCRIPTION = "Voici quelques photos de La Bougeotte :";
        public static readonly string PHOTO_TITRE_MODIFIER = "Gérer les photos";
        public static readonly string PHOTO_DESCRIPTION_MODIFIER = "Voici les photos actuelles de La Bougeotte :";
        #endregion

        #region Nous Joindre
        public static readonly string CONTACT_TITRE = "Nous joindre";
        public static readonly string CONTACT_DESCRIPTION = "Pour toute information concernant La Bougeotte ou <i>Crédit $anté<sup>®</sup></i>, communiquez avec l’administrateur du projet <a href=\"mailto:Evelyne.Menard @cstjean.qc.ca\" target=\"_top\">Evelyne.Menard@cstjean.qc.ca</a>.";
        #endregion

        #region Histoire
        public static readonly string HISTOIRE_TITRE = "Notre Historique";
        public static readonly string HISTOIRE_DESCRIPTION =
            "<p>La semaine de La Bougeotte est un événement annuel qui a été créé en 2004 par le Département d’éducation physique du Cégep Saint-Jean-sur-Richelieu. " +
            "Auparavant appelée « Les automnales » et « La bougeotte automnale », la semaine de « La Bougeotte » se veut une semaine de sensibilisation sur l’importance " +
            "de bouger et sur l’effet préventif de l’activité physique pour la santé. Pendant cette semaine, toute la communauté du Cégep est invitée à bouger " +
            "et à mettre leurs efforts en commun. L’activité physique faite par les étudiantes et étudiants, les membres du personnel ainsi que nos partenaires " +
            "à l’interne durant cette semaine est transformée en Crédit $anté<sup>®</sup>. Chaque Crédit $anté<sup>®</sup> représente une économie " +
            "d’un dollar pour notre système de santé. À la fin de la semaine de La Bougeotte, nous remettons un chèque symbolique équivalent au nombre de " +
            "Crédits $anté total accumulé aux représentants de la Fondation Santé Haut-Richelieu-Rouville, montant qui équivaut à ce que l’hôpital " +
            "économiserait en dollars grâce à La Bougeotte du Cégep. C’est une façon très concrète de faire comprendre qu’en bougeant, nous pouvons faire économiser des " +
            "sommes intéressantes au système de santé.<br/><br/>" +

            "À partir d’études existantes, nous avons élaboré une façon de calculer ce que nous appelons les <i>Crédits $anté<sup>®</sup></i>. Cette unité de mesure tient " +
            "compte du bénéfice supplémentaire d’une activité physique faite à des intensités différentes.<br/>Ainsi, un <i>Crédit $anté<sup>®</sup></i> équivaut à :<ul>" +
                "<li>30 minutes d’activité physique d’intensité faible;</li>" +
                "<li>15 minutes d’activité physique d’intensité modérée;</li>" +
                "<li>10 minutes d’activité physique d’intensité élevée.</li></ul>" +

            "En nous appuyant sur les directives canadiennes en matière d’activité physique, nous recommandons de cumuler un minimum de dix Crédits $anté " +
            "ou plus par semaine pour favoriser la santé.<br/><br/>" +

            "Ce logiciel, permettant de calculer les Crédits $anté, a été conçu par deux étudiants finissants en Techniques de l’informatique du " +
            "Cégep Saint-Jean-sur-Richelieu (René-Pier Deshaies Gélinas et Francis Labonté) dans le cadre de leur projet d’intégration de fin d’études. " +
            "Les droits sur le logiciel ainsi que le concept de <i>Crédit $anté<sup>®</sup></i> appartiennent au Cégep Saint-Jean-sur-Richelieu.</p>" +
            "<p> Sommes amassées antérieurement pendant les semaines de<i> La Bougeotte</i> :</p>" +
            "<ul>" +
            "<li>2016 :&nbsp;28 442,00 $</li>" +
            "<li>2015 :&nbsp;26 808,00 $</li>" +
            "<li>2014 :&nbsp;27 888,01 $</li>" +
            "<li>2013 :&nbsp;35 233,58 $</li>" +
            "<li>2012 :&nbsp;35 633,00 $</li>" +
            "<li>2011 :&nbsp;35 275,00 $</li>" +
            "<li>2010 :&nbsp;36 439,00 $</li>" +
            "<li>2009 :&nbsp;25 388,00 $</li>" +
            "<li>2008 :&nbsp;10 069,00 $</li>" +
            "<li>2007 :&nbsp;&nbsp;&nbsp;7 827,00 $</li>" +
            "<li>2006 :&nbsp;&nbsp;&nbsp;6 780,00 $</li>" +
            "<li>2005 :&nbsp;&nbsp;&nbsp;6 294,00 $</li>" +
            "<li>2004 :&nbsp;&nbsp;&nbsp;5 991,00 $</li> </ul>";
        #endregion

        #region Inscription
        public static readonly string UTILISATEUR = "Identifiant* : ";
        public static readonly string UTILISATEUR_CONDITIONNEL = "Identifiant ({0}) : ";
        public static readonly string UTILISATEUR_ETU_CEGEP = "7 chiffres";
        public static readonly string UTILISATEUR_PER_CEGEP = "5 chiffres";
        public static readonly string UTILISATEUR_EXT_PART = "10 chiffres du numéro de téléphone";
        public static readonly string UTILISATEUR_EXT_ECO = "code permanent";

        public static readonly string REGEX_ETU_CEGEP = @"^[0-9]{7}$";
        public static readonly string REGEX_PER_CEGEP = @"^[0-9]{5}$";
        public static readonly string REGEX_EXT_PART = @"^[0-9]{10}$";
        public static readonly string REGEX_EXT_ECO = @"^[a-zA-Z]{4}[0-9]{8}$";
        public static readonly string REGEX_EMAIL = @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$";

        public static readonly string MOT_DE_PASSE = "Mot de passe : ";
        public static readonly string MOT_DE_PASSE_AVEC_MINIMUM = "Mot de passe (minimum {0} caractères)* : ";
        public static readonly string INSCRIPTION_CONFIRMER_MOT_DE_PASSE = "Confirmez le mot de passe* : ";
        public static readonly string CONCENTRATION = "Groupe d'appartenance* : ";
        #endregion

        #region ConfirmationAjout
        public static readonly string CONFIRMATION_TITRE = "Confirmation";
        public static readonly string CONFIRMATION_DESCRIPTION = "Votre nouveau total de <i>Crédits $anté<sup>®</sup></i> est de : ";
        #endregion

        #region ÀPropos
        public static readonly string APROPOS_TITRE = "À propos de <i>Crédits $anté<sup>®</sup></i>";
        public static readonly string HISTORIQUE_TITRE = "Historique de <i>Crédit $anté<sup>®</sup></i>";
        public static readonly string APROPOS_DESCRIPTION = "Au fil des années, nous avons développé une expertise de sensibilisation à l’importance de bouger " +
            "pour être en santé. Le nom de cet événement a changé au fil des années : Les automnales, La Bougeotte automnale et maintenant La Bougeotte. " +
            "Cet événement a deux buts : faire bouger les gens de façon simple et leur faire comprendre que chaque action est une économie pour le système " +
            "de santé qui en a grand besoin." +

            "<br/><br/>À partir d’études, nous avons élaboré une façon de calculer ce que nous appelons les « <i>Crédits $anté<sup>®</sup></i> ». Cette unité de mesure tient compte du bénéfice supplémentaire " +
            "d’une activité plus intense. Ainsi un Crédit $anté équivaut à :	<ul>" +
            "<li>30 minutes d’activité d’intensité faible;</li>" +
            "<li>15 minutes d’activité d’intensité modérée;</li>" +
            "<li>10 minutes d’activité d’intensité élevée.</li>" +
            "</ul>" +

            "<br/>En nous appuyant sur les directives canadiennes en matière d’activité physique, nous recommandons de cumuler 10 <i>Crédits $anté<sup>®</sup></i> ou plus par semaine pour favoriser la santé." +

            "<br/><br/>Ce qui est important de retenir c’est qu’un Crédit $anté équivaut à 1.00 $ sauver au système de santé. Ce n’est pas de l’argent dépensé " +
            "ou injecté, mais bien de l’argent que l’on fait sauver donc disponible d’autres soins. " +

            "<br/><br/>Le programme pour calculer les <i>Crédits $anté<sup>®</sup></i> est un logiciel qui a été conçu par deux finissants en informatique au cégep " +
            "St-Jean-sur-Richelieu (René Pier  Deshaies Gélinas et Francis Labonté) dans le cadre de leur projet d’intégration de fin d’études. Les droits sur le logiciel ainsi que le concept de Crédit $anté appartiennent au cégep Saint-Jean-sur-Richelieu.";

        #endregion

        #region Activités
        public static readonly string ACTIVITE_TITRE = "Activités";
        #endregion

        #region Paramètres
        public static readonly string NB_MAX_CREDITS_SEMAINE = "Nombre maximum de crédits autorisés par semaine : ";
        public static readonly string NB_MAX_CREDITS_SEANCE = "Nombre maximum de crédits par séance : ";
        public static readonly string NB_MAX_MINUTES_SEANCE = "Nombre maximum de minutes par séance : ";
        public static readonly string NB_MIN_MINUTES_SUSPECT = "Nombre de minutes à partir duquel les séances seront classées comme étant suspectes : ";
        public static readonly string NB_MAX_SEANCES_JOUR = "Nombre maximum de séances par jour : ";
        #endregion

        #region Compte

        public static readonly string MODIFICATION_COMPTE = "Modification du Compte";
        public static readonly string ANCIEN_MDP = "Ancien mot de passe : ";
        public static readonly string INFORMATIONS = "Informations";
        public static readonly string NOUVEAU_MOT_DE_PASSE = "Nouveau mot de passe : ";
        public static readonly string CONFIRMATION_NOUVEAU_MOT_DE_PASSE = "Confirmation du nouveau mot de passe : ";
        public static readonly string VALIDER = "Valider";

        #endregion Compte

        #region RécupérerMotPasse

        public static readonly string RECUPERATION_MOT_PASSE = "Récupération du mot de passe";
        public static readonly string ENVOYER = "Envoyer";
        public static readonly string RETOUR_ACCUEIL = "Retour à l'accueil.";
        public static readonly string COURRIEL_SUJET = "Nouveau mot de passe Crédit $anté";
        public static readonly string COURRIEL_BODY = "Voici votre nouveau mot de passe pour le site web Crédit $anté.<br/>" +
                                                            "Vous pourrez le modifier en naviguant vers votre profil.<br/>" +
                                                            "<br/>Copier/Coller le mot de passe suivant&nbsp;: ";

        #endregion

        #region Autre
        public static readonly string AUCUNE_DONNEE = "Aucune donnée à afficher.";
        public static readonly string AUCUNE_ENTREE = "Aucune entrée pour cet utilisateur.";
        #endregion
    }
}
