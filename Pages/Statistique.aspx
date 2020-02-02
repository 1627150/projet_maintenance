<%@ Page Title="" Language="C#" EnableEventValidation="false"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Statistique.aspx.cs" Inherits="CS2013.Statistique" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <asp:Label ID="labelTitre" runat="server" />
    </h2>
    <asp:Panel ID="formulaireStatistique" runat="server" CssClass="statistiques">
        <table id="tableStats" runat="server" class="stats_table">
            
        </table>
        <table class="option_table">
            <tr>
                <td >
                    <fieldset>
                        <legend>Choisir l'une des options suivantes</legend>
                        <div style="position: relative; top: -10px;">
                            <asp:RadioButtonList ID="radioStatistiques" runat="server" OnSelectedIndexChanged="Actualiser"
                                AutoPostBack="true" RepeatDirection="Vertical">
                                <asp:ListItem Text="Statistiques personelles" Value="StatsPerso" />
                                <asp:ListItem Text="Programmes d’études" Value="Programmes" />
                                <asp:ListItem Text="Départements et services" Value="DepartementsServices" />
                                <asp:ListItem Text="Partenaires du Cégep" Value="Partenaires" />
                                <asp:ListItem Text="Communautés extérieures au Cégep" Value="Communautes" />
                                <asp:ListItem Text="Établissements scolaires" Value="Ecole" />
                            </asp:RadioButtonList>
                        </div>
                        <asp:DropDownList ID="dropConcentration" runat="server" AutoPostBack="true" Visible="false" Width="230"
                            OnSelectedIndexChanged="dropConcentration_SelectedIndexChanged" Style="margin-left: 10px" />
                    </fieldset>
                </td>
                <td style="padding: 20px 0 0 70px; vertical-align: top;">
                    <asp:Label ID="labelErreur" CssClass="messageErreur" runat="server" />
                    <asp:Label ID="labelGlobal" runat="server" />
                    <asp:GridView ID="Grid" runat="server" AllowSorting="True" AllowPaging="True" AutoGenerateColumns="False"
                        DataKeyNames="NOM_CONCENTRATION" OnPageIndexChanged="Grid_PageIndexChanged" OnPageIndexChanging="Grid_PageIndexChanging"
                        OnRowCreated="Grid_RowCreated" CellPadding="4" ForeColor="#333333" GridLines="None" EmptyDataRowStyle-ForeColor="Red" CssClass="global_stats">
                        <Columns>
                            <asp:BoundField DataField="NOM_CONCENTRATION" HeaderText="Groupes d’appartenance" ReadOnly="true"
                                Visible="True" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="NB_CREDITS" HeaderText="Nombre de crédits" ReadOnly="true" Visible="false"
                                HeaderStyle-HorizontalAlign="Right" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="OBJECTIF_CREDITS" HeaderText="Objectif" ReadOnly="true"
                                Visible="False" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="Nombre de crédits" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="labelCredits" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Objectif" ItemStyle-Width="60" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="labelObjectif" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="%" ItemStyle-Width="60" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="labelObjectifPourcentage" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle BackColor="#de5900" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#fbf3e6" HorizontalAlign="Center" />
                        <AlternatingRowStyle BackColor="White" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#de5900" ForeColor="White" HorizontalAlign="Center" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>

                    <asp:GridView ID="GridPerso" runat="server" AllowSorting="True" AllowPaging="True" PageSize="20"
                        AutoGenerateColumns="False" DataKeyNames="ID_SEANCE" OnPageIndexChanged="GridPerso_PageIndexChanged"
                        OnRowCreated="GridPerso_RowCreated" OnPageIndexChanging="GridPerso_PageIndexChanging"
                        CellPadding="4" ForeColor="#333333" GridLines="None" EmptyDataRowStyle-ForeColor="Red" Visible="false">
                        <Columns>
                            <asp:BoundField DataField="ID_SEANCE" HeaderText="ID" ReadOnly="true" Visible="false" />
                            <asp:BoundField DataField="DATE" HeaderText="Date" ItemStyle-Width="80" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="INTENSITE" HeaderText="Intensité" ItemStyle-Width="60" Visible="false" />
                            <asp:TemplateField HeaderText="Intensité" ItemStyle-Width="65" Visible="true" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="labelIntensite" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="TEMPS_ACTIVITE" HeaderText="Durée en minutes" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" Visible="true" />
                            <asp:BoundField DataField="NB_CREDITS" HeaderText="Nombre de Crédits $anté<sup>®</sup>" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Right" Visible="false" />
                            <asp:TemplateField HeaderText="Nombre de crédits" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="labelCredits" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle BackColor="#de5900" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#fbf3e6" />
                        <AlternatingRowStyle BackColor="White" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#de5900" ForeColor="White" HorizontalAlign="Center" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                </td>
            </tr>
        </table>

        <br />

        <asp:Panel ID="panelInviteFriend" runat="server">
            <div style="text-align: right;height:50px;">
                <table id="tableCapsule" runat="server" class="stats_table" style="float: right">
                    <tr>
                        <td style="border: none;">
                            <asp:HyperLink ID="inviteFriend" runat="server" Style="text-align: right; cursor: pointer;">Inviter un ami</asp:HyperLink></td>
                    </tr>
                </table>

            </div>

            <div>
                <asp:Panel ID="modalPanel" runat="server" CssClass="modalPanel">
                    <asp:Label ID="labelMessage" runat="server" Text="Inviter un ami" CssClass="modalHeader"></asp:Label>

                    <asp:Label ID="labelDestinataire" runat="server" CssClass="modalTitre" Text="Destinataire :"></asp:Label>
                    <asp:TextBox ID="manualDest" runat="server" TextMode="SingleLine" Width="350"></asp:TextBox>

                    <div style="height: 35px;">
                        <div style="float: right">
                            <asp:Button ID="ok" runat="server" Text="Envoyer" CssClass="modalButton" OnClick="ok_Click" />
                            <asp:Button ID="cancel" runat="server" Text="Annuler" CssClass="modalButton" />
                        </div>
                    </div>
                </asp:Panel>

                <ajaxToolkit:ModalPopupExtender ID="modal" runat="server"
                    PopupControlID="modalPanel"
                    TargetControlID="inviteFriend"
                    BackgroundCssClass="modalBackground"
                    DropShadow="true"
                    CancelControlID="cancel" />
            </div>
        </asp:Panel>
    </asp:Panel>

</asp:Content>
