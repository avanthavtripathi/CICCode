<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="RepeatedRpt.aspx.cs" Inherits="Reports_RepeatedRpt" Title="Untitled Page"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">

<script type="text/javascript">
function EndGetData(arg)
{
   document.getElementById("grid").innerHTML = arg;
}

setTimeout("<asp:literal runat="server" id="ltCallback" />", 200);

function ValidateDate()
{
    var srno = document.getElementById('<%= txtprodSrNo.ClientID %>').value; 
    var my_date
    var indate
    var dDate,mMonth,yYear

    var prdcode=document.getElementById("ctl00_MainConHolder_ddlUnit").value;
    var LoggedDateFrom,LoggedDateTo;
    LoggedDateFrom=document.getElementById("ctl00_MainConHolder_txtFromDate").value;
    LoggedDateTo=document.getElementById("ctl00_MainConHolder_txtToDate").value;

        if(srno != "")
        {
            document.getElementById("ctl00_MainConHolder_txtFromDate").value="";
            document.getElementById("ctl00_MainConHolder_txtToDate").value="";
        }


        if(LoggedDateFrom!="" && LoggedDateTo!="")
        {
            indate=new Date(LoggedDateFrom);
            my_date=new Date(LoggedDateTo);
            var m
            var selm
            m=parseInt(indate.getMonth());
            if(prdcode==0)
            {
                m=m+0
                selm=1
            }
            else
            {
                m=m+12
                selm=12
            }
            
            var msg
            if(selm ==1 )
            {
                 msg="You can view the data only for current month. Please change your date selection.\n Or select product division to view the data for previous 12 months.";
            }
            else
            {
                 msg="You can view the data only for previous 12 month. Please change your date selection.";
            }
            
          if(indate.getFullYear()< my_date.getFullYear())
             {
                    alert(msg);
                    return false;
             }
          if( parseInt(m)< parseInt(my_date.getMonth()))
             {
                   alert(msg);
                   return false;
             }
        }
}

 function funRepeatedComplaints(SapProductCode) {
            var strUrl = 'DetailedRepeatedComplaints.aspx?PSN=' + SapProductCode;
            window.open(strUrl, 'History', 'height=450,width=850,left=20,top=30,Location=0');
        }

</script>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <ContentTemplate>
            <table width="100%" border="0">
                <tr>
                    <td class="headingred" style="width: 40%">
                        Repeated Complaint Report
                    </td>
                    <td align="right" style="padding-right:10px;" width="500px">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
             <tr>
             <td colspan="2">
             
                 <table style="width: 100%">
                     <tr>
                         <td align="right" width="40%">
                             Product Division
                         </td>
                         <td>
                    <asp:DropDownList ID="ddlUnit" runat="server" Width="175px" CssClass="simpletxt1" 
                            >
                    </asp:DropDownList>
                                      
                                   </td>
                     </tr>
                     <tr>
                         <td align="right" width="40%">
                         Region
                         </td>
                         <td>
                     <asp:DropDownList ID="DdlRegion" runat="server" Width="175px" CssClass="simpletxt1" 
                             OnSelectedIndexChanged="DdlRegion_SelectedIndexChanged"     AutoPostBack="True">
                     </asp:DropDownList>
                             </td>
                     </tr>
                            <tr>
                         <td align="right" width="40%">
                         Branch
                         </td>
                         <td>
                     <asp:DropDownList ID="DDlBranch" runat="server" Width="175px" CssClass="simpletxt1" 
                        AutoPostBack="True" onselectedindexchanged="DDlBranch_SelectedIndexChanged">
                     <asp:ListItem Text="Select" Value="0" />
                     </asp:DropDownList>
                             </td>
                     </tr>
                     <tr>
                         <td align="right" width="40%">
                             ASC</td>
                         <td>
                     <asp:DropDownList ID="ddlSerContractor" runat="server" Width="175px" CssClass="simpletxt1" >
                      <asp:ListItem Text="Select" Value="0" />
                     </asp:DropDownList>
                            </td>
                     </tr>
                     <tr>
                         <td align="right" width="40%">
                             Date Range</td>
                         <td>
                              <asp:TextBox runat="server" ID="txtFromDate" CssClass="txtboxtxt" ValidationGroup="editt"
                                        MaxLength="10" />
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" Type="Date" Operator="DataTypeCheck"
                                        ControlToValidate="txtFromDate" Display="none" SetFocusOnError="true"></asp:CompareValidator>
                                    <cc1:CalendarExtender ID="CEFrom" runat="server" TargetControlID="txtFromDate">
                                    </cc1:CalendarExtender>
                                    To
                                    <asp:TextBox runat="server" ID="txtToDate" CssClass="txtboxtxt" ValidationGroup="editt"
                                        MaxLength="10" />
                                    <asp:CompareValidator ID="CompareValidator7" runat="server" Type="Date" Operator="DataTypeCheck"
                                        ControlToValidate="txtToDate" Display="none" SetFocusOnError="true"></asp:CompareValidator>
                                    <cc1:CalendarExtender ID="CETo" runat="server" TargetControlID="txtToDate">
                                    </cc1:CalendarExtender>
                                    <asp:Label ID="lblDateErr" runat="server" ForeColor="Red" Text=""></asp:Label>
                           </td>
                     </tr>
                     <tr>
                         <td align="right" width="40%">
                             Product Sr No </td>
                         <td>
                            <asp:TextBox runat="server" ID="txtprodSrNo" CssClass="txtboxtxt" MaxLength="15" />
                         </td>
                     </tr>
           </table>
             
             </td>
             </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="Search" 
                            Width="100" onclick="btnSearch_Click"
                             />
                        <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn" Text="Export To Execl"
                            Width="100" onclick="btnExport_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    <div id="grid">
                    <span>
                        <%--  <img src="../images/loading9.gif" alt="Loading....." /> --%>
                    </span>
                        <asp:GridView ID="gvReport" CssClass="simpletxt1" runat="server" RowStyle-CssClass="bgcolorcomm"
                            Width="100%" AlternatingRowStyle-CssClass="fieldName" HeaderStyle-CssClass="fieldNamewithbgcolor"
                            GridGroups="both" HorizontalAlign="Left" AutoGenerateColumns="false"  >
                            <Columns>
                            <asp:BoundField DataField="Region_Desc" HeaderText="Region" />
                            <asp:BoundField DataField="Branch_Name" HeaderText="Branch" />
                            <asp:BoundField DataField="ProductDivision_Desc" HeaderText="ProductDivision" />
                            <asp:BoundField DataField="ProductGroup_Desc" HeaderText="ProductGroup" />
                            <asp:BoundField DataField="ProductLine_Desc" HeaderText="ProductLine" />
                            <asp:BoundField DataField="ComplaintRefNo" HeaderText="ComplaintRefNo" />
                            <asp:BoundField DataField="LoggedDate" HeaderText="LoggedDate" />
                                    <asp:TemplateField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        HeaderText="ProductSrNo">
                                        <ItemTemplate>
                                            <a href="Javascript:void(0);" onclick="funRepeatedComplaints('<%#Eval("SapProductCode")%>')">
                                                <%--<%#Eval("CustomerId")%></a>--%>
                                                <%#Eval("SapProductCode")%>
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                            <asp:BoundField DataField="SC_Name" HeaderText="SC Name" />
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
                       </div>
                    </td>
                 </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

