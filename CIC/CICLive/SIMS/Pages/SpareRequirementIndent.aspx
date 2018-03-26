<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true"
    CodeFile="SpareRequirementIndent.aspx.cs" Inherits="SIMS_Pages_SpareRequirementIndent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>

            <script language="javascript" type="text/javascript">
              
            
                function CalculateAmount()
                {
                    var Proposedqty = parseInt(document.getElementById('ctl00_MainConHolder_txtProposedqty').value);
                    var Rate = parseFloat(document.getElementById('ctl00_MainConHolder_lblRate').innerHTML);
                    var Discount = parseFloat(document.getElementById('ctl00_MainConHolder_lblDiscount').innerHTML);
                    var Amount=parseFloat(Rate * (1 - (Discount / 100)) * Proposedqty);
                    document.getElementById('ctl00_MainConHolder_lblValue').innerHTML=Amount.toFixed(2); 
                }
            </script>

            <table width="100%" cellpadding="2" cellspacing="0">
                <tr>
                    <td class="headingred">
                        Spare Requirement Advice Screen
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
                                            <td colspan="2">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="right">
                                                Date:
                                                <asp:Label ID="lbldate" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="10%">
                                                ASC Name:
                                            </td>
                                            <td>
                                                <asp:Label ID="lblASCName" runat="server"></asp:Label>
                                                <asp:HiddenField ID="hdnASC_Code" runat="server" />
                                            </td>
                                        </tr>
                                        <%--<tr>
                                    <td>To CG Branch:</td>
                                    <td><asp:Label ID="lblCGBranch" runat="server"></asp:Label></td>   
                                </tr>--%>
                                        <tr>
                                            <td>
                                                Division:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlProductDivision" runat="server" CssClass="simpletxt1" Width="175px"
                                                    ValidationGroup="editt" AutoPostBack="True" OnSelectedIndexChanged="ddlProductDivision_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlProductDivision"
                                                    ErrorMessage="Product Division is required." InitialValue="Select" SetFocusOnError="true"
                                                    ValidationGroup="editt" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Search Spares:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFindSpare" ValidationGroup="ProductRef" CssClass="txtboxtxt"
                                                    runat="server" Width="140" CausesValidation="True"></asp:TextBox>
                                                <asp:Button ID="btnGoSpare" runat="server" ValidationGroup="ProductRef" Width="20px"
                                                    Text="Go" CssClass="btn" OnClick="btnGoSpare_Click" />
                                            </td>
                                        </tr>
                                        <tr id="trDraft" runat="server" visible="false">
                                            <td colspan="2">
                                                Drafted Orders:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="gvDrafted" runat="server" AllowPaging="false" AlternatingRowStyle-CssClass="fieldName"
                                                    AutoGenerateColumns="False" DataKeyNames="Spare_Requirement_Draft_Id" GridGroups="both"
                                                    HeaderStyle-CssClass="fieldNamewithbgcolor" HorizontalAlign="Left" RowStyle-CssClass="gridbgcolor"
                                                    Width="100%">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Draft No" SortExpression="Draft_No">
                                                            <ItemTemplate>
                                                                <a href='SpareRequirementIndentConfirm.aspx?draftno=<%# DataBinder.Eval(Container.DataItem,"Draft_No") %>'>
                                                                    <%# DataBinder.Eval(Container.DataItem, "Draft_No")%></a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Draft_Date" SortExpression="Draft_Date" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Draft Date" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="200px">
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="left" class="MsgTDCount">
                                                Total Number of Advices :
                                                <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                           <%-- Commented on 26 dec 12 On CG Request by Bhawesh
                                            AllowPaging="True" PageSize="10" OnPageIndexChanging="gvComm_PageIndexChanging" --%>
                                                <asp:GridView ID="gvComm" runat="server"  AlternatingRowStyle-CssClass="fieldName"
                                                    AutoGenerateColumns="False" DataKeyNames="Proposal_Id" GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor"
                                                    HorizontalAlign="Left" 
                                                    RowStyle-CssClass="gridbgcolor" Width="100%" OnRowDataBound="gvComm_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Division" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProductdivision" runat="server" Text='<%#Eval("ProductDivision") %>'></asp:Label>
                                                                <asp:HiddenField ID="hndProductDivision_Id" runat="server" Value='<%#Eval("ProductDivision_Id") %>' />
                                                                <asp:HiddenField ID="hdnsparepropid" runat="server" Value='<%#Eval("Proposal_Id") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Transaction_No" SortExpression="Transaction_No" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Transaction No" ItemStyle-HorizontalAlign="Left">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Spare_Name" SortExpression="Spare_Name" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Proposed Spare" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="200px">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CurrentStock" SortExpression="CurrentStock" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Current stock" ItemStyle-HorizontalAlign="Left">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OrderedPendingQty" SortExpression="OrderedPendingQty"
                                                            HeaderStyle-HorizontalAlign="Left" HeaderText="Qty Pending to be Received" ItemStyle-HorizontalAlign="Left">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Proposed_Qty" SortExpression="Proposed_Qty" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Proposed qty" ItemStyle-HorizontalAlign="Left">
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Ordered Qty">
                                                            <ItemTemplate>
                                                                <asp:TextBox Width="50px" ID="txtOrderedQty" CssClass="txtboxtxt" Text='<%# DataBinder.Eval(Container.DataItem,"Ordered_Qty") %>'
                                                                    runat="server" AutoPostBack="false" OnTextChanged="txtOrderedQty_TextChanged"></asp:TextBox>
                                                                <asp:RequiredFieldValidator Display="Dynamic" ID="RFStateDesc" runat="server" SetFocusOnError="true"
                                                                    ErrorMessage="Ordered qty is required." ControlToValidate="txtOrderedQty" ValidationGroup="confirm"></asp:RequiredFieldValidator>
                                                                <asp:RangeValidator Display="Dynamic" ID="RangeValidator3" SetFocusOnError="true"
                                                                    ControlToValidate="txtOrderedQty" MinimumValue="1" MaximumValue="64550" Type="Integer"
                                                                    ValidationGroup="confirm" runat="server" ErrorMessage="Ordered Proposed qty is required."></asp:RangeValidator>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Rate" SortExpression="Rate" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Rate" ItemStyle-HorizontalAlign="Left">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Discount" SortExpression="Discount" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Discount" ItemStyle-HorizontalAlign="Left">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Value" SortExpression="Value" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Value" ItemStyle-HorizontalAlign="Left">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Created_By" SortExpression="Created_By" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Proposed By" ItemStyle-HorizontalAlign="Left">
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Confirm">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="ChkConfirm" runat="server" AutoPostBack="true" OnCheckedChanged="ChkConfirm_CheckedChanged" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Proposal_Id" SortExpression="Proposal_Id" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Proposal ID" ItemStyle-HorizontalAlign="Left">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Spare_Id" SortExpression="Spare_Id" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Spare Id" ItemStyle-HorizontalAlign="Left">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Transaction_No" SortExpression="Transaction_No" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Transaction No" ItemStyle-HorizontalAlign="Left">
                                                        </asp:BoundField>
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
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Search Spares:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSearchSpare" ValidationGroup="ProductRef" CssClass="txtboxtxt"
                                                    runat="server" Width="140" CausesValidation="True"></asp:TextBox>
                                                <asp:Button ID="btnSpareSearch" runat="server" ValidationGroup="ProductRef" Width="20px"
                                                    Text="Go" CssClass="btn" OnClick="btnSpareSearch_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                            <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                            <table width="100%">
                                                    <tr>
                                                        <td bgcolor="#CCCCCC">
                                                            <b>Division:</b>
                                                        </td>
                                                        <td bgcolor="#CCCCCC">
                                                            <b>Spare:</b>
                                                        </td>
                                                        <td bgcolor="#CCCCCC">
                                                            <b>Current stock:</b>
                                                        </td>
                                                        <td bgcolor="#CCCCCC">
                                                            <b>Qty Pending to be Received:</b>
                                                        </td>
                                                        <td bgcolor="#CCCCCC">
                                                            <b>Proposed qty:</b>
                                                        </td>
                                                        <td bgcolor="#CCCCCC">
                                                            <b>Rate</b>
                                                        </td>
                                                        <td bgcolor="#CCCCCC">
                                                            <b>Discount</b>
                                                        </td>
                                                        <td bgcolor="#CCCCCC">
                                                            <b>Value</b>
                                                        </td>
                                                        <td bgcolor="#CCCCCC" style="display:none"> <%--display none by bhawesh 16 oct 12--%>
                                                        <b>Complaint No.</b>
                                                        </td>
                                                        <td bgcolor="#CCCCCC">
                                                            <b>&nbsp;</b>
                                                        </td>
                                                        <td bgcolor="#CCCCCC">
                                                            <b>Transaction No.</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:DropDownList ID="ddlSpareDivision" runat="server" CssClass="simpletxt1" ValidationGroup="editt"
                                                                Width="130px" AutoPostBack="True" OnSelectedIndexChanged="ddlSpareDivision_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <br />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSpareDivision"
                                                                ErrorMessage="Division is required." InitialValue="Select" SetFocusOnError="true"
                                                                ValidationGroup="editt" Display="Dynamic"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:DropDownList ID="ddlSpare" runat="server" CssClass="simpletxt1" ValidationGroup="editt"
                                                                Width="130px" AutoPostBack="True" OnSelectedIndexChanged="ddlSpare_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <br />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSpare"
                                                                ErrorMessage="Spare is required." InitialValue="Select" SetFocusOnError="true"
                                                                ValidationGroup="editt" Display="Dynamic"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:Label ID="lblCurrentStock" runat="server"></asp:Label>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:Label ID="lblQtyPendingToBeReceived" runat="server"></asp:Label>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:TextBox ID="txtProposedqty" runat="server" onblur="javascript:CalculateAmount();"
                                                                CssClass="txtboxtxt" OnTextChanged="txtProposedqty_TextChanged"></asp:TextBox>
                                                            <br />
                                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RFStateDesc" runat="server" SetFocusOnError="true"
                                                                ErrorMessage="Proposed qty is required." ControlToValidate="txtProposedqty" ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                            <%--<asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtProposedqty" ID="RegularExpressionValidator1" ValidationGroup="editt" runat="server" 
                                                            ErrorMessage="Proper Proposed qty is required."  ValidationExpression="^\d*$"></asp:RegularExpressionValidator>--%>
                                                            <asp:RangeValidator Display="Dynamic" ID="RangeValidator3" ControlToValidate="txtProposedqty"
                                                                MinimumValue="1" MaximumValue="64550" Type="Integer" ValidationGroup="editt"
                                                                runat="server" ErrorMessage="Proper Proposed qty is required."></asp:RangeValidator>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:Label ID="lblRate" runat="server"></asp:Label>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:Label ID="lblDiscount" runat="server"></asp:Label>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:Label ID="lblValue" runat="server"></asp:Label>
                                                        </td>
                                                        <td valign="top" style="display:none" >
                                                     <%-- BP 23 Jan 13 ; New screen should be used if spare r required to complete a complaint.--%>
                                                     
                                                       <asp:DropDownList ID="ddlComplaintNo" runat="server" AutoPostBack="true" CssClass="simpletxt1" Width="130px">
                                                        </asp:DropDownList>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:Button Text="Add" Width="70px" CssClass="btn" ID="imgBtnAdd" runat="server"
                                                                CausesValidation="True" ValidationGroup="editt" OnClick="imgBtnAdd_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblTransactionNo" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <!-- For button portion update -->
                                                <table>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Button Text="Confirm" Width="70px" CssClass="btn" ID="imgBtnConfirm" runat="server"
                                                                CausesValidation="True" ValidationGroup="confirm" OnClick="imgBtnConfirm_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="imgBtnCancel" Width="70px" runat="server" CausesValidation="true"
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
