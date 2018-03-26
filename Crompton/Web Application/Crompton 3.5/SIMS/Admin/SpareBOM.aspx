<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true" EnableEventValidation="false" 
    CodeFile="SpareBOM.aspx.cs" Inherits="Admin_SpareBOM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportToExcel" />
        </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Spare BOM
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
                        <td colspan="2" align="right" style="padding-right: 10px">
                            <a href="../BulkUpload/bulkUploadBOMMaster.xls" target="_blank">Spare BOM Bulk Upload</a>
                        </td>
                    </tr>
                <tr>
                    <td colspan="2" align="right" style="padding-right: 10px">
                        <asp:RadioButtonList ID="rdoboth" RepeatDirection="Horizontal" RepeatColumns="3"
                            runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdoboth_SelectedIndexChanged">
                            <asp:ListItem Value="1" Text="Active" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="0" Text="In-Active"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Both"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" colspan="2">
                        <table border="0" width="100%">
                            <tr>
                                <td align="left" class="MsgTDCount">
                                    Total Number of Records :
                                    <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server">
                                    </asp:Label>
                                </td>
                                <td align="right">
                                    Search For
                                    <asp:DropDownList ID="ddlSearch" runat="server" Width="160px" CssClass="simpletxt1">
                                        <asp:ListItem Text="Product Division" Value="Unit_Desc"></asp:ListItem>
                                        <asp:ListItem Text="Product Line" Value="ProductLine_Desc"></asp:ListItem>
                                        <asp:ListItem Text="Product" Value="Product_Desc"></asp:ListItem>
                                        <asp:ListItem Text="Spare" Value="SAP_Desc"></asp:ListItem>
                                        
                                    </asp:DropDownList>
                                    With
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="txtboxtxt" Width="100px" Text=""></asp:TextBox>
                                    <asp:Button Text="Go" Width="25px" CssClass="btn" ID="imgBtnGo" runat="server" CausesValidation="False"
                                        ValidationGroup="editt" OnClick="imgBtnGo_Click" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0">
                            <tr>
                                <td>
                                    <asp:GridView RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="True"
                                        AllowSorting="True" AutoGenerateColumns="False" ID="gvComm" runat="server" 
                                        OnPageIndexChanging="gvComm_PageIndexChanging" Width="100%"
                                        HorizontalAlign="Left" OnSorting="gvComm_Sorting">
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                       
                                        <asp:TemplateField HeaderText="">
                                        <HeaderTemplate><asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" /></HeaderTemplate>
                                            <ItemTemplate><asp:CheckBox ID="ChkSpareBOMID" runat="server" /></ItemTemplate>
                                        </asp:TemplateField>
                                            
                                            <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                <HeaderStyle Width="20px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Unit_Desc" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Division" SortExpression="Unit_Desc">
                                                <HeaderStyle HorizontalAlign="Left" Width="80px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProductLine_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Product Line" SortExpression="ProductLine_Desc">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Product_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Product" SortExpression="Product_Desc">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Product_Code" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="ProductCode" SortExpression="Product_Code">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SAP_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Spare" SortExpression="SAP_Desc">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Spare_QtyPerUnit" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Spare Qty Per Unit" SortExpression="Spare_QtyPerUnit">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Alternate_Spare1" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Alternate Spare1" SortExpression="Alternate_Spare1">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ALT_Spare1_QtyPerUnit" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="ALT Spare 1 Qty Per Unit" SortExpression="ALT_Spare1_QtyPerUnit">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Alternate_Spare2" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Alternate Spare2" SortExpression="Alternate_Spare2">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ALT_Spare2_QtyPerUnit" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="ALT Spare 2 Qty Per Unit" SortExpression="ALT_Spare2_QtyPerUnit">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Alternate_Spare3" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Alternate Spare3" SortExpression="Alternate_Spare3">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ALT_Spare3_QtyPerUnit" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="ALT Spare 3 Qty Per Unit" SortExpression="ALT_Spare3_QtyPerUnit">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Alternate_Spare4" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Alternate Spare4" SortExpression="Alternate_Spare4">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ALT_Spare4_QtyPerUnit" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="ALT Spare 4 Qty Per Unit" SortExpression="ALT_Spare4_QtyPerUnit">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Active_Flag" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Status" SortExpression="Active_Flag">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <%--<asp:CommandField ShowSelectButton="True" HeaderStyle-Width="60px" HeaderText="Edit">
                                                <HeaderStyle Width="60px" />
                                            </asp:CommandField>--%>
                                            <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                            <a id="spareBomSelect" href="javascript:void(0);" onclick="fillSpareBomDetails('<%# Eval("SPARE_BOM_ID") %>');">Select</a>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Spare_BOM_ID" Visible="false">
                                            <ItemTemplate><asp:Label ID="lblSpareBOM_ID" runat="server" Text='<%# Bind("SPARE_BOM_ID") %>'></asp:Label>
                                            </ItemTemplate>
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
                                    <!-- Batch Listing OnSelectedIndexChanging="gvComm_SelectedIndexChanging"DataKeyNames="SPARE_BOM_ID" -->
                                    <!-- End Batch Listing -->
                                </td>
                            </tr>
                            <tr><td align="left">
                               <asp:Button Text="Make InActive" Width="120px" CssClass="btn" ID="imgbtnInActive" 
                                    runat="server" onclick="imgbtnInActive_Click"/>            
                            &nbsp;&nbsp;<asp:Label ID="lblmsgActiveInActive" ForeColor="Red" runat="server"></asp:Label>
                            </td></tr>
                            <tr>
                               <asp:Button Width="100px" Text="Export to Excel" CssClass="btn" CausesValidation="true" ID="btnExportToExcel"  
                                        runat="server" onclick="btnExportToExcel_Click"
                                         />
                            </tr>
                            <tr>
                                <td width="100%" align="left" class="bgcolorcomm">
                                    <table width="98%" border="0">
                                        <tr>
                                            <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" colspan="2" style="text-align:center;">
                                                <img id="ImgProgessBar" src="<%=ConfigurationManager.AppSettings["SIMSAjaxLoadingImageName"]%>" alt="" style="display:none;" />
                                            </td>
                                            <td colspan="2" align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                                <font color='red'>*</font>
                                                <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:HiddenField ID="hdnSpareBOMId" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="15%">
                                                Division<font color='red'>*</font>
                                            </td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="ddlDivision" runat="server" CssClass="simpletxt1" onchange="GetProductLine(this.value, 'Select'); GetSpareAltSpare_DropdownSelectedchane(this.value);" Width="450px" ValidationGroup="editt">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Division is Required"
                                                    ControlToValidate="ddlDivision" InitialValue="0" SetFocusOnError="true" ValidationGroup="editt">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Product Line<font color='red'>*</font>
                                            </td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="ddlProductLine" runat="server" CssClass="simpletxt1" Width="450px" onchange="GetProductDetails(this.value, 'Select');SetDataInHidden(this.value,'hdnproductLine') " ValidationGroup="editt">
                                                </asp:DropDownList>
                                                 <asp:HiddenField ID="hdnproductLine" runat="server" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Product Line is Required"
                                                    ValidationGroup="editt" ControlToValidate="ddlProductLine" SetFocusOnError="true"
                                                    InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Product<font color='red'>*</font>
                                            </td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="ddlFGCode" runat="server" CssClass="simpletxt1" Width="450px" onchange="SetDataInHidden(this.value,'hdnProduct')"
                                                    ValidationGroup="editt">
                                                </asp:DropDownList>
                                                 <asp:HiddenField ID="hdnProduct" runat="server" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Product is Required"
                                                    ValidationGroup="editt" ControlToValidate="ddlFGCode" SetFocusOnError="true"
                                                    InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Spare<font color='red'>*</font>
                                            </td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="ddlSpareCode" ClientIDMode="Static" runat="server" CssClass="simpletxt1" Width="450px" onchange="SetDataInHidden(this.value,'hdnSpare')"
                                                    ValidationGroup="editt">
                                                </asp:DropDownList>
                                                 <asp:HiddenField ID="hdnSpare" runat="server" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Spare is Required"
                                                    ControlToValidate="ddlSpareCode" InitialValue="0" SetFocusOnError="true" ValidationGroup="editt">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Qty/Unit<font color='red'>*</font>
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtQtyPerUnit" Width="50px" MaxLength="4" runat="server" CssClass="txtboxtxt"
                                                    ValidationGroup="editt"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtQtyPerUnit"
                                                    ErrorMessage="Qty/Unit is Required" SetFocusOnError="true" ValidationGroup="editt">
                                                </asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtQtyPerUnit"
                                                    ValidationGroup="editt" Type="Integer" ErrorMessage="Qty/Unit greater then zero"
                                                    MaximumValue="1000" MinimumValue="1"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                ALT spare code-1
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlAltSpareCode1" runat="server" CssClass="simpletxt1" Width="300px" onchange="SetDataInHidden(this.value,'hdnAltSpareCode1')"
                                                    ValidationGroup="editt">
                                                </asp:DropDownList>
                                                 <asp:HiddenField ID="hdnAltSpareCode1" runat="server" />
                                            </td>
                                            <td>
                                                Qty/Unit Of ALT-1
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQtyPerUnitOfAlt1" Width="50px" MaxLength="4" runat="server" CssClass="txtboxtxt"
                                                    ValidationGroup="editt"></asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtQtyPerUnitOfAlt1"
                                                    ValidationGroup="editt" Type="Integer" ErrorMessage="Qty/Unit must be greater than zero"
                                                    MaximumValue="1000" MinimumValue="1"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                ALT spare code-2
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlAltSpareCode2" runat="server" CssClass="simpletxt1" Width="300px" onchange="SetDataInHidden(this.value,'hdnAltSpareCode2')"
                                                    ValidationGroup="editt">
                                                </asp:DropDownList>
                                                 <asp:HiddenField ID="hdnAltSpareCode2" runat="server" />
                                            </td>
                                            <td>
                                                Qty/Unit Of ALT-2
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQtyPerUnitOfAlt2" Width="50px" runat="server" MaxLength="4" CssClass="txtboxtxt"
                                                    ValidationGroup="editt"></asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtQtyPerUnitOfAlt2"
                                                    ValidationGroup="editt" Type="Integer" ErrorMessage="Qty/Unit must be greater than zero"
                                                    MaximumValue="1000" MinimumValue="0"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                ALT spare code-3
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlAltSpareCode3" runat="server" CssClass="simpletxt1" Width="300px" onchange="SetDataInHidden(this.value,'hdnAltSpareCode3')"
                                                    ValidationGroup="editt">
                                                </asp:DropDownList>
                                                 <asp:HiddenField ID="hdnAltSpareCode3" runat="server" />
                                            </td>
                                            <td>
                                                Qty/Unit Of ALT-3
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQtyPerUnitOfAlt3" Width="50px" MaxLength="4" runat="server" CssClass="txtboxtxt"
                                                    ValidationGroup="editt"></asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="txtQtyPerUnitOfAlt3"
                                                    ValidationGroup="editt" Type="Integer" ErrorMessage="Qty/Unit must be greater than zero"
                                                    MaximumValue="1000" MinimumValue="0"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                ALT spare code-4
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlAltSpareCode4" runat="server" CssClass="simpletxt1" Width="300px" onchange="SetDataInHidden(this.value,'hdnAltSpareCode4')"
                                                    ValidationGroup="editt">
                                                </asp:DropDownList>
                                                 <asp:HiddenField ID="hdnAltSpareCode4" runat="server" />
                                            </td>
                                            <td>
                                                Qty/Unit Of ALT-4
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQtyPerUnitOfAlt4" Width="50px" MaxLength="4" runat="server" CssClass="txtboxtxt"
                                                    ValidationGroup="editt"></asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="txtQtyPerUnitOfAlt4"
                                                    Type="Integer" ErrorMessage="Qty/Unit must be greater than zero" MaximumValue="1000"
                                                    MinimumValue="1"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Status
                                            </td>
                                            <td colspan="3">
                                                <asp:RadioButtonList ID="rdoStatus" RepeatDirection="Horizontal" RepeatColumns="2"
                                                    runat="server">
                                                    <asp:ListItem Value="1" Text="Active" Selected="True">
                                                    </asp:ListItem>
                                                    <asp:ListItem Value="0" Text="In-Active">
                                                    </asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" height="10px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="center">
                                            <table border="0" cellpadding="1" cellspacing="0">
                                            <tr>
                                            <td><asp:Button Text="Add" Width="70px" CssClass="btn" ID="imgBtnAdd" runat="server"
                                                    CausesValidation="True" ValidationGroup="editt" OnClick="imgBtnAdd_Click" /></td>
                                           
                                              <td><asp:Button Text="Save" Width="70px" ID="imgBtnUpdate" CssClass="btn" runat="server"
                                                    CausesValidation="True" ValidationGroup="editt" OnClick="imgBtnUpdate_Click" /></td>
                                             <td><asp:Button ID="imgBtnCancel" Width="70px" runat="server" CausesValidation="false"
                                                    CssClass="btn" Text="Cancel" OnClick="imgBtnCancel_Click" /></td>
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
                                    <br />
                                      <asp:Label ID="lblTemp" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    
