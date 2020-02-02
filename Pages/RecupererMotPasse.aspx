<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecupererMotPasse.aspx.cs" Inherits="CS2013.RecupererMotPasse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="accueil">
        <h2>
            <asp:Label ID="labelMessageBienvenue" runat="server" CssClass="bienvenue" />
        </h2>

        <asp:Panel ID="formulaireConnexion" runat="server">
            <div>
                <fieldset style="width: 215px">
                    <legend>
                        <asp:Label ID="labelFieldSetConnexion" runat="server" /></legend>
                    <table>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="labelIdentifiant" runat="server" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtCouriel" runat="server" required autofocus="autofocus" Width="100px" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="boutonEnvoyer" OnClick="EnvoyerRecuperation" runat="server" Style="float: right" />
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="labelErreur" runat="server" CssClass="messageErreur" />
                    <p style="margin-left: 0px">
                        <asp:HyperLink ID="retourAccueil" NavigateUrl="~/Default.aspx" runat="server" />
                    </p>
                </fieldset>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
