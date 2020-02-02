using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CS2013
{
    public partial class GestionCapsules : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((ClCompte)Session["utilisateur"] == null)
                Response.Redirect("~/Pages/FinSession.aspx");

            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            GridCapsules.DataSource = ClCourtierCapsuleSante.GetInstance().ObtenirTous();
            GridCapsules.DataBind();
        }

        protected void GridCapsules_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ClCapsuleSante capsule = e.Row.DataItem as ClCapsuleSante;

                if (e.Row.RowIndex != GridCapsules.EditIndex)
                {
                    (e.Row.FindControl("Titre") as Label).Text = capsule.Titre;
                    (e.Row.FindControl("Date") as Label).Text = capsule.DatePublication.ToShortDateString();
                    (e.Row.FindControl("Contenu") as Label).Text = capsule.Contenu;
                }
                else
                {
                    (e.Row.FindControl("Titre") as TextBox).Text = capsule.Titre;
                    (e.Row.FindControl("Date") as TextBox).Text = capsule.DatePublication.ToShortDateString();
                    (e.Row.FindControl("Contenu") as TextBox).Text = capsule.Contenu;

                    (e.Row.FindControl("Dater") as AjaxControlToolkit.CalendarExtender).SelectedDate = capsule.DatePublication;
                }
            }

            else if (e.Row.RowType == DataControlRowType.Footer || e.Row.RowType == DataControlRowType.EmptyDataRow)
            {
                (e.Row.FindControl("Dater") as AjaxControlToolkit.CalendarExtender).SelectedDate = DateTime.Now;
            }
        }

        protected void GridCapsules_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = GridCapsules.DataKeys[e.RowIndex].Value.ToString();

            if (!ClCourtierCapsuleSante.GetInstance().SupprimerSelonId(id))
                labelErreur.Text = Erreur.SUPPRESSION;

            BindData();
        }

        protected void GridCapsules_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridCapsules.EditIndex = e.NewEditIndex;
            GridCapsules.SelectedIndex = -1;

            BindData();
        }

        protected void GridCapsules_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox titre = (TextBox)GridCapsules.Rows[GridCapsules.EditIndex].FindControl("Titre");
            AjaxControlToolkit.CalendarExtender date = (AjaxControlToolkit.CalendarExtender)GridCapsules.Rows[GridCapsules.EditIndex].FindControl("Dater");
            TextBox contenu = (TextBox)GridCapsules.Rows[GridCapsules.EditIndex].FindControl("Contenu");

            if (date.SelectedDate == null)
                date.SelectedDate = DateTime.Now;

            ClCapsuleSante capsule =
                new ClCapsuleSante(
                    titre.Text,
                    date.SelectedDate.Value,
                    contenu.Text);

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
            else
            {
                GridCapsules.EditIndex = -1;
                GridCapsules.SelectedIndex = -1;
            }

            BindData();
        }

        protected void GridCapsules_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridCapsules.EditIndex = -1;
            GridCapsules.SelectedIndex = -1;

            BindData();
        }

        protected void Insert_Click(object sender, EventArgs e)
        {
            Panel container = (sender as Button).Parent as Panel;

            ClCapsuleSante capsule =
                new ClCapsuleSante(
                    (container.FindControl("txtTitre") as TextBox).Text,
                    Convert.ToDateTime((container.FindControl("txtDate") as TextBox).Text),
                    (container.FindControl("txtContenu") as TextBox).Text);

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

            BindData();
        }

        protected void Add_Capsule(object sender, ImageClickEventArgs e)
        {
            ClCapsuleSante capsule =
                new ClCapsuleSante(
                    (GridCapsules.FooterRow.FindControl("Titre") as TextBox).Text,
                    Convert.ToDateTime((GridCapsules.FooterRow.FindControl("Date") as TextBox).Text),
                    (GridCapsules.FooterRow.FindControl("Contenu") as TextBox).Text);

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

            BindData();
        }
    }
}