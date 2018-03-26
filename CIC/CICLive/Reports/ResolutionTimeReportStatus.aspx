<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="ResolutionTimeReportStatus.aspx.cs" Inherits="Reports_ResolutionTimeReportStatus"
    Title="Resolution Time Report With Warranty Status" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <table width="100%" border="0">
        <tr>
            <td class="headingred">
                MIS-BRM Resolution Time Report with warranty status 0-2 days for CP & 0-3 for Motors
            </td>
            <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                </asp:UpdateProgress>
            </td>
        </tr>
        <tr>
            <td class="bgcolorcomm" colspan="2">
                <asp:UpdatePanel ID="pnl" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table width="100%" border="0" cellpadding="1" cellspacing="0">
                            <tr>
                                <td style="width: 15%" valign="top">
                                    Warranty Status
                                </td>
                                <td style="width: 20%">
                                    <asp:DropDownList ValidationGroup="Report" Width="170px" runat="server" CssClass="simpletxt1"
                                        ID="ddlWarrantyStatus">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                    </asp:DropDownList>
                                    <br />
                                    <asp:RequiredFieldValidator ID="reqProduct" runat="server" ControlToValidate="ddlWarrantyStatus"
                                        ValidationGroup="Report" ErrorMessage="select Warranty Status" Display="Dynamic"
                                        InitialValue="0"></asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 48%" align="left">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="Search" ValidationGroup="Report"
                                        CausesValidation="true" Width="70px" OnClick="btnSearch_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="left" id="tdTotalRecord" runat="server">
                                    Total Number of Records :
                                    <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <%--<div id="divReport" style="width: 100%; height: 450px;">--%>
                                        <rsweb:ReportViewer ID="rvMisDeatil" Width="100%" Font-Names="Verdana" Font-Size="8pt"
                                            runat="server">
                                        </rsweb:ReportViewer>
                                    <%--</div>--%>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="center">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="center">
                                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSearch" />
                        <asp:PostBackTrigger ControlID="rvMisDeatil" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
