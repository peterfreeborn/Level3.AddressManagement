<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SysAdmin.aspx.cs" Inherits="Level3.AddressManagement.UI.Web.SysAdmin" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="background: !important" class="jumbotron">
        The settings below affect the list of recipients that receive system notificaiton and the CDW query that pulls in new address records.  They CAN BE changed by SYSTEM ADMINISTRATORs.  To view the dynacmic QUERY that is executed against the CDW to identify new managed service orders... click <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/CDWQuery.aspx" Target="_blank">here</asp:HyperLink>.
    </div>
    <asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>
    <div>
        <telerik:RadGrid Skin="Bootstrap" ID="RadGrid1" runat="server" ExportSettings-ExportOnlyData="true" ExportSettings-IgnorePaging="true" AutoGenerateColumns="False" AllowPaging="False" AllowCustomPaging="False" PageSize="100" AllowSorting="True" OnNeedDataSource="RadGrid1_NeedDataSource" CellSpacing="0" GridLines="None">
            <MasterTableView CommandItemDisplay="Top" Caption="" Font-Size=".8em" DataKeyNames="SystemConfigItemID">
                <CommandItemSettings ExportToExcelText="Export all pages to Excel" ShowExportToCsvButton="false" ShowExportToExcelButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                <Columns>
                    <telerik:GridHyperLinkColumn HeaderText="Config Setting Name" DataNavigateUrlFields="SystemConfigItemID" UniqueName="GoToLink" DataNavigateUrlFormatString="~/ConfigSettingItemDetail?settingId={0}" DataTextField="ConfigSettingName" HeaderStyle-Font-Bold="true" />
                    <telerik:GridBoundColumn HeaderText="Value" DataField="ConfigSettingValue" ReadOnly="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left" HeaderStyle-Font-Bold="true" />
                    <telerik:GridBoundColumn HeaderText="Last Update Date" DataField="DateUpdated" ReadOnly="true" DataFormatString="{0:MMM dd yyyy}" HeaderStyle-Font-Bold="true" />
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>

    <div style="background: !important" class="jumbotron">
        The settings below are managed by DEVELOPERs and/or PROD SUPPORT personnel.
    </div>
    <asp:Label ID="lblErrorMessage2" runat="server" Text=""></asp:Label>
    <div>
        <telerik:RadGrid Skin="Bootstrap" ID="RadGrid2" runat="server" ExportSettings-ExportOnlyData="true" ExportSettings-IgnorePaging="true" AutoGenerateColumns="False" AllowPaging="False" AllowCustomPaging="False" PageSize="100" AllowSorting="True" OnNeedDataSource="RadGrid2_NeedDataSource" CellSpacing="0" GridLines="None">
            <MasterTableView CommandItemDisplay="Top" Caption="" Font-Size=".8em" >
                <CommandItemSettings ExportToExcelText="Export all pages to Excel" ShowExportToCsvButton="false" ShowExportToExcelButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                <Columns>
                    <telerik:GridBoundColumn HeaderText="Config Setting Name" DataField="Name" ReadOnly="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left" HeaderStyle-Font-Bold="true" />
                    <telerik:GridBoundColumn HeaderText="Value" DataField="Value" ReadOnly="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left" HeaderStyle-Font-Bold="true" />
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
</asp:Content>
