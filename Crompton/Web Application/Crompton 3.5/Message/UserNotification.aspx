<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="UserNotification.aspx.cs" Inherits="Message_UserNotification" %>

<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="pnlUpdateMessage" runat="server">
        <ContentTemplate>
            <div style="text-align: center">
                <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
            </div>
            <fieldset>
                <legend><b>Post Message</b></legend>
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="txtMessageCode" runat="server" Text="" Width="70%" MaxLength="50"
                                placeholder="Enter code"></asp:TextBox>
                            &nbsp;
                            <asp:CheckBox ID="chkStatus" runat="server" Text="Active/Non Active" TextAlign="Right" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <FTB:FreeTextBox ID="FtxtMessage" runat="Server" ToolbarLayout="ParagraphMenu, Bold, Italic, Underline, Strikethrough, CreateLink, Unlink, 
                RemoveFormat, JustifyLeft, JustifyRight, JustifyCenter, JustifyFull, BulletedList, 
                NumberedList, Indent, Outdent, Cut, Copy, Paste, Undo, Redo, ieSpellCheck" Width="100%" Height="150px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%;">
                        </td>
                        <td align="right">
                            <asp:Button ID="btnsend" runat="server" Text="submit" OnClick="SubmitContent" />&nbsp;
                            <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="ClearConctrol" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend><b>Message Details</b></legend>
                <asp:GridView ID="grdMessage" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found."
                    EmptyDataRowStyle-BorderWidth="0px" OnRowCommand="grdMessage_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno" ItemStyle-Width="5%" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Literal ID="ltrlMessage" runat="server" Text='<%# Bind("Messagetext") %>'></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField HeaderText="Message" DataField="Messagetext" ItemStyle-Width="65%"
                    HeaderStyle-Width="65%" />--%>
                        <asp:BoundField HeaderText="Author" DataField="CreatedBy" ItemStyle-Width="7%" HeaderStyle-Width="7%" />
                        <asp:BoundField HeaderText="CreatedDate" DataField="CreatedDate" ItemStyle-Width="10%"
                            HeaderStyle-Width="10%" />
                        <asp:BoundField HeaderText="Status" DataField="ActiveFlag" ItemStyle-Width="5%" HeaderStyle-Width="5%" />
                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="5%" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkUpdate" runat="server" Text="O" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                    CommandName="updt"></asp:LinkButton>
                                |
                                <asp:LinkButton ID="lnkDelete" runat="server" Text="D" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                    CommandName="del" OnClientClick="return confirm('Are you sure want to delete?')"
                                    OnClick="lnkDeleteMessage"></asp:LinkButton>
                                <asp:HiddenField ID="hdnMessgeId" runat="server" Value='<%# Bind("MessageId") %>' />
                                <asp:HiddenField ID="hdnMessageCode" runat="server" Value='<%# Bind("MessageCode") %>' />
                                <asp:HiddenField ID="chkActiveStatus" runat="server" Value='<%# Bind("ActiveFlag") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
