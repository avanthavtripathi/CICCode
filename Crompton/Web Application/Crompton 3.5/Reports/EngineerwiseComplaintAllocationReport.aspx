<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="EngineerwiseComplaintAllocationReport.aspx.cs" Inherits="Reports_EngineerwiseComplaintAllocationReport" Title="" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    <ContentTemplate>
        <table width="100%" border="0">
            <tr>
                <td style="width:40%" class="headingred">
                    Service EngineerWise Complaint Allocation Report
                </td>
                <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"] %>" style="padding-right: 10px;">
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"] %>" alt="" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>
            </tr>
            <tr>
                <td align="right">Region:</td>
                <td><asp:DropDownList ID="ddlRegion" runat="server" 
                CssClass="simpletxt1" Width="175" AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged"></asp:DropDownList></td>
            </tr>
            <tr>
                <td align="right">Branch:</td>
                <td><asp:DropDownList ID="ddlBranch" runat="server" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                CssClass="simpletxt1" Width="175" AutoPostBack="true"></asp:DropDownList></td>
            </tr>
            <tr>
                <td align="right">Product Division:</td>
                <td><asp:DropDownList ID="ddlProductDivision" runat="server" 
                CssClass="simpletxt1" Width="175" ></asp:DropDownList></td>
            </tr>
            <tr>
                <td align="right">Service Contractor:</td>
                <td><asp:DropDownList ID="ddlServiceContractor" runat="server" OnSelectedIndexChanged="ddlServiceContractor_SelectedIndexChanged"
                CssClass="simpletxt1" Width="175" AutoPostBack="true"></asp:DropDownList></td>
            </tr>
            <tr>
                <td align="right">Service Engineer:</td>
                <td><asp:DropDownList ID="ddlServiceEngineer" runat="server" 
                CssClass="simpletxt1" Width="175" AutoPostBack="true"></asp:DropDownList></td>
            </tr>
            <tr>
                <td align="right">Date From:</td>
                <td><asp:DropDownList ID="ddlFromYr" runat="server" 
                CssClass="simpletxt1" Width="52" AutoPostBack="false">
                 <asp:ListItem Value="0">Year</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlFromMth" runat="server" 
                CssClass="simpletxt1" Width="59" AutoPostBack="false">
                <asp:ListItem Value="0">Month</asp:ListItem>
                            <asp:ListItem Value="01">Jan</asp:ListItem>
                            <asp:ListItem Value="02">Feb</asp:ListItem>
                            <asp:ListItem Value="03">Mar</asp:ListItem>
                            <asp:ListItem Value="04">Apr</asp:ListItem>
                            <asp:ListItem Value="05">May</asp:ListItem>
                            <asp:ListItem Value="06">Jun</asp:ListItem>
                            <asp:ListItem Value="07">Jul</asp:ListItem>
                            <asp:ListItem Value="08">Aug</asp:ListItem>
                            <asp:ListItem Value="09">Sep</asp:ListItem>
                            <asp:ListItem Value="10">Oct</asp:ListItem>
                            <asp:ListItem Value="11">Nov</asp:ListItem>
                            <asp:ListItem Value="12">Dec</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlFromWk" runat="server" 
                CssClass="simpletxt1" Width="57" AutoPostBack="false">
                 <asp:ListItem Value="0">Week</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                </asp:DropDownList>
                To
                <asp:DropDownList ID="ddlToYr" runat="server" 
                CssClass="simpletxt1" Width="52" AutoPostBack="false">
                <asp:ListItem Value="0">Year</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlToMth" runat="server" 
                CssClass="simpletxt1" Width="59" AutoPostBack="false">
                 <asp:ListItem Value="0">Month</asp:ListItem>
                            <asp:ListItem Value="01">Jan</asp:ListItem>
                            <asp:ListItem Value="02">Feb</asp:ListItem>
                            <asp:ListItem Value="03">Mar</asp:ListItem>
                            <asp:ListItem Value="04">Apr</asp:ListItem>
                            <asp:ListItem Value="05">May</asp:ListItem>
                            <asp:ListItem Value="06">Jun</asp:ListItem>
                            <asp:ListItem Value="07">Jul</asp:ListItem>
                            <asp:ListItem Value="08">Aug</asp:ListItem>
                            <asp:ListItem Value="09">Sep</asp:ListItem>
                            <asp:ListItem Value="10">Oct</asp:ListItem>
                            <asp:ListItem Value="11">Nov</asp:ListItem>
                            <asp:ListItem Value="12">Dec</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlToWk" runat="server" 
                CssClass="simpletxt1" Width="57" AutoPostBack="false">
                 <asp:ListItem Value="0">Week</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                </asp:DropDownList>
                </td>
            </tr>
             <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="Search" Width="100"
                            OnClick="btnSearch_Click" ValidationGroup="Date" />
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
                        <asp:GridView ID="gvMIS" CssClass="simpletxt1" runat="server" RowStyle-CssClass="bgcolorcomm"
                            Width="100%" AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                            GridGroups="both" AllowPaging="false" PagerStyle-HorizontalAlign="Center" AllowSorting="True"
                            AutoGenerateColumns="False" HorizontalAlign="Left" OnPageIndexChanging="gvMIS_PageIndexChanging"
                            OnSorting="gvMIS_Sorting">
                            <Columns>
                                <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                    <HeaderStyle Width="40px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="loggeddate" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Month">
                                    <ItemTemplate>
                                        <%#Eval("loggeddate")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Week" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Week">
                                    <ItemTemplate>
                                        <%#Eval("Week")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Branch_Name" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Branch">
                                    <ItemTemplate>
                                            <%#Eval("Branch_Name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="ProductDivision_Desc" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Product Division">
                                    <ItemTemplate>
                                            <%#Eval("ProductDivision_Desc")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="SC_Name" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Service Contractor">
                                    <ItemTemplate>
                                            <%#Eval("SC_Name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="SE_Name" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Service Engineer">
                                    <ItemTemplate>
                                            <%#Eval("SE_Name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Total Allocated" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Total No Of Calls Allocated to Service Engineer">
                                    <ItemTemplate>
                                            <%#Eval("Total Allocated")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Total Attented" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Total No Of Calls attended by Service Engineer-(FIR DONE)">
                                    <ItemTemplate>
                                            <%#Eval("Total Attented")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Total Attented With in 1 Day" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Total No Of Calls attended by Service Engineer FIR DONE within 1 day">
                                    <ItemTemplate>
                                            <%#Eval("Total Attented With in 1 Day")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Total Attented With in 2 to 5 Days" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Total No Of Calls attended by Service Engineer FIR DONE within 2-5 days">
                                    <ItemTemplate>
                                            <%#Eval("Total Attented With in 2 to 5 Days")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Total Attented With in 5 Days"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Total No Of Calls attended by Service Engineer FIR DONE within5>days">
                                    <ItemTemplate>
                                            <%#Eval("Total Attented With in 5 Days")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Total Closed With" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Total No Of Calls attended are closed by Service Engineer">
                                    <ItemTemplate>
                                            <%#Eval("Total Closed With")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Total Closed With in 1 Day" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Total No Of Calls attended are closed by Service Engineer Within 1 day">
                                    <ItemTemplate>
                                            <%#Eval("Total Closed With in 1 Day")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Total Closed With in 2 to 5 Days"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Total No Of Calls attended are closed by Service Engineer Within 2-5 days">
                                    <ItemTemplate>
                                            <%#Eval("Total Closed With in 2 to 5 Days")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Total Closed With in 5 Days"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Total No Of Calls attended are closed by Service Engineer Within >5 days">
                                    <ItemTemplate>
                                            <%#Eval("Total Closed With in 5 Days")%>
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
                    <td colspan="2">
                        <asp:GridView ID="gvExport" CssClass="simpletxt1" runat="server" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                    <HeaderStyle Width="40px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="loggeddate" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Month">
                                    <ItemTemplate>
                                        <%#Eval("loggeddate")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Week" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Week">
                                    <ItemTemplate>
                                        <%#Eval("Week")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Branch_Name" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Branch">
                                    <ItemTemplate>
                                            <%#Eval("Branch_Name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="ProductDivision_Desc" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Product Division">
                                    <ItemTemplate>
                                            <%#Eval("ProductDivision_Desc")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="SC_Name" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Service Contractor">
                                    <ItemTemplate>
                                            <%#Eval("SC_Name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="SE_Name" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Service Engineer">
                                    <ItemTemplate>
                                            <%#Eval("SE_Name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Total Allocated" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Total No Of Calls Allocated to Service Engineer">
                                    <ItemTemplate>
                                            <%#Eval("Total Allocated")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Total Attented" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Total No Of Calls attended by Service Engineer-(FIR DONE)">
                                    <ItemTemplate>
                                            <%#Eval("Total Attented")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Total Attented With in 1 Day" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Total No Of Calls attended by Service Engineer FIR DONE within 1 day">
                                    <ItemTemplate>
                                            <%#Eval("Total Attented With in 1 Day")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Total Attented With in 2 to 5 Days" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Total No Of Calls attended by Service Engineer FIR DONE within 2-5 days">
                                    <ItemTemplate>
                                            <%#Eval("Total Attented With in 2 to 5 Days")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Total Attented With in 5 Days"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Total No Of Calls attended by Service Engineer FIR DONE within5>days">
                                    <ItemTemplate>
                                            <%#Eval("Total Attented With in 5 Days")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Total Closed With" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Total No Of Calls attended are closed by Service Engineer">
                                    <ItemTemplate>
                                            <%#Eval("Total Closed With")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Total Closed With in 1 Day" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Total No Of Calls attended are closed by Service Engineer Within 1 day">
                                    <ItemTemplate>
                                            <%#Eval("Total Closed With in 1 Day")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Total Closed With in 2 to 5 Days"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Total No Of Calls attended are closed by Service Engineer Within 2-5 days">
                                    <ItemTemplate>
                                            <%#Eval("Total Closed With in 2 to 5 Days")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Total Closed With in 5 Days"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Total No Of Calls attended are closed by Service Engineer Within >5 days">
                                    <ItemTemplate>
                                            <%#Eval("Total Closed With in 5 Days")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

