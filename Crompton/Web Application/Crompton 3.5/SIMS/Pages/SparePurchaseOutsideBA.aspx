<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true" CodeFile="SparePurchaseOutsideBA.aspx.cs" Inherits="SIMS_Pages_SparePurchaseOutsideBA" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
<%--<asp:UpdatePanel ID="updatepnl" runat="server">
     
        <ContentTemplate>--%>
            <table width="100%">
                <tr>
                    <td class="headingred">
                     Spare Purchase Rejected/Resend - Outside CG
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["SIMSAjaxLoadingImageName"]%>" alt="" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" colspan="2">
                        <table width="98%" border="0">
                          <tr>
                                <td colspan="2" align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                    <font color='red'>*</font>
                                    <%=ConfigurationManager.AppSettings["MandatoryText"]%>
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
                                    <asp:DropDownList ID="ddlBranch" runat="server" Width="175px" CssClass="simpletxt1"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    ASC
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlASC" runat="server" Width="175px" CssClass="simpletxt1"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlASC_SelectedIndexChanged" ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Date From
                                </td>
                                <td align="left">
                                    <asp:TextBox runat="server" ID="txtFromDate" CssClass="txtboxtxt" ValidationGroup="editt" 
                                        MaxLength="10"  />
                                    <%--<asp:CompareValidator ID="CompareValidator1" runat="server" Type="Date" Operator="DataTypeCheck"
                                        ControlToValidate="txtFromDate" Display="none" ValidationGroup="editt" SetFocusOnError="true"></asp:CompareValidator>--%>
                                    <cc1:CalendarExtender ID="CEFrom" runat="server" TargetControlID="txtFromDate" Format="dd/MMM/yy" >
                                    </cc1:CalendarExtender>
                                    To
                                    <asp:TextBox runat="server" ID="txtToDate" CssClass="txtboxtxt" ValidationGroup="editt"
                                        MaxLength="10" />
                                    <%--<asp:CompareValidator ID="CompareValidator7" runat="server" Type="Date" Operator="DataTypeCheck"
                                        ControlToValidate="txtToDate" Display="none" ValidationGroup="editt" SetFocusOnError="true"></asp:CompareValidator>--%>
                                    <cc1:CalendarExtender ID="CETo" runat="server" TargetControlID="txtToDate" Format="dd/MMM/yy">
                                    </cc1:CalendarExtender>
                                    <%--<asp:CompareValidator ID="CompareValidator2" Type="Date" ControlToValidate="txtToDate" 
                                        ControlToCompare="txtFromDate" Operator="GreaterThanEqual" runat="server" ErrorMessage="To Date should be greater than From Date"
                                        ValidationGroup="editt"></asp:CompareValidator>--%>
                                    <asp:Label ID="lblDateErr" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                            <td>
                            
                            </td>
                            <td align="left">
                            <asp:Button Width="80px" Text="Go" CssClass="btn" ValidationGroup="editt" CausesValidation="true"
                                        ID="btnSearch" runat="server" OnClick="btnSearch_Click" />
                            </td>
                            </tr>
                            <tr>
                            <td colspan="2">
                            <asp:GridView ID="GvDetails" runat="server" 
                                    AlternatingRowStyle-CssClass="fieldName" AutoGenerateColumns="False" GridGroups="both"
                                                    HeaderStyle-CssClass="fieldNamewithbgcolor" HorizontalAlign="Left"
                                                    RowStyle-CssClass="gridbgcolor" Width="100%" 
                                    onrowdatabound="GvDetails_RowDataBound" >
                                                    <RowStyle CssClass="gridbgcolor" />
                                                    <Columns>
                                                  
													<asp:TemplateField HeaderText="Sno" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                     </asp:TemplateField>
                                                       <%-- <asp:BoundField DataField="RowNumber" HeaderText="S.No." ItemStyle-HorizontalAlign="Left" />--%>
                                                        <asp:BoundField DataField="Unit_Desc" HeaderText="Product Division" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                             <asp:BoundField DataField="Branch_Name" HeaderText="Branch" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SC_Name" HeaderText="Service Contractor" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                           <asp:BoundField DataField="SAP_Desc" HeaderText="Spare" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                       <asp:BoundField DataField="QuantityPurchased" HeaderText="Quantity" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                         <asp:BoundField DataField="Bill_No"  HeaderText="Bill No." ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Document No." ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lbldocno" runat="server" Text='<%# Eval("DocumentNo") %>'  />
                                                        </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField> 
                                                         <asp:BoundField DataField="Actiondate"  HeaderText="Document Date" DataFormatString="{0:dd-MMM-yy}" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                            <asp:CheckBox ID="chkreject" Checked='<%# Eval("IsApproved") %>' runat="server" Visible="false"  />
                                                            <asp:Label ID="lblRejectionReason" runat="server" Text='<%# Eval("rejectionReason") %>' ></asp:Label>
                                                             </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Re-Approval">
                                                            <ItemTemplate>
                                                             <asp:HiddenField ID="hdnSpareID" runat="server" Value='<%# Eval("Spare_ID") %>' />
                                                             <asp:HiddenField ID="hdnBillNo" runat="server" Value='<%# Eval("Bill_No") %>'  />
                                                            <asp:CheckBox ID="chkresend" runat="server" Visible="false"   />
                                                            <asp:TextBox ID="txtcomment" runat="server" Visible="false"  ></asp:TextBox>
                                                             </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                       </Columns>
                                                    <EmptyDataTemplate>
                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                                    <img src="<%=ConfigurationManager.AppSettings["simsUserMessage"]%>" alt="" />
                                                                    <b>No Record found</b>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                  
                                                  
                                                    <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                    <AlternatingRowStyle CssClass="fieldName" />
                                                </asp:GridView>
                            
                            </td>
                            </tr>
                            <tr>
                            <td colspan="2">
                            
                            <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="ReSend for Approval" 
                                    onclick="btnSave_Click" /></td>
                            </tr>
                            <tr>
                            <td  colspan="2">
                            <asp:Label ID="lblMessage" runat="server" ForeColor="Green" Text=""></asp:Label>
                            </td>
                            </tr>
                            </table>
                            </td>
                            </tr>
                            </table>
                         <%--   </ContentTemplate>
                            </asp:UpdatePanel>--%>
 </asp:Content>

