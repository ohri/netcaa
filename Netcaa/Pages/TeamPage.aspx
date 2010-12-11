<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Inherits="netcaa.Pages.TeamPage"
    ValidateRequest="false" Title="Team Page" CodeBehind="TeamPage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    <asp:Label ID="lblPageTitle" runat="server" CssClass="pagetitle"></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="Server">
    <table border="0" cellpadding="10" cellspacing="10">
        <tr>
            <td>
                <p>
                    <font size="3">
                        <asp:Literal ID="litEmailAddress" runat="server"></asp:Literal>
                        <asp:Literal ID="litOwnerInfo" runat="server"></asp:Literal>
                        <asp:Literal ID="litEditLink" runat="server"></asp:Literal>
                    </font>
                </p>
                </td><td>
                <p>
                    <font size="3">Record:
                        <asp:Label ID="lblRecord" runat="server"></asp:Label>
                    </font>
                    <br/>
                    <font size="3">
                        <asp:HyperLink ID="hlTeamHistory" runat="server" Font-Size="Smaller" NavigateUrl="TeamHistory.aspx?TeamId=">View History</asp:HyperLink>
                    </font>
                </p>
                <p>
                    <font size="2">Last Login:
                        <asp:Label ID="lblLastLogin" runat="server"></asp:Label>
                    </font>
                </p>
            </td></tr><tr>
            <td colspan="2" width="600">
                <asp:DataGrid ID="dgRoster" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    CssClass="grid">
                    <HeaderStyle CssClass="gridheader"></HeaderStyle>
                    <Columns>
                        <asp:HyperLinkColumn DataNavigateUrlField="PlayerId" DataNavigateUrlFormatString="DetailedStats.aspx?PlayerId={0}"
                            DataTextField="Player" HeaderText="Name">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:HyperLinkColumn>
                        <asp:BoundColumn DataField="Position" HeaderText="Pos">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:HyperLinkColumn DataNavigateUrlField="RealTeamId" DataNavigateUrlFormatString="RealTeamView.aspx?RealTeamId={0}"
                            DataTextField="RealTeam" HeaderText="Team"></asp:HyperLinkColumn>
                        <asp:BoundColumn DataField="NetPPG" HeaderText="NetPPG" DataFormatString="{0:0.0}">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NetPPM" HeaderText="NetPPM" DataFormatString="{0:0.00}">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="LastGame" HeaderText="Last Game">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="Status">
                            <ItemTemplate>
                                <center>
                                    <asp:Literal ID="litIR" runat="server"></asp:Literal></center>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
            </td>
        </tr>
    </table>
</asp:Content>
