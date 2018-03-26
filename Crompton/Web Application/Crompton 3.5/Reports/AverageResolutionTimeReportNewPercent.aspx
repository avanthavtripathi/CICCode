<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="AverageResolutionTimeReportNewPercent.aspx.cs" Inherits="Reports_AverageResolutionTimeReportNewPercent" 
Title="Average Resolution Time Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
<asp:UpdatePanel ID="updatepnl" runat="server">
<Triggers>
        <asp:PostBackTrigger ControlID="lbtnVerify" />
         <asp:PostBackTrigger ControlID="btnExport" />
</Triggers>
  <ContentTemplate>
    <table width="100%" border="0">
        <tr>
            <td class="headingred">
                MIS-BRM Resolution Time Report (%) (New)</td>
            <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                </asp:UpdateProgress>
            </td>
        </tr>
        <tr><td colspan="2" style="padding-bottom:10px;"> 
            <asp:DropDownList ID="ddlCPIS" AutoPostBack="true" CssClass="simpletxt1" 
                runat="server" onselectedindexchanged="ddlCPIS_SelectedIndexChanged">
            </asp:DropDownList>
        </td></tr>
        
        <tr>
            <td class="bgcolorcomm" colspan="2">
                        <table width="100%" border="0" cellpadding="1" cellspacing="0">
                            <tr>
                                <td>
                                   
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <div id="divReport"  style="width:100%;"  >                                      
                            <asp:GridView ID="gvReport" CssClass="simpletxt1" runat="server" RowStyle-CssClass="bgcolorcomm"
                            HeaderStyle-CssClass="fieldNamewithbgcolor" Width="900px"
                            GridGroups="both" HorizontalAlign="Left" AutoGenerateColumns="False" >
                            <RowStyle CssClass="bgcolorcomm"></RowStyle>
                            <Columns>
                            <asp:BoundField HeaderText="Region" DataField="Region">
                            <HeaderStyle HorizontalAlign="Left" Width="10%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Period" DataField="MonthName">
                                <HeaderStyle HorizontalAlign="Left" Width="20%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Fans %" DataField="fan">
                            <HeaderStyle HorizontalAlign="Right" Width="10%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Pumps %" DataField="pumps">
                            <HeaderStyle HorizontalAlign="Right" Width="10%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" />
                             </asp:BoundField>
                            <asp:BoundField HeaderText="Lighting %" DataField="lighting">
                            <HeaderStyle HorizontalAlign="Right" Width="10%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            <asp:BoundField HeaderText="Appliances %" DataField="Appliances">
                            <HeaderStyle HorizontalAlign="Right" Width="10%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            <asp:BoundField HeaderText="FHP Motors %" DataField="fhpmotors">
                            <HeaderStyle HorizontalAlign="Right" Width="10%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            <asp:BoundField HeaderText="LT Motors %" DataField="ltmotors">
                            <HeaderStyle HorizontalAlign="Right" Width="10%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            
                            </Columns>
                            
                    <HeaderStyle CssClass="fieldNamewithbgcolor"></HeaderStyle>
                            
                            </asp:GridView>
                                    </div>
                                     <div style="text-align:center">
                                     <asp:Button ID="btnExport" runat="server" CssClass="btn" Text="Export To Execl" Width="100" onclick="btnExport_Click" />  
                                     </div>
                                     
                                     <div style="vertical-align:top;padding-right:20px">
                                     <asp:LinkButton ID="lbtnVerify" runat="server" Text="Verify" Visible="false" OnClick="lbtnVerify_Click" /> 
                                    
                          <asp:GridView ID="gvdata" runat="server" GridGroups="both" Visible="false" AutoGenerateColumns="false" >
                            <Columns>
                            <asp:BoundField HeaderText="MonthName" DataField="MonthName" HeaderStyle-Width="150px" />
                            <asp:BoundField HeaderText="Region" DataField="Region_desc" HeaderStyle-Width="150px" />
                            <asp:BoundField HeaderText="Productdivision" DataField="productdivision_desc" HeaderStyle-Width="250px" />
                            <asp:BoundField HeaderText="Closed (0-2/3D)" DataField="Closed" HeaderStyle-Width="150px" />
                            <asp:BoundField HeaderText="Total" DataField="Total" HeaderStyle-Width="150px" />
                            </Columns>
                          </asp:GridView>
                                     </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left"><br />
                                For Fan,Lighting,Appliances,Pump<br />
  1)(Total no of Complaints which are closed in 0 -2 days in Current month / Total number of Complaints in Current Month) * 100<br />
For FHP and LT motors<br />
  2)(Total no of Complaints which are closed in 0 -3 days in Current month / Total number of Complaints in Current Month) * 100<br />
<br />For Cum Complaints<br />
                                    3)(Total no of Complaints which are closed in 0 -2/3 days from Last twelve month to till date / Total number of Complaints from Last twelve month to till date) * 100<br />
<br />*Based on SLA Date<br />
                                </td>
                            </tr>
                      </table>
            </td>
        </tr>
    </table>
    </ContentTemplate></asp:UpdatePanel>
</asp:Content>

