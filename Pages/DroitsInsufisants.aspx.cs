using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CS2013
{
    public partial class DroitsInsufisants : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            labelDescription.Text = "Vos droits sont insufisants.";
            hyperLinkAccueil.NavigateUrl = "~/Default.aspx";
            hyperLinkAccueil.Text = "Retour à l'accueil";

            Response.AddHeader("REFRESH", String.Format("5;URL={0}", "~/Default.aspx"));
        }
    }
}