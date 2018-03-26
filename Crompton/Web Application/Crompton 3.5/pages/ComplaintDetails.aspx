<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="ComplaintDetails.aspx.cs" Inherits="pages_ComplaintDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <table width="100%">
        <tr>
            <td>
                <b>
                    <asp:Label ID="lblLanguage" runat="server"></asp:Label></b>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td class="headingred">
                Complaint Status
            </td>
        </tr>
        <tr>
            <td class="bgcolorcomm">
                <asp:UpdatePanel ID="pnl" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="panMain" runat="server">
                            <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                <tr>
                                    <td>
                                        <b>Customer Details</b>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                            <tr>
                                                <td width="108" style="padding-left: 60px">
                                                    Select Customers
                                                </td>
                                                <td width="25%">
                                                    <asp:DropDownList CssClass="simpletxt1" ID="ddlCustomers" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:Button ID="btnGo" CausesValidation="false" runat="server" Text="Go" CssClass="btn"
                                                        OnClick="btnGo_Click" />
                                                </td>
                                                <td width="15%">
                                                    Customer ID</td>
                                                <td>
                                                  <asp:TextBox ID="TxtCustID" MaxLength="8" CssClass="txtboxtxt" runat="server" 
                                                        Text="" ValidationGroup="c"></asp:TextBox>
                                                    &nbsp;<asp:Button ID="BtnSearchCustID" runat="server" Text="Search" 
                                                        CssClass="btn" onclick="BtnSearchCustID_Click" ValidationGroup="c" />
                                               
                                                  
                                               
                                                   
                                               <div>
                                               <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TxtCustID" 
                                                        SetFocusOnError="true" ErrorMessage="Invalid Customer ID" Display="Dynamic" 
                                                        ValidationExpression="\d{8}" ValidationGroup="c" ></asp:RegularExpressionValidator>
                                               </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                            <tr>
                                                <td width="108" style="padding-left: 60px">
                                                    Pri. Phone:
                                                </td>
                                                <td width="25%">
                                                    <asp:TextBox ID="txtUnique" onkeypress="javascript:return checkNumberOnly(event);"
                                                        CssClass="txtboxtxt" runat="server" MaxLength="11" Text=""></asp:TextBox>
                                                       <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn" OnClick="btnSearch_Click" />
                                                        <asp:RegularExpressionValidator
                                                            ID="RegularExpressionValidator2" runat="server" ErrorMessage="Valid Phone only"
                                                            ControlToValidate="txtUnique" SetFocusOnError="True" ValidationExpression="\d{10,11}"></asp:RegularExpressionValidator>
                                                      
                                                </td>
                                                <td width="15%">
                                                    Complaint Ref No:
                                                </td>
                                                <td>
                                                    <asp:TextBox onkeypress="javascript:return checkNumberOnly(event);" ID="txtComplaintNo"
                                                        MaxLength="10" CssClass="txtboxtxt" runat="server" Text=""></asp:TextBox>
                                                    &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn" OnClick="btnSearch_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 60px">
                                                    Alt. Phone:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAltPhone" onkeypress="javascript:return checkNumberOnly(event);"
                                                        MaxLength="11" CssClass="txtboxtxt" runat="server" Text="" /><asp:RegularExpressionValidator
                                                            ID="RegularExpressionValidator1" runat="server" ErrorMessage="Valid Phone only"
                                                            ControlToValidate="txtAltPhone" SetFocusOnError="True" ValidationExpression="\d{10,11}"></asp:RegularExpressionValidator>
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
                                                    <asp:Label ID="lblFax" runat="server" Text=""></asp:Label>&nbsp;
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
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="panOpenCalls" runat="server">
                                            <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="1" bgcolor="#60A3AC">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>Open calls</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div id="dvGrid" style="width: 980px; overflow: auto;">
                                                            <!-- Detail grid Start-->
                                                            <asp:GridView PageSize="10" Width="980px" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                                                HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="True"
                                                                AllowSorting="True" DataKeyNames="BaseLineId" AutoGenerateColumns="False" ID="gvDetails"
                                                                runat="server" HorizontalAlign="Left" 
                                                                OnPageIndexChanging="gvDetails_PageIndexChanging" 
                                                                onrowcommand="gvDetails_RowCommand">
                                                                <RowStyle CssClass="gridbgcolor" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                                        <HeaderStyle Width="40px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Complaint_RefNo" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderText="Pri. Complaint RefNo">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="Sec. Complaint RefNo">
                                                                        <ItemTemplate>
                                                                            <a href="Javascript:void(0);" onclick="funCommonPopUp(<%#Eval("BaseLineId")%>)">
                                                                                <%#Eval("SecComplaint_RefNo")%></a>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderText="Stage">
                                                                        <ItemTemplate>
                                                                            <a href="Javascript:void(0);" onclick="funHistoryLog('<%#Eval("Complaint_RefNo")%>',<%#Eval("SplitComplaint_RefNo")%>)">
                                                                                <%#Eval("CallStage")%></a>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="LoggedDate" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-HorizontalAlign="Left" HeaderText="Log Date">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Unit_Desc" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-HorizontalAlign="Left" HeaderText="Product Division">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Quantity" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderText="Quantity">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderText="Service Contractor">
                                                                        <ItemTemplate>
                                                                            <a href="Javascript:void(0);" onclick="funSCPopUp(<%#Eval("Sc_Sno")%>)">
                                                                                <%#Eval("SC_Name")%></a>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-Width="200px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderText="Communication Details">
                                                                        <ItemTemplate>
                                                                            <a href="Javascript:void(0);" onclick="funCommLog('<%#Eval("Complaint_RefNo")%>',<%#Eval("SplitComplaint_RefNo")%>)">
                                                                                <%#Eval("LastComment")%></a>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="200px" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderStyle-Width="300px"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderText="Escalation" Visible="false">
                                                                        <ItemTemplate>
                                                                          <asp:CheckBox ID="chkescalate" runat="server" AutoPostBack="true" Checked='<%#Eval("isescalated")%>' Enabled='<%# ! Convert.ToBoolean(Eval("isescalated")) %>'
                                                                                oncheckedchanged="chkescalate_CheckedChanged" />
                                                                          <asp:TextBox ID="txtescalate" Width="110px" Enabled="false" runat="server" TextMode="MultiLine" Columns="2" ></asp:TextBox>
                                                                          <asp:Button ID="btnSave" Width="50px" Enabled="false" CommandName='<%#Eval("Complaint_Refno")%>' CommandArgument='<%#Eval("SplitComplaint_Refno")%>' CssClass="btn" runat="server" Text="Save" />
                                                                          
                                                                          
                                                                        </ItemTemplate>
                                                                         <HeaderStyle HorizontalAlign="Left" Width="300px" />
                                                                         <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    <img src="<%=ConfigurationManager.AppSettings["UserMessage"]%>" alt="" />
                                                                    <b>No Records found.</b>
                                                                </EmptyDataTemplate>
                                                                <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                                <AlternatingRowStyle CssClass="fieldName" />
                                                            </asp:GridView>
                                                            <!-- Detail grid End-->
                                                        </div>
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
                                        <asp:Button ID="btnClosedCall" runat="server" CssClass="btn" Text="Show Closed Calls"
                                            OnClick="btnClosedCall_Click" />&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnAddNew" runat="server" CausesValidation="false" CssClass="btn" Text="New Registration" OnClick="btnAddNew_Click" />&nbsp;&nbsp;&nbsp;
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
                                                                PageSize="10" RowStyle-CssClass="gridbgcolor" Width="980px">
                                                                <RowStyle CssClass="gridbgcolor" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                                        <HeaderStyle Width="40px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Complaint_RefNo" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderText="Pri. Complaint RefNo">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="Sec. Complaint RefNo">
                                                                        <ItemTemplate>
                                                                            <a href="Javascript:void(0);" onclick="funCommonPopUp(<%#Eval("BaseLineId")%>)">
                                                                                <%#Eval("SecComplaint_RefNo")%></a>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderText="Stage">
                                                                        <ItemTemplate>
                                                                            <a href="Javascript:void(0);" onclick="funHistoryLog('<%#Eval("Complaint_RefNo")%>',<%#Eval("SplitComplaint_RefNo")%>)">
                                                                                <%#Eval("CallStage")%></a>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="LoggedDate" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-HorizontalAlign="Left" HeaderText="Log Date">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Unit_Desc" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-HorizontalAlign="Left" HeaderText="Product Division">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Quantity" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderText="Quantity">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderText="Service Contractor">
                                                                        <ItemTemplate>
                                                                            <a href="Javascript:void(0);" onclick="funSCPopUp(<%#Eval("Sc_Sno")%>)">
                                                                                <%#Eval("SC_name")%></a>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-Width="200px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderText="Communication Details">
                                                                        <ItemTemplate>
                                                                            <a href="Javascript:void(0);" onclick="funCommLog('<%#Eval("Complaint_RefNo")%>',<%#Eval("SplitComplaint_RefNo")%>)">
                                                                                <%#Eval("LastComment")%></a>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="200px" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                         <%--// Bhawesh : 13 june 12 ; pump , appliance (we can repeat the complaint the complaint only for Two Div.)--%>
                                                                            <asp:Button ID="BtnRegister" runat="server" Text="Repeat Complaint" CssClass="btn" CommandArgument='<%#Eval("SecComplaint_RefNo")%>'
                                                                                onclick="BtnRegister_Click" Visible='<%# !Convert.ToBoolean(Eval("ReOpened")) %>' Enabled='<%# (Convert.ToString(Eval("ProductDivision_Sno")).Contains("16") || Convert.ToString(Eval("ProductDivision_Sno")).Contains("18"))  %>' />
                                                                       </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    <img src="<%=ConfigurationManager.AppSettings["UserMessage"]%>" alt="" />
                                                                    <b>No Records found.</b>
                                                                </EmptyDataTemplate>
                                                                <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                                <AlternatingRowStyle CssClass="fieldName" />
                                                            </asp:GridView>
                                                            <!-- Detail grid End-->
                                                        </div>
                                                    </td>
                                                    <tr>
                                                        <td style="width: 24%">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
