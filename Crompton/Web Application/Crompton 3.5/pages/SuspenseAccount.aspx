<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="SuspenseAccount.aspx.cs" Inherits="pages_SuspenseAccount" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script src="../scripts/jquery-1.2.6.js" type="text/javascript"></script>
    <link href="../css/suspance.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function() {
            getdatea(); // First Time Call Method for Notification
            var res = setInterval(function() { getdatea(); }, 1000); // Call Notification Method at every time interval
            /************** Load Notification Method on Action ***************/
            $("select[ID$='ddlProductDiv'],select[ID$='ddlState'],select[ID$='ddlAppointment'],select[ID$='ddlCity'],select[ID$='DDlModeOfReceipt']").live("change", function
            () {
                ActiononEvent();
            });
            $("submit[ID$='btnSearch']").live("click", function() {
                ActiononEvent();
            });
            /************* End **************/
        });
        function ActiononEvent() {
            $(".loading").css("display", "block");
            $("#mrq").empty();
            $("#maindv").empty();
            getdatea();
            $(".loading").css("display", "none");
        }
        /************* Mthod to Intract with Service*******************/
        function getdatea() {
            var data = "{ unitSno: '" + $('select[id$="ddlProductDiv"] option:selected').val() + "',fromLoggedDate:'" + $('label[id$="txtFromDate"]').text() + "'," +
        "toLoggedDate: '" + $('label[id$="txtToDate"]').text() + "', stateSno: '" + $('select[id$="ddlState"] option:selected').val() + "'," +
        "citySno: " + $('select[id$="ddlCity"] option:selected').val() + ", appointmentReq: " + $('select[id$="ddlAppointment"] option:selected').val() + "," +
        "modeOfRecp: '" + $('select[id$="DDlModeOfReceipt"] option:selected').val() + "' }";
            $.ajax({
                type: "POST",
                url: "SuspenseAccount.aspx/GetSuspanceNotification",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: data,
                success: function(response) {
                    var jsonData = $.parseJSON(response.d);
                    var suspanceDtls = jsonData.SuspanceDtls;
                    var suspanceHdrDtls = jsonData.SuspanceHeaderDtls;
                    var intCount = 0;
                    $("#mrq").empty();
                    if (suspanceHdrDtls != undefined || suspanceDtls != "") {
                        $.each(suspanceHdrDtls, function(index, item) {
                            $("#mrq").append("<span>" + item.stateDesc + "</span> <span class='suscount'>" + item.totalCount + "</span> &nbsp;|&nbsp;");
                            intCount = intCount + item.totalCount;
                        });
                        $("#maindv").empty();
                        $("#maindv").append("<div class='dvsusl'><b>State Desc</b></div><div class='dvsusr'><b>Total</b></div><div class='cls'></div>");
                        $.each(suspanceDtls, function(index, items) {
                            $("#maindv").append("<div class='dvsusl'>" + items.stateDesc + "</div><div class='dvsusr'>" + items.totalCount + "</div><div class='cls'></div>");
                        });
                        $("#totalSusCount").text(intCount);
                    }
                },
                failure: function(response) {
                    alert(response.d);
                }
            });
        }
        /**************** End *******************/
    </script>
    <script language="javascript" type="text/javascript">
        var TotalChkBx;
        var Counter;

        window.onload = function() {
            //Get total no. of CheckBoxes in side the GridView.
            TotalChkBx = parseInt('<%= this.gvFresh.Rows.Count %>');

            //Get total no. of checked CheckBoxes in side the GridView.
            Counter = 0;
        }

        function HeaderClick(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl =
           document.getElementById('<%= this.gvFresh.ClientID %>');
            var TargetChildControl = "chkBxSelect";

            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            //Checked/Unchecked all the checkBoxes in side the GridView.
            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' &&
                    Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
                Inputs[n].checked = CheckBox.checked;

            //Reset Counter
            Counter = CheckBox.checked ? TotalChkBx : 0;
        }

        function ChildClick(CheckBox, HCheckBox) {
            //get target control.
            var HeaderCheckBox = document.getElementById(HCheckBox);

            //Modifiy Counter; 
            if (CheckBox.checked && Counter < TotalChkBx)
                Counter++;
            else if (Counter > 0)
                Counter--;

            //Change state of the header CheckBox.
            if (Counter < TotalChkBx)
                HeaderCheckBox.checked = false;
            else if (Counter == TotalChkBx)
                HeaderCheckBox.checked = true;
        }
    </script>

    <asp:UpdatePanel ID="updateSkill" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="headingred">
                        Suspense Account
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div style="width: 100%; position: relative;">
                            <div class="loading">
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                            </div>
                            <div style="width: 90%; float: left;">
                                <marquee id="mrq" scrollamount="6" truespeed style="margin: 0px 0 0 14px!important; width:860px;"></marquee>
                            </div>
                            <div style="width: 7%; float: right; text-align: right; cursor: pointer; padding-right:1%;" id="aNotification">
                                ***<label class="suscount" id="totalSusCount"></label>
                                <div class="dvMain" id="maindv">
                                </div>
                            </div>
                            <div style="clear: both;">
                            </div>
                        </div>
                    </td>
                </tr>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" colspan="2">
                        <table width="100%" style="border: solid 1px #eeeeee;">
                            <tr>
                                <td colspan="4" align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                    <font color='red'>*</font>
                                    <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4" class="MsgTDCount" id="tdRowCount" runat="server">
                                    Total Number of Records :
                                    <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                </td>
                            </tr>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        P<asp:GridView ID="gvCommSearch" runat="server" 
                            AlternatingRowStyle-CssClass="fieldName" AutoGenerateColumns="False" 
                            DataKeyNames="Sc_Sno" HeaderStyle-CssClass="fieldNamewithbgcolor" 
                            HorizontalAlign="Left" 
                            OnSelectedIndexChanging="gvCommSearch_SelectedIndexChanging" PageSize="5" 
                            RowStyle-CssClass="gridbgcolor" Width="100%">
                            <RowStyle CssClass="gridbgcolor" />
                            <Columns>
                                <asp:BoundField DataField="RowNo" HeaderStyle-Width="40px" HeaderText="SNo">
                                    <HeaderStyle Width="40px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Territory" 
                                    ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnPreference" runat="server" 
                                            Value='<%#Eval("Preference")%>' />
                                        <asp:HiddenField ID="hdnTrr" runat="server" 
                                            Value='<%#Eval("Territory_Sno")%>' />
                                        <asp:Label ID="lblTRR" runat="server" Text='<%#Eval("Territory_Desc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SpecialRemarks" HeaderStyle-HorizontalAlign="Left" 
                                    HeaderText="Remarks" ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="SC Name" 
                                    ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" Width="200px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSC" runat="server" Text='<%#Eval("SC_Name") %>'></asp:Label>
                                        <asp:HiddenField ID="hdnGridScName" runat="server" 
                                            Value='<%#Eval("SC_Name")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Contact_Person" HeaderStyle-HorizontalAlign="Left" 
                                    HeaderText="Contact Person" ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Address1" HeaderStyle-HorizontalAlign="Left" 
                                    HeaderText="Address" ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" Width="320px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="80px" 
                                    HeaderText="Weekly Off Day" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnGridScNo" runat="server" Value='<%#Eval("Sc_Sno")%>' />
                                        <asp:HiddenField ID="hdnGridTerritoryDesc" runat="server" 
                                            Value='<%#Eval("Territory_Desc")%>' />
                                        <asp:HiddenField ID="hdnGridWO" runat="server" 
                                            Value='<%#Eval("Weekly_Off_Day")%>' />
                                        <%#Eval("Weekly_Off_Day") %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:ButtonField ButtonType="Link" CommandName="Select" HeaderText="Select" 
                                    Text="Select" />
                            </Columns>
                            <EmptyDataTemplate>
                                <img alt="" src='<%=ConfigurationManager.AppSettings["UserMessage"]%>' /><b>No 
                                records found</b>
                            </EmptyDataTemplate>
                            <HeaderStyle CssClass="fieldNamewithbgcolor" />
                            <AlternatingRowStyle CssClass="fieldName" />
                        </asp:GridView>
                        roduct Division: <font color="red">*</font>
                    </td>
                    <td align="left" style="width: 358px">
                        <asp:DropDownList ID="ddlProductDiv" runat="server" AutoPostBack="True" CssClass="simpletxt1"
                            Width="120px" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlProductDiv_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlProductDiv"
                            InitialValue="Select" SetFocusOnError="true" ValidationGroup="editt">Product Division is required.</asp:RequiredFieldValidator>
                    </td>
                    <td align="left">
                        State:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True" CssClass="simpletxt1"
                            OnSelectedIndexChanged="ddlState_SelectedIndexChanged" ValidationGroup="Search"
                            Width="175px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="height: 23px">
                        Appointment:
                    </td>
                    <td align="left" style="height: 23px; width: 358px;">
                        <asp:DropDownList ID="ddlAppointment" runat="server" CssClass="simpletxt1" Width="120px">
                            <asp:ListItem Value="99">Select</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left" style="height: 23px">
                        &nbsp;City:
                    </td>
                    <td align="left" style="height: 23px">
                        <asp:DropDownList ID="ddlCity" runat="server" AutoPostBack="True" CssClass="simpletxt1"
                            ValidationGroup="Search" Width="175px">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                
                   <tr>
                    <td align="left" style="height: 23px">
                        Mode of Receipt:
                    </td>
                    <td align="left" style="height: 23px; width: 358px;">
                        <asp:DropDownList ID="DDlModeOfReceipt" runat="server" CssClass="simpletxt1" Width="120px">
                        </asp:DropDownList>
                    </td>
                    <td align="left" style="height: 23px">
                    </td>
                    <td align="left" style="height: 23px">
                   </td>
                </tr>
                
                <tr>
                    <td align="left">
                        From Date:
                    </td>
                    <td align="left" style="width: 358px">
                        <asp:TextBox runat="server" ID="txtFromDate" CssClass="txtboxtxt" ValidationGroup="Select" />
                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtFromDate">
                        </cc1:CalendarExtender>
                        <asp:CompareValidator Operator="DataTypeCheck" Type="Date" ControlToValidate="txtFromDate"
                            ErrorMessage="Not a vaild Date" runat="server" ID="cmptxtFromDate" ValidationGroup="Select">
                        </asp:CompareValidator>
                    </td>
                    <td align="left">
                        To Date:
                    </td>
                    <td align="left">
                        <asp:TextBox runat="server" ID="txtTodate" CssClass="txtboxtxt" ValidationGroup="Select" />
                        <asp:CompareValidator Operator="DataTypeCheck" Type="Date" ControlToValidate="txtTodate"
                            ErrorMessage="Not a vaild Date" runat="server" ID="cmptxtTodate" ValidationGroup="Select">
                        </asp:CompareValidator>
                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtTodate">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="4">
                        <asp:Button ID="btnSearch" runat="server" ValidationGroup="editt" CssClass="btn"
                            OnClick="btnSearch_Click" Text="Search" Width="70px" CausesValidation="true" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="4">
                        <asp:GridView ID="gvFresh" runat="server" AllowPaging="True" AllowSorting="True"
                            AlternatingRowStyle-CssClass="fieldName" AutoGenerateColumns="False" DataKeyNames="BaseLineId"
                            GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor" Width="98%" RowStyle-CssClass="gridbgcolor"
                            OnPageIndexChanging="gvFresh_PageIndexChanging" OnSorting="gvFresh_Sorting" OnRowDataBound="gvFresh_RowDataBound"
                            OnRowEditing="gvFresh_RowEditing" OnRowUpdating="gvFresh_RowUpdating" OnRowCancelingEdit="gvFresh_RowCancelingEdit">
                            <RowStyle CssClass="gridbgcolor" />
                            <Columns>
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkBxSelect" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkBxHeader" onclick="javascript:HeaderClick(this);" runat="server" />
                                    </HeaderTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sno">
                                    <ItemTemplate>
                                        <%#Eval("SNo")%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Complaint RefNo" SortExpression="Complaint_RefNo">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnBaselineID" runat="server" Value='<%#Eval("BaseLineId")%>' />
                                        <asp:HiddenField ID="hdnComplaint_RefNo" runat="server" Value='<%#Eval("Complaint_RefNo")%>' />
                                        <asp:HiddenField ID="hdnAppointmentReq" runat="server" Value='<%#Eval("AppointmentReq")%>' />
                                        <asp:HiddenField ID="hdnCity_Desc" runat="server" Value='<%#Eval("City_Desc")%>' />
                                        <asp:HiddenField ID="hdnState_Desc" runat="server" Value='<%#Eval("State_Desc")%>' />
                                        <asp:HiddenField ID="hdnAddress" runat="server" Value='<%#Eval("Address")%>' />
                                        <asp:HiddenField ID="hdnLastName" runat="server" Value='<%#Eval("CustName")%>' />
                                        <asp:HiddenField ID="hdnUNIT_DESC" runat="server" Value='<%#Eval("UNIT_DESC")%>' />
                                        <a href="Javascript:void(0);" onclick="funCommonPopUp('<%#Eval("BaseLineId")%>')">
                                            <%#Eval("Complaint_RefNo")%></a>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Complaint Date" SortExpression="LoggedDate">
                                    <ItemTemplate>
                                        <%#Eval("LoggedDate")%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ModeOfReceipt" SortExpression="ModeOfReceipt_Desc">
                                    <ItemTemplate>
                                        <%#Eval("ModeOfReceipt_Desc")%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Division" SortExpression="UNIT_DESC">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductDivision" runat="server" Text=' <%#Eval("UNIT_DESC")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:HiddenField ID="hdnPD" runat="server" Value='<%#Bind("Unit_Desc")%>' />
                                        <asp:DropDownList ID="ddlProductDivisionGV" runat="server" Width="175px" CssClass="simpletxt1"
                                            DataTextField="Unit_Desc" DataValueField="Unit_SNo">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name" SortExpression="CustName">
                                    <ItemTemplate>
                                        <%#Eval("CustName")%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address" SortExpression="Address1">
                                    <ItemTemplate>
                                        <%#Eval("Address1")%><%#Eval("Address2")%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="State" SortExpression="STDesc_Code">
                                    <ItemTemplate>
                                        <%#Eval("STDesc_Code")%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="City" SortExpression="CYDesc_Code">
                                    <ItemTemplate>
                                        <%#Eval("CYDesc_Code")%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nature Of Complaint" SortExpression="NatureOfComplaint">
                                    <ItemTemplate>
                                        <%#Eval("NatureOfComplaint")%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderStyle-Width="300px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                        <div id="dvfalse" runat="server" visible='<%# Convert.ToString(Eval("ModeOfReceipt_Sno")).Equals("10") %>' >
                                          <asp:CheckBox ID="chkfalse" runat="server" Text="Close False Complaint" Checked="true" Enabled="false" />
                                          <asp:TextBox ID="txtcomment" Width="120px" runat="server" TextMode="MultiLine" Columns="2" ></asp:TextBox>
                                          <asp:Button ID="btnSave" CommandName='<%#Eval("Complaint_Refno")%>' CommandArgument='<%#Eval("SplitComplaint_Refno")%>' CssClass="btn" runat="server" Text="Save & Close" OnClick="btnClosecomplaint_Click" />
                                         </div> 
                                       </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Left" Width="400px" />
                                         <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                <asp:CommandField HeaderText="Edit" ShowEditButton="True" HeaderStyle-Width="50" />
                            </Columns>
                            <HeaderStyle CssClass="fieldNamewithbgcolor" />
                            <AlternatingRowStyle CssClass="fieldName" />
                            <PagerStyle HorizontalAlign="Center" />
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
            <table width="100%">
                <tr>
                    <td id="tbSearch" runat="server">
                        <asp:DropDownList Width="225px" ID="ddlSC" runat="server" CssClass="simpletxt1" ValidationGroup="Allow">
                            <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;
                        <asp:Button ID="btnGo" CssClass="btn" runat="server" Text="Find" Width="50px" 
                            OnClick="btnGo_Click" ValidationGroup="editt" />
                        &nbsp;<asp:Button ID="btnAllocate" CausesValidation="true" ValidationGroup="Allow"
                            runat="server" CssClass="btn" OnClick="btnAllocate_Click" Text="Allocate" Width="70px" />&nbsp;
                        <!-- Div Open  for search -->
                        <div id="dvSearch" class="dvInsertI" style="display: none; width: 100%;">
                            <table width="100%">
                                <tr>
                                    <td style="padding: 1px" align="center" class="bgcolorcomm">
                                        <table width="100%" border="0">
                                            <tr>
                                                <td width="100%" align="left">
                                                    <table width="98%" border="0" id="tableHeader" runat="server">
                                                        <tr>
                                                            <td style="width: 114px">
                                                                Product Division&nbsp;
                                                            </td>
                                                            <td id="tdProductDivision" runat="server">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 114px">
                                                                Product Line&nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList Width="175px" ID="ddlProductLine" runat="server" CssClass="simpletxt1">
                                                                    <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 114px">
                                                                State&nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlStateSearch" ValidationGroup="Search" runat="server" CssClass="simpletxt1"
                                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlStateSearch_SelectedIndexChanged"
                                                                    Width="175px">
                                                                    <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 114px">
                                                                City
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlCitySearch" ValidationGroup="Search" runat="server" CssClass="simpletxt1"
                                                                    AutoPostBack="True" Width="175px" OnSelectedIndexChanged="ddlCitySearch_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 114px">
                                                                Territory
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlTerritory" ValidationGroup="Search" runat="server" Width="175px"
                                                                    CssClass="simpletxt1" AutoPostBack="True" OnSelectedIndexChanged="ddlTerritory_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <%-- SERVICE CONTRACTOR FIELD ADDED BY BADRISH --%>
                                                        <tr>
                                                            <td style="width: 114px">
                                                                Service Contractor
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtServiceContractorName" ValidationGroup="Search" runat="server"
                                                                    CssClass="simpletxt1" Width="170px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="25" align="left" style="width: 114px">
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <!-- For button portion update -->
                                                                <table>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Button Text="Search" Width="70px" ID="imgBtnSearch" CssClass="btn" runat="server"
                                                                                CausesValidation="False" ValidationGroup="Search" OnClick="imgBtnSearch_Click" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Button ID="imgBtnCancel" Width="70px" runat="server" CssClass="btn" Text="Cancel"
                                                                                OnClick="imgBtnCancel_Click" CausesValidation="False" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <!-- For button portion update end -->
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:HiddenField ID="hdnType" runat="server" />
                                                    <asp:HiddenField ID="hdnIsSCClick" runat="server" />
                                                        <!-- Service Contractor Listing   -->
                                                </td>
                                            </tr>
                                            <tr>
                                            <td>
                                                <%-- custom Paging --%>
                                                <asp:DataList CellPadding="5" RepeatDirection="Horizontal" runat="server" ID="dlPager" onitemcommand="dlPager_ItemCommand">
                                                    <ItemTemplate>
                                                       <asp:LinkButton Enabled='<%#Eval("Enabled") %>' Width="40px" BorderStyle="Ridge" style="text-decoration:none; text-align:center;" runat="server" ID="lnkPageNo" Text='<%#Eval("Text") %>' CommandArgument='<%#Eval("Value") %>' CommandName="PageNo"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </td>
                                        </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblMessageSearch" runat="server" ForeColor="Red" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <!-- End Div open search -->
                    </td>
                    <tr>
                        <td align="center">
                            <asp:CompareValidator Operator="NotEqual" ErrorMessage="Please click on ‘find button’ to allocate Service Contractor"
                                runat="server" ID="cmvddlPer" ControlToValidate="ddlSC" ValueToCompare="0" ValidationGroup="Allow">
                            </asp:CompareValidator>
                        </td>
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblMessage1" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:HiddenField ID="hdnBaseLineID" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:HiddenField ID="hdnComplaintRef" runat="server" />
                            </td>
                        </tr>
                    </tr>
                </tr>
            </table>
            <table width="100%" visible="false" runat="server" id="tbBasicRegistrationInformation">
                <tr>
                    <td align="center">
                        &nbsp;
                    </td>
                </tr>
            </table>
            </td> </tr> </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
