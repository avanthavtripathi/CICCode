<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="BusinessArea.aspx.cs" Inherits="Admin_BusinessArea" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Business Area Master
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" colspan="2">
                        <table border="0" width="98%">
                            <tr>
                                <td align="right">Search For <asp:DropDownList ID="ddlSearch" runat="server" Width="130px" CssClass="simpletxt1">
                                <asp:ListItem Text="Code" Value="BA_Code"></asp:ListItem>
                                <asp:ListItem Text="Description" Value="BA_Desc"></asp:ListItem>
                                 <asp:ListItem Text="SBU" Value="SBU_Desc"></asp:ListItem>
                                  <asp:ListItem Text="Company" Value="Company_Name"></asp:ListItem>
                                <%--<asp:ListItem Text="Status" Value="Active_Flag"></asp:ListItem>--%>
                                
                                </asp:DropDownList>
                                
                                With <asp:TextBox ID="txtSearch" runat="server" CssClass="txtboxtxt" Width="100px" Text=""></asp:TextBox>
                                
                                  <asp:Button Text="Go" Width="25px" CssClass="btn" ID="imgBtnGo" runat="server"
                                    CausesValidation="False" ValidationGroup="editt" OnClick="imgBtnGo_Click"  />
                                </td>
                            </tr>
                        </table>

                        <table width="98%" border="0">
                            <tr>
                                <td>
                                    <!-- Business Area Listing   -->
                                    <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridLines="both" AllowPaging="True"
                                        AllowSorting="True" DataKeyNames="BA_Sno" AutoGenerateColumns="False" ID="gvComm"
                                        runat="server" OnPageIndexChanging="gvComm_PageIndexChanging" Width="100%" OnSelectedIndexChanging="gvComm_SelectedIndexChanging"
                                        HorizontalAlign="Left">
                                        <Columns>
                                            <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                <HeaderStyle Width="40px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="BA_Code" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Code">
                                                <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="BA_Desc" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Description">
                                                <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SBU_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="SBU">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Company_Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Company">
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
                                    <!-- End Company Listing -->
                                </td>
                            </tr>
                            <tr><td>&nbsp;</td></tr>
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
                                                <asp:HiddenField ID="hdnBASNo" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Business Area Code:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtBACode" runat="server" Width="170px" Text=""
                                                    MaxLength="10" /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                        SetFocusOnError="true" ErrorMessage="Business Area Code is required." ControlToValidate="txtBACode"
                                                        ValidationGroup="editt" ToolTip="Business Area Code is required."></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                SBU:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlSBU" ValidationGroup="editt" runat="server" CssClass="simpletxt1">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RFSBU" runat="server" SetFocusOnError="true" ErrorMessage="SBU is required."
                                                    ControlToValidate="ddlSBU" ValidationGroup="editt" ToolTip="SBU is required."
                                                    InitialValue="Select"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                BA Description:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtBADescription" runat="server" Width="170px"
                                                    Text="" MaxLength="100" /><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                                        runat="server" SetFocusOnError="true" ErrorMessage="Business Area Description is required."
                                                        ControlToValidate="txtBADescription" ToolTip="Business Area Description is required." ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                ST No:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCSTNo" runat="server" CssClass="txtboxtxt" Text="" Width="170px"
                                                    MaxLength="50" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                CST No:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSTNo" runat="server" CssClass="txtboxtxt" Text="" Width="170px"
                                                    MaxLength="50" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Excise No:
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtExciseNo" runat="server" Width="170px" Text="" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Status:
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
