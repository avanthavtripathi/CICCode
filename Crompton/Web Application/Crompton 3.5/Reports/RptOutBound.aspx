<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="RptOutBound.aspx.cs" Inherits="Reports_RptOutBound" %>
<%@ Register src="../UC/OutBoundRpt.ascx" tagname="OutBoundRpt" tagprefix="uc1" %>
<asp:Content ContentPlaceHolderID="MainConHolder" ID="cntOut" runat="server" >
    <uc1:OutBoundRpt ID="OutBoundRpt1" runat="server" />
</asp:Content>
