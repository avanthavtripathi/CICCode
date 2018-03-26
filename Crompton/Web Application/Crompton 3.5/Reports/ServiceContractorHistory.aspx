<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="ServiceContractorHistory.aspx.cs" Inherits="Reports_ServiceContractorHistory"
    Title="Service Contractor History" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="ContentServiceContractor" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Service Contractor History
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
                        <table border="0" width="100%">
                            <tr>
                                <td align="left" class="MsgTDCount">
                                    Total Number of Records :
                                    <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    Search For
                                    <asp:DropDownList ID="ddlSearch" runat="server" Width="130px" CssClass="simpletxt1">
                                        <asp:ListItem Text="SC Code" Value="SC_Username"></asp:ListItem>
                                        <asp:ListItem Text="SC Name" Value="SC_Name"></asp:ListItem>
                                    </asp:DropDownList>
                                    With
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="txtboxtxt" Width="100px" Text=""></asp:TextBox>
                                    <asp:Button Text="Go" Width="25px" CssClass="btn" ID="imgBtnGo" runat="server" CausesValidation="False"
                                        ValidationGroup="editt" OnClick="imgBtnGo_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                
                                </td>
                                <td align="right"> From 
                                <asp:TextBox ID="txtFrom" runat="server" CssClass="txtboxtxt" Width="100px"></asp:TextBox>
                                <cc1:CalendarExtender ID="CEFrom" runat="server" TargetControlID="txtFrom">
                                    </cc1:CalendarExtender>
                                    &nbsp;To
                                    <asp:TextBox ID="txtTo" runat="server" CssClass="txtboxtxt" Width="100px">
                                    </asp:TextBox>
                                    <cc1:CalendarExtender ID="CETo" runat="server" TargetControlID="txtTo">
                                    </cc1:CalendarExtender>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0">
                            <tr>
                                <td>
                                    <!-- Service Contractor Listing   -->
                                    <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridLines="both" AllowPaging="True"
                                        AllowSorting="True" AutoGenerateColumns="False" ID="gvServiceContractor"
                                        runat="server" OnPageIndexChanging="gvServiceContractor_PageIndexChanging" Width="100%"
                                        HorizontalAlign="Left"
                                        EnableSortingAndPagingCallbacks="True" OnSorting="gvServiceContractor_Sorting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sno" HeaderStyle-Width="40px" ItemStyle-Width="40px">
                                            <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Region_Desc" SortExpression="Region_Desc" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Region">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Branch_Name" SortExpression="Branch_Name" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Branch">
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="SC_UserName" SortExpression="SC_UserName" HeaderStyle-Width="60px"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="ASC Code">
                                                <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SC_Name" SortExpression="SC_Name" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="ASC Name">
                                            </asp:BoundField>                                            
                                            <asp:BoundField DataField="ModifiedBy" SortExpression="ModifiedBy" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Modified By">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ModifiedDate" SortExpression="ModifiedDate" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Modification Date">
                                            </asp:BoundField>
                                             <asp:BoundField DataField="Status" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Status" />
                                            <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderText="Details">
                                                <ItemTemplate>
                                                    <a href="Javascript:void(0)" onclick="window.open('../Admin/ServiceContractorModificationPopup.aspx?aHtI='+<%# Eval("createId") %>,'','menubar=no,scrollbars=yes,top=100,left=10,toolbar=no');" >
                                                        Details</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
                                    <!-- End Service Contractor Listing -->
                                </td>
                            </tr>
                            <tr>
                                <td>
                                  <asp:Button Text="Excel DownLoad" CssClass="btn" ID="BtnExportExcel" 
                                        runat="server" CausesValidation="False" onclick="BtnExportExcel_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID ="BtnExportExcel" />
        </Triggers>
    </asp:UpdatePanel>

    <script language="javascript" type="text/javascript">
        
        function validateAddress1(oSrc, args)
        {
            
            var x = (document.getElementById('ctl00_MainConHolder_txtAddOne').value); 
            if(x.length > 100)
            
             {
                args.IsValid = false
             }
             else
             {
           
                args.IsValid = true
             }
           
             
        }
        
        function validateAddress2(oSrc, args)
        {
            
            var x = (document.getElementById('ctl00_MainConHolder_txtAddTwo').value); 
            if(x.length > 100)
            
             {
                args.IsValid = false
             }
             else
             {
                args.IsValid = true
             }
        }
        
    </script>

</asp:Content>
