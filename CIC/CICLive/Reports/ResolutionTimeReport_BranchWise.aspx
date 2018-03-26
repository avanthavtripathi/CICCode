<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="ResolutionTimeReport_BranchWise.aspx.cs" Inherits="Reports_ResolutionTimeReport_BranchWise" Title="Untitled Page" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">

<table width="100%"> <tr>
            <td class="headingred">
                MIS-ASC Branch Wise Resolution Time Report 0-2 days for CP & 0-3 for Motors
            </td>
            <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                </asp:UpdateProgress>
            </td>   
        </tr>
        <tr>
        <td colspan="2">
        
        <table width="70%">
             <tr id="TrRegion" runat="server">
            <td align="right">
                Region:
            </td>
            <td>
                <asp:DropDownList ID="ddlregion" runat="server" 
                    OnSelectedIndexChanged="ddlregion_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
         <tr id="TrBranch" runat="server">
            <td align="right">
                Branch:
            </td>
            <td>
                <asp:DropDownList ID="ddlbranch" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        
        <tr><td align="right">Select CP - IS:</td>
        <td> 
            <asp:DropDownList ID="ddlCPIS" Width="70px" CssClass="simpletxt1" runat="server" >
            </asp:DropDownList>
        </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" CssClass="btn" Width="70px" Text="Search" 
                    onclick="btnSearch_Click" />
            </td>
        </tr>
        
        </table>
        </td>
        </tr>
   
        <tr>
        <td colspan="2">
                    <div id="divReport" style="width:100%; height:450px; "  >                                      
                                        <rsweb:ReportViewer ID="rvMisDetail" 
                                            Font-Names="Verdana" runat="server" Width="100%" >
                                        </rsweb:ReportViewer>  
                   </div>
                                  
        
        </td></tr>
        <tr>
            <td colspan="2" class="MsgTotalCount">
                Note : Cumulative calculated for last one year.</td>
        </tr>
        <tr><td></td>  <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label><td></td></tr>
        </table>
          
                                
</asp:Content>

