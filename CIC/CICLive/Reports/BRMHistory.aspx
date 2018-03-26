<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="BRMHistory.aspx.cs" Inherits="Reports_BRMHistory" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <ContentTemplate>
<table width="100%" border="0">
        <tr>
            <td class="headingred">
                MIS-BRM Resolution Time Report (History)</td>
            <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /> </ProgressTemplate>
                </asp:UpdateProgress>
            </td>
        </tr>
        <caption>
            <p style="text-align:center;">
            </p>
            <tr>
                <td class="bgcolorcomm" colspan="2">
                    <table border="0" cellpadding="1" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <table border="0" cellpadding="1" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            Select BRM
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDLBRM" runat="server" AutoPostBack="True" 
                                                CssClass="simpletxt1" onselectedindexchanged="DDLBRM_SelectedIndexChanged" 
                                                ValidationGroup="editt" Width="200px">
                                                <asp:ListItem Text="Select" Value="0" />
                                                <asp:ListItem Text="AverageResolutionTime" Value="AVG" />
                                                <asp:ListItem Text="ResolutionTime (%)" Value="PER" />
                                                <asp:ListItem Text="ResolutionTime ASC-BRANCH" Value="ASCBR" />
                                                <asp:ListItem Text="AverageResolutionTime Branchwise" Value="AVGBR" />
                                                <asp:ListItem Text="ResolutionTime Branchwise (%)" Value="PERBR" />
                                                <asp:ListItem Text="AverageResolutionTime CP" Value="AVG_CP" />
                                                <asp:ListItem Text="ResolutionTime CP(%)" Value="PER_CP" />
                                                <asp:ListItem Text="BRM Verification" Value="BRM_VERIFY" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvBRM" runat="server" 
                                                ControlToValidate="DDLBRM" Display="Dynamic" InitialValue="0" 
                                                SetFocusOnError="True" ValidationGroup="editt" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Year
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="Ddlyear" runat="server" AutoPostBack="True" 
                                                CssClass="simpletxt1" onselectedindexchanged="Ddlyear_SelectedIndexChanged" 
                                                ValidationGroup="editt" Width="200px">
                                                <asp:ListItem>2015</asp:ListItem>
                                                <asp:ListItem>2016</asp:ListItem>
                                                <asp:ListItem>2017</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Month
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="simpletxt1" 
                                                ValidationGroup="editt" Width="200px">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RFVMonth" runat="server" 
                                                ControlToValidate="ddlMonth" Display="Dynamic" InitialValue="0" 
                                                SetFocusOnError="True" ValidationGroup="editt" />
                                        </td>
                                    </tr>
                                    <tr ID="trRegion" runat="server">
                                        <td>
                                            Region
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DdlRegion" runat="server" AutoPostBack="True" 
                                                CssClass="simpletxt1" onselectedindexchanged="DdlRegion_SelectedIndexChanged" 
                                                ValidationGroup="editt" Width="200px">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RfvRegion" runat="server" 
                                                ControlToValidate="DdlRegion" Display="Dynamic" InitialValue="0" 
                                                SetFocusOnError="True" ValidationGroup="editt" />
                                        </td>
                                    </tr>
                                    <tr ID="trBranch" runat="server">
                                        <td>
                                            Branch
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DdlBranch" runat="server" CssClass="simpletxt1" 
                                                ValidationGroup="editt" Width="200px">
                                                <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RfvBranch" runat="server" 
                                                ControlToValidate="DdlBranch" Display="Dynamic" InitialValue="0" 
                                                SetFocusOnError="True" ValidationGroup="editt" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            CP - IS</td>
                                        <td>
                                            <asp:DropDownList ID="ddlCPIS" runat="server" CssClass="simpletxt1">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn" 
                                                onclick="btnSearch_Click" Text="Search" ValidationGroup="editt" Width="100" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div ID="divReport" style="width:100%;overflow:scroll">
                                    <asp:GridView ID="gvReport" runat="server" 
                                        AlternatingRowStyle-CssClass="fieldName" CssClass="simpletxt1" 
                                        GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor" 
                                        HorizontalAlign="Center" RowStyle-CssClass="bgcolorcomm" 
                                        RowStyle-HorizontalAlign="Center" Width="100%">
                                    </asp:GridView>
                                    <asp:GridView ID="gvReportSC" runat="server" 
                                        AlternatingRowStyle-CssClass="fieldName" AutoGenerateColumns="false" 
                                        CssClass="simpletxt1" GridGroups="both" 
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" HorizontalAlign="Center" 
                                        RowStyle-CssClass="bgcolorcomm" RowStyle-HorizontalAlign="Center" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="SCName" HeaderStyle-Width="125px" 
                                                HeaderStyle-Wrap="true" HeaderText="SCName" ItemStyle-Width="125px" />
                                            <asp:BoundField DataField="Period" HeaderStyle-Width="125px" 
                                                HeaderStyle-Wrap="true" HeaderText="Period" ItemStyle-Width="125px" />
                                            <asp:BoundField DataField="Fans" HeaderStyle-Width="125px" 
                                                HeaderStyle-Wrap="true" HeaderText="Fans %" ItemStyle-Width="125px" />
                                            <asp:BoundField DataField="fanB" HeaderStyle-Width="125px" 
                                                HeaderStyle-Wrap="true" HeaderText="Fans % Business" ItemStyle-Width="125px" />
                                            <asp:BoundField DataField="Pumps" HeaderStyle-Width="125px" 
                                                HeaderStyle-Wrap="true" HeaderText="Pumps %" ItemStyle-Width="125px" />
                                            <asp:BoundField DataField="PumpB" HeaderStyle-Width="125px" 
                                                HeaderStyle-Wrap="true" HeaderText="Pumps % Business" ItemStyle-Width="125px" />
                                            <asp:BoundField DataField="Lighting" HeaderStyle-Width="125px" 
                                                HeaderStyle-Wrap="true" HeaderText="Lighting %" ItemStyle-Width="125px" />
                                            <asp:BoundField DataField="LightingB" HeaderStyle-Width="125px" 
                                                HeaderStyle-Wrap="true" HeaderText="Lighting % Business" 
                                                ItemStyle-Width="125px" />
                                            <asp:BoundField DataField="Appliances" HeaderStyle-Width="125px" 
                                                HeaderStyle-Wrap="true" HeaderText="Appliances %" ItemStyle-Width="125px" />
                                            <asp:BoundField DataField="AppliancesB" HeaderStyle-Width="125px" 
                                                HeaderStyle-Wrap="true" HeaderText="Appliances % Business" 
                                                ItemStyle-Width="125px" />
                                            <asp:BoundField DataField="FHP Motors" HeaderStyle-Width="125px" 
                                                HeaderStyle-Wrap="true" HeaderText="FHP Motors %" ItemStyle-Width="125px" />
                                            <asp:BoundField DataField="FHPMotorB" HeaderStyle-Width="125px" 
                                                HeaderStyle-Wrap="true" HeaderText="FHP Motors % Business" 
                                                ItemStyle-Width="125px" />
                                            <asp:BoundField DataField="LT Motors" HeaderStyle-Width="125px" 
                                                HeaderStyle-Wrap="true" HeaderText="LT Motors %" ItemStyle-Width="125px" />
                                            <asp:BoundField DataField="LTMotorB" HeaderStyle-Width="125px" 
                                                HeaderStyle-Wrap="true" HeaderText="LT Motors % Business" 
                                                ItemStyle-Width="125px" />
                                        </Columns>
                                    </asp:GridView>
                                    <div ID="dvLogic1" runat="server">
                                        <br />
                                        <br />
                                        1)Total Time taken to Closed Complaints in Current Month/ Total no. Of Closed 
                                        Complaint in Current Month.<br />
                                        <br />
                                        For Cum Complaints<br />
                                        2)Total Time taken to Closed Complaints from (complaints of last 12 months) 
                                        /Total no. Of Closed Complaint (complaints of Last twelve months.)<br />
                                        <br />
                                        *Based on SLA Date<br />
                                    </div>
                                    <div ID="dvLogic2" runat="server">
                                        <br />
                                        For Fan,Lighting,Appliances,Pump<br />
                                        1)(Total no of Complaints which are closed in 0 -2 days in Current month / Total 
                                        number of Complaints in Current Month) * 100<br />
                                        For FHP and LT motors<br />
                                        2)(Total no of Complaints which are closed in 0 -3 days in Current month / Total 
                                        number of Complaints in Current Month) * 100<br />
                                        <br />
                                        For Cum Complaints<br />
                                        3)(Total no of Complaints which are closed in 0 -2/3 days from Last twelve month 
                                        to till date / Total number of Complaints from Last twelve month to till date) * 
                                        100<br />
                                        <br />
                                        *Based on SLA Date<br />
                                    </div>
                                    <div ID="dvLogic3" runat="server">
                                        <br />
                                        For a Contractor for current month Period<br />
                                        1) [Fan %] = Fan Complaints Closed (Within 2 days ) of current month of SC / 
                                        Total Fan Complaints in Current Month to SC.<br />
                                        2) Cumulative Period [Fan %] = Fan-Closed Complaints count for SC out of 
                                        received in last 12 month from current / Fan Total Complaints received in last 
                                        12 month to SC
                                        <br />
                                        3) [Fan % business] = Total Fan Complaints in current month to / Fan Complaints 
                                        received in Current Month of selected branch.<br />
                                        4) Cumulative Period [Fan % business] = Fan-Total Complaints received in last 12 
                                        month to SC / Fan-Complaints received in selected branch in last 12 month
                                        <br />
                                        <br />
                                        *Based on SLA Date
                                        <br />
                                        <br />
                                    </div>
                                    <div ID="dvLogic4" runat="server">
                                        <br />
                                        1)Total Time taken to Closed Complaints in Current Month/ Total no. Of Closed 
                                        Complaint in Current Month.<br />
                                        <br />
                                        For Cum Complaints<br />
                                        2)Total Time taken to Closed Complaints from (complaints of last 12 months) 
                                        /Total no. Of Closed Complaint (complaints of Last twelve months.)<br />
                                        <br />
                                        3)Unallocated Complaints are not included.<br />
                                        <br />
                                    </div>
                                    <div ID="dvLogic5" runat="server">
                                        <br />
                                        For Fan,Lighting,Appliances,Pump<br />
                                        1)(Total no of Complaints which are closed in 0 -2 days in Current month / Total 
                                        number of Complaints in Current Month) * 100<br />
                                        For FHP and LT motors<br />
                                        2)(Total no of Complaints which are closed in 0 -3 days in Current month / Total 
                                        number of Complaints in Current Month) * 100<br />
                                        <br />
                                        For Cum Complaints<br />
                                        3)(Total no of Complaints which are closed in 0 -2/3 days from Last twelve month 
                                        to till date / Total number of Complaints from Last twelve month to till date) * 
                                        100
                                        <br />
                                        *Based on SLA Date (Unallocated Complaints are not included.)<br />
                                    </div>
                                </div>
                                <div style="text-align:center">
                                    <asp:Button ID="btnExport" runat="server" CssClass="btn" 
                                        onclick="btnExport_Click" Text="Export To Execl" Visible="False" Width="100" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </caption>
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

