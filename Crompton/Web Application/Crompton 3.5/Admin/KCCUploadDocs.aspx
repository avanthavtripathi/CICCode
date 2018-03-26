<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="KCCUploadDocs.aspx.cs" Inherits="Admin_KCCUploadDocs" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server" >

            <table width="100%">
                <tr>
                    <td class="headingred">
                        Product Related Technical & Other Document Upload
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                 <tr>
                    <td colspan="2" align="right" style="padding-right:10px">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" class="bgcolorcomm" colspan="2">
                   
                        <table border="0" width="100%">
                            <tr>
                                <td align="left" class="MsgTDCount">
                                    Total Number of Records :
                                    <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    <%--Search For
                                    <asp:DropDownList ID="ddlSearch" runat="server" Width="130px" CssClass="simpletxt1">
                                        <asp:ListItem Text="Name" Value="MenuText"></asp:ListItem>
                                        <asp:ListItem Text="URL" Value="NavigateURL"></asp:ListItem>
                                    </asp:DropDownList>
                                    With
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="txtboxtxt" Width="100px" Text=""></asp:TextBox>
                                    <asp:Button Text="Go" Width="25px" CssClass="btn" ID="imgBtnGo" runat="server" CausesValidation="False"
                                        ValidationGroup="editt" OnClick="imgBtnGo_Click" />--%>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0">
                            <tr>
                                <td>
                                    <!-- Menu Listing -->
                                    <asp:GridView PageSize="15" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="True"
                                        AutoGenerateColumns="False" ID="gv" runat="server"  Width="100%" OnPageIndexChanging="gv_OnPageIndexChanging" 
                                        HorizontalAlign="Left" onrowcommand="gv_RowCommand" DataKeyNames="KC_SNo"
                                        onselectedindexchanging="gv_SelectedIndexChanging" >
                                        <Columns>
                                         <asp:BoundField DataField="KCC_Desc" HeaderText="Knowledge Center Category"></asp:BoundField>
                                        <asp:BoundField DataField="Unit_Desc" HeaderText="Division"></asp:BoundField>
                                        <asp:BoundField DataField="ProductLine_Desc" HeaderText="Product Line"></asp:BoundField>
                                        <asp:BoundField DataField="ProductGroup_Desc" HeaderText="Product Group"></asp:BoundField>
                                        <asp:BoundField DataField="CreatedBy" HeaderText="Uploaded By"></asp:BoundField>
                                        <asp:BoundField DataField="Createddate" HeaderText="Uploaded Date"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Download">
                                        <ItemTemplate>
                                        <asp:LinkButton id="xx" runat="server" CausesValidation="false" CommandName="download" CommandArgument='<%# Eval("Filename") %>'  >Download </asp:LinkButton>
                                         </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:BoundField DataField="Status" HeaderText="Status"></asp:BoundField>
                                              <asp:CommandField ShowSelectButton="True" HeaderStyle-Width="60px" HeaderText="Edit">
                                                <HeaderStyle Width="60px" />
                                            </asp:CommandField>                                    
                                        </Columns>
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
                                    </asp:GridView>
                                    <!-- End Menu Listing -->
                                </td>
                            </tr>
                     </table>
                        <table border="0" cellpadding="0" style="width: 100%">
                                                            <tr>
                                                                <td align="center" class="headingred">
                                                                    <asp:HiddenField ID="hdnNo" runat="server" />
                                                                   </td>
                                                                <td align="left">
                                                                  </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" class="headingred" colspan="2">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" width="30%">
                                                                    &nbsp;Knowledge Center Category: <font color='red'>*</font>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:DropDownList ID="ddlKCCat"  runat="server" Width="175px" 
                                                                        CssClass="simpletxt1" AutoPostBack="True" 
                                                                        onselectedindexchanged="ddlKCCat_SelectedIndexChanged" >
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RfvKcc" runat="server" 
                                                                        ControlToValidate="ddlKCCat" Display="Dynamic" ErrorMessage="Select a category" 
                                                                        InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                </td>
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
                                                                  Product Line: <font color='red'>*</font>
                                                                </td>
                                                                <td align="left">
                                                                   <asp:DropDownList ID="ddlProductLine" CssClass="simpletxt1" Width="175" runat="server"
                                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlProductLine_SelectedIndexChanged">
                                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvPLine" runat="server" InitialValue="Select"
                                                                    SetFocusOnError="true" ControlToValidate="ddlProductLine">Product line is required</asp:RequiredFieldValidator>
                                         
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                Product Group: 
                                                                </td>
                                                                <td align="left">
                                                                                 <asp:DropDownList ID="ddlProductGroup" CssClass="simpletxt1" Width="175" runat="server">
                                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                
                                                                 </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblBrowseFile" runat="server" >Select File(Max. Size Limit : 4MB)</asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:FileUpload ID="uploadfile" runat="server"  CssClass="btn" />
                                                                    
                                                                </td>
                                                            </tr>
                                                             <tr>
                                            <td align="right">
                                                Status
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="rdoStatus" RepeatDirection="Horizontal" RepeatColumns="2"
                                                    runat="server">
                                                    <asp:ListItem Value="1" Text="Active" Selected="True" >
                                                    </asp:ListItem>
                                                    <asp:ListItem Value="0" Text="In-Active">
                                                    </asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    &nbsp;</td>
                                                                <td align="left">
                                                                   <table>
                                                    <tr>
                                                        <td align="right">
                                                           <asp:Button ID="fldUpload" Width="70px" runat="server" Text="Upload" CssClass="btn" 
                                                             OnClick="fldUpload_Click" />
                                                           <asp:Button Text="Save" Width="70px" ID="imgBtnUpdate" CssClass="btn" runat="server"
                                                            CausesValidation="True" OnClick="imgBtnUpdate_Click" />
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
                                                                <td align="right">
                                                                    &nbsp;</td>
                                                                <td align="left">
                                                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="False"></asp:Label>
                                                                     
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    &nbsp;</td>
                                                                <td align="left">
                                                                    
                                                                     
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            <td></td>                                                            
                                                            <td>
                                                             </td>      
                                                            </tr>  
                                                             <tr>
                                                            <td></td>                                                            
                                                            <td>
                                                                </td>
                                                            </tr>                                                          
                                                        </table>
                    </td>
                </tr>
            </table>
</asp:Content>

