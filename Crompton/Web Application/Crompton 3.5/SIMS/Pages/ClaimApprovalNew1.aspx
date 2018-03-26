<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true" CodeFile="ClaimApprovalNew1.aspx.cs" Inherits="SIMS_Pages_ClaimApprovalNew1"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script src="../../scripts/jquery-1.3.1.min.js" type="text/javascript"></script>
<script src="../../scripts/ActivityDeletePopUp.js" type="text/javascript"></script>
<link href="../../css/Activity_Delete_Popup.css" rel="stylesheet" type="text/css" />
 
<script language="javascript" type="text/javascript">
function funRepeatedComplaints(SapProductCode,CmpLogDate) 
{
    var SC_Sno = document.getElementById("<%=ddlasc.ClientID %>").value;
    var strUrl = '90DaysRepeatedComplaints.aspx?PSN=' + SapProductCode + '&CmpLogDate=' + CmpLogDate + '&SCNo=' + SC_Sno;
    window.open(strUrl, 'History', 'height=450,width=850,left=20,top=30,Location=0');
}
function Openpopup(popurl)
 {
   winpops = window.open(popurl,"","width=1000, height=600, left=45, top=15, scrollbars=yes, menubar=no,resizable=no,directories=no,location=center")
 }

</script>

<script type="text/javascript">
    $("[id*=chkHeader]").live("click", function() {
        var chkHeader = $(this);
        var grid = $(this).closest("table");
        $("input[type=checkbox]", grid).each(function() {
            if (chkHeader.is(":checked")) {
                $(this).attr("checked", "checked");
            } else {
                $(this).removeAttr("checked");
            }
        });
    });
    $("[id*=chkChild]").live("click", function() {
        var grid = $(this).closest("table");
        var chkHeader = $("[id*=chkHeader]", grid);
        if (!$(this).is(":checked")) {
            chkHeader.removeAttr("checked");
        } else {
            if ($("[id*=chkChild]", grid).length == $("[id*=chkChild]:checked", grid).length) {
                chkHeader.attr("checked", "checked");
            }
        }
    });
