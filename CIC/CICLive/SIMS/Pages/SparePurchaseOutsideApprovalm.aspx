<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true" CodeFile="SparePurchaseOutsideApprovalm.aspx.cs" Inherits="SIMS_Pages_SparePurchaseOutsideApprovalm" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


    
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">

    <script language="javascript" type="text/javascript">
window.onload = function()
{ 
   Counter = 0;
}

function ChildClick(CheckBox)
{   
   var HeaderCheckBox =document.getElementById ("ctl00_MainConHolder_GvDetails_ctl01_chkHeader");
  TotalChkBx =document.getElementById("ctl00_MainConHolder_GvDetails").rows.length;
  
  TotalChkBx=TotalChkBx-2;
  
   if(CheckBox.checked && Counter < TotalChkBx)
     { 
        Counter++;
     }
    else if(CheckBox.checked && Counter == 0)
      { Counter++;}
    else if(Counter > 0) 
     {Counter--;}
     if(Counter < TotalChkBx)
    {
       HeaderCheckBox.checked = false;
    }   
   else if(Counter == TotalChkBx)
    {
       HeaderCheckBox.checked = true;   
    }
}

function SelectAllCheckboxes(spanChk){
    TotalChkBx =document.getElementById("ctl00_MainConHolder_GvDetails").rows.length;
    TotalChkBx=TotalChkBx-1;
    var oItem = spanChk.children;
    var theBox=(spanChk.type=="checkbox")?spanChk:spanChk.children.item[0];
    xState=theBox.checked;
    elm=theBox.form.elements;
    for(i=0;i<elm.length;i++)
    if(elm[i].type=="checkbox" && elm[i].id!=theBox.id)
    {
        if(elm[i].checked!=xState)
        elm[i].click();
    }
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
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Branch
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlBranch" runat="server" Width="175px" CssClass="simpletxt1"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    ASC
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlASC" runat="server" Width="175px" CssClass="simpletxt1"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlASC_SelectedIndexChanged" ValidationGroup="editt">
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
                                                     <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkHeader" runat="server" onclick="javascript:SelectAllCheckboxes(this);" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkChild" onclick="javascript:ChildClick(this);" runat="server" />
                                                                </ItemTemplate>
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
                                                          <asp:BoundField DataField="QuantityPurchased" HeaderText="Quantity" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                           <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Document No." ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lbldocno" runat="server" Text='<%# Eval("DocumentNo") %>'  />
                                                          </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField> 
                                                           <asp:BoundField DataField="Actiondate" HeaderText="Document Date" DataFormatString="{0:dd-MMM-yy}" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                         <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkview" CausesValidation="false" runat="server" CommandName="complaintaction"
                                                                    OnClick="lnkview_Click">View</asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
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
                                                            &nbsp;</td>
                                                        <td>
                                                            <asp:Button ID="imgBtnApprove" Width="70px" runat="server" CausesValidation="false"
                                                                CssClass="btn" Text="Approve" OnClick="imgBtnApprove_Click" />
                                                            <asp:Button Text="Close" Width="70px" ID="imgBtnClose" CssClass="btn" runat="server"
                                                                CausesValidation="True" OnClientClick="javascript:window.close();" 
                                                                ValidationGroup="editt" Visible="true" onclick="imgBtnClose_Click" />
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


