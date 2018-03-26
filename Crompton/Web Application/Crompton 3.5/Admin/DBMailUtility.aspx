<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="DBMailUtility.aspx.cs" Inherits="Admin_DBMailUtility" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                       Mail Watcher
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right:10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" colspan="2">
                         <table width="100%" border="0">
                            <tr>
                                <td>
                                    <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" ID="gvComm" AutoGenerateColumns="false" 
                                        runat="server" Width="100%" HorizontalAlign="Left" >
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Day" >
                                               <ItemTemplate>
                                                 <asp:Label ID="lblday" runat="server" Text='<%# Eval("Day2Show") %>' /> 
                                               </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sent Count" >
                                               <ItemTemplate>
                                                 <asp:Label ID="lblcnt" runat="server" Text='<%# Eval("Sentcount") %>' /> 
                                                  (Queue : <%# Eval("Queued")%>)
                                               </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Failed Count" >
                                               <ItemTemplate>
                                                 <asp:Label ID="lblfcnt" runat="server" Text='<%# Eval("failedcount") %>' /> 
                                               </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField >
                                               <ItemTemplate> 
                                               <asp:HiddenField ID="hdnYear" runat="server" Value='<%# Eval("Year") %>' />
                                               <asp:HiddenField ID="hdnMonth" runat="server" Value='<%# Eval("month") %>' />
                                               <asp:HiddenField ID="hdnDay" runat="server" Value='<%# Eval("Day") %>' />
                                                 <asp:LinkButton ID="lnkbtnshow" runat="server" 
                                                       Visible='<%# !Eval("failedcount").ToString().Equals("0")  %>' 
                                                       onclick="lnkbtnshow_Click" >Show</asp:LinkButton>
                                               </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                        <AlternatingRowStyle CssClass="fieldName" />
                                    </asp:GridView>
                                 </td>
                            </tr>
                          
                            <tr>
                                <td align="center"> 
                                <asp:GridView ID="gvdetail" runat="server" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" Width="100%" HorizontalAlign="Left" AutoGenerateColumns="false"  >
                                    <RowStyle CssClass="gridbgcolor" />
                                <Columns>
                                <asp:TemplateField HeaderText="Recipients" >
                                               <ItemTemplate>    
                                               <div style="width:250px;text-align:left;word-break:break-all;" > <%#Eval("recipients")%> </div>
                                             
                                               </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CopyRecipients" >
                                               <ItemTemplate>
                                                 <asp:Label ID="lblcnt" runat="server" Text='<%# Eval("copy_recipients") %>' /> 
                                               </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Subject" >
                                               <ItemTemplate>
                                               
                                                 <asp:Label ID="lblfcnt" runat="server" Text='<%# Eval("subject") %>' /> 
                                                 
                                               </ItemTemplate>
                                            </asp:TemplateField> 
                                                <%--  <asp:TemplateField HeaderText="View" >
                                               <ItemTemplate>
                                               <img id="imgdv" runat="server" src="../images/arrow-blue.jpg" onclick="document.getElementById('dvpop').style.display='block';" />
                                               <div id="dvpop" style="width:250px;text-align:left;word-break:break-all;z-index:1000;display:none;" > <%#Eval("body")%> </div> 
                                                
                                               </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Description" >
                                               <ItemTemplate>
                                                <div style="width:350px;text-align:left;word-break:break-all;" > <%#Eval("description")%> </div>
                                               </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField>
                                               <ItemTemplate>
                                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updpnl_send" >
                                                <ProgressTemplate>
                                                    Sending..<img src="../images/loading9.gif" alt="" />
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                            <asp:UpdatePanel ID="updpnl_send" runat="server" UpdateMode="Conditional" >
                                            <ContentTemplate>
                                            <asp:LinkButton ID="lbtnresebd" CommandArgument='<%# Eval("mailitem_id")%>' runat="server" Text="resend" onclick="lbtnresebd_Click" />
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                                 
                                               </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                </Columns>
                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                    <AlternatingRowStyle CssClass="fieldName" />
                                        </asp:GridView>
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

