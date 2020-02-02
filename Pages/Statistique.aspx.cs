using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CS2013
{
    public partial class Statistique : System.Web.UI.Page
    {
        ClCompte m_compteConnecte = null;

        protected void Page_Load(object sender, EventArgs e)
        {

            m_compteConnecte = (ClCompte)Session["utilisateur"];

            if (m_compteConnecte == null)
                Response.Redirect("~/Pages/FinSession.aspx");

            if (!Page.IsPostBack)
            {
                BindData();
                ChargerPage();
            }
        }

        private void setStatistiques()
        {
            string[] statsAfficher =
               ClCourtierModificationAffichage.GetInstance().ObtenirContenu("Statistique").Split('/');

            HtmlTableRow r = new HtmlTableRow();
            HtmlTableCell c = new HtmlTableCell();
            HtmlTableCell c2 = new HtmlTableCell();
            Label labelPresentationNbCredits = new Label();
            labelPresentationNbCredits.ID = "labelPresentationNbCredits";
            labelPresentationNbCredits.Text = Constantes.PRESENTATION_NB_CREDITS;
            labelPresentationNbCredits.Style["white-space"] = "nowrap";
            Label labelTotalCredits = new Label();
            labelTotalCredits.ID = "labelTotalCredits";
            labelTotalCredits.Text = AfficherTotalCredits();
            labelTotalCredits.Style["white-space"] = "nowrap";
            c.Controls.Add(labelPresentationNbCredits);
            c2.Controls.Add(labelTotalCredits);
            r.Cells.Add(c);
            r.Cells.Add(c2);
            tableStats.Rows.Add(r);

            if (statsAfficher[0] == "1")
            {
                r = new HtmlTableRow();
                c = new HtmlTableCell();
                c2 = new HtmlTableCell();
                Label labeltotalBougeotteCegep = new Label();
                labeltotalBougeotteCegep.ID = "labeltotalBougeotteCegep";
                labeltotalBougeotteCegep.Text = Constantes.TOTAL_BOUGEOTTE_CEGEP;
                labeltotalBougeotteCegep.Style["white-space"] = "nowrap";
                Label totalBougeotteCegep = new Label();
                totalBougeotteCegep.ID = "totalBougeotteCegep";
                totalBougeotteCegep.Text = Utilitaire.CreditsEnTexte(ClCourtierSeanceEntrainement.GetInstance().ObtenirTotalBougeotteCegep()) + " $";
                totalBougeotteCegep.Style["white-space"] = "nowrap";
                c.Controls.Add(labeltotalBougeotteCegep);
                c2.Controls.Add(totalBougeotteCegep);
                r.Cells.Add(c);
                r.Cells.Add(c2);
                tableStats.Rows.Add(r);
            }
            if (statsAfficher[1] == "1")
            {
                r = new HtmlTableRow();
                c = new HtmlTableCell();
                c2 = new HtmlTableCell();
                Label labeltotalBougeotte = new Label();
                labeltotalBougeotte.ID = "labeltotalBougeotte";
                labeltotalBougeotte.Text = Constantes.GRAND_TOTAL_BOUGEOTTE;
                labeltotalBougeotte.Style["white-space"] = "nowrap";
                Label totalBougeotte = new Label();
                totalBougeotte.ID = "totalBougeotte";
                totalBougeotte.Text = Utilitaire.CreditsEnTexte(ClCourtierSeanceEntrainement.GetInstance().ObtenirTotalBougeotte()) + " $";
                totalBougeotte.Style["white-space"] = "nowrap";
                c.Controls.Add(labeltotalBougeotte);
                c2.Controls.Add(totalBougeotte);
                r.Cells.Add(c);
                r.Cells.Add(c2);
                tableStats.Rows.Add(r);
            }
            if (statsAfficher[2] == "1")
            {
                r = new HtmlTableRow();
                c = new HtmlTableCell();
                c2 = new HtmlTableCell();
                Label labeltotalBougeotteAnnuel = new Label();
                labeltotalBougeotteAnnuel.ID = "labeltotalAnnuelCegep";
                labeltotalBougeotteAnnuel.Text = Constantes.TOTAL_ANNUEL_CEGEP;
                labeltotalBougeotteAnnuel.Style["white-space"] = "nowrap";
                Label totalBougeotteAnnuel = new Label();
                totalBougeotteAnnuel.ID = "totalAnnuelCegep";
                totalBougeotteAnnuel.Text = Utilitaire.CreditsEnTexte(ClCourtierSeanceEntrainement.GetInstance().ObtenirTotalAnnuelCegep()) + " $";
                totalBougeotteAnnuel.Style["white-space"] = "nowrap";
                c.Controls.Add(labeltotalBougeotteAnnuel);
                c2.Controls.Add(totalBougeotteAnnuel);
                r.Cells.Add(c);
                r.Cells.Add(c2);
                tableStats.Rows.Add(r);
            }

            r = new HtmlTableRow();
            c = new HtmlTableCell();
            c2 = new HtmlTableCell();
            Label labeltotalAnnuel = new Label();
            labeltotalAnnuel.ID = "labeltotalAnnuel";
            labeltotalAnnuel.Text = Constantes.GRAND_TOTAL_ANNUEL;
            labeltotalAnnuel.Style["white-space"] = "nowrap";
            Label totalAnnuel = new Label();
            totalAnnuel.ID = "totalAnnuel";
            totalAnnuel.Text = Utilitaire.CreditsEnTexte(ClCourtierSeanceEntrainement.GetInstance().ObtenirTotalAnnuel()) + " $";
            totalAnnuel.Style["white-space"] = "nowrap";
            c.Controls.Add(labeltotalAnnuel);
            c2.Controls.Add(totalAnnuel);
            r.Cells.Add(c);
            r.Cells.Add(c2);
            r.Style["border-bottom"] = "none";
            tableStats.Rows.Add(r);
        }

        private void ChargerPage()
        {
            Grid.EmptyDataText = Constantes.AUCUNE_DONNEE;
            GridPerso.EmptyDataText = Constantes.AUCUNE_DONNEE;

            labelTitre.Text = Constantes.STATISTIQUES_TITRE;
            
            AfficherTotalCredits();

            setStatistiques();

            panelInviteFriend.Visible = Convert.ToInt32(Session[ClParametre.TypeParametre.UtilisationInviteAmi.ToString()]) == 1;

            AfficherRadioButton();
        }

        private void AfficherRadioButton()
        {
            string affichageRadioStats = ClCourtierModificationAffichage.GetInstance().ObtenirContenu("affichageRadioStats");

            string[] listeRadioStats = affichageRadioStats.Split('|');

            for (int i = 0; i < listeRadioStats.Length; i++)
            {
                if (listeRadioStats[i] != "Visible")
                    radioStatistiques.Items[i].Attributes.Add("hidden", "hidden");
            }
        }

        private string AfficherTotalCredits()
        {
            List<ClSeanceEntrainement> seances =
                ClCourtierSeanceEntrainement.GetInstance().ObtenirSeancesSelonIdUtilisateur(m_compteConnecte.IdUtilisateur);

            double nbCreditsTotal = 0.0;

            foreach (ClSeanceEntrainement seance in seances)
                nbCreditsTotal += seance.NbCredits;

            return Utilitaire.CreditsEnTexte(nbCreditsTotal) + " $";
        }

        public void Actualiser(Object sender, EventArgs e)
        {
            ChargerPage();
            AfficherRadioButton();
            BindData();
        }

        public void BindData()
        {
            switch (radioStatistiques.SelectedValue)
            {
                case "StatsPerso":
                    labelGlobal.Text = String.Empty;
                    dropConcentration.Visible = false;
                    GridPerso.Visible = true;
                    Grid.Visible = false;
                    BindDataPerso();
                    return;
                case "Programmes":
                    labelGlobal.Text = String.Empty;
                    GridPerso.Visible = false;
                    Grid.Visible = true;
                    dropConcentration.Visible = false;
                    Grid.DataSource = ClCourtierConcentration.GetInstance().ObtenirTotalProgrammes();
                    break;
                case "DepartementsServices":
                    labelGlobal.Text = String.Empty;
                    dropConcentration.Visible = false;
                    GridPerso.Visible = false;
                    Grid.Visible = true;
                    Grid.DataSource = ClCourtierConcentration.GetInstance().ObtenirTotalDepartementsServices();
                    break;
                case "Partenaires":
                    dropConcentration.Visible = true;
                    GridPerso.Visible = false;
                    Grid.Visible = false;
                    RemplirComboDepartements(TypeUser.Partenaire);
                    labelGlobal.Text = String.Empty;/*
                    Grid.DataSource = ClCourtierSousConcentration.GetInstance().ObtenirTotalPartenaires(dropConcentration.SelectedValue);
                    double objectif = ClCourtierConcentration.GetInstance().ObtenirObectifSelonId(dropConcentration.SelectedValue);
                    if (objectif > 0)
                        labelGlobal.Text = String.Format("L'objectif global de {0} est de {1}$",
                            dropConcentration.SelectedItem.Text,
                            Utilitaire.CreditsEnTexte(objectif));
                    else
                        labelGlobal.Text = String.Empty;*/
                    break;
                case "Communautes":
                    dropConcentration.Visible = true;
                    GridPerso.Visible = false;
                    Grid.Visible = false;
                    RemplirComboDepartements(TypeUser.Communaute);
                    labelGlobal.Text = String.Empty;
                    /*Grid.DataSource = ClCourtierSousConcentration.GetInstance().ObtenirTotalCommunautés(dropConcentration.SelectedValue);
                    double objectifComm = ClCourtierConcentration.GetInstance().ObtenirObectifSelonId(dropConcentration.SelectedValue);
                    if (objectifComm > 0)
                        labelGlobal.Text = String.Format("L'objectif global de {0} est de {1}$",
                            dropConcentration.SelectedItem.Text,
                            Utilitaire.CreditsEnTexte(objectifComm));
                    else
                        labelGlobal.Text = String.Empty;*/
                    break;
                case "Ecole":
                    dropConcentration.Visible = true;
                    GridPerso.Visible = false;
                    Grid.Visible = false;
                    RemplirComboDepartements(TypeUser.EtudiantExterne);
                    labelGlobal.Text = String.Empty;
                    /*Grid.DataSource = ClCourtierSousConcentration.GetInstance().ObtenirTotalEcole(dropConcentration.SelectedValue);
                    double objectifEcole = ClCourtierConcentration.GetInstance().ObtenirObectifSelonId(dropConcentration.SelectedValue);
                    if (objectifEcole > 0)
                        labelGlobal.Text = String.Format("L'objectif global de {0} est de {1}$",
                            dropConcentration.SelectedItem.Text,
                            Utilitaire.CreditsEnTexte(objectifEcole));
                    else
                        labelGlobal.Text = String.Empty;*/
                    break;
            }

            if (Grid.DataSource != null)
                Grid.DataBind();
        }

        private void RemplirComboDepartements(TypeUser typeUser)
        {
            dropConcentration.Items.Clear();

            ListItem item = new ListItem("Choisir...", "-1");
            dropConcentration.Items.Add(item);

            foreach (ClConcentration concentration in
                ClCourtierConcentration.GetInstance().ObtenirTousSelonTypeUser(typeUser))
            {
                item = new ListItem(concentration.NomConcentration, concentration.IdConcentration.ToString());

                dropConcentration.Items.Add(item);
            }
        }

        protected void Grid_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && Grid.DataSource != null)
            {
                string credits = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[1].ToString();
                string objectif = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[2].ToString();
                string pourcentage = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString();
                Label labelCredits = e.Row.FindControl("labelCredits") as Label;
                Label labelObjectif = e.Row.FindControl("labelObjectif") as Label;
                Label labelObjectifPourcentage = e.Row.FindControl("labelObjectifPourcentage") as Label;
                labelCredits.Text = Utilitaire.CreditsEnTexte(Convert.ToDouble(credits)) + " $";
                labelObjectif.Text = Utilitaire.CreditsEnTexte(Convert.ToDouble(objectif)) + " $";
                labelObjectifPourcentage.Text = Utilitaire.CreditsEnTexte(Convert.ToDouble(pourcentage)) + " %";
            }
        }

        protected void Grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Grid.PageIndex = e.NewPageIndex;

            Grid.EditIndex = -1;
            Grid.SelectedIndex = -1;
        }

        protected void Grid_PageIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        public void BindDataPerso()
        {
            GridPerso.DataSource =
                ClCourtierSeanceEntrainement.GetInstance().ObtenirSeancesStatsPersoSQLData(
                                                                m_compteConnecte.IdUtilisateur);
            GridPerso.DataBind();
        }

        protected void GridPerso_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridPerso.PageIndex = e.NewPageIndex;

            GridPerso.EditIndex = -1;
            GridPerso.SelectedIndex = -1;
        }

        protected void GridPerso_PageIndexChanged(object sender, EventArgs e)
        {
            BindDataPerso();
        }

        protected void GridPerso_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.DataItem != null)
            {
                string intensite = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[2].ToString();

                if (!String.IsNullOrEmpty(intensite))
                {
                    Label labelIntensite = e.Row.FindControl("labelIntensite") as Label;
                    labelIntensite.Text = Utilitaire.ConvertirIntensite(Int32.Parse(intensite));
                }

                string credits = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[4].ToString();
                Label labelCredits = e.Row.FindControl("labelCredits") as Label;

                labelCredits.Text = Utilitaire.CreditsEnTexte(Convert.ToDouble(credits)) + " $";
            }
        }
        protected void dropConcentration_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid.Visible = true;
            GridPerso.Visible = false;

            BindDataSousConc();
        }

        private void BindDataSousConc()
        {
            switch (radioStatistiques.SelectedValue)
            {
                case "Partenaires":
                    Grid.DataSource = ClCourtierSousConcentration.GetInstance().ObtenirTotalPartenaires(dropConcentration.SelectedValue);
                    break;
                case "Communautes":
                    Grid.DataSource = ClCourtierSousConcentration.GetInstance().ObtenirTotalCommunautés(dropConcentration.SelectedValue);
                    break;
                case "Ecole":
                    Grid.DataSource = ClCourtierSousConcentration.GetInstance().ObtenirTotalEcole(dropConcentration.SelectedValue);
                    break;
            }

            double objectif = ClCourtierConcentration.GetInstance().ObtenirObectifSelonId(dropConcentration.SelectedValue);
            if (objectif > 0)
                labelGlobal.Text = String.Format("L'objectif global de {0} est de {1}$",
                    dropConcentration.SelectedItem.Text,
                    Utilitaire.CreditsEnTexte(objectif));
            else
                labelGlobal.Text = String.Empty;

            GridPerso.Visible = false;
            Grid.Visible = true;

            if (Grid.DataSource != null)
                Grid.DataBind();
        }

        protected void ok_Click(object sender, EventArgs e)
        {
            if (Regex.IsMatch(manualDest.Text, "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*"))
            {
                MailMessage msg = new MailMessage();
                msg.Body = String.Format("{0} ({1}) vous invite à vous inscrire sur l'application Crédit $anté <a href=\"www.google.com\">Crédit $anté</a>", (String.IsNullOrEmpty(m_compteConnecte.Nom) ? "Un ami" : m_compteConnecte.Nom), m_compteConnecte.IdUtilisateur);
                msg.IsBodyHtml = true;
                msg.Subject = "Invitation";
                msg.To.Add(manualDest.Text);

                new SmtpClient().Send(msg);
            }
        }
    }
}