<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="ChangePassword.aspx.cs" Inherits="pages_ChangePassword" %>

<%@ Register src="../UC/ChangePwd.ascx" tagname="ChangePwd" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Change Password
                    </td>
                </tr>
                <tr>
                    <td>
                        <uc1:ChangePwd ID="ChangePwd1" runat="server" />
                    </td>
               </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
