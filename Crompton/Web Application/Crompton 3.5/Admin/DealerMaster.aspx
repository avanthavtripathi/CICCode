<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="DealerMaster.aspx.cs" Inherits="Admin_DealerMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Dealer Master
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right:10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" colspan="2">
                        <table border="0" width="100%">

                            <tr>
                                <td align="right">Search For <asp:DropDownList ID="ddlSearch" runat="server" Width="130px" CssClass="simpletxt1">
                                <asp:ListItem Text="Code" Value="Dealer_Code"></asp:ListItem>
                                <asp:ListItem Text="Description" Value="Dealer_Name"></asp:ListItem>
                                <asp:ListItem Text="Dealer Email" Value="Email"></asp:ListItem>
                                <%--<asp:ListItem Text="Status" Value="Active_Flag"></asp:ListItem>--%>
                               </asp:DropDownList>
                                
                                With <asp:TextBox ID="txtSearch" runat="server" CssClass="txtboxtxt" Width="100px"></asp:TextBox>
                                
                                  <asp:Button Text="Go" Width="25px" CssClass="btn" ID="imgBtnGo" runat="server"
                                    CausesValidation="False" ValidationGroup="editt" OnClick="imgBtnGo_Click"  />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0">
                            <tr>
                                <td>
                                    <!-- Dealer Listing -->
                                    <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="True"
                                        AllowSorting="True" DataKeyNames="Dealer_SNo" AutoGenerateColumns="False" ID="gvComm"
                                        runat="server" OnPageIndexChanging="gvComm_PageIndexChanging" Width="100%" OnSelectedIndexChanging="gvComm_SelectedIndexChanging"
                                        HorizontalAlign="Left" OnRowDataBound="gvComm_RowDataBound" Visible="true">
                                        <Columns>
                                            <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                <HeaderStyle Width="40px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Dealer_Code" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Code">
                                                <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Dealer_Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Description">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="Email" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Dealer Email">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Active_Flag" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Status">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:CommandField ShowSelectButton="True" HeaderStyle-Width="60px" HeaderText="Edit">
                                                <HeaderStyle Width="60px" />
                                            </asp:CommandField>
                                        </Columns>
                                    </asp:GridView>
                                    <!-- End Dealer Listing -->
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                    
                                </td>
                            </tr>
                            <tr>
                                <td width="100%" align="left" class="bgcolorcomm">
                                    <table width="98%" border="0">
                                        <tr>
                                            <td colspan="2" align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                                <font color='red'>*</font>
                                                <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:HiddenField ID="hdnDealerSNo" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Dealer&nbsp; Code<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtDealerCode" runat="server" Width="170px"
                                                    Text="" /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                        SetFocusOnError="true" ErrorMessage="Dealer Code is required." ControlToValidate="txtDealerCode"
                                                        ValidationGroup="editt" ToolTip="Dealer Code is required."></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Dealer&nbsp; Name<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtDealerDesc" runat="server" Width="170px"
                                                    Text="" /><asp:RequiredFieldValidator ID="reqvalDeptIname" runat="server" SetFocusOnError="true"
                                                        ErrorMessage="Dealer Description is required." ControlToValidate="txtDealerDesc"
                                                        ValidationGroup="editt" ToolTip="Dealer Description is required."></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td width="30%">
                                                Dealer&nbsp; Email<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtEmail" runat="server" Width="170px"
                                                    Text="" /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true"
                                                        ErrorMessage="Dealer Email is required." ControlToValidate="txtEmail"
                                                        ValidationGroup="editt" ToolTip="Dealer Email is required."></asp:RequiredFieldValidator>
                                                         <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Valid Email is required."
                                                                ControlToValidate="txtEmail" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                Display="None"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td width="30%">
                                                Address 1</font><font color="red">*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtAddress" runat="server" Width="170px" Text=""
                                                    MaxLength="100" TextMode="MultiLine" />
                                                <asp:RequiredFieldValidator ID="RFVAddOne" runat="server" SetFocusOnError="true"
                                                    Display="Dynamic" ErrorMessage="Address is required." ControlToValidate="txtAddress"
                                                    ValidationGroup="editt" ToolTip="Address is required."></asp:RequiredFieldValidator>
                                              
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                </font> Country Code</td>
                                            <td>
                                              <asp:DropDownList ID="ddlCountryCode" runat ="server" CssClass="simpletxt1" 
                                                    
                                                    ></asp:DropDownList>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td width="30%">
                                                </font> Region</td>
                                            <td>
                                              <asp:DropDownList ID="ddlRegion" runat ="server" CssClass="simpletxt1" 
                                                    AutoPostBack="True" onselectedindexchanged="ddlRegion_SelectedIndexChanged"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        
                                           <tr>
                                            <td width="30%">
                                                </font> State Code</td>
                                            <td>
                                              <asp:DropDownList ID="ddlStateCode" runat ="server" CssClass="simpletxt1" 
                                                    AutoPostBack="True" onselectedindexchanged="ddlStateCode_SelectedIndexChanged"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        
                                         <tr>
                                            <td width="30%">
                                                </font> Branch</td>
                                            <td>
                                              <asp:DropDownList ID="ddlBranch" runat ="server" CssClass="simpletxt1"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        
                                      
                                        
                                         
                                        
                                         <tr>
                                            <td width="30%">
                                                </font> City Code</td>
                                            <td>
                                              <asp:DropDownList ID="ddlCityCode" runat ="server" CssClass="simpletxt1"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Status
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rdoStatus" runat="server" RepeatColumns="2" 
                                                    RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="True" Text="Active" Value="1">
                                                    </asp:ListItem>
                                                    <asp:ListItem Text="In-Active" Value="0">
                                                    </asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        <tr>
                                            <td align="left" height="25">
                                                &nbsp;
                                            </td>
                                            <td>
                                                <!-- For button portion update -->
                                                <table>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Button ID="imgBtnAdd" runat="server" CausesValidation="True" 
                                                                CssClass="btn" OnClick="imgBtnAdd_Click" Text="Add" ValidationGroup="editt" 
                                                                Width="70px" />
                                                            <asp:Button ID="imgBtnUpdate" runat="server" CausesValidation="True" 
                                                                CssClass="btn" OnClick="imgBtnUpdate_Click" Text="Save" ValidationGroup="editt" 
                                                                Width="70px" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="imgBtnCancel" runat="server" CausesValidation="false" 
                                                                CssClass="btn" OnClick="imgBtnCancel_Click" Text="Cancel" Width="70px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <!-- For button portion update end -->
                                            </td>
                                        </tr>
                                        
                                         <tr>
                                            <td height="25" align="left">
                                                &nbsp;
                                            </td>
                                            <td>                               
                                               
                                            </td>
                                            <td>                               
                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="../BulkUpload/bulkUploadDealerMaster.xls">Bulk Upload for Dealer Master</asp:HyperLink>
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
    </asp:UpdatePanel>
</asp:Content>
