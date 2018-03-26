<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="ComplaintClosureReport.aspx.cs" Inherits="Reports_ComplaintClosureReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="ComplainClosure" ContentPlaceHolderID="MainConHolder" runat="Server">
<script language="javascript" type="text/javascript">
         function funUserDetail(custNo, compNo)
        {
            var strUrl='CustomerDetail.aspx?custNo='+ custNo + '&CompNo='+ compNo;
            window.open(strUrl,'History','height=550,width=750,left=20,top=30,Location=0');
        }
         function funReqDetail(compNo)
        {
            var strUrl='ComplaintDetailPopUp.aspx?compNo='+ compNo;
            window.open(strUrl,'History','height=550,width=750,left=20,top=30,Location=0');
        }
         function funSCDetail(SCNo)
        {
            var strUrl='../Pages/SCPopup.aspx?scno='+ SCNo + '&type=display';
            window.open(strUrl,'History','height=550,width=750,left=20,top=30,Location=0');
        }
    </script>
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        MIS Complaint Closure Report
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
                           
                            <tr>
                                <td width="30%" align="right">
                                    Select Region
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlRegion" runat="server" Width="175px" CssClass="simpletxt1"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Select Branch
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlBranch" runat="server" Width="175px" CssClass="simpletxt1"
                                         ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Select Product Divison
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlProductDivison" runat="server" Width="175px" CssClass="simpletxt1"
                                        ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Date From
                                </td>
                                <td align="left">
                                    <asp:TextBox runat="server" ID="txtFromDate" CssClass="txtboxtxt" ValidationGroup="editt"
                                        AutoPostBack="True" />
                                    <cc1:CalendarExtender ID="CEFrom" runat="server" TargetControlID="txtFromDate">
                                    </cc1:CalendarExtender>
                                    To
                                    <asp:TextBox runat="server" ID="txtToDate" CssClass="txtboxtxt" ValidationGroup="editt"
                                        AutoPostBack="True" />
                                    <cc1:CalendarExtender ID="CETo" runat="server" TargetControlID="txtToDate">
                                    </cc1:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">Complaint No</td>
                                <td align="left">
                                    <asp:TextBox runat="server" ID="txtReqNo" CssClass="txtboxtxt" ValidationGroup="editt"/>&nbsp;&nbsp;
                                    <asp:Button Width="70px" Text="Search" CssClass="btn" ValidationGroup="editt" ID="btnSearch" runat="server" OnClick="btnSearch_Click" />
                                    
                                </td>
                            </tr>
                            <tr>
                                   
                                       <td align="left" class="MsgTDCount">
                                            Total Number of Records : <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                        </td>
                                        <td></td>
                                   
                                    
                            </tr>
                           
                        </table>
                        <br />
                        <table width="100%" border="0">
                            <tr>
                                <td>
                                    <!-- Action Listing -->
                                    <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="True" PagerStyle-HorizontalAlign="Left" 
                                        AllowSorting="True" AutoGenerateColumns="False" ID="gvComm"
                                        runat="server" OnPageIndexChanging="gvComm_PageIndexChanging" Width="100%" 
                                        HorizontalAlign="Left"  Visible="true" OnSorting="gvComm_Sorting">
                                        <Columns>
                                            <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                <HeaderStyle Width="40px" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Complaint_RefNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                HeaderText="Complaint No">
                                                <ItemTemplate>
                                                    <a href="Javascript:void(0);" onclick="funReqDetail('<%#Eval("Complaint_RefNo")%>')">
                                                        <%#Eval("Complaint_RefNo")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="100px" SortExpression="CustomerId" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                HeaderText="Customer">
                                                <ItemTemplate>
                                                    <a href="Javascript:void(0);" onclick="funUserDetail('<%#Eval("CustomerId")%>','<%#Eval("Complaint_RefNo")%>')">
                                                        <%#Eval("CustomerId")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                             <asp:BoundField DataField="SLA_Date" SortExpression="SLA_Date" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="SLA Start Date">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            
                                            <asp:TemplateField HeaderStyle-Width="120px" SortExpression="SC_Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                HeaderText="Service Contractor">
                                                <ItemTemplate>
                                                    <a href="Javascript:void(0);" onclick="funSCDetail('<%#Eval("SC_SNo")%>')">
                                                        <%#Eval("SC_Name")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                           <%--  <asp:BoundField DataField="SC_Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Service Contractor">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>--%>
                                            <asp:BoundField DataField="StageDesc" SortExpression="StageDesc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Complaint Status">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Product_Code"  SortExpression="Product_Code" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Product Code">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProductDivision_Desc" SortExpression="ProductDivision_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Product Division">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Quantity" SortExpression="Quantity" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Quantity">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NatureOfComplaint" SortExpression="NatureOfComplaint" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="NatureOfComplaint">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>                                            
                                        </Columns>
                                    </asp:GridView>
                                    <!-- End Action Listing -->
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
