<%@ Page Title="" Language="C#" MasterPageFile="../SiteAdmin.Master" AutoEventWireup="true" CodeBehind="GestionOnglets.aspx.cs" Inherits="CS2013.Administration.GestionOnglets" %>
<script runat="server">

</script>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Gérer les onglets à afficher</h2>
    <br />
    <asp:Button id="Button2" runat="server" type="submit" Text="Charger les onglets actifs" OnClick="Load_Activ" />
    <br />
    <asp:Label ID="modification" ForeColor="Green" runat="server" Text=""></asp:Label>
    <br />
    <asp:Label ID="labelErreur" ForeColor="Red" runat="server" />
    <fieldset>
        <legend>Choisir les onglets à afficher</legend>
        <div style="position: relative; top: -10px;">
                <asp:CheckBoxList ID="CheckBoxOnglet" runat="server">
                    <asp:ListItem ID="onglet1" runat="server" text="Capsules Santés" />
                    <asp:ListItem ID="onglet2" runat="server" text="Statistiques" />
                    <asp:ListItem ID="onglet3" runat="server" text="Historique" />
                    <asp:ListItem ID="onglet4" runat="server" text="Photos" />
                    <asp:ListItem ID="onglet5" runat="server" text="Nous joindre" />
                    <asp:ListItem ID="onglet6" runat="server" text="Evenements" />
                </asp:CheckBoxList>
            <asp:Button id="submitButton" runat="server" type="submit" Text="Valider" OnClick="Change_Value" />
        </div>
    </fieldset>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

