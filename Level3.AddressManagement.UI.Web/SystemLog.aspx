<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SystemLog.aspx.cs" Inherits="Level3.AddressManagement.UI.Web.SystemLog" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>
        <label id="lblListTitle" runat="server"></label>
    </h3>
    <asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>
    <div>
        <telerik:RadGrid Skin="Bootstrap" ID="RadGrid1" runat="server" ExportSettings-ExportOnlyData="true" ExportSettings-IgnorePaging="true" AutoGenerateColumns="False" AllowPaging="False" AllowCustomPaging="False" PageSize="100" AllowSorting="True" OnNeedDataSource="RadGrid1_NeedDataSource" CellSpacing="0" GridLines="None">

            <MasterTableView CommandItemDisplay="Top" Caption="" Font-Size=".8em" DataKeyNames="SystemLogItemID">
                <CommandItemSettings ExportToExcelText="Export all pages to Excel" ShowExportToCsvButton="false" ShowExportToExcelButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                <Columns>
                    <telerik:GridBoundColumn HeaderText="Creation Date" DataField="DateCreated" ReadOnly="true"  DataFormatString="{0:MMM dd yyyy HH:mm:ss}" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="col-sm-2" />
                    <telerik:GridBoundColumn HeaderText="Log Note" DataField="Note" ReadOnly="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"  ItemStyle-CssClass="col-sm-10" />
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
</asp:Content>
