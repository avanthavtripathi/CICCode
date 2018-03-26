<%@ Page Title="" Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true" CodeFile="90DaysRepetitiveComplainReport.aspx.cs" Inherits="SIMS_Pages_90DaysRepetitiveComplainReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">

<script type="text/javascript" language="javascript">
    function funRepeatedComplaints(SapProductCode, CmpLogDate, SC_Sno) {
        //var SC_Sno = document.getElementById("<%=ddlasc.ClientID %>").value;
        var strUrl = '90DaysRepeatedComplaints.aspx?PSN=' + SapProductCode + '&CmpLogDate=' + CmpLogDate + '&SCNo=' + SC_Sno;
        window.open(strUrl, 'History', 'height=450,width=850,left=20,top=30,Location=0');
    }
</script>

<asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
        <table width="100%" cellpadding="2" cellspacing="0">
                <tr>
                    <td class="headingred">
                        Repetitive Complaint Report Of 30 Days
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
                                <td width="100%" align="left" class="bgcolorcomm">
                                    <table border="0" style="width: 100%">
                               <tr>
                                <td width="30%">
                                    Region
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlRegion" runat="server" Width="180px" CssClass="simpletxt1"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Branch
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlBranch" runat="server" Width="180px" CssClass="simpletxt1"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                            <td valign="top">
                                                ASC:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlasc" runat="server" CssClass="simpletxt1">
                                                </asp:DropDownList> 
                                            </td>
                                        </tr>
                            <tr>
                                            <td valign="top">
                                                Division:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlDivision" runat="server" CssClass="simpletxt1" Width="180px">
                                                </asp:DropDownList>
                                                   <asp:RequiredFieldValidator ID="rfvdiv" runat="server" ControlToValidate="ddlDivision" Display="Dynamic" Text="Division is mendatory." SetFocusOnError="true" InitialValue="0" />
                                            </td>
                            </tr>
                                        
                                        <tr>
                                            <td>
                                              Date:  <font color='red'>*</font></td>
                                            <td>
                                  From  <asp:TextBox runat="server" ID="txtFromDate" CssClass="txtboxtxt" 
                                        MaxLength="10" Width="100px" />
                                    <cc1:CalendarExtender ID="CEFrom" runat="server" TargetControlID="txtFromDate">
                                    </cc1:CalendarExtender>
                                                &nbsp;To
                                    <asp:TextBox runat="server" ID="txtToDate" CssClass="txtboxtxt" 
                                        MaxLength="10" Width="100px" />
                                    <cc1:CalendarExtender ID="CETo" runat="server" TargetControlID="txtToDate">
                                    </cc1:CalendarExtender> 
                                    </td>
                                        </tr>
                                        <tr><td style="padding-top:10px;"></td>
                                        <td style="padding-top:10px;"> <asp:Button ID="BtnSearch" runat="server" CssClass="btn" Text="SHOW REPORT" onclick="BtnSearch_Click"/>           
                                        <asp:Button ID="lnkDownload" runat="server" Text="DOWNLOAD EXCEL" OnClick="lnkDownload_Click" CssClass="btn"></asp:Button></td></tr>
                                        <tr><td colspan="2" style="padding-top:10px;">
                                        <table border="0" cellpadding="2" cellspacing="0"><tr>
                                        <td>Total Number of Records : <asp:Label ID="lblRowsCount" CssClass="MsgTotalCount" runat="server"></asp:Label></td>
                                        <td style="padding-left:50px;">Approved complaint : <asp:Label ID="lblApproved" CssClass="MsgTotalCount" runat="server"></asp:Label></td>
                                        <td style="padding-left:50px;">Not Approved complaint : <asp:Label ID="lblNotapproved" CssClass="MsgTotalCount" runat="server"></asp:Label></td>
                                        </tr></table>
                                        </td></tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="GvDetails" runat="server" AllowSorting="True"
                                                    AlternatingRowStyle-CssClass="fieldName" AutoGenerateColumns="False" GridGroups="both"
                                                    HeaderStyle-CssClass="fieldNamewithbgcolor" HorizontalAlign="Left"
                                                    RowStyle-CssClass="gridbgcolor" Width="100%" EnableSortingAndPagingCallbacks="True">
                                                    <RowStyle CssClass="gridbgcolor" />
                                                    <Columns>
                                                        <asp:BoundField DataField="RowNumber" HeaderText="S.No." ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="Region_Desc" HeaderText="Region Desc" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Branch_Name" HeaderText="Branch Name" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Service_Contractor" HeaderText="Service Contractor" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProductLine_Desc" HeaderText="ProductLine Desc" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProductGroup_Desc" HeaderText="ProductGroup Desc" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Product_Code" HeaderText="Product Code" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Complaint No.">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkcomplaint" runat="server" CommandArgument='<%#Eval("complaint_no")%>'
                                                                    CausesValidation="false" CommandName="stage" Text='<%#Eval("complaint_no")%>'
                                                                   OnClick="lnkcomplaint_Click" > </asp:LinkButton> 
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="LoggedDate" HeaderText="Logged Date" ItemStyle-HorizontalAlign="Left" dataformatstring="{0:M/dd/yyyy}">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                      <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Product Serial No.">
                                                            <ItemTemplate>
                                                            <a href="javascript:void(0);" id="dfh" onclick="funRepeatedComplaints('<%#Eval("SapProdCode")%>','<%#Eval("LoggedDate")%>','<%#Eval("ASC_Id")%>')" >
                                                              <%#Eval("SapProdCode")%>
                                                            </a>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ApprovalStatus" HeaderText="Approval Status" ItemStyle-HorizontalAlign="Left">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
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
                                            <td colspan="2">
                                                <%-- custom Paging --%>
                                                <asp:DataList CellPadding="5" RepeatDirection="Horizontal" runat="server" ID="dlPager"
                                                    onitemcommand="dlPager_ItemCommand">
                                                    <ItemTemplate>
                                                       <asp:LinkButton Enabled='<%#Eval("Enabled") %>' Width="40px" BorderStyle="Ridge" style="text-decoration:none; text-align:center;" runat="server" ID="lnkPageNo" Text='<%#Eval("Text") %>' CommandArgument='<%#Eval("Value") %>' CommandName="PageNo"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </td>
                                        </tr>
                                       
                                    </table>
                                </td>
                            </tr>
                            
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
    <asp:PostBackTrigger ControlID="lnkDownload" />
    </Triggers>
        </asp:UpdatePanel>
</asp:Content>

