using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CS2013
{
    public partial class Compte : System.Web.UI.Page
    {
        ClCompte m_compteConnecte = null;
        bool m_requiredInfo = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                m_compteConnecte = (ClCompte)Session["utilisateur"];

                if (m_compteConnecte == null)
                    Response.Redirect("~/Pages/FinSession.aspx");

                if (!IsPostBack)
                {
                    m_requiredInfo = Request.Url.ToString().EndsWith("required=1");
                    ChargerPage();
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/Pages/Ajouter.aspx");
            }
        }

        private void ChargerPage()
        {
            RemplirSetDépartementDropDownList();

            if (dropDownListDepartement.SelectedValue == m_compteConnecte.Concentration)
                dropSousConc.Visible = false;
            else
                RemplirSousConcDropDownList();

            //Text par défault des labels
            labelMessageModficationCompte.Text = Constantes.MODIFICATION_COMPTE;
            labelFieldSetModificationCompte.Text = Constantes.INFORMATIONS;
            labelTitreUtilisateur.Text = Constantes.UTILISATEUR;
            labelTitreAncienMotDePasse.Text = Constantes.ANCIEN_MDP;
            labelTitreNouveauMotDePasse.Text = Constantes.NOUVEAU_MOT_DE_PASSE;
            labelTitreConfirmationNouveauMotDePasse.Text = Constantes.CONFIRMATION_NOUVEAU_MOT_DE_PASSE;
            boutonModification.Text = Constantes.VALIDER;
            labelDépartement.Text = Constantes.CONCENTRATION;
            labelOldEmail.Text = "Courriel actuel : ";
            labelEmail.Text = "Nouvelle adresse courriel : ";
            labelConfirmmail.Text = "Confimez la nouvelle adresse courriel : ";

            labelNom.Text = "Nom et/ou Prénom : ";
            //Valeurs par défaults
            oldEmail.Text = m_compteConnecte.Email.Equals("null") ? String.Empty : m_compteConnecte.Email;
            nom.Text = m_compteConnecte.Nom.Equals("null") ? String.Empty : m_compteConnecte.Nom;
            nomUtilisateur.Text = m_compteConnecte.IdUtilisateur;

            if (m_requiredInfo)
                suppInfo.Text = "Vous pouvez désormais entrer votre nom ainsi que votre courriel.<br>Veuillez valider vos informations.";

            Utilitaire.RemplirComboFournisseur(dropFournisseur);

            if (!String.IsNullOrEmpty(m_compteConnecte.EmailTelephone))
            {
                string[] texto = m_compteConnecte.EmailTelephone.Split('@');
                numeroCell.Text = texto[0];

                dropFournisseur.Items.FindByValue("@" + texto[1]).Selected = true;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            try
            {
                base.OnInit(e);
            }
            catch (Exception)
            {
                Response.Redirect("~/Pages/Ajouter.aspx");
            }
        }

        private void Valider()
        {
            labelEmailErreur.Text = labelNomErreur.Text = String.Empty;
            bool désireModifierMDP = (ancienMotDePasse.Text != "" || confirmationNouveauMotDePasse.Text != "" || nouveaMotDePasse.Text != "");

            string numeroCellulaire = numeroCell.Text.Replace("-", String.Empty).Replace(" ", String.Empty).Replace("(", String.Empty).Replace(")", String.Empty) + dropFournisseur.SelectedValue;

            string id_concentration = String.Empty;

            if (dropSousConc.Visible)
                id_concentration = dropSousConc.SelectedValue;
            else
                id_concentration = dropDownListDepartement.SelectedValue;

            ClCompte compteModifié = new ClCompte(m_compteConnecte.IdUtilisateur, nouveaMotDePasse.Text,
                id_concentration, m_compteConnecte.Role, m_compteConnecte.Type, email.Text, nom.Text,
                numeroCellulaire);

            string motDePasseCrypté = String.Empty;

            //Si un des champs de mot de passe contient du text, les autres doivent en contenir et être validé aussi.
            if (désireModifierMDP)
            {
                //Validation du mot de passe
                if (compteModifié.MotDePasse.Length != 0)
                {
                    if (compteModifié.MotDePasse.Length < ClCompte.NB_CHARACTÈRE_MOT_DE_PASSE)
                        compteModifié.tableauErreurs.Add("labelMotDePasse", "Le mot de passe doit être minimum de " + ClCompte.NB_CHARACTÈRE_MOT_DE_PASSE);
                }
                else
                    compteModifié.tableauErreurs.Add("labelMotDePasse", "Le mot de passe est obligatoire");

                //Ancien mot de passe
                if (ancienMotDePasse.Text != "")
                {
                    motDePasseCrypté = FormsAuthentication.HashPasswordForStoringInConfigFile(ancienMotDePasse.Text, "MD5");

                    if (motDePasseCrypté != m_compteConnecte.MotDePasse)
                        compteModifié.tableauErreurs.Add("labelAncienMotDePasse", "Le mot de passe ne correspond pas à l'ancien mot de passe.");
                }
                else
                    labelAncienMotDePasse.Text = "L'ancien mot de passe ne peut pas être vide.";

                //La gestion du champ nouveau mot de passe ce fait dans la class ClCompte.

                //Confirmation mot de passe
                if (confirmationNouveauMotDePasse.Text.Length != 0)
                {
                    if (!nouveaMotDePasse.Text.Equals(confirmationNouveauMotDePasse.Text))
                        compteModifié.tableauErreurs.Add("labelConfirmationNouveauMotDePasse", Erreur.CONFIRMATION_MDP);
                }
                else
                    compteModifié.tableauErreurs.Add("labelConfirmationNouveauMotDePasse", Erreur.CONFIRMATION_MDP_MANQUANTE);
            }

            if (!String.IsNullOrEmpty(email.Text))
            {
                if (ClCourtierCompte.GetInstance().EmailExiste(email.Text))
                    compteModifié.tableauErreurs.Add("labelEmailErreur", "Cette adresse courriel est déjà utilisée.");
                else if (!Regex.IsMatch(email.Text.ToUpper(), Constantes.REGEX_EMAIL))
                    compteModifié.tableauErreurs.Add("labelEmailErreur", "Courriel invalide.");
                else if (!email.Text.ToUpper().Equals(emailConfirmation.Text.ToUpper()))
                    compteModifié.tableauErreurs.Add("labelErreurEmailConfirmation", "Le courriel de confirmation ne correspond pas.");
                else
                    compteModifié.Email = email.Text;
            }
            else
                compteModifié.Email = oldEmail.Text;

            if (!String.IsNullOrEmpty(numeroCell.Text))
            {
                long buffer = 0;
                string trimedNumber = numeroCell.Text.Replace("-", String.Empty).Replace(" ", String.Empty).Replace("(", String.Empty).Replace(")", String.Empty);
                if (!Int64.TryParse(trimedNumber, out buffer))
                    compteModifié.tableauErreurs.Add("labelNumeroCellErreur", "Numéro de téléphone invalide.");
            }

            //S'il n'y a pas d'erreur, on insère le compte dans la b. de d.
            if (compteModifié.tableauErreurs.Count == 0)
            {
                bool sqlExécutéSansErreur = false;

                if (désireModifierMDP)
                    sqlExécutéSansErreur = ClCourtierCompte.GetInstance().Modifier(compteModifié);
                else
                    sqlExécutéSansErreur = ClCourtierCompte.GetInstance().ModifierSansMotDePasse(compteModifié);

                if (sqlExécutéSansErreur)
                {
                    compteModifié.MotDePasse = motDePasseCrypté;
                    Session["Utilisateur"] = compteModifié;
                    m_compteConnecte = compteModifié;
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    ViderMessageErreur();
                    Utilitaire.RemplirLabelDErreurs(
                        compteModifié.tableauErreurs, formulaireEditionCompte);
                }
            }

            //Sinon, on remplis les Label des différents messages d'erreur contenu dans le tableau.
            else
            {
                ViderMessageErreur();
                Utilitaire.RemplirLabelDErreurs(
                    compteModifié.tableauErreurs, formulaireEditionCompte);
            }
        }

        protected void VerifierInfo(Object sender, EventArgs e)
        {
            Valider();
        }

        private void ViderMessageErreur()
        {
            labelAncienMotDePasse.Text = "";
            labelMotDePasse.Text = "";
            labelConfirmationNouveauMotDePasse.Text = "";
        }

        private void RemplirSetDépartementDropDownList()
        {
            List<ClConcentration> concentrations = ClCourtierConcentration.GetInstance().ObtenirTous();

            dropDownListDepartement.DataSource = concentrations.OrderBy(conc => conc.NomConcentration).ToList();
            dropDownListDepartement.DataBind();

            ClConcentration userConc = concentrations.Find(conc => conc.IdConcentration == m_compteConnecte.Concentration);
            if (userConc != null)
            {
                dropDownListDepartement.SelectedValue = m_compteConnecte.Concentration;
            }
        }

        private void RemplirSousConcDropDownList()
        {
            dropSousConc.Items.Clear();

            dropDownListDepartement.SelectedValue = ClCourtierSousConcentration.GetInstance().ObtenirObjetSelonId(m_compteConnecte.Concentration).IdParent;

            foreach (ClSousConcentration concentration in
                ClCourtierSousConcentration.GetInstance().ObtenirTousSelonParent(dropDownListDepartement.SelectedValue))
            {
                ListItem item = new ListItem(concentration.NomConcentration, concentration.IdConcentration);

                if (m_compteConnecte.Concentration == concentration.IdConcentration)
                {
                    item.Selected = true;
                    dropDownListDepartement.SelectedValue = concentration.IdParent;
                }

                dropSousConc.Items.Add(item);
            }
        }

        protected void dropDownListDepartement_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ClCourtierSousConcentration.GetInstance().ObtenirSelonNomParent(dropDownListDepartement.SelectedValue) != null)
            {
                dropSousConc.Visible = true;
                RemplirSousConcDropDownList();
            }
            else
            {
                dropSousConc.Visible = false;
            }
        }
    }
}