<%@ Page Title="" Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true" CodeFile="ServiceCost.aspx.cs" Inherits="SIMS_Reports_ServiceCost" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
<script language="javascript" type="text/javascript">
        function funSpareActivityDetail(compNo)
        {
            var strUrl='../Pages/SpareActivityLog.aspx?CompNo='+ compNo;
            window.open(strUrl,'History','height=550,width=850,left=20,scrollbars=1,top=30,Location=0');
        }
    </script>
   <script language="javascript" type="text/javascript">
       function SelectDate() {
           var my_date
           var indate
           var dDate, mMonth, yYear
           var DateFrom, DateTo
           DateFrom = Date.parse(document.getElementById("ctl00_MainConHolder_txtFromDate").value);
           DateTo = Date.parse(document.getElementById("ctl00_MainConHolder_txtToDate").value);
           if (DateFrom != "" && DateTo != "") {
               // The number of milliseconds in one day
               var ONE_DAY = 1000 * 60 * 60 * 24

               // Convert both dates to milliseconds
//               var date1_ms = DateTo.getTime()
//               var date2_ms = DateFrom.getTime()

               // Calculate the difference in milliseconds
               //  var difference_ms = Math.abs(date1_ms - date2_ms)
               var difference_ms = Math.abs(DateFrom - DateTo)

               if (Math.round(difference_ms / ONE_DAY) > 31) {
                   alert("Date difference can not be more then one month. \n Please change your date selection.");
                   return false;
               }
           }
           else
           {
            alert("Please select your date selection.");
            return false;
           }
       }
    </script>

    <asp:UpdatePanel ID="updatepnl" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportToExcel" />
        </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                       Within Warranty Complaint Wise Service Cost Report
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["SIMSAjaxLoadingImageName"]%>" alt="" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" colspan="2">
                        <table width="98%" border="0">
                            <tr>
                                <td colspan="2" align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                    <font color='red'>*</font>
                                    <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Divison<font color='red'>*</font>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlProductDivison" runat="server" Width="175px" CssClass="simpletxt1"
                                        ValidationGroup="editt" AutoPostBack="True" 
                                        onselectedindexchanged="ddlProductDivison_SelectedIndexChanged">
                                         <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="0"
                                                    SetFocusOnError="true" ControlToValidate="ddlProductDivison" 
                                        ValidationGroup="editt">Product Division is required.</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            
                                 <tr>
                                <td width="30%" align="right">
                                    Product Line
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlProductLine" runat="server" Width="175px" CssClass="simpletxt1"
                                        ValidationGroup="editt" 
                                        onselectedindexchanged="ddlProductLine_SelectedIndexChanged" 
                                        AutoPostBack="True">
                                         <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            
                                  <tr>
                                <td width="30%" align="right">
                                    Product Group
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="DdlProductGroup" runat="server" Width="175px" CssClass="simpletxt1"
                                        ValidationGroup="editt" >
                                         <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                 
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Activity 
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="DDlActivity" runat="server" Width="175px" CssClass="simpletxt1"
                                        ValidationGroup="editt">
                                       <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                               </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Region
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlRegion" runat="server" Width="175px" CssClass="simpletxt1"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" ValidationGroup="editt">
                                       <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Branch
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlBranch" runat="server" Width="175px" CssClass="simpletxt1"
                                        ValidationGroup="editt">
                                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Warranty Status
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlwarrst" runat="server" Width="175px" CssClass="simpletxt1" >
                                     <asp:ListItem Text="YES" Value="Y" Selected="True"></asp:ListItem>
                                     <asp:ListItem Text="NO" Value="N"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            
                            <tr>
                                <td align="right">
                                    Date From
                                    <div>(Logged Date)</div>
                                </td>
                                <td align="left">
                                    <asp:TextBox runat="server" ID="txtFromDate" CssClass="txtboxtxt" ValidationGroup="editt"
                                        MaxLength="10" />
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" Type="Date" Operator="DataTypeCheck"
                                        ControlToValidate="txtFromDate" Display="none" ValidationGroup="editt" SetFocusOnError="true"></asp:CompareValidator>
                                    <cc1:CalendarExtender ID="CEFrom" runat="server" TargetControlID="txtFromDate">
                                    </cc1:CalendarExtender>
                                    To
                                    <asp:TextBox runat="server" ID="txtToDate" CssClass="txtboxtxt" ValidationGroup="editt"
                                        MaxLength="10" />
                                    <asp:CompareValidator ID="CompareValidator7" runat="server" Type="Date" Operator="DataTypeCheck"
                                        ControlToValidate="txtToDate" Display="none" ValidationGroup="editt" SetFocusOnError="true"></asp:CompareValidator>
                                    <cc1:CalendarExtender ID="CETo" runat="server" TargetControlID="txtToDate">
                                    </cc1:CalendarExtender>
                                    <asp:CompareValidator ID="CompareValidator2" Type="Date" ControlToValidate="txtToDate"
                                        ControlToCompare="txtFromDate" Operator="GreaterThanEqual" runat="server" ErrorMessage="To Date should be greater than From Date"
                                        ValidationGroup="editt"></asp:CompareValidator>
                                    <asp:Label ID="lblDateErr" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                </td>
                                <td align="left">
                                    <br />
                                    <asp:Button Width="80px" Text="Search" CssClass="btn" ValidationGroup="editt" CausesValidation="true"
                                        ID="btnSearch" runat="server" OnClick="btnSearch_Click" />
                                    &nbsp;
                                    <asp:Button Width="85px" Text="Export to Excel" CssClass="btn" ValidationGroup="editt"
                                        CausesValidation="true" ID="btnExportToExcel" Visible="false" runat="server"
                                        OnClick="btnExportToExcel_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="MsgTDCount">
                                    Total Number of Records :
                                    <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="MsgTDCount" colspan="2">
                                    
                                     <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" PagerStyle-HorizontalAlign="Center" 
                                        ID="gvComm" runat="server" Width="100%" AutoGenerateColumns="false" 
                                        HorizontalAlign="Left" >
                                        <Columns>
                                         <asp:BoundField DataField="sno" HeaderText="S.No." />
                                         <asp:BoundField DataField="region_desc" HeaderText="Region" />
                                         <asp:BoundField DataField="productdivision_desc" HeaderText="Divison" />
                                         <asp:BoundField DataField="productline_desc" HeaderText="Product Line" />
                                         <asp:BoundField DataField="productgroup_desc" HeaderText="Product Group" />
                                         <asp:BoundField DataField="branch_name" HeaderText="Branch" />
                                         <asp:BoundField DataField="Complaintcount" HeaderText="Complaint count" />
                                         <asp:BoundField DataField="ClaimCount" HeaderText="Claim Count" />
                                         <asp:BoundField DataField="activity_description" HeaderText="Activity Name" />
                                         <asp:BoundField DataField="MaterialCost" HeaderText="Material Cost" />
                                         <asp:BoundField DataField="ServiceCost" HeaderText="Service Cost" />
                                         <asp:BoundField DataField="TotalCost" HeaderText="Total Cost" />

                                        </Columns>
                                        <EmptyDataTemplate>
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                        <img src="<%=ConfigurationManager.AppSettings["SIMSUserMessage"]%>" alt="" />
                                                        <b>No Record found</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                    <asp:GridView ID="gvExport" AutoGenerateColumns="false" runat="server" >
                                    
                                        <Columns>
                                            <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                <HeaderStyle Width="40px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Region_desc" SortExpression="Region_desc" HeaderStyle-Width="100px"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Region">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Branch_Name" SortExpression="Branch_Name" HeaderStyle-Width="100px"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Branch">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ASC_Name" SortExpression="ASC_Name" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Service Contractor">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProductDivision_Desc" SortExpression="ProductDivision_Desc"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Division">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Complaint_RefNo" SortExpression="Complaint_RefNo" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Complaint No">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Material_Cost" SortExpression="Material_Cost" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Material Cost">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Service_Cost" SortExpression="Service_Cost" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Service Cost">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Total_Cost" SortExpression="Total_Cost" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Total Cost">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                    </td>
                               
                            </tr>
                            <tr>
                    <td class="headingred">
                       
                    </td>
                  <td></td>
                </tr>
            <tr>
            <td colspan="2">
            <div id="dvsumm" runat="server"  style="text-align:left;display:none;"  class="headingred">
                       Complaint Wise Service Cost Report Summary
            </div>
            <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" 
                    GridGroups="both" PagerStyle-HorizontalAlign="Center" AutoGenerateColumns="false"
                                        ID="GridView1" runat="server" Width="100%" 
                                        HorizontalAlign="Left" CaptionAlign="Left"  >
                                       <Columns>
                                          <asp:BoundField DataField="region_desc" HeaderText="Region" />
                                          <asp:BoundField DataField="productdivision_desc" HeaderText="Divison" />
                                          <asp:BoundField DataField="productline_desc" HeaderText="Product Line" />
                                             <asp:BoundField DataField="productgroup" HeaderText="Product Group" />
                                             <asp:BoundField DataField="Complaintcount" HeaderText="Complaint count" />
                                             
                                            <asp:BoundField DataField="ClaimCount" HeaderText="Claim Count" />
                                             <asp:BoundField DataField="MaterialCost" HeaderText="Material Cost" />
                                             <asp:BoundField DataField="ServiceCost" HeaderText="Service Cost" />
                                             <asp:BoundField DataField="TotalCost" HeaderText="Total Cost" />
                                       </Columns>
                                        <RowStyle CssClass="gridbgcolor" />
                                        <PagerStyle HorizontalAlign="Center" />
                                         <EmptyDataTemplate>
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                        <img src="<%=ConfigurationManager.AppSettings["SIMSUserMessage"]%>" alt="" />
                                                        <b>No Record found</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                        <AlternatingRowStyle CssClass="fieldName" />
                                    </asp:GridView>
            </td>
            </tr>
            <tr>
            <td align="center" colspan="2">
                                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </td>
            </tr>
                        </table>
                      
                        
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

