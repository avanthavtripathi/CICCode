<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master"
    CodeFile="ASCStockView.aspx.cs" Inherits="SIMS_Pages_ASCStockView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="ContentStateMaster" ContentPlaceHolderID="MainConHolder" runat="Server">
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
            </script>

            <table width="100%">
                <tr>
                    <td class="headingred">
                        ASC Stock View
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
                        <table width="80%" border="0" align="center" class="bgcolorcomm">
                            <tr>
                                <td width="100%" align="left">
                                    <table width="100%" border="0">
                                        <%--<tr>
                                            <td align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                                <font style="color: #FF0000">*</font>
                                                <%=ConfigurationManager.AppSettings["MandatoryText"]%>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td>
                                                <table width="100%">
                                                    <tr>
                                                        <td style="width: 126px">
                                                            <b>Product Division </b>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlproddiv" runat="server" CssClass="simpletxt1" Width="228px">
                                                            </asp:DropDownList>                                                            
                                                            <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn" OnClick="Button1_Click" />
                                                            
                                                            <%-- <asp:RequiredFieldValidator ID="RFComplaintNo" runat="server" ControlToValidate="ddlproddiv"
                                                                ErrorMessage="Product division is required." InitialValue="0" SetFocusOnError="true" ValidationGroup="editt"></asp:RequiredFieldValidator>--%> 
                                                            <br>
                                                        </td>
                                                    </tr>
                                                   
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="SpareSearch" runat="server" visible="false">
                                            <td align="right">
                                                <b>Search Spare</b>
                                                            <asp:TextBox ID="txtspare" runat="server" CssClass="txtboxtxt" Height="16px" Width="90px" 
                                                                ></asp:TextBox>
                                                                <asp:Button ID="BtnGo" runat="server" Text="Go" CssClass="btn" 
                                                                onclick="BtnGo_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" width="100%">
                                                <asp:GridView ID="gvComm" runat="server" AllowPaging="True" AllowSorting="True" AlternatingRowStyle-CssClass="fieldName"
                                                    AutoGenerateColumns="False" GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor"
                                                    HorizontalAlign="Center"  OnPageIndexChanging="gvComm_PageIndexChanging" 
                                                    PageSize="10" RowStyle-CssClass="gridbgcolor" Width="100%" EnableSortingAndPagingCallbacks="True"
                                                   
                                                    
                                                    Caption='<table width="100%" cellpadding="0" cellspacing="0"><tr><td align="left"><b>Location :Default</b></td></tr></table>' 
                                                    onsorting="gvComm_Sorting">
                                                    <RowStyle CssClass="gridbgcolor" />
                                                    <Columns>
                                                        <asp:BoundField DataField="SPARE"  HeaderText="Spare" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemStyle Width="60%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="qty" HeaderText="Quantity" >
                                                            <ItemStyle Width="20%" HorizontalAlign="Center" />
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
                                                    <HeaderStyle CssClass="fieldNamewithbgcolor" Font-Bold="True" />
                                                    <AlternatingRowStyle CssClass="fieldName" />
                                                </asp:GridView>
                                                <!-- Branch Listing -->
                                                <!-- End Branch Listing -->
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gvEng" runat="server" AutoGenerateColumns="false"  
                                                    Width="100%" GridLines="None" ShowHeader="False" 
                                                     >                                                 
                                                  
                                                    <Columns>
                                                         <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblloc" runat="server" Text='<%#Eval("LOC_NAME") %>'></asp:Label>
                                                            
                                                            </ItemTemplate>
                                                        
                                                        
                                                        </asp:TemplateField>
                                                    
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:GridView ID="grdkit" runat="server" AutoGenerateColumns="false"
                                                                 AlternatingRowStyle-CssClass="fieldName"
                                                    GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor"
                                                    HorizontalAlign="Center"   
                                                     RowStyle-CssClass="gridbgcolor" Width="100%" 
                                                                   > 
                                                                <Columns>
                                                                    <asp:BoundField ItemStyle-Width="60%" DataField="SPARE" HeaderText="Spare" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                                                     <asp:BoundField ItemStyle-Width="20%" DataField="qty" HeaderText="Quantity" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                
                                                                </Columns>
                                                                
                                                                
                                                               
                                                                </asp:GridView><br />
                                                            
                                                            </ItemTemplate>
                                                            
                                                        
                                                        
                                                        </asp:TemplateField>
                                                    
                                                       
                                                       
                                                    
                                                    </Columns>
                                                   
                                                  
                                                    <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td align="center" width="100%">
                                                <asp:GridView ID="grDefrecpt" runat="server" AllowPaging="True" AllowSorting="True"
                                                    AlternatingRowStyle-CssClass="fieldName" 
                                                    AutoGenerateColumns="False"                                                    
                                                    EnableSortingAndPagingCallbacks="True" GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor"
                                                    HorizontalAlign="Left"  
                                                   
                                                    PageSize="10" RowStyle-CssClass="gridbgcolor" Width="100%"
                                                    
                                                    Caption='<table  width="100%" cellpadding="0" cellspacing="0" ><tr><td align="left"><b>Location : Defective</b><br /><br /><b>Defective At receipt</b></td></tr></table>' 
                                                    onpageindexchanging="grDefrecpt_PageIndexChanging">
                                                    <RowStyle CssClass="gridbgcolor" />
                                                    <Columns>
                                                        <asp:BoundField DataField="sap_desc" HeaderText="Spare" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemStyle Width="60%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="qty" HeaderText="Quantity" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="20%" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                                    <img alt="" src='<%=ConfigurationManager.AppSettings["SIMSUserMessage"]%>' />
                                                                    <b>No Record found</b>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                    <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                    <AlternatingRowStyle CssClass="fieldName" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" width="100%">
                                                <asp:GridView ID="grddefcomplaint" runat="server" AllowPaging="True" AllowSorting="True"
                                                    AlternatingRowStyle-CssClass="fieldName" AutoGenerateColumns="False" Caption='<table  width="100%" cellpadding="0" cellspacing="0"
 ><tr><td align="left"><b>Returnable Defective Spares:</b></td></tr></table>'
                                                    EnableSortingAndPagingCallbacks="True" GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor"
                                                    HorizontalAlign="Left"
                                                    
                                                    PageSize="10" RowStyle-CssClass="gridbgcolor" Width="100%" 
                                                    onpageindexchanging="grddefcomplaint_PageIndexChanging">
                                                    <RowStyle CssClass="gridbgcolor" />
                                                    <Columns>
                                                        <asp:BoundField DataField="sap_desc" HeaderText="Spare" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemStyle Width="60%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="qty" HeaderText="Quantity" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="20%" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                                    <img alt="" src='<%=ConfigurationManager.AppSettings["SIMSUserMessage"]%>' />
                                                                    <b>No Record found</b>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                    <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                    <AlternatingRowStyle CssClass="fieldName" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" width="100%">
                                                <asp:GridView ID="grdcomplaintD" runat="server" AllowPaging="True" AllowSorting="True"
                                                    AlternatingRowStyle-CssClass="fieldName" AutoGenerateColumns="False" Caption='<table  width="100%" cellpadding="0" cellspacing="0"
 ><tr><td align="left"><b>Destroyable Defective Spares:</b></td></tr></table>'
                                                    EnableSortingAndPagingCallbacks="True" GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor"
                                                    HorizontalAlign="Left" 
                                                    
                                                    PageSize="10" RowStyle-CssClass="gridbgcolor" Width="100%" 
                                                    onpageindexchanging="grdcomplaintD_PageIndexChanging">
                                                    <RowStyle CssClass="gridbgcolor" />
                                                    <Columns>
                                                        <asp:BoundField DataField="sap_desc" HeaderText="Spare" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemStyle Width="60%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="qty" HeaderText="Quantity" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="20%" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                                    <img alt="" src='<%=ConfigurationManager.AppSettings["SIMSUserMessage"]%>' />
                                                                    <b>No Record found</b>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                    <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                    <AlternatingRowStyle CssClass="fieldName" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" width="100%">
                                                <asp:GridView ID="defstockmov" runat="server" AllowPaging="True" AllowSorting="True"
                                                    AlternatingRowStyle-CssClass="fieldName" AutoGenerateColumns="False" Caption='<table  width="100%" cellpadding="0" cellspacing="0"
 ><tr><td align="left"><b>Defective From Stock Movement</b></td></tr></table>'
                                                    EnableSortingAndPagingCallbacks="True" GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor"
                                                    HorizontalAlign="Left"  
                                                    
                                                    PageSize="10" RowStyle-CssClass="gridbgcolor" Width="100%" 
                                                    onpageindexchanging="defstockmov_PageIndexChanging">
                                                    <RowStyle CssClass="gridbgcolor" />
                                                    <Columns>
                                                        <asp:BoundField DataField="sap_desc" HeaderText="Spare" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemStyle Width="60%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="qty" HeaderText="Quantity" ItemStyle-HorizontalAlign ="Center">
                                                            <ItemStyle Width="20%" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                                    <img alt="" src='<%=ConfigurationManager.AppSettings["SIMSUserMessage"]%>' />
                                                                    <b>No Record found</b>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                    <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                    <AlternatingRowStyle CssClass="fieldName" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td Width="100%" >
                                                <asp:GridView ID="gvshrt" runat="server" AllowPaging="True" AllowSorting="True" AlternatingRowStyle-CssClass="fieldName"
                                                    AutoGenerateColumns="False" GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor"
                                                    HorizontalAlign="Left" 
                                                    PageSize="10" RowStyle-CssClass="gridbgcolor" EnableSortingAndPagingCallbacks="True"
                                                    Caption='<table  width="100%" cellpadding="0" cellspacing="0"
 ><tr><td align="left"><b>Location : Short</b></td></tr></table>' Width="100%" onpageindexchanging="gvshrt_PageIndexChanging">
                                                    <RowStyle CssClass="gridbgcolor" />
                                                    <Columns>
                                                        <asp:BoundField DataField="SPARE" HeaderText="Spare" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle Width="60%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="sap_invoice_no" HeaderText="Invoice" Visible="false">
                                                            <ItemStyle Width="10%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="sap_sales_order" HeaderText="Sales Order" Visible="false">
                                                            <ItemStyle Width="5%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="sims_indent_no" HeaderText="Sims Indent No." Visible="false">
                                                            <ItemStyle Width="5%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="QTY" HeaderText="Quantity" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="20%" />
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
                                                    <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                    <AlternatingRowStyle CssClass="fieldName" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
