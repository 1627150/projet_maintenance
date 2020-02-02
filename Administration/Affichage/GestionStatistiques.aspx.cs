using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CS2013.Administration
{
    public partial class GestionStatistiques : System.Web.UI.Page
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

        private void ChargerPage()
        {
            string affichageRadioStats = ClCourtierModificationAffichage.GetInstance().ObtenirContenu("affichageRadioStats");
            string affichageStats = ClCourtierModificationAffichage.GetInstance().ObtenirContenu("Statistique");

            string[] listeRadioStats = affichageRadioStats.Split('|');

            for (int i = 0; i < listeRadioStats.Length; i++)
            {
                if (listeRadioStats[i] == "Visible")
                    radioStatistiques.Items[i].Selected = true;
            }

            string[] listeStats = affichageStats.Split('/');

            for (int i = 0; i < listeStats.Length; i++)
            {
                if (listeStats[i] == "1")
                    radioAffichageStats.Items[i].Selected = true;
            }
        }

        protected void buttonValider_Click(object sender, EventArgs e)
        {
            string affichageRadioStats = "";

            for (int i = 0; i < radioStatistiques.Items.Count; i++)
            {
                if (i == 0)
                {
                    affichageRadioStats += (radioStatistiques.Items[i].Selected ? "Visible" : "Cache");
                }
                else
                {
                    affichageRadioStats += "|" + (radioStatistiques.Items[i].Selected ? "Visible" : "Cache");
                }
            }

            ClCourtierModificationAffichage.GetInstance().Modifier("affichageRadioStats", "", affichageRadioStats);
            modification.Text = "Enregistrement réussi.";
        }

        protected void buttonValiderStats_Click(object sender, EventArgs e)
        {
            string affichageStats = "";

            for (int i = 0; i < radioAffichageStats.Items.Count; i++)
            {
                if (i == 0)
                {
                    affichageStats += (radioAffichageStats.Items[i].Selected ? "1" : "0");
                }
                else
                {
                    affichageStats += "/" + (radioAffichageStats.Items[i].Selected ? "1" : "0");
                }
            }

            ClCourtierModificationAffichage.GetInstance().Modifier("Statistique", "", affichageStats);
            modification.Text = "Enregistrement réussi.";
        }
    }
}