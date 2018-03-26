<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SIMSSpareConsumption.aspx.cs"
    Inherits="SIMS_Admin_SIMSSpareConsumption" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Spare Consumption</title>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/global.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../../scripts/Common.js"></script>

    <script type="text/javascript">
        // The below function is used for check whether press key is number or not
        // Only allow numbers
        function Confirm() {
            var hdnval = document.getElementById("<%=hdnActivityCharges.ClientID %>").value;
            if (hdnval > 0) {
                if (confirm('Added Activity will be removed \n Do you want to continue?')) {
                    document.getElementById('<%=btnConfirm.ClientID %>').click();
                    return true;
                }
                return true;
            }
            else
                document.getElementById('<%=btnConfirm.ClientID %>').click();
        }

        function checkNumberOnly(e) {
            var KeyID;
            if (navigator.appName == "Microsoft Internet Explorer") {
                KeyID = e.keyCode;
            }
            else {
                KeyID = e.charCode;
            }
            if (window.event) {
                if (window.event.keyCode == 13) {
                    return false;
                }
            }
            //            if(KeyID==13) 
            //            { 
            //                return false; 
            //            } 
            if ((KeyID > 47 && KeyID < 58) || (KeyID == 32) || (KeyID == 8)) {
            }
            else {
                alert("Please enter numbers only.");
                return false;
            }
        }
        // The below function is used for check whether press key is number and char or not
        // Only allow number and characters
        function checkNumberCharOnly(e) {
            var KeyID;
            if (navigator.appName == "Microsoft Internet Explorer") {
                KeyID = e.keyCode;
            }
            else {
                KeyID = e.charCode;
            }
            if (window.event) {
                if (window.event.keyCode == 13) {
                    return false;
                }
            }
            //            if(KeyID==13) 
            //            { 
            //                return false; 
            //            } 
            if ((KeyID > 47 && KeyID < 58) || (KeyID == 32) || (KeyID == 8)) {
            }
            else {
                alert("Please enter numbers only.");
                return false;
            }
        }
        // The below function is used for check whether press key is valid character or not
        // Only allow characters
        function checkCharacterOnly(e) {

            var KeyID;
            if (navigator.appName == "Microsoft Internet Explorer") {
                KeyID = e.keyCode;
            }
            else {
                KeyID = e.charCode;
            }
            if (window.event) {
                if (window.event.keyCode == 13) {
                    return false;
                }
            }
            if (KeyID == 13) {
                return false;
            }
            if ((KeyID >= 65 && KeyID < 91) || ((KeyID > 96 && KeyID < 123)) || (KeyID == 32) || (KeyID == 46) || (KeyID == 8)) {

            }
            else {
                alert("Please enter alphabets only.");
                return false;
            }

        }
        // The below function is used for check whether press key is A+, B+, O+, AB+, A-, B-, O-, AB-
        function checkCharBloodGroup(e) {
            var KeyID;
            if (navigator.appName == "Microsoft Internet Explorer") {
                KeyID = e.keyCode;
            }
            else {
                KeyID = e.charCode;
            }
            if (window.event) {
                if (window.event.keyCode == 13) {
                    return false;
                }
            }
            if (KeyID == 13) {
                return false;
            }

            if ((KeyID == 65) || (KeyID == 66) || (KeyID == 79) || (KeyID == 97) || (KeyID == 98) || (KeyID == 111) || (KeyID == 45) || (KeyID == 43) || (KeyID == 8)) {

            }
            else {
                alert("Please enter A+, B+, O+, AB+, A-, B-, O-, AB- only.");
                return false;
            }

        }

        //This function is used for disable Enter

        function DisableEnterKey(e) {
            var KeyID;
            if (navigator.appName == "Microsoft Internet Explorer") {
                KeyID = e.keyCode;
            }
            else {
                KeyID = e.charCode;
            }
            if (window.event) {
                if (window.event.keyCode == 13) {
                    return false;
                }
            }
            if (KeyID == 13) {
                return false;
            }
            return true;
        }
        function checkExperience(e) {
            var KeyID;

            if (navigator.appName == "Microsoft Internet Explorer") {
                KeyID = e.keyCode;
            }
            else {
                KeyID = e.charCode;
            }
            if (window.event) {
                if (window.event.keyCode == 13) {
                    return false;
                }
            }
            if (KeyID == 13) {
                return false;
            }
            if ((KeyID > 47 && KeyID < 58) || (KeyID == 46) || (KeyID == 8)) {
            }
            else {
                alert("Please enter numbers only.");
                return false;
            }

        }
        function CheckMaxLength(obj, len, e) {

            var KeyID;
            if (navigator.appName == "Microsoft Internet Explorer") {
                KeyID = e.keyCode;
            }
            else {
                KeyID = e.charCode;
            }
            //alert(obj.value.length);
            if (obj.value.length > len) {
                if ((KeyID == 8) || (KeyID == 46)) {
                }
                else {
                    ;
                    return false;
                }
            }
        }
        //added by sandeep:29/12/2010
        function OpenActivityPop(url) {
            newwindow = window.open(url, 'name', 'height=400,width=300,scrollbars=1,resizable=no,top=1,left=1');
            if (window.focus) {
                newwindow.focus()
            }
            return false;
        }

        function CheckQuantity(chk) {
            var str = chk.id;
            var dd = str.split("_");
            var num = (dd[dd.length - 2]);
            var chkConfirm = document.getElementById("gvActivityCharges_" + num + "_chkActivityConfirm");

            var Quantity = document.getElementById("gvActivityCharges_" + num + "_txtActualQty").value;

            if (chkConfirm.checked == true && Quantity == '') {

                alert('Enter quantity.');
                chkConfirm.checked = false;
            }
        }
       
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="600" runat="server"
            EnablePageMethods="true" />
        <asp:UpdatePanel ID="updatepnl" runat="server">
            <ContentTemplate>

                <script type="text/javascript">
                    function checkDate(sender, args) {
                        //create a new date var and set it to the
                        //value of the senders selected date
                        var selectedDate = new Date();
                        selectedDate = sender._selectedDate;
                        //create a date var and set it's value to today
                        var todayDate = new Date();
                        var mssge = "";

                        if (selectedDate > todayDate) {
                            //set the senders selected date to today
                            sender._selectedDate = todayDate;
                            //set the textbox assigned to the cal-ex to today
                            sender._textbox.set_Value(sender._selectedDate.format(sender._format));
                            //alert the user what we just did and why
                            alert("Date Cannot be greater than present date");
                        }
                    }
                    function ClaimAmount() {

                        document.getElementById("ctl00_MainConHolder_gvComm_ctl02_lblClaimAmount").value = document.getElementById("ctl00_MainConHolder_gvComm_ctl02_txtConsumedQty").value;

                    }


                    function getNumeric_only(strvalue) {
                        if (!(event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                            event.returnValue = false;
                            alert("Please enter numbers only.");

                        }
                    }
                    function ValidateAdvices() {
                        if (hdnProposedSpares.Value != "") {
                            alert("Advice has been generated for spares: " + hdnProposedSpares.Value + ".");
                        }
                        //return true;
                        alert(hdnProposedSpares.Value);
                    }

                    function CalculateCurrentAmount(txtAmount, txtQty, lblRate, lblModifiedRate) {
                        var modrate = lblModifiedRate;
                        document.getElementById(txtAmount).value = document.getElementById(txtQty).value * document.getElementById(lblRate).innerHTML;
                        if (modrate != 0)
                            document.getElementById(txtAmount).value = document.getElementById(txtAmount).value * modrate / 100;


                        if (document.getElementById(txtAmount).value == "NaN") {
                            document.getElementById(txtAmount).value = "0";
                        }
                    }
                </script>

                <table width="100%">
                    <tr>
                        <td class="headingred">
                            Spare Consumption for a complaint
                        </td>
                        <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                            <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                <ProgressTemplate>
                                    <img src="<%=ConfigurationManager.AppSettings["SIMSAjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 10px" align="center" colspan="2">
                            <table width="100%" border="0">
                                <tr>
                                    <td width="100%" align="left" class="bgcolorcomm">
                                        <table width="98%" border="0">
                                            <tr>
                                                <td align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                                    <font color='red'>*</font>
                                                    <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <%--<td width="30%">
                                                Branch Name<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtBranchName" runat="server" Width="170px"
                                                    Text="" />
                                                <asp:RequiredFieldValidator ID="RFStateDesc" runat="server" SetFocusOnError="true"
                                                    ErrorMessage="Branch Name is required." ControlToValidate="txtBranchName" ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>--%>
                                                <td>
                                                    <table width="100%">
                                                        <tr>
                                                            <td style="width: 14%">
                                                                Complaint No.<font color='red'>*</font>
                                                            </td>
                                                            <td colspan="5">
                                                                <%--<asp:DropDownList ID="ddlComplaintNo" runat="server" CssClass="simpletxt1" Width="175px"
                                                                ValidationGroup="editt" AutoPostBack="True" OnSelectedIndexChanged="ddlComplaintNo_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RFComplaintNo" runat="server" ControlToValidate="DDLComplaintNo"
                                                                ErrorMessage="*" InitialValue="Select" SetFocusOnError="true" ValidationGroup="editt"></asp:RequiredFieldValidator>--%>
                                                                <asp:Label runat="server" ID="lblComplaintNo"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 14%">
                                                                Complaint Date
                                                            </td>
                                                            <td colspan="5">
                                                                <asp:Label ID="lblComplaintDate" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 14%">
                                                                Product Division
                                                            </td>
                                                            <td colspan="5">
                                                                <asp:Label ID="lblProdDiv" runat="server" BorderStyle="None"></asp:Label>
                                                                <asp:HiddenField ID="hdnProductDivision_Id" runat="server" />
                                                                <asp:HiddenField ID="hdnProduct_Id" runat="server" />
                                                                <asp:HiddenField ID="hdnASC_Id" runat="server" />
                                                                <asp:HiddenField ID="hdnUserType_Code" runat="server" />
                                                                <asp:HiddenField ID="hdnProposedSpares" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 14%">
                                                                Product
                                                            </td>
                                                            <td colspan="5">
                                                                <asp:Label ID="lblProdDesc" runat="server" BorderStyle="None" Height="16px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 14%">
                                                                Warranty Status
                                                            </td>
                                                            <td colspan="5">
                                                                <asp:Label ID="lblwarrantystatus" runat="server" BorderStyle="None"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr id="trTotalsplitCount" runat="server" visible="true">
                                                            <td style="width: 14%">
                                                                Total Split Complaint
                                                            </td>
                                                            <td colspan="5">
                                                                <asp:Label ID="lblTotalsplitComplaintNo" ForeColor="Green" runat="server" BorderStyle="None"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>Material Consumption</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Search Spares:
                                                    <asp:TextBox ID="txtFindSpare" ValidationGroup="ProductRef" CssClass="txtboxtxt"
                                                        runat="server" Width="130" CausesValidation="True"></asp:TextBox>
                                                    <asp:Button ID="btnGoSpare" runat="server" ValidationGroup="ProductRef" Width="20px"
                                                        Text="Go" CssClass="btn" OnClick="btnGoSpare_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="gvComm" runat="server" AlternatingRowStyle-CssClass="fieldName"
                                                        AutoGenerateColumns="False" GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor"
                                                        HorizontalAlign="Left" RowStyle-CssClass="gridbgcolor" Width="1024px" OnRowDataBound="gvComm_RowDataBound"
                                                        OnRowEditing="gvComm_RowEditing" OnRowUpdating="gvComm_RowUpdating" OnRowCancelingEdit="gvComm_RowCancelingEdit"
                                                        HeaderStyle-VerticalAlign="Top">
                                                        <RowStyle CssClass="gridbgcolor" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Transaction_No" DataField="Transaction_No" SortExpression="Transaction_No"
                                                                ReadOnly="True" />
                                                            <asp:TemplateField HeaderText="Spare">
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container.DataItem,"Spare") %>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="DDLSpareCode" runat="server" CssClass="simpletxt1" AutoPostBack="True"
                                                                        OnSelectedIndexChanged="DDLSpareCode_SelectedIndexChanged" ValidationGroup="editt"
                                                                        Width="200px">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RFSpare" runat="server" ControlToValidate="DDLSpareCode"
                                                                        ErrorMessage="Spare is required." InitialValue="0" SetFocusOnError="true" ValidationGroup="editt"
                                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Alternate Spare" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container.DataItem, "AlternateSpare")%>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddlalternatesparecode" runat="server" CssClass="simpletxt1"
                                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlalternatesparecode_SelectedIndexChanged"
                                                                        Width="200px">
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                                <ItemStyle Width="20px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Qty required As Per BOM" DataField="QtyRequiredAsPerBOM"
                                                                SortExpression="QtyRequiredAsPerBOM" ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Total Available Stock" DataField="TotalAvailableStock"
                                                                SortExpression="TotalAvailableStock" ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Ordered But Not Recieved" DataField="OrderedNotRecieved"
                                                                SortExpression="OrderedNotRecieved" ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Shortage If Any" DataField="Shortage" SortExpression="Shortage"
                                                                ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Location">
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container.DataItem, "Location")%>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddllocation" runat="server" CssClass="simpletxt1" AutoPostBack="True"
                                                                        OnSelectedIndexChanged="ddllocation_SelectedIndexChanged" ValidationGroup="editt">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RFLocation1" runat="server" ControlToValidate="ddllocation"
                                                                        ErrorMessage="Location is required." InitialValue="0" SetFocusOnError="true"
                                                                        ValidationGroup="editt" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Available Qty" DataField="AvailableQty" SortExpression="AvailableQty"
                                                                ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Quantity Consumed">
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container.DataItem, "QuantityConsumed")%>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtConsumedQty" runat="server" CssClass="simpletxt1" Width="40px"
                                                                        AutoPostBack="true" OnTextChanged="txtConsumedQty_TextChanged" ValidationGroup="editt"></asp:TextBox>
                                                                    <%--<asp:RequiredFieldValidator Display="Dynamic" ID="RFStateDesc" runat="server" SetFocusOnError="true"
                                                                        ErrorMessage="Qty required." ControlToValidate="txtConsumedQty" ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                                        <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvRate" runat="server" ControlToValidate="txtConsumedQty"
                                                                        Display="Dynamic" ErrorMessage="Qty can't be zero." InitialValue="0" ValidationGroup="editt"></asp:RequiredFieldValidator>--%>
                                                                    <asp:RegularExpressionValidator EnableClientScript="true" Display="Dynamic" ControlToValidate="txtConsumedQty"
                                                                        ID="RegularExpressionValidator1" ValidationGroup="editt" runat="server" ErrorMessage="Numeric Value required."
                                                                        ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Quantity Required">
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container.DataItem, "Consumed_Required_Qty")%>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtRequiredQty" runat="server" CssClass="txtboxtxt" Width="40px"
                                                                        OnTextChanged="txtRequiredQty_TextChanged" ValidationGroup="editt" Enabled="false"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator EnableClientScript="true" Display="Dynamic" ControlToValidate="txtRequiredQty"
                                                                        ID="RegularExpressionValidator101" ValidationGroup="editt" runat="server" ErrorMessage="Numeric Value required."
                                                                        ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Defective return Flag" DataField="DefectiveReturnFlag"
                                                                SortExpression="DefectiveReturnFlag" ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Defective Qty Generated" DataField="DefectiveQtyGenerated"
                                                                SortExpression="DefectiveQtyGenerated" ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Rate" DataField="Rate" ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Discount %" DataField="Discount" ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Amount" DataField="Amount" ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btndelete" runat="server" Text="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Transaction_No")%>'
                                                                        CssClass="btn" OnClick="btndelete_Click" />
                                                                    <asp:Button ID="btnInsert" runat="server" Text="ADD" ValidationGroup="editt" CausesValidation="true"
                                                                        CssClass="btn" OnClick="btnInsert_Click" />
                                                                    <asp:Label ID="lblQtyMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:CommandField ShowEditButton="true" ControlStyle-CssClass="btn" ButtonType="Button">
                                                                <ControlStyle CssClass="btn" />
                                                            </asp:CommandField>
                                                            <asp:TemplateField HeaderText="Reject">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="ChkReject" runat="server" />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Rejection Reason" DataField="RejectionReason" ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Spare_Id" DataField="Spare_Id" ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Loc_Id" DataField="Loc_Id" ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Alternate_Spare_Id" DataField="Alternate_Spare_Id" ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="QuantityConsumed" DataField="QuantityConsumed" ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="SaveShortage" DataField="SaveShortage" ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Spare" DataField="Spare" ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="AlternateSpare" DataField="AlternateSpare" ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Consumed_Required_Qty" DataField="Consumed_Required_Qty"
                                                                ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Pre_Consumed_Qty" DataField="Pre_Consumed_Qty" ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <%-- <EmptyDataTemplate>
                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                                    <img src="<%=ConfigurationManager.AppSettings["UserMessage"]%>" alt="" />
                                                                    <b>No Record found</b>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>--%>
                                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                        <AlternatingRowStyle CssClass="fieldName" />
                                                    </asp:GridView>
                                                    <!-- Branch Listing -->
                                                    <!-- End Branch Listing -->
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                </td>
                                            </tr>
                                            <tr id="trActivityHeader" runat="server">
                                                <td>
                                                    <b>
                                                        <asp:Label ID="lblActivityCharges" runat="server" Text="Activity Charges"></asp:Label></b>
                                                </td>
                                            </tr>
                                            <tr id="tblOtherDiv" runat="server">
                                                <td>
                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                        <tr id="trActivitySearch" runat="server">
                                                            <%--Id Added On 23.9.14 By Ashok--%>
                                                            <td>
                                                                <div id="dvActivity" runat="server" style="width: 50%; float: left;">
                                                                    <asp:Label ID="lblActivitySearch" runat="server" Text="Search Activity:"></asp:Label>
                                                                    <asp:TextBox ID="TxtActivityearch" runat="server" CausesValidation="True" CssClass="txtboxtxt"
                                                                        ValidationGroup="ProductRef" Width="130"></asp:TextBox>
                                                                    <asp:Button ID="BtnSearch" runat="server" CssClass="btn" OnClick="BtnSearch_Click1"
                                                                        Text="Go" ValidationGroup="ProductRef" Width="20px" />
                                                                    &nbsp;
                                                                    <asp:LinkButton ID="LnkActivity" runat="server" ForeColor="#1c3d74" OnClick="LnkActivity_Click">View Activity</asp:LinkButton>
                                                                </div>
                                                               
                                                                
                                                                 <div style="width: 70%; float: left;" id="dvOutstationLocal" runat="server">
                                                                    <asp:RadioButtonList ID="rbnLocalOutStation" runat="server" RepeatDirection="Horizontal"
                                                                        onClick="return Confirm();">
                                                                        <asp:ListItem Text="Local Charges" Value="L" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="OutStation Charges" Value="O"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                    <asp:Button ID="btnConfirm" runat="server" Text="" OnClick="btnConfirmClick" Style="display: none;" />
                                                                    <asp:HiddenField ID="hdnActivityCharges" runat="server" />
                                                                </div>
                                                               
                                                                
                                                            
                                                                 <div style="width: 70%; float: left;" id="dvFan" runat="server">
                                                                    <asp:RadioButtonList ID="rdfan" runat="server" RepeatDirection="Horizontal"                                                                      >
                                                                        <asp:ListItem Text="Local Charges" Value="L" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="OutStation Charges" Value="O"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                  
                                                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                                                </div>
                                                                
                                                                
                                                                
                                                                
                                                                
                                                                    
                                                                
                                                                 <div style="clear: both">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr id="trDemoCharges" runat="server" visible="false">
                                                            <td>
                                                                <b><span>Demo Charges</span></b>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:GridView ID="GridActivitySearch" runat="server" AlternatingRowStyle-CssClass="fieldName"
                                                                    AutoGenerateColumns="false" GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor"
                                                                    HeaderStyle-VerticalAlign="Top" HorizontalAlign="Left" RowStyle-CssClass="gridbgcolor"
                                                                    Width="1024px" OnRowDataBound="GridActivitySearch_RowDataBound" OnRowCreated="GridActivitySearch_RowCreated">
                                                                    <RowStyle CssClass="gridbgcolor" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="ActivityParameter_SNo" HeaderText="ActivitySno" ReadOnly="True"
                                                                            SortExpression="ActivityParameter_SNo" Visible="false" />
                                                                        <asp:BoundField DataField="Activity_Description" HeaderText="Activity" ReadOnly="True"
                                                                            SortExpression="Activity_Description" />
                                                                        <asp:BoundField DataField="Parameter_Code1" HeaderText="Param-1" ReadOnly="True"
                                                                            SortExpression="Parameter_Code1">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Possible_Value1" HeaderText="PV-1" ReadOnly="True" SortExpression="Possible_Value1">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Parameter_Code2" HeaderText="Param-2" ReadOnly="True"
                                                                            SortExpression="Parameter_Code2">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Possible_Value2" HeaderText="PV-2" ReadOnly="True" SortExpression="Possible_Value2">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Parameter_Code3" HeaderText="Param-3" ReadOnly="True"
                                                                            SortExpression="Parameter_Code3">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Possible_Value3" HeaderText="PV-3" ReadOnly="True" SortExpression="Possible_Value3">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Parameter_Code4" HeaderText="Param-4" ReadOnly="True"
                                                                            SortExpression="Parameter_Code4">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Possible_Value4" HeaderText="PV-4" ReadOnly="True" SortExpression="Possible_Value4">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Rate" HeaderText="Rate" ReadOnly="True" SortExpression="Rate">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="Select">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="CheckSelect" runat="server" CausesValidation="false" />
                                                                                <asp:HiddenField ID="hdnActivityParameter_SNo" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ActivityParameter_SNo")%>' />
                                                                                <asp:HiddenField ID="HdnActivityCode" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Activity_Code")%>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                                    <AlternatingRowStyle CssClass="fieldName" />
                                                                </asp:GridView>
                                                                <asp:HiddenField ID="hdnIsManpowerCount" Value="0" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Button ID="BtnAdd" runat="server" CausesValidation="False" CssClass="btn" OnClick="BtnAdd_Click"
                                                                    Text="Add" ValidationGroup="Add" Width="70px" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:GridView ID="gvActivityCharges" runat="server" AlternatingRowStyle-CssClass="fieldName"
                                                                    AutoGenerateColumns="False" GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor"
                                                                    HeaderStyle-VerticalAlign="Top" HorizontalAlign="Left" OnRowDataBound="gvActivityCharges_RowDataBound"
                                                                    RowStyle-CssClass="gridbgcolor" Width="1024px">
                                                                    <RowStyle CssClass="gridbgcolor" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="Activity_Description" HeaderText="Activity" ReadOnly="True"
                                                                            SortExpression="Activity_Description" />
                                                                        <asp:BoundField DataField="Parameter_Code1" HeaderText="Param-1" ReadOnly="True"
                                                                            SortExpression="Parameter_Code1">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Possible_Value1" HeaderText="PV-1" ReadOnly="True" SortExpression="Possible_Value1">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Parameter_Code2" HeaderText="Param-2" ReadOnly="True"
                                                                            SortExpression="Parameter_Code2">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Possible_Value2" HeaderText="PV-2" ReadOnly="True" SortExpression="Possible_Value2">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Parameter_Code3" HeaderText="Param-3" ReadOnly="True"
                                                                            SortExpression="Parameter_Code3">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Possible_Value3" HeaderText="PV-3" ReadOnly="True" SortExpression="Possible_Value3">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Parameter_Code4" HeaderText="Param-4" ReadOnly="True"
                                                                            SortExpression="Parameter_Code4">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Possible_Value4" HeaderText="PV-4" ReadOnly="True" SortExpression="Possible_Value4">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="Rate">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRate" runat="server" CssClass="simpletxt1" Text='<%# DataBinder.Eval(Container.DataItem, "Rate")%>'></asp:Label>
                                                                                <asp:HiddenField ID="HdnActivityCode" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Activity_Code")%>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="UOM" HeaderText="UOM" ReadOnly="True" SortExpression="UOM">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="Quantity">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtActualQty" runat="server" CssClass="simpletxt1" Text='<%# DataBinder.Eval(Container.DataItem, "Actual_Qty")%>'
                                                                                    ValidationGroup="editt1" Width="60px"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="rfvRate1" runat="server" ControlToValidate="txtActualQty"
                                                                                    Display="Dynamic" ErrorMessage="Qty can't be zero." InitialValue="0" SetFocusOnError="true"
                                                                                    ValidationGroup="editt1"></asp:RequiredFieldValidator>
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator123" runat="server"
                                                                                    ControlToValidate="txtActualQty" Display="Dynamic" EnableClientScript="true"
                                                                                    ErrorMessage="Numeric Value required." ValidationExpression="^\d*$" ValidationGroup="editt1">
                                                                                </asp:RegularExpressionValidator>
                                                                                <asp:RangeValidator ID="rngKmValidatior" Type="Integer" runat="server" ValidationGroup="editt1"
                                                                                    MaximumValue="100" ControlToValidate="txtActualQty" MinimumValue="0" ErrorMessage="Enter Less than or equall to 100 km"
                                                                                    EnableClientScript="true" Display="Dynamic">
                                                                                </asp:RangeValidator>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Amount">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtAmount" runat="server" CssClass="simpletxt1" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>'
                                                                                    ValidationGroup="editt1" Width="60px"></asp:TextBox>
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator12345" runat="server"
                                                                                    ControlToValidate="txtAmount" Display="Dynamic" EnableClientScript="true" ErrorMessage="Proper Amount required."
                                                                                    ValidationExpression="^\d{1,8}(\.\d{1,2})?$" ValidationGroup="editt1">
                                                                                </asp:RegularExpressionValidator>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Remarks">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="simpletxt1" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks")%>'
                                                                                    TextMode="MultiLine" ValidationGroup="editt1" Width="150px"></asp:TextBox>
                                                                                <asp:HiddenField ID="hdnActivityParameterSno" runat="server" Value='<%# Eval("ActivityParameter_SNo") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Select">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkActivityConfirm" runat="server" AutoPostBack="true" Checked=' <%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Confirm"))%>'
                                                                                    OnCheckedChanged="chkActivityConfirm_CheckedChanged" onClick="CheckQuantity(this)" />
                                                                                <asp:Label ID="lblactivityid" runat="server" Style="display: none" Text=' <%# Eval("Activity_Id")%>' />
                                                                                <asp:Label ID="lbldiscount" runat="server" Style="display: none" Text=' <%# Eval("Discount")%>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="ActivityParameter_SNo" HeaderText="ActivityParameter_SNo"
                                                                            ReadOnly="True" SortExpression="ActivityParameter_SNo">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <%-- Added for actual amiunt (editable) logic 29 sept bp--%>
                                                                        <asp:TemplateField HeaderText="FixedAmount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblactual" runat="server" Text='<%# Eval("FixedAmount") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                                    <AlternatingRowStyle CssClass="fieldName" />
                                                                    <EmptyDataTemplate>
                                                                        <p align="center">
                                                                            <b>Currently no activity added for this complaint. Please search to add new activities.</b></p>
                                                                    </EmptyDataTemplate>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <asp:HiddenField ID="hdnActivity_param_sno" runat="server" />
                                                        <asp:HiddenField ID="hdnActual" runat="server" />
                                                        <asp:HiddenField ID="hdnManpower" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hdnManpowerCharges" runat="server" Value="0.0" />
                                                        <asp:HiddenField ID="hdnTotalManpowerCharges" runat="server" Value="0.0" />
                                                        <tr id="trManpowerlabourCharges" runat="server" visible="false">
                                                            <td align="right" class="MsgTDCount">
                                                                <div>
                                                                    <div style="width: 80%; float: left; color: Red;">
                                                                        (Man Days)x</div>
                                                                    <div style="width: 9%; float: left; color: Red;">
                                                                        (Per Days Charges)</div>
                                                                    <div style="width: 10%; float: left;">
                                                                        &nbsp;</div>
                                                                    <div style="clear: both;">
                                                                    </div>
                                                                </div>
                                                                <div>
                                                                    <div style="width: 80%; float: left;">
                                                                        <asp:DropDownList ID="ddlManDaysNo" runat="server" CssClass="simpletxt1" Width="50px"
                                                                            AutoPostBack="True" OnSelectedIndexChanged="ddlManDaysNo_SelectedIndexChanged">
                                                                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                            <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        &nbsp;x&nbsp;
                                                                    </div>
                                                                    <div style="width: 9%; float: left; border-bottom: 1px solid #000;">
                                                                        <asp:Label ID="lblManPerDayCharg" runat="server" Text="0"></asp:Label>
                                                                    </div>
                                                                    <div style="width: 10%; float: left; border-bottom: 1px solid #000;">
                                                                        <asp:Label ID="lblManPowerCharges" runat="server" Text="0" CssClass="MsgTotalCount"></asp:Label></div>
                                                                    <div style="clear: both;">
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" class="MsgTDCount">
                                                                <div>
                                                                    <div style="width: 80%; float: left;">
                                                                        &nbsp;</div>
                                                                    <div style="width: 9%; float: left;">
                                                                        Total Amount :</div>
                                                                    <div style="width: 10%; float: left;">
                                                                        <asp:Label ID="lbltamount" runat="server" CssClass="MsgTotalCount"></asp:Label>
                                                                    </div>
                                                                    <div style="clear: both;">
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr id="trdiscount" runat="server" visible="false">
                                                            <td align="right" class="MsgTDCount">
                                                                If more than one repair is done then 65 % of sum of individual charges to be given.
                                                                (only for PUMP)
                                                                </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" class="MsgTDCount">
                                                                <div>
                                                                    <div style="width: 80%; float: left;">
                                                                        &nbsp;</div>
                                                                    <div style="width: 9%; float: left;">
                                                                        Actual Amount :</div>
                                                                    <div style="width: 10%; float: left;">
                                                                        <asp:Label ID="lblTotalAmount" runat="server" CssClass="MsgTotalCount"></asp:Label>
                                                                    </div>
                                                                    <div style="clear: both;">
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table style="width: 58%">
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Button ID="imgbtnSave" runat="server" CausesValidation="true" CssClass="btn"
                                                                    OnClick="imgbtnSave_Click" Text="Save &amp; Close" ValidationGroup="editt1" Width="110px" />
                                                                <asp:Button ID="imgBtnCancel" runat="server" CausesValidation="False" CssClass="btn"
                                                                    OnClick="imgBtnCancel_Click1" Text="Cancel" ValidationGroup="editt" Width="70px" />
                                                                <asp:Panel ID="popupPanel" runat="server" Style="display: none; width: 200px; background-color: Gray;
                                                                    border-width: 2px; border-color: Black; border-style: solid; padding: 20px;">
                                                                    <br />
                                                                    <br />
                                                                    <div style="text-align: left;">
                                                                        <b>Are You Sure to confirm?</b><br />
                                                                        <br />
                                                                    </div>
                                                                    <div style="text-align: right;">
                                                                        <asp:Button ID="ButtonOk" runat="server" CssClass="btn" Text="OK" />
                                                                        <asp:Button ID="ButtonCancel" runat="server" CssClass="btn" Text="Cancel" />
                                                                    </div>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
