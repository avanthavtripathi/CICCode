<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="Maintainance.aspx.cs" Inherits="pages_Maintennace"%>

<%@ Register src="../UserControls/getMaintainence.ascx" tagname="getMaintainence" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
   <div align="center">
     <uc1:getMaintainence ID="getMaintainence1" runat="server" /></div>
</asp:Content>

