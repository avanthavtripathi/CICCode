<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="SuspanceRequest.aspx.cs" Inherits="pages_SuspanceRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function funCommLog(compNo,splitNo)
        {
            var strUrl='CommunicationLog.aspx?CompNo='+ compNo + '&SplitNo='+ splitNo;
            window.open(strUrl,'CommunicationLog','height=550,width=750,left=20,top=30');
        }
        function funHistoryLog(compNo,splitNo)
        {
            var strUrl='HistoryLog.aspx?CompNo='+ compNo + '&SplitNo='+ splitNo;
            window.open(strUrl,'History','height=550,width=750,left=20,top=30,Location=1');
        }
         function funUserDetail(custNo, compNo)
        {
            var strUrl='UserDetail.aspx?custNo='+ custNo + '&CompNo='+ compNo;
            window.open(strUrl,'History','height=550,width=750,left=20,top=30,Location=0');
        }
    </script>
    <asp:Panel ID="panOpenCalls" runat="server">
        <table width="100%" border="0" cellpadding="1" cellspacing="0">
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4" height="1" bgcolor="#60A3AC">
                </td>
            </tr>
            <tr>
                <td>
                    <b>Open calls</b>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="dvGrid" style="width: 980px; overflow: auto;">
                        <!-- Detail grid Start-->
                        <asp:GridView PageSize="10" Width="980px" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                            HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="True"
                            AllowSorting="True" DataKeyNames="BaseLineId" AutoGenerateColumns="False" ID="gvDetails"
                            runat="server" HorizontalAlign="Left" OnPageIndexChanging="gvDetails_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                    <HeaderStyle Width="40px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Complaint_RefNo" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                    HeaderText="Pri. Complaint RefNo">
                                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                </asp:BoundField>
                                
                                 <asp:TemplateField HeaderStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    HeaderText="User Name">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="funUserDetail('<%#Eval("CustomerId")%>','<%#Eval("Complaint_RefNo")%>')">
                                            <%#Eval("Name")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    HeaderText="Status">
                                    <ItemTemplate>
                                        <%#Eval("StageDesc")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="LoggedDate" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Log Date"></asp:BoundField>
                                <asp:BoundField DataField="Unit_Desc" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Prod. Division"></asp:BoundField>
                                <asp:BoundField DataField="NatureofComplaint" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Nature of Complaint"></asp:BoundField>
                                <asp:BoundField DataField="Quantity" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                    HeaderText="Quantity">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                            <EmptyDataTemplate>
                                <b>No Records found.</b>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <!-- Detail grid End-->
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
