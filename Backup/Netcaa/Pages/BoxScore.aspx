<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="BoxScore.aspx.cs" Inherits="netcaa.Pages.BoxScore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
    <asp:Label ID="lblPageTitle" runat="server" CssClass="pagetitle">Box Score</asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Main" runat="server">
    <asp:Label ID="lblGameScore" runat="server"></asp:Label>
    <br />
    <br />
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="height: 21px">
                <center>
                    <asp:Label ID="lblAway" runat="server" Font-Bold="True" Font-Size="Larger">Label</asp:Label></center>
            </td>
            <td style="height: 21px">
                <center>
                    <asp:Label ID="lblHome" runat="server" Font-Bold="True" Font-Size="Larger">Label</asp:Label></center>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DataGrid ID="dgAway" runat="server" CssClass="grid" AutoGenerateColumns="False"
                    Width="350px" onitemdatabound="dg_ItemDataBound" >
                    <HeaderStyle CssClass="gridheader"></HeaderStyle>
                    <Columns>
                        <asp:BoundColumn DataField="Status"></asp:BoundColumn>
                        <asp:HyperLinkColumn DataNavigateUrlField="PlayerId" DataNavigateUrlFormatString="DetailedStats.aspx?PlayerId={0}"
                            DataTextField="Player" HeaderText="Player"></asp:HyperLinkColumn>
                        <asp:BoundColumn DataField="Offense" HeaderText="O">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Defense" HeaderText="D">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundColumn>
                    </Columns>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Size="Small" Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid>
            </td>
            <td>
                <asp:DataGrid ID="dgHome" runat="server" CssClass="grid" AutoGenerateColumns="False"
                    Width="350px" onitemdatabound="dg_ItemDataBound" >
                    <HeaderStyle CssClass="gridheader"></HeaderStyle>
                    <Columns>
                        <asp:BoundColumn DataField="Status"></asp:BoundColumn>
                        <asp:HyperLinkColumn DataNavigateUrlField="PlayerId" DataNavigateUrlFormatString="DetailedStats.aspx?PlayerId={0}"
                            DataTextField="Player" HeaderText="Player"></asp:HyperLinkColumn>
                        <asp:BoundColumn DataField="Offense" HeaderText="O">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Defense" HeaderText="D">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundColumn>
                    </Columns>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Size="Small" Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid>
            </td>
        </tr>
    </table>
</asp:Content>
