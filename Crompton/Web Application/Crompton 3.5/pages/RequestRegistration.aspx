<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="RequestRegistration.aspx.cs" MaintainScrollPositionOnPostback="true"
    Inherits="pages_RequestRegistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script language="javascript" type="text/javascript">

        function funOpenDivSC() {
            var strProductDivision = document.getElementById('ctl00_MainConHolder_ddlProductDiv');
            var strState = document.getElementById('ctl00_MainConHolder_ddlState');
            var strCity = document.getElementById('ctl00_MainConHolder_ddlCity');
            var strTerr = document.getElementById('ctl00_MainConHolder_ddlSC');
            if (strState.value == "Select") {
                alert("Please select State");
                strState.focus();
                return false;
            }
            if (strCity.value == "Select") {
                alert("Please select City");
                strCity.focus();
                return false;
            }
            if (strProductDivision.value == "0") {
                alert("Please select Product Division");
                strProductDivision.focus();
                return false;
            }
            document.getElementById("dvSearch").style.display = 'block';

        }
        function funConfirm() {
            if (confirm('Are you sure to cancel this request?')) {
                return true;
            }
            return false;
        }
        function funOpenDivCS() {
            var strUrl = '../pages/TerritoryCitySearch.aspx';
            newWin = window.open(strUrl, 'MyWin', 'height=600,width=700,scrollbars=1');
            if (window.focus) { newWin.focus(); }
        }
      // Added for Customer Search 1 Apr 12           
        function hideShowCustomer() {
            var ele = document.getElementById('ctl00_MainConHolder_pnlCustomerSearch');
            if(ele.style.display == "none") 
                     ele.style.display = 'block';
              else
                     ele.style.display = 'none';
                return false;
        }
        
      
    </script>
    <table width="100%">
        <tr>
       
            <td class="headingred">
          
                New Service Registration
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="bgcolorcomm" colspan="2">
                <table width="99%">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="pnl" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <table width="100%">
                                        <tr height="20">
                                            <td align="right" width="87%">
                                                <asp:LinkButton ID="lnkbtnCustomerSearch" runat="server" 
                                                    OnClientClick="return hideShowCustomer()" CausesValidation="false" OnClick="lnkbtnCustomerSearch_Click">Show Customer Search</asp:LinkButton>
                                                <asp:HiddenField ID="hdnCustomerId" Value="0" runat="server" />
                                            </td>
                                            <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                                                <asp:UpdateProgress AssociatedUpdatePanelID="pnl" ID="UpdateProgress1" runat="server">
                                                    <ProgressTemplate>
                                                        <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" class="bgcolorcomm">
                                                <asp:Panel ID="pnlCustomerSearch" runat="server" style="display:none">
                                                    <!-- Customer Search Start -->
                                                    <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                                        <tr>
                                                            <td width="20%" style="padding-left: 40px">
                                                                Phone:<font color='red'>*</font>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtUnique" ValidationGroup="CustomerSearchNew" CssClass="txtboxtxt"
                                                                    runat="server" MaxLength="11" Text=""></asp:TextBox>&nbsp;<asp:Button ID="btnSearchCustomer" CommandName="PHONE"
                                                                        CssClass="btn" ValidationGroup="CustomerSearchNew" runat="server" Text="Search"  
                                                                        OnClick="btnSearchCustomer_Click" />
                                                                &nbsp;<asp:RequiredFieldValidator ControlToValidate="txtUnique" ValidationGroup="CustomerSearchNew"
                                                                    ID="RequiredFieldValidator8" runat="server" 
                                                                    ErrorMessage="Valid phone Number is required." Display="Dynamic" 
                                                                    EnableViewState="False"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator
                                                                        ID="RegularExpressionValidator6" runat="server" ErrorMessage="Valid Phone Number is required."
                                                                        ControlToValidate="txtUnique" SetFocusOnError="True" ValidationGroup="CustomerSearchNew"
                                                                        ValidationExpression="\d{10,11}" Display="Dynamic" 
                                                                    EnableViewState="False"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="20%" style="padding-left: 40px">
                                                                Customer ID:<font color='red'>*</font>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCustomerID" ValidationGroup="CustIDSearch" CssClass="txtboxtxt"
                                                                    runat="server" MaxLength="15" Text=""></asp:TextBox>&nbsp;<asp:Button ID="BtnCustSearchByID"
                                                                        CssClass="btn" ValidationGroup="CustIDSearch" runat="server" Text="Search"
                                                                        OnClick="btnSearchCustomer_Click"   />
                                                                &nbsp;<asp:RequiredFieldValidator ControlToValidate="txtCustomerID" ValidationGroup="CustIDSearch"
                                                                    ID="RequiredFieldValidator6" runat="server" 
                                                                    ErrorMessage="Valid CustomerID is required." Display="Dynamic" 
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" 
                                                                    runat="server" ErrorMessage="Valid CustomerID is required."
                                                                        ControlToValidate="txtCustomerID" SetFocusOnError="True" ValidationGroup="CustIDSearch"
                                                                        ValidationExpression="\d{8}" Display="Dynamic" EnableViewState="False"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" align="center">
                                                                <asp:Label ID="lblCustMsg" runat="server" ForeColor="Red" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <!-- Users List Start Grid -->
                                                                <%--<div id="dvCustomerSearch" style="display: none;">--%>
                                                                <%--</div>--%>
                                                                <!-- Users LIst End Grid -->
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <!-- Customer Search End -->
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" class="bgcolorcomm">
                                                <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                                    <tr>
                                                        <td colspan="3">
                                                            <b>Customer Information:</b> &nbsp;
                                                        </td>
                                                        <td align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                                            <asp:HiddenField ID="hdnTerritoryDesc" runat="server" />
                                                            <asp:HiddenField ID="hdnScNo" runat="server" />
                                                            <font color='red'>*</font>
                                                            <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="120px">
                                                            Prefix
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:DropDownList ID="ddlPrefix" runat="server" CssClass="simpletxt1">
                                                                <asp:ListItem Selected="True" Value="Mr">Mr.</asp:ListItem>
                                                                <asp:ListItem Value="Ms">Ms.</asp:ListItem>
                                                                <asp:ListItem Value="Mrs">Mrs.</asp:ListItem>
                                                                <asp:ListItem Value="Dr">Dr.</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="120px">
                                                            First Name<font color="red">*</font>
                                                        </td>
                                                        <td width="350px">
                                                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtboxtxt" Width="158px"
                                                                MaxLength="30"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFirstName"
                                                                ErrorMessage="First Name is required." SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                                <div>
                                                           <%--    <asp:RegularExpressionValidator ID="RegularExpressiontxtFirstName" runat="server" ErrorMessage="Special characters not allowed."
                                                                ControlToValidate="txtFirstName" Display="Dynamic" SetFocusOnError="True" ValidationExpression= "([a-z]|[A-Z]|[0-9]|[ ]|[-]|[_])*" 
                                                                ></asp:RegularExpressionValidator>--%>
                                                                </div>
                                                        </td>
                                                        <td align="right" width="120px">
                                                            Last Name
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtLastName" runat="server" CssClass="txtboxtxt" Width="158px" MaxLength="20"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtLastName"
                                                                ErrorMessage="Last Name is required." SetFocusOnError="true" 
                                                                Display="None" Enabled="False"></asp:RequiredFieldValidator>
																<%--<div><asp:RegularExpressionValidator ID="RegularExpressiontxtLastName" 
                                                                        runat="server" ErrorMessage="Special characters not allowed."
                                                                ControlToValidate="txtLastName" Display="Dynamic" SetFocusOnError="True" 
                                                                        ValidationExpression= "([a-z]|[A-Z]|[0-9]|[ ]|[-]|[_])*" Enabled="False" 
                                                                ></asp:RegularExpressionValidator></div>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Customer Type
                                                        </td>
                                                        <td style="width: 24%">
                                                            <asp:DropDownList ID="ddlCustomerType" runat="server" CssClass="simpletxt1">
                                                                <asp:ListItem Selected="True" Value="D">Domestic</asp:ListItem>
                                                                <asp:ListItem Value="I">Industrial</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="right" style="width: 8%">
                                                           Call Type
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlcalltype" runat="server" CssClass="simpletxt1">
                                                                <asp:ListItem Selected="True" Value="1">New Customer Complaint</asp:ListItem>
                                                                <asp:ListItem Value="2">New Complaint Existing Customer</asp:ListItem>
                                                                <asp:ListItem Value="3">Repeated Complaint</asp:ListItem>
                                                            </asp:DropDownList>
                                                           <asp:CheckBox ID="chkEscalated" Text="Escalated (Y/N)" runat="server" Checked="false" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Company Name
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox runat="server" MaxLength="150" Width="350Px" CssClass="txtboxtxt" ID="txtCompanyName"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Address1<font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox runat="server" MaxLength="50" Width="350Px" CssClass="txtboxtxt" ID="txtAdd1" ></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" SetFocusOnError="true"
                                                                ControlToValidate="txtAdd1" ErrorMessage="Address1 is required." Display="None"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Address2
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtAdd2" runat="server" CssClass="txtboxtxt" MaxLength="49" Width="350px"></asp:TextBox>
                                                  <%--           <asp:RegularExpressionValidator ID="RegularExpressiontxtAdd2" runat="server" ErrorMessage="Special characters not allowed."
                                                                ControlToValidate="txtAdd2" SetFocusOnError="True" ValidationExpression= "([a-z]|[A-Z]|[0-9]|[ ]|[-]|[_])*" 
                                                                ></asp:RegularExpressionValidator>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Landmark
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtLandmark" runat="server" CssClass="txtboxtxt" MaxLength="150"
                                                                Width="350px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            State<font color='red'>*</font>
                                                        </td>
                                                        <td style="width: 24%">
                                                            <asp:DropDownList Width="170px" runat="server" CssClass="simpletxt1" ID="ddlState"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator1" runat="server"
                                                                ControlToValidate="ddlState" ErrorMessage="State is required." Operator="NotEqual"
                                                                ValueToCompare="Select" Display="None"></asp:CompareValidator>
                                                        </td>
                                                        <td style="width: 8%" valign="top" align="right">
                                                            City<font color='red'>*</font>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:DropDownList Width="170px" ID="ddlCity" AutoPostBack="true" runat="server" CssClass="simpletxt1"
                                                                OnSelectedIndexChanged="ddlCity_SelectedIndexChanged">
                                                                <asp:ListItem Text="Select" Value="Select" Selected="True"></asp:ListItem>
                                                            </asp:DropDownList>
                                                             <input type="button" id="Button1" value="???" 
                                                            class="btn" onclick="return funOpenDivCS();" />
                                                            <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator2" runat="server"
                                                                ControlToValidate="ddlCity" ErrorMessage="City is required." Operator="NotEqual"
                                                                ValueToCompare="Select" Display="None"></asp:CompareValidator>
                                                            <asp:TextBox ID="txtOtherCity" Width="100px" MaxLength="30" runat="server" CssClass="txtboxtxt"></asp:TextBox><asp:RequiredFieldValidator
                                                                ID="reqCityOther" runat="server" SetFocusOnError="true" Display="None" ErrorMessage="Other City is required."
                                                                ControlToValidate="txtOtherCity"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Pin Code
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtPinCode" EnableViewState="true" onkeypress="javascript:return checkNumberOnly(event);"
                                                             runat="server" AutoPostBack="true" CssClass="txtboxtxt" MaxLength="6" 
                                                                OnTextChanged="txtPinCode_TextChanged"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Valid pin is required."
                                                                ControlToValidate="txtPinCode" SetFocusOnError="True" ValidationExpression="\d{6}"
                                                                Display="None"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" height="24">
                                                            <b>Contact Information:</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Mode Of Receipt
                                                        </td>
                                                        <td style="width: 28%">
                                                            <asp:DropDownList ID="ddlModeOfRec" runat="server" CssClass="simpletxt1" 
                                                                Width="170px">
                                                            </asp:DropDownList>
                                                            <asp:CheckBox ID="chkDefautMode" Text="Set as Default" runat="server" AutoPostBack="True"
                                                                OnCheckedChanged="chkDefautMode_CheckedChanged" />
                                                        </td>
                                                        <td style="width: 8%" align="right">
                                                            Language
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList Width="170px" ID="ddlLanguage" runat="server" CssClass="simpletxt1">
                                                                <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:CheckBox ID="chkLanguage" Text="Set as Default" runat="server" AutoPostBack="True"
                                                                OnCheckedChanged="chkLanguage_CheckedChanged" />
                                                            <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator3" runat="server"
                                                                ControlToValidate="ddlLanguage" ErrorMessage="Language is required." Operator="NotEqual"
                                                                ValueToCompare="0">*</asp:CompareValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>  <%--added by vikas on 15 May 2013--%>
                                                        <td>
                                                            Source Of Complaint<font color='red'>*</font>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlSourceOfComp" runat="server" AutoPostBack="True" 
                                                                onselectedindexchanged="ddlSourceOfComp_SelectedIndexChanged" CssClass="simpletxt1" Width="170px">
                                                                <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="CC-Customer" Value="CC-Customer"></asp:ListItem>
                                                                <asp:ListItem Text="CC-Dealer" Value="CC-Dealer"></asp:ListItem>
                                                                <asp:ListItem Text="CC-ASC" Value="CC-ASC"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvSOC" runat="server" ControlToValidate="ddlSourceOfComp"
                                                                ErrorMessage="Source Of Complaint is Required." SetFocusOnError="True" InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblComplaintType" Visible="false" runat="server" Text="Type Of Complaint"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlDealer" runat="server" Visible="false" CssClass="simpletxt1" Width="170px">
                                                                <asp:ListItem Text="Complaint for Dealer Stock Piece"></asp:ListItem>
                                                                <asp:ListItem Text="Complaint for Customer Piece"></asp:ListItem>
                                                                <asp:ListItem Text="Complaint for another Dealer or Retailer Piece"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlASC" runat="server" Visible="false" CssClass="simpletxt1" Width="170px">
                                                                <asp:ListItem Text="Complaint for Dealer"></asp:ListItem>
                                                                <asp:ListItem Text="Complaint for Customer"></asp:ListItem>
                                                            </asp:DropDownList>  
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Customer Mobile No.<font color="red">*</font>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtContactNo" Width="213px" placeholder="Please enter 10-digit Mobile # ONLY" runat="server" CssClass="txtboxtxt" 
                                                                MaxLength="10"></asp:TextBox>
                                                            <div>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                                    ControlToValidate="txtContactNo" Display="None" 
                                                                    ErrorMessage="Contact Number is required." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                                                                    ControlToValidate="txtContactNo" Display="None" 
                                                                    ErrorMessage="Valid Contact Number is required." SetFocusOnError="True" 
                                                                    ValidationExpression="\d{10,11}"></asp:RegularExpressionValidator>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            Calling No.
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCallingNo" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Alternate Contact No.
                                                        </td>
                                                        <td style="width: 28%">
                                                            <asp:TextBox runat="server" MaxLength="11" ID="txtAltConatctNo" Width="213px" CssClass="txtboxtxt"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Valid Alternate Contact Number is required."
                                                                ControlToValidate="txtAltConatctNo" SetFocusOnError="True" ValidationExpression="\d{10,11}"
                                                                Display="None"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td style="width: 8%" align="right">
                                                            Extension
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtExtension" MaxLength="5" runat="server" CssClass="txtboxtxt"></asp:TextBox>
                                                            <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Valid Extension is required."
                                                                Operator="DataTypeCheck" ControlToValidate="txtExtension" Type="Integer" Display="None"></asp:CompareValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            E-Mail
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtEmail" MaxLength="60" runat="server" CssClass="txtboxtxt" Width="213px"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Valid Email is required."
                                                                ControlToValidate="txtEmail" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                Display="None"></asp:RegularExpressionValidator>
 						    </td>
                                                        <td>  
                                                        </td>
                                                        <td>        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Fax No.
                                                        </td>
                                                        <td style="width: 28%">
                                                            <asp:TextBox ID="txtFaxNo" runat="server" Width="213px" CssClass="txtboxtxt" MaxLength="11"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtFaxNo"
                                                                ErrorMessage="Valid Fax is required." SetFocusOnError="True" ValidationExpression="\d{10,11}"
                                                                Display="None"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td style="width: 8%"> 
                                                        </td>
                                                        <td> 
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Appointment Req.
                                                        </td>
                                                        <td style="width: 28%">
                                                            <asp:CheckBox ID="chkAppointment" runat="server" />
                                                        </td>
                                                        <td style="width: 8%">
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkUpdateCustomerData" Text="Update Customer Info" 
                                                            Checked="false"
                                                                runat="server" />
                                                        </td>
                                                    </tr>
                                                    
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnFileUpload" />
                                </Triggers>
                                <ContentTemplate>
                                    <table width="100%">
                                        <tr height="20">
                                            <td class="bgcolorcomm">
                                                <table width="99%">
                                                    <tr height="20">
                                                        <td style="width: 15%">
                                                            Upload File
                                                        </td>
                                                        <td>
                                                            <input type="file" class="btn" id="flUpload" runat="server" onkeydown="if(event.keyCode==9){return true;}else{return false;}" />&nbsp;<asp:Button
                                                                ID="btnFileUpload" runat="server" CssClass="btn" CausesValidation="false" Text="Upload"
                                                                OnClick="btnFileUpload_Click" />
                                                        </td>
                                                        <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                                                            <asp:UpdateProgress AssociatedUpdatePanelID="UpdatePanel1" ID="UpdateProgress2" runat="server">
                                                                <ProgressTemplate>
                                                                    <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                                                                </ProgressTemplate>
                                                            </asp:UpdateProgress>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:GridView ID="gvFiles" runat="server" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                                                        HeaderStyle-CssClass="fieldNamewithbgcolor" AutoGenerateColumns="False" BorderStyle="None"
                                                                        GridLines="none" Width="100%" OnPageIndexChanging="gvFiles_PageIndexChanging"
                                                                        OnRowDeleting="gvFiles_RowDeleting">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="File Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Height="25px">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("FileName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:CommandField ShowDeleteButton="True" DeleteText="Remove" HeaderText="Action"
                                                                                HeaderStyle-HorizontalAlign="Left" />
                                                                        </Columns>
                                                                        <AlternatingRowStyle BorderStyle="None" />
                                                                    </asp:GridView>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="bgcolorcomm">
                                                <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                                    <tr>
                                                        <td colspan="4">
                                                            <b>Product & Complaint Information:</b> &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 15%">
                                                            Product Division<font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:DropDownList Width="175px" ID="ddlProductDiv" AutoPostBack="true" runat="server"
                                                                CssClass="simpletxt1" OnSelectedIndexChanged="ddlProductDiv_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:CompareValidator SetFocusOnError="true" ID="compValProdDiv" runat="server" ControlToValidate="ddlProductDiv"
                                                                ErrorMessage="Product Division is required." Operator="NotEqual" ValueToCompare="0"
                                                                Display="None"></asp:CompareValidator>
                                                        </td>
                                                    </tr>
                                                    <tr id="trFrames" runat="server">
                                                        <td>
                                                            Frames<font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtFrames" runat="server" CssClass="txtboxtxt" onkeypress="javascript:return checkNumberOnly(event);"
                                                                MaxLength="3" Text="" Width="50px" AutoPostBack="True" OnTextChanged="txtFrames_TextChanged"></asp:TextBox><asp:RequiredFieldValidator
                                                                    ID="reqValFrames" SetFocusOnError="true" runat="server" ControlToValidate="txtFrames"
                                                                    ErrorMessage="Frames is required." Display="None"></asp:RequiredFieldValidator><asp:CompareValidator
                                                                        ID="CompareValidator4" runat="server" SetFocusOnError="true" ErrorMessage="No. of Frames is required."
                                                                        Operator="DataTypeCheck" ControlToValidate="txtFrames" Type="Integer" Display="None"></asp:CompareValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Product Line
                                                        </td>
                                                        <td style="width: 21%">
                                                            <asp:DropDownList Width="175px" ID="ddlProductLine" runat="server" CssClass="simpletxt1"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlProductLine_SelectedIndexChanged">
                                                                <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:Button ID="btnGo" runat="server" CausesValidation="false" CssClass="btn" 
                                                                OnClick="btnGo_Click" OnClientClick="return funOpenDivSC();" Text="Find" 
                                                                Visible="false" />
                                                        </td>
                                                        <td>
                                                            
                                                        </td>
                                                        <td>
                                                 
                                                            <%--<input type="button" id="btnGo" value="Find" class="btn" onclick="return funOpenDivSC();" />--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Territory
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:DropDownList Width="175px" ID="ddlSC" runat="server" CssClass="simpletxt1" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlSC_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            
                                                        
                                                       
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Quantity<font color='red'>*</font>
                                                        </td>
                                                        <td style="width: 35%">
                                                            <asp:TextBox ID="txtQuantity" runat="server" CssClass="txtboxtxt" MaxLength="2" Text="1"
                                                                Width="50px" onkeypress="javascript:return checkNumberOnly(event);"></asp:TextBox><asp:RequiredFieldValidator
                                                                    ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Quantity is required."
                                                                    Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator><asp:CompareValidator
                                                                        ID="CompareValidator6" runat="server" ErrorMessage="Valid Quantity is required."
                                                                        Operator="DataTypeCheck" ControlToValidate="txtQuantity" Type="Integer" Display="None"
                                                                        SetFocusOnError="true"></asp:CompareValidator>
                                                        </td>
                                                       
                                                        <td align="left" colspan="2">
                                                        SRF.(Tick if yes)&nbsp;
                                                        <asp:CheckBox ID="chkSRF" Checked="false" runat="server" />
                                                        
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            Complaint Details<font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtComplaintDetails" runat="server" CssClass="txtboxtxt" Width="380px"
                                                                TextMode="MultiLine" onkeydown="return CheckMaxLength(this,500,event);" Height="72px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator SetFocusOnError="true" ID="reqValComplaint" runat="server"
                                                                Display="None" ControlToValidate="txtComplaintDetails" ErrorMessage="Complaint Details is required."></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Invoice No.
                                                        </td>
                                                        <td style="width: 21%">
                                                            <asp:TextBox runat="server" ID="txtInvoiceNum" CssClass="txtboxtxt" MaxLength="50"
                                                                ValidationGroup="Report" />
                                                        </td>
                                                        <td colspan="2">
                                                            Purchase Date&nbsp;<asp:TextBox MaxLength="10" runat="server" ID="txtxPurchaseDate"
                                                                CssClass="txtboxtxt" /><asp:CompareValidator ID="CompareValidator7" runat="server"
                                                                    Type="Date" Operator="DataTypeCheck" ControlToValidate="txtxPurchaseDate" Display="None"
                                                                    ErrorMessage="Valid Purchase date is required. "></asp:CompareValidator>
                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtxPurchaseDate">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Dealer Name
                                                        </td>
                                                        <td style="width: 21%">
                                                            <asp:TextBox ID="txtxPurchaseFrom" runat="server" CssClass="txtboxtxt" ValidationGroup="Report" />
                                                        </td>
                                                        <td align="left">
                                                           <asp:Label ID="lblVisitCharge" runat="server" Visible="false" Text="0"></asp:Label>&nbsp;<%--Rs.--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Service Contractor<font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3" align="left">
                                                            <asp:TextBox ID="txtSc" CssClass="txtboxtxt" Width="250px" Enabled="false" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                                                ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtSc" ErrorMessage="Service Contractor is required."
                                                                Display="None"></asp:RequiredFieldValidator>
                                                                <asp:CheckBox ID="chkMultipleRegistration" runat="server" Checked="false" 
                                                                Text="Multiple Registration" />
                                                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn" CausesValidation="false"
                                                                OnClick="btnReset_Click" ToolTip="Reset Complaint Information" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:HiddenField ID="hdnScId" runat="server" />
                                                        </td>
                                                        <td align="left" colspan="3">
                                                                                                                    </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            <asp:LinkButton OnClientClick="return funOpenDivSC();" CausesValidation="false" ID="lnkBtnSCSelection"
                                                                runat="server" OnClick="lnkBtnSCSelection_Click">Service Contractor Selection</asp:LinkButton>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblMsgSC" runat="server" ForeColor="Red" Text=""></asp:Label>
                                                        </td>
                                                        <td width="5%">
                                                            <asp:ImageButton CausesValidation="false" ID="imgbtnCloseSC" runat="server" ImageUrl="~/images/close.gif"
                                                                OnClick="imgbtnCloseSC_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <!-- Div Open  for search -->
                                                            <div id="dvSearch" class="dvInsertI" style="display: none; width: 860px;">
                                                                <asp:UpdatePanel ID="UpdatePanel31" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td style="padding: 1px" align="center" class="bgcolorcomm">
                                                                                    <table width="98%" border="0">
                                                                                        <tr>
                                                                                            <td align="right">
                                                                                                <asp:Button ID="btnSuspenceAccount" CausesValidation="false" CssClass="btn" runat="server"
                                                                                                    Text="Suspence Account" OnClick="btnSuspenceAccount_Click" />&nbsp;&nbsp;&nbsp;
                                                                                                <asp:Button ID="btnFindMore" CausesValidation="false" CssClass="btn" runat="server"
                                                                                                    Text="Find more" OnClick="btnFindMore_Click" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td width="100%" align="left">
                                                                                                <table width="98%" border="0" id="tableHeader" runat="server">
                                                                                                    <tr>
                                                                                                        <td style="width: 114px">
                                                                                                            Product Line&nbsp;
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:DropDownList Width="175px" ID="ddlProductLineSearch" runat="server" CssClass="simpletxt1"
                                                                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlProductLineSearch_SelectedIndexChanged">
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
                                                                                                    <tr>
                                                                                                        <td style="width: 114px">
                                                                                                            Landmark
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:DropDownList ID="ddlLadmarkSearch" runat="server" CssClass="simpletxt1" ValidationGroup="Search1"
                                                                                                                Width="175px" AutoPostBack="True" OnSelectedIndexChanged="ddlLadmarkSearch_SelectedIndexChanged">
                                                                                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                            &nbsp;
                                                                                                            <asp:TextBox ID="txtSearchLandmark" Width="70px" MaxLength="30" runat="server" CssClass="txtboxtxt"></asp:TextBox>
                                                                                                            &nbsp;<asp:Button Text="Go" CssClass="btn" ID="imgBtnGoLandmark" runat="server" CausesValidation="false"
                                                                                                                OnClick="imgBtnGoLandmark_Click" />
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
                                                                                                <div id="dvGrid" style="width: 850px; height: 200px; overflow: auto;">
                                                                                                    <!-- Service Contractor Listing   -->
                                                                                                    <asp:GridView ID="gvCommSearch" runat="server" AllowPaging="True" AllowSorting="True"
                                                                                                        AlternatingRowStyle-CssClass="fieldName" AutoGenerateColumns="False" DataKeyNames="Sc_Sno"
                                                                                                        HeaderStyle-CssClass="fieldNamewithbgcolor" HorizontalAlign="Left" OnPageIndexChanging="gvCommSearch_PageIndexChanging"
                                                                                                        OnSelectedIndexChanging="gvCommSearch_SelectedIndexChanging" PageSize="5" RowStyle-CssClass="gridbgcolor"
                                                                                                        Width="100%" OnRowDataBound="gvCommSearch_RowDataBound">
                                                                                                        <RowStyle CssClass="gridbgcolor" />
                                                                                                        <Columns>
                                                                                                            <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                                                                                <HeaderStyle Width="40px" />
                                                                                                            </asp:BoundField>
                                                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Territory" ItemStyle-HorizontalAlign="Left">
                                                                                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:HiddenField ID="hdnPreference" runat="server" Value='<%#Eval("Preference")%>' />
                                                                                                                    <asp:HiddenField ID="hdnTrr" runat="server" Value='<%#Eval("Territory_Sno")%>' />
                                                                                                                    <asp:Label ID="lblTRR" runat="server" Text='<%#Eval("Territory_Desc") %>'></asp:Label></ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Landmark" ItemStyle-HorizontalAlign="Left">
                                                                                                                <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblLM" runat="server" Text='<%#Eval("Landmark_Desc") %>'></asp:Label></ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:BoundField DataField="SpecialRemarks" HeaderStyle-HorizontalAlign="Left" HeaderText="Remarks"
                                                                                                                ItemStyle-HorizontalAlign="Left">
                                                                                                                <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                                            </asp:BoundField>
                                                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="SC Name" ItemStyle-HorizontalAlign="Left">
                                                                                                                <HeaderStyle HorizontalAlign="Left" Width="200px" />
                                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblSC" runat="server" Text='<%#Eval("SC_Name") %>'></asp:Label></ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:BoundField DataField="Contact_Person" HeaderStyle-HorizontalAlign="Left" HeaderText="Contact Person"
                                                                                                                ItemStyle-HorizontalAlign="Left">
                                                                                                                <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                                            </asp:BoundField>
                                                                                                            <asp:BoundField DataField="Address1" HeaderStyle-HorizontalAlign="Left" HeaderText="Address"
                                                                                                                ItemStyle-HorizontalAlign="Left">
                                                                                                                <HeaderStyle HorizontalAlign="Left" Width="320px" />
                                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                                            </asp:BoundField>
                                                                                                            <asp:BoundField DataField="PhoneNo" HeaderStyle-HorizontalAlign="Left" HeaderText="Phone No"
                                                                                                                ItemStyle-HorizontalAlign="Left">
                                                                                                                <HeaderStyle HorizontalAlign="Left" Width="80px" />
                                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                                            </asp:BoundField>
                                                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="80px" HeaderText="Weekly Off Day"
                                                                                                                ItemStyle-HorizontalAlign="Left">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:HiddenField ID="hdnGridScNo" runat="server" Value='<%#Eval("Sc_Sno")%>' />
                                                                                                                    <asp:HiddenField ID="hdnGridTerritoryDesc" runat="server" Value='<%#Eval("Territory_Desc")%>' />
                                                                                                                    <asp:HiddenField ID="hdnGridWO" runat="server" Value='<%#Eval("Weekly_Off_Day")%>' />
                                                                                                                    <%#Eval("Weekly_Off_Day") %>
                                                                                                                    <%--<asp:LinkButton ID="btnLandmark" CausesValidation="false" CommandArgument='<%#Eval("Territory_Sno")%>'
                                                                                                                            runat="server" Text="Landmark" OnClick="btnLandmark_Click"></asp:LinkButton>--%>
                                                                                                                </ItemTemplate>
                                                                                                                <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:ButtonField ButtonType="Link" CommandName="Select" HeaderText="Select" Text="Select" />
							                                                                            </Columns>
							                                                                            <EmptyDataTemplate>
							                                                                                <img alt="" src='<%=ConfigurationManager.AppSettings["UserMessage"]%>' /><b>No 
							                                                                                records found</b>
							                                                                            </EmptyDataTemplate>
							                                                                            <HeaderStyle CssClass="fieldNamewithbgcolor" />
							                                                                            <AlternatingRowStyle CssClass="fieldName" />
							                                                                        </asp:GridView>
                                                                                                    <!-- End Service Contractor Listing -->
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="center">
                                                                                                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <!-- End Div open search -->
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" align="left" style="padding-left: 300px;">
                                                            <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                                                                runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" width="100%" align="center">
                                                            <!-- Product Division details Start-->
                                                            <table width="98%" border="0">
                                                                <tr>
                                                                    <td align="center">
                                                                        <!-- Action Listing -->
                                                                        <asp:GridView PageSize="10" DataKeyNames="SnoDisp" RowStyle-CssClass="gridbgcolor"
                                                                            AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                                                                            GridGroups="both" AllowPaging="True" AutoGenerateColumns="False" ID="gvComm"
                                                                            runat="server" OnPageIndexChanging="gvComm_PageIndexChanging" Width="98%" Visible="true"
                                                                            OnRowDeleting="gvComm_RowDeleting" OnRowCommand="gvComm_RowCommand">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="SnoDisp" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left"
                                                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="SNo"></asp:BoundField>
                                                                                <asp:BoundField DataField="ProductDivision" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Left"
                                                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Product Division"></asp:BoundField>
                                                                                <asp:BoundField DataField="SC" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left"
                                                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Service Contractor">
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="ProductLine" HeaderStyle-Width="150px" ItemStyle-Wrap="true"
                                                                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Product Line">
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="QuantityDisp" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left"
                                                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Quantity">
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="InvoiceNumDisp" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left"
                                                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Invoice Number">
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="PurchasedFromDisp" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left"
                                                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Purchased From">
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                </asp:BoundField>
                                                                                <asp:TemplateField HeaderText="Edit/Delete">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnkBtnEdit" CommandArgument='<%#Eval("SnoDisp")%>' CommandName="Change"
                                                                                            CausesValidation="false" runat="server">Edit</asp:LinkButton>
                                                                                        /<asp:LinkButton ID="lnkBtnDelete" CausesValidation="false" CommandName="Delete"
                                                                                            runat="server">Delete</asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                        <!-- End Action Listing -->
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <!-- Product Division End -->
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" align="center">
                                                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn" OnClick="btnSubmit_Click"
                                                                Text="Save" Width="70px" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="btnAddMore" runat="server" CssClass="btn" OnClick="btnAddMore_Click"
                                                                Text="Add More" Width="70px" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="btnCancel" runat="server" CausesValidation="false" CssClass="btn"
                                                                OnClick="btnCancel_Click" OnClientClick="return funConfirm();" Text="Cancel" />
                                                            &nbsp; &nbsp;&nbsp;<asp:Button ID="btnClosed" runat="server" Text="Final Closed" CssClass="btn"
                                                                CausesValidation="false" OnClick="btnClosed_Click" Visible="false" />
                                                            <asp:HiddenField ID="hfIsSubmit"  runat="server" />
                                                            <asp:HiddenField ID="hdnKeyForUpdate" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" align="center">
                                                            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="99%" id="tableResult" runat="server">
                                        <tr>
                                            <td>
                                                <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        Your request has been processed. Please find below details:&nbsp;<b><asp:GridView 
                                                                            ID="gvCustomerList" runat="server" AlternatingRowStyle-CssClass="fieldName" 
                                                                            AutoGenerateColumns="False" BorderStyle="None" DataKeyNames="CustomerId" 
                                                                            GridLines="none" HeaderStyle-CssClass="fieldNamewithbgcolor" 
                                                                            OnPageIndexChanging="gvCustomerList_PageIndexChanging" 
                                                                            OnSelectedIndexChanging="gvCustomerList_SelectedIndexChanging" PageSize="15" 
                                                                            RowStyle-CssClass="gridbgcolor" Width="100%">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderStyle-Height="25px" HeaderStyle-HorizontalAlign="Left" 
                                                                                    HeaderText="Name">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:CommandField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" 
                                                                                    HeaderText="Action" SelectText="Select" ShowSelectButton="True" />
                                                                            </Columns>
                                                                            <AlternatingRowStyle BorderStyle="None" />
                                                                        </asp:GridView>
                                                                        <asp:Label ID="lblReference"
                                                                            runat="server" Text=""></asp:Label></b>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:GridView PageSize="15" DataKeyNames="Sno" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                                                            HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="True"
                                                                            AutoGenerateColumns="False" ID="gvFinal" PagerSettings-HorizontalAlign="left"
                                                                            runat="server" OnPageIndexChanging="gvFinal_PageIndexChanging" Width="98%" Visible="true">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="Sno" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left"
                                                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="SNo"></asp:BoundField>
                                                                                <asp:TemplateField HeaderStyle-Width="160px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                                    HeaderText="Product Division">
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("ProductDivision")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderStyle-Width="280px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                                    HeaderText="Complaint Ref No">
                                                                                    <ItemTemplate>
                                                                                        <a href="Javascript:void(0);" onclick="funComplaintPopUp('<%#Eval("ComplaintRefNo")%>')">
                                                                                            <%#Eval("ComplaintRefNo")%></a>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                 <asp:TemplateField HeaderStyle-Width="280px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                                    HeaderText="Customer ID">
                                                                                    <ItemTemplate>
                                                                                             <%#Eval("CustomeriD")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                                    HeaderText="Service Contractor">
                                                                                    <ItemTemplate>
                                                                                        <a href="Javascript:void(0);" onclick="funSCPopUp(<%#Eval("Sc_Sno")%>)">
                                                                                            <%#Eval("SCName")%></a>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:Button ID="btnFresh" runat="server" CssClass="btn" Text="New Registration" OnClick="btnFresh_Click"
                                                                            CausesValidation="false" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
