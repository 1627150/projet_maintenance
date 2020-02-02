<%@ Page Title="" Language="C#" MasterPageFile="SiteAdmin.Master" EnableEventValidation="false" CodeBehind="Affichage.aspx.cs" Inherits="CS2013.Affichage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Affichage</h2>
    <br />
    <asp:Label ID="labelErreur" ForeColor="Red" runat="server" />
    
    <asp:HyperLink ID="LinkGererAfficheAccueil" NavigateUrl="~/Administration/Affichage/GestionAfficheAccueil.aspx" runat="server">Gérer l'affiche d'accueil</asp:HyperLink>
    <br />
    <asp:HyperLink ID="LinkGererLogoCegep" NavigateUrl="~/Administration/Affichage/GestionLogoCegep.aspx" runat="server">Gérer le logo du cégep</asp:HyperLink>
    <br />
    <asp:HyperLink ID="LinkGererPhotosBougeotte" NavigateUrl="~/Administration/Affichage/GestionPhotosBougeotte.aspx" runat="server">Gérer les photos de la bougeotte</asp:HyperLink>
    <br />
    <asp:HyperLink ID="LinkGererStatistiques" NavigateUrl="~/Administration/Affichage/GestionStatistiques.aspx" runat="server">Gérer les options de statistiques à afficher</asp:HyperLink>
    <br />
    <asp:HyperLink ID="LinkGererHistorique" NavigateUrl="~/Administration/Affichage/GestionHistorique.aspx" runat="server">Gérer la page historique</asp:HyperLink>
    <br />
    <asp:HyperLink ID="LingGererNousJoindre" NavigateUrl="~/Administration/Affichage/GestionNousJoindre.aspx" runat="server">Gérer la page nous joindre</asp:HyperLink>
    <br />
    <asp:HyperLink ID="LinkGererOnglets" NavigateUrl="~/Administration/Affichage/GestionOnglets.aspx" runat="server">Gérer les onglets à afficher</asp:HyperLink>
    <br />
</asp:Content>
