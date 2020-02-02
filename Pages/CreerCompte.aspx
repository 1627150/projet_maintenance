<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/Site.Master" CodeBehind="CreerCompte.aspx.cs" Inherits="CS2013.CreerCompte" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>Créer un compte
    </h2>
    <td>
        <p><asp:Label ID="labelInfo" runat="server" /></p>
        <asp:Label ID="requiredFields" runat="server" CssClass="messageErreur" />
    </td>
    <asp:Panel ID="formulaireCréerCompte" runat="server">
        <fieldset>
            <legend>Informations</legend>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="labelTypeUtilisateur" runat="server" />
                    </td>
                    <td>
                        <asp:DropDownList ID="typeUtilisateur" runat="server" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="typeUtilisateur_SelectedIndexChanged" />
                    </td>
                    <td>
                        <asp:Label ID="labelTypeUtilisateurErreur" runat="server" CssClass="messageErreur" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="labelTitreDepartement" runat="server" />
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownDepartements" runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="departement_SelectedIndexChanged" />
                    </td>
                    <td>
                        <asp:Label ID="labelDepartement" runat="server" CssClass="messageErreur" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="labelTitreSousDepartement" runat="server" Visible="false" />
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownSousDepartements" runat="server" AutoPostBack="true" Width="300px" Visible="false" />
                    </td>
                    <td>
                        <asp:Label ID="labelSousDepartement" runat="server" CssClass="messageErreur" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="labelTitreUtilisateur" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="nomUtilisateur" runat="server" Width="150px" required />
                    </td>
                    <td>
                        <asp:Label ID="labelNomUtilisateur" runat="server" CssClass="messageErreur" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="labelTitreMotDePasse" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="motDePasse" TextMode="password" Width="150px" runat="server" required />
                    </td>
                    <td>
                        <asp:Label ID="labelMotDePasse" runat="server" CssClass="messageErreur" Text="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="labelTitreConfirmerMDP" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="confirmationMotDePasse" TextMode="password" Width="150px" runat="server" required />
                    </td>
                    <td>
                        <asp:Label ID="labelConfirmationMotDePasse" runat="server" CssClass="messageErreur"
                            Text="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="labelNom" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="nom" runat="server" Width="250px" />
                    </td>
                    <td>
                        <asp:Label ID="labelErreurNom" runat="server" CssClass="messageErreur" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="labelEmail" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="email" runat="server" Width="250px" />
                    </td>
                    <td>
                        <asp:Label ID="labelErreurEmail" runat="server" CssClass="messageErreur" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="labelConfirmmail" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="emailConfirmation" runat="server" Width="250px" />
                    </td>
                    <td>
                        <asp:Label ID="labelErreurEmailConfirmation" runat="server" CssClass="messageErreur" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="labelNumCell" runat="server" Text="Numéro de cellulaire : "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="numeroCell" runat="server" MaxLength="15"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="labelFournisseur" runat="server" Text="Fournisseur de service : "></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="dropFournisseur" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="ButtonCréerCompte" OnClick="CréerCompte" Text="Valider" runat="server"
                            Style="float: right" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
</asp:Content>
