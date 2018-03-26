<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="MDReportCP.aspx.cs" Inherits="Reports_MDReportCP" Title="Untitled Page" %>
<asp:Content ID="CntMd2" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportToExcel" />
        </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                       Complaint Ageing for CP 
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" colspan="2">
                        <table width="98%" border="0">
                            <tr>
                                <td width="30%" align="right">
                                   Year
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="Ddlyear" runat="server" Width="175px"
                                        CssClass="simpletxt1" ValidationGroup="editt" >
                                        <asp:ListItem>2012</asp:ListItem>
                                        <asp:ListItem>2013</asp:ListItem>
                                        <asp:ListItem>2014</asp:ListItem>
                                        <asp:ListItem>2015</asp:ListItem>
                                        <asp:ListItem>2016</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                   Month
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlMonth1" runat="server" Width="175px" CssClass="simpletxt1"
                                       ValidationGroup="editt">
                                    </asp:DropDownList>
                                      <asp:DropDownList ID="ddlMonth2" runat="server" Width="175px" CssClass="simpletxt1"
                                       ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Target Days
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="Ddlday" runat="server" Width="175px" CssClass="simpletxt1"
                                     ValidationGroup="editt">
                                        <asp:ListItem Selected="True" Value="0" Text="0-0"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="0-1"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="0-2"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="0-3"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="0-4"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="0-5"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="0-6"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="0-7"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr style="display:none;">
                                <td align="right" width="30%">
                                    Product Group 
                                    &nbsp;</td>
                                <td align="left">
                              <asp:DropDownList ID="ddlPg" runat="server" Width="175px" CssClass="simpletxt1" >
                                     <asp:ListItem Value="13,14,16,18" Text="CP" Selected="True"></asp:ListItem>
                              </asp:DropDownList>
                          <%--    Appliances 18 on live 23 on UAT--%>
                                    &nbsp;</td>
                            </tr>
                            
                            <tr>
                                <td align="right">
                                
                                </td>
                                <td align="left">
                                    <br />
                                    <asp:Button Width="70px" Text="Search" CssClass="btn" ValidationGroup="editt" CausesValidation="true"
                                        ID="btnSearch" runat="server" OnClick="btnSearch_Click" />
                                    &nbsp;
                                    <asp:Button Width="80px" Text="Export to Excel" CssClass="btn" ValidationGroup="editt"
                                        CausesValidation="true" ID="btnExportToExcel" Visible="false" 
                                        runat="server" onclick="btnExportToExcel_Click"
                                         />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table width="100%" border="0">
                            <tr>
                                <td>
                                     <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" ID="gvComm" runat="server" 
                                        Width="100%" HorizontalAlign="Left" >
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
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" >
                                *Data is Based on SLA Date
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


