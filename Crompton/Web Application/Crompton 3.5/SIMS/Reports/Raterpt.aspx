<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true" CodeFile="Raterpt.aspx.cs" Inherits="SIMS_Reports_Raterpt" Title="Rate Sheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function SearchPostBack()
        {
            var btn = document.getElementById('ctl00_MainConHolder_imgBtnGo');
            if (btn) btn.click();            
        }
    </script>

    <asp:UpdatePanel ID="updatepnl" runat="server">
    <Triggers>
        <asp:PostBackTrigger ControlID="btnExportToExcela" />
        <asp:PostBackTrigger ControlID="BtnExportForUpdate" />
    </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Rates Details 
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
                    <td align="right" colspan="2">
                        
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
                <caption>
                    <tr>
                        <td align="center" colspan="2" style="padding: 10px">
                            <table border="0" width="100%">
                                <tr>
                                    <td align="left" class="MsgTDCount">
                                        Total Number of Records :
                                        <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                    </td>
                                    <td align="right">
                                        Search For
                                        <asp:DropDownList ID="ddlSearch" runat="server" CssClass="simpletxt1" Width="130px">
                                            <asp:ListItem Text="Product Division" Value="Unit_Desc"></asp:ListItem>
                                            <asp:ListItem Text="Activity" Value="Activity_Description"></asp:ListItem>
                                            <asp:ListItem Text="Parameter" Value="Parameter_Description"></asp:ListItem>
                                            <%--<asp:ListItem Text="Possible Value" Value="Possible_Value"></asp:ListItem>--%>
                                        </asp:DropDownList>
                                        With
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="txtboxtxt" Text="" Width="100px"></asp:TextBox>
                                        <asp:Button ID="imgBtnGo" runat="server" CausesValidation="False" CssClass="btn"
                                            OnClick="imgBtnGo_Click" Text="Go" ValidationGroup="editt" Width="25px" />
                                    </td>
                                </tr>
                            </table>
                            <table border="0" width="100%">
                                <tr>
                                    <td>
                                        <!-- Product Line Listing   -->
                                        <asp:GridView ID="gvComm" runat="server" AllowPaging="True" AllowSorting="True" AlternatingRowStyle-CssClass="fieldName"
                                            AutoGenerateColumns="False" DataKeyNames="ActivityParameter_SNo" GridLines="both"
                                            PageSize="10" Width="100%" HorizontalAlign="Left" OnPageIndexChanging="gvComm_PageIndexChanging"
                                           EnableSortingAndPagingCallbacks="True"
                                            OnSorting="gvComm_Sorting">
                                            <Columns>
                                                <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                    <HeaderStyle Width="40px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Unit_Desc" SortExpression="Unit_Desc" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Product Division" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Activity_Code" SortExpression="Activity_Code" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Activity Code" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Activity_Description" SortExpression="Activity_Description"
                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Activity" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Parameter_Code1" SortExpression="Parameter_Code1" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="60px" HeaderText="Parameter-1" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Possible_Value1" SortExpression="Possible_Value1" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="70px" HeaderText="Possible Value-1" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Parameter_Code2" SortExpression="Parameter_Code2" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Parameter-2" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Possible_Value2" SortExpression="Possible_Value2" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="70px" HeaderText="Possible Value-2" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Parameter_Code3" SortExpression="Parameter_Code3" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Parameter-3" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Possible_Value3" SortExpression="Possible_Value3" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="70px" HeaderText="Possible Value-3" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Parameter_Code4" SortExpression="Parameter_Code4" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Parameter-4" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Possible_Value4" SortExpression="Possible_Value4" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="70px" HeaderText="Possible Value-4" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="UOM" SortExpression="UOM" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="UOM" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Rate" SortExpression="Rate" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Rate" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SC_Name" SortExpression="SC_Name" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="SC Name" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                
                                                <asp:BoundField DataField="Actual" SortExpression="Actual" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Actual" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                
                                                <asp:BoundField DataField="Active_Flag" SortExpression="Active_Flag" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Status" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                         </Columns>
                                            <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                            <AlternatingRowStyle CssClass="fieldName" />
                                            <RowStyle CssClass="gridbgcolor" />
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
                                        </asp:GridView>
                           
                                        <asp:GridView ID="gvexcel" runat="server" AutoGenerateColumns="False" Width="100%" HorizontalAlign="Left" >
                                            <Columns>
                                                <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                    <HeaderStyle Width="40px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Unit_Desc" SortExpression="Unit_Desc" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Product Division" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Activity_Code" SortExpression="Activity_Code" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Activity Code" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Activity_Description" SortExpression="Activity_Description"
                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Activity" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Parameter_Code1" SortExpression="Parameter_Code1" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="60px" HeaderText="Parameter-1" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Possible_Value1" SortExpression="Possible_Value1" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="70px" HeaderText="Possible Value-1" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Parameter_Code2" SortExpression="Parameter_Code2" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Parameter-2" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Possible_Value2" SortExpression="Possible_Value2" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="70px" HeaderText="Possible Value-2" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Parameter_Code3" SortExpression="Parameter_Code3" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Parameter-3" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Possible_Value3" SortExpression="Possible_Value3" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="70px" HeaderText="Possible Value-3" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Parameter_Code4" SortExpression="Parameter_Code4" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Parameter-4" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Possible_Value4" SortExpression="Possible_Value4" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="70px" HeaderText="Possible Value-4" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="UOM" SortExpression="UOM" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="UOM" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Rate" SortExpression="Rate" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Rate" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SC_Name" SortExpression="SC_Name" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="SC Name" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                
                                                <asp:BoundField DataField="Actual" SortExpression="Actual" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Actual" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                
                                                <asp:BoundField DataField="Active_Flag" SortExpression="Active_Flag" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Status" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                              <%--  <asp:BoundField DataField="UCode"  HeaderText="DivisionCode" />
                                                <asp:BoundField DataField="pcode1"  HeaderText="ParamCode1" />
                                                <asp:BoundField DataField="pcode2"  HeaderText="ParamCode2" />
                                                <asp:BoundField DataField="pcode3"  HeaderText="ParamCode3" />
                                                <asp:BoundField DataField="pcode4"  HeaderText="ParamCode4" />
                                                <asp:BoundField DataField="ActualFlag"  HeaderText="ActualFlag" />
                                                <asp:BoundField DataField="ActiveFlag"  HeaderText="ActiveFlag" />--%>
                                                
                           
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
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                       <asp:Button Width="85px" Text="Export to Excel" CssClass="btn" 
                                        CausesValidation="false" ID="btnExportToExcela" runat="server"
                                        OnClick="btnExportToExcel_Click" /> 
                                        
                                         <asp:Button Width="85px" Text="Export for Update" CssClass="btn" 
                                        CausesValidation="false" ID="BtnExportForUpdate" runat="server" onclick="BtnExportForUpdate_Click"
                                         /> 
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

