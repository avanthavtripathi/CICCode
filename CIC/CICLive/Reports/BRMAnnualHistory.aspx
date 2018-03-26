<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="BRMAnnualHistory.aspx.cs" Inherits="Reports_BRMAnnualHistory" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
<asp:UpdatePanel ID="updatepnl" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <ContentTemplate>
<table width="100%" border="0">
        <tr>
            <td class="headingred">
                MIS-BRM Average Resolution Time (IS)_FY</td>
            <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /> </ProgressTemplate>
                </asp:UpdateProgress>
            </td>
        </tr>
        <caption>
            <p style="text-align:center;">
            </p>
            <tr>
                <td class="bgcolorcomm" colspan="2">
                    <table border="0" cellpadding="1" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <table border="0" cellpadding="1" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            Select Unit
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDLUnit" runat="server" AutoPostBack="True" 
                                                CssClass="simpletxt1"  
                                                ValidationGroup="editt" Width="200px">
                                                <asp:ListItem Text="Select" Value="0" />
                                                <asp:ListItem Text="FHP Motors" Value="AF" />
                                                <asp:ListItem Text="LT Motors" Value="AK" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvUnit" runat="server" 
                                                ControlToValidate="DDLUnit" Display="Dynamic" InitialValue="0" 
                                                SetFocusOnError="True" ValidationGroup="editt" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Year
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="Ddlyear" runat="server" AutoPostBack="True" 
                                                CssClass="simpletxt1"  
                                                ValidationGroup="editt" Width="200px">
                                                <asp:ListItem>2015</asp:ListItem>
                                                <asp:ListItem>2016</asp:ListItem>
                                                <asp:ListItem>2017</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn" 
                                                onclick="btnSearch_Click" Text="Search" ValidationGroup="editt" Width="100" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div ID="divReport" style="width:100%;overflow:scroll">
                                    <asp:GridView ID="gvReport" runat="server" 
                                        AlternatingRowStyle-CssClass="fieldName" CssClass="simpletxt1" 
                                        GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor" 
                                        HorizontalAlign="Center" RowStyle-CssClass="bgcolorcomm" 
                                        RowStyle-HorizontalAlign="Center" Width="100%">
                                    </asp:GridView>
                                </div>
                                <div style="text-align:center">
                                    <asp:Button ID="btnExport" runat="server" CssClass="btn" 
                                        onclick="btnExport_Click" Text="Export To Execl" Visible="False" Width="100" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </caption>
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

