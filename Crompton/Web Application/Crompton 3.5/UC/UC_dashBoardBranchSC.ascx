<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_dashBoardBranchSC.ascx.cs" Inherits="UC_dashBoardBranchSC" %>

 <table border="0" cellpadding="0" cellspacing="0" style="background-color:White">

                <tr>
                    <td class="headingred">
                       Dash Board 
                    </td>
                    <td align="right">
                      
                    </td>
                </tr>
                <tr>
                <td></td>
                <td></td>
                </tr>
         
    
                <tr>
                    <td align="center" colspan="2" style="width:100%">
                    <div style="width: 920px;" class="fieldNamewithbgcolor">
                    <table style="width:98%;">
                    <tr>
                    <td style="width:350px;text-align:center">Names \ Period-></td>
                    <td style="width:150px;text-align:center">0</td>
                    <td style="width:150px;text-align:center">1</td>
                    <td style="width:150px;text-align:center">2</td>
                    <td style="width:150px;text-align:center">3</td>
                    <td style="width:150px;text-align:center">4</td>
                    <td style="width:150px;text-align:center">>=5</td>
                    <td style="width:150px;text-align:center">Total</td>
                    </tr>
                    </table>
                    </div>
                    
                    <div id="dvdash" runat="server" style="position:relative;height:450px;width: 920px;overflow:auto;" >
                        <asp:GridView ID="gvFresh" runat="server" AutoGenerateColumns="false" GridGroups="both" Width="98%"  Height="100%" ShowHeader="false"
                        RowStyle-CssClass="gridbgcolor" >
                            <RowStyle CssClass="gridbgcolor" />
                             <HeaderStyle CssClass="fieldNamewithbgcolor" />
                            <AlternatingRowStyle CssClass="fieldName" />
                            <PagerStyle HorizontalAlign="Center" />
                            <Columns>
                            <asp:TemplateField>
                            <ItemTemplate>
                           <b> <%# (Eval("Name").ToString() == string.Empty) ? Eval("Branch_Name").ToString() : string.Empty %> </b> 
                            <table style="widows:1077px;"  >
                   
                            <tr>
                            <td style="width:310px;word-wrap:break-word;word-break:break-all;">
                               <%# Eval("Name")%>  
                            </td>
                            <td style="width:110px;">
                            <%# Eval("Pending0D")%>
                            </td>
                            <td style="width:110px;">
                            <%# Eval("Pending1D")%>
                            </td>
                            <td style="width:110px;">
                            <%# Eval("Pending2D")%>
                            </td>
                            <td style="width:110px;">
                            <%# Eval("Pending3D")%>
                            </td>
                            <td style="width:110px;">
                            <%# Eval("Pending4D")%>
                            </td>
                            <td style="width:110px;">
                            <%# Eval("PendingGT5D")%>
                            </td>
                            <td style="width:110px;">
                            <%# Eval("TotalPending")%>
                            </td>
                          
                            
                            
                            </tr>
                            </table>
                            </ItemTemplate>
                            </asp:TemplateField>
                           
                         
                             </Columns>
                            <EmptyDataTemplate>
                                <table width="50%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                            <img src="<%=ConfigurationManager.AppSettings["UserMessage"]%>" alt="" />
                                            <b>No Record found OR You are not a SC.</b>
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                    </td>
                </tr>
            </table>
