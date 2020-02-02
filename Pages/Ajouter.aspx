<%@ Page Title="" Language="C#" Culture="fr-CA" UICulture="fr-CA" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ajouter.aspx.cs" Inherits="CS2013.Ajouter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div id="ajout">
        <h2>
            <asp:Label ID="labelTitre" runat="server" /></h2>

        <table id="tableCapsule" runat="server" class="stats_table" style="float: right">
            <tr>
                <td style="border: none;">
                    <asp:HyperLink ID="linkCapsule" runat="server" Style="text-align: right;"></asp:HyperLink></td>
            </tr>
        </table>

        <div style="float: right; margin-top: 50px; margin-left: 0.500em; clear: both; width: 45%;">
            <img id="tableau" runat="server" class="tableau_calcul" src="../Images/intensite/intensite.jpg" alt="Tableau de calcul de l'intensité" />
            <asp:Label ID="imgDef" runat="server" Style="clear: both;">Le tableau de droite indique l'intensité d'entraînement en fonction de la fréquence cardiaque et de l'âge.</asp:Label>
        </div>

        <asp:Panel ID="formulaireAjouterCredit" runat="server" Style="width: 450px;">
            <h3>
                <asp:Label ID="labelMessageTotalCredits" runat="server" Font-Bold="false" />
                <asp:Label ID="labelTotalCredits" runat="server" Font-Bold="false" />
            </h3>
            <h3>
                <asp:Label ID="labelMessageTotalBougeotte" runat="server" Font-Bold="false" />
            </h3>
            <h3>
                <asp:Label ID="labelMessageObjectifConcentration" runat="server" Font-Bold="false" />
                <asp:Label ID="labelObjectifConcentration" runat="server" Font-Bold="false" />
            </h3>
            <h3>
                <asp:Label ID="labelMessageObjectifPersonnel" runat="server" Font-Bold="false" />
                <asp:Label ID="labelObjectifPersonnel" runat="server" Font-Bold="false" />
            </h3>
            <asp:Label ID="labelErreurMaximumAtteint" runat="server" CssClass="messageErreur" />
            <fieldset style="padding: 0px; max-width: 450px">
                <legend>
                    <asp:Label ID="labelFieldSetAjout" runat="server" /></legend>

                <table style="border-bottom: 1px solid #909090; padding: 5px; width: 100%">
                    <asp:Label ID="labelTemps" runat="server" CssClass="messageErreur" Style="white-space: nowrap; position: relative; left: 18px;" />
                    <asp:Label ID="labelIntensiteErreur" runat="server" CssClass="messageErreur" Style="white-space: nowrap; position: relative; left: 18px;" />
                    <asp:Label ID="labelDateErreur" runat="server" CssClass="messageErreur" Style="white-space: nowrap; position: relative; left: 18px;" />
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="labelDate" runat="server" Style="padding-left: 10px" /><asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender
                                ID="calendar"
                                runat="server"
                                PopupButtonID="dateButton"
                                TargetControlID="txtDate"
                                ViewStateMode="Enabled"
                                EnableViewState="true" />
                            <asp:ImageButton ID="dateButton" runat="server" ImageUrl="../Images/icons/calendar.png" Height="25" CssClass="dateButton" />

                            <p>
                                Entrez la quantité d'activité à l'aide de <b>l'un des deux outils<br />
                                    suivants</b>, puis validez.
                            </p>
                            <asp:Button ID="ButtonValider" OnClick="ValiderCredits" Text="Valider" runat="server"
                                Style="float: right" />
                        </td>
                    </tr>
                </table>
                <table style="background-color: #F0F0F0; border-bottom: 1px solid #909090; padding: 5px; width: 100%">
                    <tr>
                        <td colspan="2">
                            <h2>Crédit $anté</h2>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 170px;">
                            <asp:Label ID="labelNbCredits" runat="server" Style="padding-left: 10px; white-space: nowrap" />
                        </td>
                        <td>
                            <asp:TextBox ID="textNbCredits" runat="server" Width="35" MaxLength="4"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table style="background-color: #E0E0E0; padding: 5px; width: 100%">
                    <tr>
                        <td colspan="2">
                            <h2>Durée et intensité</h2>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100px">
                            <asp:Label ID="labelTempsMinutes" runat="server" Style="padding-left: 10px; white-space: nowrap" />
                        </td>
                        <td>
                            <asp:TextBox ID="textMinutes" runat="server" Width="35" MaxLength="4"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <asp:UpdatePanel ID="intensiteUP" runat="server">
                    <ContentTemplate>
                        <table>

                            <tr>
                                <td style="width: 100px; padding-left: 10px;">
                                    <asp:Label ID="labelIntensite" runat="server" Text="Intensité" />
                                </td>
                                <td colspan="2" style="padding-left: 10px;">
                                    <asp:Label ID="labelDefinition" runat="server" Text="Définition de l'intensité" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 85px">
                                    <asp:RadioButtonList ID="radioIntensite" runat="server" AutoPostBack="true" OnSelectedIndexChanged="MettreAJourDescriptionRadio">
                                        <asp:ListItem Value="1" Text="Faible" />
                                        <asp:ListItem Value="2" Text="Modérée" />
                                        <asp:ListItem Value="3" Text="Élevée" />
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td style="height: 12px;">
                                                <asp:Label ID="labelDescription1" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 12px;">
                                                <asp:Label ID="labelDescription2" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 12px;">
                                                <asp:Label ID="labelDescription3" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Label ID="labelDescription" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </fieldset>
        </asp:Panel>
    </div>
</asp:Content>
