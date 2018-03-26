<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="AverageResolutionTimeReportNew.aspx.cs" Inherits="Reports_AverageResolutionTimeReportNew" 
Title="Average Resolution Time Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
<asp:UpdatePanel ID="updatepnl" runat="server">
<Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
  <ContentTemplate>
    <table width="100%" border="0">
        <tr>
            <td class="headingred">
                MIS-BRM Average Resolution Time Report (New)</td>
            <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                </asp:UpdateProgress>
            </td>
        </tr>
        <tr><td colspan="2" style="padding-bottom:10px;"> Select Report Type
            <asp:DropDownList ID="ddlCPIS" AutoPostBack="true" CssClass="simpletxt1" 
                runat="server" onselectedindexchanged="ddlCPIS_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        </tr>
        <tr>
            <td class="bgcolorcomm" colspan="2">
               <table width="100%" border="0" cellpadding="1" cellspacing="0">
                            <tr>
                                <td align="left" >
                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <div id="divReport"  style="width:100%;"  >                                      
                                         <asp:GridView ID="gvReport" CssClass="simpletxt1" runat="server" RowStyle-CssClass="bgcolorcomm"
                            HeaderStyle-CssClass="fieldNamewithbgcolor" Width="900px"
                            GridGroups="both" HorizontalAlign="Left" AutoGenerateColumns="False" >
                                             <RowStyle CssClass="bgcolorcomm" />
                            <Columns>
                            <asp:BoundField HeaderText="Region" DataField="Region" >
                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                </asp:BoundField>
                            <asp:BoundField HeaderText="Period" DataField="MonthName">
                                <HeaderStyle HorizontalAlign="Left" Width="20%"/>
                                </asp:BoundField>
                            <asp:BoundField HeaderText="Fans" DataField="fan" >
                                <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            <asp:BoundField HeaderText="Pumps" DataField="pumps" >
                                <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            <asp:BoundField HeaderText="Lighting" DataField="lighting">
                                <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            <asp:BoundField HeaderText="Appliances" DataField="Appliances">
                                <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            <asp:BoundField HeaderText="FHP Motors" DataField="fhpmotors">
                                <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            <asp:BoundField HeaderText="LT Motors" DataField="ltmotors">
                                <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            </Columns>
                          <HeaderStyle CssClass="fieldNamewithbgcolor" />
                            
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
            </td>
        </tr>
    </table>
  </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

