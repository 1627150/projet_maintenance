using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace CS_Shark
{
    static class Shark
    {
        [STAThread]
        static void Main()
        {
            StreamWriter logger = new StreamWriter("shark_log.txt", true);

            try
            {
                SqlConnection thisConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DonneesCreditsSante"].ConnectionString);

                thisConnection.Open();
                SqlCommand thisCommand = thisConnection.CreateCommand();

                // on supprime les utilisateurs qui ne se sont pas connectés depuis 2 ans
                thisCommand.CommandText = "DELETE FROM utilisateur WHERE last_login < (SELECT DATEADD(year, -2, GETDATE()))";
                logger.WriteLine(String.Format("{0} - {1} utilisateur(s) supprimé(s).", DateTime.Now.ToString(), thisCommand.ExecuteNonQuery()));

                SqlCommand cleanSousConcentration = thisConnection.CreateCommand();

                // on supprime les sous-concentrations orphelines
                cleanSousConcentration.CommandText =
                    @"DELETE FROM sous_concentration 
                      WHERE id_concentration IN (
                          SELECT sc.id_concentration 
                          FROM sous_concentration sc LEFT JOIN concentration c 
                              ON sc.id_parent = c.id_concentration 
                          WHERE c.id_concentration IS NULL)";
                logger.WriteLine(String.Format("{0} - {1} sous-concentration(s) supprimées.", DateTime.Now.ToString(), cleanSousConcentration.ExecuteNonQuery()));

                thisConnection.Close();
            }
            catch (SqlException e)
            {
                logger.WriteLine(String.Format("{0} - {1}", DateTime.Now.ToString(), e.Message));
            }

            logger.Close();
            return;
        }
    }
}
