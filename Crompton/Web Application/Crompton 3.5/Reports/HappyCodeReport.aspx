<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="HappyCodeReport.aspx.cs" Inherits="Reports_HappyCodeReport" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
 <script type="text/javascript">
    function Validate() {
        var ddlYear = document.getElementById('<%= ddlYear.ClientID %>');
        var ddlMonth = document.getElementById('<%= ddlMonth.ClientID %>');
       
       
       
        if (ddlYear.value == "0") {
            alert("Please select Year!");
            return false;
        }
        if (ddlMonth.value == "0") {
            alert("Please select Month!");
            return false;
        }
        return true;
    }
</script>
    <table width="100%" border="0">
        <tr>
            <td class="headingred">
               Happy Code Check Report</td>
            <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                </asp:UpdateProgress>
            </td>
        </tr>
        <tr>
            <td class="bgcolorcomm" colspan="2">
                <asp:UpdatePanel ID="pnl" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                    <center>
                        <table width="50%" border="0" cellpadding="3" cellspacing="0">
                            <tr>
                                <td>Year <span style="color:Red;">*</span></td>
                                <td>
                                    <asp:DropDownList ID="ddlYear" runat="server" Width="124px" 
                                        CssClass="simpletxt1" Height="16px">
                                        <asp:ListItem Value="2017">2017</asp:ListItem>
                                        <asp:ListItem Value="2018">2018</asp:ListItem>
                                      
                                    </asp:DropDownList>
                                    
                                </td>
                                <td>Month <span style="color:Red;">*</span></td>
                                <td>
                                    <asp:DropDownList ID="ddlMonth" runat="server" Width="111px" 
                                        CssClass="simpletxt1" Height="16px">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="1">January</asp:ListItem>
                                    <asp:ListItem Value="2">February</asp:ListItem>
                                    <asp:ListItem Value="3">March</asp:ListItem>
                                    <asp:ListItem Value="4">April</asp:ListItem>
                                    <asp:ListItem Value="5">May</asp:ListItem>
                                    <asp:ListItem Value="6">June</asp:ListItem>
                                    <asp:ListItem Value="7">July</asp:ListItem>
                                    <asp:ListItem Value="8">August</asp:ListItem>
                                    <asp:ListItem Value="9">September</asp:ListItem>
                                    <asp:ListItem Value="10">October</asp:ListItem>
                                    <asp:ListItem Value="11">November</asp:ListItem>
                                    <asp:ListItem Value="12">December</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            
                             
                               
                          
                            </tr>
                            
                        <tr>
                        
                          <td>Complaint No-</td>  
                               <td>
                               <asp:TextBox ID="txtcomplaint" runat="server" EnableTheming="False" MaxLength="10" 
                                       Width="126px"></asp:TextBox>
                               </td>
                        </tr>
                            <tr>
                            <td colspan="4" align="center" style="padding-bottom:10px;">
                                <asp:Button ID="BtnSEARCH" runat="server" Text="SEARCH" OnClientClick="return Validate()" CssClass="btn" Width="100px"
                                    onclick="BtnSEARCH_Click" />
                               
                                    </td></tr>
                            </table>
                      </center>
                          
                          
                          
                        
                           
                      
                              
                        
                        
                           
                              <div style="float:right;margin-top: 2%;">  <asp:Button ID="btnExport" Visible="false" runat="server" Text="EXPORT TO EXCEL" CssClass="btn" Width="120px" onclick="btnExport_Click" /> </div>
                        
                          <table width="100%" border="0" cellpadding="1" cellspacing="0" style="margin-top: 4%;">
                             <tr><td><asp:Label ID="lblheader" runat="server" CssClass="headingred" ></asp:Label></td>  
                             
                             
                       
                             </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvSummary" Width="100%" runat="server" 
                                        AutoGenerateColumns="true" style="text-align:center;">
                                        <RowStyle CssClass="gridbgcolor" />
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
                                         <HeaderStyle CssClass="fieldNamewithbgcolor" HorizontalAlign="Center" />
                                        
                                        
                                        
        
                                        
                                        
                                        
                                        <AlternatingRowStyle CssClass="fieldName" />
                                    </asp:GridView>
                                </td>
                            </tr>
                           
                          
                           
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblsummry" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                        
                    </ContentTemplate>
                    
                    
                    
                    
                    
                    
                    
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnExport" />
                    </Triggers>
                    
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>

