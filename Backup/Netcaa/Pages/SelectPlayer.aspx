<%@ Page Language="c#" Inherits="netcaa.Pages.SelectPlayer" CodeBehind="SelectPlayer.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="navbar" Src="/Controls/navbar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="footer" Src="/Controls/footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>SelectPlayer</title>
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
                <p>
                    <asp:Label ID="lblPageTitle" runat="server" CssClass="pagetitle">Select Player</asp:Label></p>
                <hr align="left" width="100%" color="red" size="1">
                <p>
                </p>
                <p>
                    <asp:Panel ID="pnlSelection" runat="server" Height="98px">
                        <table id="Table2" cellspacing="5" cellpadding="5" border="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblPositions" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rblPositions_SelectedIndexChanged">
                                        <asp:ListItem Value="%G%" Selected="True">Guards</asp:ListItem>
                                        <asp:ListItem Value="%F%">Forwards</asp:ListItem>
                                        <asp:ListItem Value="%C%">Centers</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:ListBox ID="lbPlayers" runat="server" Width="248px" Height="160px"></asp:ListBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="right" colspan="2">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click">
                                    </asp:Button>
                                    <p>
                                    </p>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </p>
                <asp:Panel ID="pnlConfirmation" runat="server" Height="44px" Visible="False">
                    <p>
                        &nbsp;</p>
                    <p>
                        <asp:Label ID="lblPickInfo" runat="server">Label</asp:Label></p>
                    <p>
                        Press Ok to confirm your selection.</p>
                    <p>
                        Enter a comment if you like (shows up in the notification email):</p>
                    <p>
                        <asp:TextBox ID="tbComments" runat="server" Width="416px" Height="46px"></asp:TextBox></p>
                    <p>
                        <asp:Button ID="btnOk" runat="server" Text="Ok" OnClick="btnOk_Click"></asp:Button>&nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click">
                        </asp:Button></p>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <uc1:footer ID="Footer1" runat="server"></uc1:footer>
    </form>
</body>
</html>
