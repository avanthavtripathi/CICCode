<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" 
CodeFile="DealerRequestRegistration.aspx.cs" Inherits="pages_DealerRequestRegistration" Title="Dealer Registration Screen" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script language="javascript" type="text/javascript">
    function funOpenDivSC()
    {
               var strProductDivision=document.getElementById('ctl00_MainConHolder_ddlProductDiv');
               var strState=document.getElementById('ctl00_MainConHolder_ddlState');
               var strCity=document.getElementById('ctl00_MainConHolder_ddlCity');
               var strTerr=document.getElementById('ctl00_MainConHolder_ddlSC');
               
               if(strState.value=="Select")
                {
                    alert("Please select State");
                    strState.focus();
                    return false;
                }
                 if(strCity.value=="Select")
                {
                    alert("Please select City");
                    strCity.focus();
                    return false;
                }
//                if(strProductDivision.value=="0")
//                {
//                    alert("Please select Product Division");
//                    strProductDivision.focus();
//                    return false;
//                }
                document.getElementById("dvSearch").style.display='block';
                
    }
    function funConfirm()
    {
        if(confirm('Are you sure to cancel this request?'))
        {
          return true;
        }
        return false;
    }
    
    
    
    function CheckMaxLengthDealer(obj,len,e)

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
        
        
        
        function validateBatchCode(oSrc, args)
{

            //var varBatchYear = (document.getElementById('ctl00_MainConHolder_hdnValidBatch').value);
            var varBatchYear = (document.getElementById('<% = hdnValidBatch.ClientID %>').value);
        
            var varBatchMonth = 'A|B|C|D|E|F|G|H|I|J|K|L';
//            var currYear      = '2001|2002|2003|2004|2005|2006|2007|2008|2009|2010|2011|2012';
//            var my_month=new Date();
//            var month_name=new Array(12);
//            month_name[0]="January"
//            month_name[1]="February"
//            month_name[2]="March"
//            month_name[3]="April"
//            month_name[4]="May"
//            month_name[5]="June"
//            month_name[6]="July"
//            month_name[7]="August"
//            month_name[8]="September"
//            month_name[9]="October"
//            month_name[10]="November"
//            month_name[11]="December"

           // alert ("Current month = " + month_name[my_month.getMonth()]); 
            var arrayBatchYear =varBatchYear.split('|');
            var arrayBatchMonth =varBatchMonth.split('|');
          //  var arrayyear = currYear.split('|'); 
            var bolBatchYearExit= new Boolean(false);
            var inputProdSer=document.getElementById('ctl00_MainConHolder_txtProductSrno').value.toUpperCase();
            var inputBatchYear;
            var inputBatchMonth;
            //var ddlProductDivFir= document.getElementById('ctl00_MainConHolder_ddlFirProductDiv')
            //var productDiv=ddlProductDivFir.options[ddlProductDivFir.selectedIndex].text.toUpperCase();
            var productDiv=$get('ctl00_MainConHolder_lblUnit').innerHTML.toUpperCase();
            if(productDiv=='APPLIANCES' || productDiv=='APPLIANCE')
                {
                    inputProdSer=inputProdSer.substring(2,4);
                    inputBatchYear=inputProdSer.substring(0,1);
                    inputBatchMonth=inputProdSer.substring(1,2);
                }
            else
                {
                    inputProdSer=inputProdSer.substring(0,2);
                    inputBatchYear=inputProdSer.substring(0,1);
                    inputBatchMonth=inputProdSer.substring(1,2);
                }
            
            
            for (BatchYear in arrayBatchYear)
            {
               if( arrayBatchYear[BatchYear]==inputBatchYear)
                {
                    bolBatchYearExit=true;
                    break; 
                }
            }
            if (bolBatchYearExit==true)
            {
                for (BatchMonth in arrayBatchMonth)
                {
                    if( arrayBatchMonth[BatchMonth]==inputBatchMonth)
                    {
                        args.IsValid = true;
                        document.getElementById('ctl00_MainConHolder_txtBatchNo').value=inputProdSer;   
                        break; 
                    }
                    else
                    {
                     args.IsValid = false;
                     document.getElementById('ctl00_MainConHolder_txtBatchNo').value="Not a Vaild Serial No"; 
                    }
                    
                }
            }
            else
            {
             args.IsValid = false
             document.getElementById('ctl00_MainConHolder_txtBatchNo').value="Not a Vaild Serial No"; 
            }
}  
           
    </script>

    <table width="100%">
        <tr>
            <td class="headingred">
                Dealer New Service Registration
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="bgcolorcomm" colspan="2">
                <table width="99%">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="pnl" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <table width="100%">
                                        <tr height="20">
                                            <td align="right" width="87%">
                                                <asp:LinkButton ID="lnkbtnCustomerSearch" runat="server" OnClick="lnkbtnCustomerSearch_Click"
                                                    CausesValidation="false">Dealer Search</asp:LinkButton>
                                                <asp:HiddenField ID="hdnDealerId" Value="0" runat="server" />
                                                <asp:HiddenField ID="hdnCustomerId" Value="0" runat="server" />
                                                <asp:HiddenField ID="lblUnit" runat ="server" />
                                                <asp:HiddenField runat ="server" ID="txtBatchNo" />
                                            </td>
                                            <td  align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                                                <asp:UpdateProgress AssociatedUpdatePanelID="pnl" ID="UpdateProgress1" runat="server">
                                                    <ProgressTemplate>
                                                        <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" class="bgcolorcomm">
                                                <asp:Panel ID="pnlDealerSearch" runat="server">
                                                
                                                <!-- Customer Search Start -->
                                                <table width="100%" border="0" cellpadding="1" cellspacing="0" >
                                                    <tr>
                                                        <td width="12%" style="padding-left: 60px">
                                                            <asp:DropDownList CssClass="simpletxt1" ID="ddldealerCode" runat ="server" >
                                                            <asp:ListItem Selected ="True" Text ="Code" Value ="Dealer_Code"></asp:ListItem>
                                                            <asp:ListItem Text ="Name" Value ="Dealer_Name" ></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtUnique" ValidationGroup="CustomerSearchNew" CssClass="txtboxtxt"
                                                                runat="server" MaxLength="20" Text=""></asp:TextBox>&nbsp;<asp:Button ID="btnSearchCustomer"
                                                                    CssClass="btn" ValidationGroup="CustomerSearchNew" runat="server" Text="Search"
                                                                    OnClick="btnSearchCustomer_Click" />                                                           
                                                        </td>
                                                        <td >
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" align="center">
                                                            <asp:Label ID="lblCustMsg" runat="server" ForeColor="Red" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <!-- Users List Start Grid -->
                                                            <%--<div id="dvCustomerSearch" style="display: none;">--%>
                                                            <asp:GridView ID="gvCustomerList" runat="server" RowStyle-CssClass="gridbgcolor"
                                                                AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                                                                AutoGenerateColumns="False" DataKeyNames ="Dealer_Code" BorderStyle="None" GridLines="none" 
                                                                Width="100%" 
                                                                onselectedindexchanging="gvCustomerList_SelectedIndexChanging" 
                                                                onpageindexchanging="gvCustomerList_PageIndexChanging" AllowPaging="True" 
                                                                HorizontalAlign="Left">
                                                                <RowStyle CssClass="gridbgcolor" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="S.No">
                                                                    <ItemTemplate>
                                                                    <%# Container.DisplayIndex + 1 %>
                                                                    </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Code" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Height="25px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer_Code") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                          <HeaderStyle Height="25px" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Height="25px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Dealer_Name") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Height="25px" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>                                                                    
                                                                    <asp:CommandField ShowSelectButton="True" HeaderStyle-Width="100px" SelectText="Select"
                                                                        HeaderText="Action" HeaderStyle-HorizontalAlign="Left" >
                                                                                                                                             
                                                                        
                                                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                     </asp:CommandField>
                                                                                                                                             
                                                                        
                                                                </Columns>
                                                                <PagerStyle HorizontalAlign="Center" />
                                                                <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                                <AlternatingRowStyle BorderStyle="None" />
                                                            </asp:GridView>
                                                            <%--</div>--%>
                                                            <!-- Users LIst End Grid -->
                                                        </td>
                                                    </tr>
                                                </table>
                                                <!-- Customer Search End -->
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" class="bgcolorcomm">
                                                <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                                
                                                     <tr>
                                                        <td colspan="3">
                                                            <font color="red">Search and select Dealer Information first</font>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <b>Dealer Information:</b> &nbsp;
                                                        </td>
                                                        <td align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                                            <asp:HiddenField ID="hdnTerritoryDesc" runat="server" />
                                                            &nbsp;<asp:HiddenField ID="hdnScNo" runat="server" />
                                                            <asp:HiddenField ID="hdnSENo" runat="server" />
                                                            <font color='red'>*</font>
                                                            <%=ConfigurationManager.AppSettings["MandatoryText"]%></td>
                                                    </tr>
                                                   
                                                    <tr>
                                                        <td >
                                                            Prefix
                                                        </td>
                                                        <td >
                                                            <asp:DropDownList ID="ddlPrefix" runat="server" CssClass="simpletxt1">
                                                                <asp:ListItem Selected="True" Value="Mr">Mr</asp:ListItem>
                                                                 <asp:ListItem Value="Miss">Miss</asp:ListItem>
                                                                <asp:ListItem Value="Mrs">Mrs</asp:ListItem>
                                                                <asp:ListItem Value="Dr">Dr</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        
                                                        <td align="right" >
                                                            Dealer Code <font color="red">*</font>
                                                        </td>
                                                        <td >
                                                            <asp:TextBox id="txtDealerCode" runat ="server" ReadOnly ="true" CssClass="txtboxtxt"></asp:TextBox>
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDealerCode"
                                                                ErrorMessage="Dealer Code is required." SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                      <tr>
                                                        <td >
                                                          First Name<font color="red">*</font>
                                                        </td>
                                                        <td >
                                                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtboxtxt" Width="158px"
                                                                MaxLength="30"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldFirstName" runat="server" ControlToValidate="txtFirstName"
                                                                ErrorMessage="First Name is required." SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td align="right" >
                                                            Last Name<font color="red">*</font>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtLastName" runat="server" CssClass="txtboxtxt" Width="158px" MaxLength="20"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldLastName" runat="server" ControlToValidate="txtLastName"
                                                                ErrorMessage="Last Name is required." SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Dealer Type
                                                        </td>
                                                        <td >
                                                            <asp:DropDownList ID="ddlCustomerType" runat="server" CssClass="simpletxt1">
                                                            
                                                                <asp:ListItem Selected="True" Value="J">Dealer</asp:ListItem>
                                                          <%--      <asp:ListItem Value="I">Industrial</asp:ListItem>--%>
                                                                <%--<asp:ListItem Value="D">Dealer</asp:ListItem>--%>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td >
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Company Name
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox runat="server" MaxLength="150" Width="350Px" CssClass="txtboxtxt" 
                                                                ID="txtCompanyName" Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Address1<font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox runat="server" MaxLength="50" Width="350Px" CssClass="txtboxtxt" ID="txtAdd1"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" SetFocusOnError="true"
                                                                ControlToValidate="txtAdd1" ErrorMessage="Address1 is required." Display="None"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Address2
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtAdd2" runat="server" CssClass="txtboxtxt" MaxLength="50" Width="350px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Landmark
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtLandmark" runat="server" CssClass="txtboxtxt" MaxLength="150"
                                                                Width="350px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            State<font color='red'>*</font>
                                                        </td>
                                                        <td >
                                                            <asp:DropDownList Width="170px" runat="server" CssClass="simpletxt1" 
                                                                ID="ddlState" AutoPostBack="True" 
                                                                onselectedindexchanged="ddlState_SelectedIndexChanged" Enabled="False"
                                                                 >
                                                            </asp:DropDownList>
                                                            <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator1" runat="server"
                                                                ControlToValidate="ddlState" ErrorMessage="State is required." Operator="NotEqual"
                                                                ValueToCompare="Select" Display="None"></asp:CompareValidator>
                                                        </td>
                                                        <td valign="top" align="right">
                                                            City<font color='red'>*</font>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:DropDownList Width="170px" ID="ddlCity"  runat="server" 
                                                                CssClass="simpletxt1" AutoPostBack="True" 
                                                                onselectedindexchanged="ddlCity_SelectedIndexChanged" Enabled="False"
                                                                >
                                                                <asp:ListItem Text="Select" Value="Select" Selected="True"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator2" runat="server"
                                                                ControlToValidate="ddlCity" ErrorMessage="City is required." Operator="NotEqual"
                                                                ValueToCompare="Select"  Display="None"></asp:CompareValidator>
                                                            <asp:TextBox ID="txtOtherCity" Width="100px" MaxLength="30" runat="server" CssClass="txtboxtxt"></asp:TextBox><asp:RequiredFieldValidator
                                                                ID="reqCityOther" runat="server" SetFocusOnError="true" Display="None" ErrorMessage="Other City is required."
                                                                ControlToValidate="txtOtherCity"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Pin Code
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtPinCode" runat="server" CssClass="txtboxtxt" MaxLength="6"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Valid pin is required."
                                                                ControlToValidate="txtPinCode" SetFocusOnError="True" ValidationExpression="\d{6}"
                                                                Display="None"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" height="24">
                                                            <b>Contact Information:</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Mode Of Receipt
                                                        </td>
                                                        <td >
                                                            <asp:DropDownList ID="ddlModeOfRec" runat="server" CssClass="simpletxt1">
                                                            </asp:DropDownList>
                                                            <%--<asp:CheckBox ID="chkDefautMode" Text="Set as Default" runat="server" AutoPostBack="True"
                                                               />--%>
                                                        </td>
                                                        <td align="right">
                                                            Language
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList Width="170px" ID="ddlLanguage" runat="server" CssClass="simpletxt1">
                                                                <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:CheckBox ID="chkLanguage" Text="Set as Default" runat="server" AutoPostBack="True"
                                                                 />
                                                            <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator3" runat="server"
                                                                ControlToValidate="ddlLanguage" ErrorMessage="Language is required." Operator="NotEqual"
                                                                ValueToCompare="0">*</asp:CompareValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Contact No.<font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtContactNo" MaxLength="11" runat="server" CssClass="txtboxtxt"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" ControlToValidate="txtContactNo"
                                                                runat="server" ErrorMessage="Contact Number is required." Display="None"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Valid Contact Number is required."
                                                                ControlToValidate="txtContactNo" SetFocusOnError="True" ValidationExpression="\d{10,11}"
                                                                Display="None"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Alternate Contact No.
                                                        </td>
                                                        <td >
                                                            <asp:TextBox runat="server" MaxLength="11" ID="txtAltConatctNo" CssClass="txtboxtxt"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Valid Alternate Contact Number is required."
                                                                ControlToValidate="txtAltConatctNo" SetFocusOnError="True" ValidationExpression="\d{10,11}"
                                                                Display="None"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td  align="right">
                                                            Extension
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtExtension" MaxLength="5" runat="server" CssClass="txtboxtxt"></asp:TextBox>
                                                            <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Valid Extension is required."
                                                                Operator="DataTypeCheck" ControlToValidate="txtExtension" Type="Integer" Display="None"></asp:CompareValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            E-Mail
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtEmail" MaxLength="60" runat="server" CssClass="txtboxtxt" Width="213px"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Valid Email is required."
                                                                ControlToValidate="txtEmail" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                Display="None"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Fax No.
                                                        </td>
                                                        <td >
                                                            <asp:TextBox ID="txtFaxNo" runat="server" CssClass="txtboxtxt" MaxLength="15"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtFaxNo"
                                                                ErrorMessage="Valid Fax is required." SetFocusOnError="True" ValidationExpression="\d{10,11}"
                                                                Display="None"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td >
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                 <%--   <tr>
                                                        <td>
                                                            Appointment Req.
                                                        </td>
                                                        <td style="width: 28%">
                                                            <asp:CheckBox ID="chkAppointment" runat="server" />
                                                        </td>
                                                        <td style="width: 8%">
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkUpdateCustomerData" Text="Update Customer Info" Checked="false"
                                                                runat="server" />
                                                        </td>
                                                    </tr>--%>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                               
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td style="padding: 1px" align="left" class="bgcolorcomm">
                                                                                
                                                                                 <asp:UpdatePanel ID="ASCupdate" runat="server" UpdateMode="Always">
                                                                                 <ContentTemplate > 
                              
                                                                                    <table width="98%" border="0"> 
                                                                                                                                                                   
                                                                                       <tr>
                                                                                            <tr>
                                                                                            <td colspan="4" height="24">
                                                                                                <b>Service Contrator Information:</b>
                                                                                            </td>
                                                                                        </tr>
                                                                                       
                                                                                            <td >                                                           
                                                                                                Branch Name <font color='red'>*</font>
                                                                                            </td>
                                                                                             <td >                                                           
                                                                                               <asp:DropDownList Width="175px" ID="ddlBranch" AutoPostBack="true" runat="server"
                                                                                                    CssClass="simpletxt1" OnSelectedIndexChanged ="ddlBranch_SelectedIndexChanged" >
                                                                                                </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator4" SetFocusOnError="true" ControlToValidate="ddlBranch"
                                                                                            runat="server" InitialValue ="0" ErrorMessage="Select branch name." Display="None"></asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                            <td >                                                           
                                                                                                   Service Contractor <font color='red'>*</font>
                                                                                            </td>
                                                                                             <td >                                                           
                                                                                               <asp:DropDownList Width="175px" ID="ddlServiceContrator" AutoPostBack="true" runat="server"
                                                                                                    CssClass="simpletxt1" 
                                                                                                     onselectedindexchanged="ddlServiceContrator_SelectedIndexChanged" >
                                                                                                </asp:DropDownList>
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" SetFocusOnError="true" ControlToValidate="ddlServiceContrator"
                                                                                            runat="server" InitialValue ="0" ErrorMessage="Select service contrator." Display="None"></asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                      </tr> 
                                                                                      
                                                                                      <tr>
                                                                                            <td >                                                           
                                                                                                Service Engineer <font color='red'>*</font>
                                                                                            </td>
                                                                                             <td  colspan ="2">                                                           
                                                                                               <asp:DropDownList Width="175px" ID="ddlServiceEngg" AutoPostBack="true" runat="server"
                                                                                                    CssClass="simpletxt1" >
                                                                                                </asp:DropDownList>
                                                                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator8" SetFocusOnError="true" ControlToValidate="ddlServiceEngg"
                                                                                            runat="server" InitialValue ="0" ErrorMessage="Select service engineer." Display="None"></asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                           
                                                                                      </tr>                                                                                     
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:HiddenField ID="hdnType" runat="server" />
                                                                                                <asp:HiddenField ID="hdnIsSCClick" runat="server" />
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
                                                                                </td>
                                                                            </tr>
                                                                        </table> 
                                                            <!-- End Div open search -->
                                                        </td>
                                                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                              
                                <ContentTemplate>
                                    <table width="100%">                                       
                                        <tr>
                                            <td class="bgcolorcomm">
                                                <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                                    <tr>
                                                        <td colspan="4">
                                                            <b>Product & Complaint Information:</b> &nbsp;
                                                            <asp:GridView ID="gvCommSearch" runat="server" AllowPaging="True" 
                                                                AllowSorting="True" AlternatingRowStyle-CssClass="fieldName" 
                                                                AutoGenerateColumns="False" DataKeyNames="Routing_Sno" 
                                                                HeaderStyle-CssClass="fieldNamewithbgcolor" HorizontalAlign="Left" PageSize="5" 
                                                                RowStyle-CssClass="gridbgcolor" Width="100%">
                                                                <RowStyle CssClass="gridbgcolor" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                                                        <HeaderStyle Width="40px" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Territory" 
                                                                        ItemStyle-HorizontalAlign="Left">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                        <ItemTemplate>
                                                                            <asp:HiddenField ID="hdnTrr" runat="server" 
                                                                                Value='<%#Eval("Territory_Sno")%>' />
                                                                            <asp:Label ID="lblTRR" runat="server" Text='<%#Eval("Territory_Desc") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Landmark" 
                                                                        ItemStyle-HorizontalAlign="Left">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLM" runat="server" Text='<%#Eval("Landmark_Desc") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="SC Name" 
                                                                        ItemStyle-HorizontalAlign="Left">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="200px" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSC" runat="server" Text='<%#Eval("SC_Name") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Contact_Person" HeaderStyle-HorizontalAlign="Left" 
                                                                        HeaderText="Contact Person" ItemStyle-HorizontalAlign="Left">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Address1" HeaderStyle-HorizontalAlign="Left" 
                                                                        HeaderText="Address" ItemStyle-HorizontalAlign="Left">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="320px" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="PhoneNo" HeaderStyle-HorizontalAlign="Left" 
                                                                        HeaderText="Phone No" ItemStyle-HorizontalAlign="Left">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="80px" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="80px" 
                                                                        HeaderText="Weekly Off Day" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:HiddenField ID="hdnGridScNo" runat="server" Value='<%#Eval("Sc_Sno")%>' />
                                                                            <asp:HiddenField ID="hdnGridTerritoryDesc" runat="server" 
                                                                                Value='<%#Eval("Territory_Desc")%>' />
                                                                            <asp:HiddenField ID="hdnGridWO" runat="server" 
                                                                                Value='<%#Eval("Weekly_Off_Day")%>' />
                                                                            <%#Eval("Weekly_Off_Day") %>
                                                                            <%--<asp:LinkButton ID="btnLandmark" CausesValidation="false" CommandArgument='<%#Eval("Territory_Sno")%>'
                                                                                                                            runat="server" Text="Landmark" OnClick="btnLandmark_Click"></asp:LinkButton>--%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:ButtonField ButtonType="Link" CommandName="Select" HeaderText="Select" 
                                                                        Text="Select" />
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    <img alt="" src='<%=ConfigurationManager.AppSettings["UserMessage"]%>' /><b>No 
                                                                    records found</b>
                                                                </EmptyDataTemplate>
                                                                <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                                <AlternatingRowStyle CssClass="fieldName" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td >
                                                            Product Division<font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:DropDownList Width="175px" ID="ddlProductDiv" AutoPostBack="true" runat="server"
                                                                CssClass="simpletxt1" 
                                                                onselectedindexchanged="ddlProductDiv_SelectedIndexChanged" >
                                                            </asp:DropDownList>
                                                            <asp:CompareValidator SetFocusOnError="true" ID="compValProdDiv" runat="server" ControlToValidate="ddlProductDiv"
                                                                ErrorMessage="Product Division is required." Operator="NotEqual" ValueToCompare="0"
                                                                Display="None"></asp:CompareValidator>
                                                        </td>
                                                    </tr>
                                                    <tr id="trFrames" runat="server">
                                                        <td>
                                                            Frames<font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtFrames" runat="server" CssClass="txtboxtxt" onkeypress="javascript:return checkNumberOnly(event);" MaxLength="3" Text=""
                                                                Width="50px" AutoPostBack="True" ></asp:TextBox><asp:RequiredFieldValidator
                                                                    ID="reqValFrames" runat="server" ControlToValidate="txtFrames" ErrorMessage="Frames is required."
                                                                    Display="None"></asp:RequiredFieldValidator><asp:CompareValidator ID="CompareValidator4"
                                                                        runat="server" ErrorMessage="No. of Frames is required." Operator="DataTypeCheck"
                                                                        ControlToValidate="txtFrames" Type="Integer" Display="None"></asp:CompareValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Product Line <font color='red'>*</font>
                                                        </td>
                                                        <td >
                                                            <asp:DropDownList Width="175px" ID="ddlProductLine" runat="server" 
                                                                CssClass="simpletxt1" AutoPostBack="True" 
                                                                onselectedindexchanged="ddlProductLine_SelectedIndexChanged">
                                                                <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator
                                                                    ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlProductLine" InitialValue ="0" ErrorMessage="Product line is required."
                                                                    Display="None"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td align="right">
                                                        </td>
                                                        <td>
                                                            <%--<input type="button" id="btnGo" value="Find" class="btn" onclick="return funOpenDivSC();" />--%>
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td>
                                                            Product Group 
                                                        </td>
                                                        <td >
                                                            <asp:DropDownList Width="175px" ID="ddlProductGroup" runat="server" 
                                                                CssClass="simpletxt1" AutoPostBack="True" 
                                                                onselectedindexchanged="ddlProductGroup_SelectedIndexChanged">
                                                                <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                           
                                                        </td>
                                                        <td align="right">
                                                        </td>
                                                        <td>
                                                            <%--<input type="button" id="btnGo" value="Find" class="btn" onclick="return funOpenDivSC();" />--%>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td>
                                                            Product 
                                                        </td>
                                                        <td >
                                                            <asp:DropDownList Width="410px" ID="ddlProduct" runat="server" CssClass="simpletxt1">
                                                                <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                           
                                                        </td>
                                                        <td align="right">
                                                        
                                                            Product Serial no.<font color='red'>*</font>
                                                        
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtProductSrno" runat ="server" CssClass="txtboxtxt" MaxLength ="10" ></asp:TextBox>
                                                             <asp:RequiredFieldValidator
                                                                    ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtProductSrno" ErrorMessage="Product serial no. is required."
                                                                    Display="None"></asp:RequiredFieldValidator>
                                                                     <asp:CustomValidator  ID="CustomValidator7" runat="server"
                                        ControlToValidate="txtProductSrno" Display="None" ErrorMessage="Product Serial No is not Valid"
                                        ClientValidationFunction="validateBatchCode">
                                    </asp:CustomValidator>
                                                                    <asp:HiddenField ID="hdnValidBatch" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Quantity<font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtQuantity" runat="server" CssClass="txtboxtxt" MaxLength="2" Text="1"
                                                                Width="50px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                                                    runat="server" ControlToValidate="txtQuantity" ErrorMessage="Quantity is required."
                                                                    Display="None"></asp:RequiredFieldValidator><asp:CompareValidator ID="CompareValidator6"
                                                                        runat="server" ErrorMessage="Valid Quantity is required." Operator="DataTypeCheck"
                                                                        ControlToValidate="txtQuantity" Type="Integer" Display="None"></asp:CompareValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            Complaint Details<font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtComplaintDetails" runat="server" CssClass="txtboxtxt" Width="380px"
                                                                TextMode="MultiLine" MaxLength="500" Height="72px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator SetFocusOnError="true" ID="reqValComplaint" runat="server"
                                                                Display="None" ControlToValidate="txtComplaintDetails" ErrorMessage="Complaint Details is required."></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                <%--  <tr>
                                                        <td>
                                                            Invoice No.
                                                        </td>
                                                        <td style="width: 21%">
                                                            <asp:TextBox runat="server" ID="txtInvoiceNum" CssClass="txtboxtxt" ValidationGroup="Report" />
                                                        </td>
                                                        <td colspan="2">
                                                            Purchase date&nbsp;<asp:TextBox MaxLength="10" runat="server" ID="txtxPurchaseDate"
                                                                CssClass="txtboxtxt" /><asp:CompareValidator ID="CompareValidator7"
                                                                    runat="server" Type="Date" Operator="DataTypeCheck" ControlToValidate="txtxPurchaseDate" Display="None" ErrorMessage="Valid Purchase date is required. "></asp:CompareValidator>
                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtxPurchaseDate">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Purchased from
                                                        </td>
                                                        <td style="width: 21%">
                                                            <asp:TextBox ID="txtxPurchaseFrom" runat="server" CssClass="txtboxtxt" ValidationGroup="Report" />
                                                        </td>
                                                        <td align="left">
                                                       
                                                           <asp:Label ID="lblVisitCharge" runat="server" Text="0"></asp:Label>&nbsp;Rs.
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Service Contractor<font color='red'>*</font>
                                                        </td>
                                                        <td colspan="3" align="left">
                                                            <asp:TextBox ID="txtSc" CssClass="txtboxtxt" Width="250px" Enabled="false" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                                                ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtSc" ErrorMessage="Service Contractor is required."
                                                                Display="None"></asp:RequiredFieldValidator>
                                                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn" CausesValidation="false"
                                                                 />
                                                        </td>
                                                    </tr>  --%>
                                                    <tr>
                                                        <td>
                                                            <asp:HiddenField ID="hdnScId" runat="server" />
                                                        </td>
                                                        <td align="left" colspan="3">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td colspan="4" align="left" style="padding-left: 300px;">
                                                            <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                                                                runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" width="100%" align="center">
                                                            <!-- Product Division details Start-->
                                                            <table width="98%" border="0">
                                                                <tr>
                                                                    <td align="center">
                                                                        <!-- Action Listing -->
                                                                        <asp:GridView PageSize="10" DataKeyNames="SnoDisp" RowStyle-CssClass="gridbgcolor"
                                                                            AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                                                                            GridGroups="both" AllowPaging="True" AutoGenerateColumns="False" ID="gvComm"
                                                                            runat="server"  Width="98%" Visible="true" 
                                                                            onpageindexchanging="gvComm_PageIndexChanging" onrowcommand="gvComm_RowCommand" 
                                                                            onrowdeleting="gvComm_RowDeleting"  >
                                                                            <Columns>
                                                                                <asp:BoundField DataField="SnoDisp" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left"
                                                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="SNo"></asp:BoundField>
                                                                                <asp:BoundField DataField="ProductDivision" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Left"
                                                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Product Division"></asp:BoundField>
                                                                                <asp:BoundField DataField="SC" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left"
                                                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Service Contractor">
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                </asp:BoundField>
                                                                                      <asp:BoundField DataField="SE" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left"
                                                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Service Engg">
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                </asp:BoundField>
                                                                                  <asp:BoundField DataField="ProductLine" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Left"
                                                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Product line">
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                </asp:BoundField>
                                                                                
                                                                                  <asp:BoundField DataField="ProductGroup" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Left"
                                                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Product Group">
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                </asp:BoundField>
                                                                                
                                                                                  <asp:BoundField DataField="Product" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Left"
                                                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Product">
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="ProductSrno" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left"
                                                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Product Sr no">
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="NatureOfComplaintDisp" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left"
                                                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Nature of Complaint">
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                </asp:BoundField>
                                                                              
                                                                              
                                                                                <asp:TemplateField HeaderText="Edit/Delete">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnkBtnEdit" CommandArgument='<%#Eval("SnoDisp")%>' CommandName="Change"
                                                                                            CausesValidation="false" runat="server">Edit</asp:LinkButton>
                                                                                        /<asp:LinkButton ID="lnkBtnDelete" CausesValidation="false" CommandName="Delete"
                                                                                            runat="server">Delete</asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                        <!-- End Action Listing -->
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <!-- Product Division End -->
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" align="center">
                                                            <asp:Button ID="btnSubmit"  runat="server" CssClass="btn" 
                                                                Text="Save" Width="70px" onclick="btnSubmit_Click" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="btnAddMore"  runat="server" CssClass="btn" 
                                                                Text="Add More" Width="70px" onclick="btnAddMore_Click" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="btnCancel" runat="server" CausesValidation="false" CssClass="btn"
                                                                 OnClientClick="return funConfirm();" Text="Cancel" onclick="btnCancel_Click"
                                                                 />
                                                            &nbsp;
                                                            <asp:HiddenField ID="hdnKeyForUpdate" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" align="center">
                                                            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="99%" id="tableResult" runat="server">
                                        <tr>
                                            <td>
                                                <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        Your request has been processed. Please find below details:&nbsp;<b><asp:Label ID="lblReference"
                                                                            runat="server" Text=""></asp:Label></b>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:GridView PageSize="15" DataKeyNames="Sno" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                                                            HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="True"
                                                                            AutoGenerateColumns="False" ID="gvFinal" PagerSettings-HorizontalAlign="left"
                                                                            runat="server"  Width="98%" Visible="true">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="Sno" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left"
                                                                                    HeaderStyle-HorizontalAlign="Left" HeaderText="SNo"></asp:BoundField>
                                                                                <asp:TemplateField HeaderStyle-Width="160px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                                    HeaderText="Product Division">
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("ProductDivision")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderStyle-Width="280px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                                    HeaderText="Complaint Ref No">
                                                                                    <ItemTemplate>
                                                                                        <a href="Javascript:void(0);" onclick="funComplaintPopUp('<%#Eval("ComplaintRefNo")%>')">
                                                                                            <%#Eval("ComplaintRefNo")%></a>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                                    HeaderText="Service Contractor">
                                                                                    <ItemTemplate>
                                                                                        <a href="Javascript:void(0);" onclick="funSCPopUp(<%#Eval("Sc_Sno")%>)">
                                                                                            <%#Eval("SCName")%></a>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:Button ID="btnFresh" runat="server" CssClass="btn" Text="New Registration" 
                                                                            CausesValidation="false" onclick="btnFresh_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
