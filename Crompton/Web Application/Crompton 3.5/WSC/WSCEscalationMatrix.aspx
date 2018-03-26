<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="WSCEscalationMatrix.aspx.cs" Inherits="WSC_WSCEscalationMatrix" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Escalation Matrix
                    </td>
                </tr>
                <tr>
                    <td align="right" style="padding-right: 10px">
                        <asp:RadioButtonList ID="rdoboth" RepeatDirection="Horizontal" RepeatColumns="3"
                            runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdoboth_SelectedIndexChanged">
                            <asp:ListItem Value="1" Text="Active" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="0" Text="In-Active"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Both"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center">
                        <table border="0" width="100%">
                            <tr>
                                <td align="left" class="MsgTDCount">
                                    Total Number of Records :
                                    <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    Search For
                                    <asp:DropDownList ID="ddlSearch" runat="server" Width="140px" CssClass="simpletxt1">
                                        <asp:ListItem Text="UserId" Value="To_UserId"></asp:ListItem>
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
                                    <table align="center" width="930" border="0" cellspacing="0" cellpadding="0" class="fieldNamewithbgcolor">
                                        <tr>
                                            <td width="430" style="border: 1px solid #ACA899">
                                                <font color="#ffffff">First Mailer</font>
                                            </td>
                                            <td width="235" style="border: 1px solid #ACA899">
                                                <font color="#ffffff">ESCALATION-1</font>
                                            </td>
                                            <td width="148" style="border: 1px solid #ACA899">
                                                <font color="#ffffff">ESCALATION-2</font>
                                            </td>
                                            <td width="104" style="border: 1px solid #ACA899">
                                                <font color="#ffffff">&nbsp;</font>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="True"
                                        AllowSorting="True" DataKeyNames="EscalationId" AutoGenerateColumns="False" ID="gvComm"
                                        runat="server" OnPageIndexChanging="gvComm_PageIndexChanging" Width="100%" OnSelectedIndexChanging="gvComm_SelectedIndexChanging"
                                        HorizontalAlign="Left" OnRowDataBound="gvComm_RowDataBound" Visible="true" OnSorting="gvComm_Sorting">
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                <HeaderStyle Width="40px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Region_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Region" SortExpression="Region_Desc">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="State_Desc" SortExpression="State_Desc" HeaderStyle-Width="60px"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="State">
                                                <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Unit_Desc" SortExpression="Unit_Desc" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Product">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="To_UserName" SortExpression="To_UserName" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="To User Name">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CC_UserName" SortExpression="CC_UserName" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="CC User Name">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="To_UserName1" SortExpression="To_UserName1" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="To User Name1">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CC_UserName1" SortExpression="CC_UserName1" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="CC User Name1">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ElaspTimeMatrix1" SortExpression="ElaspTimeMatrix1" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="ElaspTime">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="To_UserName2" SortExpression="To_UserName2" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="To User Name2">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ElaspTimeMatrix2" SortExpression="ElaspTimeMatrix2" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="ElaspTime">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <%-- <asp:BoundField DataField="CC_UserName2" SortExpression="CC_UserName2" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="CC User Name2">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>--%>
                                            <asp:BoundField DataField="Active_Flag" SortExpression="Active_Flag" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Status">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:CommandField ShowSelectButton="True" HeaderStyle-Width="60px" HeaderText="Edit">
                                                <HeaderStyle Width="60px" />
                                            </asp:CommandField>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Center" />
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
                                    <!-- End Product Listing -->
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td width="100%" align="left" class="bgcolorcomm">
                                    <table width="98%" border="0" cellpadding="1" cellspacing="0">
                                        <tr>
                                            <td height="1" width="24%">
                                            </td>
                                            <td width="20%">
                                            </td>
                                            <td width="18%">
                                            </td>
                                            <td width="38%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                                <font color='red'>*</font>
                                                <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:HiddenField ID="hndEscalationId" runat="server" />
                                            </td>
                                            <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                                                <asp:UpdateProgress AssociatedUpdatePanelID="updatepnl" ID="UpdateProgress2" runat="server">
                                                    <ProgressTemplate>
                                                        <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" height="10">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Region: <font color='red'>*</font>
                                            </td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="ddlRegion" runat="server" CssClass="simpletxt1" Width="175px"
                                                    OnSelectedIndexChanged="ddlRegion_SelectIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlRegion"
                                                    ErrorMessage="Region is required" SetFocusOnError="true" ValidationGroup="editt"
                                                    InitialValue="00" Display="None"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                State: <font color='red'>*</font>
                                            </td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="ddlState" CssClass="simpletxt1" Width="175" runat="server">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="00"
                                                    SetFocusOnError="true" ControlToValidate="ddlState" ErrorMessage="State is required"
                                                    ValidationGroup="editt" Display="None"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Product Division: <font color='red'>*</font>
                                            </td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="ddlProductDiv" CssClass="simpletxt1" Width="175" runat="server">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="00"
                                                    SetFocusOnError="true" ControlToValidate="ddlProductDiv" ValidationGroup="editt"
                                                    ErrorMessage="ProductDivision is required" Display="None"></asp:RequiredFieldValidator>
                                                <%-- AutoPostBack="True" OnSelectedIndexChanged="ddlProductDiv_SelectedIndexChanged">--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td height="1" width="24%">
                                                        </td>
                                                        <td width="20%">
                                                        </td>
                                                        <td width="18%">
                                                        </td>
                                                        <td width="38%">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" height="10">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" bgcolor="#60A3AC" height="20" style="padding-left: 10px; color: #ffffff;
                                                            font-size: 12px">
                                                            <b>First Mailer</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" height="10">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            To User(EIC-MTO): <font color='red'>*</font>
                                                        </td>
                                                        <td>
                                                            <asp:ListBox ID="lstToUser" runat="server" SelectionMode="Multiple" CssClass="simpletxt1"
                                                                Width="175" Height="300px"></asp:ListBox>
                                                            <asp:RequiredFieldValidator ID="RFLSTToUser" runat="server" SetFocusOnError="true"
                                                                ControlToValidate="lstToUser" Display="None" ValidationGroup="editt" ErrorMessage="Please select EIC-MTO User"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td valign="top" style="padding-left: 20px">
                                                            CC User(RSH-MTO):<%--<font color='red'>*</font>--%>
                                                        </td>
                                                        <td>
                                                            <asp:ListBox ID="lstCCUser" runat="server" SelectionMode="Multiple" CssClass="simpletxt1"
                                                                Height="300px" Width="175"></asp:ListBox>
                                                            <%--<asp:RequiredFieldValidator ID="RFLSTCCUser" runat="server" SetFocusOnError="true"
                                                                ControlToValidate="lstCCUser" Display="None" ValidationGroup="editt" ErrorMessage="Please select RSH User"></asp:RequiredFieldValidator>--%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                                    <tr>
                                                        <td height="1" width="24%">
                                                        </td>
                                                        <td width="20%">
                                                        </td>
                                                        <td width="18%">
                                                        </td>
                                                        <td width="38%">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" bgcolor="#60A3AC" height="20" style="padding-left: 10px; color: #ffffff;
                                                            font-size: 12px">
                                                            <b>ESCALATION 1</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" height="10">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            To User(DS Exe):
                                                            <%--<font color='red'>*</font>--%>
                                                        </td>
                                                        <td>
                                                            <asp:ListBox ID="lstToUser1" runat="server" SelectionMode="Multiple" CssClass="simpletxt1"
                                                                Height="300px" Width="175"></asp:ListBox>
                                                            <%-- <asp:RequiredFieldValidator ID="RFLSTTOUSER1" runat="server" SetFocusOnError="true"
                                                                ControlToValidate="lstToUser1" Display="None" ValidationGroup="editt" ErrorMessage="Please select DS Exe. User"></asp:RequiredFieldValidator>--%>
                                                        </td>
                                                        <td valign="top" style="padding-left: 20px">
                                                            CC User(AIMM-MTO):<%--<font color='red'>*</font>--%>
                                                        </td>
                                                        <td>
                                                            <asp:ListBox ID="lstCCUser1" runat="server" SelectionMode="Multiple" CssClass="simpletxt1"
                                                                Height="300px" Width="175"></asp:ListBox>
                                                            <%--<asp:RequiredFieldValidator ID="RFLSTCCUser1" runat="server" SetFocusOnError="true"
                                                                ControlToValidate="lstCCUser1" Display="None" ValidationGroup="editt" ErrorMessage="Please select AIMM-MTO User"></asp:RequiredFieldValidator>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Duration(no. of days)<%--*:<font color='red'>*</font>--%>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox CssClass="txtboxtxt" ID="txtElaspTimeMatrix1" MaxLength="3" runat="server"
                                                                Width="40px" Text="" />
                                                            <%--<asp:RequiredFieldValidator ID="reqvalDeptIname" runat="server" SetFocusOnError="true"
                                                                ErrorMessage="Elapse time is required for Escalation1" ControlToValidate="txtElaspTimeMatrix1"
                                                                ValidationGroup="editt" Display="None"></asp:RequiredFieldValidator>
                                                            <asp:CompareValidator ID="vcmpday" runat="server" ControlToValidate="txtElaspTimeMatrix1"
                                                                ValueToCompare="0" Type="Integer" Operator="GreaterThan" ValidationGroup="editt"
                                                                ErrorMessage="Elapse time must be greater than zero" Display="None">
                                                            </asp:CompareValidator>--%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <table width="98%" border="0" cellpadding="1" cellspacing="0">
                                                    <tr>
                                                        <td height="1" width="24%">
                                                        </td>
                                                        <td width="20%">
                                                        </td>
                                                        <td width="18%">
                                                        </td>
                                                        <td width="38%">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" bgcolor="#60A3AC" height="20" style="padding-left: 10px; color: #ffffff;
                                                            font-size: 12px">
                                                            <b>ESCALATION 2</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" height="10">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            To User(DH-MTO):
                                                            <%--<font color='red'>*</font>--%>
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:ListBox ID="lstToUser2" runat="server" SelectionMode="Multiple" CssClass="simpletxt1"
                                                                Height="100px" Width="175"></asp:ListBox>
                                                            <%--<asp:RequiredFieldValidator ID="RFLSTTOUser2" runat="server" SetFocusOnError="true" Display="None"
                                                                ControlToValidate="lstToUser2" ValidationGroup="editt" ErrorMessage="Please select DH-MTO User"></asp:RequiredFieldValidator>--%>
                                                        </td>
                                                        <%--   <td width="30%" valign="top">
                                                            CC User:<font color='red'>*</font>
                                                        </td>
                                                        <td>
                                                            <asp:ListBox ID="lstCCUser2" runat="server" SelectionMode="Multiple" Width="175">
                                                            </asp:ListBox>
                                                        </td>--%>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Duration(No. of days)<%--*:<font color='red'>*</font>--%>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox CssClass="txtboxtxt" ID="txtElaspTimeMatrix2" MaxLength="3" runat="server"
                                                                Width="40px" Text="" />
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" SetFocusOnError="true"
                                                                ErrorMessage="Elapse time is required for Escalation2" ControlToValidate="txtElaspTimeMatrix2"
                                                                ValidationGroup="editt" Display="None"></asp:RequiredFieldValidator>
                                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtElaspTimeMatrix2"
                                                                ValueToCompare="0" Type="Integer" Operator="GreaterThan" ValidationGroup="editt"
                                                                ErrorMessage="Elapse time must be greater than zero" Display="None">
                                                            </asp:CompareValidator>
                                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtElaspTimeMatrix2"
                                                                Type="String" Operator="GreaterThan" ValidationGroup="editt" ErrorMessage="Elapse time for Escalation2 must be greater than Escalation1"
                                                                ControlToCompare="txtElaspTimeMatrix1" Display="None">
                                                            </asp:CompareValidator>--%>
                                                        </td>
                                                    </tr>
                                                </table>
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
                                            <td height="25" align="left">
                                                &nbsp;
                                            </td>
                                            <td colspan="3">
                                                <!-- For button portion update -->
                                                <table>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Button Text="Add" Width="70px" CssClass="btn" ID="imgBtnAdd" runat="server"
                                                                CausesValidation="True" ValidationGroup="editt" OnClick="imgBtnAdd_Click" />
                                                            <asp:Button Text="Save" Width="70px" ID="imgBtnUpdate" CssClass="btn" runat="server"
                                                                CausesValidation="True" ValidationGroup="editt" OnClick="imgBtnUpdate_Click" />
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
                            <tr>
                                <td>
                                    <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="editt" ShowMessageBox="true"
                                        ShowSummary="false" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
