<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="SummaryResolutionTimeAnalysisNew.aspx.cs" Inherits="Reports_SummaryResolutionTimeAnalysisSLA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script type="text/javascript" language="javascript">
    function funMISComplaintLocal(Type,region,branch,month,Week)
    {
        var varmonth=month.substring(0,3);
        var varYear =month.substring(4,6);
        var ddlRegion  = $get('ctl00_MainConHolder_ddlRegion');
        var Region_Sno = ddlRegion.options[ddlRegion.selectedIndex].value;
        
        var ddlBranch  = $get('ctl00_MainConHolder_ddlBranch');
        var Branch_Sno
        Branch_Sno= ddlBranch.options[ddlBranch.selectedIndex].value;      
        
        var ddlProductDivision = $get('ctl00_MainConHolder_ddlProductDivision');
        var ProductDiv_Sno;
        ProductDiv_Sno= ddlProductDivision.options[ddlProductDivision.selectedIndex].value;         
        var ddlBusinessLine = $get('ctl00_MainConHolder_ddlBusinessLine');            
        var BusinessLine;       
        BusinessLine=ddlBusinessLine.options[ddlBusinessLine.selectedIndex].value;           
          
         var ddlSC = document.getElementById('ctl00_MainConHolder_ddlSC');
         var SC_No;
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
         var ddlCGExec;
         var CG_User;
         var ddlCGContractEmp 
         var CG_Contract_Emp;
     
       
        if(BusinessLine==2)
        {
            Region_Sno = region
            Branch_Sno = branch
            ddlSC = document.getElementById('ctl00_MainConHolder_ddlSC');  
            if(ddlSC!=null)
            {                
              SC_No= ddlSC.options[ddlSC.selectedIndex].value;
            }               
           // funSummaryReportMTSNew(Type,Region_Sno,Branch_Sno,ProductDiv_Sno,SC_No,BusinessLine,varYear,varmonth,Week); By BP
           // Added By Bhawesh 13-March-13
            var ddlPgp  = $get('ctl00_MainConHolder_DDlPgp');
            var ddlPgp_Sno = ddlPgp.options[ddlPgp.selectedIndex].value;
            funSummaryReportMTSWithGroup(Type,Region_Sno,Branch_Sno,ProductDiv_Sno,SC_No,BusinessLine,varYear,varmonth,Week,ddlPgp_Sno);  
        }
        else if(BusinessLine==1)
        {                   
                if(ResolverType==0)
                {                    
                     funSummaryReportMTO_ALLUserMIS6New(Type,Region_Sno,Branch_Sno,ProductDiv_Sno,ResolverType,0,'NA','NA',BusinessLine,varYear,varmonth,Week);  
                }
                else if(ResolverType==2)
                {
                
                   ddlCGExec = document.getElementById('ctl00_MainConHolder_ddlCGExec');       
                    if(ddlCGExec!=null)
                      {                                
                          CG_User= ddlCGExec.options[ddlCGExec.selectedIndex].value;
                       }
                       
                   funSummaryReportMTO_CGUserNew(Type,Region_Sno,Branch_Sno,ProductDiv_Sno,ResolverType,CG_User,BusinessLine,varYear,varmonth,Week);
                     
                 }
              else if(ResolverType==5)
                {
                    ddlCGContractEmp = document.getElementById('ctl00_MainConHolder_ddlCGContractEmp');       
                    if(ddlCGContractEmp!=null)
                    {
                      CG_Contract_Emp= ddlCGContractEmp.options[ddlCGContractEmp.selectedIndex].value;
                    }                    
                 funSummaryReportMTO_CGContractUserNew(Type,Region_Sno,Branch_Sno,ProductDiv_Sno,ResolverType,CG_Contract_Emp,BusinessLine,varYear,varmonth,Week);
                        
                } 
               else if(ResolverType==3)
               {
                    ddlSC = document.getElementById('ctl00_MainConHolder_ddlSC');  
                    if(ddlSC!=null)
                    {                
                      SC_No= ddlSC.options[ddlSC.selectedIndex].value;
                    }                     
                  funSummaryReportMTO_SCNew(Type,Region_Sno,Branch_Sno,ProductDiv_Sno,ResolverType,SC_No,BusinessLine,varYear,varmonth,Week);;
               }                
              
        }
        //End
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
                        MIS-7 New Weekly Analysis of Resolution Time(Based On SLADate)
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
                        Product Group(CP/IS):
                    </td>
                    <td>
                         <asp:DropDownList ID="DDlPgp" Width="175" runat="server" CssClass="simpletxt1" 
                             AutoPostBack="true" onselectedindexchanged="DDlPg_SelectedIndexChanged">
                           <asp:ListItem Text="Select" Value="0" Selected="True" ></asp:ListItem>
                           <asp:ListItem Text="CP" Value="1" ></asp:ListItem>
                           <asp:ListItem Text="IS" Value="2" ></asp:ListItem>
                         </asp:DropDownList>
                    </td>
                </tr>   
                <tr>
                    <td align="right">
                        Product Division:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlProductDivision" Width="175" runat="server" 
                            CssClass="simpletxt1" AutoPostBack="True" 
                            onselectedindexchanged="ddlProductDivision_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>              
                <tr id="trResolvertype" runat="server">
                    <td width="30%" align="right">
                        Resolver Type:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlResolver" AutoPostBack="true" runat="server" Width="175px"
                            CssClass="simpletxt1" ValidationGroup="editt" OnSelectedIndexChanged="ddlResolver_SelectedIndexChanged">
                           <asp:ListItem Selected="True" Text="All" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Service Contractor" Value="3"></asp:ListItem>
                            <asp:ListItem Text="CG Employee" Value="2"></asp:ListItem>
                            <asp:ListItem Text="CG Contract Employee" Value="5"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <div id="divSC" runat="server" visible="false">                   
                    <tr>
                        <td align="right">
                            Service Contractor:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSC" Width="175" runat="server" CssClass="simpletxt1">
                            </asp:DropDownList>
                        </td>
                    </tr>                   
                </div>
                <tr id="trCGExce" runat="server" visible="false">
                    <td width="30%" align="right">
                        CG Employee:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlCGExec" AutoPostBack="true" runat="server" Width="175" CssClass="simpletxt1"
                            ValidationGroup="editt">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="trCgContractEmp" runat="server" visible="false">
                    <td width="30%" align="right">
                        CG Contract Employee:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlCGContractEmp" AutoPostBack="true" runat="server" Width="175"
                            CssClass="simpletxt1" ValidationGroup="editt">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Date From:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFromYr" Width="52" runat="server" CssClass="simpletxt1">
                            <asp:ListItem Value="0">Year</asp:ListItem>
                        </asp:DropDownList>                      
                        <asp:DropDownList ID="ddlFromMth" Width="59" runat="server" CssClass="simpletxt1">
                            <asp:ListItem Value="0">Month</asp:ListItem>
                            <asp:ListItem Value="01">Jan</asp:ListItem>
                            <asp:ListItem Value="02">Feb</asp:ListItem>
                            <asp:ListItem Value="03">Mar</asp:ListItem>
                            <asp:ListItem Value="04">Apr</asp:ListItem>
                            <asp:ListItem Value="05">May</asp:ListItem>
                            <asp:ListItem Value="06">Jun</asp:ListItem>
                            <asp:ListItem Value="07">Jul</asp:ListItem>
                            <asp:ListItem Value="08">Aug</asp:ListItem>
                            <asp:ListItem Value="09">Sep</asp:ListItem>
                            <asp:ListItem Value="10">Oct</asp:ListItem>
                            <asp:ListItem Value="11">Nov</asp:ListItem>
                            <asp:ListItem Value="12">Dec</asp:ListItem>
                        </asp:DropDownList>                       
                        <asp:DropDownList ID="ddlFromWk" Width="57" runat="server" CssClass="simpletxt1">
                            <asp:ListItem Value="0">Week</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                        </asp:DropDownList>                      
                        To
                        <asp:DropDownList ID="ddlToYr" Width="52" runat="server" CssClass="simpletxt1">
                            <asp:ListItem Value="0">Year</asp:ListItem>
                        </asp:DropDownList>                       
                        <asp:DropDownList ID="ddlToMth" Width="59" runat="server" CssClass="simpletxt1">
                            <asp:ListItem Value="0">Month</asp:ListItem>
                            <asp:ListItem Value="01">Jan</asp:ListItem>
                            <asp:ListItem Value="02">Feb</asp:ListItem>
                            <asp:ListItem Value="03">Mar</asp:ListItem>
                            <asp:ListItem Value="04">Apr</asp:ListItem>
                            <asp:ListItem Value="05">May</asp:ListItem>
                            <asp:ListItem Value="06">Jun</asp:ListItem>
                            <asp:ListItem Value="07">Jul</asp:ListItem>
                            <asp:ListItem Value="08">Aug</asp:ListItem>
                            <asp:ListItem Value="09">Sep</asp:ListItem>
                            <asp:ListItem Value="10">Oct</asp:ListItem>
                            <asp:ListItem Value="11">Nov</asp:ListItem>
                            <asp:ListItem Value="12">Dec</asp:ListItem>
                        </asp:DropDownList>                       
                        <asp:DropDownList ID="ddlToWk" Width="57" runat="server" CssClass="simpletxt1">
                            <asp:ListItem Value="0">Week</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                        </asp:DropDownList>                       
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="Search" Width="100"
                            OnClick="btnSearch_Click" ValidationGroup="Date" />
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
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvMIS" CssClass="simpletxt1" runat="server" RowStyle-CssClass="bgcolorcomm"
                            Width="100%" AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                            GridGroups="both" AllowPaging="false" PagerStyle-HorizontalAlign="Center" AllowSorting="True"
                            AutoGenerateColumns="False" HorizontalAlign="Left" OnPageIndexChanging="gvMIS_PageIndexChanging"
                            OnSorting="gvMIS_Sorting" onrowdatabound="gvMIS_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                    <HeaderStyle Width="40px" />
                                </asp:BoundField>
                                  <asp:BoundField DataField="Region_desc" HeaderStyle-Width="40px" HeaderText="Region">
                                  </asp:BoundField>
                                  <asp:BoundField DataField="Branch_name" HeaderStyle-Width="40px" HeaderText="Branch">
                                  </asp:BoundField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="SLADate" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Month">
                                    <ItemTemplate>
                                        <%#Eval("SLADate")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Week" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Week">
                                    <ItemTemplate>
                                        <%#Eval("Week")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Total Received" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Total Received">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SUMMATOTREC','<%#Eval("Region_Sno")%>','<%#Eval("Branch_Sno")%>','<%#Eval("SLADate")%>','<%#Eval("Week")%>')">
                                            <%#Eval("Total Received")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Total Resolved" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Total Resolved">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SUMMATOTRSO','<%#Eval("Region_Sno")%>','<%#Eval("Branch_Sno")%>','<%#Eval("SLADate")%>','<%#Eval("Week")%>')">
                                            <%#Eval("Total Resolved")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <%-- <asp:TemplateField HeaderStyle-Width="100px" SortExpression="TempClosed" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Temporary Closed">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SUMMATOTEMPCLO','<%#Eval("Region_Sno")%>','<%#Eval("Branch_Sno")%>','<%#Eval("SLADate")%>','<%#Eval("Week")%>')">
                                            <%#Eval("TempClosed")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>     --%>                           
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="FIR Within 1 Day" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="FIR Within 1 Day">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SUMMAFIR1D','<%#Eval("Region_Sno")%>','<%#Eval("Branch_Sno")%>','<%#Eval("SLADate")%>','<%#Eval("Week")%>')">
                                            <%#Eval("FIR Within 1 Day")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Closure Within 1 Day"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Closure Within 1 Day">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SUMMACLO1D','<%#Eval("Region_Sno")%>','<%#Eval("Branch_Sno")%>','<%#Eval("SLADate")%>','<%#Eval("Week")%>')">
                                            <%#Eval("Closure Within 1 Day")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>                                                                
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="FIR Within 2 Day" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="FIR Within 2 Day">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SUMMAFIR2D','<%#Eval("Region_Sno")%>','<%#Eval("Branch_Sno")%>','<%#Eval("SLADate")%>','<%#Eval("Week")%>')">
                                            <%#Eval("FIR Within 2 Day")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Closure Within 2 Day"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Closure Within 2 Day">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SUMMACLO2D','<%#Eval("Region_Sno")%>','<%#Eval("Branch_Sno")%>','<%#Eval("SLADate")%>','<%#Eval("Week")%>')">
                                            <%#Eval("Closure Within 2 Day")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>                                
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="FIR Within 3 Day" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="FIR Within 3 Day">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SUMMAFIR3D','<%#Eval("Region_Sno")%>','<%#Eval("Branch_Sno")%>','<%#Eval("SLADate")%>','<%#Eval("Week")%>')">
                                            <%#Eval("FIR Within 3 Day")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Closure Within 3 Day"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Closure Within 3 Day">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SUMMACLO3D','<%#Eval("Region_Sno")%>','<%#Eval("Branch_Sno")%>','<%#Eval("SLADate")%>','<%#Eval("Week")%>')">
                                            <%#Eval("Closure Within 3 Day")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>                                                               
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="FIR Within 4&7 Days"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="FIR Within 4&7 Days">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SUMMAFIR4T7D','<%#Eval("Region_Sno")%>','<%#Eval("Branch_Sno")%>','<%#Eval("SLADate")%>','<%#Eval("Week")%>')">
                                            <%#Eval("FIR Within 4&7 Days")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Closure Within 4&7 Days"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Closure Within 4&7 Days">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SUMMACLO4T7D','<%#Eval("Region_Sno")%>','<%#Eval("Branch_Sno")%>','<%#Eval("SLADate")%>','<%#Eval("Week")%>')">
                                            <%#Eval("Closure Within 4&7 Days")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>                                
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="FIR more than 7 Days"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="FIR more than 7 Days">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SUMMAFIRMT7D','<%#Eval("Region_Sno")%>','<%#Eval("Branch_Sno")%>','<%#Eval("SLADate")%>','<%#Eval("Week")%>')">
                                            <%#Eval("FIR more than 7 Days")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Closure more than 7 Days"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Closure more than 7 Days">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SUMMACLOMT7D','<%#Eval("Region_Sno")%>','<%#Eval("Branch_Sno")%>','<%#Eval("SLADate")%>','<%#Eval("Week")%>')">
                                            <%#Eval("Closure more than 7 Days")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Pending FIR" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Pending FIR">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SUMMAFIRPEN','<%#Eval("Region_Sno")%>','<%#Eval("Branch_Sno")%>','<%#Eval("SLADate")%>','<%#Eval("Week")%>')">
                                            <%#Eval("Pending FIR")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Pending Closure" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Pending Closure">
                                    <ItemTemplate>
                                        <a href="Javascript:void(0);" onclick="javascript:return funMISComplaintLocal('SUMMACLOPEN','<%#Eval("Region_Sno")%>','<%#Eval("Branch_Sno")%>','<%#Eval("SLADate")%>','<%#Eval("Week")%>')">
                                            <%#Eval("Pending Closure")%></a>
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
                        <asp:GridView ID="gvExport" CssClass="simpletxt1" runat="server" 
                            AutoGenerateColumns="False" onrowdatabound="gvExport_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                    <HeaderStyle Width="40px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Region_desc" HeaderStyle-Width="40px" HeaderText="Region">
                                  </asp:BoundField>
                                <asp:BoundField DataField="Branch_name" HeaderStyle-Width="40px" HeaderText="Branch">
                                  </asp:BoundField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="SLADate" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Month">
                                    <ItemTemplate>
                                        <%#Eval("SLADate")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Week" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Week">
                                    <ItemTemplate>
                                        <%#Eval("Week")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Total Received" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Total Received">
                                    <ItemTemplate>
                                        <%#Eval("Total Received")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Total Resolved" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Total Resolved">
                                    <ItemTemplate>
                                        <%#Eval("Total Resolved")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <%-- <asp:TemplateField HeaderStyle-Width="100px" SortExpression="TempClosed" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Temporary Closed">
                                    <ItemTemplate>
                                        <%#Eval("TempClosed")%>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="FIR Within 1 Day" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="FIR Within 1 Day">
                                    <ItemTemplate>
                                        <%#Eval("FIR Within 1 Day")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Closure Within 1 Day"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Closure Within 1 Day">
                                    <ItemTemplate>
                                        <%#Eval("Closure Within 1 Day")%>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="FIR Within 2 Day" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="FIR Within 2 Day">
                                    <ItemTemplate>
                                        <%#Eval("FIR Within 2 Day")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Closure Within 2 Day"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Closure Within 2 Day">
                                    <ItemTemplate>
                                        <%#Eval("Closure Within 2 Day")%>
                                    </ItemTemplate>
                                </asp:TemplateField>         
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="FIR Within 3 Day" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="FIR Within 3 Day">
                                    <ItemTemplate>
                                        <%#Eval("FIR Within 3 Day")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Closure Within 3 Day"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Closure Within 3 Day">
                                    <ItemTemplate>
                                        <%#Eval("Closure Within 3 Day")%>
                                    </ItemTemplate>
                                </asp:TemplateField>                               
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="FIR Within 4&7 Days"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="FIR Within 4&7 Days">
                                    <ItemTemplate>
                                        <%#Eval("FIR Within 4&7 Days")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Closure Within 4&7 Days"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Closure Within 4&7 Days">
                                    <ItemTemplate>
                                        <%#Eval("Closure Within 4&7 Days")%>
                                    </ItemTemplate>
                                </asp:TemplateField>                               
                                
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="FIR more than 7 Days"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="FIR more than 7 Days">
                                    <ItemTemplate>
                                        <%#Eval("FIR more than 7 Days")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Closure more than 7 Days"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Closure more than 7 Days">
                                    <ItemTemplate>
                                        <%#Eval("Closure more than 7 Days")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Pending FIR" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Pending FIR">
                                    <ItemTemplate>
                                        <%#Eval("Pending FIR")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Pending Closure" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Pending Closure">
                                    <ItemTemplate>
                                        <%#Eval("Pending Closure")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
