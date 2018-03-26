<%@ Page Language="C#" MasterPageFile="~/MasterPages/WSCCICPage.master" AutoEventWireup="true"
    CodeFile="WSCCheckCustomer.aspx.cs" Inherits="WSC_WSCCheckCustomer" Title="Check User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function funConfirm() {
            if (confirm('Are you sure to cancel this request?')) {
                return true;
            }
            return false;
        }


        function Close() {

            window.close();
        }          
            
    </script>

    <asp:UpdatePanel ID="pnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred" style="width: 832px">
                        Login User
                    </td>
                    <td id="tdRequestNo" runat="server">
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                        <b><font color="red">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label> </b></font>
                    </td>
                </tr>
                <tr id="trMain" runat="server">
                    <td colspan="2">
                        <table width="100%">
                            <tr>
                                <td>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td colspan="2" class="bgcolorcomm">
                                                <table width="90%" border="0" cellpadding="1" cellspacing="0" align="center">
                                                    <tr>
                                                        <td colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            E-Mail<font color='red'>*</font>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtEmail" MaxLength="99" runat="server" CssClass="txtboxtxt" Width="300px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RFEmail" SetFocusOnError="true" ControlToValidate="txtEmail"
                                                                runat="server" ErrorMessage="Email is required." Display="None"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RGEmail" runat="server" ErrorMessage="Valid Email is required."
                                                                ControlToValidate="txtEmail" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                Display="None"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td colspan="2">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Password<font color='red'>*</font>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPassword" TextMode="Password" MaxLength="15" runat="server" CssClass="txtboxtxt"
                                                                Width="300px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="ReqtxtPassword" SetFocusOnError="true" ControlToValidate="txtPassword"
                                                                runat="server" ErrorMessage="Password is required." Display="None"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td colspan="2">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <div id="divPassword" runat="server">
                                                        <tr>
                                                            <td>
                                                                Re-Type Password.<font color='red'>*</font>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="txtboxtxt"
                                                                    MaxLength="15" Width="300px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RegConfirmPassword" SetFocusOnError="true" ControlToValidate="txtConfirmPassword"
                                                                    runat="server" ErrorMessage="Confirm Password is required." Display="None"></asp:RequiredFieldValidator>
                                                                <asp:CompareValidator ID="ReqPasswordCompare" runat="server" SetFocusOnError="True"
                                                                    ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" Display="None"
                                                                    ErrorMessage="The Re-Type Password must match the Password entry."></asp:CompareValidator>
                                                            </td>
                                                        </tr>
                                                    </div>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnLogin" runat="server" CssClass="btn" Text="Login" Width="70px"
                                                                OnClick="btnLogin_Click" />
                                                            <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Cancel" Width="70px"
                                                                OnClientClick="Close();" />(Click cancel to go back)
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td align="right">
                                                            <div id="divForgate" runat="server">
                                                                <span class="MTOLink"><a href="../WSCPasswordRecovery.aspx" class="MTOLink">Forgot Password
                                                                    ?</a></span>&nbsp;&nbsp;&nbsp;
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
