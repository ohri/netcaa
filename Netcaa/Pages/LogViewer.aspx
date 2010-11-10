<%@ Register TagPrefix="uc1" TagName="footer" Src="/Controls/footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="navbar" Src="/Controls/navbar.ascx" %>

<%@ Page Language="c#" Inherits="netcaa.Pages.LogViewer" CodeBehind="LogViewer.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Log Viewer</title>
    <link href="/Styles/netcaa.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table id="Table1" cellpadding="8">
        <tr>
            <td>
                <uc1:navbar ID="Navbar1" runat="server"></uc1:navbar>
            </td>
            <td>
                <br />
                <p>
                    &nbsp;</p>
                <p>
                    &nbsp;</p>
                <asp:Label ID="lblPageTitle" runat="server" CssClass="pagetitle">Log Viewer</asp:Label>
                <hr align="left" width="100%" color="red" size="1">
                <p>
                    <font face="Arial" size="6">
                        <asp:DataGrid ID="dgLogEntries" runat="server" CssClass="grid" AllowPaging="True"
                            PageSize="20" Font-Size="Small">
                            <HeaderStyle CssClass="gridheader"></HeaderStyle>
                        </asp:DataGrid></font></p>
            </td>
        </tr>
    </table>
    <uc1:footer ID="Footer1" runat="server"></uc1:footer>
    </form>
</body>
</html>
