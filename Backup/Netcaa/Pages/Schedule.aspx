<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Inherits="netcaa.Pages.Schedule"
    Title="Schedule" CodeBehind="Schedule.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Schedule
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="Server">
    <p>
        <asp:DropDownList ID="ddlSeasons" runat="server">
        </asp:DropDownList>
        <asp:Button ID="btnGo" runat="server" Text="Go" OnClick="btnGo_Click"></asp:Button></p>
    <asp:DataGrid ID="dgSchedules" runat="server" CssClass="grid" AutoGenerateColumns="False">
        <HeaderStyle CssClass="gridheader"></HeaderStyle>
        <Columns>
            <asp:BoundColumn DataField="Week" HeaderText="Week"></asp:BoundColumn>
            <asp:BoundColumn DataField="StartDate" HeaderText="Start" 
                DataFormatString="{0:d}"></asp:BoundColumn>
            <asp:BoundColumn DataField="HomeTeam" HeaderText="Home"></asp:BoundColumn>
            <asp:BoundColumn DataField="VisitingTeam" HeaderText="Away"></asp:BoundColumn>
            <asp:BoundColumn DataField="NumGames" HeaderText="Games"></asp:BoundColumn>
            <asp:HyperLinkColumn DataNavigateUrlField="GameId" DataNavigateUrlFormatString="boxscore.aspx?GameId={0}"
                DataTextField="GameResult" HeaderText="Result"></asp:HyperLinkColumn>
        </Columns>
    </asp:DataGrid>
</asp:Content>
