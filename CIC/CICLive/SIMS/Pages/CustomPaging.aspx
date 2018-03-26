<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true"
    CodeFile="CustomPaging.aspx.cs" Inherits="SIMS_Pages_CustomPaging" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <div align="center">
        <asp:GridView ID="grdUserDtl" runat="server" AutoGenerateColumns="false" AllowPaging="true"
            PageSize="10" PagerSettings-Visible="true" OnPageIndexChanging="grdUserDtl_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="CustomerId" HeaderText="Customer ID" />
                <asp:BoundField DataField="Complaint_RefNo" HeaderText="Complaint No" />
                <asp:BoundField DataField="ProductDivision_Desc" HeaderText="ProductDivision Desc" />
            </Columns>
            <EmptyDataTemplate>
                No company found for your search
            </EmptyDataTemplate>
        </asp:GridView>
       
    </div>
    
</asp:Content>
