<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" EnableEventValidation="false" 
    CodeFile="ProductMaster.aspx.cs" Inherits="Admin_ProductMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Product Master
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
                                    <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    Search For
                                    <asp:DropDownList ID="ddlSearch" runat="server" Width="140px" CssClass="simpletxt1">
                                        <asp:ListItem Text="Product Code" Value="Product_Code"></asp:ListItem>
                                        <asp:ListItem Text="Product Description" Value="Product_Desc"></asp:ListItem>
                                        <asp:ListItem Text="Product Group" Value="ProductGroup_desc"></asp:ListItem>
                                        <asp:ListItem Text="Product Line" Value="ProductLine_desc"></asp:ListItem>
                                        <asp:ListItem Text="Product Division" Value="Unit_Desc"></asp:ListItem>
                                        <asp:ListItem Text="Product Segment" Value="Rating_Status"></asp:ListItem>
                                        <asp:ListItem Text="Product Type" Value="PRODUCTTYPE_DESC"></asp:ListItem>
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
                                    <!-- product Listing -->
                                    <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="True"
                                        AllowSorting="True" DataKeyNames="Product_SNo" AutoGenerateColumns="False" ID="gvComm"
                                        runat="server" OnPageIndexChanging="gvComm_PageIndexChanging" Width="100%" OnSelectedIndexChanging="gvComm_SelectedIndexChanging"
                                        HorizontalAlign="Left" Visible="true" OnSorting="gvComm_Sorting">
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                <HeaderStyle Width="40px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="BusinessLine_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Business Line" SortExpression="BusinessLine_Desc">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Product_Code" SortExpression="Product_Code" HeaderStyle-Width="60px"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Product Code">
                                            </asp:BoundField>
                                              
                                            <asp:BoundField DataField="Product_Desc" SortExpression="Product_Desc" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Product Description">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProductGroup_desc" SortExpression="ProductGroup_desc"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Product Group">
                                              </asp:BoundField>
                                            
                                            <asp:BoundField DataField="ProductGroup_Code"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Product Group Code" Visible="false">
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="ProductLine_Desc" SortExpression="ProductLine_Desc" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Product Line">
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="ProductLine_Code"  ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Product Line Code" Visible="false">
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="Unit_Desc" SortExpression="Unit_Desc" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Product Division">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Rating_Status" SortExpression="Rating_Status" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Product Segment">
                                             </asp:BoundField>
                                             <asp:BoundField DataField="PRODUCTTYPE_DESC" SortExpression="PRODUCTTYPE_DESC" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Product Type">
                                             </asp:BoundField>
                                            <asp:BoundField DataField="Active_Flag" SortExpression="Active_Flag" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Status">
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
                                  <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn" Text="Export To Execl"
                            Width="100" onclick="btnExport_Click"  />
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
                                                <asp:HiddenField ID="hdnProductSNo" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Business Line <font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlBusinessLine" runat="server" CssClass="simpletxt1" Width="175px" OnSelectedIndexChanged="ddlBusinessLine_SelectIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlBusinessLine"
                                                    ErrorMessage="Business Line is required." SetFocusOnError="true" ValidationGroup="editt"
                                                    InitialValue="Select"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Product Division: <font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlUnit" CssClass="simpletxt1" Width="175" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="Select"
                                                    SetFocusOnError="true" ControlToValidate="ddlUnit" ValidationGroup="editt">Product Division is required.</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Product Line: <font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlProductLine" CssClass="simpletxt1" Width="175" runat="server"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlProductLine_SelectedIndexChanged">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="Select"
                                                    SetFocusOnError="true" ControlToValidate="ddlProductLine" ValidationGroup="editt">Product line is required</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Product Group: <font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlProductGroup" CssClass="simpletxt1"  onactivate="this.style.width='auto';" Width="175px" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="true"
                                                    ErrorMessage="Select Product Group." InitialValue="Select" ControlToValidate="ddlProductGroup"
                                                    ValidationGroup="editt">Product group is required</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Product:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtProductCode" runat="server" Width="170px"
                                                    Text="" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldProductCode" runat="server" SetFocusOnError="true"
                                                    ErrorMessage="Product is required" ControlToValidate="txtProductDesc" ToolTip="Product is required"
                                                    ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Product Description<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtProductDesc" TextMode="MultiLine" runat="server"
                                                    Width="170px" Text="" />
                                                <asp:RequiredFieldValidator ID="reqvalDeptIname" runat="server" SetFocusOnError="true"
                                                    ErrorMessage="Sevice Desc is required." ControlToValidate="txtProductDesc" ValidationGroup="editt">Product description is required</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Product Segment
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlRating" runat="server" CssClass="simpletxt1" Width="175">
                                                    <%--<asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="C">Current</asp:ListItem>
                                                    <asp:ListItem Value="O">Obsolete</asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td width="30%">
                                                Type of Product
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlProducType" runat="server" CssClass="simpletxt1" Width="175">
                                                <asp:ListItem>Select</asp:ListItem>
                                                </asp:DropDownList>
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
                                            <td height="25" align="left">
                                                &nbsp;
                                            </td>
                                            <td>
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
                                        <tr>
                                            <td height="25" align="left">
                                                &nbsp;
                                            </td>
                                            <td>                               
                                               
                                            </td>
                                            <td>                               
                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="http://115.114.96.186/cic/bulkuploaded/bulkUploadProductMaster.xls">Bulk Upload for Product Master</asp:HyperLink>
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
