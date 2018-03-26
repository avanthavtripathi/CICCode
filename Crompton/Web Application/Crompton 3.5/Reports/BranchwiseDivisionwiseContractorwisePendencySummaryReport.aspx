<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="BranchwiseDivisionwiseContractorwisePendencySummaryReport.aspx.cs"
    Inherits="Reports_BranchwiseDivisionwiseContractorwisePendencySummaryReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script type="text/javascript" language="javascript">
    
    function funMISComplaintLocal(Type,intBranch_Sno,intProductDivision_Sno,intSc_Sno,CGEmployee1,CGContract1,userType_Code,Total)
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
        var ProductDiv_Sno1;
        ProductDiv_Sno1= ddlProductDivision.options[ddlProductDivision.selectedIndex].value;  
            //alert(ProductDiv_Sno1); 
            // alert(Total);
        var ddlResolver= document.getElementById('ctl00_MainConHolder_ddlResolver');
         var ResolverType;
          
         if(ddlResolver!=null)
            {                             
              ResolverType=ddlResolver.options[ddlResolver.selectedIndex].value;
            }  
            else
            {
                ResolverType=0;
            }
            
        var ProductDiv_Sno = intProductDivision_Sno;//hdnProductDiv.value;
            if(ProductDiv_Sno ==0)
                {
                    ProductDiv_Sno= ddlProductDivision.options[ddlProductDivision.selectedIndex].value;      
                }
                
            var ddlBusinessLine = $get('ctl00_MainConHolder_ddlBusinessLine');            
            var BusinessLine;       
            BusinessLine=ddlBusinessLine.options[ddlBusinessLine.selectedIndex].value;  
                    
            if(BusinessLine==2)
             {               
                        
                var ddlSC = $get('ctl00_MainConHolder_ddlSC');
                var Sc_Sno=intSc_Sno;
                    if(Sc_Sno ==0)
                        {
                             Sc_Sno= ddlSC.options[ddlSC.selectedIndex].value;      
                        }
                 funMISComplaint(Type,BusinessLine,Region_Sno,Branch_Sno,intProductDivision_Sno,Sc_Sno);  
           
             }
         else if(BusinessLine==1)
         {  
                if(ResolverType==0)
                {
                   if(ProductDiv_Sno1==0)
                        {
                            funSummaryReportMTO_ALLUser(Type,Region_Sno,Branch_Sno,intProductDivision_Sno,ResolverType,intSc_Sno,CGEmployee1,CGContract1,BusinessLine);  
                        }
                        else
                        {
                            intProductDivision_Sno=ProductDiv_Sno1; 
                            funSummaryReportMTO_ALLUser(Type,Region_Sno,Branch_Sno,intProductDivision_Sno,ResolverType,intSc_Sno,CGEmployee1,CGContract1,BusinessLine);  
                        }
              
                }                
                else if(ResolverType==2)
                {
                   
                    if(Total=="Total")
                    {                       
                        if(ProductDiv_Sno1==0)
                        {
                            funSummaryReportMTO_CGUser2(Type,Region_Sno,Branch_Sno,intProductDivision_Sno,ResolverType,0,BusinessLine);  
                        }
                        else
                        {                       
                            intProductDivision_Sno=ProductDiv_Sno1;                        
                            funSummaryReportMTO_CGUser2(Type,Region_Sno,Branch_Sno,intProductDivision_Sno,ResolverType,0,BusinessLine);  
                        }
                    }
                    else
                    {                   
                        funSummaryReportMTO_CGUser2(Type,Region_Sno,Branch_Sno,intProductDivision_Sno,ResolverType,CGEmployee1,BusinessLine);  
                    } 
                     
                 }
              else if(ResolverType==5)
                {
                    if(Total=="Total")
                    {
                        if(ProductDiv_Sno1==0)
                        {
                            funSummaryReportMTO_CGContractUser2(Type,Region_Sno,Branch_Sno,intProductDivision_Sno,ResolverType,0,BusinessLine);
                         }
                        else
                        {                       
                            intProductDivision_Sno=ProductDiv_Sno1;                        
                            funSummaryReportMTO_CGContractUser2(Type,Region_Sno,Branch_Sno,intProductDivision_Sno,ResolverType,0,BusinessLine);
                        }
                    }
                    else
                    {               
                        funSummaryReportMTO_CGContractUser2(Type,Region_Sno,Branch_Sno,intProductDivision_Sno,ResolverType,CGContract1,BusinessLine);
                    }    
                }                
               else if(ResolverType==3)
               {
                    if(Total=="Total")
                    {   if(ProductDiv_Sno1==0)
                        {
                                funMISComplaintMTO(Type,BusinessLine,Region_Sno,Branch_Sno,intProductDivision_Sno,0,ResolverType);
                         }
                        else
                        {                       
                            intProductDivision_Sno=ProductDiv_Sno1;                        
                           funMISComplaintMTO(Type,BusinessLine,Region_Sno,Branch_Sno,intProductDivision_Sno,0,ResolverType);
                        }
                    }
                    else
                    {    
                        funMISComplaintMTO(Type,BusinessLine,Region_Sno,Branch_Sno,intProductDivision_Sno,intSc_Sno,ResolverType);
                    }
                  
                }    
         
            }     
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
                        MIS-2 Agewise analysis of pending service complaints 
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr><td colspan="2" style="width:100%" align="center"><table border="0" cellpadding="2" cellspacing="0">
                <tr>
                    <td align="right">
                        Business Line
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlBusinessLine" runat="server" AutoPostBack="True" 
                            CssClass="simpletxt1" 
                            OnSelectedIndexChanged="ddlBusinessLine_SelectedIndexChanged" 
                            ValidationGroup="editt" Width="175px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Region <span style="color:Red;">*</span>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="true" 
                            CssClass="simpletxt1" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" 
                            Width="175">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator InitialValue="0" ControlToValidate="ddlRegion" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Region"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Branch <span style="color:Red;">*</span>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="true" 
                            CssClass="simpletxt1" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" 
                            Width="175">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator InitialValue="0" ControlToValidate="ddlBranch" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Branch"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Product Division <span style="color:Red;">*</span>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlProductDivision" runat="server" CssClass="simpletxt1" 
                            Width="175">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator InitialValue="0" ControlToValidate="ddlProductDivision" ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Select Product Division"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr ID="trResolvertype" runat="server">
                    <td align="right">
                        Resolver Type
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlResolver" runat="server" AutoPostBack="true" 
                            CssClass="simpletxt1" OnSelectedIndexChanged="ddlResolver_SelectedIndexChanged" 
                            ValidationGroup="editt" Width="175px">
                            <asp:ListItem Selected="True" Text="All" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Service Contractor" Value="3"></asp:ListItem>
                            <asp:ListItem Text="CG Employee" Value="2"></asp:ListItem>
                            <asp:ListItem Text="CG Contract Employee" Value="5"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <div ID="divSC" runat="server" visible="false">
                    <tr>
                        <td align="right">
                            Service Contractor
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlSC" runat="server" CssClass="simpletxt1" Width="175">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </div>
                <tr ID="trCGExce" runat="server" visible="false">
                    <td align="right">
                        CG Employee
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlCGExec" runat="server" AutoPostBack="true" 
                            CssClass="simpletxt1" ValidationGroup="editt" Width="175">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr ID="trCgContractEmp" runat="server" visible="false">
                    <td align="right">
                        CG Contract Employee
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlCGContractEmp" runat="server" AutoPostBack="true" 
                            CssClass="simpletxt1" ValidationGroup="editt" Width="175">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn" 
                            OnClick="btnSearch_Click" Text="Search" Width="100" />
                        <asp:Button ID="btnExport" runat="server" CssClass="btn" 
                            OnClick="btnExport_Click" Text="Export To Execl" Visible="false" Width="100" />
                    </td>
                </tr>
             </table></td></tr>   
                <tr>
                    <td align="left" colspan="2">
                        Total Count:
                        <asp:Label ID="lblCount" runat="server" ForeColor="Red" Text="0"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvMIS" runat="server" AllowSorting="True" 
                            AlternatingRowStyle-CssClass="fieldName" AutoGenerateColumns="False" 
                            CssClass="simpletxt1" GridGroups="both" 
                            HeaderStyle-CssClass="fieldNamewithbgcolor" HorizontalAlign="Left" 
                            OnPageIndexChanging="gvMIS_PageIndexChanging" 
                            OnRowDataBound="gvMIS_RowDataBound" OnSorting="gvMIS_Sorting" 
                            PagerStyle-HorizontalAlign="Center" RowStyle-CssClass="bgcolorcomm" 
                            Width="100%">
                            <RowStyle CssClass="bgcolorcomm" />
                            <Columns>
                                <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                    <HeaderStyle Width="40px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" 
                                    HeaderText="Branch Name" ItemStyle-HorizontalAlign="Left" 
                                    SortExpression="Branch_Name">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="gvhdnBranch" runat="server" 
                                            Value='<%#Eval("Branch_SNo") %>' />
                                        <%#Eval("Branch_Name") %>
                                        <asp:HiddenField ID="gvhdnContractorStatus" runat="server" 
                                            Value='<%#Eval("ContractorStatus") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CGEmployee" HeaderStyle-HorizontalAlign="Left" 
                                        HeaderText="CG Employee" ItemStyle-HorizontalAlign="Left" 
                                        SortExpression="CGEmployee">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CGContract" HeaderStyle-HorizontalAlign="Left" 
                                        HeaderText="CG Contract Employee" ItemStyle-HorizontalAlign="Left" 
                                        SortExpression="CGContract">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" 
                                        HeaderText="Product Division" ItemStyle-HorizontalAlign="Left" 
                                        SortExpression="ProductDivision_Desc">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="gvhdnProductDiv" runat="server" 
                                                Value='<%#Eval("ProductDivision_Sno")%>' />
                                            <%#Eval("ProductDivision_Desc")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" 
                                        HeaderText="Service Contractor" ItemStyle-HorizontalAlign="Left" 
                                        SortExpression="SC_Name">
                                        <ItemTemplate>
                                            <%#Eval("SC_Name")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" 
                                        HeaderText="Total Number of open complaints" ItemStyle-HorizontalAlign="Left" 
                                        SortExpression="TotalPending">
                                        <ItemTemplate>
                                            <a href="Javascript:void(0);" 
                                                onclick='javascript:return funMISComplaintLocal(&#039;TOTALMIS2&#039;,&#039;<%#Eval("Branch_SNo") %>&#039;,&#039;<%#Eval("ProductDivision_Sno")%>&#039;,&#039;<%#Eval("Sc_Sno") %>&#039;,&#039;<%#Eval("CGEmployee1") %>&#039;,&#039;<%#Eval("CGContract1") %>&#039;,&#039;<%#Eval("userType_Code") %>&#039;,&#039;<%#Eval("ProductDivision_Desc")%>&#039;)'>
                                            <%#Eval("TotalPending")%></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" 
                                        HeaderText="Open complaints(NON SRF)with age&gt;2days and&lt;=5days" 
                                        ItemStyle-HorizontalAlign="Left" SortExpression="Pending2To5_NonSRF">
                                        <ItemTemplate>
                                            <a href="Javascript:void(0);" 
                                                onclick='javascript:return funMISComplaintLocal(&#039;NONSRF2T5MIS2&#039;,&#039;<%#Eval("Branch_SNo") %>&#039;,&#039;<%#Eval("ProductDivision_Sno")%>&#039;,&#039;<%#Eval("Sc_Sno") %>&#039;,&#039;<%#Eval("CGEmployee1") %>&#039;,&#039;<%#Eval("CGContract1") %>&#039;,&#039;<%#Eval("userType_Code") %>&#039;)'>
                                            <%#Eval("Pending2To5_NonSRF")%></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" 
                                        HeaderText="Open complaints(non SRF)with age&gt;5days" 
                                        ItemStyle-HorizontalAlign="Left" SortExpression="Pending&gt;5_NonSRF">
                                        <ItemTemplate>
                                            <a href="Javascript:void(0);" 
                                                onclick='javascript:return funMISComplaintLocal(&#039;NONSRF5MIS2&#039;,&#039;<%#Eval("Branch_SNo") %>&#039;,&#039;<%#Eval("ProductDivision_Sno")%>&#039;,&#039;<%#Eval("Sc_Sno") %>&#039;,&#039;<%#Eval("CGEmployee1") %>&#039;,&#039;<%#Eval("CGContract1") %>&#039;,&#039;<%#Eval("userType_Code") %>&#039;)'>
                                            <%#Eval("Pending>5_NonSRF")%></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" 
                                        HeaderText="Open complaints(SRF)with age&gt;2days and&lt;=5days" 
                                        ItemStyle-HorizontalAlign="Left" SortExpression="Pending2To5_SRF">
                                        <ItemTemplate>
                                            <a href="Javascript:void(0);" 
                                                onclick='javascript:return funMISComplaintLocal(&#039;SRF2T5MIS2&#039;,&#039;<%#Eval("Branch_SNo") %>&#039;,&#039;<%#Eval("ProductDivision_Sno")%>&#039;,&#039;<%#Eval("Sc_Sno") %>&#039;,&#039;<%#Eval("CGEmployee1") %>&#039;,&#039;<%#Eval("CGContract1") %>&#039;,&#039;<%#Eval("userType_Code") %>&#039;)'>
                                            <%#Eval("Pending2To5_SRF")%></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" 
                                        HeaderText="Open complaints(SRF)with age&gt;5days" 
                                        ItemStyle-HorizontalAlign="Left" SortExpression="Pending&gt;5_SRF">
                                        <ItemTemplate>
                                            <a href="Javascript:void(0);" 
                                                onclick='javascript:return funMISComplaintLocal(&#039;SRF5MIS2&#039;,&#039;<%#Eval("Branch_SNo") %>&#039;,&#039;<%#Eval("ProductDivision_Sno")%>&#039;,&#039;<%#Eval("Sc_Sno") %>&#039;,&#039;<%#Eval("CGEmployee1") %>&#039;,&#039;<%#Eval("CGContract1") %>&#039;,&#039;<%#Eval("userType_Code") %>&#039;)'>
                                            <%#Eval("Pending>5_SRF")%></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="usertype_code" HeaderText="" Visible="false" />
                                </Columns>
                                <PagerStyle HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td align="center" 
                                                style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                <img alt="" src='<%=ConfigurationManager.AppSettings["UserMessage"]%>' /> <b>No 
                                                Record found</b>
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                <AlternatingRowStyle CssClass="fieldName" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gvExport" runat="server" AutoGenerateColumns="False" 
                                CssClass="simpletxt1" Visible="False">
                                <Columns>
                                    <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                        <HeaderStyle Width="40px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" 
                                        HeaderText="Branch Name" ItemStyle-HorizontalAlign="Left" 
                                        SortExpression="Branch_Name">
                                        <ItemTemplate>
                                            <%#Eval("Branch_Name") %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CGEmployee" HeaderStyle-HorizontalAlign="Left" 
                                        HeaderText="CG Employee" ItemStyle-HorizontalAlign="Left" 
                                        SortExpression="CGEmployee">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CGContract" HeaderStyle-HorizontalAlign="Left" 
                                        HeaderText="CG Contract Employee" ItemStyle-HorizontalAlign="Left" 
                                        SortExpression="CGContract">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" 
                                        HeaderText="Product Division" ItemStyle-HorizontalAlign="Left" 
                                        SortExpression="ProductDivision_Desc">
                                        <ItemTemplate>
                                            <%#Eval("ProductDivision_Desc")%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" 
                                        HeaderText="Service Contractor" ItemStyle-HorizontalAlign="Left" 
                                        SortExpression="SC_Name">
                                        <ItemTemplate>
                                            <%#Eval("SC_Name")%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" 
                                        HeaderText="Total Pending Complaints" ItemStyle-HorizontalAlign="Left" 
                                        SortExpression="TotalPending">
                                        <ItemTemplate>
                                            <%#Eval("TotalPending")%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" 
                                        HeaderText="Site Complaints Pending 2To5 Days" ItemStyle-HorizontalAlign="Left" 
                                        SortExpression="Pending2To5_NonSRF">
                                        <ItemTemplate>
                                            <%#Eval("Pending2To5_NonSRF")%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" 
                                        HeaderText="Site Complaints Pending &gt;5 Days" 
                                        ItemStyle-HorizontalAlign="Left" SortExpression="Pending&gt;5_NonSRF">
                                        <ItemTemplate>
                                            <%#Eval("Pending>5_NonSRF")%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" 
                                        HeaderText="SRF Complaints Pending 2To5 Days" ItemStyle-HorizontalAlign="Left" 
                                        SortExpression="Pending2To5_SRF">
                                        <ItemTemplate>
                                            <%#Eval("Pending2To5_SRF")%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" 
                                        HeaderText="SRF Complaints Pending &gt;5 Days" ItemStyle-HorizontalAlign="Left" 
                                        SortExpression="Pending&gt;5_SRF">
                                        <ItemTemplate>
                                            <%#Eval("Pending>5_SRF")%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="usertype_code" HeaderText="" Visible="false" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
