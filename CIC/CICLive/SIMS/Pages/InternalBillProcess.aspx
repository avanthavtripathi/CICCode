<%@ Page Title="" Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true" CodeFile="InternalBillProcess.aspx.cs" Inherits="SIMS_Pages_InternalBillProcess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
 
        
<asp:GridView PageSize="5" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridLines="both" AllowPaging="True"
                                        AllowSorting="True" AutoGenerateColumns="false" ID="gvConfirmation"
                                        runat="server" Width="100%" HorizontalAlign="Left"
                                        EnableSortingAndPagingCallbacks="True" 
                                        onselectedindexchanged="gvConfirmation_SelectedIndexChanged">
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                       <%-- PaymentRecID   Created_By Created_Date Modified_By Modified_Date      --%>
                                        <asp:BoundField DataField="ProductDivision_Desc" SortExpression="ProductDivision_Desc" HeaderStyle-Width="60px"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Division">
                                                <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Branch_Name" SortExpression="Branch_Name" HeaderStyle-Width="60px"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Branch">
                                                <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ASC_Name" SortExpression="ASC_Name" HeaderStyle-Width="60px"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="SC Name">
                                                <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>

                                      <asp:TemplateField  HeaderStyle-Width="150px" HeaderText="Internal Bill No">
                                      <ItemTemplate>
                                      <asp:LinkButton ID="lbtn" runat="server" Text='<%#Eval("Internal_Bill_No") %>'  ></asp:LinkButton>
                                      </ItemTemplate>
                                      </asp:TemplateField>
                                            <asp:BoundField DataField="Bill_Created_Date" SortExpression="Bill_Created_Date" HeaderStyle-Width="60px"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Bill Date">
                                                <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Amount" SortExpression="Amount" HeaderStyle-Width="60px"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Amount">
                                                <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                     <asp:TemplateField  HeaderStyle-Width="150px" HeaderText="Confirm">
                                      <ItemTemplate>
                                      <asp:CheckBox ID="chkbtn" runat="server" />
                                      </ItemTemplate>
                                      </asp:TemplateField>
                                       
                                           <%-- <asp:BoundField DataField="Region_Desc" SortExpression="Region_Desc" HeaderStyle-Width="60px"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Region">
                                                <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Branch_Name" SortExpression="Branch_Name" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Branch">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SC_Name" SortExpression="SC_Name"
                                                HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="SC Name">
                                                <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Unit_Desc" SortExpression="Unit_Desc" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Division">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PaymentMode" SortExpression="PaymentMode" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="PaymentMode">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="EffectiveFrom" SortExpression="EffectiveFrom" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Effective From">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                          <asp:BoundField DataField="Active_Flag" SortExpression="Active_Flag" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Status">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:CommandField ShowSelectButton="True" HeaderStyle-Width="60px" HeaderText="Edit">
                                                <HeaderStyle Width="60px" />
                                            </asp:CommandField>--%>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                        <img src="<%=ConfigurationManager.AppSettings["SIMSUserMessage"]%>" alt="" />
                                                        <b>No Record found</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                        <AlternatingRowStyle CssClass="fieldName" />
                                    </asp:GridView>
</asp:Content>

