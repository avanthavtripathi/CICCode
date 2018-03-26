<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WSCPopUp.aspx.cs" Inherits="pages_WSCPopUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Details</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/global.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../scripts/Common.js" type="text/javascript">   
   
    </script>

    <script type="text/javascript">
    function ClosedWindows(id)
      {
       
            var strId=id;
            if(strId=="1")
            {
               window.close(); 
            }
            else
            {
               var strWebRefNo=document.getElementById('<% =hdnfWSCCustomerId.ClientID %>').value;
               window.opener.location.href='../pages/MTOServiceRegistration.aspx?WSCCustomerId='+strWebRefNo;
                window.close();
               
            }
     }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="96%" border="0" align="center" cellpadding="0" cellspacing="0" style="border-left: 1px Solid #080000;
            border-right: 1px Solid #080000">
            <tr>
                <td width="249" height="" background="../images/headerBg.jpg">
                    <img src="../images/CGlogo.jpg">
                </td>
                <td width="651" align="right" background="../images/headerBg.jpg">
                </td>
            </tr>
        </table>
        <table align="center" width="96%" style="border-bottom: 1px Solid #080000; border-left: 1px Solid #080000;
            border-right: 1px Solid #080000">
            <tr>
                <td align="right" colspan="2" height="15px">
                </td>
            </tr>
            <tr bgcolor="white">
                <td align="right" colspan="2" style="padding-right: 10px">
                    <a href="javascript:void(0)" class="MTOLink" onclick="window.close();">Close</a>
                </td>
            </tr>
            <tr>
                <td style="padding: 10px" align="center" colspan="2" class="bgcolorcomm">
                    <table width="100%" border="0" cellpadding="1" cellspacing="0">
                        <tr>
                            <td colspan="2" align="left" class="popAltGridbgcolor" style="font-weight: bold;">
                                Customer Details Information
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" height="20px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor" style="font-weight: bold;">
                            <td width="25%" style="padding-left: 35px">
                                Web service number:
                            </td>
                            <td width="25%">
                                <asp:Label ID="lblWebservicenumber" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                         <tr id="trComplaintRefNo" runat="server" visible="false" align="left" class="popAltGridbgcolor" style="font-weight: bold;">
                            <td width="25%" style="padding-left: 35px">
                                Complaint Ref. No:
                            </td>
                            <td width="25%">
                                <asp:Label ID="lblComplaintRefNo" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popGridbgcolor">
                            <td width="25%" style="padding-left: 35px">
                                Company’s Name:
                            </td>
                            <td width="25%">
                                <asp:Label ID="lblCompanyName" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td align="left" style="padding-left: 35px">
                                Contact Person name:
                            </td>
                            <td>
                                <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Email:
                            </td>
                            <td>
                                <asp:Label ID="lblEmail" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Address:
                            </td>
                            <td>
                                <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Telephone/Mobile number:
                            </td>
                            <td>
                                <asp:Label ID="lblContactNo" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Country:
                            </td>
                            <td>
                                <asp:Label ID="lblCountry" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                State:
                            </td>
                            <td>
                                <asp:Label ID="lblState" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
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
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Type of Feedback:
                            </td>
                            <td>
                                <asp:Label ID="lblFeedbackType" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr id="tdDivision" runat="server" align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Division:
                            </td>
                            <td>
                                <asp:Label ID="lblDivision" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr id="tdProductLine" runat="server" align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Product :
                            </td>
                            <td>
                                <asp:Label ID="lblPDline" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr id="tdRating" runat="server" align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Rating/Voltage Class:
                            </td>
                            <td>
                                <asp:Label ID="lblRating" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr id="tdMGFSeerialNo" runat="server" align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Manufacturer Serial Number:
                            </td>
                            <td>
                                <asp:Label ID="lblMgfSerialNo" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr id="tdYearMGF" runat="server" align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Year of Manufacture:
                            </td>
                            <td>
                                <asp:Label ID="lblYearMgf" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr id="tdSiteLocation" runat="server" align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Site Location / Site Address:
                            </td>
                            <td>
                                <asp:Label ID="lblLocation" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr id="tdNatureofComplaint" runat="server" align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Nature of Complaint / Description:
                            </td>
                            <td>
                                <asp:Label ID="lblNature" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr id="trCGFeedbackCategory" runat="server" align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left" valign="top">
                                CG executive category:<font color='red'>*</font>
                            </td>
                            <td>
                                <asp:DropDownList Width="168px" ID="ddlGCExecutive" runat="server" CssClass="simpletxt1">
                                </asp:DropDownList>
                                <asp:CompareValidator SetFocusOnError="true" ID="CVState" runat="server" ControlToValidate="ddlGCExecutive"
                                    ErrorMessage="Feedback Type is required." Operator="NotEqual" ValueToCompare="0"
                                    Display="None"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr id="trCGExeComment" runat="server" align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left" valign="top">
                                CG Executive comments:<font color='red'>*</font>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCGExecutive" runat="server" CssClass="txtboxtxt" Width="300"
                                    TextMode="MultiLine" Height="50px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFCGExecutive" runat="server" SetFocusOnError="true"
                                    ControlToValidate="txtCGExecutive" ErrorMessage="CG Executive comments is required."
                                    Display="None"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor" id="trCGViewFeedback" runat="server">
                            <td style="padding-left: 35px" align="left">
                                CG executive category:
                            </td>
                            <td id="tdCGFeedbackDesc" runat="server">
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor" id="trViewComentCG" runat="server">
                            <td valign="top" style="padding-left: 35px" align="left">
                                CG Executive comments:
                            </td>
                            <td id="tdCGComment" runat="server">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="padding-right: 195px" colspan="2">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn" OnClick="btnSubmit_Click"
                                    Text="Save" Width="70px" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" CausesValidation="false" CssClass="btn"
                                    OnClick="btnCancel_Click" Width="70px" Text="Cancel" />
                                &nbsp;
                                <asp:HiddenField ID="hdnWSCCustomerId" runat="server" />
                                <asp:HiddenField ID="hndIsConvertMTO" runat="server" />
                                <asp:HiddenField ID="hndFeedBackTypeID" runat="server" />  <!-- Bhawesh Add : 29-7-13 -->
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="padding-left: 300px;" colspan="2">
                                <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                                    runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trConvertToMTO" runat="server">
                            <td colspan="2" align="center">
                                <asp:HiddenField ID="hdnfWSCCustomerId" runat="server" />
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="MTOLink" OnClientClick="ClosedWindows(2)"
                                    Text="Convert to MTO"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
        </table>
        <br />
    </form>
</body>
</html>
