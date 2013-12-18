<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="netcaa.Pages.login" %>
<%@ Register TagPrefix="uc1" TagName="navbar" Src="/Controls/navbar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="footer" Src="/Controls/footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="/Styles/netcaa.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="Table1" cellpadding="8">
            <tr valign="top">
                <td rowspan="2">
                </td>
                <td>
                </td>
            </tr>
            <tr valign="top">
                <td valign="top">
                    <p>
                        Please log in:</p>
                    <p>
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label></p>
                    <table border="0" cellpadding="5" cellspacing="5">
                        <tr>
                            <td>
                                <p>
                                    Username:</p>
                            </td>
                            <td>
                                <asp:TextBox ID="TextUsername" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <p>
                                    Password:</p>
                            </td>
                            <td>
                                <asp:TextBox ID="TextPassword" runat="server" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="ButtonLogin" runat="server" Text="Go"></asp:Button>
                </td>
            </tr>
            <tr valign="top">
                <td colspan="2">
                </td>
            </tr>
        </table>
        <uc1:footer ID="Footer1" runat="server"></uc1:footer>
    </div>
    </form>
</body>
</html>
