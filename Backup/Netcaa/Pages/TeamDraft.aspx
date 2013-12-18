<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    Inherits="netcaa.Pages.TeamDraft" Title="Team Draft" Codebehind="TeamDraft.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Team Draft History
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="Server">
    <br />
    <asp:DropDownList ID="ddlTeams" runat="server">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlSeasons" runat="server">
    </asp:DropDownList>
    <asp:Button ID="btnGo" runat="server" Text="Go" OnClick="btnGo_Click" /><br />
    <br />
    <asp:GridView ID="gvDraft" runat="server" CssClass="grid" AutoGenerateColumns="False">
        <HeaderStyle CssClass="gridheader"></HeaderStyle>
        <Columns>
            <asp:BoundField DataField="Pick" ReadOnly="True" HeaderText="Pick" DataFormatString="{0}" />
            <asp:BoundField DataField="PickTime" ReadOnly="True" HeaderText="Time" DataFormatString="{0}" />
            <asp:BoundField DataField="OriginalTeamAbbrev" ReadOnly="True" HeaderText="Original"
                DataFormatString="{0}" />
            <asp:HyperLinkField DataNavigateUrlFields="PlayerId" 
                DataNavigateUrlFormatString="DetailedStats.aspx?PlayerId={0}" 
                DataTextField="Player" DataTextFormatString="{0}" HeaderText="Player" />
            <asp:BoundField Visible="False" DataField="DraftPickId" ReadOnly="True" HeaderText="PickId"
                DataFormatString="{0}" />
        </Columns>
    </asp:GridView>
</asp:Content>
