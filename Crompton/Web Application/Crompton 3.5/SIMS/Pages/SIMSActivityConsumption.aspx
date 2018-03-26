<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SIMSActivityConsumption.aspx.cs"
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
                                    <img src="<%=ConfigurationManager.AppSettings["SIMSAjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
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
                                            <tr id="trAtivitySearch" runat="server">
                                                <td>
                                                    <asp:Label ID="lblActivitySearch" runat="server" Text="Search Activity:"></asp:Label>
                                                    <asp:TextBox ID="TxtActivityearch" ValidationGroup="ProductRef" CssClass="txtboxtxt"
                                                        runat="server" Width="130" CausesValidation="True"></asp:TextBox>
                                                    <asp:Button ID="BtnSearch" runat="server" ValidationGroup="ProductRef" Width="20px"
                                                        Text="Go" CssClass="btn" OnClick="BtnSearch_Click1" />&nbsp;
                                                </td>
                                            </tr>
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
                                                        HeaderStyle-VerticalAlign="Top" 
                                                        onrowcreated="GridActivitySearch_RowCreated" 
                                                        onrowdatabound="GridActivitySearch_RowDataBound" >
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
                                                                    <asp:CheckBox ID="CheckSelect" runat="server" CausesValidation="false" />
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
                                                                    HeaderStyle-VerticalAlign="Top" HorizontalAlign="Left" OnRowDataBound="gvActivityCharges_RowDataBound"
                                                                    RowStyle-CssClass="gridbgcolor" Width="1300px">
                                                                    <RowStyle CssClass="gridbgcolor" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="Activity_Description" HeaderText="Activity" ReadOnly="True"
                                                                            SortExpression="Activity_Description" />
                                                                        <asp:BoundField DataField="Parameter_Code1" HeaderText="Param-1" ReadOnly="True"
                                                                            SortExpression="Parameter_Code1">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Possible_Value1" HeaderText="PV-1" ReadOnly="True" SortExpression="Possible_Value1">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Parameter_Code2" HeaderText="Param-2" ReadOnly="True"
                                                                            SortExpression="Parameter_Code2">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Possible_Value2" HeaderText="PV-2" ReadOnly="True" SortExpression="Possible_Value2">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Parameter_Code3" HeaderText="Param-3" ReadOnly="True"
                                                                            SortExpression="Parameter_Code3">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Possible_Value3" HeaderText="PV-3" ReadOnly="True" SortExpression="Possible_Value3">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Parameter_Code4" HeaderText="Param-4" ReadOnly="True"
                                                                            SortExpression="Parameter_Code4">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Possible_Value4" HeaderText="PV-4" ReadOnly="True" SortExpression="Possible_Value4">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="Rate">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRate" runat="server" CssClass="simpletxt1" Text='<%# DataBinder.Eval(Container.DataItem, "Rate")%>'></asp:Label>
                                                                                <asp:HiddenField ID="HdnActivityCode" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Activity_Code")%>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="UOM" HeaderText="UOM" ReadOnly="True" SortExpression="UOM">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="Quantity">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtActualQty" runat="server" CssClass="simpletxt1" Text='<%# DataBinder.Eval(Container.DataItem, "Actual_Qty")%>'
                                                                                    ValidationGroup="editt1" Width="60px"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="rfvRate1" runat="server" ControlToValidate="txtActualQty"
                                                                                    Display="Dynamic" ErrorMessage="Qty can't be zero." InitialValue="0" SetFocusOnError="true"
                                                                                    ValidationGroup="editt1"></asp:RequiredFieldValidator>
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator123" runat="server"
                                                                                    ControlToValidate="txtActualQty" Display="Dynamic" EnableClientScript="true"
                                                                                    ErrorMessage="Numeric Value required." ValidationExpression="^\d*$" ValidationGroup="editt1">
                                                                                </asp:RegularExpressionValidator>
                                                                                <asp:RangeValidator ID="rngKmValidatior" Type="Integer" runat="server" ValidationGroup="editt1"
                                                                                    MaximumValue="100" ControlToValidate="txtActualQty" MinimumValue="0" ErrorMessage="Enter Less than or equall to 100 km"
                                                                                    EnableClientScript="true" Display="Dynamic">
                                                                                </asp:RangeValidator>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Amount">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtAmount" runat="server" CssClass="simpletxt1" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>'
                                                                                    ValidationGroup="editt1" Width="60px"></asp:TextBox>
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator12345" runat="server"
                                                                                    ControlToValidate="txtAmount" Display="Dynamic" EnableClientScript="true" ErrorMessage="Proper Amount required."
                                                                                    ValidationExpression="^\d{1,8}(\.\d{1,2})?$" ValidationGroup="editt1">
                                                                                </asp:RegularExpressionValidator>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Remarks">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="simpletxt1" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks")%>'
                                                                                    TextMode="MultiLine" ValidationGroup="editt1" Width="150px"></asp:TextBox>
                                                                                <asp:HiddenField ID="hdnActivityParameterSno" runat="server" Value='<%# Eval("ActivityParameter_SNo") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Select">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkActivityConfirm" runat="server" AutoPostBack="true" Checked=' <%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Confirm"))%>'
                                                                                    OnCheckedChanged="chkActivityConfirm_CheckedChanged" onClick="CheckQuantity(this)" />
                                                                                <asp:Label ID="lblactivityid" runat="server" Style="display: none" Text=' <%# Eval("Activity_Id")%>' />
                                                                                <asp:Label ID="lbldiscount" runat="server" Style="display: none" Text=' <%# Eval("Discount")%>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="ActivityParameter_SNo" HeaderText="ActivityParameter_SNo"
                                                                            ReadOnly="True" SortExpression="ActivityParameter_SNo">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <%-- Added for actual amiunt (editable) logic 29 sept bp--%>
                                                                        <asp:TemplateField HeaderText="FixedAmount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblactual" runat="server" Text='<%# Eval("FixedAmount") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                                    <AlternatingRowStyle CssClass="fieldName" />
                                                                    <EmptyDataTemplate>
                                                                        <p align="center">
                                                                            <b>Currently no activity added for this complaint. Please search to add new activities.</b></p>
                                                                    </EmptyDataTemplate>
                                                                </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="MsgTDCount">
                                                    Total Amount :
                                                    <asp:Label ID="lblTotalAmount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Button Text="Save & Close" Width="110px" ID="imgbtnSave" CssClass="btn" runat="server"
                                                        CausesValidation="true" ValidationGroup="editt1" OnClick="imgbtnSave_Click" />
                                                    <asp:Button Text="Cancel" Width="70px" ID="imgBtnCancel" CssClass="btn" runat="server"
                                                        CausesValidation="False" ValidationGroup="editt" OnClick="imgBtnCancel_Click1" />
                                                    <asp:Panel ID="popupPanel" runat="server" Style="display: none; width: 200px; background-color: Gray;
                                                        border-width: 2px; border-color: Black; border-style: solid; padding: 20px;">
                                                        <br />
                                                        <br />
                                                        <div style="text-align: left;">
                                                            <b>Are You Sure to confirm?</b><br />
                                                            <br />
                                                        </div>
                                                        <div style="text-align: right;">
                                                            <asp:Button ID="ButtonOk" runat="server" Text="OK" CssClass="btn" />
                                                            <asp:Button ID="ButtonCancel" runat="server" CssClass="btn" Text="Cancel" />
                                                        </div>
                                                    </asp:Panel>
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
