<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfirmedPaymentsInternalBillClaims.aspx.cs"
    Inherits="SIMS_Reports_ConfirmedPaymentsInternalBillClaims" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Internal bill Confirmation</title>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/global.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        function funComplainDetail(strUrl) {

            window.open(strUrl, "_blank", 'width=900,height=600,scrollbars=1,resizable=no,top=1,left=1');
            return false;
        }

               

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" width="100%">
            <tr>
                <td align="left" class="MsgTDCount">
                    Total Number of Records :
                    <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                </td>
                <td align="right">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView PageSize="5" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridLines="both" AllowPaging="True"
                        AllowSorting="True" AutoGenerateColumns="false" ID="gvConfirmation" runat="server"
                        Width="100%" HorizontalAlign="Left" OnPageIndexChanging="gvConfirmation_PageIndexChanging"
                        >
                        <RowStyle CssClass="gridbgcolor" />
                        <Columns>
                            <asp:TemplateField HeaderText="" HeaderStyle-Width="150px">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ASC_Name" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left" HeaderText="Service Contractor">
                                <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ProductDivision_Desc" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left" HeaderText="Product Division">
                                <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderStyle-Width="150px" HeaderText="Complaint No" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <a href="#" onclick='return funComplainDetail("../../pages/PopUp.aspx?BaseLineId=<%#Eval("BaseLineId")%>")'>
                                        <%#Eval("Complaint_No")%>
                                    </a>
                                </ItemTemplate>
                                <HeaderStyle Width="150px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="InternalBill No" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <a href="#" onclick='return funComplainDetail("../Pages/InternalBillDetails.aspx?Rep=true&BillNo=<%#Eval("Internal_Bill_No")%>&Div=<%#Eval("ProductDivision_ID")%>&ASC=<%#Eval("ASC_ID")%>")'>
                                        <%#Eval("Internal_Bill_No")%>
                                    </a>
                                </ItemTemplate>
                                <HeaderStyle Width="150px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Claim No" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblClaimNo" runat="server" Text='<%#Eval("Claim_No") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Claim_Date" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left" HeaderText="Claim Generated Date">
                                <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Product_Code" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left" HeaderText="Product Serial No">
                                <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Product" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left" HeaderText="Product">
                                <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Defects" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left" HeaderText="Defects">
                                <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Amount" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left" HeaderText="Amount(Material+Service)">
                                <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderStyle-Width="150px" HeaderText="Approval/Disapprove Flag">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkActivityConfirm" Enabled="false" runat="server" Checked='<%#bool.Parse(Eval("Approve").ToString())%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="150px" HeaderText="Disapprove Comment">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtComment" Enabled="false" Height="50px" runat="server" Width="200"
                                        CssClass="txtboxtxt" TextMode="MultiLine" Text='<%#Eval("BA_Comment")%>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                        <img src="<%=ConfigurationManager.AppSettings["SIMSUserMessage"]%>" alt="" />
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
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnCancel" Width="70px" runat="server" CssClass="btn" Text="Close" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
