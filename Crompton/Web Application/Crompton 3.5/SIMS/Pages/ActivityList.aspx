<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ActivityList.aspx.cs" Inherits="SIMS_Pages_ActivityList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Activities</title>
     <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/global.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border=0>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
 
        <tr>
            <td align="center" style="width: 90%">
                <asp:GridView ID="gvActivity" runat="server" AutoGenerateColumns="False"
                   AlternatingRowStyle-CssClass="fieldName"
                 HeaderStyle-CssClass="fieldNamewithbgcolor" RowStyle-CssClass="gridbgcolor" 
                    HeaderStyle-VerticalAlign="Top" EmptyDataText="No Data Found..">
                   <RowStyle CssClass="gridbgcolor" />
                    <Columns>
                        <asp:BoundField DataField="Activity_Code" HeaderText="Activity Code" />
                        <asp:BoundField DataField="Activity_Description" HeaderText="Activity Description" />
                    </Columns>
                    <HeaderStyle CssClass="fieldNamewithbgcolor" VerticalAlign="Top" />
                    <AlternatingRowStyle CssClass="fieldName" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
