<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="AverageResolutionTimeReportStatus.aspx.cs" Inherits="Reports_AverageResolutionTimeReportStatus"
    Title="Average Resolution Time Status Wise Report" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <table width="100%" border="0">
        <tr>
            <td class="headingred">
                MIS-BRM % Average Resolution Time Report with warranty status
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
                                <td style="width: 20%" valign="top">
                                    Warranty Status
                                </td>
                                <td style="width: 20%">
                                    <asp:DropDownList ValidationGroup="Report" Width="170px" runat="server" CssClass="simpletxt1"
                                        ID="ddlWarrantyStatus">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                    </asp:DropDownList>
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
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3" align="left" id="tdTotalRecord" runat="server">
                                    Total Number of Records :
                                    <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                   <div id="divReport"  style="width:100%;"  >                                      
                                         <asp:GridView ID="gvReport" CssClass="simpletxt1" runat="server" RowStyle-CssClass="bgcolorcomm"
                            Width="900px" AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                            GridGroups="both" HorizontalAlign="Left" AutoGenerateColumns="false" >
                            <Columns>
                            <asp:BoundField HeaderText="Region" DataField="Region" HeaderStyle-Width="150px" />
                            <asp:BoundField HeaderText="Period" DataField="MonthName" HeaderStyle-Width="250px" />
                            <asp:BoundField HeaderText="Fan" DataField="fan" HeaderStyle-Width="150px" />
                            <asp:BoundField HeaderText="Pumps" DataField="pumps" HeaderStyle-Width="150px" />
                            <asp:BoundField HeaderText="Lighting" DataField="lighting" HeaderStyle-Width="150px" />
                            <asp:BoundField HeaderText="Appliances" DataField="Appliances" HeaderStyle-Width="150px" />
                            <asp:BoundField HeaderText="FHPMotors" DataField="fhpmotors" HeaderStyle-Width="150px" />
                            <asp:BoundField HeaderText="LTMotors" DataField="ltmotors" HeaderStyle-Width="150px" />
                            
                            
                            </Columns>
                            
                            </asp:GridView>
                                    </div>
                                      <div style="text-align:center">
                                     <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn" Text="Export To Execl" Width="100" onclick="btnExport_Click" />
                                     </div>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="center">
                                    &nbsp;
                                </td>
                                <td>
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
                        <asp:PostBackTrigger ControlID="btnExport" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
