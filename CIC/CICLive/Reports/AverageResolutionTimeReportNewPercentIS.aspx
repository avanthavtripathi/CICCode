<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="AverageResolutionTimeReportNewPercentIS.aspx.cs" Inherits="Reports_AverageResolutionTimeReportPercentIS" 
Title="Average Resolution Time Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <table width="100%" border="0">
        <tr>
            <td class="headingred">
                MIS-BRM Resolution Time Report IS(%)</td>
            <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                </asp:UpdateProgress>
            </td>
        </tr>
        <tr>
            <td class="bgcolorcomm" colspan="2">
                        <table width="100%" border="0" cellpadding="1" cellspacing="0" id="ReportPart" runat="server">
                            <tr>
                                <td colspan="3">
                                   
                                </td>
                            </tr>
                            <tr>
                              <td colspan="3">
                                 <div id="divReport"  style="width:100%;text-align:center" align="center" >                                      
                            <asp:GridView ID="gvReport" CssClass="simpletxt1" runat="server" RowStyle-CssClass="bgcolorcomm"
                            HeaderStyle-CssClass="fieldNamewithbgcolor" Width="900px"
                            GridGroups="both" HorizontalAlign="Left" AutoGenerateColumns="false" >
                            <RowStyle CssClass="bgcolorcomm"></RowStyle>
                            <Columns>
                                <asp:BoundField HeaderText="Region" DataField="Region" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Justify" >
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Period" DataField="Period" HeaderStyle-Width="250px" ItemStyle-HorizontalAlign="Justify"  >
                                </asp:BoundField>
                                <asp:BoundField HeaderText="FHP Motors %" DataField="fhp motors" HeaderStyle-Width="200px" />
                                <asp:BoundField HeaderText="LT Motors %" DataField="lt motors" HeaderStyle-Width="200px" />
                                <asp:BoundField HeaderText="IS %" DataField="IS" HeaderStyle-Width="200px" />
                            </Columns>
                            <HeaderStyle CssClass="fieldNamewithbgcolor"></HeaderStyle>
                            </asp:GridView>
                                 </div>
                                 <div style="text-align:center">
                                     <asp:Button ID="btnExport" runat="server" CssClass="btn" Text="Export To Execl" Width="100" onclick="btnExport_Click" />
                                 </div>
                                </td>
                            </tr>
                      </table>
                      <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

