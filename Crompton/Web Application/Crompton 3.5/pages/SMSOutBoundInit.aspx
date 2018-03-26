<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="SMSOutBoundInit.aspx.cs" Inherits="pages_OutBoundInit" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function funCommLog(compNo,splitNo,Iscallclosed)
        {
            var strUrl='CommunicationLog.aspx?CompNo='+ compNo + '&SplitNo='+ splitNo + '&Iscallclosed=' + Iscallclosed ;
            window.open(strUrl,'CommunicationLog','height=550,width=750,left=20,top=30');
        }
        function funHistoryLog(compNo,splitNo)
        {
            var strUrl='HistoryLog.aspx?CompNo='+ compNo + '&SplitNo='+ splitNo;
            window.open(strUrl,'History','height=550,width=750,left=20,top=30');
        } 
        
         function validateAppointmentDatedt(oSrc, args) {
            var varActiondate = (document.getElementById('ctl00_MainConHolder_txtAppointMentDate').value);
            var varServerDate = (document.getElementById('ctl00_MainConHolder_hdnGlobalDate').value);
            var arrayAction = varActiondate.split('/');
            var arrayServer = varServerDate.split('/');
            var actionDate = new Date();
            var serverDate = new Date();
            actionDate.setFullYear(arrayAction[2], (arrayAction[0] - 1), arrayAction[1]);
            actionDate.setHours(0, 0, 59, 0);
            serverDate.setFullYear(arrayServer[2], (arrayServer[0] - 1), arrayServer[1]);
            serverDate.setHours(0, 0, 59, 0);
            if (actionDate < serverDate) {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }
        
      function OpenCustomerPop() {
       var CustID = document.getElementById('<%= HdnCustID.ClientID %>');
       nWindow = window.open('UpdateCustAddress.aspx?CustID='+ CustID.value,'CustomerInfo', 'height=500,width=800,scrollbars=1,resizable=yes,top=1');
        if (window.focus) {
            nWindow.focus()
        }
       return false;
    }   
        
        
        
       
    </script>

    <asp:UpdatePanel ID="pnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="1000" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="headingred">
                        Out Bound Call : Appointment Schedule (for Status updated by SMS)
                    </td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress AssociatedUpdatePanelID="pnl" ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" /></ProgressTemplate>
                        </asp:UpdateProgress>
                </tr>
                <tr>
                    <td class="bgcolorcomm" colspan="2">
                        <%--<asp:Panel ID="panMain" runat="server">--%>
                            <table width="100%" border="0" cellpadding="1" cellspacing="0" style="margin-bottom: 27px">
                                <tr>
                                    <td>
                                        <b>Customer Details</b>
                                </tr>
                                <tr>
                                    <td>
                                      <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                               <tr>
                                                <td width="25%" style="padding-left: 60px">
                                                    Select Complaint
                                                </td>
                                                <td colspan="2">
                                                    <asp:DropDownList CssClass="simpletxt1" ID="ddlCustomers" ValidationGroup="grpGo"
                                                        runat="server" Width="200px">
                                                    </asp:DropDownList>
                                                     <asp:Button ID="btnGo" ValidationGroup="grpGo" runat="server" Text="Go" CssClass="btn"
                                                        OnClick="btnGo_Click" />
                                                        <div>
                                                        <asp:RequiredFieldValidator ID="rfvCust" runat="server" ErrorMessage="Select Customer"
                                                        ControlToValidate="ddlCustomers" InitialValue="0" ValidationGroup="grpGo"  ></asp:RequiredFieldValidator>
                                                       </div>
                                                </td>
                                             
                                                <td>
                                                     <asp:HiddenField ID="hdnSMSID" runat="server" />
                                                     <asp:HiddenField ID="hdnGlobalDate" runat="server" />
                                                     <asp:HiddenField ID="HdnCustID" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                            <td colspan="4">
                                            <table id="tblcust" runat="server" style="display:none;width:100%">
                                               <tr>
                                                <td width="25%" style="padding-left: 60px">
                                                    Pri. Phone:
                                                </td>
                                                <td width="25%">
                                                    <asp:Label ID="txtUnique" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td width="20%">
                                                    Alt. Phone:
                                                </td>
                                                <td width="30%">
                                                    <asp:Label ID="txtAltPhone" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 60px">
                                                    Alt. Phone:
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    Email:
                                                </td>
                                                <td valign="middle">
                                                    <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 60px">
                                                    Extension:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblExt" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td>
                                                    Fax:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblFax" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 60px">
                                                    Name:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td>
                                                    Address:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 60px">
                                                    Landmark:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblLandmark" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td>
                                                    Country:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCountry" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 60px">
                                                    State:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblState" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td>
                                                    City:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCity" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 60px">
                                                    Pin Code:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPinCode" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td>
                                                    Compnay Name:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCompany" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                   <td style="padding-left: 60px">
                                                       &nbsp;</td>
                                                   <td>
                                                       <asp:LinkButton ID="LbtnUpdateCustInfo" runat="server" 
                                                           OnClientClick="OpenCustomerPop();" Visible="False" >Update Information</asp:LinkButton>
                                                   </td>
                                                   <td>
                                                       &nbsp;</td>
                                                   <td>
                                                       &nbsp;</td>
                                               </tr>
                                            </table>
                                            </td>
                                            </tr>
                                         
                                               <tr>
                                                   <td style="padding-left: 60px">
                                                       &nbsp;</td>
                                                   <td>
                                                       &nbsp;</td>
                                                   <td>
                                                       &nbsp;</td>
                                                   <td>
                                                       &nbsp;</td>
                                               </tr>
                                        </table>
                                    </td>
                                </tr>
                             
                               
                             
                                <tr>
                                    <td>
                                     <asp:GridView PageSize="15" RowStyle-CssClass="gridbgcolor" AlternatingRowStyle-CssClass="fieldName"
                                                HeaderStyle-CssClass="fieldNamewithbgcolor" GridLines="both" AutoGenerateColumns="False" ID="gvCommunication" runat="server" 
                                                Width="98%" HorizontalAlign="Left">
                                                <RowStyle CssClass="gridbgcolor" />
                                                <Columns>
                                                    <asp:BoundField DataField="Sno" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderText="SNo">
                                                        <HeaderStyle HorizontalAlign="Left" Width="30px" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CommentDate" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-HorizontalAlign="Left" HeaderText="Date">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                  
                                                    <asp:BoundField DataField="Remarks" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderText="Remarks">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="StageDesc" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderText="Status">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                      <asp:BoundField DataField="Name" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-HorizontalAlign="Left" HeaderText="User">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="RoleName" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-HorizontalAlign="Left" HeaderText="Role">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    
                                                    
                                                </Columns>
                                                <EmptyDataTemplate>
                                                   <img src="<%=ConfigurationManager.AppSettings["UserMessage"]%>" alt="" /><b>No Records found.</b>
                                                </EmptyDataTemplate>
                                                <HeaderStyle CssClass="fieldNamewithbgcolor" />
                                                <AlternatingRowStyle CssClass="fieldName" />
                                            </asp:GridView>
                                     
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td>
                                       <table width="100%" border="0" cellpadding="1" cellspacing="0">
                                                <tr>
                                                    <td colspan="4">
                                                    </td>
                                                </tr>
                                                <tr>
                                               
                                                    <td align="center" colspan="4">
                                                        <b>Schedule Appointment</b>
                                                    </td>
                                                   
                                                </tr>
                                               <tr>
                                                   <td align="right"> Date :</td>
                                                   <td colspan ="3">
                                                   
                                                <asp:TextBox ID="txtAppointMentDate" runat="server" Width="100" CssClass="txtboxtxt"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender3" TargetControlID="txtAppointMentDate"
                                                    runat="server">
                                                </cc1:CalendarExtender>
                                                       <asp:DropDownList ID="ddlInitAppHr" runat="server" ValidationGroup="init"
                                                    CssClass="simpletxt1">
                                                    <asp:ListItem>1</asp:ListItem>
                                                    <asp:ListItem>2</asp:ListItem>
                                                    <asp:ListItem>3</asp:ListItem>
                                                    <asp:ListItem>4</asp:ListItem>
                                                    <asp:ListItem>5</asp:ListItem>
                                                    <asp:ListItem>6</asp:ListItem>
                                                    <asp:ListItem>7</asp:ListItem>
                                                    <asp:ListItem>8</asp:ListItem>
                                                    <asp:ListItem>9</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>11</asp:ListItem>
                                                    <asp:ListItem>12</asp:ListItem>
                                                </asp:DropDownList>
                                                :
                                               <asp:DropDownList ID="ddlInitAppMin" runat="server" ValidationGroup="init" CssClass="simpletxt1">
                                                    <asp:ListItem Value="0">00</asp:ListItem>
                                                    <asp:ListItem Value="1">01</asp:ListItem>
                                                    <asp:ListItem Value="2">02</asp:ListItem>
                                                    <asp:ListItem Value="3">03</asp:ListItem>
                                                    <asp:ListItem Value="4">04</asp:ListItem>
                                                    <asp:ListItem Value="5">05</asp:ListItem>
                                                    <asp:ListItem Value="6">06</asp:ListItem>
                                                    <asp:ListItem Value="7">07</asp:ListItem>
                                                    <asp:ListItem Value="8">08</asp:ListItem>
                                                    <asp:ListItem Value="9">09</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>11</asp:ListItem>
                                                    <asp:ListItem>12</asp:ListItem>
                                                    <asp:ListItem>13</asp:ListItem>
                                                    <asp:ListItem>14</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>16</asp:ListItem>
                                                    <asp:ListItem>17</asp:ListItem>
                                                    <asp:ListItem>18</asp:ListItem>
                                                    <asp:ListItem>19</asp:ListItem>
                                                    <asp:ListItem>20</asp:ListItem>
                                                    <asp:ListItem>21</asp:ListItem>
                                                    <asp:ListItem>22</asp:ListItem>
                                                    <asp:ListItem>23</asp:ListItem>
                                                    <asp:ListItem>24</asp:ListItem>
                                                    <asp:ListItem>25</asp:ListItem>
                                                    <asp:ListItem>26</asp:ListItem>
                                                    <asp:ListItem>27</asp:ListItem>
                                                    <asp:ListItem>28</asp:ListItem>
                                                    <asp:ListItem>29</asp:ListItem>
                                                    <asp:ListItem>30</asp:ListItem>
                                                    <asp:ListItem>31</asp:ListItem>
                                                    <asp:ListItem>32</asp:ListItem>
                                                    <asp:ListItem>33</asp:ListItem>
                                                    <asp:ListItem>34</asp:ListItem>
                                                    <asp:ListItem>35</asp:ListItem>
                                                    <asp:ListItem>36</asp:ListItem>
                                                    <asp:ListItem>37</asp:ListItem>
                                                    <asp:ListItem>38</asp:ListItem>
                                                    <asp:ListItem>39</asp:ListItem>
                                                    <asp:ListItem>40</asp:ListItem>
                                                    <asp:ListItem>41</asp:ListItem>
                                                    <asp:ListItem>42</asp:ListItem>
                                                    <asp:ListItem>43</asp:ListItem>
                                                    <asp:ListItem>44</asp:ListItem>
                                                    <asp:ListItem>45</asp:ListItem>
                                                    <asp:ListItem>46</asp:ListItem>
                                                    <asp:ListItem>47</asp:ListItem>
                                                    <asp:ListItem>48</asp:ListItem>
                                                    <asp:ListItem>49</asp:ListItem>
                                                    <asp:ListItem>50</asp:ListItem>
                                                    <asp:ListItem>51</asp:ListItem>
                                                    <asp:ListItem>52</asp:ListItem>
                                                    <asp:ListItem>53</asp:ListItem>
                                                    <asp:ListItem>54</asp:ListItem>
                                                    <asp:ListItem>55</asp:ListItem>
                                                    <asp:ListItem>56</asp:ListItem>
                                                    <asp:ListItem>57</asp:ListItem>
                                                    <asp:ListItem>58</asp:ListItem>
                                                    <asp:ListItem>59</asp:ListItem>
                                                    <asp:ListItem>60</asp:ListItem>
                                                </asp:DropDownList>
                                                :
                                               <asp:DropDownList ID="ddlInitAppAm" runat="server" ValidationGroup="init" CssClass="simpletxt1">
                                                    <asp:ListItem>AM</asp:ListItem>
                                                    <asp:ListItem>PM</asp:ListItem>
                                                </asp:DropDownList>
                                      <div>
                                                 <asp:CustomValidator ID="CustomVali12" runat="server" ControlToValidate="txtAppointMentDate" 
                                                 Display="Dynamic" ErrorMessage="Appointment Date cannot be past date"
                                                 ClientValidationFunction="validateAppointmentDatedt" ValidationGroup="grp" ></asp:CustomValidator>
                                                <asp:RequiredFieldValidator ID="rfvdate" runat="server" Display="Dynamic" ControlToValidate="txtAppointMentDate" ValidationGroup="grp" ErrorMessage="Appointment Date cannot be blank." ></asp:RequiredFieldValidator>
                                      </div>
                                                   </td>
                                                    
                                                </tr>
                                                 <tr>
                                                    <td align="right">
                                                        Remark :</td>
                                                    <td align="left" valign="top">
                                                        <asp:TextBox ID="txtComment" runat="server" CssClass="txtboxtxt" Height="50px" 
                                                            TextMode="MultiLine" ValidationGroup="grp" Width="200px" MaxLength="500" ></asp:TextBox>
                                                        <asp:CheckBox ID="ChkFalseUpdate" runat="server" Text="False Update by Engineer" style="vertical-align:top" />
                                                        <div>
                                                         <asp:RequiredFieldValidator ID="rfvRemark" runat="server" Display="Dynamic" ControlToValidate="txtComment" ValidationGroup="grp" ErrorMessage="Enter Remark."  ></asp:RequiredFieldValidator>
                                                        </div>
                                                    </td>
                                                    <td align="center"  >
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                <td>&nbsp;</td>
                                                <td align="left" valign="top">
                                                    <asp:Button ID="btnSave" runat="server" CssClass="btn" OnClick="btnSave_Click" 
                                                        Text="Save Appointment" ValidationGroup="grp" Width="100px" />
                                                    </td>
                                                <td align="center">
                                                
                                                    &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td align="left" valign="top">
                                                     &nbsp;</td>
                                                    <td align="center">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td align="left" valign="top">
                                                    
                                                        <b>OR</b>
                                                    </td>
                                                    <td align="center">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td align="left" valign="top">
                                                        &nbsp;</td>
                                                    <td align="center">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td align="left" valign="top">
                                                        <asp:DropDownList ID="DDLStatus" runat="server" CssClass="simpletxt1" 
                                                            ValidationGroup="init">
                                                            <asp:ListItem Text="Select" Value="0" />
                                                            <asp:ListItem Text="Customer did not Responded" Value="NR" />
                                                            <asp:ListItem Text="PhoneNumber is unreachable" Value="UN" />
                                                            <asp:ListItem Text="Right party not available" Value="NA" />
                                                            <asp:ListItem Text="PhoneNumber is switch-off" Value="OF" />
                                                            <asp:ListItem Text="Contact Not established" Value="NE" />
                                                            <asp:ListItem Text="Call-back later" Value="CL" />
                                                            <asp:ListItem Text="Call Disconnected" Value="CD" />
                                                            <asp:ListItem Text="Language Barrier" Value="LB" />
                                                        </asp:DropDownList>
                                                        <div>
                                                            <asp:RequiredFieldValidator ID="rfvdisposition" runat="server" Display="Dynamic" 
                                                                ControlToValidate="DDLStatus" ErrorMessage="Select Calling Status" 
                                                                InitialValue="0" ValidationGroup="init"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </td>
                                                    <td align="center">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        Remark :&nbsp; </td>
                                                    <td align="left" valign="top">
                                                        <asp:TextBox ID="txtdisposeRemark" runat="server" CssClass="txtboxtxt" 
                                                            Height="50px" MaxLength="500" TextMode="MultiLine" ValidationGroup="grp" 
                                                            Width="200px"></asp:TextBox>
                                                        <div>
                                                            <asp:RequiredFieldValidator ID="rfvdisposeremark" runat="server" 
                                                                ControlToValidate="txtdisposeRemark" Display="Dynamic" 
                                                                ErrorMessage="Enter Remark." ValidationGroup="init"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </td>
                                                    <td align="center">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td align="left" valign="top">
                                                        <asp:Button ID="btnLogCall" runat="server" CssClass="btn" 
                                                            OnClick="btnLogCall_Click" Text="Log Call" ValidationGroup="init" 
                                                            Width="100px" />
                                                    </td>
                                                    <td align="center">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td align="left" colspan="2" style="width: 171px" valign="top">
                                                        &nbsp;</td>
                                                    <td align="left" valign="top">
                                                        &nbsp;</td>
                                                    <td align="center">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                    </td>
                                </tr>
                            </table>
                   <%--     </asp:Panel>--%>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

