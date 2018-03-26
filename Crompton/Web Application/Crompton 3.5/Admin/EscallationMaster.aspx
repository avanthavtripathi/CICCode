<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="EscallationMaster.aspx.cs" Inherits="Admin_EscallationMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script language="javascript" type="text/javascript">
    
      function ToggleState() 

            {
            //alert("Hi");
            var chkBoxList = $get('ctl00_MainConHolder_CheckBoxProductDivision');
            var chkCount=0;
            var chkTotalCount;
            var chkboxes = chkBoxList.getElementsByTagName('input'); 
            chkTotalCount=chkboxes.length;
            for(var i=0;i<chkTotalCount;i++) 
            {
                if(chkboxes[i].checked)
                {
                    chkCount=chkCount+1;
                }             
            }
            if(chkCount==0)
            {
                alert('Product Division is required');
                return false;
            }
            else
            {
                return true;
            }
            
            

}


    </script>

    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Escallation Master
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right" style="padding-right: 10px">
                        <asp:RadioButtonList ID="rdoboth" RepeatDirection="Horizontal" RepeatColumns="3"
                            runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdoboth_SelectedIndexChanged">
                            <asp:ListItem Value="1" Text="Active" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="0" Text="In-Active"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Both"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" colspan="2">
                        <table border="0" width="100%">
                            <tr>
                                <td align="left" class="MsgTDCount">
                                    Total Number of Records :
                                    <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server">
                                    </asp:Label>
                                </td>
                                <td align="right">
                                    Search For
                                    <asp:DropDownList ID="ddlSearch" runat="server" Width="130px" CssClass="simpletxt1">
                                        <asp:ListItem Text="Product" Value="Unit_Desc"></asp:ListItem>
                                        <asp:ListItem Text="Call Status" Value="EM.CallStage"></asp:ListItem>
                                        <asp:ListItem Text="Mile Stone" Value="StageDesc"></asp:ListItem>
                                        <asp:ListItem Text="BHQ/Non-BHQ" Value="BHQ_Status"></asp:ListItem>
                                        <%-- <asp:ListItem Text="Branch" Value="Branch_name"></asp:ListItem>--%>
                                        <asp:ListItem Text="At/Repair Site" Value="SC_RepairLocation"></asp:ListItem>
                                        <asp:ListItem Text="Role" Value="RoleName"></asp:ListItem>
                                        <%-- <asp:ListItem Text="User Id" Value="UserName"></asp:ListItem>   --%>
                                    </asp:DropDownList>
                                    With
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="txtboxtxt" Width="100px" Text=""></asp:TextBox>
                                    <asp:Button Text="Go" Width="25px" CssClass="btn" ID="imgBtnGo" runat="server" CausesValidation="False"
                                        ValidationGroup="editt" OnClick="imgBtnGo_Click" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0">
                            <tr>
                                <td class="bgcolorcomm">
                                    <!-- Escallation Listing -->
                                    <asp:GridView RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="True"
                                        AllowSorting="True" DataKeyNames="Escallation_SNo" AutoGenerateColumns="False"
                                        ID="gvComm" runat="server" OnPageIndexChanging="gvComm_PageIndexChanging" Width="100%"
                                        OnSelectedIndexChanging="gvComm_SelectedIndexChanging" HorizontalAlign="Left"
                                        OnRowDataBound="gvComm_RowDataBound" OnSorting="gvComm_Sorting">
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                <HeaderStyle Width="40px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Unit_Desc" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Product" SortExpression="Unit_Desc">
                                                <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CallStage" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Call Status" SortExpression="CallStage">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="StageDesc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="MileStone" SortExpression="StageDesc">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="BHQ_Status" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="BHQ/Non-BHQ" SortExpression="BHQ_Status">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <%-- <asp:BoundField DataField="Branch_name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Branch" SortExpression="Branch_name">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>--%>
                                            <asp:BoundField DataField="SC_RepairLocation" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="At Site/Service Center" SortExpression="SC_RepairLocation">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Duration_Hours" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Duration" SortExpression="Duration_Hours">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="RoleName" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Role" SortExpression="RoleName">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="IsClosure" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Is For Closure" SortExpression="IsClosure">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <%-- <asp:BoundField DataField="UserName" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="User Id" SortExpression="UserName">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>--%>
                                            <asp:BoundField DataField="Active_Flag" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Status" SortExpression="Active_Flag">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:CommandField ShowSelectButton="True" HeaderStyle-Width="60px" HeaderText="Edit">
                                                <HeaderStyle Width="60px" />
                                            </asp:CommandField>
                                        </Columns>
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
                                    <!-- End Escallation Listing -->
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
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
                                                <asp:HiddenField ID="hdnEscallationSNo" runat="server" />
                                            </td>
                                        </tr>
                                        <%-- <tr>
                                            <td width="30%">
                                                Product Division<font color="red">*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlProductDivision" runat="server" CssClass="simpletxt1" Width="170px">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlProductDivision"
                                                    ErrorMessage="Product Division is required" InitialValue="Select" SetFocusOnError="true"
                                                    ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td width="30%">
                                                Product Division<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:CheckBoxList ID="CheckBoxProductDivision" RepeatDirection="Horizontal" RepeatColumns="6"
                                                    runat="server" OnSelectedIndexChanged="CheckBoxProductDivision_SelectedIndexChanged">
                                                </asp:CheckBoxList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                For Pending Closure<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlClosureYesNo" CssClass="simpletxt1" Width="170px" runat="server">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Call Stage<font color='red'>*</font>
                                            </td>
                                            <td>
                                            
                                                <asp:DropDownList ID="ddlCallStatus" CssClass="simpletxt1" Width="170px" runat="server"
                                                    OnSelectedIndexChanged="ddlCallStatus_SelectedIndexChanged" AutoPostBack="True">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="Initialization">Initialization</asp:ListItem>
                                                    <asp:ListItem Value="WIP">WIP</asp:ListItem>
                                                    <asp:ListItem Value="TempClosed">TempClosed</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="Select"
                                                    SetFocusOnError="true" ErrorMessage="Call Stage is required" ControlToValidate="ddlCallStatus"
                                                    ValidationGroup="editt" ></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Mile Stone<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlMileStone" runat="server" CssClass="simpletxt1" Width="420px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlMileStone"
                                                    ErrorMessage="Mile Stone is required" InitialValue="Select" SetFocusOnError="true"
                                                    ValidationGroup="editt" ></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                BHQ/Non BHQ<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlBHQ" CssClass="simpletxt1" Width="170px" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem Value="Y">BHQ</asp:ListItem>
                                                    <asp:ListItem Value="N">Non BHQ</asp:ListItem>
                                                    <asp:ListItem>NA</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="true"
                                                    ErrorMessage="BHQ/Non BHQ is required" InitialValue="Select" ControlToValidate="ddlBHQ"
                                                    ValidationGroup="editt" ></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Repair Location<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlRepairLocation" CssClass="simpletxt1" Width="170px" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem Value="SC">Service Center</asp:ListItem>
                                                    <asp:ListItem Value="AS">At Site</asp:ListItem>
                                                    <asp:ListItem Value="NA">NA</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" SetFocusOnError="true"
                                                    ErrorMessage="Repair Location is required" InitialValue="Select" ControlToValidate="ddlRepairLocation"
                                                    ValidationGroup="editt" ></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td width="30%">
                                                Duration<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlDuration" CssClass="simpletxt1" Width="130px" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem Value="24">1day</asp:ListItem>
                                                    <asp:ListItem Value="48">2day</asp:ListItem>
                                                    <asp:ListItem Value="72">3day</asp:ListItem>
                                                    <asp:ListItem Value="96">4day</asp:ListItem>
                                                    <asp:ListItem Value="120">5day</asp:ListItem>
                                                    <asp:ListItem Value="144">6day</asp:ListItem>
                                                    <asp:ListItem Value="168">7day</asp:ListItem>
                                                    <asp:ListItem Value="192">More then 7 day</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" SetFocusOnError="true"
                                                    ErrorMessage="Duration is required" InitialValue="Select" ControlToValidate="ddlDuration"
                                                    ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td width="30%">
                                                Duration(no. of days)<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDuration" MaxLength="2" CssClass="txtboxtxt" Width="165px" ValidationGroup="editt"
                                                    runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" SetFocusOnError="true"
                                                    ErrorMessage="Duration is required" ControlToValidate="txtDuration" ValidationGroup="editt"></asp:RequiredFieldValidator><asp:CompareValidator
                                                        ValidationGroup="editt" ID="CompareValidator6" runat="server" ErrorMessage="Number is required."
                                                        Operator="DataTypeCheck"  ControlToValidate="txtDuration" Type="Integer"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                Role<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlRole" CssClass="simpletxt1" Width="170px" runat="server"
                                                    AutoPostBack="false" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" InitialValue="Select"
                                                    SetFocusOnError="true" ErrorMessage="Role is required" ControlToValidate="ddlRole"
                                                    ValidationGroup="editt" ></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <%-- <tr>
                                            <td width="30%">
                                                User Id<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlUsers" CssClass="simpletxt1" runat="server" Width="170px">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" InitialValue="0"
                                                    SetFocusOnError="true" ErrorMessage="User Id is required" ControlToValidate="ddlUsers"
                                                    ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 17%">
                                                Region<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlRegion" AutoPostBack="true" runat="server" CssClass="simpletxt1"
                                                    Width="170px" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:CompareValidator ID="CVRegion" runat="server" ControlToValidate="ddlRegion"
                                                    ErrorMessage="Region is required." Operator="NotEqual" SetFocusOnError="True"
                                                    ValueToCompare="Select" ValidationGroup="editt"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 17%">
                                                Branch<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="simpletxt1" Width="170px">
                                                    <asp:ListItem Text="Select" Value="Select" Selected="True"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:CompareValidator ID="CVBranch" runat="server" ControlToValidate="ddlBranch"
                                                    ErrorMessage="Branch is required." Operator="NotEqual" SetFocusOnError="True"
                                                    ValueToCompare="Select" ValidationGroup="editt"></asp:CompareValidator>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td width="30%">
                                                Status
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rdoStatus" RepeatDirection="Horizontal" RepeatColumns="2"
                                                    runat="server">
                                                    <asp:ListItem Value="1" Text="Active" Selected="True">
                                                    </asp:ListItem>
                                                    <asp:ListItem Value="0" Text="In-Active">
                                                    </asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="25" align="left">
                                                &nbsp;
                                            </td>
                                            <td>
                                                <!-- For button portion update -->
                                                <table>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Button Text="Add" Width="70px" CssClass="btn" ID="imgBtnAdd" runat="server"
                                                                CausesValidation="True" ValidationGroup="editt" OnClientClick="return ToggleState();"
                                                                OnClick="imgBtnAdd_Click" />
                                                            <asp:Button Text="Save" Width="70px" ID="imgBtnUpdate" CssClass="btn" runat="server"
                                                                CausesValidation="True" ValidationGroup="editt" OnClientClick="return ToggleState();"
                                                                OnClick="imgBtnUpdate_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="imgBtnCancel" Width="70px" runat="server" CausesValidation="false"
                                                                CssClass="btn" Text="Cancel" OnClick="imgBtnCancel_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <!-- For button portion update end -->
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
