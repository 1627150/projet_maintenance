using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace CS2013
{
    class ClCourtierModificationAffichage : ClCourtier
    {
        public enum TypeColonne
        {
            ID,
            TITRE,
            CONTENU
        }

        private String NomTable
        {
            get
            {
                return "modificationAffichage";
            }
        }

        public ClCourtierModificationAffichage()
        { }

        private static ClCourtierModificationAffichage m_instance = null;

        public static ClCourtierModificationAffichage GetInstance()
        {
            if (m_instance == null)
                m_instance = new ClCourtierModificationAffichage();

            return m_instance;
        }

        public void Init()
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "CREATE TABLE modificationAffichage (" +
                    "[id] NVARCHAR(36) NOT NULL, " +
                    "[titre]   NVARCHAR(500)  NOT NULL, " +
                    "[contenu] NVARCHAR (MAX) NOT NULL, " +
                    "PRIMARY KEY CLUSTERED([id] ASC)); ", cn);

                cn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                    Insérer("nousJoindre", HttpUtility.HtmlEncode(Constantes.CONTACT_TITRE), HttpUtility.HtmlEncode(Constantes.CONTACT_DESCRIPTION));
                    Insérer("Historique", HttpUtility.HtmlEncode(Constantes.HISTORIQUE_TITRE), HttpUtility.HtmlEncode(Constantes.HISTOIRE_DESCRIPTION));
                    Insérer("affichageRadioStats", "", Constantes.AFFICHAGE_RADIO_STATS);
                }
                catch { }
            }
        }

        public bool Insérer(String p_id, String p_titre, String p_contenu)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO " + NomTable +
                    " VALUES (@ID, @TITRE, @CONTENU);", cn);

                cmd.Parameters.Add("@ID", SqlDbType.NVarChar).Value = p_id;
                cmd.Parameters.Add("@TITRE", SqlDbType.NVarChar).Value = p_titre;
                cmd.Parameters.Add("@CONTENU", SqlDbType.NVarChar).Value = p_contenu;

                cn.Open();
                int ret = cmd.ExecuteNonQuery();

                return (ret == 1);
            }
        }

        public bool Modifier(String p_id, String p_titre, String p_contenu)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE " + NomTable +
                    " SET " + TypeColonne.TITRE + " = @TITRE," +
                    TypeColonne.CONTENU + " = @CONTENU " +
                    " WHERE " + TypeColonne.ID + " = @ID;", cn);

                cmd.Parameters.Add("@TITRE", SqlDbType.NVarChar).Value = p_titre;
                cmd.Parameters.Add("@CONTENU", SqlDbType.NVarChar).Value = p_contenu;
                cmd.Parameters.Add("@ID", SqlDbType.NVarChar).Value = p_id;

                cn.Open();
                int ret = cmd.ExecuteNonQuery();

                return (ret == 1);
            }
        }

        public String ObtenirTitre(String p_id)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT " + TypeColonne.TITRE + " FROM " + NomTable +
                    " WHERE " + TypeColonne.ID + " = '" + p_id + "'; ", cn);

                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();

                return reader.GetString(0);
            }
        }

        public String ObtenirContenu(String p_id)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT " + TypeColonne.CONTENU + " FROM " + NomTable +
                    " WHERE " + TypeColonne.ID + " = '" + p_id + "'; ", cn);

                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();

                return reader.GetString(0);
            }
        }
    }
}
