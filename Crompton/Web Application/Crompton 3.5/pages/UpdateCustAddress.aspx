<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateCustAddress.aspx.cs" Inherits="pages_UpdateCustAddress" Title="Update Address" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>View Uploaded Files</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/global.css" rel="stylesheet" type="text/css" />
</head>
<script type="text/javascript">
function refreshparent()
{ 
    alert("Customer Address Updated successfully.");
    var url = window.opener.location.href ;
    if( url.indexOf("ReturnId") != -1)
    window.opener.location.href =  url;
    else
    window.opener.location.href =  url +'?ReturnId=True'
    window.close();
}
</script>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div align="left">
    <asp:UpdatePanel runat="server" >
    <ContentTemplate>
    <table>
                                                    <tr>
                                                        <td width="120px">
                                                            &nbsp;</td>
                                                        <td colspan="3">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td width="120px">
                                                            &nbsp;</td>
                                                        <td colspan="3" class="headingred">
                                                            Update Customer Address</td>
                                                    </tr>
                                                    <tr>
                                                        <td width="120px">
                                                            &nbsp;</td>
                                                        <td colspan="3">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td width="120px">
                                                            Prefix
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:DropDownList ID="ddlPrefix" runat="server" CssClass="simpletxt1" 
                                                                Enabled="False">
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
                                                                MaxLength="30" ReadOnly="True"></asp:TextBox>
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
                                                            <asp:TextBox ID="txtLastName" runat="server" CssClass="txtboxtxt" Width="158px" 
                                                                MaxLength="20" ReadOnly="True"></asp:TextBox>
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
                                                        <td>
                                                           
                                                        </td>
                                                        <td>
                                                            
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
                                                            <asp:DropDownList Width="170px" ID="ddlCity" runat="server" CssClass="simpletxt1" >
                                                                <asp:ListItem Text="Select" Value="Select" Selected="True"></asp:ListItem>
                                                            </asp:DropDownList>
                                                             &nbsp;<asp:CompareValidator SetFocusOnError="true" ID="CompareValidator2" runat="server"
                                                                ControlToValidate="ddlCity" ErrorMessage="City is required." Operator="NotEqual"
                                                                ValueToCompare="Select" Display="None"></asp:CompareValidator>
                                                        </td>
                                                    </tr>
                                                       <%-- <tr>
                                                        <td>
                                                            Territory
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:DropDownList Width="175px" ID="ddlSC" runat="server" CssClass="simpletxt1" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlSC_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            
                                                        
                                                       
                                                    </tr>--%>
                                                    <tr>
                                                        <td>
                                                            Pin Code
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtPinCode" EnableViewState="true" onkeypress="javascript:return checkNumberOnly(event);"
                                                             runat="server" CssClass="txtboxtxt" MaxLength="6"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Valid pin is required."
                                                                ControlToValidate="txtPinCode" SetFocusOnError="True" ValidationExpression="\d{6}"
                                                                Display="None"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                         <tr>
                                                        <td>
                                                            Contact No.
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtContactNo" MaxLength="11" runat="server" CssClass="txtboxtxt" ReadOnly="True"></asp:TextBox>
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
                                                        <td valign="top">
                                                            &nbsp;</td>
                                                        <td style="width: 24%">
                                                            <asp:Button ID="BtnUpdateAddress" runat="server" Text="Update Address" 
                                                                class="btn" onclick="BtnUpdateAddress_Click" />
                                                        </td>
                                                        <td style="width: 8%" valign="top" align="right">
                                                            &nbsp;</td>
                                                        <td valign="top">
                                                            &nbsp;</td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td colspan="4" height="24" align="center">
                                                           
                                                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label>
                                                           
                                                        </td>
                                                    </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
  </div>
    </form>
</body>
</html>

