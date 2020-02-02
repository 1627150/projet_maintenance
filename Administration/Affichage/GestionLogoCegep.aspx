<%@ Page Title="" Language="C#" MasterPageFile="../SiteAdmin.Master" AutoEventWireup="true" CodeBehind="GestionLogoCegep.aspx.cs" Inherits="CS2013.Administration.GestionLogoCegep" %>
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
    <h2>Gérer le logo du cégep</h2>
    <br />
    <asp:Label ID="labelErreur" ForeColor="Red" runat="server" />
    <table>
        <tr>
            <td class="auto-style2">
                <asp:Label ID="labelChangerLogo" runat="server" />
            </td>
            <td class="auto-style2">
                <asp:FileUpload ID="fileUploadLogo" runat="server" />
                <br /><br />
                <asp:Label ID="labelStatus" runat="server" />
            </td>
            <td><asp:Button ID="buttonValider" Text="Valider" runat="server" style="float: right" OnClick="ButtonValider_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
