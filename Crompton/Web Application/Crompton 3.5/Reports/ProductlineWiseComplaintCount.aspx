<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" 
CodeFile="ProductlineWiseComplaintCount.aspx.cs" Inherits="ReportProductlineWiseComplaintCount" Title="Product Line Wise Complaint Count" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
 
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <ContentTemplate>
            <table width="100%" border="0">
                <tr>
                    <td class="headingred" style="width: 40%">
                        Product line wise service complaints count
                        <%--Branch Wise Division Wise Contractor Wise Pendency Report--%>
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                <td align="center" colspan="2">
                    <asp:RadioButtonList ID="rdbtnSelection" runat="server" AutoPostBack="True" 
                        RepeatDirection="Horizontal" 
                        onselectedindexchanged="rdbtnSelection_SelectedIndexChanged" >
                        <asp:ListItem Selected="True">Month Wise</asp:ListItem>
                        <asp:ListItem>Financial Year Wise</asp:ListItem>
                    </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Region</td>
                    <td>
                        <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="True" 
                            CssClass="simpletxt1" onselectedindexchanged="ddlRegion_SelectedIndexChanged" 
                            Width="175">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                <td align="right">Branch</td>
                <td><asp:DropDownList ID="ddlBranch" Width="175" runat="server" CssClass="simpletxt1"></asp:DropDownList></td>
                </tr>
                <tr>
                        <td align="right">Division</td>
                        <td>
                            <asp:DropDownList ID="ddlProductDivision" runat="server" CssClass="simpletxt1" Width="175">
                            </asp:DropDownList>
                        </td>
                    </tr>
                <tr id="TrYear" runat="server">
                    <td align="right">
                        Year</td>
                    <td>
                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="simpletxt1" Width="175">
                        </asp:DropDownList>
                    </td></tr>
                    <tr id="TrMonth" runat="server">
                        <td align="right">
                            Month</td>
                        <td>
                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="simpletxt1" 
                                Width="175">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    
                    <tr id="TrFinYear" runat="server" visible="false">
                        <td align="right">
                            Fin. Year
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFiYear" Width="175" runat="server" CssClass="simpletxt1">
                                <asp:ListItem Text="2016-2017" Value="2016"></asp:ListItem>
                                <asp:ListItem Text="2015-2016" Value="2015"></asp:ListItem>
                                <asp:ListItem Text="2014-2015" Value="2014"></asp:ListItem>
                                <asp:ListItem Text="2013-2014" Value="2013"></asp:ListItem>
                                <asp:ListItem Text="2012-2013" Value="2012"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                        </td>
                        <td align="left">
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn" 
                                OnClick="btnSearch_Click" Text="Search" Width="100" />
                            <asp:Button ID="btnExport" runat="server" CssClass="btn" 
                                OnClick="btnExport_Click" Text="Export To Execl" Visible="false" Width="100" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            Total Count:
                            <asp:Label ID="lblCount" runat="server" ForeColor="Red" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gvMIS" runat="server" AllowPaging="True" AllowSorting="True" 
                                AlternatingRowStyle-CssClass="fieldName" AutoGenerateColumns="False" 
                                CssClass="simpletxt1" GridGroups="both" 
                                HeaderStyle-CssClass="fieldNamewithbgcolor" HorizontalAlign="Left" 
                                OnPageIndexChanging="gvMIS_PageIndexChanging"
                                PagerStyle-HorizontalAlign="Center" PageSize="30" 
                                RowStyle-CssClass="bgcolorcomm" Width="100%">
                                <RowStyle CssClass="bgcolorcomm" />
                                <Columns>
                                    <asp:BoundField DataField="Sno" HeaderText="SNo" HeaderStyle-HorizontalAlign="Left">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Months" HeaderText="Month" HeaderStyle-HorizontalAlign="Left">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Region" HeaderText="Region" HeaderStyle-HorizontalAlign="Left">
                                   </asp:BoundField>
                                    <asp:BoundField DataField="Branch" HeaderText="Branch" HeaderStyle-HorizontalAlign="Left">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ProductLineDesc" HeaderText="Product Line" HeaderStyle-HorizontalAlign="Left">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TotalComplaint" HeaderText="No of complaints" HeaderStyle-HorizontalAlign="Left">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TotalComplaintWithWarranty" HeaderText="Complaints With Warranty" HeaderStyle-HorizontalAlign="Left">
                                    </asp:BoundField>
                                </Columns>
                                <PagerStyle HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td align="center" 
                                                style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                <img alt="" src='<%=ConfigurationManager.AppSettings["UserMessage"]%>' /> <b>No 
                                                Record found</b>
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
                            <b>Summary</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="GVSummary" runat="server" AllowPaging="false" 
                                AllowSorting="True" AlternatingRowStyle-CssClass="fieldName" 
                                AutoGenerateColumns="False" CssClass="simpletxt1" GridGroups="both" 
                                HeaderStyle-CssClass="fieldNamewithbgcolor" HorizontalAlign="Left" 
                                OnPageIndexChanging="gvMIS_PageIndexChanging" 
                                PagerStyle-HorizontalAlign="Center" RowStyle-CssClass="bgcolorcomm" 
                                Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-Width="40px" HeaderText="Sno" 
                                        ItemStyle-Width="40px">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField ControlStyle-Width="20%" DataField="ProductLineDesc" 
                                        HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="20%" 
                                        HeaderText="Product Line" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField ControlStyle-Width="37.5%" DataField="NoOfComplaints" 
                                        HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="37.5%" 
                                        HeaderText="No of complaints" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField ControlStyle-Width="37.5%" DataField="ComplaintsWithWarranty" 
                                        HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="37.5%" 
                                        HeaderText="Complaints With Warranty" ItemStyle-HorizontalAlign="Left" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td align="center" 
                                                style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                <img alt="" src='<%=ConfigurationManager.AppSettings["UserMessage"]%>' /> <b>No 
                                                Record found</b>
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                </tr>
            </table>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

