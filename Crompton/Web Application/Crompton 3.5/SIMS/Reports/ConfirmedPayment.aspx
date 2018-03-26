<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true"
    CodeFile="ConfirmedPayment.aspx.cs" Inherits="SIMS_Reports_ConfirmedPayment"
    Title="Confirmed Payment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <%--   <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>--%>

  <script language="javascript" type="text/javascript">
      function funReqDetail(strUrl) {

          window.open(strUrl, 'History', 'height=700,width=1050,left=20,top=30,Location=0,scrollbars=yes');
          return false;
      }
    </script>

    <script type="text/javascript">

        function SelectDeselectAllCheckboxes(spanChk) {

            // Added as ASPX uses SPAN for checkbox  



            var oItem = spanChk.children;

            var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];

            xState = theBox.checked;

            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++)

                if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {

                //elm[i].click();  

                if (elm[i].checked != xState)

                    elm[i].click();

                //elm[i].checked=xState;  

            }

        }  
  
    </script>
     <script type="text/javascript" language="javascript">
         function validateCheckBoxes() {
             var gv = document.getElementById("<%=gvConfirmedPayment.ClientID%>");
             var rbs = gv.getElementsByTagName("input");
             var flag = 0;
             for (var i = 0; i < rbs.length; i++) {
                 if (rbs[i].type == "checkbox") {
                     if (rbs[i].checked) {
                         flag = 1; break;
                     }
                 }
             }
             if (flag == 0) {
                 alert("Please select the box for download.");
                 return false;
             }
         }
    </script>

    <table width="100%">
        <tr>
            <td class="headingred">
                Confirmed Payments
            </td>
            <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                    <ProgressTemplate>
                        <img src="<%=ConfigurationManager.AppSettings["SIMSAjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                </asp:UpdateProgress>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right" style="padding-right: 10px">
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
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0">
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
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td width="30%" align="right">
                                        Region<font color='red'>*</font>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlRegion" AutoPostBack="true" runat="server" CssClass="simpletxt1"
                                            Width="175px" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvRegion" runat="server" SetFocusOnError="true"
                                            ErrorMessage="Branch is required." InitialValue="0" ControlToValidate="ddlRegion"
                                            ValidationGroup="editt" ToolTip="Branch is required." Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="30%" align="right">
                                        Branch<font color='red'>*</font>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="simpletxt1" Width="175px"
                                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" SetFocusOnError="true"
                                            ErrorMessage="Branch is required." InitialValue="0" ControlToValidate="ddlBranch"
                                            ValidationGroup="editt" ToolTip="Branch is required." Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="30%" align="right">
                                        Service Contractor<font color='red'>*</font>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DDlAsc" runat="server" CssClass="simpletxt1" Width="175px"
                                            OnSelectedIndexChanged="DDlAsc_SelectedIndexChanged" AutoPostBack="True">
                                         <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem> 
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RFVScName" runat="server" SetFocusOnError="true"
                                            ErrorMessage="Service Contractor Name is required." InitialValue="0" ControlToValidate="DDlAsc"
                                            ValidationGroup="editt" ToolTip="Service Contractor Name is required." Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="30%" align="right">
                                        Divison<font color='red'>*</font>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlProductDivison" runat="server" Width="175px" CssClass="simpletxt1"
                                            ValidationGroup="editt">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RFRegionDesc" runat="server" ControlToValidate="ddlProductDivison"
                                            ErrorMessage="Division is required." InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="editt" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="30%" align="right">
                                        &nbsp;</td>
                                    <td align="left">
                                        <b>OR</b></td>
                                </tr>
                                <tr>
                                    <td width="30%" align="right">
                                        Select Data Range</td>
                                    <td align="left">
                                      <asp:TextBox runat="server" ID="txtFromDate" CssClass="txtboxtxt" ValidationGroup="ed"  MaxLength="10" />
                                      <asp:RequiredFieldValidator ID="reffrom" runat="server" Text="*" ControlToValidate="txtFromDate" SetFocusOnError="true" ValidationGroup="ed"> </asp:RequiredFieldValidator>
                                      <cc1:CalendarExtender ID="CEFrom" runat="server" TargetControlID="txtFromDate">
                                      </cc1:CalendarExtender>
                                    To
                                    <asp:TextBox runat="server" ID="txtToDate" CssClass="txtboxtxt" ValidationGroup="ed"
                                        MaxLength="10" />
                                        <asp:RequiredFieldValidator ID="refto" runat="server" Text="*" ControlToValidate="txtToDate" SetFocusOnError="true" ValidationGroup="ed"> </asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CETo" runat="server" TargetControlID="txtToDate">
                                    </cc1:CalendarExtender>
                                         <asp:Button ID="btnGo" runat="server" CausesValidation="True" 
                                             CssClass="btn" OnClick="btnGo_Click" Text="Go" ValidationGroup="ed" 
                                             Width="70px" />
                                 </td>
                                </tr>
                                <tr>
                                    <td width="30%" align="right">
                                        &nbsp;</td>
                                    <td align="left">
                                        &nbsp;</td>
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
                                                    <%-- <asp:Button Text="Add" Width="70px" CssClass="btn" ID="imgBtnAdd" runat="server"
                                                                CausesValidation="True" ValidationGroup="editt" OnClick="imgBtnAdd_Click" />--%>
                                                    <asp:Button Text="Search" Width="70px" ID="btnSearch" CssClass="btn" runat="server"
                                                        CausesValidation="True" ValidationGroup="editt" OnClick="btnSearch_Click" />
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
                        <td align="left">
                            <div id="divConfirmedPayments" runat="server" visible="false" style="text-align: left">
                                <!-- Service Contractor Listing   -->
                                <b>(A) Summary list of payments confirmed:</b><br />
                                <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                    HeaderStyle-CssClass="fieldNamewithbgcolor" GridLines="both" AllowSorting="True"
                                    AutoGenerateColumns="false" ID="gvConfirmedPayment" runat="server" Width="100%"
                                    HorizontalAlign="Left" EnableSortingAndPagingCallbacks="false" AllowPaging="True"
                                    OnPageIndexChanging="gvConfirmedPayment_PageIndexChanging" OnRowCommand="gvConfirmedPayment_RowCommand"
                                    OnSorting="gvConfirmedPayment_Sorting" OnRowDataBound="gvConfirmedPayment_RowDataBound">
                                    <RowStyle CssClass="gridbgcolor" />
                                    <Columns>
                                        <%-- PaymentRecID   Created_By Created_Date Modified_By Modified_Date      --%>
                                        <asp:BoundField DataField="Branch_Name" SortExpression="Branch_Name" HeaderStyle-Width="60px"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Branch">
                                            <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderStyle-Width="150px" HeaderText="Service Contactor" SortExpression="ASC_Name">
                                            <ItemTemplate>
                                                <%#Eval("ASC_Name")%>
                                                <asp:HiddenField ID="hdnASCID" runat="server" Value=' <%#Eval("ASC_ID")%>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="50px" HeaderText="Division" SortExpression="ProductDivision_Desc">
                                            <ItemTemplate>
                                                <%#Eval("ProductDivision_Desc")%>
                                                <asp:HiddenField ID="hdnDivisionID" runat="server" Value='<%#Eval("ProductDivision_ID")%>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="150px" HeaderText="Transaction No" SortExpression="TransactionNo">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkTransactionNo" runat="server" CommandName="Trans" CommandArgument='<%#Eval("TransactionNo")%>'><%#Eval("TransactionNo")%></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SAPTransactionNo" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="SAPTransactionNo">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TransactionDate" SortExpression="TransactionDate" HeaderStyle-Width="100px"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Transaction Date">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:TemplateField SortExpression="Amount" HeaderText="Amount" HeaderStyle-Width="60px"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField SortExpression="Download" HeaderText="Download" HeaderStyle-Width="60px"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDownload" runat="server" CommandArgument='<%#Eval("TextfileName")%>'
                                                    CommandName="Download" Text="Download"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField SortExpression="IsDownloaded" HeaderText="Downloaded" HeaderStyle-Width="60px"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldownloaded" runat="server" Text='<%#Eval("IsDownloaded")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField  HeaderText="Select" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" Visible="false" >
                                            <HeaderTemplate>
                                                <input id="chkAll" onclick="javascript:SelectDeselectAllCheckboxes(this);" runat="server"
                                                    type="checkbox" />
                                                <asp:LinkButton ID="btnDownload" runat="server" Text="Download All" OnClientClick="javascript:return validateCheckBoxes()"></asp:LinkButton><%--OnClick="btnDownload_download" --%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                                <!-- End Service Contractor Listing -->
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblMessage" Font-Bold="true" runat="server" ForeColor="Red" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="divTransactionDetail" runat="server" visible="false" style="text-align: left">
                                <br />
                                <br />
                                <br />
                                <b>(B) Details of payments confirmed:</b><br />
                                <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                    HeaderStyle-CssClass="fieldNamewithbgcolor" GridLines="both" AllowSorting="True"
                                    AutoGenerateColumns="false" ID="gvTransactionDetail" runat="server" Width="100%"
                                    HorizontalAlign="Left">
                                    <RowStyle CssClass="gridbgcolor" />
                                    <Columns>
                                        <%-- PaymentRecID   Created_By Created_Date Modified_By Modified_Date      --%>
                                        <asp:BoundField DataField="ASC_Name" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left" HeaderText="Service Contactor">
                                            <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ProductDivision_Desc" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left" HeaderText="Division">
                                            <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TransactionNo" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left" HeaderText="Transaction No">
                                            <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"
                                                HeaderText="Internal Bill No" SortExpression="InternalBillNo">
                                                <ItemTemplate>
                                                    <a href="#" rel='<%#Eval("InternalBillNo")%>' onclick='return funReqDetail("../Reports/ConfirmedPaymentsInternalBillClaims.aspx?asc=<%#Eval("ASC_ID")%>&division=<%#Eval("ProductDivision_ID")%>&bill=<%#Eval("InternalBillNo")%>&trno=<%#Eval("TransactionNo")%>")'>
                                                        <%#Eval("InternalBillNo")%>
                                                    </a>
                                                    
                                                </ItemTemplate>
                                                <HeaderStyle Width="150px" />
                                            </asp:TemplateField>
                                        
                                       
                                        <asp:BoundField DataField="InternalBillDate" HeaderStyle-Width="160px" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left" HeaderText="Internal Bill Date">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ActualAmmount" HeaderStyle-Width="160px" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left" HeaderText="Amount">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
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
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <%-- PaymentRecID   Created_By Created_Date Modified_By Modified_Date      --%> 
</asp:Content>
