using CS2013.App_Code.ObjetsAffaires;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace CS2013
{
    class ClCourtierOnglets : ClCourtier
    {
        public enum TypeColonne
        {
            ID,
            TITRE,
            VALID,
        }

        public String NomTable
        {
            get
            {
                return "onglets";
            }
        }

        public static List<ClOnglets> ObtenirTout()
        {
            SqlCommand cmd = new SqlCommand("Select * from Onglets",new SqlConnection(StringDeConnexion));

            cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                List<ClOnglets> onglets = new List<ClOnglets>();

                while (reader.Read())
                {
                    onglets.Add(new ClOnglets(reader.GetInt32(0), reader.GetString(1), reader.GetBoolean(2)));
                }
            cmd.Connection.Close();
                return onglets;
        }

        public static bool Modifier(bool valid, string titre)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Onglets"  +
                    " SET valid = @VALID" +
                    " WHERE titre = @TITRE;", cn);

                cmd.Parameters.Add("@VALID", SqlDbType.Bit).Value = valid;
                cmd.Parameters.Add("@TITRE", SqlDbType.NVarChar).Value = titre;

                cn.Open();
                int ret = cmd.ExecuteNonQuery();

                return (ret == 1);
            }
        }

        public static bool ObtenirValidation(string titre)
        {
            SqlCommand cmd = new SqlCommand("Select valid from Onglets where titre = @TITRE", new SqlConnection(StringDeConnexion));
            cmd.Parameters.Add("@TITRE", SqlDbType.NVarChar).Value = titre;
            cmd.Connection.Open();
            
            bool valid = true;
            SqlDataReader reader = cmd.ExecuteReader();

            List<ClOnglets> onglets = new List<ClOnglets>();

            while (reader.Read())
            {
                valid = reader.GetBoolean(0);
            }
            cmd.Connection.Close();

            return valid;
        }

    }


}
