<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="LockStatus.aspx.cs" Inherits="Admin_LockStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Locked Users
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
                                    <asp:DropDownList ValidationGroup="editt1" ID="ddlSearch" runat="server" Width="130px"
                                        CssClass="simpletxt1">
                                        <asp:ListItem Text="User Id" Value="MSU.UserName"></asp:ListItem>
                                        <asp:ListItem Text="Name" Value="MSU.Name"></asp:ListItem>
                                        <asp:ListItem Text="User Type" Value="UserType_Name"></asp:ListItem>
                                    </asp:DropDownList>
                                    With
                                    <asp:TextBox ID="txtSearch" ValidationGroup="editt1" runat="server" CssClass="txtboxtxt"
                                        Width="100px" Text=""></asp:TextBox>
                                    <asp:Button Text="Go" Width="25px" CssClass="btn" ID="imgBtnGo" runat="server" CausesValidation="False"
                                        ValidationGroup="editt1" OnClick="imgBtnGo_Click" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0">
                            <tr>
                                <td>
                                    <asp:GridView PageSize="15" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="True"
                                        AllowSorting="True" DataKeyNames="UserName" AutoGenerateColumns="False" ID="gvShowUser"
                                        runat="server" OnPageIndexChanging="gvShowUser_PageIndexChanging" Width="100%" OnSelectedIndexChanging="gvShowUser_SelectedIndexChanging"
                                        HorizontalAlign="Left" OnSorting="gvShowUser_Sorting">
                                        <Columns>
                                            <asp:BoundField DataField="Sno" HeaderStyle-Width="60px" HeaderText="SNo">
                                                <HeaderStyle Width="40px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="UserName" HeaderStyle-Wrap="true" HeaderStyle-Width="80px"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="User Id"
                                                SortExpression="UserName"></asp:BoundField>
                                            <asp:BoundField DataField="Name" HeaderStyle-Wrap="true"  HeaderStyle-Width="350px"  ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Name" SortExpression="Name"></asp:BoundField>
                                            <asp:BoundField DataField="UserType_Name" HeaderStyle-Wrap="true" HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="User Type" SortExpression="UserType_Name">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="IsLockedOut" HeaderStyle-Wrap="true" HeaderStyle-Width="80px"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="IsLocked"
                                                SortExpression="IsLockedOut"></asp:BoundField>
                                            <asp:BoundField DataField="LastLockoutDate" HeaderStyle-Wrap="true" HeaderStyle-Width="90px"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Locked Date"
                                                SortExpression="LastLockoutDate"></asp:BoundField>
                                            <asp:CommandField SelectText="UnLock" ShowSelectButton="True" ItemStyle-CssClass="link" HeaderStyle-Width="80px"
                                                HeaderText="Un Lock"></asp:CommandField>
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
                                    </asp:GridView>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
