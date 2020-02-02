<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CapsulesSante.aspx.cs" Inherits="CS2013.CapsulesSante" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <asp:Label ID="labelTitre" runat="server" /></h2>
    <p>
        <asp:Label ID="labelTexte" runat="server" />
    </p>
    <div id="maincontent" runat="server" class="capsules_sante"></div>
</asp:Content>
