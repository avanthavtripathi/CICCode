<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true" CodeFile="SpareChalanReceipt.aspx.cs" Inherits="SIMS_Pages_SpareChalanReceipt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
<asp:UpdatePanel ID="updatepnl" runat="server">
<ContentTemplate>
    <table width="100%" cellpadding="2" cellspacing="0">
        <tr>
            <td class="headingred">
                <h3>Spare Chalan Receipt</h3>
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
            <td style="padding: 10px" align="center" colspan="2">
                <table width="100%" border="0">
                    <tr>
                        <td width="100%" align="left" class="bgcolorcomm">
                            <table border="0" style="width: 100%">
                                <tr>
                                    <td colspan="2" align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                        <font color='red'>*</font>
                                        <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                </tr>
                                <tr>
                                    <td>ASC Challan No.:<font color='red'>*</font></td>
                                    <td>
                                        <asp:DropDownList ID="ddlASCChallan" runat="server" CssClass="simpletxt1" Width="175px"
                                            ValidationGroup="editt" AutoPostBack="True" 
                                            onselectedindexchanged="ddlASCChallan_SelectedIndexChanged"  >
                                        </asp:DropDownList><asp:RequiredFieldValidator ID="RFRegionDesc" runat="server" ControlToValidate="ddlASCChallan"
                                            ErrorMessage="ASC Challan No. is required." InitialValue="Select" SetFocusOnError="true"
                                            ValidationGroup="editt" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td>ASC Name:</td>
                                    <td>
                                            <asp:Label ID="lblASCName" runat="server"></asp:Label>
                                            <asp:HiddenField ID="hdnASC_Code" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Date of Transcation: </td>
                                    <td><asp:Label ID="lbldate" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>CGL Branch:</td>
                                    <td><asp:Label ID="lblStockTransferDocumentNo" runat="server"></asp:Label></td>  
                                </tr>
                                <tr>
                                    <td>CG Engineer Name:</td>
                                    <td><asp:Label ID="lblCGSEName" runat="server"></asp:Label></td>  
                                </tr>
                                
                                <tr>
                                  <td colspan="2">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table width="100%">
                                            <tr>
                                                <td bgcolor="#CCCCCC"><b>Claim Number</b></td>
                                                <td bgcolor="#CCCCCC"><b>Spare Code & Description</b></td>
                                                <td bgcolor="#CCCCCC"><b>UOM</b></td>
                                                <td bgcolor="#CCCCCC"><b>Dispatch Qty.</b></td>
                                                <td bgcolor="#CCCCCC"><b>Received Qty.</b></td>
                                                <td bgcolor="#CCCCCC"><b>SAP Document</b></td>
                                            </tr>
                                            <tr>
                                                <td valign="top" >
                                                    
                                                </td>
                                                <td valign="top" >
                                                    
                                                </td>
                                                <td valign="top"></td>
                                                <td valign="top"></td>
                                                <td valign="top">
                                                   
                                                </td>
                                                <td valign="top" ></td>
                                                <td valign="top" >
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                  <td colspan="2">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <!-- For button portion update -->
                                        <table>
                                            <tr>
                                                <td align="right">
                                                    <asp:Button Text="Confirm" Width="70px" CssClass="btn" ID="imgBtnAdd" runat="server"
                                                        CausesValidation="True" ValidationGroup="editt" 
                                                        onclick="imgBtnAdd_Click"  />
                                                </td>
                                                <td>
                                                    <asp:Button ID="imgBtnCancel" Width="70px" runat="server" CausesValidation="false"
                                                        CssClass="btn" Text="Cancel" onclick="imgBtnCancel_Click"  />
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- For button portion update end -->                                        </td>
                                </tr>
                            </table>
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

