<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="SiteAdmin.Master" AutoEventWireup="true" CodeBehind="GestionCapsulesSante.aspx.cs" Inherits="CS2013.GestionCapsulesSante" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Gestion des <i>Capsules Santé</i>.</h2>
    <br />
    <p>*Une capsule ajoutée à une date future sera affichée à cette date.</p>
    <p>Pour ajouter un hyperlien, le mettre entre crochets comme suit : [www.cstjean.qc.ca]</p>
    <asp:UpdatePanel ID="updateCapsules" runat="server">
        <ContentTemplate>
            <asp:Label ID="labelErreur" ForeColor="Red" runat="server" />
            <asp:GridView ID="GridCapsuleSante" runat="Server" AutoGenerateColumns="False" BackColor="White" AllowSorting="True" AllowPaging="True" PageSize="10"
                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                GridLines="Vertical" SkinID="RecordList" Width="100%" OnPageIndexChanged="GridConcentrations_PageIndexChanged"
                OnPageIndexChanging="GridConcentrations_PageIndexChanging" OnRowCancelingEdit="GridConcentrations_RowCancelingEdit"
                OnRowCreated="GridConcentrations_RowCreated" OnRowDeleting="GridConcentrations_RowDeleting" OnRowEditing="GridConcentrations_RowEditing"
                OnRowUpdating="GridConcentrations_RowUpdating" OnRowDataBound="GridConcentrations_RowDataBound"
                ShowFooter="True" DataKeyNames="ID" EnableViewState="True" EmptyDataRowStyle-ForeColor="Red">
                <Columns>
                    <asp:BoundField DataField="ID" ReadOnly="true" Visible="false" />
                    <asp:BoundField DataField="TITLE" ReadOnly="true" Visible="false" />
                    <asp:BoundField DataField="DATE_PUBLICATION" Visible="false" ReadOnly="true" />
                    <asp:BoundField DataField="CONTENT" ReadOnly="true" Visible="false" />
                    <asp:TemplateField HeaderText="Titre" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="Titre" runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="Titre" runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="Titre" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date de publication" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="Date" runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="Date" runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="Date" runat="server" PopupButtonID="dateButton" TargetControlID="txtDate" />
                            <asp:ImageButton ID="dateButton" runat="server" ImageUrl="../Images/icons/calendar.png" Height="25" CssClass="dateButton" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Contenu" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300">
                        <ItemTemplate>
                            <asp:Label ID="Contenu" runat="server" TextMode="MultiLine" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="Contenu" runat="server" TextMode="MultiLine" Width="400px" Height="300px" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="Contenu" runat="server" TextMode="MultiLine" Width="400px" Height="300px" />
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Objectif" ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="Edit" runat="server" ImageUrl="~/Images/Icons/edit-icon.png" Width="15" CommandName="Edit" ToolTip="Modifier" />
                            <asp:ImageButton ID="Delete" runat="server" ImageUrl="~/Images/icons/delete-icon.png" Width="15" CommandName="Delete" ToolTip="Supprimer" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:ImageButton ID="Update" runat="server" ImageUrl="~/Images/Icons/Accept.png" Width="15" CommandName="Update" ToolTip="Accepter" />
                            <asp:ImageButton ID="Cancel" runat="server" ImageUrl="~/Images/Icons/cancel.png" Width="15" CommandName="Cancel" ToolTip="Annuler" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="Insert" runat="server" Text="Ajouter" OnClick="Insert_Click" CommandArgument="Insert" />
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
                            <asp:Label ID="labelTitreArticle" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="titreArticle" runat="Server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="labelDate" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="Date" runat="server" PopupButtonID="dateButton" TargetControlID="txtDate" />
                            <asp:ImageButton ID="dateButton" runat="server" ImageUrl="../Images/icons/calendar.png" Height="25" CssClass="dateButton" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="labelTexte" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="texte" runat="server" TextMode="MultiLine" Width="500" Height="250" />
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
