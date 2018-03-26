<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="TerritoryMaster.aspx.cs" Inherits="Admin_TerritoryMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Territory Master
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
                    <td colspan="2" align="right" style="padding-right:10px">
                        <asp:RadioButtonList ID="rdoboth" RepeatDirection="Horizontal" RepeatColumns="3"
                            runat="server" AutoPostBack="True" 
                            onselectedindexchanged="rdoboth_SelectedIndexChanged">
                            <asp:ListItem Value="1" Text="Active" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="0" Text="In-Active"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Both"></asp:ListItem>
                        </asp:RadioButtonList>
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
                                    Search For
                                    <asp:DropDownList ID="ddlSearch" runat="server" Width="130px" CssClass="simpletxt1">
                                        <asp:ListItem Text="Code" Value="Territory_Code"></asp:ListItem>
                                        <asp:ListItem Text="Description" Value="Territory_Desc"></asp:ListItem>
                                        <asp:ListItem Text="City" Value="City_Desc"></asp:ListItem>
                                        <asp:ListItem Text="PinCode" Value="PinCode"></asp:ListItem>
                                        <%--<asp:ListItem Text="Status" Value="Active_Flag"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                    With
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="txtboxtxt" Width="100px" Text=""></asp:TextBox>
                                    <asp:Button Text="Go" Width="25px" CssClass="btn" ID="imgBtnGo" runat="server" CausesValidation="False"
                                        ValidationGroup="editt" OnClick="imgBtnGo_Click" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0">
                            <tr>
                                <td>
                                    <!-- Territory  Listing -->
                                    <asp:GridView PageSize="10" PagerStyle-HorizontalAlign="Center" RowStyle-CssClass="gridbgcolor"
                                        AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                                        GridGroups="both" AllowPaging="True" AllowSorting="True" DataKeyNames="Territory_SNo"
                                        AutoGenerateColumns="False" ID="gvTerritory" runat="server" OnPageIndexChanging="gvTerritory_PageIndexChanging"
                                        Width="100%" OnSelectedIndexChanging="gvTerritory_SelectedIndexChanging" HorizontalAlign="Left"  EnableSortingAndPagingCallbacks="True" onsorting="gvTerritory_Sorting">
                                        <Columns>
                                            <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo" ItemStyle-HorizontalAlign="Center">
                                                <HeaderStyle Width="40px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Territory_Code" SortExpression="Territory_Code" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Code">
                                                <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Territory_Desc" SortExpression="Territory_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Description">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="City_Desc" SortExpression="City_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="City">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Pincode" SortExpression="Pincode" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Pincode">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Active_Flag" SortExpression="Active_Flag" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Status">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:CommandField ShowSelectButton="True" HeaderStyle-Width="60px" HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                <HeaderStyle Width="60px" />
                                            </asp:CommandField>
                                        </Columns>
                                         <EmptyDataTemplate>
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                    <img src="<%=ConfigurationManager.AppSettings["UserMessage"]%>" alt="" /> <b>No Record found</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                        <AlternatingRowStyle CssClass="fieldName" />
                                    </asp:GridView>
                                    <!-- End Territory Listing -->
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
                                                <asp:HiddenField ID="hdnTerritorySNo" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Territory Code<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtTerritoryCode" runat="server" MaxLength="10" Width="170px"
                                                    Text="" />
                                                <asp:RequiredFieldValidator ID="RFTerritoryCode" runat="server" SetFocusOnError="true"
                                                    ErrorMessage="Territory Code is required." ControlToValidate="txtTerritoryCode"
                                                    ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Territory Description<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtTerritoryDesc" runat="server" Width="170px"
                                                    Text="" MaxLength="100"/>
                                                <asp:RequiredFieldValidator ID="RFTerritoryDesc" runat="server" SetFocusOnError="true"
                                                    ErrorMessage="Territory Desc is required." ControlToValidate="txtTerritoryDesc"
                                                    ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Country<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCountry" ValidationGroup="editt" runat="server" AutoPostBack="true"
                                                    CssClass="simpletxt1" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"
                                                    Width="175px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="true"
                                                    InitialValue="Select" ErrorMessage="Country is required." ControlToValidate="ddlCountry"
                                                    ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                State<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlState" ValidationGroup="editt" runat="server" CssClass="simpletxt1"
                                                    OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true" Width="175px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" SetFocusOnError="true"
                                                    InitialValue="Select" ErrorMessage="State is required." ControlToValidate="ddlState"
                                                    ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                City<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCity" ValidationGroup="editt" runat="server" CssClass="simpletxt1"
                                                    Width="175px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true"
                                                    InitialValue="Select" ErrorMessage="City is required." ControlToValidate="ddlCity"
                                                    ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Pincode
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtPincode" ValidationGroup="editt"
                                                     runat="server" Width="170px"
                                                 Text="" MaxLength="6"/>
                                                <asp:RegularExpressionValidator ValidationGroup="editt"
                                                     ID="RegularExpressionValidator1" ControlToValidate="txtPincode"
                                                     runat="server" ErrorMessage="Not a valid Pincode" 
                                                    ValidationExpression="\d{6}"></asp:RegularExpressionValidator>
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
                                            <td height="25" align="left">&nbsp;
                                                
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
                                    <table width="100%" >
                                    <tr id="showTR" runat ="server" >
                                    <td align="right"><a href="../bulkuploaded/bulkUploadTerritoryMaster.xls">Click Here For Bulk Upload</a> </td>
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
