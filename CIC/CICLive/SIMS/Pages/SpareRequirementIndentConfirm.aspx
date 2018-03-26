<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true" CodeFile="SpareRequirementIndentConfirm.aspx.cs" Inherits="SIMS_Pages_SpareRequirementIndentConfirm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
<asp:UpdatePanel ID="updatepnl" runat="server">
<ContentTemplate>
    <script language="javascript" type="text/javascript">
        function  funOpenDeliverySchedule(transactionno)
        {
            var strUrl='DeliverySchedule.aspx?transactionno='+ transactionno;
            window.open(strUrl,'Reprint','height=550,width=1000,left=20,top=150,resizable=no,location=0,scrollbars=1');
        }
        function SearchPostBack()
        {
            var btn = document.getElementById('ctl00_MainConHolder_btnSearch');
            if (btn) btn.click();
        }
    </script>
    <table width="100%" cellpadding="2" cellspacing="0">
        <tr>
            <td class="headingred">
                <h3>Spare Requirement Indent Generation Screen</h3>
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
                                    <td colspan="2" align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                        <font color='red'>*</font>
                                        <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="right">Date: <asp:Label ID="lbldate" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table cellpadding="2" cellspacing="3">
                                            <tr>
                                                <td>ASC Name:</td>
                                                <td>
                                                    <asp:Label ID="lblASCName" runat="server"></asp:Label>
                                                    <asp:HiddenField ID="hdnASC_Code" runat="server" />
                                                    <asp:HiddenField ID="hdnDraft_No" runat="server" />
                                                </td>                                    
                                            </tr>
                                            <%--<tr>
                                                <td>To CG Branch:</td>
                                                <td><asp:Label ID="lblCGBranch" runat="server"></asp:Label></td>   
                                            </tr>--%>
                                            <tr>
                                                <td>Division:</td>
                                                <td>
                                                    <asp:Label ID="lblProductDivision" runat="server"></asp:Label>
                                                    <asp:HiddenField ID="hdnProductDivId" runat="server" />
                                                </td>  
                                            </tr>                                
                                            <tr>
                                                <td>Branch Plant:</td>
                                                <td><asp:Label ID="lblBranchPlant" runat="server"></asp:Label></td>   
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                  <td colspan="2">&nbsp;</td>
                                </tr>
                                <tr>
                                  <td colspan="2">
                                    <asp:GridView ID="gvComm" runat="server" AllowPaging="false" AlternatingRowStyle-CssClass="fieldName"
                                        AutoGenerateColumns="False" DataKeyNames="Transaction_No" GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor"
                                        HorizontalAlign="Left" RowStyle-CssClass="gridbgcolor" Width="100%" 
                                          onrowdatabound="gvComm_RowDataBound" >
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                <HeaderStyle Width="40px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Spare_Name" SortExpression="Spare_Name" HeaderStyle-HorizontalAlign="Left"
                                                 HeaderText="Spare" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left"  />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Proposed_Qty" SortExpression="Proposed_Qty" HeaderStyle-HorizontalAlign="Left"
                                                 HeaderText="Proposed Qty" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left"  />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Ordered Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtOrderedQty" CssClass="txtboxtxt"  
                                                        Text='<%# DataBinder.Eval(Container.DataItem,"Ordered_Qty") %>' runat="server" 
                                                        AutoPostBack="True" ontextchanged="txtOrderedQty_TextChanged" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RFStateDesc" runat="server" SetFocusOnError="true"
                                                        ErrorMessage="Ordered qty is required." ControlToValidate="txtOrderedQty" ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                        <asp:RangeValidator Display="Dynamic" ID="RangeValidator3" SetFocusOnError="true" ControlToValidate="txtOrderedQty" MinimumValue="1" MaximumValue="64550" Type="Integer" ValidationGroup="editt" runat="server" 
                                                        ErrorMessage="Numeric value is required."></asp:RangeValidator>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Rate" SortExpression="Rate" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Rate" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Discount" SortExpression="Discount" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Discount" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Value" SortExpression="Value" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Value" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Spare_Id" SortExpression="Spare_Id" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Spare Id" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Transaction_No" SortExpression="Transaction_No" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Transaction No" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <%--<asp:TemplateField HeaderText="Complete Delivery Date">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtCompDelDate" CssClass="txtboxtxt"
                                                        ValidationGroup="editt" />
                                                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtCompDelDate">
                                                    </cc1:CalendarExtender> <asp:RequiredFieldValidator ControlToValidate="txtCompDelDate" ID="RequiredFieldValidator1"
                                                        runat="server" ErrorMessage="Complete Delivery date is Required" ValidationGroup="editt"
                                                        Display="Dynamic">
                                                    </asp:RequiredFieldValidator> <asp:CompareValidator Operator="DataTypeCheck" Type="Date" ControlToValidate="txtCompDelDate"
                                                        ErrorMessage="<br>Not a vaild Date" runat="server" Display="Dynamic" ID="CompareValidator3"
                                                        ValidationGroup="editt"></asp:CompareValidator>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                                            </asp:TemplateField>           
                                            <asp:TemplateField HeaderText="Part Delivery Schedule">
                                                <ItemTemplate>
                                                    <a href="javascript:void(0);" onclick="funOpenDeliverySchedule('<%# DataBinder.Eval(Container.DataItem,"Transaction_No") %>');" class="fpassword">Delivery Schedule</a>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                                            </asp:TemplateField>--%>
                                        </Columns>
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
                                <tr>
                                    <td align="left" class="MsgTDCount">
                                        Total Number of Records : <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                    </td>
                                     <td align="right" class="MsgTDCount" >
                                        Total Amount : <asp:Label ID="lblTotalAmount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                  <td colspan="2">&nbsp;</td>
                                </tr>
                                <tr>
                                  <td colspan="2">
                                    <asp:GridView ID="gvPDS" runat="server" AllowPaging="false" AlternatingRowStyle-CssClass="fieldName"
                                        AutoGenerateColumns="False" DataKeyNames="Part_Delivery_Id" 
                                          GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor"
                                        HorizontalAlign="Left"  RowStyle-CssClass="gridbgcolor" Width="100%" >
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <asp:BoundField DataField="Spare_Name" SortExpression="Spare_Name" HeaderStyle-HorizontalAlign="Left"
                                                 HeaderText="Spare" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left"  />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Part_Delivery_Date" SortExpression="Part_Delivery_Date" HeaderStyle-HorizontalAlign="Left"
                                                 HeaderText="Delivery Date" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left"  />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Quantity" SortExpression="Quantity" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Quantity" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>                                          
                                        </Columns>
                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                        <AlternatingRowStyle CssClass="fieldName" />
                                    </asp:GridView>
                                  </td>
                                </tr>
                                <tr>
                                  <td colspan="2">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table width="100%" >
                                           <%--<tr>
                                                <td>Complete Delivery Date:</td>
                                                <td valign="top">
                                                    <asp:TextBox runat="server" ID="txtCompDelDate" CssClass="txtboxtxt"
                                                        ValidationGroup="editt" />
                                                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtCompDelDate">
                                                    </cc1:CalendarExtender> <asp:RequiredFieldValidator ControlToValidate="txtFromDate" ID="RequiredFieldValidator1"
                                                        runat="server" ErrorMessage="Complete Delivery date is Required" ValidationGroup="editt"
                                                        Display="Dynamic">
                                                    </asp:RequiredFieldValidator> <asp:CompareValidator Operator="DataTypeCheck" Type="Date" ControlToValidate="txtCompDelDate"
                                                        ErrorMessage="<br>Not a vaild Date" runat="server" Display="Dynamic" ID="CompareValidator3"
                                                        ValidationGroup="editt"></asp:CompareValidator>
                                                </td>
                                                <td>Part Delivery Date:</td>
                                                <td valign="top">
                                                    <asp:TextBox runat="server" ID="txtPartDelDate" CssClass="txtboxtxt" 
                                                        ValidationGroup="editt" />
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPartDelDate">
                                                    </cc1:CalendarExtender> <asp:RequiredFieldValidator ControlToValidate="txtFromDate" ID="RequiredFieldValidator3"
                                                        runat="server" ErrorMessage="Part Delivery date is Required" ValidationGroup="editt"
                                                        Display="Dynamic">
                                                    </asp:RequiredFieldValidator> <asp:CompareValidator Operator="DataTypeCheck" Type="Date" ControlToValidate="txtPartDelDate"
                                                        ErrorMessage="<br>Not a vaild Date" runat="server" Display="Dynamic" ID="CompareValidator1"
                                                        ValidationGroup="editt"></asp:CompareValidator>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td>Sales Order Type:<font color='red'>*</font></td>
                                                <td valign="top">
                                                    <asp:DropDownList ID="ddlSalesOrderType" runat="server" CssClass="simpletxt1" Width="175px"
                                                        ValidationGroup="editt"  >
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSalesOrderType"
                                                        ErrorMessage="Sales Order Type is required." InitialValue="0" SetFocusOnError="true"
                                                        ValidationGroup="editt" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>Tax Form Type:<font color='red'>*</font></td>
                                                <td valign="top">
                                                    <asp:DropDownList ID="ddlTaxFormType" runat="server" CssClass="simpletxt1" Width="175px"
                                                        ValidationGroup="editt"  >
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlTaxFormType"
                                                        ErrorMessage="Tax Form Type is required." InitialValue="0" SetFocusOnError="true"
                                                        ValidationGroup="editt" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">ASC Ref/PO Number<font color='red'>*</font></td>
                                                <td valign="top">
                                                        <asp:TextBox ID="txtPONumber" Width="170px" runat="server" CssClass="txtboxtxt" ></asp:TextBox>
                                                        <asp:RequiredFieldValidator Display="Dynamic" ID="RFStateDesc" runat="server" SetFocusOnError="true"
                                                            ErrorMessage="PO Number is required." ControlToValidate="txtPONumber" ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                        <%--<asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtPONumber" ID="RegularExpressionValidator1" ValidationGroup="editt" runat="server" 
                                                            ErrorMessage="Proper Proposed qty is required."  ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
                                                        <asp:RangeValidator Display="Dynamic" ID="RangeValidator3" ControlToValidate="txtPONumber" MinimumValue="1" MaximumValue="64550" Type="Integer" ValidationGroup="editt" runat="server" 
                                                        ErrorMessage="Proper Proposed qty is required."></asp:RangeValidator>--%>
                                                </td>
                                                <td>Inco Terms:<font color='red'>*</font></td>
                                                <td valign="top">
                                                    <asp:DropDownList ID="ddlIncoTerms" runat="server" CssClass="simpletxt1" Width="175px"
                                                        ValidationGroup="editt" >
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlIncoTerms"
                                                        ErrorMessage="Inco Terms is required." InitialValue="0" SetFocusOnError="true"
                                                        ValidationGroup="editt" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Consigned To:<font color='red'>*</font></td>
                                                <td valign="top">
                                                    <asp:DropDownList ID="ddlConsignedTo" runat="server" CssClass="simpletxt1" Width="175px"
                                                        ValidationGroup="editt" AutoPostBack="true"  OnSelectedIndexChanged="ddlConsignedTo_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True" Value="Select">Select</asp:ListItem>
                                                        <asp:ListItem Value="Door">Door</asp:ListItem>
                                                        <asp:ListItem Value="Self_At">Self At</asp:ListItem>
                                                        <asp:ListItem Value="Consigned_Copy">Consigned Copy</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlConsignedTo"
                                                        ErrorMessage="Consigned To is required." InitialValue="Select" SetFocusOnError="true"
                                                        ValidationGroup="editt" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>  
                                                <td valign="top" style="display:none;">Name<font color='red'>*</font></td>
                                                <td valign="top" style="display:none;">
                                                    <asp:TextBox ID="txtName" runat="server" Width="170px"  CssClass="txtboxtxt" ></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ControlToValidate="txtName"  Display="Dynamic" ID="RequiredFieldValidator7" runat="server" SetFocusOnError="true"
                                                        ErrorMessage="Name is required." ValidationGroup="editt"></asp:RequiredFieldValidator>--%>
                                                </td>                                              
                                            </tr>
                                            <tr>                                                
                                                <td valign="top">Address Line1<font color='red'>*</font></td>
                                                <td valign="top">
                                                    <asp:TextBox ID="txtAddressLine1" MaxLength="500" runat="server" CssClass="txtboxtxt" 
                                                        Width="170px" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ControlToValidate="txtAddressLine1"  Display="Dynamic" ID="RequiredFieldValidator8" runat="server" SetFocusOnError="true"
                                                        ErrorMessage="Address Line1 is required." ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                </td>
                                                <td valign="top">Address Line2</td>
                                                <td valign="top">
                                                    <asp:TextBox ID="txtAddressLine2" MaxLength="500"  runat="server" CssClass="txtboxtxt"  Width="170px"  ></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>                                                
                                                <td valign="top">Address Line3</td>
                                                <td valign="top">
                                                    <asp:TextBox ID="txtAddressLine3" MaxLength="500"  runat="server" Width="170px"  CssClass="txtboxtxt" ></asp:TextBox>
                                                </td>
                                                <td valign="top">State<font color='red'>*</font></td>
                                                <td valign="top">
                                                    <asp:DropDownList ID="ddlState" runat="server" CssClass="simpletxt1"
                                                        Width="175px" AutoPostBack="true"  OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="CVState" runat="server" ControlToValidate="ddlState" ErrorMessage="State code is required."
                                                        Operator="NotEqual" SetFocusOnError="True" ValueToCompare="0" ValidationGroup="editt"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>                                                
                                                <td valign="top">City<font color='red'>*</font></td>
                                                <td valign="top">
                                                    <asp:DropDownList ID="ddlCity" runat="server" CssClass="simpletxt1" Width="175px">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="CVCity" runat="server" ControlToValidate="ddlCity" ErrorMessage="City code is required."
                                                        Operator="NotEqual" SetFocusOnError="True" ValueToCompare="0" ValidationGroup="editt"></asp:CompareValidator>
                                                </td>
                                                <td valign="top">Pin Code</td>
                                                <td valign="top">
                                                    <asp:TextBox ID="txtPinCode" runat="server" Width="170px"  CssClass="txtboxtxt" ></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ControlToValidate="txtPinCode"  Display="Dynamic" ID="RequiredFieldValidator11" runat="server" SetFocusOnError="true"
                                                        ErrorMessage="Pin Code is required." ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtPinCode" ID="RegularExpressionValidator1" ValidationGroup="editt" runat="server" 
                                                            ErrorMessage="Proper Pin Code is required."  ValidationExpression="^\d*$"></asp:RegularExpressionValidator>--%>
                                                </td>
                                            </tr>
                                            <tr>                                                
                                                <td valign="top">ECC Number<font color='red'>*</font></td>
                                                <td valign="top">
                                                        <asp:TextBox ID="txtECCNumber"  Width="170px" runat="server" CssClass="txtboxtxt" ></asp:TextBox>
                                                        <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator9" runat="server" SetFocusOnError="true"
                                                            ErrorMessage="ECC Number is required." ControlToValidate="txtECCNumber" ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                </td>
                                                <td valign="top">TIN Number<font color='red'>*</font></td>
                                                <td valign="top">
                                                        <asp:TextBox ID="txtTINNumber" Width="170px" runat="server" CssClass="txtboxtxt" ></asp:TextBox>
                                                        <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true"
                                                            ErrorMessage="TIM Number is required." ControlToValidate="txtTINNumber" ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>                                                
                                                <td valign="top"><asp:Label ID="lblTransactionName" runat="server"></asp:Label></td>
                                                <td valign="top"><asp:Label ID="lblTransactionNo" runat="server"></asp:Label></td>
                                                <td valign="top"><asp:Label ID="lblSAPSalesOrderName" runat="server"></asp:Label></td>
                                                <td valign="top"><asp:Label ID="lblSAPSalesOrderNo" runat="server"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                  <td colspan="2">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <!-- For button portion update -->
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button Text="Save as Draft" Width="80px" CssClass="btn" ID="imgBtnDraft" runat="server"
                                                        CausesValidation="false" ValidationGroup="editt" OnClick="imgBtnDraft_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button Text="Generate Sales Order" Width="120px" CssClass="btn" ID="imgBtnConfirm" runat="server"
                                                        CausesValidation="True" ValidationGroup="editt" OnClick="imgBtnConfirm_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="imgBtnDiscard" Width="70px" runat="server" CausesValidation="false"
                                                        CssClass="btn" Text="Discard Draft" OnClick="imgBtnDiscard_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="imgBtnCancel" Width="70px" runat="server" CausesValidation="false"
                                                        CssClass="btn" Text="Back" OnClick="imgBtnCancel_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- For button portion update end -->                                        </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="display:none;">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn" OnClick="btnSearch_Click"
                                                    ValidationGroup="TopSearch" Text="Search" Width="70px" />
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

