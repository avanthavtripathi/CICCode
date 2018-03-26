<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="SCTR.aspx.cs"  Inherits="pages_SCTR" MaintainScrollPositionOnPostback="true" ValidateRequest="false" %>
<%@ Register Src="~/UC/PendingForSpare.ascx" TagPrefix="uc1" TagName="PendingForSpareCtrl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <link href="../css/Popup.css" rel="stylesheet" type="text/css" />
<script src="../scripts/SCTR.js" type="text/javascript"></script>

    <asp:UpdatePanel ID="updateSC" runat="server">
        <ContentTemplate>
        <asp:HiddenField ID="hdnComplaintFlag" Value="" runat="server" />
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="headingred" style="width: 30%">
                        Service Contractor
                        <asp:HiddenField ID="hdnGlobalDate" runat="server" />
                        <asp:HiddenField ID="HDComp" runat="server" />
                    </td>
                    <td align="center" class="headingred" style="width: 40%">
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px; width: 30%">
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" class="bgcolorcomm" colspan="3">
                        <table width="100%">
                            <tr>
                                <td align="left" width="20%">
                                    Stage:
                                </td>
                                <td align="left" width="30%">
                                    <asp:DropDownList ID="ddlStage" CssClass="simpletxt1" Width="175" runat="server"
                                        AutoPostBack="true" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlStage_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="Initialization">Initialization</asp:ListItem>
                                        <asp:ListItem Value="WIP">WIP</asp:ListItem>
                                        <asp:ListItem Value="Closure">Closure</asp:ListItem>
                                        <asp:ListItem Value="TempClosed">TempClosed</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="left" width="20%">City:</td>
                                <td align="left" width="30%">
                                    <asp:DropDownList ID="ddlCity" runat="server" CssClass="simpletxt1" Width="175">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">Status:</td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlStatus" CssClass="simpletxt1" Width="400" runat="server" AppendDataBoundItems="True">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="left">
                                    Product Division:
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlSearchProductDivision" runat="server" CssClass="simpletxt1" Width="175">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">Complaint Ref. No.:</td>
                                <td align="left">
                                <asp:TextBox ID="txtComplaintRefNo" Width="170" MaxLength="11" runat="server" CssClass="txtboxtxt"></asp:TextBox>
                                </td>
                                <td align="left">SRF:</td>
                                <td align="left">
                                     <asp:DropDownList ID="ddlSrf" CssClass="simpletxt1" Width="175" runat="server">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="N">No</asp:ListItem>
                                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" height="10px">Total Number of Records :</td>
                                <td align="left" colspan="3">
                                    <asp:Label ID="lblRowCount" Text="0" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                </td>
                                
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:HiddenField ID="hdnSLADate" runat="server" />
                                    <asp:HiddenField ID="hdnoldcomplaint" runat="server" />
                                    <asp:HiddenField ID="hdnInternational" runat="server" Value="0" />
                                </td>
                                <td align="center" colspan="2">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn" OnClick="btnSearch_Click"
                                        OnClientClick="javascript:SetCounter()" CausesValidation="false" Text="Search"
                                        Width="70px" />
                                </td>
                                <td align="left"></td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">
                                    <asp:GridView ID="gvFresh" runat="server" AllowPaging="True" AllowSorting="True"
                                        AlternatingRowStyle-CssClass="fieldName" AutoGenerateColumns="False" DataKeyNames="BaseLineId"
                                        GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor" Width="100%" RowStyle-CssClass="gridbgcolor"
                                        OnRowDataBound="gvFresh_RowDataBound">
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkHeader" runat="server" onclick="javascript:SelectAllCheckboxes(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkChild" onclick="javascript:ChildClick(this);" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="RowNumber" HeaderText="Sno." />
                                            <asp:TemplateField HeaderText="Complaint RefNo" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnBaselineID" runat="server" Value='<%#Eval("BaseLineId")%>' />
                                                    <asp:HiddenField ID="hdnProductDivision_Sno" runat="server" Value='<%#Eval("ProductDivision_Sno")%>' />
                                                    <asp:HiddenField ID="hdnUnit_Desc" runat="server" Value='<%#Eval("ProductDivision_Desc")%>' />
                                                    <asp:HiddenField ID="hdnComplaint_RefNo" runat="server" Value='<%#Eval("Complaint_RefNo")%>' />
                                                    <asp:HiddenField ID="hdnAppointmentFlag" runat="server" Value='<%#Eval("AppointmentReq")%>' />
                                                    <asp:HiddenField ID="gvFreshhdnCallStatus" runat="server" Value='<%#Eval("CallStatus")%>' />
                                                    <asp:HiddenField ID="gvFreshhdnCallStage" runat="server" Value='<%#Eval("CallStage")%>' />
                                                    <asp:HiddenField ID="hdnProdSNo" runat="server" Value='<%#Eval("Product_SNo")%>' />
                                                    <asp:HiddenField ID="gvFreshhdnASCReAllocFlag" runat="server" Value='<%#Eval("ASCReAllocFlag")%>' />
                                                    <asp:HiddenField ID="hdnSplitComplaint_RefNo" runat="server" Value='<%#Eval("SplitComplaint_RefNo")%>' />
                                                    <asp:HiddenField ID="hdnSc_Sno" runat="server" Value='<%#Eval("SC_SNo")%>' />
                                                    <a href="Javascript:void(0);" onclick="funCommonPopUp(<%#Eval("BaseLineId")%>)">
                                                        <%#Eval("Complaint_RefNo")%>/<%#Eval("split")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%#Eval("ComplaintDate", "{0:dd/MM/yyyy}")%> <%#Eval("ComplaintDate", "{0:T}")%>
                                                    <asp:HiddenField ID="hdnComplaintLoggedDate" runat="server" Value='<%#Eval("ComplaintDate")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <a href="Javascript:void(0);" onclick="funCustomerMaster('<%#Eval("CustomerId")%>','<%#Eval("Complaint_RefNo")%>')">
                                                        <%#Eval("CustName")%></a>
                                                    <asp:HiddenField ID="gvFreshhdnLastComment" runat="server" Value='<%#Eval("LastComment")%>' />
                                                    <asp:HiddenField ID="gvFreshhdnLastName" runat="server" Value='<%#Eval("CustName")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="gvFreshhdnFullAddress" runat="server" Value='<%#Eval("FullAddress")%>' />
                                                    <%#Eval("Address1")%><%#Eval("Address2")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contact No." HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%#Eval("UniqueContact_No")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%#Eval("ProductDivision_Desc")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate><%#Eval("Quantity")%></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Engineer Name" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSEname" runat="server" Text='<%# Eval("SEName") %>' LoggedBy='<%# Eval("LoggedBy") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SRF" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%#Eval("SRF") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Comm. History" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <a href="Javascript:void(0);" onclick="funCommLog('<%#Eval("Complaint_RefNo")%>',<%#Eval("SplitComplaint_RefNo")%>)">
                                                        <%#Eval("LastComment")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Stage" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <a href="Javascript:void(0);" onclick="funHistoryLog('<%#Eval("Complaint_RefNo")%>',<%#Eval("SplitComplaint_RefNo")%>)">
                                                        <%#Eval("CallStage")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="File" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <a href="Javascript:void(0);" onclick="funUploadPopUp('<%#Eval("Complaint_RefNo")%>')">Click</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkBtnNext" CommandArgument='<%#Eval("Complaint_RefNo")%>' CausesValidation="false"
                                                        runat="server" CommandName='<%#Eval("CallStatus")%>' Text="Next" OnClick="lnkBtnNext_Click">
                                                    </asp:LinkButton>
                                                    <a href="Javascript:void(0);" onclick="funPrint('<%#Eval("Complaint_RefNo")%>','<%#Eval("BaseLineId")%>','<%#Eval("ProductDivision_Sno")%>')">
                                                        / Print</a>
                                                        <asp:HiddenField ID="hdnWarrantyStatus" runat="server" Value='<%# Eval("WarrantyStatus") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                        <AlternatingRowStyle CssClass="fieldName" />
                                        <EmptyDataTemplate>
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                        <b>No Complaints found</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                    <asp:Button ID="btnPre" runat="server" CssClass="btn" OnClick="btnPre_Click" Text="<<" />
                                    <asp:Button ID="btnNxt" runat="server" CssClass="btn" OnClick="btnNxt_Click" Text=">>" />
                                    &nbsp;
                                    <asp:Button ID="btnPrint" runat="server" Text="Print Multiple" CssClass="btn" OnClientClick="alert('Please Use IE7/IE8 Or Firefox To Print Multiple Complaints.');" OnClick="btnPrint_Click" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table id="tbLifted" runat="server" width="100%" class="bgcolorcomm" cellpadding="3"
                            visible="false" border="0">
                            <tr>
                                <td height="20" colspan="2" align="left" valign="bottom" style="border-bottom: 1px solid #396870">
                                    <b>Action: Equipment Lifted by Customer</b>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">Complaint RefNo: </td>
                                <td align="left">
                                    <asp:Label ID="eqlblComplaint" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Action Remarks:<font color="red">*</font>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="eqtxtRemarks" CssClass="txtboxtxt" ValidationGroup="eqp" Width="200px"
                                        runat="server" />
                                    <asp:HiddenField ID="eqphdnBaselineID" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtInitialActionDate"
                                        ErrorMessage="Enter Action Remarks" Display="None" ValidationGroup="eqp">
                                    </asp:RequiredFieldValidator>
                                    <asp:ValidationSummary ID="eqpValidSummary" ShowSummary="False" runat="server" ValidationGroup="eqp"
                                        ShowMessageBox="true" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="eqbtnLifted" ValidationGroup="eqp" runat="server" Text="Equipment Lifted"
                                        CssClass="btn" OnClick="eqbtnLifted_Click" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" class="bgcolorcomm" cellpadding="3" id="tbIntialization" runat="server"
                            visible="true" border="0">
                            <tr>
                                <td height="20" align="left" valign="bottom" style="border-bottom: 1px solid #396870">
                                    <b>Initialization</b>
                                </td>
                            </tr>
                            <tr>
                                <td height="20" align="left" style="padding-left: 200px">
                                    Action:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlInitAction" runat="server" Width="417" CssClass="simpletxt1"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlInitAction_SelectedIndexChanged" />
                                    <asp:RequiredFieldValidator ID="RfvddlInitAction" runat="server" ControlToValidate="ddlInitAction"
                                        InitialValue="0" ErrorMessage="Select Action" Display="None" ValidationGroup="init" />
                                    <asp:LinkButton ID="LnkActivity" runat="server" OnClick="LnkActivity_Click" ForeColor="#1c3d74"
                                        Visible="False">Add Activity</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td width="100%" height="0" align="left" style="padding-left: 260px">
                                <div runat="server" id="trInitAppointDateTime" style="display:none;">
                                    <table align="left" border="0">
                                        <tr>
                                            <td align="left" width="75%">
                                                Appointment Date:
                                                <asp:TextBox ID="txtAppointMentDate" runat="server" Width="100" CssClass="txtboxtxt"></asp:TextBox>
                                                <asp:CustomValidator ID="CustomVali11" runat="server" ControlToValidate="txtAppointMentDate"
                                                    Display="None" ErrorMessage="Appointment Date cannot be past date" ClientValidationFunction="validateAppointmentDatedt"
                                                    ValidationGroup="init"> </asp:CustomValidator>
                                                <cc1:CalendarExtender ID="CalendarExtender3" TargetControlID="txtAppointMentDate"
                                                    runat="server">
                                                </cc1:CalendarExtender>
                                                Appointment Time:<asp:DropDownList ID="ddlInitAppHr" runat="server" ValidationGroup="init"
                                                    CssClass="simpletxt1">
                                                    <asp:ListItem>1</asp:ListItem>
                                                    <asp:ListItem>2</asp:ListItem>
                                                    <asp:ListItem>3</asp:ListItem>
                                                    <asp:ListItem>4</asp:ListItem>
                                                    <asp:ListItem>5</asp:ListItem>
                                                    <asp:ListItem>6</asp:ListItem>
                                                    <asp:ListItem>7</asp:ListItem>
                                                    <asp:ListItem>8</asp:ListItem>
                                                    <asp:ListItem>9</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>11</asp:ListItem>
                                                    <asp:ListItem>12</asp:ListItem>
                                                </asp:DropDownList>
                                                :
                                                <asp:DropDownList ID="ddlInitAppMin" runat="server" ValidationGroup="init" CssClass="simpletxt1">
                                                    <asp:ListItem Value="0">00</asp:ListItem>
                                                    <asp:ListItem Value="1">01</asp:ListItem>
                                                    <asp:ListItem Value="2">02</asp:ListItem>
                                                    <asp:ListItem Value="3">03</asp:ListItem>
                                                    <asp:ListItem Value="4">04</asp:ListItem>
                                                    <asp:ListItem Value="5">05</asp:ListItem>
                                                    <asp:ListItem Value="6">06</asp:ListItem>
                                                    <asp:ListItem Value="7">07</asp:ListItem>
                                                    <asp:ListItem Value="8">08</asp:ListItem>
                                                    <asp:ListItem Value="9">09</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>11</asp:ListItem>
                                                    <asp:ListItem>12</asp:ListItem>
                                                    <asp:ListItem>13</asp:ListItem>
                                                    <asp:ListItem>14</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>16</asp:ListItem>
                                                    <asp:ListItem>17</asp:ListItem>
                                                    <asp:ListItem>18</asp:ListItem>
                                                    <asp:ListItem>19</asp:ListItem>
                                                    <asp:ListItem>20</asp:ListItem>
                                                    <asp:ListItem>21</asp:ListItem>
                                                    <asp:ListItem>22</asp:ListItem>
                                                    <asp:ListItem>23</asp:ListItem>
                                                    <asp:ListItem>24</asp:ListItem>
                                                    <asp:ListItem>25</asp:ListItem>
                                                    <asp:ListItem>26</asp:ListItem>
                                                    <asp:ListItem>27</asp:ListItem>
                                                    <asp:ListItem>28</asp:ListItem>
                                                    <asp:ListItem>29</asp:ListItem>
                                                    <asp:ListItem>30</asp:ListItem>
                                                    <asp:ListItem>31</asp:ListItem>
                                                    <asp:ListItem>32</asp:ListItem>
                                                    <asp:ListItem>33</asp:ListItem>
                                                    <asp:ListItem>34</asp:ListItem>
                                                    <asp:ListItem>35</asp:ListItem>
                                                    <asp:ListItem>36</asp:ListItem>
                                                    <asp:ListItem>37</asp:ListItem>
                                                    <asp:ListItem>38</asp:ListItem>
                                                    <asp:ListItem>39</asp:ListItem>
                                                    <asp:ListItem>40</asp:ListItem>
                                                    <asp:ListItem>41</asp:ListItem>
                                                    <asp:ListItem>42</asp:ListItem>
                                                    <asp:ListItem>43</asp:ListItem>
                                                    <asp:ListItem>44</asp:ListItem>
                                                    <asp:ListItem>45</asp:ListItem>
                                                    <asp:ListItem>46</asp:ListItem>
                                                    <asp:ListItem>47</asp:ListItem>
                                                    <asp:ListItem>48</asp:ListItem>
                                                    <asp:ListItem>49</asp:ListItem>
                                                    <asp:ListItem>50</asp:ListItem>
                                                    <asp:ListItem>51</asp:ListItem>
                                                    <asp:ListItem>52</asp:ListItem>
                                                    <asp:ListItem>53</asp:ListItem>
                                                    <asp:ListItem>54</asp:ListItem>
                                                    <asp:ListItem>55</asp:ListItem>
                                                    <asp:ListItem>56</asp:ListItem>
                                                    <asp:ListItem>57</asp:ListItem>
                                                    <asp:ListItem>58</asp:ListItem>
                                                    <asp:ListItem>59</asp:ListItem>
                                                    <asp:ListItem>60</asp:ListItem>
                                                </asp:DropDownList>
                                                :
                                                <asp:DropDownList ID="ddlInitAppAm" runat="server" ValidationGroup="init" CssClass="simpletxt1">
                                                    <asp:ListItem>AM</asp:ListItem>
                                                    <asp:ListItem>PM</asp:ListItem>
                                                </asp:DropDownList>
                                                
                                            </td>
                                        </tr>
                                    </table>
                                    </div>
                                </td>
                            </tr>
                            <tr id="trInitEngineer" visible="false" runat="server">
                                <td width="100%" height="20" align="left" style="padding-left: 260px">
                                    <table align="left" border="0">
                                        <tr>
                                            <td align="left" width="75%">
                                                Engineer:<asp:DropDownList ID="ddlServiceEngg" runat="server" Width="200" CssClass="simpletxt1"
                                                    ValidationGroup="init">
                                                    <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:CompareValidator ID="cmpDdlEngg" runat="server" ControlToValidate="ddlServiceEngg"
                                                    ErrorMessage="Select Engineer" ValueToCompare="Select" ValidationGroup="init"
                                                    Operator="NotEqual" Display="none"></asp:CompareValidator>
                                            </td>
                                            <td align="right" width="20%">
                                                <asp:Label ID="lblSMS" runat="server" Text="Send SMS"></asp:Label>
                                            </td>
                                            <td width="5%"><asp:CheckBox ID="chkSMS" runat="server" Checked="True" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="padding-left: 200px">
                                    Remarks:<font color="red">*</font> &nbsp;<asp:TextBox ID="txtInitializationActionRemarks" Text="" runat="server"
                                        Width="405" CssClass="txtboxtxt"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator9"
                                            runat="server" ControlToValidate="txtInitializationActionRemarks" ErrorMessage="Enter Action Remarks"
                                            Display="None" ValidationGroup="init">
                                        </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="padding-left: 180px">
                                    Action Date:<font color="red">*</font>
                                    <asp:TextBox ID="txtInitialActionDate" CssClass="txtboxtxt" runat="server">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtInitialActionDate"
                                        ErrorMessage="Enter Action Date" Display="Dynamic" ValidationGroup="init">
                                    </asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender4" TargetControlID="txtInitialActionDate"
                                        runat="server">
                                    </cc1:CalendarExtender>
                                    <asp:CompareValidator Operator="DataTypeCheck" Type="Date" ControlToValidate="txtInitialActionDate"
                                        Display="Dynamic" ErrorMessage="Not a vaild Date" runat="server" ID="CompareValidator1"
                                        ValidationGroup="init">
                                    </asp:CompareValidator>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Action Time:
                                    <asp:Label ID="LblTimeInit" runat="server"></asp:Label>
                                    <asp:ValidationSummary ID="valsuminitial" runat="server" ValidationGroup="init" ShowMessageBox="True"
                                        ShowSummary="False" />
                                    <asp:CustomValidator ValidationGroup="init" ID="CustomValidator2" runat="server"
                                        ControlToValidate="txtInitialActionDate" Display="Dynamic" ErrorMessage="Action Date cannot be less then two days from current date"
                                        ClientValidationFunction="validateInitializeDate">
                                    </asp:CustomValidator>
                                    <asp:CustomValidator ValidationGroup="init" ID="CustomValidator10" runat="server"
                                        ControlToValidate="txtInitialActionDate" Display="Dynamic" ErrorMessage="Action Date can not be future date"
                                        ClientValidationFunction="validateInitializeDate1">
                                    </asp:CustomValidator>
                                    <asp:HiddenField ID="hdnInitDate" runat="server" />
                                </td>
                            </tr>
                            <tr id="trApplianceDemo" runat="server">
                                <td align="center">
                                    <table style="width: 90%;">
                                        <tr>
                                            <td colspan="4" height="20" align="left" valign="bottom" style="border-bottom: 1px solid #396870">
                                                <b>Product Details</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                Product Division :
                                                <asp:Label ID="lblDemoProductDivision" Text="" runat="server"></asp:Label>&nbsp|
                                                Complaint RefNo :
                                                <asp:Label ID="lblDemoComplaintRefNo" Text="" runat="server"></asp:Label>
                                                <asp:HiddenField ID="hdnDemoProductDiv" runat="server" />
                                                <asp:HiddenField ID="hdnMfgUnitDemo" runat="server" />
                                            </td>
                                            <td style="text-align: right;">Complaint Date :</td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblDemocComplaintRefDate" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">Product Line:</td>
                                            <td style="text-align: left;">
                                                <asp:DropDownList ID="ddlDemoProductLine" runat="server" CssClass="simpletxt1" Width="250px">
                                                    <asp:ListItem Text="KA-FP(073)" Value="85" Selected="True"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: right;">
                                                Product Serial:<font color="Red">*</font>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtDemoPSerialNo" runat="server" Text="" CssClass="txtboxtxt" MaxLength="13"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rqftxtDemoPSerialNo" runat="server" ControlToValidate="txtDemoPSerialNo"
                                                    ErrorMessage="Enter Product Serial No." ValidationGroup="init" Display="None" />
                                                <asp:CustomValidator ValidationGroup="init" ID="csttxtDemoPSerialNo" runat="server"
                                                    ControlToValidate="txtDemoPSerialNo" Display="None" ErrorMessage="Product Serial No is not Valid"
                                                    ClientValidationFunction="validateBatchCode" />
                                                <asp:HiddenField ID="hdntxtDemoPSerialNo1" runat="server" />
                                                <asp:HiddenField ID="hdntxtDemoPSerialNo2" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">Product Group:</td>
                                            <td style="text-align: left;">
                                                <asp:DropDownList ID="ddlDemoProductGroup" runat="server" CssClass="simpletxt1" Width="250px">
                                                    <asp:ListItem Selected="True" Text="FOOD PROCESSOR(APFP01)" Value="226"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: right;">Batch No:</td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtDemoBatchNO" runat="server" Text="" CssClass="txtboxtxt"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="rgVtxtDemoBatchNO" runat="server" ControlToValidate="txtDemoBatchNO"
                                        ValidationGroup="init" ErrorMessage="First two letters should alphabatic in Product Serial No."
                                        ValidationExpression="[a-zA-Z]{2,}.*" Display="None"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">Product:<font color="Red">*</font></td>
                                            <td style="text-align: left;" colspan="3">
                                                <asp:DropDownList ID="ddlDemoProduct" runat="server" CssClass="simpletxt1" Width="400px">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rqfddlDemoProduct" runat="server" ControlToValidate="ddlDemoProduct"
                                                    ErrorMessage="Select Product" InitialValue="0" ValidationGroup="init" Display="None"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">FIR Date:<font color="Red">*</font></td>
                                            <td style="text-align: left;" colspan="3">
                                                <asp:TextBox ID="txtDemoFirDate" CssClass="txtboxtxt" runat="server"></asp:TextBox>
                                                <cc1:CalendarExtender ID="DemoCalendarExtender" TargetControlID="txtDemoFirDate"
                                                    runat="server">
                                                </cc1:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rqftxtDemoFirDateDemo" runat="server" ControlToValidate="txtDemoFirDate"
                                                    ErrorMessage="Enter FIR Date" ValidationGroup="init" Display="None"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator Operator="DataTypeCheck" Type="Date" ControlToValidate="txtDemoFirDate"
                                                    Display="None" ErrorMessage="Not a vaild Date" runat="server" ID="cmptxtDemoFirDate"
                                                    ValidationGroup="init">
                                                </asp:CompareValidator>
                                                <asp:CustomValidator ValidationGroup="init" ID="cmftxtDemoFirDate1" runat="server"
                                                    ControlToValidate="txtDemoFirDate" Display="None" ErrorMessage="FIR Date cannot be less then two days from current date"
                                                    ClientValidationFunction="validateDate">
                                                </asp:CustomValidator>
                                                <asp:Label ID="lblDemoTime" runat="server" Text=""></asp:Label>
                                            </td>
                                            
                                        </tr>
                                        <asp:HiddenField ID="hdnDemoDealerName" runat="server" Value="" />
                                        <asp:HiddenField ID="hdnDemoSourceOfComplaint" runat="server" Value="" />
                                        <asp:HiddenField ID="hdnDemoTypeofComplaint" runat="server" Value="" />
                                        <asp:HiddenField ID="hdnDemosplitcompRefNo" runat="server" Value="" />   
                                       <asp:HiddenField ID="hdnTag" runat="server" Value="" />     
                                    </table>                                    
                                </td>
                            </tr>                             
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%" align="center">
                                    <asp:Button ID="btnAllocate" runat="server" Width="70px" Text="Save" CssClass="btn"
                                        ValidationGroup="init" CausesValidation="true" OnClick="btnAllocate_Click" />
                                    <asp:Button ID="btnInitialiseClose" runat="server" Width="70px" Text="Close" CssClass="btn"
                                        OnClick="btnInitialiseClose_Click" />
                                </td>
                            </tr>
                        </table>
                       
                        <div runat="server" id="tbBasicRegistrationInformation" style="display:none;">
                        <table width="100%" style="padding: 3px">
                            <tr>
                                <td colspan="4" height="20" align="left" valign="bottom" style="border-bottom: 1px solid #396870; padding-bottom:5px;">
                                    <b>Products Details Entry</b>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">Complaint Ref No.:</td>
                                <td><asp:Label ID="lblComplainNo" runat="server" Text=""></asp:Label></td>
                                <td align="right">Complaint Date:</td>
                                <td align="left"><asp:Label ID="lblComplainDate" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td width="25%" align="right">Product Division:</td>
                                <td align="left" width="35%">
                                    <asp:Label ID="lblUnit" runat="server" Text=""></asp:Label>
                                    <asp:DropDownList ID="ddlFirProductDiv" Width="120px" CssClass="simpletxt1" runat="server"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlFirProductDiv_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:LinkButton ID="lnkFirEditProdDiv" runat="server" OnClick="lnkFirEditProdDiv_Click">Edit Product Division</asp:LinkButton>
                                    <asp:HiddenField ID="hdnCustmerID" runat="server" />
                                    <asp:HiddenField ID="hdnProductDvNo" runat="server" />
                                    <asp:HiddenField ID="hdnMfgUnit" runat="server" />
                                    <asp:HiddenField ID="hdnProductDvCode" runat="server" />
                                    <asp:HiddenField ID="hdnComplaintRef" runat="server" />
                                    <asp:HiddenField ID="hdnCallStatus" runat="server" />
                                    <asp:HiddenField ID="hdnNatureOfComplaint" runat="server" />
                                    <asp:HiddenField ID="hdnFirState" runat="server" />
                                    <asp:HiddenField ID="hdnFirCity" runat="server" />
                                    <asp:HiddenField ID="hdnloggedDate" runat="server" />                                   
                                </td>
                                <td width="15%"><asp:HiddenField ID="hdnComplainLogDate" runat="server" /></td>
                                <td width="25%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right">Product Line:<font color="red">*</font></td>
                                <td align="left">
                                    <asp:DropDownList ValidationGroup="AddTempGv" ID="ddlProductLine" CssClass="simpletxt1"
                                        Width="250px" runat="server" OnSelectedIndexChanged="ddlProductLine_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="AddTempGv"
                                        runat="server" ErrorMessage="Select Product Line" InitialValue="0" ControlToValidate="ddlProductLine"
                                        Display="None"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtFindPL" ValidationGroup="ProductRef" CssClass="txtboxtxt" runat="server"
                                        Width="70" CausesValidation="True"></asp:TextBox>
                                    <asp:Button ID="btnGoPL" runat="server" Width="20px" Text="Go" CssClass="btn" OnClick="btnGoPL_Click" />
                                </td>
                                <td valign="top" align="right">Product Serial No:<font color="red">*</font></td>
                                <td align="left">
                                    <asp:TextBox ID="txtProductRefNo" ValidationGroup="AddTempGv" MaxLength="13" CssClass="txtboxtxt"
                                        runat="server" onkeypress="ClearBatchNo();" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtProductRefNo"
                                        ErrorMessage="Enter Product Serial No." ValidationGroup="AddTempGv" Display="None" />
                                    <asp:CustomValidator ValidationGroup="AddTempGv" ID="CustomValidator7" runat="server"
                                        ControlToValidate="txtProductRefNo" Display="None" ErrorMessage="Product Serial No is not Valid"
                                        ClientValidationFunction="validateBatchCode" />
                                    <asp:HiddenField ID="hdnValidBatch" runat="server" />
                                    <asp:HiddenField ID="HdnValid3Chars" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">Product Group:<font color="red">*</font></td>
                                <td align="left">
                                    <asp:DropDownList ValidationGroup="AddTempGv" ID="ddlProductGroup" CssClass="simpletxt1"
                                        Width="250px" runat="server" AppendDataBoundItems="false" OnSelectedIndexChanged="ddlProductGroup_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0"
                                        ControlToValidate="ddlProductGroup" ErrorMessage="Select Product Group" ValidationGroup="AddTempGv"
                                        Display="None"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtFindPG" ValidationGroup="ProductRef" Width="70" CssClass="txtboxtxt"
                                        runat="server"></asp:TextBox>
                                    <asp:Button ID="btnGoPG" runat="server" Width="20px" Text="Go" CssClass="btn" OnClick="btnGoPG_Click" />
                                </td>
                                <td align="right">Batch No:</td>
                                <td align="left">
                                    <asp:TextBox ID="txtBatchNo" ValidationGroup="AddTempGv" CssClass="txtboxtxt" runat="server"
                                        disabled="true"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtBatchNo"
                                        ValidationGroup="AddTempGv" ErrorMessage="First two letters should alphabatic in Product Serial No."
                                        ValidationExpression="[a-zA-Z]{2,}.*" Display="None"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Product:<font color="red">*</font>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ValidationGroup="AddTempGv" ID="ddlProduct" AppendDataBoundItems="false"
                                        CssClass="simpletxt1" Width="410px" runat="server">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlProduct"
                                        ErrorMessage="Select Product" InitialValue="0" ValidationGroup="AddTempGv" Display="None"></asp:RequiredFieldValidator>
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtFindP" ValidationGroup="ProductRef" CssClass="txtboxtxt" runat="server"></asp:TextBox>
                                </td>
                                <td align="left" valign="top">
                                    <asp:Button ID="btnGoP" runat="server" Width="20px" Text="Go" CssClass="btn" OnClick="btnGoP_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Invoice Date:<asp:Label ID="lblStarInvDt" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtInvoiceDate" ValidationGroup="AddTempGv" CssClass="txtboxtxt"
                                        runat="server">
                                    </asp:TextBox>
                                    <asp:CustomValidator ValidationGroup="AddTempGv" ID="CustomValidator6" runat="server"
                                        ControlToValidate="txtInvoiceDate" Display="None" ErrorMessage="Invoice date should be less than current date"
                                        ClientValidationFunction="validateInvoiceDate">
                                    </asp:CustomValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtInvoiceDate" runat="server"
                                        ControlToValidate="txtInvoiceDate" ErrorMessage="Enter Invoice Date" ValidationGroup="AddTempGv"
                                        Display="None"></asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtInvoiceDate" runat="server">
                                    </cc1:CalendarExtender>
                                </td>
                                <td align="right">
                                    Invoice Number:<asp:Label ID="lblStarInvNum" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtInvoiceNo" ValidationGroup="AddTempGv" CssClass="txtboxtxt" runat="server">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtInvoiceNo" runat="server"
                                        ControlToValidate="txtInvoiceNo" ErrorMessage="Enter Invoice Number" ValidationGroup="AddTempGv"
                                        Display="None"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    Additional Information:<font color="red">*</font>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtWarranty" ValidationGroup="AddTempGv" Text="" CssClass="txtboxtxt"
                                        Height="50px" Width="250px" TextMode="MultiLine" runat="server">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtWarranty"
                                        ErrorMessage="Enter Additional Information" ValidationGroup="AddTempGv" Display="None">
                                    </asp:RequiredFieldValidator>
                                    <asp:HiddenField ID="hdnBaseLineID" runat="server" />
                                    <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="AddTempGv" runat="server"
                                        ShowMessageBox="True" ShowSummary="False" />
                                </td>
                                <td align="right">Dealer Name :</td>
                                <td align="left">
                                    <asp:TextBox ID="txtDealername" CssClass="txtboxtxt" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    FIR Date:<font color="red">*</font>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtFirDate" CssClass="txtboxtxt" runat="server">
                                    </asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txtFirDate" runat="server">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtFirDate"
                                        ErrorMessage="Enter FIR Date" ValidationGroup="AddTempGv" Display="None"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator Operator="DataTypeCheck" Type="Date" ControlToValidate="txtFirDate"
                                        Display="None" ErrorMessage="Not a vaild Date" runat="server" ID="cmptxtFromDate"
                                        ValidationGroup="AddTempGv">
                                    </asp:CompareValidator>
                                    <asp:CustomValidator ValidationGroup="AddTempGv" ID="CustomValidator1" runat="server"
                                        ControlToValidate="txtFirDate" Display="None" ErrorMessage="FIR Date cannot be less then two days from current date"
                                        ClientValidationFunction="validateDate">
                                    </asp:CustomValidator>
                                    <asp:Label ID="LblTimeFIR" runat="server"></asp:Label>
                                </td>
                                <td align="right" valign="top">Call Under Warranty:</td>
                                <td align="left" valign="top">
                                    <asp:RadioButtonList ID="rdbWarranty" runat="server" RepeatDirection="Horizontal"
                                        AutoPostBack="True" OnSelectedIndexChanged="rdbWarranty_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Yes</asp:ListItem>
                                        <asp:ListItem Value="1">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right"> Mfg Unit:</td>
                                <td align="left">
                                    <asp:DropDownList ValidationGroup="AddTempGv" ID="ddlMfgUnit" CssClass="simpletxt1"
                                        Width="200px" runat="server" AppendDataBoundItems="false">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtMfgUnit" runat="server" CssClass="txtboxtxt" disabled="true" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlMfgUnit" ValidationGroup="AddTempGv"
                                        runat="server" ErrorMessage="Select Manufacturing Unit" InitialValue="0" ControlToValidate="ddlMfgUnit"
                                        Display="None"></asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td align="right">Complaint Source<font color="red">*</font></td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlSourceOfComp" runat="server" AutoPostBack="True" CssClass="simpletxt1"
                                        OnSelectedIndexChanged="ddlSourceOfComp_SelectedIndexChanged" Width="200px">
                                        <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="CC-Customer" Value="CC-Customer"></asp:ListItem>
                                        <asp:ListItem Text="CC-Dealer" Value="CC-Dealer"></asp:ListItem>
                                        <asp:ListItem Text="CC-ASC" Value="CC-ASC"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSOC" runat="server" ValidationGroup="AddTempGv"
                                        ControlToValidate="ddlSourceOfComp" Display="None" ErrorMessage="Select Source Of Complaint."
                                        InitialValue="0"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:Label ID="lblComplaintType" runat="server" Text="Complaint Type" Visible="false"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlDealer" runat="server" CssClass="simpletxt1" Visible="false">
                                        <asp:ListItem Text="Complaint for customer Piece"></asp:ListItem>
                                        <asp:ListItem Text="Complaint for Dealer Stock Piece"></asp:ListItem>
                                        <asp:ListItem Text="Complaint for another Dealer or Retailer Piece"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlASC" runat="server" CssClass="simpletxt1" Visible="false">
                                        <asp:ListItem Text="Complaint for Customer"></asp:ListItem>
                                        <asp:ListItem Text="Complaint for Dealer"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">
                                    <asp:Label ID="lblSave" runat="server" Text="" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        </div>
                        <table width="99%" style="border: solid 1px #eeeeee;" visible="false" runat="server" id="tbTempGrid">
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnAdd" runat="server" Width="100px" Text="Save/Add More" ValidationGroup="AddTempGv" CssClass="btn" OnClick="btnAdd_Click" />
                                    <asp:Button ID="btnFirClose" runat="server" Width="70px" Text="Close" CssClass="btn" OnClick="btnFirClose_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:GridView ID="gvAddTemp" runat="server" AllowPaging="True" DataKeyNames="Sno"
                                        AllowSorting="True" AlternatingRowStyle-CssClass="fieldName" AutoGenerateColumns="False"
                                        GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor" Width="100%" RowStyle-CssClass="gridbgcolor"
                                        OnRowDataBound="gvAddTemp_RowDataBound">
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate><%#Eval("Sno")%></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Complaint RefNo" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdngvCustomerID" Value='<%#Bind("CustomerID")%>' runat="server" />
                                                    <asp:HiddenField ID="hdngvComplaint_RefNo" Value='<%#Bind("Complaint_RefNo")%>' runat="server" />
                                                    <asp:HiddenField ID="hdngvSplitComplaint_RefNo" Value='<%#Bind("SplitComplaint_RefNo")%>' runat="server" />
                                                    <asp:HiddenField ID="hdngvComplaintDate" Value='<%#Bind("ComplaintDate")%>' runat="server" />
                                                    <asp:HiddenField ID="hdngvNatureOfComplaint" Value='<%#Bind("NatureOfComplaint")%>' runat="server" />
                                                    <asp:HiddenField ID="hdngvCallStatus" Value='<%#Bind("CallStatus")%>' runat="server" />
                                                    <asp:HiddenField ID="hdngvManufacture_SNo" Value='<%#Bind("Manufacture_SNo")%>' runat="server" />
                                                    <asp:HiddenField ID="hdnMfgUnit" Value='<%#Bind("ManufactureUnit")%>' runat="server" />
                                                    <asp:HiddenField ID="hdngvDefectAccFlag" Value='<%#Bind("DefectAccFlag")%>' runat="server" />
                                                    <asp:HiddenField ID="hdngvProduct_SerialNo" Value='<%#Bind("Product_SerialNo")%>' runat="server" />
                                                    <asp:HiddenField ID="hdngvSLADate" runat="server" Value='<%#Eval("SLADate")%>' />
                                                    <%#Eval("Complaint_RefNo")%>/<%#Eval("SplitComplaint_RefNo")%><asp:HiddenField ID="hdngvDealerName" Value='<%#Bind("DealerName")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product Division" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvProductDivision" runat="server" Text='<%#Eval("ProductDivision_Desc")%>'></asp:Label>
                                                    <asp:HiddenField ID="hdngvProductDivisionSno" Value='<%#Bind("ProductDivision_Sno")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product Line" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvProductLine" runat="server" Text='<%#Eval("ProductLine_Desc")%>'></asp:Label>
                                                    <asp:HiddenField ID="hdngvProductLineSno" Value='<%#Bind("ProductLine_Sno")%>' runat="server" />
                                                    <asp:HiddenField ID="hdngvProductGroupSno" Value='<%#Bind("ProductGroup_SNo")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvProduct" runat="server" Text='<%#Eval("Product_Desc")%>'></asp:Label>
                                                    <asp:HiddenField ID="hdngvProductSno" Value='<%#Bind("Product_SNo")%>' runat="server" />
                                                    <asp:HiddenField ID="hdngvInvoiceNo" Value='<%#Bind("InvoiceNo")%>' runat="server" />
                                                    <asp:HiddenField ID="hdngvInvoiceDate" Value='<%#Bind("InvoiceDate")%>' runat="server" />
                                                    <asp:HiddenField ID="hdngvWarrantyDate" Value='<%#Bind("WarrantyDate")%>' runat="server" />
                                                    <asp:HiddenField ID="hdngvActionTime" Value='<%#Bind("ActionTime")%>' runat="server" />
                                                    <asp:HiddenField ID="hdngvVisitCharges" Value='<%#Bind("VisitCharges")%>' runat="server" />
                                                    <asp:HiddenField ID="hdnSourceOfComplaint" Value='<%#Bind("SourceOfComplaint")%>'
                                                        runat="server" />
                                                    <asp:HiddenField ID="hdnTypeOfComplaint" Value='<%#Bind("TypeOfComplaint")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Batch" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvBatch" runat="server" Text='<%#Eval("Batch_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Additional Info" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvAdditionalInfo" runat="server" Text='<%#Eval("LastComment")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Warranty Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvWarrantyStatus" runat="server" Text='<%#Eval("WarrantyStatus")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkTempGvDefect" Visible="false" OnClick="llnkTempGvDefect_Click"
                                                        runat="server" Text="Add Defect" CausesValidation="false" CommandArgument='<%#Eval("Complaint_RefNo")%>'
                                                        CommandName='<%#Eval("SplitComplaint_RefNo")%>'>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="lnkTempGvViewDefect" Visible="false" OnClick="lnkTempGvViewDefect_Click"
                                                        runat="server" Text="View Defect" CausesValidation="false" CommandArgument='<%#Eval("Complaint_RefNo")%>'
                                                        CommandName='<%#Eval("SplitComplaint_RefNo")%>'>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="lnkTempGvAction" Visible="false" OnClick="llnkTempGvAction_Click"
                                                        runat="server" CausesValidation="false" Text=" / Action" CommandArgument='<%#Eval("Complaint_RefNo")%>'
                                                        CommandName='<%#Eval("SplitComplaint_RefNo")%>'>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnSimsTemp" CommandArgument='<%#Eval("Complaint_RefNo")%>' CommandName='<%#Eval("SplitComplaint_RefNo")%>'
                                                        runat="server" Text="/ Enter Spare & Activity" Visible="false" OnClick="btnSimsTemp_Click">
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="lnkTempGv" OnClick="llnkTempGv_Click" runat="server" Text="Remove"
                                                        CausesValidation="false" CommandArgument='<%#Eval("Sno")%>'>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                        <AlternatingRowStyle CssClass="fieldName" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnSave" runat="server" Width="70px" Visible="false" Text="Save FIR"
                                        CausesValidation="false" ValidationGroup="AddTempGv" CssClass="btn" OnClick="btnSave_Click" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <asp:GridView ID="gvCustDetail" runat="server" AllowPaging="true" AllowSorting="True"
                                        AlternatingRowStyle-CssClass="fieldName" AutoGenerateColumns="False" DataKeyNames="BaseLineId"
                                        GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor" OnPageIndexChanging="gvCustDetail_PageIndexChanging"
                                        OnRowDataBound="gvCustDetail_RowDataBound" RowStyle-CssClass="gridbgcolor" Width="100%">
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <asp:BoundField DataField="SNo" HeaderText="Sno." />
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Complaint RefNo"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdngvCustomerID" runat="server" Value='<%#Bind("CustomerID")%>' />
                                                    <asp:HiddenField ID="hdngvComplaint_RefNo" runat="server" Value='<%#Bind("Complaint_RefNo")%>' />
                                                    <asp:HiddenField ID="hdngvCallStatus" runat="server" Value='<%#Bind("CallStatus")%>' />
                                                    <asp:HiddenField ID="hdngvSplitComplaint_RefNo" runat="server" Value='<%#Bind("SplitComplaint_RefNo")%>' />
                                                    <asp:HiddenField ID="hdngvComplaintDate" runat="server" Value='<%#Bind("ComplaintDate")%>' />
                                                    <asp:HiddenField ID="hdngvLastComment" runat="server" Value='<%#Bind("LastComment")%>' />
                                                    <asp:HiddenField ID="hdngvDefectAccFlag" runat="server" Value='<%#Bind("DefectAccFlag")%>' />
                                                    <asp:HiddenField ID="hdngvManufacture_SNo" runat="server" Value='<%#Bind("Manufacture_SNo")%>' />
                                                    <asp:HiddenField ID="hdngvManufactureUnit" runat="server" Value='<%#Bind("ManufactureUnit")%>' />
                                                    <asp:HiddenField ID="hdngvWarrantyStatus" runat="server" Value='<%#Bind("WarrantyStatus")%>' />
                                                    <asp:HiddenField ID="hdngvSLADate" runat="server" Value='<%#Eval("SLADate")%>' />
                                                    <asp:HiddenField ID="hdngvProduct_SerialNo" runat="server" Value='<%#Bind("ProductSerial_No")%>' />
                                                    <asp:HiddenField ID="hdngvSc_sno" runat="server" Value='<%#Bind("SC_SNo")%>' />
                                                    <asp:HiddenField ID="hdnBaseLineId" runat="server" Value='<%#Bind("BaseLineId")%>' />
                                                    <a href="Javascript:void(0);" onclick='funCommonPopUp(<%#Eval("BaseLineId")%>)'>
                                                        <%#Eval("Complaint_RefNo")%>/<%#Eval("split")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Customer Name" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%#Eval("CustName")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Product Division" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvProductDivision" runat="server" Text='<%#Eval("ProductDivision_Desc")%>'></asp:Label>
                                                    <asp:HiddenField ID="hdngvProductDivisionSno" runat="server" Value='<%#Bind("ProductDivision_Sno")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Product Line" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvProductLine" runat="server" Text='<%#Eval("ProductLine_Desc")%>'></asp:Label>
                                                    <asp:HiddenField ID="hdngvProductLineSno" runat="server" Value='<%#Bind("ProductLine_Sno")%>' />
                                                    <asp:HiddenField ID="hdngvProductGroupSno" runat="server" Value='<%#Bind("ProductGroup_SNo")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Product" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvProduct" runat="server" Text='<%#Eval("Product_Desc")%>'></asp:Label>
                                                    <asp:HiddenField ID="hdngvProductSno" runat="server" Value='<%#Bind("Product_SNo")%>' />
                                                    <asp:HiddenField ID="hdngvInvoiceNo" runat="server" Value='<%#Bind("InvoiceNo")%>' />
                                                    <asp:HiddenField ID="hdngvInvoiceDate" runat="server" Value='<%#Bind("InvoiceDate")%>' />
                                                    <asp:HiddenField ID="hdngvVisitCharges" runat="server" Value='<%#Bind("VisitCharges")%>' />
                                                    <asp:HiddenField ID="hdnoldcomplaint_refno" runat="server" Value='<%#Bind("oldcomplaint_refno")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Batch" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvBatch" runat="server" Text='<%#Eval("Batch_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Warranty Status"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvWarrantyStatus" runat="server" Text='<%#Eval("WarrantyStatus")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkCustGvDefect" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Complaint_RefNo")%>'
                                                        CommandName='<%#Eval("SplitComplaint_RefNo")%>' OnClick="lnkCustGvDefect_Click" Text="Add Defect ">
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="lnkCustGvViewDefect" runat="server" CausesValidation="false"
                                                        CommandArgument='<%#Eval("Complaint_RefNo")%>' CommandName='<%#Eval("SplitComplaint_RefNo")%>'
                                                        OnClick="lnkCustGvViewDefect_Click" Text="View Defect " Visible="false">
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="lnkCustGvAction" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Complaint_RefNo")%>'
                                                        CommandName='<%#Eval("SplitComplaint_RefNo")%>' OnClick="lnkCustGvAction_Click" Text="/ Action">
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnSims" runat="server" CommandArgument='<%#Eval("Complaint_RefNo")%>'
                                                        CommandName='<%#Eval("SplitComplaint_RefNo")%>' OnClick="btnSims_Click" Text="/ Enter Spare &amp; Activity" Visible="false">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                        <AlternatingRowStyle CssClass="fieldName" />
                                        <EmptyDataTemplate>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                        <b>Selected Complain is Unallocated</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                    <asp:GridView ID="gvViewDefects" runat="server" AllowPaging="True" DataKeyNames="Sno"
                                        AllowSorting="True" AlternatingRowStyle-CssClass="fieldName" AutoGenerateColumns="False"
                                        GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor" Width="100%" RowStyle-CssClass="gridbgcolor">
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate> <%#Eval("Sno")%></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Defect Category" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdngvDefectCategory_Sno" Value='<%#Bind("DefectCategory_Sno")%>'
                                                        runat="server" />
                                                    <asp:Label ID="lblgvDefectCategory" runat="server" Text='<%#Eval("DefectCategory")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Defect" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDefect_Desc" runat="server" Text='<%#Eval("Defect_Desc")%>'></asp:Label>
                                                    <asp:HiddenField ID="hdngvDefect_Sno" Value='<%#Bind("Defect_Sno")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bearing/Capacitor" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMAKE_CAP" runat="server" Text='<%# Convert.ToString(Eval("MAKE_CAP"))!=""? Eval("MAKE_CAP"): Eval("MAKE_AGREED")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Blade Vendor" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvBlade_Vendor" runat="server" Text='<%#Eval("Blade_Vendor")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Winding Vendor" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvWinding_Unit" runat="server" Text='<%#Eval("WindingUnit")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtQty" runat="server" Text='<%#Bind("NUM_OF_DEF") %>' CssClass="txtboxtxt" Width="40"></asp:Label>
                                                    <asp:HiddenField ID="hdnGvDefectSRNO" runat="server" Value='<%#Eval("SRNO") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                        <AlternatingRowStyle CssClass="fieldName" />
                                    </asp:GridView>
                                    <table id="tbViewAttribute" runat="server" width="100%" visible="false">
                                        <tr>
                                            <td>
                                                <tr>
                                                    <td colspan="4" height="20" align="left" valign="bottom" style="border-bottom: 1px solid #396870">
                                                        <b>Attribute Details</b>
                                                    </td>
                                                </tr>
                                                <tr id="trAvrV" runat="server" visible="false">
                                                    <td align="right" valign="top">
                                                        Avr SrNo.:<font color="red">*</font>
                                                    </td>
                                                    <td align="left" colspan="3">
                                                        <asp:TextBox ID="txtAvrV" runat="server" Enabled="false" Width="250px" CssClass="txtboxtxt"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr id="trApplicationV" runat="server" visible="false">
                                                    <td align="right" valign="top">
                                                        Application:<font color="red">*</font>
                                                    </td>
                                                    <td align="left" colspan="3">
                                                        <asp:TextBox ID="txtApplicationV" runat="server" Enabled="false" Width="250px" CssClass="txtboxtxt"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr id="trEXCISESERALNOV" runat="server" visible="false">
                                                    <td align="right" valign="top">
                                                        Excies Serial No.:<font color="red">*</font>
                                                    </td>
                                                    <td align="left" colspan="3">
                                                        <asp:TextBox ID="txtEXCISESERALNOV" runat="server" Enabled="false" Width="250px"
                                                            CssClass="txtboxtxt"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr id="trSerialNoV" runat="server" visible="false">
                                                    <td align="right" valign="top">
                                                        Machine No.:<font color="red">*</font>
                                                    </td>
                                                    <td align="left" colspan="3">
                                                        <asp:TextBox ID="txtSerialNoV" runat="server" Enabled="false" Width="250px" CssClass="txtboxtxt"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr id="trLOADV" runat="server" visible="false">
                                                    <td align="right" valign="top">
                                                        Load:<font color="red">*</font>
                                                    </td>
                                                    <td align="left" colspan="3">
                                                        <asp:TextBox ID="txtLOADV" runat="server" Enabled="false" Width="250px" CssClass="txtboxtxt"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr id="trFrameV" runat="server" visible="false">
                                                    <td align="right" valign="top">
                                                        Frame:<font color="red">*</font>
                                                    </td>
                                                    <td align="left" colspan="3">
                                                        <asp:TextBox ID="txtFrameV" runat="server" Enabled="false" Width="250px" CssClass="txtboxtxt"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr id="trHPV" runat="server" visible="false">
                                                    <td align="right" valign="top">
                                                        HP/Pole:<font color="red">*</font>
                                                    </td>
                                                    <td align="left" colspan="3">
                                                        <asp:TextBox ID="txtHPV" runat="server" Enabled="false" Width="250px" CssClass="txtboxtxt"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr id="trIDV" runat="server" visible="false">
                                                    <td align="right" valign="top">
                                                        Instrument Details (Current,Voltage,Capacity Rating & Output Range):<font color="red">*</font>
                                                    </td>
                                                    <td align="left" colspan="3">
                                                        <asp:TextBox ID="txtInstrumentDetailsV" runat="server" MaxLength="200" Width="250px"
                                                            CssClass="txtboxtxt"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorInstrumentDetailsV" runat="server"
                                                            ControlToValidate="txtInstrumentDetailsV" Display="None" ErrorMessage="Enter Instrument Details "
                                                            ValidationGroup="SaveDefect">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr id="trIMNV" runat="server" visible="false">
                                                    <td align="right" valign="top">
                                                        Instrument Manufacturer Name:<font color="red">*</font>
                                                    </td>
                                                    <td align="left" colspan="3">
                                                        <asp:TextBox ID="txtInstrumentManufacturerNameV" MaxLength="200" runat="server" Width="250px"
                                                            CssClass="txtboxtxt"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorIMNV" runat="server" ControlToValidate="txtInstrumentManufacturerNameV"
                                                            Display="None" ErrorMessage="Enter Instrument Details " ValidationGroup="SaveDefect">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr id="trAINV" runat="server" visible="false">
                                                    <td align="right" valign="top">
                                                        Application Instrument Name:<font color="red">*</font>
                                                    </td>
                                                    <td align="left" colspan="3">
                                                        <asp:TextBox ID="txtApplicationInstrumentNameV" runat="server" MaxLength="200" Width="250px"
                                                            CssClass="txtboxtxt"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorAINV" runat="server" ControlToValidate="txtApplicationInstrumentNameV"
                                                            Display="None" ErrorMessage="Enter Instrument Details " ValidationGroup="SaveDefect">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr id="trRatingV" runat="server" visible="false">
                                                    <td align="right" valign="top">
                                                        Rating:<font color="red">*</font>
                                                    </td>
                                                    <td align="left" colspan="3">
                                                        <asp:TextBox ID="txtRatingV" runat="server" Enabled="false" Width="250px" CssClass="txtboxtxt"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table width="100%" visible="false" runat="server" id="tbDefect" style="padding: 3px">
                            <tr>
                                <td colspan="4" height="20" align="left" valign="bottom" style="border-bottom: 1px solid #396870">
                                    <b>Defect Details</b>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="bottom" style="border-bottom: 1px solid #396870" colspan="4"
                                    height="20">
                                    <asp:UpdateProgress AssociatedUpdatePanelID="updateSC" ID="UpdateProgress3" runat="server">
                                        <ProgressTemplate>
                                            <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    Complaint Refrence No.:
                                </td>
                                <td align="left" colspan="3">
                                    <asp:Label ID="lblDefComp" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    Product Division:
                                </td>
                                <td align="left" colspan="3">
                                    <asp:Label ID="lblDefProductDiv" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    Product Line:
                                </td>
                                <td align="left" colspan="3">
                                    <asp:Label ID="lblDefectProductLine" runat="server" Text=""></asp:Label>
                                    <asp:HiddenField ID="hdnProductLineCode" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    MFG Unit/Vendor Location
                                </td>
                                <td align="left" colspan="3">
                                    <asp:Label ID="LblMfgUnit" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    Defect Category:<font color="red">*</font>
                                </td>
                                <td align="left" colspan="3" style="width: 500px;">
                                    <asp:DropDownList ID="ddlDefectCat" CssClass="simpletxt1" Width="335px" runat="server"
                                        AppendDataBoundItems="True" AutoPostBack="True" ValidationGroup="SaveDefect"
                                        OnSelectedIndexChanged="ddlDefectCat_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RegularExpressionValidatorddlDefectCat" Display="None"
                                        InitialValue="0" ValidationGroup="SaveDefect" ControlToValidate="ddlDefectCat"
                                        runat="server" ErrorMessage="Select Defect Category">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    Select Defect:<font color="red">*</font>
                                </td>
                                <td align="left" colspan="3" style="width: 500px;">
                                    <asp:DropDownList ID="ddlDefect" CssClass="simpletxt1" Width="335px" ValidationGroup="SaveDefect"
                                        runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlDefect_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlDefect" Display="None" ValidationGroup="SaveDefect"
                                        ControlToValidate="ddlDefect" InitialValue="0" runat="server" ErrorMessage="Select Defect">
                                    </asp:RequiredFieldValidator>
                                    <asp:ValidationSummary ID="validAddDefect" runat="server" ValidationGroup="AddDefect"
                                        ShowSummary="false" ShowMessageBox="true" />
                                </td>
                            </tr>
                            <tr id="trDefectMake" runat="server" visible="false">
                                <td align="right" valign="top">
                                    <asp:Label ID="LblDefectAttribue" runat="server" Text="Make Capacitor:" />
                                    <font color="red">*</font>
                                </td>
                                <td align="left" colspan="3">
                                    <asp:DropDownList ID="DdlDefectAttribute" runat="server" CssClass="simpletxt1" onchange="WindingVendorFlag();">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RfvDdlDefectAttribute" Display="None" ControlToValidate="DdlDefectAttribute"
                                        InitialValue="0" runat="server" Enabled="false">
                                    </asp:RequiredFieldValidator>
                                    <asp:HiddenField ID="hdnDefectCustomerID" runat="server" />
                                    <asp:HiddenField ID="hdnDefectSliptComplaint" runat="server" />
                                    <asp:HiddenField ID="hdnDefectProductSrNo" runat="server" />
                                    <asp:HiddenField ID="hdnDefectComplaintRef_No" runat="server" />
                                    <asp:HiddenField ID="hdnDefectBatch" runat="server" />
                                    <asp:HiddenField ID="hdnDefectComplaintLoggDate" runat="server" />
                                    <asp:HiddenField ID="hdnDefectProductDiv_Sno" runat="server" />
                                    <asp:HiddenField ID="hdnDefectProductGroup_Sno" runat="server" />
                                    <asp:HiddenField ID="hdnDefectProductLine_Sno" runat="server" />
                                    <asp:HiddenField ID="hdnDefectCallStatus" runat="server" />
                                    <asp:HiddenField ID="hdnDefectProduct_Sno" runat="server" />
                                    <asp:HiddenField ID="hdnDefectProductCode" runat="server" />
                                    <asp:HiddenField ID="hdnDefectMFGUNIT" runat="server" />
                                </td>
                            </tr>
                            <tr id="trNaWindingUnti" style="display:none;">
                            <td align="right" valign="top">Other Winding Vender Details: </td>
                            <td colspan="3" align="left">
                             <asp:TextBox ID="txtWindingUnit" CssClass="txtboxtxt" runat="server" Text=""></asp:TextBox>
                            </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    &nbsp;
                                </td>
                                <td colspan="3" align="left">
                                    <asp:Button ID="btnAddTempDefect" runat="server" CssClass="btn" 
                                        OnClick="btnAddTempDefect_Click" 
                                        OnClientClick="javascript:return CheckDefect();" Text="Add" Width="50px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    Defect No.:<font color="red">*</font>
                                </td>
                                <td align="left" colspan="3">
                                    <asp:TextBox ID="txtDefectQty" Text="0" MaxLength="3" ReadOnly="true" runat="server"
                                        CssClass="txtboxtxt"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ReqtxtDefectQty" runat="server" ValidationGroup="SaveDefect"
                                        ErrorMessage="Enter Defect Quantity" ControlToValidate="txtDefectQty" Display="None"></asp:RequiredFieldValidator>
                                    <asp:CustomValidator ID="CustomValidator5" runat="server" ControlToValidate="txtDefectQty"
                                        Display="None" ErrorMessage="Enter Number Only in Quantity" ClientValidationFunction="validateDefectQty">
                                    </asp:CustomValidator>
                                    <div id="Divpumpdef" runat="server" visible="false">
                                        <a href="javascript:void(0);" class="links" onclick="fnOpendefectPump();"><b>Click here
                                            to Enter Winding Defect</b></a>
                                    </div>
                                    <div id="dvFHPMotorDef" runat="server" visible="false">
                                        <a href="javascript:void(0);" class="links" onclick="fnOpendefectFHPMotor();"><b>Click
                                            here to Enter Winding Defect</b></a>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center">
                                    <asp:GridView ID="gvDefectTemp" runat="server" AllowPaging="True" DataKeyNames="Sno"
                                        AllowSorting="True" AlternatingRowStyle-CssClass="fieldName" AutoGenerateColumns="False"
                                        GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor" Width="100%" RowStyle-CssClass="gridbgcolor">
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%#Eval("Sno")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Defect Category" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdngvDefectCategory_Sno" Value='<%#Bind("DefectCategory_Sno")%>'
                                                        runat="server" />
                                                    <asp:Label ID="lblgvDefectCategory" runat="server" Text='<%#Eval("DefectCategory")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Defect" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDefect_Desc" runat="server" Text='<%#Eval("Defect_Desc")%>'></asp:Label>
                                                    <asp:HiddenField ID="hdngvDefect_Sno" Value='<%#Bind("Defect_Sno")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bearing/Capacitor" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMAKE_CAP" runat="server" Text='<%# Convert.ToString(Eval("MAKE_CAP"))!=""? Eval("MAKE_CAP"): Eval("MAKE_AGREED")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Blade Vendor" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvBlade_Vendor" runat="server" Text='<%#Eval("BLADE_VENDOR")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Winding Vendor" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvWinding_Unit" runat="server" Text='<%#Eval("WindingUnit")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtQty" runat="server" Text='<%#Bind("NUM_OF_DEF") %>' CssClass="txtboxtxt"
                                                        Width="40"></asp:Label>
                                                    <asp:HiddenField ID="hdnGvDefectSRNO" runat="server" Value='<%#Eval("SRNO") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDefectGv" OnClick="lnkDefectGv_Click" CommandName='<%#Eval("SRNO")%>'
                                                        runat="server" Text="Remove" CommandArgument='<%#Eval("Sno")%>'>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                        <AlternatingRowStyle CssClass="fieldName" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" height="20" align="left" valign="bottom" style="border-bottom: 1px solid #396870">
                                    <b>Attribute Details</b>
                                </td>
                            </tr>
                            <tr id="trAvr" runat="server" visible="false">
                                <td align="right" valign="top">
                                    AVR Serial No.:<font color="red">*</font>
                                </td>
                                <td align="left" colspan="3">
                                    <asp:TextBox ID="txtAvr" runat="server" Width="250px" CssClass="txtboxtxt"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtAvr" runat="server" ControlToValidate="txtAvr"
                                        Display="None" ErrorMessage="Enter Avr" ValidationGroup="SaveDefect">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr id="trMachine" runat="server" visible="false">
                                <td align="right" valign="top">
                                    Machine:<font color="red">*</font>
                                </td>
                                <td align="left" colspan="3">
                                    <asp:TextBox ID="txtMachine" runat="server" Width="250px" CssClass="txtboxtxt"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtMachine" runat="server"
                                        ControlToValidate="txtMachine" Display="None" ErrorMessage="Enter Machine" ValidationGroup="SaveDefect">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr id="trApplication" runat="server" visible="false">
                                <td align="right" valign="top">
                                    Application:<font color="red">*</font>
                                </td>
                                <td align="left" colspan="3">
                                    <asp:TextBox ID="txtApplication" runat="server" Width="250px" CssClass="txtboxtxt"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtApplication" runat="server"
                                        ControlToValidate="txtApplication" Display="None" ErrorMessage="Enter Application"
                                        ValidationGroup="SaveDefect">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr id="trEXCISESERALNO" runat="server" visible="false">
                                <td align="right" valign="top">
                                    Excies Serial No.:<font color="red">*</font>
                                </td>
                                <td align="left" colspan="3">
                                    <asp:TextBox ID="txtEXCISESERALNO" runat="server" Width="250px" CssClass="txtboxtxt"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtEXCISESERALNO" runat="server"
                                        ControlToValidate="txtEXCISESERALNO" Display="None" ErrorMessage="Enter Excise Serial No."
                                        ValidationGroup="SaveDefect">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr id="trSerialNo" runat="server" visible="false">
                                <td align="right" valign="top">
                                    Machine Number:<font color="red">*</font>
                                </td>
                                <td align="left" colspan="3">
                                    <asp:TextBox ID="txtSerialNo" runat="server" Width="250px" CssClass="txtboxtxt"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtSerialNo" runat="server"
                                        ControlToValidate="txtSerialNo" Display="None" ErrorMessage="Enter Serial No."
                                        ValidationGroup="SaveDefect">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr id="trLOAD" runat="server" visible="false">
                                <td align="right" valign="top">
                                    Load:<font color="red">*</font>
                                </td>
                                <td align="left" colspan="3">
                                    <asp:TextBox ID="txtLOAD" runat="server" Width="250px" CssClass="txtboxtxt"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtLOAD" runat="server" ControlToValidate="txtLOAD"
                                        Display="None" ErrorMessage="Enter Load" ValidationGroup="SaveDefect">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr id="trFrame" runat="server" visible="false">
                                <td align="right" valign="top">
                                    Frame:<font color="red">*</font>
                                </td>
                                <td align="left" colspan="3">
                                    <asp:TextBox ID="txtFrame" runat="server" Width="250px" CssClass="txtboxtxt"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtFrame" runat="server" ControlToValidate="txtFrame"
                                        Display="None" ErrorMessage="Enter Frame" ValidationGroup="SaveDefect">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr id="trHP" runat="server" visible="false">
                                <td align="right" valign="top">
                                    HP:<font color="red">*</font>
                                </td>
                                <td align="left" colspan="3">
                                    <asp:TextBox ID="txtHP" runat="server" Width="250px" CssClass="txtboxtxt"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtHP" runat="server" ControlToValidate="txtHP"
                                        Display="None" ErrorMessage="Enter HP" ValidationGroup="SaveDefect">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr id="trRating" runat="server" visible="false">
                                <td align="right" valign="top">
                                    Rating:<font color="red">*</font>
                                </td>
                                <td align="left" colspan="3">
                                    <asp:TextBox ID="txtRating" runat="server" Width="250px" CssClass="txtboxtxt"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtRating" runat="server" ControlToValidate="txtRating"
                                        Display="None" ErrorMessage="Enter Rating" ValidationGroup="SaveDefect">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr id="trID" runat="server" visible="false">
                                <td align="right" valign="top">
                                    Instrument Details (Current,Voltage,Capacity Rating & Output Range):<font color="red">*</font>
                                </td>
                                <td align="left" colspan="3">
                                    <asp:TextBox ID="txtInstrumentDetails" runat="server" MaxLength="200" Width="250px"
                                        CssClass="txtboxtxt"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorInstrumentDetails" runat="server"
                                        ControlToValidate="txtInstrumentDetails" Display="None" ErrorMessage="Enter Instrument Details "
                                        ValidationGroup="SaveDefect">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr id="trIMN" runat="server" visible="false">
                                <td align="right" valign="top">
                                    Instrument Manufacturer Name:<font color="red">*</font>
                                </td>
                                <td align="left" colspan="3">
                                    <asp:TextBox ID="txtInstrumentManufacturerName" MaxLength="200" runat="server" Width="250px"
                                        CssClass="txtboxtxt"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorIMN" runat="server" ControlToValidate="txtInstrumentManufacturerName"
                                        Display="None" ErrorMessage="Enter Instrument Details " ValidationGroup="SaveDefect">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr id="trAIN" runat="server" visible="false">
                                <td align="right" valign="top">
                                    Application Instrument Name:<font color="red">*</font>
                                </td>
                                <td align="left" colspan="3">
                                    <asp:TextBox ID="txtApplicationInstrumentName" runat="server" MaxLength="200" Width="250px"
                                        CssClass="txtboxtxt"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorAIN" runat="server" ControlToValidate="txtApplicationInstrumentName"
                                        Display="None" ErrorMessage="Enter Instrument Details " ValidationGroup="SaveDefect">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    Action Remarks:<font color="red">*</font>
                                </td>
                                <td align="left" colspan="3">
                                    <asp:TextBox ID="txtDefectServiceActionRemarks" Text="" ValidationGroup="SaveDefect"
                                        runat="server" Width="250px" Height="40" CssClass="txtboxtxt"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtDefectServiceActionRemarks"
                                        Display="None" ErrorMessage="Enter Defect Action remarks" ValidationGroup="SaveDefect">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    Defect Entry Date:<font color="red">*</font>
                                </td>
                                <td align="left" colspan="3">
                                    <asp:TextBox ID="txtDefectDate" ValidationGroup="SaveDefect" runat="server" CssClass="txtboxtxt"
                                        Height="16px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtDefectDate"
                                        Display="None" ErrorMessage="Enter Defect Entry Date" ValidationGroup="SaveDefect">
                                    </asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender5" TargetControlID="txtDefectDate" runat="server">
                                    </cc1:CalendarExtender>
                                    <asp:CompareValidator Operator="DataTypeCheck" Type="Date" ControlToValidate="txtDefectDate"
                                        Display="None" ErrorMessage="Not a vaild Date" runat="server" ID="CompareValidator2"
                                        ValidationGroup="SaveDefect">
                                    </asp:CompareValidator>
                                    <asp:Label ID="LblTimeDefect" runat="server"></asp:Label>
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="SaveDefect"
                                        ShowMessageBox="True" ShowSummary="False" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">
                                    <asp:Label ID="lblDefectMsg" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">
                                    <asp:Button ID="btnSaveDefect" runat="server" Width="70px" Text="Save" CssClass="btn"
                                        OnClick="btnSaveDefect_Click" ValidationGroup="SaveDefect" />
                                    <asp:Button ID="btnDefectApprove" runat="server" Width="70px" Text="Approve" CssClass="btn"
                                        ValidationGroup="SaveDefect" OnClick="btnDefectApprove_Click" />
                                    <asp:Button ID="btnDefectClose" runat="server" Width="70px" Text="Close" CssClass="btn"
                                        OnClick="btnDefectClose_Click" />
                                </td>
                            </tr>
                        </table>
                        
                        <table width="99%" visible="false" runat="server" id="tbAction" style="padding: 3px">
                            <tr>
                                <td colspan="2" height="20" align="left" valign="bottom" style="border-bottom: 1px solid #396870">
                                    <b>Service Action Entry</b>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="bottom" style="border-bottom: 1px solid #396870" colspan="2"
                                    height="20">
                                    <asp:UpdateProgress AssociatedUpdatePanelID="updateSC" ID="UpdateProgress4" runat="server">
                                        <ProgressTemplate>
                                            <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%" align="right">
                                    Product Division:
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblActionProductDiv" runat="server" Text="">
                                    </asp:Label><asp:HiddenField ID="hdnActionSplitNo" runat="server" />
                                    <asp:HiddenField ID="hdnActionCallStatusID" runat="server" />
                                    <asp:HiddenField ID="hdnActionWarrantyStatus" runat="server" />
                                    <asp:HiddenField ID="hdnActionSLADate" runat="server" />
                                    <asp:HiddenField ID="hdnProductDivisionSno" runat="server" Value="0" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%" align="right">
                                    Complaint Ref No:
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblActionComplaintRefNo" runat="server" Text="">
                                    </asp:Label>
                                    <asp:HiddenField ID="hdnActionComplaintRefNo" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%" align="right">
                                    Product:
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblActionProduct" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%" align="right">Batch No:</td>
                                <td align="left">
                                    <asp:Label ID="lblActionBatch" runat="server" Text="">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%" align="right">Warranty Status: </td>
                                <td align="left">
                                    <asp:Label ID="lblActionWarranty" runat="server" Text="">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" style="width: 30%">
                                    Action:<font color="red">*</font>
                                </td>
                                <td align="left">
                                <asp:HiddenField ID="hdnPopupCat" runat="server" Value="" />
                                    <asp:DropDownList ID="ddlActionStatus" runat="server" CssClass="simpletxt1" Width="310px"
                                        OnSelectedIndexChanged="ddlActionStatus_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlActionStatus"
                                        InitialValue="0" Display="None" ErrorMessage="Select Action" ValidationGroup="SaveAction">
                                    </asp:RequiredFieldValidator>
                                    <asp:LinkButton ID="LbtnSpareReq" runat="server" OnClick="LbtnSpareReq_Click" ForeColor="#1c3d74"
                                        Visible="False">Spare Requirement</asp:LinkButton>
                                </td>
                            </tr>
                            <tr id="trSticker" runat="server" visible="false">
                                <td align="right" valign="top" style="width: 30%">
                                    Sticker Code :<font color="red">*</font>
                                </td>
                                <td align="left">
                                <asp:DropDownList ID="ddlStickerCode" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rqfStickercodevalidatior" runat="server" ControlToValidate="ddlStickerCode" 
                                 InitialValue="0" Display="None" ErrorMessage="Select Sticker" ValidationGroup="SaveAction">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" valign="top">
                                    <asp:GridView ID="Gvspares" runat="server" AlternatingRowStyle-CssClass="fieldName"
                                        Visible="false" AutoGenerateColumns="false" GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor"
                                        Width="50%" RowStyle-CssClass="gridbgcolor">
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Spare" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSpare" runat="server" Text='<%#Eval("SAP_Desc")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Proposed Quantity" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProposedQty" runat="server" Text='<%#Eval("Proposed_Qty")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                        <AlternatingRowStyle CssClass="fieldName" />
                                    </asp:GridView>
                                    <uc1:PendingForSpareCtrl ID="PendingForSpare1" Visible="false" runat="server" />
                                </td>
                            </tr>
                            <tr id="trServiceDate" runat="server" visible="false">
                                <td align="right">
                                    Service Invoice Date:
                                    <asp:Label ID="lblStarServiceDate" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtServiceDate" Width="175" CssClass="txtboxtxt" runat="server">
                                    </asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender7" TargetControlID="txtServiceDate" runat="server">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="rqftxtServiceDate" ControlToValidate="txtServiceDate"
                                        runat="server" ErrorMessage="Enter Service Invoice Date" ValidationGroup="SaveAction" Display="None">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr id="trServiceNumber" runat="server" visible="false">
                                <td align="right">Service Invoice Number:
                                    <asp:Label ID="lblStarServiceNumber" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtServiceNumber" Width="175" MaxLength="15" CssClass="txtboxtxt"
                                        runat="server">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rqftxtServiceNumber" ControlToValidate="txtServiceNumber"
                                        runat="server" ErrorMessage="Enter Service Invoice Number" ValidationGroup="SaveAction"
                                        Display="None">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr id="trServiceAmount" runat="server" visible="false">
                                <td align="right">Service Amount:
                                    <asp:Label ID="lblStarServiceAmount" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtServiceAmount" Width="175" MaxLength="10" CssClass="txtboxtxt"
                                        runat="server">
                                    </asp:TextBox>
                                    <asp:RangeValidator ID="regValidateAmount" runat="server" ControlToValidate="txtServiceAmount"
                                        ValidationGroup="SaveAction" Display="None" ErrorMessage="Not a Valid Amount" Type="Double"></asp:RangeValidator>
                                    <asp:RequiredFieldValidator ID="rqftxtServiceAmount" ControlToValidate="txtServiceAmount"
                                        runat="server" ErrorMessage="Enter Service Amount" ValidationGroup="SaveAction" Display="None">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%" align="right">
                                    Action Details:<font color="red">*</font>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtActionDetails" Width="250" Text="" Height="40" CssClass="txtboxtxt" runat="server">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtActionDetails"
                                        Display="None" ErrorMessage="Enter Action Remarks" ValidationGroup="SaveAction">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%" align="right">
                                    Action Date:<font color="red">*</font>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtActionEntryDate" CssClass="txtboxtxt" runat="server">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtActionEntryDate"
                                        Display="None" ErrorMessage="Enter Action Date" ValidationGroup="SaveAction">
                                    </asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender6" TargetControlID="txtActionEntryDate"
                                        runat="server">
                                    </cc1:CalendarExtender>
                                    <asp:CompareValidator Operator="DataTypeCheck" Type="Date" ControlToValidate="txtActionEntryDate"
                                        Display="None" ErrorMessage="Not a vaild Date" runat="server" ID="CompareValidator3"
                                        ValidationGroup="SaveAction">
                                    </asp:CompareValidator>
                                    <asp:Label ID="LblTimeAction" runat="server"></asp:Label>
                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="SaveAction"
                                        ShowMessageBox="True" ShowSummary="False" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Label ID="lblActionMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr><td></td>
                                <td>
                                    <asp:Button ID="btnSaveAction" runat="server" Width="70px" Text="Save" ValidationGroup="SaveAction"
                                        CssClass="btn" OnClick="btnSaveAction_Click" />
                                    <asp:Button ID="btnCloseAction" runat="server" Width="70px" Text="Close" CssClass="btn"
                                        OnClick="btnCloseAction_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                    </td>
                </tr>
            </table>
            <asp:Panel ID="panelUpdateProgress" Style="width: auto; display: none;" runat="server"
                CssClass="modalPopup">
                <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
                    <ProgressTemplate>
                        <div>
                            <img src="../images/loading9.gif" title="Image" alt="Please wait.." />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </asp:Panel>
            <cc1:ModalPopupExtender RepositionMode="RepositionOnWindowResize" ID="ModalProgress"
                runat="server" TargetControlID="panelUpdateProgress" BackgroundCssClass="modalBackground"
                PopupControlID="panelUpdateProgress" />
           <asp:HiddenField ID="hdnActionMode" runat="server" Value="" />
            <div id="dvBlanket" style="display: none;">
            </div>
            <div id="dvPopup" class="popUpDiv" style="display: none;">
                <a href="javascript:void(0);" onclick="CloseDiv(dvPopup,dvBlanket)" class="close">
                    <img alt="" src="../images/PopupClose.png" style="border: 0;" />
                </a>
                <div style="margin-left: 5px;">
                    <table>
                        <tr>
                            <td>
                                Reason for Cancellation:
                            </td>
                            <td>
                                <asp:TextBox ID="txtComment" runat="server" MaxLength="500" CssClass="simpletxt1"
                                    TextMode="MultiLine"></asp:TextBox>
                                <asp:HiddenField ID="hdnComment" runat="server" />
                                <asp:HiddenField ID="hdnRef" runat="server" />
                                <asp:RequiredFieldValidator ID="reqComplaintComment1" runat="server" ValidationGroup="a" 
                                 ControlToValidate="txtComment" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="btnSubmitComplaint" Text="OK" runat="server" ValidationGroup="a" OnClick="btnSubmitComplaint_Click" CssClass="btn" />  
                                 <input type="button" id="btnCancelComplaint" name="Cancel" value="Cancel" onclick="CloseDiv(dvPopup,dvBlanket);" class="btn" />                               
                            </td>
                        </tr>
                    </table>
                </div>
            </div> 
            
     <div id="dvConfirmBlanket" style="display: none;"></div>
            <div id="dvConfirmDublicateProductSerial" runat="server" class="popUpDiv" style="display: block; height:50px;">  
                <div style="margin-left: 5px;">
                    <table>
                        <tr>
                            <td>
                               Entered Product Serial No. is already used within 30 days. Do you want to Save it?
                            </td>                           
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="BtnConfirmYes" Text="YES" runat="server" OnClick="BtnConfirmYes_Click" CssClass="btn" />  
                                <input type="button" name="No" value="No" id="btnNo" class="btn" onclick="CloseConfirmDiv();" />   
                            </td>
                        </tr>
                    </table>
                </div>
            </div> 
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <script type="text/javascript">
        function CloseConfirmDiv() {

            document.getElementById('<%= dvConfirmDublicateProductSerial.ClientID %>').style.display = "none";
        }
</script>
</asp:Content>
