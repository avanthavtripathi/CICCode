<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DestructibleSpareAndServiceActivityList.aspx.cs"
    Inherits="SIMS_Pages_DestructibleSpareAndServiceActivityList" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        window.onload = function() {
            Counter = 0;
        }
        function SetCounter() {
            Counter = 0;
        }

        function ChildClick(CheckBox) {
            var HeaderCheckBox = document.getElementById("ctl00_MainConHolder_gvChallanDetail_ctl01_chkHeader");
            TotalChkBx = document.getElementById("ctl00_MainConHolder_gvChallanDetail").rows.length;

            TotalChkBx = TotalChkBx - 2;
            if (CheckBox.checked && Counter < TotalChkBx) {
                Counter++;
            }
            else if (CheckBox.checked && Counter == 0)
            { Counter++; }
            else if (Counter > 0)
            { Counter--; }
            if (Counter < TotalChkBx) {
                HeaderCheckBox.checked = false;
            }
            else if (Counter == TotalChkBx) {
                HeaderCheckBox.checked = true;
            }
        }

        function SelectAllCheckboxes(spanChk) {
            TotalChkBx = document.getElementById("ctl00_MainConHolder_gvChallanDetail").rows.length;
            TotalChkBx = TotalChkBx - 1;
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;
            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
                if (elm[i].checked != xState)
                    elm[i].click();
            }
        }
    </script>

    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred" colspan="2">
                        Destructible Spare List(Return Flag=False)
                    </td>
                </tr>
            </table>
            <table width="100%" class="bgcolorcomm">
                <tr id="trASC" runat="server" visible="false">
                    <td width="8%">
                        ASC:
                    </td>
                    <td>
                        <asp:Label ID="lblASC" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="trCGEIC" runat="server">
                    <td>
                        ASC:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlASC" runat="server" CssClass="simpletxt1" AutoPostBack="True"
                            Width="206px" OnSelectedIndexChanged="ddlASC_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Division:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="simpletxt1" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" Width="206px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Total Number of Records :
                        <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvChallanDetail" runat="server" AllowPaging="True" AllowSorting="True"
                            AlternatingRowStyle-CssClass="fieldName" AutoGenerateColumns="False" EnableSortingAndPagingCallbacks="True"
                            GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor" HorizontalAlign="Left"
                            PageSize="50" RowStyle-CssClass="gridbgcolor" Width="100%" OnPageIndexChanging="gvChallanDetail_PageIndexChanging">
                            <RowStyle CssClass="gridbgcolor" />
                            <Columns>
                                <asp:TemplateField HeaderText="SNo.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldefid" runat="server" Text='<%#Eval("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Service Contractor" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSCName" runat="server" Text='<%#Eval("ServiceContractor") %>'></asp:Label>
                                        <asp:HiddenField ID="hndSCNo" runat="server" Value='<%#Eval("Asc_Id") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Division" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductdivision" runat="server" Text='<%#Eval("Productdivision") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Complaint No./Split Complaint Refno" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblcomplaint" CommandName="stage" runat="server" CommandArgument='<%#Eval("BASELINEID")%>'
                                            CausesValidation="false" Text='<%#Eval("complaint_no") %>' OnClick="lblcomplaint_Click">LinkButton</asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="spare" HeaderText="Spare" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblspareid" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Parameter1" HeaderText="Parameter" ItemStyle-HorizontalAlign="Left"
                                    Visible="false">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField Visible="false" DataField="Possiblevalue1" HeaderText="Possible Value1"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField Visible="false" DataField="Parameter2" HeaderText="Parameter2" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField Visible="false" DataField="Possiblevalue2" HeaderText="Possible Value2"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField Visible="false" DataField="Parameter3" HeaderText="Parameter3" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField Visible="false" DataField="Possiblevalue3" HeaderText="Possible Value3"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField Visible="false" DataField="Parameter4" HeaderText="Parameter4" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField Visible="false" DataField="Possiblevalue4" HeaderText="Possible Value4"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Qty" HeaderText="Qty" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Destroy Flag" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="8">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldestroyed" runat="server" Text='<%#Eval("Destroy_Flag") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="8px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Center"
                                    Visible="false">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Center"
                                    Visible="false">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkHeader" runat="server" onclick="javascript:SelectAllCheckboxes(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" onclick="javascript:ChildClick(this);" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                            <img alt="" src='<%=ConfigurationManager.AppSettings["SIMSUserMessage"]%>' />
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
                    <td align="center" colspan="2">
                        <asp:Button ID="imgBtnConfirm" runat="server" CssClass="btn" OnClick="imgBtnConfirm_Click"
                            Text="Destroy" Width="78px" Visible="false" />
                        <asp:Button ID="ImgBtnCancel" runat="server" CssClass="btn" OnClick="ImgBtnCancel_Click"
                            Text="Cancel" Width="74px" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
