using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CS2013
{
    class ClCourtierEvenement : ClCourtier
    {
        public enum TypeColonne
        {
            ID,
            TITRE,
            DATE_PUBLICATION,
            CONTENU
        }

        private String NomTable
        {
            get
            {
                return "evenement";
            }
        }

        public ClCourtierEvenement()
        { }

        private static ClCourtierEvenement m_instance = null;

        public static ClCourtierEvenement GetInstance()
        {
            if (m_instance == null)
                m_instance = new ClCourtierEvenement();

            return m_instance;
        }

        public bool Insérer(ClEvenement p_capsule)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO " + NomTable +
                    " VALUES (@ID, @TITRE, @DATE_PUBLICATION, @CONTENU);", cn);

                cmd.Parameters.Add("@ID", SqlDbType.NVarChar).Value = Guid.NewGuid().ToString();
                cmd.Parameters.Add("@TITRE", SqlDbType.NVarChar).Value = p_capsule.Titre;
                cmd.Parameters.Add("@DATE_PUBLICATION", SqlDbType.DateTime).Value = p_capsule.DatePublication;
                cmd.Parameters.Add("@CONTENU", SqlDbType.NVarChar).Value = p_capsule.Contenu;

                cn.Open();
                int ret = cmd.ExecuteNonQuery();

                return (ret == 1);
            }
        }

        public bool Modifier(ClEvenement p_capsule)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE " + NomTable +
                    " SET " + TypeColonne.TITRE + " = @TITRE ," +
                    TypeColonne.CONTENU + " = @CONTENU " +
                    " WHERE " + TypeColonne.ID + " = @ID;", cn);

                cmd.Parameters.Add("@TITRE", SqlDbType.NVarChar).Value = p_capsule.Titre;
                cmd.Parameters.Add("@CONTENU", SqlDbType.NVarChar).Value = p_capsule.Contenu;
                cmd.Parameters.Add("@ID", SqlDbType.NVarChar).Value = p_capsule.ID;

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
                    " WHERE " + TypeColonne.ID.ToString() + " = @ID", cn);
                cmd.Parameters.Add("@ID", SqlDbType.NVarChar).Value = p_idConcentration;

                cn.Open();
                int ret = cmd.ExecuteNonQuery();

                return (ret == 1);
            }
        }

        public List<ClEvenement> ObtenirTous()
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * " +
                    " FROM " + NomTable +
                    " WHERE " + TypeColonne.DATE_PUBLICATION + " < (SELECT DATEADD(dd, DATEDIFF(dd, - 1, GETDATE()), 0))" +
                    " ORDER BY " + TypeColonne.DATE_PUBLICATION + " DESC;", cn);
                cn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                List<ClEvenement> capsulesEvenements = new List<ClEvenement>();

                while (reader.Read())
                {
                    capsulesEvenements.Add(new ClEvenement(reader.GetString(0), reader.GetString(1), reader.GetDateTime(2), reader.GetString(3)));
                }

                return capsulesEvenements;
            }
        }

        public ClEvenement ObtenirPlusRecente()
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT TOP 1 * " +
                    " FROM " + NomTable +
                    " WHERE " + TypeColonne.DATE_PUBLICATION + " < (SELECT DATEADD(dd, DATEDIFF(dd, - 1, GETDATE()), 0))" +
                    " ORDER BY " + TypeColonne.DATE_PUBLICATION + " DESC;", cn);
                cn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                ClEvenement capsule = new ClEvenement();

                if (reader.Read())
                {
                    capsule.ID = reader.GetString(0);
                    capsule.Titre = reader.GetString(1);
                    capsule.DatePublication = reader.GetDateTime(2);
                    capsule.Contenu = reader.GetString(3);
                }

                return capsule;
            }
        }

        public DataSet ObtenirTousSQLData()
        {
            SqlCommand commande = new SqlCommand();
            DataSet donnees = new DataSet();
            SqlConnection connexion =
                new SqlConnection(Utilitaire.GetConnectionString("DonneesCreditsSante"));

            commande.CommandText =
                    "SELECT * FROM " + NomTable +
                    " ORDER BY " + TypeColonne.DATE_PUBLICATION + " DESC;";
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
