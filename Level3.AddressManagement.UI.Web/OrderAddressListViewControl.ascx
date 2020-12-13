<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderAddressListViewControl.ascx.cs" Inherits="Level3.AddressManagement.UI.Web.OrderAddressListViewControl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<h3>
    <label id="lblListTitle" runat="server"></label>
</h3>
<asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>
<div>
    <telerik:RadGrid Skin="Bootstrap" ID="RadGrid1" runat="server" ExportSettings-ExportOnlyData="true" ExportSettings-IgnorePaging="true" AutoGenerateColumns="False" AllowPaging="True" AllowCustomPaging="True" VirtualItemCount="1" PageSize="100" AllowSorting="True" OnNeedDataSource="RadGrid1_NeedDataSource" CellSpacing="0" GridLines="None" OnItemCommand="RadGrid1_ItemCommand" OnItemDataBound="RadGrid1_ItemDataBound">

        <MasterTableView CommandItemDisplay="Top" Caption="" Font-Size=".8em" DataKeyNames="OrderAddressID">
            <%--<CommandItemSettings ExportToExcelText="Export all pages to Excel" ShowExportToCsvButton="false" ShowExportToExcelButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" />--%>
            <CommandItemSettings ExportToExcelText="Export all pages to Excel" ShowExportToExcelButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
            <Columns>
                <telerik:GridBoundColumn HeaderText="S.O.R." DataField="OrderSystemOfRecordDesc" ReadOnly="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <telerik:GridBoundColumn HeaderText="Address Type" DataField="OrderAddressTypeDesc" ReadOnly="true" HeaderStyle-Wrap="true" ItemStyle-Wrap="true" />
                <telerik:GridBoundColumn HeaderText="Order #" DataField="ServiceOrderNumber" ReadOnly="true"  HeaderStyle-Wrap="true" ItemStyle-Wrap="true" />
                <telerik:GridBoundColumn HeaderText="Order Date" DataField="FIRST_ORDER_CREATE_DT" ReadOnly="true" DataFormatString="{0:MMM dd yyyy}" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="true" ItemStyle-Wrap="true" />
                <telerik:GridHyperLinkColumn HeaderText="Address 1" DataNavigateUrlFields="OrderAddressID" UniqueName="GoToLink" DataNavigateUrlFormatString="~/OrderAddressDetail?orderAddressId={0}" DataTextField="CDWAddressOne"  HeaderStyle-Wrap="true" ItemStyle-Wrap="true" />
                <telerik:GridBoundColumn HeaderText="City" DataField="CDWCity" ReadOnly="true"  HeaderStyle-Wrap="true" ItemStyle-Wrap="true" />
                <telerik:GridBoundColumn HeaderText="State" DataField="CDWState" ReadOnly="true"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <telerik:GridBoundColumn HeaderText="Postal Code" DataField="CDWPostalCode" ReadOnly="true"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <telerik:GridBoundColumn HeaderText="Country" DataField="CDWCountry" ReadOnly="true"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <telerik:GridBoundColumn HeaderText="Floor" DataField="CDWFloor" ReadOnly="true"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <telerik:GridBoundColumn HeaderText="Room" DataField="CDWRoom" ReadOnly="true"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <telerik:GridBoundColumn HeaderText="Suite" DataField="CDWSuite" ReadOnly="true"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <telerik:GridBoundColumn HeaderText="Migration Status" DataField="MigrationStatusDesc" ReadOnly="true"  HeaderStyle-Wrap="true" ItemStyle-Wrap="true" />
                <telerik:GridBoundColumn HeaderText="Date Created" DataField="DateCreated" ReadOnly="true" DataFormatString="{0:MMM dd yyyy HH:mm:ss}"  HeaderStyle-Wrap="true" ItemStyle-Wrap="true" />
                <telerik:GridBoundColumn HeaderText="Status Last Updated" DataField="DateTimeOfLastMigrationStatusUpdate" ReadOnly="true" DataFormatString="{0:MMM dd yyyy HH:mm:ss}"  HeaderStyle-Wrap="true" ItemStyle-Wrap="true" />
                <telerik:GridBoundColumn HeaderText="Processing Time" DataField="TotalProcessingTimeAsHumanReadable" ReadOnly="true"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>
