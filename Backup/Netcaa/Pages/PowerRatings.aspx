<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Inherits="netcaa.Pages.PowerRatings"
    Title="Power Ratings" CodeBehind="PowerRatings.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Power Ratings
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="Server">
    <p>
        <asp:DropDownList ID="ddlSeasons" runat="server">
        </asp:DropDownList>
        <asp:Button ID="btnGo" runat="server" Text="Go" OnClick="btnGo_Click"></asp:Button></p>
    <asp:DataGrid ID="dgPowerRatings" runat="server" AllowSorting="True" CssClass="grid">
        <HeaderStyle CssClass="gridheader"></HeaderStyle>
    </asp:DataGrid>
</asp:Content>
