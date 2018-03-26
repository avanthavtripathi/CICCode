<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="MaterialTransaction.aspx.cs" Inherits="pages_MaterialTransaction" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">



 <script  language="javascript" type="text/javascript">
    var TotalChkBx;
    var Counter;

    window.onload = function()
    {
       //Get total no. of CheckBoxes in side the GridView.
       TotalChkBx = parseInt('<%= this.gvFresh.Rows.Count %>');

       //Get total no. of checked CheckBoxes in side the GridView.
       Counter = 0;
    }

    function HeaderClick(CheckBox)
    {
       //Get target base & child control.
       var TargetBaseControl = 
           document.getElementById('<%= this.gvFresh.ClientID %>');
       var TargetChildControl = "chkBxSelect";

       //Get all the control of the type INPUT in the base control.
       var Inputs = TargetBaseControl.getElementsByTagName("input");

       //Checked/Unchecked all the checkBoxes in side the GridView.
       for(var n = 0; n < Inputs.length; ++n)
          if(Inputs[n].type == 'checkbox' && 
                    Inputs[n].id.indexOf(TargetChildControl,0) >= 0)
             Inputs[n].checked = CheckBox.checked;

       //Reset Counter
       Counter = CheckBox.checked ? TotalChkBx : 0;
    }

    function ChildClick(CheckBox, HCheckBox)
    {
       //get target control.
       var HeaderCheckBox = document.getElementById(HCheckBox);

       //Modifiy Counter; 
       if(CheckBox.checked && Counter < TotalChkBx)
          Counter++;
       else if(Counter > 0) 
          Counter--;

       //Change state of the header CheckBox.
       if(Counter < TotalChkBx)
          HeaderCheckBox.checked = false;
       else if(Counter == TotalChkBx)
          HeaderCheckBox.checked = true;
    }
    </script>
