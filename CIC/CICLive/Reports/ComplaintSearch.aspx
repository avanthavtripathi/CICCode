<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="ComplaintSearch.aspx.cs" Inherits="Reports_ComplaintSearch" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%" border="0">
                <tr>
                    <td class="headingred" style="width: 40%">
                       Customer Search By Contact No
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
                        Contact No:
                    </td>
                    <td>
                        <asp:TextBox ID="TxtContactNo" Width="175" runat="server"></asp:TextBox>  
                        <div>
                        <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="TxtContactNo" ErrorMessage="Please enter a contact No" Display="Dynamic" ></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rfvreg" runat="server" ErrorMessage="Please enter atleast 8-10 digits of contact no." 
                            ControlToValidate="TxtContactNo" Display="Dynamic" 
                            ValidationExpression="\d{8,10}" ></asp:RegularExpressionValidator>
                       </div>
                    </td></tr><tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="Search" Width="100"
                            OnClick="btnSearch_Click" />
                        <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn" Text="Export To Excel"
                            Width="100"  />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left">
                        Total Count:
                        <asp:Label ID="lblCount" ForeColor="Red" runat="server" Text="0"></asp:Label></td></tr><tr>
                    <td colspan="2">
                        <asp:GridView ID="gvMIS" CssClass="simpletxt1" runat="server" RowStyle-CssClass="bgcolorcomm"
                            Width="100%" AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                            GridGroups="both" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="Left" RowStyle-HorizontalAlign="Left" >
                            <Columns>
                            <asp:BoundField DataField="Name" HeaderText="Customer Name" />
                            <asp:TemplateField  HeaderText="ComplaintNo">
                            <ItemTemplate>
                            <a href="Javascript:void(0);" onclick="funCommonPopUp(<%#Eval("BaseLineId")%>)" >
                            <%#Eval("Complaint_Split")%>
                            </a>
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="LoggedDate" HeaderText="LoggedDate"  />
                            <asp:BoundField DataField="Address" HeaderText="Address"  />
                            <asp:BoundField DataField="State" HeaderText="State" />
                            <asp:BoundField DataField="City" HeaderText="City" />
                            <asp:BoundField DataField="ProductDivision_Desc" HeaderText="Product" />
                            <asp:BoundField DataField="NatureOfComplaint" HeaderText="NatureOfComplaint" />
                            <asp:BoundField DataField="SC_Name" HeaderText="SC Name" Visible="false" />
                            <asp:BoundField DataField="CallStage" HeaderText="CallStage" />
                            <asp:BoundField DataField="StageDesc" HeaderText="StageDesc" />
                             <%-- ContactNo Alternate Number RegistrationDate  BaseLineId  ProductLine_Desc Product_Desc   Branch_Name--%></Columns><EmptyDataTemplate>
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
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;</td></tr></table></ContentTemplate></asp:UpdatePanel></asp:Content>