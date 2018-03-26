<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true" CodeFile="DeliverySchedule.aspx.cs" Inherits="SIMS_Pages_DeliverySchedule" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
<asp:UpdatePanel ID="updatepnl" runat="server">
<ContentTemplate>
 <table width="100%" cellpadding="2" cellspacing="0">
        <tr>
            <td class="headingred">
                <h3>Part Delivery Schedule</h3>
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
                                    <td colspan="4"></td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <table>
                                            <tr>
                                                <td>Spare Description:</td>
                                                <td>
                                                    <asp:Label ID="lblSpare" runat="server"></asp:Label>
                                                    <asp:HiddenField ID="hdn_Ordered_Transaction_No" runat="server" />
                                                    <asp:HiddenField ID="hdnSpare_Id" runat="server" />
                                                </td>  
                                            </tr>
                                            <tr>          
                                                <td>Ordered Quantity:</td>
                                                <td>
                                                    <asp:Label ID="lblOrderedQty" runat="server"></asp:Label>
                                                </td>  
                                            </tr>
                                        </table>
                                    </td>
                                    
                                </tr>   
                                <tr>
                                  <td colspan="4">&nbsp;</td>
                                </tr>
                                <tr style="display:none;"><td colspan="4" ><asp:Label ID="lblRowCount" runat="server"></asp:Label></td></tr>
                                <tr>
                                  <td colspan="4">
                                    <asp:GridView ID="gvComm" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="fieldName"
                                        AutoGenerateColumns="False" DataKeyNames="Part_Delivery_Id" 
                                          GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor"
                                        HorizontalAlign="Left" OnPageIndexChanging="gvComm_PageIndexChanging" onrowdatabound="gvComm_RowDataBound" 
                                        PageSize="10" RowStyle-CssClass="gridbgcolor" Width="100%" >
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
                                            <asp:BoundField DataField="Part_Delivery_Id" SortExpression="Part_Delivery_Id" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Part_Delivery_Id" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Remove">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkRemove" Text="Remove" runat="server" 
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Part_Delivery_Id") %>' 
                                                        onclick="lnkRemove_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            
                                        </Columns>
                                        <%--<EmptyDataTemplate>
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                        <img src="<%=ConfigurationManager.AppSettings["UserMessage"]%>" alt="" />
                                                        <b>No Record found</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>--%>
                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                        <AlternatingRowStyle CssClass="fieldName" />
                                    </asp:GridView>
                                  </td>
                                </tr>
                                <tr>
                                  <td colspan="4">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <table >
                                            <tr style="display:none;"><td colspan="5" ><asp:TextBox CausesValidation="false" ID="txtRemainingQty" runat="server"></asp:TextBox></td></tr>
                                            <tr>
                                                <td>Delivery Quantity:</td>
                                                <td>
                                                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="txtboxtxt"  ValidationGroup="editt" ></asp:TextBox>
                                                </td>
                                                <td>Delivery Date:</td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtDeliveryDate" CssClass="txtboxtxt" ValidationGroup="editt" />
                                                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtDeliveryDate"></cc1:CalendarExtender> 
                                                    
                                                </td>
                                                <td>
                                                    <asp:Button Text="Add" Width="80px" CssClass="btn" ID="imgBtnSave" runat="server"
                                                        CausesValidation="True" ValidationGroup="editt" OnClick="imgBtnSave_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RFStateDesc" runat="server" SetFocusOnError="true"
                                                            ErrorMessage="Delivery Quantity is required." ControlToValidate="txtQuantity" ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                            <asp:CompareValidator Display="Dynamic" ID="CompareValidator4" runat="server" 
                                                        ControlToCompare="txtRemainingQty" Operator="LessThanEqual" ControlToValidate="txtQuantity" 
                                                        ErrorMessage="Delivery Quantity should not grater than Total Ordered Qty."  ValidationGroup="editt" Type="Integer"></asp:CompareValidator>
                                                        <br />
                                                        <asp:RangeValidator Display="Dynamic" ID="RangeValidator3" ControlToValidate="txtQuantity" MinimumValue="1" MaximumValue="64550" Type="Integer" ValidationGroup="editt" runat="server" 
                                                        ErrorMessage="Proper Proposed qty is required."></asp:RangeValidator>
                                                    
                                                </td>
                                                <td colspan="3">
                                                    <asp:RequiredFieldValidator ControlToValidate="txtDeliveryDate" ID="RequiredFieldValidator1" runat="server" 
                                                        ErrorMessage="Delivery date is Required" ValidationGroup="editt" Display="Dynamic"></asp:RequiredFieldValidator> 
                                                    <asp:CompareValidator Operator="DataTypeCheck" Type="Date" ControlToValidate="txtDeliveryDate"
                                                        ErrorMessage="Not a vaild Date" runat="server" Display="Dynamic" ID="CompareValidator3"
                                                        ValidationGroup="editt"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                  <td colspan="4">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <!-- For button portion update -->
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button Text="Save & Close" Width="80px" CssClass="btn" ID="imgBtnClose" runat="server"
                                                        CausesValidation="false" OnClick="imgBtnClose_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- For button portion update end -->                                        </td>
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


