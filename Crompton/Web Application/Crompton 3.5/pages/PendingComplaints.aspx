<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="PendingComplaints.aspx.cs" Inherits="pages_PendingComplaints" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="PendingSerRegReport" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script language="javascript" type="text/javascript">
         function funUserDetail(custNo, compNo)
        {
            var strUrl = '../Reports/CustomerDetail.aspx?custNo=' + custNo + '&CompNo=' + compNo;
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

        function funUploadPopUp(CRefNo) {
            var strUrl = '../Pages/UploadedFilePopUp.aspx?CompNo=' + CRefNo;
            custWin = window.open(strUrl, 'SCPopup', 'height=550,width=750,left=20,top=30,scrollbars=1');
            if (window.focus) { custWin.focus() }
        }  
        
    </script>

    <asp:UpdatePanel ID="updatepnl" runat="server">
             <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                       MD's office Complaint Report
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
                                     <span style="color: #FF3300">*</span> Business Line
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlBusinessLine" AutoPostBack="true" runat="server" Width="175px"
                                        CssClass="simpletxt1" ValidationGroup="editt" OnSelectedIndexChanged="ddlBusinessLine_SelectedIndexChanged">
                                    </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="ddlBusinessLine" 
                                 InitialValue="0" SetFocusOnError="true" ValidationGroup="editt"   ></asp:RequiredFieldValidator>
                                </td>
                            </tr>
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
                                    <asp:DropDownList ID="ddlBranch" runat="server" Width="175px" 
                                        CssClass="simpletxt1" ValidationGroup="editt">
                                    <asp:ListItem Text="All" Value="0"></asp:ListItem>
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
                                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr runat="server" visible="false">
                                <td width="30%" align="right">
                                    Product Line
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlProductLine" runat="server" Width="350px" CssClass="simpletxt1"
                                        ValidationGroup="editt">
                                          <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Complaint Stage
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlCallStage" runat="server" CssClass="simpletxt1" Width="175px"
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
                                <td align="right">
                                    Complaint No
                                </td>
                                <td align="left">
                                    <asp:TextBox runat="server" ID="txtReqNo" CssClass="txtboxtxt" ValidationGroup="editt" />
                                </td>
                            </tr>
                              <tr>
                                <td align="right" width="30%">
                                    MD's Complaints</td>
                                <td align="left">
                                      <asp:DropDownList ID="ddlmdcomplaints" runat="server" CssClass="simpletxt1" Width="350px"
                                        ValidationGroup="editt">
                                        <asp:ListItem Text="Select" Value="null"></asp:ListItem>
                                        <asp:ListItem Text="Yes" Selected="True" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                   Complaint Date From
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
                                    <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both"
                                        PagerStyle-HorizontalAlign="Center" AutoGenerateColumns="False"
                                        ID="gvComm" runat="server" Width="100%"
                                        HorizontalAlign="Left" Visible="true"  
                                        OnRowDataBound="gvComm_RowDataBound">
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <asp:BoundField DataField="RowNumber" HeaderStyle-Width="40px" HeaderText="SNo">
                                                <HeaderStyle Width="40px" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Complaint_RefNo" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderText="Complaint No">
                                                <ItemTemplate>
                                                    <a href="Javascript:void(0);" onclick="funCommonPopUpReport(<%#Eval("BaseLineId")%>)">
                                                        <%--<%#Eval("Complaint_RefNo")%>/<%#Eval("splitComplaint_RefNo")%></a>--%>
                                                        <%#Eval("Complaint_Split")%></a>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="100px" SortExpression="CustomerId" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderText="Customer">
                                                <ItemTemplate>
                                                    <a href="Javascript:void(0);" onclick="funUserDetail('<%#Eval("CustomerId")%>','<%#Eval("Complaint_RefNo")%>')">
                                                        <%--<%#Eval("CustomerId")%></a>--%>
                                                        <%#Eval("FirstName")%>
                                                        <%#Eval("LastName")%>
                                                    </a>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" />
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
                                                <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="120px" SortExpression="ServiceEng_Name" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderText="Service Engineer">
                                                <ItemTemplate>
                                                    <%#Eval("ServiceEng_Name")%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <%--Added By Gaurav Garg--%>
                                            <asp:BoundField DataField="CGEmployee" SortExpression="CGEmployee" HeaderStyle-Width="100px"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="CG Employee">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                           
                                            <%--END--%>
                                            <asp:BoundField DataField="StageDesc" SortExpression="StageDesc" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Complaint Status">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                                 <asp:BoundField DataField="ProductDivision_Desc" SortExpression="ProductDivision_Desc"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Product Division">
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
                                            <asp:BoundField DataField="UserType_Code" HeaderText="" Visible="false" />
                                            <asp:TemplateField HeaderText="File" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                <div id="LinkButton1" runat="server" aaa='<%#Eval("FileView")%>' bbb= '<%#Eval("Complaint_RefNo")%>'   >Click</div>
                                                 <%--   <a id="LinkButton1" runat="server" aaa='<%#Eval("FileView")%>' onclick=';' title="Click" >
                                                       </a>--%><%--   <a id="LinkButton1" runat="server" aaa='<%#Eval("FileView")%>' funUploadPopUp(<%# Eval("Complaint_RefNo")%>) onclick='funUploadPopUp(<%# Eval("Complaint_RefNo")%>);' title="Click" >
                                                       </a>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        <%--    <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderText="File View">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName='<%#Eval("File_Name")%>'
                                                        CommandArgument='<%#Eval("Complaint_RefNo")%>' OnClick="LinkButton1_Click" Text='<%#Eval("FileView")%>'>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>--%>
                                              <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderText="MD's Complaints">
                                                <ItemTemplate>
                                               
                                                   <asp:CheckBox ID="chk" runat="server"  
                                                        Checked='<%# Eval("ismdcomplaint")%>' CNo='<%#Eval("Complaint_RefNo")%>' 
                                                        SpNo='<%#Eval("Complaint_Split")%>'  AutoPostBack="True" 
                                                        oncheckedchanged="chk_CheckedChanged" />
                                                 
                                           </ItemTemplate>
                                              
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
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
                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                        <AlternatingRowStyle CssClass="fieldName" />
                                    </asp:GridView>
                                    <!-- End Action Listing -->
                                </td>
                            </tr>
                            <tr id="trPaging">
                                <td>
                                   <%-- 19 dec 2011
								    <asp:Button ID="btnPre" runat="server" CssClass="btn" OnClick="btnPre_Click" Text="<<" />
                                    <asp:Button ID="btnNxt" runat="server" CssClass="btn" OnClick="btnNxt_Click" Text=">>" />
                             --%>  
                           <%--   <-- added bhawesh 14 dec 11 for custom Paging -->--%>
                                                  <asp:Repeater ID="repager" runat="server" onitemcommand="repager_ItemCommand"  >
                                                    <ItemTemplate>
                                                    <asp:LinkButton ID="lbtn" runat="server" Text='<%# Container.DataItem %>' Font-Bold="false" ></asp:LinkButton>
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
                        <br />
                        <!-- Excel Grid -->
                        <!-- End excelGrid -->
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
