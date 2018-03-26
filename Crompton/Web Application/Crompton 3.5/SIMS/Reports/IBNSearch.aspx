<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true" CodeFile="IBNSearch.aspx.cs" Inherits="Reports_IBNSearch" Title="IBN Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%" border="0">
                <tr>
                    <td class="headingred" style="width: 40%">
                       IBN Detail Search
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["SIMSAjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Internal Bill No:
                    </td>
                    <td>
                        <asp:TextBox ID="TxtIBN" Width="175" runat="server" MaxLength="20"></asp:TextBox>  
                        <div>
                        <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="TxtIBN" ErrorMessage="Please enter a Bill No" Display="Dynamic" ></asp:RequiredFieldValidator>
                       </div>
                    </td></tr><tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="Search" Width="100"
                            OnClick="btnSearch_Click" />
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
                            <asp:BoundField DataField="Internal_Bill_No" HeaderText="Internal Bill No"  />
                            <asp:BoundField DataField="Complaint_No" HeaderText="Complaint No"  />
                            <asp:BoundField DataField="Claim_No" HeaderText="Claim No" />
                            <asp:BoundField DataField="Claim_Date" HeaderText="Claim Date" />
                            <asp:BoundField DataField="Bill_Created_By" HeaderText="Created By" />
                            <asp:BoundField DataField="Destroy_Transaction_No" HeaderText="Transaction No" />
                            <asp:BoundField DataField="Destroyed_Date" HeaderText="Destroyed Date" />
                            <asp:BoundField DataField="Challan_No" HeaderText="Challan No" />
                            <asp:BoundField DataField="Challan_By" HeaderText="Challan By" />
                            <asp:BoundField DataField="Challan_Date" HeaderText="Challan Date" />
                             </Columns>
                            <EmptyDataTemplate>
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                            <img src="<%=ConfigurationManager.AppSettings["SIMSUserMessage"]%>" alt="" />
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