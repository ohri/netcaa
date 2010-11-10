<%@ Register TagPrefix="uc1" TagName="navbar" Src="/Controls/navbar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="footer" Src="/Controls/footer.ascx" %>

<%@ Page Language="c#" Inherits="netcaa.Pages.ChangePassword" CodeBehind="ChangePassword.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Change Password</title>
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
                <p>
                    &nbsp;</p>
                <p>
                    <br />
                    &nbsp;</p>
                <p>
                    <asp:Label ID="lblMessage" runat="server"></asp:Label></p>
                <table border="0" cellpadding="5" cellspacing="5">
                    <tr>
                        <td>
                            Old Password:
                        </td>
                        <td>
                            <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            New Password:
                        </td>
                        <td>
                            <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Repeat New Password:
                        </td>
                        <td>
                            <asp:TextBox ID="txtConfirmNewPass" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right">
                            <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" OnClick="btnChangePassword_Click">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <uc1:footer ID="Footer1" runat="server"></uc1:footer>
    </form>
</body>
</html>
