<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DetailedRepeatedComplaints.aspx.cs"
    Inherits="Reports_DetailedRepeatedComplaints" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Repeated Complaints</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/global.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        function funUserDetail(custNo, compNo) {
            var strUrl = 'CustomerDetail.aspx?custNo=' + custNo + '&CompNo=' + compNo;
            window.open(strUrl, 'History', 'height=550,width=750,left=20,top=30,Location=0');
        }
    
    </script>

    <script language="javascript" type="text/javascript" src="../scripts/Common.js">
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td align="right">
                    <asp:Button ID="Button1" runat="server" Width="114px" Text="Save To Excel" CssClass="btn"
                        OnClick="btnExportExcel_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    Total Rows Count:
                    <asp:Label ID="lblRowsCount" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" PagerStyle-HorizontalAlign="Center"
                        AutoGenerateColumns="False" ID="gvComm" runat="server" Width="100%" HorizontalAlign="Left">
                        <RowStyle CssClass="gridbgcolor" />
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                HeaderText="Complaint No">
                                <ItemTemplate>
                                    <a href="Javascript:void(0);" onclick="funCommonPopUpReport(<%#Eval("BaseLineId")%>)">
                                    <%#Eval("Complaint_Split")%></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                HeaderText="Customer">
                                <ItemTemplate>
                                    <a href="Javascript:void(0);" onclick="funUserDetail('<%#Eval("CustomerId")%>','<%#Eval("Complaint_RefNo")%>')">
                                      <%#Eval("CustomerName")%>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="SC_Name" HeaderStyle-Width="120px" HeaderText="Service Contractor" />
                            <asp:BoundField DataField="ProductDivision_Desc" HeaderText="ProductDivision" />
                            <asp:BoundField DataField="ProductLine_Desc" HeaderText="ProductLine" />
                            <asp:BoundField DataField="Product_Desc" HeaderText="Product" HeaderStyle-Width="120px" />
                            <asp:BoundField DataField="sapproductcode" HeaderText="ProductSrNo" />
                            <asp:BoundField DataField="LoggedDate" HeaderText="Logged Date" />
                            <asp:BoundField DataField="WIPEndDate" HeaderText="Closure Date" />
                        </Columns>
                        <PagerStyle HorizontalAlign="Center" />
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
        </table>
    </div>
    </form>
</body>
</html>
