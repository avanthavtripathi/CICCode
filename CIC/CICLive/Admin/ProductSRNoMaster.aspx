<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="ProductSRNoMaster.aspx.cs" Inherits="Admin_ProductSRNoMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Product Serial No. Master
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right:10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                  <tr>
                    <td colspan="2" align="right" style="padding-right:10px">
                    <fieldset>
                        <table style="width:95%;">
                        <tr><td colspan="4" align="right">
                        <asp:RadioButtonList ID="rdoboth" RepeatDirection="Horizontal" RepeatColumns="3"
                            runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdoboth_SelectedIndexChanged">
                            <asp:ListItem Value="1" Text="Active" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="0" Text="In-Active"></asp:ListItem>
                        </asp:RadioButtonList>
                        </td></tr>
                        <tr>
                        <td style="width:19%" align="right">
                        Product Division :
                        </td>
                        <td style="width:30%" align="left">
                        <asp:DropDownList ID="ddlProductDivision" Width="72%" runat="server" CssClass="simpletxt1 dlw">
                            <%--<asp:ListItem Text="All" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Fans" Value="13"></asp:ListItem>
                            <asp:ListItem Text="Lighting" Value="14"></asp:ListItem>
                            <asp:ListItem Text="LT Motors" Value="15"></asp:ListItem>
                            <asp:ListItem Text="Pumps" Value="16"></asp:ListItem>
                            <asp:ListItem Text="Appliances" Value="18"></asp:ListItem>
                            <asp:ListItem Text="FHP Motors" Value="19"></asp:ListItem>--%>
                        </asp:DropDownList>
                        </td>
                        <td style="width:19%"  align="right">
                        Vendor :
                        </td>
                        <td style="width:30%" align="left">
                        <asp:DropDownList ID="ddlVendorName" runat="server" CssClass="simpletxt1 dlw">
                            <asp:ListItem Text="All" Value="0" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                        </td>                        
                        </tr>
                        <tr>
                        <td style="width:19%"  align="right">
                        Location : 
                        </td>
                        <td style="width:30%" align="left">
                        <asp:TextBox ID="txtLocation" runat="server" Text="" CssClass="txtboxtxt"></asp:TextBox>
                        </td>
                        <td style="width:19%"  align="right">&nbsp;</td>
                        <td align="left">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn" 
                                onclick="btnSearch_Click" /> &nbsp;
                                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn" 
                                onclick="btnClear_Click" />
                        </td>
                        </tr>
                        </table>
                        <legend>Filter Area</legend>
                        </fieldset>
                        </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" colspan="2">
                        <table border="0" width="100%">
                            <tr>
                            
                                <td align="left" class="MsgTDCount">
                                    Total Number of Records : <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                </td>


                                <td align="right">
                                 <asp:Button ID="lnkDownload" Visible="false" runat="server" Text="DOWNLOAD EXCEL" OnClick="lnkDownload_Click" CssClass="btn"></asp:Button>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0">
                            <tr>
                                <td>
                                   <asp:GridView ID="gvComm" PageSize="10" 
                                   RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridLines="both" 
                                        AutoGenerateColumns="false" DataKeyNames="RecID" 
                                        Width="100%" runat="server" HorizontalAlign="Left"
                                        AllowPaging="true" AllowSorting="true" 
                                        onselectedindexchanging="gvComm_SelectedIndexChanging" 
                                        onpageindexchanging="gvComm_PageIndexChanging" onsorting="gvComm_Sorting" >
                                      <Columns>
                                           <asp:BoundField DataField="Vendor" SortExpression="Vendor" HeaderStyle-Width="25%" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Vendor">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="VendorCode" HeaderStyle-Width="5%" ItemStyle-Width="5%" SortExpression="VendorCode" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="VendorCode">
                                            </asp:BoundField>
                                             <asp:BoundField DataField="Location" SortExpression="Location" HeaderStyle-Width="8%" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Location">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LocationCode" SortExpression="LocationCode" HeaderStyle-Width="7%" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="LocationCode">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Unit_Desc" SortExpression="Unit_Desc" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="ProductDivision" HeaderStyle-Width="7%" ItemStyle-Width="7%">
                                            </asp:BoundField>
                                            <asp:TemplateField SortExpression="ProductLine_Desc" HeaderStyle-HorizontalAlign="Left" HeaderText="ProductLine" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                              <asp:HiddenField ID="hdnUnitSno" runat="server" />
                                              <asp:HiddenField ID="hdnPLineSno" runat="server" />
                                              <asp:Label ID="LblPline" runat="server" Text='<%# Eval("ProductLine_Desc")%>' />
                                            </ItemTemplate> 
                                            </asp:TemplateField>
                                                    <asp:BoundField DataField="Active_Flag" HeaderStyle-Width="7%" ItemStyle-Width="7%" SortExpression="Active_Flag" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Status">
                                            </asp:BoundField>
                                            <asp:CommandField ShowSelectButton="True" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                            </asp:CommandField>
                                        </Columns>
                                        
                                          <EmptyDataTemplate>
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                    <img src="<%=ConfigurationManager.AppSettings["UserMessage"]%>" alt="" />    <b>No Record found</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                    <!-- End Unit Listing -->
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                    
                                </td>
                            </tr>
                            <tr>
                                <td width="100%" align="left" class="bgcolorcomm">
                                    <table width="98%" border="0">
                                        <tr align="right">
                                            <td align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>" colspan="2">
                                                <font color='red'>*</font>
                                                <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:HiddenField ID="hdnMappingID" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" width="30%">
                                                Product Division<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlUnit" runat="server" CssClass="simpletxt1" 
                                                    Width="175px" AutoPostBack="True" 
                                                    onselectedindexchanged="ddlUnit_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvunit" runat="server" ControlToValidate="ddlUnit" 
                                                    ErrorMessage="ProductDivision is required." SetFocusOnError="true" ValidationGroup="editt"
                                                    InitialValue="0"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" align="right">
                                                Product Line<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlProductLine" runat="server" CssClass="simpletxt1" Width="175px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvPLine" runat="server" ControlToValidate="ddlProductLine"
                                                    ErrorMessage="ProductLine is required." SetFocusOnError="true" ValidationGroup="editt"
                                                    InitialValue="0"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" align="right">
                                                Vendor Name<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtVendorName" runat="server" Width="170px"  MaxLength="100" />
                                                <asp:RequiredFieldValidator ID="Rfvvname" runat="server" SetFocusOnError="true" ErrorMessage="Vendor Name is required." ControlToValidate="txtVendorName" 
                                                        ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" align="right">
                                                Vendor Code<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtVendorCode" runat="server" Width="170px" MaxLength="2" />
                                                <asp:RequiredFieldValidator ID="rfvvcode" runat="server" SetFocusOnError="true" ErrorMessage="Vendor Code is required." ControlToValidate="txtVendorCode" 
                                                        ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                          <tr>
                                            <td width="30%" align="right">
                                                Location Name<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtLocationName" runat="server" Width="170px"  MaxLength="100" />
                                                <asp:RequiredFieldValidator ID="rfvlname" runat="server" SetFocusOnError="true" ErrorMessage="Location Name is required." ControlToValidate="txtLocationName"  
                                                        ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" align="right" style="height: 20px">
                                                Location Code<font color='red'>*</font>
                                            </td>
                                            <td style="height: 20px">
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtLocationCode" runat="server" Width="170px" MaxLength="1" />
                                                <asp:RequiredFieldValidator ID="rfvlcode" runat="server" SetFocusOnError="true" ErrorMessage="Location Code is required." ControlToValidate="txtLocationCode" 
                                                        ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" align="right">
                                                Status
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rdoStatus" RepeatDirection="Horizontal" RepeatColumns="2"
                                                    runat="server">
                                                    <asp:ListItem Value="1" Text="Active" Selected="True"> </asp:ListItem>
                                                    <asp:ListItem Value="0" Text="In-Active"> </asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="25" align="left">&nbsp;
                                                
                                            </td>
                                            <td>
                                               <table>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Button Text="Add" Width="70px" CssClass="btn" ID="imgBtnAdd" runat="server"
                                                                CausesValidation="True" ValidationGroup="editt" OnClick="imgBtnAdd_Click" />
                                                            <asp:Button Text="Save" Width="70px" ID="imgBtnUpdate" CssClass="btn" runat="server"
                                                                CausesValidation="True" ValidationGroup="editt" Visible="false" OnClick="imgBtnUpdate_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="imgBtnCancel" Width="70px" runat="server" CausesValidation="false"
                                                                CssClass="btn" Text="Cancel" OnClick="imgBtnCancel_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
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
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkDownload" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
