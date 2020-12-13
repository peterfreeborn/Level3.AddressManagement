<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderAddressSearchControls.ascx.cs" Inherits="Level3.AddressManagement.UI.Web.OrderAddressSearchControls" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<h3>
    <label id="lblListTitle" runat="server">Search:</label></h3>

<div class="panel panel-default">
    <table class="table table-bordered table-condensed">
        <thead>
        </thead>
        <tbody>
            <tr>
                <td class="regular success" colspan="4">
                    <label runat="server" id="lblError" class="text-danger"></label>
                </td>
            </tr>
            <tr>
                <td class="success right middlev">S.O.R.</td>
                <td class="" colspan="3">
                    <telerik:RadComboBox Skin="Bootstrap" NoWrap="true" DropDownAutoWidth="Enabled" ID="ddlOrderSystemOfRecords" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true"></telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="success right middlev">Service Order Number</td>
                <td class="" colspan="3">
                    <asp:TextBox ID="txtCustomerOrderNumber" runat="server" class="col-sm-8 form-control" placeholder=""></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="success right middlev">Order Date</td>
                <td class="">
                    <telerik:RadDatePicker Skin="Bootstrap" ID="dteOrderDateStart" runat="server"></telerik:RadDatePicker>
                </td>

                <td class="success center middlev">to</td>
                <td class="">
                    <telerik:RadDatePicker Skin="Bootstrap" ID="dteOrderDateEnd" runat="server"></telerik:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="success right middlev">Address Line 1</td>
                <td class="" colspan="3">
                    <asp:TextBox ID="txtAddressLine1" runat="server" class="col-sm-8 form-control" placeholder=""></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="success right middlev ">Migration Status</td>
                <td class="" colspan="3">
                    <telerik:RadComboBox Skin="Bootstrap" NoWrap="true" DropDownAutoWidth="Enabled" ID="ddlMigrationStatuses" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true"></telerik:RadComboBox>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/OrderAddressStatusKey.aspx" Target="_blank">( view status descriptions )</asp:HyperLink>
                </td>
    
            </tr>
            <tr>
                <td class="success right middlev ">Date Created</td>
                <td class="">
                    <telerik:RadDatePicker Skin="Bootstrap" ID="dteDateCreatedStart" runat="server"></telerik:RadDatePicker>
                </td>

                <td class="success  center middlev">to</td>
                <td class="">
                    <telerik:RadDatePicker Skin="Bootstrap" ID="dteDateCreatedEnd" runat="server"></telerik:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="success right middlev ">Address Type</td>
                <td class="" colspan="3">
                    <telerik:RadComboBox Skin="Bootstrap" NoWrap="true" DropDownAutoWidth="Enabled" ID="ddlOrderAddressTypes" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true"></telerik:RadComboBox>
                </td>
            </tr>

            <tr>
                <td class="success right" colspan="1">
                    <asp:LinkButton class="btn btn-link verticalspacer" ID="lnkReset" runat="server" OnClick="lnkReset_Click">Reset</asp:LinkButton>
                </td>
                <td class="" colspan="3">
                    <asp:Button class="btn btn-block btn-primary verticalspacer" ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />

                </td>
            </tr>

            <tr>
                <td class="success right" colspan="4">
                    <label id="lblWhereClause" runat="server"></label>
                </td>

            </tr>
        </tbody>
        
    </table>
</div>
