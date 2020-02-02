<%@ Page Title="" Language="C#" MasterPageFile="SiteAdmin.Master" AutoEventWireup="true" CodeBehind="Parametres.aspx.cs" Inherits="CS2013.Parametres" %>
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
    <h2>Paramètres</h2>
    <br />
    <asp:Label ID="labelErreur" ForeColor="Red" runat="server" />
    <table style="text-align:left" class="paramètreForm">
        <tr>
            <td>
                <asp:Label ID="labelInviteFriend" runat="server" />
            </td>
            <td>
                <asp:CheckBox ID="inviteFriend" runat="server" />
            </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="labelCreditsMaxSemaine" runat="server" />
            </td>
            <td>
                <asp:TextBox ID="txtCreditsMaxSemaine" runat="server" Width="50px"/>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="labelCreditsMaxSeance" runat="server" />
            </td>
            <td>
                <asp:TextBox ID="txtCreditsMaxSeance" runat="server" Width="50px"/>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="labelTempsMaxSession" runat="server" />
            </td>
            <td>
                <asp:TextBox ID="txtTempsMaxSession" runat="server" Width="50px"/>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="labelTempsPourNotification" runat="server" />
            </td>
            <td>
                <asp:TextBox ID="txtTempsPourNotification" runat="server" Width="50px"/>
            </td>
        </tr>

        <tr>
            <td class="auto-style2">
                <asp:Label ID="labelMaxSeanceJour" runat="server" />
            </td>
            <td class="auto-style2">
                <asp:TextBox ID="txtMaxSeanceJour" runat="server" Width="50px" />
            </td>
        </tr>
    </table>

    <br /><br />

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
        </tr>
    </table>

    <br /><br />

    <table>
        <tr>
            <td style="vertical-align:text-top" class="auto-style1">
                <asp:Label ID="labelNotification" runat="server" />
            </td>
            <td style="max-width:450px" class="auto-style1">
                <asp:TextBox ID="txtNotification" runat="server" Width="432px" style="margin-top:0px"/>
                <asp:Label ID="notificationInfo" runat="server" style="vertical-align:top;font-size:smaller;white-space:nowrap"/>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="labelDebutBougeotte" runat="server" />
            </td>
            <td>
                <asp:TextBox ID="txtDebutBougeotte" runat="server"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="calDebutBougeotte" runat="server" PopupButtonID="imgDebutBougeotte" TargetControlID="txtDebutBougeotte" />
                <asp:ImageButton ID="imgDebutBougeotte" runat="server" ImageUrl="../Images/icons/calendar.png" Height="25" CssClass="dateButton"/>
            </td>
        </tr> 
        <tr>
            <td>
                <asp:Label ID="labelFinBougeotte" runat="server" />
            </td>
            <td>
                <asp:TextBox ID="txtDateFinBougeotte" runat="server"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="calFinBougeotte" runat="server" PopupButtonID="imgFinBougeotte" TargetControlID="txtDateFinBougeotte" />
                <asp:ImageButton ID="imgFinBougeotte" runat="server" ImageUrl="../Images/icons/calendar.png" Height="25" CssClass="dateButton"/>
            </td>
        </tr>
        <tr align="right">
            <td></td>
            <td align="center"><asp:Button ID="buttonValider" Text="Valider" runat="server" style="float: right" OnClick="ButtonValider_Click" /></td>
        </tr>
    </table>
</asp:Content>
