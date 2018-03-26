<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="UpLoadDailer.aspx.cs" Inherits="pages_UpLoadDailer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <table width="99%" style="border: solid 1px #eeeeee;">
                         <tr style="display:none">
                                <td align="left">
                                    Region:<font color='red'>*</font>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlRegion" CssClass="simpletxt1" 
                                        ValidationGroup="Select" Enabled="False" /> 
                                      <asp:RequiredFieldValidator ErrorMessage="Select Region" ID="ddlReqRegion" runat="server"
                                        ValidationGroup="Select" ControlToValidate="ddlRegion" Enabled="False" 
                                        EnableTheming="True"></asp:RequiredFieldValidator>                                                                 
                                </td>
                                <td align="left">
                                    Division:<font color='red'>*</font>
                                </td>
                                <td>
                                     <asp:DropDownList runat="server" ID="ddlDivision" CssClass="simpletxt1" 
                                         ValidationGroup="Select" Enabled="False" /> 
                                      <asp:RequiredFieldValidator ErrorMessage="Select Division" ID="ddlReqDivision" runat="server"
                                        ValidationGroup="Select" ControlToValidate="ddlDivision" Enabled="False"></asp:RequiredFieldValidator> 
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    From Date:<font color='red'>*</font>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtFromDate" CssClass="txtboxtxt" ValidationGroup="Select" />
                                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtFromDate">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ErrorMessage="From Date is required" ID="rfvFromdate"
                                        runat="server" ValidationGroup="Select" ControlToValidate="txtFromDate"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator Operator="DataTypeCheck" Type="Date" ControlToValidate="txtFromDate"
                                        ErrorMessage="Not a vaild Date" runat="server" ID="cmptxtFromDate" ValidationGroup="Select">
                                    </asp:CompareValidator>
                                </td>
                                <td align="left">
                                    TO Date:<font color='red'>*</font>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtTodate" CssClass="txtboxtxt" ValidationGroup="Select" />
                                    <asp:CompareValidator Operator="DataTypeCheck" Type="Date" ControlToValidate="txtTodate"
                                        ErrorMessage="Not a vaild Date" runat="server" ID="cmptxtTodate" ValidationGroup="Select">
                                    </asp:CompareValidator>
                                    <asp:RequiredFieldValidator ErrorMessage="To Date is required" ID="rfvTo" runat="server"
                                        ValidationGroup="Select" ControlToValidate="txtTodate"></asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtTodate">
                                    </cc1:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Select Percentage(%):<font color='red'>*</font>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlPer" CssClass="simpletxt1" Width="175" ValidationGroup="Select"
                                        runat="server" Enabled="False">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="10">10%</asp:ListItem>
                                        <asp:ListItem Value="20">20%</asp:ListItem>
                                        <asp:ListItem Value="30">30%</asp:ListItem>
                                        <asp:ListItem Value="40">40%</asp:ListItem>
                                        <asp:ListItem Value="50">50%</asp:ListItem>
                                        <asp:ListItem Value="60">60%</asp:ListItem>
                                        <asp:ListItem Value="70">70%</asp:ListItem>
                                        <asp:ListItem Value="80">80%</asp:ListItem>
                                        <asp:ListItem Value="90">90%</asp:ListItem>
                                        <asp:ListItem Value="100">100%</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:CompareValidator Operator="NotEqual" ErrorMessage="Percentage is required "
                                        runat="server" ID="cmvddlPer" ControlToValidate="ddlPer" 
                                        ValueToCompare="0" ValidationGroup="Select" Enabled="False"></asp:CompareValidator>
                                </td>
                                <td align="left">
                                    &nbsp;
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnUpload" runat="server" Width="114px" Text="Upload Dialer Call"
                                        CssClass="btn" OnClick="btnSearch_Click" ValidationGroup="Select" />
                                </td>
                            </tr>
                            <tr>
                                <td id="tdMessage" runat="server" visible="true" align="left">
                                    <asp:Label ID="lblStrMessage" runat="server"></asp:Label>
                                </td>
                                <td align="left">
                                </td>
                                <td align="left">
                                </td>
                                <td id="tdExport" runat="server" visible="false" align="left">
                                    <asp:Button ID="btnExportExcel" runat="server" Width="114px" Text="Save To Excel"
                                        CssClass="btn" OnClick="btnExportExcel_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <div>
            <asp:GridView ID="gvcomm" runat="server">
                </asp:GridView>
            </div>
        </ContentTemplate>
          <Triggers>
            <asp:PostBackTrigger ControlID="btnExportExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
