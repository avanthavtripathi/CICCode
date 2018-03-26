<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="SMSOutBoundAllocation.aspx.cs" Inherits="pages_SMSOutBoundAllocation" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
    <asp:UpdatePanel ID="pnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="1000" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="headingred">
                       SMS Allocation
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress AssociatedUpdatePanelID="pnl" ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                        </asp:UpdateProgress>
                </tr>
                <tr>
                    <td class="bgcolorcomm" colspan="2">
                        <table width="100%" border="0" cellpadding="1" cellspacing="0" style="margin-bottom: 27px;vertical-align:top">
                                <tr>
                                    <td>
                                        &nbsp;</tr>
                                <tr>
                                    <td>
                                      <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                               <tr>
                                                <td width="25%" style="padding-left:10px" valign="top">
                                                    Total Unallocated Complaint
                                                </td>
                                                <td colspan="2">
                                                    <asp:Label ID="LblComplaints" runat="server" />
                                                    <div style="text-align:center;vertical-align:bottom">
                                                     <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                                    </div>
                                                     
                                                    <div style="text-align:right;padding-right:50px">
                                                           <asp:Button ID="btnAllocate" runat="server" CssClass="btn" 
                                                            Text="Auto Equal Allocation" ValidationGroup="grp" 
                                                               onclick="btnAllocate_Click" />
                                              </div>
                                                </td>
                                     </table>
                                    </td>
                                </tr>
                             
                               
                             
                                <tr>
                                    <td>
                                     <asp:DataList RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                                HeaderStyle-CssClass="fieldNamewithbgcolor" GridLines="both" AutoGenerateColumns="False" ID="gvCommunication" runat="server" 
                                                Width="98%" HorizontalAlign="Left" RepeatColumns="3" RepeatDirection="Horizontal" >
                                                 <ItemTemplate>
                                                  <asp:CheckBox ID="chk" runat="server" />
                                                  <asp:Label ID="LblName" runat="server" ToolTip='<%# Eval("UserName") %>'  Text='<%# Eval("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                    </asp:DataList>
                                     
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td>
                                       <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                                <tr>
                                                    <td colspan="4">
                                                    </td>
                                                </tr>
                                                     <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td align="left" valign="top">
                                                 
                                                        <asp:GridView ID="GvAllocated" runat="server">
                                                        </asp:GridView>
                                                 
                                                    </td>
                                                    <td align="center">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                <td align="left" colspan="2" style="width: 171px" valign="top">&nbsp;</td>
                                                <td align="left" valign="top">
                                                    &nbsp;</td>
                                                <td align="center">
                                                
                                                    &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                      
                                                    </td>
                                                    <td>
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

