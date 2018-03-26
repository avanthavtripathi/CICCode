<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true"
    CodeFile="SalesOrderReceipts.aspx.cs" Inherits="SIMS_Pages_SalesOrderReceipts" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script language="javascript" type="text/javascript">
                function funUploadPopUp(PoNo)
                {
                    var strUrl='SalesOrderHistoryLog.aspx?pono='+ PoNo;
                     custWin=   window.open(strUrl,'SCPopup','height=650,width=850,left=20,top=30,scrollbars=1');
                        if (window.focus) {custWin.focus()}
                 }  
    </script>

    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%" cellpadding="2" cellspacing="0">
                <tr>
                    <td class="headingred" style="width: 968px">
                        Spare Order Receipts ASC
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
                            <tr id="tdSparereciept" runat="server">
                                <td width="100%" align="left" class="bgcolorcomm">
                                    <table width="98%" border="0">
                                        <tr>
                                            <td colspan="4" align="right">
                                                <font color='red'>*</font>Mandatory Fields
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 22%">
                                                Division:
                                            </td>
                                            <td style="width: 62%">
                                                <asp:Label ID="lblDivision" runat="server"></asp:Label>
                                            </td>
                                            <td width="25%">
                                                &nbsp;
                                            </td>
                                            <td width="25%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30%">
                                                CG Sales Order Number:<font color='red'>*</font>
                                            </td>
                                            <td style="width: 62%">
                                                <asp:TextBox ID="txtSONumber" CssClass="txtboxtxt" Width="170px" MaxLength="20" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" ID="ReqSONumber" runat="server"
                                                    ControlToValidate="txtSONumber" ErrorMessage="Sales Order Number is required."
                                                    Display="Dynamic" ToolTip="Sales Order Number is required." ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hdnIspart" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 22%">
                                                CG Invoice Number:<font color='red'>*</font>
                                            </td>
                                            <td style="width: 62%">
                                                <asp:TextBox ID="txtInvoiceNumber" CssClass="txtboxtxt" Width="170px" MaxLength="20"
                                                    runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator2" runat="server"
                                                    ControlToValidate="txtInvoiceNumber" ErrorMessage="Invoice Number is required."
                                                    Display="Dynamic" ToolTip="Invoice Number is required." ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hdnDivision_Id" runat="server" />
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hdnTransNo" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 22%">
                                                CG Invoice Date:<font color='red'>*</font>
                                            </td>
                                            <td style="width: 62%">
                                                <asp:TextBox MaxLength="10" runat="server" ID="txtInvoiceDate" CssClass="txtboxtxt"
                                                    Width="170px" />&nbsp;(mm/dd/yyyy)
                                                <cc1:CalendarExtender ID="CalInvoiceDate" runat="server" TargetControlID="txtInvoiceDate">
                                                </cc1:CalendarExtender>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator3" runat="server"
                                                    ControlToValidate="txtInvoiceDate" ErrorMessage="Invoice Date is required." Display="Dynamic"
                                                    ToolTip="Invoice Date is required." ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hdnASC_Code" runat="server" />
                                                <asp:HiddenField ID="hdnAllPONumbers" runat="server" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 22%">
                                                Challan Number of CG:<font color='red'>*</font>
                                            </td>
                                            <td style="width: 62%">
                                                <asp:TextBox MaxLength="20" runat="server" ID="txtChallanNo" CssClass="txtboxtxt"
                                                    Width="170px" />
                                                <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator4" runat="server"
                                                    ControlToValidate="txtInvoiceDate" ErrorMessage="Challan Number is required."
                                                    Display="Dynamic" ToolTip="Challan Number is required." ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hdnIndentNo" runat="server" />
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 22%">
                                                Challan Date of CG:<font color='red'>*</font>
                                            </td>
                                            <td style="width: 62%">
                                                <asp:TextBox MaxLength="10" runat="server" ID="txtChallanDate" CssClass="txtboxtxt"
                                                    Width="170px" />&nbsp;(mm/dd/yyyy)
                                                <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator5" runat="server"
                                                    ControlToValidate="txtChallanDate" ErrorMessage="Challan Date is required." Display="Dynamic"
                                                    ToolTip="Challan Date is required." ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                <cc1:CalendarExtender ID="CalChallanDate" runat="server" TargetControlID="txtChallanDate">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblRowCount" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <table width="100%" border="0" cellspacing="1" cellpadding="2">
                                                    <tr>
                                                        <td>
                                                            <asp:GridView RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                                                HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" PageSize="20" AllowPaging="True"
                                                                AllowSorting="True" DataKeyNames="SIMS_Indent_No" AutoGenerateColumns="False"
                                                                ID="gvComm" runat="server" OnPageIndexChanging="gvComm_PageIndexChanging" Width="100%"
                                                                HorizontalAlign="Left" OnSorting="gvComm_Sorting" OnRowDataBound="gvComm_RowDataBound">
                                                                <RowStyle CssClass="gridbgcolor" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Spare Code & Desc">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSpareCode" runat="server" Text='<%# Bind("Spare_Name") %>'></asp:Label>
                                                                            <asp:HiddenField ID="hdnProductDivId" runat="server" Value='<%# Bind("ProductDivision_id") %>' />
                                                                            <asp:HiddenField ID="hndSpareId" runat="server" Value='<%# Bind("Spare_id") %>' />
                                                                            <asp:HiddenField ID="hndIsSAPData" runat="server" Value='<%# Bind("IsSAPData") %>' />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="System Indent Number">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblIndentNumber" runat="server" Text='<%# Bind("SIMS_Indent_No") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="PO Number">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPONumber" runat="server" Text='<%# Bind("PO_Number") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                   <asp:TemplateField HeaderText="Stage" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <a href="Javascript:void(0);" onclick="funUploadPopUp('<%#Eval("PO_Number")%>')">
                                                                                <%#Eval("Stage")%>
                                                                            </a>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Qty Ordered">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblOrdered" runat="server" Text='<%# Bind("Ordered_Qty") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Qty Pending">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPendingQty" runat="server" Text='<%# Bind("Pending_Qty") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Qty Challan">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtChallanQty" Text='<%# Bind("Challan_Qty") %>' runat="server"
                                                                                MaxLength="9" CssClass="txtboxtxt"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilChallanQty" runat="server" FilterType="Custom"
                                                                                TargetControlID="txtChallanQty" ValidChars="0123456789.">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                            <asp:RequiredFieldValidator SetFocusOnError="true" ID="ReqChallanQty" runat="server"
                                                                                ControlToValidate="txtChallanQty" ErrorMessage="Challan Quantity is required."
                                                                                Display="Dynamic" ToolTip="Challan Quantity is required." ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Qty Received">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtReceivedQty" AutoPostBack="true" OnTextChanged="txtReceivedQty_TextChanged"
                                                                                MaxLength="9" runat="server" CssClass="txtboxtxt" Width="70px"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilReceived" runat="server" FilterType="Custom"
                                                                                TargetControlID="txtReceivedQty" ValidChars="0123456789.">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                            <asp:RequiredFieldValidator SetFocusOnError="true" ID="ReqReceivedQty" runat="server"
                                                                                ControlToValidate="txtReceivedQty" ErrorMessage="Qty. Received is required."
                                                                                Display="Dynamic" ToolTip="Qty. Received is required." ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                                            <asp:Label ID="lblMsgReceived" runat="server" Text=""></asp:Label>
                                                                            <asp:CompareValidator ID="cv1111" runat="server" ControlToCompare="txtChallanQty"
                                                                                ControlToValidate="txtReceivedQty" Operator="LessThanEqual" Type="Integer" ValidationGroup="editt"
                                                                                Display="Dynamic" ErrorMessage="Recieved Qty should not be greater than Qty Challan." />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Short Reciept">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtShortfall" Text='' runat="server" CssClass="txtboxtxt" Width="90px"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Damaged">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtDeffective" runat="server" CssClass="txtboxtxt" MaxLength="5"
                                                                                Width="70px"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilDeffective" runat="server" FilterType="Custom"
                                                                                TargetControlID="txtDeffective" ValidChars="0123456789.">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                            <asp:CompareValidator ID="cv1" runat="server" ControlToCompare="txtReceivedQty" ControlToValidate="txtDeffective"
                                                                                Operator="LessThanEqual" Type="Integer" ValidationGroup="editt" Display="Dynamic"
                                                                                ErrorMessage="Damaged Qty should not be greater than Qty Recieved." />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                                                <img src="<%=ConfigurationManager.AppSettings["SIMSUserMessage"]%>" alt="" />
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
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="25" colspan="4" align="center">
                                                <table>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Button Text="Received" Width="70px" ID="imgBtnAdd" CssClass="btn" runat="server"
                                                                CausesValidation="True" ValidationGroup="editt" OnClick="imgBtnAdd_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="imgBtnCancel" Width="70px" runat="server" CausesValidation="false"
                                                                CssClass="btn" Text="Cancel" OnClick="imgBtnCancel_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                                    <asp:HiddenField ID="hndSpare_Id" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
