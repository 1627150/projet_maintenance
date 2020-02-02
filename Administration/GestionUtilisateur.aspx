<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="SiteAdmin.Master" AutoEventWireup="true" CodeBehind="GestionUtilisateur.aspx.cs" Inherits="CS2013.GestionUtilisateur" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Gestion des utilisateurs</h2>
    <p>
        Cette page permet de gérer les types d'accès des utilisateur au système de <i>Crédit $anté<sup>®</sup></i>.
    </p>
    <p>
        Elle permet aussi de supprimer les séances d'entraînements des utilisateurs.
    </p>
    <p>
        <b>Utilisez cette page avec précautions.</b>
    </p>
    <br />

    <div style="padding-bottom: 10px">
        Recherche : 
        <asp:DropDownList ID="filterDropDownList" runat="server"></asp:DropDownList>
        <asp:TextBox ID="recherche" runat="server"></asp:TextBox>
        <asp:Button ID="rechercherButton" runat="server" OnClick="Rechercher" Text="Rechercher"></asp:Button>
    </div>

    <div style="padding-bottom: 10px">
        Trier par : 
        <asp:DropDownList ID="sortByDropDownList" runat="server"></asp:DropDownList>
        <asp:Button ID="trierButton" runat="server" OnClick="Rechercher" Text="Trier"></asp:Button>
    </div>

    <asp:UpdatePanel ID="GridUpdate" runat="server">
        <ContentTemplate>
            <asp:Label ID="labelErreur" ForeColor="Red" runat="server" />
            <asp:GridView ID="GridUtilisateur" runat="server" AllowSorting="True" AllowPaging="True" Width="100%"
                AutoGenerateColumns="False" PageSize="10" DataKeyNames="ID_UTILISATEUR" OnPageIndexChanged="GridUtilisateur_PageIndexChanged"
                OnPageIndexChanging="GridUtilisateur_PageIndexChanging" OnRowCancelingEdit="GridUtilisateur_RowCancelingEdit"
                OnRowCreated="GridUtilisateur_RowCreated" OnRowDeleting="GridUtilisateur_RowDeleting"
                OnRowEditing="GridUtilisateur_RowEditing" OnRowUpdating="GridUtilisateur_RowUpdating"
                OnRowDataBound="GridUtilisateur_RowDataBound" OnSelectedIndexChanged="GridUtilisateur_InitModification"
                CellPadding="4" ForeColor="#333333" GridLines="None" EmptyDataRowStyle-ForeColor="Red">
                <Columns>
                    <asp:BoundField DataField="ID_UTILISATEUR" HeaderText="Utilisateur" ReadOnly="true" />
                    <asp:BoundField DataField="ID_CONCENTRATION" HeaderText="Concentration" ReadOnly="true" Visible="false" />
                    <asp:BoundField DataField="EMAIL" HeaderText="Utilisateur" ReadOnly="true" Visible="false" />
                    <asp:BoundField DataField="NOM" HeaderText="Utilisateur" ReadOnly="true" Visible="false" />
                    <asp:BoundField DataField="EMAIL_TELEPHONE" HeaderText="Texto" ReadOnly="true" Visible="false" />

                    <asp:TemplateField HeaderText="Nom" ItemStyle-Width="70">
                        <ItemTemplate>
                            <asp:Label ID="Nom" runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="Nom" runat="server" TextMode="SingleLine" Width="70"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Courriel" ItemStyle-Width="70">
                        <ItemTemplate>
                            <asp:Label ID="Courriel" runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="Courriel" runat="server" TextMode="SingleLine" Width="120"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Texto" ItemStyle-Width="70">
                        <ItemTemplate>
                            <asp:Label ID="Texto" runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="Texto" runat="server" TextMode="SingleLine" Width="100"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Type d'utilisateur" ItemStyle-Width="120">
                        <ItemTemplate>
                            <asp:Label ID="Type" runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="Type" runat="server">
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Concentration" ItemStyle-Width="200">
                        <ItemTemplate>
                            <asp:Label ID="Concentration" runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="Concentration" runat="server" Width="150">
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Mot de passe" ItemStyle-Width="100">
                        <ItemTemplate>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="MotDePasse" runat="server" TextMode="password" Width="80"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Actions" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:ImageButton ID="Select" runat="server" ImageUrl="~/Images/icons/seances.png" Width="15" CommandName="Select" ToolTip="Gérer séances d'entrainement" />
                            <asp:ImageButton ID="Edit" runat="server" ImageUrl="~/Images/Icons/edit-icon.png" Width="15" CommandName="Edit" ToolTip="Modifier" />
                            <asp:ImageButton ID="Delete" runat="server" ImageUrl="~/Images/icons/delete-icon.png" Width="15" CommandName="Delete" ToolTip="Supprimer" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:ImageButton ID="Update" runat="server" ImageUrl="~/Images/Icons/Accept.png" Width="15" CommandName="Update" ToolTip="Accepter" />
                            <asp:ImageButton ID="Cancel" runat="server" ImageUrl="~/Images/Icons/cancel.png" Width="15" CommandName="Cancel" ToolTip="Annuler" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="Insert" runat="server" Text="Ajouter" OnClick="AddButon_Click" />
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#de5900" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                <RowStyle BackColor="#fbf3e6" />
                <AlternatingRowStyle BackColor="White" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <EditRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#de5900" ForeColor="White" HorizontalAlign="Center" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="GridDetail" runat="server" AllowSorting="True" AllowPaging="True" ShowFooter="true"
                PageSize="15" AutoGenerateColumns="False" DataKeyNames="ID_SEANCE" OnPageIndexChanged="GridDetail_PageIndexChanged"
                OnPageIndexChanging="GridDetail_PageIndexChanging" OnRowCreated="GridDetail_RowCreated" Width="100%"
                OnRowDeleting="GridDetail_RowDeleting" CellPadding="4" ForeColor="#333333" GridLines="None" EmptyDataRowStyle-ForeColor="Red">
                <Columns>
                    <asp:BoundField DataField="ID_SEANCE" HeaderText="ID" ReadOnly="true" Visible="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="INTENSITE" HeaderText="Intensité" ItemStyle-Width="70"
                        ItemStyle-HorizontalAlign="Center" Visible="false">
                        <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Intensité" ItemStyle-Width="70px">
                        <ItemTemplate>
                            <asp:Label ID="labelIntensite" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="TEMPS_ACTIVITE" HeaderText="Durée en minutes" ItemStyle-Width="130"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center" Width="130px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="NB_CREDITS" HeaderText="Crédits $anté" ItemStyle-Width="90"
                        ItemStyle-HorizontalAlign="Center" Visible="false">
                        <ItemStyle HorizontalAlign="Center" Width="90px"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Crédits $anté<sup>®</sup>" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="labelCreditsSante" runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtNbCredits" runat="server" Width="100" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="DATE" HeaderText="Date" DataFormatString="{0:MM-dd-yyyy}" Visible="false">
                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Date" ItemStyle-Width="200" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="labelDate" runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="datePicker" runat="server" PopupButtonID="dateButton" TargetControlID="txtDate" />
                            <asp:ImageButton ID="dateButton" runat="server" ImageUrl="../Images/icons/calendar.png" Height="25" CssClass="dateButton" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Objectif" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="Delete" runat="server" ImageUrl="~/Images/icons/delete-icon.png" Width="15" CommandName="Delete" ToolTip="Supprimer" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#de5900" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                <RowStyle BackColor="#fbf3e6" />
                <AlternatingRowStyle BackColor="White" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#de5900" ForeColor="White" HorizontalAlign="Center" />
            </asp:GridView>

            <form>
                <table id="insert_table" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="labelNbCredits" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="nbCredits" runat="Server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="labelDate" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="datePicker" runat="server" PopupButtonID="dateButton" TargetControlID="txtDate" />
                            <asp:ImageButton ID="dateButton" runat="server" ImageUrl="../Images/icons/calendar.png" Height="25" CssClass="dateButton" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="btnInsert" runat="Server" Text="Ajouter" CommandArgument="EmptyInsert" OnClick="AddButon_Click" />
                        </td>
                    </tr>
                </table>
            </form>
            <label>Supprimer tous les utilisateurs qui ne ce sont pas connecté depuis : </label>
            <asp:TextBox ID="txtBoxPurgerUtilisateur" Width="30" xmlns:asp="#unknown" runat="server" />
            <label>mois.</label>
            <asp:Button ID="btnPurgerUtilisateur" runat="server" Text="Purger" OnClick="btnPurgerUtilisateur_Click" OnClientClick="if ( !confirm('Voulez-vous vraiment supprimer les utilisateurs inactif ?')) return false;" /> 
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
    ControlToValidate="txtBoxPurgerUtilisateur" runat="server"
    ErrorMessage="Veuillez entrer un nombre" ForeColor="red"
    ValidationExpression="\d+">
</asp:RegularExpressionValidator>
            <asp:Label ID="MessageRetourPurge" runat="server" Text="" ForeColor="#33CC33"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
