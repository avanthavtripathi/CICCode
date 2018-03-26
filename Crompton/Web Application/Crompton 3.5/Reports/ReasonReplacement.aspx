<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="ReasonReplacement.aspx.cs" Inherits="Reports_ReasonReplacement" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">



   <table width="100%" border="0">
        <tr>
            <td class="headingred">
                Reason of Replacement Report
            </td>
            <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                </asp:UpdateProgress>
            </td>
        </tr>
        <tr>
            <td class="bgcolorcomm" colspan="2">
                <asp:UpdatePanel ID="pnl" runat="server">
                    <ContentTemplate>
                        <center>
                            <table border="0" cellpadding="3" cellspacing="0">
                                <tr id="trBranchAsc" runat="server">
                                    <td style="width: 7%;">
                                        Region :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="True" CssClass="simpletxt1"
                                            Style="min-width: 124px;" 
                                            onselectedindexchanged="ddlRegion_SelectedIndexChanged">
                                            <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="East" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="West" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="North" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="South" Value="7"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 10%;">
                                        Branch :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlBranchSearch" runat="server" CssClass="simpletxt1" AutoPostBack="True"
                                             Style="min-width: 124px;" 
                                            onselectedindexchanged="ddlBranchSearch_SelectedIndexChanged">
                                            <asp:ListItem Text="select" Value="">Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 18%;">
                                        Service Contractor :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlServicecontractorSearch" CssClass="simpletxt1" runat="server"
                                            AutoPostBack="True"
                                            Style="min-width: 180px; height: 19px;">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Year
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlYear" runat="server" Width="124px" CssClass="simpletxt1"
                                            Height="16px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        Month
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlMonth" runat="server" Width="124px" CssClass="simpletxt1"
                                            Height="16px">
                                           
                                        </asp:DropDownList>
                                    </td>
                                    
                                    <td>
                                    
                                    Product Division
                                    </td>
                                    <td>
                                     <asp:DropDownList ID="ddlproddivision" runat="server" Width="124px" CssClass="simpletxt1"
                                            Height="16px">
                                            <asp:ListItem Value="0">All</asp:ListItem>
                                           
                                        </asp:DropDownList>
                                    </td>
                                    
                                </tr>
                             
                                <tr>
                                    <td colspan="4" align="center" style="padding-bottom: 10px;">
                                       <%-- <asp:Button ID="BtnSEARCH" runat="server" Text="SEARCH"
                                            CssClass="btn" Width="100px"  />--%> &nbsp;&nbsp;&nbsp;
                                             <asp:Button ID="btnExport"  runat="server" Text="EXPORT TO EXCEL"
                                            CssClass="btn" Width="120px" onclick="btnExport_Click" />
                                    </td>
                                   
                                </tr>
                            </table>
                        </center>
                      <%--  <div style="float: right; margin-top: 2%;">
                            <table width="100%" border="0" cellpadding="1" cellspacing="0" style="margin-top: 4%;">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblheader" runat="server" CssClass="headingred"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gvSummary" Width="100%" runat="server" AutoGenerateColumns="true"
                                            Style="text-align: center;" AllowPaging="True" OnPageIndexChanging="gvSummary_PageIndexChanging"
                                            PageSize="10">
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
                                            <HeaderStyle CssClass="fieldNamewithbgcolor" HorizontalAlign="Center" />
                                            <AlternatingRowStyle CssClass="fieldName" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblsummry" runat="server" ForeColor="Red" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            </div>--%>
                            
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnExport" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>

</asp:Content>

