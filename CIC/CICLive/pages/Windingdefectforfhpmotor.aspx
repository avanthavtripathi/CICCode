<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Windingdefectforfhpmotor.aspx.cs"
    Inherits="pages_Windingdefectforfhpmotor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Crompton Greaves :: Customer Interaction Center</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/global.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
    function SHdesc()
    {
        var rbn= document.getElementById("<%=rbnListAppInstrumentType.ClientID %>");
        var trDesc=document.getElementById("<%=txtApplicationInstTypeDesc.ClientID %>");
        var inputs = rbn.getElementsByTagName('input');
        var flag=false;
        for(var i = 0; i < inputs.length; i++)
        {
            if(inputs[i].checked && inputs[i].value.trim()=="Any Other Application (Not Mentioned in the options)"){
                flag=true;
            }
        }
    
        if(flag==true)
        {
            trDesc.style.display="block";
        }
        else
        {
            trDesc.style.display="none";
        }
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="SchmFHPMotors" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UPDTFHPMotor" runat="server">
        <ContentTemplate>
            <div>
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
                    border: 0px solid #000000;">
                    <tr>
                        <td valign="top" style="padding: 10px" class="mainTableBGcolor">
                            <table cellpadding="1" cellspacing="1" style="border-collapse: collapse; border: 1px solid #000000;"
                                width="100%">
                                <tr>
                                    <td width="49%" align="left" class="h1" colspan="3">
                                        <b>Defect Page for Winding Defect Category</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="3" >
                                    <div style="width:60%; float:left;">
                                    <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </div>
                                    <div style="width:20%; float:right;">
                                        <p align="right">
                                            <font size="2" color="red"><b>* Fields are mandatory</b></font></p>
                                            </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        Field Voltage Observed
                                    </td>
                                    <td>
                                        <font style="color: Red;">*</font>:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtFVOVolt" runat="server" Text="" CssClass="simpletxt1" MaxLength="7"
                                            TabIndex="1"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="reqFVOVolt" ControlToValidate="txtFVOVolt"
                                            ErrorMessage="*" />
                                        <asp:RegularExpressionValidator ID="rgFVOVolt" runat="server" ErrorMessage="xxx.xxx"
                                            ControlToValidate="txtFVOVolt" ValidationExpression="^\d+(\.\d\d\d)?$"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr style="display:none;">
                                    <td style="width: 15%; text-align: right;">
                                        Field Current Observed
                                    </td>
                                    <td style="width: 2%;">
                                        <font style="color: Red;">*</font>:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtFCOAmp" runat="server" CssClass="simpletxt1" Text="00.000" TabIndex="2"
                                            MaxLength="6"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="rqfFCOAmp" ControlToValidate="txtFCOAmp"
                                            ErrorMessage="*" Enabled="false" />
                                        <asp:RegularExpressionValidator ID="rgFCOAmp" runat="server" ErrorMessage="xx.xxx"
                                            ControlToValidate="txtFCOAmp" ValidationExpression="^\d+(\.\d\d\d)?$" Enabled="false" />
                                    </td>
                                </tr>
                                <tr style="border-top: 1px solid #F2F2EB;">
                                    <td style="text-align: right;">
                                        Starter Used
                                    </td>
                                    <td>
                                        <font style="color: Red;">*</font>:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:RadioButtonList ID="rbnListStarterUsed" runat="server" TabIndex="3" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator runat="server" ID="rqfrbnListStarterUsed" ControlToValidate="rbnListStarterUsed"
                                            ErrorMessage="*" InitialValue="" />
                                    </td>
                                </tr>
                                <tr style="border-top: 1px solid #F2F2EB;">
                                    <td style="width: 15%; text-align: right;">
                                        Winding Burnt Due to
                                    </td>
                                    <td style="width: 2%;">
                                        <font style="color: Red;">*</font>:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:RadioButtonList ID="rbnListWBDueto" runat="server" RepeatDirection="Horizontal"
                                            RepeatColumns="3" RepeatLayout="Table" TabIndex="4">
                                            <asp:ListItem Text="Rotor Jam" Value="Rotor Jam"></asp:ListItem>
                                            <asp:ListItem Text="Rotor Touch" Value="Rotor Touch"></asp:ListItem>
                                            <asp:ListItem Text="Bearing Jam" Value="Bearing Jam"></asp:ListItem>
                                            <asp:ListItem Text="Inter Turn Short" Value="Inter Turn Short"></asp:ListItem>
                                            <asp:ListItem Text="Overload" Value="Overload"></asp:ListItem>
                                            <asp:ListItem Text="Water Entry" Value="Water Entry"></asp:ListItem>
                                            <asp:ListItem Text="Low Voltage" Value="Low Voltage"></asp:ListItem>
                                            <asp:ListItem Text="Wrong Application" Value="Wrong Application"></asp:ListItem>
                                            <asp:ListItem Text="Flash in Cable Joint" Value="Flash in Cable Joint"></asp:ListItem>
                                            <asp:ListItem Text="High Voltage" Value="High Voltage"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator runat="server" ID="rqfWBDueto" ControlToValidate="rbnListWBDueto"
                                            ErrorMessage="*" InitialValue="" />
                                    </td>
                                </tr>
                                <tr style="border-top: 1px solid #F2F2EB;">
                                    <td style="width: 15%; text-align: right;">
                                        Burnt Winding Photo Uploaded in CIC
                                    </td>
                                    <td style="width: 2%;">
                                        <font style="color: Red;">*</font>:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:RadioButtonList ID="rbnListBWPhotoUpload" runat="server" TabIndex="5" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator runat="server" ID="rqfBWPhotoUpload" ControlToValidate="rbnListBWPhotoUpload"
                                            ErrorMessage="*" InitialValue="" />
                                    </td>
                                </tr>
                                <tr style="border-top: 1px solid #F2F2EB;">
                                    <td style="width: 15%; text-align: right;">
                                        Winding Burnt at
                                    </td>
                                    <td style="width: 2%;">
                                        <font style="color: Red;">*</font>:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:RadioButtonList ID="rbnListWindingBurntAt" runat="server" TabIndex="6" RepeatDirection="Horizontal"
                                            RepeatColumns="3">
                                            <asp:ListItem Text="DE Side" Value="DE Side"></asp:ListItem>
                                            <asp:ListItem Text="ODE Side" Value="ODE Side"></asp:ListItem>
                                            <asp:ListItem Text="In Slot" Value="In Slot"></asp:ListItem>
                                            <asp:ListItem Text="In Overhang" Value="In Overhang"></asp:ListItem>
                                            <asp:ListItem Text="TotalBurnt" Value="TotalBurnt"></asp:ListItem>
                                            <asp:ListItem Text="Cable Joint" Value="Cable Joint"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator runat="server" ID="rqfWindingBurntAt" ControlToValidate="rbnListWindingBurntAt"
                                            ErrorMessage="*" InitialValue="" />
                                    </td>
                                </tr>
                                <tr style="border-top: 1px solid #F2F2EB;">
                                    <td style="width: 15%; text-align: right;">
                                        CF Gear Defective
                                    </td>
                                    <td style="width: 2%;">
                                        <font style="color: Red;">*</font>:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:RadioButtonList ID="rbnListCFGDefective" runat="server" TabIndex="7" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator runat="server" ID="rqfCFGDefective" ControlToValidate="rbnListCFGDefective"
                                            ErrorMessage="*" InitialValue="" />
                                    </td>
                                </tr>
                                
                                <tr style="border-top: 1px solid #F2F2EB;">
                                    <td style="width: 15%; text-align: right;">
                                        Start Capacitor Damaged
                                    </td>
                                    <td style="width: 2%;">
                                        <font style="color: Red;">*</font>:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:RadioButtonList ID="rbnStartcapacitorDamaged" runat="server" TabIndex="7" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator runat="server" ID="rqfStartcapacitorDamaged" ControlToValidate="rbnStartcapacitorDamaged"
                                            ErrorMessage="*" InitialValue="" />
                                    </td>
                                </tr>
                                
                                <tr style="border-top: 1px solid #F2F2EB;">
                                    <td style="text-align: right;">
                                        OC Switch Defective
                                    </td>
                                    <td>
                                        <font style="color: Red;">*</font>:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:RadioButtonList ID="rbnListOCSwitchDefective" runat="server" TabIndex="8" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator runat="server" ID="rqfOCSwitchDefective" ControlToValidate="rbnListOCSwitchDefective"
                                            ErrorMessage="*" InitialValue="" />
                                    </td>
                                </tr>
                                
                                <tr style="border-top: 1px solid #F2F2EB;">
                                    <td style="text-align: right;">
                                        Winding Burnt In
                                    </td>
                                    <td>
                                        <font style="color: Red;">*</font>:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:RadioButtonList ID="rbnWindingBurntIn" runat="server" TabIndex="8" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Main Coil" Value="Main Coil"></asp:ListItem>
                                            <asp:ListItem Text="Aux Coil" Value="Aux Coil"></asp:ListItem>
                                            <asp:ListItem Text="Both" Value="Both"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator runat="server" ID="rqfWindingBurntIn" ControlToValidate="rbnWindingBurntIn"
                                            ErrorMessage="*" InitialValue="" />
                                    </td>
                                </tr>
                                
                                <tr style="border-top: 1px solid #F2F2EB; margin-left: 5px;">
                                    <td style="width: 15%; text-align: right;">
                                        Your Comment (Why the Winding Burnt)<br />
                                        (Maximum 50 characters)
                                    </td>
                                    <td style="width: 2%;">
                                        <font style="color: Red;">*</font>:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtYComment" runat="server" MaxLength="50" TextMode="MultiLine"
                                            TabIndex="9"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="rqfYComment" ControlToValidate="txtYComment"
                                            ErrorMessage="*" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%; text-align: right;">
                                        Your Comment Regarding Application &amp; Load<br />
                                        (Maximum 50 Characters)
                                    </td>
                                    <td style="width: 2%;">
                                        <font style="color: Red;">*</font>:
                                        <br />
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtYCWithAppLoad" runat="server" MaxLength="50" TextMode="MultiLine"
                                            TabIndex="10"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="rqfYCWithAppLoad" ControlToValidate="txtYCWithAppLoad"
                                            ErrorMessage="*" />
                                    </td>
                                </tr>
                                <tr style="border-top: 1px solid #F2F2EB;">
                                    <td style="width: 15%; text-align: right;">
                                        Application Instrument Type
                                    </td>
                                    <td style="width: 2%;">
                                        <font style="color: Red;">*</font>:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:RadioButtonList ID="rbnListAppInstrumentType" runat="server" RepeatDirection="Horizontal"
                                            TabIndex="11" RepeatColumns="2" RepeatLayout="Table" onClick="SHdesc();">
                                            <asp:ListItem Text="Exhaust Fan" Value="Exhaust Fan"></asp:ListItem>
                                            <asp:ListItem Text="Blower" Value="Blower"></asp:ListItem>
                                            <asp:ListItem Text="Cooler" Value="Cooler"></asp:ListItem>
                                            <asp:ListItem Text="Chiller Unit" Value="Chiller Unit"></asp:ListItem>
                                            <asp:ListItem Text="Condenser Unit" Value="Condenser Unit"></asp:ListItem>
                                            <asp:ListItem Text="Roof Extractor" Value="Roof Extractor"></asp:ListItem>
                                            <asp:ListItem Text="Wood Working Machine" Value="Wood Working Machine"></asp:ListItem>
                                            <asp:ListItem Text="Grinder" Value="Grinder"></asp:ListItem>
                                            <asp:ListItem Text="Wet Grinder" Value="Wet Grinder"></asp:ListItem>
                                            <asp:ListItem Text="Juicer" Value="Juicer"></asp:ListItem>
                                            <asp:ListItem Text="Drilling Machine" Value="Drilling Machine"></asp:ListItem>
                                            <asp:ListItem Text="Ice Cream Machine" Value="Ice Cream Machine"></asp:ListItem>
                                            <asp:ListItem Text="Vacuum Pump" Value="Vacuum Pump"></asp:ListItem>
                                            <asp:ListItem Text="Pump" Value="Pump"></asp:ListItem>
                                            <asp:ListItem Text="Paint Mixer" Value="Paint Mixer"></asp:ListItem>
                                            <asp:ListItem Text="Lawn Mower" Value="Lawn Mower"></asp:ListItem>
                                            <asp:ListItem Text="Tire Changer" Value="Tire Changer"></asp:ListItem>
                                            <asp:ListItem Text="Surgical Pumps" Value="Surgical Pumps"></asp:ListItem>
                                            <asp:ListItem Text="Medical Equipments" Value="Medical Equipments"></asp:ListItem>
                                            <asp:ListItem Text="X-ray Machine" Value="X-ray Machine"></asp:ListItem>
                                            <asp:ListItem Text="Lathe Machine" Value="Lathe Machine"></asp:ListItem>
                                            <asp:ListItem Text="Chaff Cutter" Value="Chaff Cutter"></asp:ListItem>
                                            <asp:ListItem Text="Rice Huller" Value="Rice Huller"></asp:ListItem>
                                            <asp:ListItem Text="Compressor" Value="Compressor"></asp:ListItem>
                                            <asp:ListItem Text="Wheel Balancer" Value="Wheel Balancer"></asp:ListItem>
                                            <asp:ListItem Text="Hoist" Value="Hoist"></asp:ListItem>
                                            <asp:ListItem Text="Atta Chakki" Value="Atta Chakki"></asp:ListItem>
                                            <asp:ListItem Text="GharGhanti" Value="GharGhanti"></asp:ListItem>
                                            <asp:ListItem Text="Flour Mill" Value="Flour Mill"></asp:ListItem>
                                            <asp:ListItem Text="Thresher" Value="Thresher"></asp:ListItem>
                                            <asp:ListItem Text="High Mast" Value="High Mast"></asp:ListItem>
                                            <asp:ListItem Text="Fuel Dispenser" Value="Fuel Dispenser"></asp:ListItem>
                                            <asp:ListItem Text="Jewelry Cutting / Polishing Machine" Value="Jewelry Cutting / Polishing Machine"></asp:ListItem>
                                            <asp:ListItem Text="Sugar cane Juice Extractor" Value="Sugar cane Juice Extractor"></asp:ListItem>
                                            <asp:ListItem Text="Floor Polishing Machine" Value="Floor Polishing Machine"></asp:ListItem>
                                            <asp:ListItem Text="Power Packing Machine" Value="Power Packing Machine"></asp:ListItem>
                                            <asp:ListItem Text="Any Other Application (Not Mentioned in the options)" Value="Any Other Application (Not Mentioned in the options)"></asp:ListItem>                                            
                                        </asp:RadioButtonList>
                                        <asp:TextBox ID="txtApplicationInstTypeDesc" style="display:none;" runat="server" 
                                            CssClass="simpletxt1" Text="" MaxLength="500"  Width="60%">
                                        </asp:TextBox>
                                        <asp:RegularExpressionValidator ID="rqfApplicationInstTypeDesc" runat="server"
                                            ErrorMessage="Only Alphabetical" ControlToValidate="txtApplicationInstTypeDesc" ValidationExpression="^[A-Za-z ]+$"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator runat="server" ID="rqfAppInstrumentType" ControlToValidate="rbnListAppInstrumentType"
                                            ErrorMessage="*" InitialValue="" />
                                        <br />
                                    </td>
                                </tr>
                                <tr style="border-top: 1px solid #F2F2EB;">
                                    <td style="width: 15%; text-align: right;">
                                        Application Instrument Manufacturer Name
                                    </td>
                                    <td style="width: 2%;">
                                        <font style="color: Red;">*</font>:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtApplinstrumentMgfname" runat="server" TabIndex="12" CssClass="simpletxt1"
                                            Text="" MaxLength="35">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="rqfApplinstrumentMgfname" ControlToValidate="txtApplinstrumentMgfname"
                                            ErrorMessage="*" />
                                    </td>
                                </tr>
                                <tr style="display:none;">
                                    <td style="width: 15%; text-align: right;">
                                        Application Instrument Current Rating
                                    </td>
                                    <td style="width: 2%;">
                                        <font style="color: Red;">*</font>:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtApplinstrumentCurrentRating" runat="server" CssClass="simpletxt1"
                                            Text="0000.000" MaxLength="8" TabIndex="13"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="rqfApplinstrumentCurrentRating" ControlToValidate="txtApplinstrumentCurrentRating"
                                            ErrorMessage="*" Enabled="false" />
                                        <asp:RegularExpressionValidator ID="rgApplinstrumentCurrentRating" runat="server"
                                          Enabled="false"   ErrorMessage="xxxx.xxx" ControlToValidate="txtApplinstrumentCurrentRating" ValidationExpression="^\d+(\.\d\d\d)?$"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%; text-align: right;">
                                        Application Instrument Voltage Rating
                                    </td>
                                    <td style="width: 2%;">
                                        <font style="color: Red;">*</font>:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtApplinstrumentVoltageRating" runat="server" CssClass="simpletxt1"
                                            Text="" MaxLength="8" TabIndex="14"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="rqfApplinstrumentVoltageRating" ControlToValidate="txtApplinstrumentVoltageRating"
                                            ErrorMessage="*" />
                                        <asp:RegularExpressionValidator ID="rgApplinstrumentVoltageRating" runat="server"
                                            ErrorMessage="xxxx.xxx" ControlToValidate="txtApplinstrumentVoltageRating" ValidationExpression="^\d+(\.\d\d\d)?$"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%; text-align: right;">
                                        Application Instrument Model No.
                                    </td>
                                    <td style="width: 2%;">
                                        <font style="color: Red;">*</font>:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtApplinstrumentModelNo" runat="server" CssClass="simpletxt1" Text=""
                                            MaxLength="35" TabIndex="15"> </asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="rqfApplinstrumentModelNo" ControlToValidate="txtApplinstrumentModelNo"
                                            ErrorMessage="*" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%; text-align: right;">
                                        Application Instrument’s Output Range/ Capacity Rating
                                    </td>
                                    <td style="width: 2%;">
                                        <font style="color: Red;">*</font>:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtApplinstrumentOPCR" runat="server" CssClass="simpletxt1" Text=""
                                            MaxLength="35" TabIndex="16"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="rqfApplinstrumentOPCR" ControlToValidate="txtApplinstrumentOPCR"
                                            ErrorMessage="*" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%; text-align: right;">
                                        Technician’s Name
                                    </td>
                                    <td style="width: 2%;">
                                        <font style="color: Red;"></font>:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtTechnisianName" runat="server" CssClass="simpletxt1" Text=""
                                            MaxLength="50" TabIndex="17" Enabled="false">
                                        </asp:TextBox>
                                       <%-- <asp:RequiredFieldValidator runat="server" ID="rqfTechnisianName" ControlToValidate="txtTechnisianName"
                                            ErrorMessage="*" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%; text-align: right;">
                                        Technician’s Mobile Ph No.
                                    </td>
                                    <td style="width: 2%;">
                                        <font style="color: Red;"></font>:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtTechnisianMobNo" runat="server" CssClass="simpletxt1" Text=""
                                            MaxLength="12" TabIndex="18" Enabled="false">
                                        </asp:TextBox>
                                       <%-- <asp:RequiredFieldValidator runat="server" ID="rqfTechnisianMobNo" ControlToValidate="txtTechnisianMobNo"
                                            ErrorMessage="*" />
                                        <asp:RegularExpressionValidator ID="rgTechnisianMobNo" runat="server" ErrorMessage="Numeric Value Only."
                                            ControlToValidate="txtTechnisianMobNo" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%; text-align: right;">
                                    </td>
                                    <td style="width: 2%;">
                                        &nbsp;
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn" TabIndex="19"
                                            OnClick="btnSubmit_Click" />
                                        &nbsp;
                                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn" TabIndex="20"
                                            OnClick="btnClear_Click" CausesValidation="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%; text-align: right;">
                                    </td>
                                    <td style="width: 2%;">
                                        &nbsp;
                                    </td>
                                    <td style="text-align: left;">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hdnFHPMotorDefectId" Value="" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
