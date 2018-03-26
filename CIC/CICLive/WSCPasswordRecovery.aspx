<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WSCPasswordRecovery.aspx.cs"
    Inherits="WSC_WSCPasswordRecovery" Title="Crompton Greaves :: Customer Interaction Center" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Welcome to Crompton Greaves - Call Center</title>
    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon">
    <meta name="GENERATOR" content="">
    <meta name="keyword" content="">
    <link rel="stylesheet" type="text/css" href="css/style.css">
    <link rel="stylesheet" type="text/css" href="css/NewStyles.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="600" runat="server"
            EnablePageMethods="true" />
        <table width="1000" border="0" align="center" cellpadding="0" cellspacing="0" style="border-left: 1px Solid #080000;
            border-right: 1px Solid #080000">
            <tr>
                <td width="249" background="images/headerBg.jpg">
                    <img src="images/CGLogo.jpg" alt="" />
                </td>
                <td width="751" background="images/headerBg.jpg" align="right">
                </td>
            </tr>
        </table>
        <table width="1000" border="0" align="center" cellpadding="0" cellspacing="0" style="border-left: 1px Solid #080000;
            border-right: 1px Solid #080000">
            <tr>
                <td bgcolor="#FFFFFF" valign="top" style="padding: 20px">
                    <asp:PasswordRecovery ID="PasswordRecovery2" runat="server" Width="100%" GeneralFailureText="Email ID does not exist. Please register first."
                        UserNameFailureText="Email ID does not exist. Please register first.">
                        <MailDefinition BodyFileName="ForgotPassword.txt" From="cgcare@cgglobal.com" Subject="Login Details">
                        </MailDefinition> <%--bhawesh comment cgcare@cgl.co.in--%>
                        <UserNameTemplate>
                            <table width="778" border="0" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" valign="middle" align="center">
                                    </td>
                                    <td style="width: 90%" align="right" valign="middle">
                                        <br />
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td align="right" height="20">
                                                </td>
                                                <td align="left">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="height: 24px">
                                                    <span class="sign_in_text">EMAIL ID&nbsp;</span>
                                                </td>
                                                <td style="height: 24px" align="left">
                                                    <asp:TextBox ID="UserName" runat="server" class="loginTxtbox" ValidationGroup="Log"
                                                        MaxLength="99" Style="width: 210px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqTxtUser" runat="server" ValidationGroup="Log"
                                                        ControlToValidate="UserName" Display="Static">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 51%; height: 5px">
                                                </td>
                                                <td style="width: 49%; height: 5px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 51%">
                                                </td>
                                                <td style="width: 49%">
                                                    <div align="left">
                                                        <asp:ImageButton ID="LoginButton" runat="server" CommandName="Submit" ImageUrl="images/New-btn-send.jpg"
                                                            ValidationGroup="Log" src="images/New-btn-send.jpg" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td align="right" style="padding-right: 10px">
                                                    <font class="text"><b>Enter your EMAIL ID to receive password.</b><br>
                                                        <a class="links" href="WSC/WSCCheckCustomer.aspx?id=2">Click here</a> to login </font>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="right">
                                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                                    &nbsp;&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </UserNameTemplate>
                        <SuccessTemplate>
                            <br />
                            <br />
                            <font>Your password has been sent successfully to your Email id.<br />
                                <a href="WSC/WSCCheckCustomer.aspx">Click here</a> to go login screen.</font>
                        </SuccessTemplate>
                    </asp:PasswordRecovery>

                    <script language="javascript" type="text/javascript">
            if(document.getElementById("PasswordRecovery2_UserNameContainerID_UserName"))
            {
            document.getElementById("PasswordRecovery2_UserNameContainerID_UserName").focus();
            }
          
   
                    </script>

                </td>
            </tr>
            <tr>
                <td height="25px" align="center" bgcolor="#ffffff" class="footer">
                    Powered by Avantha Technologies Limited &copy; 2008
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
