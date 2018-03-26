<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExportToText.aspx.cs" Inherits="ExportToText" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td align="center" style="font-family: Verdana;">
                    <b>
                        <%=Session["HeaderText"]%></b>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
