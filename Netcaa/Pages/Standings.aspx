<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Inherits="netcaa.Pages.Standings" Title="Standings" Codebehind="Standings.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">
Standings
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="Server">
    <p>
        <asp:DropDownList ID="ddlSeasons" runat="server">
        </asp:DropDownList>
        <asp:Button ID="btnGo" runat="server" Text="Go" OnClick="btnGo_Click"></asp:Button></p>
    <asp:DataGrid ID="dgStandings" runat="server" CssClass="grid" AutoGenerateColumns="False"
        ShowHeader="False" Font-Size="Smaller">
        <Columns>
            <asp:HyperLinkColumn DataNavigateUrlField="TeamId" DataNavigateUrlFormatString="TeamPage.aspx?TeamId={0}"
                DataTextField="Team"></asp:HyperLinkColumn>
            <asp:BoundColumn DataField="Wins">
                <ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="Losses">
                <ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
</asp:Content>
