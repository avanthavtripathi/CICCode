<%@ Page Title="" Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master"  EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="SCPaymentMaster.aspx.cs" Inherits="SIMS_Admin_SCPaymentMaster"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="ContentSCPaymentMst" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script language="JavaScript" type="text/javascript">

        function CheckFromDate() {
            var days = 0;
            var difference = 0;
            var LoggedDateFrom;

            LoggedDateFrom = document.getElementById("ctl00_MainConHolder_txtFromDate").value;
            if (LoggedDateFrom) {
                today = new Date();
                var curr_date = today.getDate();
                var curr_month = today.getMonth();
                curr_month = curr_month + 1;
                var curr_year = today.getFullYear();
                today = curr_month + '/' + curr_date + '/' + curr_year;
                if (LoggedDateFrom < today) {

                   
                    document.getElementById("ctl00_MainConHolder_lblMessage").value = 'Effective date should be greater than or equal to current date';
                    document.getElementById("ctl00_MainConHolder_txtFromDate").value = '';
                    alert('Effective date should be greater than or equal to current date');                
                    return false;
                }

            }


        }
        //    </script>

    <asp:UpdatePanel ID="updatepnl" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportToExcel" />
        </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        ASC Payment Master
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["SIMSAjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right" style="padding-right: 10px">
                        <asp:RadioButtonList ID="rdoboth" RepeatDirection="Horizontal" RepeatColumns="3"
                            runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdoboth_SelectedIndexChanged">
                            <asp:ListItem Value="1" Text="Active" ></asp:ListItem>
                            <asp:ListItem Value="0" Text="In-Active"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Both" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" colspan="2">
                        <table border="0" width="100%">
                            <tr>
                                <td align="left" class="MsgTDCount">
                                    Total Number of Records :
                                    <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    Search For
                                    <asp:DropDownList ID="ddlSearch" runat="server" Width="130px" CssClass="simpletxt1">
                                        <asp:ListItem Text="Region" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Branch" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="SC Name" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Payment Mode" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Division" Value="4"></asp:ListItem>
                                    </asp:DropDownList>
                                    With
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="txtboxtxt" Width="100px" Text=""></asp:TextBox>
                                    <asp:Button Text="Go" Width="25px" CssClass="btn" ID="imgBtnGo" runat="server" CausesValidation="False"
                                        ValidationGroup="editt" OnClick="imgBtnGo_Click" />&nbsp;
                                        <asp:Button Text="Reset" Width="40px" CssClass="btn" ID="btnReset" 
                                        runat="server" CausesValidation="False"
                                        ValidationGroup="editt" onclick="btnReset_Click"  />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0">
                            <tr>
                                <td>
                                    <!-- Service Contractor Listing   -->
                                    <asp:GridView PageSize="100" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridLines="both" AllowSorting="True" DataKeyNames="PaymentRecID" 
                                        AutoGenerateColumns="false" ID="gvASCPayment" runat="server" Width="100%" HorizontalAlign="Left" 
                                        EnableSortingAndPagingCallbacks="True" OnSelectedIndexChanging="gvASCPayment_SelectedIndexChanging" 
                                        AllowPaging="true" OnPageIndexChanging="gvASCPayment_PageIndexChanging"
                                        OnSorting="gvASCPayment_Sorting" OnRowDataBound="gvASCPayment_RowDataBound">
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <asp:BoundField DataField="Region_Desc" SortExpression="Region_Desc" HeaderStyle-Width="60px"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Region">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Branch_Name" SortExpression="Branch_Name" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Branch">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SC_Name" SortExpression="SC_Name" HeaderStyle-Width="260px"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="SC Name">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Unit_Desc" SortExpression="Unit_Desc" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Division">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PaymentMode" SortExpression="PaymentMode" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="PaymentMode">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AscRate" SortExpression="AscRate" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Payment Amount">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="EffectiveFrom" SortExpression="EffectiveFrom" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Effective From">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="EffectiveTo" SortExpression="EffectiveTo" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Effective To">
                                            </asp:BoundField>  
                                                                                   
                                            <asp:TemplateField SortExpression="Active_Flag" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Active_Flag") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:BoundField DataField="TaxAllowed" SortExpression="TaxAllowed" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Tax Allowed">
                                            </asp:BoundField> 
                                            <asp:CommandField ShowSelectButton="True"   HeaderStyle-Width="60px"
                                                HeaderText="Edit">
                                                <HeaderStyle Width="60px" />
                                            </asp:CommandField>
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
                                    <!-- End Service Contractor Listing -->
                                </td>
                            </tr>
                            <tr>
                                <td>
                                <asp:Button Width="80px" Text="Export to Excel"  CssClass="btn" ID="btnExportToExcel" runat="server" onclick="btnExportToExcel_Click" />
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
                                                <asp:HiddenField ID="hdnPaymentRecID" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" align="right">
                                                Region<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlRegion" AutoPostBack="true" runat="server" CssClass="simpletxt1"
                                                    Width="175px" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvRegion" runat="server" SetFocusOnError="true"
                                                    ErrorMessage="Region is required." ControlToValidate="ddlRegion" ValidationGroup="editt"
                                                    ToolTip="Region is required." Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" align="right">
                                                Branch<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="simpletxt1" Width="175px"
                                                    OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvBranch" runat="server" SetFocusOnError="true"
                                                    ErrorMessage="Branch is required." ControlToValidate="ddlBranch" ValidationGroup="editt"
                                                    ToolTip="Branch is required." Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" align="right">
                                                Service Contractor<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DDlAsc" runat="server" CssClass="simpletxt1" Width="175px"
                                                    OnSelectedIndexChanged="DDlAsc_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvASC" runat="server" SetFocusOnError="true" ErrorMessage="Service Contractor is required."
                                                    ControlToValidate="DDlAsc" ValidationGroup="editt" ToolTip="Service Contractor is required."
                                                    Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" align="right">
                                                Divison<font color='red'>*</font>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlProductDivison" runat="server" Width="175px" CssClass="simpletxt1"
                                                    ValidationGroup="editt">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvDivison" runat="server" SetFocusOnError="true"
                                                    ErrorMessage="Divison is required." ControlToValidate="ddlProductDivison" ValidationGroup="editt"
                                                    ToolTip="Divison is required." Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" align="right">
                                                Payment Mode<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlpaymentmode" runat="server" Width="175px" CssClass="simpletxt1"
                                                    ValidationGroup="editt" AutoPostBack="true" 
                                                    onselectedindexchanged="ddlpaymentmode_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="L">LUMPSUM (L)</asp:ListItem>
                                                    <asp:ListItem Value="B">BILL WISE (B)</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvPaymentmode" runat="server" SetFocusOnError="true"
                                                    ErrorMessage="Payment Mode is required." ControlToValidate="ddlpaymentmode" ValidationGroup="editt"
                                                    ToolTip="Payment Mode is required." Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr id="trAscRate" runat="server" visible="false">
                                            <td align="right" width="30%">
                                                Payment</td>
                                            <td>
                                                <asp:TextBox ID="txtAscRate" runat="server" Text="0" Width="175px" CssClass="txtboxtxt"></asp:TextBox> 
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true"
                                                   ErrorMessage="Asc Rate required." ControlToValidate="txtAscRate" ValidationGroup="editt"
                                                  ToolTip="Asc Rate required." Display="Dynamic" InitialValue="">
                                                  </asp:RequiredFieldValidator>--%>
                                                 </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Effective From<font color='red'>*</font>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox runat="server" ID="txtFromDate" CssClass="txtboxtxt" ValidationGroup="editt"
                                                    MaxLength="10" />
                                                <cc1:CalendarExtender ID="CEFrom" runat="server" TargetControlID="txtFromDate"  >
                                                </cc1:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" SetFocusOnError="true"
                                                    ErrorMessage="Effective date is required." ControlToValidate="txtFromDate" ValidationGroup="editt"
                                                    ToolTip="Effective date is required." Display="Dynamic"></asp:RequiredFieldValidator>
                                                To
                                                <asp:TextBox runat="server" ID="txtToDate" CssClass="txtboxtxt" ValidationGroup="editt"
                                                    MaxLength="10" Text="12/31/9999" />
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate">
                                                </cc1:CalendarExtender>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtFromDate"
                                                    ControlToValidate="txtToDate" ErrorMessage="Effective to date should be greater than from date."
                                                    Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="editt"></asp:CompareValidator>
                                                <asp:Label ID="lblDateErr" runat="server" ForeColor="Red" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" align="right">
                                                Tax Allowed</td>
                                            <td>
                                                <asp:CheckBox ID="chktaxstatus" runat="server" Text="" /></td>
                                        </tr>
                                        <tr>
                                            <td align="right" width="30%">
                                                Status
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rdoStatus" runat="server" AutoPostBack="True" 
                                                    onselectedindexchanged="rdoStatus_SelectedIndexChanged" RepeatColumns="2" 
                                                    RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="True" Text="Active" Value="1">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </asp:ListItem>
                                                    <asp:ListItem Enabled="false" Text="In-Active" Value="0">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="25" align="left">
                                                &nbsp;
                                            </td>
                                            <td>
                                                <!-- For button portion update -->
                                                <table>
                                                    <tr>
                                                        <td align="right">
                                                            <%-- <asp:Button Text="Add" Width="70px" CssClass="btn" ID="imgBtnAdd" runat="server"
                                                                CausesValidation="True" ValidationGroup="editt" OnClick="imgBtnAdd_Click" />--%>
                                                            <asp:Button Text="Save" Width="70px" ID="btnSave" CssClass="btn" runat="server" CausesValidation="true"
                                                                 ValidationGroup="editt" OnClick="btnSave_Click" />
                                                         <%-- Comment 2 Nov 12 Bhawesh on Seema Request : btnSave OnClientClick="if(CheckFromDate()) return false;"--%>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="imgBtnCancel" Width="70px" runat="server" CausesValidation="false"
                                                                CssClass="btn" Text="Cancel" OnClick="imgBtnCancel_Click" />
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

    <script language="javascript" type="text/javascript">

        function validateAddress1(oSrc, args) {

            var x = (document.getElementById('ctl00_MainConHolder_txtAddOne').value);
            if (x.length > 100) {
                args.IsValid = false
            }
            else {

                args.IsValid = true
            }


        }

        function validateAddress2(oSrc, args) {

            var x = (document.getElementById('ctl00_MainConHolder_txtAddTwo').value);
            if (x.length > 100) {
                args.IsValid = false
            }
            else {
                args.IsValid = true
            }
        }
        
    </script>

</asp:Content>
