<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Alerts.ascx.cs" Inherits="netcaa.Controls.Alerts" %>
<link href="Styles\netcaa.css" type="text/css" rel="stylesheet"></link>
<asp:Panel ID="Panel1" BackColor="White" BorderColor="Silver" BorderStyle="Solid"
    runat="server">
    <center>
        <strong>Alerts</strong></center>
    <asp:DataList ID="dlAlerts" runat="server" CellPadding="4" ForeColor="#333333" RepeatColumns="1"
        ShowFooter="False" ShowHeader="False" CssClass="subtlegrid">
        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <ItemStyle BackColor="White" ForeColor="Red" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <ItemTemplate>
            <asp:Label ID="messageLabel" runat="server" Text='<%# Eval("message") %>'></asp:Label><br />
        </ItemTemplate>
    </asp:DataList></asp:Panel>
