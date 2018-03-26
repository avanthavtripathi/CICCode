<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="OutOfScope.aspx.cs" Inherits="pages_OutOfScope" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
<script language="javascript" type="text/javascript">


document.domain='tvt.com'; // This needs to be defined before calling our Cz-Bar in iframe.


function getCallPhoneId(phone,session_id) {
	alert("==Phone==="+phone+"====Session===="+session_id);
	// your code for idata lookup will come here.
}

function getCRMCallIds(phone, camp_id, session_id, lead_id) {
    //alert(lead_id+"===================="+phone+"======================="+camp_id);
    var refererURL = window.location.href;
    var urlVariables = new Array();
    urlVariables[urlVariables.length] = "phone=" + phone;
    //urlVariables[urlVariables.length] = "customerId=" + lead_id;
    //urlVariables[urlVariables.length] = "campaignId=" + camp_id;
    urlVariables[urlVariables.length] = "sessionId=" + session_id;
    window.location.href = window.location.href + "&" + urlVariables.join("&");

}
</script>
<iframe id="box2" name="box2" scrolling="no" frameborder="1" height="50" width="100%" marginheight="0" marginwidth="0"  src="http://first.tvt.com/czhandler/cti_handler.php?e=2001" ></iframe>
  
     <table width="100%">
        <tr>
       
            <td class="headingred">
          
                Out of Scope
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="bgcolorcomm" colspan="2">
                <table width="99%">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="pnl" runat="server">
                                <ContentTemplate>
                                    <table width="100%">
                                        <tr style="height:20">
                                            <td align="right" width="87%">
                                                &nbsp;</td>
                                            <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                                                <asp:UpdateProgress AssociatedUpdatePanelID="pnl" ID="UpdateProgress1" runat="server">
                                                    <ProgressTemplate>
                                                        <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" class="bgcolorcomm">
                                                <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                                    <tr>
                                                        <td style="text-align: right" width="20%">
                                                            </td>
                                                        <td colspan="3">
                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" style="text-align: right">
                                                            Call Type<font color="red">*</font></td>
                                                        <td colspan="3">
                                                            <asp:DropDownList ID="ddlcalltype" runat="server" CssClass="simpletxt1" 
                                                                AutoPostBack="True" onselectedindexchanged="ddlcalltype_SelectedIndexChanged">
                                                                 <asp:ListItem Value="0" Selected="True">Select</asp:ListItem>
                                                                <%--<asp:ListItem Value="4">Escalation (w/o complaint reference)</asp:ListItem>--%>
                                                                <asp:ListItem Value="11">General Inquiry</asp:ListItem>                                                             
                                                                <asp:ListItem Value="8">Sales Call</asp:ListItem>
                                                                <asp:ListItem Value="5">Client Call</asp:ListItem>
                                                                <asp:ListItem Value="15">Non-CG Customer</asp:ListItem>
                                                                <asp:ListItem Value="18">Language Barrier</asp:ListItem>
                                                                <asp:ListItem Value="9">Others</asp:ListItem>       
                                                            </asp:DropDownList> 
                                                             <asp:DropDownList Width="170px" ID="ddlLanguage" runat="server" CssClass="simpletxt1" Enabled="false">
                                                                <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <div>
                                                                 <asp:RequiredFieldValidator ID="rfvcalltype" runat="server" InitialValue="0"
                                                                ControlToValidate="ddlcalltype" Display="None" SetFocusOnError="true" 
                                                                ErrorMessage="Select a CallType"></asp:RequiredFieldValidator>
                                                                 <asp:RequiredFieldValidator ID="rfvlang" runat="server" InitialValue="0"
                                                                ControlToValidate="ddlLanguage" Display="None" SetFocusOnError="true" 
                                                                ErrorMessage="Language is required."></asp:RequiredFieldValidator>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" style="text-align: right">
                                                            State</td>
                                                        <td style="width: 24%">
                                                            <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True" 
                                                                CssClass="simpletxt1" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" 
                                                                Width="170px">
                                                                 <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <div>
                                                            <asp:RequiredFieldValidator ID="Comparator1" runat="server" 
                                                                ControlToValidate="ddlState" Display="Dynamic" ErrorMessage="State is required." 
                                                                InitialValue="0" Enabled="False" ></asp:RequiredFieldValidator>
                                                                </div>
                                                        </td>
                                                        <td style="width: 50px">
                                                            City
                                                        </td>
                                                        <td valign="top">
                                                            <asp:DropDownList ID="ddlCity" runat="server" CssClass="simpletxt1"  
                                                                Width="170px">
                                                                <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                             <div>
                                                            <asp:RequiredFieldValidator ID="CompareVaor2" runat="server" 
                                                                ControlToValidate="ddlCity" Display="None" InitialValue="0" ErrorMessage="City is required." 
                                                                SetFocusOnError="true" Enabled="False" ></asp:RequiredFieldValidator>
                                                                </div>
                                                       </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" style="text-align: right">
                                                            Customer Name</td>
                                                        <td style="width: 24%">
                                                            <asp:TextBox ID="txtCustName" runat="server" CssClass="txtboxtxt" 
                                                                MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:CheckBox ID="chkEscalated" runat="server" Checked="false" 
                                                                Text="Escalated (Y/N)" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right">
                                                            Contact No.<font color="red">*</font>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtContactNo" runat="server" 
                                                                CssClass="txtboxtxt" MaxLength="11"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                                ControlToValidate="txtContactNo" Display="None" 
                                                                ErrorMessage="Contact Number is required." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                                                                ControlToValidate="txtContactNo" Display="None" 
                                                                ErrorMessage="Valid Contact Number is required." SetFocusOnError="True" 
                                                                ValidationExpression="\d{10,11}"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right">
                                                            Reference Complaint No.</td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtComplaintRefNo" runat="server" CssClass="txtboxtxt" 
                                                                MaxLength="15"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right">
                                                            E-Mail
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtEmail" MaxLength="60" runat="server" CssClass="txtboxtxt" Width="213px"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Valid Email is required."
                                                                ControlToValidate="txtEmail" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                Display="None"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right">
                                                            Comment<font color="red">*</font></td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtComment" runat="server" CssClass="txtboxtxt" 
                                                                MaxLength="200" Rows="3" TextMode="MultiLine" Width="213px" Height="30px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvcomment" runat="server" 
                                                                ControlToValidate="txtComment" Display="None" 
                                                                ErrorMessage="Please Enter Comments." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td style="text-align: right">
                                                            &nbsp;</td>
                                                        <td>
                                                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn" 
                                                                OnClick="btnSubmit_Click" Text="Save" Width="70px" />
                                                            <asp:Button ID="BtnNewcall" runat="server" style="margin-left:10px" CssClass="btn" 
                                                                OnClick="BtnNewcall_Click" Text="New Call" Width="70px" 
                                                                ValidationGroup="n" />
                                                        </td>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td style="text-align: right">
                                                            &nbsp;</td>
                                                        <td>
                                                            <asp:Label ID="lblmsg" runat="server" CssClass="MsgTotalCount"></asp:Label>
                                                        </td>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td colspan="4">
                                                        <table width="99%" id="tableResult" runat="server">
                                        <tr>
                                            <td>
                                                <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblcallstatus" runat="server" ></asp:Label>
                                                                    </td>
                                                                </tr>
                                                               
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
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>


