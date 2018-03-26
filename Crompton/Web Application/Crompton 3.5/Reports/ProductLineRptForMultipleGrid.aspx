<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductLineRptForMultipleGrid.aspx.cs"
    Inherits="Reports_ProductLineRptForMultipleGrid" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvExport" CssClass="simpletxt1" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                <HeaderStyle Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Month" DataField="Months" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Region" HeaderText="Region" HeaderStyle-HorizontalAlign="Left"/>
                            <asp:BoundField DataField="Branch" HeaderText="Branch" HeaderStyle-HorizontalAlign="Left"/>
                            <asp:BoundField HeaderText="Product Line" DataField="ProductLineDesc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
                            <asp:BoundField HeaderText="No of complaints" DataField="TotalComplaint" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
                            <asp:BoundField HeaderText="Complaints With Warranty" DataField="TotalComplaintWithWarranty" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
            <td colspan="2"></td>
            </tr>
            <tr>
            <td>
            <b>Summary</b>
            </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="GVSummaryExport" CssClass="simpletxt1" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField HeaderText="Product Line" DataField="ProductLineDesc" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left" />
                            <asp:BoundField HeaderText="No of complaints" DataField="NoOfComplaints" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left" />
                            <asp:BoundField HeaderText="Complaints With Warranty" DataField="ComplaintsWithWarranty"
                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
