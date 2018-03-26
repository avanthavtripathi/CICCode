<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true"
    CodeFile="InvoiveSummary.aspx.cs" Inherits="SIMS_Reports_InvoiveSummary" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <table width="100%" border="0">
        <tr>
            <td class="headingred">
                Claim Approval For Fan
            </td>
            <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </td>
        </tr>
        <tr>
            <td class="bgcolorcomm" colspan="2">
                <asp:UpdatePanel ID="pnls" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <center>
                            <table border="0" cellpadding="3" cellspacing="0" style="width: 100%; margin-left: 0px">
                                <tr id="trBranchAsc" runat="server">
                                    <td style="width: 7%;">
                                        Region :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="True" CssClass="simpletxt1"
                                            OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" Style="min-width: 100px;">
                                            <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="East" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="West" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="North" Value="7"></asp:ListItem>
                                            <asp:ListItem Text="South" Value="4"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 7%;">
                                        Branch :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlBranchSearch" runat="server" CssClass="simpletxt1" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlBranchSearch_SelectedIndexChanged" Style="min-width: 97px;">
                                            <asp:ListItem Text="select" Value="">Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 18%;">
                                        Service Contractor :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlServicecontractorSearch" CssClass="simpletxt1" runat="server"
                                            Style="min-width: 291px;">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Year <span style="color: Red;">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlYear" runat="server" Width="100px" CssClass="simpletxt1">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="2015">2015</asp:ListItem>
                                            <asp:ListItem Value="2016">2016</asp:ListItem>
                                            <asp:ListItem Value="2017">2017</asp:ListItem>
                                            <asp:ListItem Value="2018">2018</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        Month <span style="color: Red;">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlMonth" runat="server" Width="100px" CssClass="simpletxt1">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">January</asp:ListItem>
                                            <asp:ListItem Value="2">February</asp:ListItem>
                                            <asp:ListItem Value="3">March</asp:ListItem>
                                            <asp:ListItem Value="4">April</asp:ListItem>
                                            <asp:ListItem Value="5">May</asp:ListItem>
                                            <asp:ListItem Value="6">June</asp:ListItem>
                                            <asp:ListItem Value="7">July</asp:ListItem>
                                            <asp:ListItem Value="8">August</asp:ListItem>
                                            <asp:ListItem Value="9">September</asp:ListItem>
                                            <asp:ListItem Value="10">October</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">December</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="display: none;">
                                        &nbsp;
                                    </td>
                                    <td style="display: none;">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" style="padding-bottom: 10px;">
                                        <asp:Button ID="btnsummary" runat="server" Text="Download Summary Report" CssClass="btn"
                                            Width="150px" OnClick="btnsummary_Click" />
                                    </td>
                                </tr>
                            </table>
                        </center>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnsummary" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
