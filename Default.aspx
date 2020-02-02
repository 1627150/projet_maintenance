<%@ Page Title="" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CS2013.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="Scripts/jquery-1.9.1.js"></script>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="accueil">
    <h2>
        <asp:Label ID="labelMessageBienvenue" runat="server" CssClass="bienvenue" />
        <asp:Label ID="labelConnexion" runat="server" CssClass="connexion" />
    </h2>
        
        <asp:Panel ID="formulaireConnexion" runat="server">
            <div>
            <div style="text-align:left;float:right" class="image_accueil">
                <img id="Img1" alt="Accueil" src="~/Images/AfficheAccueil.jpg" style="max-width:550px;max-height:350px;width:auto;height:auto;" 
                runat="server" />
                <br />
            </div>
            <fieldset style="width: 215px">
                <legend>
                    <asp:Label ID="labelFieldSetConnexion" runat="server" /></legend>
                <table>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="labelTitreUtilisateur" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="nomUtilisateur" runat="server" required autofocus="autofocus" Width="100px" />
                        </td>
                        <td>
                            <asp:Label ID="labelNomUtilisateur" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" class="style1">
                            <asp:Label ID="labelTitreMotDePasse" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="motDePasse" TextMode="password" runat="server" required Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:CheckBox ID="rememberMe" runat="server" Text="Rester connecté" Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="boutonConnexion" OnClick="VerifierInfo" runat="server" Style="float: right" />
                        </td>
                    </tr>
                </table>
                <asp:Label ID="labelErreur" runat="server" CssClass="messageErreur" />
                <p style="margin-left: 0px">
                    <asp:Label ID="labelDescriptionNouveauCompte" runat="server" />
                    <asp:HyperLink ID="linkNouveau" NavigateUrl="~/Pages/CreerCompte.aspx" runat="server" />
                    <br />
                    <asp:HyperLink ID="lostPassword" NavigateUrl="~/Pages/RecupererMotPasse.aspx" runat="server" />
                </p>
            </fieldset>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
