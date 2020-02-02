<%@ Page Title="" Language="C#" MasterPageFile="../SiteAdmin.Master" AutoEventWireup="true" CodeBehind="GestionAfficheAccueil.aspx.cs" Inherits="CS2013.Administration.GestionAfficheAccueil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 26px;
        }
        .auto-style2 {
            height: 24px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Gérer l'affiche d'accueil</h2>
    <br />
    <asp:Label ID="labelErreur" ForeColor="Red" runat="server" />
    <table>
        <tr>
            <td class="auto-style2">
                <asp:Label ID="labelChangerAffiche" runat="server" />
            </td>
            <td class="auto-style2">
                <asp:FileUpload ID="fileUploadAffiche" runat="server" />
                <br /><br />
                <asp:Label ID="labelStatus" runat="server" />
            </td>
            <td><asp:Button ID="buttonValider" Text="Valider" runat="server" style="float: right" OnClick="ButtonValider_Click" />
            </td>
        </tr>
        <tr><td><div style="text-align:left;float:right" class="image_accueil">
                    <img id="Img1" alt="Accueil" src="~/Images/AfficheAccueil.jpg" style="max-width:550px;max-height:350px;width:auto;height:auto;" 
                        runat="server" />
                    <br />
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
