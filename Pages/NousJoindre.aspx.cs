using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CS2013
{
    public partial class NousJoindre : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ChargerPage();
            }
        }

        private void ChargerPage()
        {
            labelTitre.Text = HttpUtility.HtmlDecode(ClCourtierModificationAffichage.GetInstance().ObtenirTitre("nousJoindre")); // Constantes.CONTACT_TITRE;
            labelDescription.Text = HttpUtility.HtmlDecode(ClCourtierModificationAffichage.GetInstance().ObtenirContenu("nousJoindre")); // Constantes.CONTACT_DESCRIPTION;
        }
    }
}