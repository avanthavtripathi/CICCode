<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" 
CodeFile="AverageResolutionTimeReportforMTO.aspx.cs" Inherits="Reports_AverageResolutionTimeReport" 
Title="Average Resolution Time Report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <table width="100%" border="0">
        <tr>
            <td class="headingred">
               MTO-BRM % Average Resolution Time Report</td>
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
                        <table width="100%" border="0" cellpadding="1" cellspacing="0">
                            <tr>
                                <td align="left" >
                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <div id="divReport"  style="width:100%;overflow:scroll"  >                                      
                                         <asp:GridView ID="gvReport" CssClass="simpletxt1" runat="server" RowStyle-CssClass="bgcolorcomm"
                            Width="100%" AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                            GridGroups="both" HorizontalAlign="Left" ></asp:GridView>
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

                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                 </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>

