<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="Last6MonthComplaints.aspx.cs" Inherits="Reports_Last6MonthComplaints" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script type="text/javascript" language="javascript">
function funOpenForProductSrNo(ProductSrNo)
{       
        var strUrl='../Reports/Last6MonthsPopup.aspx?ProdSrNo=' + ProductSrNo;
        newWin= window.open(strUrl,'PopUp','height=600,width=800,left=20,top=30,scrollbars=1');
        if(window.focus) {newWin.focus();}
}
    </script>

    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Repeated Complaints Exception Report
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
                                <td align="right" width="20%">
                                    Contract No
                                </td>
                                <td align="left" class="MsgTDCount">
                                    <asp:TextBox runat="server" ID="txtTelephoneNo" CssClass="txtboxtxt" />
                                </td>
                            </tr>--%>
                            <tr>
                                <td align="right" width="20%">
                                    Product Serial No
                                </td>
                                <td align="left" class="MsgTDCount">
                                    <asp:TextBox runat="server" ID="txtProductSerialNo" CssClass="txtboxtxt" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="20%">
                                    Product Division
                                </td>
                                <td align="left" class="MsgTDCount">
                                    <asp:DropDownList ID="ddlProductDiv" CssClass="simpletxt1" Width="200" runat="server">
                                        <%-- <asp:ListItem Value="0">Select</asp:ListItem>--%>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td align="left" class="MsgTDCount">
                                    <asp:Button Width="70px" Text="Search" ID="btnSearch" CssClass="btn" runat="server"
                                        OnClick="btnSearch_Click" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table width="98%" border="0">
                            <tr>
                                <td align="left" class="MsgTDCount">
                                    Total Number of Complaints :
                                    <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" Text="0" runat="server"></asp:Label>
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
                                            <%--<asp:TemplateField HeaderStyle-Width="120px" SortExpression="UniqueContact_No" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderText="Telephone Number ">
                                                <ItemTemplate>
                                                    <%#Eval("UniqueContact_No")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderStyle-Width="120px" SortExpression="ProductDivision_Desc"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Product Division">
                                                <ItemTemplate>
                                                    <%#Eval("ProductDivision_Desc")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Product_Serial_No" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderText="Product Serial No.">
                                                <ItemTemplate>
                                                    <a href="Javascript:void(0);" onclick="funOpenForProductSrNo('<%#Eval("Product_Serial_No")%>')">
                                                        <%#Eval("Product_Serial_No")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="120px" SortExpression="Count" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderText="Repeat Count">
                                                <ItemTemplate>
                                                    <%#Eval("Count")%>
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
</asp:Content>
