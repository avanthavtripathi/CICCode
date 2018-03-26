<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="CityBranchMappingWithRespectToSuspenceAccount.aspx.cs" Inherits="Admin_CityBranchMappingWithRespectToSuspenceAccount" %>

<asp:Content ID="ContentStateMaster" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        City Branch Mapping Master
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right" style="padding-right: 10px">
                        <asp:RadioButtonList ID="rdoboth" RepeatDirection="Horizontal" RepeatColumns="3"
                            runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdoboth_SelectedIndexChanged">
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
                                    Total Number of Records :
                                    <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    Search For
                                    <asp:DropDownList ID="ddlSearch" runat="server" Width="150px" CssClass="simpletxt1">
                                       
                                        <asp:ListItem Text="Region" Value="region_DESc"></asp:ListItem>
                                        <asp:ListItem Text="State" Value="State_Desc"></asp:ListItem>
                                         <asp:ListItem Text="City" Value="City_Desc"></asp:ListItem>
                                         <asp:ListItem Text="Branch" Value="Branch_Name"></asp:ListItem>
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
                                <td class="bgcolorcomm">
                                    <asp:GridView ID="gvComm" runat="server" AllowPaging="True" AllowSorting="True" AlternatingRowStyle-CssClass="fieldName"
                                        AutoGenerateColumns="False" DataKeyNames="SRNo" EnableSortingAndPagingCallbacks="True"
                                        GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor" HorizontalAlign="Left"
                                        OnPageIndexChanging="gvComm_PageIndexChanging" OnSelectedIndexChanging="gvComm_SelectedIndexChanging"
                                        OnSorting="gvComm_Sorting" RowStyle-CssClass="gridbgcolor" Width="100%">
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                <HeaderStyle Width="40px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="region_DESc" HeaderStyle-HorizontalAlign="Left" HeaderText="Region"
                                                ItemStyle-HorizontalAlign="Left" SortExpression="region_DESc">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="State_Desc" HeaderStyle-HorizontalAlign="Left" HeaderText="State"
                                                ItemStyle-HorizontalAlign="Left" SortExpression="State_Desc">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="City_Desc" HeaderStyle-HorizontalAlign="Left" HeaderText="City"
                                                ItemStyle-HorizontalAlign="Left" SortExpression="City_Desc">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Branch_Name" HeaderStyle-HorizontalAlign="Left" HeaderText="Branch"
                                                ItemStyle-HorizontalAlign="Left" SortExpression="Branch_Name">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Active_Flag" HeaderStyle-HorizontalAlign="Left" HeaderText="Status"
                                                ItemStyle-HorizontalAlign="Left" SortExpression="Active_Flag">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:CommandField HeaderStyle-Width="60px" HeaderText="Edit" ShowSelectButton="True">
                                                <HeaderStyle Width="60px" />
                                            </asp:CommandField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                        <img alt="" src='<%=ConfigurationManager.AppSettings["UserMessage"]%>' />
                                                        <b>No Records found</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                        <AlternatingRowStyle CssClass="fieldName" />
                                    </asp:GridView>
                                    <!-- Branch Listing -->
                                    <!-- End Branch Listing -->
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
                                        <tr>
                                            <td colspan="2" align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>"
                                                style="height: 17px">
                                                <font color='red'>*</font>
                                                <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:HiddenField ID="hdnCityBranchMappingSNo" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Region Name <font color="red">*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlRegionDesc" runat="server" CssClass="simpletxt1" Width="175px"
                                                    ValidationGroup="editt" AutoPostBack="True" OnSelectedIndexChanged="ddlRegionDesc_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RFRegionDesc" runat="server" ControlToValidate="ddlRegionDesc"
                                                    ErrorMessage="Region Name is required." InitialValue="0" SetFocusOnError="true"
                                                    ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                State<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlState" runat="server" CssClass="simpletxt1" Width="175px"
                                                    ValidationGroup="editt" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlState"
                                                    ErrorMessage="State is required." InitialValue="0" SetFocusOnError="true"
                                                    ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                City
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCity" runat="server" CssClass="simpletxt1" Width="175px">
                                                    <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Branch Name
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlBranchName" runat="server" CssClass="simpletxt1" Width="175px">
                                                    <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
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
