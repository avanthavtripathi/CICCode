<%@ Page Title="" Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true" CodeFile="SIMSReport.aspx.cs" Inherits="SIMS_Reports_SIMSReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="SIMSReport" ContentPlaceHolderID="MainConHolder" runat="Server">


    <asp:UpdatePanel ID="updatepnl" runat="server">
        <Triggers>
        <%--    <asp:PostBackTrigger ControlID="btnExportToExcel" />--%>
        </Triggers>
        <ContentTemplate>
        <script language="JavaScript" type="text/javascript">

function days_between()
 {
    var fromdate = (document.getElementById('ctl00_MainConHolder_txtFromDate').value);
    var todate = (document.getElementById('ctl00_MainConHolder_txtToDate').value);
    var ONE_DAY = 1000 * 60 * 60 * 24
      var arrayFrom = fromdate.split('/');
         var arrayTo = todate.split('/');
    
        var arFromDate = new Date();
        var arToDate = new Date();
      
        arFromDate.setFullYear(arrayFrom[2], (arrayFrom[0] - 1), arrayFrom[1]);
        arFromDate.setHours(0, 0, 59, 0);
        arToDate.setFullYear(arrayTo[2], (arrayTo[0] - 1), arrayTo[1]);
        arToDate.setHours(0, 0, 59, 0);
        
    if (fromdate == '' || todate == '') {
        alert('Please Select Date range');
        return false;
    }
    else {
    
    var days = (arToDate - arFromDate) / ONE_DAY;
                if (arToDate < arFromDate) {
                    alert("From date can't be greater than To date.");
                    return false;
                }
                else if (days > 30)
                {
                 alert("Date Range can't be greater than 30 days.");
                return false;
                }
                else 
                {
                    if (typeof (Page_ClientValidate) == 'function') {
                        Page_ClientValidate();
                        return Page_IsValid;
                    }
                    else {
                        return true;

                    }                 
                  }
      
  }
 }
</script>
            <table width="100%">
                <tr>
                    <td class="headingred">
                       SIMS Report
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["SIMSAjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" colspan="2">
                        <table width="98%" border="0">
                            <tr>
                                <td colspan="2" align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Region<font color='red'>*</font>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlRegion" runat="server" Width="175px" CssClass="simpletxt1"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" ValidationGroup="editt">
                                    </asp:DropDownList>
                                      <asp:RequiredFieldValidator ID="Requirlidator1" runat="server" ControlToValidate="ddlRegion"
                                        ErrorMessage="Region is required." InitialValue="Select" SetFocusOnError="true"
                                        ValidationGroup="editt" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            
                            <tr>
                                <td width="30%" align="right">
                                    Branch<font color='red'>*</font>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlBranch" runat="server" Width="175px" CssClass="simpletxt1"
                                        ValidationGroup="editt"> 
                                        <asp:ListItem>Select</asp:ListItem>
                                    </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RFBranchDesc" runat="server" ControlToValidate="ddlBranch"
                                        ErrorMessage="Branch is required." InitialValue="Select" SetFocusOnError="true"
                                        ValidationGroup="editt" Display="Dynamic"></asp:RequiredFieldValidator>
                                   
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Divison<font color='red'>*</font>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlProductDivison" runat="server" Width="175px" CssClass="simpletxt1"
                                        ValidationGroup="editt" >
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RFRegionDesc" runat="server" ControlToValidate="ddlProductDivison"
                                        ErrorMessage="Division is required." InitialValue="Select" SetFocusOnError="true"
                                        ValidationGroup="editt" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            
                            <tr>
                                <td align="right">
                                    Date From
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
                                        ID="btnSearch" runat="server" OnClientClick ="javascript:return days_between();" OnClick="btnSearch_Click" />
                                    &nbsp;
                                 <%--   <asp:Button Width="85px" Text="Export to Excel" CssClass="btn" ValidationGroup="editt"
                                        CausesValidation="true" ID="btnExportToExcel" Visible="false" runat="server"
                                        OnClick="btnExportToExcel_Click" />--%>
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
                        </table>
                        <br />
                        <table width="100%" border="0">
                            <tr>
                                <td>                                   
                                    <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="True"
                                        PagerStyle-HorizontalAlign="Center" AutoGenerateColumns="false" 
                                        ID="gvComm" runat="server" OnPageIndexChanging="gvComm_PageIndexChanging" Width="100%"
                                        HorizontalAlign="Left" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sno" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
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
                                            <asp:BoundField DataField="sc_name" SortExpression="sc_name" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Service Contractor">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                              <asp:BoundField DataField="address1" SortExpression="address1" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="ASC Address">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                                  <asp:BoundField DataField="simsliveflag" SortExpression="simsliveflag" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Live in SIMS">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="createddate" SortExpression="createddate"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Activation Date">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MaterialClaims" SortExpression="MaterialClaims" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Material Claims">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ServiceClaims" SortExpression="ServiceClaims" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Service Claims">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="IndentCount" SortExpression="IndentCount" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Indents Count">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ComplaintCount" SortExpression="ComplaintCount" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Complaints Count">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
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
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
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


