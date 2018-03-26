<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ServiceContractorTerritory.aspx.cs"
    Inherits="pages_ServiceContractorTerritory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Crompton Greaves :: Customer Interaction Center</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/global.css" rel="stylesheet" type="text/css" />
</head>
<body bgcolor="white">
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="updatepnl" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr bgcolor="white">
                        <td class="headingred">
                            Service Contractor Details
                        </td>
                        <td>
                            <a href="javascript:void(0)" class="links" onclick="window.close();">Close</a>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 10px" align="center" colspan="2" class="bgcolorcomm">
                            <table width="98%" border="0">
                                <tr>
                                    <td>
                                        <!-- Service Contractor Listing   -->
                                        <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                            HeaderStyle-CssClass="fieldNamewithbgcolor" GridLines="both" AllowPaging="True"
                                            AllowSorting="True" DataKeyNames="SC_SNo" AutoGenerateColumns="False" ID="gvServiceContractor"
                                            runat="server" OnPageIndexChanging="gvServiceContractor_PageIndexChanging" Width="98%"
                                            HorizontalAlign="Left">
                                            <Columns>
                                                <asp:BoundField DataField="Sno" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="SNo">
                                                    <HeaderStyle HorizontalAlign="Left" Width="30px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Unit_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Product Division">
                                                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="State_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="State">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="City_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="City">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Territory_Desc" HeaderText="Territory Desc">
                                                    <HeaderStyle Width="120px" HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SC_Code" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="SC Code">
                                                    <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SC_Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="SC Name">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="address" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Address">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                        <!-- End Service Contractor Listing -->
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
