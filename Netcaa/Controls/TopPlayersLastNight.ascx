<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopPlayersLastNight.ascx.cs"
    Inherits="netcaa.Controls.TopPlayersLastNight" %>
<link href="Styles\netcaa.css" type="text/css" rel="stylesheet" />
<asp:Panel ID="Panel1" runat="server" BorderStyle="Solid" BorderColor="Silver" BackColor="White">
    <center>
        <strong>Top Performances</strong></center>
    <center>
        <strong>
            <asp:Label ID="lblDate" runat="server" CssClass="italicitem" Font-Size="XX-Small"></asp:Label>
        </strong>
    </center>
    <asp:DataGrid ID="dgTopPerformances" runat="server" CssClass="subtlegrid" AutoGenerateColumns="False"
        ShowHeader="False" GridLines="None">
        <Columns>
            <asp:BoundColumn DataField="TeamAbbrev" HeaderText="Team"></asp:BoundColumn>
            <asp:BoundColumn DataField="Player" HeaderText="Player"></asp:BoundColumn>
            <asp:BoundColumn DataField="TotalPoints" HeaderText="Points"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
</asp:Panel>
