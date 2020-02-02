using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CS2013
{
    public partial class Historique : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Page.IsPostBack)
                ChargerPage();
        }

        private void ChargerPage()
        {
            labelTitre.Text = HttpUtility.HtmlDecode(ClCourtierModificationAffichage.GetInstance().ObtenirTitre("Historique")); //Constantes.HISTORIQUE_TITRE;
            labelDescriptionHistorique.Text = HttpUtility.HtmlDecode(ClCourtierModificationAffichage.GetInstance().ObtenirContenu("Historique")); //Constantes.HISTOIRE_DESCRIPTION;
        }
    }
}