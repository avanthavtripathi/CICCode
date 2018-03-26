<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="ASCDivisions.aspx.cs" Inherits="Reports_ASCDivisions" Title="ASC Division-wise Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<Triggers>
<asp:PostBackTrigger ControlID="btnExport" />
</Triggers> 
          <ContentTemplate>
            <table width="100%" border="0">
                <tr>
                    <td class="headingred" style="width: 40%">
                        ASC Division-wise Report
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
             
                 
             
             </td>
             </tr>
               <tr>
                    <td colspan="2">
                       <div style="text-align:center">
                                     <asp:Button ID="btnExport" runat="server" CssClass="btn" Text="Export To Execl" Width="100" onclick="btnExport_Click" />
                                     </div>

                    <div style="width:1010px;" class="fieldNamewithbgcolor">
                    <table style="width:1010px;">
                    <tr>
                    <td style="width:100px;">Region</td>
                    <td style="width:200px;">Branch</td>
                    <td style="width:250px;">SC Name</td>
                    <td style="width:60px;">LT Motors</td>
                    <td style="width:60px;">FHP Motors</td>
            
                    <td style="width:100px;">Created Date </td>
                    </tr>
                    </table>
                    </div>
                       <div id="dvdash" runat="server" style="position:relative;height:450px;overflow:auto;" >
                        <asp:GridView ID="gvReport" CssClass="simpletxt1" runat="server" RowStyle-CssClass="bgcolorcomm"
                            Width="1010px" AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                            GridGroups="both" HorizontalAlign="Left" AutoGenerateColumns="false" onrowdatabound="gvReport_RowDataBound" >
                            <Columns>
                            <asp:BoundField DataField="Region" HeaderText="Region" ItemStyle-Width="100px" />
                            <asp:BoundField DataField="Branch"  HeaderText="Branch"  ItemStyle-Width="200px" />
                            <asp:BoundField DataField="SC Name" HeaderText="SC Name"  ItemStyle-Width="250px" />
                            <asp:BoundField DataField="LT Motors" HeaderText="LT Motors"  ItemStyle-Width="60px" />
                            <asp:BoundField DataField="FHP Motors" HeaderText="FHP Motors" ItemStyle-Width="60px" />
                            <asp:BoundField DataField="Created Date" HeaderText="Created Date" ItemStyle-Width="100px" />
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
                       </div>
                        
                             </td>
                       </tr>
                       <tr>
                    <td  colspan="2">
                    <div class="headingred"> Summary</div>
                           <asp:GridView ID="GridView1" CssClass="simpletxt1" runat="server" RowStyle-CssClass="bgcolorcomm"
                            Width="100%" AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                            GridGroups="both" HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" >
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

