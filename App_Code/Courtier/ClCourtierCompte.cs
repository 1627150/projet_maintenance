using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
// using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using CS2013;

namespace CS2013
{
    class ClCourtierCompte : ClCourtier
    {
        public enum TypeColonne
        {
            ID_UTILISATEUR,
            MOT_DE_PASSE,
            ID_CONCENTRATION,
            TYPE_ROLE,
            EST_ACTIF,
            TYPE_USER,
            EMAIL,
            NOM,
            EMAIL_TELEPHONE,
            LAST_LOGIN
        }

        public String NomTable
        {
            get
            {
                return "utilisateur";
            }
        }

        private ClCourtierCompte()
        { }

        static private ClCourtierCompte m_instance = null;

        static public ClCourtierCompte GetInstance()
        {
            if (m_instance == null)
            {
                m_instance = new ClCourtierCompte();
            }

            return m_instance;
        }

        public bool Insérer(ref ClCompte p_compte)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                string mcrypte = FormsAuthentication.HashPasswordForStoringInConfigFile(p_compte.MotDePasse, "MD5");

                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO " + NomTable +
                    " VALUES (@ID_UTILISATEUR, @MOT_DE_PASSE, @ID_CONCENTRATION, @TYPE_ROLE, @TYPE_USER, @EMAIL, @NOM, @EMAIL_TELEPHONE, GETDATE());", cn);
                //cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MOT_DE_PASSE", SqlDbType.NVarChar).Value =
                    FormsAuthentication.HashPasswordForStoringInConfigFile(p_compte.MotDePasse, "MD5");
                cmd.Parameters.Add("@ID_CONCENTRATION", SqlDbType.NVarChar).Value = p_compte.Concentration;
                cmd.Parameters.Add("@ID_UTILISATEUR", SqlDbType.NVarChar).Value = p_compte.IdUtilisateur;
                cmd.Parameters.Add("@TYPE_ROLE", SqlDbType.NVarChar).Value = p_compte.Role.ToString();
                cmd.Parameters.Add("@TYPE_USER", SqlDbType.Int).Value = (int)p_compte.Type;
                cmd.Parameters.Add("@EMAIL", SqlDbType.NVarChar).Value = p_compte.Email;
                cmd.Parameters.Add("@NOM", SqlDbType.NVarChar).Value = p_compte.Nom;
                cmd.Parameters.Add("@EMAIL_TELEPHONE", SqlDbType.NVarChar).Value = p_compte.EmailTelephone;

                cn.Open();

