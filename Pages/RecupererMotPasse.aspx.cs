using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CS2013
{
    public partial class RecupererMotPasse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                ChargerPage();
        }

        private void ChargerPage()
        {
            labelMessageBienvenue.Text = Constantes.RECUPERATION_MOT_PASSE;
            labelFieldSetConnexion.Text = Constantes.COMPTE;
            labelIdentifiant.Text = "Courriel : ";
            boutonEnvoyer.Text = Constantes.ENVOYER;
            retourAccueil.Text = Constantes.RETOUR_ACCUEIL;
        }

        protected void EnvoyerRecuperation(object sender, EventArgs e)
        {
            if (ClCourtierCompte.GetInstance().EmailExiste(txtCouriel.Text) && Regex.IsMatch(txtCouriel.Text.ToUpper(), Constantes.REGEX_EMAIL))
            {
                ClCompte compte = ClCourtierCompte.GetInstance().ObtenirCompteSelonCourriel(txtCouriel.Text);

                string nouveauMotPasse = System.Web.Security.Membership.GeneratePassword(10, 4);

                compte.MotDePasse = nouveauMotPasse;

                ClCourtierCompte.GetInstance().Modifier(compte);

                EnvoyerCourriel(compte.Email, nouveauMotPasse);

                labelErreur.Text = "Un nouveau mot de passe à été envoyé à l'adresse courriel saisie.";
            }
            else
            {
                labelErreur.Text = "Aucun compte trouvé avec cette adresse courriel.";
            }
        }

        public void EnvoyerCourriel(string p_courriel, string p_nouveauMotPasse)
        {
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = "outlook.cstjean.qc.ca";
            client.Port = 587;

            NetworkCredential credentials =
                new NetworkCredential("nepasrepondre@cstjean.qc.ca", "n123repondre");
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("nepasrepondre@cstjean.qc.ca");
            mailMessage.To.Add(new MailAddress(p_courriel));

            mailMessage.Subject = Constantes.COURRIEL_SUJET;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = String.Format(Constantes.COURRIEL_BODY + "<b>" + p_nouveauMotPasse + "</b>");

            client.Send(mailMessage);
        }
    }
}