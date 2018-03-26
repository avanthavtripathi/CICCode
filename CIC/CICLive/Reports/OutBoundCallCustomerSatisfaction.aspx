<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" 
CodeFile="OutBoundCallCustomerSatisfaction.aspx.cs" Inherits="Reports_OutBoundCallCustomerSatisfaction" 
Title="Customer Dissatisfaction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <ContentTemplate>
            <table width="100%" border="0">
                <tr>
                    <td class="headingred" style="width: 40%">
                      OutBound Customer Satisfaction Survey Details
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                        </asp:UpdateProgress>
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
                <%--Added By Binay on 18 Nov for MTO--%>
                <tr>
                    <td align="right">
                        Product Division:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlProductDivision" Width="175" runat="server" CssClass="simpletxt1">
                        </asp:DropDownList>
                    </td>
                </tr>
               
            
                    <%--END-- Added By Binay on 16 Nov for MTO--%>
                    <tr>
                        <td align="right">
                            Service Contractor:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSC" Width="175" runat="server" CssClass="simpletxt1">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <%--Added By Binay on 15 Nov for MTO--%>
               
                
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
                    <td align="center" colspan="2">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="Search" Width="100"
                            OnClick="btnSearch_Click" />
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
                            GridGroups="both" PagerStyle-HorizontalAlign="Center" AllowSorting="True" AutoGenerateColumns="False"
                            HorizontalAlign="Left" OnPageIndexChanging="gvMIS_PageIndexChanging" OnSorting="gvMIS_Sorting"
                           >
                            <RowStyle CssClass="bgcolorcomm" />
                            <Columns>
                                <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                    <HeaderStyle Width="40px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Complaint_refno" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Complaint no.">
                                    <ItemTemplate>
                                       <a href="Javascript:void(0);" onclick="funCommonPopUp(<%#Eval("BaseLineId")%>)">
                                                        <%--<%#Eval("Complaint_RefNo")%>/<%#Eval("splitComplaint_RefNo")%></a>--%>
                                                        <%#Eval("Complaint_refno")%></a>
                                       <%-- <asp:Label ID="lblComplaintrefno" runat="server" Text='<%#Eval("Complaint_refno") %>'>
                                        </asp:Label>--%>                                     
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Region_desc" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Region">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRegion_desc" runat="server" Text='<%#Eval("Region_desc") %>'>
                                        </asp:Label>                                     
                                    </ItemTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Branch_Name" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Branch">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBranch_Name" runat="server" Text='<%#Eval("Branch_Name") %>'>
                                        </asp:Label>                                     
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                   <asp:TemplateField HeaderStyle-Width="100px" SortExpression="ProductDivision_desc" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Product Division">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductDivision_desc" runat="server" Text='<%#Eval("ProductDivision_desc") %>'>
                                        </asp:Label>                                     
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                  <asp:TemplateField HeaderStyle-Width="100px" SortExpression="CustomerName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Customer Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomerName" runat="server" Text='<%#Eval("CustomerName") %>'>
                                        </asp:Label>                                     
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderStyle-Width="100px" SortExpression="SC_Name" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="ASC Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSC_Name" runat="server" Text='<%#Eval("SC_Name") %>'>
                                        </asp:Label>                                     
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Survey Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSurveyDate" runat="server" Text='<%#Eval("CreatedOn") %>'>
                                        </asp:Label>                                     
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                  <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Question">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuestion" runat="server"   Text='<%#Eval("Question") %>'>
                                        </asp:Label>                                     
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                  <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Answer">
                                    <ItemTemplate>
                                        <asp:Label ID="lblScaleAnswer_Desc" runat="server" Text='<%#Eval("ScaleAnswer_Desc") %>'>
                                        </asp:Label>                                     
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                  <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Satisfaction score">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSatisfaction_score" runat="server" Text='<%#Eval("Satisfaction_score") %>'>
                                        </asp:Label>                                     
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                   <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Ans weight">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAns_weight" runat="server" Text='<%#Eval("Ans_weight") %>'>
                                        </asp:Label>                                     
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                
                                   <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Total Score">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalScore" runat="server" Text='<%#Eval("TotalScore") %>'>
                                        </asp:Label>                                     
                                    </ItemTemplate>
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
                            <HeaderStyle CssClass="fieldNamewithbgcolor" />
                            <AlternatingRowStyle CssClass="fieldName" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvExport" CssClass="simpletxt1" runat="server" AutoGenerateColumns="False"
                            >
                            <Columns>
                                                            
                             
                                        <asp:BoundField DataField="Complaint_refno"  HeaderText="Complaint refno">
                                            
                                        </asp:BoundField>
                                         <asp:BoundField DataField="Region_desc"  HeaderText="Region">
                                            
                                        </asp:BoundField>
                                         <asp:BoundField DataField="Branch_Name"  HeaderText="Branch">
                                         
                                        </asp:BoundField>
                                         <asp:BoundField DataField="ProductDivision_desc" HeaderText="Product Division">
                                           
                                        </asp:BoundField>
                                           <asp:BoundField DataField="CustomerName"  HeaderText="Customer Name">
                                           
                                        </asp:BoundField>
                                           <asp:BoundField DataField="SC_Name"  HeaderText="ASC Name">
                                          
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CreatedOn"  HeaderText="Survey date">
                                          
                                        </asp:BoundField>
                                        
                                        <asp:BoundField DataField="Question"  HeaderText="Question">
                                          
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ScaleAnswer_Desc"  HeaderText="Answer">
                                          
                                        </asp:BoundField>
                                        
                                         <asp:BoundField DataField="Satisfaction_score"  HeaderText="Satisfaction score">
                                          
                                        </asp:BoundField> 
                                        
                                        <asp:BoundField DataField="Ans_weight"  HeaderText="Ans weight">
                                          
                                        </asp:BoundField>
                                        
                                  
                                         
                                        
                                        <asp:BoundField DataField="TotalScore"  HeaderText="Total Score">
                                          
                                        </asp:BoundField>                 
                                
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