<script type="text/javascript" language="javascript">
     function funLandmark(CRefNo,bid)
                {
                    var strUrl='ComplainRefNo.aspx?baseLineId='+ bid;
                       compWin= window.open(strUrl,'SCPopup','height=550,width=750,left=20,top=30');
                        if (window.focus) {compWin.focus()}
                 } 
                 
     function funCustomerMaster(cid,CRefNo)
                {
                    var strUrl='CustomerDetails.aspx?cid='+ cid +'&CompNo='+ CRefNo;
                     custWin=   window.open(strUrl,'SCPopup','height=550,width=750,left=20,top=30,location=0 ');
                        if (window.focus) {custWin.focus()}
                 }  
                 
         function funCommLog(compNo,splitNo)
        {
            var strUrl='CommunicationLog.aspx?CompNo='+ compNo + '&SplitNo='+ splitNo;
            logWin= window.open(strUrl,'CommunicationLog','height=550,width=750,left=20,top=30');
            if (window.focus) {logWin.focus()}
        }
        function funHistoryLog(compNo,splitNo)
        {
            var strUrl='HistoryLog.aspx?CompNo='+ compNo + '&SplitNo='+ splitNo;
           hisWin= window.open(strUrl,'History','height=550,width=750,left=20,top=30');
            if (window.focus) {hisWin.focus()}
        }
         function funSCDetail(SCNo)
        {
            var strUrl='../Pages/SCPopup.aspx?scno='+ SCNo + '&type=display';
            window.open(strUrl,'History','height=550,width=750,left=20,top=30,Location=0,scrollbars=1');
        }
    </script>
 
 <asp:UpdatePanel ID="updateSC" runat="server">
        <ContentTemplate>
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="headingred" style="width:30%">
                        Material Transaction
                    </td>
                    <td align="center" class="headingred" style="width:40%">
                        
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;" style="width:30%">
                        <asp:UpdateProgress AssociatedUpdatePanelID="updateSC" ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                            </ProgressTemplate>
                        </asp:UpdateProgress></td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" class="bgcolorcomm" colspan="3" >
                        <table width="100%" border="0">
                            <tr>
                                <td align="left" width="20%">Stage:</td>
                                <td align="left" width="30%">WIP</td>
                                <td align="left" width="20%">City:</td>
                                <td align="left" width="30%">
                                    <asp:DropDownList ID="ddlCity" runat="server"  CssClass="simpletxt1"
                                        Width="175" >
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">Status:</td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlStatus" CssClass="simpletxt1" Width="200" runat="server">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="25">Pending for Replacement</asp:ListItem>
                                        <asp:ListItem Value="8">Waiting for spare</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="left">Product Division:</td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlProductDivision" runat="server" 
                                        CssClass="simpletxt1" Width="175">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Service Contractor
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlSerContractor" runat="server" Width="225px" CssClass="simpletxt1"
                                        ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <%--<tr>
                                <td align="left">Complaint Ref. No.:</td>
                                <td align="left">
                                    <asp:TextBox ID="txtComplaintRefNo" Width="175" runat="server" CssClass="txtboxtxt"></asp:TextBox>
                                </td>
                            </tr>--%>
                            <tr>
                                <td align="left" colspan="4" height="10px">
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                </td>
                                <td align="center" colspan="2">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn"  OnClick="btnSearch_Click"
                                    CausesValidation="true" Text="Search" Width="70px" />
                                </td>
                                <td align="left">
                                </td>
                            </tr>
                            <tr id="TrGrid" runat="server">
                                <td align="center" colspan="4">
                                    <asp:GridView ID="gvFresh" runat="server" AllowPaging="True" AllowSorting="True"
                                        AlternatingRowStyle-CssClass="fieldName" AutoGenerateColumns="False" DataKeyNames="BaseLineId"
                                        GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor" Width="100%" RowStyle-CssClass="gridbgcolor"
                                        OnPageIndexChanging="gvFresh_PageIndexChanging" >
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkBxSelect" runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkBxHeader" onclick="javascript:HeaderClick(this);" runat="server" />
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="SNo" HeaderText="Sno." />
                                            <asp:TemplateField HeaderText="Complaint RefNo" 
                                            HeaderStyle-HorizontalAlign="Left"
                                             ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnBaselineID" runat="server" Value='<%#Eval("BaseLineId")%>' />
                                                    <asp:HiddenField ID="hdnComplaint_RefNo" runat="server" Value='<%#Eval("Complaint_RefNo")%>' />
                                                    <asp:HiddenField ID="hdnAppointmentFlag" runat="server" Value='<%#Eval("AppointmentReq")%>' />
                                                    <asp:HiddenField ID="gvFreshhdnCallStatus" runat="server" Value='<%#Eval("CallStatus")%>' />
                                                    <a href="Javascript:void(0);" onclick="funCommonPopUp(<%#Eval("BaseLineId")%>)">
                                                        <%#Eval("Complaint_RefNo")%>/<%#Eval("split")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name"  HeaderStyle-HorizontalAlign="Left"
                                             ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <a href="Javascript:void(0);" onclick="funCustomerMaster('<%#Eval("CustomerId")%>','<%#Eval("Complaint_RefNo")%>')">
                                                        <%#Eval("CustName")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="Address"  HeaderStyle-HorizontalAlign="Left"
                                             ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%#Eval("Address1")%>
                                                    <%#Eval("Address2")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                HeaderText="Service Contractor">
                                                <ItemTemplate>
                                                    <a href="Javascript:void(0);" onclick="funSCDetail('<%#Eval("SC_SNo")%>')">
                                                        <%#Eval("SC_Name")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contact No."  HeaderStyle-HorizontalAlign="Left"
                                             ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%#Eval("UniqueContact_No")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product"  HeaderStyle-HorizontalAlign="Left"
                                             ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%#Eval("Unit_Desc")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="Nature Of Complaint"  HeaderStyle-HorizontalAlign="Left"
                                             ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%#Eval("NatureOfComplaint")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Comm. History"  HeaderStyle-HorizontalAlign="Left"
                                             ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                   <a href="Javascript:void(0);" 
                                                   onclick="funCommLog('<%#Eval("Complaint_RefNo")%>',<%#Eval("SplitComplaint_RefNo")%>)">
                                                        <%#Eval("LastComment")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Stage"  HeaderStyle-HorizontalAlign="Left"
                                             ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <a href="Javascript:void(0);" onclick="funHistoryLog('<%#Eval("Complaint_RefNo")%>',<%#Eval("SplitComplaint_RefNo")%>)">
                                                        <%#Eval("CallStage")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                        <AlternatingRowStyle CssClass="fieldName" />
                                        <EmptyDataTemplate>
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                        <b>No Complains found</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table width="100%" class="bgcolorcomm" cellpadding="3" id="tbIntialization" runat="server"
                            visible="false" border="0">
                            <tr>
                                <td colspan="3" height="20" align="left" valign="bottom" style="border-bottom: 1px solid #396870"></td>
                            </tr>
                            <tr>
                                <td style="width: 30%" height="30" align="left">Action:
                                    &nbsp;<asp:TextBox ID="TextBox1" runat="server" Width="200" Text="Material Issued" ReadOnly="true" CssClass="txtboxtxt"></asp:TextBox>
                                </td>
                                <td style="width: 30%" height="30" align="left">GRC Number:
                                    <asp:TextBox ID="txtGRCNo" runat="server" Width="200" CssClass="txtboxtxt" MaxLength="15"></asp:TextBox>
                                        <asp:Label ID="lblError" runat="server" ForeColor="Red" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%" height="30" align="left" valign="middle">Remark:
                                    <asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server" Width="190" Height="50" CssClass="txtboxtxt"></asp:TextBox>
                                </td>
                                <%--<td style="width: 30%" height="30" align="left">GRC Number:
                                    <asp:TextBox ID="TextBox3" runat="server" Width="200" CssClass="txtboxtxt" MaxLength="15"></asp:TextBox>
                                        <asp:Label ID="Label1" runat="server" ForeColor="Red" ></asp:Label>
                                </td>--%>
                            </tr>
                            <tr>
                                <td colspan="3" height="10" align="left" valign="bottom" style="border-top: 1px solid #396870">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%" align="center" colspan="3">
                                <asp:Button ID="btnSave" runat="server" Width="70px" Text="Save" CssClass="btn"
                                        CausesValidation="true"  OnClick="btnSave_Click" />
                                    <asp:Button ID="btnClose" runat="server" Width="70px" Text="Close" CausesValidation="false" 
                                        CssClass="btn" onclick="btnClose_Click" />
                                </td>
                            </tr>
                        </table>
                        <br />
                    </td>
                </tr>
                <tr align="center">
                    <td align="center" colspan="3">
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

