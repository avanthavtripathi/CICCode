<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="SMSUpdateForSC.aspx.cs" Inherits="Reports_SMSUpdateForSC" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
<script language="javascript" type="text/javascript">
function funUserDetail(custNo, compNo)
    {
    
        var strUrl='CustomerDetail.aspx?custNo='+ custNo + '&CompNo='+ compNo;
        window.open(strUrl,'History','height=550,width=750,left=20,top=30,Location=0');
    }
function fnOpenSC(compNo)
    {
        var strUrl='ServiceContractorTr.aspx?CrefNo='+ compNo;
        window.open(strUrl,'SCPage','height=600,width=900,left=20,top=30,scrollbars=1,resizable=yes');
    }        
        
        
</script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>
            <table width="100%" border="0">
                <tr>
                    <td class="headingred" style="width: 40%">
                        SMS Update Report for SC
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
              <%--  <tr>
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
                        Service Contractor:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSC" Width="175" runat="server" CssClass="simpletxt1">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="Search" Width="100"
                            OnClick="btnSearch_Click" />
                        <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn" Text="Export To Excel"
                            Width="100" OnClick="btnExport_Click" />
                    </td>
                </tr>--%>
                
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
                            GridGroups="both" PagerStyle-HorizontalAlign="Center" 
                            AutoGenerateColumns="False" HorizontalAlign="Left" 
                            onrowdatabound="gvMIS_RowDataBound" >
                            <RowStyle CssClass="bgcolorcomm" />
                            <Columns>
                        <%--        <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                    <HeaderStyle Width="40px" />
                                </asp:BoundField>--%>
                                
                                
                                    <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="ComplaintRefNo">
                                    <ItemTemplate>
                                     <a href="Javascript:void(0);" onclick="funCommonPopUp(<%#Eval("BaseLineId")%>)">
                                                        <%#Eval("Complaint_RefNo")%></a>
                                     <asp:HiddenField ID="Hdnbaselineid" runat="server"  Value='<%#Eval("BaseLineId")%>' />  
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                     <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Product Division">
                                    <ItemTemplate>
                                        <%#Eval("ProductDivision_Desc")%></ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                         <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Service Contractor">
                                    <ItemTemplate>
                                        <%#Eval("SC_Name")%></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Customer Name">
                                    <ItemTemplate>
                                       <a href="Javascript:void(0);" onclick="funUserDetail('<%#Eval("CustomerId")%>','<%#Eval("Complaint_RefNo")%>')">
                                           <%#Eval("FirstName")%>  <%#Eval("LastName")%>
                                       </a>
                                    </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                       <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Complaint Nature">
                                    <ItemTemplate>
                                        <%#Eval("NatureOfComplaint")%></ItemTemplate>
                                           <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                           <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" >
                                    <ItemTemplate>
                                <asp:LinkButton ID="lblsms" runat="server">SMS Communication</asp:LinkButton>
                                   <div id="divGV" runat="server" style="position:absolute;margin-left:-500px;display:none;background-color:#d7e5f7;border:1px solid #396870;"  onmouseover="this.style.display='block'" onmouseout="this.style.display='none'">
                                  
                                   <asp:GridView ID="Gv" CssClass="simpletxt1" runat="server" Width="500px" GridLines="Both"  
                            AutoGenerateColumns="False" HorizontalAlign="Left" style="margin:3 3 3 3"   >
                           
                            <Columns>
                         <asp:TemplateField HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="CCE Remark">
                                    <ItemTemplate>
                                        <%#Eval("CCERemark")%></ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Left"  />
                                         <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                         <asp:TemplateField HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Last Status Updated">
                                    <ItemTemplate>
                                   <%#Eval("StageDesc")%>
                                     </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                         <asp:TemplateField HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="FalseUpdate">
                                    <ItemTemplate>
                                        <%#Eval("FalseUpdateBySE")%></ItemTemplate>
                                     <HeaderStyle HorizontalAlign="Left" />
                                     <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                         <asp:TemplateField HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Appointment Date">
                                    <ItemTemplate>
                                        <%#Eval("AppointmentDate")%></ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left"  />
                                        <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                            </Columns>
                          </asp:GridView>
                                   </div>          
                                    
                  
                                        </ItemTemplate>
                                           <HeaderStyle HorizontalAlign="Left" />
                                           <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" >
                                    <ItemTemplate>
                                   <a href="Javascript:void(0);" onclick="fnOpenSC('<%# Eval("Complaint_RefNo")%>')">
                                          Take Action
                                   </a>    
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left"  />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                
                                      
                            </Columns>
                            <PagerStyle HorizontalAlign="Center" />
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
                            <AlternatingRowStyle CssClass="fieldName" />
                        </asp:GridView>
                        <br />
                        
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
