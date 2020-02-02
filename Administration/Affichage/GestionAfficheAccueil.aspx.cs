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
    public partial class GestionAfficheAccueil : System.Web.UI.Page
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

            if (fileUploadAffiche.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName(fileUploadAffiche.FileName);
                    if (!Utilitaire.EstFichierJpeg(filename))
                    {
                        throw new NotSupportedException("Seuls les fichiers JPEG sont autorisés comme image d'accueil.");
                    }
                    fileUploadAffiche.SaveAs(Request.PhysicalApplicationPath + "/Images/AfficheAccueil.jpg");
                    labelStatus.ForeColor = Color.White;
                    labelStatus.BackColor = Color.LightGreen;
                    labelStatus.Text = "État du téléversement: Fichier téléversé avec succès!";
                }
                catch (Exception ex)
                {
                    ++nbErreurs;
                    labelStatus.ForeColor = Color.White;
                    labelStatus.BackColor = Color.Red;
                    labelStatus.Text = "État du téléversement: Le fichier n'a pas pu être téléversé. L'erreur suivante est survenue: « " + ex.Message + " »";
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
            labelChangerAffiche.Text = "Changer l'affiche d'accueil: ";
        }
    }
}