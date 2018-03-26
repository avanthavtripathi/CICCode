<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="SourceAndTypeWiseComplaintReport.aspx.cs" Inherits="pages_SourceAndTypeWiseComplaintReport"
    Title="Complaint source and Type wise Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function funComplainDetail(strUrl) {

            window.open(strUrl, "_blank", 'width=900,height=600,scrollbars=1,resizable=no,top=1,left=1');
            return false;
        }
    </script>

    <asp:UpdatePanel ID="updatepnl" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportExcel" />
        </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Complaint Report
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" colspan="2">
                        <table width="98%" border="0">
                            <tr>
                                <td align="right">
                                    Region
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlRegion" runat="server" Width="175px" CssClass="simpletxt1"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Branch
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlBranch" runat="server" Width="175px" CssClass="simpletxt1"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Product Divison
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlProductDivison" runat="server" Width="175px" CssClass="simpletxt1"
                                        ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Mode Of Receipt
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlModeOfReceipt" runat="server" Width="175px" CssClass="simpletxt1"
                                        ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Date From<font color="red">*</font>
                                </td>
                                <td align="left">
                                    <asp:TextBox runat="server" ID="txtFromDate" CssClass="txtboxtxt" ValidationGroup="editt"
                                        MaxLength="10" />
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" Type="Date" Operator="DataTypeCheck"
                                        ControlToValidate="txtFromDate" Display="none" ValidationGroup="editt" SetFocusOnError="true"></asp:CompareValidator>
                                    <cc1:CalendarExtender ID="CEFrom" runat="server" TargetControlID="txtFromDate">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="rfvFromDate" ControlToValidate="txtFromDate" ErrorMessage="*"
                                        Display="Dynamic" runat="server" SetFocusOnError="true" ValidationGroup="editt"></asp:RequiredFieldValidator>
                                    To<font color="red">*</font>
                                    <asp:TextBox runat="server" ID="txtToDate" CssClass="txtboxtxt" ValidationGroup="editt"
                                        MaxLength="10" />
                                    <asp:CompareValidator ID="CompareValidator7" runat="server" Type="Date" Operator="DataTypeCheck"
                                        ControlToValidate="txtToDate" Display="none" ValidationGroup="editt" SetFocusOnError="true"></asp:CompareValidator>
                                    <cc1:CalendarExtender ID="CETo" runat="server" TargetControlID="txtToDate">
                                    </cc1:CalendarExtender>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtToDate" ErrorMessage="*"
                                        Display="Dynamic" runat="server" SetFocusOnError="true" ValidationGroup="editt"></asp:RequiredFieldValidator>
                                    <asp:Label ID="lblDateErr" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                </td>
                                <td align="left">
                                    <br />
                                    <asp:Button Width="70px" Text="Search" CssClass="btn" ValidationGroup="editt" CausesValidation="true"
                                        ID="btnSearch" runat="server" OnClick="btnSearch_Click" />
                                    <asp:Button ID="btnExportExcel" runat="server" Width="114px" Text="Save To Excel"
                                        CssClass="btn" onclick="btnExportExcel_Click" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table width="100%" border="0">
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblMSG" Visible="false" runat="server" Font-Bold="true" Text="Total Rows Count:"></asp:Label>
                                    <asp:Label ID="lblRowsCount" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <!-- Action Listing -->
                                    <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" PagerStyle-HorizontalAlign="Center"
                                        AutoGenerateColumns="False" ID="gvComm" runat="server" Width="100%" HorizontalAlign="Left"
                                        Visible="true">
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                HeaderText="Product Division">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductDivision" runat="server" Text='<%#Eval("ProductDivision")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                HeaderText="CC-Customer">
                                                <ItemTemplate>
                                                    <a href="Javascript:void(0);" onclick='return funComplainDetail("DetailedComplaintSourceAndTypewiseReport.aspx?ISMOR=0&PROD=<%#Eval("ProductSNo")%>&SOC=CC-Customer&FRMD=<%#Eval("FromDate")%>&TMD=<%#Eval("ToDate")%>")'>
                                                        <%#Eval("cntCCCustomer")%>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                HeaderText="CC-Dealer">
                                                <ItemTemplate>
                                                    <a href="Javascript:void(0);" onclick='return funComplainDetail("DetailedComplaintSourceAndTypewiseReport.aspx?ISMOR=0&PROD=<%#Eval("ProductSNo")%>&SOC=CC-Dealer&FRMD=<%#Eval("FromDate")%>&TMD=<%#Eval("ToDate")%>")'>
                                                        <%#Eval("cntCCDealer")%>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                HeaderText="CC-ASC">
                                                <ItemTemplate>
                                                    <a href="Javascript:void(0);" onclick='return funComplainDetail("DetailedComplaintSourceAndTypewiseReport.aspx?ISMOR=0&PROD=<%#Eval("ProductSNo")%>&SOC=CC-ASC&FRMD=<%#Eval("FromDate")%>&TMD=<%#Eval("ToDate")%>")'>
                                                        <%#Eval("cntCCASC")%>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                HeaderText="WebForm">
                                                <ItemTemplate>
                                                    <a href="Javascript:void(0);" onclick='return funComplainDetail("DetailedComplaintSourceAndTypewiseReport.aspx?ISMOR=1&PROD=<%#Eval("ProductSNo")%>&MOR=10&FRMD=<%#Eval("FromDate")%>&TMD=<%#Eval("ToDate")%>")'>
                                                        <%#Eval("cntWebForm")%>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                HeaderText="PJC">
                                                <ItemTemplate>
                                                    <a href="Javascript:void(0);" onclick='return funComplainDetail("DetailedComplaintSourceAndTypewiseReport.aspx?ISMOR=1&PROD=<%#Eval("ProductSNo")%>&MOR=7&FRMD=<%#Eval("FromDate")%>&TMD=<%#Eval("ToDate")%>")'>
                                                    </a>
                                                    <%#Eval("cntPJC")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
                                    </asp:GridView>
                                    <!-- End Action Listing -->
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
