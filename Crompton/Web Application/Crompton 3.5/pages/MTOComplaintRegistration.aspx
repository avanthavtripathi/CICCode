<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" EnableEventValidation="false" 
    CodeFile="MTOComplaintRegistration.aspx.cs" MaintainScrollPositionOnPostback="true"
    Inherits="pages_MTOComplaintRegistration" %>

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
                        MTO Service Registration (International)
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;" >
                        <asp:UpdateProgress AssociatedUpdatePanelID="updateSC" ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="tableBGcolor" colspan="2">
                           <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                <tr>
                                    <td colspan="3">
                                        <b>Site Information - Contact Person Info:</b> &nbsp;
                                    </td>
                                    <td align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                                        <font color='red'>*</font>
                                        <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                            <asp:HiddenField ID="hdnsearch" runat="server" />
                                            <asp:HiddenField ID="hdnwebreqno" runat="server" /> <%--bhawesh for RSD--%>
                                    </td>
                                </tr>
                                                        <tr>
                                                            <td colspan="4">&nbsp;</td>
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
                                                            <td>
                                                                Name<font color="red">*</font>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtboxtxt" Width="140px" MaxLength="15"></asp:TextBox>
                                                                &nbsp; <asp:TextBox ID="txtMiddleName" runat="server" CssClass="txtboxtxt" Width="140" MaxLength="15"></asp:TextBox>
                                                                &nbsp;&nbsp;<asp:TextBox ID="txtLastName" runat="server" CssClass="txtboxtxt" MaxLength="15" Width="140px"></asp:TextBox>
                                                                &nbsp;<div>
                                                                <cc1:TextBoxWatermarkExtender ID="exn1" runat="server" TargetControlID="txtFirstName" WatermarkCssClass="GrayCss" WatermarkText="First Name" />
                                                                <cc1:TextBoxWatermarkExtender ID="exn2" runat="server" TargetControlID="txtMiddleName" WatermarkCssClass="GrayCss" WatermarkText="Middle Name" />
                                                                <cc1:TextBoxWatermarkExtender ID="exn3" runat="server" TargetControlID="txtLastName" WatermarkCssClass="GrayCss" WatermarkText="Last Name" />
                                                                    <asp:RequiredFieldValidator ID="rfvFname" runat="server" 
                                                                        ControlToValidate="txtFirstName" Display="Dynamic" 
                                                                        ErrorMessage="First Name is required." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                    <asp:RequiredFieldValidator ID="rfvLname" runat="server" 
                                                                        ControlToValidate="txtLastName" Display="Dynamic" 
                                                                        ErrorMessage="Last Name is required." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                               OEM Customer Name
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:TextBox runat="server" MaxLength="100" Width="350Px" CssClass="txtboxtxt" ID="TxtOEMCustName"></asp:TextBox>
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
                                                                Address 1<font color='red'>*</font>
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
                                        LandMark
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtLandmark" runat="server" CssClass="txtboxtxt" MaxLength="150" Width="350px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Country<font color='red'>*</font>
                                    </td>
                                    <td style="width: 24%">
                                        <asp:DropDownList Width="170px" runat="server" CssClass="simpletxt1" ID="ddlCountry" >
                                        </asp:DropDownList>
                                        <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator2" runat="server"
                                            ControlToValidate="ddlCountry" ErrorMessage="Country is required." Operator="NotEqual"
                                            ValueToCompare="Select" Display="None"></asp:CompareValidator>
                                    </td>
                                    <td>
                                        City<font color="red">*</font>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtCity" runat="server" CssClass="txtboxtxt" Width="170px" />
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
                                        <asp:DropDownList ID="ddlModeOfRec" runat="server" CssClass="simpletxt1" Enabled="false" >
                                        <asp:ListItem Text="International Webform" Value="11" Selected="True" />
                                        </asp:DropDownList>
                                     </td>
                                    <td>
                               
                                    </td>
                                    <td>
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Contact No.<font color='red'>*</font>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtContactNo" MaxLength="15" runat="server" CssClass="txtboxtxt"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" ControlToValidate="txtContactNo"
                                            runat="server" ErrorMessage="Contact Number is required." Display="None"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Valid Contact Number is required."
                                            ControlToValidate="txtContactNo" SetFocusOnError="True" ValidationExpression="\d{10,15}"
                                            Display="None"></asp:RegularExpressionValidator>
                                     </td>
                                </tr>
                                <tr>
                                    <td>
                                        Alternate Contact No.
                                    </td>
                                    <td style="width: 28%">
                                        <asp:TextBox runat="server" MaxLength="15" ID="txtAltConatctNo" CssClass="txtboxtxt"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Valid Alternate Contact Number is required."
                                            ControlToValidate="txtAltConatctNo" SetFocusOnError="True" ValidationExpression="\d{10,15}"
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
                                        E-Mail<font color='red'>*</font>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtEmail" MaxLength="60" runat="server" CssClass="txtboxtxt" Width="213px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="Rfvemail" runat="server" ControlToValidate="txtEmail" SetFocusOnError="true" Display="Dynamic" ></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="reqEmail" runat="server" ErrorMessage="Valid Email is required."
                                            ControlToValidate="txtEmail" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            Display="None"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                           </table>
                                   
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
                                                <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                                    <tr style="height:25">
                                                        <td colspan="2">
                                                            <b>Product & Complaint Information:</b> &nbsp;
                                                        </td>
                                                <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;" colspan="2">
                                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server">
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
                                                            <asp:TextBox ID="txtQuantity" runat="server" CssClass="txtboxtxt" MaxLength="2" Text=""
                                                                Width="50px" onkeypress="javascript:return checkNumberOnly(event);"></asp:TextBox><asp:RequiredFieldValidator
                                                                    ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Quantity is required."
                                                                    Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator><asp:CompareValidator
                                                                        ID="CompareValidator7" runat="server" ErrorMessage="Valid Quantity is required."
                                                                        Operator="DataTypeCheck" ControlToValidate="txtQuantity" Type="Integer" Display="None"
                                                                        SetFocusOnError="true"></asp:CompareValidator>
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
                                                    <tr id="tr1" runat="server" visible="false">
                                                        <td>
                                                            Equipment Name
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtequipname" runat="server" CssClass="txtboxtxt" 
                                                                MaxLength="150"></asp:TextBox>
                                                        </td>
                                                        <td align="right" style="width: 230px">
                                                            Coach No.
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtcoachNo" runat="server" CssClass="txtboxtxt" 
                                                                MaxLength="150"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="tr2" runat="server" visible="false">
                                                        <td>
                                                            Train No.
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtTrainNo" runat="server" CssClass="txtboxtxt" 
                                                                MaxLength="150"></asp:TextBox>
                                                        </td>
                                                        <td align="right" style="width: 230px">
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
                                                            <asp:TextBox ID="txtInvoiceNum" runat="server" CssClass="txtboxtxt" 
                                                                MaxLength="50" ValidationGroup="Report" />
                                                        </td>
                                                        <td align="right" style="width: 230px">
                                                            Invoice Date &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtxPurchaseDate" runat="server" AutoPostBack="true" 
                                                                CssClass="txtboxtxt" MaxLength="10" 
                                                                OnTextChanged="txtxPurchaseDate_TextChanged" ToolTip="Purchase date" />
                                                            &nbsp;(mm/dd/yyyy)
                                                            <asp:CompareValidator ID="CompareValidator8" runat="server" 
                                                                ControlToValidate="txtxPurchaseDate" Display="None" 
                                                                ErrorMessage="Valid Purchase date is required. " Operator="DataTypeCheck" 
                                                                Type="Date"></asp:CompareValidator>
                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                                                                TargetControlID="txtxPurchaseDate">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                          Product SR No.<font color="red">*</font> 
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPrdSRNo" runat="server" CssClass="txtboxtxt" MaxLength="10" 
                                                                SetFocusOnError="true" />
                                                            <asp:RequiredFieldValidator ID="reqPrdSRNo" runat="server" 
                                                                ControlToValidate="txtPrdSRNo" Display="Dynamic" ErrorMessage="" 
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                       </td>
                                                        <td align="right" style="width: 230px">
                                                          
                                                        </td>
                                                        <td>
                                                        
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Manufacture Date<font color="red">*</font> <%--PO Date--%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPoDate" runat="server" CssClass="txtboxtxt" 
                                                                MaxLength="10" />
                                                            &nbsp;(mm/dd/yyyy)
                                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" 
                                                                TargetControlID="txtPoDate">
                                                            </cc1:CalendarExtender>
                                                            <asp:CompareValidator ID="CompareValidator16" runat="server" 
                                                                ControlToValidate="txtPoDate" Display="None" 
                                                                ErrorMessage="Valid Manufacture date is required. " Operator="DataTypeCheck" 
                                                                Type="Date"></asp:CompareValidator>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                                                ControlToValidate="txtPoDate" Display="None" 
                                                                ErrorMessage="Reporting date is required"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td align="right" style="width: 230px">
                                                            Date Of Dispatch
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDateofDispatch" runat="server" CssClass="txtboxtxt" 
                                                                MaxLength="10" SetFocusOnError="true" />
                                                            &nbsp;(mm/dd/yyyy)<asp:CompareValidator ID="CompareValidator9" runat="server" 
                                                                ControlToValidate="txtDateofDispatch" Display="None" 
                                                                ErrorMessage="Valid date is required. " Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                                          <cc1:CalendarExtender ID="CalendarExtender2" runat="server" 
                                                                TargetControlID="txtDateofDispatch">
                                                            </cc1:CalendarExtender>
                                                            <asp:CustomValidator ID="CustomValidator2" runat="server" 
                                                                ClientValidationFunction="validateDate" ControlToValidate="txtDateofDispatch" 
                                                                Display="none" ErrorMessage="Date should not be after todays date." 
                                                                SetFocusOnError="true"></asp:CustomValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Date of Commission
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCommissionDate" runat="server" CssClass="txtboxtxt" 
                                                                MaxLength="10" />
                                                            &nbsp;(mm/dd/yyyy)
                                                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" 
                                                                TargetControlID="txtCommissionDate">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                        <td align="right">
                                                            Warranty Expiry Date
                                                        </td>
                                                        <td>
                                                       <asp:TextBox ID="txtWarrantyDate" runat="server" CssClass="txtboxtxt" 
                                                                MaxLength="10"  />
                                                            &nbsp;(mm/dd/yyyy)
                                                            <cc1:CalendarExtender ID="CalendarExtender5" runat="server" 
                                                                TargetControlID="txtWarrantyDate">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Date of Reporting <font color="red">*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtDateofReporting" runat="server" CssClass="txtboxtxt" 
                                                                MaxLength="10" />
                                                            &nbsp;(mm/dd/yyyy)(Problem Identified and reported to CG)
                                                            <cc1:CalendarExtender ID="CalendarExtender6" runat="server" 
                                                                TargetControlID="txtDateofReporting">
                                                            </cc1:CalendarExtender>
                                                            <asp:CompareValidator ID="CompareValidator15" runat="server" 
                                                                ControlToValidate="txtDateofReporting" Display="None" 
                                                                ErrorMessage="Valid Reporting date is required. " Operator="DataTypeCheck" 
                                                                Type="Date"></asp:CompareValidator>
                                                            <asp:RequiredFieldValidator ID="reqtxtDateofReporting" runat="server" 
                                                                ControlToValidate="txtDateofReporting" Display="None" 
                                                                ErrorMessage="Reporting date is required"></asp:RequiredFieldValidator>
                                                            <asp:CustomValidator ID="CustomValidator1" runat="server" 
                                                                ClientValidationFunction="validateDateofReporting" 
                                                                ControlToValidate="txtDateofReporting" Display="None" 
                                                                ErrorMessage="Date of Reporting less then current date">
                                                            </asp:CustomValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Allocate To Myself
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkSelfAllocatopn" runat="server" Checked="true" 
                                                                Enabled="false" />
                                                        </td>
                                                    </tr>
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
                                                        <td align="center" colspan="4">
                                                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn" 
                                                                OnClick="btnSubmit_Click" Text="Save" Width="70px" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="btnCancel" runat="server" CssClass="btn" 
                                                                OnClick="btnCancel_Click" OnClientClick="return funConfirm();" Text="Cancel" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="4">
                                                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table width="99%" id="tableResult" runat="server">
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
                                                                                                HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" 
                                                                                                AutoGenerateColumns="False" ID="gvFinal" runat="server" Width="60%" 
                                                                                                Visible="true">
                                                                                                <RowStyle CssClass="gridbgcolor" />
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="Sno" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left"
                                                                                                        HeaderStyle-HorizontalAlign="Left" HeaderText="SNo">
                                                                                                        <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:TemplateField HeaderStyle-Width="160px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                                                        HeaderText="Product Division">
                                                                                                        <ItemTemplate>
                                                                                                            <%#Eval("ProductDivision")%>
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle HorizontalAlign="Left" Width="160px" />
                                                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderStyle-Width="280px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                                                        HeaderText="Complaint Ref No">
                                                                                                        <ItemTemplate>
                                                                                                            <%#Eval("ComplaintRefNo")%>
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle HorizontalAlign="Left" Width="280px" />
                                                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                                <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                                                                <AlternatingRowStyle CssClass="fieldName" />
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
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
