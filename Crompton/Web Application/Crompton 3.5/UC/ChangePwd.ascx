<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChangePwd.ascx.cs" Inherits="UC_ChangePwd" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

 <asp:UpdatePanel ID="uppwd" runat="server" >
 <ContentTemplate>
    <table border="0" cellpadding="0" cellspacing="0" width="522px" >
                                                <tr>
                                                    <td>
                                                        <table id="tblchangepwd" runat="server" border="0" cellpadding="0" width="100%">
                                                            <tr>
                                                                <td align="left" colspan="2" style="padding:10px;" >
                                                                  <div style="text-align:center;font-size:14px;color:#0066cc">
                                                                  <asp:Label ID="LblMsg" runat="server" Text="Change Password." ></asp:Label>
                                                                  </div>
                                                                 
                                                                       
                                                                            <pre style="text-align:left;font-family:Calibri;font-size:12px" >How to Change Your Password 	 
  Step 1: Enter old password 	 
  Step 2: Enter new password 	 
  (8-16 chars using uppercase,lowercase,numeric and special characters)
  Step 3: Re-enter new password 	 
  Step 4: Click ChangePassword     
</pre>
<div align="right">
                                                                <asp:UpdateProgress runat="server" ID="Updp">
                                                                <ProgressTemplate>
                                                                <img src="../images/loading9.gif" alt="Please wait...."  />
                                                                </ProgressTemplate>
                                                                </asp:UpdateProgress>
                                                                  </div>
                                                              
                                                           </td>
                                                            </tr>
                                                   
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="TxtCurrentPassword">Old Password<font color="red">*</font></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="TxtCurrentPassword" runat="server" CssClass="txtboxtxt" ValidationGroup="cpLogin"
                                                                        TextMode="Password"></asp:TextBox>
                                                                        <div>
                                                                    <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" 
                                                                        ControlToValidate="TxtCurrentPassword" Display="Dynamic" ErrorMessage="Password is required." SetFocusOnError="true" 
                                                                        ToolTip="Password is required." ValidationGroup="cpLogin"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="TxtNewPassword">New Password<font color='red'>*</font></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="TxtNewPassword" runat="server" TextMode="Password" ValidationGroup="cpLogin"
                                                                        CssClass="txtboxtxt"></asp:TextBox>
                                                                    <div>
                                                                    <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="TxtNewPassword" Display="Dynamic" SetFocusOnError="true"  
                                                                        ErrorMessage="New Password is required." ToolTip="New Password is required."
                                                                        ValidationGroup="cpLogin"></asp:RequiredFieldValidator>
                                                                     <asp:RegularExpressionValidator ID="RevPwd" runat="server" ValidationExpression="^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W]).*$" 
                                                                     ControlToValidate="TxtNewPassword" Display="Dynamic" SetFocusOnError="true"  ErrorMessage="Passwod Policy not satisfied." ValidationGroup="cpLogin"  />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="TxtConfirmNewPassword">Confirm New Password<font color='red'>*</font></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="TxtConfirmNewPassword" runat="server" TextMode="Password" ValidationGroup="cpLogin" 
                                                                        CssClass="txtboxtxt"></asp:TextBox>
                                                                  <div>
                                                                    <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="TxtConfirmNewPassword" Display="Dynamic" 
                                                                        SetFocusOnError="true" ErrorMessage="Confirm New Password is required." ToolTip="Confirm New Password is required."
                                                                        ValidationGroup="cpLogin"></asp:RequiredFieldValidator>
                                                                    <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="TxtNewPassword" Display="Dynamic" 
                                                                       SetFocusOnError="true"  ControlToValidate="TxtConfirmNewPassword" ErrorMessage="The Confirm Password must match the New Password."
                                                                        ValidationGroup="cpLogin"></asp:CompareValidator>       
                                                                 </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" colspan="2" style="color: Red;">
                                                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" colspan="2">
                     
                                                                </td>
                                                            </tr>
                                                            <tr style="padding-bottom:10px">
                                                                <td align="left" colspan="2" style="padding-left: 80px;">
                                                                    <asp:Button CssClass="btn" ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword"
                                                                        Text="Change Password" ValidationGroup="cpLogin" 
                                                                        onclick="ChangePasswordPushButton_Click" />
                                                                    &nbsp;<asp:Button  CssClass="btn" ID="CancelPushButton" runat="server" CausesValidation="False"
                                                                        CommandName="Cancel" Text="Cancel"  />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <div id="dvsucessmsg" runat="server" class="copyright" style="padding:10px;display:none" >Your password has been changed successfully and sent to your Email id.<br /></div>
                                                    </td>
                                                </tr>
                                            </table>
                           
            
                             
 </ContentTemplate>
 </asp:UpdatePanel>
  