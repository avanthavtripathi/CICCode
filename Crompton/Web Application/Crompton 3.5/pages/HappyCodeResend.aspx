<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="HappyCodeResend.aspx.cs" Inherits="pages_HappyCodeResend" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">

  <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                       Happy Code Resend
                    </td>
                </tr>
                <tr>
                <td align="right">
                    <asp:Label ID="lblComplaintNo" runat="server">Complaint No<font color="red">*</font></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtComplaintNo" runat="server" CssClass="txtboxtxt" ValidationGroup="cpLogin"></asp:TextBox>
                        <div>
                    <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" 
                        ControlToValidate="txtComplaintNo" Display="Dynamic" ErrorMessage="Complaint No is required." SetFocusOnError="true" 
                        ToolTip="Complaint No is required."></asp:RequiredFieldValidator>
                        </div>
              
            
                </td>
            </tr>
            
<tr style="padding-bottom:10px">
                                            <td align="center" colspan="2" style="padding-left: 80px;">
                                                <asp:Button CssClass="btn" ID="btnComplaintNo" runat="server" 
                                                    Text="Resend" onclick="btnComplaintNo_Click"  />
                                           
                                           
                                           
                                                &nbsp;<asp:Button  CssClass="btn" ID="CancelPushButton" runat="server" CausesValidation="False"
                                                    CommandName="Cancel" Text="Cancel"  />
                                            </td>
                                        </tr>
            </table>
              <asp:Label ID="lblmsg" runat="server" style="color:Green;"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

