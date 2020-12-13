<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Level3.AddressManagement.UI.Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div style="background: !important" class="jumbotron">
       
            <div class="col-sm-12 col-md-12">
  <div class="answer-picture col-xs-12" id="bookListDiv">
    <div id="loadme" style="display:block; margin:0 auto;text-align:center">
      <img alt="App logo" src="Images/App_Logo.png" style="width: 536px; height: 278px"/>
    </div>
  </div>
</div>

           
       
        <p class="lead" style="text-decoration: underline">System Overview:</p>
        <p class="lead">This application is responsible for adding <strong><font color="red">&quot;MISSING&quot;</font> ORDER ADDRESSES</strong> to <strong>GLM</strong> and <strong>SAP</strong>, so that orders with shipments bound-for-delivery to those missing addresses <strong>can be fulfilled from within SAP</strong>.&nbsp; It also ensures that GLM assigns <strong>SITE CODES</strong> and <strong>S CODES</strong> to EON and Pipeline customer addresses, since both the dowstream <strong>&#39;SAP Address Import&#39;</strong> and <strong>&#39;Spares&#39;</strong> processes are <strong>dependant upon those values</strong> and expect them to be pre-assigned in GLM.</p>

        <p></p>
        <p></p>

        <p class="lead" style="text-decoration: underline">Technical Design Documentation:</p>
        <p class="lead">The technical design document detailing the programmatic flow and API calls orchestrated by this application can be downloaded <asp:HyperLink ID="lnkPDF" runat="server" NavigateUrl="~/FileDisplayHandler.aspx?strFileName=" Target="_blank">here... get ready to zoom and horizontally scroll for days</asp:HyperLink></p>
         <p></p>
        <p></p>

        <p class="lead" style="text-decoration: underline">Background Info:</p>
        <p class="lead">After SAP Go-Live, it was discovered that users processing part requisitions in SAP were <strong>unable to fulfill orders originating in EON and PIPELINE</strong> (order management systems).&nbsp; Root cause analysis conducted proved that the absence of the addresses in SAP was inherent to the fact that EON and PIPELINE are <strong>NOT integrated with GLM</strong>.&nbsp; GLM is the System of Record for customer SITES and SERVICE LOCATIONS to which parts are shipped, and is thereby the mechanism through which new and changed delivery addresses make their way downstream into SAP for order fulfillment.</p>

        <p class="lead">This was not an issue prior to SAP Go-Live because the old part requisition system allowed users to key-in addresses &quot;ad-hoc&quot; and as they were found to be &quot;missing&quot;, thereby allowing users to fulfill orders originating in EON and PIPELINE.</p>
        
        <p></p>
        <p></p>

        <p class="lead" style="text-decoration: underline">Data Sources, Integrations, and System Architecture:</p>
        <p class="lead">In order to identify pertinant order addresses, a back-end query is executed to pull EON and Pipeline addresses&nbsp; from the Corporate Data Warehouse (<strong>CDW</strong>).&nbsp; The CDW is <strong>refreshed once per day</strong>, and so this application in-turn picks-up new order addresses once per day.&nbsp; Once an address is identified as &quot;missing&quot;, it is queued for processing.&nbsp; A number of calls are then made to <strong>APIs</strong> resident in <strong>GLM</strong> and <strong>SAP</strong>, to locate and prep the address record for import into SAP.&nbsp; In come cases, API interactions with GLM trigger an <strong>EMP </strong>event to SAP, which then imports the address from GLM as part of its <strong>next regularly scheduled batch run (every 30 mins)</strong>.&nbsp; In select cases, a direct API call to SAP is made to force the import of the address from GLM in <strong>REAL-TIME,</strong> and circumventing a 48 hour Site Code assignment lead-time 
            window.&nbsp; </p>
        <p class="lead">While the system does re-attempt failed API calls, records yielding <strong>
            multiple failed API calls</strong> will be flagged accordingly and<strong>
            must be &quot;worked&quot; </strong>by end users of this application.&nbsp; &quot;Working&quot; an address can consist of <strong>correcting typos in the ordering system of record</strong> or getting system administrators to <strong>manaully add the address to GLM and/or SAP</strong>.</p>
        <p class="lead" style="text-decoration: underline">How to Use this App:</p>
        <p class="lead">As addresses navigate the various steps orchestrated by this system, they are given a &quot;status&quot; that identifies where they currently reside in the workflow.&nbsp; A list of the various statuses that an address can assume as it moves through the system is provided <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/OrderAddressStatusKey.aspx" Target="_blank">here</asp:HyperLink>.</p>

        <p class="lead">The <b><u><font color="red">'Issues'</font></u></b> tab lists any/all/only order addresses that need some form of user interaction and/or who&#39;s last API call to a downstream system failed.  In other words, this tab shows only the order addresses with issues, and filters out records that are behaving as expected on their way downstream into GLM and SAP.  Much like 'true beauty', issues in this system are 'timeless' and will not dissapear from this tab until they are resolved through successful transmission or until they are flagged as 'IGNORED' by an end user.</p>

        <p class="lead">The <b><u><font color="red">'Recent'</font></u></b> tab lists all &quot;missing&quot; addresses added to EON or PIPELINE within the last 14 days and that are being managed by this application, regardless of their status.</p>

        <p class="lead">The <b><u><font color="red">'All In Workflow'</font></u></b> tab lists order addresses that are moving through the migration worlflow without issue.</p>

        <p class="lead">The <b><u><font color="red">'Search'</font></u></b> tab allows users to search all address records in the system by specifying one or many search criteria ('System of Record', 'Order Number', 'Order Date', 'Address Line One', 'Migration Status', 'Date Created', 'Address Type').  When a user clicks the 'search' button, the system embeds the search criteria in the link/web address to the page, allowing users to 'bookmark' repetitive searches (so users can avoid the tedious task of re-specifynig search criteria for common searches and so that searches can be shared with other users by simply copying/pating the page's link).</p>

        <p class="lead">The <b><u><font color="red">'Admin'</font></u></b> tab allows users to view the current set of product codes that define how the system identifies a &quot;managed service order&quot;.  Users with proper permissions can configure/change the values appearing on this tab.</p>

         <p class="lead">The <b><u><font color="red">'System Log'</font></u></b> tab allows users to view the the log of major system events that have occurred over the last few weeks, including data syncs with the CDW  and product code value changes.</p>

        <p class="lead">The <b><u><font color="red">'API Call Log'</font></u></b> tab allows users to view the the log of all web service calls made to GLM and SAP, along with their resulting status codes.  The first column of the grid provides a link that can be used to pull up the detail page for the address responsible for the calls.</p>

    </div>
  
</asp:Content>
