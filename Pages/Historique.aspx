<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Historique.aspx.cs" Inherits="CS2013.Historique" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <asp:Label ID="labelTitre" runat="server" /></h2>
    <div class="contenu">
            <asp:Label ID="labelDescriptionHistorique" runat="server" />
    </div>
</asp:Content>
