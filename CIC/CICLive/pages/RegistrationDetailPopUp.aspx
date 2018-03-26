<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegistrationDetailPopUp.aspx.cs"
    Inherits="pages_RegistrationDetailPopUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Crompton Greaves :: Customer Interaction Center</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/global.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
         .style1
         {
             width: 117px;
         }
         .style2
         {
             width: 17%;
         }
         .style3
         {
             width: 22%;
         }
     </style>
</head>
<body bgcolor="white">
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="updatepnl" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr bgcolor="white">
                        <td class="headingred">
                            Registration Details
                        </td>
                        <td align="right">
                            <a href="javascript:void(0)" class="links" onclick="window.close();">Close</a>
                        </td>
                    </tr>
                    <tr>
                        <td class="bgcolorcomm">
                            <table width="100%">
                                <tr>
                                    <td class="style2">
                                        <asp:CheckBox ID="chkEdit" AutoPostBack="true" Text="Edit" runat="server" 
                                            oncheckedchanged="chkEdit_CheckedChanged" /></td>
                                    <td style="width: 28%">
                                    </td>
                                    <td colspan="2" align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                        <font color='red'>*</font>
                                        <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <b>Customer Information:</b> &nbsp;<asp:HiddenField runat="server" ID="hdnCustomerId" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        Prefix:
                                    </td>
                                    <td >
                                        <asp:DropDownList ID="ddlPrefix" runat="server" CssClass="simpletxt1">
                                            <asp:ListItem Selected="True" Value="Mr">Mr</asp:ListItem>
                                            <asp:ListItem Value="Mrs">Mrs</asp:ListItem>
                                            <asp:ListItem Value="Dr">Dr</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" class="style1">Complaint Ref No:
                                    </td><td id="tdRefNo" runat="server"></td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        First Name:<font color="red">*</font>
                                    </td>
                                    <td style="width: 28%">
                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtboxtxt" Width="158px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFirstName"
                                            ErrorMessage="*" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </td>
                                    <td align="right" class="style1">
                                        Last Name:<font color="red">*</font>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="txtboxtxt" Width="158px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtLastName"
                                            ErrorMessage="*" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        Customer Type:
                                    </td>
                                    <td style="width: 28%">
                                        <asp:DropDownList ID="ddlCustomerType" runat="server" CssClass="simpletxt1">
                                            <asp:ListItem Selected="True" Value="D">Domestic</asp:ListItem>
                                            <asp:ListItem Value="I">Industrial</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style1">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        Company Name:
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox runat="server" MaxLength="150" Width="350Px" CssClass="txtboxtxt" ID="txtCompanyName"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        Address1:<font color='red'>*</font>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox runat="server" MaxLength="50" Width="350Px" CssClass="txtboxtxt" ID="txtAdd1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" SetFocusOnError="true"
                                            ControlToValidate="txtAdd1" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        Address2:
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtAdd2" runat="server" CssClass="txtboxtxt" MaxLength="50" Width="350px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        Landmark:
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtLandmark" runat="server" CssClass="txtboxtxt" MaxLength="150"
                                            Width="350px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" class="style2">
                                        State:
                                    </td>
                                    <td style="width: 28%" id="tdState" runat="server">
                                    </td>
                                    <td valign="top" align="right" class="style1">
                                        City:
                                    </td>
                                    <td valign="top" id="tdCity" runat="server">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        Pin Code:
                                    </td>
                                    <td colspan="3" id="tdPin" runat="server">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" height="24">
                                        <b>Contact Information:</b>
                                    </td>
                                </tr>
                               
                                <tr>
                                    <td class="style2">
                                        Contact No.:
                                    </td>
                                    <td colspan="3" id="tdContactNo" runat="server">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        Alternate Contact No.:
                                    </td>
                                    <td style="width: 28%">
                                        <asp:TextBox runat="server" MaxLength="11" ID="txtAltConatctNo" CssClass="txtboxtxt"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Valid No. only"
                                            ControlToValidate="txtAltConatctNo" SetFocusOnError="True" ValidationExpression="\d{10,11}"></asp:RegularExpressionValidator>
                                    </td>
                                    <td align="right" class="style1">
                                        Extension:
                                    </td>
                                    <td id="tdExt" runat="server">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        E-Mail:
                                    </td>
                                    <td  id="tdEmail" runat="server">
                                    </td>
                                    <td align="right" class="style1">
                                        Fax No.:
                                    </td>
                                    <td id="tdFax" runat="server">
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td class="style2">
                                        Appointment Req.:
                                    </td>
                                    <td style="width: 28%" id="tdAppointment" runat="server">
                                        <asp:CheckBox ID="chkAppointment" runat="server" />
                                    </td>
                                    <td class="style1">
                                    </td>
                                    <td>
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
                                    <td class="style2">
                                        Product Division:
                                    </td>
                                    <td  id="tdProdDiv" runat="server">
                                    </td>
                                    <td align="right" class="style3">Frames:
                                    </td>
                                    <td id="tdFrames" runat="server">
                                    </td>
                                </tr>
                               
                                <tr>
                                    <td class="style2">
                                        Product Line:
                                    </td>
                                    <td style="width: 21%" id="tdProdLine" runat="server">
                                    </td>
                                    <td align="right" class="style3"> Quantity:
                                    </td>
                                    <td id="tdQuantity" runat="server">
                                        &nbsp;
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td valign="top" class="style2">
                                        Complaint Details:
                                    </td>
                                    <td colspan="3" id="tdComplaintDetails" runat="server">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        Invoice Number:
                                    </td>
                                    <td style="width: 21%" id="tdInvoicedNum" runat="server">
                                    </td>
                                    <td class="style3" align="right">
                                        Purchase date:
                                    </td>
                                    <td id="tdPurchased" runat="server">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        Purchased from:
                                    </td>
                                    <td style="width: 21%" id="tdPurchasedFrom" runat="server">
                                    </td>
                                    <td colspan="2" align="center">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        &nbsp;
                                    </td>
                                    <td style="width: 21%">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn" OnClick="btnSubmit_Click"
                                            Text="Save" Width="70px" />
                                    </td>
                                    <td colspan="2">
                                        <asp:Button ID="btnCancel" runat="server" CausesValidation="false" CssClass="btn"
                                            OnClick="btnCancel_Click" Text="Close and Exit"
                                             />
                                        &nbsp;
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
