<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SIMS/MasterPages/SIMSCICPage.master"
    CodeFile="DefectiveSpareReceiptConfirmationScreen.aspx.cs" Inherits="SIMS_Pages_DefectiveSpareReceiptConfirmationScreen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>

            <script type="text/javascript">
        
       function ClientCheck()
              {
                  var flag=false;
                for(var i=0;i<document.forms[0].length;i++)
                    {
                     if(document.forms[0].elements[i].id.indexOf('chk')!=-1)
                    {
                        if(document.forms[0].elements[i].checked)
                         {
                            flag=true
                    }  
                }
            } 
            if (flag==true)
              {
                alert('Confirmed Successfully')               
                return true
              }else
              {
                alert('Please select at least one Complaint.')
                return false
              }

                }

                // Added By Ashok for check uncheck of grid checkbox on 7.11.2014
                function CheckUncheckAll(evt) {
                    var grd = document.getElementById("<%=gvChallanDetail.ClientID %>");
                    var input = grd.getElementsByTagName('input');
                    var cb=[];

                    if (evt.id.indexOf('chAllChall') >= 0) {
                        for (var i = 0; i < input.length; i++) {
                            if (input[i].type == "checkbox" && input[i].id != evt.id) {
                                cb.push(i);
                                input[i].checked = evt.checked;
                            }
                        }
                    }
                    else if (evt.id.indexOf('chk') >= 0) {
                        var count = 0;
                        for (var i = 0; i < input.length; i++) {
                            if (input[i].type == "checkbox" && input[i].id.indexOf('chAllChall') < 0) 
                            {
                                cb.push(i);
                                if (input[i].checked)
                                count = count + 1;
                            }
                        }
                        var chkHd = input[0].checked = count == cb.length;
                    }
                }
        
            </script>

            <table width="100%" cellpadding="2" cellspacing="0">
                <tr>
                    <td class="headingred">
                        Defective Spare Receipt Confirmation(Return Flag=True)
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
                                    <table border="0" style="width: 100%">
                                        <tr>
                                            <td colspan="2" align="<%=ConfigurationManager.AppSettings["MandatoryTextAlign"]%>">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px; text-align:right;">
                                                ASC:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlASC" runat="server" CssClass="simpletxt1" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlASC_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px; text-align:right;">
                                                Division:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlDivision" runat="server" CssClass="simpletxt1" Width="133px"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                        <td style="width: 100px; text-align:right;">
                                            Challan No: </td>
                                        <td>
                                          <asp:DropDownList ID="ddlChallanNo" runat="server" CssClass="simpletxt1"
                                           OnSelectedIndexChanged="ddlChallansSelectIndexChanged"  Width="133px" AutoPostBack="true">
                                        </asp:DropDownList>  
                                        </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="gvChallanDetail" runat="server" AllowPaging="True" AllowSorting="True"
                                                    AlternatingRowStyle-CssClass="fieldName" AutoGenerateColumns="False" EnableSortingAndPagingCallbacks="True"
                                                    GridGroups="both" HeaderStyle-CssClass="fieldNamewithbgcolor" HorizontalAlign="Left"
                                                    PageSize="50" PagerStyle-HorizontalAlign="Center" OnPageIndexChanging="gvChallanDetail_PageIndexChanging"
                                                    RowStyle-CssClass="gridbgcolor" Width="100%">
                                                    <RowStyle CssClass="gridbgcolor" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sno"  HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Service_Contractor" HeaderText="Service Contractor" ItemStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" Width="210px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Product_Division" HeaderText="Product Division" ItemStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" Width="210px" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Complaint No.">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkcomplaint" runat="server" 
                                                                    CommandArgument='<%#Eval("BASELINEID")%>' CausesValidation="false" 
                                                                    CommandName="stage" Text='<%#Eval("complaint_no") %>' 
                                                                    onclick="lnkcomplaint_Click"></asp:LinkButton>
                                                                
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField  Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="spareId" runat="server" Text='<%#Eval("spare_id") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        
                                                        
                                                        
                                                        <asp:BoundField DataField="spare" HeaderText="Spare" ItemStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" Width="210px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="defreturnqty" HeaderText="Qty" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                          <asp:BoundField DataField="transportationdetail" HeaderText="Transportation Detail" ItemStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Left" Width="190px" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Challan No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblClhallanNo" runat="server" Text='<%#Eval("Challan_No") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Claim No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblClaimNo" runat="server" Text='<%#Eval("Claim_No") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Received Qty" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtreceivedqty" runat="server" Width="50px" Text="1" />
                                                                <asp:RequiredFieldValidator ID="RequiredFir1" ControlToValidate="txtreceivedqty" ValidationGroup="cc"
                                                                InitialValue="0" runat="server" ErrorMessage="quantity cannot be zero." Display="Dynamic" />
                                                                 <asp:RegularExpressionValidator ID="Requilidator1" ControlToValidate="txtreceivedqty" ValidationGroup="cc"
                                                                 runat="server" ValidationExpression="\d{1}" ErrorMessage="Incorrect format."  Display="Dynamic" /> 
                                                            </ItemTemplate>
                                                            
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CG Action" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlCGaction" runat="server" CssClass="simpletxt1" Width="150px">
                                                                    <%--<asp:ListItem Selected="True" Value="0">Select</asp:ListItem>--%>
                                                                    <asp:ListItem Value="R">Return To Division</asp:ListItem>
                                                                    <asp:ListItem Value="D">Destruction</asp:ListItem>
                                                                    <asp:ListItem Value="S">Salvage</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCGaction"
                                                                    ErrorMessage="Required" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                        <HeaderTemplate>
                                                        <asp:CheckBox ID="chAllChall" runat="server"  Text="All/None" onclick="CheckUncheckAll(this);"/>
                                                        </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chk" runat="server" onclick="CheckUncheckAll(this);"/>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle HorizontalAlign="Center" />
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
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td width="100%">
                                    <table align="center">
                                        <tr>
                                            <td width="100%">
                                                <asp:Button ID="imgBtnConfirm" runat="server" CssClass="btn" OnClick="imgBtnConfirm_Click"
                                                    Text="Confirm" Width="70px" ValidationGroup="cc" />&nbsp;
                                                <asp:Button ID="imgBtnCancel" Width="70px" runat="server" CausesValidation="false"
                                                    CssClass="btn" Text="Cancel" OnClick="imgBtnCancel_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
