<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderAddressDetail.aspx.cs" Inherits="Level3.AddressManagement.UI.Web.OrderAddressDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Order Address Detail:
    <label id="lblOrderAddressHeader" runat="server"></label>
    </h3>
    <asp:Label ID="lblErrorMessage" runat="server" Text="" class="alert-danger"></asp:Label>

    <div class="panel panel-default">
        <div class="panel-heading">Address Details</div>
        <div class="panel-body">
            <div class="panel panel-default">
                <table class="table table-bordered table-condensed">
                    <thead>
                    </thead>
                    <tbody>
                        <tr>
                            <td class="success col-sm-2">Customer Order #</td>
                            <td class="col-sm-4">
                                <label id="lblCustomerOrderNumber" runat="server"></label>
                            </td>
                            <td class="success  col-sm-2">Order Date</td>
                            <td class="col-sm-4">
                                <label id="lblOrderDate" runat="server"></label>
                            </td>
                        </tr>
                        <tr>
                            <td class="success col-sm-2">Address Line 1</td>
                            <td class="col-sm-4">
                                <label id="lblAddressLineOne" runat="server"></label>
                            </td>
                            <td class="success col-sm-2">City</td>
                            <td class="col-sm-4">
                                <label id="lblCity" runat="server"></label>
                            </td>
                        </tr>
                        <tr>
                            <td class="success col-sm-2">State</td>
                            <td class="col-sm-4">
                                <label id="lblState" runat="server"></label>
                            </td>
                            <td class="success col-sm-2">Postal Code</td>
                            <td class="col-sm-4">
                                <label id="lblPostalCode" runat="server"></label>
                            </td>
                        </tr>
                        <tr>
                            <td class="success col-sm-2">Country</td>
                            <td class="col-sm-4">
                                <label id="lblCountry" runat="server"></label>
                            </td>
                            <td class="success col-sm-2">Floor</td>
                            <td class="col-sm-4">
                                <label id="lblFloor" runat="server"></label>
                            </td>
                        </tr>
                        <tr>
                            <td class="success col-sm-2">Room</td>
                            <td class="col-sm-4">
                                <label id="lblRoom" runat="server"></label>
                            </td>
                            <td class="success col-sm-2">Suite</td>
                            <td class="col-sm-4">
                                <label id="lblSuite" runat="server"></label>
                            </td>
                        </tr>
                        <tr>
                            <td class="success col-sm-2">Date Updated</td>
                            <td class="col-sm-4">
                                <label id="lblDateUpdated" runat="server"></label>
                            </td>
                            <td class="success col-sm-2">Date Created</td>
                            <td class="col-sm-4">
                                <label id="lblDateCreated" runat="server"></label>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="panel panel-default">
                <table class="table table-bordered table-condensed">
                    <thead>
                    </thead>
                    <tbody>
                        <tr>
                            <td class="success col-sm-2 ">Current Status</td>
                            <td class="col-sm-10">
                                <p class="text-primary">
                                    <mark>
                                        <label id="lblCurrentMigrationStatusDesc" runat="server"></label>
                                    </mark>
                                </p>
                                <p class="text-white">
                                    <label id="lblCurrentMigrationStatusDescExtended" runat="server" class="text-secondary"></label>
                                </p>

                            </td>
                        </tr>
                        <tr>
                            <td class="success col-sm-2">Processing Time</td>
                            <td class="col-sm-10">
                                <label id="lblProcessingTime" runat="server"></label>
                            </td>
                        </tr>
                        <tr>
                            <td class="success col-sm-2">Last Status Update</td>
                            <td class="col-sm-10">
                                <label id="lblLastStatusUpdate" runat="server"></label>
                            </td>
                        </tr>
                        <tr>
                            <td class="success col-sm-2"></td>
                            <td class="col-sm-10">
                                <telerik:RadLinkButton CssClass="btn btn-success" runat="server" Target="_blank" ID="lbtnAPICallHistory" Text="View API Call History" ToolTip="Click to open the API Call history for this address in a new window" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>


            <div class="panel panel-default">
                <table class="table table-bordered table-condensed">
                    <thead>
                    </thead>
                    <tbody>
                        <tr>
                            <td class="col-sm-6">
                                <asp:LinkButton ID="lnkRetryAPICall" runat="server" OnClick="lnkRetryAPICall_Click">Retry API Call</asp:LinkButton>
                                <asp:LinkButton ID="lnkForceNextStep" runat="server" OnClick="lnkForceNextStep_Click">Force Next Processing Step</asp:LinkButton>
                            </td>
                            <td class="col-sm-6">
                                <asp:LinkButton ID="lnkSetToIgnored" runat="server" OnClick="lnkSetToIgnored_Click">Ignore (remove from issue list)</asp:LinkButton>
                                <asp:LinkButton ID="lnkDontIgnore" runat="server" OnClick="lnkDontIgnore_Click">Don't Ignore (revert status)</asp:LinkButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>


        </div>
    </div>





    <div>
        <asp:Button ID="btnBack" CssClass="btn btn-primary" Visible="true" runat="server" Text="<< Back to List" OnClick="btnBack_Click" />
    </div>
    <br />

    <div class="panel panel-default">
        <div class="panel-heading">GLM Info</div>
        <div class="panel-body">
            <div class="panel panel-default">
                <table class="table table-bordered table-condensed">
                    <thead>
                    </thead>
                    <tbody>
                        <tr>
                            <td class="success col-sm-2">PL Number (Site)</td>
                            <td class="col-sm-10">
                                <label id="lblPLNumber" runat="server"></label>
                            </td>
                        </tr>
                        <tr>
                            <td class="success col-sm-2">Site Code</td>
                            <td class="col-sm-10">
                                <label id="lblSiteCode" runat="server"></label>
                            </td>
                        </tr>
                        <tr>
                            <td class="success col-sm-2">SL Number (Serv. Loc.)</td>
                            <td class="col-sm-10">
                                <label id="lblSLNumber" runat="server"></label>
                            </td>
                        </tr>
                        <tr>
                            <td class="success col-sm-2">S Code</td>
                            <td class="col-sm-10">
                                <label id="lblSCode" runat="server"></label>
                            </td>
                        </tr>

                        <tr>
                            <td class="success col-sm-2">GLM Lookup</td>
                            <td class="col-sm-10">
                                <asp:HyperLink ID="lnkGLMLookup" runat="server" Target="_blank"></asp:HyperLink>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>

    <div class="panel panel-default">
        <div class="panel-heading">Status Log</div>
        <div class="panel-body">
            <div>
                <telerik:RadGrid Skin="Bootstrap" ID="RadGridLogItems" runat="server" ExportSettings-ExportOnlyData="true" ExportSettings-IgnorePaging="true" AutoGenerateColumns="False" AllowPaging="False" AllowCustomPaging="False" PageSize="100" AllowSorting="False" OnNeedDataSource="RadGridLogItems_NeedDataSource" CellSpacing="0" GridLines="None">
                    <MasterTableView CommandItemDisplay="Top" Caption="" Font-Size=".8em">
                        <CommandItemSettings ExportToExcelText="Export all pages to Excel" ShowExportToCsvButton="false" ShowExportToExcelButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                        <Columns>
                            <telerik:GridBoundColumn HeaderText="Log Event ID" DataField="OrderAddressLogItemID" ReadOnly="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn HeaderText="Status" DataField="MigrationStatusDesc" ReadOnly="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn HeaderText="Log Message" DataField="LogMessage" ReadOnly="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn HeaderText="Date Created" DataField="DateCreated" ReadOnly="true" DataFormatString="{0:F}" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </div>
    </div>
</asp:Content>

