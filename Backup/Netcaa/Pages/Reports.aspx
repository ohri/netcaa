<%@ Page Title="Reports" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="netcaa.Pages.Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="Server">
    Reports
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <asp:DropDownList ID="ddlReports" runat="server">
    </asp:DropDownList>&nbsp;<asp:DropDownList ID="ddlSeasons" runat="server">
    </asp:DropDownList>&nbsp;
    <asp:Button ID="ButtonShowReport" runat="server" Text="Show"
        OnClick="ButtonShowReport_Click"></asp:Button><br /><br />
    <asp:DataGrid ID="dgReportOutput" runat="server" CssClass="grid" AutoGenerateColumns="False"
        PageSize="20" AllowPaging="True" Font-Size="Smaller">
        <HeaderStyle CssClass="gridheader"></HeaderStyle>
        <Columns>
            <asp:TemplateColumn HeaderText="Rank">
                <ItemTemplate>
                    <asp:Label ID="lblRowCount" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="Player" SortExpression="Player" ReadOnly="True" HeaderText="Player">
            </asp:BoundColumn>
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
            <asp:HyperLinkColumn DataNavigateUrlField="PlayerId" DataNavigateUrlFormatString="DetailedStats.aspx?PlayerId={0}"
                DataTextField="NetPPG" SortExpression="NetPPG" HeaderText="NetPPG" DataTextFormatString="{0:0.0}">
                <ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:HyperLinkColumn>
            <asp:BoundColumn DataField="NetPPM" HeaderText="NetPPM" DataFormatString="{0:0.00}">
                <ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:BoundColumn>
        </Columns>
        <PagerStyle NextPageText="=&amp;gt;" PrevPageText="&amp;lt;=" PageButtonCount="12"></PagerStyle>
    </asp:DataGrid>
</asp:Content>
