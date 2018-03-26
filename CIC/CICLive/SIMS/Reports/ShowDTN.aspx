<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true" CodeFile="ShowDTN.aspx.cs" Inherits="SIMS_Reports_ShowDTN" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
<asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                      Re-Print Destroy Transaction(s) Report
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
                                   Service Contractor
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="DDlscs" runat="server" Width="175px"
                                        CssClass="simpletxt1" ValidationGroup="editt" >
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID ="RfvSC" runat="server" ValidationGroup="editt" InitialValue="0" Text="*" ControlToValidate="DDlscs" />
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                   Product Division
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlproductDiv" runat="server" Width="175px" CssClass="simpletxt1"
                                       ValidationGroup="editt">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID ="RequiredFieldValidator1" ValidationGroup="editt" runat="server" InitialValue="0" Text="*" ControlToValidate="DDlscs" />
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Year
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="DdlYear" runat="server" Width="175px" CssClass="simpletxt1"
                                     ValidationGroup="editt">
                                        <asp:ListItem Selected="True" Value="0" Text="Select"></asp:ListItem>
                                        <asp:ListItem Value="2011" Text="2011"></asp:ListItem>
                                        <asp:ListItem Value="2012" Text="2012"></asp:ListItem>
                                        <asp:ListItem Value="2013" Text="2013"></asp:ListItem>
                                        <asp:ListItem Value="2014" Text="2014"></asp:ListItem>
                                   </asp:DropDownList>
                                   <asp:RequiredFieldValidator ID ="RequiredFieldValidator2" ValidationGroup="editt" runat="server" InitialValue="0" Text="*" ControlToValidate="DDlscs" />
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
                        </table>
                        <br />
                        <table width="100%" border="0">
                            <tr>
                                <td>
                                     <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" ID="gvComm" runat="server" AutoGenerateColumns="false" 
                                        Width="100%" HorizontalAlign="Left" onrowcommand="gvComm_RowCommand" >
                                        <Columns>
                                         <asp:TemplateField HeaderText="DestroyTransactionNo">
                                        <ItemTemplate>
                                         <asp:Label runat="server"  ID="LblBillNo" Text='<%# Eval("DestroyTransactionNo")%>'  />
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField  HeaderText="ProductDivision">
                                        <ItemTemplate>
                                         <asp:Label runat="server"  ID="LblProdDiv" Text='<%# Eval("ProductDivision")%>'  />
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Complaints Count" DataField="ComplaintCount" />
                                        <asp:TemplateField>
                                        <ItemTemplate>
                                         <asp:LinkButton runat="server"  Text="Print" ID="BtnPrint"  />
                                        </ItemTemplate>
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
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

