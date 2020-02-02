using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CS2013
{
    public partial class Affichage : System.Web.UI.Page
    {
        ClCompte m_compteConnecte = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "jquerry", "http://code.jquery.com/jquery-latest.js");

            m_compteConnecte = (ClCompte)Session["utilisateur"];

            if (m_compteConnecte == null)
            {
                Response.Redirect("~/Pages/FinSession.aspx");
            }
        }

        public void ButtonValider_Click(object sender, EventArgs e)
        {
            int nbErreurs = 0;
            
            if (nbErreurs == 0)
            {
                List<ClParametre> listParams = new List<ClParametre>();
                listParams.Add(new ClParametre(ClParametre.TypeParametre.MaxTempsEntrainement.ToString(), ""));

                foreach (ClParametre param in listParams)
                {
                    if (param.Nom == ClParametre.TypeParametre.DateFinBougeotte.ToString() || param.Nom == ClParametre.TypeParametre.DateDebutBougeotte.ToString())
                    {
                        if (param.Date != Convert.ToDateTime(Session[param.Nom]))
                        {
                            ClCourtierParametre.GetInstance().Modifier(param);
                            Session[param.Nom] = param.Date;
                        }
                    }
                    else if (param.Valeur != Int32.Parse(Session[param.Nom].ToString()))
                    {
                        ClCourtierParametre.GetInstance().Modifier(param);
                        Session[param.Nom] = param.Valeur;
                    }

                    if (param.Nom == ClParametre.TypeParametre.NotifiedAdminEmail.ToString())
                        ClCourtierParametre.GetInstance().Modifier(param);
                }

                labelErreur.ForeColor = System.Drawing.Color.Green;
                labelErreur.Text = "Modifications effectuées avec succès";
            }
            else
            {
                labelErreur.ForeColor = System.Drawing.Color.Red;
                labelErreur.Text = "Une ou plusieurs informations sont invalides";
            }

            Utilitaire.AffecterParametres(Session);
            ChargerPage();
        }

        private void ChargerPage()
        {
        }
    }
}