<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Inherits="netcaa.Pages.TradeList"
    Title="Trades" CodeBehind="TradeList.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Trades
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:DropDownList ID="ddlTeams" runat="server" />
    &nbsp;&nbsp;
    <asp:Button ID="btnInitiateTrade" runat="server" Text="New Trade" OnClick="btnInitiateTrade_Click" />
    <br />
    <br />
    <i><span class="style1">Click on the gray banners to show/hide trades.</span></i>
    <cc1:CollapsiblePanelExtender ID="cpeActiveTrades" TargetControlID="pnlContentActiveTrades"
        CollapseControlID="imgActiveTrades" ExpandControlID="imgActiveTrades" SuppressPostBack="true"
        Collapsed="false" ScrollContents="false" runat="server">
    </cc1:CollapsiblePanelExtender>
    <cc1:CollapsiblePanelExtender ID="cpeCompletedTrades" TargetControlID="pnlContentCompletedTrades"
        CollapseControlID="imgCompletedTrades" ExpandControlID="imgCompletedTrades" SuppressPostBack="true"
        Collapsed="true" ScrollContents="false" runat="server">
    </cc1:CollapsiblePanelExtender>
    <asp:Panel ID="pnlTitleActiveTrades" runat="server" CssClass="style1">
        <asp:Image ID="imgActiveTrades" runat="server" ImageUrl="~/images/active_trades2.png"
            Style="cursor: pointer" />
    </asp:Panel>
    <asp:Panel ID="pnlContentActiveTrades" runat="server">
        <table border="0" cellpadding="5" cellspacing="5">
            <tr>
                <td>
                    <asp:DataGrid ID="dgActiveTrades" runat="server" CssClass="grid" AutoGenerateColumns="False"
                        Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="Small"
                        Font-Strikeout="False" Font-Underline="False">
                        <HeaderStyle Font-Bold="True"></HeaderStyle>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="Small"
                            Font-Strikeout="False" Font-Underline="False" />
                        <Columns>
                            <asp:BoundColumn DataField="LastUpdated" ReadOnly="True" HeaderText="Date" DataFormatString="{0:M/d/yy}">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TradeStatus" ReadOnly="True" HeaderText="Status" DataFormatString="{0}">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TradeContents" ItemStyle-Width="370px" ReadOnly="True"
                                HeaderText="Description" DataFormatString="{0}"></asp:BoundColumn>
                            <asp:HyperLinkColumn DataNavigateUrlField="TradeId" DataNavigateUrlFormatString="tradepropose.aspx?TradeId={0}"
                                Text="Edit"></asp:HyperLinkColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlTitleCompletedTrades" runat="server">
        <asp:Image ID="imgCompletedTrades" runat="server" ImageUrl="~/images/completed_trades.png"
            Style="cursor: pointer" />
    </asp:Panel>
    <asp:Panel ID="pnlContentCompletedTrades" runat="server">
        <table border="0" cellpadding="5" cellspacing="5">
            <tr>
                <td>
                    <asp:DataGrid ID="dgCompletedTrades" runat="server" CssClass="grid" AutoGenerateColumns="False"
                        Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="Small"
                        Font-Strikeout="False" Font-Underline="False">
                        <HeaderStyle Font-Bold="True"></HeaderStyle>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="Small"
                            Font-Strikeout="False" Font-Underline="False" />
                        <Columns>
                            <asp:BoundColumn DataField="LastUpdated" ReadOnly="True" HeaderText="Date" DataFormatString="{0:M/d/yy}">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TradeStatus" ReadOnly="True" HeaderText="Result" DataFormatString="{0}">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TradeContents" ItemStyle-Width="370px" ReadOnly="True"
                                HeaderText="Description" DataFormatString="{0}"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <br />
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1
        {
            font-size: x-small;
        }
    </style>
</asp:Content>
