<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" 
CodeFile="MIS6-ASC.aspx.cs" Inherits="Reports_MIS6_ASC" Title="MIS6-ASC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script type="text/javascript" language="javascript">
    function funMISComplaintLocal(Type,month,Week)
    {
        var varmonth=month.substring(0,3);
        var varYear =month.substring(4,6);
        var ddlRegion  = $get('ctl00_MainConHolder_ddlRegion');
        var Region_Sno = ddlRegion.options[ddlRegion.selectedIndex].value;
        
        var ddlBranch  = $get('ctl00_MainConHolder_ddlBranch');
        var Branch_Sno
        Branch_Sno= ddlBranch.options[ddlBranch.selectedIndex].value;      
        
        var ddlProductDivision = $get('ctl00_MainConHolder_ddlProductDivision');
        var ProductDiv_Sno;
        ProductDiv_Sno= ddlProductDivision.options[ddlProductDivision.selectedIndex].value;   
        
        var ddlSC = $get('ctl00_MainConHolder_ddlSC');
        var Sc_Sno=0;
         Sc_Sno= ddlSC.options[ddlSC.selectedIndex].value;                    
             
            
        funASCSummaryReport(Type,2,Region_Sno,Branch_Sno,ProductDiv_Sno,Sc_Sno,varYear,varmonth,Week);  
    }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <ContentTemplate>
            <table width="100%" border="0">
                <tr>
                    <td class="headingred" style="width: 40%">
                        MIS-6 Weekly Analysis of Resolution Time
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
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
                        <asp:DropDownList ID="ddlProductDivision" Width="175" runat="server" 
                            CssClass="simpletxt1" AppendDataBoundItems="True">
                            <asp:ListItem Selected ="True" Text ="All" Value ="0"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                 <tr>
                    <td align="right">
                        Service Contractor:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSC" Width="175" runat="server" CssClass="simpletxt1">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Date From:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFromYr" Width="52" runat="server" CssClass="simpletxt1">
                            <asp:ListItem Value="0">Year</asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlFromYr"
                                 Display="None" ErrorMessage="Select From Year" InitialValue="0" ValidationGroup="Date"
                                  SetFocusOnError="true" runat="server"></asp:RequiredFieldValidator>--%>
                        <asp:DropDownList ID="ddlFromMth" Width="59" runat="server" CssClass="simpletxt1">
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
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlFromMth"
                                 Display="None" ErrorMessage="Select From Month" InitialValue="0" ValidationGroup="Date"
                                  SetFocusOnError="true" runat="server"></asp:RequiredFieldValidator>--%>
                        <asp:DropDownList ID="ddlFromWk" Width="57" runat="server" CssClass="simpletxt1">
                            <asp:ListItem Value="0">Week</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlFromWk"
                                 Display="None" ErrorMessage="Select From Week" InitialValue="0" ValidationGroup="Date"
                                  SetFocusOnError="true" runat="server"></asp:RequiredFieldValidator>--%>
                        To
                        <asp:DropDownList ID="ddlToYr" Width="52" runat="server" CssClass="simpletxt1">
                            <asp:ListItem Value="0">Year</asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlToYr"
                                 Display="None" ErrorMessage="Select To Year" InitialValue="0" ValidationGroup="Date"
                                  SetFocusOnError="true" runat="server"></asp:RequiredFieldValidator>--%>
                        <asp:DropDownList ID="ddlToMth" Width="59" runat="server" CssClass="simpletxt1">
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
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="ddlToMth"
                                 Display="None" ErrorMessage="Select To Month" InitialValue="0" ValidationGroup="Date"
                                  SetFocusOnError="true" runat="server"></asp:RequiredFieldValidator>--%>
                        <asp:DropDownList ID="ddlToWk" Width="57" runat="server" CssClass="simpletxt1">
                            <asp:ListItem Value="0">Week</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="ddlToWk"
                                 Display="None" ErrorMessage="Select To Week" InitialValue="0" ValidationGroup="Date"
                                  SetFocusOnError="true" runat="server"></asp:RequiredFieldValidator>--%>
                        <%--<asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="Date" DisplayMode="BulletList"
                                     ShowMessageBox="true" ShowSummary="false" runat="server"/>--%>
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
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Total Received" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Total Received">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SUMMATOTREC','<%#Eval("loggeddate")%>','<%#Eval("Week")%>')">
                                            <%#Eval("Total Received")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Total Resolved" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Total Resolved">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SUMMATOTRSO','<%#Eval("loggeddate")%>','<%#Eval("Week")%>')">
                                            <%#Eval("Total Resolved")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="FIR Within 1 Day" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="FIR Within 1 Day">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SUMMAFIR1D','<%#Eval("loggeddate")%>','<%#Eval("Week")%>')">
                                            <%#Eval("FIR Within 1 Day")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Closure Within 1 Day"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Closure Within 1 Day">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SUMMACLO1D','<%#Eval("loggeddate")%>','<%#Eval("Week")%>')">
                                            <%#Eval("Closure Within 1 Day")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="FIR Within 2 Days" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="FIR Within 2 Days">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SUMMAFIR2D','<%#Eval("loggeddate")%>','<%#Eval("Week")%>')">
                                            <%#Eval("FIR Within 2 Days")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Closure Within 2 Days"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Closure Within 2 Days">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SUMMACLO2D','<%#Eval("loggeddate")%>','<%#Eval("Week")%>')">
                                            <%#Eval("Closure Within 2 Days")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="FIR Within 3&4 Days"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="FIR Within 3&4 Days">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SUMMAFIR3T4D','<%#Eval("loggeddate")%>','<%#Eval("Week")%>')">
                                            <%#Eval("FIR Within 3&4 Days")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Closure Within 3&4 Days"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Closure Within 3&4 Days">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SUMMACLO3T4D','<%#Eval("loggeddate")%>','<%#Eval("Week")%>')">
                                            <%#Eval("Closure Within 3&4 Days")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="FIR Within 5to7 Days"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="FIR Within 5to7 Days">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SUMMAFIR5T7D','<%#Eval("loggeddate")%>','<%#Eval("Week")%>')">
                                            <%#Eval("FIR Within 5to7 Days")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Closure Within 5to7 Days"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Closure Within 5to7 Days">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SUMMACLO5T7D','<%#Eval("loggeddate")%>','<%#Eval("Week")%>')">
                                            <%#Eval("Closure Within 5to7 Days")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="FIR more than 8 Days"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="FIR more than 8 Days">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SUMMAFIRMT8D','<%#Eval("loggeddate")%>','<%#Eval("Week")%>')">
                                            <%#Eval("FIR more than 8 Days")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Closure more than 8 Days"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Closure more than 8 Days">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SUMMACLOMT8D','<%#Eval("loggeddate")%>','<%#Eval("Week")%>')">
                                            <%#Eval("Closure more than 8 Days")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Pending FIR" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Pending FIR">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SUMMAFIRPEN','<%#Eval("loggeddate")%>','<%#Eval("Week")%>')">
                                            <%#Eval("Pending FIR")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Pending Closure" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Pending Closure">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SUMMACLOPEN','<%#Eval("loggeddate")%>','<%#Eval("Week")%>')">
                                            <%#Eval("Pending Closure")%></a>
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
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Total Received" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Total Received">
                                    <ItemTemplate>
                                        <%#Eval("Total Received")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Total Resolved" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Total Resolved">
                                    <ItemTemplate>
                                        <%#Eval("Total Resolved")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="FIR Within 1 Day" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="FIR Within 1 Day">
                                    <ItemTemplate>
                                        <%#Eval("FIR Within 1 Day")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Closure Within 1 Day"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Closure Within 1 Day">
                                    <ItemTemplate>
                                        <%#Eval("Closure Within 1 Day")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="FIR Within 2 Days" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="FIR Within 2 Days">
                                    <ItemTemplate>
                                        <%#Eval("FIR Within 2 Days")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Closure Within 2 Days"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Closure Within 2 Days">
                                    <ItemTemplate>
                                        <%#Eval("Closure Within 2 Days")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="FIR Within 3&4 Days"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="FIR Within 3&4 Days">
                                    <ItemTemplate>
                                        <%#Eval("FIR Within 3&4 Days")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Closure Within 3&4 Days"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Closure Within 3&4 Days">
                                    <ItemTemplate>
                                        <%#Eval("Closure Within 3&4 Days")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="FIR Within 5to7 Days"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="FIR Within 5to7 Days">
                                    <ItemTemplate>
                                        <%#Eval("FIR Within 5to7 Days")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Closure Within 5to7 Days"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Closure Within 5to7 Days">
                                    <ItemTemplate>
                                        <%#Eval("Closure Within 5to7 Days")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="FIR more than 8 Days"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="FIR more than 8 Days">
                                    <ItemTemplate>
                                        <%#Eval("FIR more than 8 Days")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Closure more than 8 Days"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Closure more than 8 Days">
                                    <ItemTemplate>
                                        <%#Eval("Closure more than 8 Days")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Pending FIR" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Pending FIR">
                                    <ItemTemplate>
                                        <%#Eval("Pending FIR")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Pending Closure" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Pending Closure">
                                    <ItemTemplate>
                                        <%#Eval("Pending Closure")%>
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


