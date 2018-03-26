<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_dashBoardl.ascx.cs" Inherits="UC_UC_dashBoardl" %>

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
                           <asp:BoundField DataField="name" ItemStyle-Width="350px" ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-HorizontalAlign="Center" HeaderText="Names \ Period->">
                           </asp:BoundField>
                           <asp:BoundField DataField="Pending0D" ItemStyle-Width="150px" ItemStyle-CssClass="tdPad"  
                            HeaderStyle-HorizontalAlign="Center" HeaderText="0">
                            </asp:BoundField>
                             <asp:BoundField DataField="Pending1D" ItemStyle-Width="150px"    ItemStyle-CssClass="tdPad"
                            HeaderStyle-HorizontalAlign="Center" HeaderText="1">
                                 </asp:BoundField>
                             <asp:BoundField DataField="Pending2D"  ItemStyle-Width="150px" ItemStyle-CssClass="tdPad"
                            HeaderStyle-HorizontalAlign="Center" HeaderText="2">
                            </asp:BoundField>
                       
                             <asp:BoundField DataField="Pending3D"  ItemStyle-Width="150px"   ItemStyle-CssClass="tdPad"
                            HeaderStyle-HorizontalAlign="Center" HeaderText="3">
                         </asp:BoundField>
                             <asp:BoundField DataField="Pending4D" ItemStyle-Width="150px"  ItemStyle-CssClass="tdPad"
                            HeaderStyle-HorizontalAlign="Center" HeaderText="4">
                              </asp:BoundField>
                           <asp:BoundField DataField="PendingGT5D"  ItemStyle-Width="150px"   ItemStyle-CssClass="tdPad"
                            HeaderStyle-HorizontalAlign="Center" HeaderText="5 and more">
                             </asp:BoundField>
                             <asp:BoundField DataField="TotalPending" ItemStyle-Width="150px"  ItemStyle-CssClass="tdPad"
                            HeaderStyle-HorizontalAlign="Center" HeaderText="Total">
                             </asp:BoundField>
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
