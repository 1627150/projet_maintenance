<%@ Page Title="" Language="C#" MasterPageFile="../SiteAdmin.Master" AutoEventWireup="true" CodeBehind="GestionStatistiques.aspx.cs" Inherits="CS2013.Administration.GestionStatistiques" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Gérer les options de statistiques à afficher</h2>
    <br />
    <asp:Label ID="modification" ForeColor="Green" runat="server" Text=""></asp:Label>
    <br />
    <asp:Label ID="labelErreur" ForeColor="Red" runat="server" />
    <fieldset>
        <legend>Choisir les statistiques à afficher</legend>
        <div style="position: relative; top: -10px;">
            <asp:CheckBoxList ID="radioAffichageStats" runat="server" RepeatDirection="Vertical">
                <asp:ListItem Text="Total de la bougeote pour le Cegep" Value="totalBougeotteCegep" />
                <asp:ListItem Text="Total de la bougeote" Value="totalBougeotte" />
                <asp:ListItem Text="Total annuel du Cégep" Value="totalAnnuelCegep" />
            </asp:CheckBoxList>
        </div>
        <asp:Button ID="buttonValiderStats" Text="Valider" runat="server" Style="float: left" OnClick="buttonValiderStats_Click" />
    </fieldset>
    <fieldset>
        <legend>Choisir les options à afficher</legend>
        <div style="position: relative; top: -10px;">
            <asp:CheckBoxList ID="radioStatistiques" runat="server" RepeatDirection="Vertical">
                <asp:ListItem Text="Statistiques personelles" Value="StatsPerso" />
                <asp:ListItem Text="Programmes d’études" Value="Programmes" />
                <asp:ListItem Text="Départements et services" Value="DepartementsServices" />
                <asp:ListItem Text="Partenaires du Cégep" Value="Partenaires" />
                <asp:ListItem Text="Communautés extérieures au Cégep" Value="Communautes" />
                <asp:ListItem Text="Établissements scolaires" Value="Ecole" />
            </asp:CheckBoxList>
        </div>
        <asp:Button ID="buttonValider" Text="Valider" runat="server" Style="float: left" OnClick="buttonValider_Click" />
    </fieldset>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
