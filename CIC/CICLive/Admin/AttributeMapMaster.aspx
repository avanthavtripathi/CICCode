<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="AttributeMapMaster.aspx.cs" Inherits="Admin_AttributeMapMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Attribute Map Master
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
                                        <asp:ListItem Text="Attribute Desc" Value="Attribute_Desc"></asp:ListItem>
                                        <asp:ListItem Text="Attribute Default Value" Value="AttrDefaultValue"></asp:ListItem>
                                        <asp:ListItem Text="ProductLine Desc" Value="ProductLine_Desc"></asp:ListItem>
                                        <asp:ListItem Text="Attribute Required" Value="AttrRequired"></asp:ListItem>
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
                                    <!-- Defect Listing -->
                                    <asp:GridView RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="True"
                                        AllowSorting="True" DataKeyNames="AttrMapping_Sno" AutoGenerateColumns="False"
                                        ID="gvComm" runat="server" OnPageIndexChanging="gvComm_PageIndexChanging" Width="100%"
                                        OnSelectedIndexChanging="gvComm_SelectedIndexChanging" HorizontalAlign="Left"
                                        OnSorting="gvComm_Sorting">
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                <HeaderStyle Width="40px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Attribute_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Attribute Desc" SortExpression="Attribute_Desc">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AttrDefaultValue" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Attribute Default Value" SortExpression="AttrDefaultValue">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="BusinessLine_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Business Line" SortExpression="BusinessLine_Desc">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProductLine_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="ProductLine Desc" SortExpression="ProductLine_Desc">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AttrRequired" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Attribute Required" SortExpression="AttrRequired">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Active_Flag" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Status" SortExpression="Active_Flag">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:CommandField ShowSelectButton="True" HeaderStyle-Width="60px" HeaderText="Edit">
                                                <HeaderStyle Width="60px" />
                                            </asp:CommandField>
                                        </Columns>
                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                        <AlternatingRowStyle CssClass="fieldName" />
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
                                    <!-- End Defect Listing -->
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
                                                <asp:HiddenField ID="hdnAttrMappingSno" runat="server" />
                                            </td>
                                        </tr>
                                        <%--Added By Gaurav Garg on 20 Oct 09 For MTO--%>
                                         <tr>
                                            <td width="30%">
                                                Business Line <font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlBusinessLine" runat="server" CssClass="simpletxt1" Width="170px" OnSelectedIndexChanged="ddlBusinessLine_SelectIndexChanged" AutoPostBack="true" >
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlBusinessLine"
                                                    ErrorMessage="Business Line is required." SetFocusOnError="true" ValidationGroup="editt"
                                                    InitialValue="Select"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <%--END--%>
                                        <tr>
                                            <td width="30%">
                                                Attribute Description<font color="red">*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlAttriDesc" runat="server" CssClass="simpletxt1" ValidationGroup="editt"
                                                    Width="170px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RFCountry" runat="server" ControlToValidate="ddlAttriDesc"
                                                    ErrorMessage="Attribute Description is required." InitialValue="0" SetFocusOnError="true"
                                                    ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                ProductLine Description<font color="red">*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlPlineDesc" runat="server" CssClass="simpletxt1" ValidationGroup="editt"
                                                    Width="170px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlPlineDesc"
                                                    ErrorMessage="ProductLine Description is required." InitialValue="0" SetFocusOnError="true"
                                                    ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Attribute Default Value<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAttDefaulValue" runat="server" CssClass="txtboxtxt" Text="" Width="170px"
                                                    MaxLength="50" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAttDefaulValue"
                                                    ErrorMessage="Attribute Default Value." SetFocusOnError="true" ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Attribute Required
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rdoAttriReq" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="True" Text="YES" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="NO" Value="N"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Status
                                            </td>
                                            <td>
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
                                            <td>
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
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
