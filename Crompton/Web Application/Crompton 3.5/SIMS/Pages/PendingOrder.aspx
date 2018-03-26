<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true"
    CodeFile="PendingOrder.aspx.cs" Inherits="SIMS_Pages_PendingOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    

    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
        <script language="javascript" type="text/javascript">
                function funUploadPopUp(PoNo)
                {
                    var strUrl='SalesOrderHistoryLog.aspx?pono='+ PoNo;
                     custWin=   window.open(strUrl,'SCPopup','height=650,width=850,left=20,top=30,scrollbars=1');
                        if (window.focus) {custWin.focus()}
                    }
					
        // 4 nov bhawesh for postback in modalpopup
                    function fnClickOK(sender, e) {
                        __doPostBack(sender, e);
                    } 
    </script>
            <table width="100%">
                <tr>
                    <td class="headingred" style="width: 968px">
                        Pending List Of Sales Order
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["SIMSAjaxLoadingImageName"]%>" alt="" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right" style="padding-right: 10px">
                    <%--
                      <asp:Button Visible="false" Text="Go" Width="25px" CssClass="btn" ID="imgBtnGo" runat="server"
                            CausesValidation="False" ValidationGroup="editt1" OnClick="imgBtnGo_Click" /> --%>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" colspan="2">
                        <table border="0" width="100%">
                            <tr>
                                <td align="left" class="MsgTDCount">
                                    Total Number of Records :
                                    <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server">
                                    </asp:Label>
                                    <asp:HiddenField ID="hdnASC_Id" runat="server" />
                                </td>
                                <td align="right">
                                    Search For
                                    <asp:DropDownList ID="ddlSearch" runat="server" Width="170px" CssClass="simpletxt1">
                                        <asp:ListItem Text="Invoice Number" Value="MISDetail.SAP_Invoice_No"></asp:ListItem> <%-- ISID.SAP_Invoice_No--%>
                                        <asp:ListItem Text="Sap Sales Order" Value="MISDetail.SAP_Sales_Order"></asp:ListItem> <%-- ISID.SAP_Sales_Order--%>
                                        <asp:ListItem Text="System Ident Number" Value="ISSO.SIMS_Indent_No"></asp:ListItem>
                                        <asp:ListItem Text="PO Number" Value="ISSO.PO_Number"></asp:ListItem>
                                    </asp:DropDownList>
                                    With
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="txtboxtxt" Width="150px" Text=""></asp:TextBox>
                                    <asp:Button Text="Go" Width="25px" CssClass="btn" ID="Button1" runat="server" CausesValidation="False"
                                        ValidationGroup="editt" OnClick="imgBtnGo_Click" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvSearch" runat="server" AllowPaging="True" PageSize="10" AllowSorting="True"
                                        AlternatingRowStyle-CssClass="fieldName" AutoGenerateColumns="False" GridGroups="both"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" DataKeyNames="SIMS_Indent_No" HorizontalAlign="Left"
                                        OnRowUpdating="gvSearch_RowUpdating" RowStyle-CssClass="gridbgcolor" Width="100%"
                                        OnRowDataBound="gvSearch_RowDataBound" 
                                        onpageindexchanging="gvSearch_PageIndexChanging" >
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Division">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductDiv" runat="server" Text='<%# Bind("Unit_Desc") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PO NUMBER">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblPOnumber" runat="server" CommandArgument='<%# Eval("PO_Number") %>'
                                                        CommandName="Update" Text='<%# Eval("PO_Number") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SYSTEM INDENT NUMBER">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblIndentNo" runat="server" CommandName="Update" CommandArgument='<%# Eval("SIMS_Indent_No") %>'
                                                        Text='<%# Eval("SIMS_Indent_No") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                               <asp:TemplateField HeaderText="INDENT DATE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblorderdate" runat="server" Text='<%# Bind("OrderDate") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SAP SALES ORDER">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblSalesorder" runat="server" CommandArgument='<%# Eval("SAP_Sales_Order") %>'
                                                        CommandName="Update" Text='<%# Eval("SAP_Sales_Order") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SAP SALES ORDER PUSH DATE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSalesorderdate" runat="server" Text='<%# Bind("SAP_Sales_Order_Date") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="INVOICE NUMBER">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblInvoiceNo" runat="server" CommandArgument='<%# Eval("SAP_Invoice_No") %>'
                                                        CommandName="Update" Text='<%# Eval("SAP_Invoice_No") %>' />
                                                    <asp:HiddenField ID="hdnSpareFlag" runat="server" Value='<%# Bind("SpareFlag") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Stage">
                                                <ItemTemplate>
                                                    <a href="Javascript:void(0);" onclick="funUploadPopUp('<%#Eval("PO_Number")%>')">
                                                        <%#Eval("Stage")%></a>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChkPONumber" runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
											<%--4 nov bp--%>
                                            <asp:TemplateField>
                                            <ItemTemplate>
                                            
                                            <asp:LinkButton runat="server" Text="Cancel" ID="lbtncancel" onclick="lbtncancel_Click" ></asp:LinkButton>
                                            <cc1:ModalPopupExtender runat="server" ID="mpas" TargetControlID="lbtncancel" BackgroundCssClass="modalBackground" 
                                            PopupControlID="dvcomment" 
                                            CancelControlID="CancelButton" >
                                            </cc1:ModalPopupExtender>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                        <img alt="" src='<%=ConfigurationManager.AppSettings["SIMSUserMessage"]%>' />
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
                                <td>
                              <%-- 4 nov bhawesh Pop up for cancel reason--%>
                                <asp:Label ID="hd2" runat="server" Visible="false" />
                                
                                    <div id="dvcomment" runat="server"  class="modalPopup" style="display:none">
                                    <table style="vertical-align:top;text-align:left;width:350px">
                                    <tr>
                                    <td>SIMS Intend No</td>
                                    <td> <asp:Label ID="hd1" runat="server" /></td>
                                    </tr>
                                    <tr>
                                    <td>SIMS Intend Date</td>
                                    <td> <asp:Label ID="lblinddate" runat="server" /></td>
                                    </tr>
                                     <tr>
                                    <td>PO No</td>
                                    <td><asp:Label ID="hd3" runat="server" /></td>
                                    </tr>
                                    <tr>
                                    
                                    <td>
                                    Cancel Reason
                                    </td>
                                    <td>
                                    <asp:TextBox ID="txtreason" runat="server" ></asp:TextBox>
                                    <div>
                                    <asp:RequiredFieldValidator ID="reqreason" runat="server" ControlToValidate="txtreason" ErrorMessage="Cancel reason is mendatory." ValidationGroup="r"></asp:RequiredFieldValidator>
                                    </div>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td></td>
                                    <td>  <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Confirm" ValidationGroup="r" 
                                            onclick="btnCancel_Click" />
                                    <asp:Button ID="CancelButton" runat="server" CssClass="btn" Text="Cancel" 
                                             /></td>
                                    </tr>
                                    </table>
                                  
                                    </div>
                                </td>
                                <tr>
                                    <td height="10" align="center">
                                        <!-- For button portion update -->
                                        <table>
                                            <tr>
                                                <td align="right">
                                                    <asp:Button Text="SUBMIT" Width="60px" ID="imgBtnPO" CssClass="btn" runat="server"
                                                        CausesValidation="True" ValidationGroup="editt" OnClick="imgBtnPO_Click" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- For button portion update end -->
                                    </td>
                                </tr>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
						
                            <td class="headingred" style="width: 968px;text-align:left">
                      <%--   added 4 nov List Of Cancelled Sales Order--%>
                        List Of Cancelled Sales Order
                    </td>
                            </tr>
                            <tr>
                            
                                <td>
                                 <table border="0" width="100%">
                            <tr>
                                <td align="left" class="MsgTDCount">
                                    Total Number of Records :
                                    <asp:Label ID="lblcancount" CssClass="MsgTotalCount" runat="server">
                                    </asp:Label>
                                   
                                </td>
                                   <td align="right">
                                    Search For
                                    <asp:DropDownList ID="ddlsearch2" runat="server" Width="170px" 
                                           CssClass="simpletxt1">
                                       <asp:ListItem Text="System Ident Number" Value="ISSO.SIMS_Indent_No"></asp:ListItem>
                                       <asp:ListItem Text="PO Number" Value="ISSO.PO_Number"></asp:ListItem>
                                    </asp:DropDownList>
                                    With
                                    <asp:TextBox ID="txtsearch2" runat="server" CssClass="txtboxtxt" Width="150px" 
                                           Text=""></asp:TextBox>
                                    <asp:Button Text="Go" Width="25px" CssClass="btn" ID="btnsearch2" runat="server" CausesValidation="False"
                                        ValidationGroup="editt" onclick="btnsearch2_Click"  />
                                </td>
                            </tr>
                        </table>
                        </td>
                                </tr>
                            <tr>
                            <td>
                            
                            <asp:GridView ID="gvcancelled" runat="server" AllowPaging="True" PageSize="10" AllowSorting="True"
                                        AlternatingRowStyle-CssClass="fieldName" AutoGenerateColumns="False" GridGroups="both"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" 
                                    DataKeyNames="SIMS_Indent_No" HorizontalAlign="Left"
                                        RowStyle-CssClass="gridbgcolor" Width="100%" 
                                        onpageindexchanging="gvcancelled_PageIndexChanging" >
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Division">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductDiv" runat="server" Text='<%# Bind("Unit_Desc") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PO NUMBER">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPOnumber" runat="server" Text='<%# Eval("PO_Number") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SYSTEM INDENT NUMBER">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIndentNo" runat="server" Text='<%# Eval("SIMS_Indent_No") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                               <asp:TemplateField HeaderText="INDENT DATE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblorderdate" runat="server" Text='<%# Bind("OrderDate") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SAP SALES ORDER">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSalesorder" runat="server" Text='<%# Eval("SAP_Sales_Order") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SAP SALES ORDER PUSH DATE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSalesorderdate" runat="server" Text='<%# Bind("SAP_Sales_Order_Date") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="INVOICE NUMBER">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvoiceNo" runat="server" Text='<%# Eval("SAP_Invoice_No") %>' />
                                                    </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Stage">
                                                <ItemTemplate>
                                                    <a href="Javascript:void(0);" onclick="funUploadPopUp('<%#Eval("PO_Number")%>')">
                                                        <%#Eval("Stage")%></a>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="Cancel Reason">
                                            <ItemTemplate>
                                             <asp:Label runat="server" Text='<%# Bind("Rejection_Reason") %>' ID="lblcancel" ></asp:Label>
                                        
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                        <img alt="" src='<%=ConfigurationManager.AppSettings["SIMSUserMessage"]%>' />
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
                        </table>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
