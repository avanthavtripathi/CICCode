<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InternalBillDetails.aspx.cs"
    Inherits="SIMS_Pages_InternalBillDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/global.css" rel="stylesheet" type="text/css" />
</head>

<script language="javascript" type="text/javascript">
      function printdiv(printpage){var headstr = "<html><head><title></title></head><body>";
      var footstr = "</body>";var newstr = document.all.item(printpage).innerHTML;
      var oldstr = document.body.innerHTML;document.body.innerHTML = headstr+newstr+footstr;window.print(); document.body.innerHTML = oldstr;return false;
      }
//       function closePopup()
//        {        
//            window.opener.location.href = window.opener.location;
//            self.close();
//        }
</script>

<script type="text/javascript">
  
  
       
        function closePopup()
        {


               window.close();

        }
   

</script>

<body onunload="closePopup()">
    <form id="form1" runat="server">
    <div style="height: 20px">
    </div>
    <div align="right">
        <input name="b_print" type="button" class="btn" onclick="printdiv('div_print');"
            value="Print" style="width: 70px" />&nbsp;
        <asp:Button Text="Close" Width="70px" ID="imgBtnClose" CssClass="btn" runat="server"
            OnClick="imgBtnClose_Click" />
    </div>
    <div id="div_print">
        <table width="98%" border="0">
            <tr>
                <td colspan="2" style="height: 15px">
                    <b id="dvreprint" runat="server" style="font-size: large; padding-top: 10px; padding-left: 5px;
                        display: none;">REPRINT</b>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="headingred">
                    Internal Bill Generation Print
                </td>
            </tr>
            <tr>
                <td width="22%">
                    ASC:
                </td>
                <td>
                    <asp:Label ID="lblasc" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Division:
                </td>
                <td width="62%">
                    <asp:Label ID="lblDivision" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    ASC Address:
                </td>
                <td width="62%">
                    <asp:Label ID="lblascaddress" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Branch:
                </td>
                <td width="62%">
                    <asp:Label ID="lblbranch" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Branch Address:
                </td>
                <td width="62%">
                    <asp:Label ID="lblbranchaddress" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Internal Bill No.:
                </td>
                <td width="62%">
                    <asp:Label ID="lbltransactionno" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    Total Number of Records :
                    <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvChallanDetail" runat="server" AllowPaging="false" AllowSorting="false"
                        AlternatingRowStyle-CssClass="fieldName" AutoGenerateColumns="False" HeaderStyle-CssClass="fieldNamewithbgcolor"
                        HorizontalAlign="Left" RowStyle-CssClass="gridbgcolor" Width="100%">
                        <RowStyle CssClass="gridbgcolor" />
                        <Columns>
                            <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo" ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Complaint No" DataField="complaint_no" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Claim No" DataField="Claim_No" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Type (M/S)" DataField="Type" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Claim Approver Name" DataField="Claim_Approved_By" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Claim Approved Date" DataField="Approved_Date" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Service/Spare Used" DataField="Services_Spare" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Parameter-Possible Value" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblParameters" runat="server" Text='<%#Eval("Parameter_Possible_Value") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Quantity" DataField="Quantity" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Rate" DataField="Rate" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Amount" DataField="Amount" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Remarks" DataField="Remarks" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                        <AlternatingRowStyle CssClass="fieldName" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <b>Total Amount:&nbsp;</b><asp:Label ID="lbltotalamount" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
