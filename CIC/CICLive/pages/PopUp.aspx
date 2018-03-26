<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopUp.aspx.cs" Inherits="pages_PopUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Details</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/global.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        function fnOpenCommPopUp() {
            funCommLog("" + document.getElementById('hdnComNo').value + "", document.getElementById('hdnBaseLineId').value);
        }
        function fnOpenCommPopUp() {
            funCommLog("" + document.getElementById('hdnComNo').value + "", document.getElementById('hdnSplitNo').value);
        }
        function fnOpenHistPopUp() {
            funHistoryLog("" + document.getElementById('hdnComNo').value + "", document.getElementById('hdnSplitNo').value);
        }

        function fnOpenViewDefectPopUp() {
            funDefectEntry("" + document.getElementById('hdnComNo').value + "", document.getElementById('hdnSplitNo').value);
        }

        function fnOpenSpareConsumption() {
            var varComplaintNo = document.getElementById('hdnSplitNo').value;
            if (document.getElementById('hdnSplitNo').value.length == 1) {
                varComplaintNo = "0" + document.getElementById('hdnSplitNo').value;
            }
            varComplaintNo = document.getElementById('hdnComNo').value + "/" + varComplaintNo;
            //alert(varComplaintNo);
            var strUrl = '../SIMS/Pages/SpareActivityLog.aspx?CompNo=' + varComplaintNo;
            window.open(strUrl, 'History', 'height=550,width=850,left=20,scrollbars=1,top=30,Location=0');
        }
        // added by bhawesh 11 jan 12
        function fnOpenMDComm() {

            var varComplaintNo = document.getElementById('hdnSplitNo').value;
            if (document.getElementById('hdnSplitNo').value.length == 1) {
                varComplaintNo = "0" + document.getElementById('hdnSplitNo').value;
            }
            varComplaintNo = document.getElementById('hdnComNo').value + "/" + varComplaintNo;
            //alert(varComplaintNo);
            var strUrl = '../Pages/MDCommunicationLog.aspx?CompNo=' + varComplaintNo;
            window.open(strUrl, 'History', 'height=550,width=850,left=20,scrollbars=1,top=30,Location=0');
        }
    
    </script>

    <script language="javascript" src="../scripts/Common.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td width="249" background="../images/headerBg.jpg">
                    <img src="../images/small-logo.jpg">
                </td>
                <td width="651" align="right" background="../images/headerBg.jpg">
                    <img src="../images/small-cic-txt.jpg" width="267" height="86">
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 22px; background-color: #396870">
                </td>
            </tr>
        </table>
        <br />
        <table width="100%">
            <tr bgcolor="white">
                <td class="headingred">
                    Complaint Details<asp:HiddenField ID="hdnBaseLineId" runat="server" />
                    <asp:HiddenField ID="hdnComNo" runat="server" />
                    <asp:HiddenField ID="hdnSplitNo" runat="server" />
                </td>
                <td align="right">
                    <a href="javascript:void(0)" class="links" onclick="window.close();">Close</a>
                </td>
            </tr>
            <tr>
                <td style="padding: 10px" align="center" colspan="2" class="bgcolorcomm">
                    <table width="100%" border="0" cellpadding="1" cellspacing="0">
                        <tr align="left" class="popAltGridbgcolor" style="font-weight: bold;">
                            <td width="25%" style="padding-left: 35px">
                                Complaint Status:
                            </td>
                            <td width="25%">
                                <asp:Label ID="lblCallStatus" runat="server" Text=""></asp:Label>
                            </td>
                            <td width="25%" style="padding-left: 35px">
                                Current Stage:
                            </td>
                            <td width="25%">
                                <asp:Label ID="lblStage" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popGridbgcolor">
                            <td width="25%" style="padding-left: 35px">
                                Complaint Ref No:
                            </td>
                            <td width="25%">
                                <asp:Label ID="lblComRefNo" runat="server" Text=""></asp:Label>
                                <asp:LinkButton ID="LbtnoldNo" runat="server" Text="" OnClientClick="return false;"></asp:LinkButton>
                            </td>
                            <td width="25%" style="padding-left: 35px">
                                Product Serial No.:
                            </td>
                            <td width="25%">
                                <asp:Label ID="lblProductSerialNo" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor" id="trNoFrame" runat="server">
                            <td align="left" style="padding-left: 35px">
                                Complaint Log date:
                            </td>
                            <td>
                                <asp:Label ID="lblComLogDate" runat="server" Text=""></asp:Label>
                            </td>
                            <td width="25%" style="padding-left: 35px">
                                Product Division:
                            </td>
                            <td>
                                <asp:Label ID="lblProductDivision" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                No Of Frame(s):
                            </td>
                            <td>
                                <asp:Label ID="lblFrames" runat="server" Text=""></asp:Label>
                                <asp:HiddenField ID="hdnProductLine_Sno" runat="server" />
                                <asp:HiddenField ID="hdnComplaintRef_No" runat="server" />
                                <asp:HiddenField ID="hdnSplit" runat="server" />
                            </td>
                            <td style="padding-left: 35px">
                                Product:
                            </td>
                            <td>
                                &nbsp;<asp:Label ID="lblproduct" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Product Line:
                            </td>
                            <td>
                                <asp:Label ID="lblProductLine" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                Mode Of Receipt:
                            </td>
                            <td>
                                <asp:Label ID="lblModeReceipt" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Language:
                            </td>
                            <td>
                                <asp:Label ID="lblLanguage" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                Quantity:
                            </td>
                            <td>
                                <asp:Label ID="lblquantity" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Nature Of Complaint:
                            </td>
                            <td>
                                <asp:Label ID="lblNatureOfComp" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px" align="left">
                                Dealer Name :
                            </td>
                            <td>
                                <asp:Label ID="lbldealername" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px">
                                Invoice Date:
                            </td>
                            <td>
                                <asp:Label ID="lblInvoiceDate" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                Invoice No:
                            </td>
                            <td>
                                <asp:Label ID="lblInvoiceNo" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Service Invoice Date:
                            </td>
                            <td>
                                <asp:Label ID="lblServiceDate" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                Service Invoice Number:
                            </td>
                            <td>
                                <asp:Label ID="lblServiceNumber" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Service Amount:
                            </td>
                            <td>
                                <asp:Label ID="lblServiceAmt" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                Warranty Status(Y/N):
                            </td>
                            <td>
                                <asp:Label ID="lblWarrantyStatus" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <%-- Code added by Vikas on 15 May 2013--%>
                        <tr align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Source Of Complaint:
                            </td>
                            <td>
                                <asp:Label ID="lblSOC" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                Type Of Complaint:
                            </td>
                            <td>
                                <asp:Label ID="lblTOC" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <%--END Vikas--%>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <table width="100%">
            <tr bgcolor="white">
                <td class="headingred">
                    Customer Details
                </td>
            </tr>
            <tr>
                <td style="padding: 10px" align="center" colspan="2" class="bgcolorcomm">
                    <table width="100%" border="0" cellpadding="1" cellspacing="0">
                        <tr align="left" class="popGridbgcolor">
                            <td width="25%" style="padding-left: 35px">
                                Customer Name:
                            </td>
                            <td width="25%">
                                <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>
                            </td>
                            <td width="25%" style="padding-left: 35px">
                            </td>
                            <td width="25%">
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td align="left" style="padding-left: 35px">
                                Pri. Phone:
                            </td>
                            <td>
                                <asp:Label ID="lblPriPhone" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Alt. Phone:
                            </td>
                            <td>
                                <asp:Label ID="lblAltPhone" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                Email:
                            </td>
                            <td>
                                <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Extension:
                            </td>
                            <td>
                                <asp:Label ID="lblExt" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                Fax:
                            </td>
                            <td>
                                <asp:Label ID="lblFax" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Address1:
                            </td>
                            <td>
                                <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                Address2:
                            </td>
                            <td>
                                <asp:Label ID="lblAddress2" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Landmark:
                            </td>
                            <td>
                                <asp:Label ID="lblLandmark" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                Country:
                            </td>
                            <td>
                                <asp:Label ID="lblCountry" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                State:
                            </td>
                            <td>
                                <asp:Label ID="lblState" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                City:
                            </td>
                            <td>
                                <asp:Label ID="lblCity" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" class="popAltGridbgcolor">
                            <td style="padding-left: 35px" align="left">
                                Pin Code:
                            </td>
                            <td>
                                <asp:Label ID="lblPinCode" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="padding-left: 35px">
                                Company Name:
                            </td>
                            <td>
                                <asp:Label ID="lblCompany" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <table width="100%" runat="server" id="tblDefectDetails">
            <tr bgcolor="white">
                <td class="headingred">
                    Defect details
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <%--id="dvGrid" style="width: 870px; overflow: auto;">--%>
                        <!-- Communication Details for Complaint Ref No Listing   -->
                        <asp:GridView PageSize="13" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                            HeaderStyle-CssClass="fieldNamewithbgcolor" GridLines="both" AllowPaging="True"
                            AutoGenerateColumns="False" ID="gvHistory" runat="server" OnPageIndexChanging="gvHistory_PageIndexChanging"
                            Width="99%" HorizontalAlign="Left" Style="margin-top: 0px">
                            <RowStyle CssClass="gridbgcolor" />
                            <Columns>
                                <asp:BoundField DataField="Sno" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                    HeaderText="SNo">
                                    <HeaderStyle HorizontalAlign="Left" Width="30px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <%--<asp:BoundField DataField="DEF_CAT_CODE " ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left" HeaderText="Code">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DefectCategory_Sno" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left" HeaderText="Defect Category Number">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>--%>
                                <asp:BoundField DataField="DefectCategory" ItemStyle-Width="170px" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Defect Category">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Defect_Desc" ItemStyle-Width="250px" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Defect Description">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="REMARK" ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Remarks">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                            <EmptyDataTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["UserMessage"]%>" alt="" /><b>No Records
                                    found.</b>
                            </EmptyDataTemplate>
                            <HeaderStyle CssClass="fieldNamewithbgcolor" />
                            <AlternatingRowStyle CssClass="fieldName" />
                        </asp:GridView>
                        <!-- End Communication Details for Complaint Ref No Listing -->
                    </div>
                    <!-- Attribute details in read only mode -->
                </td>
            </tr>
            <tr>
                <td>
                    <table id="tbViewAttribute" runat="server" width="100%" visible="false">
                        <tr>
                            <td>
                                <tr>
                                    <td colspan="4" height="20" class="headingred" align="left" valign="bottom" style="border-bottom: 1px solid #396870">
                                        <b>Attribute Details</b>
                                    </td>
                                </tr>
                                <tr id="trAvrV" runat="server" visible="false">
                                    <td align="right" valign="top">
                                        Avr SrNo.:<font color="red">*</font>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtAvrV" runat="server" Enabled="false" Width="250px" CssClass="txtboxtxt"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="trApplicationV" runat="server" visible="false">
                                    <td align="right" valign="top">
                                        Application:<font color="red">*</font>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtApplicationV" runat="server" Enabled="false" Width="250px" CssClass="txtboxtxt"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="trEXCISESERALNOV" runat="server" visible="false">
                                    <td align="right" valign="top">
                                        Excies Serial No.:<font color="red">*</font>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtEXCISESERALNOV" runat="server" Enabled="false" Width="250px"
                                            CssClass="txtboxtxt"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="trSerialNoV" runat="server" visible="false">
                                    <td align="right" valign="top">
                                        Machine No.:<font color="red">*</font>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtSerialNoV" runat="server" Enabled="false" Width="250px" CssClass="txtboxtxt"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="trLOADV" runat="server" visible="false">
                                    <td align="right" valign="top">
                                        Load:<font color="red">*</font>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtLOADV" runat="server" Enabled="false" Width="250px" CssClass="txtboxtxt"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="trFrameV" runat="server" visible="false">
                                    <td align="right" valign="top">
                                        Frame:<font color="red">*</font>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtFrameV" runat="server" Enabled="false" Width="250px" CssClass="txtboxtxt"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="trHPV" runat="server" visible="false">
                                    <td align="right" valign="top">
                                        HP/Pole:<font color="red">*</font>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtHPV" runat="server" Enabled="false" Width="250px" CssClass="txtboxtxt"></asp:TextBox>
                                    </td>
                                </tr>
                                <%--Added By Ashok  9 April 14--%>
                                <tr id="trIDV" runat="server" visible="false">
                                    <td align="right" valign="top">
                                        Instrument Details (Current,Voltage,Capacity Rating & Output Range):<font color="red">*</font>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtInstrumentDetailsV" Enabled="false" runat="server" MaxLength="200" Width="250px"
                                            CssClass="txtboxtxt"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr id="trIMNV" runat="server" visible="false">
                                    <td align="right" valign="top">
                                        Instrument Manufacturer Name:<font color="red">*</font>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtInstrumentManufacturerNameV" Enabled="false" MaxLength="200" runat="server" Width="250px"
                                            CssClass="txtboxtxt"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr id="trAINV" runat="server" visible="false">
                                    <td align="right" valign="top">
                                        Application Instrument Name:<font color="red">*</font>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtApplicationInstrumentNameV" Enabled="false" runat="server" MaxLength="200" Width="250px"
                                            CssClass="txtboxtxt"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr id="trRatingV" runat="server" visible="false">
                                    <td align="right" valign="top">
                                        Rating:<font color="red">*</font>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtRatingV" runat="server" Enabled="false" Width="250px" CssClass="txtboxtxt"></asp:TextBox>
                                    </td>
                                </tr>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <table width="100%" border="0" id="tblLink" runat="server">
            <tr>
                <td align="center" width="20%">
                    <a href="javascript:void(0);" class="links" onclick="fnOpenCommPopUp();">Communication
                        History </a>
                </td>
                <td align="center" width="20%">
                    <a id="lnkmd" runat="server" href="javascript:void(0);" class="links" onclick="fnOpenMDComm();">
                        MD Escalation Log </a>
                </td>
                <td align="center" width="20%">
                    <a href="javascript:void(0);" class="links" onclick="fnOpenHistPopUp();">Stage</a>
                </td>
                <td align="center" width="20%">
                    <a href="javascript:void(0);" class="links" onclick="fnOpenSpareConsumption();">Spare
                        Consumption & Activity Details</a>
                </td>
                <%--  <td>
                    <a href="javascript:void(0);" class="links" onclick="fnOpenViewDefectPopUp();">View
                        Defect</a>
                </td>--%>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
