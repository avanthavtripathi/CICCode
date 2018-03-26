<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SpareConsumptionActivity.aspx.cs"
    Inherits="SIMS_Pages_SpareConsumptionActivity" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Spare Consumption</title>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/global.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../../scripts/Common.js">

    </script>

    <script type="text/javascript">
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
        function CloseAfterSave() {

            //                        alert("Save Successfully");
            window.opener.location.href = "RejectedClaimScreenASC.aspx?ReturnId=True";

            window.close();

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


        //added by vikas on 19-Oct-2011

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

    <script type="text/javascript">
        var TargetBaseControl = null;
        window.onload = function() {
            try {
                TargetBaseControl =
         document.getElementById('<%= this.gvActivityCharges.ClientID %>');
            }
            catch (err) {
                TargetBaseControl = null;
            }
        }
        function TestCheckBox() {
            if (TargetBaseControl == null) return false;

            var TargetChildControl = "chkActivityConfirm";

            var Inputs = TargetBaseControl.getElementsByTagName("input");
            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' &&
           Inputs[n].id.indexOf(TargetChildControl, 0) >= 0 &&
            Inputs[n].checked) {
                closePopup();
                return true;
            }
            alert('Select at least one Activity!');
            return false;
        }
        function closePopup() {
            window.opener.location.href = window.opener.location;
            self.close();
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

                        var selectedDate = new Date();
                        selectedDate = sender._selectedDate;

                        var todayDate = new Date();
                        var mssge = "";

                        if (selectedDate > todayDate) {

                            sender._selectedDate = todayDate;

                            sender._textbox.set_Value(sender._selectedDate.format(sender._format));

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
                                                <td>
                                                    <table width="100%">
                                                        <tr>
                                                            <td style="width: 12%">
                                                                Complaint No.<font color='red'></font>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblComplaintNo" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Complaint Date
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblComplaintDate" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Product Division
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblProdDiv" runat="server" BorderStyle="None"></asp:Label>
                                                                <asp:HiddenField ID="hdnProductDivision_Id" runat="server" />
                                                                <asp:HiddenField ID="hdnProduct_Id" runat="server" />
                                                                <asp:HiddenField ID="hdnASC_Id" runat="server" />
                                                                <asp:HiddenField ID="hdnUserType_Code" runat="server" />
                                                                <asp:HiddenField ID="hdnProposedSpares" runat="server" />
                                                                <asp:HiddenField ID="hdnClosepopup" runat="server" />
                                                                <asp:HiddenField ID="hdnClaimNo" runat="server" />
                                                                <asp:HiddenField ID="hdnClaimDate" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Product
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblProdDesc" runat="server" BorderStyle="None" Height="16px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Warranty Status
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblwarrantystatus" runat="server" BorderStyle="None"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr id="trTotalsplitCount" runat="server" visible="true">
                                                            <td style="width: 14%">
                                                                Total Split Complaint
                                                            </td>
                                                            <td>
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
                                                        HorizontalAlign="Left" RowStyle-CssClass="gridbgcolor" OnRowDataBound="gvComm_RowDataBound"
                                                        OnRowEditing="gvComm_RowEditing" OnRowUpdating="gvComm_RowUpdating" OnRowCancelingEdit="gvComm_RowCancelingEdit"
                                                        OnRowUpdated="gvComm_RowUpdated" OnPageIndexChanging="gvComm_PageIndexChanging">
                                                        <RowStyle CssClass="gridbgcolor" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Transaction_No" DataField="Transaction_No" ReadOnly="True" />
                                                            <asp:TemplateField HeaderText="Spare">
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container.DataItem,"Spare") %>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="DDLSpareCode" runat="server" CssClass="simpletxt1" AutoPostBack="True"
                                                                        Width="180px" OnSelectedIndexChanged="DDLSpareCode_SelectedIndexChanged" ValidationGroup="editt">
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
                                                                        Width="90px" AutoPostBack="True" OnSelectedIndexChanged="ddlalternatesparecode_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                                <ItemStyle Width="20px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Qty required As Per BOM" DataField="QtyRequiredAsPerBOM"
                                                                ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Total Available Stock" DataField="TotalAvailableStock"
                                                                ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Ordered But Not Recieved" DataField="OrderedNotRecieved"
                                                                ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Shortage If Any" DataField="Shortage" ReadOnly="True">
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
                                                            <asp:BoundField HeaderText="Available Qty" DataField="AvailableQty" ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Quantity Consumed">
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container.DataItem, "QuantityConsumed")%>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtConsumedQty" runat="server" CssClass="simpletxt1" Width="60px"
                                                                        AutoPostBack="True" OnTextChanged="txtConsumedQty_TextChanged" ValidationGroup="editt"></asp:TextBox>
                                                                    <%-- <asp:RequiredFieldValidator Display="Dynamic" ID="RFStateDesc" runat="server" SetFocusOnError="true"
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
                                                                    <asp:TextBox ID="txtRequiredQty" runat="server" CssClass="simpletxt1" Width="60px"
                                                                        OnTextChanged="txtRequiredQty_TextChanged" ValidationGroup="editt" Enabled="false"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator EnableClientScript="true" Display="Dynamic" ControlToValidate="txtRequiredQty"
                                                                        ID="RegularExpressionValidator101" ValidationGroup="editt" runat="server" ErrorMessage="Numeric Value required."
                                                                        ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Defective return Flag" DataField="DefectiveReturnFlag"
                                                                ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Defective Qty Generated" DataField="DefectiveQtyGenerated"
                                                                ReadOnly="True">
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
                                                            <asp:BoundField HeaderText="Rejected" DataField="Reject" ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Rejection Reason" DataField="RejectionReason" ReadOnly="True">
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
                                                                <ControlStyle CssClass="btn" Width="50px" />
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
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblqtyconsumed" runat="server" Text='<%#Eval("QuantityConsumed_lbl") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Consumed_Required_Qty" DataField="Consumed_Required_Qty"
                                                                ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                        <AlternatingRowStyle CssClass="fieldName" />
                                                    </asp:GridView>
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
                                                    <table>
                                                        <tr id="trActivitySearch" runat="server">
                                                            <%--Id Added On 23.9.14 By Ashok--%>
                                                            <td>
                                                                <div style="width: 28%; float: left;">
                                                                    <asp:Label ID="lblActivitySearch" runat="server" Text="Search Activity:"></asp:Label>
                                                                    <asp:TextBox ID="TxtActivityearch" runat="server" CausesValidation="True" CssClass="txtboxtxt"
                                                                        ValidationGroup="ProductRef" Width="130"></asp:TextBox>
                                                                    <asp:Button ID="BtnSearch" runat="server" CssClass="btn" OnClick="BtnSearch_Click1"
                                                                        Text="Go" ValidationGroup="ProductRef" Width="20px" />
                                                                    &nbsp;
                                                                    <asp:LinkButton ID="lnk" runat="server" ForeColor="#1c3d74" OnClick="lnk_Click">View Activity</asp:LinkButton>
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
                                                                    AutoGenerateColumns="False" GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor"
                                                                    HorizontalAlign="Left" RowStyle-CssClass="gridbgcolor" Width="1300px" HeaderStyle-VerticalAlign="Top"
                                                                    Visible="False" OnRowDataBound="GridActivitySearch_RowDataBound" OnRowCreated="GridActivitySearch_RowCreated">
                                                                    <RowStyle CssClass="gridbgcolor" />
                                                                    <Columns>
                                                                        <asp:BoundField HeaderText="ActivitySno" Visible="false" DataField="ActivityParameter_SNo"
                                                                            SortExpression="ActivityParameter_SNo" ReadOnly="True" />
                                                                        <asp:BoundField HeaderText="Activity" DataField="Activity_Description" SortExpression="Activity_Description"
                                                                            ReadOnly="True" />
                                                                        <asp:BoundField HeaderText="Param-1" DataField="Parameter_Code1" SortExpression="Parameter_Code1"
                                                                            ReadOnly="True">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="PV-1" DataField="Possible_Value1" SortExpression="Possible_Value1"
                                                                            ReadOnly="True">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Param-2" DataField="Parameter_Code2" SortExpression="Parameter_Code2"
                                                                            ReadOnly="True">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="PV-2" DataField="Possible_Value2" SortExpression="Possible_Value2"
                                                                            ReadOnly="True">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Param-3" DataField="Parameter_Code3" SortExpression="Parameter_Code3"
                                                                            ReadOnly="True">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="PV-3" DataField="Possible_Value3" SortExpression="Possible_Value3"
                                                                            ReadOnly="True">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Param-4" DataField="Parameter_Code4" SortExpression="Parameter_Code4"
                                                                            ReadOnly="True">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="PV-4" DataField="Possible_Value4" SortExpression="Possible_Value4"
                                                                            ReadOnly="True">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Rate" DataField="Rate" SortExpression="Rate" ReadOnly="True">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="Select">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="CheckSelect" runat="server" />
                                                                                <asp:HiddenField ID="hdnActivityParameter_SNo" Value='<%# DataBinder.Eval(Container.DataItem, "ActivityParameter_SNo")%>' runat="server" />
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
                                                                <asp:Button Text="Add" Width="70px" ID="BtnAdd" Visible="false" CssClass="btn" runat="server"
                                                                    CausesValidation="False" ValidationGroup="Add" OnClick="BtnAdd_Click" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:GridView ID="gvActivityCharges" runat="server" AlternatingRowStyle-CssClass="fieldName"
                                                                    AutoGenerateColumns="False" GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor"
                                                                    HorizontalAlign="Left" RowStyle-CssClass="gridbgcolor"  style="min-width:1300px;" OnRowDataBound="gvActivityCharges_RowDataBound"
                                                                    OnPageIndexChanging="gvActivityCharges_PageIndexChanging">
                                                                    <RowStyle CssClass="gridbgcolor" />
                                                                    <Columns>
                                                                        <asp:BoundField HeaderText="Activity" DataField="Activity_Description" ReadOnly="True" />
                                                                        <asp:BoundField HeaderText="Param-1" DataField="Parameter_Code1" ReadOnly="True">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="PV-1" DataField="Possible_Value1" ReadOnly="True">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Param-2" DataField="Parameter_Code2" ReadOnly="True">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="PV-2" DataField="Possible_Value2" ReadOnly="True">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Param-3" DataField="Parameter_Code3" ReadOnly="True">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="PV-3" DataField="Possible_Value3" ReadOnly="True">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Param-4" DataField="Parameter_Code4" ReadOnly="True">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="PV-4" DataField="Possible_Value4" ReadOnly="True">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <%--<asp:BoundField HeaderText="Rate" DataField="Rate" ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>--%>
                                                                        <asp:TemplateField HeaderText="Rate">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRate" runat="server" CssClass="simpletxt1" Text='<%# DataBinder.Eval(Container.DataItem, "Rate")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="UOM" DataField="UOM" ReadOnly="True">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="Qty/Kms">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtActualQty" runat="server" CssClass="simpletxt1" Width="60px"
                                                                                    ValidationGroup="editt1" Text='<%# DataBinder.Eval(Container.DataItem, "Actual_Qty")%>'></asp:TextBox>
                                                                                <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvRate1" runat="server" ControlToValidate="txtActualQty"
                                                                                    Display="Dynamic" ErrorMessage="Qty can't be zero." InitialValue="0" ValidationGroup="editt1"></asp:RequiredFieldValidator>
                                                                                <asp:RegularExpressionValidator EnableClientScript="true" Display="Dynamic" ControlToValidate="txtActualQty"
                                                                                    ID="RegularExpressionValidator123" ValidationGroup="editt1" runat="server" ErrorMessage="Numeric Value required."
                                                                                    ValidationExpression="^\d*$">
                                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                </asp:RegularExpressionValidator>
                                                                                <asp:RangeValidator ID="rngKmValidatior" Type="Integer" runat="server" ValidationGroup="editt1"
                                                                                    MaximumValue="100" ControlToValidate="txtActualQty" MinimumValue="0" ErrorMessage="Enter Less than or equall to 100 km"
                                                                                    EnableClientScript="true" Display="Dynamic">
                                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                </asp:RangeValidator>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--<asp:BoundField HeaderText="Amount" DataField="Amount" ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>--%>
                                                                        <asp:TemplateField HeaderText="Amount">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtAmount" AutoPostBack="false" OnTextChanged="txtAmount_TextChanged"
                                                                                    runat="server" CssClass="simpletxt1" Width="60px" ValidationGroup="editt1" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>'></asp:TextBox>
                                                                                <asp:RegularExpressionValidator EnableClientScript="true" Display="Dynamic" ControlToValidate="txtAmount"
                                                                                    ID="RegularExpressionValidator12345" ValidationGroup="editt1" runat="server"
                                                                                    ErrorMessage="Proper Amount required." ValidationExpression="^\d{1,8}(\.\d{1,2})?$">
                                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                </asp:RegularExpressionValidator>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Remarks">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="simpletxt1" Width="150px" TextMode="MultiLine"
                                                                                    ValidationGroup="editt1" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks")%>'></asp:TextBox>
                                                                                <asp:HiddenField ID="hdnActivityParameterSno" runat="server" Value='<%# Eval("ActivityParameter_SNo") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="Rejected" DataField="Rejected" ReadOnly="True">
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Rejection Reason" DataField="RejectionReason" ReadOnly="True">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="Select">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkActivityConfirm" OnCheckedChanged="chkActivityConfirm_CheckedChanged"
                                                                                    AutoPostBack="True" Checked=' <%# DataBinder.Eval(Container.DataItem, "Confirm")%>'
                                                                                    runat="server" onClick="CheckQuantity(this)" />
                                                                                <asp:Label ID="lblactivityid" Text=' <%# Eval("Activity_Id")%>' runat="server" Style="display: none" />
                                                                                <asp:Label ID="lbldiscount" runat="server" Style="display: none" Text=' <%# Eval("Discount")%>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="ActivityParameter_SNo" DataField="ActivityParameter_SNo"
                                                                            ReadOnly="True">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <%--<asp:BoundField HeaderText="FixedAmount" DataField="FixedAmount" SortExpression="FixedAmount"
                                                                            ReadOnly="True">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>--%>
                                                                        <%-- Added By Ashok on 26.11.2014--%>
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
                                                                 <%--<asp:Label ID="lblspecialdiscount" CssClass="MsgTotalCount" runat="server"></asp:Label>--%>
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
                                                                        <asp:Label ID="lblTotalAmount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div style="clear: both;">
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Button Text="Save & Close" Width="80px" ID="imgbtnSave" CssClass="btn" runat="server"
                                                                    CausesValidation="true" ValidationGroup="editt1" OnClick="imgbtnSave_Click" />
                                                                <asp:Button Text="Cancel" Width="70px" Visible="false" ID="imgBtnCancel" CssClass="btn"
                                                                    runat="server" CausesValidation="False" ValidationGroup="editt" OnClick="imgBtnCancel_Click1" />
                                                                <asp:Panel ID="popupPanel" runat="server" Style="display: none; width: 200px; background-color: Gray;
                                                                    border-width: 2px; border-color: Black; border-style: solid; padding: 20px;">
                                                                    <br />
                                                                    <br />
                                                                    <div style="text-align: left;">
                                                                        <b>Are You Sure to confirm?</b><br />
                                                                        <br />
                                                                    </div>
                                                                    <div style="text-align: right;">
                                                                        <asp:Button ID="ButtonOk" runat="server" Text="OK" CssClass="btn" />
                                                                        <asp:Button ID="ButtonCancel" runat="server" CssClass="btn" Text="Cancel" />
                                                                    </div>
                                                                </asp:Panel>
                                                                <asp:Button Text="Resend For Approval" Width="128px" ID="imgbtnResendApproval" CssClass="btn"
                                                                    runat="server" CausesValidation="true" ValidationGroup="editt1" OnClick="imgbtnResendApproval_Click" />
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
