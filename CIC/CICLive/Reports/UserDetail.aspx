<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserDetail.aspx.cs" Inherits="pages_UserDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Crompton Greaves :: Customer Interaction Center</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/global.css" rel="stylesheet" type="text/css" /></head>
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
                            Customer Details
                        </td>
                        <td>
                            <a href="javascript:void(0)" class="links" onclick="window.close();">Close</a>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 10px" align="center" colspan="2" class="bgcolorcomm">
                            <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                <tr>
                                    <td width="25%" style="padding-left: 60px" align="right">
                                        Customer Name:
                                    </td>
                                    <td width="25%">
                                        <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td width="20%" align="right">
                                        Complaint Ref No:
                                    </td>
                                    <td width="30%">
                                        <asp:Label ID="lblComRefNo" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="25%" style="padding-left: 60px" align="right">
                                        Pri. Phone:
                                    </td>
                                    <td width="25%">
                                        <asp:Label ID="lblPriPhone" runat="server" Text=""></asp:Label>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td style="padding-left: 60px" align="right">
                                        Alt. Phone:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblAltPhone" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td align="right">
                                        Email:
                                    </td>
                                    <td valign="middle">
                                        <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 60px" align="right">
                                        Extension:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblExt" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td align="right">
                                        Fax:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblFax" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 60px" align="right">
                                        Address1:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td align="right">
                                        Address2:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblAddress2" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td style="padding-left: 60px" align="right">
                                        Landmark:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblLandmark" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td align="right">
                                        Country:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCountry" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 60px" align="right">
                                        State:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblState" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td align="right">
                                        City:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCity" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 60px" align="right">
                                        Pin Code:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPinCode" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td align="right">
                                        Compnay Name:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCompany" runat="server" Text=""></asp:Label>
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
