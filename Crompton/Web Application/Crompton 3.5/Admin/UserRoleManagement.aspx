<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="UserRoleManagement.aspx.cs" Inherits="Admin_UserRoleManagement"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
<script src="../scripts/Jquery.1.7.2.min.js" type="text/javascript"></script>
<script src="../scripts/jquery-customselect.js" type="text/javascript"></script>
<link href="../css/CustomSelectddl.css" rel="stylesheet" type="text/css" />
 
<script type="text/javascript">
    function CustomSelectddl() {
    $("[id$=ddlUserList]").customselect();
    };
</script>
<asp:UpdatePanel ID="pnlRoles" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        User Role Management
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td class="bgcolorcomm" colspan="2">
                        <table width="100%">
                            <tr>
                                <td>
                                    <b>Manage Roles By User</b>
                                </td>
                            </tr>
                            
                            <tr>
                                <td><asp:DropDownList ID="ddlUserList" CssClass="custom-select" runat="server" AutoPostBack="True" 
                                       OnSelectedIndexChanged="ddlUserList_SelectedIndexChanged">
                                    </asp:DropDownList></td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DataList Width="100%" ID="rptUsersRoleList" runat="server" RepeatDirection="Horizontal"
                                        RepeatColumns="5">
                                        <HeaderTemplate>
                                            <b>Roles</b><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chkRoleCheckBox" AutoPostBack="true" Text='<%# Eval("RoleName") %>'
                                                OnCheckedChanged="chkRoleCheckBox_CheckChanged" />
                                            <br />
                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                            </tr>
                            </table></td></tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Label ID="ActionStatus" runat="server" CssClass="simpletxt1" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                                    <tr>
                    <td class="bgcolorcomm" colspan="2">
                        <table width="100%">
                            <tr>
                                <td style="padding-bottom:10px;">
                                    
                                    <b>Manage Users By Role</b>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-bottom:10px;">
                                    Select a Role:
                                    <asp:DropDownList CssClass="simpletxt1" ID="ddlRoleList" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlRoleList_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            
                            <tr>
                                <td>
                                    <asp:GridView ID="gvRolesUserList" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridLines="both" runat="server" AutoGenerateColumns="False" EmptyDataText="No users belong to this role."
                                        OnRowDeleting="gvRolesUserList_RowDeleting" OnRowDataBound="gvRolesUserList_RowDataBound"
                                        Width="366px" AllowPaging="True" OnPageIndexChanging="gvRolesUserList_PageIndexChanging"
                                        PageSize="15">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Users" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                <asp:HiddenField ID="hdnUserName" Value='<%#Eval("Username")%>' runat="server" />
                                                    <asp:Label runat="server" ID="lblUserName" Text='<%#Eval("name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remove" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnDelete" CausesValidation="false" runat="server" Text="Remove"
                                                        CommandName="Delete"></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        
    </asp:UpdatePanel>



</asp:Content>

