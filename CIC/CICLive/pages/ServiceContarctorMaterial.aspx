<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="ServiceContarctorMaterial.aspx.cs" Inherits="pages_ServiceContarctorMaterial"
    %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <table width="100%">
            <tr>
                <td style="width:30%"> 
                    Spare Required for Site Complaint:
                </td>
                <td>
                    <asp:TextBox ID="txtSpareSite" runat="server" 
                    TextMode="MultiLine" CssClass="txtboxtxt" Width="100%" Height="40px">
                    </asp:TextBox>
                </td>
            </tr>
             <tr>
                <td style="width:30%"> 
                    Spare Required for SRF Complaint:
                </td>
                <td>
                    <asp:TextBox ID="txtSpareSRF" runat="server" 
                    TextMode="MultiLine" CssClass="txtboxtxt" Width="100%" Height="40px">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnSaveSpare" 
                    runat="server" CssClass="btn" Width="100px" Text="Save" 
                        onclick="btnSaveSpare_Click" />
                </td>
            </tr>
             <tr>
                                <td align="center" colspan="2">
                                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </td>
                            </tr>
        </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
