<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login"
	AspCompat="true" %>
	
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>Welcome to Crompton Greaves - Call Center</title>
	<link rel="shortcut icon" href="favicon.ico" type="image/x-icon" />
	<meta name="verify-v1" content="A7mJVwiaqy6xEr+snBp8utvJnDuN6WFo+npik4Fymc0=" />
	<meta name=" keywords" content=" crompton greaves call center, cg call center, cgcallcenter, cg call center in india, cgcallcenter in india, crompton greaves, call center, call centers in india, call centers in delhi, crompton greaves customer support, crompton greaves customer care center, crompton greaves customer interaction center, avanthatechnologies, avantha, avantha technologies,avanthatechnologies.com" />
	<meta name="description" content=" Crompton Greaves Customer Care and Call Center Support system for product users." />
	<meta name="language" content="EN" />
	<meta http-equiv="content-language" content="en-US" />
	<meta name="copyright" content="Copyright 2008, all rights reserved with Avantha Technologies Limited" />
	<meta name="author" content="Avantha Technologies Limited, India" />
	<meta name="robots" content="index,follow" />
	<meta name="googlebot" content="index,follow" />
	<meta name="revisit-after" content="7 days" />
	<meta name="document-classification" content="Internet" />
	<meta name="classification" content="Computer" />
	<meta name="document-type" content="Public" />
	<meta name="rating" content="general" />
	<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7"/>
	<meta name="document-distribution" content="global" />
	<meta http-equiv="Content-Type" content="text/html" />
	<link rel="stylesheet" type="text/css" href="css/style.css" />
	<link rel="stylesheet" type="text/css" href="css/NewStyles.css"  />
	
	
</head>
<!--<body background="images/bodyBg.jpg" style="background-repeat: repeat-x;"> -->
<body>
	<form id="form1" runat="server">
	<p><br /></p>
  <table width="100" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="A3C624">
  <tr>
	<td>    
	 <asp:ScriptManager ID="scrptMngr" runat="server"></asp:ScriptManager>
	  <asp:UpdatePanel ID="updtPanel" runat="server">
	  <ContentTemplate>
	  <asp:Login ID="Login1" DestinationPageUrl="~/pages/Default.aspx" Width="100%" 
			 LoginButtonType="Image" runat="server" 
			 onauthenticate="Login1_Authenticate" onloginerror="Login1_LoginError">
	  <LayoutTemplate>
	<table width="778" border="0" align="center" cellpadding="0" cellspacing="0">
	  <tr>
		<td colspan="2" style="height:3Px; background-color:#A3C624;"></td>
	  </tr>
	  <tr>
		<td style="height:21Px;" valign="top">
			<table border="0" align="center" cellpadding="0" cellspacing="0">
				<tr><td align="center">&nbsp;</td></tr>
				<tr><td align="center"><img src=images/crompton_logo.png /></td></tr>
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
			<td align="right"><span class="sign_in_text">User name&nbsp;</span></td>
			<td align="left">
			<asp:TextBox ID="UserName" runat="server" class="loginTxtbox" maxlength="12" style="Width:110px"></asp:TextBox>
			<asp:RequiredFieldValidator ID="reqTxtUser" runat="server" 
			 ValidationGroup="Log"  ControlToValidate="UserName" Display="Static">*</asp:RequiredFieldValidator>
			
			</td>
		  </tr>
			  <tr><td ></td><td></td></tr>
		  <tr>
			<td align="right" style="height: 24px"><span class="sign_in_text">Password&nbsp;</span></td>
			<td style="height: 24px" align="left">
			<asp:TextBox ID="Password" runat="server" TextMode="Password" class="loginTxtbox" style="Width:110px"></asp:TextBox>
			<asp:RequiredFieldValidator ID="reqTxtPasswrd" runat="server" 
			 ValidationGroup="Log"  ControlToValidate="Password" Display="Static">*</asp:RequiredFieldValidator>
			</td>
		  </tr>
			  <tr>
				  <td style="width: 51%; height: 5px"></td>
				  <td style="width: 49%; height: 5px"></td>
			  </tr>
		  <tr>
			<td style="width:51%;">
			</td>
			<td style="width:49%"><div align="left">
			<asp:ImageButton ID="LoginButton" runat="server" 
				CommandName="Login" ValidationGroup="Log" ImageUrl="images/sign_in_button.jpg"  />
			</div></td>
		  </tr>
		  <tr>
			<td align="right" colspan="2" style="padding-right:165px">
			<asp:Label ForeColor="Red" ID="lblLoginErrors" runat="server" Text=""></asp:Label>
			</td>
		  </tr>
		  <tr>
		  <td></td>
		  <td align="right"><span class="text"><a href="PasswordRecovery.aspx" class="text">Forgot Password</a></span>&nbsp;&nbsp;&nbsp;</td>
		  </tr>
		   <tr>
		  <td></td>
		  <td align="right"><span class="text"><a href="UnlockAccount.aspx" class="text">Unlock Account</a></span>&nbsp;&nbsp;&nbsp;</td>
		  </tr>
		  
		</table>        </td>
	  </tr>
        <tr><td colspan="2" style="height:10px"></td></tr>
	 <%-- <tr>
		<td valign="top"><div align="center" >
		<a href="http://www.avanthagroup.com/" target="_blank"><Img src="images/avantha_logo.jpg" vspace="3" /></a>
		</div></td>
		<td style="height:12px;color:Red;padding-right:12px;text-align:center">
		 <div style="" >For maintanense reason, Application will be down from 7:15PM to 8.00PM.</div>
		</td>
	  </tr>--%>

	  <tr>
		<td style="height:4px; background-color:#7993B8;" colspan="4" valign="top"></td>
	  </tr>
	  <tr>
		<td style="height:40px;" colspan="4" valign="bottom"><div align="right" class="copyright">
		   Developed in association with Avantha Business Solutions. &nbsp;</div></td>
	  </tr>
	</table>

	
	
	</td></tr>
</table>


</LayoutTemplate></asp:Login>
	  <script language="javascript" type="text/javascript">
			if(document.getElementById("Login1_UserName"))
			{
			document.getElementById("Login1_UserName").focus();
			}
			if(document.getElementById("Login1_UserName").value!='')
			{
			document.getElementById("Login1_Password").focus();
			}
   
	</script>
	  </ContentTemplate>
	 </asp:UpdatePanel>
	  <table width="779" border="0" align="center" cellpadding="0" cellspacing="0">
   <tr>
   <td align="center">
   Size: Best viewed in 1024 x 768 resolution. Browser Compatible: IE: 6.0+/Firefox: 1.0.6 <br />
   &copy; copyright 2008 - <a href="http://www.avanthabsl.com/" target="_blank">Avantha Business Solution</a></td></tr>
   </table>
	</td>
	</tr>
  </table>
	</form>
	  





</body>
</html>
