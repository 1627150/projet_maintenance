<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Photos.aspx.cs" Inherits="CS2013.Photos" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <!-- script type="text/javascript" src=" https://ajax.googleapis.com/ajax/libs/jquery/3.2.0/jquery.min.js"> / -->
    <link type="text/css" href="../Scripts/pikachoose-96/styles/bottom.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/pikachoose-96/lib/jquery.jcarousel.min.js"></script>
    <script type="text/javascript" src="../Scripts/pikachoose-96/lib/jquery.pikachoose.js"></script>

    <script type="text/javascript" type="text/javascript">
        $(document).ready(
			function () {
			    $("#pikame").PikaChoose({ carousel: true });
			});
	</script>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="photos">
        <h2>
            <asp:Label ID="labelTitre" runat="server" /></h2>
        <p>
            <asp:Label ID="labelDescription" runat="server" />
        </p>
        <div class="pikachoose">
            <ul id="pikame" runat="server" ClientIDMode="Static" class="jcarousel-skin-pika"></ul>
        </div>
    </div>
</asp:Content>
