<%@ Page Language="C#" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master" AutoEventWireup="true"
    CodeFile="InternalBillSummary.aspx.cs" Inherits="SIMS_Reports_ConfirmedPayment"
    Title="Internal Bill Summary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <link href="../../css/Popup.css" rel="stylesheet" type="text/css" />

    <script src="../../scripts/Popup.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
      function funReqDetail(strUrl) {

          window.open(strUrl, 'History', 'height=700,width=1050,left=20,top=30,Location=0,scrollbars=yes');
          return false;
      }
    </script>

    <script language="javascript" type="text/javascript">
        function SelectDate() 
        {
            var my_date
            var indate
            var dDate, mMonth, yYear
            
            var LoggedDateFrom, LoggedDateTo;
            BilldDateFrom = document.getElementById("<%= txtFromDate.ClientID %>").value;
            BilldDateTo = document.getElementById("<%= txtToDate.ClientID %>").value;

                if (BilldDateFrom.trim()=="" || BilldDateTo.trim()=="") 
                 {
                    alert('Please enter From date and To date.');
                    return false;
                }
                else 
                {
                    if (BilldDateFrom != "" && BilldDateTo != "") 
                    {
                        indate = new Date(BilldDateFrom);
                        my_date = new Date(BilldDateTo);
                        var m
                        var selm

                        m=(my_date.getFullYear()-indate.getFullYear())*12+(my_date.getMonth()-(indate.getMonth()-1));
                        selm=1;
                        
                        var msg
                        if(selm==1)
                        {
                            msg = "You can view the data only for previous twelve month. Please change your date selection.";
                        }                                        
                       if(m>12)
                       {
                            alert(msg);
                            return false;
                       }                        
                    }
                    }
            }
    </script>

    <script type="text/javascript" language="javascript">
     
         function BillNoValidation()
         {
            var contractorNo=document.getElementById("<%=txtContractorBillNo.ClientID %>");
            var grNo=document.getElementById("<%=txtGrNo.ClientID %>");
            var letterNumber = /^[0-9a-zA-Z]+$/; 
            if((contractorNo.value.trim()=="") || (!contractorNo.value.trim().match(letterNumber)))
            {
                document.getElementById('rfContractorno').style.display = 'block';
                contractorNo.focus();
                return false;
            }
            else{
            document.getElementById('rfContractorno').style.display = 'none';
            }
            if((grNo.value.trim()=="") || (!grNo.value.trim().match(letterNumber)))
            {
                document.getElementById('rfGrno').style.display = 'block';
                grNo.focus();
                return false;
            }
            else{
            document.getElementById('rfGrno').style.display = 'none';
            }
            return true;
         }
    </script>

    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <div id="blanket" style="display: none;">
            </div>
            <div id="popUpDiv" style="display: none;">
                <a href="javascript:void(0);" onclick="popup('popUpDiv')" class="close">
                    <img alt="" src="../../images/PopupClose.png" style="border: 0;" />
                </a>
                <div style="margin-left: 5px;">
                    <table>
                        <tr>
                            <td>
                                Contractor Bill No. :
                            </td>
                            <td>
                                <asp:TextBox ID="txtContractorBillNo" runat="server" Text="" CssClass="simpletxt1"
                                    MaxLength="150">
                                </asp:TextBox>
                            </td>
                            <td>
                                <font color="red" id="rfContractorno" style="display: none;">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                GR No. :
                            </td>
                            <td>
                                <asp:TextBox ID="txtGrNo" runat="server" Text="" CssClass="simpletxt1" MaxLength="150">
                                </asp:TextBox>
                            </td>
                            <td>
                                <font color="red" id="rfGrno" style="display: none;">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="2" align="left">
                                <asp:Button ID="btnSubmitBillDtls" runat="server" OnClick="btnSubmitBillDtls_Click"
                                    Text="Save" CssClass="btn" />
                            </td>
                        </tr>
                    </table>
                    <div>
                        <font color="red"><i>Only AlphaNumeric Fild</i></font></div>
                    <asp:HiddenField ID="hdnIBNSummaryId" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnIBNNo" runat="server" Value="0" />
                </div>
            </div>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Internal Bill Summary
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
                                        <tr style="display:none;">
                                            <td width="30%" align="right">
                                                Business Line
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlBusinessLine" AutoPostBack="true" runat="server" Width="175px"
                                                    CssClass="simpletxt1" ValidationGroup="editt" OnSelectedIndexChanged="ddlBusinessLine_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" align="right">
                                                Region
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlRegion" AutoPostBack="true" runat="server" CssClass="simpletxt1"
                                                    Width="175px" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" align="right">
                                                Branch
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="simpletxt1" Width="175px"
                                                    OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" align="right">
                                                Service Contractor
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DDlAsc" runat="server" CssClass="simpletxt1" Width="175px"
                                                    AutoPostBack="True" onselectedindexchanged="DDlAsc_SelectedIndexChanged">
                                                    <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" align="right">
                                                Divison
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlProductDivison" runat="server" Width="175px" CssClass="simpletxt1"
                                                    ValidationGroup="editt">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" align="right">
                                                Data From
                                            </td>
                                            <td align="left">
                                                <asp:TextBox runat="server" ID="txtFromDate" CssClass="txtboxtxt" ValidationGroup="ed"
                                                    MaxLength="10" />
                                                <asp:RequiredFieldValidator ID="reffrom" runat="server" Text="*" ControlToValidate="txtFromDate"
                                                    SetFocusOnError="true" ValidationGroup="ed"> </asp:RequiredFieldValidator>
                                                <cc1:CalendarExtender ID="CEFrom" runat="server" TargetControlID="txtFromDate">
                                                </cc1:CalendarExtender>
                                                Date To
                                                <asp:TextBox runat="server" ID="txtToDate" CssClass="txtboxtxt" ValidationGroup="ed"
                                                    MaxLength="10" />
                                                <asp:RequiredFieldValidator ID="refto" runat="server" Text="*" ControlToValidate="txtToDate"
                                                    SetFocusOnError="true" ValidationGroup="ed"> </asp:RequiredFieldValidator>
                                                <cc1:CalendarExtender ID="CETo" runat="server" TargetControlID="txtToDate">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" align="right">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                &nbsp;
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
                                                            <asp:Button Text="Search" Width="70px" ID="btnSearch" CssClass="btn" runat="server"
                                                                CausesValidation="True" ValidationGroup="editt" OnClick="btnSearch_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnExportExcel" Visible="false" runat="server" Width="114px" Text="Save To Excel"
                                                                CssClass="btn" OnClick="btnExportExcel_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <!-- For button portion update end -->
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0">
                            <tr>
                                <td align="left" class="MsgTDCount">
                                    Total Number of Records :
                                    <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="MsgTDCount">
                                    <asp:Label ID="lblMessage" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView PageSize="50" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridLines="both" AllowSorting="True"
                                        AutoGenerateColumns="false" ID="gvBillSummary" runat="server" Width="100%" HorizontalAlign="Left"
                                        AllowPaging="true" OnPageIndexChanging="gvBillSummary_PageIndexChanging">
                                        <RowStyle CssClass="gridbgcolor" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="24px" HeaderStyle-HorizontalAlign="Center"
                                                HeaderText="SNo">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex+1%>
                                                    </a>
                                                </ItemTemplate>
                                                <HeaderStyle Width="24px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"
                                                HeaderText="Internal Bill No" SortExpression="InternalBillNo">
                                                <ItemTemplate>
                                                    <a href="#" rel='<%#Eval("InternalBillNo")%>' onclick='return funReqDetail("../Reports/PIBNDetails.aspx?asc=<%#Eval("ASC_ID")%>&division=<%#Eval("ProductDivision_ID")%>&bill=<%#Eval("InternalBillNo")%>&trno=<%#Eval("TransactionNo")%>")'>
                                                        <%#Eval("InternalBillNo")%>
                                                    </a>
                                                </ItemTemplate>
                                                <HeaderStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="InternalBillDate" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Center" HeaderText="Internal Bill Date"></asp:BoundField>
                                            <asp:BoundField DataField="ActualAmmount" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Center" HeaderText="Amount"></asp:BoundField>
                                            <asp:BoundField DataField="ProductDivision_Desc" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Division"></asp:BoundField>
                                            <asp:BoundField DataField="ASC_Name" HeaderStyle-Width="160px" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Center" HeaderText="Service Contactor"></asp:BoundField>
                                            <asp:BoundField DataField="ContractorBillNo" HeaderStyle-Width="160px" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Center" HeaderText="Contr. Bill No"></asp:BoundField>
                                            <asp:BoundField DataField="GRNo" HeaderStyle-Width="160px" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Center" HeaderText="GR No"></asp:BoundField>
                                            <asp:TemplateField HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <a href="javascript:void(0);" onclick="popup('popUpDiv'+'@@@'+'<%#Eval("IBNSymmaryID")%>'+'@@@'+'<%#Eval("InternalBillNo")%>')"
                                                        title="Add Contractor Bill No. and GR No.">Add</a>
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
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
