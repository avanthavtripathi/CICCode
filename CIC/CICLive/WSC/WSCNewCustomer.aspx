<%@ Page Language="C#" MasterPageFile="~/MasterPages/WSCCICPage.master" AutoEventWireup="true"
    CodeFile="WSCNewCustomer.aspx.cs" Inherits="WSC_WSCNewCustomer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function funConfirm() {
            if (confirm('Are you sure to cancel this request?')) {
                return true;
            }
            return false;
        }
        
        function funUploadPopUp(CRefNo) {
            var strUrl = 'WSCChangeComplainStatus.aspx?BaseID=' + CRefNo;
            custWin = window.open(strUrl, 'SCPopup', 'height=300,width=750,left=20,top=30,scrollbars=1');
            if (window.focus) { custWin.focus() }
        }
            
    </script>

    <asp:UpdatePanel ID="pnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                       Customer Feedback / Suggestion Form
                    </td>
                    <td id="tdRequestNo" runat="server">
                    </td>
                </tr>
                <tr id="trLogout" runat="server">
                    <td colspan="2" align="right" style="padding-right: 25px">
                        <asp:LinkButton ID="lbtnLogout" runat="server" CausesValidation="false" CssClass="MTOLink" Text="Logout" OnClick="lbtnLogout_Click"></asp:LinkButton>
                    </td>
                </tr>
                <tr id="trMain" runat="server">
                    <td colspan="2">
                        <table width="100%">
                            <tr>
                                <td>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td style="padding-left: 20px" width="87%">
                                                &nbsp;
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
                                            <td colspan="2">
                                                <asp:GridView ID="gvFresh" runat="server" AllowPaging="True" AllowSorting="True"
                                                    AlternatingRowStyle-CssClass="fieldName" AutoGenerateColumns="False" DataKeyNames="BaseLineId"
                                                    HeaderStyle-CssClass="fieldNamewithbgcolor" Width="100%" RowStyle-CssClass="gridbgcolor"
                                                    OnRowDataBound="gvFresh_RowDataBound">
                                                    <RowStyle CssClass="gridbgcolor" />
                                                    <Columns>
                                                        <%--<asp:BoundField DataField="RowNumber" HeaderText="Sno." />--%>
                                                        <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                            <HeaderStyle Width="40px" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Complaint RefNo" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdnBaselineID" runat="server" Value='<%#Eval("BaseLineId")%>' />
                                                                <asp:HiddenField ID="hdnComplaint_RefNo" runat="server" Value='<%#Eval("Complaint_RefNo")%>' />
                                                                <asp:HiddenField ID="gvFreshhdnCallStatus" runat="server" Value='<%#Eval("CallStatus")%>' />
                                                                <asp:HiddenField ID="gvFreshhdnCallStage" runat="server" Value='<%#Eval("CallStage")%>' />
                                                                <asp:HiddenField ID="gvFreshhdnNatureOfComplaint" runat="server" Value='<%#Eval("NatureOfComplaint")%>' />
                                                                <asp:HiddenField ID="gvFreshhdnSLADate" runat="server" Value='<%#Eval("SLADate")%>' />
                                                                <%--<a href="Javascript:void(0);" onclick="funCommonPopUp(<%#Eval("BaseLineId")%>)">--%>
                                                                <%#Eval("Complaint_RefNo")%>/<%#Eval("split")%><%--</a>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <%#Eval("FormatedLoggedDate")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Product" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <%#Eval("Unit_Desc")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Engineer Name" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" Visible="false">
                                                            <ItemTemplate>
                                                                <%#Eval("SEName")%>
                                                                <%--<%#Eval("NatureOfComplaint")%>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Comm. History" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <%#Eval("LastComment")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Stage" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <%#Eval("CallStage")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Change Staus" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <%--<asp:LinkButton ID="LinkButton1" OnClientClick="funUploadPopUp('')<asp:Label runat="server" Text="Label"></asp:Label>"  runat="server">LinkButton</asp:LinkButton>
                                                                
                                                                
                                                             --%>
                                                                <asp:Label ID="lblLnk" runat="server" Text=""><a href="Javascript:void(0);" onclick="funUploadPopUp('<%#Eval("BaseLineId")%>')">Click for closer feedback</a></asp:Label>
                                                                <asp:Label ID="lblstatus" runat="server" Text=""></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                    <AlternatingRowStyle CssClass="fieldName" />
                                                    <EmptyDataTemplate>
                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                                    <b>No Complaints found</b>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" class="bgcolorcomm">
                                                <table width="90%" border="0" cellpadding="1" cellspacing="0" align="center">
                                                <tr>
                                                        <td valign="top" style="line-height:40px;font-weight:bold;background-color:#dddddd">
                                                            Service Request Type<font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3" style="font-weight:bold;background-color:#dddddd">
                                                            <asp:DropDownList Width="172px" runat="server" CssClass="simpletxt1" ID="ddlFeedbackType"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlFeedbackType_SelectedIndexChanged">
                                                                <asp:ListItem Selected="True" Text="Select" Value="0" />
                                                                <asp:ListItem Text="Enquiry" Value="3" />
                                                                <asp:ListItem Text="Feedback" Value="2" />
                                                                <asp:ListItem Text="Installation/commissioning" Value="7" />
                                                                <asp:ListItem Text="Breakdown/Complaint" Value="1" />
                                                           </asp:DropDownList>
                                                            <asp:CompareValidator SetFocusOnError="true" ID="CVFeedbackType" runat="server" ControlToValidate="ddlFeedbackType"
                                                                ErrorMessage="Feedback Type is required." Operator="NotEqual" ValueToCompare="0"
                                                                Display="None"></asp:CompareValidator>
                                                        </td>
                                                    </tr>
                                                    <tr style="line-height:40px">
                                                        <td colspan="3">
                                                            <b>Customer Information:</b> 
                                                        </td>
                                                        <td align='<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>'>
                                                            <asp:HiddenField ID="hdnProductSno" runat="server" />
                                                            &nbsp;<asp:HiddenField ID="hdnScNo" runat="server" />
                                                            <font color="red">*</font> <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" height="12px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                            Prefix
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:DropDownList ID="ddlPrefix" runat="server" CssClass="simpletxt1">
                                                                <asp:ListItem Selected="True" Value="Mr">Mr</asp:ListItem>
                                                                <asp:ListItem Value="Miss">Miss</asp:ListItem>
                                                                <asp:ListItem Value="Mrs">Mrs</asp:ListItem>
                                                                <asp:ListItem Value="Dr">Dr</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            First Name<font color="red">*</font>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtboxtxt" Width="164px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RFFirstName" runat="server" ControlToValidate="txtFirstName"
                                                                ErrorMessage="First Name is required." SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            Last Name<font color="red">*</font>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtLastName" runat="server" CssClass="txtboxtxt" Width="164px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RFLastName" runat="server" ControlToValidate="txtLastName"
                                                                ErrorMessage="Last Name is required." SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Customer Type
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:DropDownList ID="ddlCustomerType" runat="server" CssClass="simpletxt1">
                                                                <asp:ListItem Selected="True" Value="I">Industrial</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Company Name<font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox runat="server" Width="164px" CssClass="txtboxtxt" ID="txtCompanyName"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RFCompanyName" runat="server" ControlToValidate="txtCompanyName"
                                                                ErrorMessage="Company Name is required." SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Address1<font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox runat="server" MaxLength="50" Width="300px" CssClass="txtboxtxt" ID="txtAdd1"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RFAddress" runat="server" SetFocusOnError="true"
                                                                ControlToValidate="txtAdd1" ErrorMessage="Address is required." Display="None"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Address2
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtAdd2" runat="server" MaxLength="50" CssClass="txtboxtxt" Width="300px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Address3
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtAdd3" runat="server" MaxLength="50" CssClass="txtboxtxt" Width="300px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Country<font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:DropDownList Width="172px" runat="server" CssClass="simpletxt1" ID="ddlCountry"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:CompareValidator SetFocusOnError="true" ID="CVCountry" runat="server" ControlToValidate="ddlCountry"
                                                                ErrorMessage="Country is required." Operator="NotEqual" ValueToCompare="0" Display="None"></asp:CompareValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            State<font color='red'>*</font>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList Width="172px" runat="server" CssClass="simpletxt1" ID="ddlState"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:CompareValidator SetFocusOnError="true" ID="CVState" runat="server" ControlToValidate="ddlState"
                                                                ErrorMessage="State is required." Operator="NotEqual" ValueToCompare="0" Display="None"></asp:CompareValidator>
                                                        </td>
                                                        <td>
                                                            City<font color='red'>*</font>
                                                        </td>
                                                        <td valign="top" align="left">
                                                            <asp:DropDownList Width="250px" ID="ddlCity" runat="server" CssClass="simpletxt1">
                                                            </asp:DropDownList>
                                                            <asp:CompareValidator SetFocusOnError="true" ID="CVCity" runat="server" ControlToValidate="ddlCity"
                                                                ErrorMessage="City is required." Operator="NotEqual" ValueToCompare="0" Display="None"></asp:CompareValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Pin Code
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtPinCode" runat="server" Width="166px" CssClass="txtboxtxt" MaxLength="6" />
                                                            <asp:RegularExpressionValidator ID="REPincode" runat="server" ControlToValidate="txtPinCode"
                                                                ErrorMessage="Pin Code Can't less 6 digits!" Display="None" ValidationExpression="^(\d){6}$"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Contact No.<font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtContactNo" MaxLength="11" runat="server" CssClass="txtboxtxt"
                                                                Width="164px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RFContractNo" SetFocusOnError="true" ControlToValidate="txtContactNo"
                                                                runat="server" ErrorMessage="Contact No is required." Display="None"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RGContractNo" runat="server" ErrorMessage="Valid Number is required."
                                                                ControlToValidate="txtContactNo" SetFocusOnError="True" ValidationExpression="\d{10,11}"
                                                                Display="None"></asp:RegularExpressionValidator>
                                                            For eg: For land line - 02267558912 & for cell no. 09821474747
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            E-Mail<font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtEmail" MaxLength="99" runat="server" CssClass="txtboxtxt" Width="300px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RFEmail" SetFocusOnError="true" ControlToValidate="txtEmail"
                                                                runat="server" ErrorMessage="Email is required." Display="None"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RGEmail" runat="server" ErrorMessage="Valid Email is required."
                                                                ControlToValidate="txtEmail" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                Display="None"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Fax No.
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFaxNo" runat="server" CssClass="txtboxtxt" MaxLength="11"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtFaxNo"
                                                                ErrorMessage="Valid Fax is required." SetFocusOnError="True" ValidationExpression="\d{10,11}"
                                                                Display="None"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <%--<div id="divPassword" runat="server">
                                                        <tr>
                                                            <td>
                                                                Password.<font color='red'>*</font>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="txtPassword" MaxLength="15" TextMode="Password" runat="server" CssClass="txtboxtxt"
                                                                    Width="164px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="ReqPassword" SetFocusOnError="true" ControlToValidate="txtPassword"
                                                                    runat="server" ErrorMessage="Password is required." Display="None"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Confirm Password.<font color='red'>*</font>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="txtboxtxt"
                                                                    MaxLength="15" Width="164px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RegConfirmPassword" SetFocusOnError="true" ControlToValidate="txtConfirmPassword"
                                                                    runat="server" ErrorMessage="Confirm Password is required." Display="None"></asp:RequiredFieldValidator>
                                                                <asp:CompareValidator ID="ReqPasswordCompare" runat="server" SetFocusOnError="True"
                                                                    ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" Display="None"
                                                                    ErrorMessage="The Confirm Password must match the Password entry."></asp:CompareValidator>
                                                            </td>
                                                        </tr>
                                                    </div>--%>
                                                    <tr>
                                                        <td>
                                                            Product Division<font color='red'>*</font>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList Width="172px" ID="ddlProductDiv" AutoPostBack="true" runat="server"
                                                                CssClass="simpletxt1" OnSelectedIndexChanged="ddlProductDiv_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:CompareValidator SetFocusOnError="true" ID="CVProductDiv" runat="server" ControlToValidate="ddlProductDiv"
                                                                ErrorMessage="Product Division is required." Operator="NotEqual" ValueToCompare="0"
                                                                Display="None"></asp:CompareValidator>
                                                            <asp:HiddenField ID="hdnprodDiv" runat="server" Value="0" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trProductDiv" runat="server">
                                                        <td>
                                                            Product<font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:DropDownList Width="300px" ID="ddlProductLine" runat="server" CssClass="simpletxt1">
                                                            </asp:DropDownList>
                                                            <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator1" runat="server"
                                                                ControlToValidate="ddlProductLine" ErrorMessage="Product is required." Operator="NotEqual"
                                                                ValueToCompare="0" Display="None"></asp:CompareValidator>
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr id="trfeedback" runat="server">
                                                        <td valign="top">
                                                            Feedback<font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtfeedback" runat="server" CssClass="txtboxtxt" Width="482" TextMode="MultiLine"
                                                                Height="72px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RFFeedback" SetFocusOnError="true" ControlToValidate="txtfeedback"
                                                                runat="server" ErrorMessage="Feedback is required." Display="None"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr id="trProduct" runat="server" visible="false">
                                                        <td>
                                                            Product:
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:DropDownList Width="350px" ID="ddlProduct" runat="server" CssClass="simpletxt1">
                                                            </asp:DropDownList>
                                                            <asp:CompareValidator SetFocusOnError="true" ID="cvProduct" runat="server" ControlToValidate="ddlProduct"
                                                                ErrorMessage="Product is required." Operator="NotEqual" ValueToCompare="0" Display="None"></asp:CompareValidator>
                                                        </td>
                                                    </tr>
                                                    <tr id="trRating" runat="server">
                                                        <td>
                                                            Rating/Voltage Class<font color='red'>*</font>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtRating" runat="server" CssClass="txtboxtxt" Width="164"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RFRating" runat="server" SetFocusOnError="true" ControlToValidate="txtRating"
                                                                ErrorMessage="Rating/Voltage Class is required." Display="None"></asp:RequiredFieldValidator>
                                                        </td>
                                                      <td></td>
                                                    </tr>
                                                    <tr>
                                                     <td>
                                                            Year of Manufacture<%--<font color='red'>*</font>--%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox MaxLength="10" runat="server" ID="txtxPurchaseDate" CssClass="txtboxtxt" />
                                                            <asp:CompareValidator ID="CVPurchaseDate" runat="server" Type="Date" Operator="DataTypeCheck"
                                                                ControlToValidate="txtxPurchaseDate" Display="None" ErrorMessage="Valid date is required. "></asp:CompareValidator>
                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtxPurchaseDate">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr id="tr2" runat="server">
                                                        <td>
                                                            Equipment Serial No. <font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtManufacturerSerialNo" runat="server" CssClass="txtboxtxt" MaxLength="15"
                                                                Width="164"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RFManufacturerSerialNo" SetFocusOnError="true" ControlToValidate="txtManufacturerSerialNo"
                                                                runat="server" ErrorMessage="Manufacturer Serial No is required." Display="None"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr id="trSite" runat="server">
                                                        <td valign="top">
                                                            Site Location/Site Address<font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtlocation" runat="server" CssClass="txtboxtxt" Width="162px" 
                                                                MaxLength="150"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RFSiteLocation" runat="server" SetFocusOnError="true"
                                                                ControlToValidate="txtlocation" ErrorMessage="Site Location is required." Display="None"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr id="tr1" runat="server">
                                                        <td valign="top">
                                                           Equipment Name<font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtequipname" runat="server" CssClass="txtboxtxt" 
                                                                Width="162px" MaxLength="150"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true"
                                                                ControlToValidate="txtequipname" ErrorMessage="Equipment name is required." Display="None"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                     <tr id="tr3" runat="server">
                                                        <td valign="top">
                                                           Coach No.
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtcoachNo" runat="server" CssClass="txtboxtxt" MaxLength="150" 
                                                                Width="164px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                     <tr id="tr4" runat="server">
                                                        <td valign="top">
                                                           Train No.</td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtTrainNo" runat="server" CssClass="txtboxtxt" MaxLength="150" 
                                                                Width="164px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                     <tr id="tr5" runat="server">
                                                        <td valign="top">
                                                          Availability of Coach/BLDC Fan at depot.</td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtAvailblty" runat="server" CssClass="txtboxtxt" MaxLength="150" 
                                                                Width="164px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="tr6" runat="server">
                                                        <td valign="top">
                                                           Date of Installation</td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtInstallDate" runat="server" CssClass="txtboxtxt" Width="164px" ></asp:TextBox>
                                                              <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtInstallDate">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                       <tr id="tr7" runat="server">
                                                        <td valign="top">
                                                           Failure Date<font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtfailureDate" runat="server" CssClass="txtboxtxt" MaxLength="150" 
                                                                Width="164px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvfd" runat="server" SetFocusOnError="true"
                                                                ControlToValidate="txtfailureDate" ErrorMessage="failure date is required." Display="None"></asp:RequiredFieldValidator>
                                                                 <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtfailureDate" >
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr id="trNature" runat="server">
                                                        <td valign="top">
                                                            Nature of Complaint/Description<font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtCompNature" runat="server" CssClass="txtboxtxt" 
                                                                Width="162px" TextMode="MultiLine"
                                                                Height="50px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RFNatureOfComplaint" runat="server" SetFocusOnError="true"
                                                                ControlToValidate="txtCompNature" ErrorMessage="Nature of Complaint is required."
                                                                Display="None"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr id="trUploadFile" runat="server">
                                                        <td>
                                                            Upload File
                                                        </td>
                                                        <td colspan="3">
                                                            <%--<asp:FileUpload ID="flUpload" runat="server" />--%>
                                                            <input type="file" class="btn" id="flUpload" runat="server" onkeydown="if(event.keyCode==9){return true;}else{return false;}" />&nbsp;
                                                            <asp:Button ID="btnFileUpload" runat="server" CssClass="btn" CausesValidation="false"
                                                                Text="Upload" OnClick="btnFileUpload_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
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
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="4">
                                                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn" OnClick="btnSubmit_Click"
                                                                Text="Submit" Width="70px" />
                                                            &nbsp;&nbsp;
                                                            <asp:Button ID="btnCancel" runat="server" CausesValidation="false" CssClass="btn"
                                                                OnClick="btnCancel_Click" OnClientClick="return funConfirm();" Text="Cancel" />
                                                            &nbsp;
                                                            <asp:HiddenField ID="hdnKeyForUpdate" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="4">
                                                            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="padding-left: 300px;" colspan="4">
                                                            <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                                                                runat="server" />
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
        <Triggers>
            <asp:PostBackTrigger ControlID="btnFileUpload" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
