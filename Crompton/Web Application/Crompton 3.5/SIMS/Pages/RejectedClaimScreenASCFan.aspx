<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true"
    CodeFile="RejectedClaimScreenASCFan.aspx.cs" Inherits="SIMS_Pages_RejectedClaimScreenASCFan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script type="text/javascript">

//        function Openpopup(popurl)
//        {
//        winpops = window.open(popurl,"","width=1000, height=700, left=15, top=15, scrollbars=yes, menubar=no,resizable=no,directories=no,location=center")
//        }
    </script>

    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <table width="100%" cellpadding="2" cellspacing="0">
                <tr>
                    <td class="headingred" style="width: 968px">
                       Fans Rejected Claim Screen for ASC
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["SIMSAjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" colspan="2">
                        <table width="100%" border="0">
                            <tr id="tdSparereciept" runat="server">
                                <td width="100%" align="left" class="bgcolorcomm">
                                    <table width="98%" border="0">
                                        <tr>
                                           
                                           <td colspan="1" style="width: 21%;">Year <span style="color:Red;">*</span>
                              
                                    <asp:DropDownList ID="ddlYear" runat="server" Width="100px" CssClass="simpletxt1">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="2015">2015</asp:ListItem>
                                        <asp:ListItem Value="2016">2016</asp:ListItem>
                                         <asp:ListItem Value="2017">2017</asp:ListItem>
                                         <asp:ListItem Value="2018">2018</asp:ListItem>
                                    </asp:DropDownList>
                                    
                                </td>
                                           
                                           <td colspan="1">Month:-<asp:DropDownList ID="ddlmnth" runat="server">
                                               <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                               <asp:ListItem Text="February" Value="2"></asp:ListItem>
                                               <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                               <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                               <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                               <asp:ListItem Text="June" Value="6"></asp:ListItem>
                                               <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                               <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                               <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                               <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                               <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                               <asp:ListItem Text="December" Value="12"></asp:ListItem>
                                               </asp:DropDownList>
                                            </td>
                                           </tr>
                                        
                                       
                                        <tr>
                                            <td>
                                                <asp:Button ID="BtnSEARCH" runat="server" CssClass="btn"  style="margin-left: 116%;margin-top: 5%;"
                                                    onclick="BtnSEARCH_Click" Text="SEARCH" Width="100px" />
                                            </td>
                                            <tr>
                                                <td colspan="2">
                                                    <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:GridView ID="gvSummary" runat="server" AutoGenerateColumns="false" 
                                                                    style="text-align:center;" Width="100%">
                                                                    <RowStyle CssClass="gridbgcolor" />
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="SNo" HeaderStyle-HorizontalAlign="Left" 
                                                                            HeaderText="SNo" ItemStyle-HorizontalAlign="Center">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Complaint_no" HeaderStyle-HorizontalAlign="Left" 
                                                                            HeaderText="ComplaintNo" ItemStyle-HorizontalAlign="Center">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Activity" HeaderStyle-HorizontalAlign="Left" 
                                                                            HeaderText="ComplaintType" ItemStyle-HorizontalAlign="Center">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="RejectRemark" HeaderStyle-HorizontalAlign="Left" 
                                                                            HeaderText="Rejection Reason" ItemStyle-HorizontalAlign="Center">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="Comment" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtcomment" runat="server" Height="20px" Text="" 
                                                                                    TextMode="MultiLine"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <HeaderStyle CssClass="fieldNamewithbgcolor" HorizontalAlign="Center" />
                                                                    <AlternatingRowStyle CssClass="fieldName" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2" height="25">
                                                    <table>
                                                        <tr>
                                                          
                                                          <td>
                                                                <asp:Button ID="imgBtnSave" runat="server" CausesValidation="false" 
                                                                    CssClass="btn" Text="Save & Close"  Width="90px" 
                                                                    onclick="imgBtnSave_Click" />
                                                            </td>
                                                          
                                                          
                                                            <td align="right">
                                                                <asp:Button ID="imgBtnAdd" runat="server" CausesValidation="True" 
                                                                    CssClass="btn" Text="Send For Reapproval" ValidationGroup="editt" 
                                                                    Width="139px" onclick="imgBtnAdd_Click" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="imgBtnCancel" runat="server" CausesValidation="false" 
                                                                    CssClass="btn" Text="Cancel"  Width="70px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text="" Font-Size="15px"></asp:Label>
                                    <asp:Label ID="lblSuccess" runat="server" ForeColor="Green" Text="" Font-Size="15px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
