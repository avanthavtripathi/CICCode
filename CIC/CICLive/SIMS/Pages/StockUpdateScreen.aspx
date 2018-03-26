<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true"
    CodeFile="StockUpdateScreen.aspx.cs" Inherits="SIMS_Pages_StockUpdateScreen" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script language="javascript" type="text/javascript">
function getNumeric_only(strvalue)
 {
   if(!(event.keyCode==46||event.keyCode==48||event.keyCode==49||event.keyCode==50||event.keyCode==51||event.keyCode==52||event.keyCode==53||event.keyCode==54||event.keyCode==55||event.keyCode==56||event.keyCode==57))
   		{	
   			event.returnValue=false;	
   			alert("Please enter numbers only.");
   		
   		}
    
 }
    </script>

    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred" style="width: 968px">
                      ASC Stock Update Screen
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
                        <a href="../BulkUpload/bulkUploadStockMaster.xls" target="_blank">Stock Bulk Upload</a>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right" style="padding-right: 10px">
                        <%--FOR FILTERING RECORD ON THE BASIS OF ACTIVE AND INACTIVE--%>
                        <asp:Button Visible="false" Text="Go" Width="25px" CssClass="btn" ID="imgBtnGo" runat="server"
                            CausesValidation="False" ValidationGroup="editt1" OnClick="imgBtnGo_Click" />
                        <%--FILTERING RECORD ON THE BASIS OF ACTIVE AND INACTIVE END--%>
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
                                    <asp:DropDownList ID="ddlSearch" runat="server" Width="130px" CssClass="simpletxt1">
                                        <asp:ListItem Text="Service Contractor" Value="MSC.SC_Name"></asp:ListItem>
                                        <asp:ListItem Text="Division" Value="MU.Unit_Desc"></asp:ListItem>
                                        <asp:ListItem Text="Spare" Value="BS.SAP_Desc"></asp:ListItem>
                                        <asp:ListItem Text="Location" Value="ASL.Loc_Name"></asp:ListItem>
                                        <asp:ListItem Text="Invoice No" Value="SL.SAP_Invoice_No"></asp:ListItem>
                                        <%-- <asp:ListItem Text="Stock Quantity" Value="SL.Qty"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                    With
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="txtboxtxt" Width="100px" Text=""></asp:TextBox>
                                    <asp:Button Text="Go" Width="25px" CssClass="btn" ID="Button1" runat="server" CausesValidation="False"
                                        ValidationGroup="editt" OnClick="imgBtnGo_Click" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0">
                            <tr>
                                <td>
                                    <!-- Batch Listing -->
                                    <asp:GridView RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="True"
                                        AllowSorting="True" DataKeyNames="Storage_Loc_Id" AutoGenerateColumns="False"
                                        ID="gvComm" runat="server" OnPageIndexChanging="gvComm_PageIndexChanging" Width="100%"
                                        OnSelectedIndexChanging="gvComm_SelectedIndexChanging" HorizontalAlign="Left"
                                        OnSorting="gvComm_Sorting" OnRowCommand="gvComm_RowCommand">
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                <HeaderStyle Width="40px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SC_Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Service Contractor" SortExpression="SC_Name">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Unit_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Division" SortExpression="Unit_Desc">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SAP_Code" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Spare" SortExpression="SAP_Code">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Loc_Name" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Location" SortExpression="Loc_Name">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SAP_Invoice_No" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Invoice No" SortExpression="SAP_Invoice_No">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Qty" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Stock Quantity" SortExpression="Qty">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <%-- <asp:BoundField DataField="Active_Flag" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Status" SortExpression="Active_Flag">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>--%>
                                            <%--<asp:CommandField ShowSelectButton="True" HeaderStyle-Width="60px" HeaderText="Edit">
                                                    <HeaderStyle Width="60px" />
                                                </asp:CommandField>--%>
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
                                    <!-- End Batch Listing -->
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td width="100%" align="left" class="bgcolorcomm">
                                    <table width="98%" border="0">
                                        <tr>
                                            <td colspan="2" align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                                <font color='red'>*</font>
                                                <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="5%">
                                                Service Contactor:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlASCCode" runat="server" CssClass="simpletxt1" Width="250px"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlASCCode_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="ReqddlASCCode" InitialValue="0" runat="server" Display="Dynamic"
                                                    SetFocusOnError="true" ErrorMessage="Service Contactor is required." ControlToValidate="ddlASCCode"
                                                    ValidationGroup="editt" ToolTip="Service Contactor is required."></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Division:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlDivision" runat="server" CssClass="simpletxt1" 
                                                    AutoPostBack="True" 
                                                    OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" Width="250px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="ReqddlDivision" InitialValue="0" runat="server" SetFocusOnError="true"
                                                    ErrorMessage="Division is required." ControlToValidate="ddlDivision" ValidationGroup="editt"
                                                    ToolTip="Division is required."></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                        <td>
                                          
                                                Search Spares:    
                                        </td>
                                        <td>
                                        <asp:TextBox ID="txtFindSpare" ValidationGroup="ProductRef" CssClass="txtboxtxt" runat="server"
                                                    Width="130" CausesValidation="True"></asp:TextBox>
                                                <asp:Button ID="btnGoSpare" runat="server"  ValidationGroup="ProductRef" Width="20px" Text="Go" CssClass="btn" OnClick="btnGoSpare_Click" />
                                      
                                        </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Spare:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlSpareCode" runat="server" CssClass="simpletxt1" 
                                                    Width="250px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="0" runat="server"
                                                    SetFocusOnError="true" ErrorMessage="Spare is required." ControlToValidate="ddlSpareCode"
                                                    ValidationGroup="editt" ToolTip="Spare is required."></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Location:<font color='red'>*</font>
                                            </td>
                                            <td style="width: 31%">
                                                <asp:DropDownList ID="ddlLocation" runat="server" CssClass="simpletxt1" Width="250px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="ReqddlLocation" InitialValue="0" runat="server" SetFocusOnError="true"
                                                    ErrorMessage="Location is required." ControlToValidate="ddlLocation" ValidationGroup="editt"
                                                    ToolTip="Location is required."></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Invoice No:
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtCGInvoice" MaxLength="15" runat="server"
                                                    Width="244px" Text="" />
                                                <%--<asp:RequiredFieldValidator SetFocusOnError="true" ID="ReqtxtCGInvoice" runat="server"
                                                    ControlToValidate="txtCGInvoice" ErrorMessage="Invoice No is required." Display="Dynamic"
                                                    ToolTip="Invoice No is required." ValidationGroup="editt"></asp:RequiredFieldValidator>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Stock Quantity:<font color='red'>*</font>
                                            </td>
                                            <td style="width: 31%">
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtCurrentStock" MaxLength="10" runat="server"
                                                    Width="244px" Text="" />
                                                <cc1:FilteredTextBoxExtender ID="FilCurrentStock" runat="server" FilterType="Custom"
                                                    TargetControlID="txtCurrentStock" ValidChars="0123456789.">
                                                </cc1:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" ID="ReqtxtCurrentStock" runat="server"
                                                    ControlToValidate="txtCurrentStock" ErrorMessage="Stock Quantity is required."
                                                    Display="Dynamic" ToolTip="Stock Quantity is required." ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Button ID="imgBtnAdd" runat="server" CausesValidation="True" 
                                                                CssClass="btn" OnClick="imgBtnAdd_Click" Text="Add" ValidationGroup="editt" 
                                                                Width="70px" />
                                                            <asp:Button ID="imgBtnUpdate" runat="server" CausesValidation="True" 
                                                                CssClass="btn" OnClick="imgBtnUpdate_Click" Text="Save" ValidationGroup="editt" 
                                                                Width="70px" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="imgBtnCancel" runat="server" CausesValidation="false" 
                                                                CssClass="btn" OnClick="imgBtnCancel_Click" Text="Cancel" Width="70px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        <tr>
                                            <td colspan="2">
                                                <asp:HiddenField ID="hdnStockUpdateId" runat="server" />
                                                <asp:HiddenField ID="hdnASCId" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
