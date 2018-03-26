<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="ServiceEngineerMaster.aspx.cs" Inherits="Admin_ServiceEngineerMaster" Title="Service Engineer Page" EnableEventValidation="false" %>

<asp:Content ID="ContentSE" ContentPlaceHolderID="MainConHolder" Runat="Server">


<asp:UpdatePanel ID="updatepnl" runat="server">
    <Triggers>
        <asp:PostBackTrigger ControlID="btnExportToExcel" /> 
    </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Service Engineer Master
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
                    <td colspan="2" align="right" style="padding-right:10px">
                        <asp:RadioButtonList ID="rdoboth" RepeatDirection="Horizontal" RepeatColumns="3"
                            runat="server" AutoPostBack="True" 
                            onselectedindexchanged="rdoboth_SelectedIndexChanged">
                            <asp:ListItem Value="1" Text="Active" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="0" Text="In-Active"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Both"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr> 
                    <td style="padding: 10px" align="center" colspan="2" class="bgcolorcomm">
                    <table border="0" width="100%">

                            <tr>
                            
                                <td align="left" class="MsgTDCount">
                                    Total Number of Records : <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                </td>


                                <td align="right">Search For <asp:DropDownList ID="ddlSearch" runat="server" Width="175px" CssClass="simpletxt1">
                                <asp:ListItem Text="SE Code" Value="ServiceEng_Code"></asp:ListItem>
                                <asp:ListItem Text="Service Engineer" Value="ServiceEng_Name"></asp:ListItem>
                                <asp:ListItem Text="Service Contractor" Value="SC_Name"></asp:ListItem>
                                <asp:ListItem Text="Mobile No" Value="se.PhoneNo"></asp:ListItem>
                                
                             
                                </asp:DropDownList>
                                
                                With <asp:TextBox ID="txtSearch" runat="server" CssClass="txtboxtxt" Width="100px" Text=""></asp:TextBox>
                                
                                  <asp:Button Text="Go" Width="25px" CssClass="btn" ID="imgBtnGo" runat="server"
                                    CausesValidation="False" ValidationGroup="editt" OnClick="imgBtnGo_Click"  />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table width="100%" border="0">
                            <tr>
                                <td>
                                    <!-- Service Contractor Listing   -->
                                    <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridLines="both" AllowPaging="True"
                                        AllowSorting="True" DataKeyNames="ServiceEng_SNo" AutoGenerateColumns="False" ID="gvServiceEng"
                                        runat="server" OnPageIndexChanging="gvServiceEng_PageIndexChanging" Width="98%" OnSelectedIndexChanging="gvServiceEng_SelectedIndexChanging"
                                        HorizontalAlign="Left" EnableSortingAndPagingCallbacks="True" onsorting="gvServiceEng_Sorting">
                                        <Columns>
                                            <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                <HeaderStyle Width="40px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ServiceEng_Code" SortExpression="ServiceEng_Code" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="SE Code">
                                                <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ServiceEng_Name" SortExpression="ServiceEng_Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Service Engineer">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="PhoneNo" SortExpression="PhoneNo" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Phone Number">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            
                                            
                                            
                                             <asp:BoundField DataField="SC_Name" SortExpression="SC_Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Service Contractor">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                             
                                            
                                            <asp:BoundField DataField="Active_Flag" SortExpression="Active_Flag" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Status">
                                            </asp:BoundField>
                                            <asp:CommandField ShowSelectButton="True" HeaderStyle-Width="60px" HeaderText="Edit">
                                                <HeaderStyle Width="60px" />
                                            </asp:CommandField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                    <img src="<%=ConfigurationManager.AppSettings["UserMessage"]%>" alt="" />    <b>No Record found</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                    <!-- End Service Contractor Listing -->
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button Width="85px" Text="Export to Excel" CssClass="btn" ID="btnExportToExcel" runat="server"
                                        OnClick="btnExportToExcel_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td width="100%" align="left" class="bgcolorcomm">
                                    <table width="98%" border="0">
                                        <tr>
                                             <td colspan="2" align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                                <font color='red'>*</font>
                                                <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:HiddenField ID="hdnSESNo" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                               Service Engineer Code:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtSECode" runat="server" Width="170px" Text=""
                                                    MaxLength="50" /><asp:RequiredFieldValidator ID="RFVSECode" runat="server"
                                                        SetFocusOnError="true" ErrorMessage="Service Engineer code is required." ToolTip="Service Engineer Code is required." ControlToValidate="txtSECode"
                                                        ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        
                                        
                                        
                                         <tr>
                                            <td width="30%">
                                               Service Engineer Name:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtSeName" runat="server" Width="170px" Text=""
                                                    MaxLength="20" /><asp:RequiredFieldValidator ID="RFVSeName" runat="server"
                                                        SetFocusOnError="true" ErrorMessage="Service Engineer Name is required." ToolTip="Service Engineer Name is required." ControlToValidate="txtSeName"
                                                        ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td width="30%">
                                                Service Contractor: <font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlServiceContractor"  runat="server" CssClass="simpletxt1"
                                                    Width="275px" >
                                                </asp:DropDownList>
                                                 <asp:CompareValidator ID="CVSc" runat="server" ControlToValidate="ddlServiceContractor" ErrorMessage="Service contractor is required."
                                                    Operator="NotEqual" SetFocusOnError="True" ValueToCompare="Select" ValidationGroup="editt"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        
                                       
                                       
                                        <tr>
                                            <td width="30%">
                                                Mobile Number(without 0) :<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="txtboxtxt" Width="170px" />
                                                   <asp:RequiredFieldValidator ID="RFVPhoneNo" runat="server"
                                                        SetFocusOnError="true" ErrorMessage="Mobile Number is Required." 
                                                    ToolTip="Mobile Number is Required." ControlToValidate="txtPhoneNo"
                                                        ValidationGroup="editt" Display="Dynamic"></asp:RequiredFieldValidator>
                                                  <asp:RegularExpressionValidator ID="RevMobile" runat="server" 
                                                    ControlToValidate="txtPhoneNo" ValidationExpression="\d{10,12}" ValidationGroup="editt" 
                                                      SetFocusOnError="true" Display="Dynamic" 
                                                    ErrorMessage="10 Digit Mobile No. is Required." />
                                            </td>
                                        </tr>
                                       
                                       
                                       
                                        <tr>
                                            <td width="30%">
                                                Status:
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rdoStatus" RepeatDirection="Horizontal" RepeatColumns="2"
                                                    runat="server">
                                                    <asp:ListItem Value="1" Text="Active" Selected="True">
                                                    </asp:ListItem>
                                                    <asp:ListItem Value="0" Text="In-Active">
                                                    </asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="25" align="left">
                                                &nbsp;
                                            </td>
                                            <td>
                                                <!-- For button portion update -->
                                                <table>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Button Text="Add" Width="70px" CssClass="btn" ID="imgBtnAdd" runat="server"
                                                                CausesValidation="True" ValidationGroup="editt" OnClick="imgBtnAdd_Click" />
                                                            <asp:Button Text="Save" Width="70px" ID="imgBtnUpdate" CssClass="btn" runat="server"
                                                                CausesValidation="True" ValidationGroup="editt" OnClick="imgBtnUpdate_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="imgBtnCancel" Width="70px" runat="server" CausesValidation="false"
                                                                CssClass="btn" Text="Cancel" OnClick="imgBtnCancel_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <!-- For button portion update end -->
                                            </td>
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

