<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="WebFeedbackAction.aspx.cs" Inherits="pages_WebFeedbackAction" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
<asp:UpdatePanel ID="updatepnl" runat="server">
      <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                   Divisional Service Head Actions
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
                                   Product Division
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="DdlProdDiv" runat="server" Width="175px"
                                        CssClass="simpletxt1" ValidationGroup="editt" >
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvpd" runat="server" 
                                        Text="*" ControlToValidate="DdlProdDiv" InitialValue="0"  ValidationGroup="editt"   />
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                   Service Request Type
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="DDlServiceRequest" runat="server" Width="175px" CssClass="simpletxt1"
                                       ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    State
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="DdlState" runat="server" Width="175px" CssClass="simpletxt1"
                                     ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="30%">
                                    Date From 
                                    &nbsp;</td>
                                <td align="left">
                          <asp:TextBox runat="server" ID="txtFromDate" CssClass="txtboxtxt" ValidationGroup="editt"
                                        AutoPostBack="True" />
                                    <cc1:CalendarExtender ID="CEFrom" runat="server" TargetControlID="txtFromDate" >
                                    </cc1:CalendarExtender>
                                    To
                                    <asp:TextBox runat="server" ID="txtToDate" CssClass="txtboxtxt" ValidationGroup="editt"
                                        AutoPostBack="True" />
                                    <cc1:CalendarExtender ID="CETo" runat="server" TargetControlID="txtToDate">
                                    </cc1:CalendarExtender>
                             </td>
                            </tr>
                     
                            <tr>
                                <td align="right">
                                <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                    &nbsp;</td>
                                <td align="left">
                                    <br />
                                    <asp:Button Width="70px" Text="Search" CssClass="btn" ValidationGroup="editt" CausesValidation="true"
                                        ID="btnSearch" runat="server" OnClick="btnSearch_Click" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table width="100%" border="0">
                            <tr>
                                <td>
                                     <asp:GridView RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" ID="gvComm" 
                                         runat="server" AutoGenerateColumns="false" Width="100%" HorizontalAlign="Left" onrowcommand="gvComm_RowCommand" >
                                        <Columns> <%-- Address	ContactNo			ActionDate	--%>
                                        <asp:TemplateField HeaderText="SNo.">
                                        <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Unit_Desc" HeaderText="ProductDivision" />
                                        <asp:BoundField DataField="date" HeaderText="Date"  />
                                        <asp:TemplateField HeaderText="CustomerName">
                                        <ItemTemplate>
                                           <asp:Label id="LblcustName" runat="server" Text='<%#Eval("CustName")%>' />
                                        </ItemTemplate>
                                        </asp:TemplateField> 
                                        <asp:BoundField DataField="ContactNo" HeaderText="ContactNo"  />
                                        <asp:BoundField DataField="EMail" HeaderText="EMail"  />
                                        <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                        <div style="width:250px;text-align:left;word-break:break-all;" > <%#Eval("FeedBack")%> </div>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:BoundField DataField="WSCFeedback_Desc" HeaderText="Type" />
                                        <asp:TemplateField>
                                        <ItemTemplate>
                                        <asp:LinkButton ID="Lbtn" runat="server" Text="FeedBack" CommandName="Action" CommandArgument='<%#Eval("RecID")%>' ></asp:LinkButton>
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
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

