<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true" CodeFile="SparePurchaseOutside.aspx.cs" Inherits="SIMS_Pages_SparePurchaseOutside" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
  <script type="text/javascript">
    function Calculate(sender , cde)
    {
      if(cde == "pur")
      {
        var qty = document.getElementById(sender.id.replace('txtpurRate','txtQuantity')) ;
        var price = document.getElementById(sender.id.replace('txtpurRate','lblAmount')) ;
        var rate = sender  ;
      }
      else
      {
        var qty = sender ;
        var rate = document.getElementById(sender.id.replace('txtQuantity','txtpurRate')) ;
        var price = document.getElementById(sender.id.replace('txtQuantity','lblAmount')) ;
      }
        var result =  qty.value * rate.value;
        price.innerHTML  = result;
     }
</script>
<asp:UpdatePanel ID="updatepnl" runat="server">
     
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                      Spare Purchase By ASC from Outside Vendors
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
                        <table width="98%" border="0">
                            <tr>
                                <td colspan="2" align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                    <font color='red'>*</font>
                                    <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Region
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlRegion" runat="server" Width="175px" CssClass="simpletxt1"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" ValidationGroup="editt">
                                    </asp:DropDownList>
                                      
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Branch
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlBranch" runat="server" Width="175px" CssClass="simpletxt1"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" ValidationGroup="editt">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                        ErrorMessage="Branch is required." InitialValue="0" SetFocusOnError="true"
                                        ValidationGroup="conf" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    ASC
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlASC" runat="server" Width="175px" CssClass="simpletxt1"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlASC_SelectedIndexChanged" ValidationGroup="editt">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvasc" runat="server" ControlToValidate="ddlASC"
                                        ErrorMessage="ASC is required." InitialValue="0" SetFocusOnError="true"
                                        ValidationGroup="conf" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Divison<font color='red'>*</font>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlProductDivison" runat="server" Width="175px" CssClass="simpletxt1"
                                        ValidationGroup="conf" AutoPostBack="true" 
                                        OnSelectedIndexChanged="ddlProductDivison_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RFRegionDesc" runat="server" ControlToValidate="ddlProductDivison"
                                        ErrorMessage="Division is required." InitialValue="0" SetFocusOnError="true"
                                        ValidationGroup="conf" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                    <td width="30%" align="right">
                        Search Spares:
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtFindSpare" ValidationGroup="ProductRef" CssClass="txtboxtxt"
                            runat="server" Width="160" CausesValidation="True"></asp:TextBox>
                        <asp:Button ID="btnGoSpare" runat="server" ValidationGroup="ProductRef" Width="20px"
                            Text="Go" CssClass="btn" OnClick="btnGoSpare_Click" />
                    </td>
                </tr>
                             <tr>
                                <td align="right">
                                </td>
                                <td align="left">
                                
                                    </td>
                            </tr>
                            <tr>
                                <td align="left" class="MsgTDCount">
                                    Total Number of Records :
                                    <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                        <br />
                        <table width="100%" border="0">
                            <tr>
                                <td> 
     
                                <asp:GridView ID="GvSpareSalePurchaseByASC" runat="server" AllowPaging="True" AllowSorting="True"
                                        AlternatingRowStyle-CssClass="fieldName" HeaderStyle-HorizontalAlign="Left" RowStyle-HorizontalAlign="Left"
                                        AlternatingRowStyle-HorizontalAlign="Left" PageSize="10" AutoGenerateColumns="False"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" Width="100%" RowStyle-CssClass="gridbgcolor"
                                        OnRowDataBound="GvSpareSalePurchaseByASC_RowDataBound">
                                        <RowStyle CssClass="gridbgcolor" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Spare">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Spare")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlSpareCode" runat="server" CssClass="simpletxt1" 
                                                        Width="130px" ValidationGroup="editt" AutoPostBack="True" 
                                                        onselectedindexchanged="ddlSpareCode_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvSpareCode" runat="server"
                                                        ControlToValidate="ddlSpareCode" Display="Dynamic" ErrorMessage="Spare Code is required."
                                                        InitialValue="0" ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <ItemStyle VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vendor">
                                               <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Vendor")%>
                                                </ItemTemplate>
                                          <EditItemTemplate>
                                                 <asp:TextBox ID="txtVendor" runat="server" Text='<%#Eval("Vendor")%>' CssClass="txtboxtxt"
                                                        ValidationGroup="editt" ></asp:TextBox>
                                          <%-- <asp:DropDownList ID="ddlVendor" runat="server" CssClass="simpletxt1" ValidationGroup="editt" Width="190px" />--%>
                                            <asp:RequiredFieldValidator ID="rfvVendor" runat="server" ControlToValidate="txtVendor"
                                                Display="Dynamic" ErrorMessage="Vendor is required." SetFocusOnError="true"
                                                ValidationGroup="editt"></asp:RequiredFieldValidator>
                                          </EditItemTemplate>
                                          </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Bill No.">
                                          <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "BillNo")%>
                                                </ItemTemplate>
                                              <EditItemTemplate>
                                              <asp:TextBox ID="txtBillNo" runat="server" Text='<%#Eval("BillNo")%>' CssClass="txtboxtxt" />
                                                   <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvbill" runat="server"
                                                        ControlToValidate="txtBillNo" Display="Dynamic" ErrorMessage="Bill No. is required."
                                                        ValidationGroup="editt"></asp:RequiredFieldValidator>
                                              </EditItemTemplate>
                                          </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Bill Date">
                                          <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "BillDate")%>
                                                </ItemTemplate>
                                              <EditItemTemplate>
                                              <asp:TextBox ID="txtBilldate" runat="server" Text='<%#Eval("BillDate")%>' CssClass="txtboxtxt" />
                                                   <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvbilld" runat="server"
                                                        ControlToValidate="txtBilldate" Display="Dynamic" ErrorMessage="Bill date is required."
                                                        ValidationGroup="editt"></asp:RequiredFieldValidator>
                                           <cc1:CalendarExtender ID="CEFrom" runat="server" TargetControlID="txtBilldate" Format="dd/MMM/yy">
                                          </cc1:CalendarExtender>
                                              </EditItemTemplate>
                                          </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Quantity">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem,"Quantity") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtQuantity" runat="server" Text='<%#Eval("Quantity")%>' CssClass="txtboxtxt"
                                                        MaxLength="10" Width="60px" onblur="Calculate(this,'')" ValidationGroup="editt" 
                                                        ></asp:TextBox>
                                                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvProposedQty" runat="server"
                                                        ControlToValidate="txtQuantity" Display="Dynamic" ErrorMessage="Quantity can't be zero."
                                                        InitialValue="0" ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvProposedQty1" runat="server"
                                                        ControlToValidate="txtQuantity" Display="Dynamic" ErrorMessage="Quantity is required."
                                                        ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtQuantity" 
                                                        ID="RegularExpressionValidator2" ValidationGroup="editt" runat="server" ErrorMessage="Proper Qty is required."
                                                        ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
                                                </EditItemTemplate>
                                        </asp:TemplateField>
                                          
                                           <asp:TemplateField HeaderText="Rate in System">
                                              <ItemTemplate>
                                              <%# DataBinder.Eval(Container.DataItem, "Rate")%>
                                              </ItemTemplate>
                                               <EditItemTemplate>
                                              <asp:Label ID="lblrate" runat="server" Text='<%#Eval("Rate")%>' />
                                              </EditItemTemplate>
                                          </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Purchased Rate">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "PurchasedRate")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                     <asp:TextBox ID="lbllrate" runat="server" Text='<%#Eval("Rate")%>' style="display:none" />
                                                    <asp:TextBox ID="txtpurRate" runat="server" Text='<%#Eval("PurchasedRate")%>' CssClass="txtboxtxt"
                                                        MaxLength="10" Width="60px" ValidationGroup="editt" onblur="Calculate(this,'pur')" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvpurQty" runat="server"
                                                        ControlToValidate="txtpurRate" Display="Dynamic" ErrorMessage="Purchased Rate can't be zero."
                                                        InitialValue="0" ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvpurQty1" runat="server"
                                                        ControlToValidate="txtpurRate" Display="Dynamic" ErrorMessage="Purchased Rate is required."
                                                        ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtpurRate"
                                                        ID="revpurQty3" ValidationGroup="editt" runat="server" ErrorMessage="Proper Purchased Rate is required."
                                                        ValidationExpression="\d{1,4}(\.\d{1,2})?"></asp:RegularExpressionValidator>
                                                    <asp:CompareValidator runat="server"  ValidationGroup="editt" Display="Dynamic" ControlToValidate="txtpurRate" ControlToCompare="lbllrate" Type="Double" Operator="LessThanEqual" ID="rrr" ErrorMessage="Purchased Rate cannot be greater then SAP Rate." ></asp:CompareValidator>
                                                </EditItemTemplate>
                                           
                                            </asp:TemplateField>
                                                
                                        
                                            <asp:TemplateField HeaderText="Amount">
                                              <ItemTemplate>
                                              <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount")%>' />
                                              </ItemTemplate>
                                          </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Comments">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Comments")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtComments" runat="server" CssClass="txtboxtxt" Width="100px"></asp:TextBox>
                                                <%--        <asp:RequiredFieldValidator SetFocusOnError="true" ID="ReqComments" runat="server"
                                                            ControlToValidate="txtComments" ErrorMessage="Comments is required." Display="Dynamic"
                                                            ToolTip="Comments is required." ValidationGroup="editt"></asp:RequiredFieldValidator>--%>
                                                </EditItemTemplate>
                                         </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:Button CommandName="Add" CommandArgument="" Text="Add" ToolTip="Add Row" ID="btnAddRow"
                                                                    runat="server" CssClass="btn" ValidationGroup="editt" CausesValidation="true"
                                                                    Style="width: 50px" OnClick="btnAddRow_Click" />
                                                            </td>
                                                            <td>
                                                                <asp:Button CommandName="DeleteRow" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Spare_Id")%>'
                                                                    Text="Delete" ToolTip="Delete Row" ID="btnDeleteRow" runat="server" CssClass="btn"
                                                                    ValidationGroup="editt" CausesValidation="false" Style="width: 50px" OnClick="btnDeleteRow_Click" />
                                                                <asp:Button CommandName="CancelRow" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Spare_Id")%>'
                                                                    Text="Cancel" ToolTip="Cancel Row" ID="btnCancelRow" runat="server" CssClass="btn"
                                                                    ValidationGroup="editt" CausesValidation="false" Style="width: 50px" OnClick="btnCancelRow_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Spare_Id" DataField="Spare_Id" ReadOnly="True" >
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
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
                                        <HeaderStyle CssClass="fieldNamewithbgcolor" HorizontalAlign="Left" />
                                        <AlternatingRowStyle CssClass="fieldName" HorizontalAlign="Left" />
                                    </asp:GridView>
                             
                                    
                                    </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
<table width="100%" border="0">
                <tr>
                    <td height="25" align="center">
                        <table>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnConfirm" runat="server" Text="Confirm" CssClass="btn" Style="width: 60px"
                                        ValidationGroup="conf" CausesValidation="true" 
                                        OnClick="btnConfirm_Click" />
                                    <%--OnClientClick="javascript:return confirm('Are you sure you want to save this Spare Requirement?')"--%>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn" Style="width: 60px"
                                        CausesValidation="false"  />
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn" Style="width: 60px"
                                        Visible="False"  />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

