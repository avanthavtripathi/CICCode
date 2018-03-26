<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeFile="OutBoundCallingDetail.aspx.cs"
    Inherits="pages_OutBoundCallingDetail" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function funCommLog(compNo,splitNo,Iscallclosed)
        {
            var strUrl='CommunicationLog.aspx?CompNo='+ compNo + '&SplitNo='+ splitNo + '&Iscallclosed=' + Iscallclosed ;
            window.open(strUrl,'CommunicationLog','height=550,width=750,left=20,top=30');
        }
        function funHistoryLog(compNo,splitNo)
        {
            var strUrl='HistoryLog.aspx?CompNo='+ compNo + '&SplitNo='+ splitNo;
            window.open(strUrl,'History','height=550,width=750,left=20,top=30');
        } 
        
        
        
        
       
    </script>

    <asp:UpdatePanel ID="pnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="1000" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="headingred">
                        Out Bound Call Details
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress AssociatedUpdatePanelID="pnl" ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                </tr>
                <tr>
                    <td class="bgcolorcomm" colspan="2">
                        <asp:Panel ID="panMain" runat="server">
                            <table width="100%" border="0" cellpadding="1" cellspacing="0" style="margin-bottom: 27px">
                                <tr>
                                    <td>
                                        <b>Customer Details</b>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                            <tr>
                                                <td width="25%" style="padding-left: 60px">
                                                    Select Customers
                                                </td>
                                                <td width="25%">
                                                    <asp:DropDownList CssClass="simpletxt1" ID="ddlCustomers" ValidationGroup="grpGo"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="cmvdllCustomer" runat="server" ErrorMessage="Not a Vaild Customer"
                                                        ControlToValidate="ddlCustomers" ValueToCompare="0" Operator="GreaterThan" ValidationGroup="grpGo"
                                                        Type="Integer">*</asp:CompareValidator>
                                                    <asp:Button ID="btnGo" ValidationGroup="grpGo" runat="server" Text="Go" CssClass="btn"
                                                        OnClick="btnGo_Click" />
                                                </td>
                                                <td width="25%" align="right">
                                                    &nbsp;
                                                </td>
                                                <td width="25%">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                            <tr>
                                                <td width="25%" style="padding-left: 60px">
                                                    Pri. Phone:
                                                </td>
                                                <td width="25%">
                                                    <asp:Label ID="txtUnique" runat="server" Text=""></asp:Label>
                                                <%--    <asp:RegularExpressionValidator
                                                        ID="RegularExpressionValidator2" runat="server" ErrorMessage="Vaild Number Required"
                                                        ControlToValidate="txtUnique" SetFocusOnError="True" ValidationExpression="\d{10,11}">*</asp:RegularExpressionValidator>--%>
                                                </td>
                                                <td width="20%">
                                                    Complaint Ref No:
                                                </td>
                                                <td width="30%">
                                                    <asp:TextBox ID="txtComplaintNo" CssClass="txtboxtxt" runat="server" Text=""></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 60px">
                                                    Alt. Phone:
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtAltPhone" runat="server" Text="" />
                                               <%--     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Valid Phone only"
                                                        ControlToValidate="txtAltPhone" SetFocusOnError="True" ValidationExpression="\d{10,11}"></asp:RegularExpressionValidator>--%>
                                                </td>
                                                <td>
                                                    Email:
                                                </td>
                                                <td valign="middle">
                                                    <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 60px">
                                                    Extension:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblExt" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td>
                                                    Fax:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblFax" runat="server" Text=""></asp:Label>&nbsp;<asp:Button ID="btnSearch"
                                                        runat="server" Text="Search" CssClass="btn" OnClick="btnSearch_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 60px">
                                                    Name:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td>
                                                    Address:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 60px">
                                                    Landmark:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblLandmark" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td>
                                                    Country:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCountry" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 60px">
                                                    State:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblState" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td>
                                                    City:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCity" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 60px">
                                                    Pin Code:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPinCode" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td>
                                                    Compnay Name:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCompany" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:HiddenField ID="hdnCustomerId" runat="server" />
                                        <asp:HiddenField ID="hdnComplaintNo" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="panClosedCall" runat="server">
                                            <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <b>Closed calls</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div id="Div1" style="width: 980px; overflow: auto;">
                                                            <!-- Detail grid Start-->
                                                            <asp:GridView ID="gvClosedCalls" runat="server" AllowSorting="True" AlternatingRowStyle-CssClass="fieldName"
                                                                AutoGenerateColumns="False" DataKeyNames="BaseLineId" GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor"
                                                                HorizontalAlign="Left" OnPageIndexChanging="gvClosedCalls_PageIndexChanging"
                                                                PageSize="5" RowStyle-CssClass="gridbgcolor" Width="980px" OnRowDataBound="gvClosedCalls_RowDataBound">
                                                                <RowStyle CssClass="gridbgcolor" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="Sno" HeaderStyle-Width="30px" HeaderText="SNo">
                                                                        <HeaderStyle Width="30px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Complaint_RefNo" HeaderStyle-HorizontalAlign="Left" HeaderText="Pri. Complaint RefNo"
                                                                        ItemStyle-HorizontalAlign="Left">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="115px" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="280px" HeaderText="Sec. Complaint RefNo"
                                                                        ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                                                                                                      
                                                                             <a href="Javascript:void(0);" onclick="funCommonPopUp('<%#Eval("BaseLineId")%>')">
                                                                                <%#Eval("SecComplaint_RefNo")%></a>
                                            
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    
                                                                    
                                                                    <%--<asp:BoundField DataField="SecComplaint_RefNo" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderText="Sec. Complaint RefNo" ItemStyle-HorizontalAlign="Left">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>--%>
                                                                    
                                                                    <asp:BoundField DataField="StageDesc" HeaderStyle-HorizontalAlign="Left" HeaderText="Status"
                                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="180px" />
                                                                        <ItemStyle HorizontalAlign="Left" Wrap="True" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="LoggedDate" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="180px"
                                                                        HeaderText="Log Date" ItemStyle-HorizontalAlign="Left">
                                                                        <HeaderStyle HorizontalAlign="Left"  />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Unit_Desc" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="120px"
                                                                        HeaderText="Prod. Division" ItemStyle-HorizontalAlign="Left">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Quantity" HeaderStyle-HorizontalAlign="Left" HeaderText="Quantity"
                                                                        ItemStyle-HorizontalAlign="Left">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="80px" HeaderText="SC Code"
                                                                        ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <%#Eval("SC_Code")%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="80px" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="280px" HeaderText="Communication History"
                                                                        ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <a href="Javascript:void(0);" onclick="funCommLog('<%#Eval("Complaint_RefNo")%>','<%#Eval("SplitComplaint_RefNo")%>','Y')">
                                                                                <%#Eval("LastComment")%>
                                                                            </a>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left"  />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="80px" HeaderText="Complain History"
                                                                        ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <a href="Javascript:void(0);" onclick="funHistoryLog('<%#Eval("Complaint_RefNo")%>',<%#Eval("SplitComplaint_RefNo")%> )">
                                                                                History</a>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="80px" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="SurveyDone" Visible="false" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-Width="120px" HeaderText="SurveyDone" ItemStyle-HorizontalAlign="Left">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                                <AlternatingRowStyle CssClass="fieldName" />
                                                                <EmptyDataTemplate>
                                                                    <b>No Records found.</b>
                                                                </EmptyDataTemplate>
                                                            </asp:GridView>
                                                            <!-- Detail grid End-->
                                                        </div>
                                                    </td>
                                                    <tr>
                                                        <td >
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                      
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        &nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="panQuestionDetail" runat="server">
                                            <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                                <tr>
                                                    <td colspan="4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" height="1" bgcolor="#60A3AC">
                                                    </td>
                                                </tr>
                                                <tr>
                                               
                                                    <td align="center" colspan="3">
                                                        <b>FeedBack Questions</b>
                                                    </td>
                                                    <td><b><asp:Label  ID="lblRemarks" Text="Remarks" runat ="server" ></asp:Label></b></td>
                                                </tr>
                                                <tr>
                                                    <td bgcolor="#60A3AC" colspan="4" height="1">
                                                    </td>
                                                </tr>
                                                <tr>
                                                   <td colspan ="4"></td>
                                                    
                                                </tr>
                                                <%--<b>Check One for all</b> &nbsp;&nbsp;&nbsp;
                                                        </td><td colspan="3">--%>
                                                <tr>
                                                    <td Width="325px">
                                                    <asp:Label ID="lblClosureComplaintstatus" runat="server" Text ="Is your complaint for (product division) closed to your satisfaction?."></asp:Label>
                                                    </td>
                                                    <td colspan="2" Width="375px">
                                                    <asp:RadioButtonList RepeatDirection ="Horizontal"  CausesValidation="true" ID="rboClosurestatus" 
                                                            runat ="server" AutoPostBack="True" 
                                                            onselectedindexchanged="rboClosurestatus_SelectedIndexChanged" >
                                                    <asp:ListItem Selected="True" Text ="Yes" Value ="1"></asp:ListItem>
                                                    <asp:ListItem Text ="No" Value ="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    </td>
                                                    <td>
                                                    <asp:TextBox ID="txtnotClosureStatus" runat ="server" MaxLength="1000" 
                                                            TextMode="MultiLine" ></asp:TextBox>
                                                    </td>
                                                   
                                                </tr>
                                                 <tr>
                                                    <td Width="325px">
                                                        <asp:Label ID="lbldissatsfy" Visible="false" runat="server" Text="Dis-satsfaction Reason"></asp:Label>
                                                    </td>
                                                    <td colspan="3" Width="375px">
                                                        <asp:DropDownList CssClass="simpletxt1" ID="ddlReason" Visible="false" Width="300px" ValidationGroup="grp" runat="server">
                                                            <asp:ListItem Selected="True" Text="<Select>" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Product still not working" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Dispute on warranty validity" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="No response by the ASC and complaint is still not attended" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="Multiple visits, but the problem still remains" Value="4"></asp:ListItem>
                                                            <asp:ListItem Text="Customer wants Replacement only" Value="5"></asp:ListItem>
                                                            <asp:ListItem Text="Customer is not willing to take the defective product to ASC/service center, as desired by the service executive"
                                                                Value="6"></asp:ListItem>
                                                            <asp:ListItem Text="Customer does not want to pay, after misusing his product in warranty."
                                                                Value="7"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvReason" runat="server" 
                                                            ControlToValidate="ddlReason" ErrorMessage="Required." InitialValue="0" 
                                                            ValidationGroup="grp" Visible="false" Enabled="false"></asp:RequiredFieldValidator>
                                                    </td>
                                                    
                                                    
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <div id="dvGrid" runat ="server" >                                                          
                                <asp:GridView ID="gvQuestion" runat="server" AlternatingItemStyle-CssClass="fieldName"
                                                       Width="100%"  GridLines="Both"  HeaderStyle-CssClass="fieldNamewithbgcolor" 
                                                                ShowHeader ="false"  ItemStyle-CssClass="gridbgcolor" AutoGenerateColumns="False" 
                                            onrowdatabound="gvQuestion_RowDataBound" onrowcommand="gvQuestion_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Question">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQuestion" runat="server" Text='<%#Eval("Question")%>'></asp:Label>
                                                        <asp:HiddenField ID="hidQuestionCode" runat="server" 
                                                            Value='<%#Eval("Question_Code")%>' />
                                                        <asp:HiddenField ID="hidQuestionType" runat="server" 
                                                            Value='<%#Eval("Question_Type")%>' />
                                                    </ItemTemplate>
                                                     <ItemStyle HorizontalAlign="Left" Width="320px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ans">
                                                    <ItemTemplate>
	                                              <asp:RadioButtonList ID="rdoListScale" runat="server" CssClass="simpletxt1" RepeatDirection="Horizontal">
                                                        </asp:RadioButtonList>

                                                        <asp:RadioButtonList ID="rdoListYesNo" runat="server" CssClass="simpletxt1" RepeatDirection="Horizontal">
                                                      </asp:RadioButtonList>
                                                          <asp:HiddenField ID="hidAnsCode" runat ="server" />
                                                    </ItemTemplate>
                                                     <ItemStyle HorizontalAlign="Left" Width="400px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Show">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtReason" TextMode ="MultiLine" MaxLength ="1000" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            
                                            </Columns>
                                               <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                        </asp:GridView>
                                                            <!-- Detail grid End-->
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="left" colspan="2" style="width: 171px">
                                                        <asp:TextBox ID="txtComment" Visible="false" runat="server" ValidationGroup="grp"
                                                            CssClass="txtboxtxt" Height="98px" TextMode="MultiLine" Width="358px"></asp:TextBox>
                                                    </td>
                                                    <td align="left" valign="top">
                                                        <asp:RadioButtonList ID="rdoListCustResponseMaster" runat="server" CssClass="simpletxt1"
                                                            RepeatDirection="Horizontal" RepeatColumns="3" Height="81px" Width="450px">
                                                        </asp:RadioButtonList>
                                                        &nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td align="left">
                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" colspan="2" valign="top" >
                                                        Select Disposition<font color="red">*</font> :
                                                       
                                                    </td>
                                                    <td >
                                                        &nbsp;
                                                        <asp:DropDownList ID="ddlDispositions" runat="server">
                                                            <asp:ListItem Selected="True" Text="Select Disposition" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Call Back Later" Value="Call Back Later"></asp:ListItem>
                                                            <asp:ListItem Text="Call Disconnected" Value="Call Disconnected"></asp:ListItem>
                                                            <asp:ListItem Text="Called Party Hung Up" Value="Called Party Hung Up"></asp:ListItem>
                                                            <asp:ListItem Text="Client Call" Value="Client Call"></asp:ListItem>
                                                            <asp:ListItem Text="Contact Not Established" Value="Contact Not Established"></asp:ListItem>
                                                            <asp:ListItem Text="Feedback Call Completed" Value="Feedback Call Completed"></asp:ListItem>
                                                            <asp:ListItem Text="Language Barrier" Value="Language Barrier"></asp:ListItem>
                                                            <asp:ListItem Text="Right Party Not available" 
                                                                Value="Right Party Not available"></asp:ListItem>
                                                            <asp:ListItem Text="Test Call" Value="Test Call"></asp:ListItem>
                                                        </asp:DropDownList>
                                                         <br />
                                                        <asp:RequiredFieldValidator ControlToValidate="ddlDispositions" InitialValue="0"
                                                            ValidationGroup="grp" ID="rfvDispostions" runat="server" ErrorMessage="Disposition required."
                                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save Comment" ValidationGroup="grp"
                                                            OnClick="btnSave_Click" />&nbsp; &nbsp;&nbsp;<asp:Button ID="btnClosed" runat="server" Text="Final Closed"
                                                            CssClass="btn" CausesValidation="false" OnClick="btnClosed_Click" Visible="false" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" valign="middle" colspan="1">
                                                        <asp:CheckBox Visible="false" ID="chkInterstedInOtherProd" runat="server" Text="Interested in Other Product" />
                                                    </td>
                                                    <td align="left" valign="middle" colspan="3">
                                                        <asp:Label ID="lblSurveyMsg" ForeColor="Red" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
