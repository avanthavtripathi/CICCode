<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" EnableEventValidation="false"  
    CodeFile="BranchWiseDivisionWisePendency.aspx.cs" Inherits="Reports_BranchWiseDivisionWisePendency" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script type="text/javascript" language="javascript">
    function funMISComplaintLocal(Type,intBranch_Sno,intProductDivision_Sno)
    {
    
        var ddlRegion  = $get('ctl00_MainConHolder_ddlRegion');
        var Region_Sno = ddlRegion.options[ddlRegion.selectedIndex].value;
        
        var ddlBranch  = $get('ctl00_MainConHolder_ddlBranch');
        var Branch_Sno = intBranch_Sno;//hdnBranch.value;
        if(Branch_Sno ==0)
            {
                 Branch_Sno= ddlBranch.options[ddlBranch.selectedIndex].value;      
            }
        
        var ddlProductDivision = $get('ctl00_MainConHolder_ddlProductDivision');
        var ProductDiv_Sno = intProductDivision_Sno;//hdnProductDiv.value;
        if(ProductDiv_Sno ==0)
            {
                 ProductDiv_Sno= ddlProductDivision.options[ddlProductDivision.selectedIndex].value;      
            }
        
        var Sc_Sno=0;
        funMISComplaint(Type,2,Region_Sno,Branch_Sno,ProductDiv_Sno,Sc_Sno);  
    }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <ContentTemplate>
            <table width="100%" border="0">
                <tr>
                    <td class="headingred" style="width: 40%">
                        MIS-1 Agewise analysis of pending service complaints (branch and division wise) 
                        <%--Branch Wise Division Wise Pendency Report--%>
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                  <tr>
                        <td width="30%" align="right">
                            Business Line:
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlBusinessLine" AutoPostBack="True" runat="server" Width="175px"
                                CssClass="simpletxt1" ValidationGroup="editt" OnSelectedIndexChanged="ddlBusinessLine_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                <tr>
                    <td align="right">
                        Region:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlRegion" Width="175" AutoPostBack="true" runat="server" CssClass="simpletxt1"
                            OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Branch:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBranch" Width="175" AutoPostBack="true" runat="server" CssClass="simpletxt1"
                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Product Division:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlProductDivision" Width="175" runat="server" CssClass="simpletxt1">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="Search" Width="100"
                            OnClick="btnSearch_Click" />
                        <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn" Text="Export To Execl" Width="100" OnClick="btnExport_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left">
                        Total Count:
                        <asp:Label ID="lblCount" ForeColor="Red" runat="server" Text="0"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvMIS" CssClass="simpletxt1" runat="server" RowStyle-CssClass="bgcolorcomm"
                            Width="100%" AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                            GridGroups="both" AllowPaging="false" PagerStyle-HorizontalAlign="Center"
                            AutoGenerateColumns="False" HorizontalAlign="Left" >
                            <Columns>
                                  <asp:TemplateField HeaderText="Sno" HeaderStyle-Width="40px" >
                                   <ItemTemplate>   
                                       <%# ((GridViewRow)Container).RowIndex + 1%>
                                   </ItemTemplate>
                                 </asp:TemplateField>
                                
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Branch_Name" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Branch Name">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="gvhdnBranch" runat="server" Value='<%#Eval("Branch_SNo") %>'>
                                        </asp:HiddenField>
                                        <%#Eval("Branch_Name") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="ProductDivision_Desc"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Product Division">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="gvhdnProductDiv" runat="server" Value='<%#Eval("ProductDivision_Sno")%>'>
                                        </asp:HiddenField>
                                        <%#Eval("ProductDivision_Desc")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="TotalPending" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Total Number of open complaints">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('TOTALMIS1','<%#Eval("Branch_SNo") %>','<%#Eval("ProductDivision_Sno")%>')">
                                            <%#Eval("TotalPending")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Pending2To5_NonSRF"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Open complaints(NON SRF)with age>2days and<=5days">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('NONSRF2T5MIS1','<%#Eval("Branch_SNo") %>','<%#Eval("ProductDivision_Sno")%>')">
                                            <%#Eval("Pending2To5_NonSRF")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Pending>5_NonSRF" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Open complaints(non SRF)with age>5 days">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('NONSRF5MIS1','<%#Eval("Branch_SNo") %>','<%#Eval("ProductDivision_Sno")%>')">
                                            <%#Eval("Pending>5_NonSRF")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Pending2To5_SRF" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="SRF Complaints Pending 2To5 Days">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SRF2T5MIS1','<%#Eval("Branch_SNo") %>','<%#Eval("ProductDivision_Sno")%>')">
                                            <%#Eval("Pending2To5_SRF")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Pending>5_SRF" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Open Complaints(SRF)with age>2days and<=5days">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SRF5MIS1','<%#Eval("Branch_SNo") %>','<%#Eval("ProductDivision_Sno")%>')">
                                            <%#Eval("Pending>5_SRF")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
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
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvExport" CssClass="simpletxt1" runat="server" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                    <HeaderStyle Width="40px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Branch_Name" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Branch Name">
                                    <ItemTemplate>
                                        <%#Eval("Branch_Name") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="ProductDivision_Desc"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Product Division">
                                    <ItemTemplate>
                                        <%#Eval("ProductDivision_Desc")%></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="TotalPending" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Total Pending Complaints">
                                    <ItemTemplate>
                                        <%#Eval("TotalPending")%></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Pending2To5_NonSRF"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Site Complaints Pending 2To5 Days">
                                    <ItemTemplate>
                                        <%#Eval("Pending2To5_NonSRF")%></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Pending>5_NonSRF" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Site Complaints Pending >5 Days">
                                    <ItemTemplate>
                                        <%#Eval("Pending>5_NonSRF")%></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Pending2To5_SRF" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="SRF Complaints Pending 2To5 Days">
                                    <ItemTemplate>
                                        <%#Eval("Pending2To5_SRF")%></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Pending>5_SRF" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="SRF Complaints Pending >5 Days">
                                    <ItemTemplate>
                                        <%#Eval("Pending>5_SRF")%></ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
