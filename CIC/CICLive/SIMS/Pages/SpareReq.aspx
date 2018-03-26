<%@ Page Language="C#" AutoEventWireup="true"
    CodeFile="SpareReq.aspx.cs" Inherits="pages_SpareReq" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Crompton Greaves :: Customer Interaction Center</title>
     <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/global.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript" src="../scripts/Common.js">

</script>
<script language="javascript" type="text/javascript">
function checkNumberOnly(e) 
         {
            var KeyID; 
            if(navigator.appName=="Microsoft Internet Explorer") 
            { 
                KeyID = e.keyCode;
            } 
            else 
            { 
                KeyID = e.charCode; 
            } 
            if(window.event) 
            { 
                if(window.event.keyCode==13) 
                { 
                    return false; 
                } 
            } 
//            if(KeyID==13) 
//            { 
//                return false; 
//            } 
            if((KeyID>47 && KeyID<58)||(KeyID==32)||(KeyID==8)) 
            {
            } 
            else 
            {
            alert("Please enter numbers only.");
                return false; 
            }          
         }
         // The below function is used for check whether press key is number and char or not
         // Only allow number and characters
        function checkNumberCharOnly(e) 
         {
            var KeyID; 
            if(navigator.appName=="Microsoft Internet Explorer") 
            { 
                KeyID = e.keyCode;
            } 
            else 
            { 
                KeyID = e.charCode; 
            } 
            if(window.event) 
            { 
                if(window.event.keyCode==13) 
                { 
                    return false; 
                } 
            } 
//            if(KeyID==13) 
//            { 
//                return false; 
//            } 
            if((KeyID>47 && KeyID<58)||(KeyID==32)||(KeyID==8)) 
            {
            } 
            else 
            {
            alert("Please enter numbers only.");
                return false; 
            }          
         }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Enter Spare Requirement
                        <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="Add" ShowMessageBox="true"
                            ShowSummary="false" runat="server" />
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress AssociatedUpdatePanelID="UpdatePanel1" ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Complaint Number:
                    </td>
                    <td>
                        <asp:Label ID="lblComplaint" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblSlash" runat="server" Text="/"></asp:Label>
                        <asp:Label ID="lblSplitComplaint" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        SpareCode/Description:<font color="red">*</font>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSpare" CssClass="simpletxt1" runat="server" ValidationGroup="Add">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSpare"
                            ErrorMessage="Enter Spare Description" Display="None" ValidationGroup="Add">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Quantity:<font color="red">*</font>
                    </td>
                    <td>
                        <asp:TextBox ID="txtQty" CssClass="simpletxt1" onkeypress="javascript:return checkNumberOnly(event);" 
                         runat="server" ValidationGroup="Add">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtQty"
                            ErrorMessage="Enter Spare Quantity" Display="None" ValidationGroup="Add">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnAdd" runat="server" Text="Add More" CssClass="btn" OnClick="btnAdd_Click"
                            ValidationGroup="Add" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="gvSpare" runat="server" AlternatingRowStyle-CssClass="fieldName"
                            RowStyle-CssClass="gridbgcolor" Width="98%" GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor"
                            AutoGenerateColumns="false">
                            <RowStyle CssClass="gridbgcolor" />
                            <Columns>
                                <asp:TemplateField HeaderText="SpareCode/Description" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblSpare" Text='<%#Eval("Spare")%>' runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="QTY" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblQty" Text='<%#Eval("Qty") %>' runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remove" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        
                                        <asp:LinkButton CssClass="simpletxt1" CommandArgument='<%#Eval("SpareSNo") %>' 
                                            ID="lnkBtnRemove" runat="server" onclick="lnkBtnRemove_Click">Remove</asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="fieldNamewithbgcolor" />
                            <AlternatingRowStyle CssClass="fieldName" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnSave" runat="server" Visible="false" Text="Save" CssClass="btn"
                            OnClick="btnSave_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
 </div>
    </form>
</body>
</html>
