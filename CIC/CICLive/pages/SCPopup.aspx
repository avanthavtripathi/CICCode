<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SCPopup.aspx.cs" Inherits="pages_SCPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SCPopup Page</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/global.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
             function funLandmark(TSNo,PdNo)
                {
                    var strUrl='LandMarkPopup.aspx?TSNo='+TSNo+'&PdNo='+ PdNo;
                    //alert(strUrl);
                        window.open(strUrl,'Landmark','height=550,width=950,left=20,top=30');
                 }      
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div align="center">
        <asp:UpdatePanel ID="pnl" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <table width="100%">
                    <tr bgcolor="white">
                        <td class="headingred" align="left">
                            Service Contractor Details
                        </td>
                        <td>
                            <a href="javascript:void(0);" class="links" onclick="window.close();">Close</a>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 10px" align="center" colspan="2" class="bgcolorcomm">
                            <div id="dv">
                                <table width="98%" border="0">
                                    <tr height="25">
                                        <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                                            <asp:UpdateProgress AssociatedUpdatePanelID="pnl" ID="UpdateProgress1" runat="server">
                                                <ProgressTemplate>
                                                    <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100%" align="left">
                                            <table width="98%" border="0" id="tableHeader" runat="server">
                                                
                                                <tr>
                                                    <td class="style1">
                                                        State&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlState" ValidationGroup="Search" runat="server" CssClass="simpletxt1"
                                                            AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" Width="175px">
                                                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style1">
                                                        City
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlCity" ValidationGroup="Search" runat="server" CssClass="simpletxt1"
                                                            AutoPostBack="True" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" Width="175px">
                                                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style1">
                                                        Territory
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlTerritory" ValidationGroup="Search" runat="server" Width="175px"
                                                            CssClass="simpletxt1">
                                                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="25" align="left" class="style1">
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <!-- For button portion update -->
                                                        <table>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Button Text="Search" Width="70px" ID="imgBtnSearch" CssClass="btn" runat="server"
                                                                        CausesValidation="true" ValidationGroup="Search" OnClick="imgBtnSearch_Click" />
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="imgBtnCancel" Width="70px" runat="server" CssClass="btn" Text="Cancel"
                                                                        OnClick="imgBtnCancel_Click" /><asp:HiddenField ID="hdnTerritoryDesc" runat="server" />
                                                                    <asp:HiddenField ID="hdnScNo" runat="server" />
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
                                        <td>
                                            <asp:HiddenField ID="hdnType" runat="server" />
                                            <div id="dvGrid" style="width: 850px; height: 100px; overflow: auto;">
                                                <!-- Service Contractor Listing   -->
                                                <asp:GridView RowStyle-CssClass="gridbgcolor" PageSize="5" AlternatingRowStyle-CssClass="fieldName"
                                                    HeaderStyle-CssClass="fieldNamewithbgcolor" AllowPaging="True" AllowSorting="True"
                                                    DataKeyNames="Routing_Sno" AutoGenerateColumns="False" ID="gvComm" runat="server"
                                                    OnPageIndexChanging="gvComm_PageIndexChanging" Width="100%" HorizontalAlign="Left"
                                                    OnSelectedIndexChanging="gvComm_SelectedIndexChanging">
                                                    <RowStyle CssClass="gridbgcolor" />
                                                    <Columns>
                                                        <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                            <HeaderStyle Width="40px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SC_Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="SC Name">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Contact_Person" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Contact Person">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Address1" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Address">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="EmailID" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="EmailID">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PhoneNo" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Phone No">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FaxNo" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Fax No">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Territory_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Territory">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="city_desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="City">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="state_desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="State">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SpecialRemarks" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Remarks">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Weekly_Off_Day" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Weekly Off Day">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderStyle-Width="80px" Visible="false" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" HeaderText="Landmark">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdnGridScNo" Value='<%#Eval("Sc_Sno")%>' runat="server" />
                                                                <asp:HiddenField ID="hdnGridTerritoryDesc" runat="server" Value='<%#Eval("Territory_Desc")%>' />
                                                                 <asp:HiddenField ID="hdnGridWO" runat="server" Value='<%#Eval("Weekly_Off_Day")%>' />
                                                                <a href="Javascript:void(0);" onclick="funLandmark(<%#Eval("Territory_Sno")%>,<%#Eval("Unit_SNo")%>)">
                                                                    Landmark</a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                       <%-- <asp:ButtonField ButtonType="Link" Text="Select" HeaderText="Select" CommandName="Select" />--%>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <b>No records found</b></EmptyDataTemplate>
                                                    <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                    <AlternatingRowStyle CssClass="fieldName" />
                                                </asp:GridView>
                                                <!-- End Service Contractor Listing -->
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnOk" runat="server" Text="Accept" CssClass="btn" OnClick="btnOk_Click" />&nbsp;
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn" OnClick="btnCancel_Click" />
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
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