</script>

    <asp:UpdatePanel ID="updatepnl" UpdateMode="Conditional" runat="server">
        <ContentTemplate>

   <div id="popUpDiv" style="display:none;" class="ontop">  
     <table>
            <tr><td style="padding-top:10px;">Activity</td>
            <td style="padding-top:10px;"> <asp:Label ID="LblActivity" runat="server" /></td>
            </tr>
            <tr><td>Delete Reason</td>
            <td><asp:TextBox ID="txtreason" runat="server" ></asp:TextBox>
            </td></tr>
            <tr><td>&nbsp;</td>
            <td><asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Delete" ValidationGroup="r" onclick="btnCancel_Click" />
            <input id="ClosePopup" type="button" onclick="hide('popUpDiv')" class="btn" value="Close Window" />
            </td></tr>
            <tr><td colspan="2">
            <asp:RequiredFieldValidator ID="reqreason" Display="Dynamic" runat="server" ControlToValidate="txtreason" ErrorMessage="Cancel reason is mendatory." ValidationGroup="r"></asp:RequiredFieldValidator>
             <cc1:ConfirmButtonExtender ID="BtnConfirmDel" runat="server" TargetControlID="btnCancel" ConfirmText="Permanent Deleting  Activity ?" />
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
             <ProgressTemplate>
             <img src="<%=ConfigurationManager.AppSettings["SIMSAjaxLoadingImageName"]%>" alt="" /> </ProgressTemplate>
             </asp:UpdateProgress>
             </td></tr>
      </table>
         <asp:HiddenField ID="HdnActivityId" runat="server" Value="0" />
         <asp:HiddenField ID="HdnComplaint_No" runat="server" Value="0" />
  </div>       
                                
            <table width="100%" cellpadding="2" cellspacing="0">
                <tr>
                    <td class="headingred">
                        Claim approval screen
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
                                            <td valign="top">
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
                                            <td>
                                              Date:  &nbsp;</td>
                                            <td>
                                  From  <asp:TextBox runat="server" ID="txtFromDate" Width="102px" CssClass="txtboxtxt" 
                                        MaxLength="10" />
                                    <cc1:CalendarExtender ID="CEFrom" runat="server" TargetControlID="txtFromDate">
                                    </cc1:CalendarExtender>
                                                &nbsp;To
                                    <asp:TextBox runat="server" ID="txtToDate" Width="102px" CssClass="txtboxtxt" 
                                        MaxLength="10" />
                                    <cc1:CalendarExtender ID="CETo" runat="server" TargetControlID="txtToDate">
                                    </cc1:CalendarExtender>            
                                                </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Division:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlDivision" runat="server" CssClass="simpletxt1" 
                                                    Width="140px" AutoPostBack="True" 
                                                    onselectedindexchanged="ddlDivision_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:Button ID="BtnSearch" runat="server" CssClass="btn" Text="Go" Width="30px" 
                                                    onclick="BtnSearch_Click" />
                                                   <div>
                                                   <asp:RequiredFieldValidator ID="rfvdiv" Display="Dynamic" runat="server" ControlToValidate="ddlDivision" Text="Division is mendatory." SetFocusOnError="true" InitialValue="0" />
                                                   </div>
                                            </td>
                                        </tr>
                                        <tr><td>Repetitive Complain: </td>
                                        <td><asp:CheckBox ID="ChkRepetitive" runat="server" /></td>
                                        </tr>
                           
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="GvDetails" runat="server" AllowSorting="True"
                                                    AlternatingRowStyle-CssClass="fieldName" AutoGenerateColumns="False" GridGroups="both"
                                                    HeaderStyle-CssClass="fieldNamewithbgcolor" HorizontalAlign="Left"
                                                    RowStyle-CssClass="gridbgcolor" Width="100%" EnableSortingAndPagingCallbacks="True"
                                                     onrowdatabound="GvDetails_RowDataBound" >
                                                    
                                                    <RowStyle CssClass="gridbgcolor" />
                                                    <Columns>
                                                    <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkHeader" runat="server" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkChild" runat="server" />
                                                                </ItemTemplate>
                                                    </asp:TemplateField>
                                                        <asp:BoundField DataField="RowNumber" HeaderText="S.No." ItemStyle-HorizontalAlign="Left" />
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
                                                                <asp:HiddenField ID="hdncallstatus" runat="server" Value= '<%#Eval("callstatus")%>' />
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
                                                        <asp:BoundField DataField="Product_Code" HeaderText="SerialNo" ItemStyle-HorizontalAlign="Left">
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
                                                      <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Repetitive Complaint">
                                                            <ItemTemplate>
                                                            <a href="javascript:void(0);" id="dfh" onclick="funRepeatedComplaints('<%#Eval("SapProductCode")%>','<%#Eval("Loggeddate")%>')" >
                                                              <%#Eval("SapProductCode")%>
                                                            </a>
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
                                            <td colspan="2">
                                                <asp:Repeater ID="repager" runat="server" onitemcommand="repager_ItemCommand">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtn" runat="server" Font-Bold="false" 
                                                            Text="<%# Container.DataItem %>"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:Repeater>
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
                                    
                                    
                                                            <asp:GridView RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                                                HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both"
                                                                AutoGenerateColumns="False" ID="gvActivity" runat="server"
                                                                Width="100%" HorizontalAlign="Left" 
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
							                               	        <asp:BoundField DataField="Product_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderText="Product">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
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
                                                                    <a href="javascript:void(0);" onclick="pop('popUpDiv'+'@@@'+'<%#Eval("id")%>'+'@@@'+'<%#Eval("activity")%>'+'@@@'+'<%#Eval("complaint_no")%>')">
                                                                    <asp:Label ID="lblDelete" runat="server">Delete</asp:Label>
                                                                    </a>
                                                                    </ItemTemplate>
                                                                    </asp:TemplateField>
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

