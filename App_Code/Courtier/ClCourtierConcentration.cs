using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CS2013
{
    class ClCourtierConcentration : ClCourtier
    {
        public enum TypeColonne
        {
            ID_CONCENTRATION,
            NOM_CONCENTRATION,
            TYPE_CONCENTRATION,
            OBJECTIF_CREDITS,
            TYPE_USER
        }

        public String NomTable
        {
            get { return "concentration"; }
        }

        private ClCourtierConcentration()
        { }

        static private ClCourtierConcentration m_instance = null;

        static public ClCourtierConcentration GetInstance()
        {
            if (m_instance == null)
                m_instance = new ClCourtierConcentration();

            return m_instance;
        }

        public bool Existe(ClConcentration p_concentration)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM " +
                    NomTable +
                    " WHERE " + TypeColonne.NOM_CONCENTRATION + " = @NOM_CONCENTRATION" +
                    " AND " + TypeColonne.TYPE_CONCENTRATION + " = @TYPE_CONCENTRATION;", cn);

                cmd.Parameters.Add("@NOM_CONCENTRATION", SqlDbType.NVarChar).Value = p_concentration.NomConcentration;
                cmd.Parameters.Add("@TYPE_CONCENTRATION", SqlDbType.NVarChar).Value = p_concentration.Concentration.ToString();

                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                    return true;
                else
                    return false;
            }
        }

        public bool Insérer(ClConcentration p_concentration)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO " + NomTable +
                    " VALUES (@ID_CONCENTRATION, @NOM_CONCENTRATION, @TYPE_CONCENTRATION, @OBJECTIF_CREDITS, @TYPE_USER);", cn);

                cmd.Parameters.Add("@NOM_CONCENTRATION", SqlDbType.NVarChar).Value = p_concentration.NomConcentration;
                cmd.Parameters.Add("@ID_CONCENTRATION", SqlDbType.NVarChar).Value = p_concentration.IdConcentration;
                cmd.Parameters.Add("@TYPE_CONCENTRATION", SqlDbType.NVarChar).Value = p_concentration.Concentration.ToString();
                cmd.Parameters.Add("@OBJECTIF_CREDITS", SqlDbType.Int).Value = p_concentration.ObjectifCredits;
                cmd.Parameters.Add("@TYPE_USER", SqlDbType.Int).Value = (int)p_concentration.TypeUser;

                cn.Open();
                int ret = cmd.ExecuteNonQuery();

                return (ret == 1);
            }
        }

        public bool Modifier(ClConcentration p_concentration)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE " + NomTable +
                    " SET " + TypeColonne.NOM_CONCENTRATION.ToString() + " = @NOM_CONCENTRATION, " +
                    TypeColonne.TYPE_CONCENTRATION.ToString() + " = @TYPE_CONCENTRATION, " +
                    TypeColonne.OBJECTIF_CREDITS.ToString() + " = @OBJECTIF_CREDITS, " +
                    TypeColonne.TYPE_USER.ToString() + " = @TYPE_USER " +
                    "WHERE " + TypeColonne.ID_CONCENTRATION.ToString() + " = @ID_CONCENTRATION;", cn);

                cmd.Parameters.Add("@ID_CONCENTRATION", SqlDbType.NVarChar).Value = p_concentration.IdConcentration;
                cmd.Parameters.Add("@NOM_CONCENTRATION", SqlDbType.NVarChar).Value = p_concentration.NomConcentration;
                cmd.Parameters.Add("@TYPE_CONCENTRATION", SqlDbType.NVarChar).Value = p_concentration.Concentration.ToString();
                cmd.Parameters.Add("@OBJECTIF_CREDITS", SqlDbType.Int).Value = p_concentration.ObjectifCredits;
                cmd.Parameters.Add("@TYPE_USER", SqlDbType.Int).Value = (int)p_concentration.TypeUser;

                cn.Open();
                int ret = cmd.ExecuteNonQuery();

                return (ret == 1);
            }
        }

        public bool SupprimerSelonId(string p_idConcentration)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM " +
                    NomTable +
                    " WHERE " + TypeColonne.ID_CONCENTRATION.ToString() + " = @ID_CONCENTRATION", cn);
                cmd.Parameters.Add("@ID_CONCENTRATION", SqlDbType.NVarChar).Value = p_idConcentration;

                cn.Open();
                int ret = cmd.ExecuteNonQuery();

                return (ret == 1);
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

        public ClConcentration ObtenirObjetSelonId(string p_concentration)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM " +
                    NomTable +
                    " WHERE " + TypeColonne.ID_CONCENTRATION + " = @ID_CONCENTRATION;", cn);

                cmd.Parameters.Add("@ID_CONCENTRATION", SqlDbType.NVarChar).Value = p_concentration;

                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                    return new ClConcentration(reader.GetString(0), reader.GetString(1),
                        ClConcentration.GetTypeConcentration(reader.GetString(2)), reader.GetInt32(3), Utilitaire.GetTypeUser(reader.GetInt32(4)));
                else
                    return null;
            }
        }

        public ClConcentration ObtenirObjetSelonNom(string p_nom)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM " +
                    NomTable +
                    " WHERE " + TypeColonne.NOM_CONCENTRATION + " = @NOM_CONCENTRATION;", cn);

                cmd.Parameters.Add("@NOM_CONCENTRATION", SqlDbType.NVarChar).Value = p_nom;

                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                    return new ClConcentration(reader.GetString(0), reader.GetString(1),
                        ClConcentration.GetTypeConcentration(reader.GetString(2)), reader.GetInt32(3), Utilitaire.GetTypeUser(reader.GetInt32(4)));
                else
                    return null;
            }
        }


        public int? ObtenirObectifSelonNom(string p_nomConcentration)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT " + TypeColonne.OBJECTIF_CREDITS + " FROM " +
                    NomTable +
                    " WHERE " + TypeColonne.NOM_CONCENTRATION + " = @NOM_CONCENTRATION;", cn);

                cmd.Parameters.Add("@NOM_CONCENTRATION", SqlDbType.NVarChar).Value = p_nomConcentration;

                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                    return reader.GetInt32(0);
                else
                    return null;
            }
        }

        public double ObtenirObectifSelonId(string p_idConcentration)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT " + TypeColonne.OBJECTIF_CREDITS + " FROM " +
                    NomTable +
                    " WHERE " + TypeColonne.ID_CONCENTRATION + " = @ID_CONCENTRATION;", cn);

                cmd.Parameters.Add("@ID_CONCENTRATION", SqlDbType.NVarChar).Value = p_idConcentration;

                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                    return reader.GetInt32(0);
                else
                    return 0.0;
            }
        }

        public string ObtenirTypeSelonNom(string p_nomConcentration)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT " + TypeColonne.TYPE_CONCENTRATION + " FROM " +
                    NomTable +
                    " WHERE " + TypeColonne.NOM_CONCENTRATION + " = @NOM_CONCENTRATION;", cn);

                cmd.Parameters.Add("@NOM_CONCENTRATION", SqlDbType.NVarChar).Value = p_nomConcentration;

                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                    return reader.GetString(0);
                else
                    return null;
            }
        }

        public DataSet ObtenirTousSQLData(string p_trierPar)
        {
            SqlCommand commande = new SqlCommand();
            DataSet donnees = new DataSet();
            SqlConnection connexion =
                new SqlConnection(Utilitaire.GetConnectionString("DonneesCreditsSante"));

            commande.CommandText =
                    "SELECT * FROM " + NomTable +
                    " ORDER BY " + p_trierPar + " ASC;";

            commande.Connection = connexion;
            SqlDataAdapter da = new SqlDataAdapter(commande);
            da.Fill(donnees);
            connexion.Open();
            commande.ExecuteNonQuery();
            connexion.Close();

            return donnees;
        }

        public DataSet ObtenirTousSQLData(string p_trierPar, string p_recherche, string p_filterColumn)
        {
            SqlCommand commande = new SqlCommand();
            DataSet donnees = new DataSet();
            SqlConnection connexion =
                new SqlConnection(Utilitaire.GetConnectionString("DonneesCreditsSante"));

            commande.CommandText =
                    "SELECT * FROM " + NomTable +
                    " WHERE " + p_filterColumn + " LIKE '%" + p_recherche + "%'" +
                    " ORDER BY " + p_trierPar + " ASC;";

            commande.Connection = connexion;
            SqlDataAdapter da = new SqlDataAdapter(commande);
            da.Fill(donnees);
            connexion.Open();
            commande.ExecuteNonQuery();
            connexion.Close();

            return donnees;
        }

        public DataSet ObtenirTousSQLSelonTypeUser(TypeUser type)
        {
            SqlCommand commande = new SqlCommand();
            DataSet donnees = new DataSet();
            SqlConnection connexion =
                new SqlConnection(Utilitaire.GetConnectionString("DonneesCreditsSante"));

            commande.CommandText =
                    "SELECT * FROM " + NomTable +
                    " WHERE " + TypeColonne.TYPE_USER + " = " + (int)type +
                    " ORDER BY " + TypeColonne.TYPE_CONCENTRATION + " ASC, " +
                    TypeColonne.NOM_CONCENTRATION + " ASC;";
            commande.Connection = connexion;
            SqlDataAdapter da = new SqlDataAdapter(commande);
            da.Fill(donnees);
            connexion.Open();
            commande.ExecuteNonQuery();
            connexion.Close();

            return donnees;
        }

        public List<ClConcentration> ObtenirTous()
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM " + NomTable +
                    " ORDER BY " + TypeColonne.TYPE_CONCENTRATION.ToString() + " ASC, " +
                    TypeColonne.NOM_CONCENTRATION.ToString() + " ASC;", cn);

                cn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                List<ClConcentration> listeServices = new List<ClConcentration>();
                while (reader.Read())
                {
                    ClConcentration dept =
                        new ClConcentration(reader.GetString(0), reader.GetString(1),
                            ClConcentration.GetTypeConcentration(reader.GetString(2)), reader.GetInt32(3), Utilitaire.GetTypeUser(reader.GetInt32(4)));
                    listeServices.Add(dept);
                }

                return listeServices;
            }
        }

        public List<ClConcentration> ObtenirTousSelonTypeUser(TypeUser type)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM " + NomTable +
                    " WHERE " + TypeColonne.TYPE_USER + " = @TYPE_USER " +
                    " ORDER BY " + TypeColonne.TYPE_CONCENTRATION.ToString() + " ASC, " +
                    TypeColonne.NOM_CONCENTRATION.ToString() + " ASC;", cn);

                cmd.Parameters.Add("@TYPE_USER", SqlDbType.Int).Value = (int)type;
                cn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                List<ClConcentration> listeServices = new List<ClConcentration>();
                while (reader.Read())
                {
                    ClConcentration conc =
                        new ClConcentration(reader.GetString(0), reader.GetString(1),
                            ClConcentration.GetTypeConcentration(reader.GetString(2)), reader.GetInt32(3), Utilitaire.GetTypeUser(reader.GetInt32(4)));
                    listeServices.Add(conc);
                }

                return listeServices;
            }
        }

        public List<ClConcentration> ObtenirSelonType(TypeConcentration p_typeConcentration)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM " + NomTable +
                    " WHERE " + TypeColonne.TYPE_CONCENTRATION + " = @TYPE_CONCENTRATION " +
                    " ORDER BY " + TypeColonne.TYPE_CONCENTRATION + " ASC;", cn);

                cmd.Parameters.Add("@TYPE_CONCENTRATION", SqlDbType.NVarChar).Value = p_typeConcentration.ToString();
                cn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                List<ClConcentration> listeServices = new List<ClConcentration>();
                while (reader.Read())
                {
                    ClConcentration dept =
                        new ClConcentration(reader.GetString(0), reader.GetString(1),
                            ClConcentration.GetTypeConcentration(reader.GetString(2)), reader.GetInt32(3), Utilitaire.GetTypeUser(reader.GetInt32(4)));
                    listeServices.Add(dept);
                }

                return listeServices;
            }
        }

        public DataSet ObtenirTotalProgrammes()
        {
            using (SqlConnection connexion = new SqlConnection(StringDeConnexion))
            {
                DataSet donnees = new DataSet();
                SqlCommand commande = new SqlCommand(
                    "SELECT concentration.NOM_CONCENTRATION, SUM(SE.NB_CREDITS) AS NB_CREDITS, concentration.objectif_credits," +
                    "CASE WHEN concentration.objectif_credits > 0 then (100 * SUM(SE.NB_CREDITS) / concentration.objectif_credits) else 0 end as 'objectif'" +
                    "FROM concentration INNER JOIN " +
                            "utilisateur ON concentration.ID_CONCENTRATION = utilisateur.ID_CONCENTRATION INNER JOIN " +
                            "seance_entrainement SE ON utilisateur.ID_UTILISATEUR = SE.ID_UTILISATEUR " +
                    "WHERE (concentration.TYPE_CONCENTRATION LIKE 'Programme%') " +
                    "GROUP BY concentration.NOM_CONCENTRATION, concentration.objectif_credits", connexion);

                commande.Connection = connexion;

                SqlDataAdapter da = new SqlDataAdapter(commande);

                da.Fill(donnees);

                connexion.Open();
                commande.ExecuteNonQuery();
                connexion.Close();

                return donnees;
            }
        }

        public DataSet ObtenirTotalDepartementsServices()
        {
            using (SqlConnection connexion = new SqlConnection(StringDeConnexion))
            {
                DataSet donnees = new DataSet();
                SqlCommand commande = new SqlCommand(
                    "SELECT concentration.NOM_CONCENTRATION, SUM(seance_entrainement.NB_CREDITS) AS NB_CREDITS, concentration.objectif_credits, " +
                    "CASE WHEN concentration.objectif_credits > 0 then (100 * SUM(seance_entrainement.NB_CREDITS) / concentration.objectif_credits) else 0 end as 'objectif'" +
                    "FROM concentration INNER JOIN " +
                            "utilisateur ON concentration.ID_CONCENTRATION = utilisateur.ID_CONCENTRATION INNER JOIN " +
                            "seance_entrainement ON utilisateur.ID_UTILISATEUR = seance_entrainement.ID_UTILISATEUR " +
                    "WHERE (concentration.TYPE_CONCENTRATION LIKE '%Département%' OR concentration.TYPE_CONCENTRATION LIKE '%Service%') " +
                    "GROUP BY concentration.NOM_CONCENTRATION, concentration.objectif_credits " +
                    "ORDER BY concentration.NOM_CONCENTRATION", connexion);

                commande.Connection = connexion;

                SqlDataAdapter da = new SqlDataAdapter(commande);
                da.Fill(donnees);
                connexion.Open();
                commande.ExecuteNonQuery();
                connexion.Close();

                return donnees;
            }
        }

        public DataSet ObtenirTotalCommunautés()
        {
            using (SqlConnection connexion = new SqlConnection(StringDeConnexion))
            {
                DataSet donnees = new DataSet();
                SqlCommand commande = new SqlCommand(
                    "SELECT concentration.NOM_CONCENTRATION, SUM(seance_entrainement.NB_CREDITS) AS NB_CREDITS, concentration.objectif_credits, " +
                    "CASE WHEN concentration.objectif_credits > 0 then (100 * SUM(seance_entrainement.NB_CREDITS) / concentration.objectif_credits) else 0 end as 'objectif'" +
                    "FROM concentration INNER JOIN " +
                            "utilisateur ON concentration.ID_CONCENTRATION = utilisateur.ID_CONCENTRATION INNER JOIN " +
                            "seance_entrainement ON utilisateur.ID_UTILISATEUR = seance_entrainement.ID_UTILISATEUR " +
                    "WHERE concentration.TYPE_CONCENTRATION LIKE '%Communauté%' " +
                    "GROUP BY concentration.NOM_CONCENTRATION, concentration.objectif_credits " +
                    "ORDER BY concentration.NOM_CONCENTRATION", connexion);

                commande.Connection = connexion;

                SqlDataAdapter da = new SqlDataAdapter(commande);
                da.Fill(donnees);
                connexion.Open();
                commande.ExecuteNonQuery();
                connexion.Close();

                return donnees;
            }
        }

        public DataSet ObtenirTotalPartenaires()
        {
            using (SqlConnection connexion = new SqlConnection(StringDeConnexion))
            {
                DataSet donnees = new DataSet();
                SqlCommand commande = new SqlCommand(
                    "SELECT concentration.NOM_CONCENTRATION, SUM(seance_entrainement.NB_CREDITS) AS NB_CREDITS, concentration.objectif_credits, " +
                    "CASE WHEN concentration.objectif_credits > 0 then (100 * SUM(seance_entrainement.NB_CREDITS) / concentration.objectif_credits) else 0 end as 'objectif'" +
                    "FROM concentration INNER JOIN " +
                            "utilisateur ON concentration.ID_CONCENTRATION = utilisateur.ID_CONCENTRATION INNER JOIN " +
                            "seance_entrainement ON utilisateur.ID_UTILISATEUR = seance_entrainement.ID_UTILISATEUR " +
                    "WHERE concentration.TYPE_Concentration LIKE '%Partenaire%' " +
                    "GROUP BY concentration.NOM_CONCENTRATION, concentration.objectif_credits " +
                    "ORDER BY concentration.NOM_CONCENTRATION", connexion);

                commande.Connection = connexion;

                SqlDataAdapter da = new SqlDataAdapter(commande);
                da.Fill(donnees);
                connexion.Open();
                commande.ExecuteNonQuery();
                connexion.Close();

                return donnees;
            }
        }

        public DataSet ObtenirTotalParConcentrationSelonTypeSQLData(TypeConcentration p_typeConcentration)
        {
            using (SqlConnection connexion = new SqlConnection(StringDeConnexion))
            {
                DataSet donnees = new DataSet();
                SqlCommand commande = new SqlCommand(
                    "SELECT concentration.NOM_CONCENTRATION, SUM(seance_entrainement.NB_CREDITS) AS NB_CREDITS, concentration.objectif_credits, " +
                    "CASE WHEN concentration.objectif_credits > 0 then (100 * SUM(seance_entrainement.NB_CREDITS) / concentration.objectif_credits) else 0 end as 'objectif'" +
                    "FROM concentration INNER JOIN " +
                            "utilisateur ON concentration.ID_CONCENTRATION = utilisateur.ID_CONCENTRATION INNER JOIN " +
                            "seance_entrainement ON utilisateur.ID_UTILISATEUR = seance_entrainement.ID_UTILISATEUR " +
                    "WHERE (concentration.TYPE_CONCENTRATION = @TYPE_CONCENTRATION) " +
                    "AND seance_entrainement.[date] BETWEEN (SELECT DATE FROM Parametre WHERE Nom LIKE 'DateDebutBougeotte') " +
                    "AND (SELECT DATE FROM Parametre WHERE Nom LIKE 'DateFinBougeotte') " +
                    "GROUP BY concentration.NOM_CONCENTRATION, concentration.objectif_credits " +
                    "ORDER BY concentration.NOM_CONCENTRATION", connexion);

                commande.Parameters.Add("@TYPE_CONCENTRATION", SqlDbType.NVarChar).Value = p_typeConcentration.ToString();
                commande.Connection = connexion;

                SqlDataAdapter da = new SqlDataAdapter(commande);
                da.Fill(donnees);
                connexion.Open();
                commande.ExecuteNonQuery();
                connexion.Close();
                return donnees;
            }
        }
    }
}
