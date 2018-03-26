<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true" CodeFile="SIMSIndent.aspx.cs" Inherits="SIMS_Reports_SIMSIndent" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
     <Triggers>
            <asp:PostBackTrigger ControlID="btnExportToExcel" />
        </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Spare Indent Report
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
                                    Region
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlRegion" runat="server" Width="175px" CssClass="simpletxt1"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Branch
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlBranch" runat="server" Width="175px" CssClass="simpletxt1"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    ASC
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlASC" runat="server" Width="175px" CssClass="simpletxt1"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlASC_SelectedIndexChanged" ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Divison<font color='red'>*</font>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlProductDivison" runat="server" Width="175px" CssClass="simpletxt1"
                                        ValidationGroup="editt">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RFRegionDesc" runat="server" ControlToValidate="ddlProductDivison"
                                        ErrorMessage="Division is required." InitialValue="0" SetFocusOnError="true"
                                        ValidationGroup="editt" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Indent Status 
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlstatus" runat="server" Width="175px" CssClass="simpletxt1"
                                        ValidationGroup="editt">
                                    <asp:ListItem Text="Select" Value="-1"></asp:ListItem>    
                                    <asp:ListItem Text="Closed" Value="0"></asp:ListItem>    
                                    <asp:ListItem Text="Open" Value="1"></asp:ListItem>  
                                    </asp:DropDownList>
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
                                        ID="btnSearch" runat="server" OnClick="btnSearch_Click" />
                                    &nbsp;
                                    <asp:Button Width="85px" Text="Export to Excel" CssClass="btn" ValidationGroup="editt"
                                        CausesValidation="true" ID="btnExportToExcel" Visible="false" 
                                        runat="server" onclick="btnExportToExcel_Click" />
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
                                        PagerStyle-HorizontalAlign="Center" AllowSorting="True" AutoGenerateColumns="False"
                                        ID="gvComm" runat="server" OnPageIndexChanging="gvComm_PageIndexChanging" Width="100%"
                                        HorizontalAlign="Left">
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
                                                  <asp:BoundField DataField="SIMS_Indent_No" SortExpression="SIMS_Indent_No"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Indent No">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                                  <asp:BoundField DataField="SAP_Sales_Order_Date" SortExpression="SAP_Sales_Order_Date"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="IndentDate">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                                <asp:BoundField DataField="Spare_Desc" SortExpression="Spare_Desc" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Spare">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                               <asp:BoundField DataField="Ordered_Qty" SortExpression="Ordered_Qty" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Ordered Qty">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                                     <asp:BoundField DataField="Recieved_Qty" SortExpression="Recieved_Qty" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Recieved Qty">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                                        <asp:BoundField DataField="Total_Pending" SortExpression="Total_Pending" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Pending Qty">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                                         <asp:BoundField DataField="SAP_Invoice_No" SortExpression="SAP_Invoice_No" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Invoice No">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SAP_Invoice_Date" SortExpression="SAP_Invoice_Date" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Invoice Date">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="CallStage" SortExpression="CallStage" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="CallStage">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                              <asp:BoundField DataField="StageDesc" SortExpression="StageDesc" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Status">
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
                                    <div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                  <div class="headingred">
                                  Summary
                                </div>
                                <div>
                                <asp:GridView ID="gvSummary" runat="server" AutoGenerateColumns="false" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both">
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
                                               <asp:BoundField DataField="TotalCount" SortExpression="TotalCount"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Total no of indent received">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                                 <asp:BoundField DataField="ClosedCount" SortExpression="ClosedCount"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Total no of Invoices executed">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                                 <asp:BoundField DataField="Pending" SortExpression="Pending"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Pending for delivery">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>  
                                            
                                       </Columns>
                                    </asp:GridView>
                                 </div>
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
                                       <asp:GridView PagerStyle-HorizontalAlign="Center" AutoGenerateColumns="False"
                                        ID="GvExcel" runat="server" Width="100%"    
                                        RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName" 
                                            
        HeaderStyle-CssClass="fieldNamewithbgcolor"  >
                                        <Columns>
                                            <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                <HeaderStyle Width="40px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Region_desc" SortExpression="Region_desc" HeaderStyle-Width="100px"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Region">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Branch_Name" SortExpression="Branch_Name" HeaderStyle-Width="100px"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Branch">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ASC_Name" SortExpression="ASC_Name" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Service Contractor">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProductDivision_Desc" SortExpression="ProductDivision_Desc"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Division">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                                  <asp:BoundField DataField="SIMS_Indent_No" SortExpression="SIMS_Indent_No"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Indent No">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                                  <asp:BoundField DataField="SAP_Sales_Order_Date" SortExpression="SAP_Sales_Order_Date"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="IndentDate">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                                <asp:BoundField DataField="Spare_Desc" SortExpression="Spare_Desc" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Spare">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                               <asp:BoundField DataField="Ordered_Qty" SortExpression="Ordered_Qty" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Ordered Qty">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                                     <asp:BoundField DataField="Recieved_Qty" SortExpression="Recieved_Qty" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Recieved Qty">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                                        <asp:BoundField DataField="Total_Pending" 
                                                SortExpression="Total_Pending" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Pending Qty">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                                         <asp:BoundField DataField="SAP_Invoice_No" 
                                                SortExpression="SAP_Invoice_No" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Invoice No">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SAP_Invoice_Date" 
                                                SortExpression="SAP_Invoice_Date" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Invoice Date">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="CallStage" SortExpression="CallStage" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="CallStage">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                              <asp:BoundField DataField="StageDesc" SortExpression="StageDesc" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Status">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                        </Columns>
                                </asp:GridView>
                                    </asp:Content>

