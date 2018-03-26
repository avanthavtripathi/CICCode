<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DestructionOfDefectiveSparesASC.aspx.cs"
    Inherits="SIMS_Pages_DestructionOfDefectiveSpares_ASC" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%" cellpadding="2" cellspacing="0">
                <tr>
                    <td class="headingred">
                        Destruction Of Defective Spares At ASC
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["SIMSAjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" colspan="2">
                        <table width="100%" border="0">
                            <tr>
                                <td width="100%" align="left" class="bgcolorcomm">
                                    <table border="0" style="width: 100%">
                                        <tr>
                                            <td colspan="2" align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                                <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="6%">
                                                ASC:
                                            </td>
                                            <td>
                                                <asp:Label ID="lblASCName" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Division:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlDivision" runat="server" CssClass="simpletxt1" Width="133px"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="gvChallanDetail" runat="server" AllowPaging="True" AllowSorting="True"
                                                    AlternatingRowStyle-CssClass="fieldName" AutoGenerateColumns="False" EnableSortingAndPagingCallbacks="True"
                                                    GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor" HorizontalAlign="Left"
                                                    PageSize="10" RowStyle-CssClass="gridbgcolor" Width="100%" OnPageIndexChanging="gvChallanDetail_PageIndexChanging">
                                                    <RowStyle CssClass="gridbgcolor" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sno"  ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Complaint No." ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcomplaint" runat="server" Text='<%#Eval("complaint_no") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="spare" HeaderText="Spare" ItemStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" Width="210px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="defreturnqty" HeaderText="Qty" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Challan_No" HeaderText="Challan No" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Claim No." Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldefid" runat="server" Text='<%#Eval("Def_id") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Destroy" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chk" runat="server" AutoPostBack="True" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                                    <img alt="" src='<%=ConfigurationManager.AppSettings["SIMSUserMessage"]%>' />
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
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="100%" colspan="2">
                                                <table align="center">
                                                    <tr>
                                                        <td width="100%">
                                                            <asp:Button ID="imgBtnConfirm" runat="server" CssClass="btn" OnClick="imgBtnConfirm_Click"
                                                                Text="Confirm" Width="78px" />
                                                            <asp:Button ID="ImgBtnCancel" runat="server" CssClass="btn" OnClick="ImgBtnCancel_Click"
                                                                Text="Cancel" Width="74px" />
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
