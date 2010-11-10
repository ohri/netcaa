<%@ Register TagPrefix="uc1" TagName="navbar" Src="/Controls/navbar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="footer" Src="/Controls/footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Scoreboard" Src="/Controls/Scoreboard.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopPlayersLastNight" Src="/Controls/TopPlayersLastNight.ascx" %>
<%@ Register TagPrefix="uc1" TagName="alerts" Src="/Controls/alerts.ascx" %>

<%@ Page Language="c#" Inherits="netcaa.Pages.Home" CodeBehind="home.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>NetCAA Home</title>
    <link href="/Styles/netcaa.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table id="Table1" cellpadding="8" bgcolor="white" width="840">
        <tr>
            <td width="160" rowspan="2">
                <uc1:navbar ID="Navbar1" runat="server"></uc1:navbar>
            </td>
            <td width="680" height="50" colspan="2">
                <uc1:Scoreboard ID="Scoreboard1" runat="server"></uc1:Scoreboard>
            </td>
        </tr>
        <tr>
            <td width="560">
                <p>
                    <font size="6">Latest News</font></p>
                <p>
                    <asp:DataList ID="dlNews" runat="server">
                        <ItemTemplate>
                            <div class="navdivider">
                            </div>
                            <strong>
                                <%#	DataBinder.Eval(Container.DataItem,	"NewsTitle") %>
                            </strong>
                            <br />
                            <%#	DataBinder.Eval(Container.DataItem,	"NewsText")	%>
                            <br />
                            <br />
                            <font size="2" style="font-style: italic">Posted
                                <%#	DataBinder.Eval(Container.DataItem,	"WhenSubmitted", "{0:d}") %>
                                by
                                <%#	DataBinder.Eval(Container.DataItem,	"Submitter") %>
                            </font>
                            <br />
                        </ItemTemplate>
                    </asp:DataList></p>
                <div class="navdivider">
                </div>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="PostNews.aspx" Font-Italic="True"
                    Font-Size="Smaller">Post News</asp:HyperLink>
            </td>
            <td width="225" bordercolor="silver">
                <p>
                    <uc1:TopPlayersLastNight ID="TopPlayersLastNight1" runat="server"></uc1:TopPlayersLastNight>
                </p>
                <p>
                    <uc1:alerts ID="Alerts1" runat="server"></uc1:alerts>
                </p>
            </td>
        </tr>
    </table>
    <uc1:footer ID="Footer1" runat="server"></uc1:footer>
    </form>
</body>
</html>
