<%@ Register TagPrefix="uc1" TagName="navbar" Src="/Controls/navbar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="footer" Src="/Controls/footer.ascx" %>

<%@ Page Language="c#" Inherits="netcaa.Pages.SelectPlayers" CodeBehind="SelectPlayers.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Select Players</title>
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
                <asp:Label ID="lblPageTitle" runat="server" CssClass="pagetitle">Select Players</asp:Label>
                <hr align="left" width="100%" color="red" size="1">
                <font face="Arial" size="6">
                    <table border="0">
                        <tr>
                            <td style="width: 120px">
                                <asp:RadioButtonList ID="rblPositions" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rblPositions_SelectedIndexChanged">
                                    <asp:ListItem Value="%G%" Selected="True">Guards</asp:ListItem>
                                    <asp:ListItem Value="%F%">Forwards</asp:ListItem>
                                    <asp:ListItem Value="%C%">Centers</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                <asp:ListBox ID="lbPlayers" runat="server" Width="224px" Height="176px" SelectionMode="Multiple">
                                </asp:ListBox>
                            </td>
                        </tr>
                    </table>
                    <center>
                        <asp:Button ID="btnSelect" runat="server" Text="Select" OnClick="btnSelect_Click">
                        </asp:Button></center>
                    <br />
                </font>
            </td>
        </tr>
    </table>
    <uc1:footer ID="Footer1" runat="server"></uc1:footer>
    </form>
</body>
</html>
