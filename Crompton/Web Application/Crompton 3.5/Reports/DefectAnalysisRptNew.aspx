<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="DefectAnalysisRptNew.aspx.cs" Inherits="Reports_DefectAnalysisRptNew" Title="Defect Analysis Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="PendingSerRegReport" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function SelectDate()
        {
            var my_date
            var indate
            var dDate,mMonth,yYear

            var prdcode=document.getElementById("ctl00_MainConHolder_ddlProductDivison").value;

            var LoggedDateFrom,LoggedDateTo;
            LoggedDateFrom=document.getElementById("ctl00_MainConHolder_txtLoggedDateFrom").value;
            LoggedDateTo=document.getElementById("ctl00_MainConHolder_txtLoggedDateTo").value;
        
            if (LoggedDateFrom=="" || LoggedDateTo=="")
            {
                alert('Please select valid date range.');
                return false;
            }
            else
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
                        selm=3
                        m=m+3
                    }
                    
                    var msg
                    if(selm==1)
                    {
                       msg="You can view the data only for current month. Please change your date selection.\n Or select product division to view the data for previous 3 months.";
                    }
                    else
                    {
                        msg="You can view the data only for previous three month. Please change your date selection.";
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
    </script>

    <asp:UpdatePanel ID="updatepnl" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportExcel" />
        </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="headingred">
                        Defect Analysis Report New 
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
                                <td width="30%" align="right">
                                    Product Divison
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlProductDivison" runat="server" Width="175px" CssClass="simpletxt1"
                                        ValidationGroup="editt" AutoPostBack="True" OnSelectedIndexChanged="ddlProductDivison_SelectedIndexChanged">
                                    </asp:DropDownList> 
                                    <asp:RequiredFieldValidator ID="rfvDiv" runat="server" ControlToValidate="ddlProductDivison" InitialValue="0" SetFocusOnError="true" Text="*" ValidationGroup="editt" />
                                </td>
                            </tr>
                            <tr>
                                <td width="30%" align="right">
                                    Product Line
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlProductLine" runat="server" Width="175px" CssClass="simpletxt1"
                                        ValidationGroup="editt" >
                                        <asp:ListItem Text="All" Value="0" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                  <%--          <tr runat="server" id="trPGroup">
                                <td width="30%" align="right">
                                    Product Group
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlProductGroup" runat="server" CssClass="simpletxt1" Width="175px"
                                     ValidationGroup="editt">
                                    </asp:DropDownList>
                                </td>
                            </tr>--%>
                             <tr>
                                <td align="right">
                                    Logged Year
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="DdlYear" runat="server" Width="175px" CssClass="simpletxt1">
                                            <asp:ListItem Text="2012" Value="2012"></asp:ListItem>
                                            <asp:ListItem Text="2013" Value="2013"></asp:ListItem>
                                            <asp:ListItem Text="2014" Value="2014"></asp:ListItem>
                                            <asp:ListItem Text="2015" Value="2015"></asp:ListItem>
                                            <asp:ListItem Text="2016" Value="2016" Selected="True"></asp:ListItem>
                                          </asp:DropDownList> 
                 Month <asp:DropDownList ID="ddlMonth" runat="server" Width="175px" CssClass="simpletxt1"
                                       ValidationGroup="editt">
                                    </asp:DropDownList> 
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                </td>
                                <td align="left">
                                    <br />
                                    &nbsp;<asp:Button ID="btnExportExcel" runat="server" Width="114px" Text="Save To Excel" ValidationGroup="editt" 
                                        CssClass="btn" OnClick="btnExportExcel_Click" />
                                </td>
                            </tr>
                         </table>      <br />
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                   
                     </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
