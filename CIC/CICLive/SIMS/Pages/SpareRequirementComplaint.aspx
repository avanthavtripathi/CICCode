<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SpareRequirementComplaint.aspx.cs" Inherits="SIMS_Pages_SpareRequirementComplaint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Spare Requirement For Complaint</title>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>

    <form id="form1" runat="server" >
    <script type="text/javascript" language="javascript">
//     window.onunload = refreshparent; BP:13Jan13
function getQueryStrings() { 
  var assoc  = {};
  var decode = function (s) { return decodeURIComponent(s.replace(/\+/g, " ")); };
  var queryString = location.search.substring(1); 
  var keyValues = queryString.split('&'); 

  for(var i in keyValues) { 
    var key = keyValues[i].split('=');
    if (key.length > 1) {
      assoc[decode(key[0])] = decode(key[1]);
    }
  } 

  return assoc; 
} 

//NOT USED onblur PROD BP:13Jan13
//function refreshparent()
//  { 
//    var url = window.opener.location.href ;
//    var aaa =  getQueryStrings()["CompNo"];
//    if( url.indexOf("ReturnId") != -1)
//    window.opener.location.href =  url;
//    else
//    window.opener.location.href =  url +'?CrefNo='+aaa+'&lnk=true';
//    window.close();
//}
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred" style="width: 968px">
                        Spare Requirement For Complaint
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["SIMSAjaxLoadingImageName"]%>" alt="" /> </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right">
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" colspan="2">
                        <table width="100%" border="0">
                            <tr>
                                <td width="100%" align="left" class="bgcolorcomm">
                                    <table width="100%" border="0" cellspacing="1" cellpadding="2">
                                        <tr align="right">
                                            <td align='<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>' colspan="3" align="right">
                                                <font color="red">*</font>
                                                <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Service Contractor:
                                            </td>
                                            <td>
                                                <asp:Label ID="lblASCName" runat="server"></asp:Label>
                                                <asp:HiddenField ID="hdnASC_Code" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="17%">
                                                Division:</td>
                                            <td width="17%">
                                                <asp:Label ID="lblDivision" runat="server"></asp:Label>
                                                <asp:HiddenField ID="hdnDivSNo" runat="server" />
                                            </td>
                                            <td width="17%">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td height="25">
                                                Product:</td>
                                            <td>
                                                <asp:Label ID="lblProduct" runat="server"></asp:Label>
                                                <asp:HiddenField ID="hdnProdSNo" runat="server" />
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <p>
                                                    Complaint No.</p>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblComplaintNo" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="100%" border="0" cellspacing="1" cellpadding="2">
                                                           <tr>
                                            <td align="center">
                                            
                                                <asp:GridView ID="GvSpareReqComplaint" runat="server" AllowPaging="True" AllowSorting="True"
                                                    AlternatingRowStyle-CssClass="fieldName" HeaderStyle-HorizontalAlign="Left" RowStyle-HorizontalAlign="Left"
                                                    AlternatingRowStyle-HorizontalAlign="Left" PageSize="5" AutoGenerateColumns="False"
                                                    HeaderStyle-CssClass="fieldNamewithbgcolor" Width="100%" RowStyle-CssClass="gridbgcolor"
                                                    PagerStyle-CssClass="Paging" OnRowDataBound="GvSpareReqComplaint_RowDataBound">
                                                    <RowStyle CssClass="gridbgcolor" HorizontalAlign="Left" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Spare Code">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlSpareCode" runat="server" CssClass="simpletxt1" Width="210px"
                                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlSpareCode_SelectedIndexChanged"
                                                                    ValidationGroup="editt">
                                                                </asp:DropDownList>
                                                                <asp:TextBox ID="txtFindSpare" runat="server" CausesValidation="True" 
                                                                    CssClass="txtboxtxt" ValidationGroup="ProductRef" Width="60px"></asp:TextBox>
                                                                <asp:Button ID="btnGoSpare" runat="server" CssClass="btn" 
                                                                    OnClick="btnGoSpare_Click" Text="Go" ValidationGroup="ProductRef" Width="20px" />
                                                               <div>
                                                                      <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvSpareCode" runat="server"
                                                                    ControlToValidate="ddlSpareCode" Display="Dynamic" ErrorMessage="Spare Code is required."
                                                                    InitialValue="0" ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                               </div>       
                                                            </ItemTemplate>
                                                            <ItemStyle Width="20%" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Current Stock">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblCurrentStock" runat="server" CssClass="txtboxtxt" ReadOnly="true"
                                                                    Text='<%#Eval("Current_Stock")%>' EnableViewState="true"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="15%" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Proposed Qty">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtProposedQty" AutoPostBack="True" runat="server" Text='<%#Eval("Proposed_Qty")%>'
                                                                    CssClass="txtboxtxt" MaxLength="10" OnTextChanged="txtProposedQty_TextChanged"
                                                                    ValidationGroup="editt"></asp:TextBox>
                                                                <div>
                                                                <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvProposedQty" runat="server"
                                                                    ControlToValidate="txtProposedQty" Display="Dynamic" ErrorMessage="Proposed Quantity should not be zero."
                                                                    InitialValue="0" ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                                <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvProposedQty1" runat="server"
                                                                    ControlToValidate="txtProposedQty" Display="Dynamic" ErrorMessage="Proposed Quantity is required."
                                                                    ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtProposedQty"
                                                                    ID="RegularExpressionValidator2" ValidationGroup="editt" runat="server" ErrorMessage="Proper Proposed Quantity is required."
                                                                    ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
                                                               </div>    
                                                                </ItemTemplate>
                                                            <ItemStyle Width="29%" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Button CommandName="Add" CommandArgument="" Text="Add" ToolTip="Add Row" ID="btnAddRow"
                                                                    runat="server" CssClass="btn" ValidationGroup="editt" CausesValidation="true"
                                                                    Style="width: 50px" OnClick="btnAddRow_Click" />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="4%" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Button CommandName="DeleteRow" CommandArgument="" Text="Delete" ToolTip="Delete Row"
                                                                    ID="btnDeleteRow" runat="server" CssClass="btn" ValidationGroup="editt" CausesValidation="false"
                                                                    Style="width: 50px" OnClick="btnDeleteRow_Click" />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="4%" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                                    <img src="<%=ConfigurationManager.AppSettings["UserMessage"]%>" alt="" />
                                                                    <b>
                                                                        <%=ConfigurationManager.AppSettings["NoRecordFound"]%></b>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                    <HeaderStyle CssClass="fieldNamewithbgcolor" HorizontalAlign="Left" />
                                                    <AlternatingRowStyle CssClass="fieldName" HorizontalAlign="Left" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0">
                <tr>
                    <td height="25" align="center">
                         <table>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnConfirm" runat="server" Text="Generate Advice" 
                                        CssClass="btn" Style="height: 20px;"
                                        ValidationGroup="editt" CausesValidation="true" 
                                        OnClick="btnConfirm_Click" Width="100px" />
                                    <%--OnClientClick="javascript:return confirm('Are you sure you want to save this Spare Requirement?')"--%>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn" Style="width: 60px" Visible="false" 
                                        CausesValidation="false" OnClick="btnCancel_Click" />
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="BtnClose" runat="server" Text="Close" CssClass="btn" Style="width: 60px"  
                                        CausesValidation="false"  OnClientClick="window.close();" /> <%-- OnClientClick="refreshparent();" BP 13jan13 Not used on Prod--%>
                                    
                                    
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
  </form>
</body>
</html>