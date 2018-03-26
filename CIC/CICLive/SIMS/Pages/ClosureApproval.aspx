<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClosureApproval.aspx.cs"
    Inherits="SIMS_Pages_ClosureApproval" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

<script language="javascript" type="text/javascript">
        function funComplainDetail(strUrl) {

            window.open(strUrl, "_blank", 'width=900,height=600,scrollbars=1,resizable=no,top=1,left=1');
            return false;
        }
</script>

<asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        DH-RSM Approval screen 
                    </td>
                </tr>
            </table>
        <table border="0" width="100%">
          <tr>
                                <td width="30%" align="right">
                                    Region
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlRegion" runat="server" Width="175px" CssClass="simpletxt1"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr >
                                <td width="30%" align="right">
                                    Branch
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlBranch" runat="server" Width="175px" CssClass="simpletxt1"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr  >
                                <td width="30%" align="right">
                                    ASC
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlASC" runat="server" Width="175px" CssClass="simpletxt1" ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr  >
                                <td width="30%" align="right">
                                    Division
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlProductDiv" runat="server" Width="175px" CssClass="simpletxt1" ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Date From
                                </td>
                                <td align="left">
                                    <asp:TextBox runat="server" ID="txtFromDate" CssClass="txtboxtxt" ValidationGroup="editt"
                                        MaxLength="10" />
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" Type="Date" Operator="DataTypeCheck"
                                        ControlToValidate="txtFromDate" Display="none" ValidationGroup="editt" SetFocusOnError="true"></asp:CompareValidator>
                                    <cc1:CalendarExtender ID="CEFrom" runat="server" TargetControlID="txtFromDate">
                                    </cc1:CalendarExtender>
                                    To
                                    <asp:TextBox runat="server" ID="txtToDate" CssClass="txtboxtxt" ValidationGroup="editt"
                                        MaxLength="10" />
                                    <asp:CompareValidator ID="CompareValidator7" runat="server" Type="Date" Operator="DataTypeCheck"
                                        ControlToValidate="txtToDate" Display="none" ValidationGroup="editt" SetFocusOnError="true"></asp:CompareValidator>
                                    <cc1:CalendarExtender ID="CETo" runat="server" TargetControlID="txtToDate">
                                    </cc1:CalendarExtender>
                                    <asp:CompareValidator ID="CompareValidator2" Type="Date" ControlToValidate="txtToDate"
                                        ControlToCompare="txtFromDate" Operator="GreaterThanEqual" runat="server" ErrorMessage="To Date should be greater than From Date"
                                        ValidationGroup="editt"></asp:CompareValidator>
                                    <asp:Label ID="lblDateErr" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr  >
                                <td align="right">
                                </td>
                                <td align="left">
                                    <br />
                                    <asp:Button Width="80px" Text="Search" CssClass="btn" ValidationGroup="editt" CausesValidation="true"
                                        ID="btnSearch" runat="server" OnClick="btnSearch_Click" />
                      
                                </td>
                            </tr>
            <tr>
                <td align="left" class="MsgTDCount">
                    Total Number of Records :
                    <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                </td>
                <td align="right">
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:GridView PageSize="5" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridLines="both" AllowPaging="True"
                        AllowSorting="True" AutoGenerateColumns="false" ID="gvConfirmation" runat="server"
                        Width="100%" HorizontalAlign="Left" OnPageIndexChanging="gvConfirmation_PageIndexChanging"
                        OnRowDataBound="gvConfirmation_RowDataBound">
                        <RowStyle CssClass="gridbgcolor" />
                        <Columns>
                            <asp:TemplateField HeaderText="" HeaderStyle-Width="150px">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <HeaderStyle Width="150px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="SC_Name" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left" HeaderText="Service Contractor">
                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ProductDivision_Desc" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left" HeaderText="Product Division">
                                <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderStyle-Width="60px" HeaderText="Complaint No" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <a href="#" onclick='return funComplainDetail("../../pages/PopUp.aspx?BaseLineId=<%#Eval("BaseLineId")%>")'>
                                        <%#Eval("Complaint_Split")%>
                                    </a>
                                    <asp:HiddenField ID="hdnLabelcomplaint" runat="server" Value='<%#Eval("Complaint_Split")%>' />
                                </ItemTemplate>
                                <HeaderStyle Width="60px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                           <asp:BoundField DataField="Product_Desc" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left" HeaderText="Product">
                                <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                               <asp:BoundField DataField="spares" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Spares">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                           <asp:TemplateField HeaderStyle-Width="60px" HeaderText="Approval Flag">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkActivityConfirm" AutoPostBack="true" runat="server" 
                                        oncheckedchanged="chkActivityConfirm_CheckedChanged" />
                                </ItemTemplate>
                                <HeaderStyle Width="60px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="150px" HeaderText="Comment">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtComment" Enabled="false" Height="30px" runat="server" Width="200"
                                        CssClass="txtboxtxt" TextMode="MultiLine" ></asp:TextBox>
                                    <asp:Label ID="lblComment" runat="server" ></asp:Label>
                               </ItemTemplate>
                                <HeaderStyle Width="150px" />
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
                    <asp:Button ID="btnSave" Width="70px" runat="server" CssClass="btn" Text="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" Width="70px" runat="server" CssClass="btn" Text="Cancel" />
                </td>
            </tr>
        </table>
   </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
