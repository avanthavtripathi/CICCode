<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="TATSummaryForMTO.aspx.cs" Inherits="Reports_TATSummaryForMTO" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <ContentTemplate>
            <table width="100%" border="0">
                <tr>
                    <td class="headingred" style="width: 40%">
                        MTO TAT Report
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" 
                        style="padding-right: 10px;" width="200px">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
             <tr>
             <td colspan="2">
             
                 <table style="width: 100%">
                     <tr>
                         <td align="right" width="40%">
                         Year
                         </td>
                         <td>
                     <asp:DropDownList ID="DdlYear" runat="server" Width="175px" CssClass="simpletxt1">
                     <asp:ListItem Text="2010" Value="2010" ></asp:ListItem>
                     <asp:ListItem Text="2011" Value="2011" ></asp:ListItem>
                     <asp:ListItem Text="2012" Value="2012" ></asp:ListItem>
                     <asp:ListItem Text="2013" Value="2013" ></asp:ListItem>
                     <asp:ListItem Text="2014" Value="2014" ></asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    </tr>
           </table>
             
             </td>
             </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="Search" Width="100"
                            OnClick="btnSearch_Click" />
                        <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn" Text="Export To Execl"
                            Width="100" onclick="btnExportToExcel_Click"  />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvReport" CssClass="simpletxt1" runat="server" RowStyle-CssClass="bgcolorcomm"
                            Width="100%" AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                            GridGroups="both" HorizontalAlign="Left" AutoGenerateColumns="false" >
                            <Columns>
                            <asp:BoundField DataField="Division" HeaderText="Division" />
                            <asp:BoundField DataField="MONTH" HeaderText="Month" />
                            <asp:BoundField DataField="ReceivedCount" HeaderText="No. Of COmplaints" />
                            <asp:BoundField DataField="ClosedCount" HeaderText="Complaints Closed" />
                            <asp:BoundField DataField="FIR1D" HeaderText="FIR within 1-day" />
                            <asp:BoundField DataField="Closed1D" HeaderText="Closed within 1-day" />
                            <asp:BoundField DataField="FIR2_3D" HeaderText="FIR in 2-3 days" />
                            <asp:BoundField DataField="Closed2_3D" HeaderText="Closed in 2-3 days" />
                            <asp:BoundField DataField="FIR4_7D" HeaderText="FIR in 4-7 days" />
                            <asp:BoundField DataField="Closed4_7D" HeaderText="Closed in 4-7 days" />
                            <asp:BoundField DataField="Closed8_15D" HeaderText="Closed in 8-15 days" />
                            <asp:BoundField DataField="Closed16_30D" HeaderText="Closed in 16-30 days" />
                            <asp:BoundField DataField="Closed31_60D" HeaderText="Closed in 31-60 days" />
                            <asp:BoundField DataField="Closed_GT60D" HeaderText="Closed beyond 60 days" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>

