<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true"
    CodeFile="RejectedClaimScreenASC.aspx.cs" Inherits="SIMS_Pages_RejectedClaimScreenASC" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script type="text/javascript">

//        function Openpopup(popurl)
//        {
//        winpops = window.open(popurl,"","width=1000, height=700, left=15, top=15, scrollbars=yes, menubar=no,resizable=no,directories=no,location=center")
//        }
    </script>

    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%" cellpadding="2" cellspacing="0">
                <tr>
                    <td class="headingred" style="width: 968px">
                        Rejected Claim Screen for ASC
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["SIMSAjaxLoadingImageName"]%>" alt="" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" colspan="2">
                        <table width="100%" border="0">
                            <tr id="tdSparereciept" runat="server">
                                <td width="100%" align="left" class="bgcolorcomm">
                                    <table width="98%" border="0">
                                        <%--<tr>
                                            <td colspan="4" align="right">
                                                <font color='red'>*</font>Mandatory Fields
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td style="width: 12%">
                                                ASC Name:
                                            </td>
                                            <td style="width: 47%">
                                                <asp:Label ID="lblASCName" runat="server"></asp:Label>
                                                <asp:DropDownList ID="ddlASCName" Visible="false" runat="server" CssClass="simpletxt1"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlASCName_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator InitialValue="0" ID="ReqASCName" runat="server" SetFocusOnError="true"
                                                    ErrorMessage="ASC Name is required." ControlToValidate="ddlASCName" ValidationGroup="editt"
                                                    ToolTip="ASC Name is required."></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                &nbsp;
                                                <asp:HiddenField ID="hdnASC_Id" runat="server" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Division:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlDivision" runat="server" CssClass="simpletxt1" Width="175px"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <%--<asp:RequiredFieldValidator InitialValue="0" ID="ReqDivision" runat="server" SetFocusOnError="true"
                                                    ErrorMessage="Division is required." ControlToValidate="ddlDivision" ValidationGroup="editt"
                                                    ToolTip="Division is required."></asp:RequiredFieldValidator>--%>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <table width="100%" border="0" cellspacing="1" cellpadding="2">
                                                    <tr>
                                                        <td>
                                                            <asp:GridView RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                                                HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="True"
                                                                AllowSorting="True" AutoGenerateColumns="False" ID="gvComm" runat="server" OnPageIndexChanging="gvComm_PageIndexChanging"
                                                                Width="100%" HorizontalAlign="Left" OnSorting="gvComm_Sorting" OnRowDataBound="gvComm_RowDataBound"
                                                                OnRowCommand="gvComm_RowCommand">
                                                                <RowStyle CssClass="gridbgcolor" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="Product_Division" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderText="Product Division">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="Complaint No.">
                                                                        <ItemTemplate>
                                                                            <%--<a href="<%#String.Format(&quot;javascript:Openpopup('SpareConsumptionActivity.aspx?complaintno={0}')&quot;,Eval(&quot;complaint_no&quot;)) %>"
                                                                                id="lnkDestination" name="lnkDestination" accesskey="lnkDestination" runat="server">
                                                                                <%#Eval("complaint_no")%></a>--%>
                                                                            <%--<asp:LinkButton ID="lnkcomplaint" runat="server" CausesValidation="false" CommandName="stage"
                                                                                Text='<%#Eval("complaint_no")%>'> </asp:LinkButton>--%>
                                                                                <asp:LinkButton ID="lnkcomplaint"  runat="server" CommandArgument='<%#Eval("BASELINEID")%>' CausesValidation="false" CommandName="stage"  Text='<%#Eval("complaint_no")%>'> </asp:LinkButton>   
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="claim_no" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderText="Claim No.">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Product_desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderText="Product">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="defect" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderText="Defects">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="amount" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderText="Amount(Material + Service)">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Rejected" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                        HeaderText="Rejected">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="ChkReject" runat="server" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkview" CausesValidation="false" runat="server" CommandName="complaintaction">View</asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
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
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="25" colspan="4" align="center">
                                                <table>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Button Text="SUBMIT" Width="70px" Visible="false" ID="imgBtnAdd" CssClass="btn"
                                                                runat="server" CausesValidation="True" ValidationGroup="editt" OnClick="imgBtnAdd_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="imgBtnCancel" Width="70px" Visible="false" runat="server" CausesValidation="false"
                                                                CssClass="btn" Text="Cancel" OnClick="imgBtnCancel_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
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
