<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="~/Reports/WindingDefectForFHPMotorsRpt.aspx.cs" Inherits="Reports_WindingDefectForFHPMotorsRpt"
    Title="FHP Motors Winding Defect Analysis Report" %>

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


            if ((LoggedDateFrom != "" || LoggedDateTo != "") && type == 'logged') 
            {
                document.getElementById("ctl00_MainConHolder_txtSLADateFrom").value = "";
                document.getElementById("ctl00_MainConHolder_txtSLADateTo").value = "";

                document.getElementById("ctl00_MainConHolder_txtDefectFrom").value = "";
                document.getElementById("ctl00_MainConHolder_txtDefectTo").value = "";
            }

           
            if (type == "validate") 
            {
                if (!((LoggedDateFrom != "" && LoggedDateTo != ""))) 
                {
                    alert('Please enter logged date.');
                    return false;
                }
                else {
                    if (LoggedDateFrom != "" && LoggedDateTo != "") 
                    {
                        indate = new Date(LoggedDateFrom);
                        my_date = new Date(LoggedDateTo);
                        var m
                        var selm
                        var yearFlag = 0;

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
                            m = m + 3
                            yearFlag = 1;
                        }

                        var msg
                        if (selm == 1) {
                            msg = "You can view the data only for current month. Please change your date selection.\n Or select product division to view the data for previous 3 months.";
                        }
                        else if(selm==4)
                        {
                            msg = "You can view the data only for previous twelve month. Please change your date selection.";
                        }
                        else 
                        {
                            msg = "You can view the data only for previous three month. Please change your date selection.";
                        }
                                        
                       if(m>12  && yearFlag=0)
                       {
                            alert(msg);
                            return false;
                       }
                        if (indate.getFullYear() < my_date.getFullYear() && (scCode==0 || scCode=="")) {
                            alert(msg);
                            return false;
                        }


                        if (parseInt(m) < parseInt(my_date.getMonth()) && (scCode==0 || scCode=="")) {
                            alert(msg);
                            return false;
                        }
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
                        FHP Motor Winding Analysis Report
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
                                        ValidationGroup="editt" AutoPostBack="True">
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
                                </td>
                            </tr>
                            <tr style="display: none;">
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
                            <tr style="display: none;">
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
                            <tr style="display: none;">
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
                            <tr style="display: none;">
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
                        <div style="width:1000px; height:auto; overflow-x:scroll;">
                        <table width="100%" border="0">
                            <tr>
                                <td>
                                    <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                        HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="True"
                                        PagerStyle-HorizontalAlign="Center" AllowSorting="true" AutoGenerateColumns="False"
                                        ID="gvComm" runat="server" OnPageIndexChanging="gvComm_PageIndexChanging" Width="100%"
                                        HorizontalAlign="Left" Visible="true" OnSorting="gvComm_Sorting" >
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="40px" HeaderText="SNo">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="100px" SortExpression="SplitComplaintRefNo"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Complaint NO">
                                                <ItemTemplate>
                                                    <a href="Javascript:void(0);" onclick="funCommonPopUpReport(<%#Eval("BaseLineId")%>)">
                                                        <%#Eval("SplitComplaintRefNo")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="100px" SortExpression="ReportedDate" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderText="Reported Date">
                                                <ItemTemplate>
                                                    <%#Eval("ReportedDate")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Region_Desc" SortExpression="Region_Desc" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Region">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Branch_Name" SortExpression="Branch_Name" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Branch">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SC_Name" SortExpression="SC_Name" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="SC Name">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CustomerName" SortExpression="CustomerName" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Customer Name">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProductLine_Desc" SortExpression="ProductLine_Desc" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Product Line">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProductGroup_Desc" SortExpression="ProductGroup_Desc"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Left" HeaderText="Product Group">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Product_Code" SortExpression="Product_Code" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Product Code">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Product_Desc" SortExpression="Product_Desc" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Product Desc">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MANF_PERIOD" SortExpression="MANF_PERIOD" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="MFG Period">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MotorSerialNo" SortExpression="MotorSerialNo" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Motor Serial No">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FieldVoltageObj" SortExpression="FieldVoltageObj" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Field Voltage Observed">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FieldCurrentObj" SortExpression="FieldCurrentObj" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Field Current Observed" Visible="false">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="StarterUsed" SortExpression="StarterUsed" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Starter Used">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="WindingBurntDueto" SortExpression="WindingBurntDueto"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Left" HeaderText="Winding Burn Due To">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="BurntWindPhotoUpload" SortExpression="BurntWindPhotoUpload"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Burnt Winding Photo Uploaded in CIC">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="WendingBurntAt" SortExpression="WendingBurntAt" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Winding Burnt At">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="GFGearDefective" SortExpression="GFGearDefective" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="CF Gear Defective">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="StartcapacitorDamaged" SortExpression="StartcapacitorDamaged"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Start Capacitor Damaged">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="OCSwitchDefective" SortExpression="OCSwitchDefective"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Left" HeaderText="OC Switch Defective">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="WindingBurntIn" SortExpression="WindingBurntIn" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Winding Burnt In">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="YCForWindingDefect" SortExpression="YCForWindingDefect"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Your Comment (Why the Winding Burnt)">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="YCApplicationLoad" SortExpression="YCApplicationLoad"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Left" Visible="true"
                                                HeaderText="Your Comment Regarding Application & Load">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ApplicationInstrumentType" SortExpression="ApplicationInstrumentType"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Left" HeaderText="Application Instrument Type"
                                                Visible="true">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AppInstTypeDesc" SortExpression="AppInstTypeDesc"
                                                ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="Left" HeaderText="Other Desc"
                                                Visible="true">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AppInstMfg" SortExpression="AppInstMfg" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Application Instrument Manufacturer Name"
                                                Visible="true">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AppInstCurrentRating" SortExpression="AppInstCurrentRating"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Left" HeaderText="Current Rating"
                                                Visible="false">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AppInstVoltageRating" SortExpression="AppInstVoltageRating"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Application Voltage Instrument Rating">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AppInstModelNo" SortExpression="AppInstModelNo" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Application Instrument Model No.*:">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AppInstOpRangeCRating" SortExpression="AppInstOpRangeCRating"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Application Instrument’s Output Range/ Capacity Rating">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Technisian" SortExpression="Technisian" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Technician Name">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TechnisianMobNo" SortExpression="TechnisianMobNo" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-HorizontalAlign="Left" HeaderText="Technician Mobile Phone">
                                            </asp:BoundField>
                                             <asp:BoundField DataField="CustomerPhoneNo" SortExpression="CustomerPhoneNo" ItemStyle-HorizontalAlign="left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Customer Phone No">
                                </asp:BoundField>
                                <asp:BoundField DataField="MaterialCharges" SortExpression="MaterialCharges" ItemStyle-HorizontalAlign="left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Material Charges">
                                </asp:BoundField>
                                <asp:BoundField DataField="ServiceCharges" SortExpression="ServiceCharges" ItemStyle-HorizontalAlign="left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Service Charges">
                                </asp:BoundField>
                                <asp:BoundField DataField="IBNNo" SortExpression="IBNNo" ItemStyle-HorizontalAlign="left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="IBN No">
                                </asp:BoundField>
                                <asp:BoundField DataField="BillCreatedDate" SortExpression="BillCreatedDate" ItemStyle-HorizontalAlign="left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="IBN Created Date">
                                </asp:BoundField>
                                <asp:BoundField DataField="ApprovedBy" SortExpression="ApprovedBy" ItemStyle-HorizontalAlign="left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Approved By">
                                </asp:BoundField>
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
                                    <!-- End Action Listing -->
                                </td>
                            </tr>                            
                        </table>
                        </div>
                        <div>
                        <table style="width:100%; border:0px;">
                        <tr>
                                <td align="center">
                                    <table runat="server" id="tblpage" visible="false" border="0" cellpadding="5" cellspacing="5"
                                        width="100%">
                                        <tr>
                                            <td width="30%" align="right">
                                                <asp:Panel ID="pnlPre" Width="2%" Visible="false" runat="server">
                                                    <asp:LinkButton runat="server" ID="lnkPre" Text="Previous"></asp:LinkButton>
                                                </asp:Panel>
                                            </td>
                                            <td width="5%">
                                                <asp:Panel ID="Panel1" runat="server">
                                                </asp:Panel>
                                            </td>
                                            <td width="2%" align="left">
                                                <asp:Panel ID="pnlNext" Width="5%" Visible="false" runat="server">
                                                    <asp:LinkButton runat="server" ID="lnkNext" Text="Next"></asp:LinkButton>
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
                                                                ID="Button1" runat="server" />
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
                        </div>
                        <br />
                        <!-- Excel Grid -->
                        <asp:GridView PageSize="10" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                            HeaderStyle-CssClass="fieldNamewithbgcolor" GridGroups="both" AllowPaging="false"
                            PagerStyle-HorizontalAlign="Center" AutoGenerateColumns="False" ID="gvExcel"
                            runat="server" Width="100%" HorizontalAlign="Left" Visible="true">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="40px" HeaderText="SNo">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="Complaint_Split" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Complaint NO">
                                    <ItemTemplate>
                                        <%#Eval("SplitComplaintRefNo")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" SortExpression="ReportedDate" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" HeaderText="Reported Date">
                                    <ItemTemplate>
                                        <%#Eval("ReportedDate")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Region_Desc" SortExpression="Region_Desc" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Region">
                                </asp:BoundField>
                                <asp:BoundField DataField="Branch_Name" SortExpression="Branch_Name" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Branch">
                               </asp:BoundField>
                                <asp:BoundField DataField="SC_Name" SortExpression="SC_Name" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="SC Name">
                                </asp:BoundField>
                                <asp:BoundField DataField="CustomerName" SortExpression="CustomerName" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Customer Name">
                                 </asp:BoundField>
                                <asp:BoundField DataField="ProductLine_Desc" SortExpression="ProductLine_Desc" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Product Line">
                                </asp:BoundField>
                                <asp:BoundField DataField="ProductGroup_Desc" SortExpression="ProductGroup_Desc"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Left" HeaderText="Product Group">
                                </asp:BoundField>
                                <asp:BoundField DataField="Product_Code" SortExpression="Product_Code" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Product Code">
                                </asp:BoundField>
                                <asp:BoundField DataField="Product_Desc" SortExpression="Product_Desc" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Product Desc">
                                </asp:BoundField>
                                <asp:BoundField DataField="MANF_PERIOD" SortExpression="MANF_PERIOD" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="MFG Period">
                               </asp:BoundField>
                                <asp:BoundField DataField="MotorSerialNo" SortExpression="MotorSerialNo" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Motor Serial No">
                               </asp:BoundField>
                                <asp:BoundField DataField="FieldVoltageObj" SortExpression="FieldVoltageObj" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Field Voltage Observed">
                                </asp:BoundField>
                                <asp:BoundField DataField="FieldCurrentObj" SortExpression="FieldCurrentObj" ItemStyle-HorizontalAlign="Left" Visible="false"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Field Current Observed">
                                </asp:BoundField>
                                <asp:BoundField DataField="StarterUsed" SortExpression="StarterUsed" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Starter Used">
                               </asp:BoundField>
                                <asp:BoundField DataField="WindingBurntDueto" SortExpression="WindingBurntDueto"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Left" HeaderText="Winding Burn Due To">
                                </asp:BoundField>
                                <asp:BoundField DataField="BurntWindPhotoUpload" SortExpression="BurntWindPhotoUpload"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Burnt Winding Photo Uploaded in CIC">
                                </asp:BoundField>
                                <asp:BoundField DataField="WendingBurntAt" SortExpression="WendingBurntAt" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Winding Burnt At">
                                </asp:BoundField>
                                <asp:BoundField DataField="GFGearDefective" SortExpression="GFGearDefective" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="CF Gear Defective">
                                 </asp:BoundField>
                                <asp:BoundField DataField="StartcapacitorDamaged" SortExpression="StartcapacitorDamaged"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Start Capacitor Damaged">
                                </asp:BoundField>
                                <asp:BoundField DataField="OCSwitchDefective" SortExpression="OCSwitchDefective"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Left" HeaderText="OC Switch Defective">
                                </asp:BoundField>
                                <asp:BoundField DataField="WindingBurntIn" SortExpression="WindingBurntIn" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Winding Burnt In">
                                </asp:BoundField>
                                <asp:BoundField DataField="YCForWindingDefect" SortExpression="YCForWindingDefect"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Your Comment (Why the Winding Burnt)">
                                </asp:BoundField>
                                <asp:BoundField DataField="YCApplicationLoad" SortExpression="YCApplicationLoad"
                                    ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="Left" Visible="true"
                                    HeaderText="Your Comment Regarding Application & Load">
                                </asp:BoundField>
                                <asp:BoundField DataField="ApplicationInstrumentType" SortExpression="ApplicationInstrumentType"
                                    ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="Left" HeaderText="Application Instrument Type"
                                    Visible="true">
                                </asp:BoundField>
                                <asp:BoundField DataField="AppInstTypeDesc" SortExpression="AppInstTypeDesc"
                                    ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="Left" HeaderText="Other Desc"
                                    Visible="true">
                                </asp:BoundField>
                                <asp:BoundField DataField="AppInstMfg" SortExpression="AppInstMfg" ItemStyle-HorizontalAlign="left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Application Instrument Manufacturer Name"
                                    Visible="true">
                                </asp:BoundField>
                                <asp:BoundField DataField="AppInstCurrentRating" SortExpression="AppInstCurrentRating"
                                    ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="Left" HeaderText="Current Rating" Visible="false">
                                </asp:BoundField>
                                <asp:BoundField DataField="AppInstVoltageRating" SortExpression="AppInstVoltageRating"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Application Voltage Instrument Rating">
                                </asp:BoundField>
                                <asp:BoundField DataField="AppInstModelNo" SortExpression="AppInstModelNo" ItemStyle-HorizontalAlign="left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Application Instrument Model No.*:">
                                 </asp:BoundField>
                                <asp:BoundField DataField="AppInstOpRangeCRating" SortExpression="AppInstOpRangeCRating"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderText="Application Instrument's Output Range/ Capacity Rating">
                                 </asp:BoundField>
                                <asp:BoundField DataField="Technisian" SortExpression="Technisian" ItemStyle-HorizontalAlign="left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Technician's Name">
                                </asp:BoundField>
                                <asp:BoundField DataField="TechnisianMobNo" SortExpression="TechnisianMobNo" ItemStyle-HorizontalAlign="left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Technician's Mobile Ph No">
                                </asp:BoundField>                                
                                <asp:BoundField DataField="CustomerPhoneNo" SortExpression="CustomerPhoneNo" ItemStyle-HorizontalAlign="left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Customer Phone No">
                                </asp:BoundField>
                                <asp:BoundField DataField="MaterialCharges" SortExpression="MaterialCharges" ItemStyle-HorizontalAlign="left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Material Charges">
                                </asp:BoundField>
                                <asp:BoundField DataField="ServiceCharges" SortExpression="ServiceCharges" ItemStyle-HorizontalAlign="left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Service Charges">
                                </asp:BoundField>
                                <asp:BoundField DataField="IBNNo" SortExpression="IBNNo" ItemStyle-HorizontalAlign="left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="IBN No">
                                </asp:BoundField>
                                <asp:BoundField DataField="BillCreatedDate" SortExpression="BillCreatedDate" ItemStyle-HorizontalAlign="left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="IBN Created Date">
                                </asp:BoundField>
                                <asp:BoundField DataField="ApprovedBy" SortExpression="ApprovedBy" ItemStyle-HorizontalAlign="left"
                                    HeaderStyle-HorizontalAlign="Left" HeaderText="Approved By">
                                </asp:BoundField>
                                
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
