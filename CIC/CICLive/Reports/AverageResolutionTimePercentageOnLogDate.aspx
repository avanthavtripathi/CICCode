<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="AverageResolutionTimePercentageOnLogDate.aspx.cs" Inherits="Reports_AverageResolutionTimePercentageOnLogDate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
<script type="text/javascript">
    function Validate() {
        var ddlYear = document.getElementById('<%= ddlYear.ClientID %>');
        var ddlMonth = document.getElementById('<%= ddlMonth.ClientID %>');
        if (ddlYear.value == "0") {
            alert("Please select Year!");
            return false;
        }
        if (ddlMonth.value == "0") {
            alert("Please select Month!");
            return false;
        }
        return true;
    }
</script>
    <table width="100%" border="0">
        <tr>
            <td class="headingred">
                Resolution Time (%) Report On Log Date</td>
            <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /> </ProgressTemplate>
                </asp:UpdateProgress>
            </td>
        </tr>
        <tr>
            <td class="bgcolorcomm" colspan="2">
                <asp:UpdatePanel ID="pnl" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                    <center>
                        <table width="50%" border="0" cellpadding="3" cellspacing="0">
                            <tr>
                                <td>Year <span style="color:Red;">*</span></td>
                                <td>
                                    <asp:DropDownList ID="ddlYear" runat="server" Width="100px" CssClass="simpletxt1">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="2015">2015</asp:ListItem>
                                        <asp:ListItem Value="2016">2016</asp:ListItem>
                                    </asp:DropDownList>
                                    
                                </td>
                                <td>Month <span style="color:Red;">*</span></td>
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
                               <td>Branch Wise</td>
                               <td> <asp:CheckBox ID="ChkIsBranch" runat="server" /></td>
                            </tr>
                            <tr>
                            <td colspan="6" align="center" style="padding-bottom:10px;">
                                <asp:Button ID="BtnSEARCH" runat="server" Text="SEARCH" OnClientClick="return Validate()" CssClass="btn" Width="100px"
                                    onclick="BtnSEARCH_Click" />
                                 <asp:Button ID="btnExport" Visible="false" runat="server" Text="EXPORT TO EXCEL" CssClass="btn" Width="120px" onclick="btnExport_Click" />   
                                    </td></tr>
                            </table>
                      </center>
                            <table width="100%" border="0" cellpadding="1" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:GridView ID="GrdReport" Width="100%" runat="server" 
                                        AutoGenerateColumns="true" onrowdatabound="GrdReport_RowDataBound">
                                        <RowStyle CssClass="gridbgcolor" />
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
                                         <HeaderStyle CssClass="fieldNamewithbgcolor" HorizontalAlign="Left" />
                                        <AlternatingRowStyle CssClass="fieldName" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr><td style="padding-top:10px;">
                                    SLA for Calls registered on Sunday and after 3 pm on Saturday should be counted from Monday. 
                                   <br />Last Two Days Calls at the month end should be counted from the 1<sup>st</sup> working day of the next month.
                                  <br /><br />*Based on Log date
                            </td></tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnExport" />
                    </Triggers>
                    
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>

