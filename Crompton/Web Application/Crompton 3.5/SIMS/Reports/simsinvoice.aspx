<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true"
    CodeFile="simsinvoice.aspx.cs" Inherits="SIMS_Reports_simsinvoice" Title="Invoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script type="text/javascript" src="../../scripts/jquery-1.3.1.min.js"> </script>

    <link href="../../css/Invoice.css" rel="stylesheet" type="text/css" />  

    <asp:UpdatePanel ID="pnlInvoice" runat="server">
        <ContentTemplate>
            <table width='90%' border='0' cellpadding='0' cellspacing='0' id="tblPrint" runat="server"
                visible="false">
                <tr>
                    <td align='right'>
                    <asp:LinkButton ID="lnkPrintPreview" runat="server" Text="Go For
                            Preview and Print" CssClass="links" OnClick="GoforPrintview"></asp:LinkButton>
                    </td>
                </tr>
            </table>
            <table width="90%">
                    <tr>
                        <td colspan="4" align="center">
                            <b>INVOICE</b>
                        </td>
                    </tr>
                    </table>
              <table width="90%">
                    <tr>
                       <td colspan="5" align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["SIMSAjaxLoadingImageName"]%>" alt="" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                    </tr>
                    <tr id="trRB" runat="server">
                        <td align="right" style="width:10%">
                            Region:
                        </td>
                        <td style="width:15%">
                            <asp:DropDownList ID="ddlRegion" runat="server" Width="214px" CssClass="simpletxt1"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" ValidationGroup="editt">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            Branch :
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="ddlBranch" runat="server" Width="227px" CssClass="simpletxt1"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" ValidationGroup="editt">
                            </asp:DropDownList>
                        </td>
                        
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblASCShowHide" runat="server">Asc :</asp:Label> 
                        </td>
                        <td style="width:15%">
                            <asp:DropDownList ID="ddlSerContractor" runat="server" Width="214px" CssClass="simpletxt1">
                                <asp:ListItem Text="Select" Value="0" />
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            Year :
                           </td>
                           <td>
                            <asp:DropDownList ID="Ddlyear" runat="server" Width="60px" CssClass="simpletxt1"
                                ValidationGroup="editt">
                            </asp:DropDownList>
                            &nbsp; Month <span style="color:Red;">*</span>: &nbsp;
                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="simpletxt1"
                                ValidationGroup="editt">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RFVMonth" runat="server" ControlToValidate="ddlMonth"
                                InitialValue="0" ValidationGroup="editt" ErrorMessage="*" Display="Dynamic" SetFocusOnError="True" />
                        </td>
                         <td align="right"><a href="SimsInvoiceFinWise.aspx" target="_self">Invoice Financial Year Wise</a></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="5" style="padding-top:10px;">
                            <asp:Button ID="btnSearch" runat="server" CausesValidation="true" 
                                CssClass="btn" OnClick="btnSearch_Click" Text="Search" ValidationGroup="editt" 
                                Width="70px" />
                                <asp:Button Width="70px" Text="Clear" CssClass="btn" ValidationGroup="editt" CausesValidation="false"
                                ID="btncancle" runat="server" OnClick="btncancle_Click" />
                        </td>
                        
                    </tr>
                    <tr>
                        <td colspan="5">
                        </td>
                    </tr>
                </table>
                <div id="dvInvoicecontent" runat="server">
                <table width='100%' border='0' cellpadding='0' cellspacing='0' id="tblInvoiceAscdetails"
                    runat="server" visible="false">
                    <tr>
                        <td>
                            <table width="99%">
                                <tr id="TrInvoiceHideShow" runat="server">
                                    <td class="ascheader">
                                        <span id="spnSoldto" runat="server">Sold To</span>
                                    </td>
                                    <td class="invoicedtsl1">
                                        Invoice No
                                    </td>
                                    <td class="invoicedtsl">: 
                                        <asp:Label ID="lblInvoiceNo" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                               <tr id="ShowHideInvoiceDate" runat="server">
                                    <td class="ascheader">
                                        <asp:Label ID="lblCustomerName" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td class="invoicedtsl1">
                                        Invoice Date
                                    </td>
                                    <td class="invoicedtsl">
                                        :&nbsp;<asp:Label ID="lblInvoiceDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ascheader" rowspan="2">
                                        <asp:Label ID="lblAscAddress" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td class="invoicedtsl1" colspan="2">
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td class="invoicedtsl1" colspan="2">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="90%" style ="width:100%;border:1px solid #b5b5b6!important;color:Black; border-collapse:collapse;" cellpadding="0" cellspacing="0" id="tblInvoiceDtls"
                    runat="server" visible="false">
                    <tr>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:50%;" align="center">
                            <b>Description</b>
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:10%; text-align:right!important;" align="center">
                            <b>Quantity</b>
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:15%; text-align:right!important;" align="center">
                            <b>Unit Price</b>
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:15%; text-align:right!important;" align="center">
                            <b>Amount</b>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:50%;">
                            <b>Fixed Compensation</b>
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:10%; text-align:right!important;">
                            <asp:Label ID="lblQuanityfd" runat="server" Text="0"></asp:Label>
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:15%; text-align:right!important;">
                            <asp:Label ID="lblfdUnitPrice" runat="server" Text="0.00"></asp:Label>
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:15%; text-align:right!important;">
                            <asp:Label ID="lblfdamount" runat="server" Text="0.00"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6;" colspan="4">
                            <b>Calls- Local(Other Products)</b>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:50%;">
                            Local Complaints (within city / town limits)
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:10%; text-align:right!important;">
                            <asp:Label ID="lbllcopquantity1" runat="server" Text="0"></asp:Label>
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:15%; text-align:right!important;">
                            <asp:Label ID="lbllcopunitprice1" runat="server" Text="100.00"></asp:Label>
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:15%; text-align:right!important;">
                            <asp:Label ID="lbllcopamount1" runat="server" Text="0.00"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:50%;">
                            Institutional Customers /Trade Partners visit where Quantity of Products are >4
                            per call
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:10%; text-align:right!important;">
                            <asp:Label ID="lbllcopquantity2" runat="server" Text="0"></asp:Label>
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:15%; text-align:right!important;">
                            <asp:Label ID="lbllcopunitprice2" runat="server" Text="400.00"></asp:Label>
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:15%; text-align:right!important;">
                            <asp:Label ID="lbllcopamount2" runat="server" Text="0.00"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6;" colspan="4">
                            <b>Calls-Local(Geysers)</b>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:50%;">
                            Local Complaints (within city / town limits)
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:10%; text-align:right!important;" >
                            <asp:Label ID="lbllcopgyquantity1" runat="server" Text="0"></asp:Label>
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:15%; text-align:right!important;">
                            <asp:Label ID="lbllcopgyunitprice1" runat="server" Text="150.00"></asp:Label>
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:15%; text-align:right!important;">
                            <asp:Label ID="lbllcopgyamount1" runat="server" Text="0.00"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:50%;">
                            Institutional Customers /Trade Partners visit where Quantity of Products are >4
                            per call
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:10%; text-align:right!important;">
                            <asp:Label ID="lbllcopgyquantity2" runat="server" Text="0"></asp:Label>
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:15%; text-align:right!important;">
                            <asp:Label ID="lbllcopgyunitprice2" runat="server" Text="500.00"></asp:Label>
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:15%; text-align:right!important;">
                            <asp:Label ID="lbllcopgyamount2" runat="server" Text="0.00"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6;" colspan="4">
                            <b>Calls-Outstation</b>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:50%;">
                            Out of Pocket Allowance (DA Charges, Lunch & Tea etc.) for journeys with same day
                            return
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:10%; text-align:right!important;">
                            <asp:Label ID="lblcoquantity1" runat="server" Text="0"></asp:Label>
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:15%; text-align:right!important;">
                            <asp:Label ID="lblcounitprice1" runat="server" Text="100.00"></asp:Label>
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:15%; text-align:right!important;">
                            <asp:Label ID="lblcoamount1" runat="server" Text="0.00"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:50%;">
                            Boarding & Lodging Expenses for overnight stay
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:10%; text-align:right!important;">
                            <asp:Label ID="lblcoquantity2" runat="server" Text="0"></asp:Label>
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:15%; text-align:right!important;">
                            <asp:Label ID="lblcounitprice2" runat="server" Text="400.00"></asp:Label>
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:15%; text-align:right!important;">
                            <asp:Label ID="lblcoamount2" runat="server" Text="0.00"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:50%;">
                            OutStation Complaints -Geysers(within city / town limits)</td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:10%; text-align:right!important;">
                            <asp:Label ID="lbllocalforoutstationwaterqty" runat="server" Text="0"></asp:Label></td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:15%; text-align:right!important;">
                            <asp:Label ID="lbllocalforoutstationwaterUnitPrice" runat="server" Text="150.00"></asp:Label></td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:15%; text-align:right!important;">
                            <asp:Label ID="lbllocalforoutstationwaterAmount" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:50%;">
                            OutStation Complaints- Other Products(within city / town limits)</td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:10%; text-align:right!important;">
                            <asp:Label ID="lbllocalforoutstationexptwaterqty" runat="server" Text="0"></asp:Label></td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:15%; text-align:right!important;">
                           <asp:Label ID="lbllocalforoutstationexptwaterUnitPrice" runat="server" Text="100.00"></asp:Label></td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:15%; text-align:right!important;">
                            <asp:Label ID="lbllocalforoutstationexptwaterAmount" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6;" colspan="4">
                            <b>Demo Charges(FP Only)</b>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:50%;">
                            FOOD PROCESSOR
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:10%; text-align:right!important;">
                            <asp:Label ID="lblFoodProcessorQuantity" runat="server" Text="0"></asp:Label>
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:15%; text-align:right!important;">
                            <asp:Label ID="lblFoodProcessorUnitPrice" runat="server" Text="150.00"></asp:Label>
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:15%; text-align:right!important;">
                            <asp:Label ID="lblFoodProcessorAmount" runat="server" Text="0.00"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6;" colspan="4">
                            <b>Conveyance Charges(Outstation Travel)</b>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:50%;">
                            Travel by either State Roadways Bus or Train Non A.C Sleeper Class
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:10%; text-align:right!important;">
                            <asp:Label ID="lblcoquantity3" runat="server" Text="0"></asp:Label>
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:15%; text-align:right!important;">
                            <asp:Label ID="lblcounitprice3" runat="server" Text="400.00"></asp:Label>
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:15%; text-align:right!important;">
                            <asp:Label ID="lblcoamount3" runat="server" Text="0.00"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:50%;">
                            Travel by Two-wheelers,Conveyance Charge @ 3.00 per Km/Max 100 KM
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:10%; text-align:right!important;">
                            <asp:Label ID="lblcoquantity4" runat="server" Text="0"></asp:Label>
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:15%; text-align:right!important;">
                            <asp:Label ID="lblcounitprice4" runat="server" Text="3.00"></asp:Label>
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:15%; text-align:right!important;">
                            <asp:Label ID="lblcoamount4" runat="server" Text="0.00"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:50%;">
                            Local Conveyance (Only when the Journey has been made by Bus or Train)
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:10%; text-align:right!important;">
                            <asp:Label ID="lblcoquantity5" runat="server" Text="0"></asp:Label>
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:15%; text-align:right!important;">
                            <asp:Label ID="lblcounitprice5" runat="server" Text="50.00"></asp:Label>
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:15%; text-align:right!important;">
                            <asp:Label ID="lblcoamount5" runat="server" Text="0.00"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width: 75%;" colspan="3" align="right">
                            <b>Sub Total</b>
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:15%; text-align:right!important;">
                            <asp:Label ID="lblTotalAmount" runat="server" Font-Bold="true" Text="0.00000" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width: 75%;" colspan="2" align="right">
                            <asp:Label runat="server" Font-Bold="true" ID="lblServicetax"></asp:Label>
                        </td>
                        <td align="center"><asp:CheckBox ID="chkServicetaxoption" runat="server" Text="Add/Remove" Checked="true"
                                AutoPostBack="true" OnCheckedChanged="chkServicetaxoptionClick" />
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:15%; text-align:right!important;">
                            <asp:Label ID="lblServiceChargesBracks" runat="server" Text="" ForeColor="Green"></asp:Label>
                            <asp:Label ID="lblTax" runat="server" Text="0.00000" Font-Bold="true" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6;"  align="right" colspan="3">
                            <b>Total</b>
                        </td>
                        <td style="padding:2px 5px; border:1px solid #c4c5c6; width:15%; text-align:right!important;">
                            <asp:Label ID="lblTAmount" runat="server" Font-Bold="true" Text="0.00000" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
                
            <div style="height:10px;"></div>
            <table width="40%" border="1" cellpadding="2" runat="server" id="SummaryTable" cellspacing="0" style="border: 1px solid #b5b5b6!important; color: Black; border-collapse: collapse;">
            <tr><td colspan="2" align="center"><b>Summary of Charges</b></td></tr>
            <tr><td>Fixed Compensation</td><td align="right"><asp:Label ID="lblSummaryFix" runat="server"></asp:Label></td></tr>
            <tr><td>Labour Charge - Local</td><td align="right"><asp:Label ID="lblSummaryLocal" runat="server"></asp:Label></td></tr>
            <tr><td>Labour Charge - Outstation</td><td align="right"><asp:Label ID="lblSummaryOutstation" runat="server"></asp:Label></td></tr>
            <tr><td>Conveyance (including Lodging, Meals etc.)</td><td align="right"><asp:Label ID="lblSummaryConveyance" runat="server"></asp:Label></td></tr>
            <tr><td><b>Total</b></td><td align="right"><asp:Label Font-Bold="true" ID="lblSummaryTotal" runat="server"></asp:Label></td></tr>
            <tr><td>Service Tax, if applicable</td><td align="right"><asp:Label ID="lblSummaryTax" runat="server" Text="-"></asp:Label></td></tr>
            <tr><td>Demo Charges</td><td align="right"><asp:Label ID="lblSummaryDemo" runat="server"></asp:Label></td></tr>
            <tr><td><b>Grand Total</b></td><td align="right"><asp:Label Font-Bold="true" ID="lblSummaryGrandTotal" runat="server"></asp:Label></td></tr>
            </table>
            
                <table width="90%" class="table_border" cellpadding="0" cellspacing="0" id="tblEmptyMessage"
                    runat="server" visible="true">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblEmptyMessage" runat="server" Text="<b>Please Click on search to generate report.</b>"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:HiddenField ID="hdnRawUrl" Value="" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
