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
    public partial class Parametres : System.Web.UI.Page
    {
        ClCompte m_compteConnecte = null;

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

        public void ButtonValider_Click(object sender, EventArgs e)
        {
            int nbErreurs = 0;

            int nbMaxCreditSemaine = 0;
            if (!Int32.TryParse(txtCreditsMaxSemaine.Text, out nbMaxCreditSemaine))
            {
                ++nbErreurs;
                txtCreditsMaxSemaine.Focus();
            }

            int nbMaxTempsEntrainement = 0;
            if (!Int32.TryParse(txtTempsMaxSession.Text, out nbMaxTempsEntrainement))
            {
                ++nbErreurs;
                txtTempsMaxSession.Focus();
            }

            int nbMaxCreditsEntrainement = 0;
            if (!Int32.TryParse(txtCreditsMaxSeance.Text, out nbMaxCreditsEntrainement))
            {
                ++nbErreurs;
                txtCreditsMaxSeance.Focus();
            }

            int nbMaxCreditsNotification = 0;
            if (!Int32.TryParse(txtTempsPourNotification.Text, out nbMaxCreditsNotification))
            {
                ++nbErreurs;
                txtTempsPourNotification.Focus();
            }

            int nbMaxSeanceJour = 0;
            if (!Int32.TryParse(txtMaxSeanceJour.Text, out nbMaxSeanceJour))
            {
                ++nbErreurs;
                txtMaxSeanceJour.Focus();
            }

            if (fileUploadAffiche.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName(fileUploadAffiche.FileName);
                    if (!Utilitaire.EstFichierJpeg(filename))
                    {
                        throw new NotSupportedException("Seuls les fichiers JPEG sont autorisés comme image d'accueil.");
                    }
                    fileUploadAffiche.SaveAs(Request.PhysicalApplicationPath + "/images/AfficheAccueil.jpg");
                    labelStatus.ForeColor = Color.White;
                    labelStatus.BackColor = Color.LightGreen;
                    labelStatus.Text = "État du téléversement: Fichier téléversé avec succès!";
                }
                catch (Exception ex)
                {
                    labelStatus.ForeColor = Color.White;
                    labelStatus.BackColor = Color.Red;
                    labelStatus.Text = "État du téléversement: Le fichier n'a pas pu être téléversé. L'erreur suivante est survenue: « " + ex.Message + " »";
                }
            }

            DateTime date = new DateTime();
            if (!DateTime.TryParse(txtDebutBougeotte.Text, out date))
            {
                ++nbErreurs;
                txtDebutBougeotte.Focus();
            }

            if (!DateTime.TryParse(txtDateFinBougeotte.Text, out date))
            {
                ++nbErreurs;
                txtDateFinBougeotte.Focus();
            }

            foreach (string email in txtNotification.Text.Split(';'))
            {
                if (!Regex.IsMatch(email.ToUpper(), @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$"))
                {
                    ++nbErreurs;
                    txtNotification.Focus();
                }
            }

            if (nbErreurs == 0)
            {
                List<ClParametre> listParams = new List<ClParametre>();
                listParams.Add(new ClParametre(ClParametre.TypeParametre.MaxTempsEntrainement.ToString(), nbMaxTempsEntrainement));
                listParams.Add(new ClParametre(ClParametre.TypeParametre.MaxTempsNotification.ToString(), nbMaxCreditsNotification));
                listParams.Add(new ClParametre(ClParametre.TypeParametre.MaxCreditsSemaine.ToString(), nbMaxCreditSemaine));
                listParams.Add(new ClParametre(ClParametre.TypeParametre.MaxSeancesJour.ToString(), nbMaxSeanceJour));
                listParams.Add(new ClParametre(ClParametre.TypeParametre.NotifiedAdminEmail.ToString(), txtNotification.Text));
                listParams.Add(new ClParametre(ClParametre.TypeParametre.DateDebutBougeotte.ToString(), Convert.ToDateTime(txtDebutBougeotte.Text)));
                listParams.Add(new ClParametre(ClParametre.TypeParametre.DateFinBougeotte.ToString(), Convert.ToDateTime(txtDateFinBougeotte.Text)));
                listParams.Add(new ClParametre(ClParametre.TypeParametre.NotifiedAdminEmail.ToString(), txtNotification.Text));
                listParams.Add(new ClParametre(ClParametre.TypeParametre.UtilisationInviteAmi.ToString(), inviteFriend.Checked ? 1 : 0));

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
            // TODO: Insérer les textes hardcodés dans la base de données commes les autres champs

            labelCreditsMaxSemaine.Text = Constantes.NB_MAX_CREDITS_SEMAINE;
            labelCreditsMaxSeance.Text = Constantes.NB_MAX_CREDITS_SEANCE;
            labelTempsMaxSession.Text = Constantes.NB_MAX_MINUTES_SEANCE;
            labelTempsPourNotification.Text = Constantes.NB_MIN_MINUTES_SUSPECT;
            labelMaxSeanceJour.Text = Constantes.NB_MAX_SEANCES_JOUR;
            labelInviteFriend.Text = "Permettre d'inviter des amis à rejoindre l'application Crédits $anté : ";
            labelNotification.Text = "Notifications envoyées à : ";
            notificationInfo.Text = "(Vous pouvez entrer plusieurs adresses en les séparrant par un point-virgule)";
            labelDebutBougeotte.Text = "Début de la Bougeotte : ";
            labelFinBougeotte.Text = "Fin de la Bougeotte : ";

            inviteFriend.Checked = Convert.ToInt32(Session[ClParametre.TypeParametre.UtilisationInviteAmi.ToString()]) == 1;
            txtCreditsMaxSemaine.Text = Session[ClParametre.TypeParametre.MaxCreditsSemaine.ToString()].ToString();
            txtCreditsMaxSeance.Text = Session[ClParametre.TypeParametre.MaxCreditsSeance.ToString()].ToString();
            txtTempsMaxSession.Text = Session[ClParametre.TypeParametre.MaxTempsEntrainement.ToString()].ToString();
            txtTempsPourNotification.Text = Session[ClParametre.TypeParametre.MaxTempsNotification.ToString()].ToString();
            labelChangerAffiche.Text = "Changer l'affiche d'accueil: ";
            txtNotification.Text = ClCourtierParametre.GetInstance().ObtenirSelonNom(ClParametre.TypeParametre.NotifiedAdminEmail.ToString()).Value;
            txtMaxSeanceJour.Text = Session[ClParametre.TypeParametre.MaxSeancesJour.ToString()].ToString();
            calFinBougeotte.SelectedDate = Convert.ToDateTime(Session[ClParametre.TypeParametre.DateFinBougeotte.ToString()]);
            calDebutBougeotte.SelectedDate = Convert.ToDateTime(Session[ClParametre.TypeParametre.DateDebutBougeotte.ToString()]);
        }
    }
}