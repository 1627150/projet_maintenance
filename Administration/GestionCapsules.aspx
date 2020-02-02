<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionCapsules.aspx.cs" Inherits="CS2013.GestionCapsules" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />

    <asp:Label ID="labelErreur" ForeColor="Red" runat="server" />
    <asp:GridView ID="GridCapsules" runat="Server" AutoGenerateColumns="False" AllowPaging="true" PageSize="15" UseAccessibleHeader="true" EnableViewState="True" ShowFooter="true"
        DataKeyNames="ID"
        OnRowDataBound="GridCapsules_RowDataBound"
        OnRowDeleting="GridCapsules_RowDeleting"
        OnRowEditing="GridCapsules_RowEditing"
        OnRowUpdating="GridCapsules_RowUpdating"
        OnRowCancelingEdit="GridCapsules_RowCancelingEdit">
        <Columns>
            <asp:TemplateField HeaderText="Titre" ItemStyle-HorizontalAlign="Left" Visible="true">
                <ItemTemplate>
                    <asp:Label ID="Titre" runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="Titre" Width="150" runat="server" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="Titre" Width="150" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Date de parution" ItemStyle-HorizontalAlign="Left" Visible="true">
                <ItemTemplate>
                    <asp:Label ID="Date" runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="Date" Width="150" runat="server" />
                    <ajaxToolkit:CalendarExtender ID="Dater" runat="server" PopupButtonID="dateButton" TargetControlID="Date" />
                    <asp:ImageButton ID="dateButton" runat="server" ImageUrl="../Images/icons/calendar.png" Height="25" CssClass="dateButton" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="Date" Width="150" runat="server" />
                    <ajaxToolkit:CalendarExtender ID="Dater" runat="server" PopupButtonID="dateButton" TargetControlID="Date" />
                    <asp:ImageButton ID="dateButton" runat="server" ImageUrl="../Images/icons/calendar.png" Height="25" CssClass="dateButton" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Contenbu" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="Contenu" runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="Contenu" runat="server" TextMode="MultiLine" Width="400px" Height="300px" DataTextField="Name" DataValueField="Id"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="Contenu" TextMode="MultiLine" runat="server" Width="400px" Height="300px" DataTextField="Name" DataValueField="Id"></asp:TextBox>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Commands" ItemStyle-Width="80" ItemStyle-HorizontalAlign="center" FooterStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:ImageButton ID="edit" runat="server" Width="20" ImageUrl="~/Images/Icons/edit-icon.png" BackColor="Transparent" BorderColor="Transparent" CommandName="Edit" />
                    <asp:ImageButton ID="delete" runat="server" Width="20" ImageUrl="~/Images/Icons/delete-icon.png" BackColor="Transparent" BorderColor="Transparent" CommandName="Delete" OnClientClick="return confirm('Supprimer cette entrée?');" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:ImageButton ID="accept" runat="server" Width="20" ImageUrl="~/Images/Icons/accept.png" BackColor="Transparent" BorderColor="Transparent" CommandName="Update" />
                    <asp:ImageButton ID="cancel" runat="server" Width="20" ImageUrl="~/Images/Icons/cancel.png" BackColor="Transparent" BorderColor="Transparent" CommandName="Cancel" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:ImageButton ID="insert" runat="server" Width="20" ImageUrl="~/Images/Icons/accept.png" BackColor="Transparent" BorderColor="Transparent" OnClick="Add_Capsule" />
                </FooterTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <asp:Panel ID="EmptyRow" runat="server">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="labelFirstName" runat="server">Titre</asp:Label></td>
                        <td>
                            <asp:Label ID="labelLastName" runat="server">Date</asp:Label></td>
                        <td>
                            <asp:Label ID="labelLocal" runat="server">Contenu</asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtTitre" runat="server" Width="150"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtDate" runat="server" Width="150"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="Dater" runat="server" PopupButtonID="dateButton" TargetControlID="txtDate" />
                            <asp:ImageButton ID="dateButton" runat="server" ImageUrl="../Images/icons/calendar.png" Height="25" CssClass="dateButton" /></td>
                        <td>
                            <asp:TextBox ID="txtContenu" runat="server" Width="150"></asp:TextBox></td>
                        <td>
                            <asp:Button ID="Insert" runat="server" Text="Insert" OnClick="Insert_Click" /></td>
                    </tr>
                </table>
            </asp:Panel>
        </EmptyDataTemplate>
        <HeaderStyle BackColor="#de5900" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#fbf3e6" />
        <AlternatingRowStyle BackColor="White" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <EditRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#de5900" ForeColor="White" HorizontalAlign="Center" />
    </asp:GridView>
</asp:Content>
