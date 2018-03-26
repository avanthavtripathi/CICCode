<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="NewPercentClosureRpt.aspx.cs" Inherits="Reports_NewPercentClosureRpt" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportToExcel" />
        </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                       % Closure Report
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
                                    <asp:DropDownList ID="ddlMonth" runat="server" Width="175px" CssClass="simpletxt1"
                                       ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                                    <tr>
                                <td align="right" width="30%">
                                    Region</td>
                                <td align="left">
                                   <asp:DropDownList ID="ddlRegion" runat="server" Width="175px" CssClass="simpletxt1"
                                       ValidationGroup="editt">
                                   </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="30%">
                                    Product Group</td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlPg" runat="server" CssClass="simpletxt1" Width="175px">
                                        <asp:ListItem Selected="True" Text="CP" Value="CP"></asp:ListItem>
                                        <asp:ListItem Text="IS" Value="IS"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="30%">
                                    &nbsp;</td>
                                <td align="left">
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
                                        runat="server"  />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table width="100%" border="0">
                            <tr>
                                <td>
                                     <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" ID="gvComm" runat="server" 
                                        Width="100%" HorizontalAlign="Left" RowStyle-Width="200px" HeaderStyle-Width="200px" HeaderStyle-HorizontalAlign="Left" >
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
                                <td align="center">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

