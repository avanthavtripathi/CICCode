<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" 
CodeFile="MIS3-ASC.aspx.cs" Inherits="Reports_MIS3_ASC" Title="MIS3-ASC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">


<script type="text/javascript" language="javascript">
    function funMISComplaintLocal(Type,intBranch_Sno,intProductDivision_Sno,intSc_Sno)
    {
    
        var ddlRegion  = $get('ctl00_MainConHolder_ddlRegion');
        var Region_Sno = ddlRegion.options[ddlRegion.selectedIndex].value;
        
        var ddlBranch  = $get('ctl00_MainConHolder_ddlBranch');
        var Branch_Sno = intBranch_Sno;
        if(Branch_Sno ==0)
            {
                 Branch_Sno= ddlBranch.options[ddlBranch.selectedIndex].value;      
            }
        
        var ddlProductDivision = $get('ctl00_MainConHolder_ddlProductDivision');
        var ProductDiv_Sno = intProductDivision_Sno;
        if(ProductDiv_Sno ==0)
            {
                 ProductDiv_Sno= ddlProductDivision.options[ddlProductDivision.selectedIndex].value;      
            }
        var ddlSC = $get('ctl00_MainConHolder_ddlSC');
        var Sc_Sno=intSc_Sno;
        if(Sc_Sno ==0)
            {
                 Sc_Sno= ddlSC.options[ddlSC.selectedIndex].value;      
            }
        funMISComplaint(Type,Region_Sno,Branch_Sno,ProductDiv_Sno,Sc_Sno);  
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
                        Service temporarily closed complaints at branches		
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                
               <%-- SEARCH FILTER--%>
               
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
                    <td align="right">
                        Service Contractor:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSC" Width="175" runat="server" CssClass="simpletxt1">
                        </asp:DropDownList>
                    </td>
                </tr>
              
              
               <%-- SEARCH FILTER END--%>
               
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="Search" Width="100"
                            OnClick="btnSearch_Click" />
                        <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn" Text="Export To Execl"
                            Width="100" OnClick="btnExport_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left">
                        Total Count:
                        <asp:Label ID="lblCount" ForeColor="Red" runat="server" Text="0"></asp:Label>
                    </td>
                </tr>
                
                <%--SEARCH GRIDVIEW  START--%>
                
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvMIS" CssClass="simpletxt1" runat="server" RowStyle-CssClass="bgcolorcomm"
                            Width="100%" AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                            GridGroups="both" AllowPaging="false" PagerStyle-HorizontalAlign="Center" AllowSorting="True"
                            AutoGenerateColumns="False" HorizontalAlign="Left" OnPageIndexChanging="gvMIS_PageIndexChanging"
                            OnSorting="gvMIS_Sorting">
                            <Columns>
                            
                                <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                    <HeaderStyle Width="40px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Branch_Name" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Branch">
                                    <ItemTemplate>
                                        <%#Eval("Branch_Name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                               
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="ProductDivision_Desc" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Product Division">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="gvhdnBranch" runat="server" 
                                        Value='<%#Eval("Branch_SNo") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="gvhdnProductDiv" runat="server" 
                                        Value='<%#Eval("ProductDivision_Sno")%>'></asp:HiddenField>
                                        <%#Eval("ProductDivision_Desc")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                             
                              
                              
                              
                               <asp:TemplateField HeaderStyle-Width="100px" SortExpression="SC_Name" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Service Contractor">
                                    <ItemTemplate>
                                        <%#Eval("SC_Name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              
                              
                             
                                
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="[Temp Closed <=7]" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Temp. Closed Complaints age <=7 days">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('MIS4POPUP7DAY','<%#Eval("Branch_SNo") %>','<%#Eval("ProductDivision_Sno")%>','<%#Eval("Sc_Sno") %>')">
                                           <%#Eval("[Temp Closed <=7]")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="[Temp Closed >7 and <=15]" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Temp. Closed complaints age >7 <= 15 days">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('MIS4POPUP15DAY','<%#Eval("Branch_SNo") %>','<%#Eval("ProductDivision_Sno")%>','<%#Eval("Sc_Sno") %>')">
                                           <%#Eval("[Temp Closed >7 and <=15]")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="[TotalTempClosedComplaints >15]" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Temp. closed complaints age >15 days">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('MIS4POPUPG15DAY','<%#Eval("Branch_SNo") %>','<%#Eval("ProductDivision_Sno")%>','<%#Eval("Sc_Sno") %>')">
                                        <%#Eval("[TotalTempClosedComplaints >15]")%></a>
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
                
               <%-- SEARCH GRIDVIEW END--%>
               
               
               <%--EXPORT TO EXCEL GRIDVIEW--%>
               
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

                                
                                
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="ProductDivision_Desc" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Product Division">
                                    <ItemTemplate>
                                        <%#Eval("ProductDivision_Desc")%></ItemTemplate>
                                </asp:TemplateField>
                                
                                
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="TotalPending" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Service Contractor">
                                    <ItemTemplate>
                                        <%#Eval("TotalPending")%></ItemTemplate>
                                </asp:TemplateField>
                                
                                
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Pending2To5_NonSRF" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Temp. Closed Complaints age <=7 days">
                                    <ItemTemplate>
                                        <%#Eval("Pending2To5_NonSRF")%></ItemTemplate>
                                </asp:TemplateField>
                                
                                
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Pending>5_NonSRF" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Temp. Closed complaints age >7 <= 15 days">
                                    <ItemTemplate>
                                        <%#Eval("Pending>5_NonSRF")%></ItemTemplate>
                                </asp:TemplateField>
                                
                                
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Pending2To5_SRF" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Temp. closed complaints age >15 days">
                                    <ItemTemplate>
                                        <%#Eval("Pending2To5_SRF")%></ItemTemplate>
                                </asp:TemplateField>
                                
                                
                               
                                
                                
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                
               <%--EXPORT TO EXCEL GRIDVIEW END--%>
                
                
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>



