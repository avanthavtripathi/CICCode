<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="DefectAnalysisRpt.aspx.cs" Inherits="Reports_DefectAnalysisRpt" Title="Defect Analysis Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="PendingSerRegReport" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function SelectDate(type) {
            var my_date
            var indate
            var dDate, mMonth, yYear
            var prdcode = document.getElementById("ctl00_MainConHolder_ddlProductDivison").value;
            var scCode = document.getElementById("ctl00_MainConHolder_ddlSerContractor").value;
            var LoggedDateFrom, LoggedDateTo, SLADateFrom, SLADateTo, DApproveFrom, DApproveTo;
            LoggedDateFrom = document.getElementById("ctl00_MainConHolder_txtLoggedDateFrom").value;
            LoggedDateTo = document.getElementById("ctl00_MainConHolder_txtLoggedDateTo").value;

            SLADateFrom = document.getElementById("ctl00_MainConHolder_txtSLADateFrom").value;
            SLADateTo = document.getElementById("ctl00_MainConHolder_txtSLADateTo").value;

            DApproveFrom = document.getElementById("ctl00_MainConHolder_txtDefectFrom").value;
            DApproveTo = document.getElementById("ctl00_MainConHolder_txtDefectTo").value;


            if ((LoggedDateFrom != "" || LoggedDateTo != "") && type == 'logged') {
                document.getElementById("ctl00_MainConHolder_txtSLADateFrom").value = "";
                document.getElementById("ctl00_MainConHolder_txtSLADateTo").value = "";

                document.getElementById("ctl00_MainConHolder_txtDefectFrom").value = "";
                document.getElementById("ctl00_MainConHolder_txtDefectTo").value = "";
            }

            if ((SLADateFrom != "" || SLADateTo != "") && type == 'sla') {
                document.getElementById("ctl00_MainConHolder_txtLoggedDateFrom").value = "";
                document.getElementById("ctl00_MainConHolder_txtLoggedDateTo").value = "";

                document.getElementById("ctl00_MainConHolder_txtDefectFrom").value = "";
                document.getElementById("ctl00_MainConHolder_txtDefectTo").value = "";
            }

            if ((DApproveFrom != "" || DApproveTo != "") && type == 'defect') {
                document.getElementById("ctl00_MainConHolder_txtSLADateFrom").value = "";
                document.getElementById("ctl00_MainConHolder_txtSLADateTo").value = "";

                document.getElementById("ctl00_MainConHolder_txtLoggedDateFrom").value = "";
                document.getElementById("ctl00_MainConHolder_txtLoggedDateTo").value = "";

            }

            if (type == "validate") {
                if (!((LoggedDateFrom != "" && LoggedDateTo != "") || (SLADateFrom != "" && SLADateTo != "") || (DApproveFrom != "" && DApproveTo != ""))) {
                    alert('Please enter at least one date.');
                    return false;
                }
                else {
                    if (LoggedDateFrom != "" && LoggedDateTo != "") {
                        indate = new Date(LoggedDateFrom);
                        my_date = new Date(LoggedDateTo);
                        var m
                        var selm

                        m = parseInt(indate.getMonth());
                        if(scCode!=0 && scCode!="")
                        {
                            m=(my_date.getFullYear()-indate.getFullYear())*12+(my_date.getMonth()-(indate.getMonth()-1));
                            selm=4;
                        }
                        else if (prdcode == 0) {
                            m = m + 0
                            selm = 1
                        }
                        else {
                            selm = 3
                            m = (my_date.getFullYear() - indate.getFullYear()) * 12 + (my_date.getMonth() - (indate.getMonth() - 1));
                        }

                        var msg
                        if (selm == 1) {
                            msg = "You can view the data only for current month. Please change your date selection.\n Or select product division to view the data for previous 3 months.";
                        }
                        else if(selm==4)
                        {
                            msg = "You can view the data only for previous twelve month. Please change your date selection.";
                        }
                        else {
                            msg = "You can view the data only for previous three month. Please change your date selection.";
                        }
                        if ((scCode != 0 || scCode != "") && m > 12 && indate.getFullYear() > my_date.getFullYear()) {
                            alert(msg);
                            return false;
                        }
                        else if (m > 3) {
                        alert(msg);
                        return false;
                         }
                        
                        
//                       if(m>12)
//                       {
//                            alert(msg);
//                            return false;
//                       }
//                        if (indate.getFullYear() < my_date.getFullYear() && (scCode==0 || scCode=="")) {
//                            alert(msg);
//                            return false;
//                        }


//                        if (parseInt(m) < parseInt(my_date.getMonth()) && (scCode==0 || scCode=="")) {
//                            alert(msg);
//                            return false;
//                        }
                    }
                    if (SLADateFrom != "" && SLADateTo != "") {
                        indate = new Date(SLADateFrom);
                        my_date = new Date(SLADateTo);
                        var m, selm
                        m = parseInt(indate.getMonth());
                        if(scCode!=0 && scCode!="")
                        {
                            m=(my_date.getFullYear()-indate.getFullYear())*12+(my_date.getMonth()-(indate.getMonth()-1));
                            selm=4;
                        }
                        else if (prdcode == 0) {
                            m = m + 0
                            selm = 1
                        }
                        else {
                            selm = 3
                            m = (my_date.getFullYear() - indate.getFullYear()) * 12 + (my_date.getMonth() - (indate.getMonth() - 1));
                        }

                        var msg
                        if (selm == 1) {
                            msg = "You can view the data only for current month. Please change your date selection.\n Or select product division to view the data for previous 3 months.";
                        }
                        else if(selm==4)
                        {
                            msg = "You can view the data only for previous twelve month. Please change your date selection.";
                        }
                        else {
                            msg = "You can view the data only for previous three month. Please change your date selection.";
                        }
                        if ((scCode != 0 || scCode != "") && m > 12 && indate.getFullYear() > my_date.getFullYear()) {
                            alert(msg);
                            return false;
                        }
                        else if (m > 3) {
                            alert(msg);
                            return false;
                        }
//                      if(m>12)
//                       {
//                            alert(msg);
//                            return false;
//                       }
//                        if (indate.getFullYear() < my_date.getFullYear() && (scCode==0 || scCode=="")) {
//                            alert(msg);
//                            return false;
//                        }


//                        if (parseInt(m) < parseInt(my_date.getMonth()) && (scCode==0 || scCode=="")) {
//                            alert(msg);
//                            return false;
//                        }
                    }
                    if (DApproveFrom != "" && DApproveTo != "") {
                        indate = new Date(DApproveFrom);
                        my_date = new Date(DApproveTo);
                        var m, selm
                        m = parseInt(indate.getMonth());
                        if(scCode!=0 && scCode!="")
                        {
                            m=(my_date.getFullYear()-indate.getFullYear())*12+(my_date.getMonth()-(indate.getMonth()-1));
                            selm=4;
                        }
                        else if (prdcode == 0) {
                            m = m + 0
                            selm = 1
                        }
                        else {
                            m = (my_date.getFullYear() - indate.getFullYear()) * 12 + (my_date.getMonth() - (indate.getMonth() - 1));
                            selm = 3
                        }
                        var msg
                        if (selm == 1) {
                            msg = "You can view the data only for current month. Please change your date selection.\n Or select product division to view the data for previous 3 months.";
                        }
                        else if(selm==4)
                        {
                            msg = "You can view the data only for previous twelve month. Please change your date selection.";
                        }
                        else {
                            msg = "You can view the data only for previous three month. Please change your date selection.";
                        }
                        if ((scCode != 0 || scCode == "") && m > 12 && indate.getFullYear() > my_date.getFullYear()) {
                            alert(msg);
                            return false;
                        }
                        else if (m > 3) {
                            alert(msg);
                            return false;
                        }
//                         if(m>12)
//                       {
//                            alert(msg);
//                            return false;
//                       }
//                        if (indate.getFullYear() < my_date.getFullYear() && (scCode==0 || scCode=="")) {
//                            alert(msg);
//                            return false;
//                        }


//                        if (parseInt(m) < parseInt(my_date.getMonth()) && (scCode==0 || scCode=="")) {
//                            alert(msg);
//                            return false;
//                        }

                    }
                }
            }

        }
    </script>

    <asp:UpdatePanel ID="updatepnl" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportExcel" />
        </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Defect Analysis Report
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px" align="center" colspan="2">
                        <table width="98%" border="0">
                            <tr>
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
                                <td align="left">
                                    <asp:DropDownList ID="ddlRegion" runat="server" Width="175px" CssClass="simpletxt1"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Branch
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlBranch" runat="server" Width="175px" CssClass="simpletxt1"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="40%">
                                    ASC
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlSerContractor" runat="server" Width="175px" CssClass="simpletxt1">
                                        <asp:ListItem Text="Select" Value="0" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Product Divison
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlProductDivison" runat="server" Width="175px" CssClass="simpletxt1"
                                        ValidationGroup="editt" AutoPostBack="True" OnSelectedIndexChanged="ddlProductDivison_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Product Line
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlProductLine" runat="server" Width="175px" CssClass="simpletxt1"
                                        ValidationGroup="editt" AutoPostBack="True" OnSelectedIndexChanged="ddlProductLine_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr runat="server" id="trPGroup">
                                <td width="30%" align="right">
                                    Product Group
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlProductGroup" runat="server" CssClass="simpletxt1" Width="175px"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlProductGroup_SelectedIndexChanged"
                                        ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Product
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlProduct" runat="server" CssClass="simpletxt1" Width="175px"
                                        ValidationGroup="editt">
                                    </asp:DropDownList>
                                    <asp:GridView ID="gvComm" runat="server" AllowPaging="false" 
                                        AllowSorting="False" AlternatingRowStyle-CssClass="fieldName" 
                                        AutoGenerateColumns="False" GridGroups="both" 
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" HorizontalAlign="Left" 
                                        OnPageIndexChanging="gvComm_PageIndexChanging" 
                                        OnRowDataBound="gvComm_RowDataBound" OnSorting="gvComm_Sorting" 
                                        PagerStyle-HorizontalAlign="Center" PageSize="10" 
                                        RowStyle-CssClass="gridbgcolor" Visible="true" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="40px" HeaderText="Sno">
                                                <ItemTemplate>
                                                    <%#Eval("RowId")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" 
                                                HeaderText="Complaint NO" ItemStyle-HorizontalAlign="Left" 
                                                SortExpression="Complaint_Split">
                                                <ItemTemplate>
                                                    <a href="Javascript:void(0);" 
                                                        onclick='funCommonPopUpReport(<%#Eval("BaseLineId")%>)'>
                                                    <%#Eval("Complaint_Split")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" 
                                                HeaderText="Reported Date" ItemStyle-HorizontalAlign="Left" 
                                                SortExpression="Reporting_Date">
                                                <ItemTemplate>
                                                    <%#Eval("Reporting_Date")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="SLADate" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderStyle-Width="100px" HeaderText="SLA Date" 
                                                ItemStyle-HorizontalAlign="Left" SortExpression="SLADate" />
                                            <asp:BoundField DataField="Closing_Date" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderStyle-Width="100px" HeaderText="Closing Date" 
                                                ItemStyle-HorizontalAlign="Left" SortExpression="Closing_Date" />
                                            <asp:BoundField DataField="InvoiceDate" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderStyle-Width="100px" HeaderText="Invoice Date" 
                                                ItemStyle-HorizontalAlign="Left" SortExpression="InvoiceDate" />
                                            <asp:BoundField DataField="Region_Desc" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Region" ItemStyle-HorizontalAlign="Left" 
                                                SortExpression="Region_Desc" />
                                            <asp:BoundField DataField="Branch_Name" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Branch" ItemStyle-HorizontalAlign="Left" 
                                                SortExpression="Branch_Name" />
                                            <asp:BoundField DataField="SC_Name" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="SC Name" ItemStyle-HorizontalAlign="Left" 
                                                SortExpression="SC_Name" />
                                            <asp:BoundField DataField="Customer_Name" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Customer Name" ItemStyle-HorizontalAlign="Left" 
                                                SortExpression="Customer_Name" />
                                            <asp:BoundField DataField="COMPANY_NAME" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Company Name" ItemStyle-HorizontalAlign="Left" 
                                                SortExpression="COMPANY_NAME" />
                                            <asp:BoundField DataField="ProductDivision_Desc" 
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Division" 
                                                ItemStyle-HorizontalAlign="Left" SortExpression="ProductDivision_Desc" />
                                            <asp:BoundField DataField="ProductLine_Desc" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Product Line" ItemStyle-HorizontalAlign="Left" 
                                                SortExpression="ProductLine_Desc" />
                                            <asp:BoundField DataField="ProductGroup_Desc" 
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Product Group" 
                                                ItemStyle-HorizontalAlign="Center" SortExpression="ProductGroup_Desc" />
                                            <asp:BoundField DataField="Product_Code" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Product Code" ItemStyle-HorizontalAlign="Center" 
                                                SortExpression="Product_Code" />
                                            <asp:BoundField DataField="Product_Desc" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Product Desc" ItemStyle-HorizontalAlign="Left" 
                                                SortExpression="Product_Desc" />
                                            <asp:BoundField DataField="MANF_PERIOD" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="MFG Period" ItemStyle-HorizontalAlign="Left" 
                                                SortExpression="MANF_PERIOD" />
                                            <asp:BoundField DataField="PRODUCT_SR_NO" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="PRD_SR_NO" ItemStyle-HorizontalAlign="Left" 
                                                SortExpression="PRODUCT_SR_NO" />
                                            <asp:BoundField DataField="MFGUNIT" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="MFG Unit" ItemStyle-HorizontalAlign="Center" 
                                                SortExpression="MFGUNIT" />
                                            <asp:BoundField DataField="BLADE_VENDOR" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="BLADE VENDOR" ItemStyle-HorizontalAlign="Left" 
                                                SortExpression="BLADE_VENDOR" />
                                            <asp:BoundField DataField="MAKE_CAP" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Make Cap" ItemStyle-HorizontalAlign="Left" 
                                                SortExpression="MAKE_CAP" />
                                            <asp:BoundField DataField="WindingUnit" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Winding Vendor" ItemStyle-HorizontalAlign="Left" 
                                                SortExpression="WindingUnit" />
                                            <asp:BoundField DataField="Defect_Category_desc" 
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Defect Category Name" 
                                                ItemStyle-HorizontalAlign="Center" SortExpression="Defect_Category_desc" />
                                            <asp:BoundField DataField="Defect_desc" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Defect Name" ItemStyle-HorizontalAlign="Left" 
                                                SortExpression="Defect_desc" />
                                            <asp:BoundField DataField="NUM_OF_DEF" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="NUM of Defects" ItemStyle-HorizontalAlign="Center" 
                                                SortExpression="NUM_OF_DEF" />
                                            <asp:BoundField DataField="REMARK" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Defect Remark" ItemStyle-HorizontalAlign="Left" 
                                                SortExpression="REMARK" />
                                            <asp:BoundField DataField="OBSERV" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Closure Remark" ItemStyle-HorizontalAlign="Center" 
                                                SortExpression="OBSERV" />
                                            <asp:BoundField DataField="APPL" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="APPL" ItemStyle-HorizontalAlign="Left" SortExpression="APPL" />
                                            <asp:BoundField DataField="AppInstrumentname" 
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Application Instrument Name" 
                                                ItemStyle-HorizontalAlign="Center" SortExpression="AppInstrumentname" 
                                                Visible="false" />
                                            <asp:BoundField DataField="InstrumentMfgName" 
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Instrument Manufacturer Name" 
                                                ItemStyle-HorizontalAlign="Center" SortExpression="InstrumentMfgName" 
                                                Visible="false" />
                                            <asp:BoundField DataField="InstrumentDetails" 
                                                HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Instrument Details (Current,Voltage,Capacity Rating &amp; Output Range)" 
                                                ItemStyle-HorizontalAlign="Center" SortExpression="InstrumentDetails" 
                                                Visible="false" />
                                            <asp:BoundField DataField="LOAD" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="LOAD" ItemStyle-HorizontalAlign="Center" SortExpression="LOAD" />
                                            <asp:BoundField DataField="EXCISE" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="EXCISE" ItemStyle-HorizontalAlign="Left" SortExpression="EXCISE" />
                                            <asp:BoundField DataField="AVR_SRNO" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="AVR_SRNO" ItemStyle-HorizontalAlign="Center" 
                                                SortExpression="AVR_SRNO" />
                                            <asp:BoundField DataField="RATING" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="RATING" ItemStyle-HorizontalAlign="Left" SortExpression="RATING" />
                                            <asp:BoundField DataField="RATING_STATUS" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="RATING_STATUS" ItemStyle-HorizontalAlign="Center" 
                                                SortExpression="RATING_STATUS" />
                                            <asp:BoundField DataField="MaterialCost" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Material Cost" ItemStyle-HorizontalAlign="Left" 
                                                SortExpression="MaterialCost" />
                                            <asp:BoundField DataField="ServiceCost" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Service Cost" ItemStyle-HorizontalAlign="Center" 
                                                SortExpression="ServiceCost" />
                                            <asp:BoundField DataField="Callstage" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="Complaint Status" ItemStyle-HorizontalAlign="Center" 
                                                SortExpression="Callstage" />
                                            <asp:BoundField DataField="ComplaintAge" HeaderStyle-HorizontalAlign="Left" 
                                                HeaderText="ComplaintAge" ItemStyle-HorizontalAlign="Center" 
                                                SortExpression="ComplaintAge" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td align="center" 
                                                        style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                                        <img alt="" src='<%=ConfigurationManager.AppSettings["UserMessage"]%>' /> <b>No 
                                                        Record found</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Defect Category
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlDefectCategory" runat="server" Width="350px" CssClass="simpletxt1"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlDefectCategory_SelectedIndexChanged"
                                        ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Defect
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlDefect" runat="server" Width="350px" CssClass="simpletxt1"
                                        ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Logged Date
                                </td>
                                <td align="left">
                                    <asp:TextBox runat="server" ID="txtLoggedDateFrom" CssClass="txtboxtxt" ValidationGroup="editt"
                                        MaxLength="10" />
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" Type="Date" Operator="DataTypeCheck"
                                        ControlToValidate="txtLoggedDateFrom" Display="none" ValidationGroup="editt"
                                        SetFocusOnError="true"></asp:CompareValidator>
                                    <cc1:CalendarExtender ID="CEFrom" runat="server" TargetControlID="txtLoggedDateFrom">
                                    </cc1:CalendarExtender>
                                    To
                                    <asp:TextBox runat="server" ID="txtLoggedDateTo" CssClass="txtboxtxt" ValidationGroup="editt"
                                        MaxLength="10" />
                                    <asp:CompareValidator ID="CompareValidator7" runat="server" Type="Date" Operator="DataTypeCheck"
                                        ControlToValidate="txtLoggedDateTo" Display="none" ValidationGroup="editt" SetFocusOnError="true"></asp:CompareValidator>
                                    <cc1:CalendarExtender ID="CETo" runat="server" TargetControlID="txtLoggedDateTo">
                                    </cc1:CalendarExtender>
                                    <asp:Label ID="lblDateErr" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    SLA Date
                                </td>
                                <td align="left">
                                    <asp:TextBox runat="server" ID="txtSLADateFrom" CssClass="txtboxtxt" ValidationGroup="editt"
                                        MaxLength="10" />
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" Type="Date" Operator="DataTypeCheck"
                                        ControlToValidate="txtSLADateFrom" Display="none" ValidationGroup="editt" SetFocusOnError="true"></asp:CompareValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSLADateFrom">
                                    </cc1:CalendarExtender>
                                    To
                                    <asp:TextBox runat="server" ID="txtSLADateTo" CssClass="txtboxtxt" ValidationGroup="editt"
                                        MaxLength="10" />
                                    <asp:CompareValidator ID="CompareValidator3" runat="server" Type="Date" Operator="DataTypeCheck"
                                        ControlToValidate="txtSLADateTo" Display="none" ValidationGroup="editt" SetFocusOnError="true"></asp:CompareValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtSLADateTo">
                                    </cc1:CalendarExtender>
                                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Defect Approved Date
                                </td>
                                <td align="left">
                                    <asp:TextBox runat="server" ID="txtDefectFrom" CssClass="txtboxtxt" ValidationGroup="editt"
                                        MaxLength="10" />
                                    <asp:CompareValidator ID="CompareValidator4" runat="server" Type="Date" Operator="DataTypeCheck"
                                        ControlToValidate="txtDefectFrom" Display="none" ValidationGroup="editt" SetFocusOnError="true"></asp:CompareValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDefectFrom">
                                    </cc1:CalendarExtender>
                                    To
                                    <asp:TextBox runat="server" ID="txtDefectTo" CssClass="txtboxtxt" ValidationGroup="editt"
                                        MaxLength="10" />
                                    <asp:CompareValidator ID="CompareValidator5" runat="server" Type="Date" Operator="DataTypeCheck"
                                        ControlToValidate="txtDefectTo" Display="none" ValidationGroup="editt" SetFocusOnError="true"></asp:CompareValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtDefectTo">
                                    </cc1:CalendarExtender>
                                    <asp:Label ID="Label2" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                </td>
                                <td align="left">
                                    <br />
                                    <asp:Button Width="70px" Text="Search" CssClass="btn" ValidationGroup="editt" CausesValidation="true"
                                        ID="btnSearch" runat="server" OnClick="btnSearch_Click" />
                                    &nbsp;
                                    <asp:Button ID="btnExportExcel" Visible="false" runat="server" Width="114px" Text="Save To Excel"
                                        CssClass="btn" OnClick="btnExportExcel_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="MsgTDCount">
                                    Total Number of Complaints :
                                    <asp:Label ID="lblRowCount" CssClass="MsgTotalCount" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                                        runat="server" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table width="100%" border="0">
                            <tr>
                                <td>
                                <div style="width:1000px; overflow:scroll;">
                                    <!-- End Action Listing -->
                                </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <table runat="server" id="tblpage" visible="false" border="0" cellpadding="5" cellspacing="5"
                                        width="100%">
                                        <tr>
                                            <td width="30%" align="right">
                                                <asp:Panel ID="pnlPre" Width="2%" Visible="false" runat="server">
                                                    <asp:LinkButton runat="server" ID="lnkPre" Text="Previous" OnClick="lnkPre_Click"></asp:LinkButton>
                                                </asp:Panel>
                                            </td>
                                            <td width="5%">
                                                <asp:Panel ID="Panel1" runat="server">
                                                </asp:Panel>
                                            </td>
                                            <td width="2%" align="left">
                                                <asp:Panel ID="pnlNext" Width="5%" Visible="false" runat="server">
                                                    <asp:LinkButton runat="server" ID="lnkNext" Text="Next" OnClick="lnkNext_Click"></asp:LinkButton>
                                                </asp:Panel>
                                            </td>
                                            <td width="30%" align="left">
                                                <asp:Panel ID="Panel2" Width="100%" Visible="true" runat="server">
                                                    <asp:TextBox ID="txtgo" runat="server" Width="30"></asp:TextBox>&nbsp;<asp:RangeValidator
                                                        ID="RangeValidator1" runat="server" ControlToValidate="txtgo" Display="Dynamic"
                                                        ErrorMessage="*" MaximumValue="65000" MinimumValue="1" Type="Integer" ValidationGroup="go"></asp:RangeValidator><asp:RequiredFieldValidator
                                                            ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtgo" Display="Dynamic"
                                                            ErrorMessage="*" ValidationGroup="go"></asp:RequiredFieldValidator>&nbsp;<asp:Button
                                                                Width="40px" Text="Go" CssClass="btn" ValidationGroup="go" CausesValidation="true"
                                                                ID="Button1" runat="server" OnClick="Button1_Click" />
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <br />
                                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text="" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <!-- Excel Grid -->
                        <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                            HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="false"
                            PagerStyle-HorizontalAlign="Center" AutoGenerateColumns="False" ID="gvExcel"
                            runat="server" Width="100%" HorizontalAlign="Left" Visible="true" OnRowDataBound="gvExcel_RowDataBound">
                            <Columns>
                                <%-- <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                    <HeaderStyle Width="40px" />
                                </asp:BoundField>--%>
                                <asp:TemplateField HeaderText="Sno" HeaderStyle-Width="40px">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Complaint_Split" SortExpression="Complaint_Split" HeaderStyle-Width="100px"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Complaint NO">
                                </asp:BoundField>
                                <asp:BoundField DataField="Reporting_Date" SortExpression="Reporting_Date" HeaderStyle-Width="100px"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Reported Date">
                                </asp:BoundField>
                                <asp:BoundField DataField="SLADate" SortExpression="SLADate" HeaderStyle-Width="100px"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="SLA Date">
                                </asp:BoundField>
                                <asp:BoundField DataField="Closing_Date" SortExpression="Closing_Date" HeaderStyle-Width="100px"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Closing Date">
                                </asp:BoundField>
                                <asp:BoundField DataField="InvoiceDate" SortExpression="InvoiceDate" HeaderStyle-Width="100px"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Invoice Date">
                                            </asp:BoundField>
                                <asp:BoundField DataField="Region_Desc" SortExpression="Region_Desc" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Region"></asp:BoundField>
                                <asp:BoundField DataField="Branch_Name" SortExpression="Branch_Name" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Branch"></asp:BoundField>
                                <asp:BoundField DataField="SC_Name" SortExpression="SC_Name" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="SC Name"></asp:BoundField>
                                <asp:BoundField DataField="CutomerDetails" SortExpression="CutomerDetails" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Customer Details"></asp:BoundField>
                                     <asp:BoundField DataField="COMPANY_NAME" SortExpression="COMPANY_NAME" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Company Name"></asp:BoundField>
                                <asp:BoundField DataField="ProductDivision_Desc" SortExpression="ProductDivision_Desc"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Division">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ProductLine_Desc" SortExpression="ProductLine_Desc" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Product Line"></asp:BoundField>
                                <asp:BoundField DataField="ProductGroup_Desc" SortExpression="ProductGroup_Desc"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Left" HeaderText="Product Group">
                                </asp:BoundField>
                                <asp:BoundField DataField="Product_Code" SortExpression="Product_Code" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Product Code"></asp:BoundField>
                                <asp:BoundField DataField="Product_Desc" SortExpression="Product_Desc" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Product Desc"></asp:BoundField>
                                <asp:BoundField DataField="MANF_PERIOD" SortExpression="MANF_PERIOD" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="MFG Period"></asp:BoundField>
                                <asp:BoundField DataField="PRODUCT_SR_NO" SortExpression="PRODUCT_SR_NO" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="PRD_SR_NO"></asp:BoundField>
                                <asp:BoundField DataField="MFGUNIT" SortExpression="MFGUNIT" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="MFG Unit"></asp:BoundField>
                                <asp:BoundField DataField="BLADE_VENDOR" SortExpression="BLADE_VENDOR" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="BLADE VENDOR"></asp:BoundField>
                                <asp:BoundField DataField="MAKE_CAP" SortExpression="MAKE_CAP" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Make Cap"></asp:BoundField>
                                <asp:BoundField DataField="MAKE_AGREED" SortExpression="MAKE_AGREED" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Bearing Failure"></asp:BoundField>
                                    <asp:BoundField DataField="WindingUnit" SortExpression="WindingUnit" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Winding Vendor"></asp:BoundField>
                                <asp:BoundField DataField="Defect_Category_desc" SortExpression="Defect_Category_desc"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Left" HeaderText="Defect Category Name">
                                </asp:BoundField>
                                <asp:BoundField DataField="Defect_desc" SortExpression="Defect_desc" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Defect Name"></asp:BoundField>
                                <asp:BoundField DataField="NUM_OF_DEF" SortExpression="NUM_OF_DEF" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="NUM of Defects"></asp:BoundField>
                                    
                                 <asp:BoundField DataField="REMARK" SortExpression="REMARK" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Defect Remark"></asp:BoundField>
                                   
                                <asp:BoundField DataField="OBSERV" SortExpression="OBSERV" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Closure Remark"></asp:BoundField>
                                <asp:BoundField DataField="APPL" SortExpression="APPL" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="APPL"></asp:BoundField>
                                <asp:BoundField DataField="AppInstrumentname" SortExpression="AppInstrumentname"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Left" Visible="true"
                                    HeaderText="Application Instrument Name"></asp:BoundField>
                                <asp:BoundField DataField="InstrumentMfgName" SortExpression="InstrumentMfgName"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Left" HeaderText="Instrument Manufacturer Name"
                                    Visible="true"></asp:BoundField>
                                <asp:BoundField DataField="InstrumentDetails" SortExpression="InstrumentDetails"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Left" HeaderText="Instrument Details (Current,Voltage,Capacity Rating & Output Range)"
                                    Visible="true"></asp:BoundField>
                                <asp:BoundField DataField="LOAD" SortExpression="LOAD" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="LOAD"></asp:BoundField>
                                <asp:BoundField DataField="EXCISE" SortExpression="EXCISE" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="EXCISE"></asp:BoundField>
                                <asp:BoundField DataField="AVR_SRNO" SortExpression="AVR_SRNO" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="AVR_SRNO"></asp:BoundField>
                                <asp:BoundField DataField="RATING" SortExpression="RATING" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="RATING"></asp:BoundField>
                                <asp:BoundField DataField="RATING_STATUS" SortExpression="RATING_STATUS" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="RATING_STATUS"></asp:BoundField>
                                <asp:BoundField DataField="MaterialCost" SortExpression="MaterialCost" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Material Cost"></asp:BoundField>
                                <asp:BoundField DataField="ServiceCost" SortExpression="ServiceCost" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Service Cost"></asp:BoundField>
                                <asp:BoundField DataField="Callstage" SortExpression="Callstage" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Complaint Status"></asp:BoundField>
                                <asp:BoundField DataField="ComplaintAge" SortExpression="ComplaintAge" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="ComplaintAge"></asp:BoundField>
                            </Columns>
                            <EmptyDataTemplate>
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="center" style="padding-top: 50px; padding-bottom: 50px; padding-left: 60px;">
                                            <img src="<%=ConfigurationManager.AppSettings["UserMessage"]%>" alt="" />
                                            <b>No Record found</b>
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <!-- End excelGrid -->
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
