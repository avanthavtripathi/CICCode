<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="RegistrationReport.aspx.cs" Inherits="Reports_ServiceRegistrationReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script language="javascript" type="text/javascript">
         function funUserDetail(custNo, compNo)
        {
            var strUrl='CustomerDetail.aspx?custNo='+ custNo + '&CompNo='+ compNo;
            window.open(strUrl,'History','height=550,width=750,left=20,top=30,Location=0');
        }
         function funReqDetail(compNo)
        {
            var strUrl='ComplaintDetailPopUp.aspx?compNo='+ compNo;
            window.open(strUrl,'History','height=550,width=750,left=20,top=30,Location=0');
        }
         function funSCDetail(SCNo)
        {
            var strUrl='../Pages/SCPopup.aspx?scno='+ SCNo + '&type=display';
            window.open(strUrl,'History','height=550,width=750,left=20,top=30,Location=0,scrollbars=1');
        }
    </script>

    <asp:UpdatePanel ID="updatepnl" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnExport" />
        </Triggers>
        <ContentTemplate>
            <table width="100%" border="0">
                <tr>
                    <td class="headingred">
                        MIS ASC Report
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
                    <td style="padding: 10px" align="center" colspan="2">
                        <table width="98%" border="0">
                            <tr>
                                <td width="30%" align="right">
                                    Service Engineer
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlServiceEngineer" runat="server" Width="175px" CssClass="simpletxt1"
                                        ValidationGroup="editt">
                                        <asp:ListItem Value="0">All</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Product Divison
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlProductDivison" runat="server" Width="175px" CssClass="simpletxt1"
                                        ValidationGroup="editt" AutoPostBack="True" OnSelectedIndexChanged="ddlProductDivison_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="regproductdivision" runat="server" ControlToValidate="ddlProductDivison"
                                        ErrorMessage="Select Product Division" InitialValue="0" ValidationGroup="editt"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Product Line
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlProductLine" runat="server" Width="175px" CssClass="simpletxt1"
                                        ValidationGroup="editt" AutoPostBack="True" OnSelectedIndexChanged="ddlProductLine_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Complaint Stage
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlCallStage" runat="server" CssClass="simpletxt1" Width="175px"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlCallStage_SelectedIndexChanged"
                                        ValidationGroup="editt">
                                        <asp:ListItem Selected="True">Select</asp:ListItem>
                                        <asp:ListItem>Initialization</asp:ListItem>
                                        <asp:ListItem>WIP</asp:ListItem>
                                        <asp:ListItem>TempClosed</asp:ListItem>
                                        <asp:ListItem>Closure</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="30%">
                                    Status (Pending/Close)
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="DDlfinalstatus" runat="server" CssClass="simpletxt1" Width="175px"
                                        ValidationGroup="editt">
                                        <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="P">Pending</asp:ListItem>
                                        <asp:ListItem Value="C">Closed</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Complaint Status
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlCallStatus" runat="server" CssClass="simpletxt1" Width="350px"
                                        ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Date From
                                </td>
                                <td align="left">
                                    <asp:TextBox runat="server" ID="txtFromDate" CssClass="txtboxtxt" ValidationGroup="editt"
                                        MaxLength="10" />
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" Type="Date" Operator="DataTypeCheck"
                                        ControlToValidate="txtFromDate" Display="none" ValidationGroup="editt" SetFocusOnError="true"></asp:CompareValidator>
                                    <cc1:CalendarExtender ID="CEFrom" runat="server" TargetControlID="txtFromDate">
                                    </cc1:CalendarExtender>
                                    To
                                    <asp:TextBox runat="server" ID="txtToDate" CssClass="txtboxtxt" ValidationGroup="editt"
                                        MaxLength="10" />
                                    <asp:CompareValidator ID="CompareValidator7" runat="server" Type="Date" Operator="DataTypeCheck"
                                        ControlToValidate="txtToDate" Display="none" ValidationGroup="editt" SetFocusOnError="true"></asp:CompareValidator>
                                    <cc1:CalendarExtender ID="CETo" runat="server" TargetControlID="txtToDate">
                                    </cc1:CalendarExtender>
                                    <asp:Label ID="lblDateErr" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Complaint No
                                </td>
                                <td align="left">
                                    <asp:TextBox runat="server" ID="txtReqNo" CssClass="txtboxtxt" ValidationGroup="editt" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Alternate No.
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Product Serial No
                                </td>
                                <td align="left">
                                    <asp:TextBox runat="server" ID="txtProductSerialNo" CssClass="txtboxtxt" ValidationGroup="editt" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Defect Category
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlDefectCategory" runat="server" Width="300px" CssClass="simpletxt1"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlDefectCategory_SelectedIndexChanged"
                                        ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Defect
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlDefect" runat="server" Width="350px" CssClass="simpletxt1"
                                        ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    SRF
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlSRF" runat="server" Width="175" CssClass="simpletxt1">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="N">No</asp:ListItem>
                                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Warranty Status
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlWarrantyStatus" runat="server" Width="175" CssClass="simpletxt1">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="N">No</asp:ListItem>
                                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                </td>
                                <td align="left">
                                    <br />
                                    <asp:Button Width="70px" Text="Search" CssClass="btn" ValidationGroup="editt" CausesValidation="true"
                                        ID="btnSearch" runat="server" OnClick="btnSearch_Click" />
                                    <asp:Button Width="100px" Text="Export To Excel" CssClass="btn" ValidationGroup="editt"
                                        CausesValidation="true" ID="BtnExport" runat="server" OnClick="BtnExport_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="MsgTDCount">
                                    Total Number of Complaints :
                                    <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="MsgTDCount">
                                    Total Number of Defects :
                                    <asp:Label ID="lblDefectCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table width="100%" border="0">
                            <tr>
                                <td>
                                    <!-- Action Listing -->
                                    <asp:GridView PageSize="10" GridGroups="both" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" ID="gvComm" runat="server" OnPageIndexChanging="gvComm_PageIndexChanging"
                                        Width="100%" HorizontalAlign="Left" Visible="true" OnSorting="gvComm_Sorting">
                                        <Columns>
                                            <asp:BoundField DataField="RowNumber" HeaderStyle-Width="40px" HeaderText="SNo">
                                                <HeaderStyle Width="40px" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Complaint_RefNo" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderText="Complaint No">
                                                <ItemTemplate>
                                                    <a href="Javascript:void(0);" onclick="funCommonPopUpReport(<%#Eval("BaseLineId")%>)">
                                                        <%#Eval("Complaint_Split")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="100px" SortExpression="CustomerId" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderText="Customer">
                                                <ItemTemplate>
                                                    <a href="Javascript:void(0);" onclick="funUserDetail('<%#Eval("CustomerId")%>','<%#Eval("Complaint_RefNo")%>')">
                                                        <%#Eval("FirstName")%>
                                                        <%#Eval("LastName")%>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="UniqueContact_No" HeaderText="ContactNo" />
                                            <asp:BoundField DataField="LoggedDate" SortExpression="LoggedDate" HeaderStyle-Width="100px"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Log Date">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SLA_Date" SortExpression="SLA_Date" HeaderStyle-Width="100px"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="SLA Date">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderStyle-Width="120px" SortExpression="SC_Name" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderText="Service Contractor">
                                                <ItemTemplate>
                                                    <a href="Javascript:void(0);" onclick="funSCDetail('<%#Eval("SC_SNo")%>')">
                                                        <%#Eval("SC_Name")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                HeaderText="Service Engineer">
                                                <ItemTemplate>
                                                    <%#Eval("ServiceEng_Name")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CallStage" HeaderText="CallStage" />
                                            <asp:BoundField DataField="StageDesc" SortExpression="StageDesc" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Complaint Status">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Product_Code" SortExpression="Product_Code" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Product Code">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProductDivision_Desc" SortExpression="ProductDivision_Desc"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Product Division">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Quantity" SortExpression="Quantity" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Quantity">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SRF" SortExpression="SRF" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="SRF">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="WarrantyStatus" SortExpression="WarrantyStatus" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Warranty Status">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NatureOfComplaint" SortExpression="NatureOfComplaint"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="NatureOfComplaint">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ComplaintAgeSLA" SortExpression="ComplaintAgeSLA" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Age On SLA date">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ComplaintAgeLOG" SortExpression="ComplaintAgeLOG" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Age On Log date">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DiffrenceInHour" SortExpression="DiffrenceInHour" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Age in Hours">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                        </Columns>
                                        <RowStyle CssClass="gridbgcolor" />
                                        <AlternatingRowStyle CssClass="fieldName" />
                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                        <PagerStyle HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
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
                                <td>
                                    <asp:GridView GridGroups="both" AutoGenerateColumns="False" ID="GVExport" runat="server"
                                        Width="100%" HorizontalAlign="Left" Visible="false">
                                        <Columns>
                                            <asp:BoundField DataField="RowNumber" HeaderStyle-Width="40px" HeaderText="SNo">
                                                <HeaderStyle Width="40px" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                HeaderText="Complaint No">
                                                <ItemTemplate>
                                                    <a href="Javascript:void(0);" onclick="funCommonPopUpReport(<%#Eval("BaseLineId")%>)">
                                                        <%#Eval("Complaint_Split")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                HeaderText="Customer">
                                                <ItemTemplate>
                                                    <%#Eval("FirstName")%>
                                                    <%#Eval("LastName")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="UniqueContact_No" HeaderText="ContactNo" />
                                            <asp:BoundField DataField="AltTelNumber" HeaderText="AltTelNumber" />
                                            <asp:BoundField DataField="Addr" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Address">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LoggedDate" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Log Date">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SLA_Date" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="SLA Start Date">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                HeaderText="Service Contractor">
                                                <ItemTemplate>
                                                    <%#Eval("SC_Name")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                HeaderText="Service Engineer">
                                                <ItemTemplate>
                                                    <%#Eval("ServiceEng_Name")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CallStage" HeaderText="CallStage" />
                                            <asp:BoundField DataField="StageDesc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Complaint Status">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Product_Code" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Product Code">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProductDivision_Desc" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Product Division">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Quantity" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Quantity">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SRF" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="SRF">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="WarrantyStatus" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Warranty Status">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NatureOfComplaint" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="NatureOfComplaint">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Remarks" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Remarks">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ComplaintAgeSLA" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Age On SLA Date">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ComplaintAgeLOG" SortExpression="ComplaintAgeLOG" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Age On Log date">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DiffrenceInHour" SortExpression="DiffrenceInHour" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Age in Hours">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                        </Columns>
                                        <RowStyle CssClass="gridbgcolor" />
                                        <AlternatingRowStyle CssClass="fieldName" />
                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                        <PagerStyle HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
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
