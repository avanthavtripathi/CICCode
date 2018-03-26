<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SMSActivityConsumption.aspx.cs" Inherits="SMSActivityConsumption" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMS Activity Consumption</title>
    <link href="../css/global.css" rel="stylesheet" type="text/css" />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
</head>
<body >
<form id="form1" runat="server">
<div>
<asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="600" runat="server" EnablePageMethods="true" />
<asp:UpdatePanel ID="updatepnl" runat="server">
<ContentTemplate>

<script type="text/javascript">
    function CalculateCurrentAmount(txtAmount,txtQty,lblRate)
    { 
        document.getElementById(txtAmount).value=document.getElementById(txtQty).value*document.getElementById(lblRate).innerHTML;
        if(document.getElementById(txtAmount).value=="NaN")
        {
        document.getElementById(txtAmount).value="0";
        }
     }
</script>

            <table width="100%">
                <tr>
                    <td class="headingred">
                        Activity Consumption for a complaint
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["SIMSAjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" colspan="2">
                        <table width="100%" border="0">
                            <tr>
                                <td width="100%" align="left" class="bgcolorcomm">
                                    <table width="98%" border="0">
                                        <tr>
                                            <td align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                                <font color='red'>*</font>
                                                <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="100%">
                                                    <tr>
                                                        <td style="width: 14%">
                                                            Complaint No.<font color='red'>*</font>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblComplaintNo"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 14%">
                                                            Complaint Date
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblComplaintDate" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 14%">
                                                            Product Division
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblProdDiv" runat="server" BorderStyle="None"></asp:Label>
                                                            <asp:HiddenField ID="hdnProductDivision_Id" runat="server" />
                                                            <asp:HiddenField ID="hdnProduct_Id" runat="server" />
                                                            <asp:HiddenField ID="hdnASC_Id" runat="server" />
                                                        </td>
                                                        <td width="5%">
                                                        </td>
                                                        <td width="10%">
                                                        </td>
                                                        <td width="15%">
                                                        </td>
                                                        <td width="5%">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        
                                        
                                       <tr>
                                            <td align="right">
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b><asp:Label ID="lblActivityCharges" runat="server" Text="Activity Charges"></asp:Label></b>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <asp:Label ID="lblActivitySearch" runat="server" Text="Search Activity:"></asp:Label>
                                                <asp:TextBox ID="TxtActivityearch" ValidationGroup="ProductRef" CssClass="txtboxtxt" runat="server"
                                                    Width="130" CausesValidation="True"></asp:TextBox>
                                                <asp:Button ID="BtnSearch" runat="server"  ValidationGroup="ProductRef" 
                                                    Width="20px" Text="Go" CssClass="btn" onclick="BtnSearch_Click1" />&nbsp;
                                                    </td>
                                        </tr>
                                        <tr>
                                        <td>
                                        <asp:GridView ID="GridActivitySearch" runat="server" AlternatingRowStyle-CssClass="fieldName"
                                                    AutoGenerateColumns="False" GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor"
                                                    HorizontalAlign="Left"  RowStyle-CssClass="gridbgcolor"  
                                                Width="1300px" HeaderStyle-VerticalAlign="Top">
                                                    <RowStyle CssClass="gridbgcolor" />
                                                    <Columns>
                                                    <asp:BoundField HeaderText="ActivitySno" Visible="false" DataField="ActivityParameter_SNo" 
                                                            SortExpression="ActivityParameter_SNo" ReadOnly="True" />    
                                                   
                                                        <asp:BoundField HeaderText="Activity" DataField="Activity_Description" 
                                                            SortExpression="Activity_Description" ReadOnly="True" />      
                                                        <asp:BoundField HeaderText="Param-1" DataField="Parameter_Code1" 
                                                            SortExpression="Parameter_Code1" ReadOnly="True">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="PV-1" DataField="Possible_Value1" 
                                                            SortExpression="Possible_Value1" ReadOnly="True">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Param-2" DataField="Parameter_Code2" 
                                                            SortExpression="Parameter_Code2" ReadOnly="True">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="PV-2" DataField="Possible_Value2" 
                                                            SortExpression="Possible_Value2" ReadOnly="True">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Param-3" DataField="Parameter_Code3" 
                                                            SortExpression="Parameter_Code3" ReadOnly="True">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="PV-3" DataField="Possible_Value3" 
                                                            SortExpression="Possible_Value3" ReadOnly="True">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Param-4" DataField="Parameter_Code4" 
                                                            SortExpression="Parameter_Code4" ReadOnly="True">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="PV-4" DataField="Possible_Value4" 
                                                            SortExpression="Possible_Value4" ReadOnly="True">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>                                                        
                                                        <asp:BoundField HeaderText="Rate" DataField="Rate" SortExpression="Rate" ReadOnly="True">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Select">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="CheckSelect" runat="server" />
                                                                <asp:HiddenField ID="hdnActivityParameter_SNo" Value='<%# DataBinder.Eval(Container.DataItem, "ActivityParameter_SNo")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                    <AlternatingRowStyle CssClass="fieldName" />
                                                    
                                                </asp:GridView>
                                            
                                        </td>
                                        
                                        </tr>
                                        <tr>
                                        <td align="center">
                                           <asp:Button Text="Add" Width="70px" ID="BtnAdd" CssClass="btn" runat="server"
                                                    CausesValidation="False" ValidationGroup="Add" 
                                                onclick="BtnAdd_Click" />
                                        </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <asp:GridView ID="gvActivityCharges" runat="server" AlternatingRowStyle-CssClass="fieldName"
                                                AutoGenerateColumns="False" GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor"
                                                    HorizontalAlign="Left"  RowStyle-CssClass="gridbgcolor"  Width="1300px" HeaderStyle-VerticalAlign="Top"
                                                    onrowdatabound="gvActivityCharges_RowDataBound">
                                                    <RowStyle CssClass="gridbgcolor" />
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Activity" DataField="Activity_Description" 
                                                            SortExpression="Activity_Description" ReadOnly="True" />      
                                                        <asp:BoundField HeaderText="Param-1" DataField="Parameter_Code1" 
                                                            SortExpression="Parameter_Code1" ReadOnly="True">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="PV-1" DataField="Possible_Value1" 
                                                            SortExpression="Possible_Value1" ReadOnly="True">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Param-2" DataField="Parameter_Code2" 
                                                            SortExpression="Parameter_Code2" ReadOnly="True">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="PV-2" DataField="Possible_Value2" 
                                                            SortExpression="Possible_Value2" ReadOnly="True">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Param-3" DataField="Parameter_Code3" 
                                                            SortExpression="Parameter_Code3" ReadOnly="True">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="PV-3" DataField="Possible_Value3" 
                                                            SortExpression="Possible_Value3" ReadOnly="True">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Param-4" DataField="Parameter_Code4" 
                                                            SortExpression="Parameter_Code4" ReadOnly="True">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="PV-4" DataField="Possible_Value4" 
                                                            SortExpression="Possible_Value4" ReadOnly="True">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>                                                        
                                                        <asp:TemplateField HeaderText="Rate">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRate" runat="server" CssClass="simpletxt1" Text='<%# DataBinder.Eval(Container.DataItem, "Rate")%>'></asp:Label>                                                           
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="UOM" DataField="UOM" 
                                                            SortExpression="UOM" ReadOnly="True">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>                                                        
                                                        <asp:TemplateField HeaderText="Quantity">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtActualQty" runat="server" CssClass="simpletxt1" Width="60px" ValidationGroup="editt1" Text='<%# DataBinder.Eval(Container.DataItem, "Actual_Qty")%>'></asp:TextBox>
                                                                <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvRate1" runat="server" ControlToValidate="txtActualQty"
                                                                    Display="Dynamic" ErrorMessage="Qty can't be zero." InitialValue="0" ValidationGroup="editt1"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator EnableClientScript="true" Display="Dynamic" ControlToValidate="txtActualQty"
                                                                    ID="RegularExpressionValidator123" ValidationGroup="editt1" runat="server" ErrorMessage="Numeric Value required."
                                                                    ValidationExpression="^\d*$">
                                                                </asp:RegularExpressionValidator>                                                            
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtAmount" AutoPostBack="false" OnTextChanged="txtAmount_TextChanged" runat="server" CssClass="simpletxt1" Width="60px" ValidationGroup="editt1" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>'></asp:TextBox>
                                                                <asp:RegularExpressionValidator EnableClientScript="true" Display="Dynamic" ControlToValidate="txtAmount"
                                                                    ID="RegularExpressionValidator12345" ValidationGroup="editt1" runat="server" ErrorMessage="Proper Amount required."
                                                                    ValidationExpression="^\d{1,8}(\.\d{1,2})?$">
                                                                </asp:RegularExpressionValidator>                                                         
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="simpletxt1" Width="150px" TextMode="MultiLine" ValidationGroup="editt1" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks")%>'></asp:TextBox>                                                  
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Select">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkActivityConfirm" 
                                                                    Checked=' <%# DataBinder.Eval(Container.DataItem, "Confirm")%>' runat="server" 
                                                                    AutoPostBack="true" oncheckedchanged="chkActivityConfirm_CheckedChanged" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                        <asp:BoundField HeaderText="ActivityParameter_SNo" 
                                                            DataField="ActivityParameter_SNo" SortExpression="ActivityParameter_SNo" 
                                                            ReadOnly="True">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="FixedAmount" DataField="FixedAmount" SortExpression="FixedAmount" ReadOnly="True">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                    <AlternatingRowStyle CssClass="fieldName" />
                                                    <EmptyDataTemplate>
                                                    <p align="center"><b>Currently no activity added for this complaint. Please search to add new activities.</b></p>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="MsgTDCount" >
                                                Total Amount : <asp:Label ID="lblTotalAmount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Button Text="Save & Close Complaint" Width="130px" ID="imgbtnSave" 
                                                    CssClass="btn" runat="server"
                                                    CausesValidation="true" ValidationGroup="editt1" 
                                                    OnClick="imgbtnSave_Click" />
                                                <asp:Button Text="Cancel" Width="70px" ID="imgBtnCancel" CssClass="btn" runat="server"
                                                    CausesValidation="False" ValidationGroup="editt" OnClick="imgBtnCancel_Click1" />
                                                <asp:Panel ID="popupPanel" runat="server" Style="display: none; width: 200px; background-color: Gray;
                                                    border-width: 2px; border-color: Black; border-style: solid; padding: 20px;">
                                                    <br />
                                                    <br />
                                                    <div style="text-align: left;">
                                                        <b>Are You Sure to confirm?</b><br />
                                                        <br />
                                                    </div>
                                                    <div style="text-align: right;">
                                                        <asp:Button ID="ButtonOk" runat="server" Text="OK" CssClass="btn" />
                                                        <asp:Button ID="ButtonCancel" runat="server" CssClass="btn" Text="Cancel" />
                                                    </div>
                                                </asp:Panel>
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
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
</form>
</body>
</html>
