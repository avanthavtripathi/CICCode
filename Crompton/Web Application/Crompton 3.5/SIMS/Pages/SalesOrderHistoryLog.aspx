<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalesOrderHistoryLog.aspx.cs"
    Inherits="SIMS_Pages_SalesOrderHistoryLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Crompton Greaves :: Customer Interaction Center</title>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/global.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="dv">
        <table width="100%">
            <tr bgcolor="white">
                <td class="headingred">
                    Sales Order History Details
                </td>
                <td>
                    <a href="javascript:void(0)" class="links" onclick="window.close();">Close</a>
                </td>
            </tr>
            <tr>
                <td style="padding: 10px" align="center" colspan="2" class="bgcolorcomm">
                    <table width="99%" border="0">
                        <tr id="trPonumber" runat="server">
                            <td align="left" width="10%">
                                PO Number.
                            </td>
                            <td align="left">
                                <b>
                                    <asp:Label ID="lblPoNumber" runat="server" Text=""></asp:Label>
                                </b>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div id="dvGrid" style="width: 870px; height: 460px; overflow: auto;">
                                    <asp:GridView PageSize="13" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridLines="both" AllowPaging="True"
                                        AutoGenerateColumns="False" ID="gvHistory" runat="server" OnPageIndexChanging="gvHistory_PageIndexChanging"
                                        Width="99%" HorizontalAlign="Left" Style="margin-top: 0px" OnRowDataBound="gvHistory_RowDataBound">
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <asp:BoundField DataField="Sno" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="SNo">
                                                <HeaderStyle HorizontalAlign="Left" Width="30px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ActionDate" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Actual Action Date">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CallStage" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Stage">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="StageDesc" ItemStyle-Width="180px" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Status">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Name" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="User">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="RoleName" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Role">
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
                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                        <AlternatingRowStyle CssClass="fieldName" />
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
