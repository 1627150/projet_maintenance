using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CS2013
{
    public partial class Evenement : System.Web.UI.Page
    {
        ClCompte m_compteConnecte = null;

        protected void Page_Load(object sender, EventArgs e)
        {

            m_compteConnecte = (ClCompte)Session["utilisateur"];

            if (m_compteConnecte == null)
                Response.Redirect("~/Pages/FinSession.aspx");
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            List<ClEvenement> capsules = ClCourtierEvenement.GetInstance().ObtenirTous();

            labelTitre.Text = "Évenements";
    
            capsules.ForEach(capsule =>
            {
                maincontent.Controls.Add(new LinkButton { Text = capsule.Titre, ID = capsule.ID });

                string content = capsule.Contenu;

                while (content.IndexOf('[') != -1)
                {
                    int debut = content.IndexOf('[') + 1;
                    int fin = content.IndexOf(']');

                    string link = content.Substring(debut, fin - debut);
                    string builtLink = "<a class='hyperlink' href=";

                    if (!link.StartsWith("http"))
                        builtLink += "http://";

                    builtLink += link + ">" + link + "</a>";

                    content = content.Replace("[" + link + "]", builtLink);
                }

                Panel div = new Panel();
                div.Controls.Add(new Literal { Text = content });
                div.Style.Add(HtmlTextWriterStyle.PaddingBottom, "20px");
                maincontent.Controls.Add(div);
            });
        }
    }
}