<%@ Page Title="" Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true" CodeFile="ClaimApprovalNonCG.aspx.cs" Inherits="SIMS_Pages_ClaimApprovalNonCG" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">



<script language="javascript" type="text/javascript">
window.onload = function()
{   //Get total no. of CheckBoxes in side the GridView.
    //Get total no. of checked CheckBoxes in side the GridView.
   Counter = 0;
   //$get('ctl00_MainConHolder_chkSMS').style.display='none';
   //$get('ctl00_MainConHolder_lblSMS').style.display='none';
}
function SetCounter()
  {
        Counter=0;
  }
  
  function ChildClick(CheckBox)
{   
   var HeaderCheckBox =document.getElementById ("ctl00_MainConHolder_GvDetails_ctl01_chkHeader");
  TotalChkBx =document.getElementById("ctl00_MainConHolder_GvDetails").rows.length;
  
  TotalChkBx=TotalChkBx-2;
  //alert(TotalChkBx);
   if(CheckBox.checked && Counter < TotalChkBx)
     { 
        Counter++;
     }
    else if(CheckBox.checked && Counter == 0)
      { Counter++;}
    else if(Counter > 0) 
     {Counter--;}
     //alert("counter" + Counter);
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
// Added as ASPX uses SPAN for checkbox
TotalChkBx =document.getElementById("ctl00_MainConHolder_GvDetails").rows.length;
TotalChkBx=TotalChkBx-1;
var oItem = spanChk.children;
var theBox=(spanChk.type=="checkbox")?spanChk:spanChk.children.item[0];
xState=theBox.checked;
elm=theBox.form.elements;
for(i=0;i<elm.length;i++)
if(elm[i].type=="checkbox" && elm[i].id!=theBox.id)
{//elm[i].click();
if(elm[i].checked!=xState)
elm[i].click();
//elm[i].checked=xState;
}
}

