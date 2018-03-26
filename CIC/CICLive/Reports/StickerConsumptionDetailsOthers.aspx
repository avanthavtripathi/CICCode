<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="StickerConsumptionDetailsOthers.aspx.cs" Inherits="Reports_StickerConsumptionDetailsOthers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="pnlInner" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        <div style="width: 80%; float: left;">
                            Sticker Consumption Details
                        </div>
                        <div align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;
                            width: 10%; float: left;">
                            <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                <ProgressTemplate>
                                    <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                        <div style="clear: both;">
                        </div>
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr id="trRegionDetails" runat="server">
                    <td style="width: 10%;" align="right">
                        Region :
                    </td>
                    <td style="width: 20%;">
                        <asp:DropDownList ID="ddlRegion" runat="server" CssClass="simpletxt1" 
                            AutoPostBack="true" onselectedindexchanged="ddlRegion_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 10%;" align="right">
                        Branch :
                    </td>
                    <td style="width: 20%;">
                        <asp:DropDownList ID="ddlBranches" runat="server" CssClass="simpletxt1" 
                            AutoPostBack="true" onselectedindexchanged="ddlBranches_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>                    
                </tr>
                <tr id="trAscDetails" runat="server">
                    <td style="width: 10%;" align="right">
                        Service Contractor :
                    </td>
                    <td colspan="3">
                        <asp:DropDownList ID="ddlAsc" runat="server" CssClass="simpletxt1" Width="30%">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%;" align="right">
                        Division :
                    </td>
                    <td style="width: 20%;">
                        <asp:DropDownList ID="ddlProductDivision" runat="server" CssClass="simpletxt1" Width="200px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 10%;" align="right">
                        Status :
                    </td>
                    <td style="width: 20%;">
                        <asp:DropDownList ID="ddlActiveStatus" runat="server" CssClass="simpletxt1">
                            <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                            <asp:ListItem Text="In-Active" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Both" Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%;" align="right">
                        Complaint No :
                    </td>
                    <td style="width: 20%;">
                        <asp:TextBox ID="txtComplaintRefNo" runat="server" CssClass="simpletxt1"></asp:TextBox>
                    </td>
                    <td style="width: 10%;" align="right">
                        Sticker Code :
                    </td>
                    <td style="width: 20%;">
                        <asp:TextBox ID="txtStickerCode" runat="server" CssClass="simpletxt1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%;" align="right">
                        Is Consumed :
                    </td>
                    <td style="width: 20%;">
                        <asp:DropDownList ID="ddlConsumptionStatus" runat="server" CssClass="simpletxt1"
                            Width="150px">
                            <asp:ListItem Selected="True" Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Both" Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 10%;">
                    </td>
                    <td style="width: 20%;">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn" OnClick="btnSearch_Click"
                            Text="Search" />
                        &nbsp;<asp:Button ID="btnClear" runat="server" CssClass="btn" OnClick="btnClear_Click"
                            Text="Reset" />
                    </td>
                </tr>
            </table>
            <table style="width: 100%" class="bgcolorcomm">
                <tr>
                    <td>
                        <div style="float: left; width: 25%;">
                            Total Count :
                            <asp:Label ID="lblCount" Text="0" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                        <div style="float: left; width: 58%;">
                            <asp:Label ID="lblUpdtErrorMessage" Text="" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                        <div style="float: right; width: 8%;">
                            <asp:Button ID="lnkDownload" runat="server" Text="Download" OnClick="lnkDownload_Click"
                                CssClass="btn"></asp:Button>
                        </div>
                        <div style="clear: both;">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                            HeaderStyle-CssClass="fieldNamewithbgcolor" PagerStyle-HorizontalAlign="Center"
                            AutoGenerateColumns="False" ID="gvStickerDetails" runat="server" Width="100%"
                            HorizontalAlign="Left" OnSorting="gvComm_Sorting" AllowSorting="true">
                            <Columns>
                                <asp:TemplateField HeaderText="SNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSno" runat="server" Text='<%# Eval ("Rn") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Sticker No" DataField="StickerDesc" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" SortExpression="StickerDesc"></asp:BoundField>
                                <asp:BoundField HeaderText="ComplaintNo" DataField="Complaintrefno" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" SortExpression="Complaintrefno"></asp:BoundField>
                                <asp:BoundField HeaderText="Region" DataField="Region_Desc" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" SortExpression="Region_Desc"></asp:BoundField>
                                <asp:BoundField HeaderText="Branch" DataField="Branch_Name" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" SortExpression="Branch_Name"></asp:BoundField>
                                <asp:BoundField HeaderText="Product Divison" DataField="Unit_Desc" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" SortExpression="Unit_Desc"></asp:BoundField>
                                <asp:BoundField HeaderText="Allocated By" DataField="AllocatedByName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" SortExpression="AllocatedByName"></asp:BoundField>
                                <asp:BoundField HeaderText="Allocated To" DataField="AllocatedToName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" SortExpression="AllocatedToName"></asp:BoundField>
                                <asp:BoundField HeaderText="Active Status" DataField="ActiveStatus" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" SortExpression="ActiveStatus"></asp:BoundField>
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
                <tr>
                <td>
                <asp:DataList CellPadding="5" RepeatDirection="Horizontal" runat="server" ID="dlPager"
                            OnItemCommand="dlPager_ItemCommand">
                            <ItemTemplate>
                                <asp:LinkButton Enabled='<%#Eval("Enabled") %>' runat="server" ID="lnkPageNo" Text='<%#Eval("Text") %>'
                                    CommandArgument='<%#Eval("Value") %>' CommandName="PageNo"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:DataList>
                </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkDownload" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
