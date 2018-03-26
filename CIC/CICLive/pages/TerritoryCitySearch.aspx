<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TerritoryCitySearch.aspx.cs" Inherits="pages_TerritoryCitySearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Territory City Search</title>
        <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/global.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div align="center">
        <asp:UpdatePanel ID="pnl" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <table width="100%">
                    <tr bgcolor="white">
                        <td class="headingred" align="left">
                            City Details
                        </td>
                        <td>
                            <a href="javascript:void(0);" class="links" onclick="window.close();">Close</a>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 10px" align="center" colspan="2" class="bgcolorcomm">
                            <div id="dv">
                                <table width="98%" border="0">
                                    <tr height="25">
                                        <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                                            <asp:UpdateProgress AssociatedUpdatePanelID="pnl" ID="UpdateProgress1" runat="server">
                                                <ProgressTemplate>
                                                    <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100%" align="left">
                                            <table width="98%" border="0" id="tableHeader" runat="server">
                                                
                                                <tr>
                                                    <td class="style1">
                                                        Territory<font color='red'>*</font>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTerritory" CssClass="txtboxtxt" ValidationGroup="Search" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                                            ID="RequiredFieldValidator1" ValidationGroup="Search" runat="server" ControlToValidate="txtTerritory" ErrorMessage="Territory is required."></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td height="25" align="left" class="style1">
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <!-- For button portion update -->
                                                        <table>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Button Text="Search" Width="70px" ID="imgBtnSearch" CssClass="btn" runat="server"
                                                                        CausesValidation="true" ValidationGroup="Search" OnClick="imgBtnSearch_Click" />
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="imgBtnCancel" Width="70px" runat="server" CssClass="btn" Text="Cancel"
                                                                        OnClick="imgBtnCancel_Click" />
                                                                    
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <!-- For button portion update end -->
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            
                                                <asp:GridView RowStyle-CssClass="gridbgcolor" PageSize="15" AlternatingRowStyle-CssClass="fieldName"
                                                    HeaderStyle-CssClass="fieldNamewithbgcolor" AllowPaging="True" AllowSorting="True"
                                                    DataKeyNames="City_Sno" AutoGenerateColumns="False" ID="gvComm" runat="server"
                                                    OnPageIndexChanging="gvComm_PageIndexChanging" Width="600" 
                                                    HorizontalAlign="Left">
                                                    <RowStyle CssClass="gridbgcolor" />
                                                    <Columns>
                                                        <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                            <HeaderStyle Width="40px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Territory_Desc"  ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Territory">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="City_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="City" HeaderStyle-Width="120px">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        
                                                        <asp:BoundField DataField="state_desc" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="State">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        
                                                          </Columns>
                                                    <EmptyDataTemplate>
                                                        <b>No records found</b></EmptyDataTemplate>
                                                    <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                    <AlternatingRowStyle CssClass="fieldName" />
                                                </asp:GridView>
                                                <!-- End Service Contractor Listing -->
                                            </div>
                                        </td>
                                    </tr>
                                    
                                    
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
