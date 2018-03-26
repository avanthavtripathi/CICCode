<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SpareActivityLog.aspx.cs"
    Inherits="SIMS_Pages_SpareActivityLog" %>

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
        <tr><td colspan="2">&nbsp;</td></tr>
            <tr bgcolor="white">
                <td class="headingred">
                    Show spare and activity consumption for a Complaint
                </td>
                <td>
                    <a href="javascript:void(0)" class="links" onclick="window.close();">Close</a>
                </td>
            </tr>
            <tr>
                <td style="padding: 10px" align="center" colspan="2" class="bgcolorcomm">
                    <table width="99%" border="0">
                        <tr>
                            <td align="left" width="15%">
                                Complaint No:
                            </td>
                            <td align="left">
                                <asp:Label ID="lblComplaintNo" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Complaint Date:
                            </td>
                            <td align="left">
                                <asp:Label ID="lblComplaintDate" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Product Division:
                            </td>
                            <td align="left">
                                <asp:Label ID="lblProductDivision" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="10%">
                                Product:
                            </td>
                            <td align="left">
                                <asp:Label ID="lblProduct" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Warranty Status:
                            </td>
                            <td align="left">
                                <asp:Label ID="lblWarrantyStatus" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" height="25px">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left">
                                <b>Material Consumption</b>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView PageSize="13" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                    HeaderStyle-CssClass="fieldNamewithbgcolor" GridLines="both" AllowPaging="True"
                                    AutoGenerateColumns="False" ID="gvSpareHistory" runat="server" OnPageIndexChanging="gvSpareHistory_PageIndexChanging"
                                    Width="99%" HorizontalAlign="Left" Style="margin-top: 0px">
                                    <RowStyle CssClass="gridbgcolor" />
                                    <Columns>
                                        <asp:BoundField DataField="Sno" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                            HeaderText="SNo">
                                            <HeaderStyle HorizontalAlign="Left" Width="20px" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SpareName" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                            HeaderText="Spare">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AlterSpareName" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                            HeaderText="Alternate Spare">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Consumed_Qty" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                            HeaderText="Quantity Consumed">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SAP_ListPrice" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left" HeaderText="Rate">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Discount" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left" HeaderText="Discount">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Spare_Amount" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left" HeaderText="Amount">
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
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left">
                                <div style="width:50%; float:left;"><b>Activity Charges</b></div>
                                <div style="width:50%; float:left;"><asp:Label ID="lblConyvanceType" runat="server" Text=""></asp:Label></div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView PageSize="13" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                    HeaderStyle-CssClass="fieldNamewithbgcolor" GridLines="both" AllowPaging="True"
                                    AutoGenerateColumns="False" ID="gvActivityHistory" runat="server" OnPageIndexChanging="gvActivityHistory_PageIndexChanging"
                                    Width="99%" HorizontalAlign="Left" Style="margin-top: 0px">
                                    <RowStyle CssClass="gridbgcolor" />
                                    <Columns>
                                        <asp:BoundField DataField="Sno" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                            HeaderText="SNo">
                                            <HeaderStyle HorizontalAlign="Left" Width="30px" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Activity" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                            HeaderText="Activity">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Parameter1_Description" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left" HeaderText="Param-1">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Possibl_Value1" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                            HeaderText="PV-1">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Parameter2_Description" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left" HeaderText="Param-2">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Possibl_Value2" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                            HeaderText="PV-2">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Parameter3_Description" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left" HeaderText="Param-3">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Possibl_Value3" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                            HeaderText="PV-3">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Parameter4_Description" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left" HeaderText="Param-4">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Possibl_Value4" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                            HeaderText="PV-4">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Activity_Rate" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left" HeaderText="Rate">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UOM" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                            HeaderText="UOM">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Activity_Qty" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                            HeaderText="Quantity">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Activity_Amount" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                            HeaderText="Amount">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Remarks" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                            HeaderText="Remarks">
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
