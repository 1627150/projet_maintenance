using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CS2013
{
    public partial class Photos : System.Web.UI.Page
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
            string cheminPhotos = Path.Combine(Request.PhysicalApplicationPath, "Images/Photos");
            labelTitre.Text = Constantes.PHOTO_TITRE;
            labelDescription.Text = Constantes.PHOTO_DESCRIPTION;

            //pikame.InnerHtml = ListeImagesHTML(Server.MapPath("/Images/Photos/"));
            pikame.InnerHtml = ListeImagesHTML(cheminPhotos);
        }

        private string ListeImagesHTML(string p_repertoire)
        {
            string sourceImage = "";
            StringWriter stringWriter = new StringWriter();
            
            using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
            {
                foreach (var fichier in Utilitaire.ListerImages(p_repertoire))
                {
                    //sourceImage = Utilitaire.RepertoireAbsoluARelatif(fichier, Server.MapPath("~/"));
                    sourceImage = Request.ApplicationPath + @"/Images/Photos/" + Path.GetFileName(fichier);

                    writer.RenderBeginTag(HtmlTextWriterTag.Li);
                    writer.AddAttribute(HtmlTextWriterAttribute.Src, sourceImage);
                    writer.RenderBeginTag(HtmlTextWriterTag.Img);
                    writer.RenderEndTag();
                    writer.RenderEndTag();
                }
            }
            
            return stringWriter.ToString();
        }
    }
}