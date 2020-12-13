<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NotAuthorized.aspx.cs" Inherits="Level3.AddressManagement.UI.Web.NotAuthorized" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div style="background: !important" class="jumbotron">
        <p>
            <img alt="Mexi-pago logo" src="Images/App_Logo.png" style="width: 268px; height: 84px" />
        </p>
        <p class="lead">Your page request has been blocked. Only Mexi-Pago system administrators can access this page, where the config values present on an ATI file can be changed.  If you require access, please contact IT.  You windows usersname will need to be added to the applications configuration information to assign you as a system administrator.</p>

       <p><a href="/Admin" class="btn btn-success btn-lg">Return to READ-ONLY Setting List &raquo;</a></p>
    </div>

</asp:Content>