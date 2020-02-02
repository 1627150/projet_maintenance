using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CS2013
{
    public partial class CreerCompte : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                ListItem item = new ListItem("Choisir...", "");
                typeUtilisateur.Items.Add(item);

                Utilitaire.FillDropDownTypeUser(typeUtilisateur);
                ChargerPage();
            }
        }

        private void ChargerPage()
        {
            labelEmail.Text = "Adresse courriel : ";
            labelConfirmmail.Text = "Confimez le courriel : ";
            labelNom.Text = "Nom : ";
            labelTypeUtilisateur.Text = "Type d'utilisateur* : ";
            labelTitreUtilisateur.Text = "Identifiant* :";
            labelTitreSousDepartement.Text = "Sous-département* : ";
            labelInfo.Text = "Les champs marqués d'un astérisque (*) sont obligatoires.";
            labelTitreMotDePasse.Text = String.Format(Constantes.MOT_DE_PASSE_AVEC_MINIMUM, ClCompte.NB_CHARACTÈRE_MOT_DE_PASSE);
            labelTitreConfirmerMDP.Text = Constantes.INSCRIPTION_CONFIRMER_MOT_DE_PASSE;
            labelTitreDepartement.Text = Constantes.CONCENTRATION;

            nomUtilisateur.Enabled = motDePasse.Enabled = confirmationMotDePasse.Enabled = DropDownDepartements.Enabled = false;

            Utilitaire.RemplirComboFournisseur(dropFournisseur);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        public void typeUtilisateur_SelectedIndexChanged(object sender, EventArgs args)
        {
            if (typeUtilisateur.SelectedIndex == 0)
                nomUtilisateur.Enabled = motDePasse.Enabled = confirmationMotDePasse.Enabled = DropDownDepartements.Enabled = false;
            else
            {
                MiseAJourID();
                RemplirComboDepartements(Utilitaire.GetTypeUser(Convert.ToInt32(typeUtilisateur.SelectedValue)));

                if (Convert.ToInt32(typeUtilisateur.SelectedValue) > 2)
                {
                    DropDownSousDepartements.Visible = labelTitreSousDepartement.Visible = true;
                    RemplirComboSousDepartements(DropDownDepartements.SelectedValue);
                }
                else
                {
                    DropDownSousDepartements.ClearSelection();
                    DropDownSousDepartements.SelectedIndex = -1;
                    DropDownSousDepartements.Items.Clear();
                    DropDownSousDepartements.Visible = labelTitreSousDepartement.Visible = false;
                }
            }
        }

        public void departement_SelectedIndexChanged(object sender, EventArgs args)
        {
            RemplirComboSousDepartements(DropDownDepartements.SelectedValue);
        }

        private void MiseAJourID()
        {
            string requirements = String.Empty;
            switch (typeUtilisateur.SelectedIndex)
            {
                case 1: requirements = Constantes.UTILISATEUR_ETU_CEGEP; break;
                case 2: requirements = Constantes.UTILISATEUR_PER_CEGEP; break;
                case 3: requirements = Constantes.UTILISATEUR_EXT_ECO; break;
                case 4: requirements = Constantes.UTILISATEUR_EXT_PART; break;
                case 5: requirements = Constantes.UTILISATEUR_EXT_PART; break;
            }

            labelTitreUtilisateur.Text = String.Format(Constantes.UTILISATEUR_CONDITIONNEL, requirements);
            nomUtilisateur.Enabled = motDePasse.Enabled = confirmationMotDePasse.Enabled = DropDownDepartements.Enabled = true;
        }

        private void RemplirComboSousDepartements(string idConcentration)
        {
            DropDownSousDepartements.Items.Clear();
            ListItem item = new ListItem("Choisir...", "");
            DropDownSousDepartements.Items.Add(item);

            foreach (ClSousConcentration concentration in
                ClCourtierSousConcentration.GetInstance().ObtenirTousSelonParent(idConcentration))
            {
                item = new ListItem(concentration.NomConcentration, concentration.IdConcentration.ToString());

                DropDownSousDepartements.Items.Add(item);
            }
        }

        private void RemplirComboDepartements(TypeUser typeUser)
        {
            DropDownDepartements.Items.Clear();

            ListItem item = new ListItem("Choisir...", "");
            DropDownDepartements.Items.Add(item);

            ClCourtierConcentration.GetInstance().ObtenirTousSelonTypeUser(typeUser).ForEach(concentration =>
                {
                    item = new ListItem(concentration.NomConcentration, concentration.IdConcentration.ToString());
                    DropDownDepartements.Items.Add(item);
                });
        }

        protected void CréerCompte(Object sender, EventArgs e)
        {
            requiredFields.Text = "";
            string numeroCellulaire = numeroCell.Text.Replace("-", String.Empty).Replace(" ", String.Empty).Replace("(", String.Empty).Replace(")", String.Empty) + dropFournisseur.SelectedValue;

            ClCompte compte = null;

            try
            {
                //Création de l'objet d'affaire
                compte = new ClCompte(
                     nomUtilisateur.Text,
                     motDePasse.Text,
                     DropDownDepartements.SelectedItem.Value.ToString(),
                     ClCompte.TypeCompte.Utilisateur,
                     Utilitaire.GetTypeUser(Convert.ToInt32(typeUtilisateur.SelectedItem.Value.ToString())),
                     email.Text,
                     nom.Text,
                     numeroCellulaire);
            }
            catch
            {
                //compte.tableauErreurs.Add("requiredFields", "Veuillez remplir tous les champs requis.");
                requiredFields.Text = "Veuillez remplir tous les champs requis.";
            }

            if (compte != null)
            {
                if (!String.IsNullOrEmpty(DropDownDepartements.SelectedValue))
                    compte.Concentration = DropDownDepartements.SelectedValue;
                else
                    compte.tableauErreurs.Add("labelDepartement", "Choisissez un groupe d'appartenance");

                if (DropDownSousDepartements.Visible && !String.IsNullOrEmpty(DropDownSousDepartements.SelectedValue))
                    compte.Concentration = DropDownSousDepartements.SelectedValue;
                else if (DropDownSousDepartements.Visible && String.IsNullOrEmpty(DropDownSousDepartements.SelectedValue))
                    compte.tableauErreurs.Add("labelSousDepartement", "Choisissez un sous-groupe");

                if (String.IsNullOrEmpty(typeUtilisateur.SelectedValue))
                    compte.tableauErreurs.Add("labelTypeUtilisateurErreur", "Choisissez un type d'utilisateur vous correspondant");

                //Validation du nom d'utilisateur
                if (compte.IdUtilisateur.Length != 0)
                {
                    if (!Regex.IsMatch(nomUtilisateur.Text, GetRegexForTypeUser()))
                    {
                        if (Utilitaire.GetTypeUser(Convert.ToInt32(typeUtilisateur.SelectedValue)) == TypeUser.EtudiantExterne)
                            compte.tableauErreurs.Add("labelNomUtilisateur", "Doit comporter 4 lettres suivies de 8 chiffres.");
                        else
                            compte.tableauErreurs.Add("labelNomUtilisateur", "Le nom d'utilisateur est invalide.");
                    }
                }
                else
                    compte.tableauErreurs.Add("labelNomUtilisateur", "L'identifiant est obligatoire.");

                //Validation du mot de passe
                if (compte.MotDePasse.Length != 0)
                {
                    if (compte.MotDePasse.Length < ClCompte.NB_CHARACTÈRE_MOT_DE_PASSE)
                    {
                        compte.tableauErreurs.Add("labelMotDePasse", String.Format("Le mot de passe doit contenir au minimum {0} caractères.", ClCompte.NB_CHARACTÈRE_MOT_DE_PASSE));
                    }
                }
                else
                {
                    compte.tableauErreurs.Add("labelMotDePasse", "Le mot de passe est obligatoire.");
                }

                if (!compte.tableauErreurs.ContainsValue("labelNomUtilisateur")
                    && ClCourtierCompte.GetInstance().ObtenirCompteSelonId(compte.IdUtilisateur) != null)
                {
                    compte.tableauErreurs.Add("labelNomUtilisateur", Erreur.UTILISATEUR_EXISTE);
                }

                if (!String.IsNullOrEmpty(email.Text))
                {
                    if (ClCourtierCompte.GetInstance().EmailExiste(email.Text))
                        compte.tableauErreurs.Add("labelErreurEmail", "Cette adresse courriel est déjà utilisée.");

                    if (!Regex.IsMatch(email.Text.ToUpper(), Constantes.REGEX_EMAIL))
                        compte.tableauErreurs.Add("labelErreurEmail", "Courriel invalide.");

                    if (!email.Text.ToUpper().Equals(emailConfirmation.Text.ToUpper()))
                        compte.tableauErreurs.Add("labelErreurEmailConfirmation", "Le courriel de confirmation ne correspond pas.");
                }

                if (confirmationMotDePasse.Text.Length != 0)
                {
                    if (!motDePasse.Text.Equals(confirmationMotDePasse.Text))
                    {
                        compte.tableauErreurs.Add("labelConfirmationMotDePasse", Erreur.CONFIRMATION_MDP);
                    }
                }
                else
                {
                    compte.tableauErreurs.Add("labelConfirmationMotDePasse", Erreur.CONFIRMATION_MDP_MANQUANTE);
                }

                if (!String.IsNullOrEmpty(numeroCell.Text))
                {
                    long buffer = 0;
                    string trimedNumber = numeroCell.Text.Replace("-", String.Empty).Replace(" ", String.Empty).Replace("(", String.Empty).Replace(")", String.Empty);
                    if (!Int64.TryParse(trimedNumber, out buffer))
                        compte.tableauErreurs.Add("labelNumeroCellErreur", "Numéro de téléphone invalide.");
                }

                //S'il n'y a pas d'erreur, on insère le compte dans la b. de d.
                if (compte.tableauErreurs.Count == 0)
                {
                    if (ClCourtierCompte.GetInstance().Insérer(ref compte))
                    {
                        Session["utilisateur"] = compte;
                        Response.Redirect("~/Pages/Ajouter.aspx");
                    }
                    else
                    {
                        ViderMessageErreur();
                        Utilitaire.RemplirLabelDErreurs(
                            compte.tableauErreurs, formulaireCréerCompte);
                    }
                }

                //Sinon, on remplis les Label des différents messages d'erreur contenu dans le tableau.
                else
                {
                    ViderMessageErreur();
                    Utilitaire.RemplirLabelDErreurs(
                        compte.tableauErreurs, formulaireCréerCompte);
                }
            }
        }

        private string GetRegexForTypeUser()
        {
            switch (Utilitaire.GetTypeUser(Convert.ToInt32(typeUtilisateur.SelectedValue)))
            {
                case TypeUser.CegepEmploye: return Constantes.REGEX_PER_CEGEP;
                case TypeUser.CegepEtudiant: return Constantes.REGEX_ETU_CEGEP;
                case TypeUser.EtudiantExterne: return Constantes.REGEX_EXT_ECO;
                case TypeUser.Communaute:
                case TypeUser.Partenaire:
                    return Constantes.REGEX_EXT_PART;
                default: return String.Empty;
            }
        }

        private void ViderMessageErreur()
        {
            labelNomUtilisateur.Text = "";
            labelMotDePasse.Text = "";
            labelConfirmationMotDePasse.Text = "";
        }
    }
}