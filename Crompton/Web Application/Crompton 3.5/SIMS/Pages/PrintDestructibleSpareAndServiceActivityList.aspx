<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintDestructibleSpareAndServiceActivityList.aspx.cs"
    Inherits="SIMS_Pages_PrintDestructibleSpareAndServiceActivityList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
        
                  
        window.opener.location.href ="DestructibleSpareAndServiceActivityList.aspx?ReturnId=True";

        window.close();

        }
   

</script>

<body>
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
                    <asp:Label ID="LblText" runat="server" style="margin-left:150px;font-size:16px;font-weight:bold"></asp:Label>
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
                    Transaction Number:
                </td>
                <td width="62%">
                    <asp:Label ID="lbltransactionno" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvChallanDetail" runat="server" AllowSorting="True"
                        AlternatingRowStyle-CssClass="fieldName" AutoGenerateColumns="False" HeaderStyle-CssClass="fieldNamewithbgcolor"
                        HorizontalAlign="Left" RowStyle-CssClass="gridbgcolor" Width="100%">
                        <RowStyle CssClass="gridbgcolor" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sno"  HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                               
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Complaint No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblcomplaint" runat="server" Text='<%#Eval("complaint_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Spare" HeaderText="Spare / Activity" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Parameter1" HeaderText="Param-1" ItemStyle-HorizontalAlign="Left" Visible="false">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Possiblevalue1" HeaderText="PV-1" ItemStyle-HorizontalAlign="Left" Visible="false">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Parameter2" HeaderText="Param-2" ItemStyle-HorizontalAlign="Left" Visible="false">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Possiblevalue2" HeaderText="PV-2" ItemStyle-HorizontalAlign="Left" Visible="false">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Parameter3" HeaderText="Param-3" ItemStyle-HorizontalAlign="Left" Visible="false">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Possiblevalue3" HeaderText="PV-3" ItemStyle-HorizontalAlign="Left" Visible="false">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Parameter4" HeaderText="Param-4" ItemStyle-HorizontalAlign="Left" Visible="false">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Possiblevalue4" HeaderText="PV-4" ItemStyle-HorizontalAlign="Left" Visible="false">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Qty" HeaderText="Qty" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Destroyed" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lbldestroyed" runat="server" Text='<%#Eval("Destroyed") %>' Width="10"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblamount" runat="server" Text='<%#Eval("amount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
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
