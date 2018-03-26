<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" 
CodeFile="OutBoundCallingScoreASCWise.aspx.cs" Inherits="Reports_OutBoundCallingScoreASCWise" 
Title="OutBound Calling ASC" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="ComplainClosure" ContentPlaceHolderID="MainConHolder" runat="Server">

<script type="text/javascript" language="javascript">
    function funMISComplaintLocal(Type,intBranch_Sno,intProductDivision_Sno)
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
        var ProductDiv_Sno = intProductDivision_Sno;//hdnProductDiv.value;
        if(ProductDiv_Sno ==0)
            {
                 ProductDiv_Sno= ddlProductDivision.options[ddlProductDivision.selectedIndex].value;      
            }
        
        var Sc_Sno=0;
        funMISComplaint(Type,2,Region_Sno,Branch_Sno,ProductDiv_Sno,Sc_Sno);  
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
                        MIS OutBound Customer Satisfaction ASC Wise Score
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
                            
                            <%--<tr>
                        <td width="30%" align="right">
                            Business Line:
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlBusinessLine" AutoPostBack="True" runat="server" Width="175px"
                                CssClass="simpletxt1" ValidationGroup="editt" OnSelectedIndexChanged="ddlBusinessLine_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>--%>
                 <tr>
                    <td align="right">
                        Region:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlRegion" Width="175" AutoPostBack="true" runat="server" CssClass="simpletxt1"
                            OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Branch:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlBranch" Width="175" AutoPostBack="true" runat="server" 
                            CssClass="simpletxt1">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Product Division:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlProductDivision" Width="175" runat="server" CssClass="simpletxt1">
                        </asp:DropDownList>
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
                                    <asp:Button Width="70px" Text="Search" CssClass="btn" ValidationGroup="editt" 
                                        ID="btnSearch" runat="server" onclick="btnSearch_Click"  />
                                     <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn" 
                                        Text="Export To Execl" onclick="btnExport_Click"  />
                                         <asp:Button ID="btnViewChart" runat="server" Visible="false" CssClass="btn" 
                                        Text="View Chart" onclick="btnViewChart_Click"  />
                            
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
                                        PagerStyle-HorizontalAlign="Left" DataKeyNames ="SC_Sno" 
                                        AutoGenerateColumns="False" ID="gvComm"
                                        runat="server"  Width="100%" 
                                        HorizontalAlign="Left"  Visible="true" >
                                        <RowStyle CssClass="gridbgcolor" />
                            <Columns >
                                          <asp:BoundField DataField="SequenceNo"  HeaderText="SNo">
                                          
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SC_Sno"  Visible ="false"  HeaderText="Seq">
                                            
                                        </asp:BoundField>
                                         <asp:BoundField DataField="SC_Name"  HeaderText="ASC Name">
                                            
                                        </asp:BoundField>
                                      
                                        
                                         <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Customer satisfaction - (Q1)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuest1" runat="server" Text='<%#Eval("Question1") %>'>
                                        </asp:Label>%                                   
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                
                                <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Technician competancy - (Q2)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuest2" runat="server" Text='<%#Eval("Question2") %>'>
                                        </asp:Label>%                                   
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  
                                     <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Courteousness - (Q3)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuest3" runat="server" Text='<%#Eval("Question3") %>'>
                                        </asp:Label>%                                   
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                  <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Buy again from CG - (Q4)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuest4" runat="server" Text='<%#Eval("Question4") %>'>
                                        </asp:Label>%                                   
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                
                                
                                  <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Recommend CG product - (Q5)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuest5" runat="server" Text='<%#Eval("Question5") %>'>
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
                                          <asp:BoundField DataField="SequenceNo"  HeaderText="SNo">
                                          
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SC_Sno"  Visible ="false"  HeaderText="Seq">
                                            
                                        </asp:BoundField>
                                         <asp:BoundField DataField="SC_Name"  HeaderText="ASC Name">
                                            
                                        </asp:BoundField>
                                            <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Customer satisfaction - (Q1)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuest1" runat="server" Text='<%#Eval("Question1") %>'>
                                        </asp:Label>%                                   
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                
                                <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Technician competancy - (Q2)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuest2" runat="server" Text='<%#Eval("Question2") %>'>
                                        </asp:Label>%                                   
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  
                                     <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Courteousness - (Q3)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuest3" runat="server" Text='<%#Eval("Question3") %>'>
                                        </asp:Label>%                                   
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                  <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Buy again from CG - (Q4)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuest4" runat="server" Text='<%#Eval("Question4") %>'>
                                        </asp:Label>%                                   
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                
                                
                                  <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Recommend CG product - (Q5)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuest5" runat="server" Text='<%#Eval("Question5") %>'>
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

