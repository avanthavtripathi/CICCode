<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true" CodeFile="SparePurchaseOutsideApproval.aspx.cs" Inherits="SIMS_Pages_SparePurchaseOutsideApproval" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">

    <script type="text/javascript">
      function CloseAfterApproval()
        {
          alert("Claim Approved Successfully");
          window.opener.location.reload(false);
          window.close();
        }
      function refreshparent()
         { 
          alert("Claim Rejected Successfully");
          window.opener.location.reload(false);
                //  window.opener.location.href = "ClaimApprovalNew1.aspx?ReturnId=True";
                  window.close();
         }
  </script>
<asp:UpdatePanel ID="updatepnl" runat="server">
     
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                      Spare Purchase Outside Approval Screen
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["SIMSAjaxLoadingImageName"]%>" alt="" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" colspan="2">
                        <table width="98%" border="0">
                            <tr>
                                <td colspan="2" align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                    <font color='red'>*</font>
                                    <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Region
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlRegion" runat="server" Width="175px" CssClass="simpletxt1"
                                        AutoPostBack="true" 
                                        OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" ValidationGroup="editt" 
                                        Enabled="False">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Branch
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlBranch" runat="server" Width="175px" CssClass="simpletxt1"
                                        AutoPostBack="true" 
                                        OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" ValidationGroup="editt" 
                                        Enabled="False">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    ASC
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlASC" runat="server" Width="175px" CssClass="simpletxt1"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlASC_SelectedIndexChanged" 
                                        ValidationGroup="editt" Enabled="False">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                </td>
                                <td align="left">
                                
                                    </td>
                            </tr>
                            <tr>
                                <td align="left" class="MsgTDCount">
                                    Total Number of Records :
                                    <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="GvDetails" runat="server" AlternatingRowStyle-CssClass="fieldName" AutoGenerateColumns="False" GridGroups="both"
                                                    HeaderStyle-CssClass="fieldNamewithbgcolor" HorizontalAlign="Left"
                                                    RowStyle-CssClass="gridbgcolor" Width="100%" >
                                                    <RowStyle CssClass="gridbgcolor" />
                                                    <Columns>
                                                  
													<asp:TemplateField HeaderText="Sno" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                     </asp:TemplateField>
                                                       <%-- <asp:BoundField DataField="RowNumber" HeaderText="S.No." ItemStyle-HorizontalAlign="Left" />--%>
                                                        <asp:BoundField DataField="Unit_Desc" HeaderText="Product Division" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                            <%-- <asp:BoundField DataField="Branch" HeaderText="Product Division" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>--%>
                                                        <asp:BoundField DataField="SC_Name" HeaderText="Service Contractor" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                           <asp:BoundField DataField="SAP_Desc" HeaderText="Spare" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                           <asp:BoundField DataField="RatePurchased" HeaderText="PurchaseRate" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                       <asp:BoundField DataField="QuantityPurchased" HeaderText="Quantity" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                           <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                         <asp:BoundField DataField="Bill_No" HeaderText="Bill No." ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                           <asp:BoundField DataField="Bill_date" HeaderText="Bill Date" DataFormatString="{0:dd-MMM-yy}" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Document No." ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lbldocno" runat="server" Text='<%# Eval("DocumentNo") %>'  />
                                                        </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField> 
                                                         <asp:TemplateField HeaderText="Reject">
                                                            <ItemTemplate>
                                                         
                                                             <asp:CheckBox ID="chkreject" runat="server"  AutoPostBack="true" OnCheckedChanged="chkreject_CheckedChanged"    />
                                                           
                                                              <asp:HiddenField ID="hdnSpareID" runat="server" Value='<%# Eval("Spare_ID") %>' />
                                                              <asp:HiddenField ID="hdnBillNo" runat="server" Value='<%# Eval("Bill_No") %>'  />
                                                              <asp:HiddenField ID="hdnQty" runat="server" Value='<%# Eval("QuantityPurchased") %>'  />
                                                            </ItemTemplate>
                                                       
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rejection Reason">
                                                        <ItemTemplate>
                                                           <asp:TextBox ID="txtRejectionReason" runat="server" Height="20px" TextMode="MultiLine"></asp:TextBox>
                                                        </ItemTemplate>
                                                        </asp:TemplateField>
                                                       </Columns>
                                                    <EmptyDataTemplate>
                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                                    <img src="<%=ConfigurationManager.AppSettings["simsUserMessage"]%>" alt="" />
                                                                    <b>No Record found</b>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                  
                                                  
                                                    <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                    <AlternatingRowStyle CssClass="fieldName" />
                                                </asp:GridView>
									        </td>
				                </tr>
                            <tr>
                                                <td height="25" colspan="2" align="center">
                                                <table>
                                                    <tr>
                                                        <td align="right" valign="top">
                                                            <asp:Button Text="Reject" Width="70px" ID="imgBtnReject" CssClass="btn" runat="server"
                                                                CausesValidation="True" ValidationGroup="editt"  
                                                                OnClick="imgBtnReject_Click"  Enabled="False"  />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="imgBtnApprove" Width="70px" runat="server" CausesValidation="false"
                                                                CssClass="btn" Text="Approve" OnClick="imgBtnApprove_Click" />
                                                            <asp:Button Text="Close" Width="70px" ID="imgBtnClose" CssClass="btn" runat="server"
                                                                CausesValidation="True" OnClientClick="javascript:window.close();" ValidationGroup="editt" Visible="true" />
                                                               <div>
                                        <br />
                                         <asp:Label ID="lblMessage" runat="server" ForeColor="Green" Text=""></asp:Label>
                                        </div>         
                                                       
                                                        </td>
                                        
                                                    </tr>
                                                </table>
                                            </td>
                                              </tr>
                                       
                        </table>
                   
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



