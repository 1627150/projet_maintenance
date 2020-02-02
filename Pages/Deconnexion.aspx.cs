﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CS2013
{
    public partial class Deconnexion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Page.IsPostBack)
                ChargerPage();

            Session.RemoveAll();
            if (Request.Cookies["UserSettings"] != null)
            {
                HttpCookie myCookie = new HttpCookie("UserSettings");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
        }

        private void ChargerPage()
        {
            labelDescription.Text = Constantes.DECONNEXTION_DESCRIPTION;

            hyperLinkAccueil.NavigateUrl = "~/Default.aspx";
            hyperLinkAccueil.Text = "Retour à l'accueil";
        }
    }
}