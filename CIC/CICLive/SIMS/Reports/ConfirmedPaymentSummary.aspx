<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true" CodeFile="ConfirmedPaymentSummary.aspx.cs" Inherits="SIMS_Reports_ConfirmedPaymentSummary" Title="Confirmed Payment Summary" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
<script language="javascript" type="text/javascript">
        function SelectDate(type)
        {
        var my_date
        var indate
        var dDate,mMonth,yYear
       
        var prdcode=document.getElementById("ctl00_MainConHolder_ddlProductDivison").value;
        var LoggedDateFrom,LoggedDateTo;
        LoggedDateFrom=document.getElementById("ctl00_MainConHolder_txtFromDate").value;
        LoggedDateTo=document.getElementById("ctl00_MainConHolder_txtToDate").value;
        if(LoggedDateFrom == "" || LoggedDateTo =="" )
          {
            alert('Please enter a date range.');
            return false;
          }
        else
          {
            indate=new Date(LoggedDateFrom);
            my_date=new Date(LoggedDateTo);
            var m
            var selm
            m=parseInt(indate.getMonth());
           if(prdcode==0)
               {
                    m=m+0
                    selm=1
               }
           else
               {
                    selm=12
                    m=m+12
               }
           
            var msg
            if(selm ==1 )
            {
                msg="You can view the data only for current month. Please change your date selection.\n Or select product division to view the data for previous 12 months.";
            }
            else
            {
                msg="You can view the data only for previous 12 month. Please change your date selection.";
            }
              if(indate.getFullYear()< my_date.getFullYear())
                 {
                        alert(msg);
                        return false;
                 }
              if( parseInt(m)< parseInt(my_date.getMonth()))
                 {
                       alert(msg);
                       return false;
                 }
            }
 }
 </script>
 
