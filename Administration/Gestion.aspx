<%@ Page Title="" Language="C#" MasterPageFile="SiteAdmin.Master" EnableEventValidation="false" CodeBehind="Gestion.aspx.cs" Inherits="CS2013.Gestion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <!-- <asp:HyperLink ID="LinkGestionActivite" NavigateUrl="~/Administration/GestionActivités.aspx" runat="server">Gérer les activités</asp:HyperLink>
    <p></p>  -->
    
    <asp:HyperLink ID="LinkGestionConcentration" NavigateUrl="~/Administration/GestionConcentration.aspx" runat="server">Gérer les concentrations</asp:HyperLink>
    <br />
    <asp:HyperLink ID="LinkGestionUtilisateur" NavigateUrl="~/Administration/GestionUtilisateur.aspx" runat="server">Gérer les utilisateurs</asp:HyperLink>
    <br />
    <asp:HyperLink ID="LinkEntréesSuspectes" NavigateUrl="~/Administration/GestionEntreesSuspectes.aspx" runat="server">Entrées suspectes</asp:HyperLink>
    <br />
    <br />
    <asp:HyperLink ID="HyperLink3" NavigateUrl="~/Administration/GestionEvenement.aspx" runat="server">Gérer les <i>Évenements</i></asp:HyperLink>
    <br /><asp:HyperLink ID="LinkCapsules" NavigateUrl="~/Administration/GestionCapsulesSante.aspx" runat="server">Gérer les <i>Capsules santé</i></asp:HyperLink>
    <br /><asp:HyperLink ID="HyperLink2" Visible="false" NavigateUrl="~/Administration/GestionCapsules.aspx" runat="server">Gérer les <i>Capsules santé 2.0</i></asp:HyperLink>
    <asp:HyperLink ID="LinkAffichage" NavigateUrl="~/Administration/Affichage.aspx" runat="server">Gérer l'affichage</asp:HyperLink>
    <br />
    <asp:HyperLink ID="LinkParametres" NavigateUrl="~/Administration/Parametres.aspx" runat="server">Paramètres d'application</asp:HyperLink>
    <br />
    <br />
    <asp:HyperLink ID="HyperLink1" NavigateUrl="~/Administration/GestionSeances.aspx" runat="server">Suppression de séances d'entrainement</asp:HyperLink>
    <br />
    
    <br />

    <div class="modal_window">
        <asp:ImageButton ID="mailbutton" runat="server" ImageUrl="~/Images/icons/send_email_group_letter.png" Width="70" ToolTip="Envoyer un message"/>
        <asp:Panel ID="modalPanel" runat="server" CssClass="modalPanel" BackColor="White">
            <asp:Label ID="labelMessage" runat="server" Text="Envoyer un message" CssClass="modalHeader"></asp:Label>

            
            <asp:Label ID="labelDestinataire" runat="server" CssClass="modalTitre" Text="Destinataire :"></asp:Label>
            <asp:DropDownList ID="dropDestinataires" runat="server" Width="200" AutoPostBack="true" OnSelectedIndexChanged="dropMode_SelectedIndexChanged"/><br />
            
            <asp:Label ID="labelMode" runat="server" CssClass="modalTitre" Text="Type d'envoi : "></asp:Label>
            <asp:DropDownList ID="dropMode" runat="server" Width="200" />
            <p id="errorRecipients" runat="server" class="modalError"></p>

            <asp:TextBox ID="manualDest" runat="server" TextMode="SingleLine" Width="350" Visible="false"></asp:TextBox>

            <p class="modalTitre">Titre</p>
            <asp:TextBox ID="titreMessage" runat="server" TextMode="SingleLine" Width="350"></asp:TextBox>
            <p id="errorTitle" runat="server" class="modalError"></p>
            
            <p class="modalTitre">Contenu</p>
            <asp:TextBox ID="message" runat="server" TextMode="MultiLine" Height="100" Width="350"></asp:TextBox>
            <p id="errorContent" runat="server" class="modalError"></p>
            
            <div style="height:35px;">
                <div style="float:right">
                    <asp:Button ID="ok" runat="server" Text="Envoyer" CssClass="modalButton" OnClick="sendMail"/>
                    <asp:Button ID="cancel" runat="server" Text="Annuler" CssClass="modalButton" />
                </div>
            </div>
        </asp:Panel>

        <ajaxToolkit:ModalPopupExtender ID="modal" runat="server"
            PopupControlID="modalPanel"
            TargetControlID="mailButton"
            BackgroundCssClass="modalBackground"
            DropShadow="true"
            CancelControlID="cancel"/>
    </div>
</asp:Content>
