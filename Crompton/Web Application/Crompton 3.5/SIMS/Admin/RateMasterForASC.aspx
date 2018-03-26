<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RateMasterForASC.aspx.cs"
    Inherits="SIMS_Admin_RateMasterForASC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Rate Master For ASC</title>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function CheckRate(ddl) {
//       debugger;
           
            var str = ddl.id;
            var dd = str.split("_");
            var num = (dd[dd.length - 2]);
            var div = document.getElementById("GvRateMasterForASC_" + num + "_hdnDivRate").value;
            
            var rate = document.getElementById("GvRateMasterForASC_" + num + "_txtRate").value;
            
            var calRate = eval(rate);
            var divrate = eval(div);

            if (calRate > divrate) {

                alert('Rate should be less than or equal to division level rate.');
                document.getElementById("GvRateMasterForASC_" + num + "_txtRate").value = "";
            }
            else {

                document.getElementById("GvRateMasterForASC_" + num + "_hdnCheckUpdate").value='U';
            }

        }
       

</script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="Mgr1" runat="server">
    </asp:ScriptManager>
    <div>
        <asp:UpdatePanel ID="Panel" runat="server">
            <ContentTemplate>
                <table width="100%" border="0">
                    <tr height="50">
                        <td class="headingred">
                            Rate Master For ASC
                        </td>
                        <td height="30" align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>"
                            style="padding-right: 10px;">
                            <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                <ProgressTemplate>
                                    <img src="<%=ConfigurationManager.AppSettings["SIMSAjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0">
                    <tr>
                        <td style="padding: 5px; width: 20%" valign="middle">
                            <strong>Product Division:</strong>
                        </td>
                        <td style="padding: 5px; width: 20%" valign="middle">
                            <asp:DropDownList ID="ddlProdDivision" runat="server" CssClass="simpletxt1"
                                ValidationGroup="editt1" Width="190" >
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvProductDiv" runat="server" ControlToValidate="ddlProdDivision"
                                ErrorMessage="Product Division is required." InitialValue="0" SetFocusOnError="true"
                                ValidationGroup="editt1"></asp:RequiredFieldValidator>
                        </td>
                        <td style="padding: 5px; width: 20%" valign="middle">
                            <strong>Service Contractor:</strong>
                        </td>
                        <td style="padding: 5px; width: 20%" valign="middle">
                            <asp:DropDownList ID="ddlASC" runat="server" CssClass="simpletxt1"
                                ValidationGroup="editt1" Width="250" >
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvProductDiv0" runat="server" ControlToValidate="ddlASC"
                                ErrorMessage="Service Contractor is required." InitialValue="0" SetFocusOnError="true"
                                ValidationGroup="editt1"></asp:RequiredFieldValidator>
                        </td>
                        <td valign="middle" style="padding-bottom:10px;">
                            <asp:Button Text="Go" Width="25px" CssClass="btn" ID="imgBtnGo" runat="server" CausesValidation="true"
                                        ValidationGroup="editt1" OnClick="imgBtnGo_Click" />
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0">
                    <tr>
                        <td style="padding: 10px" align="left" class="bgcolorcomm">
                            <asp:GridView ID="GvRateMasterForASC" runat="server" AlternatingRowStyle-CssClass="fieldName"
                                HeaderStyle-HorizontalAlign="Left" RowStyle-HorizontalAlign="Left" AlternatingRowStyle-HorizontalAlign="Left"
                                PageSize="5" AutoGenerateColumns="False" HeaderStyle-CssClass="fieldNamewithbgcolor"
                                Width="100%" RowStyle-CssClass="gridbgcolor" PagerStyle-CssClass="Paging" 
                                OnRowDataBound="GvRateMasterForASC_RowDataBound">
                                <RowStyle CssClass="gridbgcolor" HorizontalAlign="Left" />
                                <Columns>
                                    <asp:BoundField DataField="Sno" HeaderStyle-Width="40px" HeaderText="SNo">
                                        <HeaderStyle Width="40px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Unit_Desc" SortExpression="Unit_Desc" HeaderStyle-HorizontalAlign="Left"
                                        HeaderText="Product Division" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Activity_Description" SortExpression="Activity_Description"
                                        HeaderStyle-HorizontalAlign="Left" HeaderText="Activity Name" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Parameter_Code1" SortExpression="Parameter_Code1" HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="60px" HeaderText="Parameter-1" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Possible_Value1" SortExpression="Possible_Value1" HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="70px" HeaderText="Possible Value-1" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Parameter_Code2" SortExpression="Parameter_Code2" HeaderStyle-HorizontalAlign="Left"
                                        HeaderText="Parameter-2" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Possible_Value2" SortExpression="Possible_Value2" HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="70px" HeaderText="Possible Value-2" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Parameter_Code3" SortExpression="Parameter_Code3" HeaderStyle-HorizontalAlign="Left"
                                        HeaderText="Parameter-3" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Possible_Value3" SortExpression="Possible_Value3" HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="70px" HeaderText="Possible Value-3" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Parameter_Code4" SortExpression="Parameter_Code4" HeaderStyle-HorizontalAlign="Left"
                                        HeaderText="Parameter-4" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Possible_Value4" SortExpression="Possible_Value4" HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="70px" HeaderText="Possible Value-4" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UOM" SortExpression="UOM" HeaderStyle-HorizontalAlign="Left"
                                        HeaderText="UOM" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Division Level Rate" 
                                        SortExpression="Division_Rate">
                                        <ItemTemplate>
                                           
                                            <asp:Label ID="lblDivRate" runat="server" Text='<%# Bind("Division_Rate") %>'></asp:Label>
                                            <asp:HiddenField ID="hdnDivRate" runat="server" Value='<%#Eval("Division_Rate")%>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRate" runat="server" CausesValidation="true" Width="40px"
                                                CssClass="txtboxtxt" MaxLength="4" ValidationGroup="editt" Text='<%# string.Format("{0}",   String.IsNullOrEmpty(Convert.ToString(Eval("Rate"))) ? "0" : Convert.ToString(Eval("Rate")) ) %>' onblur="CheckRate(this);" ></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvRate" runat="server" ControlToValidate="txtRate"
                                                Display="Dynamic" ErrorMessage="Rate can't be zero." InitialValue="0" ValidationGroup="editt"></asp:RequiredFieldValidator>--%>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvRate1" runat="server" ControlToValidate="txtRate"
                                                Display="Dynamic" ErrorMessage="Rate is required." ValidationGroup="editt"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtRate" ID="revRate"
                                                ValidationGroup="editt" runat="server" ErrorMessage="Rate should be numeric."
                                                ValidationExpression="^([0-9]*|\d*\.\d{1}?\d*)$"></asp:RegularExpressionValidator>
                                                
                                                <asp:HiddenField ID="hdnCheckUpdate" runat="server"/>
                                               
                                        </ItemTemplate>
                                        <ItemStyle Width="100px" VerticalAlign="Middle" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Actual">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkActual" runat="server" onClick="CheckRate(this);" />
                                            <asp:HiddenField ID="hdnactual" runat="server" Value='<%#Eval("Actual")%>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="100px" VerticalAlign="Middle" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ActivityParameter_SNo" SortExpression="ActivityParameter_SNo" HeaderStyle-HorizontalAlign="Left"
                                        HeaderText="ActivityParameter_SNo" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    
                                </Columns>
                                <PagerStyle CssClass="Paging" />
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
                                <HeaderStyle CssClass="fieldNamewithbgcolor" HorizontalAlign="Left" />
                                <AlternatingRowStyle CssClass="fieldName" HorizontalAlign="Left" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0">
                    <tr>                        
                        <td align="center">
                            <asp:Button ID="imgBtnSubmit" runat="server" CausesValidation="True" CssClass="btn"
                                Text="Submit" ValidationGroup="editt" Width="70px" Visible="false" OnClick="imgBtnSubmit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <%--<a id="closeWindow" runat="server" visible="false" href="javascript:void(0)" class="links" onclick="window.close();">Close</a>--%>
                            <asp:Button ID="imgBtnCancel" runat="server" CausesValidation="false" CssClass="btn"
                                Text="Cancel" Width="70px" Visible="false" OnClick="imgBtnCancel_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>

