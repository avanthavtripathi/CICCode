<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true"
    CodeFile="DeletedActivityReport.aspx.cs" Inherits="SIMS_Reports_DeletedActivityReport"  Title="Deleted Activity Report" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server" >
<ContentTemplate>
    <table width="100%">
        <tr>
            <td class="headingred">
             Deleted Activity
            </td>
            <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                    <ProgressTemplate>
                        <img src="<%=ConfigurationManager.AppSettings["SIMSAjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                </asp:UpdateProgress>
            </td>
        </tr>
        <tr>
            <td style="padding: 10px" align="center" colspan="2">
                <table border="0" width="100%">
                    <tr>
                        <td align="left" class="MsgTDCount">
                            Total Number of Records :
                            <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                        </td>
                    </tr>
                </table>
            
                 <table width="100%" border="0">
                    <tr>
                        <td width="100%" align="left" class="bgcolorcomm">
                            <table width="98%" border="0">
                                <tr>
                                    <td colspan="2" align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                        <font color='red'>*</font>
                                        <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td width="30%" align="right">
                                        Region<font color='red'>*</font>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlRegion" AutoPostBack="true" runat="server" CssClass="simpletxt1"
                                            Width="175px" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvRegion" runat="server" SetFocusOnError="true"
                                            ErrorMessage="Branch is required." InitialValue="0" ControlToValidate="ddlRegion"
                                            ValidationGroup="editt" ToolTip="Branch is required." Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="30%" align="right">
                                        Branch<font color='red'>*</font>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="simpletxt1" Width="175px"
                                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" SetFocusOnError="true"
                                            ErrorMessage="Branch is required." InitialValue="0" ControlToValidate="ddlBranch"
                                            ValidationGroup="editt" ToolTip="Branch is required." Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="30%" align="right">
                                        Service Contractor<font color='red'>*</font>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DDlAsc" runat="server" CssClass="simpletxt1" Width="175px">
                                         <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem> 
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="30%" align="right">
                                        &nbsp;Data Range(Deletion)</td>
                                    <td align="left">
                                      <asp:TextBox runat="server" ID="txtFromDate" CssClass="txtboxtxt" ValidationGroup="ed"  MaxLength="10" />
                                      <asp:RequiredFieldValidator ID="reffrom" runat="server" Text="*" ControlToValidate="txtFromDate" SetFocusOnError="true" ValidationGroup="ed"> </asp:RequiredFieldValidator>
                                      <cc1:CalendarExtender ID="CEFrom" runat="server" TargetControlID="txtFromDate">
                                      </cc1:CalendarExtender>
                                    To
                                    <asp:TextBox runat="server" ID="txtToDate" CssClass="txtboxtxt" ValidationGroup="ed"
                                        MaxLength="10" />
                                        <asp:RequiredFieldValidator ID="refto" runat="server" Text="*" ControlToValidate="txtToDate" SetFocusOnError="true" ValidationGroup="ed"> </asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CETo" runat="server" TargetControlID="txtToDate">
                                    </cc1:CalendarExtender>
                                 </td>
                                </tr>
                                <tr>
                                    <td width="30%" align="right">
                                        &nbsp;</td>
                                    <td align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td height="25" align="left">
                                        &nbsp;
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td align="right">
                                                   <asp:Button Text="Search" Width="70px" ID="btnSearch" CssClass="btn" runat="server"
                                                        CausesValidation="True" ValidationGroup="editt" OnClick="btnSearch_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="imgBtnCancel" Width="70px" runat="server" CausesValidation="false"
                                                        CssClass="btn" Text="Cancel" Visible="False" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                    HeaderStyle-CssClass="fieldNamewithbgcolor" GridLines="both" 
                                    ID="gv" runat="server" Width="100%" HorizontalAlign="Left" >
                                    <RowStyle CssClass="gridbgcolor" />
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
                    </table>
        
                
            </td>
        </tr>
    </table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