<asp:UpdatePanel ID="RptPaySumm" runat="server" >
<Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
</Triggers>
<ContentTemplate>
    <table width="100%">
        <tr>
            <td class="headingred">
                Confirmed Payments Summary
            </td>
            <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                    <ProgressTemplate>
                        <img src="<%=ConfigurationManager.AppSettings["SIMSAjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                </asp:UpdateProgress>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right" style="padding-right: 10px">
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
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0">
                    <tr>
                        <td width="100%" align="left" class="bgcolorcomm">
                            <table width="98%" border="0">
                                <tr>
                                    <td colspan="2" align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                        <font color='red'>*</font>
                                        <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td width="30%" align="right">
                                        Region
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlRegion" AutoPostBack="true" runat="server" CssClass="simpletxt1"
                                            Width="175px" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvRegion" runat="server" SetFocusOnError="true" Enabled="false" 
                                            ErrorMessage="Region is required." InitialValue="0" ControlToValidate="ddlRegion"
                                            ValidationGroup="editt" ToolTip="Region is required." Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="30%" align="right">
                                        Branch
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="simpletxt1" Width="175px" />
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" SetFocusOnError="true" Enabled="false" 
                                            ErrorMessage="Branch is required." InitialValue="0" ControlToValidate="ddlBranch"
                                            ValidationGroup="editt" ToolTip="Branch is required." Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="30%" align="right">
                                        Divison<font color='red'>*</font>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlProductDivison" runat="server" Width="175px" CssClass="simpletxt1"
                                            ValidationGroup="editt">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RFRegionDesc" runat="server" ControlToValidate="ddlProductDivison"
                                            ErrorMessage="Division is required." InitialValue="0" SetFocusOnError="true" Enabled="false" 
                                            ValidationGroup="editt" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="30%" align="right">
                                        Select Data Range</td>
                                    <td align="left">
                                      <asp:TextBox runat="server" ID="txtFromDate" CssClass="txtboxtxt" ValidationGroup="editt"  MaxLength="10" />
                                      <asp:RequiredFieldValidator ID="reffrom" runat="server" Text="*" ControlToValidate="txtFromDate" SetFocusOnError="true" ValidationGroup="editt"> </asp:RequiredFieldValidator>
                                      <cc1:CalendarExtender ID="CEFrom" runat="server" TargetControlID="txtFromDate">
                                      </cc1:CalendarExtender>
                                    To
                                    <asp:TextBox runat="server" ID="txtToDate" CssClass="txtboxtxt" ValidationGroup="editt"
                                        MaxLength="10" />
                                        <asp:RequiredFieldValidator ID="refto" runat="server" Text="*" ControlToValidate="txtToDate" SetFocusOnError="true" ValidationGroup="editt"> </asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CETo" runat="server" TargetControlID="txtToDate">
                                    </cc1:CalendarExtender>
                                </td>
                                </tr>
                                <tr>
                                    <td width="30%" align="right">
                                        &nbsp;</td>
                                    <td align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td height="25" align="left">
                                        &nbsp;
                                    </td>
                                    <td>
                                       <table>
                                            <tr>
                                                <td align="right">
                                                    <asp:Button Text="Search" Width="70px" ID="btnSearch" CssClass="btn" runat="server"
                                                        CausesValidation="True" ValidationGroup="editt" OnClick="btnSearch_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn" Text="Export To Execl"
                                                    Width="100" onclick="btnExport_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                               <b>Payments Details</b><br />
                                <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                    HeaderStyle-CssClass="fieldNamewithbgcolor" GridLines="both" AllowSorting="True"
                                    AutoGenerateColumns="false" ID="gvConfirmedPayment" runat="server" Width="100%"
                                    HorizontalAlign="Left" OnPageIndexChanging="gvConfirmedPayment_PageIndexChanging" >
                                    <RowStyle CssClass="gridbgcolor" Wrap="true" />
                                    <HeaderStyle HorizontalAlign="Left" Width="60px" Wrap="true" />
                                    <Columns>
                                    <asp:BoundField DataField="TransactionMonth" HeaderText="Month" />
                                    <asp:BoundField DataField="Unit" HeaderText="Division" />
                                    <asp:BoundField DataField="Region" HeaderText="Region" />
                                    <asp:BoundField DataField="Branch" HeaderText="Branch" />
                                    <asp:BoundField DataField="TransactionNo" HeaderText="Payment Transaction No" />
                                    <asp:TemplateField HeaderStyle-Width="150px" HeaderText="Internal Bills" >
                                        <ItemTemplate>
                                           <div style="width:200px;text-align:left;word-break:break-all;" > <%#Eval("Bills")%> </div>
                                        </ItemTemplate>
                                   </asp:TemplateField>
                                    <asp:BoundField DataField="BillCount" HeaderText="Processed IBN" ItemStyle-HorizontalAlign="Center"  />
                                    <asp:BoundField DataField="IBNAmount" HeaderText="IBN Amount" />
                                    <asp:BoundField DataField="AmountPaid" HeaderText="Total SUM Paid" />
                                    <asp:BoundField DataField="Difference" HeaderText="Difference" />
                                    <asp:BoundField DataField="Miscellaneous1" HeaderText="Miscellaneous1" />
                                    <asp:BoundField DataField="Miscellaneous2" HeaderText="Miscellaneous2" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                    <img src="<%=ConfigurationManager.AppSettings["SIMSUserMessage"]%>" alt="" />
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
                        <td align="left">
                                <br />
                                <br />
                                <br />
                                <b>Summary</b><br />
                                <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                    HeaderStyle-CssClass="fieldNamewithbgcolor" GridLines="both" AllowSorting="True" Width="550px" 
                                    AutoGenerateColumns="false" ID="gvTransactionDetail" runat="server" HorizontalAlign="Left">
                                    <RowStyle CssClass="gridbgcolor" Wrap="true" Width="150px"  />
                                    <HeaderStyle HorizontalAlign="Left" Width="150px" Wrap="true" />
                                    <Columns>
                                    <asp:BoundField DataField="TransactionMonth" HeaderText="Month" HeaderStyle-Width="110px" ItemStyle-Width="110px" />
                                    <asp:BoundField DataField="Unit" HeaderText="Division" HeaderStyle-Width="90px" ItemStyle-Width="90px"  />
                                    <asp:BoundField DataField="Region" HeaderText="Region" HeaderStyle-Width="90px" ItemStyle-Width="90px"   />
                                    <asp:BoundField DataField="Branch" HeaderText="Branch" HeaderStyle-Width="100px" ItemStyle-Width="100px"  />
                                    <asp:BoundField DataField="TransactionCount" HeaderText="Processed Payment Transaction" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="BillCount" HeaderText="IBN Processed" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                    <img src="<%=ConfigurationManager.AppSettings["SIMSUserMessage"]%>" alt="" />
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
                </table>
            </td>
        </tr>
    </table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
