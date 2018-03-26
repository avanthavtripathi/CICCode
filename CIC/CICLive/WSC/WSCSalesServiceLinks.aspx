<%@ Page Language="C#" MasterPageFile="~/MasterPages/WSCCICPage.master" AutoEventWireup="true"
    CodeFile="WSCSalesServiceLinks.aspx.cs" Inherits="WSC_WSCSalesServiceLinks" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script language="javascript" type="text/javascript">
    function funHideUnhide()
    {
        if(document.getElementById("trNewCustomer").style.display="")
        {
          document.getElementById("trNewCustomer").style.display="none"
        }
        else
        {
         document.getElementById("trNewCustomer").style.display=""
        }
        
    }
            
           
    </script>

    <table width="100%">
        <tr>
            <td class="headingredWSC">
               
            </td>
            <td align="right">
                <a href="javascript:void(0)" class="MTOLink" onclick="window.close();">Close</a>
            </td>
        </tr>
        <tr>
            <td colspan="2" height="400" valign="top">
                <table width="62%" border="0" cellspacing="0" cellpadding="0" align="center">
                    <tr>
                        <td colspan="2">
                             <div id="dvcomment" runat="server" style="width:653px;height:479px;background-image: url('../images/CGWelLogo.jpg');background-repeat:no-repeat" class="modalPopup" >
                            <div style="position:relative;float:left;">  <img src="../images/cg_logo.jpg" /></div>
                            <div style="float:none">
                                    <table width="75%">
        <tr>
            <td style="padding-top:20px; padding-bottom: 20px; font-weight: bold; font-size: large;color:#0066cc" 
                align="center">
               Welcome To CG Customer Connect
            </td>
        </tr>
        <tr>
            <td height="200" style="padding-top:40px;padding-left:145px">
                   <table border="0" cellspacing="0" cellpadding="0" >
                    <tr>
                        <td height="50" align="center" colspan="2" style="font-weight:bold;font-size:14px;color:White" >
                            SELECT PRODUCT </td>
                       
                    </tr>
               
                    <tr>
                        <td height="30">
                            <asp:DropDownList ID="DDlUnits" runat="server" Width="200px" 
                                CssClass="simpletxt1" AutoPostBack="True" style="font-size:13px;font-weight:bold"
                                onselectedindexchanged="DDlUnits_SelectedIndexChanged">
                                <asp:ListItem Text="Select" Value="0" ></asp:ListItem>
                                <asp:ListItem Text="LT Motors" Value="15" ></asp:ListItem>
                                <asp:ListItem Text="FHP Motors" Value="19" ></asp:ListItem>
                                <asp:ListItem Text="Transformer" Value="20,21,23" ></asp:ListItem>
                                <asp:ListItem Text="Switchgear" Value="24,25,26,28" ></asp:ListItem>
                                <asp:ListItem Text="HT Motors" Value="22" ></asp:ListItem>
                                <asp:ListItem Text="Railway Signaling & Coach Application" Value="31" ></asp:ListItem>
                                <asp:ListItem Text="Drives & Automation" Value="33" ></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                        <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                        </asp:UpdateProgress>
                        </td>
                    </tr>
                   </table>
              </td>
        </tr>
    </table>
                            </div>
                                    </div>
                                     <asp:LinkButton runat="server" Text="Cancel" ID="lbtncancel" style="display:none" ></asp:LinkButton>
                                            <cc1:ModalPopupExtender runat="server" ID="mpas" TargetControlID="lbtncancel" BackgroundCssClass="modalBgCG" 
                                            PopupControlID="dvcomment" >
                                            </cc1:ModalPopupExtender>
                        </td>
                        <td width="27%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="24" colspan="2">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr id="tollFree" runat="server" visible="false" >
                        <td height="24" width="10%" valign="top" 
                            style="padding-top: 2px; width: 73%; font-size:14px" colspan="2">
                        <ul>
                            <li>
                              <b style="font-size:14px">To Lodge your service request
                                <a href="../pages/ComplaintRegistrationIS.aspx">Click here  </a></b>
                            </li>
                            </ul>
                        
                        <br />
                       <div style="text-align:center;font-weight:bold">OR</div>
                        <br />
                            <ul>
                        <li>  To speak to our customer service executive regarding any 
                            "Industrial service" requirement,
                             please dial our Toll Free Number <b> 1800-267-0505</b> (9.00 a.m. to 7.00 p.m.) 
                              (9.00 a.m. to 7.00 p.m.)((FHP and LT Motor).
                           
                          
                        
                        </li>
<br />
                                 <li>  A-	To speak to our customer service executive regarding any Industrial service
                                     <a target="_blank" style="text-decoration: none;" href="http://www.crompton.co.in/">Consumer Product</a>

                                     service requirement, please dial our Toll 
                                     Free Number <b>18004190505</b> (9.00 a.m. to 7.00 p.m.) (FAN, PUMP, LIGHTING and APPLIANCES). (9.00 a.m. to 7.00 p.m.)
                          
                        
                        </li>
                        </ul>
                             <ul style="display:none">
                       <li>
                         Click here for <asp:HyperLink ID="lbtnsclocator" runat="server" CssClass="blueText" Font-Bold="true"
                              NavigateUrl="~/pages/SCLocator.aspx" Text="Authorized Service Center"></asp:HyperLink>
                       </li>
                       </ul>  
                        </td>
                        <td valign="bottom" align="center">
                            <img src="../images/Call-Center.jpg" border="0">
                        </td>
                    </tr>
                    <tr id="trmto" runat="server" visible="false" >
                        <td height="24" width="10%" valign="top" 
                            style="padding-top: 2px; width: 73%; font-size:14px" colspan="2">
                     
                                       For <b>Transformer/switchgear/HT Motors/Railway Signaling & Coach Application</b>
                                          <ul>
                        <li>To send  Feedback / Suggestion
                        <br /> <br />
                            <asp:LinkButton ID="lbtnNewUser" runat="server" CssClass="blueText" Text="New User"
                                OnClick="lbtnNewUser_Click"></asp:LinkButton> : Click here to Register
                                 <br /> <br />
                            <asp:LinkButton ID="lbtnExistingUser" runat="server" CssClass="blueText" Text="Existing User"
                                OnClick="lbtnExistingUser_Click"></asp:LinkButton> : Click here  to login 
                        </li>
                        </ul>    
             
                        </td>
                        <td valign="top" align="left" >
                            <img src="../images/Special_Transformer.png" border="0" alt="" />
                        </td>
                    </tr>
                    <tr>
                        <td height="24" colspan="2" valign="top">
                            &nbsp;
                        </td>
                        <td valign="top">
                            &nbsp;
                        </td>
                    </tr>
                   <tr>
                        <td height="12" colspan="2" valign="top">
                            &nbsp;
                        </td>
                        <td valign="top">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
