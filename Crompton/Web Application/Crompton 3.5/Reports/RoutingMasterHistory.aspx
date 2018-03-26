<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="RoutingMasterHistory.aspx.cs" Inherits="Reports_RoutingMasterHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="ContentStateMaster" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Routing Master History
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="updatepnl" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
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
                                    <asp:DropDownList ID="ddlSearch" ValidationGroup="search" runat="server" Width="130px"
                                        CssClass="simpletxt1">
                                        <asp:ListItem Text="SC. Name" Value="SC_Name"></asp:ListItem>
                                        <asp:ListItem Text="Product Division" Value="Unit_Desc"></asp:ListItem>
                                    </asp:DropDownList>
                                    With
                                    <asp:TextBox ID="txtSearch" ValidationGroup="search" runat="server" CssClass="txtboxtxt"
                                        Width="100px" Text="" TabIndex="1"></asp:TextBox>
                                    <asp:Button Text="Go" Width="25px" CssClass="btn" ValidationGroup="search" ID="imgBtnGo"
                                        runat="server" CausesValidation="False" OnClick="imgBtnGo_Click" TabIndex="3" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td align="right">
                                    From
                                    <asp:TextBox ID="txtFrom" runat="server" CssClass="txtboxtxt" Width="100px"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CEFrom" runat="server" TargetControlID="txtFrom">
                                    </cc1:CalendarExtender>
                                    &nbsp;To
                                    <asp:TextBox ID="txtTo" runat="server" CssClass="txtboxtxt" Width="100px">
                                    </asp:TextBox>
                                    <cc1:CalendarExtender ID="CETo" runat="server" TargetControlID="txtTo">
                                    </cc1:CalendarExtender>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0">
                            <tr>
                                <td class="bgcolorcomm">
                                    <asp:GridView PageSize="50" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="true"
                                        AllowSorting="True" DataKeykNames="Routing_Sno" AutoGenerateColumns="False" ID="gvComm"
                                        runat="server" OnPageIndexChanging="gvComm_PageIndexChanging" Width="100%" HorizontalAlign="Left"
                                        OnSorting="gvComm_Sorting">
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sno" HeaderStyle-Width="40" ItemStyle-Width="40">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex+1%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Region_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Region" SortExpression="Region_Desc" HeaderStyle-Width="60px" ItemStyle-Width="60px" />
                                            <asp:BoundField DataField="Branch_Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Branch" SortExpression="Branch_Name" />
                                            <asp:BoundField DataField="Unit_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Product Division" SortExpression="Unit_Desc" />
                                            <asp:BoundField DataField="SC_Name" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="ASC Name" SortExpression="SC_Name" />
                                            <asp:BoundField DataField="Modifiedby" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Modified By" SortExpression="Modifiedby" />
                                            <asp:BoundField DataField="ModifiedDate" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Modified Date" SortExpression="ModifiedDate" />
                                            <asp:BoundField DataField="Active_Flag" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Status" />
                                            <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                HeaderText="Details">
                                                <ItemTemplate>
                                                    <a href="Javascript:void(0)" onclick="window.open('../Admin/RoutingComplaintPopup.aspx?aHtI='+<%# Eval("createId") %>,'','resizable=yes,menubar=no,scrollbars=yes,top=100,left=10,toolbar=no');">
                                                        Details</a>
                                                </ItemTemplate>
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
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
