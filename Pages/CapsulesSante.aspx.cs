using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CS2013
{
    public partial class CapsulesSante : System.Web.UI.Page
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

            List<ClCapsuleSante> capsules = ClCourtierCapsuleSante.GetInstance().ObtenirTous();

            labelTitre.Text = "Bienvenue aux Capsules Santé";
            labelTexte.Text = "Cette page est un endroit où il y aura des mises à jour et vous pourrez aller voir des liens pour des articles et recherches dans le domaine de la santé. Vous pouvez les lire en cliquant sur ces liens.<br><br>" +
                              "Si vous êtes intéressés à recevoir ces lien via votre courriel, vous n’avez qu’à cliquez sur inscription pour que les futurs liens vous soient envoyés. Il est toujours possible de vous désinscrire.";
            capsules.ForEach(capsule =>
            {
                maincontent.Controls.Add(new LinkButton { Text = capsule.Titre, ID = capsule.ID });

                string content = capsule.Contenu;

                while (content.IndexOf('[') != -1)
                {
                    int debut = content.IndexOf('[') +1;
                    int fin = content.IndexOf(']');

                    string link = content.Substring(debut, fin-debut);
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