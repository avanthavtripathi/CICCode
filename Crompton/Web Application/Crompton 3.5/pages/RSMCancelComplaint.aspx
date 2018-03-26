<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="RSMCancelComplaint.aspx.cs" Inherits="pages_RSMCancelComplaint" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <link href="../css/Popup.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">

        function CheckUncheckAll(evt) {
            var grd = document.getElementById("<%=gvDetails.ClientID %>");
            var input = grd.getElementsByTagName('input');

            if (evt.id.indexOf('chkHeader') >= 0) {
                for (var i = 0; i < input.length; i++) {
                    if (input[i].type == "checkbox" && input[i].id != evt.id) {
                        input[i].checked = evt.checked;
                    }
                }
            }
            else if (evt.id.indexOf('chkChild') >= 0) {
                var count = 0;
                for (var i = 0; i < input.length; i++) {
                    if (input[i].type == "checkbox" && input[i].id.indexOf('chkHeader') < 0 && input[i].checked) {
                        count = count + 1;
                    }
                }
                var chkHd = input[0].checked = count == input.length - 1;
            }
        }

        function ValidateGridCheckbox(evt) {            
            var bitFlag = false;            
                var grd = document.getElementById("<%=gvDetails.ClientID %>");
                var input = grd.getElementsByTagName('input');
                for (var i = 0; i < input.length; i++) {
                    if (input[i].type == "checkbox" && input[i].id.indexOf('chkHeader') < 0 && input[i].checked) {
                        bitFlag = true;
                    }
                }
                if (bitFlag) {
                    if (evt.id.indexOf("btnSubmit")>0)
                        return confirm('Are you sure want to apporve selected complaint/s.');
                    else if (evt.id.indexOf("btnCancel")>0)
                        return confirm('Are you sure want to cancel selected complaint/s.');
                }
                else {
                    alert("Please select atleast one complaint for action.");
                    return false;
                }
           }
    </script>
    <asp:UpdatePanel ID="pnlFirst" runat="server">
        <ContentTemplate>
            <table width="100%" class="bgcolorcomm" align="right">
                <tr>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;
                        width: 30%" colspan="4">
                        <asp:UpdateProgress AssociatedUpdatePanelID="pnlFirst" ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td class="la1">
                        Region : &nbsp;
                    </td>
                    <td class="ra1">
                        <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="true" 
                            CssClass="simpletxt1 dlw" 
                            OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Text="All" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RfvddlInitAction" runat="server" 
                            ControlToValidate="ddlRegion" Display="None" ErrorMessage="Select Region" 
                            InitialValue="0" ValidationGroup="init" />
                    </td>
                    <td class="la1">
                        Branch : &nbsp;
                    </td>
                    <td class="ra1">
                        <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="true" 
                            CssClass="simpletxt1 dlw" 
                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Text="All" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="ddlBranch" Display="None" ErrorMessage="Select Branch" 
                            InitialValue="0" ValidationGroup="init" />
                    </td>
                </tr>
                <tr>
                    <td class="la1">
                        Service Contractor : &nbsp;
                    </td>
                    <td class="ra1">
                        <asp:DropDownList ID="ddlAsc" runat="server" CssClass="simpletxt1 dlw">
                            <asp:ListItem Text="All" Value="0" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlAsc"
                            InitialValue="0" ErrorMessage="Select Asc" Display="None" ValidationGroup="init" />
                    </td>
                    <td class="la1">
                        Division : &nbsp;
                    </td>
                    <td class="ra1">
                        <asp:DropDownList ID="ddlProductDivision" runat="server" CssClass="simpletxt1 dlw">
                            <asp:ListItem Text="All" Value="0" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlProductDivision"
                            InitialValue="0" ErrorMessage="Select Division" Display="None" ValidationGroup="init" />
                    </td>
                </tr>
                <tr>
                    <td class="la1">
                        Date From :<span style="color: Red;">*</span>
                    </td>
                    <td class="ra1">
                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="simpletxt1"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txtFromDate" runat="server">
                        </cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ID="reqFromDate" runat="server" ControlToValidate="txtFromDate"
                            ValidationGroup="a" ErrorMessage="*" ForeColor="Red"> </asp:RequiredFieldValidator>
                    </td>
                    <td class="la1">
                        Date To :<span style="color: Red;">*</span>
                    </td>
                    <td class="ra1">
                        <asp:TextBox ID="txtToDate" runat="server" CssClass="simpletxt1"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtToDate" runat="server">
                        </cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ID="reqToDate" runat="server" ValidationGroup="a" ControlToValidate="txtToDate"
                            ErrorMessage="*" ForeColor="Red"> </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                         <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="a" CssClass="btn"
                            OnClick="btnSearch_Click" />
                    </td>
                </tr>
                <tr>
                <td colspan="2">
                <b>Total Complaint :</b> <asp:Label ID="lblTotalComplaintCount" Font-Bold="true" ForeColor="Red" runat="server" Text="0" ></asp:Label>
                </td>
                    <td colspan="2" align="center">
                       &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:GridView ID="gvDetails" runat="server" AlternatingRowStyle-CssClass="fieldName"
                            AutoGenerateColumns="False" DataKeyNames="BaseLineId" GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor"
                            Width="100%" RowStyle-CssClass="gridbgcolor" AllowSorting="true" 
                            AllowPaging="true" PageSize="20" onsorting="gvDetails_Sorting" 
                            onpageindexchanging="gvDetails_PageIndexChanging">
                            <RowStyle CssClass="gridbgcolor" />
                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkHeader" Text="All/None" TextAlign="Right" runat="server" onclick="javascript:CheckUncheckAll(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkChild" runat="server" onclick="javascript:CheckUncheckAll(this);" />
                                        <asp:HiddenField ID="hdnId" runat="server" Value='<%#Bind("Id")%>' />
                                        <asp:HiddenField ID="hdnBaselineid" runat="server" Value='<%# Bind("BaseLineId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Complaint No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" SortExpression="Complaint_RefNo">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hndComplaintNo" runat="server" Value='<%#Eval("Complaint_RefNo")%>' />
                                        <asp:HiddenField ID="hdnSplitNo" runat="server" Value='<%#Eval("split")%>' />
                                        <asp:HiddenField ID="hdnAscName" runat="server" Value='<%#Eval("CreatedBy")%>' />
                                        <a href="Javascript:void(0);" onclick="funCommonPopUp(<%#Eval("BaseLineId")%>)">
                                            <%#Eval("Complaint_RefNo")%>/<%#Eval("split")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Product Division" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" DataField="ProductDivision" SortExpression="ProductDivision" />
                                    <asp:BoundField HeaderText="Branch" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" DataField="Branch_Name" SortExpression="Branch_Name" />
                                <asp:BoundField HeaderText="Service Contractor" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" DataField="SC_Name" SortExpression="SC_Name" />
                                    <asp:BoundField HeaderText="ASC Comment" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" DataField="SCComment"  SortExpression="SC_Name"/>                                
                                <asp:TemplateField HeaderText="Comment" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtComment" TextMode="MultiLine" runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="fieldNamewithbgcolor" />
                            <AlternatingRowStyle CssClass="fieldName" />
                            <EmptyDataTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                            <b>No Records Found..!</b>
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        &nbsp;
                    </td>
                </tr>
                <tr id="trSubmit" runat="server" visible="false">
                    <td colspan="4" align="center">
                        <asp:Button ID="btnSubmit" runat="server" Text="Approved" CssClass="btn" OnClientClick="javascript:return ValidateGridCheckbox(this) " OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Reject" CssClass="btn" OnClientClick="javascript:return ValidateGridCheckbox(this)" OnClick="btnCancel_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="4">
                        <span><b>Note : </b>Please select earliest date range for more complaints.</span></td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
