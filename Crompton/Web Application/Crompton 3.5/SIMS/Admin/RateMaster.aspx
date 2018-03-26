<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true"
    CodeFile="RateMaster.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="SIMS_Admin_RateMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function SearchPostBack()
        {
            var btn = document.getElementById('ctl00_MainConHolder_imgBtnGo');
            if (btn) btn.click();            
        }
    </script>

    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Rate Master
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
                    <td align="right" colspan="3">
                        <asp:LinkButton ID="LnkbRateMasterForASC" runat="server" CausesValidation="false"
                            Text="Rate Master For ASC" ValidationGroup="editt" Width="122px" Visible="true" />
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
                <caption>
                    <tr>
                        <td align="center" colspan="2" style="padding: 10px">
                            <table border="0" width="100%">
                                <tr>
                                    <td align="left" class="MsgTDCount">
                                        Total Number of Records :
                                        <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                    </td>
                                    <td align="right"><asp:Button ID="btnExcelDownload" Text="Excel Download" runat="server" 
                                            CausesValidation="False" CssClass="btn"
                                            ValidationGroup="editt" onclick="btnExcelDownload_Click" /></td>
                                    <td align="right">
                                        Search For
                                        <asp:DropDownList ID="ddlSearch" runat="server" CssClass="simpletxt1" Width="130px">
                                            <asp:ListItem Text="Product Division" Value="Unit_Desc"></asp:ListItem>
                                            <asp:ListItem Text="Activity" Value="Activity_Description"></asp:ListItem>
                                            <asp:ListItem Text="Parameter" Value="Parameter_Description"></asp:ListItem>
                                            <asp:ListItem Text="Possible Value-1" Value="Possibl_Value"></asp:ListItem>
                                        </asp:DropDownList>
                                        With
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="txtboxtxt" Text="" Width="100px"></asp:TextBox>
                                        <asp:Button ID="imgBtnGo" runat="server" CausesValidation="False" CssClass="btn"
                                            OnClick="imgBtnGo_Click" Text="Go" ValidationGroup="editt" Width="25px" />
                                    </td>
                                </tr>
                            </table>
                            <table border="0" width="100%">
                                <tr>
                                    <td>
                                        <!-- Product Line Listing   -->
                                        <asp:GridView ID="gvComm" runat="server" AllowPaging="True" AllowSorting="True" AlternatingRowStyle-CssClass="fieldName"
                                            AutoGenerateColumns="False" DataKeyNames="ActivityParameter_SNo" GridLines="both"
                                            PageSize="10" Width="100%" HorizontalAlign="Left" OnPageIndexChanging="gvComm_PageIndexChanging"
                                            OnSelectedIndexChanging="gvComm_SelectedIndexChanging" EnableSortingAndPagingCallbacks="True"
                                            OnSorting="gvComm_Sorting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sno" HeaderStyle-Width="40px">
                                                <ItemTemplate>
                                                <%# Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Unit_Desc" SortExpression="Unit_Desc" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Product Division" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Activity_Code" SortExpression="Activity_Code" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Activity Code" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Activity_Description" SortExpression="Activity_Description"
                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Activity" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Parameter_Code1" SortExpression="Parameter_Code1" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="60px" HeaderText="Parameter-1" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Possible_Value1" SortExpression="Possible_Value1" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="70px" HeaderText="Possible Value-1" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Parameter_Code2" SortExpression="Parameter_Code2" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Parameter-2" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Possible_Value2" SortExpression="Possible_Value2" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="70px" HeaderText="Possible Value-2" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Parameter_Code3" SortExpression="Parameter_Code3" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Parameter-3" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Possible_Value3" SortExpression="Possible_Value3" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="70px" HeaderText="Possible Value-3" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Parameter_Code4" SortExpression="Parameter_Code4" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Parameter-4" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Possible_Value4" SortExpression="Possible_Value4" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="70px" HeaderText="Possible Value-4" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="UOM" SortExpression="UOM" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="UOM" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Rate" SortExpression="Rate" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Rate" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SC_Name" SortExpression="SC_Name" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="SC Name" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                
                                                <asp:BoundField DataField="Actual" SortExpression="Actual" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Actual" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                
                                                <asp:BoundField DataField="Active_Flag" SortExpression="Active_Flag" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Status" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:CommandField HeaderText="Edit" ShowSelectButton="True">
                                                    <HeaderStyle />
                                                </asp:CommandField>
                                            </Columns>
                                            <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                            <AlternatingRowStyle CssClass="fieldName" />
                                            <RowStyle CssClass="gridbgcolor" />
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
                                        </asp:GridView>
                                        <!-- End ProductLine Listing -->
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 17px">
                                    <a href="../BulkUpload/RateMasterUpdate.xls">Download Excel Formate For Rate Modification</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="bgcolorcomm" width="100%">
                                        <table border="0" width="100%">
                                            <tr align="right">
                                                <td colspan="4" align='<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>'>
                                                    <font color="red">*</font>
                                                    <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <asp:HiddenField ID="hdnProductLineSNo" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="20%">
                                                    Product Division<font color="red">*</font>
                                                </td>
                                                <td colspan="3">
                                                    <asp:DropDownList ID="ddlUnitSno" runat="server" Width="175" CssClass="simpletxt1"
                                                        ValidationGroup="editt" AutoPostBack="True" OnSelectedIndexChanged="ddlUnitSno_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvDdlProductDiv" runat="server" ControlToValidate="ddlUnitSno"
                                                        InitialValue="0" ErrorMessage="Product Division is required." SetFocusOnError="true"
                                                        ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Activity<font color="red">*</font>
                                                </td>
                                                <td colspan="3">
                                                    <asp:DropDownList ID="ddlActivityCode" runat="server" CssClass="simpletxt1" ValidationGroup="editt"
                                                        Width="175" AutoPostBack="True" OnSelectedIndexChanged="ddlActivityCode_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvDdlActivityCode" runat="server" ControlToValidate="ddlActivityCode"
                                                        InitialValue="0" ErrorMessage="Activity code is required." SetFocusOnError="true"
                                                        ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Parameter-1<font color="red">*</font>
                                                </td>
                                                <td style="width: 183px">
                                                    <asp:DropDownList AutoPostBack="true" ID="ddlParamCode1" runat="server" CssClass="simpletxt1"
                                                        ValidationGroup="editt" Width="175" OnSelectedIndexChanged="ddlParamCode1_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    Possible Value
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPossibleValue1" runat="server" CssClass="simpletxt1" ValidationGroup="editt">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvDdlParamCode1" runat="server" ControlToValidate="ddlParamCode1"
                                                        InitialValue="0" ErrorMessage="Parameter-1 is required." SetFocusOnError="true"
                                                        ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Parameter-2
                                                </td>
                                                <td style="width: 183px">
                                                    <asp:DropDownList AutoPostBack="true" ID="ddlParamCode2" runat="server" CssClass="simpletxt1"
                                                        ValidationGroup="editt" Width="175" OnSelectedIndexChanged="ddlParamCode2_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    Possible Value
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPossibleValue2" runat="server" CssClass="simpletxt1" ValidationGroup="editt">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Parameter-3
                                                </td>
                                                <td style="width: 183px">
                                                    <asp:DropDownList AutoPostBack="true" ID="ddlParamCode3" runat="server" CssClass="simpletxt1"
                                                        ValidationGroup="editt" Width="175" OnSelectedIndexChanged="ddlParamCode3_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    Possible Value
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPossibleValue3" runat="server" CssClass="simpletxt1" ValidationGroup="editt">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Parameter-4
                                                </td>
                                                <td style="width: 183px">
                                                    <asp:DropDownList AutoPostBack="true" ID="ddlParamCode4" runat="server" CssClass="simpletxt1"
                                                        ValidationGroup="editt" Width="175" OnSelectedIndexChanged="ddlParamCode4_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    Possible Value
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPossibleValue4" runat="server" CssClass="simpletxt1" ValidationGroup="editt">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    UOM<font color="red">*</font>
                                                </td>
                                                <td colspan="3">
                                                    <asp:DropDownList ID="ddlUOM" runat="server" CssClass="simpletxt1" Width="175px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Rate<font color="red">*</font>
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtRate" runat="server" CssClass="txtboxtxt" MaxLength="10" Text=""
                                                        Width="170px" />
                                                    <asp:RequiredFieldValidator ID="rfvRate" runat="server" ControlToValidate="txtRate"
                                                        ErrorMessage="Rate is required." SetFocusOnError="true" ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtRate"
                                                            ErrorMessage="Proper Rate is Required." ValidationExpression="^([0-9]*|\d*\.\d{1}?\d*)$" ValidationGroup="editt"></asp:RegularExpressionValidator>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    SC Name
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtSCName" runat="server" CssClass="txtboxtxt" Enabled="false" MaxLength="100"
                                                        Text="" Width="170px" />
                                                    <asp:HiddenField ID="hdnScNo" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Actual
                                                </td>
                                                <td colspan="3">
                                                    <asp:CheckBox ID="ChkActual" runat="server" />
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Status
                                                </td>
                                                <td colspan="3">
                                                    <asp:RadioButtonList ID="rdoStatus" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                                                        <asp:ListItem Selected="True" Text="Active" Value="1">
                                                        </asp:ListItem>
                                                        <asp:ListItem Text="In-Active" Value="0">
                                                        </asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" height="25">
                                                    &nbsp;
                                                </td>
                                                <td colspan="3">
                                                    <!-- For button portion update -->
                                                    <table>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Button ID="imgBtnAdd" runat="server" CausesValidation="True" CssClass="btn"
                                                                    OnClick="imgBtnAdd_Click" Text="Add" ValidationGroup="editt" Width="70px" />
                                                                <asp:Button ID="imgBtnUpdate" runat="server" CausesValidation="True" CssClass="btn"
                                                                    OnClick="imgBtnUpdate_Click" Text="Save" ValidationGroup="editt" Width="70px" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="imgBtnCancel" runat="server" CausesValidation="false" CssClass="btn"
                                                                    OnClick="imgBtnCancel_Click" Text="Cancel" Width="70px" />
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
                </caption>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcelDownload" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
