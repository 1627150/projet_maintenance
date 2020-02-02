<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="SiteAdmin.Master" AutoEventWireup="true" CodeBehind="GestionConcentration.aspx.cs" Inherits="CS2013.GestionConcentration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Gestion des services et départements</h2>
    <p>
        Cette page permet de gérer les différents services et départements.
    </p>
    
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

    <br />
    <asp:Label ID="labelErreur" ForeColor="Red" runat="server" />
    <asp:UpdatePanel ID="GridUpdate" runat="server">
        <ContentTemplate>
            <asp:GridView ID="GridConcentrations" runat="Server" AutoGenerateColumns="False" BackColor="White" AllowSorting="True" AllowPaging="True" PageSize="10"
                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                GridLines="Vertical" SkinID="RecordList" Width="100%" OnPageIndexChanged="GridConcentrations_PageIndexChanged"
                OnPageIndexChanging="GridConcentrations_PageIndexChanging" OnRowCancelingEdit="GridConcentrations_RowCancelingEdit"
                OnRowCreated="GridConcentrations_RowCreated" OnRowDeleting="GridConcentrations_RowDeleting" OnRowEditing="GridConcentrations_RowEditing"
                OnRowUpdating="GridConcentrations_RowUpdating" OnRowDataBound="GridConcentrations_RowDataBound" OnSelectedIndexChanged="GridConcentration_InitModification"
                ShowFooter="False" DataKeyNames="ID_CONCENTRATION" EnableViewState="True">
                <Columns>
                    <asp:BoundField DataField="ID_CONCENTRATION" HeaderText="ID" ReadOnly="true" Visible="false" />
                    <asp:BoundField DataField="TYPE_CONCENTRATION" ReadOnly="true" Visible="false" />
                    <asp:BoundField DataField="NOM_CONCENTRATION" HeaderText="Nom" ItemStyle-Width="300" HeaderStyle-HorizontalAlign="Left" Visible="false" />
                    <asp:TemplateField HeaderText="Nom" ItemStyle-Width="300px" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="labelNomConcentration" runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtNomConcentration" runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtNomConcentration" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Groupe d’appartenance" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="Type" runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="Type" runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="Type" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="OBJECTIF_CREDITS" HeaderText="Objectif" ItemStyle-Width="80"
                        ItemStyle-HorizontalAlign="Center" Visible="false" />
                    <asp:TemplateField HeaderText="Objectif" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="labelObjectif" runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="Objectif" runat="server" Width="55" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="Objectif" runat="server" Width="55" />
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Objectif" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="Select" runat="server" ImageUrl="~/Images/icons/seances.png" Width="15" CommandName="Select" ToolTip="Gérer les sous-groupes" />
                            <asp:ImageButton ID="Edit" runat="server" ImageUrl="~/Images/Icons/edit-icon.png" Width="15" CommandName="Edit" ToolTip="Modifier" />
                            <asp:ImageButton ID="Delete" runat="server" ImageUrl="~/Images/icons/delete-icon.png" Width="15" CommandName="Delete" ToolTip="Supprimer" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:ImageButton ID="Update" runat="server" ImageUrl="~/Images/Icons/Accept.png" Width="15" CommandName="Update" ToolTip="Accepter" />
                            <asp:ImageButton ID="Cancel" runat="server" ImageUrl="~/Images/Icons/cancel.png" Width="15" CommandName="Cancel" ToolTip="Annuler" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="Insert" runat="server" Text="Ajouter" OnClick="AddButon_Click" CommandArgument="Insert" />
                        </FooterTemplate>
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
    <br />
    <asp:UpdatePanel ID="DeatilUpdate" runat="server">
        <ContentTemplate>
            <asp:Label ID="labelSousConcErreur" ForeColor="Red" runat="server" />
            <asp:GridView ID="GridSousConcentration" runat="Server" AutoGenerateColumns="False" BackColor="White" AllowSorting="True" AllowPaging="True" PageSize="10"
                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                GridLines="Vertical" SkinID="RecordList" Width="100%" OnPageIndexChanged="GridSousConcentrations_PageIndexChanged"
                OnPageIndexChanging="GridSousConcentrations_PageIndexChanging" OnRowCancelingEdit="GridSousConcentrations_RowCancelingEdit"
                OnRowCreated="GridSousConcentration_RowCreated" OnRowDeleting="GridSousConcentrations_RowDeleting" OnRowEditing="GridSousConcentrations_RowEditing"
                OnRowUpdating="GridSousConcentrations_RowUpdating" OnRowDataBound="GridSousConcentrations_RowDataBound"
                ShowFooter="False" DataKeyNames="ID_CONCENTRATION" EnableViewState="True" Visible="false">
                <Columns>
                    <asp:BoundField DataField="ID_CONCENTRATION" HeaderText="ID" ReadOnly="true" Visible="false" />
                    <asp:BoundField DataField="TYPE_CONCENTRATION" ReadOnly="true" Visible="false" />
                    <asp:BoundField DataField="NOM_CONCENTRATION" HeaderText="Nom" ItemStyle-Width="300" HeaderStyle-HorizontalAlign="Left" Visible="false" />
                    <asp:TemplateField HeaderText="Nom" ItemStyle-Width="300px" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="labelNomConcentration" runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtNomConcentration" runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtNomConcentration" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="OBJECTIF_CREDITS" HeaderText="Objectif" ItemStyle-Width="80"
                        ItemStyle-HorizontalAlign="Center" Visible="false" />
                    <asp:TemplateField HeaderText="Objectif" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" Visible="true">
                        <ItemTemplate>
                            <asp:Label ID="labelObjectif" runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="Objectif" runat="server" Width="55" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="Objectif" runat="server" Width="55" />
                        </FooterTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Objectif" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="Edit" runat="server" ImageUrl="~/Images/Icons/edit-icon.png" Width="15" CommandName="Edit" ToolTip="Modifier" />
                            <asp:ImageButton ID="Delete" runat="server" ImageUrl="~/Images/icons/delete-icon.png" Width="15" CommandName="Delete" ToolTip="Supprimer" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:ImageButton ID="Update" runat="server" ImageUrl="~/Images/Icons/Accept.png" Width="15" CommandName="Update" ToolTip="Accepter" />
                            <asp:ImageButton ID="Cancel" runat="server" ImageUrl="~/Images/Icons/cancel.png" Width="15" CommandName="Cancel" ToolTip="Annuler" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="Insert" runat="server" Text="Ajouter" OnClick="AddButon_Click" CommandArgument="Insert" />
                        </FooterTemplate>
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

            <form>
                <table id="insert_table" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="nom" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="nomConcentration" runat="Server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="labelgroupe" runat="server" />
                        </td>
                        <td>
                            <asp:DropDownList ID="dropGroupe" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="labelObjectif" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="objectif" runat="Server"></asp:TextBox>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
