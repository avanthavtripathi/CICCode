<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="ComplaintRegistrationOutside.aspx.cs" Inherits="pages_ComplaintRegistration" %>
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
    

      function goBack()
      {
      window.history.back()
      }
</script>

   <table width="100%" align="center">
        
          <tr>
            <td class="bgcolorcomm" colspan="2">
                    <table width="99%">
                        <tr style="margin-top:5px;">
                            <td class="headingred" align="center" style="font-size:15px">
                            Complaint Registration Form (International Division)</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="pnl" runat="server">
                                    <ContentTemplate>
                                        <table width="100%">
                                     
                                            <tr height="20">
                                                <td align="right">
                                                   <asp:HiddenField ID="hdnCustomerId" Value="0" runat="server" />
                                                </td>
                                                <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                                                    <asp:UpdateProgress AssociatedUpdatePanelID="pnl" ID="UpdateProgress1" runat="server">
                                                        <ProgressTemplate>
                                                            <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <table width="100%" border="0" cellpadding="1" cellspacing="0" class="bgcolorcomm">
                                                        <tr>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                                               
                                                                <font color="red">*</font>&nbsp;<%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                                            </td>
                                                        </tr>
                                                        
                                                        
                                                        
                                                        
                                                        <tr>
                                                     <td colspan="2">
                                                     <table width="100%" border="0">
                                                        <tr>
                                                            <td colspan="3">
                                                                <b>Customer Information:</b> &nbsp;
                                                            </td>
                                                            <td align='<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>'>
                                                                &nbsp;</td>
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
                                                                <div>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" SetFocusOnError="true"
                                                                    ControlToValidate="txtAdd1" ErrorMessage="Address1 is required." Display="Dynamic"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                              Address 2
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="txtAdd2" runat="server" CssClass="txtboxtxt" MaxLength="50" Width="350px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr id="trLandMark" runat="server">
                                                            <td>
                                                                Landmark
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="txtLandmark" runat="server" CssClass="txtboxtxt" MaxLength="150" Width="350px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" style="width:24%"> Country<font color='red'>*</font>
                                                            </td>
                                                            <td style="width:26%" >
                                                            
                                                                <asp:DropDownList Width="170px" runat="server" CssClass="simpletxt1" ID="DdlCountry">
                                                                </asp:DropDownList>
                                                                <div>
                                                                   <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvCountry" runat="server"
                                                                    ControlToValidate="DdlCountry" ErrorMessage="Country is required." InitialValue="0" Display="Dynamic" />
                                                                </div>
                                                            </td>
                                                            <td valign="top" style="width:15%"> City<font color='red'>*</font>
                                                            </td>
                                                            <td valign="top" style="width:35%">
                                                                <asp:TextBox ID="txtCity" Width="100px" MaxLength="30" runat="server" CssClass="txtboxtxt"></asp:TextBox>
                                                                <div>
                                                                   <asp:RequiredFieldValidator ID="reqCity" runat="server" SetFocusOnError="true" ErrorMessage="City is required."
                                                                    ControlToValidate="txtCity" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td>&nbsp;</td>
                                                            <td colspan="3">
                                                               
                                                            </td>
                                                        </tr>
                                                        
                                                        <tr>
                                                            <td colspan="4" height="24">
                                                                <b>Contact Information:</b>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Contact No.<font color='red'>*</font>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="txtContactNo" MaxLength="15" runat="server" CssClass="txtboxtxt"></asp:TextBox>
                                                                <div>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" ControlToValidate="txtContactNo"
                                                                    runat="server" ErrorMessage="Contact Number is required." Display="Dynamic" ></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Valid Contact Number is required."
                                                                    ControlToValidate="txtContactNo" SetFocusOnError="True" ValidationExpression="\d{10,15}" Display="Dynamic" ></asp:RegularExpressionValidator>
                                                                </div>    
                                                            </td>
                                                        </tr>
                                                        <tr id="trAlternate" runat="server">
                                                            <td>
                                                                Alternate Contact No.
                                                            </td>
                                                            <td >
                                                                <asp:TextBox runat="server" MaxLength="15" ID="txtAltConatctNo" CssClass="txtboxtxt"></asp:TextBox>
                                                                <div>
                                                                   <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Valid Alternate Contact Number is required."
                                                                    ControlToValidate="txtAltConatctNo" SetFocusOnError="True" ValidationExpression="\d{10,15}"  Display="Dynamic"></asp:RegularExpressionValidator>
                                                                 </div>
                                                            </td>
                                                            <td >
                                                                Extension
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtExtension" MaxLength="5" runat="server" CssClass="txtboxtxt"></asp:TextBox>
                                                                <div>
                                                                   <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Valid Extension is required."
                                                                    Operator="DataTypeCheck" ControlToValidate="txtExtension" Type="Integer" Display="Dynamic" ></asp:CompareValidator>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                E-Mail
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="txtEmail" MaxLength="60" runat="server" CssClass="txtboxtxt" Width="213px"></asp:TextBox>
                                                                 <div>
                                                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Valid Email is required."
                                                                    ControlToValidate="txtEmail" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"></asp:RegularExpressionValidator>
                                                                 </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                        <td colspan="4">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="btnFileUpload" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                            <table id="tblupfile" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                        <tr height="20">
                                                                            <td style="width:23%">
                                                                                Upload File
                                                                            </td>
                                                                            <td style="width:65%; padding-left:10px" >
                                                                                <input type="file" class="btn" id="flUpload" runat="server" onkeydown="if(event.keyCode==9){return true;}else{return false;}" />&nbsp;<asp:Button
                                                                                    ID="btnFileUpload" runat="server" CssClass="btn" CausesValidation="false" Text="Upload"
                                                                                    OnClick="btnFileUpload_Click" />
                                                                            </td>
                                                                            <td  style="width:12%">
                                                                                <asp:UpdateProgress AssociatedUpdatePanelID="UpdatePanel1" ID="UpdateProgress2" runat="server">
                                                                                    <ProgressTemplate>
                                                                                        <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
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
                                                                        <tr>
                                                                        <td colspan="3"></td>
                                                                        </tr>
                                                                    </table>
                                                       <br />
                                                       <asp:UpdatePanel runat="server" UpdateMode="Conditional" >
                                                       <ContentTemplate>
                                                         <table width="100%" border="0" cellpadding="2" cellspacing="0">
                                                                        <tr>
                                                                            <td colspan="4">
                                                                                <b>Product & Complaint Information:</b> 
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                        <td colspan="4"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td >
                                                                                Product Division<font color='red'>*</font>
                                                                            </td>
                                                                            <td colspan="3">
                                                                                <asp:DropDownList Width="175px" ID="ddlProductDiv" AutoPostBack="true" runat="server"
                                                                                    CssClass="simpletxt1" OnSelectedIndexChanged="ddlProductDiv_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                                <div>
                                                                                   <asp:CompareValidator SetFocusOnError="true" ID="compValProdDiv" runat="server" ControlToValidate="ddlProductDiv"
                                                                                    ErrorMessage="Product Division is required." Operator="NotEqual" ValueToCompare="0" Display="Dynamic"></asp:CompareValidator>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td >
                                                                                Product Line
                                                                            </td>
                                                                            <td colspan="3">
                                                                                <asp:DropDownList ID="ddlProductLine" runat="server" CssClass="simpletxt1" 
                                                                                    style="width:45%;" 
                                                                                    onselectedindexchanged="ddlProductLine_SelectedIndexChanged" 
                                                                                    AutoPostBack="True"  >
                                                                                    <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td >
                                                                                Product Group
                                                                            </td>
                                                                            <td colspan="3">
                                                                                <asp:DropDownList ID="ddlProductGroup" runat="server" CssClass="simpletxt1" 
                                                                                    style="width:45%;" AutoPostBack="True" 
                                                                                    onselectedindexchanged="ddlProductGroup_SelectedIndexChanged"  >
                                                                                    <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                          <tr>
                                                                            <td >
                                                                                Product
                                                                            </td>
                                                                            <td colspan="3">
                                                                                <asp:DropDownList ID="ddlProduct" runat="server" CssClass="simpletxt1" style="width:45%;"  >
                                                                                    <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td >
                                                                                Quantity<font color='red'>*</font>
                                                                            </td>
                                                                            <td colspan="3">
                                                                                <asp:TextBox ID="txtQuantity" runat="server" CssClass="txtboxtxt" MaxLength="2" Text="1"
                                                                                    Width="50px"></asp:TextBox>
                                                                                   <div>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Quantity is required."
                                                                                         SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                        <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="Valid Quantity is required."
                                                                                            Operator="DataTypeCheck" ControlToValidate="txtQuantity" Type="Integer" SetFocusOnError="true" Display="Dynamic"></asp:CompareValidator>
                                                                                   </div> 
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td valign="top" > Details<font color='red'>*</font>
                                                                            </td>
                                                                            <td colspan="3">
                                                                                <asp:TextBox ID="txtComplaintDetails" runat="server" CssClass="txtboxtxt" Width="380px"
                                                                                    TextMode="MultiLine" MaxLength="500" Height="72px"></asp:TextBox>
                                                                                 <div>
                                                                                   <asp:RequiredFieldValidator SetFocusOnError="true" ID="reqValComplaint" runat="server"
                                                                                    ControlToValidate="txtComplaintDetails" ErrorMessage="Complaint Details is required." Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                 </div>   
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width:24%">
                                                                                Invoice No.
                                                                            </td>
                                                                            <td style="width:26%">
                                                                                <asp:TextBox runat="server" ID="txtInvoiceNum" CssClass="txtboxtxt" MaxLength="50"
                                                                                    ValidationGroup="Report" />
                                                                            </td>
                                                                            <td style="width:15%">
                                                                                Purchase Date
                                                                            </td>
                                                                            <td style="width:35%">
                                                                                <asp:TextBox MaxLength="10" runat="server" ID="txtPurchaseDate" CssClass="txtboxtxt" />
                                                                                    <div>
                                                                                        <asp:CompareValidator ID="CompareValidator7" runat="server"
                                                                                        Type="Date" Operator="DataTypeCheck" ControlToValidate="txtPurchaseDate" 
                                                                                        ErrorMessage="Valid Purchase date is required." Display="Dynamic"></asp:CompareValidator>
                                                                                    </div>
                                                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                                                                                    TargetControlID="txtPurchaseDate">
                                                                                </cc1:CalendarExtender>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td >Purchased from</td>
                                                                            <td >
                                                                                <asp:TextBox ID="txtDealerName" runat="server" CssClass="txtboxtxt" 
                                                                                    MaxLength="50" ValidationGroup="Report" />
                                                                            </td>
                                                                              <td >Logged By</td>
                                                                            <td >
                                                                                <asp:TextBox ID="TxtLoggedBy" runat="server" CssClass="txtboxtxt" 
                                                                                    MaxLength="50" ValidationGroup="Report" />
                                                                                    <div>
                                                                                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="RfvLoggedBy" runat="server"
                                                                                    ControlToValidate="TxtLoggedBy" ErrorMessage="Please Enter Your Name." Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                    </div> 
                                                                            </td>
                                                                            
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4" align="left"></td>
                                                                        </tr>
                                                              </table>
                                                       </ContentTemplate>
                                                       </asp:UpdatePanel>
                                                            
                                                               
                                                            <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                                                        <tr>
                                                                            <td align="center">
                                                                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn" OnClick="btnSubmit_Click"
                                                                                    Text="Submit" Style="width: 200px" />
                                                                                <asp:Button ID="btnCancel" runat="server" CausesValidation="false" CssClass="btn"
                                                                                    OnClick="btnCancel_Click" OnClientClick="return funConfirm();" Text="Cancel" />
                                                                                 <asp:Button ID="btnNext" runat="server" CssClass="btn" Text="Take Action" 
                                                                                    Visible="false" CausesValidation="false" 
                                                                                    Style="width:100px" onclick="btnNext_Click" Font-Bold="True" />
                                                                          </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">
                                                                                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                    
                                                                    <table id="tableResult" runat="server" width="100%" border="0" cellpadding="1" cellspacing="0">
                                                                        <tr>
                                                                            <td>
                                                                                <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                                                                    <tr>
                                                                                        <td align="center">
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
                                                                                        <td align="center">
                                                                                            &nbsp;</td>
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