<%@ Page Title="" Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master"
    AutoEventWireup="true" EnableEventValidation="true" CodeFile="PaymentConfirmation.aspx.cs"
    Inherits="SIMS_Pages_PaymentConfirmation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="cntpayconrm" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function funReqDetail(strUrl) {
          window.open(strUrl, 'History', 'height=700,width=1050,left=20,top=30,Location=0,scrollbars=yes');
            return false;
        }

        function validateCheckBoxes() {
            var gv = document.getElementById("<%=gvConfirmation.ClientID%>");
            var rbs = gv.getElementsByTagName("input");
            var flag = 0;
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "checkbox") {
                    if (rbs[i].checked) {
                        flag = 1; break;
                    }
                }
            }
            if (flag == 0) {
                alert("Select bill to confirm.");
                return false;
            }
        }
   
        function CheckInvoiceDate() {

            //debugger
            var days = 0;
            var difference = 0;
            var InvoiceDate;

            InvoiceDate = document.getElementById("ctl00_MainConHolder_txtInvoiceDate").value;
            if (InvoiceDate) {
                var mySplitResult = InvoiceDate.split("/");
                var Invoice_date = mySplitResult[1];
                var Invoice_month = mySplitResult[0];
                var Invoice_Year = mySplitResult[2];

                today = new Date();
                var curr_date = today.getDate();
                var curr_month = today.getMonth();
                curr_month = curr_month + 1;
                var curr_year = today.getFullYear();
                today = curr_month + '/' + curr_date + '/' + curr_year;
                //check current year if greater return with msg
                if (Invoice_Year > curr_year) {
                    document.getElementById("ctl00_MainConHolder_txtInvoiceDate").value = '';
                    alert('Invoice Date should be less than or equal to current date');
                    return false;
                }
                //check if current year is equal than might be possible month or day is higher
                if (Invoice_Year == curr_year) {

                    //check current month if greater return with msg
                    if (Invoice_month > curr_month) {
                        document.getElementById("ctl00_MainConHolder_txtInvoiceDate").value = '';
                        alert('Invoice Date should be less than or equal to current date');
                        return false;
                    }
                    //check current day if greater return with msg
                    if (curr_month == Invoice_month) {

                        if (Invoice_date > curr_date) {
                            document.getElementById("ctl00_MainConHolder_txtInvoiceDate").value = '';
                            alert('Invoice Date should be less than or equal to current date');
                            return false;
                        }
                    }
                }
            }
        }
        
         function SelectAll(id) {
            //get reference of GridView control

            var grid = document.getElementById("<%= gvConfirmation.ClientID %>");
            //variable to contain the cell of the grid
            var cell;

            if (grid.rows.length > 0) {
                //loop starts from 1. rows[0] points to the header.
                for (i = 1; i < grid.rows.length - 1; i++) {
                    //get the reference of first column
                    cell = grid.rows[i].cells[7];

                    //loop according to the number of childNodes in the cell
                    for (j = 0; j < cell.childNodes.length; j++) {
                        //if childNode type is CheckBox                 
                        if (cell.childNodes[j].type == "checkbox") {
                            //assign the status of the Select All checkbox to the cell checkbox within the grid
                            cell.childNodes[j].checked = id.checked;

                        }
                    }
                }
            }
        }
        


    </script>

    <asp:UpdatePanel ID="upda" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Payment Confirmation
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["SIMSAjaxLoadingImageName"]%>" alt="" />
                            </ProgressTemplate>
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
                                            <td colspan="2" align="left" bgcolor="Silver" style="padding:5px;padding-left: 50px;" > 
                                           <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
                                            <ContentTemplate>
                                               
                                              <asp:TextBox ID="txtIBNno" runat="server" ValidationGroup="2" MaxLength="20" ></asp:TextBox> 
                                              <asp:Button Text="Search IBN" ID="BtnGo" ValidationGroup="2" CssClass="btn" runat="server" onclick="BtnGo_Click" />
                                              <asp:Label ID="LblOut" runat="server" />
                                              
                                               <div>
                                                <asp:RequiredFieldValidator ID="rfvibns" runat="server"  Display="Dynamic" ControlToValidate="txtIBNno" ValidationGroup="2" ErrorMessage="*" />
                                              </div>
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" align="right">
                                                Region<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlRegion" AutoPostBack="true" runat="server" CssClass="simpletxt1"
                                                    Width="175px" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvRegion" runat="server" SetFocusOnError="true"
                                                    ErrorMessage="Branch is required." InitialValue="0" ControlToValidate="ddlRegion"
                                                    ValidationGroup="editt" ToolTip="Branch is required." Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" align="right">
                                                Branch<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="simpletxt1" Width="175px"
                                                    OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvBranch" runat="server" SetFocusOnError="true"
                                                    ErrorMessage="Branch is required." InitialValue="0" ControlToValidate="ddlBranch"
                                                    ValidationGroup="editt" ToolTip="Branch is required." Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" align="right">
                                                Service Contractor<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DDlAsc" runat="server" CssClass="simpletxt1" Width="175px"
                                                    OnSelectedIndexChanged="DDlAsc_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RFVScName" runat="server" SetFocusOnError="true"
                                                    ErrorMessage="Service Contractor Name is required." InitialValue="0" ControlToValidate="DDlAsc"
                                                    ValidationGroup="editt" ToolTip="Service Contractor Name is required." Display="Dynamic"></asp:RequiredFieldValidator>
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
                                                    ErrorMessage="Division is required." InitialValue="0" SetFocusOnError="true"
                                                    ValidationGroup="editt" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" align="right">
                                                Payment Mode<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlpaymentmode" runat="server" Width="175px" CssClass="simpletxt1"
                                                    ValidationGroup="editt">
                                                    <asp:ListItem Selected="True" Value="B">BILL WISE (B)</asp:ListItem>
                                                    <asp:ListItem Value="L">LUMPSUM (L) </asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Date From
                                                <div>
                                                    (Logged Date)<font color='red'>*</font></div>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox runat="server" ID="txtFromDate" CssClass="txtboxtxt" ValidationGroup="editt"
                                                    MaxLength="10" />
                                                <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" SetFocusOnError="true"
                                                    ErrorMessage="From Date is required." ControlToValidate="txtFromDate" ValidationGroup="editt"
                                                    ToolTip="From Date is required." Display="Dynamic"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" Type="Date" Operator="DataTypeCheck"
                                                    ControlToValidate="txtFromDate" Display="none" ValidationGroup="editt" SetFocusOnError="true"></asp:CompareValidator>
                                                <cc1:CalendarExtender ID="CEFrom" runat="server" TargetControlID="txtFromDate" Format="MM/dd/yyyy">
                                                </cc1:CalendarExtender>
                                                To<font color='red'>*</font>
                                                <asp:TextBox runat="server" ID="txtToDate" CssClass="txtboxtxt" ValidationGroup="editt"
                                                    MaxLength="10" />
                                                <asp:RequiredFieldValidator ID="rfvToDate" runat="server" SetFocusOnError="true"
                                                    ErrorMessage="To Date is required." ControlToValidate="txtToDate" ValidationGroup="editt"
                                                    ToolTip="To Date is required." Display="Dynamic"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CompareValidator7" runat="server" Type="Date" Operator="DataTypeCheck"
                                                    ControlToValidate="txtToDate" Display="none" ValidationGroup="editt" SetFocusOnError="true"></asp:CompareValidator>
                                                <cc1:CalendarExtender ID="CETo" runat="server" TargetControlID="txtToDate" Format="MM/dd/yyyy">
                                                </cc1:CalendarExtender>
                                                <asp:CompareValidator ID="CompareValidator2" Type="Date" ControlToValidate="txtToDate"
                                                    ControlToCompare="txtFromDate" Operator="GreaterThanEqual" runat="server" ErrorMessage="To Date should be greater than From Date"
                                                    ValidationGroup="editt"></asp:CompareValidator>
                                                <asp:Label ID="lblDateErr" runat="server" ForeColor="Red" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="25" align="left">
                                                &nbsp;
                                            </td>
                                            <td>
                                              <table>
                                                    <tr>
                                                        <td align="right">
                                                            <%-- <asp:Button Text="Add" Width="70px" CssClass="btn" ID="imgBtnAdd" runat="server"
                                                                CausesValidation="True" ValidationGroup="editt" OnClick="imgBtnAdd_Click" />--%>
                                                            <asp:Button Text="Search" Width="70px" ID="btnSearch" CssClass="btn" runat="server"
                                                                CausesValidation="True" ValidationGroup="editt" OnClick="btnSearch_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="imgBtnCancel" Width="70px" runat="server" CausesValidation="false"
                                                                CssClass="btn" Text="Cancel" OnClick="imgBtnCancel_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <!-- Service Contractor Listing   -->
                                         <asp:GridView RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridLines="both" AllowSorting="True"
                                        AutoGenerateColumns="false" ID="gvConfirmation" runat="server" 
                                        Width="100%" HorizontalAlign="Left" EnableSortingAndPagingCallbacks="True"
                                        OnSorting="gvConfirmation_Sorting">
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <%-- PaymentRecID   Created_By Created_Date Modified_By Modified_Date      --%>
                                            <asp:TemplateField HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center"
                                                HeaderText="Division" SortExpression="ProductDivision_Desc">
                                                <ItemTemplate>
                                                    <%#Eval("ProductDivision_Desc")%>
                                                    <asp:HiddenField ID="hdnDivisionID" runat="server" Value='<%#Eval("ProductDivision_ID")%>' />
                                                </ItemTemplate>
                                                <HeaderStyle Width="60px" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"
                                                HeaderText="Branch" SortExpression="Branch_Name">
                                                <ItemTemplate>
                                                    <%#Eval("Branch_Name")%>
                                                    <asp:HiddenField ID="hdnBranchID" runat="server" Value=' <%#Eval("Branch_Sno")%>' />
                                                </ItemTemplate>
                                                <HeaderStyle Width="150px" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Center"
                                                HeaderText="Service Contactor" SortExpression="ASC_Name">
                                                <ItemTemplate>
                                                    <%#Eval("ASC_Name")%>
                                                    <asp:HiddenField ID="hdnASCID" runat="server" Value=' <%#Eval("ASC_ID")%>' />
                                                </ItemTemplate>
                                                <HeaderStyle Width="150px" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"
                                                HeaderText="Internal Bill No" SortExpression="Internal_Bill_No">
                                                <ItemTemplate>
                                                    <a href="#" rel='<%#Eval("Internal_Bill_No")%>' onclick='return funReqDetail("../Pages/InternalBillconfirmation.aspx?asc=<%#Eval("ASC_ID")%>&division=<%#Eval("ProductDivision_ID")%>&bill=<%#Eval("Internal_Bill_No")%>")'>
                                                        <%#Eval("Internal_Bill_No")%>
                                                    </a>
                                                    <asp:HiddenField ID="hdInternalBill" Value='<%#Eval("Internal_Bill_No")%>' runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="150px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Bill_Created_Date" HeaderText="Bill Date" HeaderStyle-Width="60px"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBillDate" runat="server" Text='<%#Eval("Bill_Created_Date")%>'></asp:Label>
                                                    <asp:HiddenField ID="hdnBillDate" Value='<%#Eval("Bill_Date") %>' runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Amount" HeaderText="Amount" HeaderStyle-Width="60px"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>
                                                    <%--<asp:TextBox ID="txtAmount" runat="server" Text='<%#Eval("Amount")%>' AutoPostBack="true"></asp:TextBox> --%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="CSV" HeaderText="Rejected/Hold Complain No." HeaderStyle-Width="200px"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHold" Width="100px" Style="width: 400px;word-break: break-all" runat="server" Text='<%#Eval("CSV")%>'></asp:Label>
                                                    <%--                                                    <asp:TextBox ID="txtAmount" runat="server" Text='<%#Eval("Amount")%>' AutoPostBack="true"></asp:TextBox>--%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkHeader" runat="server" onclick="javascript:SelectAll(this);"
                                                        AutoPostBack="true" OnCheckedChanged="chkActivityConfirm_CheckedChanged" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkActivityConfirm" runat="server" AutoPostBack="true" OnCheckedChanged="chkActivityConfirm_CheckedChanged" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkMprocessed" runat="server" Text="Processed" OnClick="lnkMprocessed_Click" />
                                                    <cc1:ConfirmButtonExtender ID="cbe" runat="server" ConfirmText="Bill has been fully processed manually ?" TargetControlID="lnkMprocessed" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
                                    <!-- End Service Contractor Listing -->
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <div style="text-align: center" runat="server" id="divTotalsum">
                                        <table style="text-align: left" width="70%">
                                            <tr style="display:none">
                                                <td align="right">
                                                    Service tax applicable :
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkServiceTax" AutoPostBack="true" runat="server" OnCheckedChanged="chkServiceTax_CheckedChanged" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    Total Sum :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="lblTotalsum" runat="server" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr style="display:none">
                                                <td align="right">
                                                    Base Amount For Service Tax<font color='red'>*</font> :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtTotalSum" runat="server" MaxLength="13" AutoPostBack="true" OnTextChanged="txtTotalSum_TextChanged"></asp:TextBox>
                                                    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    Service Tax @:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlServiceTaxRate" runat="server" 
                                                        ValidationGroup="confirm" CssClass="simpletxt1" >
                                                    </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RfvTaxRate" runat="server" ControlToValidate="ddlServiceTaxRate" ErrorMessage="required." Enabled="False" 
                                                        SetFocusOnError="true" ValidationGroup="confirm" InitialValue="*()"></asp:RequiredFieldValidator>
                                                    <asp:Label ID="lblServiceTax" runat="server" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr style="display:none">
                                                <td align="right">
                                                  VAT /others charges:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlvat" runat="server" AutoPostBack="true"
                                                        ValidationGroup="confirm" CssClass="simpletxt1" 
                                                        OnSelectedIndexChanged="ddlvat_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="5P">5%</asp:ListItem>
                                                        <asp:ListItem Value="8P">8%</asp:ListItem>
                                                        <asp:ListItem Value="12.50P">12.50%</asp:ListItem>
                                                        <asp:ListItem Value="5P_ON_80P">5% on 80% of bill amt.</asp:ListItem>
                                                    </asp:DropDownList>
                                                
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    WCT</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlwtc" runat="server" ValidationGroup="confirm" 
                                                        CssClass="simpletxt1" Width="80px" >
                                                    
                                                        <asp:ListItem Selected="True">N</asp:ListItem>
                                                        <asp:ListItem>Y</asp:ListItem>
                                                    
                                                    </asp:DropDownList>
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    Miscellaneous 1:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TxtMis1" runat="server" Text="0" />
                                                    <asp:RegularExpressionValidator ID="REVTxtMis1" runat="server" ControlToValidate="TxtMis1" ValidationExpression="\d{1,4}" ErrorMessage="Use valid amount."
                                                        SetFocusOnError="true" ValidationGroup="confirm" />
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td align="right">
                                                   Miscellaneous 2:
                                                </td>
                                                <td>
                                                 <asp:TextBox ID="TxtMis2" runat="server" Text="0" />
                                                 <asp:RegularExpressionValidator ID="REVTxtMis2" runat="server" ControlToValidate="TxtMis2" ValidationExpression="\d{1,4}" ErrorMessage="Use valid amount."
                                                        SetFocusOnError="true" ValidationGroup="confirm" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    Total Payable :
                                                </td>
                                                <td>
                                                    <%--<asp:Label ID="lblTotalPaybleAmt" runat="server"></asp:Label>--%>
                                                    <asp:TextBox ID="TxtTotalPaybleAmt" runat="server" />
                                                    <asp:RequiredFieldValidator ID="RFVpaybleAmunt" runat="server" ControlToValidate="TxtTotalPaybleAmt" ErrorMessage="required."
                                                        SetFocusOnError="true" ValidationGroup="confirm"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    ASC Invoive Number<font color='red'>*</font> :
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtInvoiceNumber" MaxLength="16" Text="" ValidationGroup="confirm"></asp:TextBox>
                                                    <asp:RequiredFieldValidator
                                                        ID="rfvInvoiceNumber" runat="server" ControlToValidate="txtInvoiceNumber" ErrorMessage="Invoice Number is required."
                                                        SetFocusOnError="true" ValidationGroup="confirm"></asp:RequiredFieldValidator>
                                                    <asp:HiddenField ID="hdnTransactionNo" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    ASC Invoive Date<font color='red'>*</font> :
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox runat="server" ID="txtInvoiceDate" CssClass="txtboxtxt" ValidationGroup="confirm"
                                                        MaxLength="10" />
                                                    <asp:RequiredFieldValidator ID="rfvInvoiceDate" runat="server" ControlToValidate="txtInvoiceDate"
                                                        ErrorMessage="Required." SetFocusOnError="True" ValidationGroup="confirm"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="CompareValidator3" runat="server" Type="Date" Operator="DataTypeCheck"
                                                        ControlToValidate="txtInvoiceDate" Display="none" ValidationGroup="confirm" SetFocusOnError="true"></asp:CompareValidator>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtInvoiceDate">
                                                    </cc1:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                 <%-- Remating part BP 29Aug12--%>
                                                    Detail<font color='red'>*</font> :
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox runat="server" ID="txtDetail" CssClass="txtboxtxt" ValidationGroup="confirm"
                                                        MaxLength="50" />
                                                    <asp:RequiredFieldValidator ID="rfvDetail" runat="server" ControlToValidate="txtDetail"
                                                        ErrorMessage="Required." SetFocusOnError="True" ValidationGroup="confirm"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnConfirmpayment" OnClientClick="if(CheckInvoiceDate()) return false;"
                                                       Width="100px" Text="Confirm payment" runat="server" CssClass="btn" ValidationGroup="confirm"
                                                         OnClick="btnConfirmpayment_Click" />
                                                    <asp:CheckBox ID="ChkGenerateSAPNo" runat="server" 
                                                        Text="Generate SAP Transaction" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblMessage" Font-Bold="true" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
