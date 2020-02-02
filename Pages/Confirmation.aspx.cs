using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CS2013
{
    public partial class Confirmation : System.Web.UI.Page
    {
        ClCompte m_compteConnecte = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            m_compteConnecte = (ClCompte)Session["utilisateur"];

            if (m_compteConnecte == null)
                Response.Redirect("~/Pages/FinSession.aspx");

            if (!Page.IsPostBack)
                ChargerPage();

            List<ClSeanceEntrainement> seances =
                ClCourtierSeanceEntrainement.GetInstance().ObtenirSeancesSelonIdUtilisateur(
                    m_compteConnecte.IdUtilisateur);

            double nbCreditsTotal = 0.0;

            foreach (ClSeanceEntrainement seance in seances)
                nbCreditsTotal += seance.NbCredits;

            labelTotalCredits.Text = Utilitaire.CreditsEnTexte(nbCreditsTotal) + " $";

            Response.AddHeader("REFRESH", "3;URL=Ajouter.aspx");
        }

        private void ChargerPage()
        {
            labelTitre.Text = Constantes.CONFIRMATION_TITRE;
            labelDescription.Text = Constantes.CONFIRMATION_DESCRIPTION;
        }
    }
}