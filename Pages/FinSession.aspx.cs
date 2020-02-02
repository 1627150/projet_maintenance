using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CS2013
{
    public partial class FinSession : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            labelDescription.Text = "Vous avez été déconnecté de l'application.";
            hyperLinkAccueil.NavigateUrl = "~/Default.aspx";
            hyperLinkAccueil.Text = "Retour à l'accueil";

            Response.AddHeader("REFRESH", String.Format("5;URL={0}", "/Default.aspx"));

        }
    }
}