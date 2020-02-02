using CS2013.App_Code.ObjetsAffaires;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CS2013.Administration
{
    public partial class GestionOnglets : System.Web.UI.Page
    {
        ClCompte m_compteConnecte = null;

        protected void Page_Load(object sender, EventArgs e)
        {

            m_compteConnecte = (ClCompte)Session["utilisateur"];

            if (m_compteConnecte == null)
                Response.Redirect("~/Pages/FinSession.aspx");

        }

        protected void Load_Activ(object sender, EventArgs e)
        {
            //SqlConnection thisConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DonneesCreditsSante"].ConnectionString);

            List<ClOnglets>  onglets = ClCourtierOnglets.ObtenirTout();
            int i = 1;

            foreach(ClOnglets onglet in onglets)
            {
                switch (onglet.titre)
                {
                    case "Capsules Santés":
                        if (onglet.valid)
                        {
                          onglet1.Selected = true;
                        }
                        break;
                     case "Statistiques":
                        if (onglet.valid)
                        {
                           onglet2.Selected = true;
                        }
                        break;
                    case "Historique":
                        if (onglet.valid)
                        {
                            onglet3.Selected = true;
                        }
                        break;
                    case "Photos":
                        if (onglet.valid)
                        {
                            onglet4.Selected = true;
                        }
                        break;
                    case "Nous joindre":
                        if (onglet.valid)
                        {
                            onglet5.Selected = true;
                        }
                        break;
                    case "Evenements":
                        if (onglet.valid)
                        {
                            onglet6.Selected = true;
                        }
                        break;
                }
                
                i++;
            }
        }


        public void Change_Value(object sender, EventArgs e)
        {
            bool isValid = false;
            List<ListItem> ch = new List<ListItem>();
            ch.Add(onglet1);
            ch.Add(onglet2);
            ch.Add(onglet3);
            ch.Add(onglet4);
            ch.Add(onglet5);
            ch.Add(onglet6);

            foreach (ListItem titre in ch)
            {
                ListItem c = titre;
                switch (c.Text)
                {
                    case "Capsules Santés":
                        isValid = true;
                        break;
                    case "Statistiques":
                        isValid = true;
                        break;
                    case "Historique":
                        isValid = true;
                        break;
                    case "Photos":
                        isValid = true;
                        break;
                    case "Nous joindre":
                        isValid = true;
                        break;
                    case "Evenements":
                        isValid = true;
                        break;
                }
                ClCourtierOnglets.Modifier(c.Selected && isValid, c.Text);
                modification.Text = "Enregistrement réussi.";
            }
            Load_Activ(sender, e);
        }
    }
}