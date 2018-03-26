<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ComplaintDetailPopUp.aspx.cs"
    Inherits="Reports_ReqDetailPopUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Crompton Greaves :: Customer Interaction Center</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/global.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="updatepnl" runat="server">
            <ContentTemplate>
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="249" background="../images/headerBg.jpg">
                            <img src="../images/small-logo.jpg">
                        </td>
                        <td width="651" align="right" background="../images/headerBg.jpg">
                            <img src="../images/small-cic-txt.jpg" width="267" height="86">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 22px; background-color: #396870">
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%">
                    <tr bgcolor="white">
                        <td class="headingred">
                            Complaint Details
                        </td>
                        <td align="right">
                            <a href="javascript:void(0)" class="links" onclick="window.close();">Close</a>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 10px" align="center" colspan="2" class="bgcolorcomm">
                            <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                <tr align="left" class="popGridbgcolor">
                                    <td width="25%" style="padding-left: 35px">
                                        Complaint Ref No:
                                    </td>
                                    <td width="25%">
                                        <asp:Label ID="lblComRefNo" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td width="25%" style="padding-left: 35px">
                                        Product Division:
                                    </td>
                                    <td width="25%">
                                        <asp:Label ID="lblProductDivision" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr align="left" class="popAltGridbgcolor">
                                    <td align="left" style="padding-left: 35px">
                                        No Of Frame(s):
                                    </td>
                                    <td>
                                        <asp:Label ID="lblFrames" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr align="left" class="popGridbgcolor">
                                    <td style="padding-left: 35px" align="left">
                                        Product Line:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblProductLine" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td style="padding-left: 35px">
                                        Mode Of Receipt:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblModeReceipt" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr align="left" class="popAltGridbgcolor">
                                    <td style="padding-left: 35px" align="left">
                                        Language:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblLanguage" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td style="padding-left: 35px">
                                        Quantity:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblquantity" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr align="left" class="popGridbgcolor">
                                    <td style="padding-left: 35px" align="left">
                                        Nature Of Complaint:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNatureOfComp" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td style="padding-left: 35px">
                                        Invoice Date:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblInvoiceDate" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr align="left" class="popAltGridbgcolor">
                                    <td style="padding-left: 35px" align="left">
                                        Purchase To Date:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPurhToDate" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td style="padding-left: 35px">
                                        Purchase From Date:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPurhFrmDate" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr align="left" class="popGridbgcolor">
                                    <td style="padding-left: 35px" align="left">
                                        BHQ:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblBhq" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td style="padding-left: 35px">
                                        Warranty Status:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblWStatus" runat="server" Text=""></asp:Label>
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
