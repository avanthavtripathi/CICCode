<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerDetail.aspx.cs" Inherits="pages_UserDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Crompton Greaves :: Customer Interaction Center</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/global.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
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
                    Customer Details
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
                                Customer Name:
                            </td>
                            <td width="25%">
                                <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>
                            </td>
                            <td width="25%" style="padding-left: 35px">
                                Complaint Ref No:
                            </td>
                            <td width="25%">
                                <asp:Label ID="lblComRefNo" runat="server" Text=""></asp:Label>
                            </td>
                                    
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td align="left" style="padding-left: 35px">
                                Pri. Phone:
                            </td>
                            <td>
                                <asp:Label ID="lblPriPhone" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Alt. Phone:
                            </td>
                            <td>
                                <asp:Label ID="lblAltPhone" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                Email:
                            </td>
                            <td>
                                <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Extension:
                            </td>
                            <td>
                                <asp:Label ID="lblExt" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                Fax:
                            </td>
                            <td>
                                <asp:Label ID="lblFax" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Address1:
                            </td>
                            <td>
                                <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                Address2:
                            </td>
                            <td>
                                <asp:Label ID="lblAddress2" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Landmark:
                            </td>
                            <td>
                                <asp:Label ID="lblLandmark" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                Country:
                            </td>
                            <td>
                                <asp:Label ID="lblCountry" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                State:
                            </td>
                            <td>
                                <asp:Label ID="lblState" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                City:
                            </td>
                            <td>
                                <asp:Label ID="lblCity" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Pin Code:
                            </td>
                            <td>
                                <asp:Label ID="lblPinCode" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                Company Name:
                            </td>
                            <td>
                                <asp:Label ID="lblCompany" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
