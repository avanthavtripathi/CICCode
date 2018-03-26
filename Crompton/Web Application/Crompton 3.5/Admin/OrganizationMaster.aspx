<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="OrganizationMaster.aspx.cs" Inherits="Admin_OrganizationMaster" %>

<asp:Content ID="ContentStateMaster" ContentPlaceHolderID="MainConHolder" runat="Server">
<script language="javascript" type="text/javascript">
window.onload = function()
{ 
   Counter = 0;
}

function ChildClick(CheckBox)
{   

    var  Counter=1;
   var HeaderCheckBox =document.getElementById ("ctl00_MainConHolder_gvDiv_ctl01_chkHeader");
  TotalChkBx =document.getElementById("ctl00_MainConHolder_gvDiv").rows.length;
  
  TotalChkBx=TotalChkBx-1;
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

function SelectAllCheckboxes(spanChk)
{
// Added as ASPX uses SPAN for checkbox
var TotalChkBx =document.getElementById("ctl00_MainConHolder_gvDiv").rows.length;
 var HeaderCheckBox =document.getElementById ("ctl00_MainConHolder_gvDiv_ctl01_chkHeader");
 var childid;
 
 var HeaderChildCheckBox ='ctl00_MainConHolder_gvDiv_ctl' ;

TotalChkBx=TotalChkBx+3;
for(i=0;i< TotalChkBx;i++)
    {
    HeaderChildCheckBox = '';
    HeaderChildCheckBox = 'ctl00_MainConHolder_gvDiv_ctl';
    childid='';
    if (i >= 10)
         {
            HeaderChildCheckBox =HeaderChildCheckBox + i + '_chkChild';
           childid = HeaderChildCheckBox;
         }
         else{
           HeaderChildCheckBox =HeaderChildCheckBox + '0'+ i + '_chkChild';
           childid = HeaderChildCheckBox;
         
         }
         
         if (HeaderCheckBox.checked == true)
         {
             if (document.getElementById(childid) != null)
             {
                document.getElementById(childid).checked = true;  
             }
         }
         else
          {
               if (document.getElementById(childid) != null)
                {
                    document.getElementById(childid).checked = false ; }
                }   
          }
}
    </script>
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Organization Master
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right" style="padding-right: 10px">
                        <asp:RadioButtonList ID="rdoboth" RepeatDirection="Horizontal" RepeatColumns="3"
                            runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdoboth_SelectedIndexChanged">
                            <asp:ListItem Value="1" Text="Active" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="0" Text="In-Active"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Both"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" colspan="2">
                        <table border="0" width="100%">
                            <tr>
                                <td align="left" class="MsgTDCount">
                                    Total Number of Records :
                                    <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    Search For
                                    <asp:DropDownList ID="ddlSearch" runat="server" Width="150px" CssClass="simpletxt1">
                                        <asp:ListItem Text="User Name" Value="Name"></asp:ListItem>
                                        <asp:ListItem Text="Region" Value="region_DESc"></asp:ListItem>
                                        <asp:ListItem Text="Branch" Value="Branch_Name"></asp:ListItem>
                                        <asp:ListItem Text="Product Division" Value="Unit_Desc"></asp:ListItem>
                                        <asp:ListItem Text="Role" Value="RoleName"></asp:ListItem>
                                    </asp:DropDownList>
                                    With
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="txtboxtxt" Width="100px" Text=""></asp:TextBox>
                                    <asp:Button Text="Go" Width="25px" CssClass="btn" ID="imgBtnGo" runat="server" CausesValidation="False"
                                        ValidationGroup="editt" OnClick="imgBtnGo_Click" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0">
                            <tr>
                                <td class="bgcolorcomm">
                                    <asp:GridView ID="gvComm" runat="server" AllowPaging="True" AllowSorting="True" AlternatingRowStyle-CssClass="fieldName"
                                        AutoGenerateColumns="False" DataKeyNames="Organization_SNo" EnableSortingAndPagingCallbacks="True"
                                        GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor" HorizontalAlign="Left"
                                        OnPageIndexChanging="gvComm_PageIndexChanging" OnSelectedIndexChanging="gvComm_SelectedIndexChanging"
                                        OnSorting="gvComm_Sorting" RowStyle-CssClass="gridbgcolor" Width="100%">
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                <HeaderStyle Width="40px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Name" HeaderStyle-HorizontalAlign="Left" HeaderText="User Name"
                                                ItemStyle-HorizontalAlign="Left" SortExpression="Name">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="region_DESc" HeaderStyle-HorizontalAlign="Left" HeaderText="Region"
                                                ItemStyle-HorizontalAlign="Left" SortExpression="region_DESc">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Branch_Name" HeaderStyle-HorizontalAlign="Left" HeaderText="Branch"
                                                ItemStyle-HorizontalAlign="Left" SortExpression="Branch_Name">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <%--Added by Gaurav Garg on 20 OCT 09 for MTO--%>
                                            <asp:BoundField DataField="BusinessLine_Desc" HeaderStyle-HorizontalAlign="Left" HeaderText="Business Line"
                                                ItemStyle-HorizontalAlign="Left" SortExpression="BusinessLine_Desc">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <%--END--%>
                                            
                                            <asp:BoundField DataField="Unit_Desc" HeaderStyle-HorizontalAlign="Left" HeaderText="Product Division"
                                                ItemStyle-HorizontalAlign="Left" SortExpression="Unit_Desc">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="RoleName" HeaderStyle-HorizontalAlign="Left" HeaderText="Role"
                                                ItemStyle-HorizontalAlign="Left" SortExpression="RoleName">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Active_Flag" HeaderStyle-HorizontalAlign="Left" HeaderText="Status"
                                                ItemStyle-HorizontalAlign="Left" SortExpression="Active_Flag">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:CommandField HeaderStyle-Width="60px" HeaderText="Edit" ShowSelectButton="True">
                                                <HeaderStyle Width="60px" />
                                            </asp:CommandField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                        <img alt="" src='<%=ConfigurationManager.AppSettings["UserMessage"]%>' />
                                                        <b>No Record found</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                        <AlternatingRowStyle CssClass="fieldName" />
                                    </asp:GridView>
                                    <!-- Branch Listing -->
                                    <!-- End Branch Listing -->
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td width="100%" align="left" class="bgcolorcomm">
                                    <table width="98%" border="0">
                                        <tr>
                                            <td colspan="2" align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>"
                                                style="height: 17px">
                                                <font color='red'>*</font>
                                                <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:HiddenField ID="hdnOrganizationSNo" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Use Name<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlUserID" runat="server" CssClass="simpletxt1" Width="175px"
                                                    ValidationGroup="editt">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlUserID"
                                                    ErrorMessage="User Name is required." InitialValue="Select" SetFocusOnError="true"
                                                    ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Region Name <font color="red">*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlRegionDesc" runat="server" CssClass="simpletxt1" Width="175px"
                                                    ValidationGroup="editt" AutoPostBack="True" OnSelectedIndexChanged="ddlRegionDesc_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RFRegionDesc" runat="server" ControlToValidate="ddlRegionDesc"
                                                    ErrorMessage="Region Name is required." InitialValue="Select" SetFocusOnError="true"
                                                    ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Branch Name<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlBranchName" runat="server" CssClass="simpletxt1" Width="175px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlBranchName"
                                                    ErrorMessage="Branch Name is required." ValidationGroup="editt" InitialValue="Select"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td width="30%">
                                                Business Line <font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlBusinessLine" runat="server" CssClass="simpletxt1" Width="175px" OnSelectedIndexChanged="ddlBusinessLine_SelectIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlBusinessLine"
                                                    ErrorMessage="Business Line is required." SetFocusOnError="true" ValidationGroup="editt"
                                                    InitialValue="Select"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                       <td colspan="2">
                                     <asp:DataList Width="100%" ID="rptUsersRoleList" runat="server" RepeatDirection="Horizontal" RepeatColumns="5">
                                        <HeaderTemplate>
                                            <b>Roles<font color='red'>*</font></b><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chkRoleCheckBox" AutoPostBack="true" Text='<%# Eval("RoleName") %>' />
                                            <br />
                                        </ItemTemplate>
                                    </asp:DataList>
                                    
                                               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlRole"
                                                    ErrorMessage="Role is required." ValidationGroup="editt" InitialValue="Select"></asp:RequiredFieldValidator>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Product Division<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlProductDivision" runat="server" CssClass="simpletxt1" 
                                                    Width="175px" Enabled="False">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlProductDivision" Enabled="false" 
                                                    ErrorMessage="Product Division is required." ValidationGroup="editt" InitialValue="Select"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            
                                            </td>
                                            <td>
                                                <asp:GridView ID="gvDiv" runat="server" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="false"
                                                                        AllowSorting="True" DataKeyNames="unit_SNo" Width="100%" AutoGenerateColumns="False">
                                                                        <RowStyle CssClass="gridbgcolor" />
                                                                        <Columns>
                                                                            <%--<asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                                        <HeaderStyle Width="40px" />
                                                                    </asp:BoundField>--%>
                                                                            <asp:TemplateField>
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="chkHeader" runat="server" onclick="javascript:SelectAllCheckboxes(this);" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkChild" onclick="javascript:ChildClick(this);" runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                           <asp:TemplateField HeaderText="Product Division">
                                                                                <ItemTemplate>
                                                                                    <asp:HiddenField ID="hdnUnitSno" runat="server" Value='<%#Bind("Unit_SNo")%>' />
                                                                                    <asp:Label ID="lblUnit_Desc" runat="server" Text='<%# Bind("Unit_Desc") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                                        <AlternatingRowStyle CssClass="fieldName" />
                                                                    </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Status
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rdoStatus" RepeatDirection="Horizontal" RepeatColumns="2"
                                                    runat="server">
                                                    <asp:ListItem Value="1" Text="Active" Selected="True">
                                                    </asp:ListItem>
                                                    <asp:ListItem Value="0" Text="In-Active">
                                                    </asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="25" align="left">
                                                &nbsp;
                                            </td>
                                            <td>
                                                <!-- For button portion update -->
                                                <table>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Button Text="Add" Width="70px" CssClass="btn" ID="imgBtnAdd" runat="server"
                                                                CausesValidation="True" ValidationGroup="editt" OnClick="imgBtnAdd_Click" />
                                                            <asp:Button Text="Save" Width="70px" ID="imgBtnUpdate" CssClass="btn" runat="server"
                                                                CausesValidation="True" ValidationGroup="editt" OnClick="imgBtnUpdate_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="imgBtnCancel" Width="70px" runat="server" CausesValidation="false"
                                                                CssClass="btn" Text="Cancel" OnClick="imgBtnCancel_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <!-- For button portion update end -->
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
