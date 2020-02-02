using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CS2013
{   
    public partial class GestionCapsulesSante : System.Web.UI.Page
    {
        ClCompte m_compteConnecte = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            m_compteConnecte = (ClCompte)Session["utilisateur"];

            if (m_compteConnecte == null)
                Response.Redirect("~/Pages/FinSession.aspx");

            if (!IsPostBack)
                BindData();
        }

        public void BindData()
        {
            GridCapsuleSante.EmptyDataText = Constantes.AUCUNE_DONNEE;
            labelTitreArticle.Text = "Titre de la capsule : ";
            labelDate.Text = "Date de parution : ";
            labelTexte.Text = "Texte de l'Article : ";
            Date.SelectedDate = DateTime.Now;

            DataSet dataSet = GetData();

            GridCapsuleSante.DataSource = dataSet;
            GridCapsuleSante.DataBind();

            insert_table.Visible = GridCapsuleSante.Rows.Count == 0;
            GridCapsuleSante.Visible = GridCapsuleSante.Rows.Count > 0;
        }

        private DataSet GetData()
        {
            return ClCourtierCapsuleSante.GetInstance().ObtenirTousSQLData();
        }

        protected void GridConcentrations_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridCapsuleSante.PageIndex = e.NewPageIndex;

            GridCapsuleSante.EditIndex = -1;
            GridCapsuleSante.SelectedIndex = -1;
        }

        protected void GridConcentrations_PageIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        protected void GridConcentrations_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridCapsuleSante.EditIndex = e.NewEditIndex;
            BindData();
        }

        protected void GridConcentrations_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridCapsuleSante.EditIndex = -1;
            BindData();
        }

        protected void GridConcentrations_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (GridCapsuleSante.EditIndex == -1 || GridCapsuleSante.EditIndex != e.Row.RowIndex)
                {
                    Label titre = (Label)e.Row.Cells[2].FindControl("Titre");
                    titre.Text = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[1].ToString();

                    Label date = (Label)e.Row.Cells[3].FindControl("Date");
                    date.Text = Convert.ToDateTime(((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[2].ToString()).ToShortDateString();

                    Label contenu = (Label)e.Row.Cells[4].FindControl("Contenu");
                    contenu.Text = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString();
                }
                else if (GridCapsuleSante.EditIndex == e.Row.RowIndex)
                {
                    ((TextBox)e.Row.Cells[2].FindControl("Titre")).Text = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[1].ToString();
                    ((Label)e.Row.Cells[2].FindControl("Date")).Text = Convert.ToDateTime(((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[2].ToString()).ToShortDateString();
                    ((TextBox)e.Row.Cells[2].FindControl("Contenu")).Text = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString().Replace("<br />", "\n");
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                (e.Row.FindControl("Date") as AjaxControlToolkit.CalendarExtender).SelectedDate = DateTime.Now;
                (e.Row.FindControl("Insert") as Button).CommandArgument = "Insert";
                (e.Row.FindControl("Insert") as Button).Click += Insert_Click;
            }
        }

        protected void GridConcentrations_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton linkSupprimer = e.Row.FindControl("Delete") as ImageButton;
                if (linkSupprimer != null)
                    linkSupprimer.OnClientClick = ClConcentration.JS_CONFIRMATION_SUPRESSION;
            }
        }

        protected void GridConcentrations_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string id = GridCapsuleSante.DataKeys[e.RowIndex].Value.ToString();

            if (!String.IsNullOrEmpty(id))
            {
                TextBox titre = (TextBox)GridCapsuleSante.Rows[GridCapsuleSante.EditIndex].Cells[2].FindControl("Titre");
                TextBox contenu = (TextBox)GridCapsuleSante.Rows[GridCapsuleSante.EditIndex].Cells[2].FindControl("Contenu");

                ClCapsuleSante capsule = new ClCapsuleSante(id, titre.Text, DateTime.Now, contenu.Text.Replace("\n", "<br />"));
                if (!ClCourtierCapsuleSante.GetInstance().Modifier(capsule))
                    labelErreur.Text = Erreur.MISE_A_JOUR;
                else
                {
                    GridCapsuleSante.EditIndex = -1;
                    BindData();

                    labelErreur.Text = String.Empty;
                }
            }
        }

        protected void GridConcentrations_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = GridCapsuleSante.DataKeys[e.RowIndex].Value.ToString();

            if (!ClCourtierCapsuleSante.GetInstance().SupprimerSelonId(id))
                labelErreur.Text = Erreur.SUPPRESSION;

            BindData();
        }

        /// <summary>
        /// Insert records into datbase
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddButon_Click(object sender, EventArgs e)
        {
            Ajouter((sender as Button).CommandArgument);
        }

        /// <summary>
        /// Show Add new record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            GridCapsuleSante.ShowFooter = false;

            BindData();
        }

        protected void Insert_Click(object sender, EventArgs e)
        {
            Ajouter((sender as Button).CommandArgument);
        }

        protected void Ajouter(string p_command)
        {
            if (p_command.Equals("EmptyInsert"))
            {
                ClCapsuleSante capsule =
                new ClCapsuleSante(
                    titreArticle.Text,
                    Convert.ToDateTime(txtDate.Text),
                    texte.Text.Replace("\n", "<br>"));

                if (String.IsNullOrEmpty(capsule.Titre))
                {
                    labelErreur.Text = "Le titre est nécessaire";
                    if (String.IsNullOrEmpty(capsule.Contenu))
                        labelErreur.Text = "<br>Le contenu est nécessaire";
                }
                else if (String.IsNullOrEmpty(capsule.Contenu))
                    labelErreur.Text = "Le contenu est nécessaire";
                else if (!ClCourtierCapsuleSante.GetInstance().Insérer(capsule))
                    labelErreur.Text = Erreur.AJOUT;
            }
            else if (p_command.Equals("Insert"))
            {
                ClCapsuleSante capsule =
                    new ClCapsuleSante(
                        (GridCapsuleSante.FooterRow.FindControl("Titre") as TextBox).Text,
                        Convert.ToDateTime((GridCapsuleSante.FooterRow.FindControl("txtDate") as TextBox).Text),
                        (GridCapsuleSante.FooterRow.FindControl("Contenu") as TextBox).Text.Replace("\n", "<br />"));

                if (String.IsNullOrEmpty(capsule.Titre))
                {
                    labelErreur.Text = "Le titre est nécessaire";
                    if (String.IsNullOrEmpty(capsule.Contenu))
                        labelErreur.Text = "<br>Le contenu est nécessaire";
                }
                else if (String.IsNullOrEmpty(capsule.Contenu))
                    labelErreur.Text = "Le contenu est nécessaire";
                else if (!ClCourtierCapsuleSante.GetInstance().Insérer(capsule))
                    labelErreur.Text = Erreur.AJOUT;
            }

            BindData();
        }
    }
}