                try
                {
                    cmd.ExecuteNonQuery();
                    p_compte.MotDePasse = mcrypte;
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }

            }
        }

        public bool Modifier(ClCompte p_compte)
        {
            string motDePasseCrypté = FormsAuthentication.HashPasswordForStoringInConfigFile(p_compte.MotDePasse, "MD5");

            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE " + NomTable +
                    " SET " + TypeColonne.MOT_DE_PASSE.ToString() + " = @MOT_DE_PASSE, " +
                    TypeColonne.ID_CONCENTRATION.ToString() + " = @ID_CONCENTRATION," +
                    TypeColonne.EMAIL.ToString() + " = @EMAIL, " +
                    TypeColonne.EMAIL_TELEPHONE.ToString() + " = @EMAIL_TELEPHONE, " +
                    TypeColonne.NOM.ToString() + " = @NOM " +
                    " WHERE " + TypeColonne.ID_UTILISATEUR.ToString() + " = @ID_UTILISATEUR;", cn);
                //cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ID_UTILISATEUR", SqlDbType.NVarChar).Value = p_compte.IdUtilisateur;
                cmd.Parameters.Add("@MOT_DE_PASSE", SqlDbType.NVarChar).Value = motDePasseCrypté;
                cmd.Parameters.Add("@ID_CONCENTRATION", SqlDbType.NVarChar).Value = p_compte.Concentration;
                cmd.Parameters.Add("@EMAIL", SqlDbType.NVarChar).Value = p_compte.Email;
                cmd.Parameters.Add("@NOM", SqlDbType.NVarChar).Value = p_compte.Nom;
                cmd.Parameters.Add("@EMAIL_TELEPHONE", SqlDbType.NVarChar).Value = p_compte.EmailTelephone;

                cn.Open();
                int ret = cmd.ExecuteNonQuery();

                return (ret == 1);
            }
        }

        public bool UpdateLastLogin(string id)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE " + NomTable +
                    " SET " + TypeColonne.LAST_LOGIN + " = GETDATE() " +
                    " WHERE " + TypeColonne.ID_UTILISATEUR + " = @ID_UTILISATEUR;", cn);

                cmd.Parameters.Add("@ID_UTILISATEUR", SqlDbType.NVarChar).Value = id;

                cn.Open();
                int ret = cmd.ExecuteNonQuery();

                return (ret == 1);
            }
        }

        public bool ModifierSansMotDePasse(ClCompte p_compte)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE " + NomTable +
                    " SET " + TypeColonne.ID_CONCENTRATION.ToString() + " = @ID_CONCENTRATION, " +
                    TypeColonne.EMAIL.ToString() + " = @EMAIL, " +
                    TypeColonne.EMAIL_TELEPHONE.ToString() + " = @EMAIL_TELEPHONE, " +
                    TypeColonne.NOM.ToString() + " = @NOM " +
                    "WHERE " + TypeColonne.ID_UTILISATEUR.ToString() + " = @ID_UTILISATEUR;", cn);

                cmd.Parameters.Add("@ID_UTILISATEUR", SqlDbType.NVarChar).Value = p_compte.IdUtilisateur;
                cmd.Parameters.Add("@ID_CONCENTRATION", SqlDbType.NVarChar).Value = p_compte.Concentration;
                cmd.Parameters.Add("@EMAIL", SqlDbType.NVarChar).Value = p_compte.Email;
                cmd.Parameters.Add("@NOM", SqlDbType.NVarChar).Value = p_compte.Nom;
                cmd.Parameters.Add("@EMAIL_TELEPHONE", SqlDbType.NVarChar).Value = p_compte.EmailTelephone;

                cn.Open();
                int ret = cmd.ExecuteNonQuery();

                return (ret == 1);
            }
        }


        public bool Desactiver(string p_idCompte)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE " + NomTable +
                    " SET " + TypeColonne.EST_ACTIF + " = @EST_ACTIF" +
                    " WHERE " + TypeColonne.ID_UTILISATEUR + " = @ID_UTILISATEUR;", cn);
                //cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ID_UTILISATEUR", SqlDbType.NVarChar).Value = p_idCompte;
                cmd.Parameters.Add("@EST_ACTIF", SqlDbType.Bit).Value = false;

                cn.Open();
                int ret = cmd.ExecuteNonQuery();

                return (ret == 1);
            }
        }

        public bool ModifierRole(ClCompte p_compte)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE " + NomTable +
                    " SET " + TypeColonne.TYPE_ROLE.ToString() + " = @TYPE_ROLE" +
                    " WHERE " + TypeColonne.ID_UTILISATEUR.ToString() + " = @ID_UTILISATEUR;", cn);
                //cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ID_UTILISATEUR", SqlDbType.NVarChar).Value = p_compte.IdUtilisateur;
                cmd.Parameters.Add("@TYPE_ROLE", SqlDbType.NVarChar).Value = p_compte.Role.ToString();

                cn.Open();
                int ret = cmd.ExecuteNonQuery();

                return (ret == 1);
            }
        }

        public bool Supprimer(string p_idCompte)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM " + NomTable +
                    " WHERE " + TypeColonne.ID_UTILISATEUR + " = @ID_UTILISATEUR;" +
                    " DELETE FROM seance_entrainement WHERE " +
                    ClCourtierSeanceEntrainement.TypeColonne.ID_UTILISATEUR + " = @ID_UTILISATEUR", cn);
                //cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ID_UTILISATEUR", SqlDbType.NVarChar).Value = p_idCompte;

                cn.Open();
                int ret = cmd.ExecuteNonQuery();

                return (ret == 1);
            }
        }

        public bool SupprimerCompteSelonDate(DateTime p_date)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM " + NomTable +
                    " WHERE " + TypeColonne.LAST_LOGIN + " BETWEEN '2000-01-01 00:00:00' AND '" + p_date.ToString() + "';", cn);
                cn.Open();
                int ret = cmd.ExecuteNonQuery();

                return (ret == 1);
            }
        }

        public bool SupprimerTous()
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM " + NomTable, cn);

                cn.Open();
                int ret = cmd.ExecuteNonQuery();

                return (ret == 1);
            }
        }

        public ClCompte ObtenirCompteAuthentifie(ClCompte p_compte)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {

                string mcrypte = FormsAuthentication.HashPasswordForStoringInConfigFile(p_compte.MotDePasse, "MD5");

                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM " + NomTable +
                    " WHERE " + TypeColonne.ID_UTILISATEUR + " = @ID_UTILISATEUR " +
                    " AND " + TypeColonne.MOT_DE_PASSE + " = @MOT_DE_PASSE", cn);
                //cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ID_UTILISATEUR", SqlDbType.NVarChar).Value = p_compte.IdUtilisateur;
                cmd.Parameters.Add("@MOT_DE_PASSE", SqlDbType.NVarChar).Value = mcrypte;
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                    return new ClCompte(reader.GetString(0), reader.GetString(1),
                        reader.GetString(2), ClCompte.GetTypeCompte(reader.GetString(3)), Utilitaire.GetTypeUser(reader.GetInt32(4)),
                        reader.IsDBNull(5) ? "" : reader.GetString(5),
                        reader.IsDBNull(6) ? "" : reader.GetString(6),
                        reader.IsDBNull(7) ? "" : reader.GetString(7));
                else
                    return null;
            }
        }

        public ClCompte ObtenirCompteSelonId(string p_idUtilisateur)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM " + NomTable +
                    " WHERE " + TypeColonne.ID_UTILISATEUR + " = @ID_UTILISATEUR ", cn);
                //cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ID_UTILISATEUR", SqlDbType.NVarChar).Value = p_idUtilisateur;
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                    return new ClCompte(reader.GetString(0), reader.GetString(1),
                        reader.GetString(2), ClCompte.GetTypeCompte(reader.GetString(3)), Utilitaire.GetTypeUser(reader.GetInt32(4)),
                         reader.IsDBNull(5) ? "" : reader.GetString(5),
                         reader.IsDBNull(6) ? "" : reader.GetString(6),
                         reader.IsDBNull(7) ? "" : reader.GetString(7));
                else
                    return null;
            }
        }

        public ClCompte ObtenirCompteSelonCourriel(string p_courrielUtilisateur)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM " + NomTable +
                    " WHERE " + TypeColonne.EMAIL + " = @EMAIL_UTILISATEUR ", cn);
                //cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EMAIL_UTILISATEUR", SqlDbType.NVarChar).Value = p_courrielUtilisateur;
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                    return new ClCompte(reader.GetString(0), reader.GetString(1),
                        reader.GetString(2), ClCompte.GetTypeCompte(reader.GetString(3)), Utilitaire.GetTypeUser(reader.GetInt32(4)),
                         reader.IsDBNull(5) ? "" : reader.GetString(5),
                         reader.IsDBNull(6) ? "" : reader.GetString(6),
                         reader.IsDBNull(7) ? "" : reader.GetString(7));
                else
                    return null;
            }
        }

        public List<string> GetAllEmailPhone()
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT distinct " + TypeColonne.EMAIL_TELEPHONE +
                    " from " + NomTable +
                    " WHERE " + TypeColonne.EMAIL_TELEPHONE + " LIKE '%@%'", cn);

                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                List<string> mails = new List<string>();
                while (reader.Read())
                    mails.Add(reader.GetString(0));

                return mails;
            }
        }

        public List<string> GetAllEMails()
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT distinct Utilisateur.Email
                  FROM Utilisateur
                  WHERE (Utilisateur.Email LIKE '%@%')", cn);

                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                List<string> mails = new List<string>();
                while (reader.Read())
                    mails.Add(reader.GetString(0));

                return mails;
            }
        }

        public List<string> GetAllEMailsByTypeConcentration(TypeConcentration type)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT Utilisateur.Email
                  FROM Utilisateur INNER JOIN Concentration ON Utilisateur.Id_Concentration = Concentration.Id_Concentration
                  WHERE (Utilisateur.Email LIKE '%@%' AND Concentration.Type_Concentration = @TYPE_CONCENTRATION)
                  UNION
                  SELECT Utilisateur_1.Email
                  FROM Utilisateur AS Utilisateur_1 INNER JOIN sous_concentration ON Utilisateur_1.Id_Concentration = sous_concentration.id_concentration
                  WHERE (Utilisateur_1.Email LIKE '%@%' AND sous_concentration.Type_Concentration = @TYPE_CONCENTRATION)", cn);

                cmd.Parameters.Add("@TYPE_CONCENTRATION", SqlDbType.NVarChar).Value = type.ToString();

                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                List<string> mails = new List<string>();
                while (reader.Read())
                    mails.Add(reader.GetString(0));

                return mails;
            }
        }

        public List<string> GetAllEMailsPhoneByTypeConcentration(TypeConcentration type)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT Utilisateur_2.email_telephone
                  FROM Utilisateur AS Utilisateur_2 INNER JOIN sous_concentration ON Utilisateur_2.Id_Concentration = sous_concentration.id_concentration
                  WHERE (Utilisateur_2.Email_telephone LIKE '%@%' AND sous_concentration.Type_Concentration = @TYPE_CONCENTRATION)
                  UNION
                  SELECT Utilisateur_3.email_telephone
                  FROM Utilisateur AS Utilisateur_3 INNER JOIN sous_concentration ON Utilisateur_3.Id_Concentration = sous_concentration.id_concentration
                  WHERE (Utilisateur_3.Email_telephone LIKE '%@%' AND sous_concentration.Type_Concentration = @TYPE_CONCENTRATION)", cn);

                cmd.Parameters.Add("@TYPE_CONCENTRATION", SqlDbType.NVarChar).Value = type.ToString();

                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                List<string> mails = new List<string>();
                while (reader.Read())
                    mails.Add(reader.GetString(0));

                return mails;
            }
        }

        public bool EmailExiste(string p_email)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT 1 FROM " + NomTable +
                    " WHERE " + TypeColonne.EMAIL + " = @EMAIL ", cn);

                cmd.Parameters.Add("@EMAIL", SqlDbType.NVarChar).Value = p_email;

                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                    return true;
                else
                    return false;
            }
        }

        public DataSet ObtenirTousSQLData(string p_trierPar)
        {
            SqlCommand commande = new SqlCommand();
            DataSet donnees = new DataSet();
            SqlConnection connexion =
                new SqlConnection(Utilitaire.GetConnectionString("DonneesCreditsSante"));

            commande.CommandText =
                    "SELECT utilisateur.ID_Utilisateur, utilisateur.Mot_De_Passe, utilisateur.ID_Concentration, utilisateur.Type_Role, concentration.Nom_Concentration, utilisateur.Nom, utilisateur.Email, utilisateur.email_telephone FROM utilisateur INNER JOIN  concentration ON utilisateur.ID_Concentration = concentration.ID_Concentration " +
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
                    "SELECT utilisateur.ID_Utilisateur, utilisateur.Mot_De_Passe, utilisateur.ID_Concentration, utilisateur.Type_Role, concentration.Nom_Concentration, utilisateur.Nom, utilisateur.Email, utilisateur.email_telephone FROM utilisateur INNER JOIN  concentration ON utilisateur.ID_Concentration = concentration.ID_Concentration " +
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

        public int NbCompteParConcentration(string p_idConcentration)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM " + NomTable +
                    " WHERE " + TypeColonne.ID_CONCENTRATION + " = @ID_CONCENTRATION ", cn);

                cmd.Parameters.Add("@ID_CONCENTRATION", SqlDbType.NVarChar).Value = p_idConcentration;
                cn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public bool NeedUpdatedMeta(ClCompte compte)
        {
            using (SqlConnection cn = new SqlConnection(StringDeConnexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT " + TypeColonne.NOM + ", " + TypeColonne.EMAIL +
                    " FROM " + NomTable +
                    " WHERE " + TypeColonne.ID_UTILISATEUR + " = @ID_UTILISATEUR ", cn);

                cmd.Parameters.Add("@ID_UTILISATEUR", SqlDbType.NVarChar).Value = compte.IdUtilisateur;

                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                if (reader.IsDBNull(0) || reader.IsDBNull(1))
                    return true;
                else
                    return false;
            }
        }
    }
}
