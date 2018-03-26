<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="NewReport.aspx.cs" Inherits="Reports_NewReport" Title="Branchwise Complaint Report" EnableEventValidation="false" %>

<%@ Register src="../UC/UC_NewReport.ascx" tagname="UC_NewReport" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
   <uc1:UC_NewReport ID="UC_NewReport1" runat="server"  />
</asp:Content>

