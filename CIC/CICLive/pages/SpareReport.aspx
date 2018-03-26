<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="SpareReport.aspx.cs" Inherits="pages_SpareReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script language="javascript" type="text/javascript">
         function funUserDetail(custNo, compNo)
        {
            var strUrl='../Reports/CustomerDetail.aspx?custNo='+ custNo + '&CompNo='+ compNo;
            window.open(strUrl,'History','height=550,width=750,left=20,top=30,Location=0');
        }
         function funReqDetail(compNo)
        {
            var strUrl='../Reports/ComplaintDetailPopUp.aspx?compNo='+ compNo;
            window.open(strUrl,'History','height=550,width=750,left=20,top=30,Location=0');
        }
         function funSCDetail(SCNo)
        {
            var strUrl='../Pages/SCPopup.aspx?scno='+ SCNo + '&type=display';
            window.open(strUrl,'History','height=550,width=750,left=20,top=30,Location=0,scrollbars=1');
        }
        
         function funSendSpare(CompNo,SplitNo,Spare_desc,Spare_Sno)
        {
            var strUrl='../Pages/AddSpare.aspx?CompNo='+ CompNo + '&SplitNo=' + SplitNo + '&Spare_desc=' + Spare_desc + '&Spare_Sno=' + Spare_Sno;
            window.open(strUrl,'SendSpare','scrollbars=1');
        }
    function SearchClick()
    {
        var btn=document.getElementById('<% = btnSearch.ClientID %>');
        if(btn)btn.click();
    }
    </script>

    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Spare Report
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
                                <td width="30%" align="right">
                                    Region
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlRegion" runat="server" Width="175px" CssClass="simpletxt1"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
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
                            <%--<tr>
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
                                <td width="30%" align="right">
                                    Complaint Status
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlCallStatus" runat="server" CssClass="simpletxt1" Width="350px"
                                        ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>--%>
                            <tr>
                                <td width="30%" align="right">
                                    Status
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlSpareStatus" runat="server" CssClass="simpletxt1" Width="175px"
                                        ValidationGroup="editt">
                                        <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Open" Value="N"></asp:ListItem>
                                        <asp:ListItem Text="Closed" Value="Y"></asp:ListItem>
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
                                </td>
                                <td align="left">
                                    <br />
                                    <asp:Button Width="70px" Text="Search" CssClass="btn" ValidationGroup="editt" CausesValidation="true"
                                        ID="btnSearch" runat="server" OnClick="btnSearch_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="MsgTDCount">
                                    Total Number of Records :
                                    <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                                        runat="server" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table width="100%" border="0">
                            <tr>
                                <td>
                                    <!-- Action Listing -->
                                    <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" OnRowDataBound="gvComm_RowDataBound"
                                        AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                                        GridGroups="both" AllowPaging="True" PagerStyle-HorizontalAlign="Center" AllowSorting="True"
                                        AutoGenerateColumns="False" ID="gvComm" runat="server" OnPageIndexChanging="gvComm_PageIndexChanging"
                                        Width="100%" HorizontalAlign="Left" Visible="true" OnSorting="gvComm_Sorting">
                                        <Columns>
                                            <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                <HeaderStyle Width="40px" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Complaint_RefNo" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderText="Complaint No">
                                                <ItemTemplate>
                                                    <a href="Javascript:void(0);" onclick="funCommonPopUpReport(<%#Eval("BaseLineId")%>)">
                                                        <%--<%#Eval("Complaint_RefNo")%>/<%#Eval("splitComplaint_RefNo")%></a>--%>
                                                        <%#Eval("Complaint_Split")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Branch_Name" SortExpression="Branch_Name" HeaderStyle-Width="100px"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Name">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderStyle-Width="100px" SortExpression="CustomerId" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderText="Customer Name">
                                                <ItemTemplate>
                                                    <a href="Javascript:void(0);" onclick="funUserDetail('<%#Eval("CustomerId")%>','<%#Eval("Complaint_RefNo")%>')">
                                                        <%--<%#Eval("CustomerId")%></a>--%>
                                                        <%#Eval("FirstName")%>
                                                        <%#Eval("LastName")%>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="SLADate" SortExpression="SLADate" HeaderStyle-Width="100px"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="SLA Start Date">
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
                                            <asp:BoundField DataField="ProductDivision_Desc" SortExpression="ProductDivision_Desc"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Product Division">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="StageDesc" SortExpression="StageDesc" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Complaint Status">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CreatedDate" SortExpression="CreatedDate" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Action Date">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Spare_Desc" SortExpression="Spare_Desc" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Spare Description">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Qty_Req" SortExpression="Qty_Req" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Qty Requested">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Qty_Sent" SortExpression="Qty_Sent" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Qty Sent">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Doc_Dispatch_No" SortExpression="Doc_Dispatch_No" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Document Number">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CreatedDate" SortExpression="CreatedDate" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Date">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <%--<asp:BoundField DataField="Spare_Status" SortExpression="Spare_Status" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Status(Closed/Open)">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>--%>
                                            <asp:TemplateField SortExpression="Spare_Status" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderText="Status(Closed/Open)">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvlblStatus" Text='<%#Eval("Spare_Status")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="120px" SortExpression="SC_Name" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderText="Service Contractor">
                                                <ItemTemplate>
                                                    <table id="tbStatus" runat="server">
                                                        <tr>
                                                            <td>
                                                                <a href="Javascript:void(0);" onclick="funSendSpare('<%#Eval("Complaint_refno")%>','<%#Eval("SplitComplaint_RefNo")%>','<%#Eval("Spare_Desc")%>','<%#Eval("Spare_Sno") %>')">
                                                                    Click To Send Spare</a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
                                    <!-- End Action Listing -->
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <%-- <tr>
                           
                            <td align="center">
                            <asp:Button ID="btnExportExcel" runat="server" Width="114px" Text="Save To Excel"
                             CssClass="btn" OnClick="btnExportExcel_Click" />
                             
                                </td>
                            </tr>--%>
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
