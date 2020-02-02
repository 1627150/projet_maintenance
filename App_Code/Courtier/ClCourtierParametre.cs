using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CS2013
{
    class ClCourtierParametre : ClCourtier
    {
        public enum TypeColonne
        {
            NOM,
            VALEUR,
            DATE,
            VALUE
        }

        private String NomTable
        {
            get
            {
                return "parametre";
            }
        }

        public ClCourtierParametre()
        { }

        private static ClCourtierParametre m_instance = null;

        public static ClCourtierParametre GetInstance()
        {
            if (m_instance == null)
                m_instance = new ClCourtierParametre();

            return m_instance;
        }

        public List<ClParametre> ObtenirTous()
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM " + NomTable +
                    " ORDER BY " + TypeColonne.NOM + " ASC;", cn);
                cn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                List<ClParametre> parametres = new List<ClParametre>();

                while (reader.Read())
                {
                    if (reader.IsDBNull(2))
                        parametres.Add(new ClParametre(reader.GetString(0), reader.GetInt32(1)));
                    else
                        parametres.Add(new ClParametre(reader.GetString(0), reader.GetDateTime(2)));
                }

                return parametres;
            }
        }

        public ClParametre ObtenirSelonNom(string p_nom)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT *" +
                    " FROM " + NomTable +
                    " WHERE " + TypeColonne.NOM + " = @NOM;", cn);
                cmd.Parameters.Add("@NOM", SqlDbType.NVarChar).Value = p_nom;
                cn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    if (!reader.IsDBNull(2))
                        return new ClParametre(reader.GetString(0), reader.GetDateTime(2));
                    else if (!reader.IsDBNull(3))
                        return new ClParametre(reader.GetString(0), reader.GetString(3));
                    else
                        return new ClParametre(reader.GetString(0), reader.GetInt32(1));
                }

                return null;
            }
        }

        public bool Modifier(ClParametre p_parametre)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE " + NomTable +
                    " SET " + TypeColonne.VALEUR + " = @VALEUR " +
                    ((p_parametre.Date != new DateTime().Date) ? ", " + TypeColonne.DATE + " = @DATE" : "") +
                    (!String.IsNullOrEmpty(p_parametre.Value) ? ", " + TypeColonne.VALUE + " = @VALUE" : "") +
                    " WHERE " + TypeColonne.NOM + " = @NOM;", cn);

                cmd.Parameters.Add("@NOM", SqlDbType.NVarChar).Value = p_parametre.Nom;
                cmd.Parameters.Add("@VALEUR", SqlDbType.Int).Value = p_parametre.Valeur;

                if (p_parametre.Date != new DateTime().Date)
                    cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = p_parametre.Date;

                if (!String.IsNullOrEmpty(p_parametre.Value))
                    cmd.Parameters.Add("@VALUE", SqlDbType.NVarChar).Value = p_parametre.Value;

                cn.Open();
                int ret = cmd.ExecuteNonQuery();

                return (ret == 1);
            }
        }
    }
}
