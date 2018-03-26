<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="PPRreport.aspx.cs" Inherits="Reports_PPRreport" Title="PPR Report Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="PPRReport" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <table width="99%" style="border: solid 1px #eeeeee;">
                            <tr>
                                <td align="right">
                                    From Date:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtFromDate" CssClass="txtboxtxt" ValidationGroup="Select" />
                                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtFromDate">
                                    </cc1:CalendarExtender>
                                    <asp:CompareValidator Operator="DataTypeCheck" Type="Date" ControlToValidate="txtFromDate"
                                        ErrorMessage="Not a vaild Date" runat="server" ID="cmptxtFromDate" ValidationGroup="Select">
                                    </asp:CompareValidator>
                                </td>
                                
                            </tr>
                            <tr>
                                <td align="right">
                                    TO Date:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtTodate" CssClass="txtboxtxt" ValidationGroup="Select" />
                                    <asp:CompareValidator Operator="DataTypeCheck" Type="Date" ControlToValidate="txtTodate"
                                        ErrorMessage="Not a vaild Date" runat="server" ID="cmptxtTodate" ValidationGroup="Select">
                                    </asp:CompareValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtTodate">
                                    </cc1:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                <asp:Button ID="btnExportAlready" runat="server" Width="114px" Text="Export existing file"
                                        CssClass="btn" onclick="btnExportAlready_Click"  />
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnShowPulledRecord" runat="server" Width="114px" Text="Show Pulled Record"
                                        CssClass="btn" OnClick="btnShowPulledRecord_Click" /><asp:Button ID="btnExportExcel" runat="server" Width="114px" Text="Export New"
                                        CssClass="btn" OnClick="btnExportExcel_Click" />
                                </td>
                            </tr>
                         
                            
                              <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="lblStrMessage" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
