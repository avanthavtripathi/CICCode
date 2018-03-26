<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Last6MonthsPopup.aspx.cs"
    Inherits="Reports_Last6MonthsPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product Serial Detail</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/global.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../scripts/Common.js">
    </script>

    <script type="text/javascript" language="javascript">
function funOpenForProductSrNo(ProductSrNo)
{       
        var strUrl='../Reports/Last6MonthsPopup.aspx?ProdSrNo=' + ProductSrNo;
        newWin= window.open(strUrl,'PopUp','height=500,width=900,left=20,top=30,scrollbars=1');
        if(window.focus) {newWin.focus();}
}
 function funCommonPopUpLocal(BaseLineId)
          {
          
             var strUrl1='../pages/PopUp.aspx?BaseLineId='+BaseLineId;
                    //alert(strUrl);
             window.open(strUrl1,'Complaint','height=600,width=950,left=20,top=30,scrollbars=1');
        }
    </script>

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
        <asp:UpdatePanel ID="updatepnl" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td align="left" class="headingred">
                            Complaint Report
                        </td>
                        <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                            <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                <ProgressTemplate>
                                    <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 10px" align="center" colspan="2">
                            <table width="98%" border="0">
                               <%-- <tr>
                                    <td colspan="2" align="left" style="padding-left:5px">
                                        Customer Name
                                        <asp:TextBox runat="server" ID="txtCustomerName" CssClass="txtboxtxt" />&nbsp;
                                        <asp:Button Text="Go" Width="25px" CssClass="btn" ID="imgBtnGo" runat="server" OnClick="imgBtnGo_Click" />
                                    </td>
                                    <td align="left" class="MsgTDCount">
                                    </td>
                                </tr>--%>
                                <tr><td colspan="2">&nbsp;</td></tr>
                                <tr>
                                    <td align="left" class="MsgTDCount">
                                        Total Number of Complaints :
                                        <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="MsgTDCount">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table width="100%" border="0">
                                <tr>
                                    <td>
                                        <!-- Action Listing -->
                                        <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                            HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="True"
                                            PagerStyle-HorizontalAlign="Center" AllowSorting="True" AutoGenerateColumns="False"
                                            ID="gvComm" runat="server" OnPageIndexChanging="gvComm_PageIndexChanging" Width="100%"
                                            HorizontalAlign="Left" Visible="true" OnSorting="gvComm_Sorting">
                                            <Columns>
                                                <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" HeaderText="SNo">
                                                    <HeaderStyle Width="40px" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Product_Serial_No" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" HeaderText="Customer Name">
                                                    <ItemTemplate>
                                                        <%#Eval("CustomerName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Product_Serial_No" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" HeaderText="Product Serial No.">
                                                    <ItemTemplate>
                                                        <%#Eval("Product_Serial_No")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Width="120px" SortExpression="Complaint_RefNo" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" HeaderText="Complaint_RefNo">
                                                    <ItemTemplate>
                                                        <a href="Javascript:void(0);" onclick="funCommonPopUpLocal(<%#Eval("BaseLineId")%>)">
                                                            <%#Eval("Complaint_RefNo")%>/<%#Eval("splitComplaint_RefNo")%></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Width="120px" SortExpression="LoggedDate" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" HeaderText="Logged Date">
                                                    <ItemTemplate>
                                                        <%#Eval("LoggedDate")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Width="120px" SortExpression="State_Desc" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" HeaderText="State">
                                                    <ItemTemplate>
                                                        <%#Eval("State_Desc")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Width="120px" SortExpression="City_Desc" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" HeaderText="City">
                                                    <ItemTemplate>
                                                        <%#Eval("City_Desc")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Width="120px" SortExpression="SC_Name" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" HeaderText="ASC Name">
                                                    <ItemTemplate>
                                                        <%#Eval("SC_Name")%>
                                                    </ItemTemplate>
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
                                        <!-- End Action Listing -->
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <%-- <tr>
                           
                            <td align="center">
                            <asp:Button ID="btnExportExcel" runat="server" Width="114px" Text="Save To Excel"
                             CssClass="btn" OnClick="btnExportExcel_Click" />
                             
                                </td>
                            </tr>--%>
                                <tr>
                                    <td align="center">
                                        &nbsp;
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
