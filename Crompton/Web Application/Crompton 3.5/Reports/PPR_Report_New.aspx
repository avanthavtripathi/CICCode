<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="PPR_Report_New.aspx.cs" Inherits="Reports_PPR_Report_New" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
<asp:UpdatePanel ID="updatepnl" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportExcel" />
        </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Monthly Complaint - Defect Cost Report
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /> </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" colspan="2">
                        <table width="100%" border="0">
                           <tr>
                                <td align="right">
                                    Business Line
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlBusinessLine" AutoPostBack="true" runat="server" Width="175px"
                                        CssClass="simpletxt1" ValidationGroup="editt" 
                                        onselectedindexchanged="ddlBusinessLine_SelectedIndexChanged1">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Product Divison
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlProductDivison" runat="server" Width="175px" CssClass="simpletxt1"
                                        ValidationGroup="editt" AutoPostBack="True" OnSelectedIndexChanged="ddlProductDivison_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Product Line
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlProductLine" runat="server" Width="175px" CssClass="simpletxt1"
                                        ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                           
                            
                            <tr>
                                <td align="right">
                                    Month
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlMonth" runat="server" Width="175px" CssClass="simpletxt1" ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            
                            <tr>
                                <td align="right">
                                    Year
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlYear" runat="server" Width="175px" CssClass="simpletxt1" ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                             <tr>
                                <td align="center" style="color:Teal; font-weight:bold;" colspan="2">
                                    Check defect category to exclude from Report
                                </td>
                            </tr>
                            
                            <tr>
                                <td align="right">
                                    No Product Defect
                                </td>
                                <td align="left">
                                    <asp:CheckBox ID="chkNoDefect" runat="server" />
                               &nbsp;
                                    Others
                                &nbsp;
                                    <asp:CheckBox ID="ChkOthers" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                </td>
                                <td align="left">
                                    <br />
                                    <asp:Button Width="70px" Text="Search" CssClass="btn" ValidationGroup="editt" CausesValidation="true"
                                        ID="btnSearch" runat="server" OnClick="btnSearch_Click" />
                                    &nbsp;
                                    <asp:Button ID="btnExportExcel" runat="server" Width="114px" Text="Save To Excel"
                                        CssClass="btn" OnClick="btnExportExcel_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="MsgTDCount" colspan="2">
                                    Total Number of Complaints :
                                    <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                    <div style="width:945px; overflow:scroll;">
                                    <asp:GridView ID="gvComm" runat="server" AllowPaging="false" 
                                        AllowSorting="False" AlternatingRowStyle-CssClass="fieldName" 
                                        AutoGenerateColumns="False" GridGroups="both" 
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" HorizontalAlign="Left" 
                                        PagerStyle-HorizontalAlign="Center" PageSize="10" 
                                        RowStyle-CssClass="gridbgcolor" Visible="true" Width="4000px">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sno">
                                                <ItemTemplate>
                                                    <%#Eval("SNo")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Complaint NO" 
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <a href="Javascript:void(0);" 
                                                        onclick='funCommonPopUpReport(<%#Eval("BaseLineId")%>)'>
                                                    <%#Eval("Complaint_No")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Reported Date" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <%#Eval("Reported_Date")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="SLADate" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="SLA Date" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="Closing_Date" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Closing Date" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="InvoiceDate" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Invoice Date" ItemStyle-HorizontalAlign="Left" />    
                                            <asp:BoundField DataField="ComplaintAge" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="ComplaintAge" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="Region_Desc" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Region" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="Branch_Name" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Branch" ItemStyle-HorizontalAlign="Left" />
                                             <asp:BoundField DataField="ProductDivision_Desc" 
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Division" 
                                                ItemStyle-HorizontalAlign="Left" SortExpression="ProductDivision_Desc" />
                                            <asp:BoundField DataField="ProductLine_Desc" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Product Line" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="ProductGroup_Desc" 
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Product Group" 
                                                ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="Product_Code" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Product Code" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="Product_Desc" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Product Desc" ItemStyle-HorizontalAlign="Left" /> 
                                             <asp:BoundField DataField="PRODUCT_SR_NO" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="PRD_SR_NO" ItemStyle-HorizontalAlign="Left" />
                                             <asp:BoundField DataField="MFG PERIOD" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="MFG Period" ItemStyle-HorizontalAlign="Left" />
                                              <asp:BoundField DataField="COMPANY_NAME" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Company Name" ItemStyle-HorizontalAlign="Left" />
                                                      
                                              <asp:BoundField DataField="CutomerDetails" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Customer Details" ItemStyle-HorizontalAlign="Left" /> 
                                               <asp:BoundField DataField="SC_Name" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="SC Name" ItemStyle-HorizontalAlign="Left" /> 
                                                <asp:BoundField DataField="NatureOfComplaint" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="NatureOfComplaint" ItemStyle-HorizontalAlign="Left" />    
                                                <asp:BoundField DataField="Defect_Category" 
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Defect Category" 
                                                ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="Defect_Desc" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Defect Desc" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="Closure Remarks" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Closure Remark" ItemStyle-HorizontalAlign="Left" />
                                               <asp:BoundField DataField="Callstage" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Complaint Status" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="NUM_OF_DEF" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="NUM of Defects" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="MaterialCost" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Material Cost" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="ServiceCost" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Service Cost" ItemStyle-HorizontalAlign="Right" SortExpression="ServiceCost" />
                                            <asp:BoundField DataField="TotalCost" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Total Cost" ItemStyle-HorizontalAlign="Right" />
                                            
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td align="center" 
                                                        style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                        <img alt="" src='<%=ConfigurationManager.AppSettings["UserMessage"]%>' /> <b>No 
                                                        Record found</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                                    
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table width="100%" border="0">
                            <tr>
                                <td>
                                
                                </td>
                            </tr>
                            <tr id="trPaging">
                                <td>
                                   <asp:Repeater ID="repager" runat="server" onitemcommand="repager_ItemCommand" >
                                    <ItemTemplate>
                                    <asp:LinkButton Enabled='<%#Eval("Enabled") %>' Width="40px" BorderStyle="Ridge" style="text-decoration:none; text-align:center;" runat="server" ID="lnkPageNo" Text='<%#Eval("Text") %>' CommandArgument='<%#Eval("Value") %>' CommandName="PageNo"></asp:LinkButton>
                                    </ItemTemplate></asp:Repeater>
                              </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <br />
                                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text="" Visible="false"></asp:Label>
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

