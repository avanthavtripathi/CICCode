<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ComplainRefNo.aspx.cs" Inherits="pages_ComplainRefNo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Crompton Greaves :: Customer Interaction Center</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/global.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 30%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="updatepnl" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr bgcolor="white">
                        <td class="headingred">
                            Complaint Details
                        </td>
                        <td>
                            <a href="javascript:void(0)" class="links" onclick="window.close();">Close</a>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 10px" align="center" colspan="2" class="bgcolorcomm">
                            <table width="100%" border="0" cellpadding="1" cellspacing="0" align="left">
                                <tr>
                                    <td style="padding-left: 60px" align="right" class="style1">
                                        Complaint Ref No:
                                    </td>
                                    <td width="25%" align="left">
                                         <asp:Label ID="lblComRefNo" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td width="20%" align="right">
                                        Product Division:
                                    </td>
                                    <td width="30%" align="left">
                                       <asp:Label ID="lblProductDivision" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 60px" align="right" class="style1">
                                        Product Line:
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblProductLine" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td align="right">
                                        Mode Of Receipt:
                                    </td>
                                    <td valign="middle" align="left">
                                        <asp:Label ID="lblModeReceipt" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 60px" align="right" class="style1">
                                        Language:
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblLanguage" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td align="right">
                                        Quantity:
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblquantity" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 60px" align="right" class="style1">
                                        Nature Of Complaint:
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblNatureOfComp" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td align="right">
                                        Invoice Date:
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblInvoiceDate" runat="server" Text=""></asp:Label>
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
