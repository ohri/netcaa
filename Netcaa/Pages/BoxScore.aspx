<%@ Register TagPrefix="uc1" TagName="navbar" Src="/Controls/navbar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="footer" Src="/Controls/footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Scoreboard" Src="/Controls/Scoreboard.ascx" %>

<%@ Page Language="c#" Inherits="netcaa.Pages.BoxScore" CodeBehind="BoxScore.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Box Score</title>
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
                <uc1:Scoreboard id="Scoreboard1" runat="server">
                </uc1:Scoreboard>
                <br />
                <br />
                <br />
                <asp:Label ID="lblPageTitle" runat="server" CssClass="pagetitle">Box Score</asp:Label>
                <hr align="left" width="100%" color="red" size="1">
                <br />
                <br />
                <asp:Label ID="lblGameScore" runat="server"></asp:Label>
                <br />
                <br />
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="height: 21px">
                            <center>
                                <asp:Label ID="lblAway" runat="server" Font-Bold="True" Font-Size="Larger">Label</asp:Label></center>
                        </td>
                        <td style="height: 21px">
                            <center>
                                <asp:Label ID="lblHome" runat="server" Font-Bold="True" Font-Size="Larger">Label</asp:Label></center>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DataGrid ID="dgAway" runat="server" CssClass="grid" AutoGenerateColumns="False"
                                Width="300px">
                                <HeaderStyle CssClass="gridheader"></HeaderStyle>
                                <Columns>
                                    <asp:BoundColumn DataField="Status"></asp:BoundColumn>
                                    <asp:HyperLinkColumn DataNavigateUrlField="PlayerId" DataNavigateUrlFormatString="DetailedStats.aspx?PlayerId={0}"
                                        DataTextField="Player" HeaderText="Player"></asp:HyperLinkColumn>
                                    <asp:BoundColumn DataField="Offense" HeaderText="O">
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="Defense" HeaderText="D">
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:BoundColumn>
                                </Columns>
                            </asp:DataGrid>
                        </td>
                        <td>
                            <asp:DataGrid ID="dgHome" runat="server" CssClass="grid" AutoGenerateColumns="False"
                                Width="300px">
                                <HeaderStyle CssClass="gridheader"></HeaderStyle>
                                <Columns>
                                    <asp:BoundColumn DataField="Status"></asp:BoundColumn>
                                    <asp:HyperLinkColumn DataNavigateUrlField="PlayerId" DataNavigateUrlFormatString="DetailedStats.aspx?PlayerId={0}"
                                        DataTextField="Player" HeaderText="Player"></asp:HyperLinkColumn>
                                    <asp:BoundColumn DataField="Offense" HeaderText="O">
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="Defense" HeaderText="D">
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:BoundColumn>
                                </Columns>
                            </asp:DataGrid>
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
