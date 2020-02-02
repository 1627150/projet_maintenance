using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CS2013
{
    public partial class Ajouter : System.Web.UI.Page
    {
        ClCompte m_compteConnecte = null;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            m_compteConnecte = (ClCompte)Session["utilisateur"];

            if (m_compteConnecte == null)
                Response.Redirect("~/Pages/FinSession.aspx");

            if (!IsPostBack)
                ChargerPage();
        }

        private void ChargerPage()
        {
            labelMessageTotalCredits.Text = Constantes.AJOUT_MESSAGE_TOTAL_CREDITS;
            labelMessageTotalBougeotte.Text = "Votre objectif santé : 10 $ ou plus par semaine";
            labelMessageObjectifConcentration.Text = String.Format(Constantes.AJOUT_MESSAGE_OBJECTIF_CREDITS, DeterminerTypeConcentration());
            //labelMessageObjectifPersonnel.Text = "Objectif personnel : ";
            labelFieldSetAjout.Text = Constantes.AJOUT_FIELDSET;
            labelTitre.Text = Constantes.AJOUT_TITRE;
            labelDefinition.Visible = false;

            //labelActivite.Text = Constantes.ACTIVITE;
            labelTempsMinutes.Text = Constantes.AJOUT_TEMPS + " : ";
            //labelPremierement.Text = "Premièrement entrez le temps effectif d’activité physique en minutes.";
            //labelOu.Text = "Ou";
            labelNbCredits.Text = "Nombre de <i>Crédit $anté<sup>®</sup></i> : ";
            labelDate.Text = "Date : ";

            //calendar.SelectedDate = DateTime.Now;

            txtDate.Text = DateTime.Now.ToShortDateString();

            /*
            if (Int32.Parse(Session["UtilisationActivite"].ToString()) != 0)
                RemplirComboActivitées();
            else
            {
                labelActivite.Visible = false;
                DropDownActivitées.Visible = false;
            }*/
            ClCapsuleSante capsule = ClCourtierCapsuleSante.GetInstance().ObtenirPlusRecente();
            if (String.IsNullOrEmpty(capsule.Titre) || String.IsNullOrEmpty(capsule.Titre))
            {
                tableCapsule.Visible = false;
            }
            else
            {
                linkCapsule.Text = "<i>Capsule Santé</i> : " + capsule.Titre;
                linkCapsule.NavigateUrl = "~/Pages/CapsulesSante.aspx#MainContent_" + capsule.ID;
            }
            AfficherCredits();

            MettreAJourDescriptionRadio(new object(), new EventArgs());
        }

        private void EffecerMessagesErreur()
        {
            labelTemps.Text = labelIntensiteErreur.Text = labelDateErreur.Text = String.Empty;
        }

        private string DeterminerTypeConcentration()
        {
            ClConcentration concentration = ClCourtierConcentration.GetInstance().ObtenirObjetSelonId(m_compteConnecte.Concentration);
            if (concentration != null)
                return ClConcentration.GetConcentrationTexte(concentration.Concentration);
            else
            {
                ClSousConcentration sousConc = ClCourtierSousConcentration.GetInstance().ObtenirObjetSelonId(m_compteConnecte.Concentration);
                return ClSousConcentration.GetConcentrationTexte(sousConc.Concentration);
            }
        }

        private void AfficherCredits()
        {
            List<ClSeanceEntrainement> seances =
                ClCourtierSeanceEntrainement.GetInstance().ObtenirSeancesSelonIdUtilisateur(
                    m_compteConnecte.IdUtilisateur);

            double nbCreditsTotal = 0.0;

            foreach (ClSeanceEntrainement seance in seances)
            {
                nbCreditsTotal += seance.NbCredits;
            }

            labelTotalCredits.Text = Utilitaire.CreditsEnTexte(nbCreditsTotal) + " $";

            double totalCreditBougeotteSelonConcentration = 0.0;
            string id_concentration = String.Empty;
            if (ClCourtierConcentration.GetInstance().ObtenirObjetSelonId(m_compteConnecte.Concentration) != null)
            {
                id_concentration = m_compteConnecte.Concentration;
                totalCreditBougeotteSelonConcentration = ClCourtierSeanceEntrainement.GetInstance().ObtenirTotalBougeotteSelonConcentration(m_compteConnecte.IdUtilisateur);
            }
            else
            {
                id_concentration = ClCourtierSousConcentration.GetInstance().ObtenirObjetSelonId(m_compteConnecte.Concentration).IdParent;
                totalCreditBougeotteSelonConcentration = ClCourtierSeanceEntrainement.GetInstance().ObtenirTotalBougeotteSelonConcentrationEtIdUser(m_compteConnecte.IdUtilisateur);
            }

            double objectifConcentration = ClCourtierConcentration.GetInstance().ObtenirObectifSelonId(id_concentration);

            if (objectifConcentration != 0)
            {
                labelObjectifConcentration.Text =
                    Utilitaire.CreditsEnTexte(totalCreditBougeotteSelonConcentration) +
                    "/";

                ClConcentration conc;
                ClSousConcentration sousConc;
                if ((conc = ClCourtierConcentration.GetInstance().ObtenirObjetSelonId(m_compteConnecte.Concentration)) != null)
                    labelObjectifConcentration.Text += Utilitaire.CreditsEnTexte(conc.ObjectifCredits) + " $";
                else if ((sousConc = ClCourtierSousConcentration.GetInstance().ObtenirObjetSelonId(m_compteConnecte.Concentration)) != null)
                {
                    conc = ClCourtierConcentration.GetInstance().ObtenirObjetSelonId(sousConc.IdParent);
                    labelObjectifConcentration.Text += Utilitaire.CreditsEnTexte(conc.ObjectifCredits) + " $";
                }
            }
            else
            {
                labelMessageObjectifConcentration.Text = "";
                labelObjectifConcentration.Text = "";
                labelMessageObjectifConcentration.Visible = false;
                labelObjectifConcentration.Visible = false;
            }

            int nbUtilisateurs = ClCourtierCompte.GetInstance().NbCompteParConcentration(m_compteConnecte.Concentration);
        }

        protected void ValiderCredits(Object sender, EventArgs e)
        {
            EffecerMessagesErreur();
            ClSeanceEntrainement seanceCourante = new ClSeanceEntrainement();

            DateTime date = DateTime.Now;
            if (!String.IsNullOrEmpty(txtDate.Text))
            {
                date = Convert.ToDateTime(txtDate.Text);
                if (date > DateTime.Today)
                    seanceCourante.tableauErreurs.Add("labelDateErreur", "La date de l'entrainement ne peut être supérieure à la date d'aujourd'hui.");
                else
                    seanceCourante.DateActivite = date;
            }
            else
                seanceCourante.tableauErreurs.Add("labelDateErreur", "La date est obligatoire.");

            int intensiteChoisie = 0;
            if (seanceCourante.tableauErreurs.Count == 0)
            {
                TypeConcentration typeConc;
                ClConcentration c;
                if ((c = ClCourtierConcentration.GetInstance().ObtenirObjetSelonId(m_compteConnecte.Concentration)) != null)
                    typeConc = c.Concentration;
                else
                    typeConc = ClCourtierSousConcentration.GetInstance().ObtenirObjetSelonId(m_compteConnecte.Concentration).Concentration;

                bool estEntraineur = m_compteConnecte.Role == ClCompte.TypeCompte.Entraineur || m_compteConnecte.Role == ClCompte.TypeCompte.Administrateur
                       || typeConc == TypeConcentration.Partenaire;

                if (radioIntensite.SelectedIndex == -1 && !String.IsNullOrEmpty(textMinutes.Text))
                    seanceCourante.tableauErreurs.Add("labelIntensiteErreur", "Veuillez choisir une intensité.");
                else if (radioIntensite.SelectedIndex != -1 && String.IsNullOrEmpty(textMinutes.Text))
                    seanceCourante.tableauErreurs.Add("labelTemps", "Veuillez entrer une durée.");
                else if (radioIntensite.SelectedIndex == -1 && String.IsNullOrEmpty(textMinutes.Text) && String.IsNullOrEmpty(textNbCredits.Text))
                    seanceCourante.tableauErreurs.Add("labelTemps", "Veuillez utiliser l'un des deux outils.");
                else if (radioIntensite.SelectedIndex != -1 && !String.IsNullOrEmpty(textMinutes.Text))
                {
                    intensiteChoisie = Int32.Parse(radioIntensite.SelectedValue);

                    int nbMinutes = 0;

                    if (textMinutes.Text.Length != 0 && textNbCredits.Text.Length != 0)
                        seanceCourante.tableauErreurs.Add("labelTemps", Erreur.CHAMPS_INVALIDE);
                    else if (Int32.TryParse(textMinutes.Text, out nbMinutes) && nbMinutes > 0)
                    {
                        if (estEntraineur || EstNbMinutesValide(seanceCourante, nbMinutes))
                        {
                            double nbCredits = CalculerCredits(intensiteChoisie, nbMinutes);
                            bool estQuotaHebdoAtteint = ClCourtierSeanceEntrainement.GetInstance().EstQuotaHebdomadaireAtteint(m_compteConnecte.IdUtilisateur, nbCredits);
                            bool estQuotaQuotAtteint = ClCourtierSeanceEntrainement.GetInstance().EstQuotaQuotidienAtteint(m_compteConnecte.IdUtilisateur, nbCredits);

                            if (estEntraineur || (!estQuotaHebdoAtteint && !estQuotaQuotAtteint))
                            {
                                bool valide = (nbMinutes < Convert.ToInt32(Session[ClParametre.TypeParametre.MaxTempsNotification.ToString()]));
                                seanceCourante = new ClSeanceEntrainement(Int32.Parse(radioIntensite.SelectedValue),
                                    nbMinutes, nbCredits, m_compteConnecte.IdUtilisateur,
                                        String.Empty, estEntraineur ? true : valide);

                                seanceCourante.DateActivite = date;
                            }
                            else
                            {
                                if (estQuotaHebdoAtteint)
                                    seanceCourante.tableauErreurs.Add("labelErreurMaximumAtteint",
                                     Erreur.MAX_CREDIT_ATTEINT + Session[ClParametre.TypeParametre.MaxCreditsSemaine.ToString()].ToString() + ".");

                                if (estQuotaQuotAtteint)
                                    seanceCourante.tableauErreurs.Add("labelErreurMaximumAtteint",
                                     Erreur.MAX_SEANCE_ATTEINT + Session[ClParametre.TypeParametre.MaxSeancesJour.ToString()].ToString() + ".");
                            }
                        }
                    }
                }
                else if (textNbCredits.Text.Length > 0)
                {
                    double nbCredits = 0;
                    if (Double.TryParse(textNbCredits.Text, out nbCredits) && nbCredits > 0)
                    {
                        if (estEntraineur)
                        {
                            seanceCourante = new ClSeanceEntrainement(null,
                                    null, nbCredits, m_compteConnecte.IdUtilisateur, String.Empty, true);

                            seanceCourante.DateActivite = date;
                        }
                        else
                        {
                            bool estQuotaHebdoAtteint = ClCourtierSeanceEntrainement.GetInstance().EstQuotaHebdomadaireAtteint(m_compteConnecte.IdUtilisateur, nbCredits);
                            bool estQuotaQuotAtteint = ClCourtierSeanceEntrainement.GetInstance().EstQuotaQuotidienAtteint(m_compteConnecte.IdUtilisateur, nbCredits);

                            if (estQuotaHebdoAtteint || estQuotaQuotAtteint)
                            {
                                if (estQuotaHebdoAtteint)
                                    seanceCourante.tableauErreurs.Add("labelErreurMaximumAtteint",
                                      Erreur.MAX_CREDIT_ATTEINT + Session[ClParametre.TypeParametre.MaxCreditsSemaine.ToString()].ToString() + ".");

                                if (estQuotaQuotAtteint)
                                    seanceCourante.tableauErreurs.Add("labelErreurMaximumAtteint",
                                      Erreur.MAX_SEANCE_ATTEINT + Session[ClParametre.TypeParametre.MaxSeancesJour.ToString()].ToString() + ".");
                            }
                            else
                            {
                                bool valide = (nbCredits < Convert.ToInt32(Session[ClParametre.TypeParametre.MaxCreditsSeance.ToString()]));
                                seanceCourante = new ClSeanceEntrainement(null,
                                    null, nbCredits, m_compteConnecte.IdUtilisateur, String.Empty, estEntraineur ? true : valide);
                                seanceCourante.DateActivite = date;
                            }
                        }
                    }
                    else
                    {
                        seanceCourante.tableauErreurs.Add("labelTemps",
                         Erreur.CREDITS_INVALIDE + Session[ClParametre.TypeParametre.MaxCreditsSeance.ToString()].ToString() + " crédits.");
                    }
                }
                else
                {
                    seanceCourante.tableauErreurs.Add("labelTemps",
                        Erreur.DUREE_INVALIDE + Session[ClParametre.TypeParametre.MaxTempsEntrainement.ToString()].ToString() + " minutes.");
                }
            }

            if (seanceCourante.tableauErreurs.Count == 0)
            {
                if (ClCourtierSeanceEntrainement.GetInstance().Insérer(seanceCourante))
                    Response.Redirect("Confirmation.aspx");
            }
            else
            {
                Utilitaire.RemplirLabelDErreurs(
                    seanceCourante.tableauErreurs, formulaireAjouterCredit);
                ChargerPage();
            }
        }

        private bool EstNbMinutesValide(ClSeanceEntrainement p_se, int p_nbMinutes)
        {
            bool valeurDeRetour = true;
            ClParametre paramètreMaxTempsEntrainement = ClCourtierParametre.GetInstance().ObtenirSelonNom("MaxTempsEntrainement");
            if (p_nbMinutes > paramètreMaxTempsEntrainement.Valeur)
            {
                valeurDeRetour = false;
                p_se.tableauErreurs.Add("labelTemps", "Nombre de minutes trop élevé. Maximum : " + paramètreMaxTempsEntrainement.Valeur.ToString() + " minutes.");
            }

            return valeurDeRetour;
        }
        /*
        private void RemplirComboActivitées()
        {
            foreach (ClActivite activité in
                ClCourtierActivite.GetInstance().ObtenirsTous())
            {
                ListItem item = new ListItem(activité.NomActivite, activité.IdActivite.ToString());

                if (activité.GroupeActivite != "")
                {
                    item.Attributes["OptionGroup"] = activité.GroupeActivite.ToString();
                }

                DropDownActivitées.Items.Add(item);
            }
        }*/

        private double CalculerCredits(int p_intensiteChoisie, int p_nbMinutes)
        {
            double defaultDolard = (double)p_nbMinutes / 30.0;
            return defaultDolard * p_intensiteChoisie;
        }

        private int CalculerMinutes(int p_intensiteChoisie, double p_nbCredits)
        {
            double defaultDolard = p_nbCredits / p_intensiteChoisie;
            return Convert.ToInt32(defaultDolard * 30.0);
        }

        protected void MettreAJourDescriptionRadio(object sender, EventArgs e)
        {
            Session["Date"] = calendar.SelectedDate;

            switch (radioIntensite.SelectedIndex)
            {
                case 0:
                    labelDefinition.Visible = true;
                    labelDescription1.Text = Constantes.INTENSITE_FAIBLE_DESCRIPTION;
                    labelDescription2.Text = String.Empty;
                    labelDescription3.Text = String.Empty;
                    tableau.Src = "../images/intensite/intensite_faible.jpg";
                    break;
                case 1:
                    labelDefinition.Visible = true;
                    labelDescription1.Text = String.Empty;
                    labelDescription2.Text = Constantes.INTENSITE_MODERE_DESCRIPTION;
                    labelDescription3.Text = String.Empty;
                    tableau.Src = "../images/intensite/intensite_modere.jpg";
                    break;
                case 2:
                    labelDefinition.Visible = true;
                    labelDescription1.Text = String.Empty;
                    labelDescription2.Text = String.Empty;
                    labelDescription3.Text = Constantes.INTENSITE_ELEVE_DESCRIPTION;
                    tableau.Src = "../images/intensite/intensite_eleve.jpg";
                    break;
                default:
                    labelDefinition.Visible = false;
                    labelDescription.Text = "";
                    break;
            }
        }
    }
}