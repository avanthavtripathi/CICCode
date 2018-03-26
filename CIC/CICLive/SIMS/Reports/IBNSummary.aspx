<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IBNSummary.aspx.cs" Inherits="SIMS_Reports_IBNSummary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Internal Bill Summary</title>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <asp:GridView AutoGenerateColumns="false" ID="gvExcel" runat="server" Width="100%"
                HorizontalAlign="Left">
                <Columns>
                    <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"
                        HeaderText="Internal Bill No">
                        <ItemTemplate>
                            <%#Eval("InternalBillNo")%>
                            </a>
                        </ItemTemplate>
                        <HeaderStyle Width="150px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="InternalBillDate" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left"
                        HeaderStyle-HorizontalAlign="Left" HeaderText="Internal Bill Date">
                    </asp:BoundField>
                    <asp:BoundField DataField="ActualAmmount" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left"
                        HeaderStyle-HorizontalAlign="Left" HeaderText="Amount">
                    </asp:BoundField>
                    <asp:BoundField DataField="ProductDivision_Desc" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left"
                        HeaderStyle-HorizontalAlign="Left" HeaderText="Division">
                    </asp:BoundField>
                    <asp:BoundField DataField="ASC_Name" HeaderStyle-Width="160px" ItemStyle-HorizontalAlign="Left"
                        HeaderStyle-HorizontalAlign="Left" HeaderText="Service Contactor">                      
                    </asp:BoundField>
                    <asp:BoundField DataField="ContractorBillNo" HeaderStyle-Width="160px" ItemStyle-HorizontalAlign="Left"
                        HeaderStyle-HorizontalAlign="Center" HeaderText="Contr. Bill No"></asp:BoundField>
                    <asp:BoundField DataField="GRNo" HeaderStyle-Width="160px" ItemStyle-HorizontalAlign="Left"
                        HeaderStyle-HorizontalAlign="Center" HeaderText="GR No"></asp:BoundField>
                </Columns>
            </asp:GridView>
        </tr>
    </table>
    </form>
</body>
</html>
