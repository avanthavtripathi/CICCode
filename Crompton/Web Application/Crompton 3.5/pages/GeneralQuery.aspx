<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" 
CodeFile="GeneralQuery.aspx.cs" Inherits="pages_GeneralQuery" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">

 <table width="100%">
        <tr>
            <td class="headingred">
                General Query
            </td>
            <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right:10px;">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </td>
        </tr>
        <tr>
            <td class="bgcolorcomm" colspan="2">
                <asp:UpdatePanel ID="pnl" runat="server" UpdateMode="Conditional">
                   <%-- <Triggers>
                        <asp:PostBackTrigger ControlID="btnSubmit" />
                    </Triggers>--%>
                    <ContentTemplate>
                        <asp:Panel ID="panMain" runat="server">
                         <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                <tr>
                                    <td style="width: 24%">
                                        &nbsp;
                                    </td>
                                    <td style="width: 28%">
                                    </td>
                                    <td colspan="2" align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                        <font color='red'>*</font>
                                        <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <b>Customer Information:</b> &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 24%">
                                        Prefix
                                    </td>
                                    <td colspan="3">
                                        <asp:DropDownList ID="ddlPrefix" runat="server" CssClass="simpletxt1">
                                            <asp:ListItem Selected="True" Value="Mr">Mr</asp:ListItem>
                                            <asp:ListItem Value="Mrs">Mrs</asp:ListItem>
                                            <asp:ListItem Value="Dr">Dr</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 24%">
                                        First Name<font color="red">*</font>
                                    </td>
                                    <td style="width: 28%">
                                        <asp:TextBox ID="txtFirstName" ValidationGroup="serviceRegistration" runat="server" CssClass="txtboxtxt" Width="158px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ValidationGroup="serviceRegistration" ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFirstName"
                                            ErrorMessage="*" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </td>
                                    <td align="right" style="width: 8%">
                                        Last Name<font color="red">*</font>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLastName"  ValidationGroup="serviceRegistration" runat="server" CssClass="txtboxtxt" Width="158px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ValidationGroup="serviceRegistration" ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtLastName"
                                            ErrorMessage="*" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 24%" valign="top">
                                        State<font color='red'>*</font>
                                    </td>
                                    <td style="width: 28%">
                                        <asp:DropDownList ValidationGroup="serviceRegistration" Width="170px" runat="server" CssClass="simpletxt1" ID="ddlState"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        
                                        <asp:CompareValidator ValidationGroup="serviceRegistration" SetFocusOnError="true" ID="CompareValidator1" runat="server"
                                            ControlToValidate="ddlState" ErrorMessage="State is required." Operator="NotEqual"
                                            ValueToCompare="Select">*</asp:CompareValidator>
                                    </td>
                                    <td style="width: 8%" valign="top" align="right">
                                        City<font color='red'>*</font>
                                    </td>
                                    <td valign="top">
                                        <asp:DropDownList ValidationGroup="serviceRegistration" Width="170px" ID="ddlCity" runat="server" CssClass="simpletxt1">
                                            <%--OnSelectedIndexChanged="ddlCityCC_SelectedIndexChanged">--%>
                                            <asp:ListItem Text="Select" Value="Select" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:CompareValidator ValidationGroup="serviceRegistration" SetFocusOnError="true" ID="CompareValidator2" runat="server"
                                            ControlToValidate="ddlCity" ErrorMessage="City is required." Operator="NotEqual"
                                            ValueToCompare="Select">*</asp:CompareValidator>
                                    </td>
                                </tr>
                                
                                 <tr>
                                    <td style="width: 24%">
                                        Pin Code
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ValidationGroup="serviceRegistration" ID="txtPinCode" runat="server" CssClass="txtboxtxt" MaxLength="6"></asp:TextBox>
                                        <asp:RegularExpressionValidator ValidationGroup="serviceRegistration" ID="RegularExpressionValidator5" runat="server" ErrorMessage="Valid pin only"
                                            ControlToValidate="txtPinCode" SetFocusOnError="True" ValidationExpression="\d{6}"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" height="24">
                                        <b>Contact Information:</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 24%">
                                        Contact No.<font color='red'>*</font>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ValidationGroup="serviceRegistration" ID="txtContactNo" MaxLength="12" runat="server" CssClass="txtboxtxt"></asp:TextBox><asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator1" SetFocusOnError="true" ControlToValidate="txtContactNo" ValidationGroup="serviceRegistration"
                                            runat="server" ErrorMessage="*"> *</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ValidationGroup="serviceRegistration" ID="RegularExpressionValidator3" runat="server" ErrorMessage="Number only"
                                            ControlToValidate="txtContactNo" SetFocusOnError="True" ValidationExpression="\d{1,12}"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 24%">
                                        Alternate Contact No.
                                    </td>
                                    <td style="width: 28%">
                                        <asp:TextBox runat="server" ValidationGroup="serviceRegistration" MaxLength="15" ID="txtAltConatctNo" CssClass="txtboxtxt"></asp:TextBox>
                                        <asp:RegularExpressionValidator ValidationGroup="serviceRegistration" ID="RegularExpressionValidator2" runat="server" ErrorMessage="Number only"
                                            ControlToValidate="txtAltConatctNo" SetFocusOnError="True" ValidationExpression="\d{1,12}"></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 8%" align="right">
                                        Extension
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtExtension" ValidationGroup="serviceRegistration" MaxLength="5" runat="server" CssClass="txtboxtxt"></asp:TextBox>
                                        <asp:CompareValidator ValidationGroup="serviceRegistration" ID="CompareValidator5" runat="server" ErrorMessage="Number only."
                                            Operator="DataTypeCheck" ControlToValidate="txtExtension" Type="Integer"></asp:CompareValidator>
                                    </td>
                                </tr>
                                
                                 <tr>
                                    <td style="width: 24%">
                                        Query Type<font color='red'>*</font>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlQueryType" runat="server" CssClass="simpletxt1" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlQueryType_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="For Fans">For Fans</asp:ListItem>
                                            <asp:ListItem Value="For Pumps">For Pumps</asp:ListItem>
                                            <asp:ListItem Value="For Lighting">For Lighting</asp:ListItem>
                                            <asp:ListItem Value="Other">Other</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:CompareValidator ValidationGroup="serviceRegistration" SetFocusOnError="true" ID="CompareValidator3" runat="server"
                                            ControlToValidate="ddlQueryType" Operator="NotEqual"
                                            ValueToCompare="0">*</asp:CompareValidator>
                                    </td>
                                    <div id="divOther" runat="server" visible="false">
                                        <td style="width: 8%" align="right">
                                            Other<font color='red'>*</font>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOther" ValidationGroup="serviceRegistration" MaxLength="50" runat="server" 
                                                Width="170px" CssClass="txtboxtxt" TextMode="MultiLine" Height="20"></asp:TextBox>
                                             <asp:RequiredFieldValidator ValidationGroup="serviceRegistration" ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtOther"
                                                ErrorMessage="*" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            
                                        </td>
                                    </div>
                                </tr>
                                <tr>
                                    <td style="width: 24%">
                                        Action Take
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtActionTake" ValidationGroup="serviceRegistration" MaxLength="50" runat="server" 
                                            Width="170px" CssClass="txtboxtxt" TextMode="MultiLine" Height="20" onkeydown="return CheckMaxLength(this,10,event);"></asp:TextBox>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 24%">
                                        Remarks
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRemarks" ValidationGroup="serviceRegistration" MaxLength="50" runat="server" 
                                            Width="170px" CssClass="txtboxtxt" TextMode="MultiLine" Height="30" onkeydown="return CheckMaxLength(this,500,event);"></asp:TextBox>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                            <asp:Button ID="btnSubmit" ValidationGroup="serviceRegistration" runat="server" CssClass="btn" 
                                             OnClick="btnSubmit_Click" Text="Submit" Width="70px" />
                                               
                                    </td>
                                </tr>
                                 <tr>
                                    <td colspan="4" align="center">
                                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
        
</asp:Content>

