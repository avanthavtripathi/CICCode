<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="MenuRoleMaster.aspx.cs" Inherits="Admin_MenuRoleMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Menu Role Master
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
                    <td colspan="2" align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                        <font color='red'>*</font>
                        <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                    </td>
                </tr>
                <tr>
                    <td class="bgcolorcomm" style="padding-left: 10px" colspan="2">
                        Select Role<font color='red'>*</font>&nbsp;<asp:DropDownList CssClass="simpletxt1"
                            ID="ddlRoles" Width="170px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged"
                            ValidationGroup="apply">
                        </asp:DropDownList>
                        <asp:CompareValidator ID="valCompLocation" runat="server" ControlToValidate="ddlRoles"
                            ErrorMessage="Role is required." Operator="NotEqual" SetFocusOnError="True" ValueToCompare="Select"
                            ValidationGroup="apply"></asp:CompareValidator>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox
                                ID="chkSelectAll" Text="Select All" AutoPostBack="true" runat="server" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                    </td>
                </tr>
                <tr>
                    <td style="padding: 5px" align="center" class="bgcolorcomm" colspan="2">
                        <table width="100%" border="0">
                            <tr>
                                <td align="left">
                                    <b>Menu Name (URL)</b>
                                </td>
                            </tr>
                            <tr>
                                <td class="bgcolorcomm" align="left" valign="top">
                                    <!-- Menu Listing -->
                                    <div id="dv" style="height: 350px; width: 920px; overflow: auto; text-align: left">
                                        <asp:CheckBoxList ID="chkMenuList" DataTextField="menutext" DataValueField="MenuId"
                                            RepeatDirection="Vertical" RepeatColumns="1" runat="server" DataSourceID="SDMenus">
                                        </asp:CheckBoxList>
                                    </div>
                                    <asp:SqlDataSource ID="SDMenus" runat="server" ConnectionString="<%$ ConnectionStrings:ConnStr %>"
                                        SelectCommand="select MenuId,menutext + '(' + navigateurl + ')' as menutext from MstMenuMaster order by navigateurl"><%--MenuText">--%>
                                    </asp:SqlDataSource>
                                    <!-- End Menu Listing -->
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnApply" Width="70px" runat="server" text="Save" class="btn" OnClick="btnApply_Click"
                                        ValidationGroup="apply" />
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
