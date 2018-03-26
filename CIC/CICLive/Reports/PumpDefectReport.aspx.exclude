<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" EnableEventValidation="false"  CodeFile="PumpDefectReport.aspx.cs" Inherits="Reports_PumpDefectReport" Title="" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"  TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
    <asp:UpdatePanel ID="updateSC" runat="server">
        <ContentTemplate>
<table width="100%" cellpadding="2" cellspacing="0">
                <tr>
                    <td align="center" class="bgcolorcomm" style="padding: 10px">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            
                            <tr>
                                <td>
                                   
                                     <table border="0" cellpadding="1" cellspacing="0" width="100%">
                                            <tr>
                                                <td height="4">
                                                    <table width="100%" cellpadding="1" cellspacing="0" border="0"  align="left">
                                                        <tr>
                                                            <td align="left" colspan="6" 
                                                                
                                                                style="font-family: Arial, Helvetica, sans-serif; font-size: medium; font-weight: bold;">
                                                                Pump Defect Report </td>
                                                          
                                                        </tr>
                                                        <tr >
                                                            <td align="left">
                                                                &nbsp;</td>
                                                            <td align="left">
                                                                Base Line</td>
                                                            <td align="left">
                                                                  <asp:DropDownList ID="ddlBusinessLine" runat="server" AutoPostBack="true" 
                                                                      CssClass="simpletxt1" 
                                                                      OnSelectedIndexChanged="ddlBusinessLine_SelectedIndexChanged" 
                                                                      ValidationGroup="editt" Width="175px">
                                                                  </asp:DropDownList>
                                                            </td>
                                                            <td align="left">
                                                                &nbsp;</td>
                                                            <td align="left">
                                                                &nbsp;</td>
                                                            <td align="left">
                                                                &nbsp;</td>
                                                            </tr>
                                                        <tr>
                                                            <td align="left">
                                                                &nbsp;</td>
                                                            <td align="left">
                                                                Region
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="true" 
                                                                    CssClass="simpletxt1" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" 
                                                                    ValidationGroup="editt" Width="175px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="left">
                                                                &nbsp;</td>
                                                            <td align="left">
                                                                &nbsp;</td>
                                                            <td align="left">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                &nbsp;</td>
                                                            <td align="left">
                                                                Branch</td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="true" 
                                                                    CssClass="simpletxt1" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" 
                                                                    ValidationGroup="editt" Width="175px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="left">
                                                                &nbsp;</td>
                                                            <td align="left">
                                                                &nbsp;</td>
                                                            <td align="left">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                &nbsp;</td>
                                                            <td align="left">
                                                                Product Division</td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="ddlProductDivison" runat="server" AutoPostBack="True" 
                                                                    CssClass="simpletxt1" 
                                                                    OnSelectedIndexChanged="ddlProductDivison_SelectedIndexChanged" 
                                                                    ValidationGroup="editt" Width="175px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="left">
                                                                &nbsp;</td>
                                                            <td align="left">
                                                                &nbsp;</td>
                                                            <td align="left">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                &nbsp;</td>
                                                            <td align="left">
                                                                Product Line</td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="ddlProductLine" runat="server" AutoPostBack="True" 
                                                                    CssClass="simpletxt1" ValidationGroup="editt" Width="175px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="left">
                                                                &nbsp;</td>
                                                            <td align="left">
                                                                &nbsp;</td>
                                                            <td align="left">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                &nbsp;</td>
                                                            <td align="left">
                                                                Loged Date</td>
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
                                    <asp:Label ID="lblDateErr" runat="server" ForeColor="Red" Text=""></asp:Label></td>
                                                            <td align="left">
                                                                &nbsp;</td>
                                                            <td align="left">
                                                                &nbsp;</td>
                                                            <td align="left">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                &nbsp;</td>
                                                            <td align="left">
                                                                &nbsp;</td>
                                                            <td align="left">
                                                                <asp:Button ID="BtnSearch" runat="server" Text="Show" 
                                                                    onclick="BtnSearch_Click"  />
                                                            </td>
                                                            <td align="left">
                                                                &nbsp;</td>
                                                            <td align="left">
                                                                &nbsp;</td>
                                                            <td align="left">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                        <td align="left">
                                                                &nbsp;</td>
                                                                <td align="left">
                                                                &nbsp;</td>
                                                                <td align="left">
                                                                <asp:Label ID="lblmessage" runat="server" Font-Bold="true"></asp:Label>
                                                                &nbsp;</td>
                                                                <td align="left">
                                                                &nbsp;</td>
                                                                <td align="left">
                                                                &nbsp;</td>
                                                                <td align="left">
                                                                &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="18"></td>
                                            </tr>
                                            <tr>
                                                <td style="border-top:0px solid #95B2C1" height="1" align="left">                                                    
                                                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                                                        AlternatingRowStyle-CssClass="fieldName" AutoGenerateColumns="False" 
                                                        GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor" 
                                                        onpageindexchanging="GridView1_PageIndexChanging" PageSize="20" 
                                                        RowStyle-CssClass="gridbgcolor" Width="100%">
                                                        <RowStyle CssClass="gridbgcolor" />
                                                        <Columns>
                                                        <asp:TemplateField HeaderText="Complaint No.">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkcomplaint" runat="server" CommandArgument='<%#Eval("Complaint_Split")%>'
                                                                    CausesValidation="false" CommandName="stage" Text='<%#Eval("Complaint_Split")%>'
                                                                    OnClick="lnkcomplaint_Click"> </asp:LinkButton>
                                                            </ItemTemplate>
                                                            
                                                        </asp:TemplateField>
                                                           <asp:BoundField DataField="Reporteddate" HeaderText="Reported Date" />
                                                             <asp:BoundField DataField="Region" HeaderText="Region" />
                                                             <asp:BoundField DataField="Branch" HeaderText="Branch" />
                                                             <asp:BoundField DataField="sc_name" HeaderText="SC Name" />
                                                               <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                                                                <asp:BoundField DataField="ProductLine" HeaderText="Product Line" />
                                                             <asp:BoundField DataField="Productgroup" HeaderText="Product Group" />
                                                             <asp:BoundField DataField="product_code" HeaderText="Product Code" />
                                                               <asp:BoundField DataField="product_desc" HeaderText="Product Desc" />
                                                              <asp:BoundField DataField="MFGPeriod" HeaderText="MFG Period" />
                                                              
                                                            <asp:BoundField DataField="Sr_No" HeaderText="Pump Sr. No" />
                                                            <asp:TemplateField HeaderText="Application Agriculture">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LblAgr" runat="server" 
                                                                        Text='<%# Eval("Application_Type").ToString().Equals("Domestic") ? " " : Eval("Application")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Application Domestic">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LblDom" runat="server" 
                                                                        Text='<%# Eval("Application_Type").ToString().Equals("Agriculture") ? " " : Eval("Application")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Voltage_Observed" HeaderText="Voltage Observed(V)" />
                                                            <asp:BoundField DataField="Current_Observed" HeaderText="Current Observed(Amp)" />
                                                            <asp:BoundField DataField="CP" HeaderText="Control Panel Type" />
                                                            <asp:BoundField DataField="CP_Make" HeaderText="Control Panel Make" />
                                                            <asp:BoundField DataField="Panel_HP" HeaderText="Control Panel HP" />
                                                            <asp:BoundField DataField="Power_Supply" HeaderText="Power Supply" />
                                                            <asp:BoundField DataField="Size" HeaderText="Cable Size(Sq mm)" />
                                                            <asp:BoundField DataField="Length" HeaderText="Cable Length(Mtr)" />
                                                            <asp:BoundField DataField="Op_Head" HeaderText="Operating head(Mtr)" />
                                                            <asp:BoundField DataField="Suction_Side" HeaderText="Pipe Suction Size" />
                                                            <asp:BoundField DataField="Delivery_Side" HeaderText="Pipe Delivery Size" />
                                                            <asp:BoundField DataField="WDG_Burn" HeaderText="WDG Burn due to" />
                                                            <asp:BoundField DataField="WDG_Melt" HeaderText="Sub reason a,b,c" />
                                                            <asp:BoundField DataField="interturn_Short" HeaderText="Location- D,ODE &amp; inslot" />
                                                            <asp:BoundField DataField="Cable_Joint" HeaderText="Cable Joint Details" />
                                                          
                                                           
                                                           
                                                            
                                                           
                                                             
                                                        </Columns>
                                                        <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                        <AlternatingRowStyle CssClass="fieldName" />
                                                    </asp:GridView>
                                                          <!-- Excel Grid -->
                        <!-- End excelGrid -->                                   
                                                </td>
                                            </tr>
                                        
                                            <tr>
                                             <td align="center">
                                    <asp:Button ID="btnExportExcel" runat="server" Width="114px" Text="Save To Excel"
                                        CssClass="btn" OnClick="btnExportExcel_Click" CausesValidation="False"  />
                                        </td> 
                                            </tr>
                                                                                 
                                            <tr>
                                                <td align="left">
                                                    <table width="100%" cellpadding="1" cellspacing="0" border="0" align="left">
                                                        <tr>
                                                            <td align="left" width="17%">
                                                                &nbsp;</td>
                                                            <td align="left" width="18%">
                                                                &nbsp;</td><td align="left" width="13%">
                                                                &nbsp;</td>
                                                            <td align="left" width="20%">
                                                                &nbsp;</td>
                                                                <td width="13%">&nbsp;</td>
                                                                <td width="19%">&nbsp;</td></tr>
                                                        </table> 
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
<Triggers>
<asp:PostBackTrigger ControlID="btnExportExcel" />
</Triggers>
    </asp:UpdatePanel>
</asp:Content>

