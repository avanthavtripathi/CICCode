<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true"
    CodeFile="ASCSpecificSpareMaster.aspx.cs" Inherits="SIMS_Admin_ASCSpecificSpareMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>

            <script language="javascript" type="text/javascript">
                function NumericOnly()
                {
                     if(document.getElementById('<%=txtAvgConsumption.ClientID %>').value != '')
                            {
                              mystring = document.getElementById('<%=txtAvgConsumption.ClientID %>').value;                    
                               if (isNaN(mystring)) 
                               {            
                                 alert("Please enter numeric only.");
                                 document.getElementById('<%=txtAvgConsumption.ClientID %>').focus();
                                 return false;    
                               }
                               
                            }         
                    
                }
            </script>

            <script language="javascript" type="text/javascript">
              
            
                function CalculateReorderTrigger()
                {
                    var LeadTime = parseInt(document.getElementById('ctl00_MainConHolder_txtLeadTime').value);
                    var AvgConsumption = document.getElementById('ctl00_MainConHolder_txtAvgConsumption').value;
                    var avgConsumption_round=Math.round(AvgConsumption*100)/100
                    //alert(avgConsumption_round);
                    var Safety = parseFloat(document.getElementById('ctl00_MainConHolder_txtSafety').value);
                    var RecommendedStock = parseInt(document.getElementById('ctl00_MainConHolder_txtRecommendedStock').value);
                    //var MinOrderQty = parseInt(document.getElementById('ctl00_MainConHolder_txtMinOrderQty').value);
                    var ReorderTrigger = ((parseFloat(LeadTime)*parseFloat(avgConsumption_round))*(1+(parseFloat(Safety)/100)));
                    //alert(ReorderTrigger);
                  
                    
                    if(isNaN(ReorderTrigger)==false)
                    {
                        document.getElementById('ctl00_MainConHolder_txtReorderTrigger').value=Math.round(ReorderTrigger);
                    }
                    else
                    {
                        document.getElementById('ctl00_MainConHolder_txtReorderTrigger').value="0";
                    }
                    var sfactor=parseFloat(1+Safety/100)
                    var OrderQuantity =parseFloat((LeadTime*avgConsumption_round*sfactor)+ (avgConsumption_round*RecommendedStock));
                    //alert(OrderQuantity);
                    
                    if(isNaN(OrderQuantity)==false)
                    {
                        document.getElementById('ctl00_MainConHolder_txtOrderQuantity').value=Math.round(OrderQuantity,0);
                        //alert(document.getElementById('ctl00_MainConHolder_txtOrderQuantity').value);
                    }
                    else
                    {
                        document.getElementById('ctl00_MainConHolder_txtOrderQuantity').value="0";
                         //alert(document.getElementById('ctl00_MainConHolder_txtOrderQuantity').value);
                    }
                }
            </script>

            <table width="100%" cellpadding="2" cellspacing="0">
                <tr>
                    <td class="headingred">
                        ASC Specific Spare Master
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
                    <td colspan="2" align="right">
                        <asp:RadioButtonList ID="rdoboth" RepeatDirection="Horizontal" RepeatColumns="3"
                            runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdoboth_SelectedIndexChanged">
                            <asp:ListItem Value="1" Text="Active" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="0" Text="In-Active"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Both"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" colspan="2">
                        <table border="0" width="100%">
                            <tr>
                                <td align="left" class="MsgTDCount">
                                    Total Number of Records :
                                    <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    Search For
                                    <asp:DropDownList ID="ddlSearch" runat="server" Width="150px" CssClass="simpletxt1">
                                        <asp:ListItem Text="Service Contactor" Value="SC_Name"></asp:ListItem>
                                        <asp:ListItem Text="Product Division" Value="Unit_Desc"></asp:ListItem>
                                        <asp:ListItem Text="Spare Description" Value="SAP_Desc"></asp:ListItem>
                                        <asp:ListItem Text="Branch Plant" Value="Branch_Plant_Desc"></asp:ListItem>
                                        <%-- <asp:ListItem Text="Location" Value="Loc_Name"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                    With
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="txtboxtxt" Width="100px" Text=""></asp:TextBox>
                                    <asp:Button Text="Go" Width="25px" CssClass="btn" ID="imgBtnGo" runat="server" CausesValidation="False"
                                        ValidationGroup="editt" OnClick="imgBtnGo_Click" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0">
                            <tr>
                                <td class="bgcolorcomm">
                                    <asp:GridView ID="gvComm" runat="server" AllowPaging="True" AllowSorting="True" AlternatingRowStyle-CssClass="fieldName"
                                        AutoGenerateColumns="False" DataKeyNames="ASC_Spec_Spare_Id" GridGroups="both"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" HorizontalAlign="Left" OnPageIndexChanging="gvComm_PageIndexChanging"
                                        OnSelectedIndexChanging="gvComm_SelectedIndexChanging" PageSize="10" RowStyle-CssClass="gridbgcolor"
                                        Width="100%" EnableSortingAndPagingCallbacks="True" OnSorting="gvComm_Sorting">
                                        <Columns>
                                            <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                <HeaderStyle />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SC_Name" SortExpression="SC_Name" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Service Contractor" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Branch_Plant_Desc" SortExpression="Branch_Plant_Desc"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Plant" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Product_Division_Name" SortExpression="Product_Division_Name"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Product Division" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SAP_Desc" SortExpression="SAP_Desc" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Spare Description" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Lead_Time" SortExpression="Lead_Time" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Lead Time" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AVGConsumption_Per_Day" SortExpression="AVGConsumption_Per_Day"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="AVG Cons./Day" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Safety_Percentage" SortExpression="Safety_Percentage"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Safety %" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Reorder_Trigger" SortExpression="Reorder_Trigger" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Reorder Trigger" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Recommended_Stock" SortExpression="Recommended_Stock"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Recom. Stock" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Order_Quantity" SortExpression="Order_Quantity" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Order Qty." ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField Visible="false" DataField="Min_Order_Quantity" SortExpression="Min_Order_Quantity"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Min. Order Qty." ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Active_Flag" SortExpression="Active_Flag" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Status" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:CommandField HeaderStyle-Width="60px" HeaderText="Edit" ShowSelectButton="True">
                                                <HeaderStyle Width="60px" />
                                            </asp:CommandField>
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
                                    <asp:HiddenField ID="hdnASC_Spec_Spare_Id" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td width="100%" align="left" class="bgcolorcomm">
                                    <table width="98%" border="0">
                                        <tr>
                                            <td colspan="4" align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                                <font color='red'>*</font>
                                                <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                            </td>
                                        </tr>
                                        <tr style="display: none;">
                                            <td width="25%">
                                                Location:<font color='red'>*</font>
                                                <asp:DropDownList ID="ddlLocation" runat="server" CssClass="simpletxt1" Width="175px"
                                                    ValidationGroup="editt">
                                                </asp:DropDownList>
                                                <%--<br />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlLocation"
                                                    ErrorMessage="Location is required." InitialValue="Select" SetFocusOnError="true"
                                                    ValidationGroup="editt" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="25%">
                                                Service Contactor:<font color='red'>*</font>
                                            </td>
                                            <td width="25%">
                                                <asp:DropDownList ID="ddlASCCode" runat="server" CssClass="simpletxt1" Width="175px"
                                                    ValidationGroup="editt" AutoPostBack="True" OnSelectedIndexChanged="ddlASCCode_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <br />
                                                <asp:RequiredFieldValidator ID="RFRegionDesc" runat="server" ControlToValidate="ddlASCCode"
                                                    ErrorMessage="Service Contactor is required." InitialValue="Select" SetFocusOnError="true"
                                                    ValidationGroup="editt" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                            <td width="25%">
                                                Branch Plant:
                                            </td>
                                            <td width="25%">
                                                <asp:Label ID="lblBranchPlant" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Product Division:<font color='red'>*</font>
                                            </td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="ddlProductDivision" runat="server" CssClass="simpletxt1" ValidationGroup="editt"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlProductDivision_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <br />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlProductDivision"
                                                    ErrorMessage="Product Division is required." InitialValue="Select" SetFocusOnError="true"
                                                    ValidationGroup="editt" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                        <td>
                                          
                                                Search Spares:    
                                        </td>
                                        <td colspan="4">
                                        <asp:TextBox ID="txtFindSpare" ValidationGroup="ProductRef" CssClass="txtboxtxt" runat="server"
                                                    Width="130" CausesValidation="True"></asp:TextBox>
                                                <asp:Button ID="btnGoSpare" runat="server"  ValidationGroup="ProductRef" Width="20px" Text="Go" CssClass="btn" OnClick="btnGoSpare_Click" />
                                      
                                        </td>
                                        </tr>
                                        <tr>
                                            <td>
                                        
                                                Spare:<font color='red'>*</font>
                                            </td>
                                            <td colspan="4">
                                                <asp:DropDownList ID="ddlSpare" runat="server" CssClass="simpletxt1" 
                                                    ValidationGroup="editt" >
                                                </asp:DropDownList>
                                                <br />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSpare"
                                                    ErrorMessage="Spare is required." InitialValue="Select" SetFocusOnError="true"
                                                    ValidationGroup="editt" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Lead Time in days:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" onblur="javascript:CalculateReorderTrigger();"
                                                    ID="txtLeadTime" MaxLength="5" runat="server" Width="170px" Text="" />
                                                <br />
                                                <asp:RequiredFieldValidator Display="Dynamic" ID="RFStateDesc" runat="server" SetFocusOnError="true"
                                                    ErrorMessage="Lead Time is required." ControlToValidate="txtLeadTime" ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator EnableClientScript="true" Display="Dynamic" ControlToValidate="txtLeadTime"
                                                    ID="RegularExpressionValidator1" ValidationGroup="editt" runat="server" ErrorMessage="Numeric Value is required in Lead Time."
                                                    ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
                                            </td>
                                            <td>
                                                Avg Consumption/Day:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" onblur="javascript:CalculateReorderTrigger();"
                                                    ID="txtAvgConsumption" runat="server" Width="170px" Text="" MaxLength="5" />
                                                <br />
                                                <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator3" runat="server"
                                                    SetFocusOnError="true" ErrorMessage="Avg Consumption/Day is required." ControlToValidate="txtAvgConsumption"
                                                    ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                <%-- <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtAvgConsumption"
                                                    ID="RegularExpressionValidator2" ValidationGroup="editt" runat="server" ErrorMessage="Numeric Value is required in Avg Consumption/Day."
                                                    ValidationExpression="^\d*$"></asp:RegularExpressionValidator>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Safety %:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" onblur="javascript:CalculateReorderTrigger();"
                                                    MaxLength="5" ID="txtSafety" runat="server" Width="170px" Text="" />
                                                <br />
                                                <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator4" runat="server"
                                                    SetFocusOnError="true" ErrorMessage="Safety % is required." ControlToValidate="txtSafety"
                                                    ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                <asp:RangeValidator Display="Dynamic" ID="RangeValidator3" ControlToValidate="txtSafety"
                                                    MinimumValue="1" MaximumValue="100" Type="Double" ValidationGroup="editt" runat="server"
                                                    ErrorMessage="Safety % should be less than 100."></asp:RangeValidator>
                                            </td>
                                            <td>
                                                Reorder Trigger:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtReorderTrigger" runat="server" Width="170px"
                                                    MaxLength="15" Text="" Enabled="false" />
                                                <br />
                                                <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator5" runat="server"
                                                    SetFocusOnError="true" ErrorMessage="Reorder Trigger is required." ControlToValidate="txtReorderTrigger"
                                                    ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtReorderTrigger"
                                                    ID="RegularExpressionValidator3" ValidationGroup="editt" runat="server" ErrorMessage="Numeric Value is required in Reorder Trigger."
                                                    ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Recommended stock in days above Trigger Level:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" onblur="javascript:CalculateReorderTrigger();"
                                                    ID="txtRecommendedStock" runat="server" Width="170px" Text="" MaxLength="5" />
                                                <br />
                                                <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator6" runat="server"
                                                    SetFocusOnError="true" ErrorMessage="Recommended stock is required." ControlToValidate="txtRecommendedStock"
                                                    ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtRecommendedStock"
                                                    ID="RegularExpressionValidator4" ValidationGroup="editt" runat="server" ErrorMessage="Numeric Value is required in Recommended Stock."
                                                    ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
                                            </td>
                                            <td>
                                                Order Quantity:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtOrderQuantity" runat="server" Width="170px"
                                                    Text="" Enabled="false" MaxLength="15" />
                                                <br />
                                                <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator7" runat="server"
                                                    SetFocusOnError="true" ErrorMessage="Order Quantity is required." ControlToValidate="txtOrderQuantity"
                                                    ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtOrderQuantity"
                                                    ID="RegularExpressionValidator5" ValidationGroup="editt" runat="server" ErrorMessage="Numeric Value is required in Order Quantity."
                                                    ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                  <td colspan="4">&nbsp;</td>
                                </tr>--%>
                                        <tr>
                                            <td>
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
                                            <td id="tdMOQ" runat="server" visible="false">
                                                Min. Order Quantity:<font color='red'>*</font>
                                            </td>
                                            <td id="tdMOQ1" runat="server" visible="false">
                                                <asp:TextBox CssClass="txtboxtxt" MaxLength="5" ID="txtMinOrderQty" runat="server"
                                                    Width="170px" Text="" />
                                                <br />
                                                <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator9" runat="server"
                                                    SetFocusOnError="true" ErrorMessage="Min. Order Quantity is required." ControlToValidate="txtMinOrderQty"
                                                    ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtMinOrderQty"
                                                    ID="RegularExpressionValidator6" ValidationGroup="editt" runat="server" ErrorMessage="Numeric Value is required in Min. Order Quantity."
                                                    ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="25" align="left">
                                                &nbsp;
                                            </td>
                                            <td height="25" align="left">
                                                &nbsp;
                                            </td>
                                            <td colspan="3">
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
