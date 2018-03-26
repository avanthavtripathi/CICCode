<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" 
CodeFile="OutBoundCallCustomerdissatisfcationASCWise.aspx.cs" Inherits="Reports_OutBoundCallCustomerdissatisfcationASC" 
Title="Customer Dissatisfaction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <ContentTemplate>
            <table width="100%" border="0">
                <tr>
                    <td class="headingred" style="width: 40%">
                       OutBound ASC Wise Dissatisfaction Score
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                
                    <td align="right">
                        Region:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlRegion" Width="175" AutoPostBack="true" runat="server" CssClass="simpletxt1"
                            OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Branch:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBranch" Width="175" AutoPostBack="true" runat="server" CssClass="simpletxt1"
                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <%--Added By Binay on 18 Nov for MTO--%>
                <tr>
                    <td align="right">
                        Product Division:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlProductDivision" Width="175" runat="server" CssClass="simpletxt1">
                        </asp:DropDownList>
                    </td>
                </tr>
                
                <tr>
                                <td align="right">
                                    Date From
                                </td>
                                <td align="left">
                                    <asp:TextBox runat="server" ID="txtFromDate" CssClass="txtboxtxt" ValidationGroup="editt"
                                        />
                                        <asp:RequiredFieldValidator ID="reqfromdate" runat ="server" ControlToValidate="txtFromDate"
                                         ErrorMessage ="Enter from date" ValidationGroup ="editt" ></asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CEFrom" runat="server" TargetControlID="txtFromDate">
                                    </cc1:CalendarExtender>
                                    To
                                    <asp:TextBox runat="server" ID="txtToDate" CssClass="txtboxtxt" ValidationGroup="editt"
                                         /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat ="server" ControlToValidate="txtToDate"
                                         ErrorMessage ="Enter to date" ValidationGroup ="editt" ></asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CETo" runat="server" TargetControlID="txtToDate">
                                    </cc1:CalendarExtender>
                                </td>
                            </tr>
                
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="Search" Width="100"
                            OnClick="btnSearch_Click" />
                        <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn" Text="Export To Execl"
                            Width="100" OnClick="btnExport_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left">
                        Total Count:
                        <asp:Label ID="lblCount" ForeColor="Red" runat="server" Text="0"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left">
                       
                        <asp:Label ID="lblFormula" text ="OutboundScore = cases of dissatisfaction / number of cases surveyed" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvMIS" CssClass="simpletxt1" runat="server" RowStyle-CssClass="bgcolorcomm"
                            Width="100%" AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                            GridGroups="both" PagerStyle-HorizontalAlign="Center" AllowSorting="True" AutoGenerateColumns="False"
                            HorizontalAlign="Left" OnPageIndexChanging="gvMIS_PageIndexChanging" OnSorting="gvMIS_Sorting"
                           >
                            <RowStyle CssClass="bgcolorcomm" />
                            <Columns>
                                <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                    <HeaderStyle Width="5%" />
                                </asp:BoundField>
                                <asp:TemplateField  Visible ="false"  HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="SC id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblid" runat="server" Text='<%#Eval("id") %>'>
                                        </asp:Label>                                     
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderStyle-Width="35%"  HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="SC NAME">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSC_NAME" runat="server" Text='<%#Eval("SC_NAME") %>'>
                                        </asp:Label>                                     
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderStyle-Width="25%"  HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="cases of dissatisfaction">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomerComplaintOpenCount" runat="server" Text='<%#Eval("ClosedComplaintCount") %>'>
                                        </asp:Label>                                     
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                  <asp:TemplateField HeaderStyle-Width="20%"  HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="number of cases surveyed">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSureyComplaintCount" runat="server" Text='<%#Eval("ComplaintCount") %>'>
                                        </asp:Label>                                     
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                   <asp:TemplateField HeaderStyle-Width="15%"  HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Outbound Score">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOutboundScore" runat="server" Text='<%#Eval("OutboundScore") %>'>
                                        </asp:Label>                                     
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                            </Columns>
                            <PagerStyle HorizontalAlign="Center" />
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
                        <asp:GridView ID="gvExport" CssClass="simpletxt1" runat="server" AutoGenerateColumns="False"                   >
                            <Columns>
                               <asp:BoundField DataField="SC_NAME"  HeaderText="SC NAME">
                                            
                                        </asp:BoundField>
                                        
                                            <asp:BoundField DataField="ClosedComplaintCount"  HeaderText="cases of dissatisfaction">
                                            
                                        </asp:BoundField>
                                        
                                           <asp:BoundField DataField="ComplaintCount"  HeaderText="number of cases surveyed">
                                            
                                        </asp:BoundField>   
                             
                                        <asp:BoundField DataField="OutboundScore"  HeaderText="Outbound Score">
                                            
                                        </asp:BoundField>
                                
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


