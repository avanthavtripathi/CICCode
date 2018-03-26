<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintChallanScreen.aspx.cs"
    Inherits="SIMS_Pages_PrintChallanScreen" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Challan Print Screen</title>
</head>
<link href="../../css/style.css" rel="stylesheet" type="text/css" />
<link href="../../css/global.css" rel="stylesheet" type="text/css" />

<script language="javascript" type="text/javascript">
      function printdiv(printpage){var headstr = "<html><head><title></title></head><body>";
      var footstr = "</body>";var newstr = document.all.item(printpage).innerHTML;
      var oldstr = document.body.innerHTML;document.body.innerHTML = headstr+newstr+footstr;window.print(); document.body.innerHTML = oldstr;return false;
      }
        function refreshparent()
            { 
                   
                window.opener.window.location = "DefectiveSpareChallanGeneration.aspx?ReturnId=True";
                window.close();
            }
</script>

<style type="text/css">
    .style3
    {
        width: 334px;
    }
    .style4
    {
        width: 178px;
    }
</style>
<body onunload="refreshparent()">
    <form id="form1" runat="server">
    <div style="height: 20px">
    </div>
    <div align="right">
        <input name="b_print" runat="server" id="btnclose" type="button" class="btn" onclick="printdiv('div_print');"
            value="Print" style="width: 70px" />&nbsp;
        <asp:Button ID="Close" CssClass="btn" runat="server" Text="Close"
            Width="70px" onclick="Close_Click"  />
    </div>
    <div id="div_print">
        <table width="70%" align="center">
                 <tr>
                <td colspan="2" style="height: 15px">
                 <b id="dvreprint" runat="server" style="font-size:large;padding-top:10px;padding-left:5px;display:none;">REPRINT</b>  
                </td>
            </tr>
            <tr>
                <td colspan="2" class="headingred">
                    Defective Spare Challan Generation Print
                </td>
            </tr>
            <tr>
                <td class="style3" width="15%">
                    From ASC:
                </td>
                <td class="style4">
                    <asp:Label ID="lblascname" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style3" valign="top">
                    ASC Address:
                </td>
                <td class="style4">
                    <asp:Label ID="lblASCAdd" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    To CGL Branch Name:
                </td>
                <td class="style4">
                    <asp:Label ID="lblbranch" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    Division :
                </td>
                <td class="style4">
                    <asp:Label ID="lbldivision" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    Date Of Transaction :
                </td>
                <td class="style4">
                    <asp:Label ID="lbltransdate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    Challan No.
                </td>
                <td class="style4">
                    <asp:Label ID="lblchallanno" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowSorting="True"
                        AutoGenerateColumns="False" ID="grdchallan" runat="server" Width="100%" 
                        HorizontalAlign="Left">
                        <RowStyle CssClass="gridbgcolor" />
                        <Columns>
                            <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                <HeaderStyle Width="40px" />
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="complaint_no" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left" HeaderText="Complaint No">
                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="spare" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                HeaderText="Spare Desc">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Quantity" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                HeaderText="Quantity">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Claim_No" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                HeaderText="Claim No">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                             <asp:BoundField DataField="transportationdetail" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                HeaderText="Transportation Details">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                        <AlternatingRowStyle CssClass="fieldName" />
                    </asp:GridView>
                    <%-- <asp:GridView ID="grdchallan" runat="server" AlternatingRowStyle-CssClass="fieldName"
                        GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor" HorizontalAlign="Left"
                        RowStyle-CssClass="gridbgcolor" Width="100%">
                    </asp:GridView>--%>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
