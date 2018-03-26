<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="SMS_UPD2.aspx.cs" Inherits="Reports_SMS_UPD2" Title="SMS Status Update Report" EnableEventValidation="false"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <ContentTemplate>
            <table width="100%" border="0">
                <tr>
                    <td class="headingred" style="width: 40%">
                        SMS Status Update Report
                    </td>
                    <td align="right" style="padding-right:10px;" width="500px">
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
                         <td align="right">
                             Region:
                         </td>
                         <td>
                             <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="true" 
                                 CssClass="simpletxt1" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" 
                                 Width="175">
                             </asp:DropDownList>
                             <asp:RequiredFieldValidator ID="rfvr" runat="server" Text="*"
                                 ControlToValidate="ddlRegion" Display="Dynamic" InitialValue="0" SetFocusOnError="true" />
                         </td>
                     <tr>
                         <td align="right">
                             Branch:
                         </td>
                         <td>
                             <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="true" 
                                 CssClass="simpletxt1" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" 
                                 Width="175">
                             </asp:DropDownList>
                         </td>
                     </tr>
                     <tr>
                         <td align="right">
                             Service Contractor:
                         </td>
                         <td>
                             <asp:DropDownList ID="ddlSC" runat="server" CssClass="simpletxt1" Width="175">
                             </asp:DropDownList>
                         </td>
                     </tr>
                       <tr>
                         <td align="right">
                              Status
                         </td>
                         <td>
                        <asp:DropDownList ID="ddlSMSUPDStatus" runat="server" Width="175px" CssClass="simpletxt1" >
                         <asp:ListItem Text="Successful" Value="1" />
                        <asp:ListItem Text="Failed" Value="0" />
                           
                        </asp:DropDownList>
                      </td>
                     </tr>
                     <tr>
                         <td align="right" width="40%">
                             Date Range</td>
                         <td>
                             <asp:TextBox ID="txtFromDate" runat="server" CssClass="txtboxtxt" 
                                 MaxLength="10" ValidationGroup="editt" />
                             <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                 ControlToValidate="txtFromDate" Display="none" Operator="DataTypeCheck" 
                                 SetFocusOnError="true" Type="Date"></asp:CompareValidator>
                             <cc1:CalendarExtender ID="CEFrom" runat="server" TargetControlID="txtFromDate">
                             </cc1:CalendarExtender>
                             To
                             <asp:TextBox ID="txtToDate" runat="server" CssClass="txtboxtxt" MaxLength="10" 
                                 ValidationGroup="editt" />
                             <asp:CompareValidator ID="CompareValidator7" runat="server" 
                                 ControlToValidate="txtToDate" Display="none" Operator="DataTypeCheck" 
                                 SetFocusOnError="true" Type="Date"></asp:CompareValidator>
                             <cc1:CalendarExtender ID="CETo" runat="server" TargetControlID="txtToDate">
                             </cc1:CalendarExtender>
                             <asp:Label ID="lblDateErr" runat="server" ForeColor="Red" Text=""></asp:Label>
                         </td>
                     </tr>
               </table>
             
             </td>
             </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="Search" 
                            Width="100" onclick="btnSearch_Click" />
                        <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn" Text="Export To Execl"
                            Width="100" onclick="btnExport_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
             
                        <asp:GridView ID="gvReport" CssClass="simpletxt1" runat="server" RowStyle-CssClass="bgcolorcomm"
                            Width="98%" AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                            GridGroups="both" HorizontalAlign="Left" AutoGenerateColumns="false" 
                            AllowPaging="True" EnableSortingAndPagingCallbacks="True" 
                            onpageindexchanging="gvReport_PageIndexChanging" >
                            <RowStyle CssClass="bgcolorcomm" />
                            <Columns>
                                <asp:TemplateField HeaderText="SNo">
                                <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Region" DataField="Region_Desc" />
                                <asp:BoundField HeaderText="Branch" DataField="Branch_Name" />
                                <asp:BoundField HeaderText="SC Name" DataField="SC_Name" />
                                <asp:BoundField HeaderText="ServiceEng" DataField="ServiceEng_Name" />
                                <asp:BoundField HeaderText="ComplaintRefNo" DataField="Complaint_RefNo" />
                                <asp:BoundField HeaderText="MobileNo" DataField="MessageFrom" />
                                <asp:BoundField HeaderText="SMSDate" DataField="SMS_Date" />
                                <asp:BoundField HeaderText="Status" DataField="Status" />
                                							
                            
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
                            <HeaderStyle CssClass="fieldNamewithbgcolor" />
                            <AlternatingRowStyle CssClass="fieldName" />
                        </asp:GridView>
                        
                      
                    </td>
                 </tr>
                 <tr>
                 <td colspan="2">
                   <br /><b>Summary</b><br />
                        <div>
                        <asp:GridView ID="GvSummary" CssClass="simpletxt1" runat="server" RowStyle-CssClass="bgcolorcomm"
                            Width="100%" AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                            GridGroups="both" HorizontalAlign="Left" AutoGenerateColumns="false" >
                            <Columns>
                                <asp:TemplateField HeaderText="SNo">
                                <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Region" DataField="Region_Desc" />
                                <asp:BoundField HeaderText="Branch" DataField="Branch_Name" />
                                <asp:BoundField HeaderText="SC Name" DataField="SC_Name" />
                                <asp:BoundField HeaderText="ServiceEng" DataField="ServiceEng_Name" />
                                <asp:BoundField HeaderText="Total SMS Sent " DataField="Count" />
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
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>

