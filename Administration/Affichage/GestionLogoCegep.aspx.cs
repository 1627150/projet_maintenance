using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CS2013.Administration
{
    public partial class GestionLogoCegep : System.Web.UI.Page
    {
        ClCompte m_compteConnecte = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            m_compteConnecte = (ClCompte)Session["utilisateur"];

            if (m_compteConnecte == null)
            {
                Response.Redirect("~/Pages/FinSession.aspx");
            }

            if (!IsPostBack)
            {
                ChargerPage();
            }
        }

        public void ButtonValider_Click(object sender, EventArgs e)
        {
            int nbErreurs = 0;

            if (fileUploadLogo.HasFile)
            {
                Exception etatTeleversement = Utilitaire.TeleverserImage(fileUploadLogo.PostedFile, "Images", "LogoCegep", true);

                if (etatTeleversement == null)
                {
                    labelStatus.ForeColor = Color.White;
                    labelStatus.BackColor = Color.LightGreen;
                    labelStatus.Text = "État du téléversement: Fichier téléversé avec succès!";
                }
                else
                {
                    ++nbErreurs;
                    labelStatus.ForeColor = Color.White;
                    labelStatus.BackColor = Color.Red;
                    labelStatus.Text = 
                        "État du téléversement: Le fichier n'a pas pu être téléversé. L'erreur suivante est survenue: « " + 
                        etatTeleversement.Message + " »";
                }
            }

            if (nbErreurs == 0)
            {
                labelErreur.ForeColor = System.Drawing.Color.Green;
                labelErreur.Text = "Modification effectuée avec succès";
                foreach (System.Collections.DictionaryEntry entry in HttpContext.Current.Cache)
                {
                    HttpContext.Current.Cache.Remove((string)entry.Key);
                }
            }
            else
            {
                labelErreur.ForeColor = System.Drawing.Color.Red;
                labelErreur.Text = "Un problème est survenu";
            }

            Utilitaire.AffecterParametres(Session);
            
            ChargerPage();
        }

        private void ChargerPage()
        {
            labelChangerLogo.Text = "Changer le logo du cégep: ";
        }
    }
}