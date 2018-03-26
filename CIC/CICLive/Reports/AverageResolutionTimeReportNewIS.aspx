<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="AverageResolutionTimeReportNewIS.aspx.cs" Inherits="Reports_AverageResolutionTimeReportNewIS" 
Title="Average Resolution Time Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <table width="100%" border="0">
        <tr>
            <td class="headingred">
                MIS-BRM Average Resolution Time (IS)</td>
            <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                </asp:UpdateProgress>
            </td>
        </tr>
        <tr>
            <td class="bgcolorcomm" colspan="2">
                <asp:UpdatePanel ID="pnl" runat="server" UpdateMode="Conditional">
                <Triggers>
                <asp:PostBackTrigger ControlID="btnExport" />
                </Triggers> 
                    <ContentTemplate>
                        <table width="100%" border="0" cellpadding="1" cellspacing="0" id="ReportPart" runat="server">
                            <tr>
                                <td align="left" >
                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <div id="divReport"  style="width:100%; text-align:center"  >                                      
                                         <asp:GridView ID="gvReport" CssClass="simpletxt1" runat="server" RowStyle-CssClass="bgcolorcomm"
                            HeaderStyle-CssClass="fieldNamewithbgcolor" Width="900px"
                            GridGroups="both" HorizontalAlign="Left" AutoGenerateColumns="false" >
                            <Columns>
                            <asp:BoundField HeaderText="Region" DataField="Region" HeaderStyle-Width="150px" >
                            <HeaderStyle Width="150px"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Period" DataField="Period" HeaderStyle-Width="250px" >
                            <HeaderStyle Width="250px"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="LT Motors" DataField="LTMotors" HeaderStyle-Width="150px" >
                            <HeaderStyle Width="150px"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="FHP Motors" DataField="FHPMotors" HeaderStyle-Width="150px" >
                            <HeaderStyle Width="150px"></HeaderStyle>
                            </asp:BoundField>
                             <asp:BoundField HeaderText="IS" DataField="IS" HeaderStyle-Width="150px" >
                            <HeaderStyle Width="150px"></HeaderStyle>
                            </asp:BoundField>
                           </Columns>
                            <HeaderStyle CssClass="fieldNamewithbgcolor"></HeaderStyle>
                           </asp:GridView>
                                    </div>
                                      <div style="text-align:center">
                                     <asp:Button ID="btnExport" runat="server" CssClass="btn" Text="Export To Execl" Width="100" onclick="btnExport_Click" />
                                     </div>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td align="left"><br /><br />
1)Total Time taken to Closed Complaints in Current Month/ Total no. Of Closed Complaint in Current Month.<br />
 
<br />For Cum Complaints<br />
2)Total Time taken to Closed Complaints from (complaints of last 12 months) /Total no. Of Closed Complaint (complaints of Last twelve months.)<br />
<br />*Based on SLA date
                                </td>
                            </tr>
                        </table>
                        <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                    </ContentTemplate>
                 </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>

