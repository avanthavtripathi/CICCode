<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true"
    CodeFile="SpareSalePurchaseByASC.aspx.cs" Inherits="SIMS_Pages_SpareSalePurchaseByASC" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
        <script type="text/javascript">
            function checkDate(sender,args)
            {
                //create a new date var and set it to the
                //value of the senders selected date
                var selectedDate = new Date();
                selectedDate = sender._selectedDate;
                //create a date var and set it's value to today
                var todayDate = new Date();
                var mssge = "";

                if(selectedDate > todayDate)
                 {
                    //set the senders selected date to today
                    sender._selectedDate = todayDate;
                    //set the textbox assigned to the cal-ex to today
                    sender._textbox.set_Value(sender._selectedDate.format(sender._format));
                    //alert the user what we just did and why
                    alert("Mov. Date Can not be greater than present date!");
                 }
            }
            </script>

            <table width="100%">
                <tr>
                    <td colspan="2" class="headingred">
                        Spare Sale Purchase By ASC
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>"
                        style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["SIMSAjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td align='<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>' colspan="2">
                        <font color="red">*</font>
                        <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td width="13%">
                        ASC Name:
                    </td>
                    <td>
                        <asp:Label ID="lblASCName" runat="server"></asp:Label>
                        <asp:HiddenField ID="hdnASC_Code" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Product Division:<font color="red">*</font>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlProdDivision" runat="server" AutoPostBack="True" CssClass="simpletxt1"
                            OnSelectedIndexChanged="ddlProdDivision_SelectedIndexChanged" ValidationGroup="editt"
                            Width="190">
                        </asp:DropDownList>
                        <%-- <asp:RequiredFieldValidator ID="rfvProductDiv" runat="server" ControlToValidate="ddlProdDivision"
                            Display="Dynamic" ErrorMessage="Product Division is required." InitialValue="0"
                            SetFocusOnError="true" ValidationGroup="editt"></asp:RequiredFieldValidator>--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlProdDivision"
                            Display="Dynamic" ErrorMessage="Product Division is required." InitialValue="0"
                            SetFocusOnError="true" ValidationGroup="confirm"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Mov. Type:<font color="red">*</font>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMovType" runat="server" AutoPostBack="True" CssClass="simpletxt1"
                            ValidationGroup="confirm" Width="190px" OnSelectedIndexChanged="ddlMovType_SelectedIndexChanged">
                            <%--<asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Sale" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Purchase" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Write-Off" Value="3"></asp:ListItem>
                            <asp:ListItem Text="SAN" Value="4"></asp:ListItem>
                            <asp:ListItem Text="Good Stock To Defective" Value="5"></asp:ListItem>--%>
                        </asp:DropDownList>
                        <%-- <asp:RequiredFieldValidator ID="rfvMovType" runat="server" ControlToValidate="ddlMovType"
                            Display="Dynamic" ErrorMessage="Mov. Type is required." InitialValue="0" SetFocusOnError="true"
                            ValidationGroup="editt"></asp:RequiredFieldValidator>--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlMovType"
                            Display="Dynamic" ErrorMessage="Mov. Type is required." InitialValue="0" SetFocusOnError="true"
                            ValidationGroup="editt"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" id="trVendorName" visible="false">
                    <td>
                        Vendor Name:<font color="red">*</font>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlVendor" runat="server" CssClass="simpletxt1" ValidationGroup="editt"
                            Width="190px" AutoPostBack="True" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvVendor" runat="server" ControlToValidate="ddlVendor"
                            Display="Dynamic" ErrorMessage="Vendor is required." InitialValue="0" SetFocusOnError="true"
                            ValidationGroup="confirm"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Search Spares:
                    </td>
                    <td>
                        <asp:TextBox ID="txtFindSpare" ValidationGroup="ProductRef" CssClass="txtboxtxt"
                            runat="server" Width="160" CausesValidation="True"></asp:TextBox>
                        <asp:Button ID="btnGoSpare" runat="server" ValidationGroup="ProductRef" Width="20px"
                            Text="Go" CssClass="btn" OnClick="btnGoSpare_Click" />
                    </td>
                </tr>
                <tr id="trMovDate" runat="server" visible="false">
                    <td>
                        Mov. Date:<font color="red">*</font>
                    </td>
                    <td>
                        <asp:TextBox ID="txtMovDate" runat="server" CssClass="txtboxtxt" ValidationGroup="TopSearch"></asp:TextBox>
                        <%-- <AjaxToolKit:CalendarExtender ID="calExtMovDate" runat="server" TargetControlID="txtMovDate"
                                                    PopupButtonID="Calendar1" OnClientDateSelectionChanged="checkDate">
                                                </AjaxToolKit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvMovDate" runat="server" ControlToValidate="txtMovDate"
                                                    Display="Dynamic" ErrorMessage="Mov. Date is required." SetFocusOnError="true"
                                                    ValidationGroup="editt"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr id="trTransaction" runat="server" visible="false">
                    <td>
                        Document Number:
                    </td>
                    <td>
                        <asp:Label ID="lblTransactionNo" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table width="100%" border="0" cellspacing="1" cellpadding="2">
                            <tr>
                                <td align="left">
                                    <asp:GridView ID="GvSpareSalePurchaseByASC" runat="server" AllowPaging="True" AllowSorting="True"
                                        AlternatingRowStyle-CssClass="fieldName" HeaderStyle-HorizontalAlign="Left" RowStyle-HorizontalAlign="Left"
                                        AlternatingRowStyle-HorizontalAlign="Left" PageSize="10" AutoGenerateColumns="False"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" Width="100%" RowStyle-CssClass="gridbgcolor"
                                        PagerStyle-CssClass="Paging" OnRowDataBound="GvSpareSalePurchaseByASC_RowDataBound">
                                        <RowStyle CssClass="gridbgcolor" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Spare">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Spare")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlSpareCode" runat="server" CssClass="simpletxt1" AutoPostBack="True"
                                                        Width="130px" OnSelectedIndexChanged="ddlSpareCode_SelectedIndexChanged" ValidationGroup="editt">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvSpareCode" runat="server"
                                                        ControlToValidate="ddlSpareCode" Display="Dynamic" ErrorMessage="Spare Code is required."
                                                        InitialValue="0" ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <ItemStyle VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Avl. Stock">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem,"Current_Stock") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="lblCurrentStock" runat="server" Width="70px" CssClass="txtboxtxt"
                                                        ReadOnly="true" Text='<%#Eval("Current_Stock")%>' EnableViewState="true"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemStyle VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Location (Good Stock)">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Location")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlLocation" runat="server" CssClass="simpletxt1" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" ValidationGroup="editt">
                                                    </asp:DropDownList>
                                                    <%-- <asp:RequiredFieldValidator ID="rfvAscLocation" runat="server" ControlToValidate="ddlLocation"
                                                        ErrorMessage="Location is required." InitialValue="0" SetFocusOnError="true"
                                                        ValidationGroup="editt"></asp:RequiredFieldValidator>--%>
                                                </EditItemTemplate>
                                                <ItemStyle VerticalAlign="Top" />
                                                <%--AutoPostBack="True" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged"--%>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Avl. Stock">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem,"QtyAsPerLocation") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="lblStockAsPerLocation" runat="server" CssClass="txtboxtxt" ReadOnly="true"
                                                        Width="60px" EnableViewState="true"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemStyle VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Location (Defective Stock)" Visible="false">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Def_Location")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlDefLocation" runat="server" CssClass="simpletxt1" ValidationGroup="editt">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvToLocation" runat="server" ControlToValidate="ddlDefLocation"
                                                        ErrorMessage="Location is required." InitialValue="0" SetFocusOnError="true"
                                                        ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <ItemStyle VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem,"Quantity") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtQuantity" runat="server" Text='<%#Eval("Quantity")%>' CssClass="txtboxtxt"
                                                        MaxLength="10" Width="60px" ValidationGroup="editt"></asp:TextBox>
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
                                                <ItemStyle VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Action_Type")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlActionType" runat="server" CssClass="simpletxt1" ValidationGroup="editt">
                                                        <%--<asp:ListItem Value="1" Text="Stock Add(+)"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Stock Reduce(-)"></asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvActionType" runat="server"
                                                        ControlToValidate="ddlActionType" Display="Dynamic" ErrorMessage="Action is required."
                                                        InitialValue="0" ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <ItemStyle VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Comments">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Comments")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtComments" runat="server" CssClass="txtboxtxt" Width="100px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="ReqComments" runat="server"
                                                        ControlToValidate="txtComments" ErrorMessage="Comments is required." Display="Dynamic"
                                                        ToolTip="Comments is required." ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <ItemStyle VerticalAlign="Top" />
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
                                            <asp:BoundField HeaderText="Spare_Id" DataField="Spare_Id" ReadOnly="True">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                        </Columns>
                                        <PagerStyle CssClass="Paging" />
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
                        </table>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0">
                <tr>
                    <td height="25" align="center">
                        <table>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnConfirm" runat="server" Text="Confirm" CssClass="btn" Style="width: 60px"
                                        ValidationGroup="confirm" CausesValidation="true" OnClick="btnConfirm_Click" />
                                    <%--OnClientClick="javascript:return confirm('Are you sure you want to save this Spare Requirement?')"--%>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn" Style="width: 60px"
                                        CausesValidation="false" OnClick="btnCancel_Click" />
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn" Style="width: 60px"
                                        Visible="False" OnClick="btnBack_Click" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
