<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true"
    CodeFile="SpareStockTransferASC.aspx.cs" Inherits="SIMS_Pages_SpareStockTransferASC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%" cellpadding="2" cellspacing="0">
                <tr>
                    <td class="headingred">
                        ASC Spare Stock Transfer
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["SIMSAjaxLoadingImageName"]%>" alt="" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" colspan="2">
                        <table width="100%" border="0">
                            <tr>
                                <td width="100%" align="left" class="bgcolorcomm">
                                    <table border="0" style="width: 100%">
                                        <tr>
                                            <td colspan="4" align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                                <font color='red'>*</font>
                                                <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="right">
                                                Date:
                                                <asp:Label ID="lbldate" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="13%">
                                                Service Contactor:
                                            </td>
                                            <td>
                                                <%--<asp:DropDownList ID="ddlASCCode" runat="server" CssClass="simpletxt1" Width="175px"
                                            ValidationGroup="editt" AutoPostBack="True" 
                                            onselectedindexchanged="ddlASCCode_SelectedIndexChanged" >
                                        </asp:DropDownList>
                                        <br /><asp:RequiredFieldValidator ID="RFRegionDesc" runat="server" ControlToValidate="ddlASCCode"
                                            ErrorMessage="Service Contactor is required." InitialValue="Select" SetFocusOnError="true"
                                            ValidationGroup="editt" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                                <asp:Label ID="lblASCName" runat="server"></asp:Label>
                                                <asp:HiddenField ID="hdnASC_Code" runat="server" />
                                            </td>
                                            <td width="13%">
                                                Product Division:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlProductDivision" runat="server" CssClass="simpletxt1" ValidationGroup="editt"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlProductDivision_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <br />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlProductDivision"
                                                    ErrorMessage="Product Division is required." InitialValue="Select" SetFocusOnError="true"
                                                    ValidationGroup="editt" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Spare:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlSpare" runat="server" CssClass="simpletxt1" ValidationGroup="editt" Width="130px"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlSpare_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSpare"
                                                    ErrorMessage="Spare is required." InitialValue="Select" SetFocusOnError="true"
                                                    ValidationGroup="editt" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                           
                                            <td>
                                                Search Spares: 
                                            </td>
                                            <td>
                                               <asp:TextBox ID="txtFindSpare" ValidationGroup="ProductRef" CssClass="txtboxtxt" runat="server"
                                                    Width="130" CausesValidation="True"></asp:TextBox> 
                                                    <asp:Button ID="btnGoSpare" runat="server" CssClass="btn" 
                                                    OnClick="btnGoSpare_Click" Text="Go" ValidationGroup="ProductRef" 
                                                    Width="20px" />
                                            </td>
                                        </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Transaction No:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblStockTransferDocumentNo" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <table width="100%">
                                                        <tr>
                                                            <td bgcolor="#CCCCCC" height="22">
                                                                <b>From Location</b>
                                                            </td>
                                                            <td bgcolor="#CCCCCC">
                                                                <b>Available Qty.</b>
                                                            </td>
                                                            <td bgcolor="#CCCCCC">
                                                                <b>Service Enginer Name</b>
                                                            </td>
                                                            <td bgcolor="#CCCCCC">
                                                                <b>To Location</b>
                                                            </td>
                                                            <td bgcolor="#CCCCCC">
                                                                <b>Service Enginer Name</b>
                                                            </td>
                                                            <td bgcolor="#CCCCCC">
                                                                <b>Qty. Issued</b>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">
                                                            <asp:DropDownList ID="ddlFromLocation" runat="server" CssClass="simpletxt1" Width="175px"
                                                                ValidationGroup="editt" AutoPostBack="True" OnSelectedIndexChanged="ddlFromLocation_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                                    ControlToValidate="ddlFromLocation" Display="Dynamic" 
                                                                    ErrorMessage="From Location is required." InitialValue="Select" 
                                                                    SetFocusOnError="true" ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td valign="top">
                                                                <asp:TextBox ID="lblAvailableQty" runat="server" BorderStyle="None" 
                                                                    CssClass="txtboxtxt" ReadOnly="true" Width="91px"></asp:TextBox>
                                                            </td>
                                                            <td valign="top">
                                                                <asp:Label ID="lblFromServiceEnginer" runat="server"></asp:Label>
                                                            </td>
                                                            <td valign="top">
                                                                <asp:DropDownList ID="ddlToLocation" runat="server" AutoPostBack="True" 
                                                                    CssClass="simpletxt1" 
                                                                    OnSelectedIndexChanged="ddlToLocation_SelectedIndexChanged" 
                                                                    ValidationGroup="editt" Width="175px">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                                    ControlToValidate="ddlToLocation" Display="Dynamic" 
                                                                    ErrorMessage="To Location is required." InitialValue="Select" 
                                                                    SetFocusOnError="true" ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td valign="top">
                                                                <asp:Label ID="lblToServiceEnginer" runat="server"></asp:Label>
                                                            </td>
                                                            <td valign="top">
                                                                <asp:TextBox ID="txtTransferedQty" runat="server" CssClass="txtboxtxt" 
                                                                    Width="75px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                                                    ControlToValidate="txtTransferedQty" Display="Dynamic" 
                                                                    ErrorMessage="Quantity Issued is required." SetFocusOnError="true" 
                                                                    ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                                                    ControlToValidate="txtTransferedQty" Display="Dynamic" 
                                                                    ErrorMessage="Proper Quantity Issued is required." ValidationExpression="^\d*$" 
                                                                    ValidationGroup="editt"></asp:RegularExpressionValidator>
                                                                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                                                    ControlToCompare="lblAvailableQty" ControlToValidate="txtTransferedQty" 
                                                                    ErrorMessage="Quantity Issued should be less than or equal to Available Quantity." 
                                                                    Operator="LessThanEqual" SetFocusOnError="True" Type="Integer" 
                                                                    ValidationGroup="editt"></asp:CompareValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="4">
                                                    <!-- For button portion update -->
                                                    <table>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Button ID="imgBtnAdd" runat="server" CausesValidation="True" 
                                                                    CssClass="btn" OnClick="imgBtnAdd_Click" Text="Confirm" ValidationGroup="editt" 
                                                                    Width="70px" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="imgBtnCancel" runat="server" CausesValidation="false" 
                                                                    CssClass="btn" OnClick="imgBtnCancel_Click" Text="Cancel" Width="70px" />
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
</asp:Content>
