<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="ProductTypeMaster.aspx.cs" Inherits="Admin_ProductTypeMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
<asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Product Type Master
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
                                    <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server">
                                    </asp:Label>
                                </td>
                                <td align="right">
                                    Search For
                                    <asp:DropDownList ID="ddlSearch" runat="server" Width="160px" CssClass="simpletxt1">
                                        <asp:ListItem Text="Product Type Desc" Value="ProductType_Desc"></asp:ListItem>
                                        <asp:ListItem Text="Product Division" Value="UNIT_DESC"></asp:ListItem>
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
                                    <asp:GridView RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="True"
                                        AllowSorting="True" DataKeyNames="ProductType_Sno" AutoGenerateColumns="False"
                                        ID="gvComm" runat="server" OnPageIndexChanging="gvComm_PageIndexChanging" Width="98%"
                                        HorizontalAlign="Left" OnSorting="gvComm_Sorting">
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                <HeaderStyle Width="40px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                             <asp:TemplateField HeaderText="Business Line" SortExpression="BusinessLine_Desc">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblBusinessLine" runat="server" Text='<%# Bind("BusinessLine_Desc") %>'></asp:Label>
                                                     <asp:HiddenField ID="hdnBLId" runat="server" Value='<%# Bind("BusinessLine_Sno") %>' />
                                                 </ItemTemplate>
                                                 <HeaderStyle HorizontalAlign="Left" />
                                                 <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product Type Desc" 
                                                SortExpression="ProductType_Desc">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductType_Desc" runat="server" Text='<%# Bind("ProductType_Desc") %>'></asp:Label>
                                                    <asp:HiddenField ID="hdnProductTypeId" runat="server" Value='<%# Bind("ProductType_SNO") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product Division" SortExpression="UNIT_DESC">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUNIT_DESC" runat="server" Text='<%# Bind("UNIT_DESC") %>'></asp:Label>
                                                    <asp:HiddenField ID="hdnUnitSno" runat="server" Value='<%# Bind("Unit_Sno") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" SortExpression="Active_Flag">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Active_Flag") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" onclick="lnkEdit_Click">Select</asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
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
                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                        <AlternatingRowStyle CssClass="fieldName" />
                                    </asp:GridView>
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
                                            <td colspan="2" align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                                <font color='red'>*</font>
                                                <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:HiddenField ID="hdnProductTypeId" runat="server" />
                                            </td>
                                        </tr>
                                         <tr>
                                            <td width="30%">
                                                Bussiness Line <font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlBusinessLine" runat="server" CssClass="simpletxt1" Width="175px" OnSelectedIndexChanged="ddlBusinessLine_SelectIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlBusinessLine"
                                                    ErrorMessage="Business Line is required." SetFocusOnError="true" ValidationGroup="editt"
                                                    InitialValue="Select"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Product Division <font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlProductDivision" runat="server" CssClass="simpletxt1" Width="175px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlProductDivision"
                                                    ErrorMessage="Product Division is required." SetFocusOnError="true" ValidationGroup="editt"
                                                    InitialValue="Select"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Product Type Desc <font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtProductTypeDesc" runat="server" Width="170px"
                                                    Text="" /><asp:RequiredFieldValidator ID="reqvalQuestionCode" runat="server" SetFocusOnError="true"
                                                        ErrorMessage="Attribute Desc is required." ControlToValidate="txtProductTypeDesc"
                                                        ToolTip="Product Type Desc is required." ValidationGroup="editt"></asp:RequiredFieldValidator>
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

