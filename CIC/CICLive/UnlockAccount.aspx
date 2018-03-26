<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UnlockAccount.aspx.cs"
    Inherits="PasswordRecovery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Welcome to Crompton Greaves - Call Center</title>
    <link rel="shortcut icon" HREF="favicon.ico" type="image/x-icon">
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <link rel="stylesheet" type="text/css" href="css/NewStyles.css" />
</head>
<!--<body background="images/bodyBg.jpg" style="background-repeat: repeat-x;"> -->
<body>
    <p><br /></p>
	<p><br /></p>
    
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scrptMngr" runat="server"></asp:ScriptManager>
    <table width="100" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="A3C624">
     <tr>
        <td>
    <asp:UpdatePanel ID="updtPanel" runat="server">
    <ContentTemplate>
        
     
          <table width="778" border="0" align="center" cellpadding="0" cellspacing="0">
          <tr>
            <td colspan="2" style="height:3Px; background-color:#A3C624;">
            </td>
          </tr>
  	      <tr>
            <td style="height:21Px;" valign="top">
        	    <table border="0" align="center" cellpadding="0" cellspacing="0">
        		    <tr><td align="center">&nbsp;</td></tr>
        		    <tr><td align="center"><img src="images/cg_logo.jpg" /></td></tr>
          		    <tr><td align="center"><img src="images/AppName.JPG" /></td>
        		    </tr>
         	    </table></td>
            <td align="center" valign="bottom">
            <div id="dvsuccess" runat="server" visible="false">
              <font class="copyright">Your password has been sent successfully to your Email id.<br />
              <a href="login.aspx">Click here</a> to go home</font>
              </div>
            </td>
          </tr>

          <tr>
            <td valign="middle" align="center">
             <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="https://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0" width="199" height="124">
              <param name="movie" value="flash/left_flash.swf" />
              <param name="quality" value="high" />
              <embed src="flash/left_flash.swf" quality="high" pluginspage="https://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash" width="199" height="124"></embed>
            </object>
            </td>
            <td style="width:90%" align="right" valign="middle"><br />
              <table id="tblAccount" runat="server"  width="100%" border="0" cellspacing="0" cellpadding="0">
  
              <tr>
                <td align="right" valign="top"><span class="sign_in_text">User name&nbsp;</span></td>
                <td style="height: 24px" align="left">
                <asp:TextBox ID="TxtUserName" runat="server" class="loginTxtbox" ValidationGroup="Log" maxlength="12" style="Width:110px" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqTxtUser" runat="server" ValidationGroup="Log"
                                                        ControlToValidate="TxtUserName" Display="Static">*</asp:RequiredFieldValidator>
                                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="Processing..." />
                             
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                           <asp:Label ID="FailureText" runat="server" EnableViewState="False" 
                                    ForeColor="#FF3300"></asp:Label>
               </td>
              </tr>
         
              <tr>
                <td style="width:51%"></td>
                <td style="width:49%"><div align="left">
                <asp:ImageButton ID="LoginButton" runat="server" CommandName="Submit" ValidationGroup="Log"
                 src="images/New-btn-send.jpg" onclick="LoginButton_Click" />
                   
             </div></td>    
              </tr>
              <tr><td colspan="2"><font class="copyright">Enter your User Id to unlock account.<br>
                  <a href="login.aspx">Click here</a> to login 
                  <br>
                  <br></br>
                  <br>
                  <br></br>
                  <br>
                  <br></br>
                  <br>
                  <br></br>
                  <br></br>
                  </br>
                  </br>
                  </br>
                  </br>
                  </br>
                  </font></td>
              </tr>
        	    <tr>
                <td colspan="2" align="right">&nbsp;&nbsp;</td>
                </tr>
            </table>

            </td>
          </tr>
          <tr>
            <td valign="top">
            <div align="center" >
		    <a href="http://www.avanthagroup.com/" target="_blank"><Img src="images/avantha_logo.jpg" vspace="3" /></a>
            </div>
            </td>
            <td style="height:12px;" valign="top" align="right">
            </td>
          </tr>

          <tr>
            <td style="height:4px; background-color:#7993B8;" colspan="4" valign="top"></td>
          </tr>
          <tr>
            <td style="height:40px;" colspan="4" valign="bottom"><div align="right" class="copyright">
               Developed in association with Avantha Technologies Limited. &nbsp;</div></td>
          </tr>
        </table>

        
        
    
    </ContentTemplate>
    </asp:UpdatePanel>
    </td>
  </tr>
</table>
  
         
  
<table width="779" border="0" align="center" cellpadding="0" cellspacing="0">
<tr>
<td align="center">
Size: Best viewed in 1024 x 768 resolution. Browser Compatible: IE: 6.0+/Firefox: 1.0.6 <br />
&copy; copyright 2008 - <a href="http://www.avanthatechnologies.com">Avantha Technologies Limited</a>
</td>
</tr>
</table>

    </form>
</body>
</html>
