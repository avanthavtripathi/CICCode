<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HistoryLog.aspx.cs" Inherits="pages_HistoryLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Crompton Greaves :: Customer Interaction Center</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/global.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="dv">
        <table width="100%">
            <tr bgcolor="white">
                <td class="headingred">
                    History Details
                </td>
                <td>
                    <a href="javascript:void(0)" class="links" onclick="window.close();">Close</a>
                </td>
            </tr>
            <tr>
                <td style="padding: 10px" align="center" colspan="2" class="bgcolorcomm">
                    <table width="99%" border="0">
                        <tr id="trLatestUser" runat="server">
                            <td width="25%" align="left">
                               <font color="#FF8040"><b>Latest Allocation To User</b></font>
                            </td>
                            <td align="left">
                                <b>
                                    <asp:Label ID="lblLastAllocation" runat="server"></asp:Label>&nbsp;<asp:Label ID="lblBline" runat="server" Visible="false"></asp:Label>
                                </b>
                            </td>
                        </tr>
                        <tr>
                            <td  align="left">
                                Complaint Ref No.
                            </td>
                            <td align="left">
                                <asp:Label ID="lblComplaintRefNo" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div id="dvGrid" style="width: 870px; height: 460px; overflow: auto;">
                                    <!-- Communication Details for Complaint Ref No Listing   -->
                                    <asp:GridView PageSize="13" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridLines="both" AllowPaging="True"
                                        AutoGenerateColumns="False" ID="gvHistory" runat="server" OnPageIndexChanging="gvHistory_PageIndexChanging"
                                        Width="99%" HorizontalAlign="Left" Style="margin-top: 0px">
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <asp:BoundField DataField="Sno" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="SNo">
                                                <HeaderStyle HorizontalAlign="Left" Width="30px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CommentDate" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="System Entry Date">
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
                                            <asp:BoundField DataField="Remarks" ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Remarks">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <img src="<%=ConfigurationManager.AppSettings["UserMessage"]%>" alt="" /><b>No Records
                                                found.</b>
                                        </EmptyDataTemplate>
                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                        <AlternatingRowStyle CssClass="fieldName" />
                                    </asp:GridView>
                                    <!-- End Communication Details for Complaint Ref No Listing -->
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
