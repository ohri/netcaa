<%@ Page Language="c#" Inherits="netcaa.Pages.Lineups" CodeBehind="Lineups.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="footer" Src="/Controls/footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="navbar" Src="/Controls/navbar.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Lineup Submission</title>
    <link href="/Styles/netcaa.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <script language="javascript" type="text/javascript" src="/Scripts/jquery-1.4.1.js"></script>
    <form id="Form1" method="post" runat="server">
    <table id="Table1" cellpadding="8">
        <tr>
            <td>
                <uc1:navbar ID="Navbar1" runat="server"></uc1:navbar>
            </td>
            <td>
                <br />
                <p>&nbsp;</p>
                <p>&nbsp;</p>
                <asp:Label ID="lblPageTitle" runat="server" CssClass="pagetitle">Lineup Submission</asp:Label>
                <hr align="left" width="100%" color="red" size="1" />
                <p></p>
                <div>
                    <asp:Panel ID="PlayerSelection" runat="server" Visible="False" BorderStyle="None"
                        Height="396px">
                        <p>
                            <asp:Label ID="Label1" runat="server" Height="40px" Width="672px">Instructions: Select a player from the Roster listbox on the left and click on the desired place on the depth chart on the right. Note: the tool will not stop you from putting in a bogus lineup!</asp:Label></p>
                        <p>
                            <asp:Label ID="lblGameHeader" runat="server" Font-Bold="True" Font-Italic="True"></asp:Label></p>
                        <div>
                            <table id="Table2" cellpadding="10" border="0">
                                <tr>
                                    <td>
                                        <p><strong><u>Roster</u></strong></p>
                                        <p><asp:ListBox ID="lbRoster" runat="server" SelectionMode="Single" Rows="12"></asp:ListBox></p>
                                    </td>
                                    <td>
                                        <p><strong><u>Starters</u></strong></p>
                                        <strong>PG:</strong>
                                        <asp:Label ID="lblPG" runat="server" CssClass="handcursor" BorderStyle="Solid" Width="160px"
                                            Font-Size="X-Small" BorderColor="Silver" BackColor="#E0E0E0" BorderWidth="1px"
                                            onClick='$("#hiddenSPG").val( $("#lbRoster").val() );$("#lblPG").html( $("#lbRoster :selected").text() );'>
                                            Pick 'n Click</asp:Label><br />
                                        <br />
                                        <strong>SG</strong>:
                                        <asp:Label ID="lblSG" runat="server" CssClass="handcursor" BorderStyle="Solid" Width="160px"
                                            Font-Size="X-Small" BorderColor="Silver" BackColor="#E0E0E0" BorderWidth="1px"
                                            onClick='$("#hiddenSSG").val( $("#lbRoster").val() );$("#lblSG").html( $("#lbRoster :selected").text() );'>
                                            Pick 'n Click</asp:Label><br />
                                        <br />
                                        <strong>SF</strong>:
                                        <asp:Label ID="lblSF" runat="server" CssClass="handcursor" BorderStyle="Solid" Width="160px"
                                            Font-Size="X-Small" BorderColor="Silver" BackColor="#E0E0E0" BorderWidth="1px"
                                            onClick='$("#hiddenSSF").val( $("#lbRoster").val() );$("#lblSF").html( $("#lbRoster :selected").text() );'>
                                            Pick 'n Click</asp:Label><br />
                                        <br />
                                        <strong>PF</strong>:
                                        <asp:Label ID="lblPF" runat="server" CssClass="handcursor" BorderStyle="Solid" Width="160px"
                                            Font-Size="X-Small" BorderColor="Silver" BackColor="#E0E0E0" BorderWidth="1px"
                                            onClick='$("#hiddenSPF").val( $("#lbRoster").val() );$("#lblPF").html( $("#lbRoster :selected").text() );'>
                                            Pick 'n Click</asp:Label><br />
                                        <br />
                                        <strong>C&nbsp; </strong>:
                                        <asp:Label ID="lblC" runat="server" CssClass="handcursor" BorderStyle="Solid" Width="160px"
                                            Font-Size="X-Small" BorderColor="Silver" BackColor="#E0E0E0" BorderWidth="1px"
                                            onClick='$("#hiddenSC").val( $("#lbRoster").val() );$("#lblC").html( $("#lbRoster :selected").text() );'>
                                            Pick 'n Click</asp:Label>
                                    </td>
                                    <td>
                                        <p><strong><u>Backups</u></strong></p>
                                        <strong>PG</strong>:
                                        <asp:Label ID="lblBackupPG" runat="server" CssClass="handcursor" BorderStyle="Solid"
                                            Width="160px" Font-Size="X-Small" BorderColor="Silver" BackColor="#E0E0E0" BorderWidth="1px"
                                            onClick='$("#hiddenBPG").val( $("#lbRoster").val() );$("#lblBackupPG").html( $("#lbRoster :selected").text() );'>
                                            Pick 'n Click</asp:Label><br />
                                        <br />
                                        <strong>SG</strong>:
                                        <asp:Label ID="lblBackupSG" runat="server" CssClass="handcursor" BorderStyle="Solid"
                                            Width="160px" Font-Size="X-Small" BorderColor="Silver" BackColor="#E0E0E0" BorderWidth="1px"
                                            onClick='$("#hiddenBSG").val( $("#lbRoster").val() );$("#lblBackupSG").html( $("#lbRoster :selected").text() );'>
                                            Pick 'n Click</asp:Label><br />
                                        <br />
                                        <strong>SF</strong>:
                                        <asp:Label ID="lblBackupSF" runat="server" CssClass="handcursor" BorderStyle="Solid"
                                            Width="160px" Font-Size="X-Small" BorderColor="Silver" BackColor="#E0E0E0" BorderWidth="1px"
                                            onClick='$("#hiddenBSF").val( $("#lbRoster").val() );$("#lblBackupSF").html( $("#lbRoster :selected").text() );'>
                                            Pick 'n Click</asp:Label><br />
                                        <br />
                                        <strong>PF</strong>:
                                        <asp:Label ID="lblBackupPF" runat="server" CssClass="handcursor" BorderStyle="Solid"
                                            Width="160px" Font-Size="X-Small" BorderColor="Silver" BackColor="#E0E0E0" BorderWidth="1px"
                                            onClick='$("#hiddenBPF").val( $("#lbRoster").val() );$("#lblBackupPF").html( $("#lbRoster :selected").text() );'>
                                            >Pick 'n Click</asp:Label><br />
                                        <br />
                                        <strong>C&nbsp;</strong>&nbsp;:
                                        <asp:Label ID="lblBackupC" runat="server" CssClass="handcursor" BorderStyle="Solid"
                                            Width="160px" Font-Size="X-Small" BorderColor="Silver" BackColor="#E0E0E0" BorderWidth="1px"
                                            onClick='$("#hiddenBC").val( $("#lbRoster").val() );$("#lblBackupC").html( $("#lbRoster :selected").text() );'>
                                            Pick 'n Click</asp:Label>
                                    </td>
                                    <td>
                                        <p>
                                            <strong><u>Garbage</u></strong></p>
                                        <strong>1</strong>:
                                        <asp:Label ID="lblGarbage1" runat="server" CssClass="handcursor" BorderStyle="Solid"
                                            Width="160px" Font-Size="X-Small" BorderColor="Silver" BackColor="#E0E0E0" BorderWidth="1px"
                                            onClick='$("#hiddenG1").val( $("#lbRoster").val() );$("#lblGarbage1").html( $("#lbRoster :selected").text() );'>
                                            Pick 'n Click</asp:Label><br />
                                        <br />
                                        <strong>2</strong>:
                                        <asp:Label ID="lblGarbage2" runat="server" CssClass="handcursor" BorderStyle="Solid"
                                            Width="160px" Font-Size="X-Small" BorderColor="Silver" BackColor="#E0E0E0" BorderWidth="1px"
                                            onClick='$("#hiddenG2").val( $("#lbRoster").val() );$("#lblGarbage2").html( $("#lbRoster :selected").text() );'>
                                            Pick 'n Click</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <center><font size="3"><strong><u>Comments</u></strong></font></center>
                        <center><font size="2">&nbsp;(will show up in email):</font></center>
                        <br />
                        <center><asp:TextBox ID="tbComment" Rows="4" runat="server" TextMode="MultiLine"  Width="360px" /></center>
                        <br />
                        <center>
                            <asp:Button ID="ButtonSubmitLineup" runat="server" Text="Submit" OnClick="ButtonSubmitLineup_Click"></asp:Button>
                            <asp:Literal ID="litHiddenPlaceholder" runat="server"></asp:Literal>
                        </center>
                    </asp:Panel>
                    <br />
                    <asp:Panel ID="TeamWeekSelection" runat="server" BorderStyle="None">
                        <table cellspacing="20" border="0">
                            <tr>
                                <th>
                                    Select team:
                                </th>
                                <th>
                                    Select week:
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:ListBox ID="lbTeams" runat="server" Rows="16"></asp:ListBox>
                                </td>
                                <td>
                                    <asp:ListBox ID="lbWeeks" runat="server" Rows="19"></asp:ListBox>
                                </td>
                            </tr>
                        </table>
                        <center>
                            <asp:Button ID="ButtonSubmitTeamWeek" runat="server" Text="Submit" OnClick="ButtonSubmitTeamWeek_Click">
                            </asp:Button></center>
                    </asp:Panel>
                </div>
                <p></p>
                <p></p>
                <p></p>
            </td>
        </tr>
    </table>
    <uc1:footer ID="Footer1" runat="server"></uc1:footer>
    </form>
</body>
</html>
