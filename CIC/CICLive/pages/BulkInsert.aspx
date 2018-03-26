<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BulkInsert.aspx.cs" Inherits="pages_BulkInsert" MasterPageFile="~/MasterPages/CICPage.master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
        <table width="100%">
                                                <tr>
                                                      <td>
                                                        <table border="0" cellpadding="0" style="width: 100%">
                                                            <tr>
                                                                <td align="center" class="headingred">
                                                                    Bulk Insert</td>
                                                                <td align="left">
                                                                  </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblTable" runat="server">Select Table</asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:DropDownList ID="ddlTableNames" runat="server" AutoPostBack="True" 
                                                                        onselectedindexchanged="ddlTableNames_SelectedIndexChanged">
                                                                        <asp:ListItem Selected="True" Value="1">PARTYMASTER</asp:ListItem>
                                                                        <asp:ListItem Value="2">PARTYLOCRELATION</asp:ListItem>
                                                                        <asp:ListItem Value="3">MATERIALFGSALES</asp:ListItem>
                                                                        <asp:ListItem Value="4">MATERIALFGSALESDETAILS</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblBrowseFile" runat="server" >Browse File</font></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:FileUpload ID="uploadfile" runat="server"  CssClass="btn" />&nbsp;<asp:Button ID="btnUpload" runat="server" Text="Upload" onclick="Button1_Click" CssClass="btn" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    &nbsp;</td>
                                                                <td align="left">
                                                                    <asp:Button ID="btnInsert" runat="server" CssClass="btn" Enabled="false" 
                                                                        onclick="btnInsert_Click" Text="Insert Data" />
                                                                  </td>                                                                                   
                                                             
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    &nbsp;</td>
                                                                <td align="left">
                                                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="False"></asp:Label>
                                                                     
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    &nbsp;</td>
                                                                <td align="left">
                                                                    
                                                                     <b> <asp:Label ID="Label2"  runat="server" Text=" * Maximum 3MB file can be uploaded"></asp:Label></b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            <td></td>                                                            
                                                            <td>
                                                                  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                                                        <ProgressTemplate>
                                                                            <img src="../images/loading9.gif"alt="" />
                                                                        </ProgressTemplate>
                                                                    </asp:UpdateProgress>
                                                                  </td>      
                                                            </tr>  
                                                             <tr>
                                                            <td></td>                                                            
                                                            <td>
                                                                <asp:HyperLink ID="hlnkError" runat="server" Visible="False">Error Details</asp:HyperLink></td>
                                                            </tr>                                                          
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>       
        </ContentTemplate>
        <Triggers>       
        <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
        </asp:UpdatePanel>
        </asp:Content>