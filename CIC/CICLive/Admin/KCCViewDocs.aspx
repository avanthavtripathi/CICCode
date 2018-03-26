<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="KCCViewDocs.aspx.cs" Inherits="Admin_KCCViewDocs" Title="Knowledge Center Documnet View" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
    <link href="../css/AjaxStyles.css" rel="stylesheet" type="text/css" />
<table width="100%">
                <tr>
                    <td class="headingred">
                        Product & Technical Information View/Download
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                 <tr>
                    <td colspan="2" align="right" style="padding-right:10px">
                        &nbsp;</td>
                </tr>
                <tr>
                <td colspan="2">
              <asp:UpdatePanel runat="server">
              <ContentTemplate>
                 <table border="0" cellpadding="0" style="width: 100%">
                                                            <tr>
                                                                <td align="center" class="headingred">
                                                                    &nbsp;</td>
                                                                <td align="left">
                                                                  </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" class="headingred" colspan="2">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td align="right">
                                                                 Product Division: <font color='red'>*</font>
                                                                </td>
                                                                <td align="left">
                                                                       <asp:DropDownList ID="ddlUnit" CssClass="simpletxt1" Width="175" runat="server" AutoPostBack="True"
                                                                        OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged">
                                                                        <asp:ListItem>Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="Select"
                                                                        SetFocusOnError="true" ControlToValidate="ddlUnit">Product Division is required.</asp:RequiredFieldValidator>
                                                         
                                                                
                                                                  </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                  Product Line: 
                                                                </td>
                                                                <td align="left">
                                                                        <asp:DropDownList ID="ddlProductLine" CssClass="simpletxt1" Width="175" 
                                                                            runat="server" AutoPostBack="True"  
                                                                            onselectedindexchanged="ddlProductLine_SelectedIndexChanged">
                                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                         
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    Product Group:</td>
                                                                <td align="left">
                                                                  <asp:DropDownList ID="Ddlprodctgroup" runat="server" Width="175px" 
                                                                        CssClass="simpletxt1" >
                                                                  <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                  </asp:DropDownList>        
                                                               </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" width="30%">
                                                                    &nbsp;Knowledge Center Category: 
                                                                </td>
                                                                <td align="left">
                                                                    <asp:DropDownList ID="ddlKCCat" runat="server" Width="175px" CssClass="simpletxt1" >
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    &nbsp;</td>
                                                                <td align="left">
                                                                   <table>
                                                    <tr>
                                                        <td align="right">
                                                           <asp:Button ID="BtnSearch" Width="70px" runat="server" Text="Search" CssClass="btn" 
                                                             onclick="BtnSearch_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="imgBtnCancel" Width="70px" runat="server" CausesValidation="false"
                                                                CssClass="btn" Text="Cancel" onclick="imgBtnCancel_Click"  />
                                                        </td>
                                                    </tr>
                                                </table>
                                                                </td>                                                                                   
                                                             
                                                            </tr>
                                                            <tr>
                                                                  <td></td>
                                                                <td align="left">
                                                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="False"></asp:Label>
                                                                     
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                
                                                                 </td>
                                                            </tr>
                                                             <tr>
                                                            <td align="left" class="MsgTDCount">
                                    Total Number of Records :
                                    <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                </td>                                                            
                                                            <td>
                                                                </td>
                                                            </tr>                                                          
                                                        </table>
              </ContentTemplate>
              <Triggers>
               <asp:PostBackTrigger ControlID="BtnSearch" />
              </Triggers>
              </asp:UpdatePanel>
                
             
              
                </td>
                </tr>
                <tr>
                <td colspan="2">
            
               <cc1:Accordion ID="Accord" runat="Server" SelectedIndex="0" RequireOpenedPane="true" HeaderCssClass="accordionHeader" ContentCssClass="accordionContent" OnItemDataBound="Acc_OnItemDataBound"    > 
                    <HeaderTemplate>
                    <ul>
                    <li>
                    <asp:Label ID="lbltitle" runat="server" Text ='<%# DataBinder.Eval(Container.DataItem,"Productline_Desc") %>' Font-Underline="true" ></asp:Label>
                    </li>
                    </ul> 
                    </HeaderTemplate>
                    <ContentTemplate>
                    <ul>
                   
                   <asp:GridView id="gv" runat="server" ShowHeader="true" Width="100%" OnRowDataBound="gv_RowDataBound" OnRowCommand="gv_RowCommand" GridLines="None"
                    HeaderStyle-HorizontalAlign="Left" >
                   <Columns>
                   <asp:TemplateField>
                   <ItemTemplate>
                   <li>
                   <asp:LinkButton ID="lbtn" runat="server" CausesValidation="false" CommandName="download" CommandArgument='<%# Eval("Document") %>'  > </asp:LinkButton>
                   
                   <asp:HyperLink ID="lbtnView" runat="server" NavigateUrl='<%# Server.UrlDecode(String.Format("{0}{1}","",Eval("Document").ToString())) %>' ></asp:HyperLink>
                   </li>
                   
                   </ItemTemplate>
                   </asp:TemplateField>
                   </Columns>
                   </asp:GridView>
                   </ul>
                    </ContentTemplate>
                    
                    </cc1:Accordion>
                   
                </td>
                </tr>
                </table>

</asp:Content>

