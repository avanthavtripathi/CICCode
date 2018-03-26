<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SIMSActivityConsumption_1.aspx.cs"
    Inherits="SIMS_Admin_SIMSActivityConsumption" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Spare Consumption</title>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/global.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../../scripts/Common.js">

    </script>

    <script type="text/javascript">
   

         // The below function is used for check whether press key is number or not
         // Only allow numbers
        function checkNumberOnly(e) 
         {
            var KeyID; 
            if(navigator.appName=="Microsoft Internet Explorer") 
            { 
                KeyID = e.keyCode;
            } 
            else 
            { 
                KeyID = e.charCode; 
            } 
            if(window.event) 
            { 
                if(window.event.keyCode==13) 
                { 
                    return false; 
                } 
            } 
//            if(KeyID==13) 
//            { 
//                return false; 
//            } 
            if((KeyID>47 && KeyID<58)||(KeyID==32)||(KeyID==8)) 
            {
            } 
            else 
            {
            alert("Please enter numbers only.");
                return false; 
            }          
         }
         // The below function is used for check whether press key is number and char or not
         // Only allow number and characters
        function checkNumberCharOnly(e) 
         {
            var KeyID; 
            if(navigator.appName=="Microsoft Internet Explorer") 
            { 
                KeyID = e.keyCode;
            } 
            else 
            { 
                KeyID = e.charCode; 
            } 
            if(window.event) 
            { 
                if(window.event.keyCode==13) 
                { 
                    return false; 
                } 
            } 
//            if(KeyID==13) 
//            { 
//                return false; 
//            } 
            if((KeyID>47 && KeyID<58)||(KeyID==32)||(KeyID==8)) 
            {
            } 
            else 
            {
            alert("Please enter numbers only.");
                return false; 
            }          
         }
         // The below function is used for check whether press key is valid character or not
         // Only allow characters
         function checkCharacterOnly(e) 
         {

            var KeyID; 
            if(navigator.appName=="Microsoft Internet Explorer") 
            { 
                KeyID = e.keyCode;
            } 
            else 
            { 
                KeyID = e.charCode; 
            } 
            if(window.event) 
            { 
                if(window.event.keyCode==13) 
                { 
                    return false; 
                } 
            } 
            if(KeyID==13) 
            { 
                return false; 
            } 
            if((KeyID>=65 && KeyID<91)||((KeyID>96 && KeyID<123))||(KeyID==32)|| (KeyID==46) || (KeyID==8)) 
            {
           
            } 
            else 
            {
           alert("Please enter alphabets only.");
                return false; 
            } 
         
         }
