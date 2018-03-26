<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ASCComplaintMISPopUp.aspx.cs"
    Inherits="Reports_ASCComplaintMISPopUp" %>
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
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExport" />
            </Triggers>
            <ContentTemplate>
                <table width="100%" border="0">
                    <tr>
                        <td class="headingred" style="width: 40%">
                            Complaints Drill Down
                        </td>
                        <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                            <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                <ProgressTemplate>
                                    <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                    </tr>
                    <%--<tr>
                    <td align="right">
                        Region:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlRegion" Width="175" AutoPostBack="true" runat="server" CssClass="simpletxt1"
                            OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Branch:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBranch" Width="175" AutoPostBack="true" runat="server" CssClass="simpletxt1"
                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Product Division:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlProductDivision" Width="175" runat="server" CssClass="simpletxt1">
                        </asp:DropDownList>
                    </td>
                </tr>--%>
                    <tr>
                        <td align="center" colspan="2">
                            <%--<asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="Search" Width="100"
                            OnClick="btnSearch_Click" />--%>
                            <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn" Text="Export To Execl"
                                Width="100" OnClick="btnExport_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left">
                            Total Count:
                            <asp:Label ID="lblCount" ForeColor="Red" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                               <asp:GridView ID="gvMIS" CssClass="simpletxt1" runat="server" RowStyle-CssClass="gridbgcolor"
                                Width="100%" AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                                GridGroups="both" AllowPaging="false" PagerStyle-HorizontalAlign="Center" AllowSorting="True"
                                AutoGenerateColumns="False" HorizontalAlign="Left" OnPageIndexChanging="gvMIS_PageIndexChanging"
                                OnSorting="gvMIS_Sorting" onrowdatabound="gvMIS_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                        <HeaderStyle Width="40px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderStyle-Width="100px" SortExpression="ProductDivision_Desc"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Product Division">
                                        <ItemTemplate>
                                            <%#Eval("ProductDivision_Desc")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="100px" SortExpression="SC_Name" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" HeaderText="Service Contractor">
                                        <ItemTemplate>
                                            <%#Eval("SC_Name")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="100px" SortExpression="CGUser" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" HeaderText="CG Employee">
                                        <ItemTemplate>
                                            <%#Eval("CGUser")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="100px" SortExpression="CGContractUser" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" HeaderText="CG Contract Employee">
                                        <ItemTemplate>
                                            <%#Eval("CGContractUser")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                  
                                    <asp:TemplateField HeaderStyle-Width="100px" SortExpression="SLADate" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" HeaderText="SLA Date">
                                        <ItemTemplate>
                                            <%#Eval("SLADate")%></ItemTemplate>
                                    </asp:TemplateField>
                                    
                                                                        
                                    <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Complaint_RefNo" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" HeaderText="Complaint RefNo.">
                                        <ItemTemplate>
                                            <a href="Javascript:void(0);" onclick="funCommonPopUp(<%#Eval("BaseLineId")%>)">
                                                <%#Eval("Complaint_RefNo")%></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="100px" SortExpression="StageDesc" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" HeaderText="Stage Description">
                                        <ItemTemplate>
                                            <%#Eval("StageDesc")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="100px" SortExpression="ProductLine_Desc" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" HeaderText="Product Line">
                                        <ItemTemplate>
                                            <%#Eval("ProductLine_Desc")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Product_Desc" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" HeaderText="Product">
                                        <ItemTemplate>
                                            <%#Eval("Product_Desc")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="100px" SortExpression="SRF" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" HeaderText="SRF">
                                        <ItemTemplate>
                                            <%#Eval("SRF")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="100px" SortExpression="NatureOfComplaint" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" HeaderText="Nature Of Complaint">
                                        <ItemTemplate>
                                            <%#Eval("NatureOfComplaint")%></ItemTemplate>
                                    </asp:TemplateField>
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
                            <asp:GridView ID="gvExport" CssClass="simpletxt1" runat="server" 
                                AutoGenerateColumns="False" onrowdatabound="gvExport_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                        <HeaderStyle Width="40px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderStyle-Width="100px" SortExpression="ProductDivision_Desc"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Product Division">
                                        <ItemTemplate>
                                            <%#Eval("ProductDivision_Desc")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="100px" SortExpression="SC_Name" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" HeaderText="Service Contractor">
                                        <ItemTemplate>
                                            <%#Eval("SC_Name")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="100px" SortExpression="CGUser" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" HeaderText="CG Employee">
                                        <ItemTemplate>
                                            <%#Eval("CGUser")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="100px" SortExpression="CGContractUser" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" HeaderText="CG Contract Employee">
                                        <ItemTemplate>
                                            <%#Eval("CGContractUser")%>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderStyle-Width="100px" SortExpression="SLADate" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" HeaderText="SLA Date">
                                        <ItemTemplate>
                                            <%#Eval("SLADate")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Complaint_RefNo" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" HeaderText="Complaint RefNo.">
                                        <ItemTemplate>
                                            <%#Eval("Complaint_RefNo")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="100px" SortExpression="StageDesc" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" HeaderText="Stage Description">
                                        <ItemTemplate>
                                            <%#Eval("StageDesc")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="100px" SortExpression="ProductLine_Desc" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" HeaderText="Product Line">
                                        <ItemTemplate>
                                            <%#Eval("ProductLine_Desc")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Product_Desc" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" HeaderText="Product">
                                        <ItemTemplate>
                                            <%#Eval("Product_Desc")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="100px" SortExpression="SRF" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" HeaderText="SRF">
                                        <ItemTemplate>
                                            <%#Eval("SRF")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="100px" SortExpression="NatureOfComplaint" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" HeaderText="Nature Of Complaint">
                                        <ItemTemplate>
                                            <%#Eval("NatureOfComplaint")%></ItemTemplate>
                                    </asp:TemplateField>
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
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
