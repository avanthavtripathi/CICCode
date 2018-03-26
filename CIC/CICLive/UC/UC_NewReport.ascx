<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_NewReport.ascx.cs" EnableViewState="true" Inherits="UC_UC_NewReport"   %> 
<script language="javascript" type="text/javascript">
   function funRptComplaints(RecID)
        {
             var strUrl='../Reports/NwReportPopUp.aspx?RecId=' + RecID;
             compWin=window.open(strUrl,'NwReportPopUp','height=450,width=750,scrollbars=yes,left=20,top=30');
             if (window.focus) {compWin.focus()}
        }
</script>

<asp:Button ID="btnExport" runat="server" CssClass="btn" Text="Export" Visible="false" 
    Width="70px" onclick="btnExport_Click" />
<asp:DataList ID="Rep" runat="server" RepeatDirection="Horizontal" ItemStyle-VerticalAlign="Top" EnableViewState="false"  >
<ItemTemplate>
<asp:GridView ID="GvNReport" runat="server" AutoGenerateColumns="false" RowStyle-Height="45px" Width="350px" EnableViewState="false" >
<Columns>
<asp:TemplateField HeaderText="Region">
<ItemTemplate>
<asp:Label ID="lblRegionSNo" runat="server" Text='<%# Eval("Region_SNo")%>' Visible="false" > </asp:Label>
<asp:Label ID="lblRegion" runat="server" Text='<%# Eval("Region_Desc")%>' > </asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Branch">
<ItemTemplate>
<asp:Label ID="lblBranchSNo" runat="server" Text='<%# Eval("Branch_SNo")%>' Visible="false" > </asp:Label>
<asp:Label ID="lblBranch" runat="server" Text='<%# Eval("Branch_Name")%>' > </asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField>
<ItemTemplate>
<asp:Label ID="lblUnitSNo" runat="server" Text='<%# Eval("Unit_SNo")%>' Visible="false" > </asp:Label>
<asp:Label ID="lblUnit" runat="server" Text='<%# Eval("Unit_Desc")%>' > </asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField  HeaderText="Open Balance at the End of previous Month" > 
<ItemTemplate>
<a href="Javascript:void(0);" onclick ="funRptComplaints('<%#Eval("RecID")%>')" >
<%# Eval("LastOpen")%>
</a>

</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField  HeaderText="Complaint Received Current Month"> 
<ItemTemplate>
<asp:Label ID="lblopen" runat="server" Text='<%# Eval("Count")%>' > </asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField  HeaderText="Total Complaints"> 
<ItemTemplate>
<asp:Label ID="lbltotalMnth" runat="server" Text='<%# Eval("TotalForMonth")%>' > </asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField  HeaderText="Complaint Closed in the Month"> 
<ItemTemplate>
<asp:Label ID="lblopen" runat="server" Text='<%# Eval("ClosedCount")%>' > </asp:Label>

</ItemTemplate>
</asp:TemplateField>
</Columns>
</asp:GridView>
</ItemTemplate>
</asp:DataList>
