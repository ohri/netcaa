<%@ Register TagPrefix="uc1" TagName="navbar" Src="/Controls/navbar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="footer" Src="/Controls/footer.ascx" %>

<%@ Page Language="c#" Inherits="netcaa.Pages.Scoring" CodeBehind="Scoring.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Scoring</title>
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
                    <asp:Label ID="lblPageTitle" runat="server" CssClass="pagetitle">Scoring Management</asp:Label>
                </p>
                <hr align="left" width="100%" color="red" size="1">
                <p>
                </p>
                <p>
                    Please don&#39;t touch this page unless you know exactly what you&#39;re doing.</p>
                <p>
                    Week:
                    <asp:DropDownList ID="ddlWeeks" runat="server">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:Button ID="btnAutosub" runat="server" OnClick="btnAutosub_Click" OnClientClick="return confirm('Are you sure you want to autosub?');"
                        Text="Autosub" />
                    &nbsp;
                    <asp:Button ID="ButtonFinalize" runat="server" Text="Finalize Scores" OnClick="ButtonFinalize_Click"
                        OnClientClick="return confirm('Are you sure you want to finalize?');"></asp:Button></p>
                <div class="navdivider">
                </div>
                <asp:Calendar ID="calStatDate" runat="server"></asp:Calendar>
                <p>
                    <asp:Button ID="ButtonProcessDaily" runat="server" Text="Get Stats" OnClick="ButtonProcessDaily_Click">
                    </asp:Button></p>
                <p>
                    <asp:TextBox ID="tbScrapeResults" runat="server" ReadOnly="True" Width="272px" Height="239px"
                        TextMode="MultiLine"></asp:TextBox></p>
            </td>
        </tr>
    </table>
    <uc1:footer ID="Footer1" runat="server"></uc1:footer>
    </form>
</body>
</html>
