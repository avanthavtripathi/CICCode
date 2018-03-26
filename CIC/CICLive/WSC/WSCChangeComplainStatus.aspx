<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WSCChangeComplainStatus.aspx.cs"
    Inherits="WSC_WSCChangeComplainStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Details</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/global.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../scripts/Common.js" type="text/javascript">   
   
    </script>
<script type="text/javascript">
     function CloseAfterSave()
        {
//         alert("Save Successfully");
           window.opener.location.href ="WSCNewCustomer.aspx?ReturnId=True";
           window.close();

        }
</script>
    <script type="text/javascript">
    function ClosedWindows(id)
      {
       
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
                    <%--<a href="javascript:void(0)" class="MTOLink" onclick="window.close();">Close</a>--%>
                </td>
            </tr>
            <tr>
                <td style="padding: 10px" align="center" colspan="2" class="bgcolorcomm">
                    <table width="100%" border="0" cellpadding="1" cellspacing="0">
                        <tr id="trCGFeedbackCategory" runat="server" align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left" valign="top">
                                Select status:<font color='red'>*</font>
                            </td>
                            <td>
                                <asp:DropDownList Width="168px" ID="ddlEntityStauts" runat="server" CssClass="simpletxt1">
                                </asp:DropDownList>
                                <asp:CompareValidator SetFocusOnError="true" ID="CVState" runat="server" ControlToValidate="ddlEntityStauts"
                                    ErrorMessage="Status Type is required." Operator="NotEqual" ValueToCompare="0"
                                    Display="None"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr id="trCGExeComment" runat="server" align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left" valign="top">
                                Comments:<font color='red'>*</font>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCGExecutive" runat="server" CssClass="txtboxtxt" Width="300"
                                    TextMode="MultiLine" Height="50px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFCGExecutive" runat="server" SetFocusOnError="true"
                                    ControlToValidate="txtCGExecutive" ErrorMessage="Comments is required." Display="None"></asp:RequiredFieldValidator>
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
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="padding-left: 300px;" colspan="2">
                                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="padding-left: 300px;" colspan="2">
                                <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                                    runat="server" />
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
