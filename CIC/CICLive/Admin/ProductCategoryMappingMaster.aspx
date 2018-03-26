<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="ProductCategoryMappingMaster.aspx.cs" Inherits="Admin_ProductCategoryMappingMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Product Category Mapping Master
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
                <caption>
                    <tr>
                        <td align="center" colspan="2" style="padding: 10px">
                            <table border="0" width="100%">
                                <tr>
                                    <td align="left" class="MsgTDCount">
                                        Total Number of Records :
                                        <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                    </td>
                                    <td align="right">
                                        Search For
                                        <asp:DropDownList ID="ddlSearch" runat="server" CssClass="simpletxt1" Width="130px">
                                            <asp:ListItem Text="Product Division" Value="Unit_Desc"></asp:ListItem>
                                             <asp:ListItem Text="Category Name" Value="Category_Name"></asp:ListItem>
                                            <asp:ListItem Text="Product Name" Value="PRODUCTNAME"></asp:ListItem>                                            
                                        </asp:DropDownList>
                                        With
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="txtboxtxt" Text="" Width="100px"></asp:TextBox>
                                        <asp:Button ID="imgBtnGo" runat="server" CausesValidation="False" CssClass="btn"
                                            OnClick="imgBtnGo_Click" Text="Go" ValidationGroup="editt" Width="25px" />
                                    </td>
                                </tr>
                            </table>
                            <table border="0" width="100%">
                                <tr>
                                    <td>
                                        <!-- Product Line Listing   -->
                                        <asp:GridView ID="gvComm" runat="server" AllowPaging="True" AllowSorting="True" AlternatingRowStyle-CssClass="fieldName"
                                            AutoGenerateColumns="False" DataKeyNames="Product_ID" GridLines="both" PageSize="10"
                                            Width="100%" HorizontalAlign="Left" OnPageIndexChanging="gvComm_PageIndexChanging"
                                            OnSelectedIndexChanging="gvComm_SelectedIndexChanging" EnableSortingAndPagingCallbacks="True"
                                            OnSorting="gvComm_Sorting">
                                            <Columns>
                                                <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                    <HeaderStyle Width="40px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="BusinessLine" SortExpression="BusinessLine" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Business Line" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Unit_Desc" SortExpression="Unit_Desc" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Product Division" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Category_Name" SortExpression="Category_Name" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Category Name" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PRODUCTNAME" SortExpression="PRODUCTNAME" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Product Name" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ProductCategoryMappingId" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Mapping Id" ItemStyle-HorizontalAlign="Left">
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
                                            <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                            <AlternatingRowStyle CssClass="fieldName" />
                                            <RowStyle CssClass="gridbgcolor" />
                                            <EmptyDataTemplate>
                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                            <img src="<%=ConfigurationManager.AppSettings["UserMessage"]%>" alt="" />
                                                            <b>No Record found</b>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                        <!-- End ProductLine Listing -->
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="bgcolorcomm" width="100%">
                                        <table border="0" width="98%">
                                            <tr align="right">
                                                <td align='<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>' colspan="2">
                                                    <font color="red">*</font>
                                                    <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:HiddenField ID="hdnProductSno" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="30%">
                                                    Business Line <font color='red'>*</font>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlBusinessLine" runat="server" CssClass="simpletxt1" Width="175px"
                                                        OnSelectedIndexChanged="ddlBusinessLine_SelectIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlBusinessLine"
                                                        ErrorMessage="Business Line is required." SetFocusOnError="true" ValidationGroup="editt"
                                                        InitialValue="Select"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="30%">
                                                    Product Division <font color="red">*</font>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlUnitSno" runat="server" Width="175" CssClass="simpletxt1"
                                                        ValidationGroup="editt" AutoPostBack="True" OnSelectedIndexChanged="ddlUnitSno_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="CC1" runat="server" ControlToValidate="ddlUnitSno" ErrorMessage="Product Division is required."
                                                        Operator="NotEqual" SetFocusOnError="True" ValidationGroup="editt" ValueToCompare="Select"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="30%">
                                                    Category Description<font color="red">*</font>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlCategoryDesc" runat="server" CssClass="simpletxt1" Width="175px" />
                                                    &nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlCategoryDesc"
                                                        ErrorMessage="Category Desc is required." Operator="NotEqual" SetFocusOnError="True"
                                                        ValidationGroup="editt" ValueToCompare="Select"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="30%">
                                                    Product Description<font color="red">*</font>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtProductDesc" runat="server" CssClass="txtboxtxt" Text="" MaxLength="100"
                                                        Width="170px" />
                                                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtProductDesc"
                                                        ErrorMessage="Product Desc is required." SetFocusOnError="true" ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="30%">
                                                    Product Category Mapping Id<font color="red">*</font>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtProductCategoryMappingId" runat="server" CssClass="txtboxtxt"
                                                        Text="" MaxLength="100" Width="170px" />
                                                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtProductCategoryMappingId"
                                                        ErrorMessage="Product Category Id is required." SetFocusOnError="true" ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="30%">
                                                    Status
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rdoStatus" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                                                        <asp:ListItem Selected="True" Text="Active" Value="1">
                                                        </asp:ListItem>
                                                        <asp:ListItem Text="In-Active" Value="0">
                                                        </asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" height="25">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <!-- For button portion update -->
                                                    <table>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Button ID="imgBtnAdd" runat="server" CausesValidation="True" CssClass="btn"
                                                                    OnClick="imgBtnAdd_Click" Text="Add" ValidationGroup="editt" Width="70px" />
                                                                <asp:Button ID="imgBtnUpdate" runat="server" CausesValidation="True" CssClass="btn"
                                                                    OnClick="imgBtnUpdate_Click" Text="Save" ValidationGroup="editt" Width="70px" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="imgBtnCancel" runat="server" CausesValidation="false" CssClass="btn"
                                                                    OnClick="imgBtnCancel_Click" Text="Cancel" Width="70px" />
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
                </caption>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
