<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConfigSettingItemDetail.aspx.cs" Inherits="Level3.AddressManagement.UI.Web.ConfigSettingItemDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h3>Config Setting Detail:
    <label id="lblHeader" runat="server" class="text-info"></label>
    </h3>
    <asp:Label ID="lblErrorMessage" runat="server" Text="" class="alert-danger"></asp:Label>


    <div class="panel panel-default">
        <table class="table table-bordered table-condensed">
            <thead>
            </thead>
            <tbody>
                <tr>
                    <td class="info col-sm-2">Config Setting Name</td>
                    <td class="col-sm-10">
                        <label id="lblConfigSettingName" runat="server"></label>
                    </td>
                </tr>
                <tr>
                    <td class="info col-sm-2">Setting Value</td>
                    <td class="col-sm-10">
                        <label id="lblConfigSettingValue" runat="server"></label>
                        <asp:LinkButton ID="lnkEdit" runat="server" OnClick="lnkEdit_Click">[ Edit ]</asp:LinkButton>

                        <asp:TextBox ID="txtConfigSettingValue" runat="server"></asp:TextBox>
                        <asp:LinkButton ID="lnkSave" runat="server"  OnClick="lnkSave_Click">[Save]</asp:LinkButton>
                        
                        <asp:LinkButton ID="lnkCancel" runat="server" OnClick="lnkCancel_Click">[Cancel]</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td class="info col-sm-2">Last Update Date</td>
                    <td class="col-sm-10">
                        <label id="lblLastUpdateDate" runat="server"></label>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
   <div>
       <a runat="server" href="~/SysAdmin"><< Back to list</a>
   </div>

</asp:Content>