<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Inherits="netcaa.Pages.TeamHistory"
    Title="Team History" CodeBehind="TeamHistory.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    <asp:Label ID="lblPageTitle" runat="server" CssClass="pagetitle">Team History</asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="Server">
    <asp:DataGrid ID="dgFranchiseRecord" runat="server" CssClass="grid" AutoGenerateColumns="False">
        <HeaderStyle CssClass="gridheader"></HeaderStyle>
        <Columns>
            <asp:BoundColumn DataField="season" HeaderText="Season"></asp:BoundColumn>
            <asp:BoundColumn DataField="wins" HeaderText="Wins">
                <ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="losses" HeaderText="Losses">
                <ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="DivisionWinner" HeaderText="Won Division">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="result" HeaderText="Result"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
</asp:Content>
