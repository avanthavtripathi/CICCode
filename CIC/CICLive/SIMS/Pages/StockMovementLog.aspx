<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockMovementLog.aspx.cs"
    Inherits="pages_StockMovementLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Stock Movement Log</title>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="Mgr1" runat="server">
    </asp:ScriptManager>
    <div>
        <asp:UpdatePanel ID="Panel" runat="server">
            <ContentTemplate>
                <table width="100%" border="0">
                    <tr height="50">
                        <td class="headingred">
                            Stock Movement Log
                        </td>
                        <td height="30" align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>"
                            style="padding-right: 10px;">
                            <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                <ProgressTemplate>
                                    <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0">
                    <tr>
                        <td style="padding: 10px; width: 15%">
                            <strong>Service Contractor:</strong>
                        </td>
                        <td style="padding: 10px; width: 33%">
                            <strong>
                                <asp:Label ID="lblASCName" runat="server"></asp:Label></strong>
                            <asp:HiddenField ID="hdnASC_Code" runat="server" />
                        </td>
                        <td style="padding: 10px; width: 48%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 10px; width: 15%">
                            <strong>Product Division:</strong>
                        </td>
                        <td style="padding: 10px; width: 33%">
                            <asp:DropDownList ID="ddlProdDivision" runat="server" AutoPostBack="True" CssClass="simpletxt1"
                                OnSelectedIndexChanged="ddlProdDivision_SelectedIndexChanged" ValidationGroup="editt"
                                Width="190">
                            </asp:DropDownList>
                        </td>
                        <td style="padding: 10px; width: 48%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0">
                    <tr>
                        <td style="padding: 10px" align="left" class="bgcolorcomm" colspan="2">
                            <asp:GridView ID="GVStockMovLog" AllowPaging="True" runat="server" AutoGenerateColumns="False"
                                AllowSorting="True" AlternatingRowStyle-CssClass="fieldName" AlternatingRowStyle-HorizontalAlign="Left"
                                HeaderStyle-CssClass="fieldNamewithbgcolor" HeaderStyle-HorizontalAlign="Left"
                                PagerStyle-CssClass="Paging" PageSize="20" RowStyle-CssClass="gridbgcolor" RowStyle-HorizontalAlign="Left"
                                Width="100%" OnPageIndexChanging="GVStockMovLog_PageIndexChanging">
                                <Columns>
                                    <%--<asp:TemplateField HeaderText="ASC Name">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("SC_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Spare Code & Description">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Spare_Desc")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="From Location">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("From_Location")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To Location">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("To_Location")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transferred Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Transfered_Qty")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transferred Date">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Transfer_Date")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Created By">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Created_By")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Created Date">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Created_Date")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Modified By">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Modified_By")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Modified Date">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Modified_Date")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
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
                                <HeaderStyle CssClass="fieldNamewithbgcolor" HorizontalAlign="Left" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