// The below function is used for check whether press key is A+, B+, O+, AB+, A-, B-, O-, AB-
        function checkCharBloodGroup(e) 
         {
            var KeyID; 
            if(navigator.appName=="Microsoft Internet Explorer") 
            { 
                KeyID = e.keyCode;
            } 
            else 
            { 
                KeyID = e.charCode; 
            } 
            if(window.event) 
            { 
                if(window.event.keyCode==13) 
                { 
                    return false; 
                } 
            } 
            if(KeyID==13) 
            { 
                return false; 
            } 
            
            if((KeyID==65)||(KeyID==66)||(KeyID==79)||(KeyID==97)||(KeyID==98)||(KeyID==111)||(KeyID==45)||(KeyID==43)||(KeyID==8)) 
            {
            
            } 
            else 
            {
                alert("Please enter A+, B+, O+, AB+, A-, B-, O-, AB- only.");
                return false; 
            } 
         
         }
         
       //This function is used for disable Enter
       
       function DisableEnterKey(e)
       {
            var KeyID; 
            if(navigator.appName=="Microsoft Internet Explorer") 
            { 
                KeyID = e.keyCode;
            } 
            else 
            { 
                KeyID = e.charCode; 
            } 
            if(window.event) 
            { 
                if(window.event.keyCode==13) 
                { 
                    return false; 
                } 
            } 
            if(KeyID==13) 
            { 
                return false; 
            } 
            return true;
       }
 function checkExperience(e)
 {
            var KeyID; 
            
            if(navigator.appName=="Microsoft Internet Explorer") 
            { 
                KeyID = e.keyCode;
            } 
            else 
            { 
                KeyID = e.charCode; 
            } 
            if(window.event) 
            { 
                if(window.event.keyCode==13) 
                { 
                    return false; 
                } 
            } 
            if(KeyID==13) 
            { 
                return false; 
            } 
            if((KeyID>47 && KeyID<58)||(KeyID==46)||(KeyID==8)) 
            {
            } 
            else 
            {
                alert("Please enter numbers only.");
                return false; 
            }  
            
       }
       function CheckMaxLength(obj,len,e)

       { 

            var KeyID; 
            if(navigator.appName=="Microsoft Internet Explorer") 
            { 
                KeyID = e.keyCode;
            } 
            else 
            { 
                KeyID = e.charCode; 
            } 
      //alert(obj.value.length);
             if(obj.value.length>len)
             {
                 if((KeyID==8)|| (KeyID==46)) 
                {
                }
                else
                 {;
                 return false;
                 }
             }
        }
        //added by sandeep:29/12/2010
     function OpenActivityPop(url)
        {                         
    	    newwindow=window.open(url,'name','height=400,width=300,scrollbars=1,resizable=no,top=1,left=1');
            if (window.focus)
             {
             newwindow.focus()
             }
          return false;    
        }        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%--<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td width="249" background="../images/headerBg.jpg">
                    <img src="../../images/small-logo.jpg">
                </td>
                <td width="651" align="right" background="../../images/headerBg.jpg">
                    <img src="../../images/small-cic-txt.jpg" width="267" height="86">
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 22px; background-color: #396870">
                </td>
            </tr>
        </table>
        <br />--%>
        <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="600" runat="server"
            EnablePageMethods="true" />
        <asp:UpdatePanel ID="updatepnl" runat="server">
            <ContentTemplate>

                <script type="text/javascript">
        function checkDate(sender,args)
            {
                //create a new date var and set it to the
                //value of the senders selected date
                var selectedDate = new Date();
                selectedDate = sender._selectedDate;
                //create a date var and set it's value to today
                var todayDate = new Date();
                var mssge = "";

                if(selectedDate > todayDate)
                 {
                    //set the senders selected date to today
                    sender._selectedDate = todayDate;
                    //set the textbox assigned to the cal-ex to today
                    sender._textbox.set_Value(sender._selectedDate.format(sender._format));
                    //alert the user what we just did and why
                    alert("Date Cannot be greater than present date");
                 }
                }
                 function ClaimAmount()
                {
                
                document.getElementById("ctl00_MainConHolder_gvComm_ctl02_lblClaimAmount").value=document.getElementById("ctl00_MainConHolder_gvComm_ctl02_txtConsumedQty").value;
                    
                }

                
                function getNumeric_only(strvalue)
                 {
                   if(!(event.keyCode==46||event.keyCode==48||event.keyCode==49||event.keyCode==50||event.keyCode==51||event.keyCode==52||event.keyCode==53||event.keyCode==54||event.keyCode==55||event.keyCode==56||event.keyCode==57))
   		                {	
   			                event.returnValue=false;	
   			                alert("Please enter numbers only.");
                   		
   		                }    
                }
                function ValidateAdvices()
                {
                    if(hdnProposedSpares.Value!="")
                    {
                        alert("Advice has been generated for spares: " + hdnProposedSpares.Value + ".");
                    }
                    //return true;
                    alert(hdnProposedSpares.Value);
                }
                
                
                
                
                    function CalculateCurrentAmount(txtAmount,txtQty,lblRate)
                    { 
                        //alert(document.getElementById(txtAmount).value);
                        //alert(document.getElementById(txtQty).value);
                        //alert(document.getElementById(lblRate).innerHTML);
                        //alert(document.getElementById(txtAmount).disabled);
                        document.getElementById(txtAmount).value=document.getElementById(txtQty).value*document.getElementById(lblRate).innerHTML;
                        if(document.getElementById(txtAmount).value=="NaN")
                        {
                            document.getElementById(txtAmount).value="0";
                        }
                    }
                </script>

                <table width="100%">
                    <tr>
                        <td class="headingred">
                            Activity Consumption for a complaint
                        </td>
                        <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                            <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                <ProgressTemplate>
                                    <img src="<%=ConfigurationManager.AppSettings["SIMSAjaxLoadingImageName"]%>" alt="" /> </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 10px" align="center" colspan="2">
                            <table width="100%" border="0">
                                <tr>
                                    <td width="100%" align="left" class="bgcolorcomm">
                                        <table width="98%" border="0">
                                            <tr>
                                                <td align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                                    <font color='red'>*</font>
                                                    <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <%--<td width="30%">
                                                Branch Name<font color='red'>*</font>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtboxtxt" ID="txtBranchName" runat="server" Width="170px"
                                                    Text="" />
                                                <asp:RequiredFieldValidator ID="RFStateDesc" runat="server" SetFocusOnError="true"
                                                    ErrorMessage="Branch Name is required." ControlToValidate="txtBranchName" ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            </td>--%>
                                                <td>
                                                    <table width="100%">
                                                        <tr>
                                                            <td style="width: 14%">
                                                                Complaint No.<font color='red'>*</font>
                                                            </td>
                                                            <td>
                                                                <%--<asp:DropDownList ID="ddlComplaintNo" runat="server" CssClass="simpletxt1" Width="175px"
                                                                ValidationGroup="editt" AutoPostBack="True" OnSelectedIndexChanged="ddlComplaintNo_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RFComplaintNo" runat="server" ControlToValidate="DDLComplaintNo"
                                                                ErrorMessage="*" InitialValue="Select" SetFocusOnError="true" ValidationGroup="editt"></asp:RequiredFieldValidator>--%>
                                                                <asp:Label runat="server" ID="lblComplaintNo"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 14%">
                                                                Complaint Date
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblComplaintDate" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 14%">
                                                                Product Division
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblProdDiv" runat="server" BorderStyle="None"></asp:Label>
                                                                <asp:HiddenField ID="hdnProductDivision_Id" runat="server" />
                                                                <asp:HiddenField ID="hdnProduct_Id" runat="server" />
                                                                <asp:HiddenField ID="hdnASC_Id" runat="server" />
                                                                <asp:HiddenField ID="hdnUserType_Code" runat="server" />
                                                                <asp:HiddenField ID="hdnProposedSpares" runat="server" />
                                                            </td>
                                                            <td width="5%">
                                                            </td>
                                                            <td width="10%">
                                                            </td>
                                                            <td width="15%">
                                                                <%--<asp:Label ID="lblProduct" runat="server" Font-Bold="True"></asp:Label>--%>
                                                            </td>
                                                            <td width="5%">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 14%">
                                                                Warranty Status
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblwarrantystatus" runat="server" BorderStyle="None"></asp:Label>
                                                            </td>
                                                        </tr>
                                                         <tr id="trTotalsplitCount" runat="server" visible="false">
                                                            <td style="width: 14%">
                                                                Total Split Complaint
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblTotalsplitComplaintNo" runat="server" BorderStyle="None"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                </td>
                                            </tr>
                                            <tr id="trActivityHeader" runat="server">
                                                <td>
                                                    <b>
                                                        <asp:Label ID="lblActivityCharges" runat="server" Text="Activity Charges"></asp:Label></b>
                                                </td>
                                            </tr>
                                            <tr id="trActivitySearch" runat="server">
                                                            <%--Id Added On 23.9.14 By Ashok--%>
                                                            <td>
                                                            <div style="width:28%; float:left;">
                                                                <asp:Label ID="lblActivitySearch" runat="server" Text="Search Activity:"></asp:Label>
                                                                <asp:TextBox ID="TxtActivityearch" runat="server" CausesValidation="True" CssClass="txtboxtxt"
                                                                    ValidationGroup="ProductRef" Width="130"></asp:TextBox>
                                                                <asp:Button ID="BtnSearch" runat="server" CssClass="btn" OnClick="BtnSearch_Click1"
                                                                    Text="Go" ValidationGroup="ProductRef" Width="20px" />
                                                                &nbsp;
                                                                <%--<asp:LinkButton ID="LnkActivity" runat="server" ForeColor="#1c3d74" OnClick="LnkActivity_Click">View Activity</asp:LinkButton>--%>
                                                                </div>
                                                                <div style="width:70%; float:left;" id="dvOutstationLocal" runat="server">
                                                                <asp:RadioButtonList ID="rbnLocalOutStation" runat="server" 
                                                                        RepeatDirection="Horizontal" onClick="return Confirm();">
                                                                <asp:ListItem Text="Local Charges" Value="L" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="OutStation Charges" Value="O"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <asp:Button ID="btnConfirm" runat="server" Text="" OnClick="btnConfirmClick" style="display:none;" />
                                                                <asp:HiddenField ID="hdnActivityCharges" runat="server" />
                                                                </div>
                                                                <div style="clear:both"></div>
                                                            </td>
                                                        </tr>
                                            <%--<tr id="trAtivitySearch" runat="server">
                                                <td>
                                                    <asp:Label ID="lblActivitySearch" runat="server" Text="Search Activity:"></asp:Label>
                                                    <asp:TextBox ID="TxtActivityearch" ValidationGroup="ProductRef" CssClass="txtboxtxt"
                                                        runat="server" Width="130" CausesValidation="True"></asp:TextBox>
                                                    <asp:Button ID="BtnSearch" runat="server" ValidationGroup="ProductRef" Width="20px"
                                                        Text="Go" CssClass="btn" OnClick="BtnSearch_Click1" />&nbsp;
                                                </td>
                                            </tr>--%>
                                            <tr id="trDemoCharges" runat="server">
                                                <td>
                                                    <b>
                                                        <span>Demo Charges</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="GridActivitySearch" runat="server" AlternatingRowStyle-CssClass="fieldName"
                                                        AutoGenerateColumns="False" GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor"
                                                        HorizontalAlign="Left" RowStyle-CssClass="gridbgcolor" Width="1300px" 
                                                        HeaderStyle-VerticalAlign="Top" OnRowDataBound="GridActivitySearch_RowDataBound"
                                                        onrowcreated="GridActivitySearch_RowCreated">
                                                        <RowStyle CssClass="gridbgcolor" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="ActivitySno" Visible="false" DataField="ActivityParameter_SNo"
                                                                SortExpression="ActivityParameter_SNo" ReadOnly="True" />
                                                            <asp:BoundField HeaderText="Activity" DataField="Activity_Description" SortExpression="Activity_Description"
                                                                ReadOnly="True" />
                                                            <asp:BoundField HeaderText="Param-1" DataField="Parameter_Code1" SortExpression="Parameter_Code1"
                                                                ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="PV-1" DataField="Possible_Value1" SortExpression="Possible_Value1"
                                                                ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Param-2" DataField="Parameter_Code2" SortExpression="Parameter_Code2"
                                                                ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="PV-2" DataField="Possible_Value2" SortExpression="Possible_Value2"
                                                                ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Param-3" DataField="Parameter_Code3" SortExpression="Parameter_Code3"
                                                                ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="PV-3" DataField="Possible_Value3" SortExpression="Possible_Value3"
                                                                ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Param-4" DataField="Parameter_Code4" SortExpression="Parameter_Code4"
                                                                ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="PV-4" DataField="Possible_Value4" SortExpression="Possible_Value4"
                                                                ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Rate" DataField="Rate" SortExpression="Rate" ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Select">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="CheckSelect" runat="server" />
                                                                    <asp:HiddenField ID="hdnActivityParameter_SNo" Value='<%# DataBinder.Eval(Container.DataItem, "ActivityParameter_SNo")%>'
                                                                        runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                        <AlternatingRowStyle CssClass="fieldName" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Button Text="Add" Width="70px" ID="BtnAdd" CssClass="btn" runat="server" CausesValidation="False"
                                                        ValidationGroup="Add" OnClick="BtnAdd_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="gvActivityCharges" runat="server" AlternatingRowStyle-CssClass="fieldName"
                                                        AutoGenerateColumns="False" GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor"
                                                        HorizontalAlign="Left" RowStyle-CssClass="gridbgcolor" Width="1300px" HeaderStyle-VerticalAlign="Top"
                                                        OnRowDataBound="gvActivityCharges_RowDataBound" EmptyDataText="<p align='center'><b>Currently no activity added for this complaint. Please search to add new activities.</b></p>">
                                                        <RowStyle CssClass="gridbgcolor" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Activity" DataField="Activity_Description" SortExpression="Activity_Description"
                                                                ReadOnly="True" />
                                                            <asp:BoundField HeaderText="Param-1" DataField="Parameter_Code1" SortExpression="Parameter_Code1"
                                                                ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="PV-1" DataField="Possible_Value1" SortExpression="Possible_Value1"
                                                                ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Param-2" DataField="Parameter_Code2" SortExpression="Parameter_Code2"
                                                                ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="PV-2" DataField="Possible_Value2" SortExpression="Possible_Value2"
                                                                ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Param-3" DataField="Parameter_Code3" SortExpression="Parameter_Code3"
                                                                ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="PV-3" DataField="Possible_Value3" SortExpression="Possible_Value3"
                                                                ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Param-4" DataField="Parameter_Code4" SortExpression="Parameter_Code4"
                                                                ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="PV-4" DataField="Possible_Value4" SortExpression="Possible_Value4"
                                                                ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <%--<asp:BoundField HeaderText="Rate" DataField="Rate" SortExpression="Rate" ReadOnly="True">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>--%>
                                                            <asp:TemplateField HeaderText="Rate">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRate" runat="server" CssClass="simpletxt1" Text='<%# DataBinder.Eval(Container.DataItem, "Rate")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="UOM" DataField="UOM" SortExpression="UOM" ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Quantity">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtActualQty" runat="server" CssClass="simpletxt1" Width="60px"
                                                                        ValidationGroup="editt1" Text='<%# DataBinder.Eval(Container.DataItem, "Actual_Qty")%>'></asp:TextBox>
                                                                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvRate1" runat="server" ControlToValidate="txtActualQty"
                                                                        Display="Dynamic" ErrorMessage="Qty can't be zero." InitialValue="0" ValidationGroup="editt1"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator EnableClientScript="true" Display="Dynamic" ControlToValidate="txtActualQty"
                                                                        ID="RegularExpressionValidator123" ValidationGroup="editt1" runat="server" ErrorMessage="Numeric Value required."
                                                                        ValidationExpression="^\d*$">
                                                                    </asp:RegularExpressionValidator>
                                                                    <asp:RangeValidator ID="rngKmValidatior" runat="server" ValidationGroup="editt1" MaximumValue="50" ControlToValidate="txtActualQty" 
                                                                                MinimumValue="0" ErrorMessage="Enter Less than or equall to 50 km" EnableClientScript="true" Display="Dynamic">
                                                                                </asp:RangeValidator>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtAmount" AutoPostBack="false" OnTextChanged="txtAmount_TextChanged"
                                                                        runat="server" CssClass="simpletxt1" Width="60px" ValidationGroup="editt1" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>'></asp:TextBox>
                                                                    <asp:RegularExpressionValidator EnableClientScript="true" Display="Dynamic" ControlToValidate="txtAmount"
                                                                        ID="RegularExpressionValidator12345" ValidationGroup="editt1" runat="server"
                                                                        ErrorMessage="Proper Amount required." ValidationExpression="^\d{1,8}(\.\d{1,2})?$">
                                                                    </asp:RegularExpressionValidator>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:BoundField HeaderText="Amount" DataField="Amount" SortExpression="Amount" ReadOnly="True">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>--%>
                                                            <asp:TemplateField HeaderText="Remarks">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="simpletxt1" Width="150px" TextMode="MultiLine"
                                                                        ValidationGroup="editt1" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks")%>'></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnActivityParameterSno" runat="server" Value='<%# Eval("ActivityParameter_SNo") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField HeaderText="Select">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkActivityConfirm" Checked=' <%# DataBinder.Eval(Container.DataItem, "Confirm")%>'
                                                                        runat="server" AutoPostBack="true" OnCheckedChanged="chkActivityConfirm_CheckedChanged" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="ActivityParameter_SNo" DataField="ActivityParameter_SNo"
                                                                SortExpression="ActivityParameter_SNo" ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="FixedAmount" DataField="FixedAmount" SortExpression="FixedAmount"
                                                                ReadOnly="True">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="FixedAmount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblactual" runat="server" Text='<%# Eval("FixedAmount") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                        <AlternatingRowStyle CssClass="fieldName" />
                                                        <%--<EmptyDataTemplate>
                                                            <p align="center">
                                                                <b>Currently no activity added for this complaint. Please search to add new activities.</b></p>
                                                        </EmptyDataTemplate>--%>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <asp:HiddenField ID="hdnActivity_param_sno" runat="server" />
                                                        <asp:HiddenField ID="hdnActual" runat="server" />
                                                        <asp:HiddenField ID="hdnManpower" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hdnManpowerCharges" runat="server" Value="0.0" />
                                                        <asp:HiddenField ID="hdnTotalManpowerCharges" runat="server" Value="0.0" />
                                            <tr id="trManpowerlabourCharges" runat="server" visible="false">                                           
                                            
                                                            <td align="right" class="MsgTDCount">
                                                                <div>
                                                                    <div style="width: 80%; float: left; color:Red;">
                                                                        (Man Days)x</div>
                                                                    <div style="width: 9%; float: left; color:Red;">
                                                                        (Per Days Charges)</div>
                                                                        <div style="width: 10%; float: left;">&nbsp;</div>
                                                                    <div style="clear: both;">
                                                                    </div>
                                                                </div>
                                                                <div>
                                                                    <div style="width: 80%; float: left;">
                                                                        <asp:DropDownList ID="ddlManDaysNo" runat="server" CssClass="simpletxt1" 
                                                                            Width="50px" AutoPostBack="True" 
                                                                            onselectedindexchanged="ddlManDaysNo_SelectedIndexChanged">
                                                                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                            <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                                        </asp:DropDownList>&nbsp;x&nbsp;
                                                                    </div>
                                                                    
                                                                    <div style="width: 9%; float: left; border-bottom:1px solid #000;">
                                                                        <asp:Label ID="lblManPerDayCharg" runat="server" Text="0"></asp:Label>
                                                                    </div><div style="width: 10%; float: left; border-bottom:1px solid #000;">
                                                                    <asp:Label ID="lblManPowerCharges" runat="server" Text="0" CssClass="MsgTotalCount"></asp:Label></div>
                                                                    <div style="clear:both;"></div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" class="MsgTDCount">
                                                            <div>
                                                            <div style="width: 80%; float: left;">&nbsp;</div>
                                                            <div style="width: 9%; float: left;">Total Amount :</div>
                                                            <div style="width: 10%; float: left;">
                                                                <asp:Label ID="lbltamount" runat="server" CssClass="MsgTotalCount" Text="0"></asp:Label>
                                                                </div>
                                                                <div style="clear:both;"></div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr id="trdiscount" runat="server" visible="false">
                                                            <td align="right" class="MsgTDCount">
                                                                If more than one repair is done then 65 % of sum of individual charges to be given.
                                                                (only for PUMP)
                                                                <%--  <asp:Label ID="lblspecialdiscount" CssClass="MsgTotalCount" runat="server" ></asp:Label>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" class="MsgTDCount">
                                                            <div>
                                                            <div style="width: 80%; float: left;">&nbsp;</div>
                                                            <div style="width: 9%; float: left;">Actual Amount :</div>
                                                            <div style="width: 10%; float: left;">
                                                                <asp:Label ID="lblTotalAmount" runat="server" CssClass="MsgTotalCount"></asp:Label>
                                                                </div>
                                                               <div style="clear:both;"></div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                <td>
                                                    <table style="width: 58%">
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Button ID="imgbtnSave" runat="server" CausesValidation="true" CssClass="btn"
                                                                    OnClick="imgbtnSave_Click" Text="Save &amp; Close" ValidationGroup="editt1" Width="110px" />
                                                                <asp:Button ID="imgBtnCancel" runat="server" CausesValidation="False" CssClass="btn"
                                                                    OnClick="imgBtnCancel_Click1" Text="Cancel" ValidationGroup="editt" Width="70px" />
                                                                <asp:Panel ID="popupPanel" runat="server" Style="display: none; width: 200px; background-color: Gray;
                                                                    border-width: 2px; border-color: Black; border-style: solid; padding: 20px;">
                                                                    <br />
                                                                    <br />
                                                                    <div style="text-align: left;">
                                                                        <b>Are You Sure to confirm?</b><br />
                                                                        <br />
                                                                    </div>
                                                                    <div style="text-align: right;">
                                                                        <asp:Button ID="ButtonOk" runat="server" CssClass="btn" Text="OK" />
                                                                        <asp:Button ID="ButtonCancel" runat="server" CssClass="btn" Text="Cancel" />
                                                                    </div>
                                                                </asp:Panel>
                                                                <%--<asp:Button Text="Close Complaint" Width="100px" ID="imgbtnCloseComplaint" CssClass="btn" runat="server"
                                                    CausesValidation="true" ValidationGroup="editt1" OnClick="imgbtnCloseComplaint_Click" />--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
