<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="SBUMaster.aspx.cs" Inherits="Admin_SBUMaster"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        SBU Master
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right:10px;">
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
                                    <asp:DropDownList ID="ddlSearch" runat="server" Width="130px" CssClass="simpletxt1">
                                        <asp:ListItem Text="Code" Value="SBU_Code"></asp:ListItem>
                                        <asp:ListItem Text="Description" Value="SBU_Desc"></asp:ListItem>
                                        <asp:ListItem Text="Company Code" Value="Company_Code"></asp:ListItem>
                                       
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
                                <td class="bgcolorcomm">
                                    <!-- Country Listing -->
                                    <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="True"
                                        AllowSorting="True" DataKeyNames="SBU_SNo" AutoGenerateColumns="False" ID="gvUSB"
                                        runat="server" OnPageIndexChanging="gvUSB_PageIndexChanging" Width="100%" OnSelectedIndexChanging="gvUSB_SelectedIndexChanging"
                                        HorizontalAlign="Left" EnableSortingAndPagingCallbacks="True" onsorting="gvUSB_Sorting">
                                        <Columns>
                                            <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                <HeaderStyle Width="40px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SBU_Code" SortExpression="SBU_Code" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Code">
                                                <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SBU_Desc" SortExpression="SBU_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Description">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Company_Code" SortExpression="Company_Code" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Company Code">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Active_Flag" SortExpression="Active_Flag" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Status">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:CommandField ShowSelectButton="True" HeaderStyle-Width="60px" HeaderText="Edit">
                                                <HeaderStyle Width="60px" />
                                            </asp:CommandField>
                                        </Columns>
                                         <EmptyDataTemplate>
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                    <img src="<%=ConfigurationManager.AppSettings["UserMessage"]%>" alt="" />    <b>No Record found</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                    <!-- End Country Listing -->
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                    
                                </td>
                            </tr>
                            <tr>
                                <td width="100%" align="left" class="bgcolorcomm">
                                    <table width="98%" border="0">
                                        <tr align="right">
                                            <td align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>" colspan="2">
                                                <font color='red'>*</font>
                                                <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:HiddenField ID="hdnSBUsno" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                SBU Code<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtSBUCode" runat="server" Width="170px" Text=""
                                                    MaxLength="10" /><asp:RequiredFieldValidator ID="RFV1" runat="server" SetFocusOnError="true"
                                                        ErrorMessage=" SBU Code is required." ControlToValidate="txtSBUCode" ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Company Code <font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCompany" ValidationGroup="editt" runat="server" CssClass="simpletxt1">
                                                </asp:DropDownList>
                                                <asp:CompareValidator ID="CC1" runat="server" ControlToValidate="ddlCompany" ErrorMessage="Company Code is required."
                                                    Operator="NotEqual" SetFocusOnError="True" ValueToCompare="Select" ValidationGroup="editt"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                SBU Name<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtSBUDesc" runat="server" Width="170px" Text=""
                                                    MaxLength="100" /><asp:RequiredFieldValidator ID="RFV2" runat="server" SetFocusOnError="true"
                                                        ErrorMessage="SBU Name is required." ControlToValidate="txtSBUDesc" ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Status
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
                                            <td align="center" colspan="2">
                                                <asp:Button ID="imgBtnAdd" runat="server" CausesValidation="True" CssClass="btn"
                                                    OnClick="imgBtnAdd_Click" Text="Add" ValidationGroup="editt" Width="70px" />
                                                <asp:Button ID="imgBtnUpdate" runat="server" CausesValidation="True" CssClass="btn"
                                                    OnClick="imgBtnUpdate_Click" Text="Save" ValidationGroup="editt" Width="70px" />
                                                <asp:Button ID="imgBtnCancel" runat="server" CausesValidation="false" CssClass="btn"
                                                    OnClick="imgBtnCancel_Click" Text="Cancel" Width="70px" />
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
</asp:Content>
