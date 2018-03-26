<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="CustomerFeedbackSummary.aspx.cs" Inherits="Reports_CustomerFeedbackSummary" Title="Customer Feedback Summary" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
<link href="../css/AjaxStyles.css" rel="stylesheet" type="text/css" />
  <table width="100%">
                <tr>
                    <td class="headingred">
                       Customer Feedback Summary
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" colspan="2">
                       <table width="100%" border="0" style="text-align:left">
                       <tr>
                       <td align="right">
                       Date Range
                       </td>
                       <td>
                        <asp:DropDownList ID="DDlMonthFrom" runat="server" CssClass="simpletxt1" Width="90px" />
                        To
                        <asp:DropDownList ID="DDlMonthTo" runat="server" CssClass="simpletxt1" Width="90px" />
                        
                        <asp:DropDownList ID="DDlYearTo" runat="server" Width="90px" CssClass="simpletxt1" >
                                        <asp:ListItem>2013</asp:ListItem>
                                        <asp:ListItem>2014</asp:ListItem>
                                        <asp:ListItem>2015</asp:ListItem>
                                        <asp:ListItem>2016</asp:ListItem>
                        </asp:DropDownList>
                       </td>
                       </tr>
                           <tr>
                       <td>
                       
                       </td>
                       <td>
                          <asp:Button Width="70px" Text="Search" CssClass="btn" ID="btnSearch" runat="server" OnClick="btnSearch_Click" />
                       </td>
                       </tr>
                            <tr style="text-align:left">
                                <td colspan="2">
                    <cc1:Accordion ID="Accord" runat="Server" SelectedIndex="0" RequireOpenedPane="true" HeaderCssClass="accordionHeader" ContentCssClass="accordionContent" > 
                    <Panes>
                    <cc1:AccordionPane id="ap1" runat="server">
                    <Header>
                    <ul>
                    <li>
                  <span>Summary</span>
                  <span style="text-align:right;padding-right:100px;font-size:11px" >
                  <asp:LinkButton ID="lbtndownload1" runat="server" CausesValidation="false" OnClick="Download_Click">Download </asp:LinkButton>
                  </span>  
                    </li>
                    </ul> 
                    </Header>
                    <Content>
                    <table width="80%">
                    <tr>
                    <td>
                    <asp:GridView ID="gvRpt1" runat="server" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                    Width="750px" HeaderStyle-CssClass="fieldNamewithbgcolor" GridLines="Both" AutoGenerateColumns="false" 
                                        HorizontalAlign="Left" >
                     <Columns>
                     <asp:BoundField HeaderText="Division" DataField="Division" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                     <asp:BoundField HeaderText="Breakdown/Complaint" DataField="Breakdown/Complaint" HeaderStyle-Wrap="true" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                     <asp:BoundField HeaderText="Feedback" DataField="Feedback" HeaderStyle-Wrap="true" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                     <asp:BoundField HeaderText="Query" DataField="Query" HeaderStyle-Wrap="true" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                     <asp:BoundField HeaderText="Installation/Demonstration" DataField="Installation/Demonstration" HeaderStyle-Wrap="true" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                     </Columns>                   
                                       </asp:GridView>
                    </td>
                    </tr>
                       <tr>
                    <td>
                               <asp:GridView RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridLines="Both"  ID="gvRpt2" runat="server" 
                                        Width="80%" HorizontalAlign="Left" AutoGenerateColumns="false" >
                               <Columns>
                                    <asp:BoundField HeaderText="Total Hit" DataField="TotalHit" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Customer" DataField="Customer" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Customer Hit %" DataField="Customer Hit %" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="ASC" DataField="ASC" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="ASC Hit %" DataField="ASC Hit %" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                               </Columns>         
                              </asp:GridView>
                    </td>
                    </tr>
                    </table>
                                       
                                   
                           
                             
                             
                    </Content>
                    </cc1:AccordionPane>
                     <cc1:AccordionPane id="ap2" runat="server">
                               <Header>
                   <ul>
                    <li>
                     <span>Web Form Usage</span>
                  <span style="text-align:right;padding-right:100px;font-size:11px" >
                          <asp:LinkButton ID="lbtndownload2" runat="server" CausesValidation="false" OnClick="Download_Click">Download </asp:LinkButton>
                  </span>  
                    </li>
                    </ul> 
                    </Header>
                    <Content>
                                 <asp:GridView RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName" AutoGenerateColumns="false"  
                                        HeaderStyle-CssClass="fieldNamewithbgcolor"  ID="gvRpt3" runat="server" GridLines="Both"  
                                        Width="50%" HorizontalAlign="Left" onrowdatabound="gvRpt3_RowDataBound" >
                                  <Columns>
                                    <asp:BoundField HeaderText="ProductDivision" DataField="ProductDivision" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="ASC/Customer" DataField="ASC/Customer" HeaderStyle-Width="150" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Total" DataField="Total" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                  </Columns>
                                    </asp:GridView>
                    </Content>
                    </cc1:AccordionPane>
                    </Panes>
                             
                    </cc1:Accordion>
                    
                                 
                                </td>
                            </tr>
                    
                        </table>
                    </td>
                </tr>
            </table>
</asp:Content>

