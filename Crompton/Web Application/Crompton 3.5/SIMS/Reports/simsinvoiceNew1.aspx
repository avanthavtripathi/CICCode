<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true"
    CodeFile="simsinvoiceNew1.aspx.cs" Inherits="SIMS_Reports_simsinvoiceNew1" Title="Invoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script type="text/javascript" src="../../scripts/jquery-1.3.1.min.js"> </script>

    <link href="../../css/Invoice.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        @media print
        {
            .result
            {
                height: 100%;
                overflow: visible;
            }
            body.test #dvPrint #imglogo
            {
                display: inline-block;
            }
            body.test
            {
                width: 95%;
            }
            body img
            {
                width: 90%;
                max-width: 1048px;
            }
        }
        .style1
        {
            height: 18px;
        }
        .style2
        {
            width: 15%;
        }
    </style>

    <script type="text/javascript">
       function printDiv(divName) {
     var printContents = document.getElementById(divName).innerHTML;
     var originalContents = document.body.innerHTML;

     document.body.innerHTML = printContents;

     window.print();

     document.body.innerHTML = originalContents;
}
    </script>

    <asp:UpdatePanel ID="pnlInvoice" runat="server">
        <ContentTemplate>
            <table width='90%' border='0' cellpadding='0' cellspacing='0' id="tblPrint" runat="server"
                visible="false">
                <tr>
                    <td align='right'>
                    </td>
                </tr>
            </table>
            <table width="90%">
                <tr>
                    <td colspan="4" align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>"
                        style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["SIMSAjaxLoadingImageName"]%>" alt="" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr id="trRB" runat="server">
                    <td align="right" style="width: 10%">
                        Region:
                    </td>
                    <td style="width: 15%">
                        <asp:DropDownList ID="ddlRegion" runat="server" Width="214px" CssClass="simpletxt1"
                            AutoPostBack="true" ValidationGroup="editt">
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        Branch :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBranch" runat="server" Width="227px" CssClass="simpletxt1"
                            AutoPostBack="true" ValidationGroup="editt">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblASCShowHide" runat="server">Asc :</asp:Label>
                    </td>
                    <td style="width: 15%">
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
                        &nbsp; Month <span style="color: Red;">*</span>: &nbsp;
                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="simpletxt1" ValidationGroup="editt">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFVMonth" runat="server" ControlToValidate="ddlMonth"
                            InitialValue="0" ValidationGroup="editt" ErrorMessage="*" Display="Dynamic" SetFocusOnError="True" />
                    </td>
                    <%--     <td align="right"><a href="SimsInvoiceFinWise.aspx" target="_self">Invoice Financial Year Wise</a></td>--%>
                    <td align="right">
                        <input type="button" onclick="printDiv('dvPrint')" value="Print" class="btn" />
                    </td>
                    <td align="right">
                        <asp:Button ID="btnsummery" runat="server" CssClass="btn" Text="Summary Report" Width="120px"
                            OnClick="btnsummery_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="4" style="padding-top: 10px;">
                        <asp:Button ID="btnSearch" runat="server" CausesValidation="true" CssClass="btn"
                            Text="Search" ValidationGroup="editt" Width="70px" OnClick="btnSearch_Click" />
                        <asp:Button Width="70px" Text="Clear" CssClass="btn" ValidationGroup="editt" CausesValidation="false"
                            ID="btncancle" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                    </td>
                </tr>
            </table>
            <div id="dvPrint">
                <div id="dvInvoicecontent" runat="server">
                    <table width='100%' border='0' cellpadding='0' cellspacing='0' id="tblInvoiceAscdetails"
                        runat="server">
                        <tr>
                            <td>
                                <table width="99%">
                                    <tr id="TrInvoiceHideShow" runat="server">
                                        <td class="ascheader">
                                            <span id="spnSoldto" style="display: none" runat="server">Sold To</span>
                                        </td>
                                        <td class="invoicedtsl1" style="width: 2%; display: none;">
                                            No
                                        </td>
                                        <td class="invoicedtsl" style="display: none;">
                                            :
                                            <asp:Label ID="lblInvoiceNo" runat="server" Text="" ForeColor="Red" Font-Bold="true"> </asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="ShowHideInvoiceDate" runat="server">
                                        <td class="ascheader">
                                            <asp:Label ID="lblCustomerName" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="invoicedtsl1" style="display: none;">
                                            Date
                                        </td>
                                        <td class="invoicedtsl" style="display: none;">
                                            :&nbsp;<asp:Label ID="lblInvoiceDate" runat="server" Visible="false"></asp:Label>
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
                                <table width="90%">
                                    <tr>
                                        <td align="center">
                                            <b style="font-size: 12px;">Working of the Month:&nbsp;<asp:Label ID="lblMnth" runat="server"> </asp:Label></b>
                                        </td>
                                        <td align="center" rowspan="3">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="90%" style="width: 100%; border: 1px solid #b5b5b6!important; color: Black;
                        border-collapse: collapse;" cellpadding="0" cellspacing="0" id="tblInvoiceDtls"
                        runat="server">
                        <tr>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 50%;" align="center">
                                <b>Description</b>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%; text-align: right!important;"
                                align="center">
                                <b>Quantity</b>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 15%; text-align: right!important;"
                                align="center">
                                <b>Unit Price</b>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 15%; text-align: right!important;"
                                align="center">
                                <b>Amount</b>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 50%; height: 20px;">
                                <b>Fixed Compensation</b>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%; text-align: right!important;
                                height: 20px;">
                                <asp:Label ID="lblQuanityfd" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 15%; text-align: right!important;
                                height: 20px;">
                                <asp:Label ID="lblfdUnitPrice" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 15%; text-align: right!important;
                                height: 20px;">
                                <asp:Label ID="lblfdamount" runat="server" >0</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 50%; height: 20px;">
                                <b>Repair Institutional Customer Local</b>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%; text-align: right!important;
                                height: 20px;">
                                <asp:Label ID="lblRepLoQ" runat="server" Text="0"></asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 15%; text-align: right!important;
                                height: 20px;">
                                <asp:Label ID="lblRepLoU" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 15%; text-align: right!important;
                                height: 20px;">
                                <asp:Label ID="lblRepLoA" runat="server">0</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 50%; height: 20px;">
                                <b>Repair Institutional Customer Outstation</b>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%; text-align: right!important;
                                height: 20px;">
                                <asp:Label ID="lblRepOoQ" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 15%; text-align: right!important;
                                height: 20px;">
                                <asp:Label ID="lblRepOoU" runat="server" >0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 15%; text-align: right!important;
                                height: 20px;">
                                <asp:Label ID="lblRepOoA" runat="server">0</asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" style="border: 1px solid #b5b5b6!important; color: Black; border-collapse: collapse;"
                        cellpadding="0" cellspacing="0" id="tblFixedDtls" runat="server">
                        <%-- <tr><td rowspan="4" style="padding:2px 5px; border:1px solid #c4c5c6;" align="center"><b>Repair Charges</b></td></tr>--%>
                        <tr>
                            <th rowspan="6" style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%;"
                                class="th-cls" align="center">
                                Repair Charges
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%;" class="th-cls"
                                align="center">
                                Complaint Type
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 14%;" class="th-cls"
                                align="center">
                                H Code
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 14%;" class="th-cls"
                                align="center">
                                &lt;=18 Hrs
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 14%;" class="th-cls"
                                align="center">
                                18 &lt; Hrs&gt;=24
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 14%;" class="th-cls"
                                align="center">
                                24 &lt; Hrs &gt;=48
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 14%;" class="th-cls"
                                align="center">
                                48 &lt; Hrs &gt;=72
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%;" class="th-cls"
                                align="center">
                                72 &lt; Hrs &gt;=120
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%;" class="th-cls"
                                align="center">
                                >120 Hrs
                            </th>
                        </tr>
                        <tr>
                            <td rowspan="2" style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                Local
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center" class="style1">
                                With H Code
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center" class="style1">
                                <asp:Label ID="lblLWHU18" runat="server" >0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center" class="style1">
                                <asp:Label ID="lblLWHU24" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center" class="style1">
                                <asp:Label ID="lblLWHU72" runat="server" >0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center" class="style1">
                                <asp:Label ID="lblLWHU120" runat="server" >0</asp:Label>
                            </td>
                            <%-------------------------%>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center" class="style1">
                                <asp:Label ID="lblLWHU72120" runat="server" >0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center" class="style1">
                                <asp:Label ID="lblLWHUgreater120" runat="server" >0</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                W/O H Code
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWOHU18" runat="server" >0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWOHU24" runat="server" >0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWOHU72" runat="server" >0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWOHU120" runat="server" >0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWOHU72120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWOHUgreater120" runat="server">0</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="2" style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                Outstation
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                With H Code
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWHU18" runat="server" >0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWHU24" runat="server" >0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWHU72" runat="server" >0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWHU120" runat="server" >0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWHU72120" runat="server" >0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWHUgreater120" runat="server">0</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                W/O H Code
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWOHU18" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWOHU24" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWOHU72" runat="server" >0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWOHU120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWOHU72120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWOHUgreater120" runat="server">0</asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" style="border: 1px solid #b5b5b6!important; color: Black; border-collapse: collapse;"
                        cellpadding="0" cellspacing="0" id="tblUnitDtl" runat="server">
                        <tr>
                            <th rowspan="6" style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%;"
                                class="th-cls" align="center">
                                Unit
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%;" class="th-cls"
                                align="center">
                                Complaint Type
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 14%;" class="th-cls"
                                align="center">
                                H Code
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 14%;" class="th-cls"
                                align="center">
                                &lt;=18 Hrs
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 14%;" class="th-cls"
                                align="center">
                                18 &lt; Hrs &gt;=24
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 14%;" class="th-cls"
                                align="center">
                                24 &lt; Hrs &gt;=48
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 14%;" class="th-cls"
                                align="center">
                                48 &lt; Hrs &gt;=72
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%;" class="th-cls"
                                align="center">
                                72 &lt; Hrs &gt;=120
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%;" class="th-cls"
                                align="center">
                                >120 Hrs
                            </th>
                        </tr>
                        <tr>
                            <td rowspan="2" style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                Local
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                With H Code
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="txtLWH18" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="txtLWH24" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="txtLWH72" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="txtLWH120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="txtLWH72120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="txtLWHgreater120" runat="server">0</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                W/O H Code
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="txtLWOH18" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="txtLWOH24" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="txtLWOH72" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="txtLWOH120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="txtLWOH72120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="txtLWOHgreater120" runat="server">0</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="2" style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                Outstation
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                With H Code
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="txtOWH18" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="txtOWH24" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="txtOWH72" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="txtOWH120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="txtOWH72120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="txtOWHgreater120" runat="server">0</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                W/O H Code
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="txtOWOH18" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="txtOWOH24" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="txtOWOH72" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="txtOWOH120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="txtOWOH72120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="txtOWOHgreater120" runat="server">0</asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" style="border: 1px solid #b5b5b6!important; color: Black; border-collapse: collapse;"
                        cellpadding="0" cellspacing="0" id="tblAmount" runat="server">
                        <tr>
                            <th rowspan="6" style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%;"
                                class="th-cls" align="center">
                                Amount
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%;" class="th-cls"
                                align="center">
                                Complaint Type
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 14%;" class="th-cls"
                                align="center">
                                H Code
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 14%;" class="th-cls"
                                align="center">
                                &lt;=18 Hrs
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 14%;" class="th-cls"
                                align="center">
                                18 &lt; Hrs &gt;=24
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 14%;" class="th-cls"
                                align="center">
                                24 &lt; Hrs &gt;=48
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 14%;" class="th-cls"
                                align="center">
                                48 &lt; Hrs &gt;=72
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%;" class="th-cls"
                                align="center">
                                72 &lt; Hrs &gt;=120
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%;" class="th-cls"
                                align="center">
                                > 120 Hrs
                            </th>
                        </tr>
                        <tr>
                            <td rowspan="2" style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                Local
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                With H Code
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWH18" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWH24" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWH72" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWH120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWH72120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWHgreater120" runat="server">0</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                W/O H Code
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWOH18" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWOH24" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWOH72" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWOH120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWOH72120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWOHgreater120" runat="server">0</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="2" style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                Outstation
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                With H Code
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWH18" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWH24" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWH72" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWH120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWH72120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWHgreater120" runat="server">0</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                W/O H Code
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWOH18" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWOH24" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWOH72" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWOH120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWOH72120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWOHgreater120" runat="server">0</asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" style="border: 1px solid #b5b5b6!important; color: Black; border-collapse: collapse;"
                        cellpadding="0" cellspacing="0" id="Table1" runat="server">
                        <%-- <tr><td rowspan="4" style="padding:2px 5px; border:1px solid #c4c5c6;" align="center"><b>Repair Charges</b></td></tr>--%>
                        <tr>
                            <th rowspan="6" style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%;"
                                class="th-cls" align="center">
                                Replacement Charges
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%;" class="th-cls"
                                align="center">
                                Complaint Type
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 14%;" class="th-cls"
                                align="center">
                                H Code
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 14%;" class="th-cls"
                                align="center">
                                &lt;=18 Hrs
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 14%;" class="th-cls"
                                align="center">
                                18 &lt; Hrs &gt;=24
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 14%;" class="th-cls"
                                align="center">
                                24 &lt; Hrs &gt;=48
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 14%;" class="th-cls"
                                align="center">
                                48 &lt; Hrs &gt;=72
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%;" class="th-cls"
                                align="center">
                                72 &lt; Hrs &gt;=120
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%;" class="th-cls"
                                align="center">
                                > 120 Hrs
                            </th>
                        </tr>
                        <tr>
                            <td rowspan="2" style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                Local
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                With H Code
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWHARep18" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWHARep24" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWHARep72" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWHARep120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWHARep72120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWHARepgreater120" runat="server">0</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                W/O H Code
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWOHARep18" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWOHARep24" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWOHARep72" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWOHARep120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWOHARep72120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWOHARepgreater120" runat="server">0</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="2" style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                Outstation
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                With H Code
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWHARep18" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWHARep24" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWHARep72" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWHARep120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWHARep72120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWHARepgreater120" runat="server">0</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                W/O H Code
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWOHARep18" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWOHARep24" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWOHARep72" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWOHARep120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWOHARep72120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWOHARepgreater120" runat="server">0</asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" style="border: 1px solid #b5b5b6!important; color: Black; border-collapse: collapse;"
                        cellpadding="0" cellspacing="0" id="Table2" runat="server">
                        <tr>
                            <th rowspan="6" style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%;"
                                class="th-cls" align="center">
                                Unit
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%;" class="th-cls"
                                align="center">
                                Complaint Type
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 14%;" class="th-cls"
                                align="center">
                                H Code
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 14%;" class="th-cls"
                                align="center">
                                &lt;=18 Hrs
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 14%;" class="th-cls"
                                align="center">
                                18 &lt; Hrs &gt;=24
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 14%;" class="th-cls"
                                align="center">
                                24 &lt; Hrs &gt;=48
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 14%;" class="th-cls"
                                align="center">
                                48 &lt; Hrs &gt;=72
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%;" class="th-cls"
                                align="center">
                                72 &lt; Hrs &gt;=120
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%;" class="th-cls"
                                align="center">
                                &nbsp;&gt;=120 Hrs
                            </th>
                        </tr>
                        <tr>
                            <td rowspan="2" style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                Local
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                With H Code
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWHURep18" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWHURep24" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWHURep72" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWHURep120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWHURep72120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWHURepgreater120" runat="server">0</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                W/O H Code
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWOHURep18" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWOHURep24" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWOHURep72" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWOHURep120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWOHURep72120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWOHURepgreater120" runat="server">0</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="2" style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                Outstation
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                With H Code
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWHURep18" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWHURep24" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWHURep72" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWHURep120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWHURep72120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWHURepgreater120" runat="server">0</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                W/O H Code
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWOHURep18" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWOHURep24" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWOHURep72" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWOHURep120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWOHURep72120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWOHURepgreater120" runat="server">0</asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" style="border: 1px solid #b5b5b6!important; color: Black; border-collapse: collapse;"
                        cellpadding="0" cellspacing="0" id="Table3" runat="server">
                        <tr>
                            <th rowspan="6" style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%;"
                                class="th-cls" align="center">
                                Amount
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%;" class="th-cls"
                                align="center">
                                Complaint Type
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 14%;" class="th-cls"
                                align="center">
                                H Code
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 14%;" class="th-cls"
                                align="center">
                                &lt;=18 Hrs
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 14%;" class="th-cls"
                                align="center">
                                18 &lt; Hrs &gt;=24
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 14%;" class="th-cls"
                                align="center">
                                24 &lt; Hrs &gt;=48
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 14%;" class="th-cls"
                                align="center">
                                48 &lt; Hrs &gt;=72
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%;" class="th-cls"
                                align="center">
                                72 &lt; Hrs &gt;=120
                            </th>
                            <th style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 10%;" class="th-cls"
                                align="center">
                                > 120 Hrs
                            </th>
                        </tr>
                        <tr>
                            <td rowspan="2" style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                Local
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                With H Code
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWHUARep18" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWHUARep24" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWHUARep72" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWHUARep120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWHUARep72120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWHUARepgreater120" runat="server">0</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                W/O H Code
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWOHUARep18" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWOHUARep24" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWOHUARep72" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWOHUARep120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWOHUARep72120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblLWOHUARepgreater120" runat="server">0</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="2" style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                Outstation
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                With H Code
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWHUARep18" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWHUARep24" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWHUARep72" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWHUARep120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWHUARep72120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWHUARepgreater120" runat="server">0</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                W/O H Code
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWOHUARep18" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWOHUARep24" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWOHUARep72" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWOHUARep120" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWOHUARep12072" runat="server">0</asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="center">
                                <asp:Label ID="lblOWOHUARepgreater120" runat="server">0</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 75%;" colspan="6"
                                align="right">
                                <b>Sub Total</b>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 15%; text-align: right!important;"
                                colspan="2">
                                <asp:Label ID="lblTotalAmount" runat="server" Font-Bold="true" Text="0.00000" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 75%;" colspan="5"
                                align="right">
                                <asp:Label runat="server" Font-Bold="true" ID="lblServicetax"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:CheckBox ID="chkServicetaxoption" runat="server" Text="Add/Remove" Checked="true"
                                    AutoPostBack="true" />
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 15%; text-align: right!important;">
                                <asp:Label ID="lblServiceChargesBracks" runat="server" Text="" ForeColor="Green"></asp:Label>
                                <asp:Label ID="lblTax" runat="server" Text="0.00000" Font-Bold="true" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="right" colspan="2">
                                <b>Total Repair</b>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="left" colspan="2">
                                <asp:Label ID="lbltotalRepair" runat="server" Font-Bold="true" Text="0.00000" ForeColor="Red"></asp:Label>
                            </td>
                            <td style="padding: 2px 1px; border: 1px solid #c4c5c6;" align="right" colspan="2">
                                <b>Total Replacement</b>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="left" colspan="0">
                                <asp:Label ID="lbltotalreplacement" runat="server" Font-Bold="true" Text="0.00000"
                                    ForeColor="Red"></asp:Label>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6;" align="right" colspan="0">
                                <b><span lang="EN-IN">Grand </span>Total</b>
                            </td>
                            <td style="padding: 2px 5px; border: 1px solid #c4c5c6; width: 15%; text-align: right!important;">
                                <asp:Label ID="lblTAmount" runat="server" Font-Bold="true" Text="0.00000" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <div style="height: 10px;">
                    </div>
                    <table width="90%" class="table_border" cellpadding="0" cellspacing="0" id="tblEmptyMessage"
                        runat="server" visible="true" style="display: none;">
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblEmptyMessage" runat="server" Text="<b>Please Click on search to generate report.</b>"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:HiddenField ID="hdnRawUrl" Value="" runat="server" />
                <%--                <asp:GridView ID="grdsummery" Visible="false" runat=server></asp:GridView>--%>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnsummery" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
