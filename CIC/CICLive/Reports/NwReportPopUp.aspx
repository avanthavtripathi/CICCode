<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NwReportPopUp.aspx.cs" Inherits="Reports_NwReportPopUp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title>Details</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/global.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../scripts/Common.js">
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
             <ContentTemplate>
                <table width="100%" border="0">
                    <tr>
                        <td class="headingred" style="width: 40%">
                            Complaints Details
                        </td>
                        <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                            <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                <ProgressTemplate>
                                    <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                    </tr>
                    <tr style="display:none">
                        <td colspan="2" align="left">
                            Total Count:
                            <asp:Label ID="lblCount" ForeColor="Red" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                               <asp:GridView ID="gvMIS" CssClass="simpletxt1" runat="server" RowStyle-CssClass="gridbgcolor" AutoGenerateColumns="false"  
                                Width="100%" AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                                GridGroups="both"  PagerStyle-HorizontalAlign="Center" HorizontalAlign="Left">
                                <Columns>
                                <asp:BoundField HeaderText="SNo" DataField="SNo" />
                                <asp:TemplateField HeaderText="Complaint No">
                                                <ItemTemplate>
                                                    <a href="Javascript:void(0);" onclick="funCommonPopUpReport(<%#Eval("BaseLineId")%>)"> 
                                                         <%#Eval("ComplaintNo")%>
                                                    </a>
                                                </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:BoundField HeaderText="Region" DataField="Region" />
                                  <asp:BoundField HeaderText="Branch" DataField="Branch" />
                                   <asp:BoundField HeaderText="LoggedDate" DataField="LoggedDate" />
                                            
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
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;</td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
