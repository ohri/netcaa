<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="RealTeamView.aspx.cs" Inherits="netcaa.Pages.RealTeamView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    <asp:Label id="lblPageTitle" runat="server" CssClass="pagetitle">NBA Team View</asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <asp:DataGrid ID="dgRoster" runat="server" CssClass="grid" AutoGenerateColumns="False">
        <HeaderStyle CssClass="gridheader"></HeaderStyle>
        <Columns>
            <asp:HyperLinkColumn DataNavigateUrlField="PlayerId" DataNavigateUrlFormatString="DetailedStats.aspx?PlayerId={0}"
                DataTextField="Player" HeaderText="Player">
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Left" />
            </asp:HyperLinkColumn>
            <asp:BoundColumn DataField="PositionName" HeaderText="Pos">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundColumn>
            <asp:HyperLinkColumn DataNavigateUrlField="TeamId" DataNavigateUrlFormatString="TeamPage.aspx?TeamId={0}"
                DataTextField="TeamAbbrev" HeaderText="Team"></asp:HyperLinkColumn>
            <asp:BoundColumn DataField="TotalGames" HeaderText="Games">
                <ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="PointAverage" HeaderText="NetPPG" DataFormatString="{0:0.0}">
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" HorizontalAlign="Right" />
            </asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
</asp:Content>
