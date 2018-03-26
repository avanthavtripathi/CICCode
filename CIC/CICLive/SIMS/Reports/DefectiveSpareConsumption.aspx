<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true"
    CodeFile="DefectiveSpareConsumption.aspx.cs" Inherits="SIMS_Reports_DefectiveSpareConsumption" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="PendingSerRegReport" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <Triggers>
           <asp:PostBackTrigger ControlID="btnExportToExcel" />
        </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Spare Consumption Report
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
                                        ValidationGroup="editt" > </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RFRegionDesc" runat="server" ControlToValidate="ddlProductDivison"
                                        ErrorMessage="Division is required." InitialValue="0" SetFocusOnError="true"
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
                                        ID="btnSearch" runat="server" OnClick="btnSearch_Click" />
                                    &nbsp;
                                    <asp:Button Width="85px" Text="Export to Excel" CssClass="btn" ValidationGroup="editt"
                                        CausesValidation="true" ID="btnExportToExcel" Visible="false" 
                                        runat="server" onclick="btnExportToExcel_Click"   />
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
                                    <asp:GridView RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AutoGenerateColumns="false" 
                                        PagerStyle-HorizontalAlign="Center" ID="gvComm" runat="server" 
                                        Width="100%" HorizontalAlign="Left" onrowdatabound="gvComm_RowDataBound" >
                                        <Columns>
                                        <asp:BoundField DataField="Sno" HeaderText="Sno" />
                                        <asp:BoundField DataField="Region_Desc" HeaderText="Region" />
                                        <asp:BoundField DataField="Branch_Name" HeaderText="Branch" />
                                        <asp:BoundField DataField="SC_Name" HeaderText="SC Name" />
                                        <asp:BoundField DataField="ComplaintNo" HeaderText="Transaction Detail" />
                                        <asp:BoundField DataField="CustomerName" HeaderText="CustomerName" />
                                        <asp:BoundField DataField="UniqueContact_No" HeaderText="CustomerContact" />
                                        <asp:BoundField DataField="ProductDivision_Desc" HeaderText="ProductDivision" />
                                        <asp:BoundField DataField="lastcomment" HeaderText="ActionTaken" />
                                        <asp:TemplateField HeaderText="Total  Spare Consumed" >
                                        <ItemTemplate>
                                        <asp:Label ID="LblCount" runat="server" />
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Spare1" HeaderText="Spare1" />
                                        <asp:BoundField DataField="Spare2" HeaderText="Spare2" />
                                        <asp:BoundField DataField="Spare3" HeaderText="Spare3" />
                                        <asp:BoundField DataField="Spare4" HeaderText="Spare4" />
                                        <asp:BoundField DataField="Spare5" HeaderText="Spare5" />
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
