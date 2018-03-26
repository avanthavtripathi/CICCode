<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" 
CodeFile="OutBoundCallRegionWise.aspx.cs" Inherits="Reports_OutBoundCallRegionWise" Title="Outbound calling for region" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="ComplainClosure" ContentPlaceHolderID="MainConHolder" runat="Server">
<script language="javascript" type="text/javascript">
         function funUserDetail(custNo, compNo)
        {
            var strUrl='CustomerDetail.aspx?custNo='+ custNo + '&CompNo='+ compNo;
            window.open(strUrl,'History','height=550,width=750,left=20,top=30,Location=0');
        }
         function funReqDetail(compNo)
        {
            var strUrl='ComplaintDetailPopUp.aspx?compNo='+ compNo;
            window.open(strUrl,'History','height=550,width=750,left=20,top=30,Location=0');
        }
         function funSCDetail(SCNo)
        {
            var strUrl='../Pages/SCPopup.aspx?scno='+ SCNo + '&type=display';
            window.open(strUrl,'History','height=550,width=750,left=20,top=30,Location=0');
        }
    </script>
    <asp:UpdatePanel ID="updatepnl" runat="server">
    <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                      OutBound Customer Satisfaction Region Wise Score
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
                                <td width="30%" align="right">
                                    Product Divison
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlProductDivison" runat="server" Width="175px" CssClass="simpletxt1"
                                        ValidationGroup="editt" >                                    
                                    </asp:DropDownList><asp:RequiredFieldValidator ID="reqProductDivision" runat ="server" ControlToValidate ="ddlProductDivison" InitialValue ="Select" 
                                                ValidationGroup="editt" ErrorMessage ="Select Product Division" ></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Date From
                                </td>
                                <td align="left">
                                    <asp:TextBox runat="server" ID="txtFromDate" CssClass="txtboxtxt" ValidationGroup="editt"
                                        />
                                        <asp:RequiredFieldValidator ID="reqfromdate" runat ="server" ControlToValidate="txtFromDate"
                                         ErrorMessage ="Enter from date" ValidationGroup ="editt" ></asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CEFrom" runat="server" TargetControlID="txtFromDate">
                                    </cc1:CalendarExtender>
                                    To
                                    <asp:TextBox runat="server" ID="txtToDate" CssClass="txtboxtxt" ValidationGroup="editt"
                                         /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat ="server" ControlToValidate="txtToDate"
                                         ErrorMessage ="Enter to date" ValidationGroup ="editt" ></asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CETo" runat="server" TargetControlID="txtToDate">
                                    </cc1:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right"></td>
                                <td align="left">                                   
                                    <asp:Button Width="70px" Text="Search" CssClass="btn" ValidationGroup="editt" ID="btnSearch" runat="server" OnClick="btnSearch_Click" />
                                     <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn" 
                                        Text="Export To Execl" onclick="btnExport_Click" />
                                         <asp:Button ID="btnViewChart" runat="server" Visible="false" CssClass="btn" 
                                        Text="View Chart" onclick="btnViewChart_Click" />
                            
                                </td>
                            </tr>
                            <tr>
                             <td align="left" class="MsgTDCount">
                                            Total Number of Records : <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                             </td>
                             <td></td>
                            </tr>
                           
                        </table>
                        <br />
                        <table width="100%" border="0">
                            <tr>
                                <td>
                                    <!-- Action Listing -->
                                    <asp:GridView  RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" 
                                        PagerStyle-HorizontalAlign="Left" DataKeyNames ="Questionid" 
                                        AutoGenerateColumns="False" ID="gvComm"
                                        runat="server"  Width="100%" 
                                        HorizontalAlign="Left"  Visible="true" >
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns >
                                          <asp:BoundField DataField="Questionid"  HeaderText="SNo">
                                          
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Question"  HeaderText="Question">
                                            
                                        </asp:BoundField>
                                        
                                        <asp:BoundField DataField="Question_Desc"  HeaderText="Question Desc">
                                            
                                        </asp:BoundField>
                                       
                                        
                                  <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="North">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNorth" runat="server" Text='<%#Eval("North") %>'>
                                        </asp:Label>%                                   
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                     <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="East">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEast" runat="server" Text='<%#Eval("East") %>'>
                                        </asp:Label>%                                   
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    
                                      <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="West">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEast" runat="server" Text='<%#Eval("West") %>'>
                                        </asp:Label>%                                   
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                  <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="South">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEast" runat="server" Text='<%#Eval("South") %>'>
                                        </asp:Label>%                                   
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                                                        
                                        
                                        </Columns>
                                        
                                        <PagerStyle HorizontalAlign="Left" />
                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                        <AlternatingRowStyle CssClass="fieldName" />
                                        
                                    </asp:GridView>
                                    <!-- End Action Listing -->
                                </td>
                            </tr>
                            
                            <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvExport" Width ="100%" CssClass="simpletxt1" runat="server" AutoGenerateColumns="False">
                            <Columns >
                                          <asp:BoundField DataField="Questionid"  HeaderText="SNo">
                                          
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Question"  HeaderText="Question">
                                            
                                        </asp:BoundField>
                                        
                                              <asp:BoundField DataField="Question_Desc"  HeaderText="Question Desc">
                                            
                                        </asp:BoundField>
                                        
                                         <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="North">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNorth" runat="server" Text='<%#Eval("North") %>'>
                                        </asp:Label>%                                   
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                     <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="East">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEast" runat="server" Text='<%#Eval("East") %>'>
                                        </asp:Label>%                                   
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    
                                      <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="West">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEast" runat="server" Text='<%#Eval("West") %>'>
                                        </asp:Label>%                                   
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                  <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="South">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEast" runat="server" Text='<%#Eval("South") %>'>
                                        </asp:Label>%                                   
                                    </ItemTemplate>
                                </asp:TemplateField>                                   
                                        
                                        </Columns>
                        </asp:GridView>
                    </td>
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
                            
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
