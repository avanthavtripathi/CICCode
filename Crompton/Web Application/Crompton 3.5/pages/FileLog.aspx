<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileLog.aspx.cs" Inherits="pages_FileLog" %>

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
                    File Details
                </td>
                <td>
                    <a href="javascript:void(0)" class="links" onclick="window.close();">Close</a>
                </td>
            </tr>
            <tr>
                <td style="padding: 10px" align="center" colspan="2" class="bgcolorcomm">
                    <table width="99%" border="0">                       
                        <tr>
                            <td width="15%" align="left">
                                Complaint Ref No.
                            </td>
                            <td align="left">
                                <asp:Label ID="lblComplaintRefNo" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div id="dvGrid" style="width: 800px; height: 460px; overflow: auto;">
                                    <!-- Communication Details for Complaint Ref No Listing   -->
                                    <asp:GridView PageSize="13" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridLines="both" AllowPaging="True"
                                        AutoGenerateColumns="False" ID="gvHistory" runat="server" OnPageIndexChanging="gvHistory_PageIndexChanging"
                                        Width="99%" HorizontalAlign="Left" Style="margin-top: 0px">
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <asp:BoundField DataField="Sno" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="SNo">
                                                <HeaderStyle HorizontalAlign="Left" Width="20px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>                                            
                                             <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderText="File Name">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName='<%#Eval("FileName")%>'
                                                         OnClick="LinkButton1_Click" Text='<%#Eval("FileName")%>'>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                           
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
                                <asp:Label ID="lblMsg" runat="server" Visible="false"></asp:Label>
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
