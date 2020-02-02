using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CS2013
{
    public partial class GestionEntreesSuspectes : System.Web.UI.Page
    {
        ClCompte m_compteConnecte = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            m_compteConnecte = (ClCompte)Session["utilisateur"];

            if (m_compteConnecte == null)
                Response.Redirect("~/Pages/FinSession.aspx");

            if (!Page.IsPostBack)
            {
                GridEntreesSuspectes.EmptyDataText = Constantes.AUCUNE_DONNEE;
                BindData();
            }
        }

        public void BindData()
        {
            System.Data.DataSet data = ClCourtierSeanceEntrainement.GetInstance().ObtenirSeancesSuspectesSQLData();

            if (data.Tables.Count > 0)
            {
                // On bind les entrées suspectes dans le GridView
                GridEntreesSuspectes.DataSource = data;
                GridEntreesSuspectes.DataBind();
            }
        }

        protected void GridEntreesSuspectes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridEntreesSuspectes.PageIndex = e.NewPageIndex;

            GridEntreesSuspectes.EditIndex = -1;
            GridEntreesSuspectes.SelectedIndex = -1;

            BindData();
        }

        protected void GridEntreesSuspectes_PageIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        protected void GridEntreesSuspectes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridEntreesSuspectes.EditIndex = e.NewEditIndex;
            BindData();
        }

        protected void GridEntreesSuspectes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridEntreesSuspectes.EditIndex = -1;
            GridEntreesSuspectes.SelectedIndex = -1;
            BindData();
        }



        protected void GridEntreesSuspectes_RowCreated(object sender, GridViewRowEventArgs e)
        {
            // Pour chaque rangée de données, à la création de la grille, on attache du js au bouton de suppression
            // qui demandera confirmation. On se souvient que la suppression d'une catégorie entraîne la suppression
            // de tous les articles associés (à cause du DeleteCascade dans la bd)
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.DataItem != null)
            {
                string intensite = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[1].ToString();

                if (!String.IsNullOrEmpty(intensite))
                {
                    Label labelIntensite = e.Row.FindControl("labelIntensite") as Label;
                    labelIntensite.Text = Utilitaire.ConvertirIntensite(Int32.Parse(intensite));
                }

                string email = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[8].ToString();
                if (String.IsNullOrEmpty(email))
                {
                    HtmlGenericControl modal = e.Row.FindControl("modalWindow") as HtmlGenericControl;
                    modal.Visible = false;

                    ImageButton linkSelect = e.Row.FindControl("SendEmail") as ImageButton;

                    if (linkSelect != null)
                        linkSelect.Visible = false;
                }
                else
                {
                    TextBox destinataire = e.Row.FindControl("destinataire") as TextBox;
                    destinataire.Text = email;

                    Button ok = e.Row.FindControl("ok") as Button;
                    ok.Click += sendMail;

                    if (e.Row.RowState != DataControlRowState.Edit)
                    {
                        ImageButton linkSelect = e.Row.FindControl("SendEmail") as ImageButton;
                        AjaxControlToolkit.ModalPopupExtender modal = e.Row.FindControl("modal") as AjaxControlToolkit.ModalPopupExtender;
                        modal.TargetControlID = linkSelect.UniqueID;
                        linkSelect.OnClientClick = "return;";
                    }
                }

                ImageButton linkSupprimer = e.Row.FindControl("Delete") as ImageButton;
                if (linkSupprimer != null)
                    linkSupprimer.OnClientClick = ClSeanceEntrainement.JS_CONFIRMATION_SUPRESSION;
            }
        }

        protected void GridEntreesSuspectes_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // e.Keys, e.NewValues, and e.OldValues are only populated if using DataSourceID    
            string id = GridEntreesSuspectes.DataKeys[e.RowIndex].Value.ToString();

            if (!ClCourtierSeanceEntrainement.GetInstance().ValiderSeance(id))
            {
                labelErreur.Text = Erreur.MISE_A_JOUR;
            }

            GridEntreesSuspectes.EditIndex = -1;
            GridEntreesSuspectes.SelectedIndex = -1;
            BindData();
        }

        protected void GridEntreesSuspectes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // e.Keys and e.Values are only populated if using DataSourceID
            string id = GridEntreesSuspectes.DataKeys[e.RowIndex].Value.ToString();

            if (!ClCourtierSeanceEntrainement.GetInstance().SupprimerSelonIdSeance(id))
            {
                labelErreur.Text = Erreur.SUPPRESSION;
            }

            GridEntreesSuspectes.EditIndex = -1;
            GridEntreesSuspectes.SelectedIndex = -1;
            BindData();
        }

        protected void sendMail(object sender, EventArgs args)
        {
            string destinataire = ((sender as Button).Parent.Parent.FindControl("destinataire") as TextBox).Text;
            string sujet = ((sender as Button).Parent.Parent.FindControl("titreMessage") as TextBox).Text;
            string contenu = ((sender as Button).Parent.Parent.FindControl("message") as TextBox).Text;

            MailMessage msg = new MailMessage();
            msg.To.Add(destinataire);
            msg.Body = contenu;
            msg.IsBodyHtml = true;
            msg.Subject = sujet;

            new SmtpClient().Send(msg);

            BindData();
        }
    }
}