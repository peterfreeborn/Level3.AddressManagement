<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Issues.aspx.cs" Inherits="Level3.AddressManagement.UI.Web.Issues" %>
<%@ Register Src="~/OrderAddressListViewControl.ascx" TagPrefix="uc1" TagName="OrderAddressListViewControl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:OrderAddressListViewControl runat="server" id="OrderAddressListViewControl" />
</asp:Content>