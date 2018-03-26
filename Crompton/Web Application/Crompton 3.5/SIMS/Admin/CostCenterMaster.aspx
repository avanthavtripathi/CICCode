<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true"
    CodeFile="CostCenterMaster.aspx.cs" Inherits="SIMS_Admin_CostCenterMaster" Title="Cost Center" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred" style="width: 968px">
                        Cost Center Master
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
                    <td colspan="2" align="right" style="padding-right: 10px">
                        <%--FOR FILTERING RECORD ON THE BASIS OF ACTIVE AND INACTIVE--%>
                        <asp:RadioButtonList ID="rdoboth" RepeatDirection="Horizontal" RepeatColumns="3"
                            runat="server" AutoPostBack="True" 
                            onselectedindexchanged="rdoboth_SelectedIndexChanged" >
                            <asp:ListItem Value="1" Text="Active" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="0" Text="In-Active"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Both"></asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:Button Visible="false" Text="Go" Width="25px" CssClass="btn" ID="imgBtnGo" runat="server"
                            CausesValidation="False" ValidationGroup="editt1" OnClick="imgBtnGo_Click" />
                        <%--FILTERING RECORD ON THE BASIS OF ACTIVE AND INACTIVE END--%>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" colspan="2">
                        <table border="0" width="100%">
                            <tr>
                                <td align="left" class="MsgTDCount">
                                    Total Number of Records :
                                    <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server">
                                    </asp:Label>
                                </td>
                                <td align="right">
                                    Search For
                                    <asp:DropDownList ID="ddlSearch" runat="server" Width="130px" CssClass="simpletxt1">
                                        <asp:ListItem Text="Division" Value="MU.Unit_Desc"></asp:ListItem>
                                        <asp:ListItem Text="Cost Center Code" Value="Cost_Center_Code"></asp:ListItem>
                                        <asp:ListItem Text="Cost Center Description" Value="Cost_Center_Desc"></asp:ListItem>
                                        <asp:ListItem Text="BA Code" Value="BA_Code"></asp:ListItem>
                                    </asp:DropDownList>
                                    With
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="txtboxtxt" Width="100px" Text=""></asp:TextBox>
                                    <asp:Button Text="Go" Width="25px" CssClass="btn" ID="Button1" runat="server" CausesValidation="False"
                                        ValidationGroup="editt" OnClick="imgBtnGo_Click" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0">
                            <tr>
                                <td>
                                    <!-- Batch Listing -->
                                    <asp:GridView RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="True"
                                        AllowSorting="True" DataKeyNames="Cost_Center_Id" 
                                        AutoGenerateColumns="False" ID="gvCostCenter"
                                        runat="server"  Width="100%" 
                                        HorizontalAlign="Left" 
                                        onpageindexchanging="gvCostCenter_PageIndexChanging" 
                                        onselectedindexchanging="gvCostCenter_SelectedIndexChanging" 
                                        onsorting="gvCostCenter_Sorting" >
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sno" HeaderStyle-Width="150px" >
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                          
                                            <asp:BoundField DataField="Unit_Desc" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Division" SortExpression="Unit_Desc">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Cost_Center_Code" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Cost Center Code" SortExpression="Cost_Center_Code">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Cost_Center_Desc" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Cost Center Description" SortExpression="Cost_Center_Desc">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="BA_Code" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="BA Code" SortExpression="BA_Code">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Active_Flag" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Status" SortExpression="Active_Flag">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:CommandField ShowSelectButton="True" HeaderStyle-Width="60px" HeaderText="Edit">
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
                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                        <AlternatingRowStyle CssClass="fieldName" />
                                    </asp:GridView>
                                    <!-- End Batch Listing -->
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td width="100%" align="center" class="bgcolorcomm">
                                    <table width="98%" border="0">
                                        <tr>
                                            <td colspan="2" align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                                <font color='red'>*</font>
                                                <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:HiddenField ID="hdnCostCenterId" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td  align="right">
                                                Division:<font color='red'>*</font>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlDivision" runat="server" CssClass="simpletxt1" Width="175px">
                                                </asp:DropDownList>
                                                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator9" InitialValue="Select"
                                                    runat="server" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Division is required."
                                                    ControlToValidate="ddlDivision" ValidationGroup="editt" ToolTip="Division is required."></asp:RequiredFieldValidator></td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Cost Center Code:<font color='red'>*</font>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtCostCenterCode" MaxLength="20" runat="server"
                                                    Width="170px" Text="" />&nbsp;<asp:RequiredFieldValidator SetFocusOnError="true"
                                                        ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCostCenterCode"
                                                        ErrorMessage="Cost Center Code is required." Display="Dynamic" ToolTip="Cost Center Code is required."
                                                        ValidationGroup="editt"></asp:RequiredFieldValidator></td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Cost Center Description:<font color='red'>*</font>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtCostCenterDesc" MaxLength="100" runat="server"
                                                    Width="170px" Text="" />&nbsp;<asp:RequiredFieldValidator SetFocusOnError="true"
                                                        ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCostCenterDesc"
                                                        ErrorMessage="Cost Center Description is required." Display="Dynamic" ToolTip="Cost Center Description is required."
                                                        ValidationGroup="editt"></asp:RequiredFieldValidator></td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                BA Code:<font color='red'>*</font>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtBACode" MaxLength="4" runat="server" Width="170px"
                                                    Text="" />
                                                <asp:RequiredFieldValidator SetFocusOnError="true"
                                                        ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtBACode"
                                                        ErrorMessage="BA Code is required." Display="Dynamic" ToolTip="BA Code is required."
                                                        ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Status
                                            </td>
                                            <td align="left">
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
                                            <td colspan="2" align="center" style="padding-left: 140px">
                                                <table>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Button Text="Add" Width="70px" CssClass="btn" ID="imgBtnAdd" runat="server"
                                                                CausesValidation="True" ValidationGroup="editt" 
                                                                onclick="imgBtnAdd_Click"  />
                                                            <asp:Button Text="Save" Width="70px" ID="imgBtnUpdate" CssClass="btn" runat="server"
                                                                CausesValidation="True" ValidationGroup="editt" 
                                                                onclick="imgBtnUpdate_Click"  />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="imgBtnCancel" Width="70px" runat="server" CausesValidation="false"
                                                                CssClass="btn" Text="Cancel" onclick="imgBtnCancel_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
