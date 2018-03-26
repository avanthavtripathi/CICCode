<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GenerateInternalBill.aspx.cs"
    Inherits="SIMS_Pages_GenerateInternalBill" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

<script language="javascript" type="text/javascript">
window.onload = function()
{  
Counter = 0;
}
function SetCounter()
{
Counter=0;
}

function ChildClick(CheckBox)
{
var HeaderCheckBox = document.getElementById("ctl00_MainConHolder_gvChallanDetail_ctl01_chkHeader");
TotalChkBx = document.getElementById("ctl00_MainConHolder_gvChallanDetail").rows.length;

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
// Added as ASPX uses SPAN for checkbox
TotalChkBx = document.getElementById("ctl00_MainConHolder_gvChallanDetail").rows.length;
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
                        Internal Bill Generation Screen
                    </td>
                </tr>
            </table>
            <table width="100%" class="bgcolorcomm">
                <tr>
                    <td>
                        ASC:
                    </td>
                    <td colspan="3">
                        <asp:Label ID="lblASCName" runat="server"></asp:Label>
                        <asp:HiddenField ID="hdnASC_Id" runat="server" />
                    </td>
                        <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["SIMSAjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td>
                        Division:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="simpletxt1" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" ValidationGroup="NewBill"
                            Width="206px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Generated Bills:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBills" runat="server" CssClass="simpletxt1" ValidationGroup="RePrint"
                            Width="206px">
                        </asp:DropDownList>
                    </td>
                </tr>
              
                <tr>
                    <td>
                        Type (M/S)<font color="red">*</font></td>
                    <td>
                        <asp:DropDownList ID="ddltype" runat="server" CssClass="simpletxt1" 
                            ValidationGroup="NewBill" Width="206px">
                            <asp:ListItem Selected="True" Value="" Text="Select"></asp:ListItem>
                            <asp:ListItem>M</asp:ListItem>
                            <asp:ListItem>S</asp:ListItem>
                        </asp:DropDownList>
                         <asp:RequiredFieldValidator ID="Rfvddltype" runat="server" ControlToValidate="ddltype"
                        ErrorMessage="Type is required." InitialValue="" SetFocusOnError="true"
                        ValidationGroup="NewBill" Display="Dynamic" ></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                  <tr>
                    <td>
                        Service/Spare Used:<font color="red">*</font></td>
                    <td>
                        <asp:DropDownList ID="ddlServiceSpares" runat="server" CssClass="simpletxt1" ValidationGroup="NewBill"
                        Width="206px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFVddlServiceSpares" runat="server" ControlToValidate="ddlServiceSpares"
                        ErrorMessage="Service/Spare Used is required." InitialValue="0" SetFocusOnError="true"
                        ValidationGroup="NewBill" Display="Dynamic" Enabled="False"></asp:RequiredFieldValidator>
                        </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        Date Range(Logged Date)<font color="red">*</font></td>
                    <td>
                                    From  <asp:TextBox runat="server" ID="txtFromDate" CssClass="txtboxtxt" 
                                        MaxLength="10"  />
                                    <cc1:CalendarExtender ID="CEFrom" runat="server" TargetControlID="txtFromDate">
                                    </cc1:CalendarExtender >
                                                &nbsp;To
                                    <asp:TextBox runat="server" ID="txtToDate" CssClass="txtboxtxt" 
                                        MaxLength="10" />
                                    <cc1:CalendarExtender ID="CETo" runat="server" TargetControlID="txtToDate">
                                    </cc1:CalendarExtender>
                                    <div>
                                    <asp:CustomValidator ID="RFVdate" runat="server" 
                                        ControlToValidate="txtFromDate" Display="Dynamic" 
                                        SetFocusOnError="true" ValidationGroup="NewBill" 
                                            onservervalidate="RFVdate_ServerValidate"></asp:CustomValidator>
                                    </div>
                       </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="BtnSearch" runat="server" CssClass="btn" OnClick="BtnSearch_Click"
                            Text="Search" Width="78px" ValidationGroup="NewBill" /><br />
                     </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="BtnRePrint" runat="server" CssClass="btn" OnClick="BtnRePrint_Click"
                            Text="Re-Print" Width="78px" ValidationGroup="RePrint" /><br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlBills"
                            ErrorMessage="Bill No is required." InitialValue="0" SetFocusOnError="true" ValidationGroup="RePrint"
                            Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        &nbsp;
                    </td>
                </tr>
                <tr runat="server" id="trRecordCount">
                    <td colspan="5">
                        Total Number of Records :
                        <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <asp:GridView ID="gvChallanDetail" runat="server" AllowPaging="false" AllowSorting="false"
                            AlternatingRowStyle-CssClass="fieldName" AutoGenerateColumns="False" EnableSortingAndPagingCallbacks="True"
                            GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor" HorizontalAlign="Left"
                            RowStyle-CssClass="gridbgcolor" Width="100%" OnPageIndexChanging="gvChallanDetail_PageIndexChanging"
                            OnRowDataBound="gvChallanDetail_RowDataBound">
                            <RowStyle CssClass="gridbgcolor" />
                            <Columns>
                                <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo" ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle Width="40px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Division" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductdivision" runat="server" Text='<%#Eval("ProductDivision") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Complaint No." ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblcomplaint" CommandName="stage" runat="server" CommandArgument='<%#Eval("BASELINEID")%>'
                                            CausesValidation="false" Text='<%#Eval("complaint_no") %>' OnClick="lblcomplaint_Click">LinkButton</asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Claim No" DataField="Claim_No" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Type (M/S)" DataField="Type" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Claim Approver Name" DataField="Claim_Approved_By" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Claim Approved Date" DataField="Approved_Date" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Service/Spare Used" DataField="Services_Spare" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <%--<asp:BoundField HeaderText="Parameter-Possible Value" DataField="Parameter_Possible_Value" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>--%>
                                <asp:TemplateField HeaderText="Parameter-Possible Value" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblParameters" runat="server" Text='<%#Eval("Parameter_Possible_Value") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Quantity" DataField="Quantity" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Rate" DataField="Rate" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Amount" DataField="Amount" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Remarks" DataField="Remarks" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                  <asp:CheckBox ID="chkHeader" runat="server" onclick="javascript:SelectAllCheckboxes(this);" />
                                </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" onclick="javascript:ChildClick(this);" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="MISComplaint_Id" DataField="MISComplaint_Id" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Item_Id" DataField="Item_Id" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                            <EmptyDataTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                            <img alt="" src='<%=ConfigurationManager.AppSettings["SIMSUserMessage"]%>' />
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
                    <td align="center" colspan="5">
                        <asp:Button ID="imgBtnConfirm" runat="server" CssClass="btn" OnClick="imgBtnConfirm_Click"
                            Text="Generate" Width="78px" />
                        <asp:Button ID="ImgBtnCancel" runat="server" CssClass="btn" OnClick="ImgBtnCancel_Click"
                            Text="Cancel" Width="74px" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="5">
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                    </td>
                </tr>
                <tr runat="server" id="tr1">
                    <td colspan="5" class="headingred">
                       <b>Total Number of Records With Month Wise</b> 
                    </td>
                </tr>
                <tr>
                <td colspan="5">
                <asp:GridView ID="grdPendingIBN" runat="server"
                            AlternatingRowStyle-CssClass="fieldName" AutoGenerateColumns="False"
                            GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor" HorizontalAlign="Left"
                            RowStyle-CssClass="gridbgcolor" Width="50%" >
                            <RowStyle CssClass="gridbgcolor" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sno" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="40px" 
                                HeaderStyle-VerticalAlign="Bottom">
                                <ItemTemplate>
                                <%# Container.DataItemIndex+1%>   
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Bottom"
                                DataField="MonthName" HeaderText="Month-Year" />
                                <asp:BoundField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Bottom"
                                DataField="ProductDivision_Desc" HeaderText="Division" />
                                 <asp:BoundField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Bottom"
                                DataField="Total" HeaderText="Total" />
                            </Columns>
                            <EmptyDataTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                            <img alt="" src='<%=ConfigurationManager.AppSettings["SIMSUserMessage"]%>' />
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
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
