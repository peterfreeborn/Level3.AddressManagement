﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderAddressStatusKey.aspx.cs" Inherits="Level3.AddressManagement.UI.Web.OrderAddressStatusKey" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Migration Status List - Address Management</title>

    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>

    <webopt:BundleReference runat="server" Path="~/Content/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server">
            <Scripts>
                <%--To learn more about bundling scripts in ScriptManager see http://go.microsoft.com/fwlink/?LinkID=301884 --%>
                <%--Framework Scripts--%>
                <asp:ScriptReference Name="MsAjaxBundle" />
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Name="respond" />
                <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
                <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
                <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
                <asp:ScriptReference Name="WebFormsBundle" />
                <%--Site Scripts--%>
            </Scripts>
        </asp:ScriptManager>
        <div style="text-align: center">
            <asp:Button ID="btnClose" CssClass="btn btn-primary right" Visible="true" runat="server" Text="Close this Window" OnClick="btnClose_Click" />
        </div>
        <div style="background: !important" class="jumbotron">
            The key below details the various statuses that an order address can assume as it passes through the address migration workflow on its way into GLM and SAP
        </div>
        <asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>
        <div>
            <telerik:RadGrid Skin="Bootstrap" ID="RadGrid1" runat="server" ExportSettings-ExportOnlyData="true" ExportSettings-IgnorePaging="true" AutoGenerateColumns="False" AllowPaging="False" AllowCustomPaging="False" PageSize="100" AllowSorting="True" OnNeedDataSource="RadGrid1_NeedDataSource" CellSpacing="0" GridLines="None">
                <MasterTableView CommandItemDisplay="Top" Caption="" Font-Size=".8em" DataKeyNames="">
                    <CommandItemSettings ExportToExcelText="Export all pages to Excel" ShowExportToCsvButton="false" ShowExportToExcelButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                    <Columns>
                        <telerik:GridBoundColumn HeaderText="Status" DataField="MigrationStatusDesc" ReadOnly="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left" HeaderStyle-Font-Bold="true" />
                        <telerik:GridBoundColumn HeaderText="Description" DataField="MigrationStatusExtendedDescription" ReadOnly="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left" HeaderStyle-Font-Bold="true" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
        <br /><br />
        <div style="text-align: center">
            <asp:Button ID="btnCloseBottom" CssClass="btn btn-primary right" Visible="true" runat="server" Text="Close this Window" OnClick="btnClose_Click" />
        </div>

    </form>
</body>
</html>
