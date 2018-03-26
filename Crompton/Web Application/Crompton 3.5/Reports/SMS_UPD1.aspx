<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="SMS_UPD1.aspx.cs" Inherits="Reports_SMS_UPD1" Title="SMS Status Update Report" %>
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
                         <td align="right" width="40%">
                         Region
                         </td>
                         <td>
                            <asp:DropDownList ID="DdlRegion" runat="server" Width="175px" CssClass="simpletxt1" 
                             OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged"     
                                 AutoPostBack="True">
                        </asp:DropDownList>
                             </td>
                        </tr>
                        <tr>
                         <td align="right" width="40%">
                         Branch
                         </td>
                         <td>
                        <asp:DropDownList ID="DDlBranch" runat="server" Width="175px" CssClass="simpletxt1" 
                        AutoPostBack="True" onselectedindexchanged="ddlBranch_SelectedIndexChanged">
                        <asp:ListItem Text="Select" Value="0" />
                        </asp:DropDownList>
                             </td>
                        </tr>
                        <tr>
                         <td align="right" width="40%">
                             ASC</td>
                         <td>
                        <asp:DropDownList ID="ddlSerContractor" runat="server" Width="175px" CssClass="simpletxt1" >
                        <asp:ListItem Text="Select" Value="0" />
                        </asp:DropDownList>
                            </td>
                        </tr>
                     <tr>
                         <td align="right" width="40%">
                              Status (By SMS)
                         </td>
                         <td>
                        <asp:DropDownList ID="ddlSMSUPDStatus" runat="server" Width="190px" CssClass="simpletxt1" >
                         <asp:ListItem Text="Successful" Value="1" />
                        <asp:ListItem Text="Failed" Value="0" />
                           
                        </asp:DropDownList>
                      </td>
                     </tr>
                     <tr>
                         <td align="right" width="40%">
                              Status Updated
                         </td>
                         <td>
                        <asp:DropDownList ID="ddlStatus" runat="server" Width="190px" CssClass="simpletxt1" >
                        <asp:ListItem Text="All" Value="0" Selected="True" />
                            <asp:ListItem Text="Door Locked (By SMS)" Value="80" />
                            <asp:ListItem Text="Address Not Found (By SMS) " Value="81" />
                            <asp:ListItem Text="Power Outage at Customer site (By SMS) " Value="82" />
                            <asp:ListItem Text="Responsible person not available (By SMS) " Value="83" />
                            <asp:ListItem Text="Closed at Customer‘s Site.(Initilization)" Value="84" />
                            <asp:ListItem Text="Closed at Customer‘s Site.(WIP)" Value="85" />
                            <asp:ListItem Text="Spare Requirement(By SMS-Init)" Value="87" />
                            <asp:ListItem Text="Spare Requirement(By SMS-WIP)" Value="88" />
                        </asp:DropDownList>
                      </td>
                     </tr>
   
                     <tr>
                         <td align="right" width="40%">
                             Date Range</td>
                         <td>
                              <asp:TextBox runat="server" ID="txtFromDate" CssClass="txtboxtxt" ValidationGroup="editt"
                                        MaxLength="10" />
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" Type="Date" Operator="DataTypeCheck"
                                        ControlToValidate="txtFromDate" Display="none" SetFocusOnError="true"></asp:CompareValidator>
                                    <cc1:CalendarExtender ID="CEFrom" runat="server" TargetControlID="txtFromDate">
                                    </cc1:CalendarExtender>
                                    To
                                    <asp:TextBox runat="server" ID="txtToDate" CssClass="txtboxtxt" ValidationGroup="editt"
                                        MaxLength="10" />
                                    <asp:CompareValidator ID="CompareValidator7" runat="server" Type="Date" Operator="DataTypeCheck"
                                        ControlToValidate="txtToDate" Display="none" SetFocusOnError="true"></asp:CompareValidator>
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
                            Width="100" onclick="btnSearch_Click"
                             />
                        <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn" Text="Export To Execl"
                            Width="100" onclick="btnExport_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    <div id="grid">
                    <span>
                      </span>
                        <asp:GridView ID="gvReport" CssClass="simpletxt1" runat="server" RowStyle-CssClass="bgcolorcomm"
                            Width="100%" AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                            GridGroups="both" HorizontalAlign="Left" AutoGenerateColumns="false" 
                            PageSize="100" AllowPaging="true" 
                            onpageindexchanging="gvReport_PageIndexChanging" >
                            <Columns>
                                <asp:TemplateField HeaderText="SNo">
                                <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                </asp:TemplateField>
                              <asp:TemplateField HeaderText="ComplaintNo" >
                            <ItemTemplate>
                              <a href="Javascript:void(0);" onclick="funCommonPopUp(<%#Eval("BaseLineId")%>)" >
                            <%#Eval("Complaint_RefNo")%>   </a>
                             
                            </ItemTemplate>
                            </asp:TemplateField>
                             <asp:BoundField HeaderText="SC Name" DataField="SC_Name" />
                             <asp:BoundField HeaderText="ServiceEng" DataField="ServiceEng_Name" />
                             <asp:BoundField HeaderText="SEMobileNo" DataField="MessageFrom" />
                             <asp:BoundField HeaderText="ProductDivision" DataField="ProductDivision_Desc" />
                             <asp:BoundField HeaderText="SMSDate" DataField="SMS_Date" />
                             <asp:BoundField HeaderText="SMSText" DataField="SMSText" />
                             <asp:BoundField HeaderText="StatusUpdated" DataField="StageDesc" />
                             <asp:BoundField HeaderText="Result" DataField="StatusMessage" />
                               
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
                  <td colspan="2">
                   <span style="color:Red;">*</span>To fetch records for specific date range use <span style="color:Red;">'Export To Execl' button above</span>
                  </td>
                 </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>

