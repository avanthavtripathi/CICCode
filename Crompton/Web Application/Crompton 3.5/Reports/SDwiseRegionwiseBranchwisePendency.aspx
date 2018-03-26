<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" 
CodeFile="SDwiseRegionwiseBranchwisePendency.aspx.cs" Inherits="Reports_SDwiseRegionwiseBranchwisePendency" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">

    <script type="text/javascript" language="javascript">
    function funMISComplaintLocal(Type,intRegion_Sno,intBranch_Sno,intProductDivision_Sno)
    {    
        //var Region_Sno  = intRegion_Sno;       
        //var Branch_Sno = intBranch_Sno;        
        //var ProductDiv_Sno =intProductDivision_Sno;
        var Sc_Sno=0;
        funMISComplaint(Type,2,intRegion_Sno,intBranch_Sno,intProductDivision_Sno,Sc_Sno);
		//  funMISComplaint(Type,intRegion_Sno,intBranch_Sno,intProductDivision_Sno,Sc_Sno);    
    }
</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <ContentTemplate>
            <table width="100%" border="0">
                <tr>
                    <td class="headingred" style="width: 48%">
                        Service Division wise Region wise Branch wise Pendency Report</td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>               
                <tr>
                    <td colspan="2" align="left" id="trTotalRecord" runat="server">
                        Total Count:
                        <asp:Label ID="lblCount" ForeColor="Red" runat="server" Text="0"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvMIS" CssClass="simpletxt1" runat="server" RowStyle-CssClass="bgcolorcomm"
                            Width="100%" AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                            GridGroups="both"  PagerStyle-HorizontalAlign="Center" AllowSorting="True"
                            AutoGenerateColumns="False" HorizontalAlign="Left" OnPageIndexChanging="gvMIS_PageIndexChanging"
                            OnSorting="gvMIS_Sorting">
                            <Columns>
                                <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                    <HeaderStyle Width="40px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Branch" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Branch Name">
                                    <ItemTemplate>
                                    <asp:HiddenField ID="gvhdnProductDiv" runat="server" Value='<%#Eval("ProductDivision_Sno")%>' />
                                        <asp:HiddenField ID="gvhdnBranch" runat="server" 
                                        Value='<%#Eval("Branch_SNo") %>'></asp:HiddenField>
                                        <%#Eval("Branch") %>
                                    </ItemTemplate>
                                </asp:TemplateField>                               
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Total_Pending" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Total Number of open complaints">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('TOTALMIS3','<%#Eval("Region_Sno")%>','<%#Eval("Branch_SNo") %>','<%#Eval("ProductDivision_Sno")%>')">
                                           <%#Eval("Total_Pending")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="NONSRF_2_5" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Open complaints (NON SRF) with age > 2days and <= 5 days">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('NONSRF2T5MIS3','<%#Eval("Region_Sno")%>','<%#Eval("Branch_SNo") %>','<%#Eval("ProductDivision_Sno")%>')">
                                           <%#Eval("NONSRF_2_5")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="NONSRF_5" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Open complaints (NON SRF) with age > 5 days">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('NONSRF5MIS3','<%#Eval("Region_Sno")%>','<%#Eval("Branch_SNo") %>','<%#Eval("ProductDivision_Sno")%>')">
                                        <%#Eval("NONSRF_5")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="SRF_2_5" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Open complaints ( SRF) with age > 2days and <= 5 days">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SRF2T5MIS3','<%#Eval("Region_Sno")%>','<%#Eval("Branch_SNo") %>','<%#Eval("ProductDivision_Sno")%>')">
                                           <%#Eval("SRF_2_5")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="SRF_5" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Open complaints (SRF) with age > 5 days">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SRF5MIS3','<%#Eval("Region_Sno")%>','<%#Eval("Branch_SNo") %>','<%#Eval("ProductDivision_Sno")%>')">
                                        <%#Eval("SRF_5")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderStyle-Width="100px" SortExpression="NONSRF_5_15" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Open complaints (NON SRF) with age > 5 days and <= 15 days">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('NONSRF5T15MIS3','<%#Eval("Region_Sno")%>','<%#Eval("Branch_SNo") %>','<%#Eval("ProductDivision_Sno")%>')">
                                           <%#Eval("NONSRF_5_15")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="NONSRF_16_30" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Open complaints (NON SRF) with age > 16 days and <= 30 days">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('NONSRF16T30MIS3','<%#Eval("Region_Sno")%>','<%#Eval("Branch_SNo") %>','<%#Eval("ProductDivision_Sno")%>')">
                                           <%#Eval("NONSRF_16_30")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="NONSRF_30" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Open complaints (NON SRF) with age > 30 days">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('NONSRF30MIS3','<%#Eval("Region_Sno")%>','<%#Eval("Branch_SNo") %>','<%#Eval("ProductDivision_Sno")%>')">
                                        <%#Eval("NONSRF_30")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                  <asp:TemplateField HeaderStyle-Width="100px" SortExpression="SRF_5_15" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Open complaints(SRF) with age > 5 days and <= 15 days">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SRF5T15MIS3','<%#Eval("Region_Sno")%>','<%#Eval("Branch_SNo") %>','<%#Eval("ProductDivision_Sno")%>')">
                                           <%#Eval("SRF_5_15")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="SRF_16_30" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Open complaints(SRF) with age > 16 days and <= 30 days">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SRF16T30MIS3','<%#Eval("Region_Sno")%>','<%#Eval("Branch_SNo") %>','<%#Eval("ProductDivision_Sno")%>')">
                                           <%#Eval("SRF_16_30")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="SRF_30" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Open complaints(SRF) with age > 30 days">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SRF30MIS3','<%#Eval("Region_Sno")%>','<%#Eval("Branch_SNo") %>','<%#Eval("ProductDivision_Sno")%>')">
                                        <%#Eval("SRF_30")%></a>
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
                    <td align="center" colspan="2">
                       <!--<asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="Search" Width="100"
                            OnClick="btnSearch_Click" />!-->
                        <asp:Button ID="btnExport" runat="server" CssClass="btn" Text="Export To Execl"
                            Width="100" OnClick="btnExport_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvExport" CssClass="simpletxt1" runat="server" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                    <HeaderStyle Width="40px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Branch" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Branch Name">
                                    <ItemTemplate>
                                        <%#Eval("Branch") %></ItemTemplate>
                                </asp:TemplateField>                               
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Total_Pending" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Total Number of open complaints">
                                    <ItemTemplate>
                                        <%#Eval("Total_Pending")%></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Pending2To5_NonSRF" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Open complaints (NON SRF) with age > 2days and <= 5 days">
                                    <ItemTemplate>
                                        <%#Eval("NONSRF_2_5")%></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Pending>5_NonSRF" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Open complaints (NON SRF) with age > 5 days">
                                    <ItemTemplate>
                                        <%#Eval("NONSRF_5")%></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Pending2To5_SRF" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Open complaints ( SRF) with age > 2days and <= 5 days">
                                    <ItemTemplate>
                                        <%#Eval("SRF_2_5")%></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Pending>5_SRF" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Open complaints (SRF) with age > 5 days">
                                    <ItemTemplate>
                                        <%#Eval("SRF_5")%></ItemTemplate>
                                </asp:TemplateField>
                                
                                  <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Pending5To15_NonSRF" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Open complaints (NON SRF) with age > 5days and <= 15days">
                                    <ItemTemplate>
                                        <%#Eval("NONSRF_5_15")%></ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Pending16To30_NonSRF" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Open complaints (NON SRF) with age > 16days and <= 30days">
                                    <ItemTemplate>
                                        <%#Eval("NONSRF_16_30")%></ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Pending>30_NonSRF" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Open complaints (NON SRF) with age > 30 days">
                                    <ItemTemplate>
                                        <%#Eval("NONSRF_30")%></ItemTemplate>
                                </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Pending5To15_SRF" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Open complaints (SRF) with age > 5days and <= 15days">
                                    <ItemTemplate>
                                        <%#Eval("SRF_5_15")%></ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Pending16To30_SRF" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Open complaints (SRF) with age > 16days and <= 30days">
                                    <ItemTemplate>
                                        <%#Eval("SRF_16_30")%></ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Pending>30_SRF" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Open complaints (SRF) with age > 30 days">
                                    <ItemTemplate>
                                        <%#Eval("SRF_30")%></ItemTemplate>
                                </asp:TemplateField>
                                 
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

