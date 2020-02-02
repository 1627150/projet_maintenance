<%@ Page Title="" Language="C#" MasterPageFile="../SiteAdmin.Master" AutoEventWireup="true" CodeBehind="GestionNousJoindre.aspx.cs" Inherits="CS2013.Administration.Affichage.GestionNousJoindre" ValidateRequest="false" %>

<%@ Register Assembly="CS2013" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="//cdn.ckeditor.com/4.8.0/standard/ckeditor.js"></script>
    <style type="text/css">
        .auto-style1 {
            height: 26px;
        }

        .auto-style2 {
            height: 24px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" validateRequest="false" runat="server">
    <h2>Gérer la page Nous Joindre</h2>
    <br />
    <asp:Label ID="modification" ForeColor="Green" runat="server" Text=""></asp:Label>
    <br />
    <asp:Label ID="labelTitre" runat="server" Text="Titre : " /><br />
    <asp:TextBox ID="Titre" runat="server" Width="600px"></asp:TextBox><br />
    <br />
    <asp:Label ID="labelContenu" runat="server" Text="Contenu : " /><br />

    <asp:TextBox ID="Contenu" runat="server" Height="320px" Width="600px" TextMode="MultiLine" CssClass="asdf"></asp:TextBox><br />
    <script type='text/javascript'>
        CKEDITOR.replace('MainContent_Contenu', {
            toolbar: 'full'
        });
    </script>
    <asp:Button ID="btnValider" runat="server" Text="Enregistrer" OnClick="btnValider_Click" />
    <br />
    <br />
    <asp:Label ID="labelAideHTML" runat="server">Aide pour les balises HTML&nbsp;:&nbsp<a href="https://www.w3schools.com/tags/default.asp">W3Schools</a></asp:Label>

</asp:Content>
