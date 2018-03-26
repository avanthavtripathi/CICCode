<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="MIS-SBSDaily.aspx.cs" Inherits="Reports_MIS_SBSDaily" Title="MIS-SBSDaily Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function funReqDetail(strUrl) {

            window.open(strUrl, 'History', 'height=700,width=1050,left=20,top=30,Location=0,scrollbars=yes');
            return false;
        }

        //        function CalculateWeek() {
        //            debugger
        //            var date = new Date();
        //            var Week = document.getElementById("ctl00_MainConHolder_txtComplaintWeek").value;
        //            var ddlMonth = document.getElementById("ctl00_MainConHolder_ddlMonth").value;
        //            prefixes = ['1', '2', '3', '4', '5'];
        //            alert(prefixes[0 | date.getDate() / 7]);
        //        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="text-align: center">
                <tr>
                    <td colspan="2">
                        <asp:RadioButtonList ID="rblComplaintStatus" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                            OnSelectedIndexChanged="rblWeseReport_SelectedIndexChanged">
                            <asp:ListItem Text="Daily Basis" Selected="True" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Weekly Basis" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Monthly Basis" Value="2"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <div runat="server" id="divMonth" visible="false">
                            <table>
                                <tr>
                                    <td align="right" style="width: 100px">
                                        Year<font color='red'>*</font>
                                    </td>
                                    <td align="right">
                                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="simpletxt1" ValidationGroup="search">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 100px">
                                        Month<font color='red'>*</font>
                                    </td>
                                    <td align="right">
                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="simpletxt1" ValidationGroup="search">
                                            <asp:ListItem Selected="True" Text="<Select>" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="February" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="June" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                            <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                            <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                            <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                            <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                            <asp:ListItem Text="December" Value="12"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvMonth" runat="server" ErrorMessage="Required."
                                            ValidationGroup="search" InitialValue="0" Display="Dynamic" SetFocusOnError="true"
                                            ControlToValidate="ddlMonth"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2">
                        <div runat="server" id="divComplaintStatus">
                            <table>
                                <tr>
                                    <td align="left">
                                        Complaint Date<font color='red'>*</font>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtComplaintDate" CssClass="txtboxtxt" ValidationGroup="search" />
                                        <cc1:CalendarExtender ID="CalendarExtender1" Format="MM/dd/yyyy" runat="server" TargetControlID="txtComplaintDate">
                                        </cc1:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtComplaintDate"
                                            Display="Dynamic" ErrorMessage="Required." SetFocusOnError="True" ValidationGroup="search"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtComplaintDate"
                                            runat="server" ErrorMessage="Enter correct date." ValidationExpression="^\d{1,2}(\-|\/|\.)\d{1,2}\1\d{4}$"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div runat="server" id="divCompalintWeek" visible="false">
                            <table>
                                <tr>
                                    <td align="left">
                                        Complaint Week<font color='red'>*</font>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlWeek" runat="server" CssClass="simpletxt1" ValidationGroup="search">
                                            <asp:ListItem Selected="True" Text="<Select>" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlWeek"
                                            Display="Dynamic" ErrorMessage="Required." InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="search"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="ddlWeek"
                                            Display="Dynamic" ErrorMessage="Enter Correct Week." MaximumValue="5" MinimumValue="1"
                                            SetFocusOnError="True" Type="Integer" ValidationGroup="search"></asp:RangeValidator>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
               
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="Search" Width="80"
                            OnClick="btnSearch_Click" ValidationGroup="search" />
                    </td>
                </tr>
              
            </table>
            <%--<table>
                <tr>
                    <td>
                        <asp:GridView ID="gvMISComplaints" CssClass="simpletxt1" runat="server" RowStyle-CssClass="bgcolorcomm"
                            Width="100%" AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                            GridGroups="both" AllowPaging="True" PagerStyle-HorizontalAlign="Center" AllowSorting="True"
                            AutoGenerateColumns="False" HorizontalAlign="Left" OnPageIndexChanging="gvMISComplaints_PageIndexChanging"
                            OnSorting="gvMISComplaints_Sorting">
                            <RowStyle CssClass="bgcolorcomm" />
                            <Columns>
                                <asp:TemplateField HeaderText="SNo">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="CallType" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Call Type">
                                    <ItemTemplate>
                                        <%#Eval("CallType")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="CustomerID" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="CustomerID">
                                    <ItemTemplate>
                                        <%#Eval("CustomerID")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="ComplaintRefNo" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Complaint RefNo">
                                    <ItemTemplate>
                                        <%#Eval("ComplaintRefNo")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="ContactNo" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Contact No">
                                    <ItemTemplate>
                                        <%#Eval("ContactNo")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="RegisteredBy" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Registered By">
                                    <ItemTemplate>
                                        <%#Eval("RegisteredBy")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderStyle-Width="100px" SortExpression="RegisteredDate" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Registered Date">
                                    <ItemTemplate>
                                        <%#Eval("RegisteredDate")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Center" />
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
                            <HeaderStyle CssClass="fieldNamewithbgcolor" />
                            <AlternatingRowStyle CssClass="fieldName" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>--%>
            <div runat="server" id="divComplaints" visible="false">
             <table>
                <tr>
                    <td>
                        <asp:Label ID="Label2" Text="Complaint Registration:" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                      <b>Total Count:</b>  <asp:Label ID="lblCount1"  runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvMISComplaintsCat1" Width="600px" CssClass="simpletxt1" 
                            runat="server" RowStyle-CssClass="bgcolorcomm"
                             AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                            GridGroups="both" PagerStyle-HorizontalAlign="Center" AllowSorting="True" AutoGenerateColumns="False"
                            HorizontalAlign="Left" AllowPaging="True" 
                            onpageindexchanging="gvMISComplaintsCat1_PageIndexChanging" 
                            onsorting="gvMISComplaintsCat1_Sorting">
                            <RowStyle CssClass="bgcolorcomm" />
                            <Columns>
                                <asp:TemplateField HeaderText="SNo">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="RegisteredBy" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Registered By">
                                    <ItemTemplate>
                                        <%#Eval("RegisteredBy")%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="120px" SortExpression="cntFreshComplaints"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Fresh Complaints">
                                    <ItemTemplate>
                                        <%#Eval("cntFreshComplaints")%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="150px" SortExpression="cntFreshComplaintsE"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Fresh Complaints<br />(Existing Customers)">
                                    <ItemTemplate>
                                        <%#Eval("cntFreshComplaintsE")%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="150px" SortExpression="cntRepeatedComplaint"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Repeated Complaint">
                                    <ItemTemplate>
                                        <%#Eval("cntRepeatedComplaint")%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderStyle-Width="50px" SortExpression="TotalCount"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Total">
                                    <ItemTemplate>
                                        <%#Eval("TotalCount")%>
                                    </ItemTemplate>
                                     <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                     <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Center" />
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
                            <HeaderStyle CssClass="fieldNamewithbgcolor" />
                            <AlternatingRowStyle CssClass="fieldName" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <br />
             <br />
              <br />
               <br />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label1" Text="Escalated Calls:" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                      <b>Total Count:</b>  <asp:Label ID="lblcount2"  runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvMISComplaintsCat2" CssClass="simpletxt1" runat="server" RowStyle-CssClass="bgcolorcomm"
                            Width="600px" AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                            GridGroups="both" PagerStyle-HorizontalAlign="Center" AllowSorting="True" AutoGenerateColumns="False"
                            HorizontalAlign="Left" AllowPaging="True" 
                            onpageindexchanging="gvMISComplaintsCat2_PageIndexChanging" 
                            onsorting="gvMISComplaintsCat2_Sorting">
                            <RowStyle CssClass="bgcolorcomm" />
                            <Columns>
                                <asp:TemplateField HeaderText="SNo">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="RegisteredBy" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Registered By">
                                    <ItemTemplate>
                                        <%#Eval("RegisteredBy")%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="160px" SortExpression="ComplaintRegistration"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Complaint Registration">
                                    <ItemTemplate>
                                        <%#Eval("ComplaintRegistration")%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="160px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="80px" SortExpression="CheckCallStatus" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Call Status">
                                    <ItemTemplate>
                                        <%#Eval("CheckCallStatus")%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="80px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="150px" SortExpression="OutOfScopeCalls" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="OutOfScope Calls">
                                    <ItemTemplate>
                                        <%#Eval("OutOfScopeCalls")%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderStyle-Width="50px" SortExpression="TotalCount"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Total">
                                    <ItemTemplate>
                                        <%#Eval("TotalCount")%>
                                    </ItemTemplate>
                                     <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                     <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Center" />
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
                            <HeaderStyle CssClass="fieldNamewithbgcolor" />
                            <AlternatingRowStyle CssClass="fieldName" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
              <br />
             <br />
              <br />
               <br />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblCat3" Text="Out of Scope:" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                      <b>Total Count:</b>  <asp:Label ID="lblCount3"  runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvMISComplaintsCat3" CssClass="simpletxt1" runat="server" RowStyle-CssClass="bgcolorcomm"
                            Width="600px" AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                            GridGroups="both" PagerStyle-HorizontalAlign="Center" AllowSorting="True" AutoGenerateColumns="False"
                            HorizontalAlign="Left" AllowPaging="True" 
                            onpageindexchanging="gvMISComplaintsCat3_PageIndexChanging" 
                            onsorting="gvMISComplaintsCat3_Sorting">
                            <RowStyle CssClass="bgcolorcomm" />
                            <Columns>
                                <asp:TemplateField HeaderText="SNo">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="RegisteredBy" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Registered By">
                                    <ItemTemplate>
                                        <%#Eval("RegisteredBy")%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="150px" SortExpression="GeneralEnquiry" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="General Enquiry">
                                    <ItemTemplate>
                                        <%#Eval("GeneralEnquiry")%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="80px" SortExpression="SalesCalls" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Sales Calls">
                                    <ItemTemplate>
                                        <%#Eval("SalesCalls")%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="80px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="80px" SortExpression="ClientCalls" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Client Calls">
                                    <ItemTemplate>
                                        <%#Eval("ClientCalls")%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="80px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="150px" SortExpression="NonCgCustomer" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Non-Cg Customer">
                                    <ItemTemplate>
                                        <%#Eval("NonCgCustomer")%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderStyle-Width="50px" SortExpression="TotalCount"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Total">
                                    <ItemTemplate>
                                        <%#Eval("TotalCount")%>
                                    </ItemTemplate>
                                     <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                     <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Center" />
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
                            <HeaderStyle CssClass="fieldNamewithbgcolor" />
                            <AlternatingRowStyle CssClass="fieldName" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            </div>
           
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
