<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" 
CodeFile="ResolutionTimeReportforMTO.aspx.cs" Inherits="Reports_ResolutionTimeReport" Title="Resolution Time Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <table width="100%" border="0">
        <tr>
            <td class="headingred">
               MTO-BRM Resolution Time Report 0-3 for all divisions</td>
            <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                </asp:UpdateProgress>
            </td>
        </tr>
        <tr>
            <td class="bgcolorcomm" colspan="2">
                        <table width="100%" border="0" cellpadding="1" cellspacing="0">
                            <tr>
                                <td colspan="3">
                                   
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                     <div id="divReport"  style="width:100%;"  >                                      
                                         <asp:GridView ID="gvReport" CssClass="simpletxt1" runat="server" RowStyle-CssClass="bgcolorcomm"
                            Width="100%" AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                            GridGroups="both" HorizontalAlign="Left" ></asp:GridView>
                                    </div>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="left"><br /><br />
                                Calculation :<br />
   (Total no of Complaints which are closed in 0 -3 days in Current month / Total number of Complaints in Current Month) * 100<br />
 <br />For Cum Complaints<br />
                                    (Total no of Complaints which are closed in 0 -3 days from Last twelve month to till date / Total number of Complaints from Last twelve month to till date) * 100<br /><br />

                                </td>
                            </tr>
                      </table>
            </td>
        </tr>
    </table>
</asp:Content>

