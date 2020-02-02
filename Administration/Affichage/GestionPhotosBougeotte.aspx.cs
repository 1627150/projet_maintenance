using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Hosting;
using System.Drawing;

namespace CS2013.Administration
{
    public partial class GestionPhotosBougeotte : System.Web.UI.Page
    {
        ClCompte m_compteConnecte = null;
        List<string> m_listeImages = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            m_compteConnecte = (ClCompte)Session["utilisateur"];

            if (m_compteConnecte == null)
            {
                Response.Redirect("~/Pages/FinSession.aspx");
            }

            if (!IsPostBack)
            {
                ChargerPage();
            }
        }

        private void ChargerPage()
        {
            labelTitre.Text = Constantes.PHOTO_TITRE_MODIFIER;
            labelDescription.Text = Constantes.PHOTO_DESCRIPTION_MODIFIER;

            pikame.InnerHtml = ListeImagesHTML(Request.PhysicalApplicationPath + "/Images/Photos/");
        }

        public void ButtonValider_Click(object sender, EventArgs e)
        {
            int nbErreurs = 0;
            
            if (fileUpload.HasFiles)
            {
                foreach (HttpPostedFile fichier in fileUpload.PostedFiles)
                {
                    Exception etatTeleversement = Utilitaire.TeleverserImage(fichier, "Images/Photos/", null, false);

                    if (etatTeleversement == null)
                    {
                        labelStatus.ForeColor = Color.White;
                        labelStatus.BackColor = Color.LightGreen;
                        labelStatus.Text = "État du téléversement: Fichier téléversé avec succès!";
                    }
                    else
                    {
                        ++nbErreurs;
                        labelStatus.ForeColor = Color.White;
                        labelStatus.BackColor = Color.Red;
                        labelStatus.Text = 
                            "État du téléversement: Le fichier n'a pas pu être téléversé. L'erreur suivante est survenue: « " + 
                            etatTeleversement.Message + " »";
                        break;
                    }
                }
            }

            if (nbErreurs == 0)
            {
                labelErreur.ForeColor = System.Drawing.Color.Green;
                labelErreur.Text = "Modification effectuée avec succès";
                
                foreach (System.Collections.DictionaryEntry entry in HttpContext.Current.Cache)
                {
                    HttpContext.Current.Cache.Remove((string)entry.Key);
                }
            }
            else
            {
                labelErreur.ForeColor = System.Drawing.Color.Red;
                labelErreur.Text = "Un problème est survenu";
            }

            Utilitaire.AffecterParametres(Session);

            // TODO: Trouver un moyen propre d'afficher les messages d'erreurs. Rediriger la page sur elle-même est nécessaire
            ChargerPage();
            Response.Redirect(Request.RawUrl);
        }

        private string ListeImagesHTML(string p_repertoire)
        {
            StringWriter stringWriter = new StringWriter();

            using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
            {
                m_listeImages = Utilitaire.ListerImages(p_repertoire);

                int i = 0;
                foreach (var fichier in m_listeImages)
                {
                    string sourceImage = Request.ApplicationPath + @"Images/Photos/" + Path.GetFileName(fichier); 
                    string idImage = "image_" + i++;

                    writer.AddAttribute(HtmlTextWriterAttribute.Id, idImage);
                    writer.AddAttribute(HtmlTextWriterAttribute.Src, sourceImage);
                    writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "350px");
                    writer.RenderBeginTag(HtmlTextWriterTag.Img);
                    writer.RenderEndTag();
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "btn btn-alert btn-retirer");
                    writer.AddAttribute(HtmlTextWriterAttribute.Name, idImage);
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.InnerWriter.Write("Retirer");
                    writer.RenderEndTag();
                }
            }

            return stringWriter.ToString();
        }

        [WebMethod]
        public static void SupprimerPhoto(string nomFichier)
        {
            try
            {
                File.Delete(Path.Combine(@HostingEnvironment.ApplicationPhysicalPath, nomFichier));
            }
            catch (Exception e)
            {
                // Possibilité d'écrire un message personnalisé dans un log
            }
        }
    }
}