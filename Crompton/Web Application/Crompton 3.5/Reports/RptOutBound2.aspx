<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="RptOutBound2.aspx.cs" Inherits="Reports_Rpt2" Title="Untitled Page" %>

<%@ Register src="../UC/OutboundTrend.ascx" tagname="OutboundTrend" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
   

    <uc1:OutboundTrend ID="OutboundTrend1" runat="server" />
   

</asp:Content>

