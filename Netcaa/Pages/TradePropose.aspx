<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Inherits="netcaa.Pages.TradePropose"
    Title="Trade" CodeBehind="TradePropose.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="Server">
    Propose Trade
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Main" runat="Server">
    <table border="0" cellpadding="5" cellspacing="5">
        <tr>
            <td>
                <asp:Label ID="lblTeamA" runat="server" Text="From Team A" Font-Bold="True"></asp:Label>
                <asp:Panel ID="Panel1" runat="server" BackColor="#CCCCCC" Width="200px">
                    <asp:CheckBoxList ID="cblAssetsA" runat="server" Font-Size="X-Small">
                    </asp:CheckBoxList>
                </asp:Panel>
            </td>
            <td>
                <asp:Label ID="lblTeamB" runat="server" Text="From Team A" Font-Bold="True"></asp:Label>
                <asp:Panel ID="Panel2" runat="server" Width="200px" BackColor="#CCCCCC">
                    <asp:CheckBoxList ID="cblAssetsB" runat="server" Font-Size="X-Small">
                    </asp:CheckBoxList>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Discussion (private)"></asp:Label><br />
                <asp:TextBox ID="tbComments" Width="200px" runat="server" Rows="2" Font-Bold="true"
                    TextMode="MultiLine"></asp:TextBox><br />
                <asp:TextBox ID="tblLastComment" Width="200px" runat="server" Text="" Font-Italic="True"
                    ReadOnly="True" Rows="4" TextMode="MultiLine" Enabled="False"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Conditional Text (publicly visible)"></asp:Label>
                <asp:TextBox ID="tbConditionals" Width="200px" runat="server" Rows="7" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
    <asp:Button ID="btnPropose" runat="server" Text="Propose" OnClick="btnPropose_Click" />&nbsp;
    <asp:Button ID="btnConfirm" runat="server" Text="Confirm" OnClick="btnConfirm_Click" />&nbsp;
    <asp:Button ID="btnReject" runat="server" Text="Reject" OnClick="btnReject_Click" />&nbsp;
    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
</asp:Content>
