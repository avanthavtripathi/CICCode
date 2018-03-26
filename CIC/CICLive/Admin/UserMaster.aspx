<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="UserMaster.aspx.cs" Inherits="Admin_Screens_UserMaster" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        User Master
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                  <tr>
                    <td colspan="2" align="right" style="padding-right:10px">
                        <asp:RadioButtonList ID="rdoboth" RepeatDirection="Horizontal" RepeatColumns="3"
                            runat="server" AutoPostBack="True" 
                            onselectedindexchanged="rdoboth_SelectedIndexChanged">
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
                                    Total Number of Records : <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                </td>


                                <td align="right">
                                    Search For
                                    <asp:DropDownList ValidationGroup="editt1" ID="ddlSearch" runat="server" Width="130px"
                                        CssClass="simpletxt1">
                                        <asp:ListItem Text="User Id" Value="MSU.UserName"></asp:ListItem>
                                        <asp:ListItem Text="Name" Value="MSU.Name"></asp:ListItem>
                                        <asp:ListItem Text="User Type" Value="UserType_Name"></asp:ListItem>
                                        <asp:ListItem Text="Email" Value="Email"></asp:ListItem>
                                    </asp:DropDownList>
                                    With
                                    <asp:TextBox ID="txtSearch" ValidationGroup="editt1" runat="server" CssClass="txtboxtxt"
                                        Width="100px" Text=""></asp:TextBox>
                                    <asp:Button Text="Go" Width="25px" CssClass="btn" ID="imgBtnGo" runat="server" CausesValidation="False"
                                        ValidationGroup="editt1" OnClick="imgBtnGo_Click" />
                                </td>
                            </tr>
                          
                        </table>
                        <table width="100%" border="0">
                            <tr>
                                <td>
                                    <asp:GridView PageSize="15" PagerStyle-HorizontalAlign="center" RowStyle-CssClass="gridbgcolor"
                                        AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                                        GridLines="both" AllowPaging="True" AllowSorting="True" DataKeyNames="username"
                                        AutoGenerateColumns="False" ID="gvShowUser" runat="server" OnSelectedIndexChanging="gvShowUser_SelectedIndexChanging"
                                        OnPageIndexChanging="gvShowUser_PageIndexChanging" Width="100%" HorizontalAlign="Left"
                                        OnSorting="gvShowUser_Sorting">
                                        <Columns>
                                            <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                <HeaderStyle Width="40px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="username" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="User Id" SortExpression="username">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Name" ItemStyle-Width="170px" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Name" SortExpression="name">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="UserType_Name" ItemStyle-Width="170px" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="User Type" SortExpression="UserType_name">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="email" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Email" SortExpression="Email">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Active_Flag" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Status" SortExpression="Active_Flag">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TvtUserId" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Tvt User Id" SortExpression="TvtUserId">
                                            </asp:BoundField>
                                            <asp:CommandField ShowSelectButton="True" HeaderStyle-Width="60px" HeaderText="Edit">
                                                <HeaderStyle Width="60px" />
                                            </asp:CommandField>
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
                                    </asp:GridView>
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
                                            <td colspan="2">
                                                <asp:HiddenField ID="hdnuserName" runat="server" />
                                                <asp:HiddenField ID="hdnEditType" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 17%">
                                                User Type:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlUserType" runat="server" CssClass="simpletxt1" Width="175px"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" ID="rqfUserType" runat="server" InitialValue="0"
                                                    ControlToValidate="ddlUserType" ErrorMessage="User type is required." Display="Dynamic"
                                                    ToolTip="User type is required." ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 17%">
                                                Name:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" Text="" ID="txtName" runat="server" Width="170px"
                                                    ValidationGroup="editt" MaxLength="60" />
                                                <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator1" runat="server"
                                                    ControlToValidate="txtName" ErrorMessage="Name is required." Display="Dynamic"
                                                    ToolTip="Name is required." ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr id="trSCContactPerson" runat="server" visible="false">
                                            <td style="width: 17%">
                                                Contact Person
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtContactPerson" runat="server" CssClass="txtboxtxt" MaxLength="50"
                                                    Text="" Width="170px" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="true"
                                                    ErrorMessage="Contact Person is required." ControlToValidate="txtContactPerson"
                                                    ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr id="trSCAdd1" runat="server" visible="false">
                                            <td style="width: 17%">
                                                Address 1:</font><font color="red">*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtAddOne" runat="server" Width="170px" Text=""
                                                    MaxLength="100" TextMode="MultiLine" Height="28px" />
                                                <asp:RequiredFieldValidator ID="RFVAddOne" runat="server" SetFocusOnError="true"
                                                    ErrorMessage="Address One is required." ControlToValidate="txtAddOne" ValidationGroup="editt"
                                                    ToolTip="Address One is required."></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr id="trSCAdd2" runat="server" visible="false">
                                            <td style="width: 17%">
                                                </font> Address2:
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtAddTwo" runat="server" Width="170px" Text=""
                                                    MaxLength="100" ValidationGroup="editt" TextMode="MultiLine" />
                                            </td>
                                        </tr>
                                        <tr id="trSCPhone" runat="server" visible="false">
                                            <td style="width: 17%">
                                                Phone Number
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPhoneNo" runat="server" onkeypress="javascript:return checkNumberOnly(event);"
                                                    CssClass="txtboxtxt" MaxLength="11" Text="" Width="170px" />
                                            </td>
                                        </tr>
                                        <tr id="trSCMobile" runat="server" visible="false">
                                            <td style="width: 17%">
                                                Mobile Number
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtMobileNo" runat="server" CssClass="txtboxtxt" MaxLength="11"
                                                    Text="" Width="170px" onkeypress="javascript:return checkNumberOnly(event);" />
                                            </td>
                                        </tr>
                                        <tr id="trSCPrefence" runat="server" visible="false">
                                            <td style="width: 17%">
                                                Preference
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPrefence" runat="server" CssClass="txtboxtxt" MaxLength="1" Text="0"
                                                    Width="170px" />
                                            </td>
                                        </tr>
                                        <tr id="trSCSpecialremarks" runat="server" visible="false">
                                            <td style="width: 17%">
                                                Special Remarks
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSpecialRemarks" runat="server" CssClass="txtboxtxt" MaxLength="50"
                                                    Text="" Width="170px" />
                                            </td>
                                        </tr>
                                        <tr id="trSCFaxNo" runat="server" visible="false">
                                            <td style="width: 17%">
                                                Fax No
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFaxNo" runat="server" onkeypress="javascript:return checkNumberOnly(event);"
                                                    CssClass="txtboxtxt" MaxLength="11" Text="" Width="170px" />
                                            </td>
                                        </tr>
                                        <tr id="trSCRegion" runat="server" visible="false">
                                            <td style="width: 17%">
                                                Region:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlRegion" AutoPostBack="true" runat="server" CssClass="simpletxt1"
                                                    Width="175px" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:CompareValidator ID="CVRegion" runat="server" ControlToValidate="ddlRegion"
                                                    ErrorMessage="Region is required." Operator="NotEqual" SetFocusOnError="True"
                                                    ValueToCompare="Select" ValidationGroup="editt"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr id="trSCBranch" runat="server" visible="false">
                                            <td style="width: 17%">
                                                Branch:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="simpletxt1" Width="175px">
                                                    <asp:ListItem Text="Select" Value="Select" Selected="True"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:CompareValidator ID="CVBranch" runat="server" ControlToValidate="ddlBranch"
                                                    ErrorMessage="Branch is required." Operator="NotEqual" SetFocusOnError="True"
                                                    ValueToCompare="Select" ValidationGroup="editt"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr id="trSCState" runat="server" visible="false">
                                            <td style="width: 17%">
                                                State:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlState" AutoPostBack="true" runat="server" CssClass="simpletxt1"
                                                    Width="175px" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:CompareValidator ID="CVState" runat="server" ControlToValidate="ddlState" ErrorMessage="State code is required."
                                                    Operator="NotEqual" SetFocusOnError="True" ValueToCompare="Select" ValidationGroup="editt"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr id="trSCCity" runat="server" visible="false">
                                            <td style="width: 17%">
                                                City:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCity" runat="server" CssClass="simpletxt1" Width="175px">
                                                    <asp:ListItem Text="Select" Value="Select" Selected="True"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:CompareValidator ID="CVCity" runat="server" ControlToValidate="ddlCity" ErrorMessage="City code is required."
                                                    Operator="NotEqual" SetFocusOnError="True" ValueToCompare="Select" ValidationGroup="editt"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                         <tr id="trSCProductDivision" runat="server" visible="false">
                                            <td style="width: 17%">
                                                Product division:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlProductDivision" runat="server" CssClass="simpletxt1" Width="175px">
                                                    <asp:ListItem Text="Select" Value="Select" Selected="True"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlProductDivision" ErrorMessage="Product division is required."
                                                    Operator="NotEqual" SetFocusOnError="True" ValueToCompare="Select" ValidationGroup="editt"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr id="trSCWO" runat="server" visible="false">
                                            <td style="width: 17%">
                                                Weekly Off Day:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlWeeklyOffDay" runat="server" CssClass="simpletxt1" Width="175px">
                                                    <asp:ListItem Selected="True">Select</asp:ListItem>
                                                    <asp:ListItem>SUN</asp:ListItem>
                                                    <asp:ListItem>MON</asp:ListItem>
                                                    <asp:ListItem>TUE</asp:ListItem>
                                                    <asp:ListItem>WED</asp:ListItem>
                                                    <asp:ListItem>THU</asp:ListItem>
                                                    <asp:ListItem>FRI</asp:ListItem>
                                                    <asp:ListItem>SAT</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:CompareValidator ID="CVWeeklyOffDay" runat="server" ControlToValidate="ddlWeeklyOffDay"
                                                    ErrorMessage="Weekly Off Day is required." Operator="NotEqual" SetFocusOnError="True"
                                                    ValueToCompare="Select" ValidationGroup="editt"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr id="trUserName" runat="server">
                                            <td style="width: 17%">
                                                User Id:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" Text="" ID="txtUsername" runat="server" Width="170px"
                                                    ValidationGroup="editt" MaxLength="10" />
                                                <asp:RequiredFieldValidator SetFocusOnError="true" ID="rqvUserName" runat="server"
                                                    ControlToValidate="txtUsername" ErrorMessage="User Id is required." Display="Dynamic"
                                                    ToolTip="User Id is required." ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr id="trPassword" runat="server">
                                            <td style="width: 17%">
                                                Password:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPassword" runat="server" Text="" CssClass="txtboxtxt" Width="170px"
                                                    TextMode="Password"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" ID="PasswordRequired" runat="server"
                                                    ControlToValidate="txtPassword" ErrorMessage="Password is required." ToolTip="Password is required."
                                                    ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr id="trConPassword" runat="server">
                                            <td style="width: 17%">
                                                Confirm Password:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="txtboxtxt" Width="170px"
                                                    TextMode="Password"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="true" ID="ConfirmPasswordRequired" runat="server"
                                                    ControlToValidate="txtConfirmPassword" ErrorMessage="Confirm Password is required."
                                                    ToolTip="Confirm Password is required." ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="cvTxtConform" SetFocusOnError="true" CssClass="fieldName"
                                                    runat="server" ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword"
                                                    ErrorMessage="Password mismatch" ValidationGroup="editt">
                                                </asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr id="trEmail" runat="server">
                                            <td style="width: 17%">
                                                Email-Id:<%--<font color='red'>*</font>--%>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtUserEmailId" runat="server" Width="170px"
                                                    Text="" MaxLength="200" />
                                                <asp:RequiredFieldValidator ValidationGroup="editt" ControlToValidate="txtUserEmailId"
                                                    ID="reqEmailId" runat="server" ErrorMessage="Email is required."></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator SetFocusOnError="true" ID="RegEmail" runat="server"
                                                    ControlToValidate="txtUserEmailId" Display="dynamic" ErrorMessage="E-mail is not valid."
                                                    ValidationGroup="editt" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr id="trTvtUserid" runat="server" visible="false"><%--Added By Ashok Kumar on 10.02.2015--%>
                                            <td style="width: 17%">
                                                TVT User Id<font color='red'>*</font></td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtTvtUserId" runat="server" Width="170px"
                                                    Text="" MaxLength="4" />
                                                 <asp:RequiredFieldValidator Enabled="false" ValidationGroup="editt" ControlToValidate="txtTvtUserId"
                                                    ID="RqfTvtUserid" runat="server" ErrorMessage="Tvt User Id is required." Display="Dynamic"></asp:RequiredFieldValidator>
                                                    <asp:RangeValidator ID="RngTvtUserId" runat="server" ValidationGroup="editt" Enabled="false" MinimumValue="0"
                                                    MaximumValue="99999" ControlToValidate="txtTvtUserId" ErrorMessage="Numeric value is required." 
                                                    Display="Dynamic" Type="Integer"></asp:RangeValidator>
                                                    </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 17%">
                                                Status:
                                            </td>
                                            <td>
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
                                            <td align="left" colspan="2" style="padding-left: 200px;">
                                                <asp:Button ID="imgBtnAdd" ValidationGroup="editt" runat="server" CausesValidation="True"
                                                    CssClass="btn" OnClick="imgBtnAdd_Click" Text="Add" Width="70px" />
                                                <asp:Button ID="imgBtnUpdate" runat="server" CausesValidation="True" CssClass="btn"
                                                    OnClick="imgBtnUpdate_Click" Text="Save" ValidationGroup="editt" Width="70px" />
                                                <asp:Button ID="imgBtnCancel" runat="server" CausesValidation="false" CssClass="btn"
                                                    OnClick="imgBtnCancel_Click" Text="Cancel" Width="70px" />
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
