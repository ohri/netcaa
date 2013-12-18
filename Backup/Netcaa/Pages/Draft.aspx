<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    Inherits="netcaa.Pages.draft" Title="Draft" Codebehind="Draft.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
Draft
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="Server">
    <p>
        NOTE: The time listed is the beginning of the window of time that pick may be made
        (unless the previous pick has already been made). It is NOT the time the pick is
        due. Times are listed in EDT.</p>
    <p>
        <asp:DropDownList ID="ddlSeasons" runat="server">
        </asp:DropDownList>
        <asp:Button ID="btnGo" runat="server" Text="Go" OnClick="btnGo_Click"></asp:Button>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; <em><span style="font-size: 10pt">Current time:</span><span style="font-size: 9pt">
        </span></em>
        <asp:Label ID="lblCurrentTime" runat="server" Font-Italic="True" Font-Size="Small"
            Text="Label" Width="67px"></asp:Label></p>
    <p>
        <asp:DataGrid ID="dgDraft" runat="server" CssClass="grid" AutoGenerateColumns="False"
            ShowHeader="False">
            <HeaderStyle Font-Bold="True"></HeaderStyle>
            <Columns>
                <asp:BoundColumn DataField="Pick" ReadOnly="True" HeaderText="Pick" DataFormatString="{0}">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="PickTime" ReadOnly="True" HeaderText="Time" DataFormatString="{0}">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="OwningTeamAbbrev" ReadOnly="True" HeaderText="Owning"
                    DataFormatString="{0}"></asp:BoundColumn>
                <asp:BoundColumn DataField="OriginalTeamAbbrev" ReadOnly="True" HeaderText="Original"
                    DataFormatString="{0}"></asp:BoundColumn>
                <asp:BoundColumn DataField="Player" ReadOnly="True" HeaderText="Player" DataFormatString="{0}">
                </asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="DraftPickId" ReadOnly="True" HeaderText="PickId"
                    DataFormatString="{0}"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid></p>
</asp:Content>
