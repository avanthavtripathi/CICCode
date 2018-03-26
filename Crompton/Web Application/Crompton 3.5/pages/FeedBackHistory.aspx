<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FeedBackHistory.aspx.cs" Inherits="pages_FeedBackHistory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FeedBack History</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/global.css" rel="stylesheet" type="text/css" />
    </head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%">
                    <tr bgcolor="white">
                        <td class="headingred">
                            FeedBack History
                        </td>
                        <td align="right">
                            <a href="javascript:void(0)" class="links" onclick="window.close();">Close</a>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 10px" align="center" colspan="2" class="bgcolorcomm">
                            <asp:GridView RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                               HeaderStyle-CssClass="fieldNamewithbgcolor" GridLines="both" AutoGenerateColumns="false" 
                               ID="gvCommunication" runat="server" Width="98%" HorizontalAlign="Left" 
                                onrowdatabound="gvCommunication_RowDataBound">
                               <Columns>
                               <asp:BoundField HeaderText="Remarks" DataField="Remarks" />
                               <asp:BoundField HeaderText="UserName" DataField="UserName" />
                               <asp:BoundField HeaderText="ActionDate" DataField="ActionDate" />
                               <asp:BoundField HeaderText="Closed" DataField="IsClosed" />
                               </Columns>
                                                <RowStyle CssClass="gridbgcolor" />
                                                <EmptyDataTemplate>
                                                   <img src="<%=ConfigurationManager.AppSettings["UserMessage"]%>" alt="" /><b>No Records found.</b>
                                                </EmptyDataTemplate>
                                                <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                <AlternatingRowStyle CssClass="fieldName" />
                                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                    <td colspan="2">
                    <table id="tblaction" border="0" runat="server" align="center" width="70%" style="display:block;vertical-align:top" >
                            <tr align="left">
                                <td>
                                    Customer Name</td>
                                <td>
                                    <asp:Label ID="LblCustname" runat="server" />
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    Company Name</td>
                                <td>
                                    <asp:Label ID="LblCompanyName" runat="server" />
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    Address</td>
                                <td>
                                     <asp:Label ID="LblAddress" runat="server" />
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    City (State)</td>
                                <td>
                                    <asp:Label ID="LblCity" runat="server" />
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    Feedback Type</td>
                                <td>
                                    <asp:Label ID="LblFeedbackType" runat="server" />
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    Feedback Details</td>
                                <td>
                                    <div id="dvFeedbackDetails" 
                                        style="width:350px; text-align:left;word-break:break-all;" runat="server" />
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    Feedback Date</td>
                                <td>
                                   <asp:Label ID="LblFeedbackdate" runat="server" />
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    Product Division</td>
                                <td>
                                    <asp:Label ID="LblProducuDivision" runat="server" />
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    Contact No</td>
                                <td>
                                    <asp:Label ID="LblContactNo" runat="server" />
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    Email</td>
                                <td>
                                    <asp:Label ID="Lblemail" runat="server" />
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    Closure Remarks
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtRemarks" runat="server" Height="50px" MaxLength="500" 
                                        Rows="3" Width="300px"></asp:TextBox>
                                   <div>
                                   <asp:RequiredFieldValidator ID="RfvRemarks" runat="server" ErrorMessage="Enter Remarks." ControlToValidate="TxtRemarks" />
                                   </div>     
                                    
                                </td>
                            </tr>
                            <tr >
                            <td>
                            <asp:CheckBox ID="ChkClosed" runat="server" Text="Confirm Closure" />
                            </td>
                            <td>
                            <asp:Button Width="70px" Text="Post" CssClass="btn" ID="BtnSubmitRemarks" runat="server" onclick="BtnSubmitRemarks_Click" />
                            </td>
                            </tr>
                            <tr>
                            <td colspan="2">
                             <asp:Label ID="Lblmsg" runat="server" />
                            </td>
                            </tr>
                            </table>
                    </td>
                    </tr>
                </table>
    </div>
    </form>
</body>
</html>
