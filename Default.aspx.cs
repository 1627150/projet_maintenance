using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CS2013
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClCourtierModificationAffichage.GetInstance().Init();
            
            if (Session["utilisateur"] != null)
                Response.Redirect("~/Pages/Ajouter.aspx");

            ChargerPage();
        }

        private void ChargerPage()
        {
            labelMessageBienvenue.Text = Constantes.CONNEXION_MESSAGE_BIENVENUE;
            labelConnexion.Text = "Connexion";
            labelFieldSetConnexion.Text = Constantes.COMPTE;
            labelTitreUtilisateur.Text = "Identifiant : ";
            labelTitreMotDePasse.Text = Constantes.MOT_DE_PASSE;
            labelDescriptionNouveauCompte.Text = Constantes.CONNEXION_MESSAGE_NOUVEAU_COMPTE;
            linkNouveau.Text = Constantes.CONNEXION_LIEN_NOUVEAU_COMPTE + '.';
            boutonConnexion.Text = Constantes.CONNEXION;
            lostPassword.Text = "Mot de passe perdu ?";
        }

        protected void VerifierInfo(Object sender, EventArgs e)
        {
            ClCompte compte = new ClCompte(nomUtilisateur.Text, motDePasse.Text, "",
                ClCompte.TypeCompte.Invalide, TypeUser.Invalide, "", "", "");

            if (compte.tableauErreurs.Count == 0)
            {
                compte = ClCourtierCompte.GetInstance().ObtenirCompteAuthentifie(compte);
                if (compte != null)
                {
                    Session["utilisateur"] = compte;
                    ClCourtierCompte.GetInstance().UpdateLastLogin(compte.IdUtilisateur);

                    if (rememberMe.Checked)
                        SetCookie();

                    if (ClCourtierCompte.GetInstance().NeedUpdatedMeta(compte))
                        Response.Redirect("~/Pages/Compte.aspx?required=1");

                    Response.Redirect("~/Pages/Ajouter.aspx");
                }
                else
                    labelErreur.Text = Erreur.CONNECTION;
            }
            else
                labelErreur.Text = Erreur.CONNECTION;

            ChargerPage();
        }

        private void SetCookie()
        {
            Response.Cookies["UserSettings"]["User"] = (Session["utilisateur"] as ClCompte).IdUtilisateur;
            Response.Cookies["UserSettings"]["Password"] = (Session["utilisateur"] as ClCompte).MotDePasse;
            Response.Cookies["UserSettings"].Expires = DateTime.Now.AddDays(1d);
        }
    }
}