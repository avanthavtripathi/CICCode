<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintInvoice.aspx.cs" Inherits="SIMS_Reports_PrintInvoice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Invoice Details</title>
    <style type="text/css" media="print">
       
        @media print
        {
            .result
            {
                height: 100%;
                overflow: visible;
            }
        }
    </style>
    <style type="text/css">
        #dvInvoiceDetails
        {
            width: 99%; 
            margin:0 auto;           
        }
    </style>

    <script type="text/javascript">
        function PrintPopup() {
            if (confirm("Have you done your browser setting for print, if not then please click at ? and follow step to configure browsers print property.")) {
                var divs = document.getElementById("dvRemoev");
                divs.parentNode.removeChild(divs);
                window.print();
                window.close();
            }
            else {
                return false;
            }
        }
    </script>

</head>
<body style="font-size:11px!important; font-family:arial,helvetica,sans-serif;">
    <form id="form1" runat="server">
    <div style="width: 98%; text-align: right;" id="dvRemoev">
        <img alt="printer" src="../../images/printer.png" style="border-style: none; cursor: pointer;"
            onclick="javascript:PrintPopup();" title="Print" />
        &nbsp; <a href="../../docs/Customer/Browser Page Setup Settings.pdf" title="Help">
            <img src="../../images/information-balloon.png" alt="Information" style="border-style: none" />
        </a>
    </div>
    <div id="dvInvoiceDetails" runat="server" >
        <link href="../../css/Invoice.css" rel="stylesheet" type="text/css" />
        <table width="100%">
            <tr>
                <td style="margin-left:500px;">
                    <b>INVOICE</b>
                </td>
                <td align="center" rowspan="3">
                    <asp:Image ID="imglogo" ImageUrl="../../images/Crompton-greaves.jpg" runat="server" />
                </td>
            </tr>
            <tr id="trRB" runat="server">
                <td align="left" style="width: 64%">
                    Region :
                    <asp:Label ID="lblRegion" runat="server" Text=""></asp:Label><br />
                    Branch :
                    <asp:Label ID="lblBranch" runat="server" Text="<%=Request.form %>"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    Invoice for :
                    <asp:Label ID="lblmonth" runat="server" Text=""></asp:Label>&nbsp;-&nbsp;<asp:Label
                        ID="lblYear" Text="" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <div id="dvInvoicecontent" runat="server" style="margin:0px 15px 0px 15px;">
            <table width='100%' border='0' cellpadding='0' cellspacing='0' id="tblInvoiceAscdetails"
                runat="server" visible="false">
                <tr>
                    <td>
                        <table width="99%">
                            <tr id="TrInvoiceHideShow" runat="server">
                                <td style="width:62%">
                                    <span id="spnSoldto" runat="server">Sold To</span>
                                </td>
                                <td style="width:9%">
                                    Invoice No
                                </td>
                                <td style="width:29%">
                                    :&nbsp;<asp:Label ID="lblInvoiceNo" runat="server" Text="0001" ForeColor="Red" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr id="ShowHideInvoiceDate" runat="server">
                                <td style="width:62%">
                                    <asp:Label ID="lblCustomerName" runat="server" Text=""></asp:Label>
                                </td>
                                <td style="width:9%">
                                    Invoice Date
                                </td>
                                <td style="width:29%">
                                    :&nbsp;<asp:Label ID="lblInvoiceDate" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:62%" rowspan="2">
                                    <asp:Label ID="lblAscAddress" runat="server" Text=""></asp:Label>
                                </td>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table width="90%" style="width: 100%; border: 1px solid #b5b5b6!important; color: Black;
                border-collapse: collapse;" cellpadding="0" cellspacing="0" id="tblInvoiceDtls"
                runat="server" visible="false">
                <tr>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 30%;" align="center">
                        <b>Description</b>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 5%; text-align: right!important;"
                        align="center">
                        <b>Quantity</b>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 7%; text-align: right!important;"
                        align="center">
                        <b>Unit Price</b>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%; text-align: right!important;"
                        align="center">
                        <b>Amount</b>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 20%; text-align: left!important;"
                        align="center">
                        <b>Remarks</b>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 30%;">
                        <b>Fixed Compensation</b>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 5%; text-align: right!important;">
                        <asp:Label ID="lblQuanityfd" runat="server" Text="0"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 7%; text-align: right!important;">
                        <asp:Label ID="lblfdUnitPrice" runat="server" Text="0.00"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%; text-align: right!important;">
                        <asp:Label ID="lblfdamount" runat="server" Text="0.00"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 20%; text-align: right!important;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" colspan="5">
                        <b>Calls- Local(Other Products)</b>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 30%;">
                        Local Complaints (within city / town limits)
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 5%; text-align: right!important;">
                        <asp:Label ID="lbllcopquantity1" runat="server" Text="0"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 7%; text-align: right!important;">
                        <asp:Label ID="lbllcopunitprice1" runat="server" Text="100.00"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%; text-align: right!important;">
                        <asp:Label ID="lbllcopamount1" runat="server" Text="0.00"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 20%; text-align: right!important;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 30%;">
                        Institutional Customers /Trade Partners visit where Quantity of Products are >4
                        per call
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 5%; text-align: right!important;">
                        <asp:Label ID="lbllcopquantity2" runat="server" Text="0"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 7%; text-align: right!important;">
                        <asp:Label ID="lbllcopunitprice2" runat="server" Text="400.00"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%; text-align: right!important;">
                        <asp:Label ID="lbllcopamount2" runat="server" Text="0.00"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 20%; text-align: right!important;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" colspan="5">
                        <b>Calls-Local(Geysers)</b>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 30%;">
                        Local Complaints (within city / town limits)
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 5%; text-align: right!important;">
                        <asp:Label ID="lbllcopgyquantity1" runat="server" Text="0"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 7%; text-align: right!important;">
                        <asp:Label ID="lbllcopgyunitprice1" runat="server" Text="150.00"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%; text-align: right!important;">
                        <asp:Label ID="lbllcopgyamount1" runat="server" Text="0.00"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 20%; text-align: right!important;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 30%;">
                        Institutional Customers /Trade Partners visit where Quantity of Products are >4
                        per call
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 5%; text-align: right!important;">
                        <asp:Label ID="lbllcopgyquantity2" runat="server" Text="0"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 7%; text-align: right!important;">
                        <asp:Label ID="lbllcopgyunitprice2" runat="server" Text="500.00"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%; text-align: right!important;">
                        <asp:Label ID="lbllcopgyamount2" runat="server" Text="0.00"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 20%; text-align: right!important;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" colspan="5">
                        <b>Calls-Outstation</b>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 30%;">
                        Out of Pocket Allowance (DA Charges, Lunch & Tea etc.) for journeys with same day
                        return
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 5%; text-align: right!important;">
                        <asp:Label ID="lblcoquantity1" runat="server" Text="0"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 7%; text-align: right!important;">
                        <asp:Label ID="lblcounitprice1" runat="server" Text="100.00"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%; text-align: right!important;">
                        <asp:Label ID="lblcoamount1" runat="server" Text="0.00"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 20%; text-align: right!important;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 30%;">
                        Boarding & Lodging Expenses for overnight stay
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 5%; text-align: right!important;">
                        <asp:Label ID="lblcoquantity2" runat="server" Text="0"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 7%; text-align: right!important;">
                        <asp:Label ID="lblcounitprice2" runat="server" Text="100.00"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%; text-align: right!important;">
                        <asp:Label ID="lblcoamount2" runat="server" Text="0.00"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 20%; text-align: right!important;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 30%;">
                        OutStation Complaints -Geysers(within city / town limits)
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 5%; text-align: right!important;">
                        <asp:Label ID="lbllocalforoutstationwaterqty" runat="server" Text="0"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 7%; text-align: right!important;">
                        <asp:Label ID="lbllocalforoutstationwaterUnitPrice" runat="server" Text="150.00"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%; text-align: right!important;">
                        <asp:Label ID="lbllocalforoutstationwaterAmount" runat="server" Text="0.00"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 20%; text-align: right!important;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 30%;">
                        OutStation Complaints- Other Products(within city / town limits)
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 5%; text-align: right!important;">
                        <asp:Label ID="lbllocalforoutstationexptwaterqty" runat="server" Text="0"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 7%; text-align: right!important;">
                        <asp:Label ID="lbllocalforoutstationexptwaterUnitPrice" runat="server" Text="100.00"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%; text-align: right!important;">
                        <asp:Label ID="lbllocalforoutstationexptwaterAmount" runat="server" Text="0.00"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 20%; text-align: right!important;">
                        &nbsp;
                    </td>
                </tr>                
                <tr>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" colspan="4">
                        <b>Demo Charges(FP Only)</b>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 30%;">
                        FOOD PROCESSOR
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 5%; text-align: right!important;">
                        <asp:Label ID="lblFoodProcessorQuantity" runat="server" Text="0"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 7%; text-align: right!important;">
                        <asp:Label ID="lblFoodProcessorUnitPrice" runat="server" Text="150.00"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%; text-align: right!important;">
                        <asp:Label ID="lblFoodProcessorAmount" runat="server" Text="0.00"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 20%; text-align: right!important;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" colspan="5">
                        <b>Conveyance Charges(Outstation Travel)</b>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 30%;">
                        Travel by either State Roadways Bus or Train Non A.C Sleeper Class
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 5%; text-align: right!important;">
                        <asp:Label ID="lblcoquantity3" runat="server" Text="0"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 7%; text-align: right!important;">
                        <asp:Label ID="lblcounitprice3" runat="server" Text="400.00"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%; text-align: right!important;">
                        <asp:Label ID="lblcoamount3" runat="server" Text="0.00"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 20%; text-align: right!important;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 30%;">
                        Travel by Two-wheelers,Conveyance Charge @ 3.00 per Km/Max 100 KM
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 5%; text-align: right!important;">
                        <asp:Label ID="lblcoquantity4" runat="server" Text="0"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 7%; text-align: right!important;">
                        <asp:Label ID="lblcounitprice4" runat="server" Text="3.00"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%; text-align: right!important;">
                        <asp:Label ID="lblcoamount4" runat="server" Text="0.00"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 20%; text-align: right!important;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 30%;">
                        Local Conveyance (Only when the Journey has been made by Bus or Train)
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 5%; text-align: right!important;">
                        <asp:Label ID="lblcoquantity5" runat="server" Text="0"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 7%; text-align: right!important;">
                        <asp:Label ID="lblcounitprice5" runat="server" Text="50.00"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%; text-align: right!important;">
                        <asp:Label ID="lblcoamount5" runat="server" Text="0.00"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 20%; text-align: right!important;">
                        &nbsp;
                    </td>
                </tr>
               <tr>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 75%;" colspan="3"
                        align="right">
                        <b>Sub Total</b>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%; text-align: right!important;">
                        <asp:Label ID="lblTotalAmount" runat="server" Text="0.00000" ForeColor="Red" 
                            Font-Bold="True"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 20%; text-align: right!important;">
                        &nbsp;
                    </td>
                </tr>
                <tr id="trTaxDetails" runat="server">
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 75%;" colspan="3"
                        align="right">
                         <asp:Label runat="server" Font-Bold="true" ID="lblServicetax"></asp:Label>
                        <%--<asp:CheckBox ID="chkServicetaxoption" runat="server" Text="Add/Remove" Checked="true" />--%>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 7%; text-align: right!important;">
                        <asp:Label ID="lblServiceChargesBracks" runat="server" Text="" ForeColor="Green"></asp:Label>
                        <asp:Label ID="lblTax" runat="server" Text="0.00000" Font-Bold="true" ForeColor="Red"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 7%; text-align: right!important;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="right" colspan="3">
                        <b>Total</b>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 7%; text-align: right!important;">
                        <asp:Label ID="lblTAmount" runat="server" Text="0.00000" ForeColor="Red" 
                            Font-Bold="True"></asp:Label>
                    </td>
                    <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 7%; text-align: right!important;">
                        &nbsp;
                    </td>
                </tr>
            </table>
            
            <div style="height:10px;"></div>
            <table width="40%" border="1" cellpadding="2" runat="server" id="SummaryTable" cellspacing="0" style="border: 1px solid #b5b5b6!important; color: Black; border-collapse: collapse;">
            <tr><td colspan="2" align="center" class="style1"><b>Summary of Charges</b></td></tr>
            <tr><td>Fixed Compensation</td><td align="right"><asp:Label ID="lblSummaryFix" runat="server"></asp:Label></td></tr>
            <tr><td>Labour Charge - Local</td><td align="right"><asp:Label ID="lblSummaryLocal" runat="server"></asp:Label></td></tr>
            <tr><td class="style1">Labour Charge - Outstation</td><td align="right" 
                    class="style1"><asp:Label ID="lblSummaryOutstation" runat="server"></asp:Label></td></tr>
            <tr><td>Conveyance (including Lodging, Meals etc.)</td><td align="right"><asp:Label ID="lblSummaryConveyance" runat="server"></asp:Label></td></tr>
            <tr><td><b>Total</b></td><td align="right"><asp:Label Font-Bold="true" ID="lblSummaryTotal" runat="server"></asp:Label></td></tr>
            <tr id="TrServicetaxSummary" runat="server"><td>Service Tax, if applicable</td><td align="right"><asp:Label ID="lblSummaryTax" runat="server" Text="-"></asp:Label></td></tr>
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
    </div>
    </form>
</body>
</html>
