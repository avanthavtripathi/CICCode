<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true"
    CodeFile="BasicSpareMaster.aspx.cs" Inherits="SIMS_Admin_BasicSpareMaster" %>

<asp:Content ID="ContentBasicSpare" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script language="javascript" type="text/javascript">
function getNumeric_only(strvalue)
 {
   if(!(event.keyCode==46||event.keyCode==48||event.keyCode==49||event.keyCode==50||event.keyCode==51||event.keyCode==52||event.keyCode==53||event.keyCode==54||event.keyCode==55||event.keyCode==56||event.keyCode==57))
   		{	
   			event.returnValue=false;	
   			alert("Please enter numbers only.");
   		
   		}
    
 }
 function getnumeric_below100(strvalue)
 {
 if(!(event.keyCode==46||event.keyCode==48||event.keyCode==49||event.keyCode==50||event.keyCode==51||event.keyCode==52||event.keyCode==53||event.keyCode==54||event.keyCode==55||event.keyCode==56||event.keyCode==57))
   		{	
   			event.returnValue=false;	
   			alert("Please enter numbers only.");
   		
   		}
   		if(strvalue>=100)
   		{
   		alert("Please enter 100% or below 100%")
   		}
   		
 }
    </script>

    <script language="javascript" type="text/javascript">
        function ShowDIV(ImID)
        {  
                var obj = document.getElementById('<%= this.IMGDIV.ClientID %>');
                 //alert(obj);
                var im = document.getElementById('<%= this.imIMG.ClientID %>');
               //alert(im);
                if ( obj != null )
                {
                    obj.style.display = "block"; 
                    im.src = "../spareimage/"+ImID;
                }
        }
        function HideDIV()
        {    
              var obj = document.getElementById('<%= this.IMGDIV.ClientID %>');
                if ( obj != null )
                {
                  obj.style.display = "none"; 
                }      
        }
    </script>

    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td class="headingred">
                            Spare Master
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
                            <a href="../BulkUpload/bulkUploadBasicSpareMaster.xls" target="_blank">Spare Master Bulk Upload</a>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right" style="padding-right: 10px">
                            <%--FOR FILTERING RECORD ON THE BASIS OF ACTIVE AND INACTIVE--%>
                            <asp:RadioButtonList ID="rdoboth" RepeatDirection="Horizontal" RepeatColumns="3"
                                runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdoboth_SelectedIndexChanged">
                                <asp:ListItem Value="1" Text="Active" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="0" Text="In-Active"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Both"></asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:Button Visible="false" Text="Go" Width="25px" CssClass="btn" ID="imgBtnGo" runat="server"
                                CausesValidation="False" ValidationGroup="editt1" OnClick="imgBtnGo_Click" />
                            <%--FILTERING RECORD ON THE BASIS OF ACTIVE AND INACTIVE END--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding: 10px" align="center">
                            <table border="0" width="100%">
                                <tr>
                                    <td align="left" class="MsgTDCount">
                                        Total Number of Records :
                                        <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server">
                                        </asp:Label>
                                    </td>
                                    <td align="right">
                                        Search For
                                        <asp:DropDownList ID="ddlSearch" runat="server" Width="130px" CssClass="simpletxt1">
                                            <asp:ListItem Text="Division" Value="Unit_desc"></asp:ListItem>
                                            <asp:ListItem Text="Spare Code" Value="SAP_Code"></asp:ListItem>                                            
                                            <asp:ListItem Text="Spare Desciption" Value="SAP_Desc"></asp:ListItem>
                                            <asp:ListItem Text="Spare UOM" Value="SAP_UOM"></asp:ListItem>
                                            <%--<asp:ListItem Text="SAP ListPrice" Value="SAP_ListPrice"></asp:ListItem>--%>
                                            <asp:ListItem Text="Spare MatGroup" Value="SAP_MatGroup"></asp:ListItem>
                                            <asp:ListItem Text="Spare MatType" Value="SAP_MatType"></asp:ListItem>
                                            <asp:ListItem Text="Spare Obselete" Value="Spare_Obselete"></asp:ListItem>
                                            <asp:ListItem Text="Spare Mov Type" Value="Spare_Mov_Type"></asp:ListItem>
                                            <asp:ListItem Text="Spare Value" Value="Spare_Value"></asp:ListItem>
                                            <asp:ListItem Text="Essential Spare" Value="Essential_Spare"></asp:ListItem>
                                            <asp:ListItem Text="Spare Type" Value="Spare_Type"></asp:ListItem>
                                            <%--<asp:ListItem Text="Spare MOQ" Value="Spare_MOQ"></asp:ListItem>--%>
                                            <asp:ListItem Text="Spare Disposal" Value="Spare_Disposal_Flag"></asp:ListItem>
                                            <asp:ListItem Text="Spare Action" Value="Spare_Action_By_CG Branch"></asp:ListItem>
                                        </asp:DropDownList>
                                        With
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="txtboxtxt" Width="100px" Text=""></asp:TextBox>
                                        <asp:Button Text="Go" Width="25px" CssClass="btn" ID="Button1" runat="server" CausesValidation="False"
                                            ValidationGroup="editt" OnClick="imgBtnGo_Click" />
                                    </td>
                                </tr>
                            </table>
                            
                            <table width="100%" border="0">
                                <tr>
                                    <td>
                                        <!-- Batch Listing -->
                                        <div id="IMGDIV" runat="server" style="position: absolute; left:375px; top:375px; display: none; z-index: auto;
                                visibility: visible; vertical-align: middle; width:250;">
                                <table width="100%" align="center">
                                    <tr>
                                        <td height="100%">
                                            <table style="border: 1px solid #000000; background-color:#ffffff" cellpadding="5">
                                                <tr>
                                                    <td>
                                                        <img id="imIMG" src="" runat="server" alt="" width="250" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">                                                        
                                                        <input type="button" style="vertical-align: text-top" class="btn" value="Close" onclick="return HideDIV();" />
                                                    </td>
                                                    
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                                        <asp:GridView RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                            HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="True"
                                            AllowSorting="True" DataKeyNames="Spare_Id" AutoGenerateColumns="False" ID="gvComm"
                                            runat="server" OnPageIndexChanging="gvComm_PageIndexChanging" Width="100%" OnSelectedIndexChanging="gvComm_SelectedIndexChanging"
                                            HorizontalAlign="Left" OnSorting="gvComm_Sorting" OnRowCommand="gvComm_RowCommand"
                                            OnRowDataBound="gvComm_RowDataBound">
                                            <RowStyle CssClass="gridbgcolor" />
                                            <Columns>
                                                <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                    <HeaderStyle Width="40px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Division" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Division" SortExpression="Division">
                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField Visible="false" DataField="SAP_Code" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign ="Left" HeaderText="Spare Code" SortExpression="SAP_Code">
                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SAP_Desc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Spare Desciption" SortExpression="SAP_Desc">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SAP_UOM" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Unit of Measurement" SortExpression="SAP_Desc">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SAP_ListPrice" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="List Price" SortExpression="SAP_ListPrice">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Discount" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Discount %" SortExpression="Discount">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SAP_MatGroup" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Material Group" SortExpression="SAP_MatGroup">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SAP_MatType" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Material Type" SortExpression="SAP_MatType">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>                                                
                                                <asp:BoundField DataField="Spare_Mov_Type" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Spare Moving Type" SortExpression="Spare_Mov_Type">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Spare_Value" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Spare Value" SortExpression="Spare_Value">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>                                                
                                                <asp:BoundField DataField="Spare_Type" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Spare Type" SortExpression="Spare_Type">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Spare_MOQ" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Minimum Order Quantity" SortExpression="Spare_MOQ">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>                                                
                                                <asp:TemplateField HeaderText="Photograph" SortExpression="FileName">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnSpare_Id" runat="server" Value='<%# Bind("Spare_Id") %>' />                                                        
                                                        <a id="imgSpare" href="javascript:void(ShowDIV('<%# Eval("FileName")%>'));">
                                                            <%# Eval("FileView")%></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Spare_Disposal_Flag" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Spare disposal/ Destruction Type" SortExpression="Spare_Disposal_Flag">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Spare_Action_By_CG" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Spare Action by CG Branch" SortExpression="Spare_Action_By_CG">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Essential_Spare" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Essential Spare" SortExpression="Essential_Spare">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Spare_Obselete" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Spare Obselete" SortExpression="Spare_Obselete">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Active_Flag" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Status" SortExpression="Active_Flag">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:CommandField ShowSelectButton="True" HeaderStyle-Width="60px" HeaderText="Edit">
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
                                            <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                            <AlternatingRowStyle CssClass="fieldName" />
                                        </asp:GridView>
                                        <!-- End Batch Listing -->
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
                                                <td colspan="2" align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                                    <font color='red'></font>
                                                    <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:HiddenField ID="hdnSpareId" runat="server" />
                                                </td>
                                            </tr>
                                             <tr>
                                            <td width="10%">
                                                Division:<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlDivision" runat="server" CssClass="simpletxt1" 
                                                    Width="175px">
                                                </asp:DropDownList>
                                                &nbsp;<asp:RequiredFieldValidator InitialValue="0" ID="RequiredFieldValidator13" runat="server"
                                                    SetFocusOnError="true" ErrorMessage="Division is required." ControlToValidate="ddlDivision"
                                                    ValidationGroup="editt" ToolTip="Division is required."></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                            <tr>
                                                <td>
                                                    Spare Code:<font color='red'>*</font>
                                                </td>
                                                <td>
                                                    <asp:TextBox CssClass="txtboxtxt" ID="txtSpareCode" runat="server" MaxLength="18"
                                                        Width="170px" Text="" />&nbsp;<asp:RequiredFieldValidator ID="reqtxtSpareCode" runat="server"
                                                            SetFocusOnError="true" ErrorMessage="Spare Code is required." ControlToValidate="txtSpareCode"
                                                            ToolTip="Spare Code is required." ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Spare Description:<font color='red'>*</font>
                                                </td>
                                                <td>
                                                    <asp:TextBox CssClass="txtboxtxt" ID="txtSpareDesc" MaxLength="200" runat="server"
                                                        Width="170px" Text="" />&nbsp;<asp:RequiredFieldValidator SetFocusOnError="true"
                                                            ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSpareDesc"
                                                            ErrorMessage="Spare Description is required." Display="Dynamic" ToolTip="Spare Description is required."
                                                            ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Unit of Measurement:<font color='red'>*</font>
                                                </td>
                                                <td>
                                                     <asp:DropDownList ID="ddlUOM" runat="server" CssClass="simpletxt1" 
                                                    Width="175px">
                                                </asp:DropDownList>
                                                    <%--<asp:TextBox CssClass="txtboxtxt" ID="txtUOM" runat="server" MaxLength="20" Width="170px"
                                                        Text="" />&nbsp;<asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator2"
                                                            runat="server" ControlToValidate="txtUOM" ErrorMessage="Unit of measurement is required."
                                                            Display="Dynamic" ToolTip="Unit of measurement is required." ValidationGroup="editt"></asp:RequiredFieldValidator>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    List Price (in INR):<font color='red'>*</font>
                                                </td>
                                                <td>
                                                    <asp:TextBox CssClass="txtboxtxt" ID="txtListprice" MaxLength="10" runat="server"
                                                        Width="170px" Text="" onkeypress="getNumeric_only('txtListprice')" />&nbsp;<asp:RequiredFieldValidator
                                                            SetFocusOnError="true" ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtListprice"
                                                            ErrorMessage="List price is required." Display="Dynamic" ToolTip="List price is required."
                                                            ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Discount(in %)<font color='red'>*</font>
                                                </td>
                                                <td>
                                                    <asp:TextBox CssClass="txtboxtxt" ID="txtDiscount" MaxLength="10" runat="server"
                                                        Width="170px" Text="" onkeypress="getNumeric_only('txtDiscount')" />&nbsp;<asp:RequiredFieldValidator
                                                            SetFocusOnError="true" ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtDiscount"
                                                            ErrorMessage="Discount is required." Display="Dynamic" ToolTip="Discount is required."
                                                            ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Material Group:<font color='red'>*</font>
                                                </td>
                                                <td style="width: 31%">
                                                    <asp:TextBox ID="txtMaterialgroup" runat="server" MaxLength="50" CssClass="txtboxtxt"
                                                        Text="" Width="170px" />&nbsp;<asp:RequiredFieldValidator SetFocusOnError="true"
                                                            ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMaterialgroup"
                                                            ErrorMessage="Material group is required." Display="Dynamic" ToolTip="Material group is required."
                                                            ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Material Type:<font color='red'>*</font>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlMaterialType" runat="server" CssClass="simpletxt1" Width="175px">
                                                        <asp:ListItem Value="0" Text="Select">Select</asp:ListItem>
                                                        <asp:ListItem Value="FERT" Text="FERT" Selected="True">FERT</asp:ListItem>
                                                        <asp:ListItem Value="HAWA" Text="HAWA">HAWA</asp:ListItem>
                                                        <asp:ListItem Value="HALB" Text="HALB">HALB</asp:ListItem>
                                                        <asp:ListItem Value="ROH" Text="ROH">ROH</asp:ListItem>
                                                    </asp:DropDownList>
                                                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" SetFocusOnError="true"
                                                        ErrorMessage="Material Type is required." InitialValue="0" ControlToValidate="ddlMaterialType"
                                                        ValidationGroup="editt" ToolTip="Material Type is required."></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Spare Moving Type:<font color='red'>*</font>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlSpareMoving" runat="server" CssClass="simpletxt1" Width="175px">
                                                        <asp:ListItem Value="0" Text="Select">Select</asp:ListItem>
                                                        <asp:ListItem Value="S">Slow Moving</asp:ListItem>
                                                        <asp:ListItem Value="F" Selected="True">Fast moving</asp:ListItem>
                                                    </asp:DropDownList>
                                                    &nbsp;<asp:RequiredFieldValidator InitialValue="0" ID="RequiredFieldValidator6" runat="server"
                                                        SetFocusOnError="true" ErrorMessage="Spare Moving Type is required." ControlToValidate="ddlSpareMoving"
                                                        ValidationGroup="editt" ToolTip="Spare Moving Type is required."></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Spare Value:<font color='red'>*</font>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlSpareValue" runat="server" CssClass="simpletxt1" Width="175px">
                                                        <asp:ListItem Value="0" Text="Select">Select</asp:ListItem>
                                                        <asp:ListItem Value="H">High Value</asp:ListItem>
                                                        <asp:ListItem Value="L">Low Value</asp:ListItem>
                                                    </asp:DropDownList>
                                                    &nbsp;<asp:RequiredFieldValidator InitialValue="0" ID="RequiredFieldValidator7" runat="server"
                                                        SetFocusOnError="true" ErrorMessage="Spare Value is required." ControlToValidate="ddlSpareValue"
                                                        ValidationGroup="editt" ToolTip="Spare Value is required."></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Spare Type:<font color='red'>*</font>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlSpareType" runat="server" CssClass="simpletxt1" Width="175px">
                                                        <asp:ListItem Value="0" Text="Select">Select</asp:ListItem>
                                                        <asp:ListItem Value="R" Selected="True">Regular spare</asp:ListItem>
                                                        <asp:ListItem Value="M">Made to order spare</asp:ListItem>
                                                    </asp:DropDownList>
                                                    &nbsp;<asp:RequiredFieldValidator InitialValue="0" ID="RequiredFieldValidator9" runat="server"
                                                        SetFocusOnError="true" ErrorMessage="Spare Type is required." ControlToValidate="ddlSpareType"
                                                        ValidationGroup="editt" ToolTip="Spare Type is required."></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Minimum Order Quantity:<font color='red'>*</font>
                                                </td>
                                                <td>
                                                    <asp:TextBox CssClass="txtboxtxt" ID="txtMinimumOrder" runat="server" Width="170px"
                                                        Text="" onkeypress="getNumeric_only('txtMinimumOrder')" />&nbsp;<asp:RequiredFieldValidator
                                                            SetFocusOnError="true" ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtMinimumOrder"
                                                            ErrorMessage="Minimum order quantity is required." Display="Dynamic" ToolTip="Minimum order quantity is required."
                                                            ValidationGroup="editt"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Spare Photograph:
                                                </td>
                                                <td>
                                                    <input type="file" class="btn" id="fpSparephoto" runat="server" onkeydown="if(event.keyCode==9){return true;}else{return false;}" />&nbsp;
                                                    <asp:Button ID="btnFileUpload" runat="server" ValidationGroup="editt" CssClass="btn"
                                                        CausesValidation="false" Text="Upload" OnClick="btnFileUpload_Click" />&nbsp;
                                                    <asp:RegularExpressionValidator Display="Dynamic" ID="RegfpSparephoto" runat="server"
                                                        Text="Invalid File Format." ValidationGroup="editt" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.gif|.jpg|.jpeg|.bmp|.GIF|.JPG|.JPEG|.BMP)$"
                                                        ControlToValidate="fpSparephoto"></asp:RegularExpressionValidator>(.jpg, .jpeg, .bmp, .gif; Max Size 50kB)
                                                    <asp:Label runat="server" ID="lblImageMessage" ForeColor="Red" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:GridView ID="gvFiles" runat="server" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                                        HeaderStyle-CssClass="fieldNamewithbgcolor" AutoGenerateColumns="False" BorderStyle="None"
                                                        GridLines="none" Width="105%" OnPageIndexChanging="gvFiles_PageIndexChanging"
                                                        OnRowDeleting="gvFiles_RowDeleting">
                                                        <RowStyle CssClass="gridbgcolor" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="File Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Height="25px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("FileName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:CommandField ShowDeleteButton="True" DeleteText="Remove" HeaderText="Action"
                                                                HeaderStyle-HorizontalAlign="Left" />
                                                        </Columns>
                                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                        <AlternatingRowStyle BorderStyle="None" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Spare disposal/Destruction Type:<font color='red'>*</font>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlSparedisposal" runat="server" CssClass="simpletxt1" Width="175px">
                                                        <asp:ListItem Value="0" Text="Select">Select</asp:ListItem>
                                                        <asp:ListItem Value="D">Destruction</asp:ListItem>
                                                        <asp:ListItem Value="R">Return</asp:ListItem>
                                                    </asp:DropDownList>
                                                    &nbsp;<asp:RequiredFieldValidator InitialValue="0" ID="RequiredFieldValidator8" runat="server"
                                                        SetFocusOnError="true" ErrorMessage="Spare disposal/Destruction Type is required."
                                                        ControlToValidate="ddlSparedisposal" ValidationGroup="editt" ToolTip="Spare disposal/Destruction Type is required."></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Spare Action By CG Branch:<font color='red'>*</font>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlSpareAction" runat="server" CssClass="simpletxt1" Width="175px">
                                                        <asp:ListItem Value="0" Text="Select">Select</asp:ListItem>
                                                        <asp:ListItem Value="D">Destruction</asp:ListItem>
                                                        <asp:ListItem Value="R">Return to division</asp:ListItem>
                                                        <asp:ListItem Value="S">Salvage</asp:ListItem>
                                                    </asp:DropDownList>
                                                    &nbsp;<asp:RequiredFieldValidator InitialValue="0" ID="RequiredFieldValidator10"
                                                        runat="server" SetFocusOnError="true" ErrorMessage="Spare Action By CG is required."
                                                        ControlToValidate="ddlSpareAction" ValidationGroup="editt" ToolTip="Spare Action By CG is required."></asp:RequiredFieldValidator>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Essential Spare:
                                                </td>
                                                <td>
                                                    <asp:CheckBoxList ID="ChkEssentialSpare" runat="server">
                                                        <asp:ListItem Value="E">Yes</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Spare Obsolete:
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rdoSpareObsolete" RepeatDirection="Horizontal" RepeatColumns="3"
                                                        runat="server">
                                                        <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                                        <asp:ListItem Value="N" Text="NO" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Status
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
                                                <td colspan="2">
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="left" style="padding-left: 225px">
                                                    <asp:Button ID="imgBtnAdd" runat="server" CausesValidation="True" CssClass="btn"
                                                        OnClick="imgBtnAdd_Click" Text="Add" ValidationGroup="editt" Width="70px" />
                                                    <asp:Button ID="imgBtnUpdate" runat="server" CausesValidation="True" CssClass="btn"
                                                        OnClick="imgBtnUpdate_Click" Text="Save" ValidationGroup="editt" Width="70px" />
                                                    <asp:Button ID="imgBtnCancel" runat="server" CausesValidation="false" CssClass="btn"
                                                        OnClick="imgBtnCancel_Click" Text="Cancel" Width="70px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                    </tr>
                </table>
                
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnFileUpload" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
