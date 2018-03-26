<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true"
    CodeFile="ASCLocationMaster.aspx.cs" Inherits="SIMS_Admin_ASCLocationMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%" cellpadding="2" cellspacing="0">
                <tr>
                    <td class="headingred">
                        ASC Location Master
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["SIMSAjaxLoadingImageName"]%>" alt="" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right">
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
                                        <asp:ListItem Text="Service Contactor" Value="SC_Name"></asp:ListItem>
                                        <asp:ListItem Text="Location Code" Value="Loc_Code"></asp:ListItem>
                                        <asp:ListItem Text="Location Name" Value="Loc_Name"></asp:ListItem>
                                        <asp:ListItem Text="Service Engineer" Value="ServiceEng_Name"></asp:ListItem>
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
                                        AutoGenerateColumns="False" DataKeyNames="Loc_Id" GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor"
                                        HorizontalAlign="Left" OnPageIndexChanging="gvComm_PageIndexChanging" OnSelectedIndexChanging="gvComm_SelectedIndexChanging"
                                        PageSize="10" RowStyle-CssClass="gridbgcolor" Width="100%" EnableSortingAndPagingCallbacks="True"
                                        OnSorting="gvComm_Sorting">
                                        <Columns>
                                            <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                <HeaderStyle Width="40px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SC_Name" SortExpression="SC_Name" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Service Contractor" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" Width="200px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Loc_Code" SortExpression="Loc_Code" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Location Code" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Loc_Name" SortExpression="Loc_Name" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Location Name" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" Width="200px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ServiceEng_Name" SortExpression="ServiceEng_Name" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Service Engineer" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Active_Flag" SortExpression="Active_Flag" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Status" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:CommandField HeaderStyle-Width="60px" HeaderText="Edit" ShowSelectButton="True">
                                                <HeaderStyle Width="60px" />
                                            </asp:CommandField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                        <img src="<%=ConfigurationManager.AppSettings["SIMSUserMessage"]%>" alt="" />
                                                        <b>No Record found</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                    <!-- Location Listing -->
                                    <!-- End Location Listing -->
                                    <asp:HiddenField ID="hdnLoc_Id" runat="server" />
                                    <asp:HiddenField ID="hdnASC_Id" runat="server" />
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
                                            <td colspan="4" align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                                <font color='red'>*</font>
                                                <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Service Contactor:<font color='red'>*</font>
                                            </td>
                                            <td colspan="3">
                                                <asp:Label ID="lblASCName" runat="server"></asp:Label>
                                                <%--<asp:DropDownList ID="ddlASCCode" runat="server" CssClass="simpletxt1" Width="300px"
                                                    ValidationGroup="editt" AutoPostBack="True" OnSelectedIndexChanged="ddlASCCode_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                &nbsp;
                                                <asp:RequiredFieldValidator ID="RFRegionDesc" runat="server" ControlToValidate="ddlASCCode"
                                                    ErrorMessage="Service Contactor is required." InitialValue="Select" SetFocusOnError="true"
                                                    ValidationGroup="editt" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="14%">
                                                Location Code:<font color='red'>*</font>
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtLocCode" runat="server" MaxLength="20" Width="175px"
                                                    Text="" />&nbsp;<asp:RequiredFieldValidator ID="RFStateDesc" runat="server" SetFocusOnError="true"
                                                    ErrorMessage="Location Code is required." Display="Dynamic" ControlToValidate="txtLocCode"
                                                    ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Location Name:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtLocName" runat="server" MaxLength="50" Width="175px"
                                                    Text="" />&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                        SetFocusOnError="true" ErrorMessage="Location Name is required." ControlToValidate="txtLocName"
                                                        ValidationGroup="editt" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td>
                                                Service Engineer:
                                            </td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="ddlServiceEng" runat="server" CssClass="simpletxt1" Width="300px"
                                                    ValidationGroup="editt">
                                                </asp:DropDownList>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlServiceEng"
                                            ErrorMessage="Service Engineer is required." InitialValue="Select" SetFocusOnError="true"
                                            ValidationGroup="editt"></asp:RequiredFieldValidator>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Status:
                                            </td>
                                            <td colspan="3">
                                                <asp:RadioButtonList ID="rdoStatus" RepeatDirection="Horizontal" RepeatColumns="2"
                                                    runat="server">
                                                    <asp:ListItem Value="1" Text="Active" Selected="True">
                                                    </asp:ListItem>
                                                    <asp:ListItem Value="0" Text="In-Active">
                                                    </asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <%--<td>Default Location:</td>
                                    <td>
                                        <asp:RadioButtonList ID="rdoDefaultLocation" RepeatDirection="Horizontal" RepeatColumns="2"
                                            runat="server">
                                            <asp:ListItem Value="n" Text="No" Selected="True">
                                            </asp:ListItem>
                                            <asp:ListItem Value="y" Text="Yes">
                                            </asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>--%>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="padding-left: 150px">
                                                <!-- For button portion update -->
                                                <table>
                                                    <tr>
                                                        <td align="left">
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
