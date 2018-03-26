<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddSpare.aspx.cs" Inherits="pages_AddSpare"%>
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
                    <td class="headingred" >
                        Send Spare 
                        <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="Add"
                                                 ShowMessageBox="true" ShowSummary="false" runat="server" />
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
                        <asp:HiddenField ID="hdnSpare_Sno" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        SpareCode/Description:<font color="red">*</font>
                    </td>
                    <td>
                        <asp:Label ID="lblSpareDesc" runat="server" Text=""></asp:Label>
                       
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Quantity:<font color="red">*</font>
                    </td>
                    <td>
                        <asp:TextBox ID="txtQty" CssClass="simpletxt1" runat="server" 
                               onkeypress="javascript:return checkNumberOnly(event);" ValidationGroup="Add">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                ControlToValidate="txtQty" ErrorMessage="Enter Spare Quantity"
                                Display="None" ValidationGroup="Add">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                 <tr>
                    <td align="right">
                        Document Dispatch No:<font color="red">*</font>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDocDispatch" CssClass="simpletxt1" runat="server" ValidationGroup="Add">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="txtDocDispatch" ErrorMessage="Enter Spare Quantity"
                                Display="None" ValidationGroup="Add">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnSave" runat="server" ValidationGroup="Add" Visible="true" Text="Send" 
                            CssClass="btn" onclick="btnSave_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>




