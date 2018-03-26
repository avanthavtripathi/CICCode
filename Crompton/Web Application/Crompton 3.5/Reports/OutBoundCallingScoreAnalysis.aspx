<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="OutBoundCallingScoreAnalysis.aspx.cs" Inherits="Reports_OutBoundCallingScore" Title="OutBound Calling Analysis" %>
<asp:Content ID="ComplainClosure" ContentPlaceHolderID="MainConHolder" runat="Server">
<asp:UpdatePanel ID="updatepnl" runat="server">
    <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
     </Triggers>
    <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        OutBound Satisfaction Region-Month Wise Summary
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
                        <table width="98%" border="0">
                           <tr>
                                <td colspan ="2" align="center">
                                    <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </td>
                            </tr>
                 <tr>
                    <td align="right">
                        Year:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="Ddlyear" runat="server" Width="200px" CssClass="simpletxt1" ValidationGroup="editt" >
                                        <asp:ListItem>2013</asp:ListItem>
                                        <asp:ListItem>2014</asp:ListItem>
                                        <asp:ListItem>2015</asp:ListItem>
                                        <asp:ListItem>2016</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Region:
                    </td>
                    <td align="left">
                       <asp:DropDownList ID="ddlRegion" runat="server" Width="200px" CssClass="simpletxt1" ValidationGroup="editt">
                       </asp:DropDownList>
                                     
                    </td>
                </tr>
                <tr>
                                <td align="right"></td>
                                <td align="left">                                   
                                    <asp:Button Width="70px" Text="Search" CssClass="btn" ValidationGroup="editt" 
                                        ID="btnSearch" runat="server" onclick="btnSearch_Click"  />
                                     <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn" 
                                        Text="Export To Execl" onclick="btnExport_Click"  />
                            
                                </td>
                            </tr>
                            <tr>
                             <td align="left" class="MsgTDCount">
                                            Total Number of Records : <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server" Text="0"></asp:Label>
                             </td>
                             <td></td>
                            </tr>
                           
                        </table>
                        <br />
                        <table width="100%" border="0">
                            <tr>
                                <td>
                                   <asp:GridView  RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AutoGenerateColumns="false"  
                                        PagerStyle-HorizontalAlign="Left" ID="gvComm" runat="server"  Width="100%" 
                                        HorizontalAlign="Left"  Visible="true" >
                                        <RowStyle CssClass="gridbgcolor" />
                            <Columns >
                             <asp:BoundField DataField="Region_Desc"  HeaderText="Region"  />
                             <asp:BoundField DataField="MonthName"  HeaderText="Month"  />
                             <asp:BoundField DataField="Q1"  DataFormatString="{0:0.00}" HeaderText="Customer satisfaction - (Q1)" />
                             <asp:BoundField DataField="Q2"  DataFormatString="{0:0.00}" HeaderText="Technician competancy - (Q2)" />
                             <asp:BoundField DataField="Q3"  DataFormatString="{0:0.00}" HeaderText="Courteousness - (Q3)" />
                             <asp:BoundField DataField="Q4"  DataFormatString="{0:0.00}" HeaderText="Buy again from CG - (Q4)" />
                             <asp:BoundField DataField="Q5"  DataFormatString="{0:0.00}" HeaderText="Recommend CG product - (Q5)" />
                           </Columns>
                            <PagerStyle HorizontalAlign="Left" />
                            <HeaderStyle CssClass="fieldNamewithbgcolor" />
                            <AlternatingRowStyle CssClass="fieldName" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            
                            <tr>
                    <td>
                        &nbsp;</td>
                </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                             <tr>
                                <td align="left" style="margin:0">
                                 <pre class="mainarea">
      Note : 
      1. Satisfaction Score is taken between 0 and 1 for all questions.
      2. Calculation : ((SatisfactionScore * 20)/ComplaintCount)*5 [20 : Weightage]
      3. For Customer satisfaction :
          If Rating between 1 to 5 in (1,2) then SatisfactionScore : 0 
                                                 (3,4) then SatisfactionScore : 0.5
                                                    (5) then SatisfactionScore : 1
                                </pre>
                                </td>
                            </tr>
                       </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

