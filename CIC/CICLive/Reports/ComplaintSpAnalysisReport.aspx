<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="ComplaintSpAnalysisReport.aspx.cs" Inherits="Reports_ComplaintSpAnalysisReport" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
<script language="javascript" type="text/javascript" >
   function fnOpenSpareConsumption(varComplaintNo)
    {
        var strUrl='../SIMS/Pages/SpareActivityLog.aspx?CompNo='+ varComplaintNo;
        window.open(strUrl,'History','height=550,width=850,left=20,scrollbars=1,top=30,Location=0');
    }
</script>    

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
       <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <ContentTemplate>
            <table width="100%" border="0">
                <tr>
                    <td class="headingred" style="width: 40%">
                        Complaint Analysis Report
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
                        Region:<font color="red">*</font>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlRegion" Width="175" AutoPostBack="true" runat="server" CssClass="simpletxt1"
                            OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                        </asp:DropDownList>
                        <div>
                            <asp:RequiredFieldValidator ID="rfvRegion" runat="server" ControlToValidate="ddlRegion" Display="Dynamic" ErrorMessage="Region is required." 
                                 ></asp:RequiredFieldValidator>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Branch:<font color="red">*</font>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBranch" Width="175" runat="server" 
                            CssClass="simpletxt1">
                        </asp:DropDownList>
                        <div>
                            <asp:RequiredFieldValidator ID="rfvbranch" runat="server" ControlToValidate="ddlBranch" Display="Dynamic" ErrorMessage="Branch is required." 
                                 ></asp:RequiredFieldValidator>
                                <%-- InitialValue="0"--%>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Product Division:<font color="red">*</font>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlProductDivision" Width="175" runat="server" CssClass="simpletxt1">
                        </asp:DropDownList>
                        <div>
                            <asp:RequiredFieldValidator ID="rfvDiv" runat="server" ControlToValidate="ddlProductDivision" Display="Dynamic" ErrorMessage="Product Division is required." 
                                 ></asp:RequiredFieldValidator>
                        </div>
                    </td>
                </tr>
                <tr>
                <td  align="right">
                Date
                </td>
                <td>
                                  <asp:TextBox runat="server" ID="txtFromDate" CssClass="txtboxtxt" ValidationGroup="editt"
                                        MaxLength="10" />
                                   <cc1:CalendarExtender ID="CEFrom" runat="server" TargetControlID="txtFromDate">
                                   </cc1:CalendarExtender>
                                    To
                                    <asp:TextBox runat="server" ID="txtToDate" CssClass="txtboxtxt" ValidationGroup="editt"
                                        MaxLength="10" />
                                    <cc1:CalendarExtender ID="CETo" runat="server" TargetControlID="txtToDate">
                                    </cc1:CalendarExtender>
                                     <asp:RequiredFieldValidator ID="rfvfromdate" runat="server" ControlToValidate="txtFromDate" Display="Dynamic" ErrorMessage="Date is required." /> 
                                     <asp:CustomValidator ID="RFVdate" runat="server" ControlToValidate="txtFromDate" Display="Dynamic" 
                                        SetFocusOnError="true" OnServerValidate="RFVdate_ServerValidate"></asp:CustomValidator>
                </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="Search" Width="100"
                            OnClick="btnSearch_Click" />
                        <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn" 
                            Text="Export To Execl" Width="100" OnClick="btnExport_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left">
                        Total Count:
                        <asp:Label ID="lblCount" ForeColor="Red" runat="server" Text="0"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvMIS" CssClass="simpletxt1" runat="server" RowStyle-CssClass="bgcolorcomm"
                            Width="100%" AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                            GridGroups="both" AllowPaging="false" PagerStyle-HorizontalAlign="Center" AutoGenerateColumns="false" 
                            HorizontalAlign="Left" >
                            <Columns>
                            <asp:BoundField DataField="SC_Name" HeaderText="ASC Name" />                   
                            <asp:BoundField DataField="ProductDivision_Desc" HeaderText="ProductDivision" />
                            <asp:BoundField DataField="Complaint_Split" HeaderText="ComplaintNo" />
                            <asp:BoundField DataField="LoggedDate" HeaderText="LoggedDate" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="CustomerName" HeaderText="CustomerName" />
                            <asp:BoundField DataField="CustomerContact" HeaderText="CustomerContact" />
                            <asp:BoundField DataField="City_Desc" HeaderText="City" />
                            <asp:BoundField DataField="ProductLine_Desc" HeaderText="ProductLine" />
                            <asp:BoundField DataField="Product_Desc" HeaderText="Product" />
                            <asp:BoundField DataField="SapProductCode" HeaderText="ProductSrNo" />
                            <asp:BoundField DataField="InvoiceDate" HeaderText="InvoiceDate" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="ACost" HeaderText="ServiceCost" />
                            <asp:BoundField DataField="MCost" HeaderText="MaterialCost" />
                            <asp:BoundField DataField="Internal_Bill_No" HeaderText="InternalBillNo" />
                            <asp:TemplateField HeaderText="SpareCount" >
                            <ItemTemplate>
                             <a href="javascript:void(0);" class="links" onclick="fnOpenSpareConsumption('<%#Eval("Complaint_Split")%>')" >
                             <%# Eval("SpareCount")%>
                             </a>
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ComplaintAge" HeaderText="ComplaintAge" />
                            <asp:BoundField DataField="OBSERV" HeaderText="ClosureComment" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>

