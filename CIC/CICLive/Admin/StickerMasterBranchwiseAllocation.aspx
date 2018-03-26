<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="StickerMasterBranchwiseAllocation.aspx.cs" Inherits="Admin_StickerMasterBranchwiseAllocation" %>
<%@ Register src="~/UC/AdminStickerDetails.ascx"tagname="StickerDetails" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
 <table width="100%">
                <tr>
                    <td class="headingred">
                       Branch Wise Allocation Sticker Master
                    </td>
                    <td></td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Label ID="lblErrorMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 100%;"  class="bgcolorcomm">
                                
                <tr>
                    <td colspan="2">
                         <uc1:StickerDetails ID="uclStickerDetails" runat="server" />
                    </td>
                </tr>
            </table>            
            <table>            
            </table>

</asp:Content>

