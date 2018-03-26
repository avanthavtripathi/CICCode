<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="pages_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
<div style="color:Red;font-size:12px;text-align:center;display:none">Site will be down for maintenance from 7:20 PM.</div>
<asp:UpdatePanel ID="Updb" runat="server" UpdateMode="Conditional" >
<ContentTemplate>
<table width="100%" border="0" align="center" cellpadding="3" cellspacing="3" style="height:350px">
<tr style="height:50px" >
    <td align="right" style="vertical-align:top;width:700px;padding-right:150px;">
     <asp:Button ID="BtnSc" runat="server" Visible="true" Text="SC-Wise" onclick="BtnSc_Click" CssClass="btn" Width="150px" Height="30px" />
     <asp:Button ID="BtnBranch" runat="server" Visible="true" Text="Branch-Wise" onclick="BtnBranch_Click" CssClass="btn" Width="150px" Height="30px" />
    </td>
    <td align="left" valign="top" width="160px">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server"  >
        <ProgressTemplate>
        <img src="../images/loading9.gif" alt="Loading..." />
        </ProgressTemplate>
        </asp:UpdateProgress>
    </td>
  </tr>
  <tr >
  <td colspan="2" valign="top" align="center">
       <asp:PlaceHolder ID="PhdashBoard" runat="server" >
       </asp:PlaceHolder>
  </td>
  </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>