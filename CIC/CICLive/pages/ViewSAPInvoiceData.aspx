<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewSAPInvoiceData.aspx.cs"
    Inherits="pages_ViewSAPInvoiceData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SAP Data Details</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/global.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
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
                    SAP Customer Details
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
                                Party Code:
                            </td>
                            <td width="25%">
                                <asp:Label ID="lblPartyCode" runat="server" Text=""></asp:Label>
                            </td>
                            <td width="25%" style="padding-left: 35px">
                                Party Type Code
                            </td>
                            <td width="25%">
                                <asp:Label ID="lblPartyTypeCode" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popGridbgcolor">
                            <td width="25%" style="padding-left: 35px">
                                Party SAP Code:
                            </td>
                            <td width="25%">
                                <asp:Label ID="lblPartySapCode" runat="server" Text=""></asp:Label>
                            </td>
                            <td width="25%" style="padding-left: 35px">
                                Party Name
                            </td>
                            <td width="25%">
                                <asp:Label ID="lblPartyName" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td align="left" style="padding-left: 35px">
                                Party Short Name:
                            </td>
                            <td>
                                <asp:Label ID="lblPartyShortName" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Party Address:
                            </td>
                            <td>
                                <asp:Label ID="lblPartyAddress" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                Party City:
                            </td>
                            <td>
                                <asp:Label ID="lblCity" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Party State:
                            </td>
                            <td>
                                <asp:Label ID="lblState" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                Party Country:
                            </td>
                            <td>
                                <asp:Label ID="lblCountry" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Party Pin Code:
                            </td>
                            <td>
                                <asp:Label ID="lblPinCode" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                Party Phone:
                            </td>
                            <td>
                                <asp:Label ID="lblPhone" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Party Fax:
                            </td>
                            <td>
                                <asp:Label ID="lblFax" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                Party Email:
                            </td>
                            <td>
                                <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                CP Name:
                            </td>
                            <td>
                                <asp:Label ID="lblCPName" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                CP Address:
                            </td>
                            <td>
                                <asp:Label ID="lblCPAddress" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                CP City:
                            </td>
                            <td>
                                <asp:Label ID="lblCPCity" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                CP State:
                            </td>
                            <td>
                                <asp:Label ID="lblCPState" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                CP Country:
                            </td>
                            <td>
                                <asp:Label ID="lblCPCountry" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                CP Pin Code:
                            </td>
                            <td>
                                <asp:Label ID="lblCPPin" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                CP Phone:
                            </td>
                            <td>
                                <asp:Label ID="lblCPPhone" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                CP Fax:
                            </td>
                            <td>
                                <asp:Label ID="lblCPFax" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                CP Mobile:
                            </td>
                            <td>
                                <asp:Label ID="lblCPMobile" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                CP Email:
                            </td>
                            <td>
                                <asp:Label ID="lblCPEmail" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <table width="100%">
            <tr bgcolor="white">
                <td class="headingred">
                    Invoice Details
                    <%--<asp:HiddenField ID="hdnBaseLineId" runat="server" />
                    <asp:HiddenField ID="hdnComNo" runat="server" />
                    <asp:HiddenField ID="hdnSplitNo" runat="server" />--%>
                </td>
                
            </tr>
            <tr>
                <td style="padding: 10px" align="center" colspan="2" class="bgcolorcomm">
                    <table width="100%" border="0" cellpadding="1" cellspacing="0">
                        <tr align="left" class="popAltGridbgcolor">
                            <td width="25%" style="padding-left: 35px">
                                Loc Code:
                            </td>
                            <td width="25%">
                                <asp:Label ID="lblLocCode" runat="server" Text=""></asp:Label>
                            </td>
                            <td width="25%" style="padding-left: 35px">
                            </td>
                            <td width="25%">
                                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td width="25%" style="padding-left: 35px">
                                PONO:
                            </td>
                            <td width="25%">
                                <asp:Label ID="lblPONO" runat="server" Text=""></asp:Label>
                            </td>
                            <td width="25%" style="padding-left: 35px">
                                PODate:
                            </td>
                            <td width="25%">
                                <asp:Label ID="lblPODate" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popGridbgcolor">
                            <td width="25%" style="padding-left: 35px">
                                DISPATCH DOC NO:
                            </td>
                            <td width="25%">
                                <asp:Label ID="lblDisDocNo" runat="server" Text=""></asp:Label>
                            </td>
                            <td width="25%" style="padding-left: 35px">
                                DISPATCH DOC DATE:
                            </td>
                            <td width="25%">
                                <asp:Label ID="lblDisDocDate" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor" id="trNoFrame" runat="server">
                            <td align="left" style="padding-left: 35px">
                                DISPATCH DATE:
                            </td>
                            <td>
                                <asp:Label ID="lblDisDate" runat="server" Text=""></asp:Label>
                            </td>
                            <td width="25%" style="padding-left: 35px">
                                Commission Date:
                            </td>
                            <td>
                                <asp:Label ID="lblCommDate" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Invoice Date:
                            </td>
                            <td>
                                <asp:Label ID="lblInvoiceDate" runat="server" Text=""></asp:Label>
                                <asp:HiddenField ID="hdnProductLine_Sno" runat="server" />
                                <asp:HiddenField ID="hdnComplaintRef_No" runat="server" />
                                <asp:HiddenField ID="hdnSplit" runat="server" />
                            </td>
                            <td style="padding-left: 35px">
                                Invoice Amount:
                            </td>
                            <td>
                                &nbsp;<asp:Label ID="lblInvoiceAmt" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Warrenty Peroid:
                            </td>
                            <td>
                                <asp:Label ID="lblWarrPeriod" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                Applicable Date:
                            </td>
                            <td>
                                <asp:Label ID="lblApplicableDate" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                WARRENTY DETAILS:
                            </td>
                            <td>
                                <asp:Label ID="lblWarrDetails" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                REMARKS:
                            </td>
                            <td>
                                <asp:Label ID="lblRemarks" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                CUSTOMFIELD:
                            </td>
                            <td>
                                <asp:Label ID="lblCustomField" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                STATUS:
                            </td>
                            <td>
                                <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                PREVIOUS FLAG:
                            </td>
                            <td>
                                <asp:Label ID="lblPreviousFlag" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                Service Invoice Number:
                            </td>
                            <td>
                                <asp:Label ID="lblServiceNumber" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                MATERIAL CODE:
                            </td>
                            <td>
                                <asp:Label ID="lblMaterialCode" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                MATERIAL DESC:
                            </td>
                            <td>
                                <asp:Label ID="lblMaterialDesc" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                PRODUCT SRNO:
                            </td>
                            <td>
                                <asp:Label ID="lblPrdSRNo" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                MACHINE SRNO:
                            </td>
                            <td>
                                <asp:Label ID="lblMachineSRNo" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                BATCH NO:
                            </td>
                            <td>
                                <asp:Label ID="lblBatchNo" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                TYPE OF EQUIPMENT:
                            </td>
                            <td>
                                <asp:Label ID="lblTypeofEquip" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
