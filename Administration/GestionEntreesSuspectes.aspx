<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="SiteAdmin.Master" AutoEventWireup="true" CodeBehind="GestionEntreesSuspectes.aspx.cs" Inherits="CS2013.GestionEntreesSuspectes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Gestion des entrées suspectes</h2>
    <p>
        Cette page sert à gérer les entrées suspectes détectées par le système.
    </p>
    <br />
    <asp:Label ID="labelErreur" ForeColor="Red" runat="server" />
    <asp:UpdatePanel ID="GridUpdate" runat="server">
        <ContentTemplate>
            <asp:GridView ID="GridEntreesSuspectes" runat="server" AllowSorting="True" AllowPaging="True"
                AutoGenerateColumns="False" PageSize="15" DataKeyNames="ID_SEANCE" OnPageIndexChanged="GridEntreesSuspectes_PageIndexChanged"
                OnPageIndexChanging="GridEntreesSuspectes_PageIndexChanging" OnRowCancelingEdit="GridEntreesSuspectes_RowCancelingEdit"
                OnRowCreated="GridEntreesSuspectes_RowCreated" OnRowDeleting="GridEntreesSuspectes_RowDeleting"
                OnRowEditing="GridEntreesSuspectes_RowEditing" OnRowUpdating="GridEntreesSuspectes_RowUpdating"
                Width="100%"
                CellPadding="4" EmptyDataRowStyle-ForeColor="Red" ForeColor="#333333" GridLines="None">
                <Columns>
                    <asp:BoundField DataField="ID_UTILISATEUR" HeaderText="Utilisateur" ReadOnly="true"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70" />
                    <asp:BoundField DataField="ID_SEANCE" HeaderText="ID" ReadOnly="true" Visible="false"
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText="" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <div id="modalWindow" runat="server">
                                <asp:Panel ID="modalPanel" runat="server" CssClass="modalPanel">
                                    <asp:Label ID="labelMessage" runat="server" Text="Envoyer un message" CssClass="modalHeader"></asp:Label>

                                    <asp:Label ID="labelDestinataire" runat="server" CssClass="modalTitre" Text="Destinataire : "></asp:Label>
                                    <asp:TextBox ID="destinataire" runat="server" CssClass="modalDestinataire" Width="250"></asp:TextBox>

                                    <p class="modalTitre">Titre</p>
                                    <asp:TextBox ID="titreMessage" runat="server" TextMode="SingleLine" Width="350"></asp:TextBox>

                                    <p class="modalTitre">Contenu</p>
                                    <asp:TextBox ID="message" runat="server" TextMode="MultiLine" Height="100" Width="350"></asp:TextBox>

                                    <div style="height: 35px;">
                                        <div style="float: right">
                                            <asp:Button ID="ok" runat="server" Text="Envoyer" CssClass="modalButton"
                                                OnClick="sendMail" PostBackUrl="~/Administration/GestionEntreesSuspectes.aspx" />
                                            <asp:Button ID="cancel" runat="server" Text="Annuler" CssClass="modalButton" />
                                        </div>
                                    </div>
                                </asp:Panel>

                                <ajaxToolkit:ModalPopupExtender ID="modal" runat="server"
                                    TargetControlID="email"
                                    PopupControlID="modalPanel"
                                    BackgroundCssClass="modalBackground"
                                    DropShadow="true"
                                    CancelControlID="cancel" />

                                <asp:LinkButton ID="email" runat="server" />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="INTENSITE" HeaderText="Intensité" ReadOnly="true" ItemStyle-Width="70"
                        ItemStyle-HorizontalAlign="Center" Visible="false" />
                    <asp:TemplateField HeaderText="Intensité" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="labelIntensite" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="TEMPS_ACTIVITE" HeaderText="Durée en minutes" ReadOnly="true"
                        ItemStyle-Width="130" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="NB_CREDITS" HeaderText="Nombre de crédits" ReadOnly="true"
                        ItemStyle-Width="130" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="DATE" HeaderText="Date" HeaderStyle-HorizontalAlign="Center"
                        ReadOnly="true" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:MM-dd-yyyy}" />



                    <asp:TemplateField HeaderText="Objectif" ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="Accept" runat="server" ImageUrl="~/Images/Icons/Accept.png" Width="15" CommandName="Edit" ToolTip="Acceper" />
                            <asp:ImageButton ID="Delete" runat="server" ImageUrl="~/Images/icons/delete-icon.png" Width="15" CommandName="Delete" ToolTip="Supprimer" />
                            <asp:ImageButton ID="SendEmail" runat="server" ImageUrl="~/Images/Icons/email.jpg" Width="15" CommandName="Insert" ToolTip="Envoyer un courriel" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:ImageButton ID="Validate" runat="server" ImageUrl="~/Images/Icons/Accept.png" Width="15" CommandName="Delete" ToolTip="Valider" />
                            <asp:ImageButton ID="CancelValidate" runat="server" ImageUrl="~/Images/Icons/cancel.png" Width="15" CommandName="Cancel" ToolTip="Annuler" />
                        </EditItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <HeaderStyle BackColor="#de5900" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#fbf3e6" />
                <AlternatingRowStyle BackColor="White" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <EditRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#de5900" ForeColor="White" HorizontalAlign="Center" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
