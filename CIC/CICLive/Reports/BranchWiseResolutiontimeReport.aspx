<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="BranchWiseResolutiontimeReport.aspx.cs" Inherits="Reports_BranchWiseResolutiontimeReport"
    Title="Untitled Page" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <table width="100%" border="0">
        <tr>
            <td class="headingred">
                MIS-BRM % Branch Wise Resolution Time Report
            </td>
            <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                </asp:UpdateProgress>
            </td>
        </tr>
        <tr>
         <td colspan="2">
        <table id="tmptbl" runat="server" style="width:100%" >
        <tr>
            <td align="right" style="width: 30%">
                Region:
            </td>
            <td>
                <asp:DropDownList ID="ddlregion" runat="server" 
                    OnSelectedIndexChanged="ddlregion_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 30%">
                Branch:
            </td>
            <td>
                <asp:DropDownList ID="ddlbranch" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="Search" 
                    onclick="btnSearch_Click" />
            </td>
        </tr>
        </table>
        </td>
        </tr>
        <tr>
            <td class="bgcolorcomm" colspan="2">
                <asp:UpdatePanel ID="pnl" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table width="100%" border="0" cellpadding="1" cellspacing="0">
                            <tr>
                                <td align="left" id="tdTotalRecord" runat="server">
                                    <%--  Total Number of Records :
                                    <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divReport" style="width: 100%; height: 450px;">
                                        <rsweb:ReportViewer ID="rvMisDeatil" Width="100%" Font-Names="Verdana" Font-Size="8pt"
                                            runat="server">
                                        </rsweb:ReportViewer>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    &nbsp;
                                </td>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="rvMisDeatil" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
