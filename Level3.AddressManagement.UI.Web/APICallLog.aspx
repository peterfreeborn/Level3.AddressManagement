<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="APICallLog.aspx.cs" Inherits="Level3.AddressManagement.UI.Web.APICallLog" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>
        <label id="lblListTitle" runat="server"></label>
    </h3>
    <asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>
    <div>
        <telerik:RadGrid Skin="Bootstrap" ID="RadGrid1" runat="server" ExportSettings-ExportOnlyData="true" ExportSettings-IgnorePaging="true" AutoGenerateColumns="False" AllowPaging="False" AllowCustomPaging="False" PageSize="100" AllowSorting="True" OnNeedDataSource="RadGrid1_NeedDataSource" CellSpacing="0" GridLines="None">

            <MasterTableView CommandItemDisplay="Top" Caption="" Font-Size=".8em" DataKeyNames="APICallLogItemID">
                <CommandItemSettings ExportToExcelText="Export all pages to Excel" ShowExportToCsvButton="false" ShowExportToExcelButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                <Columns>
                            <telerik:GridHyperLinkColumn Target="_blank" HeaderText="View Record" DataNavigateUrlFields="OrderAddressID" UniqueName="GoToLink" DataNavigateUrlFormatString="~/OrderAddressDetail?orderAddressId={0}" DataTextField="OrderAddressID"  HeaderStyle-Wrap="true" ItemStyle-Wrap="true" />
                            <telerik:GridBoundColumn HeaderText="Host" DataField="Host" ReadOnly="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left" HeaderStyle-Font-Bold="true" />
                            <telerik:GridBoundColumn HeaderText="Route" DataField="FullUrl" ReadOnly="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left" HeaderStyle-Font-Bold="true" />
                            <telerik:GridBoundColumn HeaderText="Response Code" DataField="ResponseCode" ReadOnly="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left" HeaderStyle-Font-Bold="true" />
                            <telerik:GridBoundColumn HeaderText="Run Time" DataField="RunTimeHumanReadable" ReadOnly="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left" HeaderStyle-Font-Bold="true" />
                            <telerik:GridBoundColumn HeaderText="Date Creeated" DataField="DateCreated" ReadOnly="true"  DataFormatString="{0:F}" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
</asp:Content>

