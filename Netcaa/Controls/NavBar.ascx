<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavBar.ascx.cs" Inherits="netcaa.Controls.NavBar" %>
<table cellpadding="0" border="0" cellspacing="0">
    <tr valign="bottom" width="160px">
        <td valign="bottom">
            <a href="/Pages/home.aspx"><img width="160px" alt="home" border="0" src="/Images/netcaa.jpg" /></a>
        </td>
    </tr>
    <tr valign="top" bgcolor="red">
        <td>
            <br />
            <table border="0" cellpadding="15" cellspacing="0">
                <tr>
                    <td width="130">
                        <div class="navbar">
                            <asp:TextBox ID="txtPlayerSearch" runat="server" Width="48px"></asp:TextBox>
                            <asp:Button ID="ButtonSearch" runat="server" Text="Search" OnClick="ButtonSearch_Click">
                            </asp:Button>
                            <br />
                            <br />
                            <asp:DropDownList ID="ddlTeams" runat="server" Width="90px" Font-Size="X-Small" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlTeams_SelectedIndexChanged">
                            </asp:DropDownList>
                            <div class="navdivider">
                            </div>
                        </div>
                        <div class="navbar">
                            <a href="/Pages/transactions.aspx" class="navtext">Transactions</a><br />
                            <a href="/Pages/lineups.aspx" class="navtext">Submit Lineup</a><br />
                            <a href="/Pages/TradeList.aspx" class="navtext">
                                <asp:Label ID="lblTrades" runat="server" Text="Trades"></asp:Label></a><br />
                            <a href="/Pages/Draft.aspx" class="navtext">Draft</a><br />
                            <a href="/Pages/TeamDraft.aspx" class="navtext">Team Drafts</a><br />
                            <div class="navdivider">
                            </div>
                            <a href="/Pages/Reports.aspx" class="navtext">Reports</a><br />
                            <a href="/Pages/powerratings.aspx" class="navtext">Power Ratings</a><br />
                            <a href="/Pages/schedule.aspx" class="navtext">Schedule</a><br />
                            <a href="/Pages/Standings.aspx" class="navtext">Standings</a><br />
                            <div class="navdivider">
                            </div>
                            <a href="/Pages/Rules.aspx" class="navtext">Rules</a><br />
                            <a href="http://nba.com/players" class="navtext">Position Source</a><br />
                            <div class="navdivider">
                            </div>
                            <a href="http://groups.google.com/group/netcaa" class="navtext">Google Groups</a><br />
                            <a href="/Pages/ChangePassword.aspx" class="navtext">Change Password</a><br />
                            <a href="/Pages/admin.aspx" class="navtext">Admin</a><br />
                        </div>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
        </td>
    </tr>
</table>
