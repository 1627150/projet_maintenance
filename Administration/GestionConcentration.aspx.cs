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
    public partial class GestionConcentration : System.Web.UI.Page
    {
        ClCompte m_compteConnecte = null;

        protected void Page_Load(object sender, EventArgs e)
        {

            m_compteConnecte = (ClCompte)Session["utilisateur"];

            if (m_compteConnecte == null)
                Response.Redirect("~/Pages/FinSession.aspx");

            if (!Page.IsPostBack)
            {
                nom.Text = "Nom ";
                labelObjectif.Text = "Objectif ";

                filterDropDownList.Items.Add("Nom");
                filterDropDownList.Items.Add("Groupe");
                recherche.Text = "";
                sortByDropDownList.Items.Add("Nom");
                sortByDropDownList.Items.Add("Groupe");

                GridConcentrations.EmptyDataText = Constantes.AUCUNE_DONNEE;
                GridSousConcentration.EmptyDataText = Constantes.AUCUNE_DONNEE;
                BindData();
            }

            GridConcentrations.EmptyDataText = Constantes.AUCUNE_DONNEE;
            GridSousConcentration.EmptyDataText = Constantes.AUCUNE_DONNEE;

            GridConcentrations.Visible = true;
        }

        ICollection CreateDataSource()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add(new DataColumn("NOM_CONCENTRATION", typeof(String)));
            dataTable.Columns.Add(new DataColumn("VALEUR_ENUM", typeof(String)));

            dataTable.Rows.Add(CreateRow("Département",
                TypeConcentration.Département.ToString(), dataTable));
            dataTable.Rows.Add(CreateRow("Service",
               TypeConcentration.Service.ToString(), dataTable));
            dataTable.Rows.Add(CreateRow("Programme Pré-universitaire",
                TypeConcentration.ProgrammePreUniversitaire.ToString(), dataTable));
            dataTable.Rows.Add(CreateRow("Programme technique",
                TypeConcentration.ProgrammeTechnique.ToString(), dataTable));
            dataTable.Rows.Add(CreateRow("Programme accueil",
                TypeConcentration.ProgrammeAccueilIntegration.ToString(), dataTable));
            dataTable.Rows.Add(CreateRow("Partenaire du cégep",
                 TypeConcentration.Partenaire.ToString(), dataTable));
            dataTable.Rows.Add(CreateRow("Scolaire externe",
                 TypeConcentration.Ecole.ToString(), dataTable));
            dataTable.Rows.Add(CreateRow("Communauté",
                 TypeConcentration.Communaute.ToString(), dataTable));

            return new DataView(dataTable);
        }

        DataRow CreateRow(String p_texte, String p_valeur, DataTable p_dataTable)
        {
            DataRow dataRow = p_dataTable.NewRow();

            dataRow[0] = p_texte;
            dataRow[1] = p_valeur;

            return dataRow;
        }

        public void BindTypeConcentration(DropDownList p_dropDown)
        {
            // On bind les types de concentration dans le combobox
            p_dropDown.DataSource = CreateDataSource();
            p_dropDown.DataTextField = "NOM_CONCENTRATION";
            p_dropDown.DataValueField = "VALEUR_ENUM";
            p_dropDown.DataBind();
            p_dropDown.SelectedIndex = 0;
        }

        protected void Rechercher(Object sender, EventArgs e)
        {
            BindData();
        }

        public void BindData(bool showSousConcTable = false)
        {
            // On bind les concentrations dans la GridView

            string trierPar = "";
            switch (sortByDropDownList.SelectedItem.Text)
            {
                case "Nom": trierPar = "NOM_CONCENTRATION"; break;
                case "Groupe": trierPar = "TYPE_CONCENTRATION"; break;
                default: break;
            }

            if (recherche.Text == "")
            {
                GridConcentrations.DataSource = ClCourtierConcentration.GetInstance().ObtenirTousSQLData(trierPar);
                GridConcentrations.DataBind();
            }
            else
            {
                string filter = "";
                switch (filterDropDownList.SelectedItem.Text)
                {
                    case "Nom": filter = "NOM_CONCENTRATION"; break;
                    case "Groupe": filter = "TYPE_CONCENTRATION"; break;
                    default: break;
                }

                GridConcentrations.DataSource = ClCourtierConcentration.GetInstance().ObtenirTousSQLData(trierPar, recherche.Text, filter);
                GridConcentrations.DataBind();
            }

            if (GridConcentrations.Rows.Count > 0 && !showSousConcTable)
            {
                insert_table.Visible = false;
                GridConcentrations.ShowFooter = true;
                GridConcentrations.DataBind();

                BindTypeConcentration((GridConcentrations.FooterRow.FindControl("Type") as DropDownList));
            }
            else
            {
                if (!showSousConcTable)
                {
                    GridConcentrations.Visible = true;
                    labelgroupe.Visible = dropGroupe.Visible = true;
                    labelObjectif.Visible = objectif.Visible = true;

                    BindTypeConcentration(dropGroupe);
                }
                nomConcentration.Text = objectif.Text = String.Empty;
                insert_table.Visible = true;
            }
        }

        protected void GridConcentrations_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridSousConcentration.Visible = false;
            GridConcentrations.PageIndex = e.NewPageIndex;

            GridConcentrations.EditIndex = -1;
            GridConcentrations.SelectedIndex = -1;
        }

        protected void GridSousConcentrations_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridSousConcentration.PageIndex = e.NewPageIndex;

            GridSousConcentration.EditIndex = -1;
            GridSousConcentration.SelectedIndex = -1;
        }

        protected void GridConcentrations_PageIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        protected void GridSousConcentrations_PageIndexChanged(object sender, EventArgs e)
        {
            BindDataGridViewSousConcentration();
        }

        protected void GridConcentrations_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridConcentrations.SelectedIndex = -1;
            GridConcentrations.EditIndex = e.NewEditIndex;
            BindData();
        }

        protected void GridSousConcentrations_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridSousConcentration.SelectedIndex = -1;
            GridSousConcentration.EditIndex = e.NewEditIndex;
            BindDataGridViewSousConcentration();
        }

        protected void GridConcentrations_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridConcentrations.EditIndex = -1;
            BindData();
        }

        protected void GridSousConcentrations_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridSousConcentration.EditIndex = -1;
            BindDataGridViewSousConcentration();
        }

        protected void GridConcentrations_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (GridConcentrations.EditIndex == -1 || GridConcentrations.EditIndex != e.Row.RowIndex)
                {
                    Label type = (Label)e.Row.Cells[3].FindControl("Type");
                    type.Text = Utilitaire.ConcentrationTexteSelonType(ClConcentration.GetTypeConcentration((((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[2]).ToString()));
                }
                else if (GridConcentrations.EditIndex == e.Row.RowIndex)
                {
                    ((TextBox)e.Row.Cells[2].FindControl("txtNomConcentration")).Text = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[1].ToString();

                    DropDownList type = (DropDownList)e.Row.Cells[4].FindControl("Type");

                    BindTypeConcentration(type);
                    object objet = ClConcentration.GetTypeConcentration(((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[2].ToString());
                    type.SelectedValue = objet.ToString();
                }
            }
        }

        protected void GridSousConcentrations_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && GridSousConcentration.EditIndex == e.Row.RowIndex)
                ((TextBox)e.Row.Cells[2].FindControl("txtNomConcentration")).Text = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[1].ToString();
        }

        protected void GridConcentrations_RowCreated(object sender, GridViewRowEventArgs e)
        {
            // Pour chaque rangée de données, à la création de la grille, on attache du js au bouton de 
            //  suppression qui demandera confirmation. On se souvient que la suppression d'une 
            //  caté;rie entraîne la suppression de tous les articles associés 
            //  (à cause du DeleteCascade dans la bd)
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!e.Row.RowState.HasFlag(DataControlRowState.Edit) && e.Row.DataItem != null)
                {
                    string nom = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[1].ToString();
                    Label labelNom = e.Row.Cells[4].FindControl("labelNomConcentration") as Label;
                    labelNom.Text = nom;

                    string objectif = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString();
                    Label labelObjectif = e.Row.Cells[5].FindControl("labelObjectif") as Label;
                    labelObjectif.Text = Utilitaire.CreditsEnTexte(Convert.ToDouble(objectif)) + " $";


                    int typeUser = Convert.ToInt32(((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[4].ToString());
                    if (typeUser != (int)TypeUser.Communaute && typeUser != (int)TypeUser.Partenaire && typeUser != (int)TypeUser.EtudiantExterne)
                        (e.Row.FindControl("Select") as ImageButton).Visible = false;
                }
                else if (e.Row.RowState.HasFlag(DataControlRowState.Edit) && e.Row.DataItem != null)
                {
                    TextBox objectif = e.Row.Cells[5].FindControl("Objectif") as TextBox;
                    objectif.Text = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString();
                }

                ImageButton linkSupprimer = e.Row.FindControl("Delete") as ImageButton;
                if (linkSupprimer != null)
                    linkSupprimer.OnClientClick = ClConcentration.JS_CONFIRMATION_SUPRESSION;
            }
        }

        protected void GridSousConcentration_RowCreated(object sender, GridViewRowEventArgs e)
        {
            // Pour chaque rangée de données, à la création de la grille, on attache du js au bouton de 
            //  suppression qui demandera confirmation. On se souvient que la suppression d'une 
            //  caté;rie entraîne la suppression de tous les articles associés 
            //  (à cause du DeleteCascade dans la bd)
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!e.Row.RowState.HasFlag(DataControlRowState.Edit) && e.Row.DataItem != null)
                {
                    string nom = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[1].ToString();
                    Label labelNom = e.Row.Cells[4].FindControl("labelNomConcentration") as Label;
                    labelNom.Text = nom;

                    string objectif =
                            ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString();
                    Label labelObjectif = e.Row.Cells[5].FindControl("labelObjectif") as Label;
                    labelObjectif.Text = Utilitaire.CreditsEnTexte(Convert.ToDouble(objectif)) + " $";
                }
                else if (e.Row.RowState.HasFlag(DataControlRowState.Edit) && e.Row.DataItem != null)
                {
                    TextBox objectif = e.Row.Cells[5].FindControl("Objectif") as TextBox;
                    objectif.Text = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString();
                }

                ImageButton linkSupprimer = e.Row.FindControl("Delete") as ImageButton;
                if (linkSupprimer != null)
                    linkSupprimer.OnClientClick = ClConcentration.JS_CONFIRMATION_SUPRESSION;
            }
        }

        protected void GridConcentrations_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridConcentrations.SelectedIndex = -1;
            // e.Keys, e.NewValues, and e.OldValues are only populated if using DataSourceID    
            string id = GridConcentrations.DataKeys[e.RowIndex].Value.ToString();

            if (!String.IsNullOrEmpty(id))
            {
                DropDownList type = (DropDownList)GridConcentrations.Rows[GridConcentrations.EditIndex].Cells[4].FindControl("Type");
                TypeConcentration typeConcentration = ClConcentration.GetTypeConcentration(type.SelectedValue);
                TextBox txtObjectif = (TextBox)GridConcentrations.Rows[GridConcentrations.EditIndex].Cells[6].FindControl("Objectif");

                TypeUser typeUser = GetTypeUserSelonTypeConcentration(typeConcentration);

                TextBox txtNom = (TextBox)GridConcentrations.Rows[GridConcentrations.EditIndex].Cells[3].FindControl("txtNomConcentration");

                if (String.IsNullOrEmpty(txtObjectif.Text))
                    txtObjectif.Text = "0";

                int objectif = 0;
                if (typeConcentration != TypeConcentration.NULL && Int32.TryParse(txtObjectif.Text, out objectif))
                {
                    ClConcentration concentration =
                        new ClConcentration(id, txtNom.Text, typeConcentration, Convert.ToInt32(objectif), typeUser);
                    if (!ClCourtierConcentration.GetInstance().Modifier(concentration))
                        labelErreur.Text = Erreur.MISE_A_JOUR;

                    GridConcentrations.EditIndex = -1;
                    BindData();

                    labelErreur.Text = String.Empty;
                }
                else
                    labelErreur.Text = "Concentration non-valide";
            }
        }

        private TypeUser GetTypeUserSelonTypeConcentration(TypeConcentration typeConcentration)
        {
            switch (typeConcentration)
            {
                case TypeConcentration.Communaute:
                    return TypeUser.Communaute;
                case TypeConcentration.Département:
                case TypeConcentration.Service:
                    return TypeUser.CegepEmploye;
                case TypeConcentration.Ecole:
                    return TypeUser.EtudiantExterne;
                case TypeConcentration.Partenaire:
                    return TypeUser.Partenaire;
                case TypeConcentration.ProgrammeAccueilIntegration:
                case TypeConcentration.ProgrammePreUniversitaire:
                case TypeConcentration.ProgrammeTechnique:
                    return TypeUser.CegepEtudiant;
                default:
                    return TypeUser.Invalide;
            }
        }

        protected void GridSousConcentrations_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridSousConcentration.SelectedIndex = -1;
            // e.Keys, e.NewValues, and e.OldValues are only populated if using DataSourceID    
            string id = GridSousConcentration.DataKeys[e.RowIndex].Value.ToString();

            if (!String.IsNullOrEmpty(id))
            {
                TextBox txtObjectif = (TextBox)GridSousConcentration.Rows[GridSousConcentration.EditIndex].Cells[6].FindControl("Objectif");
                TextBox txtNom = (TextBox)GridSousConcentration.Rows[GridSousConcentration.EditIndex].Cells[3].FindControl("txtNomConcentration");

                int objectif = 0;
                if (Int32.TryParse(txtObjectif.Text, out objectif))
                {
                    ClSousConcentration concentration =
                        new ClSousConcentration(id, txtNom.Text, objectif);
                    if (!ClCourtierSousConcentration.GetInstance().Modifier(concentration))
                        labelErreur.Text = Erreur.MISE_A_JOUR;

                    GridSousConcentration.EditIndex = -1;
                    BindDataGridViewSousConcentration();
                    BindData();
                    labelSousConcErreur.Text = String.Empty;
                }
                else
                    labelSousConcErreur.Text = "Concentration non-valide";
            }
        }

        protected void GridConcentrations_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridConcentrations.SelectedIndex = -1;

            string id = GridConcentrations.DataKeys[e.RowIndex].Value.ToString();

            if (ClCourtierCompte.GetInstance().NbCompteParConcentration(id) != 0)
                labelErreur.Text = Erreur.CONCENTRATION_UTILISÉE;
            else
            {
                if (!ClCourtierConcentration.GetInstance().SupprimerSelonId(id))
                    labelErreur.Text = Erreur.SUPPRESSION;
            }

            GridConcentrations.EditIndex = -1;
            BindData();
        }

        protected void GridSousConcentrations_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridSousConcentration.SelectedIndex = -1;
            string id = GridSousConcentration.DataKeys[e.RowIndex].Value.ToString();
            string idParent = GridConcentrations.SelectedDataKey.Value.ToString();

            if (ClCourtierCompte.GetInstance().NbCompteParConcentration(id) != 0)
                labelSousConcErreur.Text = "Sous-concentration utilisée.";
            else if (ClCourtierCompte.GetInstance().NbCompteParConcentration(idParent) != 0)
                labelSousConcErreur.Text = Erreur.CONCENTRATION_UTILISÉE;
            else
            {
                if (!ClCourtierSousConcentration.GetInstance().SupprimerSelonId(id))
                    labelSousConcErreur.Text = Erreur.SUPPRESSION;
            }

            GridSousConcentration.EditIndex = -1;
            BindDataGridViewSousConcentration();
        }

        /// <summary>
        /// Insert records into datbase
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddButon_Click(object sender, EventArgs e)
        {
            if (GridConcentrations.SelectedIndex != -1)
            {
                GridSousConcentration.EditIndex = -1;
                AddSousConc((sender as Button).CommandArgument);
                return;
            }

            GridConcentrations.EditIndex = -1;
            if ((sender as Button).CommandArgument.Equals("EmptyInsert"))
            {
                int i;
                if (Int32.TryParse(objectif.Text, out i) || String.IsNullOrEmpty(objectif.Text))
                {
                    TypeConcentration type_conc = ClConcentration.GetTypeConcentration(dropGroupe.SelectedValue);
                    ClConcentration concentration =
                    new ClConcentration(
                        Guid.NewGuid().ToString(),
                        nomConcentration.Text,
                        type_conc,
                        String.IsNullOrEmpty(objectif.Text) ? 0 : i,
                        GetTypeUserSelonTypeConcentration(type_conc));

                    if (ClCourtierConcentration.GetInstance().Existe(concentration))
                        labelErreur.Text = Erreur.CONCENTRATION_EXISTE;
                    else
                    {
                        labelErreur.Text = String.Empty;
                        if (!ClCourtierConcentration.GetInstance().Insérer(concentration))
                            labelErreur.Text = Erreur.AJOUT;
                    }
                }
                else
                {
                    labelErreur.Text = "Entrer un objectif valide.";
                }
            }
            else if ((sender as Button).CommandArgument.Equals("Insert"))
            {
                TextBox nom = GridConcentrations.FooterRow.FindControl("txtNomConcentration") as TextBox;
                DropDownList typeConcentration = GridConcentrations.FooterRow.FindControl("Type") as DropDownList;
                TypeUser typeUser = GetTypeUserSelonTypeConcentration(ClConcentration.GetTypeConcentration(typeConcentration.SelectedValue));
                TextBox objectif = GridConcentrations.FooterRow.FindControl("Objectif") as TextBox;

                int i;
                if (Int32.TryParse(objectif.Text, out i) || String.IsNullOrEmpty(objectif.Text))
                {
                    ClConcentration concentration =
                    new ClConcentration(
                        Guid.NewGuid().ToString(),
                        nom.Text,
                        ClConcentration.GetTypeConcentration(typeConcentration.SelectedValue),
                        String.IsNullOrEmpty(objectif.Text) ? 0 : i,
                        typeUser);

                    if (ClCourtierConcentration.GetInstance().Existe(concentration))
                        labelErreur.Text = Erreur.CONCENTRATION_EXISTE;
                    else
                    {
                        labelErreur.Text = String.Empty;
                        if (!ClCourtierConcentration.GetInstance().Insérer(concentration))
                            labelErreur.Text = Erreur.AJOUT;
                    }
                }
                else
                {
                    labelErreur.Text = "Entrer un objectif valide.";
                }
            }

            BindData();
        }

        /// <summary>
        /// Show Add new record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddNewRecord(object sender, EventArgs e)
        {
            GridConcentrations.ShowFooter = true;

            BindData();
        }

        /// <summary>
        /// Show Add new record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            labelSousConcErreur.Text = String.Empty;
            GridSousConcentration.Visible = false;

            GridConcentrations.SelectedIndex = -1;
            GridConcentrations.EditIndex = -1;

            GridSousConcentration.EditIndex = -1;
            GridSousConcentration.SelectedIndex = -1;

            BindData();
        }

        protected void GridConcentration_InitModification(object sender, EventArgs e)
        {
            GridConcentrations.EditIndex = -1;

            BindData();
            BindDataGridViewSousConcentration();
        }

        private void BindDataGridViewSousConcentration()
        {
            GridSousConcentration.DataSource =
                ClCourtierSousConcentration.GetInstance().ObtenirTousSQLSelonParent(GridConcentrations.SelectedDataKey.Value.ToString());

            GridSousConcentration.DataBind();

            if (GridSousConcentration.Rows.Count > 0)
            {
                insert_table.Visible = false;
                GridSousConcentration.Visible = true;
                GridSousConcentration.ShowFooter = true;

                GridSousConcentration.DataBind();
            }
            else
            {
                GridSousConcentration.Visible = false;
                insert_table.Visible = true;
                labelgroupe.Visible = dropGroupe.Visible = false;
                labelObjectif.Visible = objectif.Visible = false;
            }

            BindData(GridSousConcentration.Rows.Count == 0);
        }

        /// <summary>
        /// Insert records into datbase
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddSousConc(string command)
        {
            if (command.Equals("EmptyInsert"))
            {
                ClConcentration concentrationParent = ClCourtierConcentration.GetInstance().ObtenirObjetSelonId(GridConcentrations.SelectedDataKey.Value.ToString());
                int i;
                if (Int32.TryParse(objectif.Text, out i) || String.IsNullOrEmpty(objectif.Text))
                {
                    TypeConcentration type_conc = ClConcentration.GetTypeConcentration(dropGroupe.SelectedValue);
                    ClSousConcentration concentration =
                    new ClSousConcentration(
                        Guid.NewGuid().ToString(),
                        nomConcentration.Text,
                        concentrationParent.Concentration,
                        String.IsNullOrEmpty(objectif.Text) ? 0 : Convert.ToInt32(objectif.Text),
                        concentrationParent.TypeUser,
                        GridConcentrations.SelectedDataKey.Value.ToString());

                    if (ClCourtierSousConcentration.GetInstance().Existe(concentration))
                        labelSousConcErreur.Text = Erreur.CONCENTRATION_EXISTE;
                    else
                    {
                        labelSousConcErreur.Text = String.Empty;
                        if (!ClCourtierSousConcentration.GetInstance().Insérer(concentration))
                            labelSousConcErreur.Text = Erreur.AJOUT;
                    }
                }
                else
                {
                    labelSousConcErreur.Text = "Entrer un objectif valide.";
                }
            }
            else if (command.Equals("Insert"))
            {
                TextBox nom = GridSousConcentration.FooterRow.FindControl("txtNomConcentration") as TextBox;
                TextBox objectif = GridSousConcentration.FooterRow.FindControl("Objectif") as TextBox;
                ClConcentration concentrationParent = ClCourtierConcentration.GetInstance().ObtenirObjetSelonId(GridConcentrations.SelectedDataKey.Value.ToString());
                int i;
                if (Int32.TryParse(objectif.Text, out i) || String.IsNullOrEmpty(objectif.Text))
                {
                    ClSousConcentration concentration =
                    new ClSousConcentration(
                        Guid.NewGuid().ToString(),
                        nom.Text,
                        concentrationParent.Concentration,
                        String.IsNullOrEmpty(objectif.Text) ? 0 : Convert.ToInt32(objectif.Text),
                        concentrationParent.TypeUser,
                        concentrationParent.IdConcentration);

                    if (ClCourtierSousConcentration.GetInstance().Existe(concentration))
                        labelSousConcErreur.Text = Erreur.CONCENTRATION_EXISTE;
                    else
                    {
                        labelSousConcErreur.Text = String.Empty;
                        if (!ClCourtierSousConcentration.GetInstance().Insérer(concentration))
                            labelSousConcErreur.Text = Erreur.AJOUT;
                    }
                }
                else
                {
                    labelSousConcErreur.Text = "Entrer un objectif valide.";
                }
            }

            BindData();
            BindDataGridViewSousConcentration();
        }
    }
}