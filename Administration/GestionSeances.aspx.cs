using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CS2013
{
    public partial class GestionSeances : System.Web.UI.Page
    {
        ClCompte m_compteConnecte = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            m_compteConnecte = (ClCompte)Session["utilisateur"];

            if (m_compteConnecte == null)
                Response.Redirect("~/Pages/FinSession.aspx");

            if (!Page.IsPostBack)
                Utilitaire.FillDropDownTypeConcentration(dropDownTypeConcentration);

            buttonResetAllData.OnClientClick = string.Format("return confirm('Vider les Crédits $anté?');");
            buttonResetData.OnClientClick = string.Format("return confirm('Vider les Crédits $anté?');");
        }

        public void buttonResetData_OnClick(object sender, EventArgs e)
        {
            bool success = true;
            if ((sender as Button).CommandArgument == "all")
                success = ClCourtierSeanceEntrainement.GetInstance().SupprimerTout();
            else if ((sender as Button).CommandArgument == "concentration")
                success = ClCourtierSeanceEntrainement.GetInstance().SupprimerSelonGroupe(dropDownTypeConcentration.SelectedValue);

            if (success)
            {
                labelConfirm.Text = "Supressions effectuées avec succès.";
                labelErreur.Text = String.Empty;
            }
            else
            {
                labelErreur.Text = "Aucune séances n'ont été supprimées.";
                labelConfirm.Text = String.Empty;
            }
        }
    }
}