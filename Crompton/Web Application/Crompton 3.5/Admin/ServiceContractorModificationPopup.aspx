0<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ServiceContractorModificationPopup.aspx.cs"
    Inherits="Admin_ServiceContractorModificationPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Service Contractor Log Details</title>
</head>
<link href="../css/style.css" rel="stylesheet" type="text/css" />
<link href="../css/global.css" rel="stylesheet" type="text/css" />
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="Schm" runat="server">
    </asp:ScriptManager>
    <div>
        <asp:UpdatePanel ID="updatepnl" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td class="headingred">
                            Service Contractor History
                        </td>
                        <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                            <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="updatepnl" runat="server">
                                <ProgressTemplate>
                                    <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
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
                                </tr>
                            </table>
                            <table width="100%" border="0">
                                <tr>
                                    <td class="bgcolorcomm">
                                        <asp:GridView PageSize="50" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                            HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="true"
                                            DataKeykNames="Routing_Sno" AutoGenerateColumns="False" ID="gvComm" runat="server"
                                            OnPageIndexChanging="gvComm_PageIndexChanging" Width="100%" HorizontalAlign="Left">
                                            <RowStyle CssClass="gridbgcolor" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sno" HeaderStyle-Width="40" ItemStyle-Width="40">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex+1%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ASCName" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="ASC Name" />
                                                <asp:BoundField DataField="ContactPerson" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Contaact Person" />
                                                <asp:BoundField DataField="Address" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="ASC Address" />
                                                <asp:BoundField DataField="Region" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Region" />
                                                <asp:BoundField DataField="Branch" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Branch" />
                                                    <asp:BoundField DataField="StateName" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="State" />
                                                <asp:BoundField DataField="CityName" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="City" />                                                
                                                <asp:BoundField DataField="CreatedBy" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Created By" />
                                                <asp:BoundField DataField="CreatedDate" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Created Date" />
                                                <asp:BoundField DataField="Modifiedby" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Modified By" />
                                                <asp:BoundField DataField="ModifiedDate" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Modified Date" />
                                                <asp:BoundField DataField="Status" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Status" />
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
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <%--<tr>
                    <td colspan="2">
                    <asp:Button ID="btnExport" runat="server" Text="Export To Excel" CssClass="btn" OnClick="btnExport_Click" />
                    </td>
                    </tr>--%>
                </table>
            </ContentTemplate>
            <%--<Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
            </Triggers>--%>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
