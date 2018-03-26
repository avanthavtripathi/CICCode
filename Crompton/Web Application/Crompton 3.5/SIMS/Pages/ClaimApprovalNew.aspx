<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master"
    CodeFile="ClaimApprovalNew.aspx.cs" Inherits="SIMS_Pages_ClaimApprovalNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>

            <script type="text/javascript">
                function Openpopup(popurl)
                {
                winpops = window.open(popurl,"","width=1000, height=600, left=45, top=15, scrollbars=yes, menubar=no,resizable=no,directories=no,location=center")
                }
            </script>

            <script language="javascript" type="text/javascript">
            var TotalChkBx;
            var Counter;

            window.onload = function()
            {
               //Get total no. of CheckBoxes in side the GridView.
               TotalChkBx = parseInt('<%= this.GvDetails.Rows.Count %>');

               //Get total no. of checked CheckBoxes in side the GridView.
               Counter = 0;
            }

            function HeaderClick(CheckBox)
            {
               //Get target base & child control.
               var TargetBaseControl = 
                   document.getElementById('<%= this.GvDetails.ClientID %>');
               var TargetChildControl = "chkBxSelect";
                
               //Get all the control of the type INPUT in the base control.
               var Inputs = TargetBaseControl.getElementsByTagName("input");

               //Checked/Unchecked all the checkBoxes in side the GridView.
               for(var n = 0; n < Inputs.length; ++n)
                  if(Inputs[n].type == 'checkbox' && 
                            Inputs[n].id.indexOf(TargetChildControl,0) >= 0)
                     Inputs[n].checked = CheckBox.checked;

               //Reset Counter
               Counter = CheckBox.checked ? TotalChkBx : 0;
            }

            function ChildClick(CheckBox, HCheckBox)
            {
               //get target control.
               var HeaderCheckBox = document.getElementById(HCheckBox);

               //Modifiy Counter; 
               if(CheckBox.checked && Counter < TotalChkBx)
                  Counter++;
               else if(Counter > 0) 
                  Counter--;

               //Change state of the header CheckBox.
               if(Counter < TotalChkBx)
                  HeaderCheckBox.checked = false;
               else if(Counter == TotalChkBx)
                  HeaderCheckBox.checked = true;
            }
            </script>

            <table width="100%" cellpadding="2" cellspacing="0">
                <tr>
                    <td class="headingred">
                        Claim approval screen
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
                        <table width="100%" border="0">
                            <tr>
                                <td colspan="2" width="100%" align="left" class="bgcolorcomm">
                                    <table border="0" style="width: 100%">
                                        <tr>
                                            <td colspan="2">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 19px">
                                                ASC:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlasc" runat="server" CssClass="simpletxt1" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlasc_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 19px">
                                                Division:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlDivision" runat="server" CssClass="simpletxt1" Width="180px"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="MsgTDCount" colspan="2">
                                                Total Number of Records :
                                                <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="GvDetails" runat="server" AllowPaging="false" AllowSorting="True"
                                                    AlternatingRowStyle-CssClass="fieldName" PagerStyle-HorizontalAlign="Center"
                                                    AutoGenerateColumns="False" GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor"
                                                    HorizontalAlign="Left"  DataKeyNames="claim_no" RowStyle-CssClass="gridbgcolor"
                                                    Width="100%" EnableSortingAndPagingCallbacks="True" OnPageIndexChanging="GvDetails_PageIndexChanging">
                                                    <RowStyle CssClass="gridbgcolor" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Select">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkBxSelect" runat="server" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkBxHeader" onclick="javascript:HeaderClick(this);" runat="server" />
                                                            </HeaderTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMId" runat="server" Text='<%#Eval("Mid") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAID" runat="server" Text='<%#Eval("Aid") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sno" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="SCName_Division" HeaderText="Service Contractor/Division"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Complaint No.">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkcomplaint" runat="server" CommandArgument='<%#Eval("complaint_no")%>'
                                                                    CausesValidation="false" CommandName="stage" Text='<%#Eval("complaint_no")%>'
                                                                    OnClick="lnkcomplaint_Click"> </asp:LinkButton>
                                                                <asp:HiddenField ID="hdnTotalRecord" runat="server" Value='<%#Eval("TotalRecord")%>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Claim No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblClaimNo" runat="server" Text='<%#Eval("claim_no") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="claim_Date" HeaderText="Claim Generated Date" ItemStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Product_desc" HeaderText="Product" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="defect" HeaderText="Defects" ItemStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="activity" HeaderText="Activity" ItemStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Parameter1" HeaderText="Param-1" ItemStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PossibleVaue1" HeaderText="PV-1" ItemStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Parameter2" HeaderText="Param-2" ItemStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PossibleVaue2" HeaderText="PV-2" ItemStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="amount" HeaderText="Amount(Material/Service)" HeaderStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="View/Reject">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkview" CausesValidation="false" runat="server" CommandName="complaintaction"
                                                                    OnClick="lnkview_Click">View</asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                                    <img src="<%=ConfigurationManager.AppSettings["simsUserMessage"]%>" alt="" />
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
                                                <asp:Panel ID="Panel1" Width="100%" runat="server">
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <asp:Button Text="Confirm" Width="70px" ID="imgBtnConfirm" CssClass="btn" runat="server"
                                                    OnClick="imgBtnConfirm_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
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
