<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="MTOServiceRegistration.aspx.cs" MaintainScrollPositionOnPostback="true"
    Inherits="pages_MTOServiceRegistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script language="javascript" type="text/javascript">
   
    function funConfirm()
    {
        if(confirm('Are you sure to cancel this request?'))
        {
          return true;
        }
        return false;
    }
    
    function validateDate(oSrc, args)
        {
            
            var varActiondate = args.Value;
            var varServerDate = (document.getElementById('ctl00_MainConHolder_hdnGlobalDate').value);
            
            var arrayAction =varActiondate.split('/');
            var arrayServer =varServerDate.split('/');
            var actionDate= new Date(); 
            var serverDate= new Date();
             
            actionDate.setFullYear(arrayAction[2],(arrayAction[0]-1),arrayAction[1]);
            actionDate.setHours(0,0,59,0);
            
            //alert(actionDate);
            
            serverDate.setFullYear(arrayServer[2],(arrayServer[0]-1),arrayServer[1]);
            serverDate.setHours(0,0,59,0);
            
            serverDate.setDate(serverDate.getDate() + 1 );
            
            //alert(serverDate);
            
            if (actionDate !="NaN")
            {
                if(actionDate<serverDate)
                {
                    args.IsValid = true
                }
                else
                {
                    args.IsValid = false
                }
            }
             
        }
        
        function validateDateofReporting(oSrc, args) {

            var varActiondate = (document.getElementById('ctl00_MainConHolder_txtDateofReporting').value);
            var varServerDate = (document.getElementById('ctl00_MainConHolder_hdnGlobalDate').value);
            var arrayAction = varActiondate.split('/');
            var arrayServer = varServerDate.split('/');
            var actionDate = new Date();
            var serverDate = new Date();
            actionDate.setFullYear(arrayAction[2], (arrayAction[0] - 1), arrayAction[1]);
            actionDate.setHours(0, 0, 59, 0);
            serverDate.setFullYear(arrayServer[2], (arrayServer[0] - 1), arrayServer[1]);
            serverDate.setHours(0, 0, 59, 0);
                         
            if (actionDate <= serverDate)
             {
                args.IsValid = true
                
            }
            else 
            {
                args.IsValid = false
            }

        }
        
    </script>

    <asp:UpdatePanel ID="updateSC" runat="server">
        <ContentTemplate>
            <table width="100%" border="0">
                <tr>
                    <td class="headingred">
                        New Service Registration
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;"
                        style="width: 30%">
                        <asp:UpdateProgress AssociatedUpdatePanelID="updateSC" ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td class="tableBGcolor" colspan="2">
                        <table border="0" cellpadding="2" cellspacing="0" width="100%">
                            <tr>
                                <%--<td align="right" style="padding-right:100px">
                                    <asp:LinkButton ID="lnkbtnCustomerSearch" runat="server" OnClick="lnkbtnCustomerSearch_Click"
                                        CausesValidation="false">Show MTO Customer Search</asp:LinkButton>
                                    
                                </td>--%>
                                <td>
                                    <b>Search MTO SAP Customer :</b> &nbsp;
                                    <asp:HiddenField ID="hdnGlobalDate" runat="server" />
                                    <asp:HiddenField ID="hdnwebreqno" runat="server" /> <%--bhawesh for RSD--%>
                                </td>
                                <td align="right">
                                    <a href="MTOServiceRegistration.aspx">Clear Search</a>
                                </td>
                            </tr>
                            <tr id="trCustomerSearch" runat="server" visible="true">
                                <td colspan="2">
                                    <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                        <tr>
                                            <td align="right" width="15%">
                                                Product Sr. No.:
                                            </td>
                                            <td width="15%">
                                                <asp:TextBox ID="txtProductSRNo" runat="server" CssClass="txtboxtxt"></asp:TextBox>
                                                <asp:HiddenField ID="hdnCustomerId" runat="server" Value="0" />
                                            </td>
                                            <td id="tdInvoiceNo" runat="server" visible="false">
                                                <asp:DropDownList ID="ddlInvoiceNo" runat="server" CssClass="simpletxt1" OnSelectedIndexChanged="ddlInvoiceNo_SelectedIndexChanged"
                                                    AutoPostBack="true" Width="158px">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" width="15%">
                                                Invoice No.:
                                            </td>
                                            <td width="15%">
                                                <asp:TextBox ID="txtInvoiceNo" runat="server" CssClass="txtboxtxt"></asp:TextBox>
                                            </td>
                                            <td id="tdProdctSRNO" runat="server" visible="false">
                                                <asp:DropDownList ID="ddlproductSRNO" runat="server" CssClass="simpletxt1" OnSelectedIndexChanged="ddlproductSRNO_SelectedIndexChanged"
                                                    AutoPostBack="true" Width="158px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Button ID="BtnProcess" runat="server" CausesValidation="false" CssClass="btn"
                                                    Enabled="true" OnClick="BtnProcess_Click" Text="Search" />
                                                <%-- <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                                                        runat="server" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="4">
                                                <asp:Label ID="lblErrormsg" runat="server" ForeColor="Red" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tableBGcolor" colspan="2">
                        <div id="divCustomerInfo" runat="server" visible="false">
                            <table border="0" cellpadding="1" cellspacing="0" width="100%">
                                <tr>
                                    <td colspan="4">
                                        <b>Customer Information:</b> &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td width="180px">
                                        First Name
                                    </td>
                                    <td width="350px">
                                        <asp:TextBox ID="txtPMFirstName" runat="server" CssClass="txtboxtxt" MaxLength="30"
                                            ReadOnly="true" Width="158px"></asp:TextBox>
                                    </td>
                                    <td align="right" width="120px">
                                        Last Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPMLastName" runat="server" CssClass="txtboxtxt" MaxLength="20"
                                            ReadOnly="true" Width="158px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Company Name
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtPMCompanyName" runat="server" CssClass="txtboxtxt" MaxLength="150"
                                            ReadOnly="true" Width="350Px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Address1
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtPMAdd1" runat="server" CssClass="txtboxtxt" MaxLength="50" ReadOnly="true"
                                            Width="350Px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Address2
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtPMAdd2" runat="server" CssClass="txtboxtxt" MaxLength="49" ReadOnly="true"
                                            Width="350px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="tdAddress3" runat="server">
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtPMAdd3" runat="server" CssClass="txtboxtxt" MaxLength="150" ReadOnly="true"
                                            Width="350px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Country
                                    </td>
                                    <td style="width: 24%">
                                        <asp:DropDownList ID="ddlPMCountry" runat="server" AutoPostBack="True" CssClass="simpletxt1"
                                            Enabled="false" OnSelectedIndexChanged="ddlPMCountry_SelectedIndexChanged" Width="170px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        State
                                    </td>
                                    <td style="width: 24%">
                                        <asp:DropDownList ID="ddlPMState" runat="server" AutoPostBack="True" CssClass="simpletxt1"
                                            Enabled="false" OnSelectedIndexChanged="ddlPMState_SelectedIndexChanged" Width="170px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" style="width: 8%" valign="top">
                                        City
                                    </td>
                                    <td valign="top">
                                        <asp:DropDownList ID="ddlPMCity" runat="server" CssClass="simpletxt1" Enabled="false"
                                            Width="170px">
                                            <asp:ListItem Selected="True" Text="Select" Value="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Pin Code
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtPMPinCode" runat="server" AutoPostBack="true" CssClass="txtboxtxt"
                                            EnableViewState="true" MaxLength="6" ReadOnly="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Phone
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtPMPhone" runat="server" CssClass="txtboxtxt" MaxLength="11" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        E-Mail
                                    </td>
                                    <td style="width: 24%">
                                        <asp:TextBox ID="txtPMEmail" runat="server" CssClass="txtboxtxt" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 8%" valign="top">
                                        Fax No.
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="txtPMFaxNo" runat="server" CssClass="txtboxtxt" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="tableBGcolor" colspan="2">
                        <div id="divSiteInfo" runat="server" visible="true">
                            <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                <tr>
                                    <td colspan="3">
                                        <b>Site Information - Contact Person Info:</b> &nbsp;
                                    </td>
                                    <td align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                        &nbsp;<asp:HiddenField ID="HiddenField2" runat="server" />
                                        <font color='red'>
                                            <asp:HiddenField ID="hdnsearch" runat="server" />
                                            *</font>
                                        <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
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
                                    </td>
                                    <td align="right" width="120px">
                                        Last Name<font color="red">*</font>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="txtboxtxt" Width="158px" MaxLength="20"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtLastName"
                                            ErrorMessage="Last Name is required." SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Customer Type
                                    </td>
                                    <td style="width: 24%">
                                        <asp:DropDownList ID="ddlCustomerType" runat="server" CssClass="simpletxt1">
                                            <%--<asp:ListItem Selected="True" Value="D">Domestic</asp:ListItem>--%>
                                            <asp:ListItem Selected="True" Value="I">Industrial</asp:ListItem>
                                            <asp:ListItem Value="E">EB</asp:ListItem>
                                            <asp:ListItem Value="P">Project</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 8%">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
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
                                        <asp:TextBox runat="server" MaxLength="50" Width="350Px" CssClass="txtboxtxt" ID="txtAdd1"></asp:TextBox>
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
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Address3
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtAdd3" runat="server" CssClass="txtboxtxt" MaxLength="150" Width="350px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Country<font color='red'>*</font>
                                    </td>
                                    <td style="width: 24%">
                                        <asp:DropDownList Width="170px" runat="server" CssClass="simpletxt1" ID="ddlCountry"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator2" runat="server"
                                            ControlToValidate="ddlCountry" ErrorMessage="Country is required." Operator="NotEqual"
                                            ValueToCompare="Select" Display="None"></asp:CompareValidator>
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
                                        <asp:DropDownList Width="170px" ID="ddlCity" runat="server" CssClass="simpletxt1">
                                            <asp:ListItem Text="Select" Value="Select" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator4" runat="server"
                                            ControlToValidate="ddlCity" ErrorMessage="City is required." Operator="NotEqual"
                                            ValueToCompare="Select" Display="None"></asp:CompareValidator>
                                        <%--<input type="button" id="Button1" value="???" 
                                        class="btn" onclick="return funOpenDivCS();" />
                                        <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator2" runat="server"
                                            ControlToValidate="ddlCity" ErrorMessage="City is required." Operator="NotEqual"
                                            ValueToCompare="Select" Display="None"></asp:CompareValidator>
                                        <asp:TextBox ID="txtOtherCity" Width="100px" MaxLength="30" runat="server" CssClass="txtboxtxt"></asp:TextBox><asp:RequiredFieldValidator
                                            ID="reqCityOther" runat="server" SetFocusOnError="true" Display="None" ErrorMessage="Other City is required."
                                            ControlToValidate="txtOtherCity"></asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Pin Code
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtPinCode" EnableViewState="true" runat="server" AutoPostBack="false"
                                            CssClass="txtboxtxt" MaxLength="6" />
                                        <%--OnTextChanged="txtPinCode_TextChanged"></asp:TextBox> onkeypress="javascript:return checkNumberOnly(event);"
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Valid pin is required."
                                            ControlToValidate="txtPinCode" SetFocusOnError="True" ValidationExpression="\d{6}"
                                            Display="None"></asp:RegularExpressionValidator>--%>
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
                                      <asp:DropDownList ID="ddlModeOfRec" runat="server" CssClass="simpletxt1"> <%-- SPRF No : 130601 --%>
                                        <asp:ListItem Selected="True" Text="Call" Value="1" />
                                        <asp:ListItem Text="Email" Value="2" />
                                        <asp:ListItem Text="Fax" Value="5" />
                                      </asp:DropDownList>
                                    </td>
                                    <td style="width: 8%" align="right">
                                        Language <font color='red'>*</font>
                                    </td>
                                    <td>
                                        <asp:DropDownList Width="170px" ID="ddlLanguage" runat="server" CssClass="simpletxt1">
                                        </asp:DropDownList>
                                        <asp:CheckBox ID="chkLanguage" Text="Set as Default" runat="server" AutoPostBack="True"
                                            OnCheckedChanged="chkLanguage_CheckedChanged" />
                                        <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator3" runat="server"
                                            ControlToValidate="ddlLanguage" ErrorMessage="Language is required." Operator="NotEqual"
                                            ValueToCompare="0">*</asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Contact No.<font color='red'>*</font>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtContactNo" MaxLength="11" runat="server" CssClass="txtboxtxt"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" ControlToValidate="txtContactNo"
                                            runat="server" ErrorMessage="Contact Number is required." Display="None"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Valid Contact Number is required."
                                            ControlToValidate="txtContactNo" SetFocusOnError="True" ValidationExpression="\d{10,11}"
                                            Display="None"></asp:RegularExpressionValidator>
                                        For eg: For land line - 02267558912 & for cell no. 09821474747
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Alternate Contact No.
                                    </td>
                                    <td style="width: 28%">
                                        <asp:TextBox runat="server" MaxLength="11" ID="txtAltConatctNo" CssClass="txtboxtxt"></asp:TextBox>
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
                                    <td colspan="3">
                                        <asp:TextBox ID="txtEmail" MaxLength="60" runat="server" CssClass="txtboxtxt" Width="213px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="reqEmail" runat="server" ErrorMessage="Valid Email is required."
                                            ControlToValidate="txtEmail" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            Display="None"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Fax No.
                                    </td>
                                    <td style="width: 28%">
                                        <asp:TextBox ID="txtFaxNo" runat="server" CssClass="txtboxtxt" MaxLength="11"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtFaxNo"
                                            ErrorMessage="Valid Fax is required." SetFocusOnError="True" ValidationExpression="\d{10,11}"
                                            Display="None"></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 8%">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <%--<tr>
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
                                </tr>--%>
                                <tr>
                                    <td colspan="4" align="center">
                                        <%--<asp:Button ID="btnNext" runat="server" Text="Next" CssClass="btn" OnClick="btnNext_Click" />--%>
                                        <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                                            runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divComplaint" runat="server" visible="true">
                            <table width="100%" border="0">
                                <tr>
                                    <td colspan="4" class="bgcolorcomm">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnFileUpload" />
                                            </Triggers>
                                            <ContentTemplate>
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
                                                            <asp:UpdateProgress AssociatedUpdatePanelID="UpdatePanel1" ID="UpdateProgress3" runat="server">
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
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="bgcolorcomm" colspan="4">
                                        <asp:UpdatePanel ID="pnlComplaint" runat="server">
                                            <ContentTemplate>
                                                <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                                    <tr height="25">
                                                        <td colspan="2">
                                                            <b>Product & Complaint Information:</b> &nbsp;
                                                        </td>
                                                <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;" colspan="2">
                                                            <asp:UpdateProgress AssociatedUpdatePanelID="pnlComplaint" ID="UpdateProgress2" runat="server">
                                                                <ProgressTemplate>
                                                                    <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                                                                </ProgressTemplate>
                                                            </asp:UpdateProgress>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 15%">
                                                            Product Division<font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:DropDownList Width="475px" ID="ddlProductDiv" AutoPostBack="true" runat="server"
                                                                CssClass="simpletxt1" OnSelectedIndexChanged="ddlProductDiv_SelectedIndexChanged">
                                                                <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:CompareValidator SetFocusOnError="true" ID="compValProdDiv" runat="server" ControlToValidate="ddlProductDiv"
                                                                ErrorMessage="Product Division is required." Operator="NotEqual" ValueToCompare="0"
                                                                Display="None"></asp:CompareValidator><br />
                                                            <asp:Label ID="lblCategoryProduct" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Product Line<font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:DropDownList Width="475px" ID="ddlProductLine" runat="server" CssClass="simpletxt1"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlProductLine_SelectedIndexChanged">
                                                                <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="regProductLine" runat="server" InitialValue="0" ControlToValidate="ddlProductLine"
                                                                ErrorMessage="Select Product line" Display="None" EnableClientScript="true"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Product <font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:DropDownList Width="475px" ID="ddlProduct" AutoPostBack="false" runat="server"
                                                                CssClass="simpletxt1">
                                                                <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator10" runat="server"
                                                                ControlToValidate="ddlProduct" ErrorMessage="Product is required." Operator="NotEqual"
                                                                ValueToCompare="0" Display="None"></asp:CompareValidator>
                                                            <asp:TextBox ID="txtFindPL" CssClass="txtboxtxt" runat="server" Width="70" CausesValidation="false"></asp:TextBox>
                                                            <asp:Button ID="btnGoPL" runat="server" Width="88px" Text="Product Search" CssClass="btn"
                                                                CausesValidation="false" OnClick="btnGoPL_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Quantity<font color='red'>*</font>
                                                        </td>
                                                        <td style="width: 35%">
                                                            <asp:TextBox ID="txtQuantity" runat="server" CssClass="txtboxtxt" MaxLength="7" Text="" Width="100px" onkeypress="javascript:return checkNumberOnly(event);" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Quantity is required." Display="None" SetFocusOnError="true" />
                                                            <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="Valid Quantity is required." Operator="DataTypeCheck" ControlToValidate="txtQuantity" Type="Integer" Display="None" SetFocusOnError="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Complaint Region <font color='red'>*</font>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlRegion" runat="server" CssClass="simpletxt1" Width="250px"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" />
                                                            <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator14" runat="server"
                                                                ControlToValidate="ddlRegion" ErrorMessage="Region is required." Operator="NotEqual"
                                                                ValueToCompare="0" Display="None"></asp:CompareValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Complaint Branch<font color='red'>*</font>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="simpletxt1" Width="250px">
                                                                <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator6" runat="server"
                                                                ControlToValidate="ddlBranch" ErrorMessage="Branch is required." Operator="NotEqual"
                                                                ValueToCompare="0" Display="None"></asp:CompareValidator>
                                                        </td>
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
                                                        <td colspan="4">
                                                            <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                                              <tr id="tr1" runat="server" visible="false">
                                                              <td>
                                                              Equipment Name
                                                              </td>
                                                              <td>
                                                                 <asp:TextBox ID="txtequipname" runat="server" CssClass="txtboxtxt" 
                                                                      MaxLength="150"></asp:TextBox>
                                                              </td>
                                                              <td style="width: 230px" align="right">
                                                               Coach No.
                                                              </td>
                                                              <td>
                                                                <asp:TextBox ID="txtcoachNo" runat="server" CssClass="txtboxtxt" MaxLength="150"></asp:TextBox>
                                                              </td>
                                                              </tr>
                                                             <tr id="tr2" runat="server" visible="false">
                                                              <td>
                                                              Train No.
                                                              </td>
                                                              <td>
                                                                <asp:TextBox ID="txtTrainNo" runat="server" CssClass="txtboxtxt" MaxLength="150"></asp:TextBox>
                                                              </td>
                                                              <td style="width: 230px" align="right">
                                                              Availability of Coach/BLDC Fan at depot.
                                                              </td>
                                                              <td>
                                                                <asp:TextBox ID="txtAvailblty" runat="server" CssClass="txtboxtxt" 
                                                                      MaxLength="150"></asp:TextBox>
                                                              </td>
                                                              </tr>
                                                              <tr>
                                                                    <td style="width: 17%">
                                                                        Invoice No.
                                                                    </td>
                                                                    <td style="width: 20%">
                                                                        <asp:TextBox runat="server" ID="txtInvoiceNum" CssClass="txtboxtxt" MaxLength="50"
                                                                            ValidationGroup="Report" />
                                                                    </td>
                                                                    <td align="right" style="width: 230px">
                                                                        Invoice Date &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox MaxLength="10" runat="server" ID="txtxPurchaseDate" OnTextChanged="txtxPurchaseDate_TextChanged"
                                                                            AutoPostBack="true" CssClass="txtboxtxt" ToolTip="Purchase date" />&nbsp;(mm/dd/yyyy)
                                                                        <asp:CompareValidator ID="CompareValidator8" runat="server" Type="Date" Operator="DataTypeCheck"
                                                                            ControlToValidate="txtxPurchaseDate" Display="None" ErrorMessage="Valid Purchase date is required. "></asp:CompareValidator>
                                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtxPurchaseDate">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <%--Purchased From--%>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtxPurchaseFrom" runat="server" CssClass="txtboxtxt" ValidationGroup="Report"
                                                                            Visible="false" />
                                                                    </td>
                                                                    <td align="right" style="width: 230px">
                                                                        Product SR No.<font color="red">*</font> &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox MaxLength="10" runat="server" ID="txtPrdSRNo" CssClass="txtboxtxt" SetFocusOnError="true" />
                                                                        <%-- Visit Charges:&nbsp;--%>
                                                                        <asp:Label ID="lblVisitCharge" runat="server" Visible="false" Text="0"></asp:Label>&nbsp;<%--Rs.--%>
                                                                        <asp:RequiredFieldValidator ID="reqPrdSRNo" runat="server" 
                                                                            ControlToValidate="txtPrdSRNo" Display="Dynamic" 
                                                                            ErrorMessage="Product SR No is required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Manufacture Date<font color='red'>*</font>
                                                                        <%--PO Date--%>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox MaxLength="10" runat="server" ID="txtMfgDate" CssClass="txtboxtxt" />&nbsp;(mm/dd/yyyy)
                                                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtMfgDate" />
                                                                        <asp:CompareValidator ID="CompareValidator16" runat="server" Type="Date" Operator="DataTypeCheck" ControlToValidate="txtMfgDate" Display="None" ErrorMessage="Valid Manufacture date is required." />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtMfgDate" ErrorMessage="Manufacture date is required" Display="None" />
                                                                            
                                                                    </td>
                                                                    <td align="right" style="width: 230px">
                                                                        Date Of Dispatch
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox MaxLength="10" runat="server" ID="txtDateofDispatch" SetFocusOnError="true"
                                                                            CssClass="txtboxtxt" />&nbsp;(mm/dd/yyyy)<asp:CompareValidator ID="CompareValidator9"
                                                                                runat="server" Type="Date" Operator="DataTypeCheck" ControlToValidate="txtDateofDispatch"
                                                                                Display="None" ErrorMessage="Valid date is required. "></asp:CompareValidator>
                                                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6"  runat="server" Display="None" ErrorMessage="Dispatch Date is required" 
                                                                      SetFocusOnError="true" ControlToValidate="txtDateofDispatch"></asp:RequiredFieldValidator>--%>
                                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateofDispatch">
                                                                        </cc1:CalendarExtender>
                                                                        <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtDateofDispatch"
                                                                            Display="none" ClientValidationFunction="validateDate" SetFocusOnError="true"
                                                                            ErrorMessage="Date should not be after todays date."></asp:CustomValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Date of Commission
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox MaxLength="10" runat="server" ID="txtCommissionDate" CssClass="txtboxtxt" />&nbsp;(mm/dd/yyyy)
                                                                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtCommissionDate">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                       
                                                                            <td align="right">
                                                                   Warranty Expiry Date
                         
                                                                    </td>
                                                                    <td>
                                                                        <%-- </td>
                                                        <td style="width: 21%">--%>
                                                                        <asp:TextBox MaxLength="10" runat="server" ID="txtWarrantyDate" CssClass="txtboxtxt"
                                                                            ReadOnly="true" />&nbsp;(mm/dd/yyyy)
                                                                        <cc1:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtWarrantyDate">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Date of Reporting <font color='red'>*</font>
                                                        </td>
                                                        <td colspan ="3">
                                                            <asp:TextBox MaxLength="10" runat="server" ID="txtDateofReporting" 
                                                                CssClass="txtboxtxt" />&nbsp;(mm/dd/yyyy)(Problem Identified and reported to CG)
                                                            <cc1:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtDateofReporting">
                                                            </cc1:CalendarExtender>
                                                                        <asp:CompareValidator ID="CompareValidator15" runat="server" Type="Date" Operator="DataTypeCheck"
                                                                            ControlToValidate="txtDateofReporting" Display="None" ErrorMessage="Valid Reporting date is required. "></asp:CompareValidator>
                                                                        <asp:RequiredFieldValidator ID="reqtxtDateofReporting" runat="server" ControlToValidate="txtDateofReporting"
                                                                            ErrorMessage="Reporting date is required" Display="None"></asp:RequiredFieldValidator>
                                                                            
                                                                        <asp:CustomValidator ID="CustomValidator1" runat="server"
                                                                            ControlToValidate="txtDateofReporting" Display="None" ErrorMessage="Date of Reporting less then current date"
                                                                            ClientValidationFunction="validateDateofReporting">
                                                                        </asp:CustomValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr id="trAllocateTo" runat="server" visible="true">
                                                                    <td>
                                                                        Allocate To
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlAllocateTo" runat="server" CssClass="simpletxt1" OnSelectedIndexChanged="ddlAllocateTo_SelectedIndexChanged"
                                                                            AutoPostBack="true">
                                                                            <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                                                            <asp:ListItem Text="Service Contractor" Value="3"></asp:ListItem>
                                                                            <asp:ListItem Text="CG Employee" Value="2"></asp:ListItem>
                                                                            <asp:ListItem Text="CG Contract Employee" Value="5"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <%--<asp:CompareValidator SetFocusOnError="true" ID="CompareValidator6" runat="server" ControlToValidate="ddlAllocateTo"
                                                                ErrorMessage="Allocate To is required." Operator="NotEqual" ValueToCompare="0"
                                                                Display="None"></asp:CompareValidator>--%>
                                                                    </td>
                                                                    <td style="width: 230px">
                                                                    </td>
                                                                    <td id="tdAllocate" runat="server" visible="false">
                                                                        <asp:DropDownList ID="ddlSC" runat="server" Visible="false" CssClass="simpletxt1"
                                                                            Width="250px">
                                                                        </asp:DropDownList>
                                                                        <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator11" runat="server"
                                                                            ControlToValidate="ddlSC" ErrorMessage="Service Contractor is required." Operator="NotEqual"
                                                                            ValueToCompare="0" Display="None"></asp:CompareValidator>
                                                                        <asp:DropDownList ID="ddlCGExec" Visible="false" runat="server" CssClass="simpletxt1"
                                                                            Width="175px">
                                                                        </asp:DropDownList>
                                                                        <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator12" runat="server"
                                                                            ControlToValidate="ddlCGExec" ErrorMessage="CG Employee is required." Operator="NotEqual"
                                                                            ValueToCompare="Select" Display="None"></asp:CompareValidator>
                                                                        <asp:DropDownList ID="ddlCGContractEmp" runat="server" CssClass="simpletxt1" Width="175px">
                                                                        </asp:DropDownList>
                                                                        <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator13" runat="server"
                                                                            ControlToValidate="ddlCGContractEmp" ErrorMessage="CG Contract Employee is required."
                                                                            Operator="NotEqual" ValueToCompare="Select" Display="None"></asp:CompareValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr id="trAllocateToMySelf" runat="server" visible="true">
                                                                    <td>
                                                                        Allocate To Myself
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkSelfAllocatopn" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelfAllocatopn_CheckedChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <%--<tr>
                                                <td>
                                                    Service Contractor<font color='red'>*</font>
                                                </td>
                                                <td colspan="3" align="left">
                                                    <asp:TextBox ID="txtSc" CssClass="txtboxtxt" Width="250px" Enabled="false" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtSc" ErrorMessage="Service Contractor is required."
                                                        Display="None"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>--%>
                                                        <tr>
                                                            <td>
                                                                <asp:HiddenField ID="hdnScId" runat="server" />
                                                            </td>
                                                            <td align="left" colspan="3">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" colspan="2">
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblMsgSC" runat="server" ForeColor="Red" Text=""></asp:Label>
                                                            </td>
                                                            <td width="5%">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4" align="left" style="padding-left: 300px;">
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
                                                                                    <asp:BoundField DataField="SC" Visible="false" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left"
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
                                                                                    <asp:BoundField DataField="PurchasedFromDisp" Visible="false" HeaderStyle-Width="100px"
                                                                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Purchased From">
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
                                                                <asp:Button ID="btnSubmit" runat="server" CausesValidation="false" CssClass="btn"
                                                                    Text="Save" Width="70px" OnClick="btnSubmit_Click" />
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="btnAddMore" runat="server" CssClass="btn" Text="Add More" Width="70px"
                                                                    OnClick="btnAddMore_Click" />
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Cancel" OnClick="btnCancel_Click"
                                                                    OnClientClick="return funConfirm();" />
                                                                &nbsp;
                                                                <asp:HiddenField ID="hdnKeyForUpdate" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4" align="center">
                                                                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
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
                                                                                    Your request has been processed. Please find below details:&nbsp;<b><asp:Label ID="lblReference"
                                                                                        runat="server" Text=""></asp:Label></b>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:GridView PageSize="15" DataKeyNames="Sno" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                                                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="True"
                                                                                        AutoGenerateColumns="False" ID="gvFinal" PagerSettings-HorizontalAlign="left"
                                                                                        runat="server" Width="98%" Visible="true">
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
                                                                                                    <%--<a href="Javascript:void(0);" onclick="funComplaintPopUp('<%#Eval("ComplaintRefNo")%>')">--%>
                                                                                                    <%#Eval("ComplaintRefNo")%></a>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                                                HeaderText="Allocated to User">
                                                                                                <ItemTemplate>
                                                                                                    <%#Eval("SCName")%></a>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <%--  <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                                        HeaderText="Allocated to User" Visible="false" >
                                                                                        <ItemTemplate>
                                                                                                <%#Eval("CGExecuative")%></a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                                        HeaderText="Allocated to User" Visible="false" >
                                                                                        <ItemTemplate>
                                                                                                <%#Eval("CGContractEmp")%></a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>--%>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <a href="MTOComplainDetails.aspx">MTO Complaint Detail screen</a>
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
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
