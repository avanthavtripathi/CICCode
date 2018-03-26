<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="YearlyResolutionRpt.aspx.cs" Inherits="Reports_YearlyResolutionRpt" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
         <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                      Yearly Resolution Report
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
                                   Year
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="Ddlyear" runat="server" Width="175px"
                                        CssClass="simpletxt1" ValidationGroup="editt" >
                                        <asp:ListItem>2014</asp:ListItem>
                                        <asp:ListItem>2015</asp:ListItem>
                                        <asp:ListItem>2016</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="30%">
                                    Product Group 
                                    &nbsp;</td>
                                <td align="left">
                              <asp:DropDownList ID="ddlPg" runat="server" Width="175px" CssClass="simpletxt1" >
                                         <asp:ListItem Value="0" Text="IS" ></asp:ListItem>
                              </asp:DropDownList>
                              <asp:RequiredFieldValidator ID="rfvpg" runat="server" ControlToValidate="ddlPg"
                              ErrorMessage="Please select a Product Group" Display="Dynamic" ValidationGroup="editt" InitialValue="-1" ></asp:RequiredFieldValidator>
                              
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
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" ID="gvComm" runat="server" 
                                        Width="100%" HorizontalAlign="Left" >
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

