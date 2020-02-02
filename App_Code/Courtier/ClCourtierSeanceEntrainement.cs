using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CS2013
{
    class ClCourtierSeanceEntrainement : ClCourtier
    {
        public enum TypeColonne
        {
            ID_SEANCE,
            INTENSITE,
            TEMPS_ACTIVITE,
            NB_CREDITS,
            ID_UTILISATEUR,
            ID_ACTIVITE,
            DATE,
            EST_VALIDE
        }

        private String NomTable
        {
            get
            {
                return "seance_entrainement";
            }
        }

        private ClCourtierSeanceEntrainement()
        { }

        static private ClCourtierSeanceEntrainement m_instance = null;

        static public ClCourtierSeanceEntrainement GetInstance()
        {
            if (m_instance == null)
            {
                m_instance = new ClCourtierSeanceEntrainement();
            }

            return m_instance;
        }

        public bool Insérer(ClSeanceEntrainement p_seanceEntrainement)
        {
            if (!p_seanceEntrainement.EstValide)
            {
                ClCompte compte = ClCourtierCompte.GetInstance().ObtenirCompteSelonId(p_seanceEntrainement.IdUtilisateur);
                MailMessage msg = new MailMessage();

                ClCourtierParametre.GetInstance().ObtenirSelonNom("NotifiedAdminEmail").Value.Split(';').ToList<string>().ForEach(to => msg.To.Add(to));;

                msg.Body = String.Format(
                    "ID : {0}<br>" +
                    "Nom : {1}<br>" +
                    "Email : {2}<br>" +
                    "Date de l'activité : {3}<br>" +
                    "Intensitée : {4}<br>" +
                    "Durée : {5}<br>" +
                    "Nombre de crédits : {6}$",
                    compte.IdUtilisateur,
                    String.IsNullOrEmpty(compte.Nom) ? "N/D" : compte.Nom,
                    String.IsNullOrEmpty(compte.Email) ? "N/D" : compte.Email,
                    p_seanceEntrainement.DateActivite.Date.ToShortDateString(),
                    (p_seanceEntrainement.Intensite.HasValue) ? Utilitaire.ConvertirIntensite(p_seanceEntrainement.Intensite.Value) : "N/D",
                    (p_seanceEntrainement.TempsActivite.HasValue) ? Convert.ToString(p_seanceEntrainement.TempsActivite.Value) + " minutes" : "N/D",
                    p_seanceEntrainement.NbCredits);

                msg.IsBodyHtml = true;
                msg.Subject = "Entrée suspecte";
                new SmtpClient().Send(msg);
            }

            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO " + NomTable + " (" +
                    TypeColonne.ID_SEANCE + ", ", cn);

                if (p_seanceEntrainement.Intensite.HasValue)
                {
                    cmd.CommandText += TypeColonne.INTENSITE + ", " +
                    TypeColonne.TEMPS_ACTIVITE + ", ";
                }

                cmd.CommandText += TypeColonne.NB_CREDITS + ", " +
                    TypeColonne.ID_UTILISATEUR + ", " +
                    TypeColonne.DATE + ", " +
                    TypeColonne.EST_VALIDE + ") " +
                    " VALUES (@ID_SEANCE, ";

                if (p_seanceEntrainement.Intensite.HasValue)
                {
                    cmd.CommandText += "@INTENSITE, @TEMPS_ACTIVITE, ";
                }

                cmd.CommandText += "@NB_CREDITS, @ID_UTILISATEUR, @DATE, @EST_VALIDE);";

                cmd.Parameters.Add("@ID_SEANCE", SqlDbType.NVarChar).Value = p_seanceEntrainement.IdSeance;

                if (p_seanceEntrainement.Intensite.HasValue)
                {
                    cmd.Parameters.Add("@INTENSITE", SqlDbType.Int).Value = p_seanceEntrainement.Intensite.Value;
                    cmd.Parameters.Add("@TEMPS_ACTIVITE", SqlDbType.Int).Value = p_seanceEntrainement.TempsActivite;
                }

                cmd.Parameters.Add("@NB_CREDITS", SqlDbType.Decimal).Value = p_seanceEntrainement.NbCredits;
                cmd.Parameters.Add("@ID_UTILISATEUR", SqlDbType.NVarChar).Value = p_seanceEntrainement.IdUtilisateur;
                cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = p_seanceEntrainement.DateActivite != new DateTime().Date ? p_seanceEntrainement.DateActivite : DateTime.Today;
                cmd.Parameters.Add("@EST_VALIDE", SqlDbType.Bit).Value = p_seanceEntrainement.EstValide;

                cn.Open();
                int ret = cmd.ExecuteNonQuery();

                return (ret == 1);
            }
        }

        public bool SupprimerSelonIdActivite(string p_idActivite)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM " +
                    NomTable +
                    " WHERE " + TypeColonne.ID_ACTIVITE + " = @ID_ACTIVITE", cn);
                //cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ID_ACTIVITE", SqlDbType.NVarChar).Value = p_idActivite;

                cn.Open();
                int ret = cmd.ExecuteNonQuery();

                return (ret > 0);
            }
        }

        public bool Supprimer(ClSeanceEntrainement p_seanceEntrainement)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM " +
                    NomTable +
                    " WHERE " + TypeColonne.ID_SEANCE + " = @ID_SEANCE", cn);
                //cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ID_SEANCE", SqlDbType.NVarChar).Value = p_seanceEntrainement.IdSeance;

                cn.Open();
                int ret = cmd.ExecuteNonQuery();

                return (ret == 1);
            }
        }

        public bool ValiderSeance(string p_idSeance)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE " + NomTable +
                    " SET " + TypeColonne.EST_VALIDE + " = 1" +
                    " WHERE " + TypeColonne.ID_SEANCE + " = @ID_SEANCE;", cn);
                //cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ID_SEANCE", SqlDbType.NVarChar).Value = p_idSeance;

                cn.Open();
                int ret = cmd.ExecuteNonQuery();

                return (ret == 1);
            }
        }

        public bool SupprimerSelonIdSeance(string p_idSeance)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM " +
                    NomTable +
                    " WHERE " + TypeColonne.ID_SEANCE + " = @ID_SEANCE", cn);

                cmd.Parameters.Add("@ID_SEANCE", SqlDbType.NVarChar).Value = p_idSeance;

                cn.Open();
                int ret = cmd.ExecuteNonQuery();

                return (ret == 1);
            }
        }

        public bool SupprimerSelonGroupe(string p_groupe)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "DELETE " + NomTable +
                    " WHERE " + ClCourtierCompte.TypeColonne.ID_UTILISATEUR + " IN " +
                    " (SELECT " + TypeColonne.ID_UTILISATEUR +
                        " FROM " + ClCourtierCompte.GetInstance().NomTable + " JOIN " + ClCourtierConcentration.GetInstance().NomTable +
                        " ON " + ClCourtierCompte.GetInstance().NomTable + "." + ClCourtierCompte.TypeColonne.ID_CONCENTRATION +
                        " = " + ClCourtierConcentration.GetInstance().NomTable + "." + ClCourtierConcentration.TypeColonne.ID_CONCENTRATION +
                        " WHERE " + ClCourtierConcentration.TypeColonne.TYPE_CONCENTRATION + " = @TYPE_CONCENTRATION)"
                    , cn);

                cmd.Parameters.Add("@TYPE_CONCENTRATION", SqlDbType.NVarChar).Value = p_groupe;

                cn.Open();
                int ret = cmd.ExecuteNonQuery();

                return (ret >= 1);
            }
        }

        public bool SupprimerTout()
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM " +
                    NomTable + ";", cn);

                cn.Open();
                int ret = cmd.ExecuteNonQuery();

                return (ret > 0);
            }
        }

        public List<ClSeanceEntrainement> ObtenirSeancesSelonIdUtilisateur(string p_idUtilisateur)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM " +
                    NomTable +
                    " WHERE " + TypeColonne.ID_UTILISATEUR + " = @ID_UTILISATEUR;", cn);

                cmd.Parameters.Add("@ID_UTILISATEUR", SqlDbType.NVarChar).Value = p_idUtilisateur;

                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<ClSeanceEntrainement> listeSeances = new List<ClSeanceEntrainement>();
                while (reader.Read())
                {
                    int? intensite;
                    if (reader.IsDBNull(1))
                        intensite = null;
                    else
                        intensite = reader.GetInt32(1);

                    int? temps;
                    if (reader.IsDBNull(2))
                        temps = null;
                    else
                        temps = reader.GetInt32(2);

                    ClSeanceEntrainement seance =
                        new ClSeanceEntrainement(reader.GetString(0), intensite, temps,
                            (double)reader.GetDecimal(3), reader.GetString(4),
                            reader.GetDateTime(6), reader.GetBoolean(7));
                    listeSeances.Add(seance);
                }

                return listeSeances;
            }
        }

        public double ObtenirTotalBougeotteSelonIdUtilisateur(string p_idUtilisateur)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT SUM(" + TypeColonne.NB_CREDITS + ") FROM " +
                    NomTable +
                    " WHERE id_utilisateur = @ID_UTILISATEUR AND " +
                    ClCourtierSeanceEntrainement.TypeColonne.DATE + " BETWEEN (SELECT DATE FROM Parametre WHERE Nom LIKE 'DateDebutBougeotte')" +
                    " AND (SELECT DATE FROM Parametre WHERE Nom LIKE 'DateFinBougeotte')", cn);

                cmd.Parameters.Add("@ID_UTILISATEUR", SqlDbType.NVarChar).Value = p_idUtilisateur;

                cn.Open();

                if (DBNull.Value != cmd.ExecuteScalar())
                    return Convert.ToDouble(cmd.ExecuteScalar());
                else
                    return 0.0;
            }
        }

        public double ObtenirTotalBougeotteCegep()
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                   "SELECT SUM(" + TypeColonne.NB_CREDITS + ") FROM " +
                   NomTable + " SE JOIN (UTILISATEUR U JOIN CONCENTRATION C ON U.Id_Concentration = C.id_concentration) on U.id_utilisateur = SE.id_utilisateur" +
                   " WHERE C." + ClCourtierConcentration.TypeColonne.TYPE_CONCENTRATION + " NOT LIKE '" + TypeConcentration.Partenaire + "' AND " +
                   ClCourtierConcentration.TypeColonne.NOM_CONCENTRATION + " NOT LIKE 'Communauté%' AND " +
                   ClCourtierParametre.TypeColonne.DATE + " BETWEEN (SELECT DATE FROM Parametre WHERE Nom LIKE 'DateDebutBougeotte')" +
                      " AND (SELECT DATE FROM Parametre WHERE Nom LIKE 'DateFinBougeotte')", cn);

                cn.Open();

                if (DBNull.Value != cmd.ExecuteScalar())
                    return Convert.ToDouble(cmd.ExecuteScalar());
                else
                    return 0.0;
            }
        }

        public double ObtenirTotalAnnuelCegep()
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                   "SELECT SUM(" + TypeColonne.NB_CREDITS + ") FROM " +
                   NomTable + " SE JOIN (UTILISATEUR U JOIN CONCENTRATION C ON U.Id_Concentration = C.id_concentration) on U.id_utilisateur = SE.id_utilisateur" +
                   " WHERE C." + ClCourtierConcentration.TypeColonne.TYPE_CONCENTRATION + " NOT LIKE '" + TypeConcentration.Partenaire + "' AND " +
                   ClCourtierConcentration.TypeColonne.NOM_CONCENTRATION + " NOT LIKE 'Communauté%'", cn);

                cn.Open();

                if (DBNull.Value != cmd.ExecuteScalar())
                    return Convert.ToDouble(cmd.ExecuteScalar());
                else
                    return 0.0;
            }
        }

        public double ObtenirTotalBougeotteSelonConcentration(string p_idUtilisateur)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT SUM(" + TypeColonne.NB_CREDITS + ") FROM " +
                    NomTable + " SE JOIN (UTILISATEUR U JOIN CONCENTRATION C ON U.Id_Concentration = C.id_concentration) on U.id_utilisateur = SE.id_utilisateur" +
                    " WHERE U.id_concentration = (select id_concentration from utilisateur where id_utilisateur = @ID_UTILISATEUR) AND " +
                    ClCourtierParametre.TypeColonne.DATE + " BETWEEN (SELECT DATE FROM Parametre WHERE Nom LIKE 'DateDebutBougeotte')" +
                    " AND (SELECT DATE FROM Parametre WHERE Nom LIKE 'DateFinBougeotte')", cn);

                cmd.Parameters.Add("@ID_UTILISATEUR", SqlDbType.NVarChar).Value = p_idUtilisateur;

                cn.Open();

                if (DBNull.Value != cmd.ExecuteScalar())
                    return Convert.ToDouble(cmd.ExecuteScalar());
                else
                    return 0.0;
            }
        }

        public double ObtenirTotalBougeotteSelonConcentrationEtIdUser(string p_idUtilisateur)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT SUM(" + TypeColonne.NB_CREDITS + ") FROM " +
                    NomTable + " SE JOIN (UTILISATEUR U JOIN CONCENTRATION C ON U.Id_Concentration = C.id_concentration) on U.id_utilisateur = SE.id_utilisateur" +
                    " WHERE U.id_concentration = " +
                        "(select id_parent from sous_concentration where id_concentration = (select id_concentration from utilisateur where id_utilisateur = @ID_UTILISATEUR) AND " +
                    ClCourtierParametre.TypeColonne.DATE + " BETWEEN (SELECT DATE FROM Parametre WHERE Nom LIKE 'DateDebutBougeotte')" +
                    " AND (SELECT DATE FROM Parametre WHERE Nom LIKE 'DateFinBougeotte'))", cn);

                cmd.Parameters.Add("@ID_UTILISATEUR", SqlDbType.NVarChar).Value = p_idUtilisateur;

                cn.Open();

                if (DBNull.Value != cmd.ExecuteScalar())
                    return Convert.ToDouble(cmd.ExecuteScalar());
                else
                    return 0.0;
            }
        }

        public double ObtenirTotalBougeotte()
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                   "SELECT SUM(" + TypeColonne.NB_CREDITS + ") FROM " +
                   NomTable +
                   " WHERE " + ClCourtierParametre.TypeColonne.DATE +
                   " BETWEEN (SELECT DATE FROM Parametre WHERE Nom LIKE 'DateDebutBougeotte') AND" +
                   " (SELECT DATE FROM Parametre WHERE Nom LIKE 'DateFinBougeotte')", cn);

                cn.Open();

                if (DBNull.Value != cmd.ExecuteScalar())
                    return Convert.ToDouble(cmd.ExecuteScalar());
                else
                    return 0.0;
            }
        }

        public double ObtenirTotalAnnuel()
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                   "SELECT SUM(" + TypeColonne.NB_CREDITS + ") FROM " + NomTable, cn);

                cn.Open();

                if (DBNull.Value != cmd.ExecuteScalar())
                    return Convert.ToDouble(cmd.ExecuteScalar());
                else
                    return 0.0;
            }
        }

        public DataSet ObtenirSeancesSQLData(string p_idUtilisateur)
        {
            SqlCommand commande = new SqlCommand();
            DataSet donnees = new DataSet();
            SqlConnection connexion =
                new SqlConnection(Utilitaire.GetConnectionString("DonneesCreditsSante"));

            commande.CommandText =
                    "SELECT * FROM " + NomTable +
                    " WHERE " + TypeColonne.ID_UTILISATEUR + " = @ID_UTILISATEUR " +
                    " ORDER BY " + TypeColonne.DATE + " ASC;";
            commande.Parameters.Add("@ID_UTILISATEUR", SqlDbType.NVarChar).Value = p_idUtilisateur;
            commande.Connection = connexion;
            SqlDataAdapter da = new SqlDataAdapter(commande);
            da.Fill(donnees);
            connexion.Open();
            commande.ExecuteNonQuery();
            connexion.Close();

            return donnees;
        }

        public DataSet ObtenirSeancesSuspectesSQLData()
        {
            SqlCommand commande = new SqlCommand();
            DataSet donnees = new DataSet();
            SqlConnection connexion =
                new SqlConnection(Utilitaire.GetConnectionString("DonneesCreditsSante"));

            commande.CommandText =
                    "SELECT se.Id_Seance, se.Intensite, se.Temps_Activite, se.Nb_Credits, se.Id_Utilisateur, se.Id_Activite, se.Date, se.Est_Valide, u.Email FROM " +
                    NomTable + " AS SE INNER JOIN utilisateur as U " +
                    " ON se.id_utilisateur = u.id_utilisateur" +
                    " WHERE " + TypeColonne.EST_VALIDE + " = 0" +
                    " ORDER BY " + TypeColonne.ID_UTILISATEUR + " ASC;";

            commande.Connection = connexion;
            SqlDataAdapter da = new SqlDataAdapter(commande);
            da.Fill(donnees);
            connexion.Open();
            commande.ExecuteNonQuery();
            connexion.Close();

            return donnees;
        }

        public DataSet ObtenirSeancesStatsPersoSQLData(string p_idUtilisateur)
        {
            SqlCommand commande = new SqlCommand();
            DataSet donnees = new DataSet();
            SqlConnection connexion =
                new SqlConnection(Utilitaire.GetConnectionString("DonneesCreditsSante"));

            commande.CommandText =
                    "SELECT " + TypeColonne.ID_SEANCE + ", " + TypeColonne.DATE + ", " +
                    TypeColonne.INTENSITE + ", " + TypeColonne.TEMPS_ACTIVITE + ", " +
                    TypeColonne.NB_CREDITS +
                    " FROM seance_entrainement " +
                    " WHERE " + TypeColonne.ID_UTILISATEUR + " = @ID_UTILISATEUR" +
                    " ORDER BY DATE";

            commande.Parameters.Add("@ID_UTILISATEUR", SqlDbType.NVarChar).Value = p_idUtilisateur;

            commande.Connection = connexion;
            SqlDataAdapter da = new SqlDataAdapter(commande);
            da.Fill(donnees);
            connexion.Open();
            commande.ExecuteNonQuery();
            connexion.Close();

            return donnees;
        }

        public bool EstQuotaQuotidienAtteint(string p_idUtilisateur, double p_nbCreditsAAjouter)
        {
            bool nbSeanceMaxAtteint;

            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM " + NomTable +
                    " WHERE DATEDIFF(dd,0, DATE) = DATEDIFF(dd,0, GETDATE()) AND (ID_UTILISATEUR = @ID_UTILISATEUR)", cn);
                cmd.Parameters.Add("@ID_UTILISATEUR", SqlDbType.NVarChar).Value = p_idUtilisateur;

                cn.Open();
                nbSeanceMaxAtteint = (int)cmd.ExecuteScalar() >= ClCourtierParametre.GetInstance().ObtenirSelonNom(ClParametre.TypeParametre.MaxSeancesJour.ToString()).Valeur;
            }

            return nbSeanceMaxAtteint;
        }

        public bool EstQuotaHebdomadaireAtteint(string p_idUtilisateur, double p_nbCreditsAAjouter)
        {
            bool nbCreditMaxAtteint;

            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT SUM(" + TypeColonne.NB_CREDITS + ") FROM " + NomTable +
                    " WHERE ({ fn WEEK(DATE) } = { fn WEEK({ fn NOW() }) }) AND (ID_UTILISATEUR = @ID_UTILISATEUR)", cn);
                cmd.Parameters.Add("@ID_UTILISATEUR", SqlDbType.NVarChar).Value = p_idUtilisateur;

                cn.Open();
                object resultat = cmd.ExecuteScalar();

                if (resultat == null || String.IsNullOrEmpty(resultat.ToString()))
                    nbCreditMaxAtteint = false;
                else
                    nbCreditMaxAtteint =
                       Convert.ToDecimal(resultat) >= (ClCourtierParametre.GetInstance().ObtenirSelonNom(ClParametre.TypeParametre.MaxCreditsSemaine.ToString()).Valeur + Convert.ToDecimal(p_nbCreditsAAjouter));
            }

            return nbCreditMaxAtteint;
        }
    }
}