<script src="../../scripts/Jquery.1.7.2.min.js" type="text/javascript"></script>

<script type="text/javascript">
    $(document).ready(function() { });
    
    // set data in hidden field for ddl
    function SetDataInHidden(ddlvalue,HdnField) {
        $("[ID$=" + HdnField + "]").val(ddlvalue);
    }
    
    // Method for Fill data based on Gridview Row Selections
    function fillSpareBomDetails(e) {
        var filterResult = "";
        $("#ImgProgessBar").show();
        $(document).ready(function() {
       
        $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "SpareBOM.aspx/GetSpareDetails",
                data: '{ "spareBomId":' + e + '}',
                //data: "{}",
                dataType: "json",
                success: function(data) {
                    filterResult = $.parseJSON(data.d);
                    if (filterResult != undefined || filterResult != null || filterResult != "") {
                        // Set the textbox value of unit Qty, Alt Qty1...
                        
                        $("[ID$='imgBtnAdd']").hide();
                        $("[ID$='imgBtnUpdate']").show();

                        $("[ID$='txtQtyPerUnit']").val(filterResult[0]["QtyPerUnit"]);
                        if (filterResult[0]["QtyPerUnitOfAlt1"] == "0") {
                            $("[ID$='txtQtyPerUnitOfAlt1']").val("");
                        }
                        else {
                            $("[ID$='txtQtyPerUnitOfAlt1']").val(filterResult[0]["QtyPerUnitOfAlt1"]);
                        }

                        if (filterResult[0]["QtyPerUnitOfAlt2"] == "0") {
                            $("[ID$='txtQtyPerUnitOfAlt2']").val("");
                        }
                        else {
                            $("[ID$='txtQtyPerUnitOfAlt2']").val(filterResult[0]["QtyPerUnitOfAlt2"]);
                        }

                        if (filterResult[0]["QtyPerUnitOfAlt3"] == "0") {
                            $("[ID$='txtQtyPerUnitOfAlt3']").val("");
                        }
                        else {
                            $("[ID$='txtQtyPerUnitOfAlt3']").val(filterResult[0]["QtyPerUnitOfAlt3"]);
                        }

                        if (filterResult[0]["QtyPerUnitOfAlt4"] == "0") {
                            $("[ID$='txtQtyPerUnitOfAlt4']").val("");
                        }
                        else {
                            $("[ID$='txtQtyPerUnitOfAlt4']").val(filterResult[0]["QtyPerUnitOfAlt4"]);
                        }
                        var radio = $("[id$=rdoStatus] input[value=" + filterResult[0]["ActiveFlag"] + "]");
                        radio.attr("checked", "checked");

                        // Set the value in hidden field
                        $("[ID$='hdnSpareBOMId']").val(e)
                        $("[ID$='hdnproductLine']").val(filterResult[0]["ProductLine_Id"]);
                        $("[ID$='hdnProduct']").val(filterResult[0]["Product_Id"]);
                        $("[ID$='hdnSpare']").val(filterResult[0]["Spare_Id"]);
                        $("[ID$='hdnAltSpareCode1']").val(filterResult[0]["AltSpareCode1"]);
                        $("[ID$='hdnAltSpareCode2']").val(filterResult[0]["AltSpareCode2"]);
                        $("[ID$='hdnAltSpareCode3']").val(filterResult[0]["AltSpareCode3"]);
                        $("[ID$='hdnAltSpareCode4']").val(filterResult[0]["AltSpareCode4"]);
                        $("[ID$='lblMessage']").empty();

                        // Set Selected Value of Product Division
                        $("[ID$='ddlDivision']").val(filterResult[0]["ProductDivision_Id"]);
                        // Fill Product Line and set selected value
                        GetProductLine(filterResult[0]["ProductDivision_Id"], filterResult[0]["ProductLine_Id"]);
                        // Fill Product Details
                        GetProductDetails(filterResult[0]["ProductLine_Id"], filterResult[0]["Product_Id"]);
                        // Fill Spare and Alternate Spares details
                        GetSpareAltSpare(filterResult);
                    }
                },
                error: function(result) {
                    // alert("Spare details");
                }
            }); 
        });
       $("#ImgProgessBar").hide();
    }

    // Method For Binding Product Line Based on ProductDivision
    function GetProductLine(productDivId, productLineId) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "SpareBOM.aspx/GetProductLineDetails",
            data: '{ "intDivisionId":' + productDivId + '}',
            dataType: "json",
            success: function(plData) {
                $('[ID$=ddlProductLine]').empty();
                var pLine=$.parseJSON(plData.d);
                var plOptions="";
                $.each(pLine, function(key, val) {
                plOptions = plOptions + '<option value="' + key + '">' + val + '</option>';
                });
                $("[ID$=ddlProductLine]").append(plOptions);
                // Set Selected Product Line
                $("[ID$=ddlProductLine]").val(productLineId);
            },
            error: function(result) {
           // alert("Error in Product Line");
             }
        });
    }

    // Method For Binding Product Details Based on ProductDivision, Product Line

    function GetProductDetails(productLineId, productId) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "SpareBOM.aspx/GetProductDetails",
            data: '{ "intProductLine":' + productLineId + '}',
            dataType: "json",
            success: function(pData) {
            $('[ID$=ddlFGCode]').empty();
            $('[ID$=ddlFGCode]').append($("<option></option>").val(0).html("Select"));
                var pResult = $.parseJSON(pData.d);
                var pOptions = "";
                $.each(pResult, function(key, val) {
                pOptions = pOptions+'<option value="' + key + '">' + val + '</option>';
                });
                $("[ID$=ddlFGCode]").append(pOptions);
                // Set Selected Product Line
                $("[ID$=ddlFGCode]").val(productId);
            },
            error: function(result) {
            //alert("Error in Product Details");
              }
        });
    }

    // Method For Binding Spare and Alternet Spare
    function GetSpareAltSpare(filterResult) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "SpareBOM.aspx/GetSpare",
            data: '{ "intProductDivisionId":' + filterResult[0]["ProductDivision_Id"] + '}',
            dataType: "json",
            success: function(spareAltSpareData) {
                $('[ID$=ddlSpareCode]').empty();
                $('[ID$=ddlAltSpareCode1]').empty();
                $('[ID$=ddlAltSpareCode2]').empty();
                $('[ID$=ddlAltSpareCode3]').empty();
                $('[ID$=ddlAltSpareCode4]').empty();
                // Insert Select Index
                $('[ID$=ddlSpareCode]').append($("<option></option>").val(0).html("Select"));
                $('[ID$=ddlAltSpareCode1]').append($("<option></option>").val(0).html("Select"));
                $('[ID$=ddlAltSpareCode2]').append($("<option></option>").val(0).html("Select"));
                $('[ID$=ddlAltSpareCode3]').append($("<option></option>").val(0).html("Select"));
                $('[ID$=ddlAltSpareCode4]').append($("<option></option>").val(0).html("Select"));
                // Fill With Data
                var result = $.parseJSON(spareAltSpareData.d);
                var strOptions = "";
                $.each(result, function(key, val) {
                    strOptions = strOptions + '<option value="'+key+'">'+val+'</option>';
                });
                $("[ID$=ddlSpareCode]").append(strOptions);
                $("[ID$=ddlAltSpareCode1]").append(strOptions);
                $("[ID$=ddlAltSpareCode2]").append(strOptions);
                $("[ID$=ddlAltSpareCode3]").append(strOptions);
                $("[ID$=ddlAltSpareCode4]").append(strOptions);
                // Set Selected Product Line
                $("[ID$=ddlSpareCode]").val(filterResult[0]["Spare_Id"]);
                $("[ID$=ddlAltSpareCode1]").val(filterResult[0]["AltSpareCode1"]);
                $("[ID$=ddlAltSpareCode2]").val(filterResult[0]["AltSpareCode2"]);
                $("[ID$=ddlAltSpareCode3]").val(filterResult[0]["AltSpareCode3"]);
                $("[ID$=ddlAltSpareCode4]").val(filterResult[0]["AltSpareCode4"]);
            },
            error: function(result) {
            //alert("Error in Spare OR Alternate Spares Details"); 
            }
        }); 
    }

    // Method For Binding Spare and Alternet Spare on dropdownSelectedindex changed
    function GetSpareAltSpare_DropdownSelectedchane(productDivId) {
       // $("#ImgProgessBar").show();
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "SpareBOM.aspx/GetSpare",
            data: '{ "intProductDivisionId":' + productDivId + '}',
            dataType: "json",
            success: function(spareAltSpareData) {
                $('[ID$=ddlSpareCode]').empty();
                $('[ID$=ddlAltSpareCode1]').empty();
                $('[ID$=ddlAltSpareCode2]').empty();
                $('[ID$=ddlAltSpareCode3]').empty();
                $('[ID$=ddlAltSpareCode4]').empty();
                // Insert Select Index
                $('[ID$=ddlSpareCode]').append($("<option></option>").val(0).html("Select"));
                $('[ID$=ddlAltSpareCode1]').append($("<option></option>").val(0).html("Select"));
                $('[ID$=ddlAltSpareCode2]').append($("<option></option>").val(0).html("Select"));
                $('[ID$=ddlAltSpareCode3]').append($("<option></option>").val(0).html("Select"));
                $('[ID$=ddlAltSpareCode4]').append($("<option></option>").val(0).html("Select"));
                // Fill With Data
                var result = $.parseJSON(spareAltSpareData.d);
                var strOptions = "";
                $.each(result, function(key, val) {
                    strOptions = strOptions + '<option value="' + key + '">' + val + '</option>';
                });
                $("[ID$=ddlSpareCode]").append(strOptions);
                $("[ID$=ddlAltSpareCode1]").append(strOptions);
                $("[ID$=ddlAltSpareCode2]").append(strOptions);
                $("[ID$=ddlAltSpareCode3]").append(strOptions);
                $("[ID$=ddlAltSpareCode4]").append(strOptions);
                
            },
            error: function(result) {
            //alert("Error in Spare OR Alternate Spares Details"); 
            }
        }); //$("#ImgProgessBar").hide();
    }
        
        function CheckAllEmp(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=gvComm.ClientID %>");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }

    
</script>
</asp:Content>
