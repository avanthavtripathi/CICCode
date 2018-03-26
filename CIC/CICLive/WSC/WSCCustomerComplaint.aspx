<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="WSCCustomerComplaint.aspx.cs" Inherits="WSC_WSCCustomerComplaint" %>

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
            
           
    </script>

    <table width="100%">
        <tr>
            <td class="headingred">
                Feedback Response Screen
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
            <td colspan="2" align="right" style="padding-right: 10px">
                <asp:RadioButtonList ID="rdoboth" RepeatDirection="Horizontal" RepeatColumns="3"
                    runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdoboth_SelectedIndexChanged">
                    <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                    <asp:ListItem Value="0" Text="In-Active"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Both" Selected="True"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="pnl" runat="server" UpdateMode="Always">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnFileUpload" />
                                    <asp:PostBackTrigger ControlID="ddlFeedbackType" />
                                </Triggers>
                                <ContentTemplate>
                                    <table border="0" width="100%">
                                        <tr>
                                            <td align="left" style="padding-left: 20px" class="MsgTDCount">
                                                Total Number of Records :
                                                <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                            </td>
                                            <td align="right">
                                                Search For
                                                <asp:DropDownList ID="ddlSearch" runat="server" Width="140px" CssClass="simpletxt1">
                                                    <asp:ListItem Text="First Name" Value="FirstName"></asp:ListItem>
                                                    <asp:ListItem Text="Last Name" Value="LastName"></asp:ListItem>
                                                    <asp:ListItem Text="Company Name" Value="Company_Name"></asp:ListItem>
                                                    <asp:ListItem Text="Email" Value="Email"></asp:ListItem>
                                                </asp:DropDownList>
                                                With
                                                <asp:TextBox ID="txtSearch" runat="server" CssClass="txtboxtxt" Width="100px" Text=""></asp:TextBox>
                                                <asp:Button Text="Go" Width="25px" CssClass="btn" ID="imgBtnGo" runat="server" CausesValidation="False"
                                                    ValidationGroup="editt" OnClick="imgBtnGo_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="80%" border="0" cellpadding="1" cellspacing="0" align="center">
                                        <tr>
                                            <td>
                                                <asp:GridView RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                                    HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="True"
                                                    AllowSorting="True" DataKeyNames="WSCCustomerId" AutoGenerateColumns="False"
                                                    ID="gvComm" runat="server" OnPageIndexChanging="gvComm_PageIndexChanging" Width="100%"
                                                    OnSelectedIndexChanging="gvComm_SelectedIndexChanging" HorizontalAlign="Left"
                                                    OnRowDataBound="gvComm_RowDataBound" OnSorting="gvComm_Sorting" OnRowUpdating="gvComm_RowUpdating">
                                                    <RowStyle CssClass="gridbgcolor" />
                                                    <Columns>
                                                        <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                            <HeaderStyle Width="40px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="WebRequest_RefNo" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Web Request No" SortExpression="WebRequest_RefNo">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Name" SortExpression="Name">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Company_Name" SortExpression="Company_Name" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left" HeaderText="Company">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Email" SortExpression="Email">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Address" SortExpression="Address" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left" HeaderText="Address">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Contact_No" SortExpression="Contact_No" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left" HeaderText="ContactNo">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Country_Desc" SortExpression="Country_Desc" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left" HeaderText="Country">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="State_Desc" SortExpression="State_Desc" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left" HeaderText="State">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="City_desc" SortExpression="City_desc" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left" HeaderText="City">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="FeedbackType" SortExpression="WSCFeedback_desc">
                                                            <ItemTemplate>
                                                               <asp:Label ID="LblFeedBackTypeID" runat="server" Text='<%# Bind("Feedback_Type") %>' Visible="false" ></asp:Label> <!-- Bhawesh add : 29-7-13     -->   
                                                                <asp:Label ID="lblFeedbackType" runat="server" Text='<%# Bind("WSCFeedback_desc") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Feedback" SortExpression="Feedback" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left" HeaderText="Feedback" Visible="false">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Unit_desc" Visible="false" SortExpression="Unit_desc"
                                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Product Division">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProductLine_desc" Visible="false" SortExpression="ProductLine_desc"
                                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="ProductLine">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Site_Location" Visible="false" SortExpression="Site_Location"
                                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Site_Location">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Nature_of_Complaint" Visible="false" SortExpression="Nature_of_Complaint"
                                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Nature_of_Complaint">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CGWSCFeedback_desc" SortExpression="CGWSCFeedback_desc"
                                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Feedback Category">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Complaint_RefNo" SortExpression="Complaint_RefNo" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left" HeaderText="Complaint No">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="View" ShowHeader="true" ItemStyle-Width="200">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Update"
                                                                    CommandArgument='<%#Eval("WSCCustomerId")%>' Text='<%#Eval("IsViewed")%>'>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="200px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="View" ShowHeader="true" ItemStyle-Width="200" Visible="false" >
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnConverttoMTO" runat="server" CausesValidation="false" Text="Convert to MTO"
                                                                    CommandArgument='<%#Eval("WSCCustomerId")%>' CommandName='<%# Eval("Feedback_Type") %>'
                                                                    OnClick="lbtnConverttoMTO_Click">
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="200px" />
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowSelectButton="True" HeaderStyle-Width="60px" HeaderText="Edit"
                                                            Visible="false">
                                                            <HeaderStyle Width="60px" />
                                                        </asp:CommandField>
                                                    </Columns>
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
                                                    <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                    <AlternatingRowStyle CssClass="fieldName" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblInformation"
                                        runat="server">
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="bgcolorcomm">
                                                <table width="90%" border="0" cellpadding="1" cellspacing="0" align="center">
                                                    <tr>
                                                        <td height="1" width="24%">
                                                        </td>
                                                        <td width="20%">
                                                        </td>
                                                        <td width="18%">
                                                        </td>
                                                        <td width="38%">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <b>Customer Information:</b> &nbsp;
                                                        </td>
                                                        <td align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                                            <asp:HiddenField ID="hdnWSCCustomerId" runat="server" />
                                                            &nbsp;<asp:HiddenField ID="hdnWebRequest_RefNo" runat="server" />
                                                            <font color='red'>*</font>
                                                            <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
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
                                                        <td style="padding-left: 20px">
                                                            Last Name<font color="red">*</font>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtLastName" runat="server" CssClass="txtboxtxt" Width="164px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RFLastName" runat="server" ControlToValidate="txtLastName"
                                                                ErrorMessage="Last Name is required." SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Company Name<font color='red'>*</font>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" Width="164px" CssClass="txtboxtxt" ID="txtCompanyName"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RFCompanyName" runat="server" ControlToValidate="txtCompanyName"
                                                                ErrorMessage="Company Name is required." SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td style="padding-left: 20px">
                                                            E-Mail<font color='red'>*</font>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtEmail" MaxLength="60" runat="server" CssClass="txtboxtxt" Width="164px"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RGEmail" runat="server" ErrorMessage="Valid Email is required."
                                                                ControlToValidate="txtEmail" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                Display="None"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Address<font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox runat="server" TextMode="MultiLine" Width="548" Height="35" CssClass="txtboxtxt"
                                                                ID="txtAdd1"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RFAddress" runat="server" SetFocusOnError="true"
                                                                ControlToValidate="txtAdd1" ErrorMessage="Address is required." Display="None"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Telephone/Mobile number<font color='red'>*</font>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtContactNo" MaxLength="11" runat="server" CssClass="txtboxtxt"
                                                                Width="164px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RFContractNo" SetFocusOnError="true" ControlToValidate="txtContactNo"
                                                                runat="server" ErrorMessage="Telephone/Mobile number is required." Display="None"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RGContractNo" runat="server" ErrorMessage="Valid Number is required."
                                                                ControlToValidate="txtContactNo" SetFocusOnError="True" ValidationExpression="\d{10,11}"
                                                                Display="None"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td style="padding-left: 20px" valign="top">
                                                            Country<font color='red'>*</font>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList Width="170px" runat="server" CssClass="simpletxt1" ID="ddlCountry"
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
                                                            <asp:DropDownList Width="170px" runat="server" CssClass="simpletxt1" ID="ddlState"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:CompareValidator SetFocusOnError="true" ID="CVState" runat="server" ControlToValidate="ddlState"
                                                                ErrorMessage="State is required." Operator="NotEqual" ValueToCompare="0" Display="None"></asp:CompareValidator>
                                                        </td>
                                                        <td valign="top" style="padding-left: 20px">
                                                            City<font color='red'>*</font>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:DropDownList Width="170px" ID="ddlCity" runat="server" CssClass="simpletxt1">
                                                            </asp:DropDownList>
                                                            <asp:CompareValidator SetFocusOnError="true" ID="CVCity" runat="server" ControlToValidate="ddlCity"
                                                                ErrorMessage="City is required." Operator="NotEqual" ValueToCompare="0" Display="None"></asp:CompareValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            Type of Feedback<font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:DropDownList Width="170px" runat="server" CssClass="simpletxt1" ID="ddlFeedbackType"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlFeedbackType_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:CompareValidator SetFocusOnError="true" ID="CVFeedbackType" runat="server" ControlToValidate="ddlFeedbackType"
                                                                ErrorMessage="Feedback Type is required." Operator="NotEqual" ValueToCompare="0"
                                                                Display="None"></asp:CompareValidator>
                                                        </td>
                                                    </tr>
                                                    <tr id="trfeedback" runat="server">
                                                        <td valign="top">
                                                            Feedback<font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtfeedback" runat="server" CssClass="txtboxtxt" Width="548" TextMode="MultiLine"
                                                                Height="72px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RFFeedback" SetFocusOnError="true" ControlToValidate="txtfeedback"
                                                                runat="server" ErrorMessage="Feedback is required." Display="None"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr id="trProductDiv" runat="server">
                                                        <td>
                                                            Product Division<font color='red'>*</font>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList Width="170px" ID="ddlProductDiv" AutoPostBack="true" runat="server"
                                                                CssClass="simpletxt1" OnSelectedIndexChanged="ddlProductDiv_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:CompareValidator SetFocusOnError="true" ID="CVProductDiv" runat="server" ControlToValidate="ddlProductDiv"
                                                                ErrorMessage="Product Division is required." Operator="NotEqual" ValueToCompare="0"
                                                                Display="None"></asp:CompareValidator>
                                                        </td>
                                                        <td style="padding-left: 20px">
                                                            Product Line<font color='red'>*</font>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList Width="168px" ID="ddlProductLine" runat="server" CssClass="simpletxt1">
                                                            </asp:DropDownList>
                                                            <asp:CompareValidator SetFocusOnError="true" ID="CVProductLine" runat="server" ControlToValidate="ddlProductLine"
                                                                ErrorMessage="Product Line is required." Operator="NotEqual" ValueToCompare="0"
                                                                Display="None"></asp:CompareValidator>
                                                        </td>
                                                    </tr>
                                                    <tr id="trMgf" runat="server">
                                                        <td>
                                                            Mgf. Serial No<font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtManufacturerSerialNo" runat="server" CssClass="txtboxtxt" MaxLength="15"
                                                                Width="164"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RFManufacturerSerialNo" SetFocusOnError="true" ControlToValidate="txtManufacturerSerialNo"
                                                                runat="server" ErrorMessage="Manufacturer Serial No is required." Display="None"></asp:RequiredFieldValidator>
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
                                                        <td style="padding-left: 20px">
                                                            Year of Manufacture<font color='red'>*</font>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox MaxLength="10" runat="server" ID="txtxPurchaseDate" CssClass="txtboxtxt"
                                                                Width="164" />
                                                            <asp:CompareValidator ID="CVPurchaseDate" runat="server" Type="Date" Operator="DataTypeCheck"
                                                                ControlToValidate="txtxPurchaseDate" Display="None" ErrorMessage="Valid date is required. "></asp:CompareValidator>
                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtxPurchaseDate">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr id="trSite" runat="server">
                                                        <td valign="top">
                                                            Site Location/Site Address/ City/State<font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtlocation" runat="server" CssClass="txtboxtxt" Width="548" TextMode="MultiLine"
                                                                Height="50px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RFSiteLocation" runat="server" SetFocusOnError="true"
                                                                ControlToValidate="txtlocation" ErrorMessage="Site Location is required." Display="None"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr id="trNature" runat="server">
                                                        <td valign="top">
                                                            Nature of Complaint/Description<font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtCompNature" runat="server" CssClass="txtboxtxt" Width="548" TextMode="MultiLine"
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
                                                            <input type="file" class="btn" id="flUpload" runat="server" onkeydown="if(event.keyCode==9){return true;}else{return false;}" />&nbsp;<asp:Button
                                                                ID="btnFileUpload" runat="server" CssClass="btn" CausesValidation="false" Text="Upload"
                                                                OnClick="btnFileUpload_Click" />
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
                                                        <td>
                                                            CG executive
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:DropDownList Width="168px" ID="ddlGCExecutive" runat="server" CssClass="simpletxt1">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            CG Executive comments
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtCGExecutive" runat="server" CssClass="txtboxtxt" Width="546"
                                                                TextMode="MultiLine" Height="50px"></asp:TextBox>
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
                                                                Text="Convert to MTO" Width="90px" />
                                                            &nbsp;&nbsp;
                                                            <asp:Button ID="btnCancel" runat="server" CausesValidation="false" CssClass="btn"
                                                                OnClick="btnCancel_Click" OnClientClick="return funConfirm();" Text="Cancel" />
                                                            &nbsp;
                                                            <asp:HiddenField ID="hdnProductSrno" runat="server" />
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
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
