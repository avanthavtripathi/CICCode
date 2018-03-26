<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="SCLocator.aspx.cs" Inherits="pages_SCLocator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
     <asp:UpdatePanel ID="pnl" runat="server" UpdateMode="Always">
     <Triggers>
     <asp:PostBackTrigger ControlID="BtnDownLoad" />
     </Triggers>
                                <ContentTemplate>
<table width="100%">
<tr>
<td></td>

 <td align="right" style="padding-right: 10px;">
   <asp:UpdateProgress AssociatedUpdatePanelID="pnl" ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
        </ProgressTemplate>
   </asp:UpdateProgress>
</td>

</tr>
<tr>
     <td class="headingred" >
         Service Contractor Locator
     </td>
     <td align="right" style="padding-right: 50px">
     <asp:LinkButton ID="BtnDownLoad" runat="server" Text="Download All India SC List" CausesValidation="false"  
             onclick="BtnDownLoad_Click" />
     </td>
</tr>

            
              <tr>
        <td>
            Product Division<font color='red'>*</font>
        </td>
        <td>
            <asp:DropDownList Width="170px" AutoPostBack="true" ID="ddlProductDiv" runat="server"
                CssClass="simpletxt1" 
                onselectedindexchanged="ddlProductDiv_SelectedIndexChanged" >
            </asp:DropDownList>
            <asp:CompareValidator SetFocusOnError="true" ID="compValProdDiv" runat="server" ControlToValidate="ddlProductDiv"
                ErrorMessage="Product Division is required." Operator="NotEqual" ValueToCompare="0"
                Display="None"></asp:CompareValidator>
        </td>
    </tr>
      <tr>
        <td>
            Product Line
        </td>
        <td>
            <asp:DropDownList Width="170px" ID="ddlProductLine" runat="server" CssClass="simpletxt1"
               >
                <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
            </asp:DropDownList>
        </td>
        </tr>
        <tr>
    <td width="200px">
        State<font color="red">*</font>
    </td>
    <td>
        <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True" 
            CssClass="simpletxt1" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" 
            Width="170px">
        </asp:DropDownList>
        <asp:CompareValidator ID="CompareValidator1" runat="server" 
            ControlToValidate="ddlState" Display="None" ErrorMessage="State is required." 
            Operator="NotEqual" SetFocusOnError="true" ValueToCompare="Select"></asp:CompareValidator>
    </td>
    </tr>
        <tr>
           <td>
            City<font color='red'>*</font>
        </td>
        <td>
            <asp:DropDownList Width="170px" ID="ddlCity" runat="server" CssClass="simpletxt1"
               CausesValidation="True">
                <asp:ListItem Text="Select" Value="Select" Selected="True"></asp:ListItem>
            </asp:DropDownList>
             <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator2" runat="server"
                ControlToValidate="ddlCity" ErrorMessage="City is required." Operator="NotEqual"
                ValueToCompare="Select" Display="None"></asp:CompareValidator>
               </td>
        </tr>
    <tr>
    <td>
    </td>
    <td>
    <asp:Button ID="btnSubmit" runat="server" CssClass="btn" Text="Search" Width="70px" 
            onclick="btnSubmit_Click" />
    </td>
    </tr>
    <tr>
    <td colspan="2">
    
    </td>
    </tr>
</table>
<asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor"
                            AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                            GridGroups="both" ID="gvComm" AutoGenerateColumns="false"
                            runat="server" Width="98%" Visible="true" >
    <Columns>
    <asp:BoundField DataField="sc_name" HeaderText="Service Contractor" />
    <asp:BoundField DataField="contact_person" HeaderText="Contact Person" />
   <%-- <asp:BoundField DataField="ProductLine_desc" HeaderText="Product Line" />--%>
  <%--  <asp:BoundField DataField="city_desc" HeaderText="City" />
    <asp:BoundField DataField="Territory_Desc" HeaderText="Territory" />--%>
    <asp:BoundField DataField="Address" HeaderText="Address" />
    <asp:BoundField DataField="phoneno" HeaderText="Phone No" />
    <asp:BoundField DataField="mobileno" HeaderText="Mobile No" />
    <asp:BoundField DataField="weekly_off_day" HeaderText="weekly Off" />

           
    </Columns>                         
         <EmptyDataTemplate>
           <img src="<%=ConfigurationManager.AppSettings["UserMessage"]%>" alt="" /><b>No records found</b>
         </EmptyDataTemplate>                     
    </asp:GridView>
    
    <asp:GridView RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
      GridGroups="both" ID="GvExcel" runat="server" Width="98%" Visible="false" >
         <EmptyDataTemplate>
           <img src="<%=ConfigurationManager.AppSettings["UserMessage"]%>" alt="" /><b>No records found</b>
         </EmptyDataTemplate>                     
    </asp:GridView>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

