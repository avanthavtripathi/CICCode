<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="UnitMaster.aspx.cs" Inherits="Admin_UnitMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Unit Master
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" colspan="2" class="bgcolorcomm">
                        <table border="0" width="55%" class="bgcolorcomm">
                           <tr align="center">  <th class="fieldNamewithbgcolor">Search</th>
                            </tr>
                            <tr>
                                <td align="left">Search For <asp:DropDownList ID="ddlSearch" runat="server" Width="130px" CssClass="simpletxt1">
                                <asp:ListItem Text="Unit" Value="Unit_Code"></asp:ListItem>
                                <asp:ListItem Text="Description" Value="Unit_Desc"></asp:ListItem>
                                <asp:ListItem Text="Company" Value="Company_Code"></asp:ListItem>
                                <%--<asp:ListItem Text="Unit Type" Value="UnitType_SNo"></asp:ListItem>--%>
                                <asp:ListItem Text="Dealing Branch" Value="DealingBranch_Code"></asp:ListItem>
                                <%--<asp:ListItem Text="Status" Value="Active_Flag"></asp:ListItem>--%>
                                
                                </asp:DropDownList>
                                
                                With <asp:TextBox ID="txtSearch" runat="server" CssClass="txtboxtxt" Width="100px" Text=""></asp:TextBox>
                                
                                  <asp:Button Text="Go" Width="25px" CssClass="btn" ID="imgBtnGo" runat="server"
                                    CausesValidation="False" ValidationGroup="editt" OnClick="imgBtnGo_Click" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table width="98%" border="0">
                            <tr>
                                <td>
                                    <!-- Unit Listing   -->
                                    <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridLines="both" AllowPaging="True"
                                        AllowSorting="True" DataKeyNames="Unit_SNo" AutoGenerateColumns="False" ID="gvComm"
                                        Width="98%" runat="server" HorizontalAlign="Left" OnSelectedIndexChanging="gvComm_SelectedIndexChanging">
                                        <%--OnPageIndexChanging="gvComm_PageIndexChanging" --%>
                                        <Columns>
                                            <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                <HeaderStyle Width="40px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Unit_Code" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Unit">
                                                <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Unit_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Unit Desc">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Company_Code" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Company">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="UnitType_SNo" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="UnitType">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DealingBranch_Code" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Dealing Branch">
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
                                    <!-- End Unit Listing -->
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
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
                                                <asp:HiddenField ID="hdnUnitSNo" runat="server" />
                                                <asp:HiddenField ID="hdnIsBA" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Company <font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="simpletxt1" Width="130px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCompany"
                                                    ErrorMessage="Company is required." SetFocusOnError="true" ValidationGroup="editt"
                                                    InitialValue="Select"></asp:RequiredFieldValidator>
                                                <%--OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Unit <font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtUnit" runat="server" Width="170px" Text=""
                                                    MaxLength="10" /><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                        SetFocusOnError="true" ErrorMessage=" Unit is required." ControlToValidate="txtUnit"
                                                        ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Unit Description <font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtUnitDesc" runat="server" Width="170px" Text=""
                                                    MaxLength="100" /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                        SetFocusOnError="true" ErrorMessage=" Unit Description is required." ControlToValidate="txtunitDesc"
                                                        ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Sap Loaction Code
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtSapLocCode" runat="server" Width="170px"
                                                    Text="" MaxLength="20" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Unit Abbr
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtUnitAbbr" runat="server" Width="170px" Text=""
                                                    MaxLength="10" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Unit Type <font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlUnitType" runat="server" CssClass="simpletxt1" AutoPostBack="true"
                                                    Width="130px" OnSelectedIndexChanged="ddlUnitType_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlUnitType"
                                                    ErrorMessage="Unit Type is required." SetFocusOnError="true" ValidationGroup="editt"
                                                    InitialValue="Select"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Business Area / Region <font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlBusArea" runat="server" CssClass="simpletxt1" Width="130px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlBusArea"
                                                    ErrorMessage="Business Area / Region is required." SetFocusOnError="true" ValidationGroup="editt"
                                                    InitialValue="Select"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Dealing Branch
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlDealingBrh" runat="server" CssClass="simpletxt1" Width="130px"
                                                    Enabled="false">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="ReqDealingBrh" runat="server" ControlToValidate="ddlDealingBrh"
                                                    ErrorMessage="Dealing Branch is required." SetFocusOnError="true" ValidationGroup="editt"
                                                    InitialValue="Select"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Warranty From Manufacturing<br />(in month)
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtWarrFManuf" runat="server" Width="170px" Text=""
                                                    MaxLength="100" />
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                                        SetFocusOnError="true" ErrorMessage=" Warranty From Manufacturing is required." ControlToValidate="txtWarrFManuf"
                                                        ValidationGroup="editt"></asp:RequiredFieldValidator>--%>
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtWarrFManuf" ErrorMessage="Enter whole numbers only."
                                                    Operator="DataTypeCheck" Type="Integer" SetFocusOnError="True" ValidationGroup="editt"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Warranty From Purchase<br />(in month)
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtWarrFPurchase" runat="server" Width="170px" Text=""
                                                    MaxLength="100" />
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                                                        SetFocusOnError="true" ErrorMessage=" Warranty From Purchase is required." ControlToValidate="txtWarrFPurchase"
                                                        ValidationGroup="editt"></asp:RequiredFieldValidator>--%>
                                                        <asp:CompareValidator ID="CC1" runat="server" ControlToValidate="txtWarrFPurchase" ErrorMessage="Enter whole numbers only."
                                                    Operator="DataTypeCheck" Type="Integer" SetFocusOnError="True" ValidationGroup="editt"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                          <tr>
                                            <td width="30%">
                                                Visting Charges
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtVistCharge" runat="server" Width="170px" Text=""
                                                    MaxLength="100" />
                                                     <asp:RegularExpressionValidator ID="RegValidCostPrice" runat="server" ControlToValidate="txtVistCharge"
                                                    ValidationGroup="editt" ValidationExpression="\d{0,10}.\d{0,2}">
                                                    Cost Price Should Be In(#.# - ##########-##) Format</asp:RegularExpressionValidator>
                                                    
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                                                        SetFocusOnError="true" ErrorMessage=" Visting Charges is required." ControlToValidate="txtVistCharge"
                                                        ValidationGroup="editt"></asp:RequiredFieldValidator>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Address1
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAdd1" runat="server" CssClass="txtboxtxt" Text="" Width="170px"
                                                    MaxLength="100" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Address2
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAdd2" runat="server" CssClass="txtboxtxt" Text="" Width="170px"
                                                    MaxLength="100" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Address3
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAdd3" runat="server" CssClass="txtboxtxt" Text="" Width="170px"
                                                    MaxLength="100" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Country <font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="simpletxt1" AutoPostBack="true"
                                                    Width="130px" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlCountry"
                                                    ErrorMessage="Country is required." SetFocusOnError="true" ValidationGroup="editt"
                                                    InitialValue="Select"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                State <font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlState" runat="server" CssClass="simpletxt1" AutoPostBack="true"
                                                    Width="130px" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlState"
                                                    ErrorMessage="State is required." SetFocusOnError="true" ValidationGroup="editt"
                                                    InitialValue="Select"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                City <font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCity" runat="server" CssClass="simpletxt1" Width="130px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlCity"
                                                    ErrorMessage="City is required." SetFocusOnError="true" ValidationGroup="editt"
                                                    InitialValue="Select"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Pin Code
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtPinCode" runat="server" Width="170px" Text=""
                                                    MaxLength="7" />&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Phone
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtPhone" runat="server" Width="170px" Text=""
                                                    MaxLength="15" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Mobile
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtMobile" runat="server" Width="170px" Text=""
                                                    MaxLength="15" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Fax
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtFax" runat="server" Width="170px" Text=""
                                                    MaxLength="15" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Email
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtEmail" runat="server" Width="170px" Text=""
                                                    MaxLength="60" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Status
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rdoStatus" RepeatDirection="Horizontal" RepeatColumns="2"
                                                    runat="server">
                                                    <asp:ListItem Value="1" Text="Active" Selected="True">
                                                    </asp:ListItem>
                                                    <asp:ListItem Value="0" Text="In-Active">
                                                    </asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="25" align="left">
                                                &nbsp;
                                            </td>
                                            <td>
                                                <!-- For button portion update -->
                                                <table>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Button Text="Add" Width="70px" CssClass="btn" ID="imgBtnAdd" runat="server"
                                                                CausesValidation="True" ValidationGroup="editt" OnClick="imgBtnAdd_Click" />
                                                            <asp:Button Text="Save" Width="70px" ID="imgBtnUpdate" CssClass="btn" runat="server"
                                                                CausesValidation="True" ValidationGroup="editt" OnClick="imgBtnUpdate_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="imgBtnCancel" Width="70px" runat="server" CausesValidation="false"
                                                                CssClass="btn" Text="Cancel" OnClick="imgBtnCancel_Click" />
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
