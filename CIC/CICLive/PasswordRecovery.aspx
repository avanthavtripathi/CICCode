<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PasswordRecovery.aspx.cs"
    Inherits="PasswordRecovery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Welcome to Crompton Greaves - Call Center</title>
    <link rel="shortcut icon" HREF="favicon.ico" type="image/x-icon">
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <link rel="stylesheet" type="text/css" href="css/NewStyles.css" />
</head>

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
            <td colspan="2" style="background-color:#A3C624;">
            </td>
          </tr>
  	      <tr>
            <td style="height:21Px;" valign="top">
        	    <table border="0" align="center" cellpadding="0" cellspacing="0">
        		    <tr><td align="center">&nbsp;</td></tr>
        		    <tr><td align="center"><img src="images/cg_logo.jpg" /></td></tr>
        		    <tr><td align="center">&nbsp;</td></tr>
        		    <tr><td align="center"><img src="images/AppName.JPG" /></td>
        		    </tr>
         	    </table></td>
            <td align="center" height="229"><img src="images/cic-home.jpg"></td>
          </tr>

          <tr>
            <td valign="middle" align="center">
            <!--<img src="images/left.JPG" /> -->
            <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="https://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0" width="199" height="124">
              <param name="movie" value="flash/left_flash.swf" />
              <param name="quality" value="high" />
              <embed src="flash/left_flash.swf" quality="high" pluginspage="https://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash" width="199" height="124"></embed>
            </object>
            </td>
            <td style="width:90%" align="right" valign="middle"><br />
              <table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td align="right" height="20" width="200px"  ></td>
                <td align="left"  >&nbsp;</td>
                  <td align="left">
                      &nbsp;</td>
              </tr>
	              <tr><td ></td><td ></td>
                      <td>
                          &nbsp;</td>
                  </tr>
              <tr>
                <td align="right" ><span class="sign_in_text">User name&nbsp;</span></td>
                <td align="left" >
                <asp:TextBox ID="TxtUserName" runat="server" class="loginTxtbox" ValidationGroup="Log" maxlength="12" style="Width:110px" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqTxtUser" runat="server" ValidationGroup="Log"
                                                        ControlToValidate="TxtUserName" Display="Static">*</asp:RequiredFieldValidator>
                                                           
               </td>
                  <td align="left">
                       <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                       <img alt="Processing..." src="images/loading9.gif" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
             </td>
              </tr>
                  <tr>
                      <td ></td>
                      <td align="left" colspan="2">

                          <asp:Label ID="LblMsg" runat="server" ></asp:Label>

                </td>
                      <td align="left">
                          &nbsp;</td>
                  </tr>
              <tr>
                <td  ></td>
                <td  ><div align="left">
                <asp:ImageButton ID="BtnLoginButton" runat="server" ValidationGroup="Log"
                 src="images/New-btn-send.jpg" onclick="BtnLoginButton_Click" />
                   
             </div></td>    
                  <td >
                      </td>
              </tr>
              <tr><td ></td>
                  <td align="right" style="padding-right:10px"><font class="copyright">Enter your User Id to receive password.<br>
                <a href="login.aspx">Click here</a> to login </font></td>
                  <td>
                      &nbsp;</td>
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

        <script language="javascript" type="text/javascript">
            if(document.getElementById("TxtUserName"))
            {
            document.getElementById("TxtUserName").focus();
            }
          
   
    </script>
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