function fnClickOK(sender, e) {
    __doPostBack(sender, e);
} 
</script>



    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>

            <script type="text/javascript">
        function Openpopup(popurl)
        {
        winpops = window.open(popurl,"","width=1000, height=600, left=45, top=15, scrollbars=yes, menubar=no,resizable=no,directories=no,location=center")
        }
       
        
        

            </script>

            <table width="100%" cellpadding="2" cellspacing="0">
                <tr>
                    <td class="headingred">
                        Claim approval for Cancel Complaints at Initialization
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["SIMSAjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" colspan="2">
                        <table width="100%" border="0">
                            <tr>
                                <td width="100%" align="left" class="bgcolorcomm">
                                    <table border="0" style="width: 100%">
                                        <tr>
                                            <td colspan="2">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 19px" valign="top">
                                                ASC:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlasc" runat="server" CssClass="simpletxt1" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlasc_SelectedIndexChanged">
                                                </asp:DropDownList> 
                                                <asp:Label ID="lblerrmsg" runat="server" ForeColor="Red" Visible="false" Text="Please Select ASC."></asp:Label>
                                                <asp:Label ID="LblCount" runat="server" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 19px" valign="top">
                                                Division:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlDivision" runat="server" CssClass="simpletxt1" 
                                                    Width="180px">
                                                </asp:DropDownList>
                                                <asp:Button ID="BtnSearch" runat="server" CssClass="btn" Text="Go" Width="20px" 
                                                    onclick="BtnSearch_Click" />
                                                   <div>
                                                   <asp:RequiredFieldValidator ID="rfvdiv" runat="server" ControlToValidate="ddlDivision" Text="Division is mendatory." SetFocusOnError="true" InitialValue="0" />
                                                   </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="GvDetails" runat="server" AllowPaging="True" AllowSorting="True"
                                                    AlternatingRowStyle-CssClass="fieldName" AutoGenerateColumns="False" GridGroups="both"
                                                    HeaderStyle-CssClass="fieldNamewithbgcolor" HorizontalAlign="Left" PageSize="10"
                                                    RowStyle-CssClass="gridbgcolor" Width="100%" EnableSortingAndPagingCallbacks="True"
                                                    OnPageIndexChanging="GvDetails_PageIndexChanging" >
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
                                                        <asp:TemplateField HeaderText="Sno" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Service_Contractor" HeaderText="Service Contractor" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Product_Division" HeaderText="Product Division" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Complaint No.">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkcomplaint" runat="server" CommandArgument='<%#Eval("complaint_no")%>'
                                                                    CausesValidation="false" CommandName="stage" Text='<%#Eval("complaint_no")%>'
                                                                    OnClick="lnkcomplaint_Click"> </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldefid" runat="server" Text='<%#Eval("claim_no") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Claim No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblClaimNo" runat="server" Text='<%#Eval("claim_no") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="CLAIM_DATE" HeaderText="Claim Generated Date" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Product_Code" HeaderText="ProductSerialNo" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Product_desc" HeaderText="Product" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="defect" HeaderText="Defects" ItemStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="amount" HeaderText="Amount(Material + Service)" HeaderStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Addr" HeaderText="Address" HeaderStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="LastStatus" HeaderText="LastStatus" HeaderStyle-HorizontalAlign="Left">
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
                                            <td colspan="2" align="center">
                                            <asp:Button Text="View" Width="70px" ID="imgBtnView" CssClass="btn" runat="server"
                                                                CausesValidation="True" ValidationGroup="editt" 
                                                    onclick="imgBtnView_Click" Visible="False"   />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                     <table width="100%" border="0" cellspacing="1" cellpadding="2">
                                                    <tr>
                                                        <td>
                                    <div id="dvcomment" runat="server"  class="modalPopup" style="display:none">
                                    <table style="vertical-align:top;text-align:left;width:350px">
                                    <tr>
                                    <td>Activity</td>
                                    <td> <asp:Label ID="LblActivity" runat="server" /></td>
                                    </tr>
                                    <tr>
                                    <td>
                                    Delete Reason
                                    </td>
                                    <td>
                                    <asp:TextBox ID="txtreason" runat="server" ></asp:TextBox>
                                    <div>
                                    <asp:RequiredFieldValidator ID="reqreason" runat="server" ControlToValidate="txtreason" ErrorMessage="Cancel reason is mendatory." ValidationGroup="r"></asp:RequiredFieldValidator>
                                    </div>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td></td>
                                    <td>  
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Delete" ValidationGroup="r" onclick="btnCancel_Click" />
                                    <asp:Button ID="btnClose" runat="server" CssClass="btn" Text="Close Window"  />
                                    <cc1:ConfirmButtonExtender ID="BtnConfirmDel" runat="server" TargetControlID="btnCancel" ConfirmText="Permanent Deleting  Activity ?" />
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                        <ProgressTemplate>
                                            <img src="<%=ConfigurationManager.AppSettings["SIMSAjaxLoadingImageName"]%>" alt="" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    </td>
                                    </tr>
                                    </table>
                                  
                                    </div>
                                                            <asp:GridView RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                                                HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both"
                                                                AllowSorting="True" AutoGenerateColumns="False" ID="gvActivity" runat="server"
                                                                Width="100%" HorizontalAlign="Left" 
                                                                onpageindexchanging="gvActivity_PageIndexChanging" 
                                                                onrowdatabound="gvActivity_RowDataBound">
                                                                <RowStyle CssClass="gridbgcolor" />
                                                                <Columns>
                                                                
                                                                
                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                           
                                                                            <asp:HiddenField ID="hdnUnit_Desc" runat="server" Value='<%#Eval("Matcheck")%>' />
                                                                            <asp:Label ID="lblid" runat="server" Text='<%#Eval("id") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Complaint No">
                                                                        <ItemTemplate>
                                                                         <asp:HiddenField ID="Hdncheck" Value='<%#Eval("Matcheck") %>' runat="server" />
                                                                            <asp:Label ID="lblcomplaintNo" runat="server" Text='<%#Eval("Complaint_no") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Spare" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderText="Spare Consumption ">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="qty" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderText="Qty">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="activity" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderText="Activity">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Parameter1" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderText="Param-1">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="PossibleVaue1" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderText="PV-1">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Parameter2" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderText="Param-2">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="PossibleVaue2" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderText="PV-2">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Parameter3" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderText="Param-3">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="PossibleVaue3" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderText="PV-3">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Parameter4" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderText="Param-4">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                      <asp:BoundField DataField="PossibleVaue4" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderText="PV-4">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <%--<asp:BoundField DataField="rate" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderText="Rate">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>--%>
                                                                         <asp:BoundField DataField="remarks" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderText="Remarks">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="amount" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderText="Claim Amount">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" Text="Delete" ID="lbtncancel" CommandName='<%#Eval("activity") %>' CommandArgument='<%#Eval("id") %>' onclick="lbtncancel_Click" ></asp:LinkButton>
                                                                        <cc1:ModalPopupExtender runat="server" ID="mpas" TargetControlID="lbtncancel" BackgroundCssClass="modalBackground" 
                                                                        PopupControlID="dvcomment" CancelControlID="btnClose" >
                                                                        </cc1:ModalPopupExtender>
                                                                    </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <%--<asp:BoundField DataField="Remarks" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderText="Remarks">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                   <asp:TemplateField HeaderText="Reject">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkActivity" runat="server" AutoPostBack="true" 
                                                                                oncheckedchanged="chkActivity_CheckedChanged" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Rejection reason">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtreason" runat="server" Height="20px" TextMode="MultiLine"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>--%>
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    
                                                                </EmptyDataTemplate>
                                                                <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                                <AlternatingRowStyle CssClass="fieldName" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="imgBtnApprove" Width="70px" runat="server" CausesValidation="false" 
                                    CssClass="btn" Text="Approve" OnClick="imgBtnApprove_Click" />
                                                            
                                    
                                    <asp:Button ID="imgBtnClose" runat="server" CausesValidation="True" 
                                        CssClass="btn"  Text="Close" 
                                        ValidationGroup="editt" Visible="true" Width="70px" 
                                        onclick="imgBtnClose_Click" />
                                        <asp:Label ID="lblMessage" runat="server" ForeColor="Green" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

