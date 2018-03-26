<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true"
    CodeFile="FG_IntermediateScreen.aspx.cs" Inherits="SIMS_Pages_FG_IntermediateScreen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        FG Intermediate Master
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["SIMSAjaxLoadingImageName"]%>" alt="" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr style="display: none">
                    <td colspan="2" align="right" style="padding-right: 10px">
                        <asp:RadioButtonList ID="rdoboth" RepeatDirection="Horizontal" RepeatColumns="3"
                            runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdoboth_SelectedIndexChanged">
                            <asp:ListItem Value="1" Text="Mapping done"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Mapping not done" Selected="True"></asp:ListItem>
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
                                <td align="right" style="display: none">
                                    Search For
                                    <asp:DropDownList ID="ddlSearch" runat="server" Width="140px" CssClass="simpletxt1">
                                        <asp:ListItem Text="Product Code" Value="Product_Code"></asp:ListItem>
                                        <asp:ListItem Text="Product Description" Value="Product_Desc"></asp:ListItem>
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
                                    <!-- product Listing -->
                                    <asp:GridView RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="True"
                                        AllowSorting="True" DataKeyNames="Intermediate_FG_Id" AutoGenerateColumns="False"
                                        ID="gvComm" runat="server" OnPageIndexChanging="gvComm_PageIndexChanging" Width="100%"
                                        OnSelectedIndexChanging="gvComm_SelectedIndexChanging" HorizontalAlign="Left"
                                        OnRowDataBound="gvComm_RowDataBound" Visible="true" 
                                        OnSorting="gvComm_Sorting">
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <asp:BoundField DataField="Sno" HeaderText="SNo">
                                                <HeaderStyle Width="40px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Product_Code" SortExpression="Product_Code" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Product Code">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Product_Desc" SortExpression="Product_Desc" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Product Description">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Product_Mapped" SortExpression="Product_Mapped" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Product Mapped">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:CommandField ShowSelectButton="True" HeaderText="Edit"></asp:CommandField>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Center" />
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
                                    <!-- End Product Listing -->
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 10px" align="center">
                                    <table width="100%" border="0">
                                        <tr style="display: none">
                                            <td width="100%" align="left" class="bgcolorcomm">
                                                <table width="100%" border="0">
                                                    <tr>
                                                        <td colspan="2" align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                                            <font color='red'>*</font>
                                                            <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="15%">
                                                            Product Code: <font color='red'>*</font>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtProductCode" runat="server" CssClass="txtboxtxt" Text="" Width="170px" />
                                                            <asp:RequiredFieldValidator ID="rfvProdCode" runat="server" ControlToValidate="txtProductCode"
                                                                ErrorMessage="Product is required" SetFocusOnError="true" ValidationGroup="editt">Product is required</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Product Description: <font color='red'>*</font>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtProductDesc" runat="server" CssClass="txtboxtxt" Text="" TextMode="MultiLine"
                                                                Width="170px" />
                                                            <asp:RequiredFieldValidator ID="rfvProdDesc" runat="server" ControlToValidate="txtProductDesc"
                                                                ErrorMessage="Product description is required." SetFocusOnError="true" ValidationGroup="editt">Product description is required.</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Product Mapping Done
                                                        </td>
                                                        <td>
                                                            <asp:RadioButtonList ID="rdoProdMapping" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="Yes" Value="1">
                                                                </asp:ListItem>
                                                                <asp:ListItem Text="No" Value="0" Selected="True">
                                                                </asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <!-- For button portion update -->
                                                            <table>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Button Text="Add" Width="70px" CssClass="btn" ID="imgBtnAddFGIntmd" runat="server"
                                                                            CausesValidation="True" ValidationGroup="editt" OnClick="imgBtnAddFGIntmd_Click" />
                                                                        <asp:Button Text="Save" Width="70px" ID="imgBtnUpdateFGIntmd" CssClass="btn" runat="server"
                                                                            CausesValidation="True" ValidationGroup="editt" OnClick="imgBtnUpdateFGIntmd_Click" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="imgBtnCancelFGIntmd" Width="70px" runat="server" CausesValidation="false"
                                                                            CssClass="btn" Text="Cancel" OnClick="imgBtnCancelFGIntmd_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <!-- For button portion update end -->
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            &nbsp;
                                                        </td>
                                                        <td align="right">
                                                            <asp:LinkButton ID="lnkbtnProductMapping" runat="server" OnClick="lnkbtnProductMapping_Click">Product Mapping</asp:LinkButton>
                                                            <asp:Button ID="imgBtnBack" Width="70px" runat="server" CausesValidation="false"
                                                                CssClass="btn" Text="<< Back" Visible="false" OnClick="imgBtnBack_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div id="divProductMapping" runat="server" visible="true">
                                                    <table width="100%" border="0">
                                                        <tr>
                                                            <td class="headingred" align="left">
                                                                Product Mapping
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="100%" align="left" class="bgcolorcomm">
                                                                <table width="100%" border="0">
                                                                    <tr>
                                                                        <td colspan="2" align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                                                            <font color='red'>*</font>
                                                                            <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <asp:HiddenField ID="hdnProductSNo" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            Product Division: <font color='red'>*</font>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlUnit" CssClass="simpletxt1" Width="420px" runat="server" AutoPostBack="True"
                                                                                OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged">
                                                                                <asp:ListItem>Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="Select"
                                                                                SetFocusOnError="true" ControlToValidate="ddlUnit" ValidationGroup="edit">Product Division is required.</asp:RequiredFieldValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            Product Line: <font color='red'>*</font>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlProductLine" CssClass="simpletxt1" Width="420px" runat="server"
                                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlProductLine_SelectedIndexChanged">
                                                                                <asp:ListItem>Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="Select"
                                                                                SetFocusOnError="true" ControlToValidate="ddlProductLine" ValidationGroup="edit">Product line is required</asp:RequiredFieldValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            Product Group: <font color='red'>*</font>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlProductGroup" CssClass="simpletxt1" Width="420px" runat="server">
                                                                                <asp:ListItem>Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="true"
                                                                                ErrorMessage="Select Product Group." InitialValue="Select" ControlToValidate="ddlProductGroup"
                                                                                ValidationGroup="edit">Product group is required</asp:RequiredFieldValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            Product:<font color='red'>*</font>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlProduct" runat="server" CssClass="simpletxt1" Width="420px">
                                                                                <asp:ListItem>Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldProductCode" runat="server" SetFocusOnError="true"
                                                                                ErrorMessage="Product is required" InitialValue="Select" ControlToValidate="ddlProduct"
                                                                                Text="Product is required" ValidationGroup="edit"></asp:RequiredFieldValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="display: none">
                                                                        <td>
                                                                            Rating Status
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlRating" runat="server" CssClass="simpletxt1" Width="175">
                                                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                                                <asp:ListItem Value="C">Current</asp:ListItem>
                                                                                <asp:ListItem Value="O">Obsolete</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="display: none">
                                                                        <td>
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
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <!-- For button portion update -->
                                                                            <table>
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <asp:Button Text="Add" Width="70px" CssClass="btn" ID="imgBtnAdd" runat="server"
                                                                                            CausesValidation="True" ValidationGroup="edit" OnClick="imgBtnAdd_Click" />
                                                                                        <%--<asp:Button Text="Save" Width="70px" ID="imgBtnUpdate" CssClass="btn" 
                                                                                            runat="server" CausesValidation="True"
                                                                                            ValidationGroup="edit" OnClick="imgBtnUpdate_Click" />--%>
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
                                                                    <tr>
                                                                        <td height="25" align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
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
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
