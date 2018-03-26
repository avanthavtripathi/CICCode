<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadedFilePopUp.aspx.cs" Inherits="pages_UploadedFilePopUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>View Uploaded Files</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/global.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 33%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div align="center">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnFileUpload" />
        </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr height="20">
                    <td colspan="2" class="bgcolorcomm">
                        <table width="99%">
                            <tr height="20">
                               <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                                    <asp:UpdateProgress AssociatedUpdatePanelID="UpdatePanel1" ID="UpdateProgress2" runat="server">
                                        <ProgressTemplate>
                                            <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                            <tr height="20">
                                <td style="width: 15%">
                                    ComplaintRef. No
                                </td>
                               <td>
                                  <asp:Label ID="lblComplaintRefNo" Text="" runat="server"></asp:Label>  
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:GridView ID="gvFiles" runat="server" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                                HeaderStyle-CssClass="fieldNamewithbgcolor" AutoGenerateColumns="False" BorderStyle="None"
                                                GridLines="none" Width="100%" 
                                                OnPageIndexChanging="gvFiles_PageIndexChanging" 
                                                onrowdatabound="gvFiles_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="File Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Height="25px">
                                                        <ItemTemplate>
                                                            <a target="_blank" href ="../Docs/Customer/<%#Eval("FileName")%>"><%#Eval("FileName")%></a>
                                                            <asp:HiddenField ID="hdngvCreatedBy" runat="server" Value='<%#Eval("CreatedBy") %>' /> 
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Height="25px">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkRemove" OnClick="lnkRemove_Click" 
                                                                        Text="Remove" runat="server" CommandName='<%#("FileName") %>'
                                                                    CommandArgument='<%#Eval("Id")%>'></asp:LinkButton>                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <AlternatingRowStyle BorderStyle="None" />
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr height="20">
                               <td style="width: 15%">
                                    Upload File
                                </td>
                                <td>
                                    <input type="file" class="btn" id="flUpload" runat="server" onkeydown="if(event.keyCode==9){return true;}else{return false;}" />&nbsp;<asp:Button
                                        ID="btnFileUpload" runat="server" CssClass="btn" CausesValidation="false" Text="Upload"
                                        OnClick="btnFileUpload_Click" />
                                </td>
                                
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    </div>
    </form>
</body>
</html>



