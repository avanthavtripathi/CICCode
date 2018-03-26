<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="SCTerritoryDivision.aspx.cs" Inherits="Reports_SCTerritoryDivision"
     EnableEventValidation="false" %>
    
<asp:Content ID="PendingSerRegReport" ContentPlaceHolderID="MainConHolder" runat="Server">
   <asp:UpdatePanel ID="updatepnl" runat="server">
    <Triggers>
    <asp:PostBackTrigger ControlID="btnExportToExcel" />
    </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                       SC Territory Division Report
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" colspan="2">
                        <table width="98%" border="0">
                            <tr>
                                <td width="30%" align="right">
                                    Region
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlRegion" runat="server" Width="175px" CssClass="simpletxt1"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Branch
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlBranch" runat="server" Width="175px" CssClass="simpletxt1"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                             <tr>
                                <td width="30%" align="right">
                                    Service Contractor
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlSerContractor" runat="server" Width="225px" CssClass="simpletxt1"
                                       AutoPostBack="true" onselectedindexchanged="ddlSerContractor_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>                            
                             <tr><td width="30%" align="right">Product Divison</td>
                             <td align="left"><asp:CheckBoxList ID="checkLProductDivision" RepeatDirection="Horizontal" runat="server" CssClass="simpletxt1"></asp:CheckBoxList> </td></tr>                  
                            <tr>
                                <td align="right"></td>
                                <td align="left"><br />
                                     <asp:Button Width="70px" Text="Search" CssClass="btn"  ValidationGroup="editt" CausesValidation="true"
                                        ID="btnSearch" runat="server" OnClick="btnSearch_Click" /> &nbsp; 
                                    <asp:Button Width="80px" Text="Export to Excel" CssClass="btn"  
                                        ValidationGroup="editt" CausesValidation="true"
                                        ID="btnExportToExcel" Visible="false" runat="server" onclick="btnExportToExcel_Click"  />
                                </td>
                            </tr>
                            
                            <tr>
                                   
                               <td align="left" class="MsgTDCount">
                                Total Number of Records : <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                               </td>
                               <td>
                                 <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                                                                runat="server" />
                               </td>
                            </tr>
                           
                        </table>
                        <br />                    
                        <table width="100%" border="0">
                            <tr>
                                <td>
                                    <!-- Report Listing -->
                                    <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="True" PagerStyle-HorizontalAlign="Center" 
                                        AllowSorting="True" AutoGenerateColumns="False" ID="gvComm" 
                                        runat="server" OnPageIndexChanging="gvComm_PageIndexChanging" Width="100%" 
                                        HorizontalAlign="Left"  Visible="true" OnSorting="gvComm_Sorting">
                                        <Columns>
                                            <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                <HeaderStyle Width="40px" />
                                            </asp:BoundField> 
                                            <asp:BoundField DataField="SC_Name" SortExpression="SC_Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Service Contractor">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Unit_Desc" SortExpression="Unit_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Product Division">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>   
                                            <asp:BoundField DataField="Region_Desc" SortExpression="Region_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Region">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField> 
                                             <asp:BoundField DataField="Branch_Name" SortExpression="Branch_Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Branch">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField> 
                                             <asp:BoundField DataField="City_Desc" SortExpression="City_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="City">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>     
                                             <asp:BoundField DataField="Territory_Desc" SortExpression="Territory_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Territory">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>                                         
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                    <img src="<%=ConfigurationManager.AppSettings["UserMessage"]%>" alt="" />    <b>No Record found</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                    <!-- End Report Listing -->
                                </td>
                            </tr>
                           
                            
                            <tr>
                                <td align="center">
                                    <asp:GridView ID="gvExport" AutoGenerateColumns="false" runat="server">
                                    <Columns>
                                             <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                <HeaderStyle Width="40px" />
                                            </asp:BoundField> 
                                              <asp:BoundField DataField="SC_Name" SortExpression="SC_Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Service Contractor">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Unit_Desc" SortExpression="Unit_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Product Division">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>   
                                            <asp:BoundField DataField="Region_Desc" SortExpression="Region_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Region">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField> 
                                             <asp:BoundField DataField="Branch_Name" SortExpression="Branch_Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Branch">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField> 
                                             <asp:BoundField DataField="City_Desc" SortExpression="City_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="City">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>     
                                             <asp:BoundField DataField="Territory_Desc" SortExpression="Territory_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Territory">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>                                 
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
