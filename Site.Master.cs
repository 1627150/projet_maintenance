using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CS2013;
using System.Globalization;
using System.Threading;

public partial class Site : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ChargerPage();
            Utilitaire.AffecterParametres(Session);
            GetCookie();
        }
        bool valid = true;
        FilDAriane.Visible = false;
        // Menu Pour tous
        if (Session["utilisateur"] == null)
            NavigationMenu.Items.Add(new MenuItem("Accueil", "", "", "~/Default.aspx"));
        else
            NavigationMenu.Items.Add(new MenuItem("Accueil", "", "", "~/Pages/Ajouter.aspx"));

        ClCompte utilisateurCourant = (ClCompte)Session["utilisateur"];

        if (utilisateurCourant != null)
        {
            FilDAriane.Visible = true;
            NavigationMenu.Items[0].Text = "Ajout de <i>Crédit $anté</i>";
            linkCompte.Visible = linkDeDeconnexion.Visible = true;
            linkCompte.Text = Constantes.COMPTE + " : " + (String.IsNullOrEmpty(utilisateurCourant.Nom) ? utilisateurCourant.IdUtilisateur : utilisateurCourant.Nom);

            // Menu Administrateur
            if (utilisateurCourant.Role == ClCompte.TypeCompte.Administrateur)
                NavigationMenu.Items.Add(new MenuItem("Administration", "administration", "", "~/Administration/Gestion.aspx"));

            valid = ClCourtierOnglets.ObtenirValidation("Capsules Santés");
            if (valid)
            {
                NavigationMenu.Items.Add(new MenuItem("<i>Capsules Santé</i>", "capsules", "", "~/Pages/CapsulesSante.aspx"));
            }

            valid = ClCourtierOnglets.ObtenirValidation("Statistiques");
            if (valid)
            {
                NavigationMenu.Items.Add(new MenuItem("Statistiques", "stats", "", "~/Pages/Statistique.aspx"));
            }
        }

        valid = ClCourtierOnglets.ObtenirValidation("Historique");
        if (valid)
        {
            NavigationMenu.Items.Add(new MenuItem("Historique", "historique", "", "~/Pages/Historique.aspx"));
        }

        valid = ClCourtierOnglets.ObtenirValidation("Photos");
        if (valid)
        {
            NavigationMenu.Items.Add(new MenuItem("Photos", "photos", "", "~/Pages/Photos.aspx"));
        }

        valid = ClCourtierOnglets.ObtenirValidation("Nous joindre");
        if (valid)
        {
            NavigationMenu.Items.Add(new MenuItem("Nous joindre", "contact", "", "~/Pages/NousJoindre.aspx"));
        }

        valid = ClCourtierOnglets.ObtenirValidation("Evenements");
        if (valid)
        {
            NavigationMenu.Items.Add(new MenuItem("Evenements", "event", "", "~/Pages/Evenement.aspx"));
        }

        SelectionnerPageActive();

        //RedirigerAuBesoin();
    }

    private void RedirigerAuBesoin()
    {
        if (Path.GetFileName(Request.PhysicalPath).Equals("Confirmation.aspx")
            || Path.GetFileName(Request.PhysicalPath).Equals("CreerCompte.aspx")
            || Path.GetFileName(Request.PhysicalPath).Equals("Deconnexion.aspx")
            || Path.GetFileName(Request.PhysicalPath).Equals("DroitsInsufisants.aspx")
            || Path.GetFileName(Request.PhysicalPath).Equals("FinSession.aspx")
            || Path.GetFileName(Request.PhysicalPath).Equals("Historique.aspx")
            || Path.GetFileName(Request.PhysicalPath).Equals("NousJoindre.aspx")
            || Path.GetFileName(Request.PhysicalPath).Equals("Photos.aspx")
            || Path.GetFileName(Request.PhysicalPath).Equals("Default.aspx"))
            return;
        else if ((ClCompte)Session["utilisateur"] == null)
            Response.Redirect("~/Default.aspx");
        else if ((Path.GetFileName(Request.PhysicalPath).Equals("Gestion.aspx")
            || Path.GetFileName(Request.PhysicalPath).Equals("CreerCompte.aspx")
            || Path.GetFileName(Request.PhysicalPath).Equals("CreerCompte.aspx")
            || Path.GetFileName(Request.PhysicalPath).Equals("CreerCompte.aspx")
            || Path.GetFileName(Request.PhysicalPath).Equals("CreerCompte.aspx")
            || Path.GetFileName(Request.PhysicalPath).Equals("CreerCompte.aspx")
            || Path.GetFileName(Request.PhysicalPath).Equals("CreerCompte.aspx"))
            && (Session["utilisateur"] as ClCompte).Role != ClCompte.TypeCompte.Administrateur)
            Response.Redirect("~/Pages/DroitsInsufisants.aspx");

    }

    private void SelectionnerPageActive()
    {
        NavigationMenu.Items[0].Selected = true;
        foreach (MenuItem item in NavigationMenu.Items)
        {
            string i = Path.GetFileName(item.NavigateUrl);
            string g = Path.GetFileName(Request.PhysicalPath);

            if ((Path.GetFileName(item.NavigateUrl).Equals(Path.GetFileName(Request.PhysicalPath),
                StringComparison.InvariantCultureIgnoreCase))
               || Path.GetFileName(item.NavigateUrl).Equals("Default.aspx") && Path.GetFileName(Request.PhysicalPath).Equals("Ajouter.aspx")
               || Path.GetFileName(item.NavigateUrl).Equals("Gestion.aspx") && Path.GetFileName(Request.PhysicalPath).Equals("GestionConcentration.aspx")
               || Path.GetFileName(item.NavigateUrl).Equals("Gestion.aspx") && Path.GetFileName(Request.PhysicalPath).Equals("GestionCapsulesSante.aspx")
               || Path.GetFileName(item.NavigateUrl).Equals("Gestion.aspx") && Path.GetFileName(Request.PhysicalPath).Equals("GestionEntreesSuspectes.aspx")
               || Path.GetFileName(item.NavigateUrl).Equals("Gestion.aspx") && Path.GetFileName(Request.PhysicalPath).Equals("GestionSeances.aspx")
               || Path.GetFileName(item.NavigateUrl).Equals("Gestion.aspx") && Path.GetFileName(Request.PhysicalPath).Equals("GestionUtilisateur.aspx")
               || Path.GetFileName(item.NavigateUrl).Equals("Gestion.aspx") && Path.GetFileName(Request.PhysicalPath).Equals("Parametres.aspx"))
            {
                item.Selected = true;
                break;
            }
        }
    }

    private void ChargerPage()
    {
        linkButtonBougeotte.Text = Constantes.MASTER_TITRE;
        litInitiative.Text = "Une initiative du département d'éducation physique";
        litSupport.Text = "Avec le soutien financier des Services à la vie étudiante et à la communauté";
        linkDeDeconnexion.Text = Constantes.DECONNEXION;
        if (File.Exists(Server.MapPath("/Images/LogoCegep.jpg")))
            logoCegep.ImageUrl = "/Images/LogoCegep.jpg";
        if (File.Exists(Server.MapPath("/Images/LogoCegep.png")))
            logoCegep.ImageUrl = "/Images/LogoCegep.png";
    }

    protected void LinkButtonBougeotte_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }

    protected void GetCookie()
    {
        HttpCookie cookie = Request.Cookies["UserSettings"];

        if (cookie != null && Session["utilisateur"] == null)
        {
            string user = cookie["User"];

            if (Path.GetFileName(Request.PhysicalPath).Equals("Deconnexion.aspx"))
                return;

            Session["utilisateur"] = ClCourtierCompte.GetInstance().ObtenirCompteSelonId(user);
            //Response.Redirect("~/Pages/Ajouter.aspx");
        }
    }
}