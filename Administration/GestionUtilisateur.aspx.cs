using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CS2013
{
    public partial class GestionUtilisateur : System.Web.UI.Page
    {
        ClCompte m_compteConnecte = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            m_compteConnecte = (ClCompte)Session["utilisateur"];

            if (m_compteConnecte == null)
                Response.Redirect("~/Pages/FinSession.aspx");

            if (!Page.IsPostBack)
            {
                insert_table.Visible = false;
                labelNbCredits.Text = "Nombre de crédits : ";
                labelDate.Text = "Date : ";

                m_compteConnecte = (ClCompte)Session["utilisateur"];
                filterDropDownList.Items.Add("ID");
                filterDropDownList.Items.Add("Nom");
                filterDropDownList.Items.Add("Concentration");
                recherche.Text = "";
                sortByDropDownList.Items.Add("ID");
                sortByDropDownList.Items.Add("Nom");
                sortByDropDownList.Items.Add("Concentration");

                GridUtilisateur.EmptyDataText = Constantes.AUCUNE_DONNEE;
                GridDetail.EmptyDataText = Constantes.AUCUNE_ENTREE;
                BindData();
            }
        }

        ICollection CreateDataSource()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add(new DataColumn("TYPE_ROLE", typeof(String)));

            foreach (ClCompte.TypeCompte type in Enum.GetValues(typeof(ClCompte.TypeCompte)))
            {
                if (type == ClCompte.TypeCompte.Invalide)
                    continue;

                DataRow dataRow = dataTable.NewRow();
                dataRow[0] = type.ToString();
                dataTable.Rows.Add(dataRow);
            }

            return new DataView(dataTable);
        }

        public void BindTypeUtilisateur(DropDownList p_dropDown)
        {
            // On bind les types de concentration dans le combobox
            p_dropDown.DataSource = CreateDataSource();
            p_dropDown.DataTextField = "TYPE_ROLE";
            p_dropDown.DataValueField = "TYPE_ROLE";
            p_dropDown.DataBind();
            p_dropDown.SelectedIndex = 0;
        }


        ICollection CreateDataSourceConcentration()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add(new DataColumn("NomConcentration", typeof(String)));

            foreach (ClConcentration concentration in
                 ClCourtierConcentration.GetInstance().ObtenirTous())
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow[0] = concentration.NomConcentration.ToString();
                dataTable.Rows.Add(dataRow);
            }

            return new DataView(dataTable);
        }

        public void BindConcentration(DropDownList p_dropDown)
        {
            p_dropDown.DataSource = ClCourtierConcentration.GetInstance().ObtenirTous();
            p_dropDown.DataTextField = "NomConcentration";
            p_dropDown.DataValueField = "NomConcentration";
            p_dropDown.DataBind();
            p_dropDown.SelectedIndex = 0;
        }

        protected void Rechercher(Object sender, EventArgs e)
        {
            BindData();
        }

        public void BindData()
        {
            // On bind les concentrations dans la GridView

            string trierPar = "";
            switch (sortByDropDownList.SelectedItem.Text)
            {
                case "ID": trierPar = "ID_UTILISATEUR"; break;
                case "Nom": trierPar = "NOM"; break;
                case "Concentration": trierPar = "NOM_CONCENTRATION"; break;
                default: break;
            }

            if (recherche.Text == "")
            {
                GridUtilisateur.DataSource = ClCourtierCompte.GetInstance().ObtenirTousSQLData(trierPar);
                GridUtilisateur.DataBind();
            }
            else
            {
                string filter = "";
                switch (filterDropDownList.SelectedItem.Text)
                {
                    case "ID": filter = "ID_UTILISATEUR"; break;
                    case "Nom": filter = "NOM"; break;
                    case "Concentration": filter = "NOM_CONCENTRATION"; break;
                    default: break;
                }

                GridUtilisateur.DataSource = ClCourtierCompte.GetInstance().ObtenirTousSQLData(trierPar, recherche.Text, filter);
                GridUtilisateur.DataBind();
            }
        }

        protected void GridUtilisateur_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridUtilisateur.PageIndex = e.NewPageIndex;

            GridUtilisateur.EditIndex = -1;
            GridUtilisateur.SelectedIndex = -1;
            insert_table.Visible = false;
        }

        protected void GridDetail_PageIndexChanged(object sender, EventArgs e)
        {
            BindDataGridViewDetails();
        }

        protected void GridDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridDetail.PageIndex = e.NewPageIndex;

            GridDetail.EditIndex = -1;
            GridDetail.SelectedIndex = -1;
        }

        protected void GridUtilisateur_PageIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        protected void GridUtilisateur_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridUtilisateur.EditIndex = e.NewEditIndex;

            GridUtilisateur.SelectedIndex = -1;
            GridDetail.Visible = false;
            insert_table.Visible = false;

            BindData();
        }

        protected void GridUtilisateur_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridUtilisateur.EditIndex = -1;
            BindData();
        }

        protected void GridUtilisateur_RowCreated(object sender, GridViewRowEventArgs e)
        {
            // Pour chaque rangée de données, à la création de la grille, on attache du js au bouton de suppression
            // qui demandera confirmation. On se souvient que la suppression d'une caté;rie entraîne la suppression
            // de tous les articles associés (à cause du DeleteCascade dans la bd)
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton linkSupprimer = e.Row.FindControl("Delete") as ImageButton;
                if (linkSupprimer != null)
                    linkSupprimer.OnClientClick = ClConcentration.JS_CONFIRMATION_SUPRESSION;
            }
        }

        protected void GridUtilisateur_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (GridUtilisateur.EditIndex == -1 || GridUtilisateur.EditIndex != e.Row.RowIndex)
                {
                    Label nom = (Label)e.Row.Cells[1].FindControl("Nom");
                    nom.Text = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[5].ToString();

                    Label email = (Label)e.Row.Cells[1].FindControl("Courriel");
                    email.Text = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[6].ToString();

                    Label texto = (Label)e.Row.Cells[1].FindControl("Texto");
                    texto.Text = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[7].ToString();

                    Label type = (Label)e.Row.Cells[1].FindControl("Type");
                    type.Text = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString();

                    Label concentration = (Label)e.Row.Cells[1].FindControl("Concentration");
                    concentration.Text = ClCourtierConcentration.GetInstance().ObtenirObjetSelonId(((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[2].ToString()).NomConcentration;
                }
                else if (GridUtilisateur.EditIndex == e.Row.RowIndex)
                {
                    TextBox nom = (TextBox)e.Row.Cells[1].FindControl("Nom");
                    nom.Text = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[5].ToString();

                    TextBox courriel = (TextBox)e.Row.Cells[1].FindControl("Courriel");
                    courriel.Text = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[6].ToString();

                    TextBox texto = (TextBox)e.Row.Cells[1].FindControl("Texto");
                    texto.Text = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[7].ToString();

                    DropDownList type = (DropDownList)e.Row.Cells[1].FindControl("Type");

                    BindTypeUtilisateur(type);
                    object currentType = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3];
                    type.SelectedValue = currentType.ToString();


                    DropDownList concentration = (DropDownList)e.Row.Cells[2].FindControl("Concentration");

                    BindConcentration(concentration);
                    object currentConcentration = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[4];
                    concentration.SelectedValue = currentConcentration.ToString();

                }
            }
        }

        protected void GridDetail_RowCreated(object sender, GridViewRowEventArgs e)
        {
            // Pour chaque rangée de données, à la création de la grille, on attache du js au bouton de suppression
            // qui demandera confirmation. On se souvient que la suppression d'une caté;rie entraîne la suppression
            // de tous les articles associés (à cause du DeleteCascade dans la bd)
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.DataItem != null)
                {
                    string intensite = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[1].ToString();

                    if (!String.IsNullOrEmpty(intensite))
                    {
                        Label labelIntensite = e.Row.FindControl("labelIntensite") as Label;
                        labelIntensite.Text = Utilitaire.ConvertirIntensite(Int32.Parse(intensite));
                    }

                    Label date = (Label)e.Row.FindControl("labelDate");
                    date.Text = Convert.ToDateTime(((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[6]).ToShortDateString();

                    string nbCredits = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString();
                    Label labelCreditsSante = e.Row.FindControl("labelCreditsSante") as Label;
                    labelCreditsSante.Text = Utilitaire.CreditsEnTexte(Convert.ToDouble(nbCredits)) + " $";
                }

                ImageButton linkSupprimer = e.Row.FindControl("Delete") as ImageButton;
                if (linkSupprimer != null)
                    linkSupprimer.OnClientClick = ClConcentration.JS_CONFIRMATION_SUPRESSION;
            }
        }

        /// <summary>
        /// Insert records into datbase
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddButon_Click(object sender, EventArgs e)
        {
            if ((sender as Button).CommandArgument.Equals("EmptyInsert"))
            {
                double i;
                if (Double.TryParse(nbCredits.Text, out i))
                {
                    ClSeanceEntrainement entrainement =
                        new ClSeanceEntrainement(Guid.NewGuid().ToString(),
                            null,
                            null,
                            i,
                            GridUtilisateur.SelectedRow.Cells[0].Text,
                            Convert.ToDateTime(txtDate.Text),
                            true);

                    labelErreur.Text = String.Empty;
                    if (!ClCourtierSeanceEntrainement.GetInstance().Insérer(entrainement))
                        labelErreur.Text = Erreur.AJOUT;
                }
                else
                {
                    labelErreur.Text = "Entrer un nombre de crédits valide.";
                }
            }
            else if ((sender as Button).CommandArgument.Equals("Insert"))
            {
                TextBox nbCredits = GridDetail.FooterRow.FindControl("txtNbCredits") as TextBox;
                DateTime date = Convert.ToDateTime((GridDetail.FooterRow.FindControl("txtDate") as TextBox).Text);

                double i;
                if (Double.TryParse(nbCredits.Text, out i))
                {
                    ClSeanceEntrainement entrainement =
                        new ClSeanceEntrainement(
                            Guid.NewGuid().ToString(),
                            null,
                            null,
                            i,
                            GridUtilisateur.SelectedRow.Cells[0].Text,
                            date,
                            true);

                    labelErreur.Text = String.Empty;
                    if (!ClCourtierSeanceEntrainement.GetInstance().Insérer(entrainement))
                        labelErreur.Text = Erreur.AJOUT;
                }
                else
                {
                    labelErreur.Text = "Entrer un nombre de crédits valide.";
                }
            }

            BindDataGridViewDetails();
        }

        /// <summary>
        /// Show Add new record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            GridUtilisateur.SelectedIndex = -1;
            BindData();
            GridDetail.Visible = false;
        }

        protected void GridUtilisateur_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridDetail.Visible = false;

            string id = GridUtilisateur.DataKeys[e.RowIndex].Value.ToString();

            DropDownList dlType = GridUtilisateur.Rows[e.RowIndex].FindControl("Type") as DropDownList;
            DropDownList dlConcentration = GridUtilisateur.Rows[e.RowIndex].FindControl("Concentration") as DropDownList;
            TextBox textboxMotDePasse = GridUtilisateur.Rows[e.RowIndex].FindControl("MotDePasse") as TextBox;
            string email = (GridUtilisateur.Rows[e.RowIndex].FindControl("Courriel") as TextBox).Text;
            string nom = (GridUtilisateur.Rows[e.RowIndex].FindControl("Nom") as TextBox).Text;
            string email_telephone = (GridUtilisateur.Rows[e.RowIndex].FindControl("Texto") as TextBox).Text;

            if (dlType.SelectedIndex != -1)
            {
                ClCompte.TypeCompte typeCompte =
                    ClCompte.GetTypeCompte(dlType.SelectedValue);

                ClCompte compte = new ClCompte(
                    id,
                    textboxMotDePasse.Text,
                    ClCourtierConcentration.GetInstance().ObtenirObjetSelonNom(dlConcentration.SelectedValue).IdConcentration,
                    typeCompte,
                    ClCourtierCompte.GetInstance().ObtenirCompteSelonId(id).Type, email, nom,
                    email_telephone);

                if (!ClCourtierCompte.GetInstance().ModifierRole(compte))
                {
                    labelErreur.Text = Erreur.MISE_A_JOUR;
                }

                if (compte.MotDePasse != null && compte.MotDePasse != "")
                {
                    if (compte.MotDePasse.Length >= ClCompte.NB_CHARACTÈRE_MOT_DE_PASSE)
                    {
                        ClCourtierCompte.GetInstance().Modifier(compte);

                        GridUtilisateur.EditIndex = -1;
                        BindData();
                        labelErreur.Text = String.Empty;
                    }
                    else
                    {
                        labelErreur.Text = Erreur.MIN_CHAR_MDP;
                    }
                }
                else
                {
                    ClCourtierCompte.GetInstance().ModifierSansMotDePasse(compte);

                    GridUtilisateur.EditIndex = -1;
                    BindData();
                    labelErreur.Text = String.Empty;
                }
            }
        }

        protected void GridUtilisateur_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridUtilisateur.SelectedIndex = -1;
            GridDetail.Visible = false;
            // e.Keys and e.Values are only populated if using DataSourceID
            string id = GridUtilisateur.DataKeys[e.RowIndex].Value.ToString();

            if (id != m_compteConnecte.IdUtilisateur)
            {
                if (!ClCourtierCompte.GetInstance().Supprimer(id))
                {
                    labelErreur.Text = Erreur.SUPPRESSION;
                }
                else
                {
                    GridDetail.Visible = false;
                    GridUtilisateur.SelectedIndex = -1;
                    GridUtilisateur.EditIndex = -1;
                }
            }
            else
            {
                labelErreur.Text = Erreur.SUPPRESSION_COMPTE_COURANT;
            }

            GridUtilisateur.EditIndex = -1;
            BindData();
        }

        protected void GridDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // e.Keys and e.Values are only populated if using DataSourceID
            string id = GridDetail.DataKeys[e.RowIndex].Value.ToString();

            if (!ClCourtierSeanceEntrainement.GetInstance().SupprimerSelonIdSeance(id))
            {
                labelErreur.Text = Erreur.SUPPRESSION;
            }

            BindDataGridViewDetails();
        }

        protected void GridUtilisateur_InitModification(object sender, EventArgs e)
        {
            GridUtilisateur.EditIndex = -1;

            BindDataGridViewDetails();
            BindData();
        }

        private void BindDataGridViewDetails()
        {
            GridDetail.DataSource =
                ClCourtierSeanceEntrainement.GetInstance().ObtenirSeancesSQLData(
                GridUtilisateur.Rows[GridUtilisateur.SelectedIndex].Cells[0].Text);
            GridDetail.DataBind();

            if (GridDetail.Rows.Count > 0)
            {
                insert_table.Visible = false;
                GridDetail.Visible = true;
            }
            else
            {
                nbCredits.Text = String.Empty;
                datePicker.SelectedDate = DateTime.Now;
                insert_table.Visible = true;
                GridDetail.Visible = false;
            }
        }

        protected void btnPurgerUtilisateur_Click(object sender, EventArgs e)
        {
            int nbMois = Int32.Parse(txtBoxPurgerUtilisateur.Text);
            DateTime dateMaintenant = DateTime.Now;
            DateTime dateSurpession = dateMaintenant.AddMonths(nbMois);

            ClCourtierCompte.GetInstance().SupprimerCompteSelonDate(dateSurpession);

            MessageRetourPurge.Text = "La suppression a été effectué avec succès";
        }
    }
}