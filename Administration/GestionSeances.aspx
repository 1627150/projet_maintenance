<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="SiteAdmin.Master" AutoEventWireup="true" CodeBehind="GestionSeances.aspx.cs" Inherits="CS2013.GestionSeances" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Supression de séances d'entrainement</h2>
    <br />
    <asp:Label ID="labelErreur" ForeColor="Red" runat="server" />
    <asp:Label ID="labelConfirm" ForeColor="Green" runat="server" />
    <p>Effacer les <b>toutes</b> les entrées de <i>Crédit $anté<sup>®</sup></i></p>
    <asp:Button ID="buttonResetAllData" runat="server" Text="Tout Effacer" OnClick="buttonResetData_OnClick" CommandArgument="all"/>

    <p>Effacer les entrées de <i>Crédit $anté<sup>®</sup></i> <b>par groupe d'appartenance</b>.</p>
    <asp:DropDownList ID="dropDownTypeConcentration" runat="server" />
    <asp:Button ID="buttonResetData" runat="server" Text="Effacer" OnClick="buttonResetData_OnClick" CommandArgument="concentration"/>
    
</asp:Content>
