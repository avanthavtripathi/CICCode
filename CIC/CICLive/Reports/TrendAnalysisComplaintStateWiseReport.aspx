<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="TrendAnalysisComplaintStateWiseReport.aspx.cs" Inherits="Reports_TrendAnalysisComplaintStateWiseReport"
    Title="Complaints StateWise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function SelectDate() {
            var my_date
            var indate
            var dDate, mMonth, yYear
            var prdcode = document.getElementById("ctl00_MainConHolder_ddlProductDivison").value;

            var LoggedDateFrom, LoggedDateTo, SLADateFrom, SLADateTo, DApproveFrom, DApproveTo;
            LoggedDateFrom = document.getElementById("ctl00_MainConHolder_txtLoggedDateFrom").value;
            LoggedDateTo = document.getElementById("ctl00_MainConHolder_txtLoggedDateTo").value;

            if (!((LoggedDateFrom != "" && LoggedDateTo != "") || (SLADateFrom != "" && SLADateTo != "") || (DApproveFrom != "" && DApproveTo != ""))) {
                alert('Please enter at least one date.');
                return false;
            }
            else {
                if (LoggedDateFrom != "" && LoggedDateTo != "") {
                    indate = new Date(LoggedDateFrom);
                    my_date = new Date(LoggedDateTo);
                    var m
                    var selm

                    m = parseInt(indate.getMonth());
                    if (prdcode == 0) {
                        m = m + 0
                        selm = 1
                    }
                    else {
                        selm = 3
                        m = m + 3
                    }

                    var msg
                    if (selm == 1) {
                        msg = "You can view the data only for current month. Please change your date selection.\n Or select product division to view the data for previous 3 months.";
                    }
                    else {
                        msg = "You can view the data only for previous three month. Please change your date selection.";
                    }

                    if (indate.getFullYear() < my_date.getFullYear()) {
                        alert(msg);
                        return false;
                    }


                    if (parseInt(m) < parseInt(my_date.getMonth())) {
                        alert(msg);
                        return false;
                    }
                }
                if (SLADateFrom != "" && SLADateTo != "") {
                    indate = new Date(SLADateFrom);
                    my_date = new Date(SLADateTo);
                    var m, selm
                    m = parseInt(indate.getMonth());
                    if (prdcode == 0) {
                        m = m + 0
                        selm = 1
                    }
                    else {
                        selm = 3
                        m = m + 3
                    }

                    var msg
                    if (selm == 1) {
                        msg = "You can view the data only for current month. Please change your date selection.\n Or select product division to view the data for previous 3 months.";
                    }
                    else {
                        msg = "You can view the data only for previous three month. Please change your date selection.";
                    }

                    if (indate.getFullYear() < my_date.getFullYear()) {
                        alert(msg);
                        return false;
                    }


                    if (parseInt(m) < parseInt(my_date.getMonth())) {
                        alert(msg);
                        return false;
                    }
                }
                if (DApproveFrom != "" && DApproveTo != "") {
                    indate = new Date(DApproveFrom);
                    my_date = new Date(DApproveTo);
                    var m, selm
                    m = parseInt(indate.getMonth());
                    if (prdcode == 0) {
                        m = m + 0
                        selm = 1
                    }
                    else {
                        m = m + 3
                        selm = 3
                    }
                    var msg
                    if (selm == 1) {
                        msg = "You can view the data only for current month. Please change your date selection.\n Or select product division to view the data for previous 3 months.";
                    }
                    else {
                        msg = "You can view the data only for previous three month. Please change your date selection.";
                    }

                    if (indate.getFullYear() < my_date.getFullYear()) {
                        alert(msg);
                        return false;
                    }


                    if (parseInt(m) < parseInt(my_date.getMonth())) {
                        alert(msg);
                        return false;
                    }

                }
            }

        }
    </script>

    <table>
        <tr>
            <td align="right">
                Logged Date
            </td>
            <td align="left">
                <asp:TextBox runat="server" ID="txtLoggedDateFrom" CssClass="txtboxtxt" ValidationGroup="editt"
                    MaxLength="10" />
                <asp:CompareValidator ID="CompareValidator1" runat="server" Type="Date" Operator="DataTypeCheck"
                    ControlToValidate="txtLoggedDateFrom" Display="none" ValidationGroup="editt"
                    SetFocusOnError="true"></asp:CompareValidator>
                <cc1:CalendarExtender ID="CEFrom" runat="server" TargetControlID="txtLoggedDateFrom">
                </cc1:CalendarExtender>
                To
                <asp:TextBox runat="server" ID="txtLoggedDateTo" CssClass="txtboxtxt" ValidationGroup="editt"
                    MaxLength="10" />
                <asp:CompareValidator ID="CompareValidator7" runat="server" Type="Date" Operator="DataTypeCheck"
                    ControlToValidate="txtLoggedDateTo" Display="none" ValidationGroup="editt" SetFocusOnError="true"></asp:CompareValidator>
                <cc1:CalendarExtender ID="CETo" runat="server" TargetControlID="txtLoggedDateTo">
                </cc1:CalendarExtender>
                <asp:Label ID="lblDateErr" runat="server" ForeColor="Red" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right">
            </td>
            <td align="left">
                <br />
                <asp:Button Width="70px" Text="Search" CssClass="btn" ValidationGroup="editt" CausesValidation="true"
                    ID="btnSearch" runat="server"  OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
    <table width="100%" border="0">
        <tr>
            <td>
                <b style="color: Red"> <asp:Label ID="lblcount" Visible="false" Text="Total Count:" runat="server"></asp:Label></b>
                <asp:Label ID="lblRowCount"  runat="server"></asp:Label>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                    HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="True"
                    PagerStyle-HorizontalAlign="Center" AllowSorting="False" AutoGenerateColumns="False"
                    ID="gvComm" runat="server" Width="100%" HorizontalAlign="Left" OnPageIndexChanging="gvComm_PageIndexChanging"
                    OnRowCommand="gvComm_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" HeaderText="State">
                            <ItemTemplate>
                                <%#Eval("State_Desc")%>
                                <asp:Label ID="lblStateDesc" Visible="true" runat="server" Text=' <%#Eval("State_SNo")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" HeaderText="City">
                            <ItemTemplate>
                                <%#Eval("City_Desc")%>
                                <asp:Label ID="lblCityDesc" runat="server" Visible="true" Text=' <%#Eval("City_Sno")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" HeaderText="Total Complaints">
                            <ItemTemplate>
                                <%#Eval("TotalComplaints")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                            HeaderText="Complaints In SLA">
                            <ItemTemplate>
                                <%#Eval("ComplaintsInSLA")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                            HeaderText="Complaints Out SLA">
                            <ItemTemplate>
                                <%#Eval("ComplaintsOutSLA")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                            HeaderText="Divisions">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDivision" runat="server" CommandName="ShowDiv" Width="100px"
                                    Style="width: 300px; word-break: break-all"><%#Eval("Divisions")%></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                            HeaderText="Service Contractor">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkSCUserName" CommandName="ShowSC" runat="server" Width="100px"
                                    Style="width: 300px; word-break: break-all"><%#Eval("SC_UserName")%></asp:LinkButton>
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
            <td colspan="2" align="center">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlComDetail" Width="100%" Visible="false" runat="server">
                    <table width="100%" border="0">
                        <tr>
                            <td>
                                <b style="color: Red">Total Count:</b>
                                <asp:Label ID="lblRowCountDetail" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                    HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="True"
                                    PagerStyle-HorizontalAlign="Center" AllowSorting="False" AutoGenerateColumns="False"
                                    ID="gvComDetail" runat="server" Width="100%" HorizontalAlign="Left" OnRowCommand="gvComDetail_RowCommand"
                                    OnRowDataBound="gvComDetail_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" HeaderText="Val1">
                                            <ItemTemplate>
                                                <%#Eval("Val1")%>
                                                <asp:Label ID="lblVal1" runat="server" Visible="false" Text=' <%#Eval("Val1")%>'></asp:Label>
                                                <asp:Label ID="lblProductDivision_SNO" runat="server" Visible="false" Text=' <%#Eval("ProductDivision_SNO")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="50px" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" HeaderText="Total Complaints">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkTotalComplaints" Visible="false" runat="server" CommandName="ProDetail"
                                                    Width="100px" Style="width: 300px; word-break: break-all"><%#Eval("TotalComplaints")%></asp:LinkButton>
                                                <asp:Label ID="lblTotalComplaints" Visible="false" runat="server" Style="width: 500px;
                                                    word-break: break-all" Text=' <%#Eval("TotalComplaints")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            HeaderText="Complaints In SLA">
                                            <ItemTemplate>
                                                <%#Eval("ComplaintsInSLA")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            HeaderText="Complaints Out SLA">
                                            <ItemTemplate>
                                                <%#Eval("ComplaintsOutSLA")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="500px" ItemStyle-Width="500px" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" HeaderText="Val2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVal2" runat="server" Style="width: 500px; word-break: break-all"
                                                    Text=' <%#Eval("Val2")%>'></asp:Label>
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
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Label ID="lblMessageDetail" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlProDetail" runat="server" Visible="false">
                                    <tr>
                                        <td>
                                            <b style="color: Red">Total Count:</b>
                                            <asp:Label ID="lblProDetail" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                                HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="True"
                                                PagerStyle-HorizontalAlign="Center" AllowSorting="False" AutoGenerateColumns="False"
                                                ID="gvProductDetail" runat="server" Width="100%" HorizontalAlign="Left">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-Width="150px" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left" HeaderText="Val1">
                                                        <ItemTemplate>
                                                            <%#Eval("Product")%>
                                                            <asp:Label ID="lblVal1" runat="server" Visible="false" Text=' <%#Eval("Product")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Width="500px" ItemStyle-Width="500px" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left" HeaderText="Serive Contrator">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblServiceContrator" runat="server" Style="width: 500px; word-break: break-all"
                                                                Text=' <%#Eval("SC_UserName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Width="50px" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left" HeaderText="Total Complaints">
                                                        <ItemTemplate>
                                                            <%#Eval("TotalComplaints")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                        HeaderText="Complaints In SLA">
                                                        <ItemTemplate>
                                                            <%#Eval("ComplaintsInSLA")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                        HeaderText="Complaints Out SLA">
                                                        <ItemTemplate>
                                                            <%#Eval("ComplaintsOutSLA")%>
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
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:Label ID="lblMessageProDetail" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
