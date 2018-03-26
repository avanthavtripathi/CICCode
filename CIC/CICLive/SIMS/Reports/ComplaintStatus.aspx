<%@ Page Title="" Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true" CodeFile="ComplaintStatus.aspx.cs" Inherits="SIMS_Reports_ComplaintStatus" %>

<asp:Content ID="ComplaintStatusrept" ContentPlaceHolderID="MainConHolder" Runat="Server">
<script language="javascript" type="text/javascript">
   function fnOpenHistPopUp(Cno,spno)
    {
      var strUrl='../../Pages/HistoryLog.aspx?CompNo='+ Cno + '&SplitNo='+ spno;
      window.open(strUrl,'History','height=550,width=900,left=20,top=30');
    }
</script>
    <asp:UpdatePanel ID="updatepnl" runat="server">
          <ContentTemplate>
            <table width="100%" >
                <tr>
                    <td class="headingred">
                   Complaint Status
                    </td>
                    <td align='<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>' 
                        style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img alt="" 
                                    src='<%=ConfigurationManager.AppSettings["SIMSAjaxLoadingImageName"]%>' />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" style="padding: 10px">
                        <table border="0" width="98%">
                            <tr>
                                <td align='<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>' 
                                    colspan="2">
                                    <font color="red">*</font> <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="30%">
                                    Complaint Number
                                </td>
                                <td align="left">
                                 
                                    <asp:TextBox ID="txtcomplaintno" runat="server" ValidationGroup="1"></asp:TextBox>
                                    <asp:Button ID="Btngo1" runat="server"  CssClass="btn" CommandName="ByComplaint" onclick="Btngo_Click" Text="Go" ValidationGroup="1" />
                                 <div>
                                     <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtcomplaintno" ErrorMessage="Please enter a complaint Number." ValidationGroup="1"></asp:RequiredFieldValidator>
                                 </div>
                                </td>
                            </tr>
                      
                           
                            <tr>
                                <td align="right" valign="top" width="30%">
                                   Returnable Defective Spare Challan Number</td>
                                <td align="left">
                                   <asp:TextBox ID="txtchallanno" runat="server" ValidationGroup="2"></asp:TextBox>
                                    <asp:Button ID="btngo2" runat="server"  CssClass="btn" CommandName="ByChallan" onclick="Btngo_Click" Text="Go" ValidationGroup="2" />
                                 <div>
                                     <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtchallanno" ErrorMessage="Please enter a challan Number." ValidationGroup="2"></asp:RequiredFieldValidator>
                                 </div></td>
                            </tr>
                      
                           
                        </table>
                        <asp:Label ID="lblmsg" runat="server" ForeColor="Red" ></asp:Label>
                        <br />
                     <table border="0" id="tbl" runat="server" width="98%" style="text-align:left;line-height:20px;">
                      <tr>
                     <td>
                        <div>
                         <b>Complaint Details :</b>
                     </div>
                     <div>
                      <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" 
                             RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                                                        
                             HeaderStyle-CssClass="fieldNamewithbgcolor" BorderStyle="None"
                                                                        GridLines="Vertical" 
                             Width="700px" >
                          <RowStyle CssClass="gridbgcolor" />
                         <Columns>
                         <asp:BoundField DataField="Branch_Name" HeaderText="Branch Name" /> 
                         <asp:BoundField DataField="ASC_Name" HeaderText="ASC Name" /> 
                         
                         <asp:TemplateField HeaderText="Complaint No">
                         <ItemTemplate>
                          <a href="javascript:void(0);" class="links" onclick="fnOpenHistPopUp('<%#Eval("c1")%>','<%#Eval("c2")%>');"><%#Eval("Complaint_No")%></a>
                        
                         </ItemTemplate>
                         
                         </asp:TemplateField>
                         <asp:BoundField DataField="Complaint_Date" HeaderText="Complaint Date" /> 
                         <asp:BoundField DataField="Product_Desc" HeaderText="Product"  /> 
                          <asp:BoundField DataField="Complaint_Warranty_Status" HeaderText="Warranty Status" /> 
                      
                         <asp:BoundField DataField="Active_Flag" HeaderText="Active_Flag"  /> 
                         
                         </Columns>
                          <HeaderStyle CssClass="fieldNamewithbgcolor" />
                          <AlternatingRowStyle CssClass="fieldName" />
                           <EmptyDataTemplate>
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                        <b>No Complaint found.</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                         </asp:GridView>
                     </div>
                        <br />
                     <div>
                         <b>Activity Details :</b>
                     </div>
                     <div>
                      <asp:GridView ID="gvactivity" runat="server" AutoGenerateColumns="false" 
                             RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                                                        
                             HeaderStyle-CssClass="fieldNamewithbgcolor" BorderStyle="None"
                                                                        GridLines="Vertical" 
                             Width="700px" >
                          <RowStyle CssClass="gridbgcolor" />
                         <Columns>
                         <asp:BoundField DataField="Activity_Description" HeaderText="Activity" /> 
                         <asp:BoundField DataField="Activity_Amount" HeaderText="Activity Amount" /> 
                     <%--    <asp:BoundField DataField="Activity_Disposal_Flag" HeaderText="Activity Disposal Flag" /> --%>
                           <asp:BoundField DataField="claim_No" HeaderText="Claim No" /> 
                         <asp:BoundField DataField="Claim_date" HeaderText="Claim Date" NullDisplayText="NA" /> 
                          <asp:BoundField DataField="CG_Approval_flag_Activity" HeaderText="Claim Approved"  NullDisplayText="Claim not processed." /> 
                         <asp:BoundField DataField="Approved_Date_Activity" HeaderText="Approved Date" /> 
                          <asp:BoundField DataField="ApprovedBy" HeaderText="ApprovedBy" /> 
                      
                         <asp:BoundField DataField="Rejection_Reason_Activity" HeaderText="Rejection Reason" NullDisplayText="NA" /> 
                         
                         </Columns>
                          <HeaderStyle CssClass="fieldNamewithbgcolor" />
                          <AlternatingRowStyle CssClass="fieldName" />
                           <EmptyDataTemplate>
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                        <b>No Activities found.</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                         </asp:GridView>
                     </div>
                        <br />
                        <div>
                            <b>Spare Details : </b>
                     </div>
                       <div><asp:GridView ID="gvspare" runat="server" AutoGenerateColumns="false" 
                               RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                                                        
                               HeaderStyle-CssClass="fieldNamewithbgcolor"  BorderStyle="None"
                                                                        GridLines="Vertical" 
                               Width="700px" >
                           <RowStyle CssClass="gridbgcolor" />
                         <Columns>
                         <asp:BoundField DataField="Spare_Desc" HeaderText="Spare" /> 
                         <asp:BoundField DataField="Spare_Amount" HeaderText="Spare Amount" /> 
                         <asp:BoundField DataField="Spare_Disposal_Flag" HeaderText="Spare Disposal Flag" /> 
                         <asp:BoundField DataField="claim_No" HeaderText="Claim No" /> 
                         <asp:BoundField DataField="Claim_date" HeaderText="Claim Date" NullDisplayText="NA" /> 
                         <asp:BoundField DataField="CG_Approval_flag_spare" HeaderText="Claim Approved"  NullDisplayText="Claim not processed." /> 
                         <asp:BoundField DataField="Approved_Date_Spare" HeaderText="Approved Date" /> 
                         <asp:BoundField DataField="ApprovedBy" HeaderText="ApprovedBy" /> 
                         <asp:BoundField DataField="Rejection_Reason_spare" HeaderText="Rejection Reason" NullDisplayText="NA" /> 
                
                         
                         
                         </Columns>
                           <HeaderStyle CssClass="fieldNamewithbgcolor" />
                           <AlternatingRowStyle CssClass="fieldName" />
                            <EmptyDataTemplate>
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                        <b>No Spare found.</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                         </asp:GridView></div>
                         <br />
                            <div>
                            <b>Other Details : </b>
                     </div>
                         <div>
                         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" 
                               RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                                                        
                               HeaderStyle-CssClass="fieldNamewithbgcolor"  BorderStyle="None"
                                                                        GridLines="Vertical" 
                              Width="650px" >
                           <RowStyle CssClass="gridbgcolor" />
                         <Columns>
                           <asp:BoundField DataField="details" HeaderText="Activity/Spares" /> 
                           <asp:BoundField DataField="Claim_No" HeaderText="Claim No" /> 
                           <asp:BoundField DataField="Stage_Desc" HeaderText="Current Stage" /> 
                            <asp:BoundField DataField="Challan_No" HeaderText="Challan No(R/D)" /> 
                           <asp:BoundField DataField="Challan_By" HeaderText="Challan By" /> 
                            <asp:BoundField DataField="Challan_date" HeaderText="Challan Date" /> 
                             <asp:BoundField DataField="TransportationDetail" HeaderText="Transportation Detail" NullDisplayText="NA" /> 
                                <asp:BoundField DataField="Action_By_CG" HeaderText="Action" NullDisplayText="NA"  /> 
                                <asp:BoundField DataField="Action_Taken_By" HeaderText="Action By" NullDisplayText="NA"  /> 
                                     <asp:BoundField DataField="Action_Date" HeaderText="Action Date" NullDisplayText="NA"  /> 
                                      <asp:BoundField DataField="Internal_Bill_No" HeaderText="Internal BillNo" NullDisplayText="Not Generated." /> 
                                      <asp:BoundField DataField="Bill_Created_By" HeaderText="Bill CreatedBy" NullDisplayText="NA"  /> 
                                     <asp:BoundField DataField="Bill_Created_Date" HeaderText="Bill CreatedDate" NullDisplayText="NA"  />   
                                              
                     
                         
                         </Columns>
                           <HeaderStyle CssClass="fieldNamewithbgcolor" />
                           <AlternatingRowStyle CssClass="fieldName" />
                         </asp:GridView></div>
                               </td>
                          </tr>
                           <tr>
                                   <td>
                                  
                                   </td>
                                  
                               </tr>
                     </table>
                     </td>
                     </tr>
                     </table>
                   
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

