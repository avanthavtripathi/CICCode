<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="ResetPassword.aspx.cs" Inherits="Admin_ResetPassword"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">

<asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Reset Password
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" colspan="2">
                      
                        <table width="100%" border="0">
                          
                            <tr>
                                <td width="100%" align="left" class="bgcolorcomm">
                                    <table width="98%" border="0">
                                       
                                        
                                        <tr >
                                            <td style="width: 23%">
                                                User Id:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" Text="" ID="txtUsername" runat="server" Width="170px"
                                                    ValidationGroup="editt" MaxLength="10" />
                                                <asp:RequiredFieldValidator SetFocusOnError="true" ID="rqvUserName" runat="server"
                                                    ControlToValidate="txtUsername" ErrorMessage="User Id is required." Display="Dynamic"
                                                    ToolTip="User Id is required." ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 23%">
                                                Password:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPassword" runat="server" Text="" CssClass="txtboxtxt" Width="170px"
                                                    TextMode="Password"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" ID="PasswordRequired" runat="server"
                                                    ControlToValidate="txtPassword" ErrorMessage="Password is required." ToolTip="Password is required."
                                                    ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 23%">
                                                Confirm Password:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="txtboxtxt" Width="170px"
                                                    TextMode="Password"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" ID="ConfirmPasswordRequired" runat="server"
                                                    ControlToValidate="txtConfirmPassword" ErrorMessage="Confirm Password is required."
                                                    ToolTip="Confirm Password is required." ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="cvTxtConform" SetFocusOnError="true" CssClass="fieldName"
                                                    runat="server" ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword"
                                                    ErrorMessage="Password mismatch" ValidationGroup="editt">
                                                </asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="2" style="padding-left: 205px;">
                                                <asp:Button ID="imgSend" ValidationGroup="editt" runat="server" CausesValidation="True"
                                                    CssClass="btn"  Text="Reset" Width="70px" onclick="imgSend_Click" />
                                                &nbsp;&nbsp;
                                                <asp:Button ID="imgBtnCancel" runat="server" CausesValidation="false" CssClass="btn"
                                                     Text="Cancel" Width="70px" onclick="imgBtnCancel_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

