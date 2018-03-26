<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
     CodeFile="PendingComplaintReport1.aspx.cs" Inherits="Reports_PendingComplaintReport1" %>
    
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="PendingSerRegReport" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function funUserDetail(custNo, compNo) {
            var strUrl = 'CustomerDetail.aspx?custNo=' + custNo + '&CompNo=' + compNo;
            window.open(strUrl, 'History', 'height=550,width=750,left=20,top=30,Location=0');
        }
        function funReqDetail(compNo) {
            var strUrl = 'ComplaintDetailPopUp.aspx?compNo=' + compNo;
            window.open(strUrl, 'History', 'height=550,width=750,left=20,top=30,Location=0');
        }
        function funSCDetail(SCNo) {
            var strUrl = '../Pages/SCPopup.aspx?scno=' + SCNo + '&type=display';
            window.open(strUrl, 'History', 'height=550,width=750,left=20,top=30,Location=0,scrollbars=1');
        }

        function funFileView(Complaint_RefNo) {
            var fileView = true;
            var strUrl = '../Pages/UploadedFilePopUp.aspx?scno=' + SCNo + '&type=display' + '&fileView=' + fileView + '';
            window.open(strUrl, 'History', 'height=550,width=750,left=20,top=30,Location=0,scrollbars=1');
        }

    </script>
     <asp:UpdatePanel ID="updatepnl" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportExcel" />
        </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">Complaint Report
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
                                <td width="35%" align="right">Business Line
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlBusinessLine" AutoPostBack="true" runat="server" Width="175px"
                                        CssClass="simpletxt1" ValidationGroup="editt" OnSelectedIndexChanged="ddlBusinessLine_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">Region
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlRegion" runat="server" Width="175px" CssClass="simpletxt1"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">Branch
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlBranch" runat="server" Width="175px" CssClass="simpletxt1"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <%--Added By Gaurav Garg on 13 Nov for MTO--%>
                            <tr id="trResolvertype" runat="server">
                                <td width="30%" align="right">Resolver Type
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlResolver" AutoPostBack="true" runat="server" Width="175px"
                                        CssClass="simpletxt1" ValidationGroup="editt" OnSelectedIndexChanged="ddlResolver_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Text="All" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Service Contractor" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="CG Employee" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="CG Contract Employee" Value="5"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <div id="divSC" runat="server" visible="false">
                                <%--END-- Added By Gaurav Garg on 13 Nov for MTO--%>
                                <tr>
                                    <td width="30%" align="right">Service Contractor
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlSerContractor" AutoPostBack="true" runat="server" Width="225px"
                                            CssClass="simpletxt1" ValidationGroup="editt" OnSelectedIndexChanged="ddlSerContractor_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="30%" align="right">Service Engineer
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlServiceEngineer" runat="server" Width="225px" CssClass="simpletxt1"
                                            ValidationGroup="editt">
                                            <asp:ListItem Value="0">All</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <%--Added By Gaurav Garg on 13 Nov for MTO--%>
                            </div>
                            <tr id="trCGExce" runat="server" visible="false">
                                <td width="30%" align="right">CG Employee
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlCGExec" AutoPostBack="true" runat="server" Width="225px"
                                        CssClass="simpletxt1" ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="trCgContractEmp" runat="server" visible="false">
                                <td width="30%" align="right">CG Contract Employee
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlCGContractEmp" AutoPostBack="true" runat="server" Width="225px"
                                        CssClass="simpletxt1" ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <%--END-- Added By Gaurav Garg on 13 Nov for MTO--%>
                            <tr>
                                <td width="30%" align="right">Product Divison
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlProductDivison" runat="server" Width="175px" CssClass="simpletxt1"
                                        ValidationGroup="editt" AutoPostBack="True" OnSelectedIndexChanged="ddlProductDivison_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">Product Line
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlProductLine" runat="server" Width="350px" CssClass="simpletxt1"
                                        ValidationGroup="editt" AutoPostBack="True" OnSelectedIndexChanged="ddlProductLine_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">Complaint Stage
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
                                <td align="right" width="30%">Status (Pending/Close)</td>
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
                                <td width="30%" align="right">Complaint Status
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlCallStatus" runat="server" CssClass="simpletxt1" Width="350px"
                                        ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">Date From
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
                                <td align="right">Complaint No
                                </td>
                                <td align="left">
                                    <asp:TextBox runat="server" ID="txtReqNo" CssClass="txtboxtxt" ValidationGroup="editt" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">Product Serial No
                                </td>
                                <td align="left">
                                    <asp:TextBox runat="server" ID="txtProductSerialNo" CssClass="txtboxtxt" ValidationGroup="editt" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">Defect Category
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlDefectCategory" runat="server" Width="300px" CssClass="simpletxt1"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlDefectCategory_SelectedIndexChanged"
                                        ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">Defect
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlDefect" runat="server" Width="350px" CssClass="simpletxt1"
                                        ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">SRF
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
                                <td align="right">Warranty Status
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
                                <td align="right">Mode Of Receipt
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlModeOfReceipt" runat="server" Width="175" CssClass="simpletxt1">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">Source Of Complaint
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlSourceOfComp" runat="server" AutoPostBack="True" CssClass="simpletxt1" OnSelectedIndexChanged="ddlSourceOfComp_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Text="Select" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Customer" Value="CC-Customer"></asp:ListItem>
                                        <asp:ListItem Text="Dealer" Value="CC-Dealer"></asp:ListItem>
                                        <asp:ListItem Text="ASC" Value="CC-ASC"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblComplaintType" Visible="false" runat="server" Text="Type Of Complaint"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlDealer" runat="server" Visible="false" CssClass="simpletxt1">
                                        <asp:ListItem Selected="True" Text="Select" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Complaint for Dealer Stock Piece"></asp:ListItem>
                                        <asp:ListItem Text="Complaint for customer Piece"></asp:ListItem>
                                        <asp:ListItem Text="Complaint for another Dealer or Retailer Piece"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlASC" runat="server" Visible="false" CssClass="simpletxt1">
                                        <asp:ListItem Selected="True" Text="Select" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Complaint for Dealer" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Complaint for customer" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right"></td>
                                <td align="left">
                                    <br />
                                    <asp:Button Width="70px" Text="Search" CssClass="btn" ValidationGroup="editt" CausesValidation="true"
                                        ID="btnSearch" runat="server" OnClick="btnSearch_Click" />

                                    <asp:Button ID="btnExportExcel" runat="server" Width="114px" Text="Save To Excel"
                                        CssClass="btn" OnClick="btnExportExcel_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">Note: Please use SaveToExcel for large range data.
                                </td>
                                <td align="left">&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="left" class="MsgTDCount">Total Number of Complaints :
                                    <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="MsgTDCount">Total Number of Defects :
                                    <asp:Label ID="lblDefectCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                        <br />
                        <table width="100%" border="0">
                            <tr>
                                <td>
                                    <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both"
                                        PagerStyle-HorizontalAlign="Center" AutoGenerateColumns="False"
                                        ID="gvComm" runat="server" Width="100%" HorizontalAlign="Left" Visible="true" OnRowDataBound="gvComm_RowDataBound">
                                        <RowStyle CssClass="gridbgcolor" />
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
                                            <asp:BoundField DataField="SLA_Date" SortExpression="SLA_Date" HeaderStyle-Width="100px"
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
                                            <asp:TemplateField HeaderStyle-Width="120px" SortExpression="ServiceEng_Name" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderText="Service Engineer">
                                                <ItemTemplate>
                                                    <%#Eval("ServiceEng_Name")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CGEmployee" SortExpression="CGEmployee" HeaderStyle-Width="100px"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="CG Employee">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CGContractEmp" SortExpression="CGContractEmp" HeaderStyle-Width="100px"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="CG Contract Employee">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
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
                                            <asp:BoundField DataField="ComplaintAge" SortExpression="ComplaintAge" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Complaint Age">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="dealer_name" SortExpression="dealer_name" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Dealer Name">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="UserType_Code" HeaderText="" Visible="false" />
                                            <asp:TemplateField HeaderStyle-Width="100px" SortExpression="FileView" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderText="File View">
                                                <ItemTemplate>
                                                      <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false"  
                                                        CommandArgument='<%#Eval("Complaint_RefNo")%>' Text='<%#Eval("FileView")%>' OnClick="LinkButton1_Click">
                                                    </asp:LinkButton>
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
                                </td>
                            </tr>
                            <tr id="trPaging">
                                <td>
                                    <asp:Repeater ID="repager" runat="server" OnItemCommand="repager_ItemCommand">
                                        <ItemTemplate>
                                            <asp:LinkButton Enabled='<%#Eval("Enabled") %>' Width="40px" BorderStyle="Ridge" Style="text-decoration: none; text-align: center;" runat="server" ID="lnkPageNo" Text='<%#Eval("Text") %>' CommandArgument='<%#Eval("Value") %>' CommandName="PageNo"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">

                                    <br />
                                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text="" Visible="false"></asp:Label>
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


