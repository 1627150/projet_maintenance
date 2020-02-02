<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Compte.aspx.cs" Inherits="CS2013.Compte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="../Scripts/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-1.9.1.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="account">
        <h2>
            <asp:Label ID="labelMessageModficationCompte" runat="server" />
        </h2>
        <div>
            <p>
                Veuillez entrer les modifications à apporter à votre compte.
            </p>
            <p>
                Il est possible de modifier vos informations sans changer votre mot de passe.
            </p>
            <asp:Label ID="suppInfo" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <asp:Panel ID="formulaireEditionCompte" runat="server" class="formulaire">
            <fieldset>
                <legend>
                    <asp:Label ID="labelFieldSetModificationCompte" runat="server" /></legend>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="labelTitreUtilisateur" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="nomUtilisateur" runat="server" Enabled="false" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="labelTitreAncienMotDePasse" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="ancienMotDePasse" TextMode="password" runat="server" autofocus="autofocus" />
                        </td>
                        <td>
                            <asp:Label ID="labelAncienMotDePasse" runat="server" CssClass="messageErreur" Text="" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="labelTitreNouveauMotDePasse" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="nouveaMotDePasse" TextMode="password" runat="server" />
                        </td>
                        <td>
                            <asp:Label ID="labelMotDePasse" runat="server" CssClass="messageErreur" Text="" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="labelTitreConfirmationNouveauMotDePasse" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="confirmationNouveauMotDePasse" TextMode="password" runat="server" />
                        </td>
                        <td>
                            <asp:Label ID="labelConfirmationNouveauMotDePasse" runat="server" CssClass="messageErreur"
                                Text="" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="labelDépartement" runat="server" />
                        </td>
                        <td>
                            <asp:DropDownList ID="dropDownListDepartement" DataTextField="NomConcentration" DataValueField="IdConcentration" runat="server" AutoPostBack="true" required OnSelectedIndexChanged="dropDownListDepartement_SelectedIndexChanged" Width="250" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="labelSousConc" runat="server" />
                        </td>
                        <td>
                            <asp:DropDownList ID="dropSousConc" runat="server" required />
                        </td>
                        <td></td>
                    </tr>
                    
                    <tr>
                        <td>
                            <asp:Label ID="labelOldEmail" runat="server"  />
                        </td>
                        <td>
                            <asp:TextBox ID="oldEmail" runat="server" Width="250px" Enabled="false" />
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
                            <asp:Label ID="labelEmailErreur" runat="server" CssClass="messageErreur"
                                Text="" />
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
                            <asp:Label ID="labelNom" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="nom" runat="server" Width="250px" />
                        </td>
                        <td>
                            <asp:Label ID="labelNomErreur" runat="server" CssClass="messageErreur"
                                Text="" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="labelNumCell" runat="server" Text="Numéro de cellulaire : "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="numeroCell" runat="server" MaxLength="15"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="labelNumeroCellErreur" runat="server" CssClass="messageErreur"
                                Text="" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="labelFournisseur" runat="server" Text="Fournisseur de service : "></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="dropFournisseur" runat="server"></asp:DropDownList>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="boutonModification" runat="server" Style="float: right" OnClick="VerifierInfo" />
                        </td>
                        <td></td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    </div>
</asp:Content>
