<%@ Page Title="" Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true" CodeFile="SimsInvoiceFinWise.aspx.cs" Inherits="SIMS_Reports_SimsInvoiceFinWise" %>
<%@ Import Namespace="System.Linq" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
<script type="text/javascript" src="../../scripts/jquery-1.3.1.min.js"> </script>
<link href="../../css/Invoice.css" rel="stylesheet" type="text/css" />  
    
    <script language="javascript" type="text/javascript">
        function HideShowDiv() {
            document.getElementById('dvid').style.display = 'block';
        }
    </script>
    <asp:UpdatePanel ID="pnlInvoice" runat="server">
        <ContentTemplate>
            <table width="90%">
                    <tr>
                        <td align="center">
                            <b>INVOICE - FINANCIAL YEAR WISE</b>
                        </td>
                    </tr>
                    </table>
              <table width="90%">
                    <tr>
                       <td colspan="4" align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["SIMSAjaxLoadingImageName"]%>" alt="" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        </td>
                    </tr>
                    <tr id="trRB" runat="server">
                        <td align="right" style="width:10%">
                            Region 
                        </td>
                        <td style="width:15%">
                            <asp:DropDownList ID="ddlRegion" runat="server" Width="214px" CssClass="simpletxt1"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" ValidationGroup="editt">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            Branch
                        </td>
                        
                        <td>
                            <asp:DropDownList ID="ddlBranch" runat="server" Width="214px" CssClass="simpletxt1"
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
                            Financial Year
                           </td>
                           <td>
                            <asp:DropDownList ID="ddlFinYear" runat="server" CssClass="simpletxt1" Width="214px">
                                <asp:ListItem Value="2015">2015 - 2016</asp:ListItem>
                                <asp:ListItem Value="2014">2014 - 2015</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button Width="70px" Text="Search" CssClass="btn" ValidationGroup="editt" CausesValidation="true"
                                ID="btnSearch" runat="server" OnClick="btnSearch_Click" />
                            &nbsp;
                            <asp:Button Width="70px" Text="Clear" CssClass="btn" ValidationGroup="editt" CausesValidation="false"
                                ID="btncancle" runat="server" OnClick="btncancle_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                        </td>
                    </tr>
                </table>
                
                <div id="dvInvoicecontent" runat="server">
                <table width='90%' border='0' cellpadding='0' cellspacing='0' id="tblInvoiceAscdetails" runat="server" visible="false">
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
                                    <td class="invoicedtsl">:&nbsp; 
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
                                    <td class="invoicedtsl1">
                                    </td>
                                    <td class="invoicedtsl">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                
                <div id="DivInvoiceDtls" visible="false" runat="server">
                <div style="padding-bottom:8px;">
                 <asp:Button ID="btnDwn" runat="server" Text="Export To Excel" CssClass="btn" onclick="btnDwn_Click" />
                    &nbsp;</div>
                    <div id="dvid" style="display:none; z-index:0; background-color:Transparent; position:absolute; padding-top:0px;">
                    <table style='border:1px solid #b5b5b6!important;color:Black; border-collapse:collapse;' border='1' cellpadding='3' cellspacing='0'>
                    <tr><td style='background-color:#60A3AC; border-right-width:0; border-right-color:#60A3AC;'>&nbsp;</td></tr>
                    <tr><td style='background-color:#60A3AC; border-left:0px none; border-right-width:0; border-right-color:#60A3AC;'><b>Description</b></td></tr>
                    <tr><td style='background-color:#fff'><b>Fixed Compensation</b></td></tr>
                    <tr><td style='background-color:#9FDA65'><b>Calls- Local(Other Products)</b></td></tr>
                    <tr><td style='background-color:#fff'>Local Complaints (within city / town limits)</td></tr>
                    <tr><td style='background-color:#fff'>Institutional Customers /Trade Partners visit where Quantity of Products are >4 per call</td></tr>
                    <tr><td style='background-color:#E3EFF0'><b>Sub Total</b></td></tr>
                    <tr><td style='background-color:#9FDA65'><b>Calls-Local(Geysers)</b></td></tr>
                    <tr><td style='background-color:#fff'>Local Complaints (within city / town limits)</td></tr>
                    <tr><td style='background-color:#fff'>Institutional Customers /Trade Partners visit where Quantity of Products are >4 per call</td></tr>
                    <tr><td style='background-color:#E3EFF0'><b>Sub Total</b></td></tr>
                    <tr><td style='background-color:#9FDA65'><b>Calls-Outstation</b></td></tr>
                    <tr><td style='background-color:#fff'>Out of Pocket Allowance (DA Charges, Lunch & Tea etc.) for journeys with same day return</td></tr>
                    <tr><td style='background-color:#fff'>Boarding & Lodging Expenses for overnight stay</td></tr>
                    <tr><td style='background-color:#fff'>OutStation Complaints - Geysers(within city / town limits)</td></tr>
                    <tr><td style='background-color:#fff'>OutStation Complaints - Other Products(within city / town limits)</td></tr>
                    <tr><td style='background-color:#E3EFF0'><b>Sub Total</b></td></tr>
                    <tr><td style='background-color:#9FDA65'><b>Demo Charges(FP Only)</b></td></tr>
                    <tr><td style='background-color:#fff'>FOOD PROCESSOR</td></tr>
                    <tr><td style='background-color:#9FDA65'><b>Conveyance Charges(Outstation Travel)</b></td></tr>
                    <tr><td style='background-color:#fff'>Travel by either State Roadways Bus or Train Non A.C Sleeper Class</td></tr>
                    <tr><td style='background-color:#fff'>Travel by Two-wheelers,Conveyance Charge @ 3.00 per Km/Max 100 KM</td></tr>
                    <tr><td style='background-color:#fff'>Local Conveyance (Only when the Journey has been made by Bus or Train)</td></tr>
                    <tr><td style='background-color:#E3EFF0'><b>Sub Total</b></td></tr>
                    <tr><td style='background-color:#fff'><b>Over All Sub Total</b></td></tr>
                    <tr><td style='background-color:#fff'><b>Service Tax </b></td></tr>
                    <tr><td style='background-color:#fff'><b>Total</b></td></tr>
                    </table>
                    </div>
                    <asp:Panel ID="Panel1" ScrollBars="Horizontal" onmouseover="HideShowDiv()" Width="950px" runat="server">
                    <asp:Literal ID="litDataDetails" runat="server"></asp:Literal>
                    </asp:Panel>
                
                
                </div>
                
                <table width="100%" class="table_border" cellpadding="0" cellspacing="0" id="tblEmptyMessage"
                    runat="server" visible="true">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblEmptyMessage" runat="server" Text="<b>Please Click on search to generate report.</b>"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <%--</asp:Panel>--%>
            <asp:Label ID="Label1" runat="server"></asp:Label>
            <asp:HiddenField ID="hdnRawUrl" Value="" runat="server" />
        </ContentTemplate>
        <Triggers><asp:PostBackTrigger ControlID="btnDwn" /></Triggers>
    </asp:UpdatePanel>
</asp:Content>

