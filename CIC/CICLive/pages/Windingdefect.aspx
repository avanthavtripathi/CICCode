<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Windingdefect.aspx.cs" Inherits="pages_test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Crompton Greaves :: Customer Interaction Center</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/global.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="border-collapse:collapse;border:0px solid #000000;">
	<tr>
		<td valign="top" style="padding: 10px" class="mainTableBGcolor">
			 
		   	<table cellpadding="1" cellspacing="1" style="border-collapse:collapse;border:1px solid #000000;" width="100%">
          <tr>
                    <td width="49%" align="left" class="h1"><b>
                        Defect Page for Winding 
                        Defect Category</td>
                </tr>
                <tr id="Tr1" runat="server">
                    <td align="right">  
                     <p align="right"><font size="2" color="#FF0000"><b>* Fields are mandatory</b></font></p>
                     </td>
                </tr>
<tr>
                    <td valign="top">
 		
           
            <table cellpadding="2" cellspacing="0" border="0" width="100%">
                                <tr>
                                  <td width="8%"></td>
                                    <td width="18%"></td>
                                    <td width="1%"></td>
                                    <td width="65%">
                                        <asp:HiddenField ID="HFCompNo" runat="server" />
                                    </td>
                                    <td width="8%"></td>
                                 </tr>
                                <tr>
                                    <td valign="top" class="fieldName">&nbsp;</td>
                                    <td align="left" valign="top" style="padding-top:6px">Application<font color="#FF0000">*</font></td>
                                    <td align="center" valign="top" style="padding-top:6px">:</td>
                                    <td><table width="100%" border="0" cellspacing="0" cellpadding="2">
                                      <tr>
                                        <td colspan="10"><table width="243" border="0" cellspacing="0" cellpadding="2">
                                          <tr>
                                              <td width="79"> <asp:RadioButton ID="RBDo" runat="server" Text="Domestic" 
                                                      GroupName="a" AutoPostBack="True" OnCheckedChanged="RBDo_CheckedChanged" /></td>
                                            <td width="24"></td>
                                            <td width="87">
                                                <asp:RadioButton ID="RBAg" runat="server" Text="Agriculture" 
                                                    GroupName="a" AutoPostBack="True" OnCheckedChanged="RBAg_CheckedChanged" /></td>
                                            <td width="37"></td>
                                          </tr>
                                          </table></td>
                                      </tr>
                                      <tr>
                                        <td colspan="10">
                                            <asp:RadioButtonList ID="RBAgDetail" runat="server" 
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Value="Well">Well</asp:ListItem>
                                                <asp:ListItem Value="Borewell">Borewell</asp:ListItem>
                                                <asp:ListItem Value="River bed">River bed</asp:ListItem>
                                                <asp:ListItem Value="Pond">Pond</asp:ListItem>
                                                <asp:ListItem Value="Canal">Canal</asp:ListItem>
                                            </asp:RadioButtonList>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator_RBAgDetail" runat="server" ValidationGroup="GS" 
                                      InitialValue="" ControlToValidate="RBAgDetail" SetFocusOnError="true" ErrorMessage="Please SELECT value.">
                                      </asp:RequiredFieldValidator>
                                          </td>
                                      </tr>
                                      <tr>
                                       <td colspan="10">
                                            <asp:RadioButtonList ID="RBDoDetail" runat="server" 
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Value="Well">Well</asp:ListItem>
                                                <asp:ListItem Value="Borewell">Borewell</asp:ListItem>
                                                <asp:ListItem Value="On pipe line">On pipe line</asp:ListItem>
                                                <asp:ListItem Value="Pond">Pond</asp:ListItem>
                                                <asp:ListItem Value="Tank">Tank</asp:ListItem>
                                            </asp:RadioButtonList>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator_RBDoDetail" runat="server" ValidationGroup="GS" 
                                      InitialValue="" ControlToValidate="RBDoDetail" SetFocusOnError="true" ErrorMessage="Please SELECT value.">
                                      </asp:RequiredFieldValidator>
                                          </td>
                                      </tr>
                                      </table>
                                   </td>
                                    <td align="center">&nbsp;</td>
                </tr>
                                <tr>
                                  <td class="fieldName">&nbsp;</td>
                                    <td align="left">Voltage observed <font color="#FF0000">*</font><br />For 1PH ( In Phase & neutral ) & <br />For 3PH ( In Between phases )</td>
                                    <td align="center">:</td>
                                  <td>
                                      <asp:TextBox ID="TxtVoltObv" runat="server" MaxLength="8"></asp:TextBox> (Volt.)
                                      <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
                                              ControlToValidate="TxtVoltObv" 
                                              ValidationExpression="^[1-9]+[0-9]*$"
                                              Display="Dynamic" SetFocusOnError="True" ErrorMessage="Please enter correct value. Value must be &gt;= 0 and &lt; 99999999">
                                              </asp:RegularExpressionValidator>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="GS" 
                                      InitialValue="" ControlToValidate="TxtVoltObv" SetFocusOnError="true" ErrorMessage="Please enter value.">
                                      </asp:RequiredFieldValidator>
                                                                </td>
                                    <td align="center">
                                        &nbsp;</td>
                </tr>
                                <tr>
                                  <td class="fieldName">&nbsp;</td>
                                  <td align="left"> Current observed</td>
                                  <td align="center">:</td>
                                  <td><asp:TextBox ID="TxtcurrObv" runat="server" MaxLength="6"></asp:TextBox>(AMP.)
                                  <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="GS" Type ="Double"
                                        ControlToValidate="TxtcurrObv" Operator="LessThanEqual"  ValueToCompare="99.99" Display="Dynamic" 
                                        SetFocusOnError="True" ErrorMessage="Please enter correct value.Value must be &gt;= 0 and &lt; 99.99 ">
                                        </asp:CompareValidator>

                                                                </td>
                                  <td align="center">
                                      &nbsp;</td>
                </tr>
                                <tr>
                                  <td valign="top" class="fieldName">&nbsp;</td>
                                  <td align="left" valign="top" style="padding-top:6px">Control panel details <font color="#FF0000">*</font></td>
                                  <td align="center" valign="top" style="padding-top:6px">:</td>
                                  <td>
                                  <table width="100%" border="0" cellspacing="0" cellpadding="2">
                                    
                                    <tr>
                                      <td colspan="10">
                                            <asp:RadioButtonList ID="RBLPanel" runat="server" 
                                                RepeatDirection="Horizontal" AutoPostBack="True" 
                                                onselectedindexchanged="RBLPanel_SelectedIndexChanged">
                                                <asp:ListItem Value="Starter">Starter</asp:ListItem>
                                                <asp:ListItem Value="DOL type">DOL type</asp:ListItem>
                                                <asp:ListItem Value="Star Delta type">Star Delta type</asp:ListItem>
                                                <asp:ListItem Value="Not applicable">Not applicable</asp:ListItem>
                                            </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_RBLPanel" runat="server" 
                                              ControlToValidate="RBLPanel" SetFocusOnError="True" ErrorMessage="Please SELECT value." InitialValue="" 
                                              ValidationGroup="GS">
                                          </asp:RequiredFieldValidator>                                            
                                          </td>
                                    </tr>
                                    <tr>
                                      <td colspan="10">
                                            <asp:RadioButtonList ID="RBLPanelMake" runat="server" 
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Value="CGL Make">CGL Make</asp:ListItem>
                                                <asp:ListItem Value="Non CGL Make">Non CGL Make</asp:ListItem>
                                           </asp:RadioButtonList>

                                          </td>
                                    </tr>
                                    
                                    <tr>
                                      <td>Panel HP</td>
                                      <td colspan="9">
                                          <asp:TextBox ID="TxtPanelHP" runat="server" MaxLength="6"></asp:TextBox>
                                        &nbsp;HP.
                                        
                                        <asp:CompareValidator ID="CompareValidator3" runat="server" ValidationGroup="GS" Type ="Double"
                                        ControlToValidate="TxtPanelHP" Operator="LessThanEqual"  ValueToCompare="99.99" 
                                        Display="Dynamic"  SetFocusOnError="True" 
                                        ErrorMessage="Please enter correct value. Value must be &gt;= 0 and &lt; 99.99">
                                        </asp:CompareValidator>
                                        
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                              ControlToValidate="TxtPanelHP" SetFocusOnError="True" ErrorMessage="Please enter value." InitialValue="" 
                                              ValidationGroup="GS">
                                          </asp:RequiredFieldValidator>
                                        
                                        </td>
                                    </tr>
                                    
                                  </table> 
                                 </td>
                                  <td align="center">&nbsp;</td>
                </tr>
                                <tr>
                                  <td class="fieldName">&nbsp;</td>
                                  <td align="left"> Power supply<font color="#FF0000">*</font></td>
                                  <td align="center">:</td>
                                  <td><table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                      <td><asp:RadioButtonList ID="RBLPsupply" runat="server" 
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Value="Authorised connection">Authorised connection</asp:ListItem>
                                                <asp:ListItem Value="Unauthorised connection">Unauthorised connection</asp:ListItem>
                                           </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_RBLPsupply" runat="server" 
                                              ControlToValidate="RBLPsupply" SetFocusOnError="True" ErrorMessage="Please SELECT value." InitialValue="" 
                                              ValidationGroup="GS">
                                          </asp:RequiredFieldValidator> 
                                           </td>
                                    </tr>
                                  </table></td>
                                  <td align="center">&nbsp;</td>
                </tr>
                                <tr>
                                  <td valign="top" class="fieldName">&nbsp;</td>
                                  <td align="left" valign="top" style="padding-top:6px">Cable size & length<font color="#FF0000">*</font></td>
                                  <td align="center" valign="top" style="padding-top:6px">:</td>
                                  <td><table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                      <td>
                                          <asp:TextBox ID="TxtSize" runat="server" MaxLength="6"></asp:TextBox>
                                        &nbsp;Sq. mm.
                                           <asp:CompareValidator ID="CompareValidator9" runat="server" 
                                              ValidationGroup="GS" Type ="Double"
                                        ControlToValidate="TxtSize" Operator="LessThanEqual"  ValueToCompare="99.99" 
                                        Display="Dynamic" SetFocusOnError="True" 
                                        ErrorMessage="Please enter correct value. Value must be &gt;= 0 and &lt; 99.99">
                                        </asp:CompareValidator>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="GS" 
                                      InitialValue="" ControlToValidate="TxtSize" SetFocusOnError="True" ErrorMessage="Please enter value.">
                                      </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                      <td height="5px"></td>
                                    </tr>
                                    <tr>
                                      <td>
                                          <asp:TextBox ID="TxtLen" runat="server" MaxLength="5"></asp:TextBox>
                                            &nbsp;mtr. Approx length in mtr from panel to motor terminal 
                                           <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                                              ControlToValidate="TxtLen" 
                                              ValidationExpression="^[1-9]+[0-9]*$"
                                              Display="Dynamic" SetFocusOnError="True" ErrorMessage="Please enter correct value. Value must be &gt;= 0 and &lt; 99999">
                                              </asp:RegularExpressionValidator>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                              ControlToValidate="TxtLen" SetFocusOnError="True" ErrorMessage="Please enter value." InitialValue="" 
                                              ValidationGroup="GS">
                                          </asp:RequiredFieldValidator>
                                            </td>
                                    </tr>
                                  </table></td>
                                  <td align="center">&nbsp;</td>
                </tr>
                                <tr>
                                  <td class="fieldName">&nbsp;</td>
                                  <td align="left">Opearting head</td>
                                  <td align="center">:</td>
                                  <td>
                                   
                                      <asp:TextBox ID="TxtOhead" runat="server" MaxLength="5"></asp:TextBox>
                                        &nbsp;mtr.
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                                              ControlToValidate="TxtOhead" 
                                              ValidationExpression="^[1-9]+[0-9]*$"
                                              Display="Dynamic" SetFocusOnError="True" ErrorMessage="Please enter correct value. Value must be &gt;= 0 and &lt; 99999">
                                              </asp:RegularExpressionValidator>
                                                                </td>
                                  <td align="center">
                                      &nbsp;</td>
                </tr>
                                <tr>
                                  <td class="fieldName">&nbsp;</td>
                                  <td align="left">Pipe size<font color="#FF0000">*</font></td>
                                  <td align="center">:</td>
                                  <td><table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                      <td width="14%">Suction side</td>
                                      <td width="25%">
                                          <asp:TextBox ID="TxtSside" runat="server" Width="83px" MaxLength="3"></asp:TextBox>
                                                &nbsp;mm.
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                              ControlToValidate="TxtSside" 
                                              ValidationExpression="^[1-9]+[0-9]*$"
                                              Display="Dynamic" SetFocusOnError="True" ErrorMessage="Please enter correct value. Value must be &gt;= 0 and &lt; 999">
                                              </asp:RegularExpressionValidator>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                              ControlToValidate="TxtSside" SetFocusOnError="True" ErrorMessage="Please enter value." InitialValue="" 
                                              ValidationGroup="GS">
                                          </asp:RequiredFieldValidator>
                                        </td>
                                      <td width="14%">Delivery side</td>
                                      <td width="47%">
                                          <asp:TextBox ID="TxtDside" runat="server" MaxLength="3" Width="90px"></asp:TextBox>
                                        &nbsp;mm.
                                          <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                              ControlToValidate="TxtDside" 
                                              ValidationExpression="^[1-9]+[0-9]*$"
                                              Display="Dynamic" SetFocusOnError="True" ErrorMessage="Please enter correct value. Value must be &gt;= 0 and &lt; 999">
                                              </asp:RegularExpressionValidator>
                                        
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                              ControlToValidate="TxtDside" SetFocusOnError="True" ErrorMessage="Please enter value." InitialValue="" 
                                              ValidationGroup="GS">
                                          </asp:RequiredFieldValidator>
                                                                            </td>
                                    </tr>
                                  </table></td>
                                  <td align="center">
                                      &nbsp;</td>
                </tr>
                                <tr>
                                  <td valign="top" class="fieldName">&nbsp;</td>
                                  <td align="left" valign="top" style="padding-top:6px">WDG failure details observed after motor dismentling*</td>
                                  <td align="center" valign="top" style="padding-top:6px">:</td>
                                  <td><table width="100%" border="0" cellspacing="0" cellpadding="2">
                                    <tr>
                                      <td colspan="9"><b>WDG Burn Due to:</b></td>
                                    </tr>
                                    <tr>
                                      <td colspan="9">
                                            <asp:RadioButtonList ID="RBLWDG" runat="server" 
                                                RepeatDirection="Horizontal" RepeatColumns="5" AutoPostBack="True" 
                                                onselectedindexchanged="RBLWDG_SelectedIndexChanged">
                                                <asp:ListItem Value="Pump jam">Pump jam</asp:ListItem>
                                                <asp:ListItem Value="Rotor jam">Rotor jam</asp:ListItem>
                                                <asp:ListItem Value="Bearing jam">Bearing jam</asp:ListItem>
                                                <asp:ListItem Value="Coil burn">Coil burn</asp:ListItem>
                                                <asp:ListItem Value="Seal leakage">Seal leakage</asp:ListItem>
                                                <asp:ListItem Value="Dry running">Dry running</asp:ListItem>
                                                <asp:ListItem Value="Single phasing">Single phasing</asp:ListItem>                                                
                                                 <asp:ListItem Value="Overload">Overload</asp:ListItem>
                                                <asp:ListItem Value="Water entry">Water entry</asp:ListItem>
                                                <asp:ListItem Value="Due to TOP">Due to TOP</asp:ListItem>
                                                <asp:ListItem Value="Not applicable">Not applicable</asp:ListItem>
                                           </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_RBLWDG" runat="server" 
                                              ControlToValidate="RBLWDG" SetFocusOnError="True" ErrorMessage="Please SELECT value." InitialValue="" 
                                              ValidationGroup="GS">
                                          </asp:RequiredFieldValidator> 
                                          </td>
                                    </tr>
                                    <tr>
                                      <td colspan="9">
                                          <asp:RadioButtonList ID="RBLShort" runat="server" RepeatColumns="3" 
                                              RepeatDirection="Horizontal" TextAlign="Left">
                                              <asp:ListItem Value="MDE side"> Wdg melt - DE side</asp:ListItem>
                                              <asp:ListItem Value="MODE side">ODE side</asp:ListItem>
                                              <asp:ListItem Value="MIn slot">In slot</asp:ListItem>
                                              <asp:ListItem Value="SDE side">Interturn short - DE side</asp:ListItem>
                                              <asp:ListItem Value="SODE side">ODE side</asp:ListItem>
                                              <asp:ListItem Value="SIn slot">In slot</asp:ListItem>
                                              <asp:ListItem Value="FDE side">Wdg flash - DE side</asp:ListItem>
                                              <asp:ListItem Value="FODE side">ODE side</asp:ListItem>
                                              <asp:ListItem Value="FIn slot">In slot</asp:ListItem>
                                              <asp:ListItem Value="PDE side">Wdg punture - DE side</asp:ListItem>
                                              <asp:ListItem Value="PODE side">ODE side</asp:ListItem>
                                              <asp:ListItem Value="PIn slot">In slot</asp:ListItem>
                                          </asp:RadioButtonList>

                                        </td>
                                      
                                    </tr>                                    
                                    
                                    
                                    <tr>
                                      <td>Cable joint</td>
                                      <td colspan="8" align="left">
                                      <asp:RadioButtonList ID="RBLJoint" runat="server" 
                                                RepeatDirection="Horizontal" RepeatColumns="5">
                                                <asp:ListItem Value="Short">Short</asp:ListItem>
                                                <asp:ListItem Value="Melt">Melt</asp:ListItem>
                                      </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_RBLJoint" runat="server" 
                                              ControlToValidate="RBLJoint" SetFocusOnError="True" ErrorMessage="Please SELECT value." InitialValue="" 
                                              ValidationGroup="GS">
                                          </asp:RequiredFieldValidator> 
                                      </td>
                                    </tr>
              
                                <tr>
                                  <td class="fieldName">&nbsp;</td>
                                  <td colspan="3" align="center">
                                      <asp:Button ID="BtnSubmit" runat="server" onclick="BtnSubmit_Click" 
                                          Text="Submit" ValidationGroup="GS" />
                                      <asp:Button ID="BtnUpdate" runat="server" onclick="BtnUpdate_Click" 
                                          Text="Update" ValidationGroup="GS" />
              
                                    </td>
                                  <td align="center">&nbsp;</td>
                </tr>
                                <tr>
                                  <td colspan="5" height="10">
                                      <asp:Label ID="LblMsg" runat="server"></asp:Label>
                                    </td>
                                </tr>
                           </table>
            </td>
            </tr>
              </table>
		   
         <div> </div>
		</td>
	</tr>
</table>
    </div>
    </form>
</body>
</html>
