using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CS2013.Administration.Affichage
{
    public partial class GestionHistorique : System.Web.UI.Page
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
            Titre.Text = HttpUtility.HtmlDecode(ClCourtierModificationAffichage.GetInstance().ObtenirTitre("Historique"));
            Contenu.Text = HttpUtility.HtmlDecode(ClCourtierModificationAffichage.GetInstance().ObtenirContenu("Historique"));
        }

        protected void btnValider_Click(object sender, EventArgs e)
        {
            ClCourtierModificationAffichage.GetInstance().Modifier("Historique", HttpUtility.HtmlEncode(Titre.Text), HttpUtility.HtmlEncode(Contenu.Text));
            modification.Text = "Enregistrement réussi.";
        }
    }
}