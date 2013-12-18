<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Inherits="netcaa.Pages.DetailedStats"
    Title="Detailed Stats" CodeBehind="DetailedStats.aspx.cs" %>
<%@ Register Assembly="WebChart" Namespace="WebChart" TagPrefix="Web" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Detailed Player Stats
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="Server">
    <br />
    <asp:Label ID="lblPlayer" runat="server" Font-Size="Large" Font-Bold="True" Font-Italic="False">Label</asp:Label><br />
    <br />
    <Web:ChartControl ID="ccMovingAverage" runat="server" BorderStyle="Solid" BorderWidth="1px"
        ChartPadding="30" HasChartLegend="False" Height="330px" Padding="25" ShowTitlesOnBackground="False"
        Width="701px" YCustomEnd="0" YCustomStart="0" YValuesInterval="0" BottomChartPadding="40"
        XTicksInterval="3">
        <YAxisFont Font="Tahoma, 8pt, style=Bold" ForeColor="White" StringFormat="Far,Near,Character,LineLimit" />
        <XTitle ForeColor="White" StringFormat="Center,Far,Character,LineLimit" Text="Game Date" />
        <PlotBackground Color="Gray" />
        <ChartTitle Font="Tahoma, 12pt, style=Bold" ForeColor="White" StringFormat="Center,Far,Character,LineLimit"
            Text="Scoring Average" />
        <Border Color="LightGray" />
        <XAxisFont ForeColor="White" StringFormat="Near,Center,Character,DirectionVertical" />
        <Background Color="SlateGray" />
        <Charts>
            <Web:ScatterChart DateRenderMode="Days" Name="Points" ScatterLines="NoLines" ShowLegend="False"
                XDataType="DateTime">
                <Line DashStyle="Dot" />
                <DataLabels>
                    <Border Color="Transparent" />
                    <Background Color="Transparent" />
                </DataLabels>
            </Web:ScatterChart>
            <Web:ScatterChart DateRenderMode="Days" ShowLineMarkers="False" XDataType="DateTime">
                <DataLabels>
                    <Border Color="Transparent" />
                    <Background Color="Transparent" />
                </DataLabels>
            </Web:ScatterChart>
        </Charts>
        <Legend Position="Bottom"></Legend>
        <YTitle ForeColor="White" StringFormat="Near,Near,Character,LineLimit" Text="Points" />
    </Web:ChartControl><br />
    <table border="0" cellspacing="20">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Font-Size="Large">Season:</asp:Label>
                <asp:DropDownList ID="ddlSeasons" runat="server" AutoPostBack="True">
                </asp:DropDownList>
                <br />
                <br />
                <asp:DataGrid ID="dgSeasonData" runat="server" CssClass="grid" Font-Size="Smaller"
                    AutoGenerateColumns="False">
                    <HeaderStyle CssClass="gridheader"></HeaderStyle>
                    <Columns>
                        <asp:BoundColumn DataField="gamedate" HeaderText="Date" DataFormatString="{0:d}">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="GameMinutes" HeaderText="Min">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="offense" HeaderText="Off">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="defense" HeaderText="Def">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="total" HeaderText="Total">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="OffBonus" HeaderText="O Bonus">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DefBonus" HeaderText="D Bonus">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Opponent" HeaderText="Opponent">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Font-Size="Large">Career</asp:Label><br />
                <asp:DataGrid ID="dgCareerData" runat="server" CssClass="grid" Font-Size="Smaller"
                    AutoGenerateColumns="False">
                    <HeaderStyle CssClass="gridheader"></HeaderStyle>
                    <Columns>
                        <asp:BoundColumn DataField="season" HeaderText="Season">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="totalgames" HeaderText="Games">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="PointAverage" HeaderText="Average" DataFormatString="{0:0.0}">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NetPPM" HeaderText="NetPPM" DataFormatString="{0:0.00}">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
                <br />
                <br />
                <asp:Label ID="Label3" runat="server" Font-Size="Large">Best Games</asp:Label><br />
                <asp:DataGrid ID="dgBestGames" runat="server" CssClass="grid" Font-Size="Smaller"
                    AutoGenerateColumns="False">
                    <HeaderStyle CssClass="gridheader"></HeaderStyle>
                    <Columns>
                        <asp:BoundColumn DataField="GameDate" HeaderText="Date" DataFormatString="{0:d}">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="GameMinutes" HeaderText="Min">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="offense" HeaderText="Off">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="defense" HeaderText="Def">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="total" HeaderText="Total">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
            </td>
        </tr>
    </table>
</asp:Content>
