<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="ComplaintResolutionReport.aspx.cs" Inherits="Reports_ComplaintResolutionReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
    <asp:UpdatePanel runat="server" >
<Triggers>
    <asp:PostBackTrigger ControlID="btnExport" />
</Triggers>
<ContentTemplate>
<table width="100%">
 <tr>
                    <td class="headingred">
                                                Complaint resolution Report
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
                  <%--                   <asp:RequiredFieldValidator ID="rfvregion" runat="server" 
                                        ValidationGroup="editt" ControlToValidate="ddlRegion" InitialValue="Select" 
                                        Text="*" Display="Dynamic" SetFocusOnError="True" />--%>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Branch
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlBranch" runat="server" Width="175px" CssClass="simpletxt1"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" ValidationGroup="editt">
                                         <asp:ListItem Value="0">Select</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Product Divison<font color="red">*</font>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlProductDivison" runat="server" Width="175px" CssClass="simpletxt1"
                                        ValidationGroup="editt" AutoPostBack="True" OnSelectedIndexChanged="ddlProductDivison_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvdiv" runat="server" ValidationGroup="editt" 
                                        ControlToValidate="ddlProductDivison" InitialValue="0" Text="*" 
                                        Display="Dynamic" SetFocusOnError="True" />
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Product Line
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlProductLine" runat="server" Width="350px" CssClass="simpletxt1"
                                        ValidationGroup="editt">
                                       <asp:ListItem Value="0">Select</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                             <tr>
                                    <td width="30%" align="right">
                                        Service Contractor
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlSerContractor" runat="server" Width="225px" CssClass="simpletxt1"
                                            ValidationGroup="editt">
                                          <asp:ListItem Value="0">Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                <td width="30%" align="right">
                                    Complaint Stage
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlCallStage" runat="server" CssClass="simpletxt1" Width="175px"
                                       ValidationGroup="editt">
                                       <asp:ListItem>Closure</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                          
                           
                            
                            <tr>
                                <td width="30%" align="right">
                                    Complaint Status
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlCallStatus" runat="server" CssClass="simpletxt1" Width="350px"
                                        ValidationGroup="editt">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="14">Resolved by Replacement</asp:ListItem>
                                        <asp:ListItem Value="15">Resolved by Repair</asp:ListItem>
                                        <asp:ListItem Value="28">Resolved by repair with replacement of spare</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            
                                  
                                          <tr>
                                            <td align="right">
                                                Warranty Status
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlWarrantyStatus" runat="server" CssClass="simpletxt1" 
                                                    Width="175">
                                                    <asp:ListItem Value="">All</asp:ListItem>
                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                    <asp:ListItem Value="Y" Selected="True">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                     <tr>
        <td align="right">
            <br />
           
        </td>
        <td align="left">
            <br /> <asp:Button ID="btnSearch" runat="server" CausesValidation="true" 
                CssClass="btn" OnClick="btnSearch_Click" Text="Search" ValidationGroup="editt" 
                Width="70px" />
            <asp:Button ID="btnExport" runat="server" CssClass="btn" Text="Export" Visible="false" Width="70px" onclick="btnExport_Click" />
        </td>
    </tr>  
    <tr>
    <td colspan="2">
    <div class="tableBGcolor" align="left"> The Report is LastUpdated for <%= DateTime.Today.AddDays(-1).ToShortDateString() %>
    </div>
    <asp:GridView ID="gvMIS" CssClass="simpletxt1" runat="server" RowStyle-CssClass="bgcolorcomm"
                            Width="100%" AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                            GridGroups="both" AllowPaging="false" PagerStyle-HorizontalAlign="Center" 
                            HorizontalAlign="Left" 
        onrowdatabound="gvMIS_RowDataBound" >
                          <Columns>
                          <asp:BoundField DataField="Status" HeaderText ="Status"  ItemStyle-Font-Bold="true" />
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
    </td>
    </tr> 
                          
</table>
</td>
</tr>
    
    
</table>


</ContentTemplate>
</asp:UpdatePanel>



</asp:Content>

