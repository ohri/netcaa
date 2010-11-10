<%@ Register TagPrefix="uc1" TagName="footer" Src="/Controls/footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="navbar" Src="/Controls/navbar.ascx" %>

<%@ Page Language="c#" Inherits="netcaa.Pages.PlayerSearch" CodeBehind="PlayerSearch.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Player Search</title>
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
                <asp:Label ID="lblPageTitle" runat="server" CssClass="pagetitle">Player Search</asp:Label>
                <hr align="left" width="100%" color="red" size="1">
                <p>
                    <asp:DataGrid ID="dgSearchResults" CssClass="grid" runat="server" AutoGenerateColumns="False">
                        <HeaderStyle CssClass="gridheader"></HeaderStyle>
                        <Columns>
                            <asp:HyperLinkColumn DataNavigateUrlField="PlayerId" DataNavigateUrlFormatString="DetailedStats.aspx?PlayerId={0}"
                                DataTextField="Player" HeaderText="Player" SortExpression="Player">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" />
                            </asp:HyperLinkColumn>
                            <asp:BoundColumn DataField="PositionName" HeaderText="Pos">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:HyperLinkColumn DataNavigateUrlField="RealTeamId" DataNavigateUrlFormatString="RealTeamView.aspx?RealTeamId={0}"
                                DataTextField="RealTeam" HeaderText="NBA Team">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:HyperLinkColumn>
                            <asp:HyperLinkColumn DataNavigateUrlField="TeamId" DataNavigateUrlFormatString="TeamPage.aspx?TeamId={0}"
                                DataTextField="TeamAbbrev" HeaderText="NetCAA Team">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:HyperLinkColumn>
                            <asp:BoundColumn DataField="TotalGames" SortExpression="Games" HeaderText="Games">
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="NetPPG" HeaderText="NetPPG" DataFormatString="{0:0.0}">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid></p>
            </td>
        </tr>
    </table>
    <uc1:footer ID="Footer1" runat="server"></uc1:footer>
    </form>
</body>
</html>
