<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true"
    CodeFile="ClaimApprovalFan.aspx.cs" Inherits="Reports_ClaimApprovalFan" Title="Untitled Page"
    EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script src="../../scripts/jquery-1.11.3.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
function funRepeatedComplaints(SapProductCode,CmpLogDate) 
{
 
     var SC_Sno = document.getElementById("<%=ddlServicecontractorSearch.ClientID %>").value;;
    var strUrl = '90DaysRepeatedComplaints.aspx?PSN=' + SapProductCode + '&CmpLogDate=' + CmpLogDate + '&SCNo=' + SC_Sno;
    window.open(strUrl, 'History', 'height=450,width=850,left=20,top=30,Location=0');
}
function Openpopup(popurl)
 {
   winpops = window.open(popurl,"","width=1000, height=600, left=45, top=15, scrollbars=yes, menubar=no,resizable=no,directories=no,location=center")
 }

    </script>

    <script type="text/javascript">
   
   
        function CheckAllEmp(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=gvSummary.ClientID %>");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
               if(GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].disabled == false){
                
                GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
            }
        } 

   function checkRejection()
   {
      
    
     if (confirm("selected complaints are going to be rejected.") == true) {
     // txt = "You pressed OK!";
    return true;
} else {
    //txt = "You pressed Cancel!";
    return false;
}
   
   }
   
   
   
   
    function Validate() {
        var ddlYear = document.getElementById('<%= ddlYear.ClientID %>');
        var ddlMonth = document.getElementById('<%= ddlMonth.ClientID %>');
        var ddlTime=document.getElementById('<%=ddlTime.ClientID%>');
        var ddlmaxTime=document.getElementById('<%=ddlmaxtime.ClientID %>');
       if ( ddlmaxTime.value <= ddlTime.value)
       {
       alert("Min hour selection can not be greater than max time.");
        return false;
       }
       
        if (ddlYear.value == "0") {
            alert("Please select Year!");
            return false;
        }
        if (ddlMonth.value == "0") {
            alert("Please select Month!");
            return false;
        }
        return true;
    }
    </script>

    <table width="100%" border="0">
        <tr>
            <td class="headingred">
                Claim Approval For Fan
            </td>
            <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </td>
        </tr>
        <tr>
            <td class="bgcolorcomm" colspan="2">
                <asp:UpdatePanel ID="pnls" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <center>
                            <table border="0" cellpadding="3" cellspacing="0" style="width: 100%; margin-left: 0px">
                                <tr id="trBranchAsc" runat="server">
                                    <td style="width: 7%;">
                                        Region :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="True" CssClass="simpletxt1"
                                            OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" Style="min-width: 100px;">
                                            <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="East" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="West" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="North" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="South" Value="7"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 7%;">
                                        Branch :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlBranchSearch" runat="server" CssClass="simpletxt1" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlBranchSearch_SelectedIndexChanged" Style="min-width: 97px;">
                                            <asp:ListItem Text="select" Value="">Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 18%;">
                                        Service Contractor :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlServicecontractorSearch" CssClass="simpletxt1" runat="server"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlServicecontractorSearch_SelectedIndexChanged"
                                            Style="min-width: 291px;">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Year <span style="color: Red;">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlYear" runat="server" Width="100px" CssClass="simpletxt1">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="2015">2015</asp:ListItem>
                                            <asp:ListItem Value="2016">2016</asp:ListItem>
                                            <asp:ListItem Value="2017">2017</asp:ListItem>
                                            <asp:ListItem Value="2018">2018</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        Month <span style="color: Red;">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlMonth" runat="server" Width="100px" CssClass="simpletxt1">
                                           <%-- <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">January</asp:ListItem>
                                            <asp:ListItem Value="2">February</asp:ListItem>
                                            <asp:ListItem Value="3">March</asp:ListItem>
                                            <asp:ListItem Value="4">April</asp:ListItem>
                                            <asp:ListItem Value="5">May</asp:ListItem>
                                            <asp:ListItem Value="6">June</asp:ListItem>
                                            <asp:ListItem Value="7">July</asp:ListItem>
                                            <asp:ListItem Value="8">August</asp:ListItem>
                                            <asp:ListItem Value="9">September</asp:ListItem>
                                            <asp:ListItem Value="10">October</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">December</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="display: none;">
                                        Branch Wise
                                    </td>
                                    <td style="display: none;">
                                        <asp:CheckBox ID="ChkIsBranch" runat="server" />
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td>
                                        Min Hour<span style="color: Red;">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTime" runat="server" Width="100px" CssClass="simpletxt1">
                                            <asp:ListItem Value="0">0</asp:ListItem>
                                            <asp:ListItem Value="18">18</asp:ListItem>
                                            <asp:ListItem Value="24">24</asp:ListItem>
                                            <asp:ListItem Value="72">72</asp:ListItem>
                                            <asp:ListItem Value="120">120</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        Max Hour<span style="color: Red;">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlmaxtime" runat="server" Width="100px" CssClass="simpletxt1">
                                            <asp:ListItem Value="18">18</asp:ListItem>
                                            <asp:ListItem Value="24">24</asp:ListItem>
                                            <asp:ListItem Value="72">72</asp:ListItem>
                                            <asp:ListItem Value="120">120</asp:ListItem>
                                            <asp:ListItem Value="M">More Than 120</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" style="padding-bottom: 10px;">
                                        <asp:Button ID="BtnSEARCH" runat="server" Text="SEARCH" OnClientClick="return Validate()"
                                            CssClass="btn" Width="100px" OnClick="BtnSEARCH_Click" />&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnsummary" runat="server" Visible="false" Text="Download Summary Report"
                                            CssClass="btn" Width="150px" OnClick="btnsummary_Click" />
                                    </td>
                                </tr>
                            </table>
                        </center>
                        <asp:HiddenField ID="SelectedGridCellIndex" runat="server" Value="-1" />
                        <asp:HiddenField ID="hdnIndex" runat="server" Value="0" />
                        <table width="100%" border="0" cellpadding="1" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:GridView ID="GrdReport" Width="100%" runat="server" AutoGenerateColumns="true"
                                        OnRowDataBound="GrdReport_RowDataBound" Style="text-align: center;" OnRowCreated="GrdReport_RowCreated"
                                        OnSelectedIndexChanged="GrdReport_SelectedIndexChanged1">
                                        <RowStyle CssClass="gridbgcolor" />
                                        <EmptyDataTemplate>
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                        <img src="<%=ConfigurationManager.AppSettings["UserMessage"]%>" alt="" />
                                                        <b>No Record found</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <HeaderStyle CssClass="fieldNamewithbgcolor" HorizontalAlign="Center" />
                                        <AlternatingRowStyle CssClass="fieldName" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <%-- <tr><td style="padding-top:10px;">
                                    SLA for Calls registered on Sunday and after 3 pm on Saturday should be counted from Monday. 
                                   <br />Last Two Days Calls at the month end should be counted from the 1<sup>st</sup> working day of the next month.
                                  <br /><br />*Based on Log date
                            </td></tr>--%>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <div style="float: right; margin-top: 2%;">
                            <asp:Button ID="btnExport" Visible="false" runat="server" Text="EXPORT TO EXCEL"
                                CssClass="btn" Width="120px" OnClick="btnExport_Click" />
                        </div>
                        <table width="100%" border="0" cellpadding="1" cellspacing="0" style="margin-top: 4%;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblheader" runat="server" CssClass="headingred"></asp:Label>
                                </td>
                                <td colspan="" style="float: right;">
                                    <div id="crumb" runat="server" visible="False">
                                        <div style="float: left; margin: 5px;">
                                            <span style="width: 16px; height: 11px; background-color: #9ae4d4; display: inline-block;">
                                            </span>Approved</div>
                                        <div style="float: left; margin: 5px;">
                                            <span style="width: 16px; height: 11px; background-color: #e49a9a; display: inline-block;">
                                            </span>Rejected</div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="gvSummary" Width="100%"
                                       runat="server" AutoGenerateColumns="false" AllowSorting="True"
                                        Style="text-align: center;" OnRowDataBound="gvSummary_RowDataBound1" 
                                        OnSorting="gvSummary_Sorting">
                                      
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="SNo" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="SNo">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <%--   
                                         <asp:BoundField DataField="ComplaintNo" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderText="ComplaintNo">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:BoundField>--%>
                                            <asp:BoundField DataField="Name" SortExpression="Name" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Name">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="City" SortExpression="City" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Address">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Contact" SortExpression="Contact" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Contact">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Complaint No." SortExpression="ComplaintNo">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkcomplaint" runat="server" CommandArgument='<%#Eval("ComplaintNo")%>'
                                                        CausesValidation="false" CommandName="stage" Text='<%#Eval("ComplaintNo")%>'
                                                        OnClick="lnkcomplaint_Click"> </asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ComplaintType" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="ComplaintType">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <%--for local to outstation--%>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Local/outsation">
                                                <ItemTemplate>
                                                    <asp:DropDownList runat="server" ID="ComplaintTypes">
                                                        <asp:ListItem Selected="True" Text="Selected" Value=""></asp:ListItem>
                                                        <asp:ListItem Text="Local" Value="L"></asp:ListItem>
                                                        <asp:ListItem Text="OutStation" Value="O"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblamount" runat="server" Text="0.00"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Repetitive Complaint">
                                                <ItemTemplate>
                                                    <a href="javascript:void(0);" id="dfh" onclick="funRepeatedComplaints('<%#Eval("SapProductCode")%>','<%#Eval("Loggeddate")%>')">
                                                        <%#Eval("SapProductCode")%>
                                                    </a>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ASCComment" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="ASC Comment">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Rejection reason">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="Hdncheck" Value='<%#Eval("IsApproved") %>' runat="server" />
                                                    <%--     <asp:HiddenField ID="hdnIsReApprovalSent" Value='<%#Eval("IsReApprovalSent") %>' runat="server" />--%>
                                                    <asp:TextBox ID="txtreason" runat="server" Height="20px" Text='<%#Eval("RejectRemark") %>'
                                                        TextMode="MultiLine"></asp:TextBox>
                                                       <asp:HiddenField ID="HdnWIPEndDate" Value='<%#Eval("WIPEndDate") %>' runat="server" />  
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            
                                         
                                            
                                        </Columns>
                                        <HeaderStyle CssClass="fieldNamewithbgcolor" HorizontalAlign="Center" />
                                        <AlternatingRowStyle CssClass="fieldName" />
                                    </asp:GridView>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <%-- <tr><td style="padding-top:10px;">
                                    SLA for Calls registered on Sunday and after 3 pm on Saturday should be counted from Monday. 
                                   <br />Last Two Days Calls at the month end should be counted from the 1<sup>st</sup> working day of the next month.
                                  <br /><br />*Based on Log date
                            </td></tr>--%>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblsummry" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table style="margin-left: 37%; margin-top: 2%;">
                            <tr align="center">
                                <td align="center">
                                    <asp:Button ID="imgBtnApprove" Width="70px" runat="server" CausesValidation="false"
                                        CssClass="btn" Text="Approve" OnClick="imgBtnApprove_Click" />
                                    <asp:Button Text="Reject" Width="70px" ID="imgBtnReject" CssClass="btn" runat="server"
                                        CausesValidation="True" ValidationGroup="editt" OnClientClick="return checkRejection()"
                                        OnClick="imgBtnReject_Click" />
                                </td>
                                <td align="center">
                                    <asp:Button Text="Close" Width="70px" ID="imgBtnClose" CssClass="btn" runat="server"
                                        CausesValidation="True" OnClientClick="javascript:window.close();" ValidationGroup="editt"
                                        Visible="true" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnExport" />
                        <asp:PostBackTrigger ControlID="btnsummary" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
