<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="DefectMaster.aspx.cs" Inherits="Admin_DefectMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
    <Triggers>
    <asp:PostBackTrigger ControlID="LbtnDownload" /> 
    </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Defect Master
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
                                        <asp:ListItem Text="Code" Value="Defect_Code"></asp:ListItem>
                                        <asp:ListItem Text="Description" Value="Defect_Desc"></asp:ListItem>
                                        <asp:ListItem Text="Defect Category" Value="Defect_Category_Desc"></asp:ListItem>
                                        <asp:ListItem Text="Product Division" Value="Unit_Desc"></asp:ListItem>
                                        <asp:ListItem Text="Product Line" Value="ProductLine_desc"></asp:ListItem>
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
                                        AllowSorting="True" DataKeyNames="Defect_SNo" AutoGenerateColumns="False" ID="gvComm"
                                        runat="server" OnPageIndexChanging="gvComm_PageIndexChanging" Width="100%" OnSelectedIndexChanging="gvComm_SelectedIndexChanging"
                                        HorizontalAlign="Left" OnRowDataBound="gvComm_RowDataBound" OnSorting="gvComm_Sorting">
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                <HeaderStyle Width="40px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Defect_Code" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Code" SortExpression="Defect_Code">
                                                <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Defect_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Description" SortExpression="Defect_Desc">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Category Code" Visible="false" DataField="Defect_Category_Code" SortExpression="Defect_Category_Code"  ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" >
                                            <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                 </asp:BoundField>
                                            <%--    <asp:BoundField DataField="ProductLine_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="ProductLine Name" SortExpression="ProductLine_Desc">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>--%>
                                            <asp:BoundField DataField="Defect_Category_Desc" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Defect Category" SortExpression="Defect_Category_Desc">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="BusinessLine_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Business Line" SortExpression="BusinessLine_Desc">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProductLine_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Product Line" SortExpression="ProductLine_Desc">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Unit_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Product Division" SortExpression="Unit_Desc">
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
                                <div align="left">  <asp:Button ID="LbtnDownload"  CssClass="btn" Width="100px" runat="server" Text="Download" onclick="LbtnDownload_Click" Visible="False"></asp:Button>   </div>
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
                                                <asp:HiddenField ID="hdnDefectSNo" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Defect&nbsp; Code<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtDefectCode" runat="server" Width="170px"
                                                    Text="" /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                        SetFocusOnError="true" ErrorMessage="Defect Code is required." ControlToValidate="txtDefectCode"
                                                        ValidationGroup="editt" ToolTip="Defect Code is required."></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Defect&nbsp; Description<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtDefectDesc" runat="server" Width="170px"
                                                    Text="" /><asp:RequiredFieldValidator ID="reqvalDeptIname" runat="server" SetFocusOnError="true"
                                                        ErrorMessage="Defect Description is required." ControlToValidate="txtDefectDesc"
                                                        ToolTip="Defect Description is required." ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <%--Added By Gaurav Garg on 20 Oct 09 For MTO--%>
                                        <tr>
                                            <td width="30%">
                                                Business Line 
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlBusinessLine" runat="server" CssClass="simpletxt1" Width="170px"
                                                    OnSelectedIndexChanged="ddlBusinessLine_SelectIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <%--END--%>
                                        <tr>
                                            <td width="30%">
                                                Product Division
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlProductDiv" runat="server" CssClass="simpletxt1" Width="175"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlProductDiv_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Product Line
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlProductLine" runat="server" CssClass="simpletxt1" Width="175"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlProductLine_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Defect Category Name <font color="red">*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlDC" runat="server" CssClass="simpletxt1" ValidationGroup="editt">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDC"
                                                    ErrorMessage="Defect Category Name is required." InitialValue="Select" SetFocusOnError="true"
                                                    ValidationGroup="editt"></asp:RequiredFieldValidator>
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
                                                                &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
                                                            <asp:HyperLink ID="herfBulkUploadDefectMaster" runat="server" NavigateUrl="http://cgcallcenter.com/cic/bulkuploaded/BulkUploadDefectMaster.xls">Bulk Upload for Defect Master</asp:HyperLink>
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
