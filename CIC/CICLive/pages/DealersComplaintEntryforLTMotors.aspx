<%@ Page Language="C#" MasterPageFile="~/MasterPages/WSCCICPage.master" AutoEventWireup="true"
    CodeFile="DealersComplaintEntryforLTMotors.aspx.cs" Inherits="pages_DealersComplaintEntryforLTMotors"
    Title="Dealers Complaint Entry for LTMotors" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script language="javascript" type="text/javascript">
   
    function funOpenDivSC()
    {
               var strProductDivision=document.getElementById('ctl00_MainConHolder_ddlProductDiv');
               var strState=document.getElementById('ctl00_MainConHolder_ddlState');
               
               var strCity=document.getElementById('ctl00_MainConHolder_ddlCity');
               var strTerr=document.getElementById('ctl00_MainConHolder_ddlSC');
               if(strState.value=="Select")
                {
                    alert("Please select State");
                    strState.focus();
                    return false;
                }
                 if(strCity.value=="Select")
                {
                    alert("Please select City");
                    strCity.focus();
                    return false;
                }
                if(strProductDivision.value=="0")
                {
                    alert("Please select Product Division");
                    strProductDivision.focus();
                    return false;
                }
                document.getElementById("dvSearch").style.display='block';
                
    }
    function funConfirm()
    {
        if(confirm('Are you sure to cancel this request?'))
        {
          return true;
        }
        return false;
    }
     function funOpenDivCS()
     {
      var strUrl='../pages/TerritoryCitySearch.aspx';
      newWin=window.open(strUrl,'MyWin','height=600,width=700,scrollbars=1');
      if(window.focus){newWin.focus();}
     }       
           
    </script>

    <table width="100%">
        <tr>
            <td class="headingred" align="center">
                Dealers Complaint Entry Screen
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
                                                <%-- <asp:LinkButton ID="lnkbtnCustomerSearch" runat="server" OnClick="lnkbtnCustomerSearch_Click"
                                                    CausesValidation="false">Show Customer Search</asp:LinkButton>--%>
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
                                                <asp:Panel ID="pnlCustomerSearch" runat="server">
                                                    <!-- Customer Search Start -->
                                                    <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                Dealer Code <font color="red">*</font> 
                                                            </td>
                                                            <td style="width: 21%">
                                                                <asp:TextBox ID="txtDealerCode" Width="180px" runat="server" CssClass="txtboxtxt"
                                                                    ValidationGroup="Report" AutoPostBack="True" OnTextChanged="txtDealerCode_TextChanged" />
                                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDealerCode"
                                                                        ErrorMessage="Dealer Code is required." SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                                <asp:Label ID="lblVisitCharge" runat="server" Visible="false" Text="0"></asp:Label>
                                                            </td>
                                                            <td align="right">
                                                                Dealer Name <font color="red">*</font>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDealerName" Width="180px" runat="server" CssClass="txtboxtxt"
                                                                    ValidationGroup="Report" />
                                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDealerName"
                                                                        ErrorMessage="Dealer Name is required." SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <tr>
                                                                <td>
                                                                    Email <font color="red">*</font>
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:TextBox ID="txtDealerEmail" Width="180px" runat="server" CssClass="txtboxtxt"
                                                                        ValidationGroup="Report" />
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Valid Email is required."
                                                                        ControlToValidate="txtDealerEmail" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                        Display="None"></asp:RegularExpressionValidator>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDealerEmail"
                                                                        ErrorMessage="Dealer email address is required." SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
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
                                                            &nbsp;<asp:HiddenField ID="hdnScNo" runat="server" />
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
                                                        <td width="170px">
                                                            Contact Person First Name<font color="red">*</font>
                                                        </td>
                                                        <td width="320px">
                                                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtboxtxt" Width="158px"
                                                                MaxLength="30"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFirstName"
                                                                ErrorMessage="First Name is required." SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td align="right" width="100px">
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
                                                            Company Name
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox runat="server" MaxLength="150" Width="350Px" CssClass="txtboxtxt" ID="txtCompanyName"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Site location Address1<font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox runat="server" MaxLength="50" Width="350Px" CssClass="txtboxtxt" ID="txtAdd1"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" SetFocusOnError="true"
                                                                ControlToValidate="txtAdd1" ErrorMessage="Address1 is required." Display="None"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Site location Address2
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtAdd2" runat="server" CssClass="txtboxtxt" MaxLength="49" Width="350px"></asp:TextBox>
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
                                                            <input type="button" id="Button1" value="???" class="btn" onclick="return funOpenDivCS();" />
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
                                                                runat="server" AutoPostBack="true" CssClass="txtboxtxt" MaxLength="6" OnTextChanged="txtPinCode_TextChanged"></asp:TextBox>
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
                                                            <asp:DropDownList ID="ddlModeOfRec" Enabled="false" runat="server" CssClass="simpletxt1">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 8%" align="right">
                                                            Language
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList Width="170px" ID="ddlLanguage" runat="server" CssClass="simpletxt1">
                                                                <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>
                                                            </asp:DropDownList>
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
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Valid Email is required."
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
                                                            <asp:CheckBox ID="chkUpdateCustomerData" Text="Update Customer Info" Checked="false"
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
                                                        </td>
                                                        <td align="right">
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnGo" Visible="false" runat="server" CausesValidation="false" Text="Find"
                                                                CssClass="btn" OnClientClick="return funOpenDivSC();" OnClick="btnGo_Click" />
                                                            <%--<input type="button" id="btnGo" value="Find" class="btn" onclick="return funOpenDivSC();" />--%>
                                                        </td>
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
                                                    <tr class="hideRow">
                                                        <td>
                                                            Territory
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:DropDownList Width="175px" ID="ddlSC" runat="server" CssClass="simpletxt1" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlSC_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                    </tr>
                                                    <tr class="hideRow">
                                                        <td>
                                                            Customer Type
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:DropDownList ID="ddlCustomerType" runat="server" CssClass="simpletxt1">
                                                                <asp:ListItem Selected="True" Value="L">Dealer</asp:ListItem>
                                                            </asp:DropDownList>
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
                                                                Text="Save & Forward to Call Center" Style="width: 200px" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="btnAddMore" runat="server" CssClass="btn" OnClick="btnAddMore_Click"
                                                                Text="Add More" Width="70px" Visible="False" />
                                                            <asp:Button ID="btnCancel" runat="server" CausesValidation="false" CssClass="btn"
                                                                OnClick="btnCancel_Click" OnClientClick="return funConfirm();" Text="Cancel" />
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
                                                                                        <%#Eval("ComplaintRefNo")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                                    HeaderText="Web form complaint">
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("SCName")%>
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
