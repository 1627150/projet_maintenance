<%@ Page Title="" Language="C#" MasterPageFile="../SiteAdmin.Master" AutoEventWireup="true" CodeBehind="GestionPhotosBougeotte.aspx.cs" Inherits="CS2013.Administration.GestionPhotosBougeotte" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link type="text/css" href="../../Scripts/pikachoose-96/styles/bottom.css" rel="stylesheet" />
    <script type="text/javascript" src="../../Scripts/pikachoose-96/lib/jquery.jcarousel.min.js"></script>
    <script type="text/javascript" src="../../Scripts/pikachoose-96/lib/jquery.pikachoose.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".btn-retirer").click(function (e) {
                var fichier = $("#" + this.name)[0].currentSrc.replace(/^.*[\\\/]/, '');
                var params = "{'nomFichier': '" + fichier + "'}";
                e.preventDefault();
                $.ajax({
                    type: "POST",
                    url: "GestionPhotosBougeotte.aspx/SupprimerPhoto",
                    data: params,
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {
                        location.reload();
                    }
                });
            });
        });
    </script>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <h2><asp:Label ID="labelTitre" runat="server" /></h2>
    <p><asp:Label ID="labelDescription" runat="server" /></p>

    <asp:Label ID="labelErreur" ForeColor="Red" runat="server" />
    <table>
        <tr><td class="auto-style2">
                <asp:Label ID="labelAjouterImages" runat="server" />
            </td>
            <td class="auto-style2">
                <asp:FileUpload ID="fileUpload" AllowMultiple="true" runat="server" />
                <br /><br />
                <asp:Label ID="labelStatus" runat="server" />
            </td>
            <td><asp:Button ID="buttonValider" Text="Valider" runat="server" Style="float: right" OnClick="ButtonValider_Click" /></td>
        </tr>
    </table>
    <hr />
    <div class="photos">
        <div class="pikachoose">
            <ul id="pikame" runat="server" clientidmode="Static" class="jcarousel-skin-pika"></ul>
        </div>
    </div>
</asp:Content>
