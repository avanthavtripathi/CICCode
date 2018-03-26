<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminStickerDetails.ascx.cs"
    Inherits="UC_AdminStickerDetails" %>
<asp:UpdatePanel ID="childPnlAdminControl" runat="server">
    <ContentTemplate>

        <script type="text/javascript">

            function ResetValueColor(e) {
                try {
                    var txtVal = Number(e.value);
                    var range = document.getElementById("<%=lblStickerAllocationRange.ClientID %>").innerHTML;

                    var minRangeVal = Number(range.split("-")[0]);
                    var maxRangeVal = Number(range.split("-")[1]);
                    if ((txtVal < minRangeVal || txtVal > maxRangeVal) && txtVal != 0) {
                        e.style.background = "Red";
                        e.title = "Invalid range distribution.";
                        return false;
                    }
                    else if (e.style.background != "") {
                        e.style.background = "";
                    }
                    var grd = document.getElementById("<%= grdAttribute.ClientID%>");
                    for (i = 1; i < grd.rows.length; i++) {
                        if (e.id.indexOf("btnResetSerialNo") >= 0) {
                            grd.rows[i].cells[2].children[0].value = "";
                            grd.rows[i].cells[3].children[0].value = "";
                        }
                        if (grd.rows[i].style.background == "")
                            continue;
                        grd.rows[i].style.background = "";
                    }
                }
                catch (err) {
                    alert(err.Description);
                }
            }
            function RangeValidatorForStickers() {
                try {
                    var grd = document.getElementById("<%= grdAttribute.ClientID%>");
                    var e1, e2, b1, b2, i, j, total;
                    total = 0;
                    var flag = "";
                    for (i = 1; i < grd.rows.length; i++) {
                        if (flag == "false")
                            return false; ;
                        e1 = Number(grd.rows[i].cells[2].children[0].value);
                        e2 = Number(grd.rows[i].cells[3].children[0].value);
                        if (e1 == 0 && (e1 === e2)) {
                            total = total + 1;
                            continue;
                        }
                        if (e1 > e2 || (e1 == 0 && e2 > 0) || (e1 > 0 && e2 == 0)) {
                            grd.rows[i].cells[2].children[0].focus();
                            grd.rows[i].style.background = "Red";
                            flag = "false";
                            return false;
                        }
                        else {
                            for (j = 1; j < grd.rows.length; j++) {
                                if (i == j) continue;
                                b1 = Number(grd.rows[j].cells[2].children[0].value);
                                b2 = Number(grd.rows[j].cells[3].children[0].value);
                                if ((b1 >= e1 && b1 <= e2) || (b2 >= e1 && b2 <= e2)) {
                                    grd.rows[i].style.background = "Red"
                                    grd.rows[j].style.background = "Red"
                                    flag = "false";
                                    grd.rows[j].title = "Incorrect Serial No.";
                                    flag = "false";
                                    return false;
                                }
                            }
                        }
                    }
                    if (total == grd.rows.length - 1) {
                        alert("Please Enter RangeFrom and RangeTo value.");
                        grd.rows[1].cells[2].children[0].focus();
                        return false;
                    }
                    else if (flag == "false")
                        return false;
                    else
                        return true;
                }
                catch (err) {
                    alert(err.Description);
                }
            }

            function onlyNum(e, t) {
                try {
                    if (window.event) {
                        var charCode = window.event.keyCode;
                    }
                    else if (e) {
                        var charCode = e.which;
                    }
                    else { return true; }
                    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                        return false;
                    }
                    return true;
                }
                catch (err) {
                    alert(err.Description);
                }
            }

            function checkRS() {
                if (document.getElementById("<%= ddlActiveStatus.ClientID %>").value == "-1") {
                    alert("Please Select Active/In-Active as Status.");
                    document.getElementById("<%=  ddlActiveStatus.ClientID %>").focus(); 
                    return false;
                }
                else if (document.getElementById("<%= ddlRegion.ClientID %>").value == "-1") {
                alert("Please select Region.");
                    document.getElementById("<%= ddlRegion.ClientID %>").focus();
                    return false;
                }
            }
        </script>

        <div  class="bgcolorcomm">
            <table style="width:900px;">
                <tr>
                    <td align="right">
                        Status : 
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlActiveStatus" runat="server" CssClass="simpletxt1" style="min-width:210px;">
                            <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                            <asp:ListItem Text="In-Active" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Both" Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        Region :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="True" 
                        CssClass="simpletxt1" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" style="min-width:150px;">
                            <asp:ListItem Text="All" Value="0"></asp:ListItem>
                            <asp:ListItem Text="East" Value="5"></asp:ListItem>
                            <asp:ListItem Text="West" Value="6"></asp:ListItem>
                            <asp:ListItem Text="North" Value="7"></asp:ListItem>
                            <asp:ListItem Text="South" Value="4"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        Sticker Code :</td>
                    <td>
                        <asp:TextBox ID="txtStickerCode" runat="server" CssClass="simpletxt1" Text=""></asp:TextBox></td>
                </tr>
                <tr id="trBranchAsc" runat="server">
                    <td align="right">
                        Branch :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBranchSearch" runat="server" CssClass="simpletxt1" AutoPostBack="True" OnSelectedIndexChanged="ddlBranchSearch_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        Service Contractor :
                    </td>
                    <td colspan="3">
                        <asp:DropDownList ID="ddlServicecontractorSearch" CssClass="simpletxt1" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlServicecontractorSearch_SelectedIndexChanged" style="min-width:413px;">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="trProductDivision" runat="server">
                <td align="right">
                        Product Division :
                    </td>
                    <td colspan="5">
                        <asp:DropDownList ID="ddlPdivision" CssClass="simpletxt1" runat="server" style="min-width:210px;">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td colspan="2">
                        <asp:Button ID="btnSearch" runat="server" Text="Search"  CssClass="btn" OnClick="btnSearch_Click" />
                        &nbsp;
                        <asp:Button ID="btnUpdate" runat="server" Text="Update"   CssClass="btn"
                            onclick="btnUpdate_Click" OnClientClick="return checkRS();" Visible="false" />
                        &nbsp;
                        <asp:Button ID="btnCleare" runat="server" CssClass="btn" 
                            OnClick="btnCleare_Click" Text="Refresh" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            <table width="99%">
                <tr>
                <td>
                <div style="float:left; width:25%;">
                Balance For Allocation :
                        <asp:Label ID="lblCount" Text="0" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <div style="float:left; width:58%;">
                <asp:Label ID="lblUpdtErrorMessage" Text="" runat="server" ForeColor="Red" ></asp:Label>
                </div>
                 <div style="float:right; padding-right:0%;">
                <asp:Button ID="btnDownload" runat="server" Visible="false" CssClass="btn" 
                         Text="Sticker Allocation Download" onclick="btnDownload_Click"/>
                </div>
                 <div style="float:right; width:8%;">
                <asp:Button ID="lnkDownload" runat="server" Visible="false" CssClass="btn" Text="Download" OnClick="lnkDownload_Click"/>
                 </div>
                <div style="clear:both;">
                </td>
                    
                </tr>
                <tr>
                    <td>
                        <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                            HeaderStyle-CssClass="fieldNamewithbgcolor" PagerStyle-HorizontalAlign="Center"
                            AutoGenerateColumns="False" ID="gvStickerDetails" runat="server" Width="960px"
                            HorizontalAlign="Left" OnSorting="gvComm_Sorting" AllowSorting="true" AllowPaging="true"
                            OnSelectedIndexChanging="gvStickerDetails_SelectedIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="SNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <%--<%# Container.DataItemIndex+1 %>--%>
                                        <asp:Label ID="lblSno" runat="server" Text='<%# Eval ("Rn") %>'></asp:Label>
                                        <asp:HiddenField ID="hdnStickerId" runat="server" Value='<%# Eval ("StickerId") %>' />
                                        <asp:HiddenField ID="hdnStickerSno" runat="server" Value='<%# Eval ("StickerDesc") %>' />
                                        <asp:HiddenField ID="hdnProductDivisionId" runat="server" Value='<%# Eval ("ProductDivisionId") %>' />
                                        <asp:HiddenField ID="hdnRegionId" runat="server" Value='<%# Eval ("RegionId") %>' />
                                        <asp:HiddenField ID="hdnBranchId" runat="server" Value='<%# Eval ("BranchId") %>' />
                                        <asp:HiddenField ID="hdnActiveStatus" runat="server" Value='<%# Eval ("ActiveStatus") %>' />
                                        <asp:HiddenField ID="hdnAscId" runat="server" Value='<%# Eval ("AscId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Sticker No" DataField="StickerDesc" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" SortExpression="StickerDesc"></asp:BoundField>
                                <asp:BoundField HeaderText="Region" DataField="Region_Desc" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" SortExpression="Region_Desc"></asp:BoundField>
                                <asp:BoundField HeaderText="Branch" DataField="Branch_Name" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" SortExpression="Branch_Name"></asp:BoundField>
                                <asp:BoundField HeaderText="Product Divison" DataField="Unit_Desc" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" SortExpression="Unit_Desc"></asp:BoundField>                                
                                <asp:BoundField HeaderText="Allocated By" DataField="AllocatedByName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" SortExpression="AllocatedByName"></asp:BoundField>
                                    <asp:BoundField HeaderText="Allocated To" DataField="AllocatedToName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" SortExpression="AllocatedToName"></asp:BoundField>
                                <%--<asp:BoundField HeaderText="Created By" DataField="CreatedByName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" SortExpression="CreatedByName"></asp:BoundField>--%>
                                <asp:BoundField HeaderText="Active Status" DataField="ActiveStatus" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" SortExpression="ActiveStatus"></asp:BoundField>
                                <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdite" OnClick="lnkEditeClick" Text="Select" runat="server"
                                            CommandArgument='<%# Container.DataItemIndex %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="center" style="padding-top: 20px; padding-bottom: 20px; padding-left: 60px;">
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
                <td>
                <asp:DataList CellPadding="5" RepeatDirection="Horizontal" runat="server" ID="dlPager"
                            OnItemCommand="dlPager_ItemCommand">
                            <ItemTemplate>
                                <asp:LinkButton Enabled='<%#Eval("Enabled") %>' runat="server" ID="lnkPageNo" Text='<%#Eval("Text") %>'
                                    CommandArgument='<%#Eval("Value") %>' CommandName="PageNo"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:DataList>
                        <asp:HiddenField ID="hdnStickersId" runat="server" Value="0" />
                </td>
                </tr>
                <tr id="trAllocationRegion" runat="server">
                    <td>
                    <div style="width:100%; line-height:20px; border-bottom:solid 1px #396870;">
                    <b>Allocation</b> 
                    </div>
                        <table width="900px" style="padding-top:10px;">
                        <tr><td>
                         <div style="float: left; margin-right:30px;">
                             <asp:LinkButton ID="lnkDownloadFormatBranchWise" runat="server" Visible="false"
                                 onclick="lnkDownloadFormatBranchWise_Click">Download Excel For Bulk Allocation</asp:LinkButton>
                         </div>
                        </td></tr>
                            <tr>
                                <td>
                                    <div style="width:100%;">
                                        <div style="float: left; margin-right:30px;">
                                            Region :
                                            <asp:DropDownList ID="ddlRegionAllocation" CssClass="simpletxt1" 
                                            runat="server" 
                                            OnSelectedIndexChanged="ddlRegionAllocation_SelectedIndexChanged"
                                            AutoPostBack="True" style="min-width:150px;">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div style="float: left; margin-right:20px;">
                                            Branch :
                                            <asp:DropDownList ID="ddlBranchAllocation" CssClass="simpletxt1" 
                                            runat="server" OnSelectedIndexChanged="ddlBranchAllocation_SelectedIndexChanged"
                                             AutoPostBack="true"
                                             style="min-width:250px;" >
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div style="float: left;">
                                            Balance For Allocation :
                                            <asp:Label ID="lblStickerAllocationRange" runat="server" Text="0-0" ForeColor="Red"></asp:Label>
                                        </div>
                                        <div style="clear: both; height: 10px;">
                                        </div>
                                    </div>
                                    <div>
                                        <asp:GridView ID="grdAttribute" runat="server" AutoGenerateColumns="False" RowStyle-CssClass="gridbgcolor"
                                            AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor" Width="900px" >
                                            <RowStyle CssClass="gridbgcolor"></RowStyle>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sno" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex+1 %>
                                                        <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("Id") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Name" DataField="Name" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left">
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Range From" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <%--<asp:TextBox ID="txtStickerRangeFrom0" runat="server" Text='<%# Eval("RangeFrom").ToString().Equals("0")? "" : Eval("RangeFrom") %>'
                                                            Enabled='<%# Eval("RangeFrom").ToString().Equals("0") %>' MaxLength="7" onkeypress="return onlyNum(event,this);"
                                                            onblur="ResetValueColor(this)"></asp:TextBox>--%>
                                                          <asp:TextBox ID="txtStickerRangeFrom" runat="server" Text="" MaxLength="7" onkeypress="return onlyNum(event,this);"
                                                            onblur="ResetValueColor(this)" CssClass="simpletxt1"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Range To" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <%--<asp:TextBox ID="txtStickerRangeTo0" runat="server" Text='<%# Eval("RangeTo").ToString().Equals("0")? "" : Eval("RangeTo") %>'
                                                            Enabled='<%# Eval("RangeTo").ToString().Equals("0") %>' MaxLength="7" onkeypress="return onlyNum(event,this);"
                                                            onblur="ResetValueColor(this)"></asp:TextBox>--%>
                                                            <asp:TextBox ID="txtStickerRangeTo" runat="server" Text="" MaxLength="7" onkeypress="return onlyNum(event,this);"
                                                            onblur="ResetValueColor(this)" CssClass="simpletxt1"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Total Allocated" DataField="TotalAllocated" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
                                                <asp:BoundField HeaderText="Total Consumed" DataField="TotalConsumed" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnAllocateStickers" runat="server" Text="Allocate" OnClientClick="return RangeValidatorForStickers()"
                                        OnClick="btnAllocateStickers_Click"  CssClass="btn" />
                                </td>
                                <%--<td>
                                    <asp:Button ID="btnResetSerialNo"  CssClass="btn" runat="server" Text="Reset" OnClientClick="ResetColor(this)" />
                                </td>--%>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblErrorMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </ContentTemplate>
    <Triggers>
    <asp:PostBackTrigger ControlID="lnkDownload" />
     <asp:PostBackTrigger ControlID="btnDownload" />
     <asp:PostBackTrigger ControlID="lnkDownloadFormatBranchWise" />
    </Triggers>
</asp:UpdatePanel>
