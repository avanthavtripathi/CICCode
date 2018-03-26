<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="RoutingMaster.aspx.cs" Inherits="Admin_RoutingMaster" %>

<asp:Content ID="ContentStateMaster" ContentPlaceHolderID="MainConHolder" runat="Server">

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
  // Check that Routing is selected or not
function validateGvTerritory()
{        
        var  count=0;
        var grdtr =document.getElementById("ctl00_MainConHolder_gvTerritory");
        var inputelements = grdtr.getElementsByTagName('input');
        for (var i = 0 ; i < inputelements.length ; i++) 
        {
            if(inputelements[i].type=="checkbox" && inputelements[i].checked==true)
            {
                count=count+1
            }
        }       
        if(count>0)
        {
            return true;            
        }
        else
        { 
            alert("please select at-least one row for rout mapping.");
            return false;
        }
}  
  
  function ChildClick(CheckBox)
{   

    var  Counter=1;
   var HeaderCheckBox =document.getElementById ("ctl00_MainConHolder_gvTerritory_ctl01_chkHeader");
  TotalChkBx =document.getElementById("ctl00_MainConHolder_gvTerritory").rows.length;
  
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
var TotalChkBx =document.getElementById("ctl00_MainConHolder_gvTerritory").rows.length;
 var HeaderCheckBox =document.getElementById ("ctl00_MainConHolder_gvTerritory_ctl01_chkHeader");
 var childid;
 
 var HeaderChildCheckBox ='ctl00_MainConHolder_gvTerritory_ctl' ;

TotalChkBx=TotalChkBx+3;
for(i=0;i< TotalChkBx;i++)
    {
    HeaderChildCheckBox = '';
    HeaderChildCheckBox = 'ctl00_MainConHolder_gvTerritory_ctl';
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

    <script language="javascript" type="text/javascript">
window.onload = function()
{   //Get total no. of CheckBoxes in side the GridView.
    //Get total no. of checked CheckBoxes in side the GridView.
   Counter1 = 0;
   Counter2=0;
   //$get('ctl00_MainConHolder_chkSMS').style.display='none';
   //$get('ctl00_MainConHolder_lblSMS').style.display='none';
}
function SetCounter1()
  {
        Counter1=0;
  }
  
  function ChildClick1(CheckBox)
{   
   var HeaderCheckBox1 =document.getElementById ("ctl00_MainConHolder_gvComm_ctl01_chkHeader1");
 var TotalChkBx1 =document.getElementById("ctl00_MainConHolder_gvComm").rows.length;
  
 var TotalChkBx1=TotalChkBx1-2;
  //alert(TotalChkBx);
   if(CheckBox.checked && Counter1 < TotalChkBx1)
     { 
        Counter1++;
     }
    else if(CheckBox.checked && Counter1 == 0)
      { Counter1++;}
    else if(Counter1 > 0) 
     {Counter1--;}
     //alert("counter" + Counter);
     if(Counter1 < TotalChkBx1)
    {
       HeaderCheckBox1.checked = false;
    }   
   else if(Counter1 == TotalChkBx1)
    {
       HeaderCheckBox1.checked = true;   
    }
}

function SelectAllCheckboxes1(spanChk1)
{
// Added as ASPX uses SPAN for checkbox
var TotalChkBx =document.getElementById("ctl00_MainConHolder_gvComm").rows.length;
var HeaderCheckBox =document.getElementById ("ctl00_MainConHolder_gvComm_ctl01_chkHeader1");
var childid;
 
 var HeaderChildCheckBox ='ctl00_MainConHolder_gvComm_ctl' ;

TotalChkBx=TotalChkBx+3;
for(i=0;i< TotalChkBx;i++)
    {
    HeaderChildCheckBox = '';
    HeaderChildCheckBox = 'ctl00_MainConHolder_gvComm_ctl';
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
                        Routing Master
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="updatepnl" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right" style="padding-right: 10px">
                        <asp:RadioButtonList ID="rdoboth" RepeatDirection="Horizontal" RepeatColumns="3"
                            runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdoboth_SelectedIndexChanged">
                            <asp:ListItem Value="1" Text="Active" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="0" Text="In-Active"></asp:ListItem>
                           <%-- <asp:ListItem Value="2" Text="Both"></asp:ListItem>--%>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" colspan="2">
                        <table border="0" width="100%">
                            <tr>
                                <td align="left" class="MsgTDCount">
                                    Total Number of Records :
                                    <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"> </asp:Label>
                                </td>
                                <td align="right">
                                    Search For
                                    <asp:DropDownList ID="ddlSearch" ValidationGroup="search" runat="server" Width="130px"
                                        CssClass="simpletxt1">
                                        <asp:ListItem Text="SC. Name" Value="SC_Name"></asp:ListItem>
                                        <asp:ListItem Text="SC. Address" Value="SCM.Address1"></asp:ListItem>
                                        <asp:ListItem Text="State" Value="State_Desc"></asp:ListItem>
                                        <asp:ListItem Text="City" Value="City_Desc"></asp:ListItem>
                                        <asp:ListItem Text="Description" Value="Territory_Desc"></asp:ListItem>
                                        <asp:ListItem Text="Product Division" Value="Unit_Desc"></asp:ListItem>
                                        <asp:ListItem Text="Product Line" Value="ProductLine_Desc"></asp:ListItem>
                                        <%--<asp:ListItem Text="Status" Value="Active_Flag"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                    With
                                    <asp:TextBox ID="txtSearch" ValidationGroup="search" runat="server" CssClass="txtboxtxt"
                                        Width="100px" Text="" TabIndex="1"></asp:TextBox>
                                    <asp:Button Text="Go" Width="25px" CssClass="btn" ValidationGroup="search" ID="imgBtnGo"
                                        runat="server" CausesValidation="False" OnClick="imgBtnGo_Click" TabIndex="3" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td align="right">
                                    &nbsp;Product Division With
                                    <asp:TextBox ID="txtProductDivSearch" runat="server" CssClass="txtboxtxt" Width="100px"> </asp:TextBox>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td align="right">
                                    &nbsp;Territory With
                                    <asp:TextBox ID="txtTerritorySearch" runat="server" CssClass="txtboxtxt" Width="100px"> </asp:TextBox> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                  
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                               <td align="right">
                                    &nbsp;City With
                                    <asp:TextBox ID="txtCitySearch" runat="server" CssClass="txtboxtxt" Width="100px"> </asp:TextBox> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0">
                            <tr>
                                <td class="bgcolorcomm">
                                    <!-- Country Listing -->
                                    <asp:GridView PageSize="50" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" CellPadding="2" 
                                        AllowSorting="True" DataKeykNames="Routing_Sno" AutoGenerateColumns="False" ID="gvComm"
                                        runat="server" Width="100%" OnSelectedIndexChanging="gvComm_SelectedIndexChanging"
                                        HorizontalAlign="Left" OnSorting="gvComm_Sorting">
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkHeader1" runat="server" onclick="javascript:SelectAllCheckboxes1(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkChild" runat="server" onclick="javascript:ChildClick1(this);" />
                                                    <asp:HiddenField ID="hdnRouting_Sno" Value='<%#Eval("Routing_Sno") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="RowNumber"  HeaderText="SNo">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SC_Name" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="SC.Name" SortExpression="SC_Name">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Address1" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="SC. Address" SortExpression="Address1">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="State_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="State" SortExpression="State_Desc">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="City_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="City" SortExpression="City_Desc">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Territory_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Description" SortExpression="Territory_Desc">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Unit_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Product Division" SortExpression="Unit_Desc">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProductLine_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Product Line" SortExpression="ProductLine_Desc">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Active_Flag" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Status">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:CommandField ShowSelectButton="True" HeaderText="Edit">
                                            </asp:CommandField>
                                        </Columns>
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
                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                        <AlternatingRowStyle CssClass="fieldName" />
                                    </asp:GridView>
                                    <!-- End Country Listing -->
                                </td>
                            </tr>
                            
                            <tr>
                                            <td>
                                                <%-- custom Paging --%>
                                                <asp:DataList CellPadding="5" RepeatDirection="Horizontal" runat="server" ID="dlPager"
                                                    onitemcommand="dlPager_ItemCommand">
                                                    <ItemTemplate>
                                                       <asp:LinkButton Enabled='<%#Eval("Enabled") %>' Width="40px" BorderStyle="Ridge" style="text-decoration:none; text-align:center;" runat="server" ID="lnkPageNo" Text='<%#Eval("Text") %>' CommandArgument='<%#Eval("Value") %>' CommandName="PageNo"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </td>
                                        </tr>
                            <tr>
                                <td >
                                    <asp:Button ID="btnSelectAllPages" Visible="false" runat="server" CssClass="btn"
                                     Text="Select Rows Of All Pages" Width="100"
                                        OnClick="btnSelectAllPages_Click" />
                                    <asp:Button ID="btnRemoveMultiple" runat="server" CssClass="btn"
                                     Text="Inactive Multiple" Width="100"
                                        OnClick="btnRemoveMultiple_Click" />
                                    <asp:Button ID="BtnActiveMultiple" runat="server" CssClass="btn"
                                     Text="Active Multiple" Width="100"
                                        OnClick="BtnActiveMultiple_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td width="100%" align="left" class="bgcolorcomm">
                                    <asp:UpdatePanel ID="UPEdit" runat="server">
                                        <ContentTemplate>
                                            <table width="98%" border="0">
                                                <tr>
                                                    <td colspan="2" align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                                        <font color='red'>*</font>
                                                        <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>"
                                                        style="padding-right: 10px;">
                                                        <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UPEdit" runat="server">
                                                            <ProgressTemplate>
                                                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:HiddenField ID="hdnRoutingSNo" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="30%">
                                                        Product Division<font color="red">*</font>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlProductDivision" runat="server" AutoPostBack="True" CssClass="simpletxt1"
                                                            OnSelectedIndexChanged="ddlProductDivision_SelectedIndexChanged" ValidationGroup="editt"
                                                            Width="175px">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlProductDivision"
                                                            ErrorMessage=" Product Division is required." InitialValue="Select" SetFocusOnError="true"
                                                            ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                        <asp:Button ID="btnAddProductDiv" Text=">>" CssClass="btn" runat="server" OnClick="btnAddProductDiv_Click" />
                                                        <asp:Button ID="btnRemoveProductDiv" Text="<<" CssClass="btn" runat="server" OnClick="btnRemoveProductDiv_Click" />
                                                        <asp:ListBox ID="lstbxProductDiv" Height="22" CssClass="simpletxt1" Width="175px"
                                                            runat="server" SelectionMode="Multiple"></asp:ListBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="30%">
                                                        Product Line
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlProductLine" runat="server" CssClass="simpletxt1" ValidationGroup="editt"
                                                            Width="175px">
                                                            <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="30%">
                                                        Service Contractor<font color="red">*</font>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlSC" runat="server" CssClass="simpletxt1" ValidationGroup="editt"
                                                            Width="250px" AutoPostBack="True" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSC"
                                                            ErrorMessage="Service Contractor is required." InitialValue="Select" SetFocusOnError="true"
                                                            ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="30%">
                                                        State <font color="red">*</font>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True" CssClass="simpletxt1"
                                                            OnSelectedIndexChanged="ddlState_SelectedIndexChanged" ValidationGroup="editt"
                                                            Width="175px">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RFState" runat="server" ControlToValidate="ddlState"
                                                            ErrorMessage="State is required." InitialValue="Select" SetFocusOnError="true"
                                                            ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="30%">
                                                        City<font color="red">*</font>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlCity" runat="server" AutoPostBack="True" CssClass="simpletxt1"
                                                            OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" TabIndex="4" ValidationGroup="editt"
                                                            Width="175px">
                                                        </asp:DropDownList>
                                                        <%-- <asp:CompareValidator runat="server" ID="cmpValddlCity" ValidationGroup="editt" ControlToValidate="ddlCity"
                                                            ControlToCompare="ddlSC" ErrorMessage="Please Select service contractore first"
                                                            ValueToCompare="Select" Operator="NotEqual">
                                                        </asp:CompareValidator>--%>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCity"
                                                            ErrorMessage="City is required." InitialValue="Select" SetFocusOnError="true"
                                                            ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr id="tdddlTerritory" runat="server">
                                                    <td width="30%">
                                                        Territory<font color='red'>*</font>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlTerritory" ValidationGroup="editt" runat="server" Width="175px"
                                                            CssClass="simpletxt1" TabIndex="8">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" SetFocusOnError="true"
                                                            ErrorMessage="Territory is required." ControlToValidate="ddlTerritory" InitialValue="Select"
                                                            ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr id="tdPreference" runat="server">
                                                    <td width="30%">
                                                        Preference
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtPreferenceUpdate" CssClass="txtboxtxt" MaxLength="1" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr id="tdSpecialRemark" runat="server">
                                                    <td width="30%">
                                                        SpecialRemark
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSpecialRemarkUpdate" CssClass="txtboxtxt" MaxLength="100" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="30%">
                                                        Status
                                                    </td>
                                                    <td>
                                                        <asp:RadioButtonList ID="rdoStatus" RepeatDirection="Horizontal" RepeatColumns="2"
                                                            runat="server" TabIndex="8">
                                                            <asp:ListItem Value="1" Text="Active" Selected="True">
                                                            </asp:ListItem>
                                                            <asp:ListItem Value="0" Text="In-Active">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" width="30%">
                                                        <asp:HiddenField ID="hdnShowGrid" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="30%">
                                                        <asp:HiddenField ID="hdnCitySno" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:HiddenField ID="hdnSCNo" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <table width="100%" border="0">
                                                            <tr id="tdTerritory" runat="server">
                                                                <td align="left" class="MsgTDCount">
                                                                    Total Number of Records :
                                                                    <asp:Label ID="lblRowCount1" CssClass="MsgTotalCount" runat="server">
                                                                    </asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="bgcolorcomm">
                                                                    <asp:GridView ID="gvTerritory" runat="server" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" 
                                                                        OnRowDataBound="gvTerritory_RowDataBound" AutoGenerateColumns="false">
                                                                        <RowStyle CssClass="gridbgcolor" />
                                                                        <Columns>
                                                                            
                                                                            <asp:TemplateField>
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="chkHeader" runat="server" onclick="javascript:SelectAllCheckboxes(this);" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkChild" onclick="javascript:ChildClick(this);" runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="SNo">
                                                                                <ItemTemplate>
                                                                                <%#Container.DataItemIndex+1 %>
                                                                                <%-- <asp:Label ID="lblSNo" runat="server" Text='<%# Bind("Sno") %>'></asp:Label>--%>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" Width="40px" />
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Territory">
                                                                                <ItemTemplate>
                                                                                    <asp:HiddenField ID="hdnTerritorySno" runat="server" Value='<%#Bind("Territory_SNo")%>' />
                                                                                    <asp:Label ID="lblTerritory_Desc" runat="server" Text='<%# Bind("Territory_Desc") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            
                                                                        </Columns>
                                                                        <PagerStyle HorizontalAlign="Center" />
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
                                                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                                        <AlternatingRowStyle CssClass="fieldName" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
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
                                                                        CausesValidation="True" ValidationGroup="editt" 
                                                                        OnClick="imgBtnAdd_Click" TabIndex="9" OnClientClick="return validateGvTerritory();" />
                                                                    <asp:Button Text="Save" Width="70px" ID="imgBtnUpdate" CssClass="btn" runat="server"
                                                                        CausesValidation="True" ValidationGroup="editt" OnClick="imgBtnUpdate_Click"
                                                                        TabIndex="11" />
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="imgBtnCancel" Width="70px" runat="server" CausesValidation="false"
                                                                        CssClass="btn" Text="Cancel" OnClick="imgBtnCancel_Click" TabIndex="3" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <!-- For button portion update end -->
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
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
