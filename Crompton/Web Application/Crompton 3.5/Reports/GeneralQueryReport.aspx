<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="GeneralQueryReport.aspx.cs" Inherits="Reports_GeneralQueryReport" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <table width="100%">
        <tr>
            <td class="headingred">
                General Query Report
            </td>
            <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </td>
        </tr>
        <tr>
            <td class="bgcolorcomm" colspan="2">
                <asp:UpdatePanel ID="pnl" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table width="100%" border="0" cellpadding="1" cellspacing="0">
                            <tr>
                                <td style="width: 24%">
                                    Log Date From
                                </td>
                                <td align="left">
                                    <asp:TextBox runat="server" ID="txtFromDate" Width="165px" CssClass="txtboxtxt" ValidationGroup="Report"
                                        MaxLength="10" />
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" Type="Date" Operator="DataTypeCheck"
                                        ControlToValidate="txtFromDate" Display="none" ValidationGroup="Report" SetFocusOnError="true"></asp:CompareValidator>
                                    <cc1:CalendarExtender ID="CEFrom" runat="server" TargetControlID="txtFromDate">
                                    </cc1:CalendarExtender>
                                </td>
                                <td style="width: 8%">
                                    To
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtToDate" Width="165px" CssClass="txtboxtxt" ValidationGroup="Report"
                                        MaxLength="10" />
                                    <asp:CompareValidator ID="CompareValidator7" runat="server" Type="Date" Operator="DataTypeCheck"
                                        ControlToValidate="txtToDate" Display="none" ValidationGroup="Report" SetFocusOnError="true"></asp:CompareValidator>
                                    <cc1:CalendarExtender ID="CETo" runat="server" TargetControlID="txtToDate">
                                    </cc1:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 24%">
                                    Query Type
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlQueryType" runat="server" AutoPostBack="true" CssClass="simpletxt1"
                                        Width="170px" OnSelectedIndexChanged="ddlQueryType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <div id="divOther" runat="server" visible="false">
                                    <td style="width: 8%">
                                        Other
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOther" runat="server" CssClass="txtboxtxt" Height="20" MaxLength="50"
                                            TextMode="MultiLine" ValidationGroup="Report" Width="165px"></asp:TextBox>
                                    </td>
                                </div>
                            </tr>
                            <tr>
                                <td style="width: 24%" valign="top">
                                    State
                                </td>
                                <td style="width: 28%">
                                    <asp:DropDownList ValidationGroup="Report" Width="170px" runat="server" CssClass="simpletxt1"
                                        ID="ddlState" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 8%" valign="top">
                                    City
                                </td>
                                <td valign="top">
                                    <asp:DropDownList ValidationGroup="Report" Width="170px" ID="ddlCity" runat="server"
                                        CssClass="simpletxt1">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 24%" valign="top">
                                    Agent
                                </td>
                                <td style="width: 28%">
                                    <asp:TextBox ID="txtAgent" runat="server" CssClass="txtboxtxt" ValidationGroup="Report"
                                        Width="165px"></asp:TextBox>
                                </td>
                                <td style="width: 8%" valign="top">
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn" OnClick="btnSearch_Click"
                                        Text="Search" ValidationGroup="Report" CausesValidation="true" Width="70px" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="left" id="tdTotalRecord" runat="server">
                                    Total Number of Records :
                                    <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <div>
                                        <asp:GridView RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                            HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="True"
                                            PagerStyle-HorizontalAlign="Center" AllowSorting="True" AutoGenerateColumns="False"
                                            ID="gvComm" runat="server" OnPageIndexChanging="gvComm_PageIndexChanging" Width="100%"
                                            HorizontalAlign="Left" OnSorting="gvComm_Sorting" 
                                            OnRowDataBound="gvComm_RowDataBound">
                                            <RowStyle CssClass="gridbgcolor" />
                                            <Columns>
                                                <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                  <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CreatedBy" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Agent Name">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="UniqueContact_No" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Calling Number">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="QueryType" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Query">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FirstName" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Caller's First Name">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LastName" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Caller's Last Name">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Remarks" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="CIC Agent Remarks">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Center" />
                                            <EmptyDataTemplate>
                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                            <img src="<%=ConfigurationManager.AppSettings["UserMessage"]%>" alt="" />
                                                            <b>No Record found</b>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                            <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                            <AlternatingRowStyle CssClass="fieldName" />
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center">
                                    <asp:Button ID="btnExport" CssClass="btn" Width="100px" runat="server" OnClick="btnExport_Click"
                                        Text="Export to Excel" />
                                </td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center">
                                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnExport" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
