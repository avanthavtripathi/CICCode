<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="SMSComplaintP2.aspx.cs" Inherits="pages_SMSComplaintP2" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">


<script language="javascript" type="text/javascript">
    function OpenActivityPop(url) {
        newwindow = window.open(url, 'name', 'height=500,width=750,scrollbars=1,resizable=no,top=1');
        if (window.focus) {
            newwindow.focus()
        }
       return false;
    }   
</script>
     <asp:UpdatePanel ID="pnl" runat="server" UpdateMode="Always">
                  <ContentTemplate>
<table width="100%">
<tr>
<td></td>

 <td align="right" style="padding-right: 10px;">
   <asp:UpdateProgress AssociatedUpdatePanelID="pnl" ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
        </ProgressTemplate>
   </asp:UpdateProgress>
</td>

</tr>
<tr>
     <td class="headingred" colspan="2" >
         Auto-Closed Complaints By SMS
     </td>
</tr>
<tr>
<td colspan="2">
<asp:GridView ID="gv" runat="server" Width="100%" AlternatingRowStyle-CssClass="fieldName" AutoGenerateColumns="False" GridGroups="both">
<RowStyle CssClass="gridbgcolor" />
<Columns>
     <asp:TemplateField HeaderText="Complaint RefNo" HeaderStyle-HorizontalAlign="Left"
        ItemStyle-HorizontalAlign="Left">
        <ItemTemplate>
            <asp:HiddenField ID="hdnBaselineID" runat="server" Value='<%#Eval("BaseLineId")%>' />
            <%#Eval("Complaint_Split")%>
        </ItemTemplate>
             <HeaderStyle HorizontalAlign="Left" />
             <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>
     <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
        <ItemTemplate>
               <%#Eval("Customer")%></a>
         </ItemTemplate>
        <HeaderStyle HorizontalAlign="Left" />
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Address" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
        <ItemTemplate>
            
            <%#Eval("Address")%>
        </ItemTemplate>
        <HeaderStyle HorizontalAlign="Left" />
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Contact No." HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
        <ItemTemplate>
            <%#Eval("UniqueContact_No")%>
        </ItemTemplate>
        <HeaderStyle HorizontalAlign="Left" />
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Product" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
        <ItemTemplate>
            <%#Eval("ProductDivision_Desc")%>
        </ItemTemplate>
        <HeaderStyle HorizontalAlign="Left" />
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Engineer Name" HeaderStyle-HorizontalAlign="Left"
        ItemStyle-HorizontalAlign="Left">
        <ItemTemplate>
          <asp:Label ID="lblSEname" runat="server" Text='<%# Eval("ServiceEng_Name") %>' ></asp:Label>
        </ItemTemplate>
        <HeaderStyle HorizontalAlign="Left" />
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Stage" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
        <ItemTemplate>
            <a href="Javascript:void(0);" onclick="funHistoryLog('<%#Eval("Complaint_RefNo")%>','01')">
                <%#Eval("CallStage")%></a>
        </ItemTemplate>
        <HeaderStyle HorizontalAlign="Left" />
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>
     <asp:TemplateField>
        <ItemTemplate>
            <asp:LinkButton ID="lnkBtnNext" CommandArgument='<%#Eval("Complaint_RefNo")%>' 
                CausesValidation="false" runat="server"  Text="Process Service Charge & Closure" 
                onclick="lnkBtnNext_Click"   >
            </asp:LinkButton>
        </ItemTemplate>
    </asp:TemplateField>
</Columns>
<HeaderStyle CssClass="fieldNamewithbgcolor" />
<AlternatingRowStyle CssClass="fieldName" />
<EmptyDataTemplate>
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                <b>No Complaints found</b>
            </td>
        </tr>
    </table>
</EmptyDataTemplate>
</asp:GridView>
</td>
</tr>

            
 
</table>

    
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>


