<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="Level3.AddressManagement.UI.Web.Search" %>
<%@ Register Src="~/OrderAddressListViewControl.ascx" TagPrefix="uc1" TagName="OrderAddressListViewControl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/OrderAddressSearchControls.ascx" TagPrefix="uc1" TagName="OrderAddressSearchControls" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:OrderAddressSearchControls runat="server" ID="OrderAddressSearchControls" />
    <uc1:OrderAddressListViewControl runat="server" ID="OrderAddressListViewControl" />
</asp:Content>