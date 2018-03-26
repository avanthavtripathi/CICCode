<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="MisTrend.aspx.cs" Inherits="Reports_MisTrend" Title="MIS Trend Report" %>
<asp:Content ID="CntMistrend" ContentPlaceHolderID="MainConHolder" Runat="Server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <ContentTemplate>
            <table width="100%" border="0">
                <tr>
                    <td class="headingred" style="width: 40%">
                        Trend Analysis Report
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
                             Product Division
                         </td>
                         <td>
                    <asp:DropDownList ID="ddlUnit" runat="server" Width="175px" CssClass="simpletxt1">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvUnit" runat="server" ErrorMessage="Select Product Division"
                    ControlToValidate="ddlUnit" InitialValue="0" ></asp:RequiredFieldValidator>
                    
                                   </td>
                     </tr>
                     <tr>
                         <td align="right" width="40%">
                         Year
                         </td>
                         <td>
                     <asp:DropDownList ID="DdlYear" runat="server" Width="175px" CssClass="simpletxt1">
                     <asp:ListItem Text="2012" Value="2012" Selected="True" ></asp:ListItem>
                     <asp:ListItem Text="2013" Value="2013" ></asp:ListItem>
                     <asp:ListItem Text="2014" Value="2014" ></asp:ListItem>
                     <asp:ListItem Text="2015" Value="2015" ></asp:ListItem>
                     <asp:ListItem Text="2016" Value="2016" ></asp:ListItem>
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
                            Width="100" onclick="btnExport_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvReport" CssClass="simpletxt1" runat="server" RowStyle-CssClass="bgcolorcomm"
                            Width="100%" AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                            GridGroups="both" HorizontalAlign="Left" >
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
                       <div>*Based on Log Date</div>
                        
                             </td>
                       </tr>
                       <tr>
                    <td  colspan="2">
                    <div class="headingred"> Trend Analysis Summary</div>
                           <asp:GridView ID="GridView1" CssClass="simpletxt1" runat="server" RowStyle-CssClass="bgcolorcomm"
                            Width="100%" AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                            GridGroups="both" HorizontalAlign="Left" >
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



