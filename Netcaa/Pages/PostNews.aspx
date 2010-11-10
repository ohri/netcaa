<%@ Page Language="c#" ValidateRequest="false" Inherits="netcaa.Pages.PostNews" CodeBehind="PostNews.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="footer" Src="/Controls/footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="navbar" Src="/Controls/navbar.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Post News</title>
    <link href="/Styles/netcaa.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table id="Table1" cellpadding="8">
        <tr>
            <td>
                <uc1:navbar id="Navbar1" runat="server">
                </uc1:navbar>
            </td>
            <td>
                <br />
                <p>
                    &nbsp;</p>
                <p>
                    &nbsp;</p>
                <asp:Label ID="lblPageTitle" runat="server" CssClass="pagetitle">Post News</asp:Label>
                <hr align="left" width="100%" color="red" size="1">
                <p>
                    <em><font size="2">Note: you can embed HTML tags in your message if you like.</font></em></p>
                <p>
                    <asp:Table ID="Table2" runat="server">
                        <asp:TableRow>
                            <asp:TableCell>Title</asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="txtTitle" Columns="72" runat="server"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>Message</asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="txtMessage" runat="server" Rows="8" Columns="56" TextMode="MultiLine"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Button ID="ButtonSubmit" runat="server" Text="Submit" OnClick="ButtonSubmit_Click">
                    </asp:Button></p>
            </td>
        </tr>
    </table>
    <uc1:footer id="Footer1" runat="server">
    </uc1:footer></form>
</body>
</html>
